<p align="center">
  <img src="./src/BluetoothNotify.App/Assets/BluetoothNotify.Notification.png" width="72" height="72" alt="Logo de Bluetooth Notify">
</p>

<h1 align="center">Bluetooth Notify</h1>

<p align="center">
  <strong>Niveau de batterie, état réel de connexion et notifications natives pour vos appareils Bluetooth—directement dans la zone de notification Windows.</strong>
</p>

<p align="center">
  <a href="./README.md">English</a> ·
  <a href="./README.ru.md">Русский</a> ·
  <a href="./README.de.md">Deutsch</a> ·
  <a href="./README.es.md">Español</a> ·
  <strong>Français</strong> ·
  <a href="./README.pt-PT.md">Português</a> ·
  <a href="./README.ja.md">日本語</a> ·
  <a href="./README.ko.md">한국어</a> ·
  <a href="./README.zh-CN.md">简体中文</a>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Windows_11-22H2%2B-0078D4?logo=windows11&amp;logoColor=white" alt="Windows 11 22H2 ou version ultérieure">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&amp;logoColor=white" alt=".NET 10">
  <img src="https://img.shields.io/badge/UI-WPF-5C2D91" alt="Interface WPF">
  <img src="https://img.shields.io/badge/architecture-x64-555555" alt="Architecture x64">
</p>

Bluetooth Notify est une application compacte pour la zone de notification de Windows 11, dédiée aux appareils Bluetooth jumelés. Elle affiche l’état de connexion signalé par Windows, consulte toutes les sources de batterie disponibles et envoie des notifications natives lorsqu’un appareil se connecte ou franchit un seuil de batterie que vous avez défini.

## Aperçu

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/main_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/main_light.png">
    <img src="./resources/main_light.png" width="680" alt="Panneau Bluetooth Notify affichant les appareils jumelés, leur état de connexion et leur batterie">
  </picture>
</p>

### Paramètres et zone de notification

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="./resources/settings_dark.png">
    <source media="(prefers-color-scheme: light)" srcset="./resources/settings_light.png">
    <img src="./resources/settings_light.png" width="680" alt="Paramètres de langue, de thème et d’alertes de batterie de Bluetooth Notify">
  </picture>
</p>

<p align="center">
  <img src="./resources/tray_light.png" width="478" alt="Icône Bluetooth Notify dans la zone de notification Windows">
</p>

## Fonctionnalités

- **Tous les appareils jumelés en un coup d’œil.** Consultez l’état de connexion actuel et le dernier niveau de batterie que Windows peut fournir.
- **Plusieurs sources de batterie.** L’application essaie la valeur système Windows, une solution de repli HFP/PnP et le service de batterie Bluetooth LE standard.
- **Des alertes sans bruit inutile.** Recevez une notification Windows native lors d’une nouvelle connexion et une seule fois lorsque la batterie franchit un seuil moyen ou bas activé.
- **Pensée pour la zone de notification.** Survolez brièvement l’icône ou faites un clic gauche pour ouvrir le panneau ; un clic droit ouvre le menu natif.
- **Une interface adaptée au système.** Suivez le thème Windows ou choisissez le mode clair ou sombre.
- **Neuf langues d’interface.** Anglais, russe, allemand, espagnol, français, portugais, japonais, coréen et chinois simplifié.
- **Données locales.** Les préférences et journaux rotatifs restent dans le dossier local de l’application ; aucun compte n’est requis.

## Configuration requise

Pour exécuter l’application :

- Windows 11 22H2 (build 22621) ou version ultérieure, x64 ;
- [.NET Desktop Runtime 10 x64](https://dotnet.microsoft.com/download/dotnet/10.0) ;
- [Windows App Runtime 1.8 x64](https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads-archive#version-18).

L’application publiée dépend du framework ; ces environnements d’exécution ne sont donc pas inclus. Pour compiler depuis les sources, installez également le SDK .NET 10.

## Démarrage rapide depuis les sources

~~~powershell
git clone https://github.com/vyach-vasiliev/bluetooth-notify.git
cd bluetooth-notify

dotnet restore .\BluetoothNotify.slnx
dotnet build .\BluetoothNotify.slnx -c Release
dotnet test .\BluetoothNotify.slnx -c Release
dotnet run --project .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release
~~~

L’application démarre dans la zone de notification et n’ouvre pas de fenêtre classique.

## Utilisation

- Survolez l’icône Bluetooth pendant environ 300 ms ou faites un clic gauche pour afficher le panneau.
- Faites un clic droit pour accéder à **Ouvrir**, **Actualiser**, **Paramètres** et **Quitter**.
- Appuyez sur <kbd>Échap</kbd>, éloignez le pointeur de l’icône et du panneau ou changez le focus pour masquer le panneau.
- Utilisez le bouton en forme de cloche en bas du panneau pour activer ou désactiver globalement les notifications de connexion.
- Dans les **Paramètres**, choisissez la langue, l’apparence, les alertes de batterie moyenne et faible ainsi que leurs deux seuils. Les modifications s’appliquent sans redémarrage.

Le panneau affiche au maximum deux cartes d’appareil à la fois ; faites défiler pour voir les autres. Un appareil déconnecté peut conserver son dernier niveau de batterie connu. L’application le signale comme obsolète et ne le compte pas comme une mesure actuelle.

## Paramètres et données locales

- Paramètres : <code>%LocalAppData%\BluetoothNotify\settings.json</code>
- Journaux rotatifs : <code>%LocalAppData%\BluetoothNotify\logs</code>
- Application installée : <code>%LocalAppData%\Programs\Bluetooth Notify</code>

## Confidentialité et informations juridiques

Les noms des appareils Bluetooth, les identifiants endpoint/container de Windows, les adresses Bluetooth disponibles, l’état de connexion et le niveau de batterie sont traités localement en mémoire et ne sont pas envoyés à l’éditeur. L’application n’écrit sur le disque que les préférences et les journaux techniques rotatifs (<code>5 × 512 Kio</code>) ; les messages d’erreur du système d’exploitation peuvent parfois contenir des détails techniques locaux.

La Politique de confidentialité, les Conditions d’utilisation et la Clause de non-responsabilité se trouvent dans <code>site/public/legal</code> dans les neuf langues de l’application. La même page autonome est publiée sous <code>/legal/</code>, intégrée à l’application pour « Paramètres → Informations juridiques » et résumée dans les écrans anglais/russe de l’installateur. Consultez la [carte de conformité et la liste de contrôle de publication (en russe)](./docs/legal-compliance.md).

## Publication et création de l’installateur

Créez une publication x64 dépendante du framework :

~~~powershell
dotnet publish .\src\BluetoothNotify.App\BluetoothNotify.App.csproj -c Release -r win-x64 --self-contained false
~~~

La sortie par défaut se trouve dans <code>src\BluetoothNotify.App\bin\Release\net10.0-windows10.0.22621.0\win-x64\publish</code>. Conservez l’exécutable avec ses DLL, fichiers de dépendances, ressources PRI et bibliothèques bootstrap/projection de Windows App SDK.

Pour créer l’installateur par utilisateur, installez [Inno Setup 6](https://jrsoftware.org/isdl.php), puis exécutez :

~~~powershell
.\tools\Build-Installer.ps1
~~~

Le script restaure les dépendances, compile, teste, publie, vérifie que le runtime .NET n’a pas été intégré et compile <code>installer\BluetoothNotify.iss</code>. Le résultat est <code>artifacts\installer\BluetoothNotify-Setup-&lt;version&gt;-x64.exe</code>. L’interface d’installation est disponible en anglais et en russe, vérifie les deux runtimes requis, s’installe sans droits administrateur et conserve les données utilisateur par défaut lors de la désinstallation.

Utilisez <code>-SkipTests</code> pour ignorer la seconde exécution des tests, ou indiquez un chemin de compilateur personnalisé :

~~~powershell
.\tools\Build-Installer.ps1 -InnoSetupCompiler 'C:\Tools\Inno Setup 6\ISCC.exe'
~~~

## Fonctionnement de la batterie et de la connexion

1. Deux observateurs WinRT documentés surveillent les appareils Bluetooth LE et Bluetooth Classic.
2. Les enregistrements sont fusionnés d’abord par Container ID, puis par adresse Bluetooth afin d’éviter les cartes en double.
3. La recherche de batterie essaie la propriété système Windows, la valeur HFP/PnP de secours et GATT Battery Service <code>0x180F</code> / Battery Level <code>0x2A19</code>.
4. Les mesures sont mises en cache pendant 20 secondes ; quatre lectures au maximum s’exécutent simultanément, avec un délai d’expiration de cinq secondes.
5. Lorsque le panneau est visible, il s’actualise chaque seconde. Lorsqu’il est masqué, l’application vérifie les seuils une fois par minute.

Le premier instantané n’envoie pas de notification de connexion. Une transition <code>Disconnected → Connected</code> utilise un anti-rebond de trois secondes et ne redevient éligible qu’après une déconnexion. Une alerte de batterie est envoyée une fois lorsque la mesure franchit vers le bas un seuil activé.

<details>
<summary><strong>Architecture pour les contributeurs</strong></summary>

- <code>BluetoothDeviceMonitor</code> — observateurs WinRT, instantanés et événements de connexion.
- <code>DeviceStateMerger</code> — déduplication LE/Classic et identités stables.
- <code>BatteryReader</code> et <code>PnpBatteryReader</code> — recherche ordonnée des sources de batterie.
- <code>TrayIconService</code> — icône et menu natifs, état de survol et récupération après redémarrage d’Explorer.
- <code>PanelPlacementService</code> — placement tenant compte du DPI pour chaque bord de barre des tâches et plusieurs moniteurs.
- <code>TrayPanelViewModel</code> — état MVVM, actualisation single-flight et mise à jour des collections via le Dispatcher.
- <code>LocalizationService</code> et <code>ThemeManager</code> — changement de langue et d’apparence à l’exécution.
- <code>NotificationService</code> — notifications Windows App SDK avec l’AppUserModelID <code>BluetoothNotify.App</code>.
- <code>SettingsStore</code> et <code>AppLogger</code> — persistance JSON atomique et journaux locaux rotatifs.

</details>

## Limites Bluetooth

L’état de connexion est en lecture seule. Windows ne fournit pas d’API publique unique permettant de connecter ou déconnecter toutes les catégories d’appareils Bluetooth ; Bluetooth Notify ne jumelle donc pas les appareils et n’utilise ni le Gestionnaire de périphériques, ni des modifications du Registre, ni des commandes HID rétroconçues, ni des protocoles propres aux fabricants.

La disponibilité de la batterie dépend de ce que Windows et l’appareil exposent. La propriété HFP/PnP est une solution de repli isolée, non documentée et sans garantie ; si elle change ou manque, les autres sources continuent de fonctionner. Pour un appareil déconnecté, l’application peut afficher une valeur système clairement marquée comme obsolète et n’ouvre pas une connexion GATT uniquement pour la mettre à jour.

## Tests

~~~powershell
dotnet test .\BluetoothNotify.slnx -c Release
~~~

Les tests couvrent la fusion LE/Classic, les identifiants stables, le tri, l’association des icônes, les seuils et valeurs de batterie absentes, l’anti-rebond des connexions, l’actualisation single-flight, les paramètres, la localisation, l’apparence et le placement du panneau à 100/125/150/200 % de DPI.

Le [rapport de vérification détaillé (en russe)](./docs/verification-report.md) contient les contrôles matériels et d’installation enregistrés.

## Références

- [Vue d’ensemble des notifications d’application Windows](https://learn.microsoft.com/windows/apps/develop/notifications/)
- [Client Bluetooth GATT](https://learn.microsoft.com/windows/apps/develop/devices-sensors/gatt-client)
- [Shell_NotifyIcon](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyiconw)
- [Shell_NotifyIconGetRect](https://learn.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyicongetrect)
- [Mentions relatives aux composants tiers](./THIRD-PARTY-NOTICES.md)
