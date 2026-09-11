<p align="center">
  <img src="./src/BluetoothNotify.App/Assets/BluetoothNotify.Notification.png" width="72" height="72" alt="Bluetooth Notify-Logo">
</p>

<h1 align="center">Bluetooth Notify</h1>

<p align="center">
  <strong>Akkustand, tatsächlicher Verbindungsstatus und native Benachrichtigungen für Bluetooth-Geräte—direkt im Windows-Infobereich.</strong>
</p>

<p align="center">
  <a href="./README.md">English</a> ·
  <a href="./README.ru.md">Русский</a> ·
  <strong>Deutsch</strong> ·
  <a href="./README.es.md">Español</a> ·
  <a href="./README.fr.md">Français</a> ·
  <a href="./README.pt-PT.md">Português</a> ·
  <a href="./README.ja.md">日本語</a> ·
  <a href="./README.ko.md">한국어</a> ·
  <a href="./README.zh-CN.md">简体中文</a>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Windows_11-22H2%2B-0078D4?logo=windows11&amp;logoColor=white" alt="Windows 11 22H2 oder neuer">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&amp;logoColor=white" alt=".NET 10">
  <img src="https://img.shields.io/badge/UI-WPF-5C2D91" alt="WPF-Benutzeroberfläche">
  <img src="https://img.shields.io/badge/architecture-x64-555555" alt="x64-Architektur">
</p>

Bluetooth Notify ist eine kompakte Windows-11-Tray-App für gekoppelte Bluetooth-Geräte. Sie zeigt den von Windows gemeldeten Verbindungsstatus, liest alle verfügbaren Akkuquellen aus und sendet native Benachrichtigungen, wenn sich ein Gerät verbindet oder einen festgelegten Akkuschwellenwert unterschreitet.

## Vorschau

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/main_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/main_light.png">
    <img src="./resources/main_light.png" width="680" alt="Bluetooth Notify mit gekoppelten Geräten, Verbindungsstatus und Akkuständen">
  </picture>
</p>

### Einstellungen und Infobereich

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/settings_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/settings_light.png">
    <img src="./resources/settings_light.png" width="680" alt="Bluetooth Notify-Einstellungen für Sprache, Design und Akkuwarnungen">
  </picture>
</p>

<p align="center">
  <img src="./resources/tray_light.png" width="478" alt="Bluetooth Notify-Symbol im Windows-Infobereich">
</p>

## Funktionen

- **Alle gekoppelten Geräte auf einen Blick.** Zeigt den aktuellen Verbindungsstatus und den neuesten Akkustand, den Windows bereitstellen kann.
- **Mehrere Akkuquellen.** Die App prüft den Windows-Systemwert, einen HFP/PnP-Fallback und den standardmäßigen Bluetooth-LE-Akkudienst.
- **Hinweise ohne unnötige Störungen.** Eine native Windows-Benachrichtigung erscheint bei einer neuen Verbindung und einmal beim Unterschreiten eines aktivierten mittleren oder niedrigen Schwellenwerts.
- **Für den Infobereich gemacht.** Kurz mit der Maus darüberfahren oder mit links klicken, um das Panel zu öffnen; ein Rechtsklick öffnet das native Menü.
- **Passend zum System.** Dem Windows-Design folgen oder hell bzw. dunkel festlegen.
- **Neun Oberflächensprachen.** Englisch, Russisch, Deutsch, Spanisch, Französisch, Portugiesisch, Japanisch, Koreanisch und vereinfachtes Chinesisch.
- **Lokale Daten.** Einstellungen und rotierende Protokolle bleiben im lokalen Anwendungsdatenordner; ein Konto ist nicht erforderlich.

## Voraussetzungen

Zum Ausführen der Anwendung werden benötigt:

- Windows 11 22H2 (Build 22621) oder neuer, x64;
- [.NET Desktop Runtime 10 x64](https://dotnet.microsoft.com/download/dotnet/10.0);
- [Windows App Runtime 1.8 x64](https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads-archive#version-18).

Die veröffentlichte App ist frameworkabhängig; diese Laufzeitumgebungen sind daher nicht enthalten. Zum Erstellen aus dem Quellcode wird zusätzlich das .NET 10 SDK benötigt.

## Schnellstart aus dem Quellcode

~~~powershell
git clone https://github.com/vyach-vasiliev/bluetooth-notify.git
cd bluetooth-notify

dotnet restore .\BluetoothNotify.slnx
dotnet build .\BluetoothNotify.slnx -c Release
dotnet test .\BluetoothNotify.slnx -c Release
dotnet run --project .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release
~~~

Die App startet im Windows-Infobereich und öffnet kein gewöhnliches Fenster.

## Bedienung

- Etwa 300 ms mit der Maus über dem Bluetooth-Symbol verweilen oder mit links darauf klicken, um das Panel anzuzeigen.
- Mit der rechten Maustaste die Befehle **Öffnen**, **Aktualisieren**, **Einstellungen** und **Beenden** aufrufen.
- <kbd>Esc</kbd> drücken, den Zeiger von Symbol und Panel wegbewegen oder den Fokus wechseln, um das Panel auszublenden.
- Mit der Glockenschaltfläche unten im Panel Verbindungsbenachrichtigungen global ein- oder ausschalten.
- Unter **Einstellungen** Sprache, Darstellung, Warnungen für mittleren und niedrigen Akkustand sowie beide Prozentwerte wählen. Änderungen gelten ohne Neustart.

Im Panel sind gleichzeitig höchstens zwei Gerätekarten sichtbar; weitere Geräte sind per Bildlauf erreichbar. Bei einem getrennten Gerät kann der zuletzt bekannte Akkustand erhalten bleiben. Die App kennzeichnet ihn als veraltet und zählt ihn nicht als aktuelle Akkuangabe.

## Einstellungen und lokale Daten

- Einstellungen: <code>%LocalAppData%\BluetoothNotify\settings.json</code>
- Rotierende Protokolle: <code>%LocalAppData%\BluetoothNotify\logs</code>
- Installierte Anwendung: <code>%LocalAppData%\Programs\Bluetooth Notify</code>

## Datenschutz und rechtliche Hinweise

Namen von Bluetooth-Geräten, Windows-Endpunkt-/Container-IDs, verfügbare Bluetooth-Adressen, Verbindungsstatus und Akkustand werden lokal im Arbeitsspeicher verarbeitet und nicht an den Herausgeber gesendet. Die App schreibt nur Einstellungen und rotierende technische Protokolle (<code>5 × 512 KiB</code>) auf den Datenträger; Fehlermeldungen des Betriebssystems können gelegentlich lokale technische Angaben enthalten.

Datenschutzerklärung, Nutzungsbedingungen und Haftungsausschluss befinden sich in <code>site/public/legal</code> in allen neun App-Sprachen. Dieselbe eigenständige Seite wird unter <code>/legal/</code> veröffentlicht, für „Einstellungen → Rechtliche Hinweise“ in die App eingebunden und in den englischen/russischen Installationsdialogen zusammengefasst. Siehe [Compliance-Übersicht und Veröffentlichungscheckliste (Russisch)](./docs/legal-compliance.md).

## Veröffentlichen und Installationsprogramm erstellen

Frameworkabhängige x64-Veröffentlichung erstellen:

~~~powershell
dotnet publish .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release -r win-x64 --self-contained false
~~~

Die Standardausgabe liegt unter <code>src\BluetoothNotify.App\bin\Release\net10.0-windows10.0.22621.0\win-x64\publish</code>. Die ausführbare Datei muss zusammen mit den DLLs, Abhängigkeitsdateien, PRI-Ressourcen und Bootstrap-/Projektionsbibliotheken des Windows App SDK bleiben.

Für das benutzerbezogene Installationsprogramm zuerst [Inno Setup 6](https://jrsoftware.org/isdl.php) installieren und dann ausführen:

~~~powershell
.\tools\Build-Installer.ps1
~~~

Das Skript stellt Abhängigkeiten wieder her, erstellt und testet das Projekt, veröffentlicht es, prüft, dass die .NET-Laufzeit nicht eingebettet wurde, und kompiliert <code>installer\BluetoothNotify.iss</code>. Die Ausgabe ist <code>artifacts\installer\BluetoothNotify-Setup-&lt;Version&gt;-x64.exe</code>. Die Installationsoberfläche ist auf Englisch und Russisch verfügbar, prüft beide Laufzeiten, benötigt keine Administratorrechte und behält Benutzerdaten bei der Deinstallation standardmäßig bei.

Mit <code>-SkipTests</code> lässt sich der erneute Testlauf überspringen; alternativ kann ein eigener Compilerpfad angegeben werden:

~~~powershell
.\tools\Build-Installer.ps1 -InnoSetupCompiler 'C:\Tools\Inno Setup 6\ISCC.exe'
~~~

## So werden Akku- und Verbindungsstatus ermittelt

1. Zwei dokumentierte WinRT-Watcher überwachen Bluetooth-LE- und Bluetooth-Classic-Geräte.
2. Einträge werden zuerst anhand der Container-ID und danach anhand der Bluetooth-Adresse zusammengeführt, um doppelte Karten zu vermeiden.
3. Die Akkusuche prüft die Windows-Systemeigenschaft, den bestmöglichen HFP/PnP-Wert und den GATT Battery Service <code>0x180F</code> / Battery Level <code>0x2A19</code>.
4. Messwerte werden 20 Sekunden zwischengespeichert; höchstens vier Lesevorgänge laufen gleichzeitig, jeweils mit fünf Sekunden Zeitlimit.
5. Bei sichtbarem Panel erfolgt jede Sekunde eine Aktualisierung. Im ausgeblendeten Zustand prüft die App einmal pro Minute auf Schwellenwertbenachrichtigungen.

Der erste Snapshot löst keine Verbindungsbenachrichtigung aus. Ein Übergang <code>Disconnected → Connected</code> verwendet eine Entprellzeit von drei Sekunden und wird erst nach einer Trennung wieder freigegeben. Eine Akkuwarnung wird einmal ausgelöst, wenn der Wert einen aktivierten Schwellenwert von oben unterschreitet.

<details>
<summary><strong>Architektur für Mitwirkende</strong></summary>

- <code>BluetoothDeviceMonitor</code> — WinRT-Watcher, Snapshots und Verbindungsereignisse.
- <code>DeviceStateMerger</code> — LE-/Classic-Deduplizierung und stabile Identitäten.
- <code>BatteryReader</code> und <code>PnpBatteryReader</code> — geordnete Suche nach Akkuquellen.
- <code>TrayIconService</code> — natives Tray-Symbol, Menü, Hover-Zustand und Wiederherstellung nach einem Explorer-Neustart.
- <code>PanelPlacementService</code> — DPI-bewusste Platzierung an jeder Taskleistenkante und auf mehreren Monitoren.
- <code>TrayPanelViewModel</code> — MVVM-Zustand, Single-Flight-Aktualisierung und Dispatcher-gebundene Sammlungsupdates.
- <code>LocalizationService</code> und <code>ThemeManager</code> — Sprach- und Darstellungswechsel zur Laufzeit.
- <code>NotificationService</code> — Windows-App-SDK-Benachrichtigungen mit AppUserModelID <code>BluetoothNotify.App</code>.
- <code>SettingsStore</code> und <code>AppLogger</code> — atomare JSON-Speicherung und rotierende lokale Protokolle.

</details>

## Bluetooth-Einschränkungen

Der Verbindungsstatus ist schreibgeschützt. Windows stellt keine einheitliche öffentliche API zum Verbinden oder Trennen aller Bluetooth-Geräteklassen bereit. Bluetooth Notify koppelt daher keine Geräte und verwendet weder den Geräte-Manager noch Registrierungsänderungen, zurückentwickelte HID-Befehle oder herstellerspezifische Protokolle.

Die Verfügbarkeit des Akkustands hängt davon ab, was Windows und das Gerät bereitstellen. Die HFP/PnP-Eigenschaft ist ein isolierter, undokumentierter Best-Effort-Fallback; fehlt oder ändert sie sich, funktionieren die anderen Quellen weiter. Für getrennte Geräte kann die App einen klar als veraltet markierten Systemwert anzeigen und öffnet nicht eigens für dessen Aktualisierung eine GATT-Verbindung.

## Tests

~~~powershell
dotnet test .\BluetoothNotify.slnx -c Release
~~~

Die Tests decken LE-/Classic-Zusammenführung, stabile IDs, Sortierung, Symbolzuordnung, Akkuschwellen und fehlende Werte, Verbindungsentprellung, Single-Flight-Aktualisierung, Einstellungen, Lokalisierung, Darstellung und Panelplatzierung bei 100/125/150/200 % DPI ab.

Der [ausführliche Prüfbericht (Russisch)](./docs/verification-report.md) enthält die aufgezeichneten Hardware- und Installationsprüfungen.

## Referenzen

- [Übersicht über Windows-App-Benachrichtigungen](https://learn.microsoft.com/windows/apps/develop/notifications/)
- [Bluetooth-GATT-Client](https://learn.microsoft.com/windows/apps/develop/devices-sensors/gatt-client)
- [Shell_NotifyIcon](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyiconw)
- [Shell_NotifyIconGetRect](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyicongetrect)
- [Hinweise zu Drittanbieterkomponenten](./THIRD-PARTY-NOTICES.md)
