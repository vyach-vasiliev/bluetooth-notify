<p align="center">
  <img src="./src/BluetoothNotify.App/Assets/BluetoothNotify.Notification.png" width="72" height="72" alt="Логотип Bluetooth Notify">
</p>

<h1 align="center">Bluetooth Notify</h1>

<p align="center">
  <strong>Заряд Bluetooth-устройств, фактическое состояние подключения и нативные уведомления — прямо в трее Windows.</strong>
</p>

<p align="center">
  <a href="./README.md">English</a> ·
  <strong>Русский</strong> ·
  <a href="./README.de.md">Deutsch</a> ·
  <a href="./README.es.md">Español</a> ·
  <a href="./README.fr.md">Français</a> ·
  <a href="./README.pt-PT.md">Português</a> ·
  <a href="./README.ja.md">日本語</a> ·
  <a href="./README.ko.md">한국어</a> ·
  <a href="./README.zh-CN.md">简体中文</a>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Windows_11-22H2%2B-0078D4?logo=windows11&amp;logoColor=white" alt="Windows 11 22H2 или новее">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&amp;logoColor=white" alt=".NET 10">
  <img src="https://img.shields.io/badge/UI-WPF-5C2D91" alt="Интерфейс WPF">
  <img src="https://img.shields.io/badge/architecture-x64-555555" alt="Архитектура x64">
</p>

Bluetooth Notify — компактное приложение для трея Windows 11, предназначенное для сопряжённых Bluetooth-устройств. Оно показывает сообщаемое Windows состояние подключения, проверяет все доступные источники заряда и отправляет нативные уведомления при подключении устройства или пересечении заданного порога заряда.

## Как это выглядит

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/main_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/main_light.png">
    <img src="./resources/main_light.png" width="680" alt="Панель Bluetooth Notify с сопряжёнными устройствами, состоянием подключения и уровнем заряда">
  </picture>
</p>

### Настройки и значок в трее

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/settings_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/settings_light.png">
    <img src="./resources/settings_light.png" width="680" alt="Настройки языка, темы и уведомлений о заряде в Bluetooth Notify">
  </picture>
</p>

<p align="center">
  <img src="./resources/tray_light.png" width="478" alt="Значок Bluetooth Notify в системном трее Windows">
</p>

## Возможности

- **Все сопряжённые устройства перед глазами.** Видно текущее состояние подключения и последний доступный Windows уровень заряда.
- **Несколько источников заряда.** Приложение последовательно проверяет системное значение Windows, HFP/PnP fallback и стандартный BLE Battery Service.
- **Уведомления без лишнего шума.** Нативное уведомление появляется при новом подключении и один раз при пересечении включённого среднего или низкого порога заряда.
- **Управление из трея.** Задержите указатель или нажмите левой кнопкой для открытия панели; правая кнопка открывает нативное меню.
- **Системный внешний вид.** Можно следовать теме Windows или явно выбрать светлую либо тёмную тему.
- **Девять языков интерфейса.** Русский, английский, немецкий, испанский, французский, португальский, японский, корейский и упрощённый китайский.
- **Локальные данные.** Настройки и журналы с ротацией хранятся в локальном профиле; учётная запись не нужна.

## Требования

Для запуска приложения нужны:

- Windows 11 22H2 (build 22621) или новее, x64;
- [.NET Desktop Runtime 10 x64](https://dotnet.microsoft.com/download/dotnet/10.0);
- [Windows App Runtime 1.8 x64](https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads-archive#version-18).

Публикация framework-dependent, поэтому эти среды выполнения не входят в комплект. Для сборки из исходного кода дополнительно установите .NET 10 SDK.

## Быстрый запуск из исходного кода

~~~powershell
git clone https://github.com/vyach-vasiliev/bluetooth-notify.git
cd bluetooth-notify

dotnet restore .\BluetoothNotify.slnx
dotnet build .\BluetoothNotify.slnx -c Release
dotnet test .\BluetoothNotify.slnx -c Release
dotnet run --project .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release
~~~

Приложение запускается в системном трее и не открывает обычное окно.

## Управление приложением

- Задержите указатель над значком Bluetooth примерно на 300 мс или нажмите левой кнопкой, чтобы показать панель.
- Нажмите правой кнопкой для команд **Открыть**, **Обновить**, **Настройки** и **Выход**.
- Нажмите <kbd>Esc</kbd>, уведите указатель от значка и панели или переключите фокус, чтобы скрыть панель.
- Кнопка с колокольчиком внизу панели глобально включает или выключает уведомления о подключении.
- В **Настройках** можно выбрать язык, тему, уведомления о среднем и низком заряде и оба процентных порога. Изменения применяются без перезапуска.

Одновременно в панели видны не более двух карточек; остальные доступны прокруткой. Отключённое устройство может сохранять последний известный заряд. Такое значение помечается как устаревшее и не учитывается как текущая передача заряда.

## Настройки и локальные данные

- Настройки: <code>%LocalAppData%\BluetoothNotify\settings.json</code>
- Журналы с ротацией: <code>%LocalAppData%\BluetoothNotify\logs</code>
- Установленное приложение: <code>%LocalAppData%\Programs\Bluetooth Notify</code>

## Конфиденциальность и юридическая информация

Имена Bluetooth-устройств, endpoint/container ID Windows, доступные Bluetooth-адреса, состояние подключения и уровень заряда обрабатываются локально в памяти и не отправляются издателю. Приложение записывает на диск только настройки и технические журналы с ротацией (<code>5 × 512 КиБ</code>); текст ошибок операционной системы иногда может содержать локальные технические сведения.

Политика конфиденциальности, Условия использования и Отказ от ответственности находятся в <code>site/public/legal</code> на всех девяти языках приложения. Та же автономная страница публикуется по адресу <code>/legal/</code>, включается в приложение для раздела «Настройки → Юридическая информация» и кратко изложена на русском и английском экранах установщика. См. [карту соответствия и чек-лист релиза](./docs/legal-compliance.md).

## Публикация и сборка установщика

Создайте framework-dependent публикацию для x64:

~~~powershell
dotnet publish .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release -r win-x64 --self-contained false
~~~

Стандартный результат находится в <code>src\BluetoothNotify.App\bin\Release\net10.0-windows10.0.22621.0\win-x64\publish</code>. Храните исполняемый файл вместе с DLL, файлами зависимостей, PRI-ресурсами и bootstrap/projection-библиотеками Windows App SDK.

Для сборки per-user установщика установите [Inno Setup 6](https://jrsoftware.org/isdl.php), затем выполните:

~~~powershell
.\tools\Build-Installer.ps1
~~~

Скрипт выполняет restore, build, test и publish, проверяет отсутствие встроенного .NET runtime и компилирует <code>installer\BluetoothNotify.iss</code>. Результат: <code>artifacts\installer\BluetoothNotify-Setup-&lt;версия&gt;-x64.exe</code>. Интерфейс установщика доступен на русском и английском, он проверяет обе среды выполнения, не требует прав администратора и по умолчанию сохраняет пользовательские данные при удалении.

Передайте <code>-SkipTests</code>, чтобы не запускать тесты повторно, или укажите нестандартный путь к компилятору:

~~~powershell
.\tools\Build-Installer.ps1 -InnoSetupCompiler 'C:\Tools\Inno Setup 6\ISCC.exe'
~~~

## Как определяется заряд и подключение

1. Два документированных WinRT watcher-а следят за Bluetooth LE и Bluetooth Classic устройствами.
2. Записи объединяются сначала по Container ID, затем по Bluetooth-адресу, чтобы не создавать дубликаты.
3. Проверяются системное свойство Windows, best-effort значение HFP/PnP и GATT Battery Service <code>0x180F</code> / Battery Level <code>0x2A19</code>.
4. Показания кешируются на 20 секунд; одновременно выполняется не более четырёх чтений с тайм-аутом 5 секунд.
5. При открытой панели обновление выполняется каждую секунду, а в скрытом состоянии — раз в минуту для пороговых уведомлений.

Первый snapshot не создаёт уведомлений. Переход <code>Disconnected → Connected</code> защищён debounce в 3 секунды и повторно разрешается только после отключения. Уведомление о заряде отправляется один раз при пересечении включённого порога сверху вниз.

<details>
<summary><strong>Архитектура для разработчиков</strong></summary>

- <code>BluetoothDeviceMonitor</code> — WinRT watcher-ы, snapshots и события подключения.
- <code>DeviceStateMerger</code> — дедупликация LE/Classic и стабильные идентификаторы.
- <code>BatteryReader</code> и <code>PnpBatteryReader</code> — последовательный поиск источника заряда.
- <code>TrayIconService</code> — нативный значок, меню, hover-состояние и восстановление после перезапуска Explorer.
- <code>PanelPlacementService</code> — DPI-aware размещение для любой стороны taskbar и нескольких мониторов.
- <code>TrayPanelViewModel</code> — MVVM-состояние, single-flight refresh и обновления коллекции через Dispatcher.
- <code>LocalizationService</code> и <code>ThemeManager</code> — смена языка и оформления без перезапуска.
- <code>NotificationService</code> — уведомления Windows App SDK с AppUserModelID <code>BluetoothNotify.App</code>.
- <code>SettingsStore</code> и <code>AppLogger</code> — атомарное хранение JSON и локальные журналы с ротацией.

</details>

## Ограничения Bluetooth

Состояние подключения доступно только для чтения. Windows не предоставляет единого публичного API для подключения и отключения всех классов Bluetooth-устройств, поэтому Bluetooth Notify не выполняет pairing и не использует Device Manager, изменения реестра, reverse-engineered HID-команды или vendor-specific протоколы.

Доступность заряда зависит от данных Windows и самого устройства. Свойство HFP/PnP — изолированный недокументированный best-effort fallback; если оно отсутствует или изменится, остальные источники продолжат работать. Для отключённого устройства приложение может показать явно помеченное устаревшее системное значение и не открывает GATT-соединение только ради его обновления.

## Тесты

~~~powershell
dotnet test .\BluetoothNotify.slnx -c Release
~~~

Набор тестов покрывает объединение LE/Classic, стабильные ID, сортировку, выбор иконок, пороги и отсутствие заряда, debounce подключения, single-flight refresh, настройки, локализацию, оформление и размещение панели при 100/125/150/200% DPI.

Результаты аппаратных и установочных проверок приведены в [подробном отчёте](./docs/verification-report.md).

## Лицензия

Copyright © 2026 [vyach-vasiliev](https://github.com/vyach-vasiliev/).

Bluetooth Notify — свободное программное обеспечение с открытым исходным кодом по [GNU Affero General Public License v3.0 only](./LICENSE.md) (`AGPL-3.0-only`). Коммерческое использование разрешено. При распространении оригинального или изменённого covered work необходимо сохранить AGPL и предоставить соответствующий исходный код согласно лицензии. Изменённая версия с удалённым сетевым взаимодействием также должна предлагать пользователям доступ к соответствующему исходному коду.

## Справочные материалы

- [Обзор Windows app notifications](https://learn.microsoft.com/windows/apps/develop/notifications/)
- [Bluetooth GATT client](https://learn.microsoft.com/windows/apps/develop/devices-sensors/gatt-client)
- [Shell_NotifyIcon](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyiconw)
- [Shell_NotifyIconGetRect](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyicongetrect)
- [Уведомления о сторонних компонентах](./THIRD-PARTY-NOTICES.md)
