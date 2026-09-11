<p align="center">
  <img src="./src/BluetoothNotify.App/Assets/BluetoothNotify.Notification.png" width="72" height="72" alt="Logótipo do Bluetooth Notify">
</p>

<h1 align="center">Bluetooth Notify</h1>

<p align="center">
  <strong>Nível da bateria, estado real da ligação e notificações nativas para dispositivos Bluetooth—diretamente na área de notificação do Windows.</strong>
</p>

<p align="center">
  <a href="./README.md">English</a> ·
  <a href="./README.ru.md">Русский</a> ·
  <a href="./README.de.md">Deutsch</a> ·
  <a href="./README.es.md">Español</a> ·
  <a href="./README.fr.md">Français</a> ·
  <strong>Português</strong> ·
  <a href="./README.ja.md">日本語</a> ·
  <a href="./README.ko.md">한국어</a> ·
  <a href="./README.zh-CN.md">简体中文</a>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Windows_11-22H2%2B-0078D4?logo=windows11&amp;logoColor=white" alt="Windows 11 22H2 ou posterior">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&amp;logoColor=white" alt=".NET 10">
  <img src="https://img.shields.io/badge/UI-WPF-5C2D91" alt="Interface WPF">
  <img src="https://img.shields.io/badge/architecture-x64-555555" alt="Arquitetura x64">
</p>

O Bluetooth Notify é uma aplicação compacta para a área de notificação do Windows 11, dedicada a dispositivos Bluetooth emparelhados. Mostra o estado da ligação comunicado pelo Windows, consulta todas as fontes de bateria disponíveis e envia notificações nativas quando um dispositivo se liga ou atravessa um limite de bateria definido pelo utilizador.

## Pré-visualização

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/main_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/main_light.png">
    <img src="./resources/main_light.png" width="680" alt="Painel do Bluetooth Notify com dispositivos emparelhados, estado da ligação e níveis de bateria">
  </picture>
</p>

### Definições e área de notificação

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/settings_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/settings_light.png">
    <img src="./resources/settings_light.png" width="680" alt="Definições de idioma, tema e avisos de bateria do Bluetooth Notify">
  </picture>
</p>

<p align="center">
  <img src="./resources/tray_light.png" width="478" alt="Ícone do Bluetooth Notify na área de notificação do Windows">
</p>

## Funcionalidades

- **Todos os dispositivos emparelhados num só lugar.** Consulte o estado atual da ligação e o nível de bateria mais recente que o Windows consegue fornecer.
- **Várias fontes de bateria.** A aplicação tenta o valor do sistema Windows, uma alternativa HFP/PnP e o serviço de bateria Bluetooth LE padrão.
- **Avisos sem ruído desnecessário.** Receba uma notificação nativa do Windows numa nova ligação e apenas uma vez quando a bateria cruza um limite médio ou baixo ativado.
- **Concebida para a área de notificação.** Passe brevemente o ponteiro ou clique com o botão esquerdo para abrir o painel; o botão direito abre o menu nativo.
- **Uma interface que acompanha o sistema.** Siga o tema do Windows ou escolha o modo claro ou escuro.
- **Nove idiomas de interface.** Inglês, russo, alemão, espanhol, francês, português, japonês, coreano e chinês simplificado.
- **Dados locais.** As preferências e os registos rotativos permanecem na pasta local de dados da aplicação; não é necessária uma conta.

## Requisitos

Para executar a aplicação:

- Windows 11 22H2 (compilação 22621) ou posterior, x64;
- [.NET Desktop Runtime 10 x64](https://dotnet.microsoft.com/download/dotnet/10.0);
- [Windows App Runtime 1.8 x64](https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads-archive#version-18).

A aplicação publicada depende da framework, pelo que estes runtimes não estão incluídos. Para compilar a partir do código-fonte, instale também o SDK do .NET 10.

## Início rápido a partir do código-fonte

~~~powershell
git clone https://github.com/vyach-vasiliev/bluetooth-notify.git
cd bluetooth-notify

dotnet restore .\BluetoothNotify.slnx
dotnet build .\BluetoothNotify.slnx -c Release
dotnet test .\BluetoothNotify.slnx -c Release
dotnet run --project .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release
~~~

A aplicação inicia na área de notificação e não abre uma janela convencional.

## Utilização

- Mantenha o ponteiro sobre o ícone Bluetooth durante cerca de 300 ms ou clique com o botão esquerdo para mostrar o painel.
- Clique com o botão direito para aceder a **Abrir**, **Atualizar**, **Definições** e **Sair**.
- Prima <kbd>Esc</kbd>, afaste o ponteiro do ícone e do painel ou mude o foco para ocultar o painel.
- Utilize o botão do sino na parte inferior do painel para ativar ou desativar globalmente as notificações de ligação.
- Em **Definições**, escolha o idioma, o aspeto, os avisos de bateria média e baixa e os respetivos limites percentuais. As alterações são aplicadas sem reiniciar.

O painel apresenta no máximo dois cartões de dispositivo ao mesmo tempo; desloque-se para ver os restantes. Um dispositivo desligado pode conservar o último nível de bateria conhecido. A aplicação assinala esse valor como desatualizado e não o considera uma leitura atual.

## Definições e dados locais

- Definições: <code>%LocalAppData%\BluetoothNotify\settings.json</code>
- Registos rotativos: <code>%LocalAppData%\BluetoothNotify\logs</code>
- Aplicação instalada: <code>%LocalAppData%\Programs\Bluetooth Notify</code>

## Privacidade e informação jurídica

Os nomes dos dispositivos Bluetooth, os IDs de endpoint/container do Windows, os endereços Bluetooth disponíveis, o estado da ligação e o nível da bateria são processados localmente em memória e não são enviados ao editor. A aplicação grava no disco apenas as preferências e os registos técnicos rotativos (<code>5 × 512 KiB</code>); as mensagens de erro do sistema operativo podem ocasionalmente conter detalhes técnicos locais.

A Política de Privacidade, os Termos de Utilização e a Exclusão de Responsabilidade encontram-se em <code>site/public/legal</code> nos nove idiomas da aplicação. A mesma página autónoma é publicada em <code>/legal/</code>, incluída na aplicação para «Definições → Informação jurídica» e resumida nos ecrãs em inglês/russo do instalador. Consulte o [mapa de conformidade e a lista de verificação de lançamento (em russo)](./docs/legal-compliance.md).

## Publicação e criação do instalador

Crie uma publicação x64 dependente da framework:

~~~powershell
dotnet publish .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release -r win-x64 --self-contained false
~~~

A saída predefinida encontra-se em <code>src\BluetoothNotify.App\bin\Release\net10.0-windows10.0.22621.0\win-x64\publish</code>. Mantenha o executável juntamente com as DLL, os ficheiros de dependências, os recursos PRI e as bibliotecas de bootstrap/projeção do Windows App SDK.

Para criar o instalador por utilizador, instale o [Inno Setup 6](https://jrsoftware.org/isdl.php) e execute:

~~~powershell
.\tools\Build-Installer.ps1
~~~

O script restaura dependências, compila, testa, publica, confirma que o runtime do .NET não foi incluído e compila <code>installer\BluetoothNotify.iss</code>. O resultado é <code>artifacts\installer\BluetoothNotify-Setup-&lt;versão&gt;-x64.exe</code>. A interface do instalador está disponível em inglês e russo, verifica os dois runtimes, instala sem privilégios de administrador e mantém os dados do utilizador por predefinição durante a desinstalação.

Utilize <code>-SkipTests</code> para omitir a repetição dos testes ou indique um caminho personalizado para o compilador:

~~~powershell
.\tools\Build-Installer.ps1 -InnoSetupCompiler 'C:\Tools\Inno Setup 6\ISCC.exe'
~~~

## Como são obtidos a bateria e o estado da ligação

1. Dois observadores WinRT documentados monitorizam dispositivos Bluetooth LE e Bluetooth Classic.
2. Os registos são combinados primeiro por Container ID e depois por endereço Bluetooth, evitando cartões duplicados.
3. A consulta da bateria tenta a propriedade de sistema do Windows, o valor HFP/PnP de melhor esforço e GATT Battery Service <code>0x180F</code> / Battery Level <code>0x2A19</code>.
4. As leituras são guardadas em cache durante 20 segundos; são executadas no máximo quatro leituras em simultâneo, com um tempo limite de cinco segundos.
5. Enquanto o painel está visível, é atualizado a cada segundo. Quando está oculto, a aplicação verifica uma vez por minuto se deve emitir notificações de limite.

O primeiro instantâneo não envia notificações de ligação. Uma transição <code>Disconnected → Connected</code> utiliza um debounce de três segundos e só volta a ficar elegível depois de uma desconexão. Um aviso de bateria é enviado uma vez quando a leitura cruza de cima para baixo um limite ativado.

<details>
<summary><strong>Arquitetura para colaboradores</strong></summary>

- <code>BluetoothDeviceMonitor</code> — observadores WinRT, instantâneos e eventos de ligação.
- <code>DeviceStateMerger</code> — remoção de duplicados LE/Classic e identidades estáveis.
- <code>BatteryReader</code> e <code>PnpBatteryReader</code> — pesquisa ordenada das fontes de bateria.
- <code>TrayIconService</code> — ícone e menu nativos, estado de hover e recuperação após reiniciar o Explorer.
- <code>PanelPlacementService</code> — posicionamento atento ao DPI em qualquer margem da barra de tarefas e em vários monitores.
- <code>TrayPanelViewModel</code> — estado MVVM, atualização single-flight e coleções ligadas ao Dispatcher.
- <code>LocalizationService</code> e <code>ThemeManager</code> — mudanças de idioma e aspeto em tempo de execução.
- <code>NotificationService</code> — notificações do Windows App SDK com AppUserModelID <code>BluetoothNotify.App</code>.
- <code>SettingsStore</code> e <code>AppLogger</code> — persistência JSON atómica e registos locais rotativos.

</details>

## Limitações do Bluetooth

O estado da ligação é só de leitura. O Windows não disponibiliza uma única API pública capaz de ligar ou desligar todas as classes de dispositivos Bluetooth; por isso, o Bluetooth Notify não emparelha dispositivos nem utiliza o Gestor de Dispositivos, alterações ao Registo, comandos HID obtidos por engenharia inversa ou protocolos específicos de fabricantes.

A disponibilidade da bateria depende do que o Windows e o dispositivo expõem. A propriedade HFP/PnP é uma alternativa isolada, não documentada e de melhor esforço; se mudar ou estiver ausente, as restantes fontes continuam a funcionar. Para dispositivos desligados, a aplicação pode apresentar um valor de sistema claramente assinalado como desatualizado e não abre uma ligação GATT apenas para o atualizar.

## Testes

~~~powershell
dotnet test .\BluetoothNotify.slnx -c Release
~~~

Os testes abrangem a combinação LE/Classic, IDs estáveis, ordenação, associação de ícones, limites e valores de bateria em falta, debounce da ligação, atualização single-flight, definições, localização, aspeto e posicionamento do painel com DPI de 100/125/150/200%.

O [relatório de verificação detalhado (em russo)](./docs/verification-report.md) contém os testes de hardware e do instalador registados.

## Referências

- [Descrição geral das notificações de aplicações Windows](https://learn.microsoft.com/windows/apps/develop/notifications/)
- [Cliente Bluetooth GATT](https://learn.microsoft.com/windows/apps/develop/devices-sensors/gatt-client)
- [Shell_NotifyIcon](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyiconw)
- [Shell_NotifyIconGetRect](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyicongetrect)
- [Avisos de terceiros](./THIRD-PARTY-NOTICES.md)
