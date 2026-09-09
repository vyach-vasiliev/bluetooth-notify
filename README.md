# Bluetooth Notify

Небольшое WPF tray-приложение для Windows 11, которое показывает сопряжённые Bluetooth-устройства, фактическое состояние подключения и доступный Windows заряд. При новом подключении приложение отправляет нативное уведомление Windows с иконкой приложения, названием устройства и цветовым индикатором заряда.

## Требования

- Windows 11 22H2 (build 22621) или новее, x64;
- [.NET Desktop Runtime 10 x64](https://dotnet.microsoft.com/download/dotnet/10.0);
- [Windows App Runtime 1.8 x64](https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads) для нативных app notifications.

Публикация framework-dependent: .NET и Windows App Runtime в неё не встроены. Установщик по текущей задаче намеренно не реализован.

## Сборка и запуск

```powershell
dotnet restore .\BluetoothNotify.slnx
dotnet build .\BluetoothNotify.slnx -c Release
dotnet test .\BluetoothNotify.slnx -c Release
dotnet run --project .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release
```

После запуска обычное окно не появляется. Управление выполняется через значок Bluetooth в системном трее:

- задержать указатель примерно на 300 мс или нажать левой кнопкой — показать панель;
- нажать правой кнопкой — «Открыть», «Обновить», «Настройки», «Выход»;
- `Esc`, потеря фокуса или уход указателя от значка и панели — скрыть панель;
- кнопка уведомлений внизу панели глобально включает/выключает сообщения о подключении.

Пункт «Настройки» доступен и в нижней части панели, и в нативном tray-меню. Страница открывается внутри той же панели и позволяет выбрать язык («Как в системе», English (United States), Русский) и тему («Как в системе», светлая, тёмная). Изменения применяются без перезапуска.

В панели одновременно видны не более двух карточек; остальные устройства доступны прокруткой. Последний известный заряд отключённого устройства остаётся в списке и отображается серым с поясняющей подсказкой, но не учитывается в «Передают заряд» и статусе tray-иконки.

Настройки находятся в `%LocalAppData%\BluetoothNotify\settings.json`, журналы — в `%LocalAppData%\BluetoothNotify\logs`.

## Публикация

```powershell
dotnet publish .\src\BluetoothNotify.App\BluetoothNotify.App.csproj `
  -c Release -r win-x64 --self-contained false
```

Стандартный результат находится в `src\BluetoothNotify.App\bin\Release\net10.0-windows10.0.22621.0\win-x64\publish`. Проверенный в этой рабочей копии артефакт также лежит в `artifacts\publish\win-x64`.

Это не «один exe»: рядом с `BluetoothNotify.App.exe` должны оставаться `.dll`, `.deps.json`, `.runtimeconfig.json`, `.pri` и bootstrap/projection-библиотеки приложения. Отсутствие `coreclr.dll`, `clrjit.dll`, `hostfxr.dll` и файлов `PresentationFramework.dll` в publish подтверждает, что .NET runtime не включён.

## Архитектура

- `Models` — transport-independent snapshot и агрегированное состояние устройства;
- `BluetoothDeviceMonitor` — два документированных WinRT `DeviceWatcher` для LE и Classic, initial snapshot и события соединения;
- `DeviceStateMerger` — дедупликация по Container ID, затем по Bluetooth-адресу;
- `BatteryReader` — системный `BatteryLife`, HFP/PnP fallback и GATT Battery Service `0x180F` / Battery Level `0x2A19`; TTL 20 секунд, не более четырёх одновременных чтений и timeout 5 секунд; для отключённых устройств читаются только сохранённые системные/PnP-значения без открытия GATT;
- `PnpBatteryReader` — изолированный best-effort fallback для HFP-заряда, который Windows показывает на дочернем узле `Hands-Free AG`;
- `TrayIconService` — `Shell_NotifyIcon`, `Shell_NotifyIconGetRect`, native menu, hover state machine, выбор иконки по теме приложения и восстановление после перезапуска Explorer;
- `PanelPlacementService` — чистая DPI-aware математика размещения для taskbar с любой стороны и нескольких мониторов;
- `TrayPanelViewModel` — MVVM, single-flight refresh и обновление коллекции только через WPF Dispatcher;
- `LocalizationService` — явный выбор поддерживаемой UI-культуры и русские/английские `.resx`-ресурсы;
- `ThemeManager` — переключение светлого и тёмного словарей WPF-ресурсов, включая отслеживание системной темы;
- `NotificationService` — Windows App SDK `AppNotificationManager`, постоянный AppUserModelID `BluetoothNotify.App`;
- `SettingsStore` — JSON с версией схемы, атомарная замена и восстановление повреждённого файла;
- `AppLogger` — локальный журнал с ротацией 5 × 512 КиБ.

Секундный refresh работает только пока панель видима. При скрытой панели изменения watcher-ов запускают отложенное обновление данных и статуса tray-иконки. Первый snapshot не создаёт уведомлений; переход `Disconnected → Connected` защищён debounce 3 секунды и повторно разрешается только после disconnect.

## Ограничения Bluetooth

Windows не предоставляет единый публичный API принудительного подключения/отключения всех Bluetooth-классов, поэтому индикатор состояния read-only. Приложение не выполняет pairing и не использует Device Manager, реестр, reverse-engineered HID-команды или vendor-specific протоколы.

Заряд запрашивается последовательно через системное свойство Windows, HFP/PnP-значение и стандартный BLE Battery Service. PnP-ключ HFP не документирован Microsoft, поэтому он изолирован как необязательный fallback: его отсутствие или изменение не ломает остальные способы. Если ни один источник не доступен, отображается `—` / «Заряд недоступен». Для отключённого устройства системное последнее значение помечается как устаревшее; приложение не инициирует GATT-подключение ради его обновления. Одна физическая периферия объединяется без дублей, если Windows предоставляет общий Container ID или одинаковый адрес.

## Тестирование

Автотесты покрывают merge LE/Classic, StableId, сортировку, icon mapping, пороги заряда, `null`-заряд, connection state machine/debounce, single-flight refresh, настройки и placement при 100/125/150/200% DPI. Подробности фактического прогона: [docs/verification-report.md](docs/verification-report.md).

Для аппаратной приёмки нужны реальные BLE-мышь/клавиатура, Classic+LE аудиоустройство и устройство без Battery Service. Эта проверка зависит от конкретного ПК и не заменяется unit-тестами.

## Источники API

- [Обзор Windows app notifications](https://learn.microsoft.com/windows/apps/develop/notifications/)
- [Bluetooth GATT client](https://learn.microsoft.com/windows/apps/develop/devices-sensors/gatt-client)
- [Shell_NotifyIcon](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyiconw)
- [Shell_NotifyIconGetRect](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyicongetrect)
