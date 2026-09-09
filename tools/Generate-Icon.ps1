param(
    [string]$OutputDirectory = (Join-Path $PSScriptRoot '..\src\BluetoothNotify.App\Assets')
)

Add-Type -AssemblyName System.Drawing

function Write-BluetoothIcon {
    param(
        [string]$OutputPath,
        [System.Drawing.Color]$ForegroundColor,
        [string]$PngOutputPath
    )

    $size = 64
    $bitmap = [System.Drawing.Bitmap]::new($size, $size, [System.Drawing.Imaging.PixelFormat]::Format32bppArgb)
    $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
    $graphics.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::AntiAlias
    $graphics.TextRenderingHint = [System.Drawing.Text.TextRenderingHint]::AntiAliasGridFit
    $graphics.Clear([System.Drawing.Color]::Transparent)

    $foreground = [System.Drawing.SolidBrush]::new($ForegroundColor)
    $font = [System.Drawing.Font]::new('Segoe Fluent Icons', 56, [System.Drawing.FontStyle]::Regular, [System.Drawing.GraphicsUnit]::Pixel)
    $format = [System.Drawing.StringFormat]::new()
    $format.Alignment = [System.Drawing.StringAlignment]::Center
    $format.LineAlignment = [System.Drawing.StringAlignment]::Center

    $graphics.DrawString([char]0xE702, $font, $foreground, [System.Drawing.RectangleF]::new(0, -2, $size, $size + 4), $format)

    if ($PngOutputPath) {
        $bitmap.Save($PngOutputPath, [System.Drawing.Imaging.ImageFormat]::Png)
    }

    $pngStream = [System.IO.MemoryStream]::new()
    $bitmap.Save($pngStream, [System.Drawing.Imaging.ImageFormat]::Png)
    $pngBytes = $pngStream.ToArray()
    $stream = [System.IO.FileStream]::new($OutputPath, [System.IO.FileMode]::Create, [System.IO.FileAccess]::Write)
    $writer = [System.IO.BinaryWriter]::new($stream)
    $writer.Write([uint16]0)
    $writer.Write([uint16]1)
    $writer.Write([uint16]1)
    $writer.Write([byte]$size)
    $writer.Write([byte]$size)
    $writer.Write([byte]0)
    $writer.Write([byte]0)
    $writer.Write([uint16]1)
    $writer.Write([uint16]32)
    $writer.Write([uint32]$pngBytes.Length)
    $writer.Write([uint32]22)
    $writer.Write($pngBytes)
    $writer.Flush()

    $writer.Dispose()
    $stream.Dispose()
    $pngStream.Dispose()
    $format.Dispose()
    $font.Dispose()
    $foreground.Dispose()
    $graphics.Dispose()
    $bitmap.Dispose()
}

[System.IO.Directory]::CreateDirectory($OutputDirectory) | Out-Null
$darkPath = Join-Path $OutputDirectory 'BluetoothNotify.Dark.ico'
$lightPath = Join-Path $OutputDirectory 'BluetoothNotify.Light.ico'
$defaultPath = Join-Path $OutputDirectory 'BluetoothNotify.ico'
$notificationPath = Join-Path $OutputDirectory 'BluetoothNotify.Notification.png'

$darkForeground = [System.Drawing.Color]::FromArgb(255, 76, 147, 255)
$lightForeground = [System.Drawing.Color]::FromArgb(255, 16, 82, 166)

Write-BluetoothIcon $darkPath $darkForeground $null
Write-BluetoothIcon $lightPath $lightForeground $notificationPath
Copy-Item -LiteralPath $lightPath -Destination $defaultPath -Force

Write-Output $darkPath
Write-Output $lightPath
Write-Output $defaultPath
Write-Output $notificationPath
