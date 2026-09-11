<p align="center">
  <img src="./src/BluetoothNotify.App/Assets/BluetoothNotify.Notification.png" width="72" height="72" alt="Bluetooth Notify のロゴ">
</p>

<h1 align="center">Bluetooth Notify</h1>

<p align="center">
  <strong>Bluetooth 機器のバッテリー残量、実際の接続状態、ネイティブ通知を Windows のシステムトレイから確認。</strong>
</p>

<p align="center">
  <a href="./README.md">English</a> ·
  <a href="./README.ru.md">Русский</a> ·
  <a href="./README.de.md">Deutsch</a> ·
  <a href="./README.es.md">Español</a> ·
  <a href="./README.fr.md">Français</a> ·
  <a href="./README.pt-PT.md">Português</a> ·
  <strong>日本語</strong> ·
  <a href="./README.ko.md">한국어</a> ·
  <a href="./README.zh-CN.md">简体中文</a>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Windows_11-22H2%2B-0078D4?logo=windows11&amp;logoColor=white" alt="Windows 11 22H2 以降">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&amp;logoColor=white" alt=".NET 10">
  <img src="https://img.shields.io/badge/UI-WPF-5C2D91" alt="WPF ユーザーインターフェイス">
  <img src="https://img.shields.io/badge/architecture-x64-555555" alt="x64 アーキテクチャ">
</p>

Bluetooth Notify は、ペアリング済み Bluetooth 機器のためのコンパクトな Windows 11 トレイアプリです。Windows が報告する接続状態を表示し、利用可能なすべてのバッテリー情報源を確認して、機器の接続時や指定したバッテリーしきい値を下回ったときにネイティブ通知を送ります。

## プレビュー

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/main_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/main_light.png">
    <img src="./resources/main_light.png" width="680" alt="ペアリング済み機器、接続状態、バッテリー残量を表示する Bluetooth Notify パネル">
  </picture>
</p>

### 設定画面とシステムトレイ

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/settings_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/settings_light.png">
    <img src="./resources/settings_light.png" width="680" alt="Bluetooth Notify の言語、テーマ、バッテリー通知設定">
  </picture>
</p>

<p align="center">
  <img src="./resources/tray_light.png" width="478" alt="Windows システムトレイの Bluetooth Notify アイコン">
</p>

## 主な機能

- **ペアリング済み機器をひと目で確認。** 現在の接続状態と、Windows が取得できる最新のバッテリー残量を表示します。
- **複数のバッテリー情報源。** Windows のシステム値、HFP/PnP フォールバック、標準 Bluetooth LE Battery Service の順に確認します。
- **必要なときだけ通知。** 新しい接続時と、有効にした中残量または低残量のしきい値を下回ったときに、一度だけ Windows のネイティブ通知を送ります。
- **トレイ中心の操作。** 短くマウスを重ねるか左クリックでパネルを開き、右クリックでネイティブメニューを表示します。
- **システムに合う外観。** Windows のテーマに追従するか、ライトまたはダークを選べます。
- **9 つの UI 言語。** 英語、ロシア語、ドイツ語、スペイン語、フランス語、ポルトガル語、日本語、韓国語、簡体字中国語に対応しています。
- **ローカルデータ。** 設定とローテーションログはローカルのアプリケーションデータフォルダーに保存され、アカウントは不要です。

## 動作要件

アプリの実行に必要な環境：

- Windows 11 22H2（ビルド 22621）以降、x64
- [.NET Desktop Runtime 10 x64](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Windows App Runtime 1.8 x64](https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads-archive#version-18)

公開ビルドはフレームワーク依存のため、これらのランタイムは同梱されません。ソースからビルドする場合は .NET 10 SDK もインストールしてください。

## ソースからのクイックスタート

~~~powershell
git clone https://github.com/vyach-vasiliev/bluetooth-notify.git
cd bluetooth-notify

dotnet restore .\BluetoothNotify.slnx
dotnet build .\BluetoothNotify.slnx -c Release
dotnet test .\BluetoothNotify.slnx -c Release
dotnet run --project .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release
~~~

アプリはシステムトレイで起動し、通常のウィンドウは開きません。

## 使い方

- Bluetooth のトレイアイコンに約 300 ミリ秒マウスを重ねるか、左クリックしてパネルを表示します。
- 右クリックすると、**開く**、**更新**、**設定**、**終了**を選べます。
- <kbd>Esc</kbd> を押す、アイコンとパネルの両方からポインターを離す、またはフォーカスを切り替えるとパネルが隠れます。
- パネル下部のベルボタンで、接続通知をまとめて有効または無効にできます。
- **設定**では、言語、外観、中残量・低残量の通知とそれぞれの割合を選べます。変更は再起動せずに反映されます。

パネルに同時表示される機器カードは最大 2 枚で、それ以外はスクロールして確認できます。切断された機器には最後に確認したバッテリー残量が残る場合があります。この値は古い情報として明示され、現在のバッテリー報告には数えられません。

## 設定とローカルデータ

- 設定：<code>%LocalAppData%\BluetoothNotify\settings.json</code>
- ローテーションログ：<code>%LocalAppData%\BluetoothNotify\logs</code>
- インストール先：<code>%LocalAppData%\Programs\Bluetooth Notify</code>

## プライバシーと法的情報

Bluetooth 機器名、Windows の endpoint/container ID、取得可能な Bluetooth アドレス、接続状態、バッテリー残量はメモリ内でローカル処理され、発行者には送信されません。アプリがディスクに書き込むのは設定とローテーションする技術ログ（<code>5 × 512 KiB</code>）だけです。オペレーティングシステムのエラーメッセージには、ローカルの技術情報が含まれる場合があります。

プライバシーポリシー、利用規約、免責事項は、アプリの 9 言語すべてで <code>site/public/legal</code> に収録されています。同じ自己完結型ページが <code>/legal/</code> で公開され、「設定 → 法的情報」用としてアプリに同梱され、英語・ロシア語のインストーラー画面にも要約されています。[コンプライアンス対応表とリリースチェックリスト（ロシア語）](./docs/legal-compliance.md)も参照してください。

## 発行とインストーラーの作成

フレームワーク依存の x64 ビルドを発行します：

~~~powershell
dotnet publish .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release -r win-x64 --self-contained false
~~~

既定の出力先は <code>src\BluetoothNotify.App\bin\Release\net10.0-windows10.0.22621.0\win-x64\publish</code> です。実行ファイルは DLL、依存関係ファイル、PRI リソース、Windows App SDK の bootstrap/projection ライブラリと一緒に保持してください。

ユーザー単位のインストーラーを作成するには、[Inno Setup 6](https://jrsoftware.org/isdl.php) をインストールしてから次を実行します：

~~~powershell
.\tools\Build-Installer.ps1
~~~

このスクリプトは restore、build、test、publish を行い、.NET ランタイムが同梱されていないことを確認し、<code>installer\BluetoothNotify.iss</code> をコンパイルします。出力は <code>artifacts\installer\BluetoothNotify-Setup-&lt;version&gt;-x64.exe</code> です。インストーラーの UI は英語とロシア語に対応し、必要な 2 つのランタイムを確認して、管理者権限なしでインストールします。アンインストール時のユーザーデータは既定で保持されます。

テストの再実行を省略するには <code>-SkipTests</code> を使い、コンパイラーの場所を指定する場合は次のように実行します：

~~~powershell
.\tools\Build-Installer.ps1 -InnoSetupCompiler 'C:\Tools\Inno Setup 6\ISCC.exe'
~~~

## バッテリーと接続状態の仕組み

1. 文書化された 2 つの WinRT ウォッチャーが Bluetooth LE と Bluetooth Classic の機器を監視します。
2. 重複カードを避けるため、レコードを Container ID、続いて Bluetooth アドレスで統合します。
3. バッテリー情報は Windows システムプロパティ、ベストエフォートの HFP/PnP 値、GATT Battery Service <code>0x180F</code> / Battery Level <code>0x2A19</code> の順に確認します。
4. 読み取り値は 20 秒間キャッシュされ、同時読み取りは最大 4 件、タイムアウトは 5 秒です。
5. パネル表示中は毎秒更新します。非表示のときは、しきい値通知のため 1 分ごとに確認します。

最初のスナップショットでは接続通知を送りません。<code>Disconnected → Connected</code> の遷移には 3 秒のデバウンスがあり、切断後にだけ再び通知対象になります。バッテリー警告は、有効なしきい値を上から下へ初めて越えたときに一度だけ送られます。

<details>
<summary><strong>コントリビューター向けアーキテクチャ</strong></summary>

- <code>BluetoothDeviceMonitor</code> — WinRT ウォッチャー、スナップショット、接続イベント。
- <code>DeviceStateMerger</code> — LE/Classic の重複排除と安定した ID。
- <code>BatteryReader</code> と <code>PnpBatteryReader</code> — 順序付きバッテリー情報源の検索。
- <code>TrayIconService</code> — ネイティブのトレイアイコンとメニュー、ホバー状態、Explorer 再起動後の復元。
- <code>PanelPlacementService</code> — タスクバーの各辺と複数モニターに対応する DPI-aware 配置。
- <code>TrayPanelViewModel</code> — MVVM 状態、single-flight 更新、Dispatcher 上のコレクション更新。
- <code>LocalizationService</code> と <code>ThemeManager</code> — 実行時の言語と外観の変更。
- <code>NotificationService</code> — AppUserModelID <code>BluetoothNotify.App</code> を使う Windows App SDK 通知。
- <code>SettingsStore</code> と <code>AppLogger</code> — アトミックな JSON 保存とローテーションするローカルログ。

</details>

## Bluetooth の制限

接続状態は読み取り専用です。Windows にはすべての Bluetooth 機器クラスを接続または切断できる単一の公開 API がないため、Bluetooth Notify は機器のペアリングを行わず、デバイス マネージャー、レジストリ変更、リバースエンジニアリングされた HID コマンド、メーカー固有プロトコルも使用しません。

バッテリー情報を取得できるかどうかは、Windows と機器が公開する情報に依存します。HFP/PnP プロパティは、分離された非文書化のベストエフォートフォールバックです。変更または欠落しても、ほかの情報源は引き続き動作します。切断済み機器では、古いことを明示したシステム値を表示する場合がありますが、その更新だけを目的に GATT 接続を開くことはありません。

## テスト

~~~powershell
dotnet test .\BluetoothNotify.slnx -c Release
~~~

テストは LE/Classic の統合、安定した ID、並べ替え、アイコン割り当て、バッテリーしきい値と欠損値、接続デバウンス、single-flight 更新、設定、ローカライズ、外観、100/125/150/200% DPI でのパネル配置を対象とします。

記録済みの実機およびインストーラー確認については、[詳細な検証レポート（ロシア語）](./docs/verification-report.md)を参照してください。

## 参考資料

- [Windows アプリ通知の概要](https://learn.microsoft.com/windows/apps/develop/notifications/)
- [Bluetooth GATT クライアント](https://learn.microsoft.com/windows/apps/develop/devices-sensors/gatt-client)
- [Shell_NotifyIcon](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyiconw)
- [Shell_NotifyIconGetRect](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyicongetrect)
- [サードパーティに関する通知](./THIRD-PARTY-NOTICES.md)
