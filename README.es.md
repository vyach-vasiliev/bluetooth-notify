<p align="center">
  <img src="./src/BluetoothNotify.App/Assets/BluetoothNotify.Notification.png" width="72" height="72" alt="Logotipo de Bluetooth Notify">
</p>

<h1 align="center">Bluetooth Notify</h1>

<p align="center">
  <strong>Batería de tus dispositivos Bluetooth, estado real de conexión y avisos nativos, directamente en la bandeja de Windows.</strong>
</p>

<p align="center">
  <a href="./README.md">English</a> ·
  <a href="./README.ru.md">Русский</a> ·
  <a href="./README.de.md">Deutsch</a> ·
  <strong>Español</strong> ·
  <a href="./README.fr.md">Français</a> ·
  <a href="./README.pt-PT.md">Português</a> ·
  <a href="./README.ja.md">日本語</a> ·
  <a href="./README.ko.md">한국어</a> ·
  <a href="./README.zh-CN.md">简体中文</a>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Windows_11-22H2%2B-0078D4?logo=windows11&amp;logoColor=white" alt="Windows 11 22H2 o posterior">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&amp;logoColor=white" alt=".NET 10">
  <img src="https://img.shields.io/badge/UI-WPF-5C2D91" alt="Interfaz WPF">
  <img src="https://img.shields.io/badge/architecture-x64-555555" alt="Arquitectura x64">
</p>

Bluetooth Notify es una aplicación compacta para la bandeja de Windows 11 dedicada a los dispositivos Bluetooth emparejados. Muestra el estado de conexión que informa Windows, consulta todas las fuentes de batería disponibles y envía notificaciones nativas cuando se conecta un dispositivo o se cruza un umbral de batería definido por ti.

## Vista previa

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/main_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/main_light.png">
    <img src="./resources/main_light.png" width="680" alt="Panel de Bluetooth Notify con dispositivos emparejados, estado de conexión y niveles de batería">
  </picture>
</p>

### Ajustes y bandeja del sistema

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/settings_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/settings_light.png">
    <img src="./resources/settings_light.png" width="680" alt="Ajustes de idioma, tema y avisos de batería de Bluetooth Notify">
  </picture>
</p>

<p align="center">
  <img src="./resources/tray_light.png" width="478" alt="Icono de Bluetooth Notify en la bandeja del sistema de Windows">
</p>

## Funciones

- **Todos los dispositivos emparejados de un vistazo.** Consulta el estado de conexión actual y el último nivel de batería que Windows puede proporcionar.
- **Más formas de obtener la batería.** La aplicación prueba el valor del sistema de Windows, una alternativa HFP/PnP y el servicio de batería Bluetooth LE estándar.
- **Avisos sin ruido.** Recibe una notificación nativa de Windows ante una nueva conexión y una sola vez cuando la batería cruza un umbral medio o bajo que hayas activado.
- **Pensada para la bandeja.** Mantén el puntero brevemente o haz clic izquierdo para abrir el panel; el clic derecho abre el menú nativo.
- **Una interfaz acorde con el sistema.** Sigue el tema de Windows o elige el modo claro u oscuro.
- **Nueve idiomas de interfaz.** Inglés, ruso, alemán, español, francés, portugués, japonés, coreano y chino simplificado.
- **Datos locales.** Las preferencias y los registros rotativos permanecen en la carpeta local de datos de la aplicación; no hace falta una cuenta.

## Requisitos

Para ejecutar la aplicación necesitas:

- Windows 11 22H2 (compilación 22621) o posterior, x64;
- [.NET Desktop Runtime 10 x64](https://dotnet.microsoft.com/download/dotnet/10.0);
- [Windows App Runtime 1.8 x64](https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads-archive#version-18).

La aplicación publicada depende del framework, por lo que estos runtimes no se incluyen. Para compilar desde el código fuente también debes instalar el SDK de .NET 10.

## Inicio rápido desde el código fuente

~~~powershell
git clone https://github.com/vyach-vasiliev/bluetooth-notify.git
cd bluetooth-notify

dotnet restore .\BluetoothNotify.slnx
dotnet build .\BluetoothNotify.slnx -c Release
dotnet test .\BluetoothNotify.slnx -c Release
dotnet run --project .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release
~~~

La aplicación se inicia en la bandeja del sistema y no abre una ventana convencional.

## Uso de la aplicación

- Mantén el puntero sobre el icono de Bluetooth durante unos 300 ms o haz clic izquierdo para mostrar el panel.
- Haz clic derecho para acceder a **Abrir**, **Actualizar**, **Ajustes** y **Salir**.
- Pulsa <kbd>Esc</kbd>, aleja el puntero del icono y el panel o cambia el foco para ocultarlo.
- Usa el botón de campana en la parte inferior del panel para activar o desactivar globalmente los avisos de conexión.
- En **Ajustes** puedes elegir el idioma, la apariencia, los avisos de batería media y baja y ambos porcentajes. Los cambios se aplican sin reiniciar.

El panel muestra como máximo dos tarjetas de dispositivo a la vez; desplázate para ver las demás. Un dispositivo desconectado puede conservar su último nivel de batería conocido. La aplicación lo marca como obsoleto y no lo cuenta como una lectura actual.

## Ajustes y datos locales

- Ajustes: <code>%LocalAppData%\BluetoothNotify\settings.json</code>
- Registros rotativos: <code>%LocalAppData%\BluetoothNotify\logs</code>
- Aplicación instalada: <code>%LocalAppData%\Programs\Bluetooth Notify</code>

## Privacidad e información legal

Los nombres de dispositivos Bluetooth, los identificadores de endpoint/container de Windows, las direcciones Bluetooth disponibles, el estado de conexión y el nivel de batería se procesan localmente en memoria y no se envían al editor. La aplicación solo escribe en disco las preferencias y los registros técnicos rotativos (<code>5 × 512 KiB</code>); los mensajes de error del sistema operativo pueden contener ocasionalmente detalles técnicos locales.

La Política de privacidad, los Términos de uso y la Exención de responsabilidad se encuentran en <code>site/public/legal</code> en los nueve idiomas de la aplicación. La misma página autónoma se publica en <code>/legal/</code>, se incluye en la aplicación para «Ajustes → Información legal» y se resume en las pantallas en inglés y ruso del instalador. Consulta el [mapa de cumplimiento y la lista de publicación (en ruso)](./docs/legal-compliance.md).

## Publicación y creación del instalador

Crea una publicación x64 dependiente del framework:

~~~powershell
dotnet publish .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release -r win-x64 --self-contained false
~~~

La salida predeterminada se encuentra en <code>src\BluetoothNotify.App\bin\Release\net10.0-windows10.0.22621.0\win-x64\publish</code>. Conserva el ejecutable junto con sus DLL, archivos de dependencias, recursos PRI y bibliotecas bootstrap/projection de Windows App SDK.

Para crear el instalador por usuario, instala [Inno Setup 6](https://jrsoftware.org/isdl.php) y ejecuta:

~~~powershell
.\tools\Build-Installer.ps1
~~~

El script restaura dependencias, compila, prueba, publica, comprueba que el runtime de .NET no se haya incluido y compila <code>installer\BluetoothNotify.iss</code>. El resultado es <code>artifacts\installer\BluetoothNotify-Setup-&lt;versión&gt;-x64.exe</code>. La interfaz del instalador está disponible en inglés y ruso, comprueba ambos runtimes, se instala sin privilegios de administrador y conserva los datos del usuario de forma predeterminada al desinstalar.

Usa <code>-SkipTests</code> para omitir la segunda ejecución de las pruebas o indica una ruta personalizada al compilador:

~~~powershell
.\tools\Build-Installer.ps1 -InnoSetupCompiler 'C:\Tools\Inno Setup 6\ISCC.exe'
~~~

## Cómo se obtienen la batería y la conexión

1. Dos monitores WinRT documentados observan dispositivos Bluetooth LE y Bluetooth Classic.
2. Los registros se combinan primero por Container ID y después por dirección Bluetooth para evitar tarjetas duplicadas.
3. La consulta de batería prueba la propiedad del sistema de Windows, el valor HFP/PnP de mejor esfuerzo y GATT Battery Service <code>0x180F</code> / Battery Level <code>0x2A19</code>.
4. Las lecturas se almacenan en caché durante 20 segundos; se ejecutan como máximo cuatro lecturas simultáneas, con un tiempo de espera de cinco segundos.
5. Mientras el panel está visible se actualiza cada segundo. Cuando está oculto, la aplicación comprueba una vez por minuto si debe generar avisos por umbral.

La primera instantánea no genera avisos de conexión. Una transición <code>Disconnected → Connected</code> usa una estabilización de tres segundos y solo vuelve a habilitarse después de una desconexión. El aviso de batería se envía una vez cuando la lectura cruza desde arriba un umbral habilitado.

<details>
<summary><strong>Arquitectura para colaboradores</strong></summary>

- <code>BluetoothDeviceMonitor</code> — monitores WinRT, instantáneas y eventos de conexión.
- <code>DeviceStateMerger</code> — eliminación de duplicados LE/Classic e identidades estables.
- <code>BatteryReader</code> y <code>PnpBatteryReader</code> — búsqueda ordenada de fuentes de batería.
- <code>TrayIconService</code> — icono y menú nativos, estado hover y recuperación tras reiniciar Explorer.
- <code>PanelPlacementService</code> — colocación consciente del DPI para cualquier borde de la barra de tareas y varios monitores.
- <code>TrayPanelViewModel</code> — estado MVVM, actualización single-flight y colecciones enlazadas al Dispatcher.
- <code>LocalizationService</code> y <code>ThemeManager</code> — cambios de idioma y apariencia en tiempo de ejecución.
- <code>NotificationService</code> — notificaciones de Windows App SDK con AppUserModelID <code>BluetoothNotify.App</code>.
- <code>SettingsStore</code> y <code>AppLogger</code> — persistencia JSON atómica y registros locales rotativos.

</details>

## Limitaciones de Bluetooth

El estado de conexión es de solo lectura. Windows no expone una única API pública que conecte o desconecte todas las clases de dispositivos Bluetooth, por lo que Bluetooth Notify no empareja dispositivos ni usa el Administrador de dispositivos, cambios en el registro, comandos HID obtenidos por ingeniería inversa o protocolos específicos de fabricantes.

La disponibilidad de la batería depende de lo que expongan Windows y el dispositivo. La propiedad HFP/PnP es una alternativa aislada, no documentada y de mejor esfuerzo; si cambia o falta, las otras fuentes siguen funcionando. Para dispositivos desconectados, la aplicación puede mostrar un valor del sistema claramente marcado como obsoleto y no abre una conexión GATT solo para actualizarlo.

## Pruebas

~~~powershell
dotnet test .\BluetoothNotify.slnx -c Release
~~~

Las pruebas cubren la combinación LE/Classic, identificadores estables, ordenación, asignación de iconos, umbrales y valores de batería ausentes, estabilización de conexiones, actualización single-flight, ajustes, localización, apariencia y colocación del panel con DPI del 100/125/150/200 %.

El [informe de verificación detallado (en ruso)](./docs/verification-report.md) contiene las comprobaciones de hardware e instalador registradas.

## Referencias

- [Introducción a las notificaciones de aplicaciones de Windows](https://learn.microsoft.com/windows/apps/develop/notifications/)
- [Cliente Bluetooth GATT](https://learn.microsoft.com/windows/apps/develop/devices-sensors/gatt-client)
- [Shell_NotifyIcon](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyiconw)
- [Shell_NotifyIconGetRect](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyicongetrect)
- [Avisos de terceros](./THIRD-PARTY-NOTICES.md)
