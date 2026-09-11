using System.Collections.ObjectModel;
using System.Windows.Threading;
using BluetoothNotify.App.Infrastructure;
using BluetoothNotify.App.Models;
using BluetoothNotify.App.Services;

namespace BluetoothNotify.App.ViewModels;

public sealed record PreferenceOption<T>(T Value, string Label);

public sealed class PreferencesChangedEventArgs(AppLanguagePreference language, AppThemePreference theme) : EventArgs
{
    public AppLanguagePreference Language { get; } = language;
    public AppThemePreference Theme { get; } = theme;
}

public sealed class BatteryStatusChangedEventArgs(int? minimumBatteryPercent) : EventArgs
{
    public int? MinimumBatteryPercent { get; } = minimumBatteryPercent;
}

public sealed class DevicesRefreshedEventArgs(
    IReadOnlyList<BluetoothDeviceState> devices,
    bool isBluetoothAvailable) : EventArgs
{
    public IReadOnlyList<BluetoothDeviceState> Devices { get; } = devices;
    public bool IsBluetoothAvailable { get; } = isBluetoothAvailable;
}

public sealed class TrayPanelViewModel : ObservableObject
{
    private readonly IBluetoothDeviceMonitor _monitor;
    private readonly ISettingsStore _settingsStore;
    private readonly IStartupRegistrationService _startupRegistration;
    private readonly IDeviceIconResolver _icons;
    private readonly Dispatcher _dispatcher;
    private readonly AsyncRefreshGate _refreshGate = new();
    private readonly RelayCommand _exitCommand;
    private readonly RelayCommand _openSettingsCommand;
    private readonly RelayCommand _closeSettingsCommand;
    private readonly RelayCommand _openPrivacyPolicyCommand;
    private readonly RelayCommand _openTermsCommand;
    private readonly RelayCommand _openDisclaimerCommand;
    private readonly RelayCommand _openGitHubCommand;
    private readonly SemaphoreSlim _settingsSaveGate = new(1, 1);
    private bool _isBusy;
    private bool _isBluetoothAvailable = true;
    private string? _warning;
    private DateTimeOffset? _updatedAt;
    private bool _notificationsEnabled = true;
    private bool _isSettingsOpen;
    private AppLanguagePreference _languagePreference;
    private AppThemePreference _themePreference;
    private bool _runAtStartup;
    private bool _mediumBatteryNotificationEnabled = true;
    private int _mediumBatteryThresholdPercent = 30;
    private bool _lowBatteryNotificationEnabled = true;
    private int _lowBatteryThresholdPercent = 15;

    public TrayPanelViewModel(
        IBluetoothDeviceMonitor monitor,
        ISettingsStore settingsStore,
        IStartupRegistrationService startupRegistration,
        IDeviceIconResolver icons,
        Dispatcher dispatcher)
    {
        _monitor = monitor;
        _settingsStore = settingsStore;
        _startupRegistration = startupRegistration;
        _icons = icons;
        _dispatcher = dispatcher;
        RefreshCommand = new AsyncCommand(() => RefreshAsync(true, false));
        ToggleNotificationsCommand = new AsyncCommand(ToggleNotificationsAsync);
        _exitCommand = new RelayCommand(() => ExitRequested?.Invoke(this, EventArgs.Empty));
        _openSettingsCommand = new RelayCommand(OpenSettings);
        _closeSettingsCommand = new RelayCommand(CloseSettings);
        _openPrivacyPolicyCommand = new RelayCommand(() => OpenLegalDocument("privacy"));
        _openTermsCommand = new RelayCommand(() => OpenLegalDocument("terms"));
        _openDisclaimerCommand = new RelayCommand(() => OpenLegalDocument("disclaimer"));
        _openGitHubCommand = new RelayCommand(OpenProjectRepository);
    }

    public ObservableCollection<BluetoothDeviceViewModel> Devices { get; } = [];
    public AsyncCommand RefreshCommand { get; }
    public AsyncCommand ToggleNotificationsCommand { get; }
    public event EventHandler? ExitRequested;
    public event EventHandler<PreferencesChangedEventArgs>? PreferencesChanged;
    public event EventHandler<BatteryStatusChangedEventArgs>? BatteryStatusChanged;
    public event EventHandler<DevicesRefreshedEventArgs>? DevicesRefreshed;
    public RelayCommand ExitCommand => _exitCommand;
    public RelayCommand OpenSettingsCommand => _openSettingsCommand;
    public RelayCommand CloseSettingsCommand => _closeSettingsCommand;
    public RelayCommand OpenPrivacyPolicyCommand => _openPrivacyPolicyCommand;
    public RelayCommand OpenTermsCommand => _openTermsCommand;
    public RelayCommand OpenDisclaimerCommand => _openDisclaimerCommand;
    public RelayCommand OpenGitHubCommand => _openGitHubCommand;

    public bool IsBusy { get => _isBusy; private set => SetProperty(ref _isBusy, value); }
    public bool IsBluetoothAvailable { get => _isBluetoothAvailable; private set { if (SetProperty(ref _isBluetoothAvailable, value)) OnPropertyChanged(nameof(ShowEmptyState)); } }
    public string? Warning { get => _warning; private set { if (SetProperty(ref _warning, value)) OnPropertyChanged(nameof(HasWarning)); } }
    public bool HasWarning => !string.IsNullOrWhiteSpace(Warning);
    public bool ShowEmptyState => IsBluetoothAvailable && Devices.Count == 0;
    public bool NotificationsEnabled
    {
        get => _notificationsEnabled;
        private set
        {
            if (!SetProperty(ref _notificationsEnabled, value)) return;
            OnPropertyChanged(nameof(NotificationsText));
            OnPropertyChanged(nameof(NotificationsIconGlyph));
        }
    }
    public string NotificationsText => NotificationsEnabled ? Properties.Strings.NotificationsEnabled : Properties.Strings.Notifications;
    public string NotificationsIconGlyph => GetNotificationsIconGlyph(NotificationsEnabled);
    public string BatterySummary => string.Format(Properties.Strings.DevicesWithBattery, CountConnectedBatteryDevices(Devices));
    public string UpdatedText => _updatedAt is { } at ? string.Format(Properties.Strings.Updated, at) : Properties.Strings.Loading;

    public static string GetNotificationsIconGlyph(bool enabled) => enabled ? "\uF2A3" : "\uF285";

    public static int CountConnectedBatteryDevices(IEnumerable<BluetoothDeviceViewModel> devices) =>
        devices.Count(device => device.IsConnected && device.HasBattery);

    public static int? GetMinimumConnectedBattery(IEnumerable<BluetoothDeviceViewModel> devices)
    {
        var values = devices.Where(device => device.IsConnected && device.HasBattery)
            .Select(device => device.BatteryPercent!.Value)
            .ToArray();
        return values.Length == 0 ? null : values.Min();
    }

    public bool IsSettingsOpen { get => _isSettingsOpen; private set => SetProperty(ref _isSettingsOpen, value); }
    public AppLanguagePreference LanguagePreference
    {
        get => _languagePreference;
        set
        {
            if (!SetProperty(ref _languagePreference, value)) return;
            _ = PersistSettingsAsync(true);
        }
    }
    public AppThemePreference ThemePreference
    {
        get => _themePreference;
        set
        {
            if (!SetProperty(ref _themePreference, value)) return;
            _ = PersistSettingsAsync(true);
        }
    }
    public bool RunAtStartup
    {
        get => _runAtStartup;
        set
        {
            var previous = _runAtStartup;
            if (!SetProperty(ref _runAtStartup, value)) return;

            try
            {
                _startupRegistration.SetEnabled(value);
            }
            catch
            {
                _runAtStartup = previous;
                OnPropertyChanged();
                Warning = Properties.Strings.StartupSettingError;
            }
        }
    }
    public bool MediumBatteryNotificationEnabled
    {
        get => _mediumBatteryNotificationEnabled;
        set
        {
            if (!SetProperty(ref _mediumBatteryNotificationEnabled, value)) return;
            _ = PersistSettingsAsync(false);
        }
    }
    public int MediumBatteryThresholdPercent
    {
        get => _mediumBatteryThresholdPercent;
        set
        {
            if (!SetProperty(ref _mediumBatteryThresholdPercent, value)) return;
            _ = PersistSettingsAsync(false);
        }
    }
    public bool LowBatteryNotificationEnabled
    {
        get => _lowBatteryNotificationEnabled;
        set
        {
            if (!SetProperty(ref _lowBatteryNotificationEnabled, value)) return;
            _ = PersistSettingsAsync(false);
        }
    }
    public int LowBatteryThresholdPercent
    {
        get => _lowBatteryThresholdPercent;
        set
        {
            if (!SetProperty(ref _lowBatteryThresholdPercent, value)) return;
            _ = PersistSettingsAsync(false);
        }
    }
    public IReadOnlyList<PreferenceOption<AppLanguagePreference>> LanguageOptions =>
    [
        new(AppLanguagePreference.System, Properties.Strings.SystemDefault),
        new(AppLanguagePreference.EnglishUnitedStates, Properties.Strings.EnglishUnitedStates),
        new(AppLanguagePreference.Russian, Properties.Strings.Russian),
        new(AppLanguagePreference.German, Properties.Strings.German),
        new(AppLanguagePreference.Japanese, Properties.Strings.Japanese),
        new(AppLanguagePreference.French, Properties.Strings.French),
        new(AppLanguagePreference.Spanish, Properties.Strings.Spanish),
        new(AppLanguagePreference.Portuguese, Properties.Strings.Portuguese),
        new(AppLanguagePreference.Chinese, Properties.Strings.Chinese),
        new(AppLanguagePreference.Korean, Properties.Strings.Korean)
    ];
    public IReadOnlyList<PreferenceOption<AppThemePreference>> ThemeOptions =>
    [
        new(AppThemePreference.System, Properties.Strings.SystemDefault),
        new(AppThemePreference.Light, Properties.Strings.LightTheme),
        new(AppThemePreference.Dark, Properties.Strings.DarkTheme)
    ];
    public IReadOnlyList<PreferenceOption<int>> MediumBatteryThresholdOptions =>
        new[] { 20, 25, 30, 35, 40, 45, 50 }.Select(value => new PreferenceOption<int>(value, $"{value}%")).ToArray();
    public IReadOnlyList<PreferenceOption<int>> LowBatteryThresholdOptions =>
        new[] { 5, 10, 15 }.Select(value => new PreferenceOption<int>(value, $"{value}%")).ToArray();

    public void OpenSettings() => IsSettingsOpen = true;
    public void CloseSettings() => IsSettingsOpen = false;

    private void OpenLegalDocument(string section)
    {
        if (!LegalDocumentService.TryOpen(section)) Warning = Properties.Strings.LegalDocumentOpenError;
    }

    private void OpenProjectRepository()
    {
        if (!LegalDocumentService.TryOpenProjectRepository()) Warning = Properties.Strings.LegalDocumentOpenError;
    }

    public void Initialize(AppSettings settings)
    {
        _notificationsEnabled = settings.NotificationsEnabled;
        _languagePreference = settings.Language;
        _themePreference = settings.Theme;
        try
        {
            _runAtStartup = _startupRegistration.IsEnabled();
        }
        catch
        {
            _runAtStartup = false;
            Warning = Properties.Strings.StartupSettingError;
        }
        _mediumBatteryNotificationEnabled = settings.MediumBatteryNotificationEnabled;
        _mediumBatteryThresholdPercent = settings.MediumBatteryThresholdPercent;
        _lowBatteryNotificationEnabled = settings.LowBatteryNotificationEnabled;
        _lowBatteryThresholdPercent = settings.LowBatteryThresholdPercent;
        OnPropertyChanged(nameof(NotificationsEnabled));
        OnPropertyChanged(nameof(NotificationsText));
        OnPropertyChanged(nameof(NotificationsIconGlyph));
        OnPropertyChanged(nameof(LanguagePreference));
        OnPropertyChanged(nameof(ThemePreference));
        OnPropertyChanged(nameof(RunAtStartup));
        OnPropertyChanged(nameof(MediumBatteryNotificationEnabled));
        OnPropertyChanged(nameof(MediumBatteryThresholdPercent));
        OnPropertyChanged(nameof(LowBatteryNotificationEnabled));
        OnPropertyChanged(nameof(LowBatteryThresholdPercent));
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        var settings = await _settingsStore.LoadAsync(cancellationToken).ConfigureAwait(false);
        await _dispatcher.InvokeAsync(() => Initialize(settings));
    }

    public async Task RefreshAsync(bool forceBatteryRead, bool skipIfBusy, CancellationToken cancellationToken = default)
    {
        await _refreshGate.RunAsync(async token =>
        {
            await _dispatcher.InvokeAsync(() => IsBusy = true);
            try
            {
                var snapshot = await _monitor.RefreshAsync(forceBatteryRead, token).ConfigureAwait(false);
                await _dispatcher.InvokeAsync(() => Apply(snapshot));
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested) { }
            catch
            {
                await _dispatcher.InvokeAsync(() => Warning = Properties.Strings.PartialRefreshWarning);
            }
            finally { await _dispatcher.InvokeAsync(() => IsBusy = false); }
        }, skipIfBusy, cancellationToken).ConfigureAwait(false);
    }

    private void Apply(MonitorSnapshot snapshot)
    {
        var existing = Devices.ToDictionary(x => x.StableId, StringComparer.OrdinalIgnoreCase);
        var ordered = new List<BluetoothDeviceViewModel>();
        foreach (var state in snapshot.Devices)
        {
            if (existing.TryGetValue(state.StableId, out var viewModel)) viewModel.Update(state);
            else viewModel = new BluetoothDeviceViewModel(state, _icons);
            ordered.Add(viewModel);
        }
        Devices.Clear();
        foreach (var item in ordered) Devices.Add(item);
        IsBluetoothAvailable = snapshot.IsBluetoothAvailable;
        Warning = snapshot.Warning;
        _updatedAt = snapshot.CompletedAt;
        OnPropertyChanged(nameof(UpdatedText));
        OnPropertyChanged(nameof(BatterySummary));
        OnPropertyChanged(nameof(ShowEmptyState));
        BatteryStatusChanged?.Invoke(this, new BatteryStatusChangedEventArgs(GetMinimumConnectedBattery(Devices)));
        DevicesRefreshed?.Invoke(this, new DevicesRefreshedEventArgs(snapshot.Devices, snapshot.IsBluetoothAvailable));
    }

    private async Task ToggleNotificationsAsync()
    {
        var previous = NotificationsEnabled;
        NotificationsEnabled = !previous;
        try { await SaveCurrentSettingsAsync().ConfigureAwait(false); }
        catch
        {
            await _dispatcher.InvokeAsync(() =>
            {
                NotificationsEnabled = previous;
                Warning = Properties.Strings.PartialRefreshWarning;
            });
        }
    }

    private async Task PersistSettingsAsync(bool notifyAppearanceChanged)
    {
        try
        {
            await SaveCurrentSettingsAsync().ConfigureAwait(false);
            if (notifyAppearanceChanged)
                await _dispatcher.InvokeAsync(() =>
                    PreferencesChanged?.Invoke(this, new PreferencesChangedEventArgs(LanguagePreference, ThemePreference)));
        }
        catch
        {
            await _dispatcher.InvokeAsync(() => Warning = Properties.Strings.PartialRefreshWarning);
        }
    }

    private async Task SaveCurrentSettingsAsync()
    {
        await _settingsSaveGate.WaitAsync().ConfigureAwait(false);
        try
        {
            var settings = await _dispatcher.InvokeAsync(() => new AppSettings
            {
                NotificationsEnabled = NotificationsEnabled,
                Language = LanguagePreference,
                Theme = ThemePreference,
                MediumBatteryNotificationEnabled = MediumBatteryNotificationEnabled,
                MediumBatteryThresholdPercent = MediumBatteryThresholdPercent,
                LowBatteryNotificationEnabled = LowBatteryNotificationEnabled,
                LowBatteryThresholdPercent = LowBatteryThresholdPercent
            });
            await _settingsStore.SaveAsync(settings).ConfigureAwait(false);
        }
        finally { _settingsSaveGate.Release(); }
    }
}
