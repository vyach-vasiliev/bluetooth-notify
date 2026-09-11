const siteHeaderCopy = {
  en: { htmlLang: "en-US", label: "English (United States)", short: "EN", languageLabel: "Select language", navigationLabel: "Main navigation", nav: { features: "Features", interface: "Interface", requirements: "Requirements" }, build: "Build from source", buildShort: "Build" },
  ru: { htmlLang: "ru-RU", label: "Русский", short: "RU", languageLabel: "Выбрать язык", navigationLabel: "Основная навигация", nav: { features: "Возможности", interface: "Интерфейс", requirements: "Требования" }, build: "Собрать из исходников", buildShort: "Сборка" },
  de: { htmlLang: "de-DE", label: "Deutsch", short: "DE", languageLabel: "Sprache auswählen", navigationLabel: "Hauptnavigation", nav: { features: "Funktionen", interface: "Oberfläche", requirements: "Anforderungen" }, build: "Aus Quellcode erstellen", buildShort: "Erstellen" },
  ja: { htmlLang: "ja-JP", label: "日本語", short: "JA", languageLabel: "言語を選択", navigationLabel: "メインナビゲーション", nav: { features: "機能", interface: "画面", requirements: "動作要件" }, build: "ソースからビルド", buildShort: "ビルド" },
  fr: { htmlLang: "fr-FR", label: "Français", short: "FR", languageLabel: "Choisir la langue", navigationLabel: "Navigation principale", nav: { features: "Fonctions", interface: "Interface", requirements: "Configuration" }, build: "Compiler depuis les sources", buildShort: "Compiler" },
  es: { htmlLang: "es-ES", label: "Español", short: "ES", languageLabel: "Seleccionar idioma", navigationLabel: "Navegación principal", nav: { features: "Funciones", interface: "Interfaz", requirements: "Requisitos" }, build: "Compilar desde el código", buildShort: "Compilar" },
  pt: { htmlLang: "pt-PT", label: "Português", short: "PT", languageLabel: "Selecionar idioma", navigationLabel: "Navegação principal", nav: { features: "Funcionalidades", interface: "Interface", requirements: "Requisitos" }, build: "Compilar a partir do código", buildShort: "Compilar" },
  zh: { htmlLang: "zh-CN", label: "简体中文", short: "ZH", languageLabel: "选择语言", navigationLabel: "主导航", nav: { features: "功能", interface: "界面", requirements: "系统要求" }, build: "从源代码构建", buildShort: "构建" },
  ko: { htmlLang: "ko-KR", label: "한국어", short: "KO", languageLabel: "언어 선택", navigationLabel: "기본 탐색", nav: { features: "기능", interface: "인터페이스", requirements: "요구 사항" }, build: "소스에서 빌드", buildShort: "빌드" }
};

const publisherLinkCopy = {
  en: { label: "Publisher links", website: "Website", repository: "Source code", contact: "Contact", license: "License", hosting: "Hosting privacy" },
  ru: { label: "Ссылки издателя", website: "Сайт", repository: "Исходный код", contact: "Контакт", license: "Лицензия", hosting: "Конфиденциальность хостинга" },
  de: { label: "Links des Herausgebers", website: "Website", repository: "Quellcode", contact: "Kontakt", license: "Lizenz", hosting: "Datenschutz des Hosters" },
  ja: { label: "発行者リンク", website: "ウェブサイト", repository: "ソースコード", contact: "連絡先", license: "ライセンス", hosting: "ホスティングのプライバシー" },
  fr: { label: "Liens de l’éditeur", website: "Site web", repository: "Code source", contact: "Contact", license: "Licence", hosting: "Confidentialité de l’hébergeur" },
  es: { label: "Enlaces del editor", website: "Sitio web", repository: "Código fuente", contact: "Contacto", license: "Licencia", hosting: "Privacidad del alojamiento" },
  pt: { label: "Links do editor", website: "Website", repository: "Código-fonte", contact: "Contacto", license: "Licença", hosting: "Privacidade do alojamento" },
  zh: { label: "发布者链接", website: "网站", repository: "源代码", contact: "联系方式", license: "许可证", hosting: "托管隐私" },
  ko: { label: "게시자 링크", website: "웹사이트", repository: "소스 코드", contact: "연락처", license: "라이선스", hosting: "호스팅 개인정보" }
};

const policies = {
  en: {
    language: "Language",
    eyebrow: "LEGAL / LOCAL-FIRST",
    title: "Legal information",
    summary: "Privacy Policy, Terms of Use, and Disclaimer for the unmodified Bluetooth Notify desktop application and its static website.",
    updated: "Effective and last updated: 11 September 2026 · Version 1.2",
    back: "Back to the website",
    nav: { privacy: "Privacy policy", terms: "Terms of use", disclaimer: "Disclaimer" },
    operator: "Publisher and contact. Bluetooth Notify is published by the developer operating the GitHub account “vyach-vasiliev”. The official website is bluetooth-notify.onrender.com and is hosted as a Render static site. Source code is published at github.com/vyach-vasiliev/bluetooth-notify. Send privacy, legal, and support requests through the GitHub profile github.com/vyach-vasiliev/.",
    documents: [
      { id: "privacy", title: "Privacy Policy", clauses: [
        ["1. Scope and roles", ["This Policy covers Bluetooth Notify, its installer, and the static product website supplied with this source. It does not cover Microsoft, Windows, a hosting provider, or external websites. The publisher does not receive personal data from the unmodified app. On-device activity is performed under the user’s control; the operator of any deployed website is responsible for request data handled by its host."]],
        ["2. Data handled by the app", ["To show Bluetooth status, the app asks Windows for paired-device names, endpoint and container identifiers, Bluetooth addresses when available, device class/category, pairing and connection state, and battery level. These device details are used in memory and are not persisted by the app.", "The app stores notification thresholds, notification state, language, theme, and a schema version in %LocalAppData%\\BluetoothNotify\\settings.json. Rotating local logs store timestamps, app events, error types, and error messages (up to five files of about 512 KiB each). Logs do not intentionally record device names or addresses, although operating-system error text could contain local technical details."]],
        ["3. Website and installer", ["The static website at bluetooth-notify.onrender.com sets no cookies, uses no analytics, advertising, accounts, forms, pixels, or fingerprinting, and sends no telemetry. It is hosted by Render. Render may process device/IP and infrastructure data under its privacy policy; Render states that static sites do not emit application request logs. External Microsoft links are governed by Microsoft’s notices.", "The installer checks locally for Microsoft prerequisites and may create a local setup log containing paths, operating-system/app details, and errors. It opens Microsoft download pages only after the user agrees. It does not send installation data to the publisher."]],
        ["4. Purposes, legal bases, and sharing", ["Local data is used only to provide device status, battery notifications, preferences, troubleshooting, installation, updates, and uninstallation. Where a legal basis is required, processing necessary to provide the user-requested software relies on performance of the user agreement; limited website security logs may rely on the operator’s legitimate interest in secure delivery. Consent is required where local law requires it.", "The unmodified app does not sell, share for cross-context behavioral advertising, rent, upload, or disclose app data to the publisher or third parties; it performs no profiling or legally significant automated decisions. The publisher has collected, sold, or shared no app-derived personal information during the preceding 12 months."]],
        ["5. Retention and security", ["In-memory Bluetooth data disappears when the app exits. Settings and logs remain on the device until the user deletes %LocalAppData%\\BluetoothNotify or chooses removal during uninstall; log rotation limits size but not a fixed time period. Setup logs remain where Windows/Inno Setup places them until the user or operating system removes them.", "Protection relies on the user’s Windows account and device security. Keep Windows and dependencies updated, restrict account access, and inspect logs before sharing them. No security method is guaranteed."]],
        ["6. Choices and regional rights", ["Users can disable notifications, delete local files, uninstall the app, and avoid external links. Because the publisher receives no app data, access, correction, deletion, portability, restriction, objection, or consent-withdrawal requests are normally completed directly on the device.", "Where applicable, residents retain rights under the GDPR/UK GDPR (including complaint to a supervisory authority), Russia’s Federal Law No. 152-FZ, California privacy law (know, delete, correct, opt out of sale/sharing, limit sensitive-data use, and non-discrimination), Japan’s APPI, Korea’s PIPA, and China’s PIPL. The unmodified app makes no publisher-controlled cross-border transfer. Contact the relevant deployment operator for host-held request logs. The app is general-purpose and does not knowingly collect children’s data."]]
      ]},
      { id: "terms", title: "Terms of Use", clauses: [
        ["1. Agreement and eligibility", ["By installing or using Bluetooth Notify, you agree to these Terms. If you do not agree, do not install or use it. You must be legally capable of agreeing, or use it with authorization from a parent, guardian, or organization. The person installing for an organization confirms authority to bind it."]],
        ["2. License", ["Bluetooth Notify is free and open-source software licensed under the GNU Affero General Public License version 3 only (AGPL-3.0-only). Commercial use is permitted. Anyone who distributes the original or a modified covered work must keep it under the AGPL and provide the corresponding source as required by the license. If a modified version supports remote network interaction, its users must also be offered access to the corresponding source. The complete license is supplied with the app and repository."]],
        ["3. Responsible use", ["Do not use the app unlawfully; bypass platform security; introduce malware; misrepresent the publisher; or redistribute a modified build as official. You are responsible for device access, backups, configuration, and checking whether Bluetooth hardware and drivers are compatible."]],
        ["4. Third parties and changes", ["Windows, .NET, Windows App Runtime, hardware, drivers, notification services, hosting, and linked sites are supplied by third parties under their own terms. The app may be changed, updated, suspended, or discontinued. Material legal changes apply prospectively from the posted date; continued use after notice constitutes acceptance where law permits."]],
        ["5. Termination", ["You may end these Terms at any time by uninstalling and deleting local data. The license may end after a material breach. Provisions concerning ownership, disclaimers, liability, disputes, and mandatory rights survive where their nature requires."]],
        ["6. Consumer rights and disputes", ["Nothing in these Terms waives warranties, remedies, withdrawal rights, jurisdiction, or other protections that applicable consumer law does not allow parties to exclude. Mandatory law of the user’s habitual residence prevails. Otherwise, disputes are governed by the law applicable to the publisher identified by the distributor, without applying conflict-of-law rules. Contact the distributor first to seek an informal resolution."]]
      ]},
      { id: "disclaimer", title: "Disclaimer", clauses: [
        ["1. Battery and connection data", ["Bluetooth Notify reports best-effort information exposed by Windows, devices, and drivers. Values may be missing, delayed, cached, stale, duplicated, or incorrect. A notification may be late, absent, or repeated. Verify critical battery or connection status using the device manufacturer’s tools."]],
        ["2. Not for critical use", ["The app is a convenience utility, not a safety, emergency, medical, accessibility, security, industrial, or life-support system. Do not rely on it where failure could cause injury, loss of property, loss of data, or interruption of essential services."]],
        ["3. Warranties", ["To the maximum extent permitted by law, the app and website are supplied “as is” and “as available”, without express or implied warranties of accuracy, uninterrupted operation, compatibility, merchantability, fitness for a particular purpose, or non-infringement. No statement creates a warranty unless mandatory law says otherwise."]],
        ["4. Liability", ["To the maximum extent permitted by law, the publisher and contributors are not liable for indirect, incidental, special, consequential, or punitive loss, or loss of data, profit, opportunity, or device availability arising from use or inability to use the app. Nothing excludes liability that cannot lawfully be excluded, including liability for fraud, intentional misconduct, gross negligence where applicable, death or personal injury caused by negligence, or mandatory consumer remedies."]]
      ]}
    ]
  },

  ru: {
    language: "Язык",
    eyebrow: "ПРАВОВАЯ ИНФОРМАЦИЯ / ЛОКАЛЬНАЯ ОБРАБОТКА",
    title: "Правовая информация",
    summary: "Политика конфиденциальности, Условия использования и Отказ от ответственности для неизменённого приложения Bluetooth Notify и его статического сайта.",
    updated: "Действует и обновлено: 11 сентября 2026 г. · Версия 1.2",
    back: "Вернуться на сайт",
    nav: { privacy: "Конфиденциальность", terms: "Условия использования", disclaimer: "Отказ от ответственности" },
    operator: "Издатель и контакты. Bluetooth Notify выпускает разработчик — владелец учётной записи GitHub «vyach-vasiliev». Официальный сайт bluetooth-notify.onrender.com размещён как статический сайт на Render. Исходный код опубликован в github.com/vyach-vasiliev/bluetooth-notify. Запросы по конфиденциальности, правовым вопросам и поддержке направляйте через профиль github.com/vyach-vasiliev/.",
    documents: [
      { id: "privacy", title: "Политика конфиденциальности", clauses: [
        ["1. Область действия и роли", ["Политика относится к Bluetooth Notify, установщику и статическому сайту из этого проекта. Она не регулирует Microsoft, Windows, хостинг-провайдера и внешние сайты. Издатель не получает персональные данные из неизменённого приложения. Обработка на устройстве выполняется под контролем пользователя; оператор опубликованного сайта отвечает за данные запросов, обрабатываемые его хостингом."]],
        ["2. Данные в приложении", ["Чтобы показать Bluetooth-статус, приложение запрашивает у Windows имена сопряжённых устройств, идентификаторы endpoint и container, Bluetooth-адрес при наличии, класс/категорию, состояние сопряжения и подключения и уровень заряда. Эти сведения используются в оперативной памяти и приложением на диск не сохраняются.", "В %LocalAppData%\\BluetoothNotify\\settings.json хранятся пороги и состояние уведомлений, язык, тема и версия схемы. Локальные журналы с ротацией содержат время, события приложения, типы и тексты ошибок — не более пяти файлов примерно по 512 КиБ. Имена и адреса устройств намеренно не журналируются, но текст ошибки Windows иногда может содержать локальные технические сведения."]],
        ["3. Сайт и установщик", ["Статический сайт bluetooth-notify.onrender.com не устанавливает cookie, не использует аналитику, рекламу, аккаунты, формы, пиксели, fingerprinting или телеметрию. Сайт размещён на Render. Render может обрабатывать сведения об устройстве/IP и инфраструктуре по своей политике конфиденциальности; согласно документации Render статические сайты не создают журналы запросов приложения. Для внешних ссылок Microsoft действуют документы Microsoft.", "Установщик локально проверяет компоненты Microsoft и может создавать локальный журнал с путями, сведениями об ОС/приложении и ошибками. Страницы загрузки Microsoft открываются только после согласия пользователя. Данные установки издателю не отправляются."]],
        ["4. Цели, основания и передача", ["Локальные данные используются только для отображения устройств, уведомлений о заряде, настроек, диагностики, установки, обновления и удаления. Когда требуется правовое основание, необходимая для запрошенной программы обработка основана на исполнении пользовательского соглашения; ограниченные журналы безопасности сайта — на законном интересе оператора в безопасной доставке. Согласие запрашивается, если этого требует применимое право.", "Неизменённое приложение не продаёт, не сдаёт в аренду, не загружает и не передаёт издателю или третьим лицам данные приложения, не использует их для межконтекстной поведенческой рекламы, профилирования или юридически значимых автоматических решений. За предыдущие 12 месяцев издатель не собирал, не продавал и не передавал полученные из приложения персональные сведения."]],
        ["5. Сроки и безопасность", ["Bluetooth-данные в памяти исчезают при завершении приложения. Настройки и журналы остаются на устройстве до удаления папки %LocalAppData%\\BluetoothNotify либо выбора их удаления при деинсталляции; ротация ограничивает размер журналов, но не задаёт срок в днях. Журналы установки остаются в месте, выбранном Windows/Inno Setup, пока пользователь или ОС их не удалит.", "Защита основана на безопасности учётной записи Windows и устройства. Обновляйте Windows и зависимости, ограничивайте доступ и проверяйте журналы перед передачей. Абсолютная безопасность не гарантируется."]],
        ["6. Выбор и региональные права", ["Пользователь может отключить уведомления, удалить локальные файлы, деинсталлировать приложение и не переходить по внешним ссылкам. Поскольку издатель не получает данные приложения, доступ, исправление, удаление, перенос, ограничение, возражение и отзыв согласия обычно выполняются самим пользователем на устройстве.", "В применимых случаях сохраняются права по GDPR/UK GDPR (включая жалобу надзорному органу), Федеральному закону РФ № 152-ФЗ, законам Калифорнии (знать, удалить, исправить, отказаться от продажи/передачи, ограничить чувствительные данные и не подвергаться дискриминации), APPI Японии, PIPA Республики Корея и PIPL КНР. Неизменённое приложение не выполняет контролируемую издателем трансграничную передачу. По серверным журналам обращайтесь к оператору размещения. Приложение имеет общее назначение и намеренно не собирает данные детей."]]
      ]},
      { id: "terms", title: "Условия использования", clauses: [
        ["1. Согласие и дееспособность", ["Устанавливая или используя Bluetooth Notify, вы соглашаетесь с Условиями. При несогласии не устанавливайте и не используйте программу. Вы должны иметь право заключить такое соглашение либо действовать с разрешения родителя, опекуна или организации. Устанавливающий для организации подтверждает свои полномочия."]],
        ["2. Лицензия", ["Bluetooth Notify — свободное программное обеспечение с открытым исходным кодом по GNU Affero General Public License версии 3 без автоматического перехода на последующие версии (AGPL-3.0-only). Коммерческое использование разрешено. При распространении оригинального или изменённого производного произведения необходимо сохранить AGPL и предоставить соответствующий исходный код согласно лицензии. Если изменённая версия поддерживает удалённое взаимодействие по сети, её пользователям также должен быть предложен доступ к соответствующему исходному коду. Полный текст лицензии поставляется с приложением и репозиторием."]],
        ["3. Допустимое использование", ["Не используйте приложение незаконно, не обходите защиту платформы, не внедряйте вредоносный код, не выдавайте себя за издателя и не распространяйте изменённую сборку как официальную. Вы отвечаете за доступ к устройству, резервные копии, конфигурацию и проверку совместимости Bluetooth-оборудования и драйверов."]],
        ["4. Третьи лица и изменения", ["Windows, .NET, Windows App Runtime, оборудование, драйверы, уведомления, хостинг и внешние сайты предоставляются третьими лицами на их условиях. Приложение может изменяться, обновляться, приостанавливаться или прекращаться. Существенные правовые изменения действуют на будущее с даты публикации; продолжение использования после уведомления означает принятие там, где это допускает закон."]],
        ["5. Прекращение", ["Вы можете прекратить соглашение, удалив приложение и локальные данные. Лицензия может прекратиться после существенного нарушения. Положения о правах, отказах, ответственности, спорах и обязательных правах продолжают действовать, если это следует из их существа."]],
        ["6. Права потребителя и споры", ["Условия не отменяют гарантии, способы защиты, право на отказ, подсудность и иные права, которые нельзя исключить применимым законодательством о потребителях. Обязательное право места обычного проживания пользователя имеет преимущество. В остальном применяется право издателя, указанного распространителем, без коллизионных норм. Сначала обратитесь к распространителю для мирного урегулирования."]]
      ]},
      { id: "disclaimer", title: "Отказ от ответственности", clauses: [
        ["1. Заряд и подключение", ["Bluetooth Notify показывает по принципу best effort сведения, доступные через Windows, устройство и драйвер. Значения могут отсутствовать, запаздывать, быть кэшированными, устаревшими, дублированными или неверными. Уведомление может опоздать, не появиться или повториться. Критичные данные проверяйте средствами производителя."]],
        ["2. Не для критичного применения", ["Это вспомогательная утилита, а не система безопасности, экстренной помощи, медицины, доступности, охраны, промышленного управления или жизнеобеспечения. Не полагайтесь на неё там, где сбой может причинить вред, утрату имущества/данных или остановку важных услуг."]],
        ["3. Гарантии", ["В максимально допустимой законом степени приложение и сайт предоставляются «как есть» и «по доступности», без явных или подразумеваемых гарантий точности, непрерывности, совместимости, товарной пригодности, пригодности для конкретной цели и ненарушения прав. Заявления не создают гарантию, если иное не требует закон."]],
        ["4. Ограничение ответственности", ["В максимально допустимой законом степени издатель и участники не отвечают за косвенные, случайные, специальные, последующие или штрафные убытки, потерю данных, прибыли, возможностей или доступности устройства из-за использования или невозможности использования. Не исключается ответственность, которую закон запрещает исключать, включая ответственность за обман, умысел, грубую неосторожность, когда применимо, смерть или вред здоровью по неосторожности и обязательные средства защиты потребителя."]]
      ]}
    ]
  },

  de: {
    language: "Sprache", eyebrow: "RECHT / LOKAL ZUERST", title: "Rechtliche Hinweise",
    summary: "Datenschutzerklärung, Nutzungsbedingungen und Haftungsausschluss für die unveränderte Bluetooth-Notify-Desktop-App und ihre statische Website.",
    updated: "Gültig und zuletzt aktualisiert: 11. September 2026 · Version 1.2", back: "Zurück zur Website",
    nav: { privacy: "Datenschutz", terms: "Nutzungsbedingungen", disclaimer: "Haftungsausschluss" },
    operator: "Herausgeber und Kontakt. Bluetooth Notify wird von dem Entwickler veröffentlicht, der das GitHub-Konto „vyach-vasiliev“ betreibt. Die offizielle Website bluetooth-notify.onrender.com wird als statische Website bei Render gehostet. Der Quellcode liegt unter github.com/vyach-vasiliev/bluetooth-notify. Datenschutz-, Rechts- und Supportanfragen können über github.com/vyach-vasiliev/ gestellt werden.",
    documents: [
      { id: "privacy", title: "Datenschutzerklärung", clauses: [
        ["1. Geltungsbereich und Rollen", ["Diese Erklärung gilt für Bluetooth Notify, den Installer und die statische Projektwebsite. Microsoft, Windows, Hosting-Anbieter und externe Websites sind nicht erfasst. Der Herausgeber erhält aus der unveränderten App keine personenbezogenen Daten. Die Verarbeitung auf dem Gerät steht unter Kontrolle des Nutzers; der Betreiber einer bereitgestellten Website verantwortet die vom Hoster verarbeiteten Anfragedaten."]],
        ["2. Daten in der App", ["Zur Statusanzeige fragt die App bei Windows Namen gekoppelter Geräte, Endpoint-/Container-Kennungen, verfügbare Bluetooth-Adressen, Klasse/Kategorie, Kopplungs- und Verbindungsstatus sowie Akkustand ab. Diese Gerätedaten werden nur im Arbeitsspeicher genutzt und nicht von der App gespeichert.", "In %LocalAppData%\\BluetoothNotify\\settings.json stehen Benachrichtigungsschwellen und -status, Sprache, Design und Schemaversion. Rotierende lokale Protokolle enthalten Zeitstempel, App-Ereignisse, Fehlertypen und -texte (höchstens fünf Dateien zu etwa 512 KiB). Gerätenamen/-adressen werden nicht gezielt protokolliert; Windows-Fehlertexte können jedoch lokale technische Angaben enthalten."]],
        ["3. Website und Installer", ["Die statische Website bluetooth-notify.onrender.com setzt keine Cookies und nutzt keine Analyse, Werbung, Konten, Formulare, Pixel, Fingerprinting oder Telemetrie. Sie wird bei Render gehostet. Render kann Geräte-/IP- und Infrastrukturdaten gemäß seiner Datenschutzerklärung verarbeiten; laut Render erzeugen statische Websites keine Anwendungs-Anfrageprotokolle. Für Microsoft-Links gelten Microsofts Hinweise.", "Der Installer prüft Microsoft-Voraussetzungen lokal und kann ein lokales Setup-Protokoll mit Pfaden, System-/App-Daten und Fehlern anlegen. Microsoft-Downloads werden nur nach Zustimmung geöffnet. Installationsdaten gehen nicht an den Herausgeber."]],
        ["4. Zwecke, Rechtsgrundlagen, Empfänger", ["Lokale Daten dienen nur Gerätestatus, Akkuhinweisen, Einstellungen, Fehlerdiagnose, Installation, Aktualisierung und Deinstallation. Soweit nötig, beruht die nutzerseitig verlangte Verarbeitung auf Vertragserfüllung; begrenzte Sicherheitslogs der Website können auf dem berechtigten Interesse an sicherer Auslieferung beruhen. Eine Einwilligung wird eingeholt, wenn das Recht dies verlangt.", "Die unveränderte App verkauft, vermietet, lädt oder übermittelt App-Daten weder an Herausgeber noch Dritte; keine verhaltensbezogene Werbung, kein Profiling und keine rechtlich erheblichen automatisierten Entscheidungen. In den letzten zwölf Monaten wurden keine aus der App stammenden personenbezogenen Daten gesammelt, verkauft oder geteilt."]],
        ["5. Aufbewahrung und Sicherheit", ["Bluetooth-Daten im Speicher verschwinden beim Beenden. Einstellungen und Logs bleiben bis zur Löschung von %LocalAppData%\\BluetoothNotify oder der entsprechenden Auswahl bei der Deinstallation; Rotation begrenzt die Größe, nicht die Zeit. Setup-Logs bleiben lokal, bis Nutzer oder System sie löschen.", "Der Schutz hängt vom Windows-Konto und Gerät ab. Windows und Abhängigkeiten aktuell halten, Kontozugriff beschränken und Logs vor Weitergabe prüfen. Absolute Sicherheit besteht nicht."]],
        ["6. Rechte", ["Benachrichtigungen lassen sich deaktivieren, lokale Dateien löschen, die App deinstallieren und externe Links meiden. Da der Herausgeber keine App-Daten erhält, werden Auskunft, Berichtigung, Löschung, Übertragbarkeit, Einschränkung, Widerspruch und Widerruf regelmäßig direkt am Gerät umgesetzt.", "Anwendbare Rechte nach DSGVO/UK GDPR, russischem Gesetz Nr. 152-FZ, kalifornischem Datenschutzrecht, japanischem APPI, koreanischem PIPA und chinesischem PIPL bleiben bestehen, einschließlich Beschwerde bei einer Aufsichtsbehörde. Die unveränderte App veranlasst keinen vom Herausgeber kontrollierten Drittlandtransfer. Für Serverlogs ist der Betreiber zuständig. Die App richtet sich allgemein und erhebt nicht wissentlich Kinderdaten."]]
      ]},
      { id: "terms", title: "Nutzungsbedingungen", clauses: [
        ["1. Zustimmung", ["Mit Installation oder Nutzung stimmen Sie diesen Bedingungen zu. Andernfalls nicht installieren oder nutzen. Sie müssen rechtlich zustimmen können oder von Eltern, Vormund bzw. Organisation ermächtigt sein. Wer für eine Organisation installiert, bestätigt seine Vertretungsmacht."]],
        ["2. Lizenz", ["Bluetooth Notify ist freie Open-Source-Software unter der GNU Affero General Public License, ausschließlich Version 3 (AGPL-3.0-only). Kommerzielle Nutzung ist erlaubt. Wer das unveränderte oder ein abgeleitetes Werk verbreitet, muss es unter der AGPL belassen und den entsprechenden Quellcode gemäß der Lizenz bereitstellen. Unterstützt eine geänderte Version die Interaktion über ein Netzwerk, muss ihren Nutzern ebenfalls Zugang zum entsprechenden Quellcode angeboten werden. Der vollständige Lizenztext liegt App und Repository bei."]],
        ["3. Zulässige Nutzung", ["Keine rechtswidrige Nutzung, Umgehung von Plattformsicherheit, Schadsoftware, falsche Herausgeberdarstellung oder Verteilung eines geänderten Builds als offiziell. Sie verantworten Gerätezugriff, Sicherungen, Konfiguration und Hardware-/Treiberkompatibilität."]],
        ["4. Dritte und Änderungen", ["Windows, .NET, Windows App Runtime, Hardware, Treiber, Benachrichtigungen, Hosting und Links stammen von Dritten und unterliegen deren Bedingungen. Die App kann geändert, aktualisiert, ausgesetzt oder eingestellt werden. Wesentliche Rechtsänderungen gelten künftig ab Veröffentlichung; weitere Nutzung gilt nur soweit zulässig als Zustimmung."]],
        ["5. Beendigung", ["Sie können durch Deinstallation und Datenlöschung beenden. Bei wesentlichem Verstoß kann die Lizenz enden. Eigentum, Ausschlüsse, Haftung, Streit und zwingende Rechte gelten ihrer Natur nach fort."]],
        ["6. Verbraucherrechte", ["Unabdingbare Gewährleistungen, Abhilfen, Widerrufsrechte, Gerichtsstände und sonstige Verbraucherschutzrechte werden nicht ausgeschlossen. Zwingendes Recht des gewöhnlichen Aufenthalts geht vor. Sonst gilt das vom Vertreiber angegebene Recht des Herausgebers ohne Kollisionsrecht. Kontaktieren Sie zunächst den Vertreiber zur gütlichen Lösung."]]
      ]},
      { id: "disclaimer", title: "Haftungsausschluss", clauses: [
        ["1. Statusdaten", ["Bluetooth Notify zeigt bestmöglich Angaben von Windows, Geräten und Treibern. Werte können fehlen, verspätet, zwischengespeichert, veraltet, doppelt oder falsch sein; Hinweise können ausbleiben, spät oder mehrfach erscheinen. Kritische Angaben mit Herstellerwerkzeugen prüfen."]],
        ["2. Keine kritische Nutzung", ["Die App ist ein Komfortwerkzeug, kein Sicherheits-, Notfall-, Medizin-, Barrierefreiheits-, Schutz-, Industrie- oder Lebenserhaltungssystem. Nicht nutzen, wenn ein Ausfall Verletzungen, Sach-/Datenverlust oder Ausfall wesentlicher Dienste verursachen kann."]],
        ["3. Gewährleistung", ["Soweit gesetzlich zulässig werden App und Website „wie besehen“ und „wie verfügbar“ ohne ausdrückliche oder stillschweigende Zusagen zu Richtigkeit, Unterbrechungsfreiheit, Kompatibilität, Marktgängigkeit, Eignung oder Rechtsmängelfreiheit bereitgestellt."]],
        ["4. Haftung", ["Soweit zulässig haften Herausgeber und Mitwirkende nicht für mittelbare, zufällige, besondere, Folge- oder Strafschäden sowie Daten-, Gewinn-, Chancen- oder Verfügbarkeitsverlust. Unabdingbare Haftung bleibt unberührt, insbesondere für Arglist, Vorsatz, gegebenenfalls grobe Fahrlässigkeit, fahrlässig verursachte Verletzung von Leben/Körper/Gesundheit und zwingende Verbraucheransprüche."]]
      ]}
    ]
  },

  fr: {
    language: "Langue", eyebrow: "JURIDIQUE / TRAITEMENT LOCAL", title: "Informations juridiques",
    summary: "Politique de confidentialité, Conditions d’utilisation et Clause de non-responsabilité de l’application Bluetooth Notify non modifiée et de son site statique.",
    updated: "Entrée en vigueur et dernière mise à jour : 11 septembre 2026 · Version 1.2", back: "Retour au site",
    nav: { privacy: "Confidentialité", terms: "Conditions d’utilisation", disclaimer: "Non-responsabilité" },
    operator: "Éditeur et contact. Bluetooth Notify est publié par le développeur qui exploite le compte GitHub « vyach-vasiliev ». Le site officiel bluetooth-notify.onrender.com est hébergé comme site statique par Render. Le code source est publié sur github.com/vyach-vasiliev/bluetooth-notify. Les demandes relatives à la vie privée, au droit et à l’assistance peuvent être adressées via github.com/vyach-vasiliev/.",
    documents: [
      { id: "privacy", title: "Politique de confidentialité", clauses: [
        ["1. Champ et rôles", ["La Politique couvre Bluetooth Notify, son installateur et le site statique fourni avec le projet. Elle ne couvre pas Microsoft, Windows, l’hébergeur ni les sites externes. L’éditeur ne reçoit aucune donnée personnelle de l’application non modifiée. Les opérations sur l’appareil restent sous le contrôle de l’utilisateur ; l’opérateur d’un site déployé répond des données de requête traitées par son hébergeur."]],
        ["2. Données de l’application", ["Pour afficher l’état Bluetooth, l’application demande à Windows le nom des appareils appairés, les identifiants de point de terminaison et de conteneur, l’adresse Bluetooth disponible, la classe/catégorie, l’état d’appairage/connexion et la batterie. Ces informations restent en mémoire et ne sont pas enregistrées par l’application.", "Le fichier %LocalAppData%\\BluetoothNotify\\settings.json contient seuils et état des notifications, langue, thème et version de schéma. Les journaux locaux rotatifs contiennent horodatages, événements, types et messages d’erreur (cinq fichiers d’environ 512 Kio maximum). Les noms/adresses ne sont pas volontairement journalisés, mais un message Windows peut contenir des détails techniques locaux."]],
        ["3. Site et installateur", ["Le site statique bluetooth-notify.onrender.com ne place aucun cookie et n’utilise ni analyse, publicité, compte, formulaire, pixel, empreinte ni télémétrie. Il est hébergé par Render. Render peut traiter des données d’appareil/IP et d’infrastructure selon sa politique de confidentialité ; Render indique que les sites statiques ne produisent pas de journaux de requêtes applicatives. Les liens Microsoft relèvent des avis Microsoft.", "L’installateur vérifie localement les composants Microsoft et peut créer un journal local avec chemins, informations système/application et erreurs. Il n’ouvre les téléchargements Microsoft qu’après accord. Aucune donnée d’installation n’est envoyée à l’éditeur."]],
        ["4. Finalités, bases et partage", ["Les données locales servent uniquement à l’état des appareils, alertes de batterie, préférences, diagnostic, installation, mise à jour et désinstallation. Si une base est requise, le traitement demandé repose sur l’exécution du contrat ; des journaux de sécurité limités du site peuvent reposer sur l’intérêt légitime de livraison sûre. Le consentement est demandé lorsque la loi l’impose.", "L’application non modifiée ne vend, loue, téléverse ni communique les données à l’éditeur ou à des tiers ; aucune publicité comportementale intercontextuelle, aucun profilage ni décision automatisée à effet juridique. Sur les douze derniers mois, l’éditeur n’a collecté, vendu ou partagé aucune donnée personnelle issue de l’application."]],
        ["5. Conservation et sécurité", ["Les données Bluetooth en mémoire disparaissent à la fermeture. Réglages et journaux restent jusqu’à suppression de %LocalAppData%\\BluetoothNotify ou choix de suppression lors de la désinstallation ; la rotation limite la taille, pas une durée fixe. Les journaux d’installation restent locaux jusqu’à suppression par l’utilisateur ou le système.", "La protection dépend du compte Windows et de l’appareil. Maintenez Windows et les dépendances à jour, limitez l’accès et vérifiez les journaux avant partage. Aucune méthode n’est infaillible."]],
        ["6. Choix et droits", ["Vous pouvez désactiver les notifications, supprimer les fichiers locaux, désinstaller et éviter les liens externes. Comme l’éditeur ne reçoit aucune donnée de l’application, accès, rectification, effacement, portabilité, limitation, opposition et retrait s’exercent normalement sur l’appareil.", "Les droits applicables du RGPD/UK GDPR, de la loi russe 152-FZ, du droit californien, de l’APPI japonaise, de la PIPA coréenne et de la PIPL chinoise demeurent, y compris la réclamation auprès d’une autorité. Aucun transfert transfrontalier contrôlé par l’éditeur n’est réalisé par l’application non modifiée. Contactez l’opérateur pour les journaux serveur. L’application est généraliste et ne collecte pas sciemment de données d’enfants."]]
      ]},
      { id: "terms", title: "Conditions d’utilisation", clauses: [
        ["1. Acceptation", ["En installant ou utilisant Bluetooth Notify, vous acceptez ces Conditions. Sinon, ne l’installez pas. Vous devez avoir la capacité juridique ou l’autorisation d’un parent, tuteur ou organisme. L’installation pour une organisation suppose le pouvoir de l’engager."]],
        ["2. Licence", ["Bluetooth Notify est un logiciel libre et open source sous GNU Affero General Public License, version 3 uniquement (AGPL-3.0-only). L’usage commercial est autorisé. Toute distribution de l’œuvre originale ou d’une œuvre dérivée doit conserver l’AGPL et fournir le code source correspondant selon la licence. Si une version modifiée permet une interaction distante par réseau, ses utilisateurs doivent également pouvoir obtenir le code source correspondant. Le texte complet accompagne l’application et le dépôt."]],
        ["3. Usage responsable", ["N’utilisez pas l’application illégalement, ne contournez pas la sécurité, n’introduisez pas de logiciel malveillant, ne vous faites pas passer pour l’éditeur et ne présentez pas une version modifiée comme officielle. Vous êtes responsable des accès, sauvegardes, réglages et de la compatibilité matériel/pilotes."]],
        ["4. Tiers et changements", ["Windows, .NET, Windows App Runtime, matériel, pilotes, notifications, hébergement et liens sont fournis par des tiers selon leurs conditions. L’application peut évoluer, être suspendue ou arrêtée. Les changements juridiques importants valent pour l’avenir dès publication ; l’usage continu vaut acceptation lorsque la loi le permet."]],
        ["5. Résiliation", ["Vous pouvez résilier en désinstallant et supprimant les données. La licence peut cesser après manquement substantiel. Propriété, exclusions, responsabilité, litiges et droits impératifs survivent selon leur nature."]],
        ["6. Consommateurs et litiges", ["Aucune garantie, recours, rétractation, compétence ou protection impérative du consommateur n’est écartée. La loi impérative de la résidence habituelle prime. Sinon s’applique la loi de l’éditeur indiqué par le distributeur, hors conflit de lois. Contactez d’abord le distributeur pour une résolution amiable."]]
      ]},
      { id: "disclaimer", title: "Clause de non-responsabilité", clauses: [
        ["1. Données d’état", ["Bluetooth Notify restitue au mieux les informations de Windows, des appareils et pilotes. Elles peuvent manquer, être retardées, mises en cache, obsolètes, dupliquées ou erronées ; une alerte peut manquer, tarder ou se répéter. Vérifiez toute information critique avec les outils du fabricant."]],
        ["2. Aucun usage critique", ["C’est un utilitaire de confort, non un système de sécurité, urgence, santé, accessibilité, sûreté, industrie ou survie. Ne vous y fiez pas si une panne peut causer blessure, perte de biens/données ou interruption d’un service essentiel."]],
        ["3. Garanties", ["Dans la mesure permise, l’application et le site sont fournis « en l’état » et « selon disponibilité », sans garantie expresse ou implicite d’exactitude, continuité, compatibilité, qualité marchande, adéquation ou absence de contrefaçon."]],
        ["4. Responsabilité", ["Dans la mesure permise, l’éditeur et les contributeurs ne répondent pas des pertes indirectes, accessoires, spéciales, consécutives ou punitives, ni de données, bénéfices, occasions ou disponibilité. Rien n’exclut une responsabilité légalement incompressible, notamment fraude, faute intentionnelle, faute lourde applicable, décès ou dommage corporel par négligence et recours impératifs du consommateur."]]
      ]}
    ]
  },

  es: {
    language: "Idioma", eyebrow: "LEGAL / PROCESAMIENTO LOCAL", title: "Información legal",
    summary: "Política de privacidad, Términos de uso y Descargo de responsabilidad de la aplicación Bluetooth Notify sin modificar y su web estática.",
    updated: "Vigente y actualizado por última vez: 11 de septiembre de 2026 · Versión 1.2", back: "Volver al sitio",
    nav: { privacy: "Privacidad", terms: "Términos de uso", disclaimer: "Descargo" },
    operator: "Editor y contacto. Bluetooth Notify lo publica el desarrollador que gestiona la cuenta de GitHub «vyach-vasiliev». El sitio oficial bluetooth-notify.onrender.com está alojado como sitio estático en Render. El código fuente se publica en github.com/vyach-vasiliev/bluetooth-notify. Envíe solicitudes de privacidad, legales y soporte mediante github.com/vyach-vasiliev/.",
    documents: [
      { id: "privacy", title: "Política de privacidad", clauses: [
        ["1. Alcance y funciones", ["La Política cubre Bluetooth Notify, su instalador y la web estática del proyecto. No cubre Microsoft, Windows, el alojamiento ni sitios externos. El editor no recibe datos personales de la aplicación sin modificar. El usuario controla el tratamiento en el dispositivo; el operador de una web desplegada responde de los datos de solicitud procesados por su proveedor."]],
        ["2. Datos de la aplicación", ["Para mostrar el estado Bluetooth, la aplicación solicita a Windows nombres de dispositivos emparejados, identificadores de endpoint/contenedor, dirección Bluetooth disponible, clase/categoría, estado de emparejamiento/conexión y batería. Se usan en memoria y la aplicación no los persiste.", "En %LocalAppData%\\BluetoothNotify\\settings.json se guardan umbrales/estado de avisos, idioma, tema y versión de esquema. Los registros locales rotatorios guardan fecha/hora, eventos, tipos y mensajes de error (hasta cinco archivos de unos 512 KiB). No se registran intencionadamente nombres ni direcciones, aunque errores de Windows podrían incluir detalles técnicos locales."]],
        ["3. Web e instalador", ["El sitio estático bluetooth-notify.onrender.com no instala cookies ni usa analítica, publicidad, cuentas, formularios, píxeles, huellas o telemetría. Está alojado por Render. Render puede tratar datos de dispositivo/IP e infraestructura según su política de privacidad; Render indica que los sitios estáticos no generan registros de solicitudes de la aplicación. Los enlaces de Microsoft se rigen por sus avisos.", "El instalador comprueba localmente requisitos de Microsoft y puede crear un registro local con rutas, datos del sistema/aplicación y errores. Solo abre descargas de Microsoft con permiso. No envía datos de instalación al editor."]],
        ["4. Fines, bases y cesiones", ["Los datos locales solo sirven para estado, alertas, preferencias, diagnóstico, instalación, actualización y desinstalación. Si se exige una base, el tratamiento solicitado se fundamenta en ejecutar el acuerdo; registros limitados de seguridad web pueden basarse en el interés legítimo de entrega segura. Se pide consentimiento cuando corresponda.", "La aplicación sin modificar no vende, alquila, carga ni comunica datos al editor o terceros, ni realiza publicidad conductual entre contextos, perfiles o decisiones automatizadas con efecto legal. En los últimos doce meses el editor no recopiló, vendió ni compartió información personal derivada de la aplicación."]],
        ["5. Conservación y seguridad", ["Los datos Bluetooth en memoria desaparecen al cerrar. Ajustes y registros permanecen hasta borrar %LocalAppData%\\BluetoothNotify o elegir su eliminación al desinstalar; la rotación limita tamaño, no tiempo. Los registros de instalación quedan locales hasta que usuario o sistema los borre.", "La protección depende de la cuenta Windows y el dispositivo. Mantenga Windows y dependencias al día, limite accesos y revise registros antes de compartirlos. Ningún método es infalible."]],
        ["6. Opciones y derechos", ["Puede desactivar avisos, borrar archivos, desinstalar y evitar enlaces. Como el editor no recibe datos de la aplicación, acceso, rectificación, supresión, portabilidad, limitación, oposición y retirada suelen ejercerse directamente en el equipo.", "Se mantienen los derechos aplicables del RGPD/UK GDPR, ley rusa 152-FZ, privacidad de California, APPI japonesa, PIPA coreana y PIPL china, incluida reclamación ante autoridad. La aplicación sin modificar no realiza transferencias internacionales controladas por el editor. Para registros del servidor contacte al operador. Es de propósito general y no recopila conscientemente datos de menores."]]
      ]},
      { id: "terms", title: "Términos de uso", clauses: [
        ["1. Aceptación", ["Al instalar o usar Bluetooth Notify acepta estos Términos. Si no acepta, no lo instale. Debe tener capacidad legal o autorización de progenitor, tutor u organización. Quien instala para una organización confirma poder para vincularla."]],
        ["2. Licencia", ["Bluetooth Notify es software libre y de código abierto bajo GNU Affero General Public License, únicamente versión 3 (AGPL-3.0-only). Se permite el uso comercial. Quien distribuya la obra original o una derivada debe mantener la AGPL y facilitar el código fuente correspondiente conforme a la licencia. Si una versión modificada admite interacción remota por red, también debe ofrecer a sus usuarios acceso al código fuente correspondiente. El texto completo acompaña a la aplicación y al repositorio."]],
        ["3. Uso responsable", ["No use ilegalmente, eluda seguridad, introduzca malware, suplante al editor ni distribuya una versión modificada como oficial. Usted responde del acceso, copias, configuración y compatibilidad de hardware/controladores."]],
        ["4. Terceros y cambios", ["Windows, .NET, Windows App Runtime, hardware, controladores, avisos, alojamiento y enlaces pertenecen a terceros y a sus términos. La aplicación puede cambiar, actualizarse, suspenderse o cesar. Cambios legales importantes rigen hacia futuro desde su publicación; seguir usando implica aceptación donde sea válido."]],
        ["5. Terminación", ["Puede terminar desinstalando y borrando datos. La licencia puede terminar por incumplimiento sustancial. Propiedad, exenciones, responsabilidad, disputas y derechos imperativos sobreviven según su naturaleza."]],
        ["6. Consumidores y disputas", ["No se renuncian garantías, remedios, desistimiento, jurisdicción u otros derechos de consumo irrenunciables. Prevalece la ley imperativa de la residencia habitual. En lo demás, rige la ley del editor indicado por el distribuidor, sin normas de conflicto. Contacte primero al distribuidor para resolver amistosamente."]]
      ]},
      { id: "disclaimer", title: "Descargo de responsabilidad", clauses: [
        ["1. Datos de estado", ["Bluetooth Notify muestra de la mejor manera posible información de Windows, dispositivos y controladores. Puede faltar, retrasarse, estar almacenada, obsoleta, duplicada o equivocada; un aviso puede faltar, llegar tarde o repetirse. Compruebe lo crítico con herramientas del fabricante."]],
        ["2. No apto para usos críticos", ["Es una utilidad, no un sistema de seguridad, emergencia, médico, accesibilidad, protección, industrial o soporte vital. No dependa de ella si un fallo puede causar lesiones, pérdida de bienes/datos o interrupción esencial."]],
        ["3. Garantías", ["Hasta donde permita la ley, aplicación y web se ofrecen «tal cual» y «según disponibilidad», sin garantías expresas o implícitas de precisión, continuidad, compatibilidad, comerciabilidad, idoneidad o no infracción."]],
        ["4. Responsabilidad", ["Hasta donde permita la ley, editor y colaboradores no responden por pérdidas indirectas, incidentales, especiales, consecuentes o punitivas ni por pérdida de datos, beneficios, oportunidades o disponibilidad. No se excluye responsabilidad irrenunciable, como fraude, dolo, negligencia grave aplicable, muerte o lesión por negligencia y remedios imperativos del consumidor."]]
      ]}
    ]
  },

  pt: {
    language: "Idioma", eyebrow: "JURÍDICO / PROCESSAMENTO LOCAL", title: "Informação jurídica",
    summary: "Política de Privacidade, Termos de Utilização e Isenção de Responsabilidade da aplicação Bluetooth Notify não modificada e do respetivo site estático.",
    updated: "Em vigor e atualizado em: 11 de setembro de 2026 · Versão 1.2", back: "Voltar ao site",
    nav: { privacy: "Privacidade", terms: "Termos de utilização", disclaimer: "Isenção" },
    operator: "Editor e contacto. O Bluetooth Notify é publicado pelo programador que gere a conta GitHub «vyach-vasiliev». O site oficial bluetooth-notify.onrender.com está alojado como site estático na Render. O código-fonte está em github.com/vyach-vasiliev/bluetooth-notify. Envie pedidos de privacidade, jurídicos e suporte através de github.com/vyach-vasiliev/.",
    documents: [
      { id: "privacy", title: "Política de Privacidade", clauses: [
        ["1. Âmbito e funções", ["A Política abrange Bluetooth Notify, instalador e site estático do projeto. Não abrange Microsoft, Windows, alojamento ou sites externos. O editor não recebe dados pessoais da aplicação não modificada. O tratamento no dispositivo fica sob controlo do utilizador; o operador de um site publicado responde pelos dados de pedidos tratados pelo alojamento."]],
        ["2. Dados da aplicação", ["Para mostrar o estado Bluetooth, a aplicação pede ao Windows nomes de dispositivos emparelhados, identificadores endpoint/contentor, endereço Bluetooth disponível, classe/categoria, estado de emparelhamento/ligação e bateria. Estes dados são usados em memória e não persistidos pela aplicação.", "Em %LocalAppData%\\BluetoothNotify\\settings.json ficam limiares/estado de notificações, idioma, tema e versão do esquema. Registos locais rotativos guardam data/hora, eventos, tipos e mensagens de erro (até cinco ficheiros de cerca de 512 KiB). Não registam intencionalmente nomes/endereço, mas erros do Windows podem conter detalhes técnicos locais."]],
        ["3. Site e instalador", ["O site estático bluetooth-notify.onrender.com não define cookies nem utiliza análise, publicidade, contas, formulários, píxeis, impressões digitais ou telemetria. É alojado pela Render. A Render pode tratar dados do dispositivo/IP e infraestrutura segundo a sua política de privacidade; a Render indica que sites estáticos não produzem registos de pedidos da aplicação. Links Microsoft seguem os respetivos avisos.", "O instalador verifica localmente requisitos Microsoft e pode criar registo local com caminhos, dados do sistema/aplicação e erros. Só abre downloads Microsoft após concordância. Não envia dados ao editor."]],
        ["4. Finalidades, bases e partilha", ["Dados locais destinam-se apenas a estado, alertas, preferências, diagnóstico, instalação, atualização e remoção. Quando exigido, o tratamento pedido baseia-se na execução do acordo; registos limitados de segurança web podem basear-se no interesse legítimo de entrega segura. É pedido consentimento se a lei o exigir.", "A aplicação não modificada não vende, aluga, carrega ou divulga dados ao editor/terceiros, nem faz publicidade comportamental entre contextos, perfis ou decisões automatizadas juridicamente relevantes. Nos últimos doze meses o editor não recolheu, vendeu ou partilhou informação pessoal derivada da aplicação."]],
        ["5. Retenção e segurança", ["Dados Bluetooth em memória desaparecem ao sair. Definições e registos permanecem até apagar %LocalAppData%\\BluetoothNotify ou escolher remoção ao desinstalar; a rotação limita tamanho, não tempo. Registos de instalação ficam locais até eliminação pelo utilizador/sistema.", "A proteção depende da conta Windows e do dispositivo. Atualize o sistema e dependências, limite acesso e reveja registos antes de os partilhar. Nenhum método é garantido."]],
        ["6. Opções e direitos", ["Pode desativar notificações, apagar ficheiros, desinstalar e evitar links. Como o editor não recebe dados da aplicação, acesso, retificação, apagamento, portabilidade, limitação, oposição e retirada são normalmente exercidos no dispositivo.", "Mantêm-se direitos aplicáveis do RGPD/UK GDPR, Lei russa 152-FZ, privacidade da Califórnia, APPI japonesa, PIPA coreana e PIPL chinesa, incluindo reclamação à autoridade. A aplicação não modificada não faz transferências internacionais controladas pelo editor. Para logs do servidor contacte o operador. É geral e não recolhe conscientemente dados de crianças."]]
      ]},
      { id: "terms", title: "Termos de Utilização", clauses: [
        ["1. Aceitação", ["Ao instalar ou usar aceita estes Termos. Se não aceitar, não instale. Deve ter capacidade legal ou autorização parental, tutelar ou organizacional. Quem instala por uma organização confirma poderes para a vincular."]],
        ["2. Licença", ["O Bluetooth Notify é software livre e de código aberto sob a GNU Affero General Public License, apenas versão 3 (AGPL-3.0-only). O uso comercial é permitido. Quem distribuir a obra original ou uma obra derivada deve manter a AGPL e disponibilizar o código-fonte correspondente nos termos da licença. Se uma versão modificada permitir interação remota por rede, os utilizadores também devem poder obter o código-fonte correspondente. O texto completo acompanha a aplicação e o repositório."]],
        ["3. Uso responsável", ["Não use ilegalmente, contorne segurança, introduza malware, se apresente como editor ou distribua versão alterada como oficial. É responsável pelo acesso, cópias, configuração e compatibilidade de hardware/controladores."]],
        ["4. Terceiros e alterações", ["Windows, .NET, Windows App Runtime, hardware, controladores, notificações, alojamento e links são de terceiros e seguem os seus termos. A aplicação pode mudar, ser suspensa ou terminar. Alterações jurídicas materiais valem futuramente desde publicação; continuar equivale a aceitar quando permitido."]],
        ["5. Cessação", ["Pode cessar desinstalando e apagando dados. A licença pode terminar após incumprimento material. Titularidade, exclusões, responsabilidade, litígios e direitos imperativos subsistem conforme a natureza."]],
        ["6. Consumidores e litígios", ["Não se afastam garantias, meios, livre resolução, competência ou proteção do consumidor imperativas. Prevalece a lei obrigatória da residência habitual. No resto vale a lei do editor identificado pelo distribuidor, sem conflitos de leis. Contacte primeiro o distribuidor para solução amigável."]]
      ]},
      { id: "disclaimer", title: "Isenção de responsabilidade", clauses: [
        ["1. Dados de estado", ["Bluetooth Notify apresenta da melhor forma dados de Windows, dispositivos e controladores. Podem faltar, atrasar, estar em cache, desatualizados, duplicados ou errados; alertas podem faltar, atrasar ou repetir. Confirme o essencial com ferramentas do fabricante."]],
        ["2. Não destinado a uso crítico", ["É utilitário de conveniência, não sistema de segurança, emergência, saúde, acessibilidade, proteção, indústria ou suporte de vida. Não dependa dele se falha puder causar lesão, perda material/de dados ou interrupção essencial."]],
        ["3. Garantias", ["No máximo permitido, aplicação e site são fornecidos «tal como estão» e «conforme disponíveis», sem garantias expressas ou implícitas de exatidão, continuidade, compatibilidade, comercialização, adequação ou não infração."]],
        ["4. Responsabilidade", ["No máximo permitido, editor e contribuidores não respondem por perdas indiretas, incidentais, especiais, consequenciais ou punitivas, nem por dados, lucro, oportunidade ou disponibilidade. Nada exclui responsabilidade legalmente inderrogável, incluindo fraude, dolo, negligência grosseira aplicável, morte/lesão por negligência e direitos imperativos do consumidor."]]
      ]}
    ]
  },

  ja: {
    language: "言語", eyebrow: "法的情報 / ローカル処理優先", title: "法的情報",
    summary: "変更されていない Bluetooth Notify デスクトップアプリと静的ウェブサイトに関するプライバシーポリシー、利用規約、免責事項です。",
    updated: "施行・最終更新：2026年9月11日 · バージョン 1.2", back: "ウェブサイトに戻る",
    nav: { privacy: "プライバシー", terms: "利用規約", disclaimer: "免責事項" },
    operator: "発行者・連絡先。Bluetooth Notify は GitHub アカウント「vyach-vasiliev」を運営する開発者が公開しています。公式サイト bluetooth-notify.onrender.com は Render の静的サイトとしてホストされています。ソースコードは github.com/vyach-vasiliev/bluetooth-notify で公開されています。プライバシー、法務、サポートの問い合わせは github.com/vyach-vasiliev/ を通じて行ってください。",
    documents: [
      { id: "privacy", title: "プライバシーポリシー", clauses: [
        ["1. 適用範囲と役割", ["本ポリシーは Bluetooth Notify、インストーラー、同梱の静的サイトに適用されます。Microsoft、Windows、ホスト、外部サイトには適用されません。変更されていないアプリから発行者へ個人データは送られません。端末内処理は利用者の管理下にあり、公開サイトの運営者はホストが処理するリクエストデータに責任を負います。"]],
        ["2. アプリが扱うデータ", ["Bluetooth 状態表示のため、Windows からペアリング済み機器名、endpoint/container 識別子、利用可能な Bluetooth アドレス、分類、ペアリング・接続状態、電池残量を取得します。これらはメモリ内だけで使用し、アプリは保存しません。", "%LocalAppData%\\BluetoothNotify\\settings.json には通知のしきい値・状態、言語、テーマ、スキーマ版を保存します。ローカルの循環ログには時刻、イベント、エラー種別・文面を最大約512 KiB×5件保存します。機器名やアドレスを意図的に記録しませんが、Windows のエラー文にローカル技術情報が含まれる場合があります。"]],
        ["3. サイトとインストーラー", ["静的サイト bluetooth-notify.onrender.com は Cookie、解析、広告、アカウント、フォーム、ピクセル、フィンガープリント、テレメトリーを使用せず、Render がホストしています。Render は同社のプライバシーポリシーに基づき端末/IP・インフラ情報を処理する場合があります。Render によれば、静的サイトはアプリケーション要求ログを生成しません。Microsoft リンクには同社の通知が適用されます。", "インストーラーは Microsoft 前提条件を端末内で確認し、パス、OS/アプリ情報、エラーを含むローカルセットアップログを作る場合があります。同意時のみ Microsoft ダウンロードを開き、発行者へインストール情報を送りません。"]],
        ["4. 目的、法的根拠、共有", ["ローカルデータは状態表示、電池通知、設定、診断、インストール、更新、削除だけに使います。根拠が必要な場合、要求されたソフト提供には契約履行、限定的なサイト安全ログには安全な配信という正当な利益を用い、法令上必要なら同意を得ます。", "変更されていないアプリはデータを販売、貸与、アップロード、発行者・第三者へ開示せず、コンテキスト横断広告、プロファイリング、法的効果のある自動決定をしません。過去12か月に発行者はアプリ由来の個人情報を収集、販売、共有していません。"]],
        ["5. 保存と安全", ["メモリ内データは終了時に消えます。設定・ログは %LocalAppData%\\BluetoothNotify の削除またはアンインストール時の削除選択まで残り、循環処理は容量のみ制限します。セットアップログは利用者かOSが削除するまで端末内に残ります。", "保護は Windows アカウントと端末の安全性に依存します。更新、アクセス制限、共有前のログ確認を行ってください。完全な安全は保証されません。"]],
        ["6. 選択と地域の権利", ["通知停止、ローカルファイル削除、アンインストール、外部リンク不使用が可能です。発行者がアプリデータを受け取らないため、開示、訂正、削除、移転、制限、異議、同意撤回は通常端末上で行います。", "GDPR/UK GDPR、ロシア152-FZ、カリフォルニア法、日本の個人情報保護法（APPI）、韓国PIPA、中国PIPLの適用される権利と監督機関への申立ては維持されます。発行者管理の越境移転はありません。サーバーログはサイト運営者へ連絡してください。一般用途であり、子どものデータを意図的に収集しません。"]]
      ]},
      { id: "terms", title: "利用規約", clauses: [
        ["1. 同意", ["インストールまたは使用により本規約に同意します。同意しない場合は使用しないでください。法的同意能力または保護者・組織の許可が必要です。組織向けに導入する者は拘束権限を確認します。"]],
        ["2. ライセンス", ["Bluetooth Notify は GNU Affero General Public License バージョン3のみ（AGPL-3.0-only）で提供される自由なオープンソースソフトウェアです。商用利用も許可されます。原版または変更した派生物を配布する場合は AGPL を維持し、ライセンスに従って対応するソースコードを提供する必要があります。変更版がネットワーク経由の遠隔操作に対応する場合、その利用者にも対応するソースコードへのアクセスを提供する必要があります。完全なライセンス本文はアプリとリポジトリに含まれます。"]],
        ["3. 適切な利用", ["違法利用、セキュリティ回避、マルウェア導入、発行者の偽装、変更版を公式として配布することは禁止です。端末アクセス、バックアップ、設定、機器・ドライバー互換性は利用者の責任です。"]],
        ["4. 第三者と変更", ["Windows、.NET、Windows App Runtime、機器、ドライバー、通知、ホスト、リンク先は各第三者の条件に従います。アプリは変更、停止、終了されることがあります。重要な法的変更は掲載日以降に適用され、法令上可能な範囲で継続利用を同意とします。"]],
        ["5. 終了", ["アンインストールとローカルデータ削除で終了できます。重大な違反時はライセンスが終了します。所有権、免責、責任、紛争、強行的権利は性質上必要な範囲で存続します。"]],
        ["6. 消費者の権利", ["法令上排除できない保証、救済、撤回、管轄、消費者保護は放棄されません。常居所の強行法が優先し、それ以外は配布者が示す発行者の法（抵触法を除く）によります。まず配布者へ円満解決を求めてください。"]]
      ]},
      { id: "disclaimer", title: "免責事項", clauses: [
        ["1. 状態情報", ["Bluetooth Notify は Windows、機器、ドライバーが提供する情報を可能な範囲で表示します。欠落、遅延、キャッシュ、陳腐化、重複、誤りがあり、通知が遅れる、出ない、重なる場合があります。重要情報はメーカー手段で確認してください。"]],
        ["2. 重要用途ではないこと", ["便利用ツールであり、安全、緊急、医療、アクセシビリティ、防犯、産業、生命維持システムではありません。障害が人身、財産・データ損失、重要サービス停止を招く用途では依存しないでください。"]],
        ["3. 保証", ["法令上許される最大限で、アプリとサイトは「現状有姿」「提供可能な範囲」で、正確性、無停止、互換性、商品性、特定目的適合性、非侵害の明示・黙示保証なく提供されます。"]],
        ["4. 責任", ["法令上許される最大限で、発行者・貢献者は間接、偶発、特別、結果、懲罰的損害、データ、利益、機会、可用性損失に責任を負いません。詐欺、故意、適用される重過失、過失による死亡・傷害、強行的消費者救済など法的に排除不能な責任は除外しません。"]]
      ]}
    ]
  },

  ko: {
    language: "언어", eyebrow: "법적 정보 / 로컬 우선", title: "법적 정보",
    summary: "수정되지 않은 Bluetooth Notify 데스크톱 앱과 정적 웹사이트에 적용되는 개인정보 처리방침, 이용약관 및 면책조항입니다.",
    updated: "시행 및 최종 업데이트: 2026년 9월 11일 · 버전 1.2", back: "웹사이트로 돌아가기",
    nav: { privacy: "개인정보", terms: "이용약관", disclaimer: "면책조항" },
    operator: "게시자 및 연락처. Bluetooth Notify는 GitHub 계정 ‘vyach-vasiliev’를 운영하는 개발자가 게시합니다. 공식 사이트 bluetooth-notify.onrender.com은 Render의 정적 사이트로 호스팅됩니다. 소스 코드는 github.com/vyach-vasiliev/bluetooth-notify에 공개됩니다. 개인정보, 법률 및 지원 문의는 github.com/vyach-vasiliev/를 이용하십시오.",
    documents: [
      { id: "privacy", title: "개인정보 처리방침", clauses: [
        ["1. 범위와 역할", ["본 방침은 Bluetooth Notify, 설치 프로그램 및 프로젝트의 정적 사이트에 적용됩니다. Microsoft, Windows, 호스팅 제공자 및 외부 사이트에는 적용되지 않습니다. 수정되지 않은 앱의 개인정보는 게시자에게 전달되지 않습니다. 기기 내 처리는 사용자가 통제하며, 배포된 사이트 운영자는 호스트가 처리하는 요청 데이터에 책임집니다."]],
        ["2. 앱 처리 데이터", ["Bluetooth 상태 표시를 위해 Windows에서 페어링 기기 이름, endpoint/container 식별자, 가능한 Bluetooth 주소, 클래스/범주, 페어링·연결 상태 및 배터리 잔량을 요청합니다. 이 정보는 메모리에서만 사용되고 앱이 저장하지 않습니다.", "%LocalAppData%\\BluetoothNotify\\settings.json에는 알림 임계값/상태, 언어, 테마, 스키마 버전이 저장됩니다. 순환 로컬 로그는 시간, 앱 이벤트, 오류 유형/메시지를 약 512 KiB 파일 최대 5개까지 저장합니다. 기기 이름/주소를 의도적으로 기록하지 않지만 Windows 오류문에 로컬 기술 정보가 포함될 수 있습니다."]],
        ["3. 웹사이트와 설치", ["정적 사이트 bluetooth-notify.onrender.com은 쿠키, 분석, 광고, 계정, 양식, 픽셀, 핑거프린팅 또는 텔레메트리를 사용하지 않으며 Render에서 호스팅됩니다. Render는 개인정보 처리방침에 따라 기기/IP 및 인프라 데이터를 처리할 수 있습니다. Render에 따르면 정적 사이트는 애플리케이션 요청 로그를 생성하지 않습니다. Microsoft 링크에는 해당 고지가 적용됩니다.", "설치 프로그램은 Microsoft 필수 구성요소를 로컬 확인하고 경로, OS/앱 정보, 오류가 포함된 로컬 설치 로그를 만들 수 있습니다. 사용자 동의 때만 Microsoft 다운로드를 열며 설치 데이터를 게시자에게 보내지 않습니다."]],
        ["4. 목적, 근거, 공유", ["로컬 데이터는 상태, 배터리 알림, 설정, 진단, 설치, 업데이트, 제거에만 사용됩니다. 법적 근거가 필요한 경우 요청된 소프트웨어 제공은 계약 이행, 제한적인 사이트 보안 로그는 안전한 제공의 정당한 이익에 의하며 법이 요구하면 동의를 받습니다.", "수정되지 않은 앱은 데이터를 판매, 대여, 업로드하거나 게시자/제3자에게 공개하지 않고, 교차 맥락 행동광고, 프로파일링 또는 법적 효과가 있는 자동결정을 하지 않습니다. 지난 12개월 동안 게시자는 앱 유래 개인정보를 수집·판매·공유하지 않았습니다."]],
        ["5. 보존과 보안", ["메모리의 Bluetooth 데이터는 종료 시 사라집니다. 설정과 로그는 %LocalAppData%\\BluetoothNotify를 삭제하거나 제거 시 삭제를 선택할 때까지 남고, 로그 순환은 크기만 제한합니다. 설치 로그는 사용자나 OS가 지울 때까지 로컬에 남습니다.", "보호는 Windows 계정과 기기 보안에 의존합니다. Windows와 구성요소를 업데이트하고 접근을 제한하며 로그 공유 전 확인하십시오. 완전한 보안은 보장되지 않습니다."]],
        ["6. 선택과 지역 권리", ["알림 끄기, 로컬 파일 삭제, 앱 제거, 외부 링크 회피가 가능합니다. 게시자가 앱 데이터를 받지 않으므로 열람, 정정, 삭제, 이동, 제한, 반대, 동의 철회는 통상 기기에서 직접 처리합니다.", "GDPR/UK GDPR, 러시아 152-FZ, 캘리포니아 개인정보법, 일본 APPI, 대한민국 개인정보 보호법(PIPA), 중국 PIPL의 적용 권리와 감독기관 민원권은 유지됩니다. 게시자가 통제하는 국외이전은 없습니다. 서버 로그는 사이트 운영자에게 문의하십시오. 앱은 일반용이며 아동 데이터를 고의로 수집하지 않습니다."]]
      ]},
      { id: "terms", title: "이용약관", clauses: [
        ["1. 동의", ["설치하거나 사용하면 본 약관에 동의합니다. 동의하지 않으면 사용하지 마십시오. 법적 동의 능력 또는 부모·보호자·조직의 승인이 필요합니다. 조직용 설치자는 조직을 구속할 권한을 확인합니다."]],
        ["2. 라이선스", ["Bluetooth Notify는 GNU Affero General Public License 버전 3 전용(AGPL-3.0-only)으로 제공되는 자유 오픈 소스 소프트웨어입니다. 상업적 이용도 허용됩니다. 원본 또는 수정된 파생 저작물을 배포하는 경우 AGPL을 유지하고 라이선스가 요구하는 해당 소스 코드를 제공해야 합니다. 수정 버전이 네트워크를 통한 원격 상호작용을 지원하면 사용자에게도 해당 소스 코드 접근을 제공해야 합니다. 전체 라이선스는 앱과 저장소에 포함됩니다."]],
        ["3. 책임 있는 사용", ["불법 사용, 보안 우회, 악성코드 삽입, 게시자 사칭, 수정 빌드를 공식판으로 배포하지 마십시오. 기기 접근, 백업, 설정, 하드웨어·드라이버 호환성은 사용자 책임입니다."]],
        ["4. 제3자와 변경", ["Windows, .NET, Windows App Runtime, 하드웨어, 드라이버, 알림, 호스팅, 링크는 제3자 조건에 따릅니다. 앱은 변경, 중단, 종료될 수 있습니다. 중요한 법적 변경은 게시일부터 장래에 적용되며 법이 허용하면 계속 사용은 동의입니다."]],
        ["5. 종료", ["앱과 로컬 데이터를 삭제해 종료할 수 있습니다. 중대한 위반 후 라이선스가 종료될 수 있습니다. 소유권, 면책, 책임, 분쟁 및 강행 권리는 성질상 필요한 범위에서 존속합니다."]],
        ["6. 소비자 권리", ["법으로 배제할 수 없는 보증, 구제, 철회, 관할, 소비자 보호를 포기시키지 않습니다. 상거소지 강행법이 우선하며 그 외에는 배포자가 밝힌 게시자의 법(저촉법 제외)에 따릅니다. 먼저 배포자에게 원만한 해결을 요청하십시오."]]
      ]},
      { id: "disclaimer", title: "면책조항", clauses: [
        ["1. 상태 데이터", ["Bluetooth Notify는 Windows, 기기, 드라이버가 제공하는 정보를 가능한 범위에서 표시합니다. 누락, 지연, 캐시, 오래됨, 중복, 오류가 있을 수 있고 알림이 늦거나 없거나 반복될 수 있습니다. 중요한 상태는 제조사 도구로 확인하십시오."]],
        ["2. 중요 용도 금지", ["편의 도구이며 안전, 비상, 의료, 접근성, 보안, 산업 또는 생명유지 시스템이 아닙니다. 실패가 부상, 재산·데이터 손실, 필수 서비스 중단을 일으킬 수 있는 곳에서는 의존하지 마십시오."]],
        ["3. 보증", ["법이 허용하는 최대 범위에서 앱과 사이트는 정확성, 무중단, 호환성, 상품성, 특정 목적 적합성, 비침해의 명시·묵시 보증 없이 ‘있는 그대로’, ‘이용 가능한 상태로’ 제공됩니다."]],
        ["4. 책임", ["법이 허용하는 최대 범위에서 게시자와 기여자는 간접, 부수, 특별, 결과적, 징벌적 손해나 데이터, 이익, 기회, 가용성 손실에 책임지지 않습니다. 사기, 고의, 적용되는 중과실, 과실로 인한 사망·상해, 강행적 소비자 구제 등 법적으로 배제할 수 없는 책임은 제외하지 않습니다."]]
      ]}
    ]
  },

  zh: {
    language: "语言", eyebrow: "法律信息 / 本地优先", title: "法律信息",
    summary: "适用于未经修改的 Bluetooth Notify 桌面应用及其静态网站的《隐私政策》《使用条款》和《免责声明》。",
    updated: "生效及最后更新：2026年9月11日 · 版本 1.2", back: "返回网站",
    nav: { privacy: "隐私政策", terms: "使用条款", disclaimer: "免责声明" },
    operator: "发布者与联系方式。Bluetooth Notify 由运营 GitHub 账户“vyach-vasiliev”的开发者发布。官方网站 bluetooth-notify.onrender.com 作为 Render 静态网站托管。源代码发布于 github.com/vyach-vasiliev/bluetooth-notify。隐私、法律和支持请求请通过 github.com/vyach-vasiliev/ 提交。",
    documents: [
      { id: "privacy", title: "隐私政策", clauses: [
        ["1. 范围与角色", ["本政策适用于 Bluetooth Notify、其安装程序及项目所附静态网站，不适用于 Microsoft、Windows、托管商或外部网站。未经修改的应用不会向发布者传送个人信息。设备内处理由用户控制；已部署网站的运营者负责托管商处理的请求数据。"]],
        ["2. 应用处理的数据", ["为显示 Bluetooth 状态，应用向 Windows 查询已配对设备名称、endpoint/container 标识符、可用的 Bluetooth 地址、设备类别、配对与连接状态和电量。这些设备资料仅在内存中使用，应用不会将其持久保存。", "%LocalAppData%\\BluetoothNotify\\settings.json 保存通知阈值/状态、语言、主题和架构版本。本地轮换日志保存时间、应用事件、错误类型和信息（最多约五个512 KiB文件）。应用不会故意记录设备名称或地址，但 Windows 错误文本偶尔可能包含本机技术信息。"]],
        ["3. 网站与安装程序", ["静态网站 bluetooth-notify.onrender.com 不设置 Cookie，不使用分析、广告、账户、表单、像素、指纹识别或遥测，并由 Render 托管。Render 可能根据其隐私政策处理设备/IP 与基础设施数据；Render 表示静态网站不会生成应用请求日志。Microsoft 外链适用其自身声明。", "安装程序在本机检查 Microsoft 前置组件，并可能生成含路径、系统/应用信息和错误的本地安装日志。只有用户同意后才会打开 Microsoft 下载页；不会向发布者发送安装资料。"]],
        ["4. 目的、依据与共享", ["本地数据仅用于设备状态、电量通知、设置、诊断、安装、更新和卸载。在需要法律依据时，应用户要求提供软件以履行协议为依据；有限的网站安全日志可基于安全提供服务的合法利益；法律要求时获取同意。", "未经修改的应用不会出售、出租、上传或向发布者/第三方披露应用数据，不进行跨情境行为广告、画像或产生法律影响的自动化决定。过去十二个月，发布者未收集、出售或共享源自应用的个人信息。"]],
        ["5. 保存与安全", ["内存中的 Bluetooth 数据在退出时消失。设置和日志保留在设备中，直至用户删除 %LocalAppData%\\BluetoothNotify 或卸载时选择删除；日志轮换只限制容量，不设固定期限。安装日志保留在本地直至用户或系统删除。", "保护依赖 Windows 账户和设备安全。请更新 Windows 与依赖项、限制账户访问，并在分享前检查日志。任何安全方式均无法绝对保证。"]],
        ["6. 选择与地区权利", ["用户可关闭通知、删除本地文件、卸载应用并避免外链。因发布者不接收应用数据，查阅、更正、删除、携带、限制、反对及撤回同意通常由用户直接在设备上完成。", "适用的 GDPR/UK GDPR、俄罗斯152-FZ、加州隐私法、日本APPI、韩国PIPA及中国《个人信息保护法》(PIPL)权利和向监管机关投诉权不受影响。未经修改的应用不进行由发布者控制的跨境传输。服务器日志请联系网站运营者。应用面向一般用途，不会明知收集儿童个人信息。"]]
      ]},
      { id: "terms", title: "使用条款", clauses: [
        ["1. 同意", ["安装或使用即表示同意本条款；不同意请勿安装或使用。您须具备法律同意能力，或取得父母、监护人或组织授权。代表组织安装者确认有权约束该组织。"]],
        ["2. 许可", ["Bluetooth Notify 是依据 GNU Affero General Public License 第3版且仅限该版本（AGPL-3.0-only）发布的自由开源软件，允许商业使用。分发原始作品或修改后的衍生作品时，必须继续采用 AGPL，并依照许可证提供对应源代码。若修改版本支持通过网络进行远程交互，还必须向其用户提供获取对应源代码的机会。完整许可证随应用和代码仓库提供。"]],
        ["3. 负责使用", ["不得违法使用、绕过平台安全、植入恶意程序、冒充发布者或将修改版作为官方版本分发。设备访问、备份、配置及 Bluetooth 硬件/驱动兼容性由用户负责。"]],
        ["4. 第三方与变更", ["Windows、.NET、Windows App Runtime、硬件、驱动、通知、托管和外链由第三方按其条款提供。应用可能变更、暂停或停止。重大法律变更自公布日起向将来适用；法律允许时，通知后继续使用视为接受。"]],
        ["5. 终止", ["用户可通过卸载并删除本地数据终止。重大违约后许可可终止。所有权、免责、责任、争议及强制性权利条款按其性质继续有效。"]],
        ["6. 消费者权利与争议", ["本条款不排除适用消费者法律不允许排除的保证、救济、撤回、管辖及其他保护。用户惯常居住地的强制性法律优先；其余适用分发者所标明发布者的法律（不含冲突规范）。请先联系分发者寻求友好解决。"]]
      ]},
      { id: "disclaimer", title: "免责声明", clauses: [
        ["1. 状态数据", ["Bluetooth Notify 尽力显示 Windows、设备与驱动提供的信息。数据可能缺失、延迟、缓存、过时、重复或错误；通知可能延迟、缺失或重复。关键电量或连接状态请使用制造商工具核验。"]],
        ["2. 不用于关键场景", ["本应用是便利工具，不是安全、应急、医疗、无障碍、安防、工业或生命支持系统。若故障可能导致人身伤害、财产/数据损失或关键服务中断，请勿依赖本应用。"]],
        ["3. 保证", ["在法律允许的最大范围内，应用与网站按“现状”和“可用状态”提供，不作有关准确、连续、兼容、适销、特定用途适用或不侵权的明示或默示保证。"]],
        ["4. 责任", ["在法律允许的最大范围内，发布者和贡献者不对间接、附带、特殊、后果性或惩罚性损失，以及数据、利润、机会或设备可用性损失负责。本条不排除法律禁止排除的责任，包括欺诈、故意行为、适用的重大过失、因过失导致的死亡或人身伤害及强制性消费者救济。"]]
      ]}
    ]
  }
};

const content = document.querySelector("#legal-content");
const languageSwitcher = document.querySelector("#language-switcher");
const languageTrigger = document.querySelector("#language-trigger");
const languageMenu = document.querySelector("#language-menu");
const languageOptions = [...document.querySelectorAll(".language-option")];

if (window.location.protocol === "file:") {
  document.body.classList.add("local-file");
  const brand = document.querySelector(".brand-link");
  brand.removeAttribute("href");
  brand.removeAttribute("aria-label");
  for (const element of document.querySelectorAll(".main-site-only")) element.hidden = true;
  document.querySelector("#back-link").hidden = true;
}

function element(tag, className, text) {
  const node = document.createElement(tag);
  if (className) node.className = className;
  if (text !== undefined) node.textContent = text;
  return node;
}

function renderDocument(documentData) {
  const section = element("section", "document");
  section.id = documentData.id;
  section.append(element("h2", "", documentData.title));
  const grid = element("div", "clause-grid");
  for (const [heading, paragraphs] of documentData.clauses) {
    grid.append(element("h3", "clause", heading));
    const body = element("div", "clause-body");
    for (const paragraph of paragraphs) body.append(element("p", "", paragraph));
    grid.append(body);
  }
  section.append(grid);
  return section;
}

function detectLanguage() {
  const requested = new URLSearchParams(window.location.search).get("lang")?.toLowerCase();
  if (requested && policies[requested]) return requested;
  const browser = navigator.language.toLowerCase().split("-")[0];
  return policies[browser] ? browser : "en";
}

function closeLanguageMenu(restoreFocus = false) {
  languageMenu.hidden = true;
  languageTrigger.classList.remove("language-trigger-open");
  languageTrigger.setAttribute("aria-expanded", "false");
  if (restoreFocus) languageTrigger.focus();
}

function openLanguageMenu() {
  languageMenu.hidden = false;
  languageTrigger.classList.add("language-trigger-open");
  languageTrigger.setAttribute("aria-expanded", "true");
  const selected = languageOptions.find((option) => option.getAttribute("aria-selected") === "true");
  (selected ?? languageOptions[0])?.focus();
}

function moveLanguageFocus(offset) {
  const focusedIndex = languageOptions.indexOf(document.activeElement);
  const selectedIndex = languageOptions.findIndex((option) => option.getAttribute("aria-selected") === "true");
  const startIndex = focusedIndex >= 0 ? focusedIndex : Math.max(0, selectedIndex);
  languageOptions[(startIndex + offset + languageOptions.length) % languageOptions.length]?.focus();
}

function render(language, replaceHistory = false) {
  const policy = policies[language] ?? policies.en;
  const header = siteHeaderCopy[language] ?? siteHeaderCopy.en;
  const publisherLinks = publisherLinkCopy[language] ?? publisherLinkCopy.en;
  document.documentElement.lang = header.htmlLang;
  document.title = `${policy.title} — Bluetooth Notify`;
  languageTrigger.setAttribute("aria-label", `${header.languageLabel}: ${header.label}`);
  languageMenu.setAttribute("aria-label", header.languageLabel);
  document.querySelector("#current-language-full").textContent = header.label;
  document.querySelector("#current-language-short").textContent = header.short;
  document.querySelector("#main-navigation").setAttribute("aria-label", header.navigationLabel);
  for (const link of document.querySelectorAll("[data-site-nav]")) {
    link.textContent = header.nav[link.dataset.siteNav];
    link.href = `/?lang=${language}#${link.dataset.siteNav}`;
  }
  document.querySelector("#header-cta-full").textContent = header.build;
  document.querySelector("#header-cta-short").textContent = header.buildShort;
  document.querySelector("#header-cta").href = `/?lang=${language}#build`;
  if (window.location.protocol !== "file:") {
    document.querySelector(".brand-link").href = `/?lang=${language}#top`;
    document.querySelector("#back-link").href = `/?lang=${language}#top`;
  }
  for (const option of languageOptions) {
    const selected = option.dataset.language === language;
    option.setAttribute("aria-selected", String(selected));
    option.classList.toggle("language-option-selected", selected);
    option.querySelector(".language-option-check").classList.toggle("language-option-check-hidden", !selected);
  }
  document.querySelector("#eyebrow").textContent = policy.eyebrow;
  document.querySelector("#page-title").textContent = policy.title;
  document.querySelector("#page-summary").textContent = policy.summary;
  document.querySelector("#updated").textContent = policy.updated;
  document.querySelector("#back-link").textContent = policy.back;
  for (const link of document.querySelectorAll("[data-nav]")) link.textContent = policy.nav[link.dataset.nav];
  document.querySelector("#operator-notice").textContent = policy.operator;
  document.querySelector("#publisher-links").setAttribute("aria-label", publisherLinks.label);
  for (const label of document.querySelectorAll("[data-publisher-label]")) label.textContent = publisherLinks[label.dataset.publisherLabel];
  content.replaceChildren(...policy.documents.map(renderDocument));

  const url = new URL(window.location.href);
  url.searchParams.set("lang", language);
  history[replaceHistory ? "replaceState" : "pushState"](null, "", url);
  if (window.location.hash) document.querySelector(window.location.hash)?.scrollIntoView();
}

languageTrigger.addEventListener("click", () => {
  if (languageMenu.hidden) openLanguageMenu();
  else closeLanguageMenu();
});
languageTrigger.addEventListener("keydown", (event) => {
  if (event.key === "ArrowDown" || event.key === "ArrowUp") {
    event.preventDefault();
    openLanguageMenu();
  }
});
languageMenu.addEventListener("keydown", (event) => {
  if (event.key === "ArrowDown") {
    event.preventDefault();
    moveLanguageFocus(1);
  } else if (event.key === "ArrowUp") {
    event.preventDefault();
    moveLanguageFocus(-1);
  } else if (event.key === "Home") {
    event.preventDefault();
    languageOptions[0]?.focus();
  } else if (event.key === "End") {
    event.preventDefault();
    languageOptions.at(-1)?.focus();
  } else if (event.key === "Escape") {
    event.preventDefault();
    closeLanguageMenu(true);
  }
});
languageSwitcher.addEventListener("focusout", () => {
  queueMicrotask(() => {
    if (!languageSwitcher.contains(document.activeElement)) closeLanguageMenu();
  });
});
document.addEventListener("pointerdown", (event) => {
  if (!languageMenu.hidden && !languageSwitcher.contains(event.target)) closeLanguageMenu();
});
for (const option of languageOptions) {
  option.addEventListener("click", () => {
    render(option.dataset.language);
    closeLanguageMenu(true);
  });
}
render(detectLanguage(), true);
