export const localeOptions = [
  { code: 'en', htmlLang: 'en-US', short: 'EN', label: 'English (United States)' },
  { code: 'ru', htmlLang: 'ru-RU', short: 'RU', label: 'Русский' },
  { code: 'de', htmlLang: 'de-DE', short: 'DE', label: 'Deutsch' },
  { code: 'ja', htmlLang: 'ja-JP', short: 'JA', label: '日本語' },
  { code: 'fr', htmlLang: 'fr-FR', short: 'FR', label: 'Français' },
  { code: 'es', htmlLang: 'es-ES', short: 'ES', label: 'Español' },
  { code: 'pt', htmlLang: 'pt-PT', short: 'PT', label: 'Português' },
  { code: 'zh', htmlLang: 'zh-CN', short: 'ZH', label: '简体中文' },
  { code: 'ko', htmlLang: 'ko-KR', short: 'KO', label: '한국어' },
]

export const translations = {
  en: {
    meta: {
      title: 'Bluetooth Notify — Bluetooth battery status for Windows 11',
      description: 'A focused Windows 11 tray app for Bluetooth battery levels, connection status, and native low-battery alerts.',
    },
    languageLabel: 'Select language',
    navigationLabel: 'Main navigation',
    nav: { features: 'Features', interface: 'Interface', requirements: 'Requirements' },
    header: { build: 'Build from source', buildShort: 'Build' },
    hero: {
      eyebrow: 'Windows 11 tray utility', title: ['Battery status.', 'Zero guesswork.'],
      description: 'Bluetooth Notify puts device levels, connection state, and native low-battery alerts exactly where they belong: in your system tray.',
      explore: 'Explore the interface', why: 'Why it helps',
      meta: ['Native alerts', 'No account', '9 languages'],
      mainAlt: 'Bluetooth Notify light panel showing connected headphones with a 90 percent battery level',
      trayCaption: 'One click away', trayAlt: 'Bluetooth Notify icon in the Windows system tray', charged: 'CHARGED', live: 'LIVE', signalLive: 'SIGNAL: LIVE',
    },
    ticker: 'REAL BATTERY DATA ✦ NATIVE WINDOWS ALERTS ✦ ALWAYS IN THE TRAY ✦',
    features: {
      index: '[ 01 — FEATURES ]', title: ['Small app.', 'Strong signal.'],
      intro: 'A focused native companion that stays quiet until you need it—then tells you exactly what Windows knows.',
      overview: { title: 'One-click overview', body: 'Paired devices, current connection state, and the latest available battery reading in a compact panel.', foot: 'Refreshes while open' },
      alerts: { title: 'Threshold alerts', body: 'Set separate medium and low levels. Each warning fires once as the device crosses your threshold.', meterLabel: 'Example battery level at 30 percent' },
      native: { title: 'Native behavior', body: 'Hover or click to open, use the tray menu, and receive standard Windows app notifications.', foot: 'Settings stay local' },
    },
    interface: {
      index: '[ 02 — INTERFACE ]', title: ['Three views.', 'No clutter.'],
      intro: 'Switch between the live panel, notification settings, and the system-aware dark appearance.', tabsLabel: 'App screenshots',
      previews: [
        { label: '01 / Panel', tag: 'Live status', title: 'Every paired device in one clean view.', description: 'See real connection state and the latest battery level Windows can report—without leaving your current task.', alt: 'Bluetooth Notify light panel showing two paired headphones and their battery levels' },
        { label: '02 / Settings', tag: 'Your thresholds', title: 'Useful alerts, tuned by you.', description: 'Choose separate medium and low battery thresholds, switch notifications on or off, and apply changes instantly.', alt: 'Bluetooth Notify settings with language, theme, and battery notification controls' },
        { label: '03 / Dark mode', tag: 'System-aware', title: 'Looks right in every Windows theme.', description: 'Follow the system appearance or select light and dark mode yourself. The panel and tray icon stay easy to read.', alt: 'Bluetooth Notify panel in dark mode' },
      ],
      light: 'Light', dark: 'Dark', languages: '9 Languages',
    },
    requirements: {
      index: '[ 03 — SYSTEM ]', title: 'At home on Windows.',
      description: 'Bluetooth Notify reads standard Windows device data and uses native tray behavior. This framework-dependent build requires both runtimes below before first launch.',
      windows: 'Windows 11 22H2 or newer', download: 'Download ↗', ready: ['SYSTEM', 'READY'], toolbar: 'SETTINGS / APPEARANCE',
      settingsAlt: 'Bluetooth Notify light appearance settings', themeAware: 'Theme-aware', themes: 'Light / Dark / System',
    },
    build: {
      index: '[ 04 — RUN IT ]', title: ['Ready to keep', 'an eye on it?'],
      description: 'The installer checks both prerequisites and opens the official Microsoft download page if either one is missing. Developers can also launch the app from source.',
      line: '01 LINE', copy: 'Copy command', copied: 'Copied', copyAria: 'Copy build command', copiedAria: 'Command copied',
    },
    footer: { legalLabel: 'Legal information', privacy: 'Privacy', terms: 'Terms', disclaimer: 'Disclaimer', license: 'License', tagline: 'Focused Bluetooth battery status for Windows 11.' },
  },

  ru: {
    meta: {
      title: 'Bluetooth Notify — заряд Bluetooth-устройств в Windows 11',
      description: 'Компактное приложение для трея Windows 11: заряд Bluetooth-устройств, состояние подключения и нативные уведомления о разрядке.',
    },
    languageLabel: 'Выбрать язык', navigationLabel: 'Основная навигация',
    nav: { features: 'Возможности', interface: 'Интерфейс', requirements: 'Требования' },
    header: { build: 'Собрать из исходников', buildShort: 'Сборка' },
    hero: {
      eyebrow: 'Утилита для трея Windows 11', title: ['Заряд устройств.', 'Без догадок.'],
      description: 'Bluetooth Notify показывает заряд, состояние подключения и нативные предупреждения о разрядке там, где им самое место: в системном трее.',
      explore: 'Посмотреть интерфейс', why: 'Зачем это нужно', meta: ['Нативные уведомления', 'Без аккаунта', '9 языков'],
      mainAlt: 'Светлая панель Bluetooth Notify с подключёнными наушниками и зарядом 90 процентов', trayCaption: 'В одном клике',
      trayAlt: 'Значок Bluetooth Notify в системном трее Windows', charged: 'ЗАРЯЖЕНО', live: 'АКТИВНО', signalLive: 'СИГНАЛ: ЕСТЬ',
    },
    ticker: 'РЕАЛЬНЫЕ ДАННЫЕ О ЗАРЯДЕ ✦ НАТИВНЫЕ УВЕДОМЛЕНИЯ WINDOWS ✦ ВСЕГДА В ТРЕЕ ✦',
    features: {
      index: '[ 01 — ВОЗМОЖНОСТИ ]', title: ['Маленькое приложение.', 'Чёткий сигнал.'],
      intro: 'Компактный нативный помощник не отвлекает без причины и точно показывает всё, что известно Windows.',
      overview: { title: 'Обзор в один клик', body: 'Сопряжённые устройства, текущее подключение и последний доступный заряд в компактной панели.', foot: 'Обновляется, пока открыто' },
      alerts: { title: 'Уведомления по порогам', body: 'Задайте средний и низкий уровни. Предупреждение сработает один раз при пересечении каждого порога.', meterLabel: 'Пример уровня заряда 30 процентов' },
      native: { title: 'Нативное поведение', body: 'Наведите или нажмите, откройте меню трея и получайте стандартные уведомления Windows.', foot: 'Настройки хранятся локально' },
    },
    interface: {
      index: '[ 02 — ИНТЕРФЕЙС ]', title: ['Три режима.', 'Ничего лишнего.'],
      intro: 'Переключайтесь между панелью устройств, настройками уведомлений и тёмным оформлением по системной теме.', tabsLabel: 'Снимки приложения',
      previews: [
        { label: '01 / Панель', tag: 'Статус сейчас', title: 'Все сопряжённые устройства в одном окне.', description: 'Проверяйте реальное подключение и последний заряд, который видит Windows, не отвлекаясь от работы.', alt: 'Светлая панель Bluetooth Notify с двумя наушниками и уровнем заряда' },
        { label: '02 / Настройки', tag: 'Ваши пороги', title: 'Полезные уведомления по вашим правилам.', description: 'Задайте отдельные пороги среднего и низкого заряда, включайте уведомления и применяйте изменения сразу.', alt: 'Настройки Bluetooth Notify с выбором языка, темы и уведомлений о заряде' },
        { label: '03 / Тёмная тема', tag: 'Учитывает систему', title: 'Органично выглядит в любой теме Windows.', description: 'Следуйте системному оформлению или выберите светлую либо тёмную тему. Панель и значок остаются читаемыми.', alt: 'Панель Bluetooth Notify в тёмной теме' },
      ],
      light: 'Светлая', dark: 'Тёмная', languages: '9 языков',
    },
    requirements: {
      index: '[ 03 — СИСТЕМА ]', title: 'Создано для Windows.',
      description: 'Bluetooth Notify читает стандартные данные устройств Windows и работает как нативное приложение в трее. Перед первым запуском framework-dependent сборке нужны оба компонента ниже.',
      windows: 'Windows 11 22H2 или новее', download: 'Скачать ↗', ready: ['СИСТЕМА', 'ГОТОВА'], toolbar: 'НАСТРОЙКИ / ОФОРМЛЕНИЕ',
      settingsAlt: 'Светлые настройки оформления Bluetooth Notify', themeAware: 'Следует теме', themes: 'Светлая / Тёмная / Системная',
    },
    build: {
      index: '[ 04 — ЗАПУСК ]', title: ['Готовы следить', 'за зарядом?'],
      description: 'Установщик проверит оба обязательных компонента и откроет официальную страницу Microsoft, если чего-то не хватает. Разработчики могут запустить приложение из исходников.',
      line: '01 СТРОКА', copy: 'Копировать команду', copied: 'Скопировано', copyAria: 'Скопировать команду сборки', copiedAria: 'Команда скопирована',
    },
    footer: { legalLabel: 'Правовая информация', privacy: 'Конфиденциальность', terms: 'Условия', disclaimer: 'Отказ от ответственности', license: 'Лицензия', tagline: 'Точный заряд Bluetooth-устройств в Windows 11.' },
  },

  de: {
    meta: {
      title: 'Bluetooth Notify — Bluetooth-Akkustatus für Windows 11',
      description: 'Eine kompakte Windows-11-Tray-App für Bluetooth-Akkustände, Verbindungsstatus und native Warnungen bei niedrigem Akkustand.',
    },
    languageLabel: 'Sprache auswählen', navigationLabel: 'Hauptnavigation',
    nav: { features: 'Funktionen', interface: 'Oberfläche', requirements: 'Anforderungen' },
    header: { build: 'Aus Quellcode erstellen', buildShort: 'Erstellen' },
    hero: {
      eyebrow: 'Tray-Dienstprogramm für Windows 11', title: ['Akkustatus.', 'Ohne Rätselraten.'],
      description: 'Bluetooth Notify zeigt Gerätestände, Verbindungsstatus und native Warnungen bei niedrigem Akkustand genau dort, wo sie hingehören: im Infobereich.',
      explore: 'Oberfläche ansehen', why: 'Darum ist es nützlich', meta: ['Native Hinweise', 'Kein Konto', '9 Sprachen'],
      mainAlt: 'Helle Bluetooth-Notify-Ansicht mit verbundenem Kopfhörer bei 90 Prozent Akku', trayCaption: 'Nur einen Klick entfernt',
      trayAlt: 'Bluetooth-Notify-Symbol im Windows-Infobereich', charged: 'GELADEN', live: 'AKTIV', signalLive: 'SIGNAL: AKTIV',
    },
    ticker: 'ECHTE AKKUDATEN ✦ NATIVE WINDOWS-HINWEISE ✦ IMMER IM INFOBEREICH ✦',
    features: {
      index: '[ 01 — FUNKTIONEN ]', title: ['Kleine App.', 'Klares Signal.'],
      intro: 'Ein fokussierter nativer Begleiter, der ruhig bleibt, bis er gebraucht wird – und dann genau zeigt, was Windows weiß.',
      overview: { title: 'Überblick mit einem Klick', body: 'Gekoppelte Geräte, aktueller Verbindungsstatus und der letzte verfügbare Akkustand in einer kompakten Ansicht.', foot: 'Aktualisiert sich im geöffneten Zustand' },
      alerts: { title: 'Schwellenwert-Warnungen', body: 'Lege mittlere und niedrige Werte getrennt fest. Jede Warnung erscheint einmal beim Unterschreiten des Schwellenwerts.', meterLabel: 'Beispiel für einen Akkustand von 30 Prozent' },
      native: { title: 'Natives Verhalten', body: 'Per Zeigen oder Klicken öffnen, das Tray-Menü nutzen und normale Windows-Benachrichtigungen erhalten.', foot: 'Einstellungen bleiben lokal' },
    },
    interface: {
      index: '[ 02 — OBERFLÄCHE ]', title: ['Drei Ansichten.', 'Kein Ballast.'],
      intro: 'Wechsle zwischen Live-Ansicht, Benachrichtigungseinstellungen und systemgesteuertem dunklem Design.', tabsLabel: 'App-Screenshots',
      previews: [
        { label: '01 / Übersicht', tag: 'Live-Status', title: 'Alle gekoppelten Geräte übersichtlich an einem Ort.', description: 'Sieh den echten Verbindungsstatus und den letzten von Windows gemeldeten Akkustand, ohne deine Arbeit zu unterbrechen.', alt: 'Helle Bluetooth-Notify-Ansicht mit zwei gekoppelten Kopfhörern und deren Akkuständen' },
        { label: '02 / Einstellungen', tag: 'Deine Schwellenwerte', title: 'Nützliche Hinweise nach deinen Regeln.', description: 'Wähle getrennte Schwellenwerte, schalte Hinweise ein oder aus und übernimm Änderungen sofort.', alt: 'Bluetooth-Notify-Einstellungen für Sprache, Design und Akkuhinweise' },
        { label: '03 / Dunkles Design', tag: 'Systemgesteuert', title: 'Passt zu jedem Windows-Design.', description: 'Folge dem System oder wähle selbst ein helles oder dunkles Design. Ansicht und Tray-Symbol bleiben gut lesbar.', alt: 'Bluetooth-Notify-Ansicht im dunklen Design' },
      ],
      light: 'Hell', dark: 'Dunkel', languages: '9 Sprachen',
    },
    requirements: {
      index: '[ 03 — SYSTEM ]', title: 'Für Windows gemacht.',
      description: 'Bluetooth Notify liest standardisierte Windows-Gerätedaten und nutzt natives Tray-Verhalten. Dieser frameworkabhängige Build benötigt vor dem ersten Start beide Laufzeitkomponenten.',
      windows: 'Windows 11 22H2 oder neuer', download: 'Herunterladen ↗', ready: ['SYSTEM', 'BEREIT'], toolbar: 'EINSTELLUNGEN / DESIGN',
      settingsAlt: 'Helle Darstellungseinstellungen von Bluetooth Notify', themeAware: 'Designbewusst', themes: 'Hell / Dunkel / System',
    },
    build: {
      index: '[ 04 — STARTEN ]', title: ['Bereit, den Akku', 'im Blick zu behalten?'],
      description: 'Das Installationsprogramm prüft beide Voraussetzungen und öffnet bei Bedarf die offizielle Microsoft-Downloadseite. Entwickler können die App auch aus dem Quellcode starten.',
      line: '01 ZEILE', copy: 'Befehl kopieren', copied: 'Kopiert', copyAria: 'Build-Befehl kopieren', copiedAria: 'Befehl kopiert',
    },
    footer: { legalLabel: 'Rechtliche Hinweise', privacy: 'Datenschutz', terms: 'Bedingungen', disclaimer: 'Haftungsausschluss', license: 'Lizenz', tagline: 'Fokussierter Bluetooth-Akkustatus für Windows 11.' },
  },

  ja: {
    meta: {
      title: 'Bluetooth Notify — Windows 11 の Bluetooth バッテリー状態',
      description: 'Bluetooth 機器のバッテリー残量、接続状態、低残量のネイティブ通知を確認できる Windows 11 トレイアプリです。',
    },
    languageLabel: '言語を選択', navigationLabel: 'メインナビゲーション',
    nav: { features: '機能', interface: '画面', requirements: '動作要件' },
    header: { build: 'ソースからビルド', buildShort: 'ビルド' },
    hero: {
      eyebrow: 'Windows 11 トレイユーティリティ', title: ['バッテリー状態を、', '迷わず確認。'],
      description: 'Bluetooth Notify は機器の残量、接続状態、低残量のネイティブ通知を、最も使いやすいシステムトレイにまとめます。',
      explore: '画面を見る', why: '便利な理由', meta: ['ネイティブ通知', 'アカウント不要', '9言語'],
      mainAlt: '接続中のヘッドホンと90パーセントの残量を表示する Bluetooth Notify のライト画面', trayCaption: 'ワンクリックで確認',
      trayAlt: 'Windows システムトレイの Bluetooth Notify アイコン', charged: '充電済み', live: '稼働中', signalLive: '信号：稼働中',
    },
    ticker: '実際のバッテリーデータ ✦ WINDOWS ネイティブ通知 ✦ いつでもトレイから ✦',
    features: {
      index: '[ 01 — 機能 ]', title: ['小さなアプリ。', '確かな情報。'],
      intro: '必要なときまでは静かに、必要になれば Windows が把握している情報を正確に伝えるネイティブアプリです。',
      overview: { title: 'ワンクリックで一覧', body: 'ペアリング済み機器、現在の接続状態、取得できる最新の残量をコンパクトな画面で確認できます。', foot: '開いている間は自動更新' },
      alerts: { title: 'しきい値通知', body: '中程度と低残量の基準を個別に設定。各しきい値を下回ったとき、一度だけ通知します。', meterLabel: 'バッテリー残量30パーセントの例' },
      native: { title: 'Windows らしい操作', body: 'ポイントまたはクリックで開き、トレイメニューと標準の Windows 通知をそのまま使えます。', foot: '設定は端末内に保存' },
    },
    interface: {
      index: '[ 02 — 画面 ]', title: ['3つの画面。', '余計なものはなし。'],
      intro: 'ライブパネル、通知設定、システムに合わせたダーク表示を切り替えて確認できます。', tabsLabel: 'アプリのスクリーンショット',
      previews: [
        { label: '01 / パネル', tag: 'ライブ状態', title: 'ペアリング済み機器を、ひとつの見やすい画面に。', description: '作業を止めずに、実際の接続状態と Windows が取得できる最新の残量を確認できます。', alt: '2台のペアリング済みヘッドホンと残量を表示する Bluetooth Notify のライト画面' },
        { label: '02 / 設定', tag: '自分のしきい値', title: '必要な通知を、自分好みに。', description: '中程度と低残量のしきい値を個別に選び、通知のオン・オフや変更をすぐに反映できます。', alt: '言語、テーマ、バッテリー通知を設定する Bluetooth Notify の画面' },
        { label: '03 / ダークモード', tag: 'システム連動', title: 'どの Windows テーマにも自然になじみます。', description: 'システム設定に合わせるか、ライトとダークを自分で選択。パネルもトレイアイコンも見やすさを保ちます。', alt: 'ダークモードの Bluetooth Notify パネル' },
      ],
      light: 'ライト', dark: 'ダーク', languages: '9言語',
    },
    requirements: {
      index: '[ 03 — システム ]', title: 'Windows に自然になじむ。',
      description: 'Bluetooth Notify は Windows 標準の機器データを読み取り、ネイティブなトレイ操作を使います。このフレームワーク依存ビルドの初回起動には、以下の2つのランタイムが必要です。',
      windows: 'Windows 11 22H2 以降', download: 'ダウンロード ↗', ready: ['システム', '準備完了'], toolbar: '設定 / 外観',
      settingsAlt: 'Bluetooth Notify のライトテーマ設定', themeAware: 'テーマ連動', themes: 'ライト / ダーク / システム',
    },
    build: {
      index: '[ 04 — 実行 ]', title: ['バッテリーを', '見守りませんか？'],
      description: 'インストーラーは2つの前提コンポーネントを確認し、不足している場合は Microsoft の公式ダウンロードページを開きます。開発者はソースから直接起動できます。',
      line: '01 行', copy: 'コマンドをコピー', copied: 'コピーしました', copyAria: 'ビルドコマンドをコピー', copiedAria: 'コマンドをコピーしました',
    },
    footer: { legalLabel: '法的情報', privacy: 'プライバシー', terms: '利用規約', disclaimer: '免責事項', license: 'ライセンス', tagline: 'Windows 11 の Bluetooth バッテリー状態を、ひと目で。' },
  },

  fr: {
    meta: {
      title: 'Bluetooth Notify — état de la batterie Bluetooth sous Windows 11',
      description: 'Une application discrète dans la zone de notification de Windows 11 pour suivre la batterie Bluetooth, la connexion et recevoir des alertes natives.',
    },
    languageLabel: 'Choisir la langue', navigationLabel: 'Navigation principale',
    nav: { features: 'Fonctions', interface: 'Interface', requirements: 'Configuration' },
    header: { build: 'Compiler depuis les sources', buildShort: 'Compiler' },
    hero: {
      eyebrow: 'Utilitaire pour la zone de notification Windows 11', title: ['État de la batterie.', 'Aucun doute.'],
      description: 'Bluetooth Notify place le niveau des appareils, leur état de connexion et les alertes natives de batterie faible exactement là où il faut : dans la zone de notification.',
      explore: 'Découvrir l’interface', why: 'Pourquoi c’est utile', meta: ['Alertes natives', 'Aucun compte', '9 langues'],
      mainAlt: 'Panneau clair de Bluetooth Notify affichant un casque connecté avec 90 pour cent de batterie', trayCaption: 'À un clic',
      trayAlt: 'Icône Bluetooth Notify dans la zone de notification Windows', charged: 'CHARGÉ', live: 'ACTIF', signalLive: 'SIGNAL : ACTIF',
    },
    ticker: 'DONNÉES DE BATTERIE RÉELLES ✦ ALERTES WINDOWS NATIVES ✦ TOUJOURS DANS LA ZONE DE NOTIFICATION ✦',
    features: {
      index: '[ 01 — FONCTIONS ]', title: ['Petite application.', 'Signal clair.'],
      intro: 'Un compagnon natif ciblé, discret jusqu’au moment utile, qui vous dit précisément ce que Windows sait.',
      overview: { title: 'Vue d’ensemble en un clic', body: 'Appareils jumelés, état de connexion actuel et dernier niveau de batterie disponible dans un panneau compact.', foot: 'Actualisation pendant l’ouverture' },
      alerts: { title: 'Alertes par seuil', body: 'Définissez séparément les niveaux moyen et faible. Chaque alerte se déclenche une fois au franchissement du seuil.', meterLabel: 'Exemple de niveau de batterie à 30 pour cent' },
      native: { title: 'Comportement natif', body: 'Survolez ou cliquez pour ouvrir, utilisez le menu de la zone de notification et recevez les alertes Windows standard.', foot: 'Les réglages restent en local' },
    },
    interface: {
      index: '[ 02 — INTERFACE ]', title: ['Trois vues.', 'Sans encombrement.'],
      intro: 'Passez du panneau en direct aux réglages des notifications ou au thème sombre adapté au système.', tabsLabel: 'Captures de l’application',
      previews: [
        { label: '01 / Panneau', tag: 'État en direct', title: 'Tous les appareils jumelés dans une vue claire.', description: 'Consultez la connexion réelle et le dernier niveau transmis à Windows sans interrompre votre travail.', alt: 'Panneau clair de Bluetooth Notify avec deux casques jumelés et leurs niveaux de batterie' },
        { label: '02 / Réglages', tag: 'Vos seuils', title: 'Des alertes utiles, réglées par vos soins.', description: 'Choisissez des seuils distincts, activez ou désactivez les notifications et appliquez les changements immédiatement.', alt: 'Réglages de Bluetooth Notify pour la langue, le thème et les notifications de batterie' },
        { label: '03 / Mode sombre', tag: 'Adapté au système', title: 'À sa place dans chaque thème Windows.', description: 'Suivez l’apparence du système ou choisissez vous-même le mode clair ou sombre. Le panneau et l’icône restent lisibles.', alt: 'Panneau Bluetooth Notify en mode sombre' },
      ],
      light: 'Clair', dark: 'Sombre', languages: '9 langues',
    },
    requirements: {
      index: '[ 03 — SYSTÈME ]', title: 'Naturellement à sa place sous Windows.',
      description: 'Bluetooth Notify lit les données standard des appareils Windows et adopte le comportement natif de la zone de notification. Cette version dépendante du framework requiert les deux composants ci-dessous avant le premier lancement.',
      windows: 'Windows 11 22H2 ou version ultérieure', download: 'Télécharger ↗', ready: ['SYSTÈME', 'PRÊT'], toolbar: 'RÉGLAGES / APPARENCE',
      settingsAlt: 'Réglages d’apparence claire de Bluetooth Notify', themeAware: 'Adapté au thème', themes: 'Clair / Sombre / Système',
    },
    build: {
      index: '[ 04 — LANCER ]', title: ['Prêt à garder', 'un œil dessus ?'],
      description: 'Le programme d’installation vérifie les deux prérequis et ouvre la page Microsoft officielle si l’un manque. Les développeurs peuvent aussi lancer l’application depuis les sources.',
      line: '01 LIGNE', copy: 'Copier la commande', copied: 'Copiée', copyAria: 'Copier la commande de compilation', copiedAria: 'Commande copiée',
    },
    footer: { legalLabel: 'Informations légales', privacy: 'Confidentialité', terms: 'Conditions', disclaimer: 'Clause de non-responsabilité', license: 'Licence', tagline: 'L’état de la batterie Bluetooth sous Windows 11, sans détour.' },
  },

  es: {
    meta: {
      title: 'Bluetooth Notify — estado de batería Bluetooth para Windows 11',
      description: 'Una aplicación compacta para la bandeja de Windows 11 con niveles de batería Bluetooth, estado de conexión y alertas nativas.',
    },
    languageLabel: 'Seleccionar idioma', navigationLabel: 'Navegación principal',
    nav: { features: 'Funciones', interface: 'Interfaz', requirements: 'Requisitos' },
    header: { build: 'Compilar desde el código', buildShort: 'Compilar' },
    hero: {
      eyebrow: 'Utilidad para la bandeja de Windows 11', title: ['Estado de batería.', 'Sin adivinar.'],
      description: 'Bluetooth Notify coloca los niveles, el estado de conexión y las alertas nativas de batería baja justo donde deben estar: en la bandeja del sistema.',
      explore: 'Explorar la interfaz', why: 'Por qué ayuda', meta: ['Alertas nativas', 'Sin cuenta', '9 idiomas'],
      mainAlt: 'Panel claro de Bluetooth Notify con unos auriculares conectados al 90 por ciento', trayCaption: 'A un clic',
      trayAlt: 'Icono de Bluetooth Notify en la bandeja del sistema de Windows', charged: 'CARGADO', live: 'ACTIVO', signalLive: 'SEÑAL: ACTIVA',
    },
    ticker: 'DATOS REALES DE BATERÍA ✦ ALERTAS NATIVAS DE WINDOWS ✦ SIEMPRE EN LA BANDEJA ✦',
    features: {
      index: '[ 01 — FUNCIONES ]', title: ['Aplicación pequeña.', 'Señal clara.'],
      intro: 'Un asistente nativo y discreto que permanece en silencio hasta que lo necesitas y te cuenta exactamente lo que sabe Windows.',
      overview: { title: 'Resumen en un clic', body: 'Dispositivos vinculados, conexión actual y la última lectura de batería disponible en un panel compacto.', foot: 'Se actualiza mientras está abierto' },
      alerts: { title: 'Alertas por umbral', body: 'Configura por separado los niveles medio y bajo. Cada aviso aparece una vez al cruzar su umbral.', meterLabel: 'Ejemplo de nivel de batería al 30 por ciento' },
      native: { title: 'Comportamiento nativo', body: 'Pasa el puntero o haz clic para abrir, usa el menú de la bandeja y recibe notificaciones estándar de Windows.', foot: 'La configuración queda en local' },
    },
    interface: {
      index: '[ 02 — INTERFAZ ]', title: ['Tres vistas.', 'Sin ruido.'],
      intro: 'Alterna entre el panel en directo, los ajustes de notificaciones y el aspecto oscuro adaptado al sistema.', tabsLabel: 'Capturas de la aplicación',
      previews: [
        { label: '01 / Panel', tag: 'Estado en directo', title: 'Todos tus dispositivos vinculados en una vista limpia.', description: 'Consulta la conexión real y el último nivel que Windows puede leer sin interrumpir tu tarea.', alt: 'Panel claro de Bluetooth Notify con dos auriculares vinculados y sus niveles de batería' },
        { label: '02 / Ajustes', tag: 'Tus umbrales', title: 'Alertas útiles, ajustadas por ti.', description: 'Elige umbrales distintos para batería media y baja, activa o desactiva avisos y aplica los cambios al instante.', alt: 'Ajustes de Bluetooth Notify con controles de idioma, tema y notificaciones de batería' },
        { label: '03 / Modo oscuro', tag: 'Adaptado al sistema', title: 'Se ve bien con cualquier tema de Windows.', description: 'Sigue el aspecto del sistema o elige el modo claro u oscuro. El panel y el icono siempre se leen bien.', alt: 'Panel de Bluetooth Notify en modo oscuro' },
      ],
      light: 'Claro', dark: 'Oscuro', languages: '9 idiomas',
    },
    requirements: {
      index: '[ 03 — SISTEMA ]', title: 'Como en casa en Windows.',
      description: 'Bluetooth Notify lee los datos estándar de dispositivos de Windows y usa el comportamiento nativo de la bandeja. Esta compilación dependiente del framework necesita los dos componentes siguientes antes del primer inicio.',
      windows: 'Windows 11 22H2 o posterior', download: 'Descargar ↗', ready: ['SISTEMA', 'LISTO'], toolbar: 'AJUSTES / APARIENCIA',
      settingsAlt: 'Ajustes de apariencia clara de Bluetooth Notify', themeAware: 'Sigue el tema', themes: 'Claro / Oscuro / Sistema',
    },
    build: {
      index: '[ 04 — EJECUTAR ]', title: ['¿Listo para no perderlo', 'de vista?'],
      description: 'El instalador comprueba ambos requisitos y abre la página oficial de Microsoft si falta alguno. Los desarrolladores también pueden iniciar la aplicación desde el código fuente.',
      line: '01 LÍNEA', copy: 'Copiar comando', copied: 'Copiado', copyAria: 'Copiar comando de compilación', copiedAria: 'Comando copiado',
    },
    footer: { legalLabel: 'Información legal', privacy: 'Privacidad', terms: 'Condiciones', disclaimer: 'Aviso legal', license: 'Licencia', tagline: 'Estado de batería Bluetooth claro y directo para Windows 11.' },
  },

  pt: {
    meta: {
      title: 'Bluetooth Notify — estado da bateria Bluetooth no Windows 11',
      description: 'Uma aplicação compacta para a área de notificação do Windows 11 com níveis de bateria Bluetooth, estado da ligação e alertas nativos.',
    },
    languageLabel: 'Selecionar idioma', navigationLabel: 'Navegação principal',
    nav: { features: 'Funcionalidades', interface: 'Interface', requirements: 'Requisitos' },
    header: { build: 'Compilar a partir do código', buildShort: 'Compilar' },
    hero: {
      eyebrow: 'Utilitário para a área de notificação do Windows 11', title: ['Estado da bateria.', 'Sem adivinhações.'],
      description: 'O Bluetooth Notify coloca os níveis dos dispositivos, o estado da ligação e os alertas nativos de bateria fraca exatamente onde devem estar: na área de notificação.',
      explore: 'Explorar a interface', why: 'Porque é útil', meta: ['Alertas nativos', 'Sem conta', '9 idiomas'],
      mainAlt: 'Painel claro do Bluetooth Notify com auscultadores ligados e 90 por cento de bateria', trayCaption: 'À distância de um clique',
      trayAlt: 'Ícone do Bluetooth Notify na área de notificação do Windows', charged: 'CARREGADO', live: 'ATIVO', signalLive: 'SINAL: ATIVO',
    },
    ticker: 'DADOS REAIS DA BATERIA ✦ ALERTAS NATIVOS DO WINDOWS ✦ SEMPRE NA ÁREA DE NOTIFICAÇÃO ✦',
    features: {
      index: '[ 01 — FUNCIONALIDADES ]', title: ['Aplicação pequena.', 'Sinal claro.'],
      intro: 'Um assistente nativo focado que permanece discreto até ser necessário e depois mostra exatamente o que o Windows sabe.',
      overview: { title: 'Visão geral num clique', body: 'Dispositivos emparelhados, estado atual da ligação e a última leitura de bateria disponível num painel compacto.', foot: 'Atualiza enquanto está aberto' },
      alerts: { title: 'Alertas por limiar', body: 'Defina níveis médio e baixo em separado. Cada aviso surge uma vez quando o dispositivo cruza o limiar.', meterLabel: 'Exemplo de nível da bateria a 30 por cento' },
      native: { title: 'Comportamento nativo', body: 'Passe o ponteiro ou clique para abrir, use o menu da área de notificação e receba notificações padrão do Windows.', foot: 'As definições ficam no dispositivo' },
    },
    interface: {
      index: '[ 02 — INTERFACE ]', title: ['Três vistas.', 'Sem confusão.'],
      intro: 'Alterne entre o painel em direto, as definições de notificações e o aspeto escuro adaptado ao sistema.', tabsLabel: 'Capturas da aplicação',
      previews: [
        { label: '01 / Painel', tag: 'Estado em direto', title: 'Todos os dispositivos emparelhados numa vista limpa.', description: 'Veja a ligação real e o nível mais recente que o Windows consegue indicar sem interromper a tarefa atual.', alt: 'Painel claro do Bluetooth Notify com dois auscultadores emparelhados e os respetivos níveis de bateria' },
        { label: '02 / Definições', tag: 'Os seus limiares', title: 'Alertas úteis, ajustados por si.', description: 'Escolha limiares distintos, ative ou desative notificações e aplique as alterações de imediato.', alt: 'Definições do Bluetooth Notify com controlos de idioma, tema e notificações da bateria' },
        { label: '03 / Modo escuro', tag: 'Adaptado ao sistema', title: 'Fica bem com qualquer tema do Windows.', description: 'Siga o aspeto do sistema ou escolha os modos claro e escuro. O painel e o ícone mantêm-se legíveis.', alt: 'Painel do Bluetooth Notify no modo escuro' },
      ],
      light: 'Claro', dark: 'Escuro', languages: '9 idiomas',
    },
    requirements: {
      index: '[ 03 — SISTEMA ]', title: 'Em casa no Windows.',
      description: 'O Bluetooth Notify lê dados padrão dos dispositivos Windows e usa o comportamento nativo da área de notificação. Esta compilação dependente da framework requer os dois componentes abaixo antes do primeiro arranque.',
      windows: 'Windows 11 22H2 ou mais recente', download: 'Transferir ↗', ready: ['SISTEMA', 'PRONTO'], toolbar: 'DEFINIÇÕES / ASPETO',
      settingsAlt: 'Definições de aspeto claro do Bluetooth Notify', themeAware: 'Segue o tema', themes: 'Claro / Escuro / Sistema',
    },
    build: {
      index: '[ 04 — EXECUTAR ]', title: ['Pronto para manter', 'tudo debaixo de olho?'],
      description: 'O instalador verifica ambos os pré-requisitos e abre a página oficial da Microsoft se faltar algum. Os programadores também podem iniciar a aplicação a partir do código.',
      line: '01 LINHA', copy: 'Copiar comando', copied: 'Copiado', copyAria: 'Copiar comando de compilação', copiedAria: 'Comando copiado',
    },
    footer: { legalLabel: 'Informação jurídica', privacy: 'Privacidade', terms: 'Termos', disclaimer: 'Isenção de responsabilidade', license: 'Licença', tagline: 'Estado da bateria Bluetooth, sem distrações, para Windows 11.' },
  },

  zh: {
    meta: {
      title: 'Bluetooth Notify — Windows 11 蓝牙电量状态',
      description: '一款简洁的 Windows 11 托盘应用，用于查看蓝牙设备电量、连接状态并接收原生低电量通知。',
    },
    languageLabel: '选择语言', navigationLabel: '主导航',
    nav: { features: '功能', interface: '界面', requirements: '系统要求' },
    header: { build: '从源代码构建', buildShort: '构建' },
    hero: {
      eyebrow: 'Windows 11 托盘工具', title: ['电量状态，', '一目了然。'],
      description: 'Bluetooth Notify 将设备电量、连接状态和原生低电量提醒放在最合适的位置：系统托盘。',
      explore: '查看界面', why: '为何实用', meta: ['原生通知', '无需账户', '9 种语言'],
      mainAlt: 'Bluetooth Notify 浅色面板，显示已连接耳机和 90% 电量', trayCaption: '一键即达',
      trayAlt: 'Windows 系统托盘中的 Bluetooth Notify 图标', charged: '已充电', live: '实时', signalLive: '信号：实时',
    },
    ticker: '真实电量数据 ✦ WINDOWS 原生通知 ✦ 常驻系统托盘 ✦',
    features: {
      index: '[ 01 — 功能 ]', title: ['小巧应用。', '清晰状态。'],
      intro: '这款专注的原生小工具平时保持安静，需要时准确告诉你 Windows 掌握的信息。',
      overview: { title: '一键概览', body: '在紧凑面板中查看已配对设备、当前连接状态和最近可用的电量读数。', foot: '打开时自动刷新' },
      alerts: { title: '阈值提醒', body: '分别设置中等和低电量阈值。设备每次跌破阈值时只提醒一次。', meterLabel: '电量为 30% 的示例' },
      native: { title: '原生体验', body: '悬停或点击即可打开，还可使用托盘菜单并接收标准 Windows 应用通知。', foot: '设置仅保存在本机' },
    },
    interface: {
      index: '[ 02 — 界面 ]', title: ['三种视图。', '没有杂乱。'],
      intro: '在实时面板、通知设置和跟随系统的深色外观之间切换。', tabsLabel: '应用截图',
      previews: [
        { label: '01 / 面板', tag: '实时状态', title: '所有已配对设备，集中清晰呈现。', description: '无需离开当前任务，即可查看真实连接状态和 Windows 能够读取的最新电量。', alt: 'Bluetooth Notify 浅色面板，显示两副已配对耳机及其电量' },
        { label: '02 / 设置', tag: '自定义阈值', title: '实用提醒，由你设定。', description: '分别选择中等和低电量阈值，开关通知并立即应用更改。', alt: 'Bluetooth Notify 设置，包含语言、主题和电量通知控件' },
        { label: '03 / 深色模式', tag: '跟随系统', title: '适配每一种 Windows 主题。', description: '跟随系统外观，或自行选择浅色与深色模式。面板和托盘图标始终清晰易读。', alt: '深色模式下的 Bluetooth Notify 面板' },
      ],
      light: '浅色', dark: '深色', languages: '9 种语言',
    },
    requirements: {
      index: '[ 03 — 系统 ]', title: '专为 Windows 而生。',
      description: 'Bluetooth Notify 读取标准 Windows 设备数据，并采用原生托盘交互。此依赖框架的版本在首次启动前需要安装以下两个运行时。',
      windows: 'Windows 11 22H2 或更高版本', download: '下载 ↗', ready: ['系统', '就绪'], toolbar: '设置 / 外观',
      settingsAlt: 'Bluetooth Notify 浅色外观设置', themeAware: '跟随主题', themes: '浅色 / 深色 / 系统',
    },
    build: {
      index: '[ 04 — 运行 ]', title: ['准备好随时掌握', '设备电量了吗？'],
      description: '安装程序会检查两个必备组件，如有缺失则打开 Microsoft 官方下载页面。开发者也可以直接从源代码启动应用。',
      line: '01 行', copy: '复制命令', copied: '已复制', copyAria: '复制构建命令', copiedAria: '命令已复制',
    },
    footer: { legalLabel: '法律信息', privacy: '隐私政策', terms: '使用条款', disclaimer: '免责声明', license: '许可证', tagline: '专注呈现 Windows 11 蓝牙设备电量。' },
  },

  ko: {
    meta: {
      title: 'Bluetooth Notify — Windows 11 Bluetooth 배터리 상태',
      description: 'Bluetooth 기기 배터리, 연결 상태, 기본 저전력 알림을 보여 주는 간결한 Windows 11 트레이 앱입니다.',
    },
    languageLabel: '언어 선택', navigationLabel: '기본 탐색',
    nav: { features: '기능', interface: '인터페이스', requirements: '요구 사항' },
    header: { build: '소스에서 빌드', buildShort: '빌드' },
    hero: {
      eyebrow: 'Windows 11 트레이 유틸리티', title: ['배터리 상태.', '추측은 필요 없습니다.'],
      description: 'Bluetooth Notify는 기기 잔량, 연결 상태, 기본 배터리 부족 알림을 가장 알맞은 곳인 시스템 트레이에 모아 줍니다.',
      explore: '인터페이스 보기', why: '유용한 이유', meta: ['기본 알림', '계정 불필요', '9개 언어'],
      mainAlt: '연결된 헤드폰과 90퍼센트 배터리를 표시하는 Bluetooth Notify 밝은 패널', trayCaption: '클릭 한 번이면 충분',
      trayAlt: 'Windows 시스템 트레이의 Bluetooth Notify 아이콘', charged: '충전됨', live: '실시간', signalLive: '신호: 실시간',
    },
    ticker: '실제 배터리 데이터 ✦ WINDOWS 기본 알림 ✦ 언제나 시스템 트레이에서 ✦',
    features: {
      index: '[ 01 — 기능 ]', title: ['작은 앱.', '분명한 신호.'],
      intro: '필요할 때까지 조용히 있다가 Windows가 알고 있는 정보를 정확하게 알려 주는 집중형 네이티브 도우미입니다.',
      overview: { title: '한 번에 보는 개요', body: '페어링된 기기, 현재 연결 상태, 사용 가능한 최신 배터리 정보를 간결한 패널에서 확인합니다.', foot: '열려 있는 동안 새로 고침' },
      alerts: { title: '임계값 알림', body: '중간 및 낮은 수준을 따로 설정하세요. 기기가 임계값을 지날 때마다 알림이 한 번만 표시됩니다.', meterLabel: '배터리 30퍼센트 예시' },
      native: { title: 'Windows다운 동작', body: '가리키거나 클릭해 열고 트레이 메뉴를 사용하며 표준 Windows 앱 알림을 받습니다.', foot: '설정은 기기에만 저장' },
    },
    interface: {
      index: '[ 02 — 인터페이스 ]', title: ['세 가지 화면.', '군더더기 없이.'],
      intro: '실시간 패널, 알림 설정, 시스템을 따르는 어두운 화면을 오가며 확인하세요.', tabsLabel: '앱 스크린샷',
      previews: [
        { label: '01 / 패널', tag: '실시간 상태', title: '페어링된 모든 기기를 하나의 깔끔한 화면에서.', description: '하던 일을 멈추지 않고 실제 연결 상태와 Windows가 읽을 수 있는 최신 배터리를 확인하세요.', alt: '페어링된 헤드폰 두 개와 배터리를 표시하는 Bluetooth Notify 밝은 패널' },
        { label: '02 / 설정', tag: '나만의 임계값', title: '필요한 알림을 내 방식대로.', description: '중간 및 낮은 배터리 임계값을 따로 정하고, 알림을 켜거나 끈 뒤 변경 사항을 즉시 적용하세요.', alt: '언어, 테마, 배터리 알림 설정이 있는 Bluetooth Notify 설정 화면' },
        { label: '03 / 다크 모드', tag: '시스템 연동', title: '어떤 Windows 테마에도 자연스럽게.', description: '시스템 모양을 따르거나 밝은 모드와 어두운 모드를 직접 선택하세요. 패널과 트레이 아이콘은 늘 선명합니다.', alt: '다크 모드의 Bluetooth Notify 패널' },
      ],
      light: '라이트', dark: '다크', languages: '9개 언어',
    },
    requirements: {
      index: '[ 03 — 시스템 ]', title: 'Windows에 자연스럽게.',
      description: 'Bluetooth Notify는 표준 Windows 기기 데이터를 읽고 네이티브 트레이 동작을 사용합니다. 이 프레임워크 종속 빌드는 처음 실행하기 전에 아래 두 런타임이 필요합니다.',
      windows: 'Windows 11 22H2 이상', download: '다운로드 ↗', ready: ['시스템', '준비 완료'], toolbar: '설정 / 모양',
      settingsAlt: 'Bluetooth Notify 밝은 모양 설정', themeAware: '테마 연동', themes: '라이트 / 다크 / 시스템',
    },
    build: {
      index: '[ 04 — 실행 ]', title: ['배터리를 계속', '지켜볼까요?'],
      description: '설치 프로그램은 두 필수 구성 요소를 확인하고, 빠진 항목이 있으면 Microsoft 공식 다운로드 페이지를 엽니다. 개발자는 소스에서 앱을 실행할 수도 있습니다.',
      line: '01줄', copy: '명령 복사', copied: '복사됨', copyAria: '빌드 명령 복사', copiedAria: '명령이 복사되었습니다',
    },
    footer: { legalLabel: '법적 정보', privacy: '개인정보', terms: '이용약관', disclaimer: '면책조항', license: '라이선스', tagline: 'Windows 11에서 Bluetooth 배터리 상태를 간결하게.' },
  },
}

export function resolveLocale(value, fallback = 'en') {
  const normalized = String(value ?? '').trim().toLowerCase().replace('_', '-')
  const language = normalized.split('-')[0]
  return localeOptions.some((option) => option.code === language) ? language : fallback
}
