<p align="center">
  <img src="./src/BluetoothNotify.App/Assets/BluetoothNotify.Notification.png" width="72" height="72" alt="Bluetooth Notify logo">
</p>

<h1 align="center">Bluetooth Notify</h1>

<p align="center">
  <strong>Bluetooth battery levels, real connection state, and native alerts—right in the Windows tray.</strong>
</p>

<p align="center">
  <strong>English</strong> ·
  <a href="./README.ru.md">Русский</a> ·
  <a href="./README.de.md">Deutsch</a> ·
  <a href="./README.es.md">Español</a> ·
  <a href="./README.fr.md">Français</a> ·
  <a href="./README.pt-PT.md">Português</a> ·
  <a href="./README.ja.md">日本語</a> ·
  <a href="./README.ko.md">한국어</a> ·
  <a href="./README.zh-CN.md">简体中文</a>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Windows_11-22H2%2B-0078D4?logo=windows11&amp;logoColor=white" alt="Windows 11 22H2 or newer">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&amp;logoColor=white" alt=".NET 10">
  <img src="https://img.shields.io/badge/UI-WPF-5C2D91" alt="WPF user interface">
  <img src="https://img.shields.io/badge/architecture-x64-555555" alt="x64 architecture">
</p>

Bluetooth Notify is a focused Windows 11 tray app for paired Bluetooth devices. It shows the connection state Windows reports, reads every available battery source, and sends native notifications when a device connects or crosses a battery threshold you set.

## See it in action

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/main_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/main_light.png">
    <img src="./resources/main_light.png" width="680" alt="Bluetooth Notify panel showing paired devices, connection state, and battery levels">
  </picture>
</p>

### Settings and tray previews

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/settings_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/settings_light.png">
    <img src="./resources/settings_light.png" width="680" alt="Bluetooth Notify settings for language, theme, and battery alerts">
  </picture>
</p>

<p align="center">
  <img src="./resources/tray_light.png" width="478" alt="Bluetooth Notify icon in the Windows system tray">
</p>

## What it gives you

- **Every paired device at a glance.** See the current connection state and the latest battery level Windows can provide.
- **More ways to find battery data.** The app tries the Windows system value, an HFP/PnP fallback, and the standard Bluetooth LE Battery Service.
- **Alerts without noise.** Get a native Windows notification on a new connection and once when a battery crosses your enabled medium or low threshold.
- **A tray-first workflow.** Hover briefly or left-click to open the panel; right-click for the native menu.
- **An interface that fits your system.** Follow the Windows theme or choose light or dark mode.
- **Nine UI languages.** English, Russian, German, Spanish, French, Portuguese, Japanese, Korean, and Simplified Chinese.
- **Local settings.** Preferences and rotating logs stay under your local application-data folder; no account is required.

## Requirements

To run the application:

- Windows 11 22H2 (build 22621) or newer, x64;
- [.NET Desktop Runtime 10 x64](https://dotnet.microsoft.com/download/dotnet/10.0);
- [Windows App Runtime 1.8 x64](https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads-archive#version-18).

The published app is framework-dependent, so these runtimes are not bundled. To build from source, install the .NET 10 SDK as well.

## Quick start from source

~~~powershell
git clone https://github.com/vyach-vasiliev/bluetooth-notify.git
cd bluetooth-notify

dotnet restore .\BluetoothNotify.slnx
dotnet build .\BluetoothNotify.slnx -c Release
dotnet test .\BluetoothNotify.slnx -c Release
dotnet run --project .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release
~~~

The app starts in the system tray; it does not open a regular window.

## Using the tray app

- Hover over the Bluetooth tray icon for about 300 ms, or left-click it, to show the panel.
- Right-click the icon for **Open**, **Refresh**, **Settings**, and **Exit**.
- Press <kbd>Esc</kbd>, move away from both the icon and panel, or switch focus to hide the panel.
- Use the bell button at the bottom of the panel to enable or disable connection notifications globally.
- Open **Settings** to choose the language, appearance, medium-battery alert, low-battery alert, and both percentage thresholds. Changes apply without restarting.

The panel shows up to two device cards at once; scroll to reach the rest. A disconnected device may retain its last known battery value. The app marks that value as stale and does not count it as a current battery report.

## Settings and local data

- Settings: <code>%LocalAppData%\BluetoothNotify\settings.json</code>
- Rotating logs: <code>%LocalAppData%\BluetoothNotify\logs</code>
- Installed application: <code>%LocalAppData%\Programs\Bluetooth Notify</code>

## Privacy and legal information

Bluetooth device names, Windows endpoint/container IDs, available Bluetooth addresses, connection state, and battery level are processed locally in memory and are not sent to the publisher. Only preferences and rotating technical logs (`5 × 512 KiB`) are written by the app; operating-system error text can occasionally contain local technical details.

The Privacy Policy, Terms of Use, and Disclaimer live in `site/public/legal` in all nine app languages. The same offline page is published at `/legal/index.html` (with `/legal/` supported as an alias), bundled into the app for Settings → Legal information, and summarized in the English/Russian installer screens. See [the compliance map and release checklist](docs/legal-compliance.md).

## Publish and build the installer

Create a framework-dependent x64 publish:

~~~powershell
dotnet publish .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release -r win-x64 --self-contained false
~~~

The default output is under <code>src\BluetoothNotify.App\bin\Release\net10.0-windows10.0.22621.0\win-x64\publish</code>. Keep the executable together with its DLLs, dependency files, PRI resources, and Windows App SDK bootstrap/projection libraries.

To build the per-user installer, install [Inno Setup 6](https://jrsoftware.org/isdl.php), then run:

~~~powershell
.\tools\Build-Installer.ps1
~~~

The script restores, builds, tests, publishes, verifies that the .NET runtime was not bundled, and compiles <code>installer\BluetoothNotify.iss</code>. Its output is <code>artifacts\installer\BluetoothNotify-Setup-&lt;version&gt;-x64.exe</code>. The installer UI is available in English and Russian, checks both required runtimes, installs without administrator rights, and keeps user data by default when uninstalling.

Use <code>-SkipTests</code> to omit the repeated test run, or pass a custom compiler path:

~~~powershell
.\tools\Build-Installer.ps1 -InnoSetupCompiler 'C:\Tools\Inno Setup 6\ISCC.exe'
~~~

## How battery and connection status work

1. Two documented WinRT watchers observe Bluetooth LE and Bluetooth Classic devices.
2. Records are merged first by Container ID and then by Bluetooth address to avoid duplicate cards.
3. Battery lookup tries the Windows system property, the best-effort HFP/PnP value, and GATT Battery Service <code>0x180F</code> / Battery Level <code>0x2A19</code>.
4. Readings are cached for 20 seconds; at most four reads run concurrently, with a five-second timeout.
5. While the panel is visible it refreshes every second. When hidden, the app checks once per minute for threshold notifications.

The first snapshot does not send connection notifications. A <code>Disconnected → Connected</code> transition uses a three-second debounce and becomes eligible again only after a disconnect. A battery warning fires once when the reading crosses an enabled threshold from above.

<details>
<summary><strong>Architecture for contributors</strong></summary>

- <code>BluetoothDeviceMonitor</code> — WinRT watchers, snapshots, and connection events.
- <code>DeviceStateMerger</code> — LE/Classic deduplication and stable identities.
- <code>BatteryReader</code> and <code>PnpBatteryReader</code> — ordered battery-source lookup.
- <code>TrayIconService</code> — native tray icon, menu, hover state, and Explorer recovery.
- <code>PanelPlacementService</code> — DPI-aware placement for every taskbar edge and multiple monitors.
- <code>TrayPanelViewModel</code> — MVVM state, single-flight refresh, and Dispatcher-bound collection updates.
- <code>LocalizationService</code> and <code>ThemeManager</code> — runtime language and appearance changes.
- <code>NotificationService</code> — Windows App SDK notifications with AppUserModelID <code>BluetoothNotify.App</code>.
- <code>SettingsStore</code> and <code>AppLogger</code> — atomic JSON persistence and rotating local logs.

</details>

## Bluetooth limitations

Bluetooth status is read-only. Windows does not expose one public API that can connect or disconnect every Bluetooth device class, so Bluetooth Notify does not pair devices or use Device Manager, registry modifications, reverse-engineered HID commands, or vendor-specific protocols.

Battery availability depends on what Windows and the device expose. The HFP/PnP property is an isolated, undocumented best-effort fallback; if it changes or is missing, the other sources continue to work. For disconnected devices, the app may show a clearly marked stale system value and will not open a GATT connection only to refresh it.

## Tests

~~~powershell
dotnet test .\BluetoothNotify.slnx -c Release
~~~

The suite covers LE/Classic merging, stable IDs, sorting, icon mapping, battery thresholds and missing values, connection debounce, single-flight refresh, settings, localization, appearance, and panel placement at 100/125/150/200% DPI.

See the [detailed verification report (Russian)](./docs/verification-report.md) for the recorded hardware and installer checks.

## License

Copyright © 2026 [vyach-vasiliev](https://github.com/vyach-vasiliev/).

Bluetooth Notify is free and open-source software licensed under the [GNU Affero General Public License v3.0 only](./LICENSE.md) (`AGPL-3.0-only`). Commercial use is permitted. Distribution of the original or a modified covered work must preserve the AGPL and provide the corresponding source as required by the license. Modified versions used for remote network interaction must also offer their users access to the corresponding source.

## References

- [Windows app notifications overview](https://learn.microsoft.com/windows/apps/develop/notifications/)
- [Bluetooth GATT client](https://learn.microsoft.com/windows/apps/develop/devices-sensors/gatt-client)
- [Shell_NotifyIcon](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyiconw)
- [Shell_NotifyIconGetRect](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyicongetrect)
- [Third-party notices](./THIRD-PARTY-NOTICES.md)
