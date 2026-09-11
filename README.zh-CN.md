<p align="center">
  <img src="./src/BluetoothNotify.App/Assets/BluetoothNotify.Notification.png" width="72" height="72" alt="Bluetooth Notify 标志">
</p>

<h1 align="center">Bluetooth Notify</h1>

<p align="center">
  <strong>在 Windows 系统托盘中直接查看蓝牙设备电量、真实连接状态并接收原生通知。</strong>
</p>

<p align="center">
  <a href="./README.md">English</a> ·
  <a href="./README.ru.md">Русский</a> ·
  <a href="./README.de.md">Deutsch</a> ·
  <a href="./README.es.md">Español</a> ·
  <a href="./README.fr.md">Français</a> ·
  <a href="./README.pt-PT.md">Português</a> ·
  <a href="./README.ja.md">日本語</a> ·
  <a href="./README.ko.md">한국어</a> ·
  <strong>简体中文</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Windows_11-22H2%2B-0078D4?logo=windows11&amp;logoColor=white" alt="Windows 11 22H2 或更高版本">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&amp;logoColor=white" alt=".NET 10">
  <img src="https://img.shields.io/badge/UI-WPF-5C2D91" alt="WPF 用户界面">
  <img src="https://img.shields.io/badge/architecture-x64-555555" alt="x64 架构">
</p>

Bluetooth Notify 是一款面向已配对蓝牙设备的轻量 Windows 11 托盘应用。它会显示 Windows 报告的连接状态，依次读取所有可用的电量来源，并在设备连接或电量越过你设定的阈值时发送原生通知。

## 界面预览

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/main_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/main_light.png">
    <img src="./resources/main_light.png" width="680" alt="Bluetooth Notify 面板，显示已配对设备、连接状态和电量">
  </picture>
</p>

### 设置与系统托盘预览

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/settings_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/settings_light.png">
    <img src="./resources/settings_light.png" width="680" alt="Bluetooth Notify 的语言、主题和电量通知设置">
  </picture>
</p>

<p align="center">
  <img src="./resources/tray_light.png" width="478" alt="Windows 系统托盘中的 Bluetooth Notify 图标">
</p>

## 功能

- **一览所有已配对设备。** 查看当前连接状态，以及 Windows 能提供的最新电量。
- **多种电量来源。** 应用会依次尝试 Windows 系统值、HFP/PnP 回退值和标准 Bluetooth LE Battery Service。
- **恰到好处的通知。** 新设备连接时发送 Windows 原生通知；电量从上方越过已启用的中等或低电量阈值时，仅提醒一次。
- **以系统托盘为中心。** 短暂悬停或左键单击即可打开面板；右键单击打开原生菜单。
- **融入系统的界面。** 可以跟随 Windows 主题，也可以手动选择浅色或深色模式。
- **九种界面语言。** 支持英语、俄语、德语、西班牙语、法语、葡萄牙语、日语、韩语和简体中文。
- **数据保存在本地。** 设置和轮换日志仅保存在本地应用数据目录，无需账号。

## 运行要求

运行应用需要：

- Windows 11 22H2（内部版本 22621）或更高版本，x64；
- [.NET Desktop Runtime 10 x64](https://dotnet.microsoft.com/download/dotnet/10.0)；
- [Windows App Runtime 1.8 x64](https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads-archive#version-18)。

发布版本依赖框架，因此不会捆绑这些运行时。从源码构建还需要安装 .NET 10 SDK。

## 从源码快速开始

~~~powershell
git clone https://github.com/vyach-vasiliev/bluetooth-notify.git
cd bluetooth-notify

dotnet restore .\BluetoothNotify.slnx
dotnet build .\BluetoothNotify.slnx -c Release
dotnet test .\BluetoothNotify.slnx -c Release
dotnet run --project .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release
~~~

应用启动后位于系统托盘，不会打开普通窗口。

## 使用方法

- 将鼠标悬停在蓝牙托盘图标上约 300 毫秒，或左键单击图标，即可显示面板。
- 右键单击图标可使用**打开**、**刷新**、**设置**和**退出**。
- 按 <kbd>Esc</kbd>、将指针移出图标和面板，或切换焦点即可隐藏面板。
- 使用面板底部的铃铛按钮，可全局启用或禁用连接通知。
- 在**设置**中可以选择语言、外观、中等电量提醒、低电量提醒以及两个百分比阈值。更改无需重启即可生效。

面板一次最多显示两张设备卡片，其余设备可通过滚动查看。已断开的设备可能保留最后一次已知电量。应用会将该值明确标记为过期，且不会把它计为当前电量报告。

## 设置与本地数据

- 设置：<code>%LocalAppData%\BluetoothNotify\settings.json</code>
- 轮换日志：<code>%LocalAppData%\BluetoothNotify\logs</code>
- 应用安装目录：<code>%LocalAppData%\Programs\Bluetooth Notify</code>

## 隐私与法律信息

蓝牙设备名称、Windows endpoint/container ID、可用的蓝牙地址、连接状态和电量均仅在本机内存中处理，不会发送给发布者。应用只会向磁盘写入设置和轮换技术日志（<code>5 × 512 KiB</code>）；操作系统错误文本有时可能包含本机技术细节。

隐私政策、使用条款和免责声明以全部九种应用语言存放在 <code>site/public/legal</code> 中。同一个独立页面发布在 <code>/legal/</code>，随应用打包并用于“设置 → 法律信息”，也在英语/俄语安装程序界面中提供摘要。请参阅[合规映射与发布检查清单（俄语）](./docs/legal-compliance.md)。

## 发布与构建安装程序

创建依赖框架的 x64 发布版本：

~~~powershell
dotnet publish .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release -r win-x64 --self-contained false
~~~

默认输出位于 <code>src\BluetoothNotify.App\bin\Release\net10.0-windows10.0.22621.0\win-x64\publish</code>。请将可执行文件与 DLL、依赖文件、PRI 资源以及 Windows App SDK bootstrap/projection 库保存在一起。

要构建按用户安装的安装程序，请先安装 [Inno Setup 6](https://jrsoftware.org/isdl.php)，然后运行：

~~~powershell
.\tools\Build-Installer.ps1
~~~

脚本会执行 restore、build、test 和 publish，验证未捆绑 .NET 运行时，并编译 <code>installer\BluetoothNotify.iss</code>。输出文件为 <code>artifacts\installer\BluetoothNotify-Setup-&lt;version&gt;-x64.exe</code>。安装程序界面支持英语和俄语，会检查两个必需运行时，无需管理员权限即可安装，并在卸载时默认保留用户数据。

使用 <code>-SkipTests</code> 可跳过重复测试，也可以指定自定义编译器路径：

~~~powershell
.\tools\Build-Installer.ps1 -InnoSetupCompiler 'C:\Tools\Inno Setup 6\ISCC.exe'
~~~

## 电量与连接状态的工作方式

1. 两个有文档说明的 WinRT watcher 分别监控 Bluetooth LE 和 Bluetooth Classic 设备。
2. 记录先按 Container ID、再按蓝牙地址合并，避免出现重复卡片。
3. 电量读取依次尝试 Windows 系统属性、尽力而为的 HFP/PnP 值，以及 GATT Battery Service <code>0x180F</code> / Battery Level <code>0x2A19</code>。
4. 读数缓存 20 秒；最多同时进行四次读取，每次超时为五秒。
5. 面板可见时每秒刷新；面板隐藏时每分钟检查一次阈值通知。

首次快照不会发送连接通知。<code>Disconnected → Connected</code> 转换使用三秒防抖，并且只有断开连接后才会重新具备通知资格。电量读数从上方越过已启用的阈值时，警告只发送一次。

<details>
<summary><strong>面向贡献者的架构</strong></summary>

- <code>BluetoothDeviceMonitor</code> — WinRT watcher、快照和连接事件。
- <code>DeviceStateMerger</code> — LE/Classic 去重和稳定标识。
- <code>BatteryReader</code> 与 <code>PnpBatteryReader</code> — 按顺序查找电量来源。
- <code>TrayIconService</code> — 原生托盘图标与菜单、悬停状态和 Explorer 重启后的恢复。
- <code>PanelPlacementService</code> — 面向任意任务栏边缘和多显示器的 DPI-aware 放置。
- <code>TrayPanelViewModel</code> — MVVM 状态、single-flight 刷新和绑定到 Dispatcher 的集合更新。
- <code>LocalizationService</code> 与 <code>ThemeManager</code> — 运行时语言和外观切换。
- <code>NotificationService</code> — 使用 AppUserModelID <code>BluetoothNotify.App</code> 的 Windows App SDK 通知。
- <code>SettingsStore</code> 与 <code>AppLogger</code> — 原子 JSON 持久化和本地轮换日志。

</details>

## Bluetooth 限制

连接状态为只读。Windows 没有提供一个能连接或断开所有蓝牙设备类别的统一公开 API，因此 Bluetooth Notify 不负责设备配对，也不使用设备管理器、注册表修改、逆向分析的 HID 命令或厂商专用协议。

能否获取电量取决于 Windows 和设备公开的信息。HFP/PnP 属性是隔离的、无文档的尽力回退方案；即使它发生变化或缺失，其他来源仍可继续工作。对于已断开的设备，应用可能显示明确标记为过期的系统值，但不会仅为刷新该值而打开 GATT 连接。

## 测试

~~~powershell
dotnet test .\BluetoothNotify.slnx -c Release
~~~

测试覆盖 LE/Classic 合并、稳定 ID、排序、图标映射、电量阈值和缺失值、连接防抖、single-flight 刷新、设置、本地化、外观，以及在 100/125/150/200% DPI 下的面板放置。

[详细验证报告（俄语）](./docs/verification-report.md)记录了硬件和安装程序检查。

## 参考资料

- [Windows 应用通知概述](https://learn.microsoft.com/windows/apps/develop/notifications/)
- [Bluetooth GATT 客户端](https://learn.microsoft.com/windows/apps/develop/devices-sensors/gatt-client)
- [Shell_NotifyIcon](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyiconw)
- [Shell_NotifyIconGetRect](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyicongetrect)
- [第三方组件声明](./THIRD-PARTY-NOTICES.md)
