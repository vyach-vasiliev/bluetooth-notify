[CmdletBinding()]
param(
    [switch]$SkipTests,
    [string]$InnoSetupCompiler
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$projectPath = Join-Path $repoRoot 'src\BluetoothNotify.App\BluetoothNotify.App.csproj'
$solutionPath = Join-Path $repoRoot 'BluetoothNotify.slnx'
$publishDirectory = Join-Path $repoRoot 'artifacts\publish\win-x64'
$installerDirectory = Join-Path $repoRoot 'artifacts\installer'
$installerScript = Join-Path $repoRoot 'installer\BluetoothNotify.iss'
$versionProps = [xml](Get-Content -LiteralPath (Join-Path $repoRoot 'Directory.Build.props') -Raw)
$appVersion = [string]$versionProps.Project.PropertyGroup.Version

if ([string]::IsNullOrWhiteSpace($appVersion)) {
    throw 'Directory.Build.props does not contain a Version property.'
}

function Assert-WorkspaceSubdirectory([string]$Path) {
    $fullPath = [System.IO.Path]::GetFullPath($Path)
    $rootWithSeparator = $repoRoot.TrimEnd('\') + '\'
    if (-not $fullPath.StartsWith($rootWithSeparator, [System.StringComparison]::OrdinalIgnoreCase)) {
        throw "Refusing to modify a directory outside the repository: $fullPath"
    }
}

function Resolve-InnoSetupCompiler([string]$ExplicitPath) {
    if ($ExplicitPath) {
        $resolved = (Resolve-Path -LiteralPath $ExplicitPath).Path
        return $resolved
    }

    $command = Get-Command ISCC.exe -ErrorAction SilentlyContinue
    if ($command) {
        return $command.Source
    }

    $candidates = @(
        (Join-Path $env:LOCALAPPDATA 'Programs\Inno Setup 6\ISCC.exe'),
        (Join-Path ${env:ProgramFiles(x86)} 'Inno Setup 6\ISCC.exe'),
        (Join-Path $env:ProgramFiles 'Inno Setup 6\ISCC.exe')
    ) | Where-Object { $_ }

    foreach ($candidate in $candidates) {
        if (Test-Path -LiteralPath $candidate -PathType Leaf) {
            return $candidate
        }
    }

    throw @'
Inno Setup 6 compiler (ISCC.exe) was not found.
Install it with `winget install --id JRSoftware.InnoSetup --exact`, or pass
`-InnoSetupCompiler C:\Path\To\ISCC.exe`, then run this script again.
'@
}

function Stop-PublishedApplication {
    $publishedExecutable = Join-Path $publishDirectory 'BluetoothNotify.App.exe'
    if (-not (Test-Path -LiteralPath $publishedExecutable -PathType Leaf)) {
        return
    }

    $publishedExecutableFullPath = [System.IO.Path]::GetFullPath($publishedExecutable)
    $running = @(Get-Process -Name 'BluetoothNotify.App' -ErrorAction SilentlyContinue |
        Where-Object {
            $_.Path -and
            [System.IO.Path]::GetFullPath($_.Path).Equals(
                $publishedExecutableFullPath,
                [System.StringComparison]::OrdinalIgnoreCase)
        })
    if (-not $running) {
        return
    }

    $exitRequest = Start-Process -FilePath $publishedExecutable -ArgumentList '--exit' `
        -PassThru -Wait -WindowStyle Hidden
    if ($exitRequest.ExitCode -ne 0) {
        throw "Bluetooth Notify exit request failed with code $($exitRequest.ExitCode)."
    }

    for ($attempt = 0; $attempt -lt 50; $attempt++) {
        $stillRunning = @(Get-Process -Name 'BluetoothNotify.App' -ErrorAction SilentlyContinue |
            Where-Object {
                $_.Path -and
                [System.IO.Path]::GetFullPath($_.Path).Equals(
                    $publishedExecutableFullPath,
                    [System.StringComparison]::OrdinalIgnoreCase)
            })
        if (-not $stillRunning) {
            return
        }
        Start-Sleep -Milliseconds 200
    }

    throw 'Bluetooth Notify is still using the publish directory. Exit it from the tray and retry.'
}

Assert-WorkspaceSubdirectory $publishDirectory
Assert-WorkspaceSubdirectory $installerDirectory
Stop-PublishedApplication

if (Test-Path -LiteralPath $publishDirectory) {
    Remove-Item -LiteralPath $publishDirectory -Recurse -Force
}
New-Item -ItemType Directory -Path $publishDirectory -Force | Out-Null
New-Item -ItemType Directory -Path $installerDirectory -Force | Out-Null
Get-ChildItem -LiteralPath $installerDirectory -Filter 'BluetoothNotify-Setup-*-x64.exe' -File |
    Remove-Item -Force

& dotnet restore $solutionPath
if ($LASTEXITCODE -ne 0) { throw 'dotnet restore failed.' }

& dotnet build $solutionPath -c Release --no-restore
if ($LASTEXITCODE -ne 0) { throw 'dotnet build failed.' }

if (-not $SkipTests) {
    & dotnet test $solutionPath -c Release --no-build
    if ($LASTEXITCODE -ne 0) { throw 'dotnet test failed.' }
}

& dotnet publish $projectPath -c Release -r win-x64 --self-contained false `
    --no-build -o $publishDirectory
if ($LASTEXITCODE -ne 0) { throw 'dotnet publish failed.' }

$requiredFiles = @(
    'BluetoothNotify.App.exe',
    'BluetoothNotify.App.dll',
    'BluetoothNotify.App.deps.json',
    'BluetoothNotify.App.runtimeconfig.json'
)
foreach ($requiredFile in $requiredFiles) {
    if (-not (Test-Path -LiteralPath (Join-Path $publishDirectory $requiredFile))) {
        throw "Publish output is incomplete: $requiredFile is missing."
    }
}

$forbiddenRuntimeFiles = @(
    'coreclr.dll',
    'clrjit.dll',
    'hostfxr.dll',
    'hostpolicy.dll',
    'PresentationFramework.dll'
)
$bundledRuntime = Get-ChildItem -LiteralPath $publishDirectory -Recurse -File |
    Where-Object { $forbiddenRuntimeFiles -contains $_.Name }
if ($bundledRuntime) {
    throw "Framework-dependent publish unexpectedly contains runtime files: $($bundledRuntime.Name -join ', ')"
}

$compiler = Resolve-InnoSetupCompiler $InnoSetupCompiler
& $compiler "/DAppVersion=$appVersion" "/DPublishDir=$publishDirectory" `
    "/DOutputDir=$installerDirectory" $installerScript
if ($LASTEXITCODE -ne 0) { throw 'Inno Setup compilation failed.' }

$installer = Get-ChildItem -LiteralPath $installerDirectory -Filter 'BluetoothNotify-Setup-*-x64.exe' |
    Sort-Object LastWriteTimeUtc -Descending |
    Select-Object -First 1
if (-not $installer) {
    throw 'Inno Setup reported success but no installer was produced.'
}

$hash = Get-FileHash -LiteralPath $installer.FullName -Algorithm SHA256
Write-Host "Installer: $($installer.FullName)"
Write-Host "Version:   $appVersion"
Write-Host "SHA-256:   $($hash.Hash)"
