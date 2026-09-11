#define AppName "Bluetooth Notify"
#define AppExeName "BluetoothNotify.App.exe"
#define AppUserModelId "BluetoothNotify.App"
#define AppMutexName "Local\BluetoothNotify.App.Singleton"
#ifndef DotNetDesktopRuntimePattern
  #define DotNetDesktopRuntimePattern "10.0.*"
#endif

#ifndef WindowsAppRuntimePackage
  #define WindowsAppRuntimePackage "Microsoft.WindowsAppRuntime.1.8"
#endif

#ifndef WindowsAppRuntimeMinimumVersion
  #define WindowsAppRuntimeMinimumVersion "8000.946.1701.0"
#endif

#ifndef PublishDir
  #define PublishDir "..\artifacts\publish\win-x64"
#endif

#ifndef OutputDir
  #define OutputDir "..\artifacts\installer"
#endif

#ifndef AppVersion
  #define AppVersion GetFileVersion(PublishDir + "\" + AppExeName)
#endif

[Setup]
AppId={{9E5F51D4-E8B7-4B01-9933-F6B066CDFB94}
AppName={#AppName}
AppVersion={#AppVersion}
AppVerName={#AppName} {#AppVersion}
AppPublisher=vyach-vasiliev
AppPublisherURL=https://github.com/vyach-vasiliev/
AppSupportURL=https://github.com/vyach-vasiliev/bluetooth-notify/issues
AppUpdatesURL=https://github.com/vyach-vasiliev/bluetooth-notify/releases
DefaultDirName={localappdata}\Programs\Bluetooth Notify
DefaultGroupName=Bluetooth Notify
DisableProgramGroupPage=yes
PrivilegesRequired=lowest
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
MinVersion=10.0.22621
OutputDir={#OutputDir}
OutputBaseFilename=BluetoothNotify-Setup-{#AppVersion}-x64
SetupIconFile=..\src\BluetoothNotify.App\Assets\BluetoothNotify.ico
UninstallDisplayIcon={app}\{#AppExeName}
UninstallDisplayName={#AppName}
Compression=lzma2/max
SolidCompression=yes
WizardStyle=modern
CloseApplications=yes
CloseApplicationsFilter={#AppExeName}
RestartApplications=no
SetupLogging=yes
UsePreviousAppDir=yes
VersionInfoVersion={#AppVersion}
VersionInfoCompany=vyach-vasiliev
VersionInfoDescription={#AppName} Setup
VersionInfoProductName={#AppName}
VersionInfoProductVersion={#AppVersion}
AppReadmeFile={app}\Legal\index.html

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"; LicenseFile: "legal\terms.en.txt"; InfoBeforeFile: "legal\privacy-disclaimer.en.txt"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"; LicenseFile: "legal\terms.ru.txt"; InfoBeforeFile: "legal\privacy-disclaimer.ru.txt"

[CustomMessages]
english.LaunchProgram=Launch %1
russian.LaunchProgram=Запустить %1
english.DotNetDesktopRuntime=.NET Desktop Runtime 10 (x64)
russian.DotNetDesktopRuntime=.NET Desktop Runtime 10 (x64)
english.WindowsAppRuntime=Windows App Runtime 1.8 (x64), version {#WindowsAppRuntimeMinimumVersion} or later
russian.WindowsAppRuntime=Windows App Runtime 1.8 (x64), версия {#WindowsAppRuntimeMinimumVersion} или новее
english.PrerequisitesMissing=Bluetooth Notify cannot be installed because these required components are missing:%n%n%1%n%nThey are not included in this installer. Open the official download page(s) now?
russian.PrerequisitesMissing=Установка Bluetooth Notify невозможна: отсутствуют обязательные компоненты:%n%n%1%n%nОни не входят в установщик. Открыть официальные страницы загрузки?
english.CloseApplicationFailed=Bluetooth Notify is still running. Exit it from the tray icon, then try again.
russian.CloseApplicationFailed=Bluetooth Notify всё ещё запущен. Завершите его через значок в трее и повторите попытку.
english.RemoveUserData=Also remove your Bluetooth Notify settings and logs?%n%nChoose No to keep them for a future installation.
russian.RemoveUserData=Также удалить настройки и журналы Bluetooth Notify?%n%nВыберите «Нет», чтобы сохранить их для будущей установки.
english.StartupTasks=Startup:
russian.StartupTasks=Автозагрузка:
english.StartWithWindows=Start Bluetooth Notify with Windows
russian.StartWithWindows=Запускать Bluetooth Notify вместе с Windows

[Tasks]
Name: "startup"; Description: "{cm:StartWithWindows}"; GroupDescription: "{cm:StartupTasks}"

[Registry]
Root: HKCU; Subkey: "Software\BluetoothNotify"; ValueType: dword; ValueName: "RunAtStartup"; ValueData: "1"; Flags: uninsdeletevalue uninsdeletekeyifempty; Tasks: startup
Root: HKCU; Subkey: "Software\BluetoothNotify"; ValueType: dword; ValueName: "RunAtStartup"; ValueData: "0"; Flags: uninsdeletevalue uninsdeletekeyifempty; Tasks: not startup
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "{#AppName}"; ValueData: """{app}\{#AppExeName}"" --autostart"; Flags: uninsdeletevalue; Tasks: startup
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueType: none; ValueName: "{#AppName}"; Flags: deletevalue; Tasks: not startup

[Files]
Source: "{#PublishDir}\*"; DestDir: "{app}"; Excludes: "*.pdb"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#AppName}"; Filename: "{app}\{#AppExeName}"; WorkingDir: "{app}"; AppUserModelID: "{#AppUserModelId}"

[Run]
Filename: "{app}\{#AppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(AppName, '&', '&&')}}"; WorkingDir: "{app}"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
Type: filesandordirs; Name: "{localappdata}\BluetoothNotify"; Check: ShouldRemoveUserData

[Code]
const
  DotNetDownloadUrl = 'https://dotnet.microsoft.com/en-us/download/dotnet/10.0';
  WindowsAppRuntimeDownloadUrl = 'https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads-archive#version-18';

var
  RemoveUserData: Boolean;

function ShouldEnableStartup: Boolean;
var
  Enabled: Cardinal;
begin
  if RegQueryDWordValue(HKCU, 'Software\BluetoothNotify',
    'RunAtStartup', Enabled) then
    Result := Enabled <> 0
  else
    Result := True;
end;

procedure InitializeWizard;
begin
  if ShouldEnableStartup then
    WizardSelectTasks('startup')
  else
    WizardSelectTasks('!startup');
end;

function HasDotNetDesktopRuntime: Boolean;
var
  DotNetRoot: String;
  RuntimeDirectory: String;
  FindRec: TFindRec;
begin
  Result := False;
  if not RegQueryStringValue(HKLM64,
    'SOFTWARE\dotnet\Setup\InstalledVersions\x64\sharedhost', 'Path', DotNetRoot) then
    DotNetRoot := ExpandConstant('{pf64}\dotnet');

  RuntimeDirectory := AddBackslash(DotNetRoot) +
    'shared\Microsoft.WindowsDesktop.App';
  if FindFirst(AddBackslash(RuntimeDirectory) + '{#DotNetDesktopRuntimePattern}', FindRec) then
  begin
    try
      repeat
        if ((FindRec.Attributes and FILE_ATTRIBUTE_DIRECTORY) <> 0) and
          (Pos('-', FindRec.Name) = 0) and
          FileExists(AddBackslash(RuntimeDirectory) + FindRec.Name +
            '\Microsoft.WindowsDesktop.App.deps.json') then
        begin
          Result := True;
          Exit;
        end;
      until not FindNext(FindRec);
    finally
      FindClose(FindRec);
    end;
  end;
end;

function HasWindowsAppRuntime: Boolean;
var
  ResultCode: Integer;
  PowerShellPath: String;
  Parameters: String;
begin
  PowerShellPath := ExpandConstant(
    '{sys}\WindowsPowerShell\v1.0\powershell.exe');
  Parameters := '-NoLogo -NoProfile -NonInteractive -ExecutionPolicy Bypass ' +
    '-Command "$p = @(Get-AppxPackage -Name ''' +
    '{#WindowsAppRuntimePackage}' +
    ''' -ErrorAction SilentlyContinue | Where-Object { $_.Architecture -eq ''X64'' ' +
    '-and $_.Version -ge [Version]''' +
    '{#WindowsAppRuntimeMinimumVersion}' +
    ''' }); if ($p.Count -gt 0) { exit 0 } else { exit 1 }"';
  if not FileExists(PowerShellPath) then
  begin
    Result := False;
    Exit;
  end;
  Result := Exec(PowerShellPath, Parameters, '', SW_HIDE,
    ewWaitUntilTerminated, ResultCode) and (ResultCode = 0);
end;

procedure OpenDownloadPage(const Url: String);
var
  ErrorCode: Integer;
begin
  ShellExec('open', Url, '', '', SW_SHOWNORMAL, ewNoWait, ErrorCode);
end;

function InitializeSetup: Boolean;
var
  Missing: String;
  DotNetMissing: Boolean;
  AppRuntimeMissing: Boolean;
begin
  DotNetMissing := not HasDotNetDesktopRuntime;
  AppRuntimeMissing := not HasWindowsAppRuntime;

  if DotNetMissing then
    Missing := '- ' + CustomMessage('DotNetDesktopRuntime');
  if AppRuntimeMissing then
  begin
    if Missing <> '' then
      Missing := Missing + #13#10;
    Missing := Missing + '- ' + CustomMessage('WindowsAppRuntime');
  end;

  Result := Missing = '';
  if not Result then
  begin
    if SuppressibleMsgBox(
      FmtMessage(CustomMessage('PrerequisitesMissing'), [Missing]),
      mbError, MB_YESNO, IDNO) = IDYES then
    begin
      if DotNetMissing then
        OpenDownloadPage(DotNetDownloadUrl);
      if AppRuntimeMissing then
        OpenDownloadPage(WindowsAppRuntimeDownloadUrl);
    end;
  end;
end;

function WaitForApplicationExit: Boolean;
var
  Attempt: Integer;
begin
  for Attempt := 1 to 50 do
  begin
    if not CheckForMutexes('{#AppMutexName}') then
    begin
      Result := True;
      Exit;
    end;
    Sleep(200);
  end;
  Result := not CheckForMutexes('{#AppMutexName}');
end;

function StopRunningApplication: Boolean;
var
  ResultCode: Integer;
  ExecutablePath: String;
begin
  if not CheckForMutexes('{#AppMutexName}') then
  begin
    Result := True;
    Exit;
  end;

  ExecutablePath := ExpandConstant('{app}\{#AppExeName}');
  if FileExists(ExecutablePath) then
    Exec(ExecutablePath, '--exit', ExpandConstant('{app}'), SW_HIDE,
      ewWaitUntilTerminated, ResultCode);
  Result := WaitForApplicationExit;
end;

function PrepareToInstall(var NeedsRestart: Boolean): String;
begin
  if StopRunningApplication then
    Result := ''
  else
    Result := CustomMessage('CloseApplicationFailed');
end;

function InitializeUninstall: Boolean;
begin
  Result := StopRunningApplication;
  if not Result then
  begin
    SuppressibleMsgBox(CustomMessage('CloseApplicationFailed'), mbError,
      MB_OK, IDOK);
    Exit;
  end;

  RemoveUserData := DirExists(ExpandConstant('{localappdata}\BluetoothNotify')) and
    (SuppressibleMsgBox(CustomMessage('RemoveUserData'), mbConfirmation,
      MB_YESNO, IDNO) = IDYES);
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  if CurUninstallStep = usUninstall then
  begin
    RegDeleteValue(HKCU, 'Software\Microsoft\Windows\CurrentVersion\Run',
      '{#AppName}');
    RegDeleteValue(HKCU, 'Software\BluetoothNotify', 'RunAtStartup');
    RegDeleteKeyIfEmpty(HKCU, 'Software\BluetoothNotify');
  end;
end;

function ShouldRemoveUserData: Boolean;
begin
  Result := RemoveUserData;
end;
