using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Text;
using BluetoothNotify.App.Interop;
using BluetoothNotify.App.Models;
using BluetoothNotify.App.Services;
using BluetoothNotify.App.ViewModels;
using BluetoothNotify.App.Views;
using Microsoft.Win32;

namespace BluetoothNotify.App;

public partial class App : Application
{
    private const string AppUserModelId = "BluetoothNotify.App";
    private readonly CancellationTokenSource _lifetime = new();
    private readonly AppLogger _logger = new();
    private readonly BatteryNotificationTracker _batteryNotificationTracker = new();
    private SingleInstanceCoordinator? _singleInstance;
    private TrayIconService? _tray;
    private BatteryReader? _battery;
    private BluetoothDeviceMonitor? _monitor;
    private NotificationService? _notifications;
    private TrayPanelViewModel? _viewModel;
    private TrayPanelWindow? _window;
    private CancellationTokenSource? _polling;
    private CancellationTokenSource? _invalidationRefresh;
    private bool _exiting;
    private bool _panelPinned;
    private bool _panelWasActivated;
    private bool _recreatingWindow;
    private DateTimeOffset _shownAt;
    private AppLanguagePreference _languagePreference;
    private AppThemePreference _themePreference;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        RegisterExceptionHandlers();
        var exitRequested = e.Args.Contains("--exit", StringComparer.OrdinalIgnoreCase);
        _singleInstance = new SingleInstanceCoordinator();
        if (!_singleInstance.TryAcquire())
        {
            await SingleInstanceCoordinator.SignalExistingAsync(exitRequested
                ? SingleInstanceCoordinator.ExitCommand
                : SingleInstanceCoordinator.ShowCommand);
            Shutdown();
            return;
        }

        // Installers and uninstallers can request a clean shutdown without starting a new tray instance.
        if (exitRequested)
        {
            _singleInstance.Dispose();
            _singleInstance = null;
            Shutdown();
            return;
        }

        try
        {
            _logger.Info($"Application starting (arguments: {string.Join(' ', e.Args)}). ");
            var settingsStore = new SettingsStore();
            var initialSettings = await settingsStore.LoadAsync(_lifetime.Token);
            _languagePreference = initialSettings.Language;
            _themePreference = initialSettings.Theme;
            LocalizationService.Apply(_languagePreference);
            ThemeManager.Apply(_themePreference);
            SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;

            var appIdResult = NativeMethods.SetCurrentProcessExplicitAppUserModelID(AppUserModelId);
            if (appIdResult < 0) _logger.Warning($"AppUserModelID could not be assigned (HRESULT 0x{appIdResult:X8}).");
            _tray = new TrayIconService(_logger, _themePreference);
            _tray.Initialize();
            WireTrayEvents();

            _battery = new BatteryReader(_logger);
            _monitor = new BluetoothDeviceMonitor(_battery, _logger);
            _notifications = new NotificationService(_logger);
            _notifications.Initialize();
            _viewModel = new TrayPanelViewModel(_monitor, settingsStore, new DeviceIconResolver(), Dispatcher);
            _viewModel.Initialize(initialSettings);
            _viewModel.ExitRequested += (_, _) => ExitApplication();
            _viewModel.PreferencesChanged += OnPreferencesChanged;
            _viewModel.BatteryStatusChanged += (_, args) => _tray?.SetBatteryStatus(args.MinimumBatteryPercent);
            _viewModel.DevicesRefreshed += OnDevicesRefreshed;
            _window = CreatePanelWindow();

            _monitor.ConnectionChanged += OnConnectionChanged;
            _monitor.DevicesInvalidated += OnDevicesInvalidated;
            await _monitor.StartAsync(_lifetime.Token);
            await SafeRefreshAsync(false, false, _lifetime.Token);
            _ = PollBatteryNotificationsAsync(_lifetime.Token);
            _singleInstance.StartServer(command => Dispatcher.BeginInvoke(() =>
            {
                if (command == SingleInstanceCoordinator.ExitCommand)
                {
                    _logger.Info("Exit command received from another instance.");
                    ExitApplication();
                }
                else
                {
                    _logger.Info("Show command received from another instance.");
                    ShowPanelFromExternalCommand();
                }
            }));
            if (e.Args.Contains("--show", StringComparer.OrdinalIgnoreCase))
                _ = Dispatcher.BeginInvoke(ShowPanelFromExternalCommand);
        }
        catch (Exception ex)
        {
            _logger.Error("Application startup failed.", ex);
            MessageBox.Show($"Bluetooth Notify: {ex.Message}", BluetoothNotify.App.Properties.Strings.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            ExitApplication();
        }
    }

    private TrayPanelWindow CreatePanelWindow()
    {
        var window = new TrayPanelWindow { DataContext = _viewModel };
        window.HideRequested += (_, _) => HidePanel();
        window.DeactivationRequested += (_, _) =>
        {
            if (_recreatingWindow) return;
            // Windows may reject foreground activation requested through IPC. Do not immediately
            // undo the explicit "show" command; subsequent outside clicks still deactivate normally.
            if (_panelWasActivated && DateTimeOffset.UtcNow - _shownAt >= TimeSpan.FromMilliseconds(750)) HidePanel();
        };
        window.LocationChanged += (_, _) => UpdatePanelBounds();
        window.SizeChanged += (_, _) =>
        {
            PositionPanel();
            UpdatePanelBounds();
        };
        return window;
    }

    private void RecreatePanelWindow()
    {
        if (_window is null) return;
        var wasVisible = _window.IsVisible;
        var activate = _panelWasActivated;
        _recreatingWindow = true;
        try
        {
            _window.CloseForExit();
            _window = CreatePanelWindow();
            if (!wasVisible) return;
            _window.ShowActivated = activate;
            PositionPanel();
            _window.Show();
            _window.UpdateLayout();
            PositionPanel();
            if (activate) { _window.Activate(); _window.Focus(); }
            UpdatePanelBounds();
            StartPolling();
        }
        finally { _recreatingWindow = false; }
    }

    private void OnPreferencesChanged(object? sender, PreferencesChangedEventArgs e)
    {
        var languageChanged = e.Language != _languagePreference;
        _languagePreference = e.Language;
        _themePreference = e.Theme;
        LocalizationService.Apply(_languagePreference);
        ThemeManager.Apply(_themePreference);
        _tray?.SetThemePreference(_themePreference);
        if (languageChanged) RecreatePanelWindow();
        else
        {
            _window?.UpdateLayout();
            PositionPanel();
            UpdatePanelBounds();
        }
    }

    private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        if (_themePreference != AppThemePreference.System || _exiting) return;
        _ = Dispatcher.BeginInvoke(() =>
        {
            ThemeManager.Apply(AppThemePreference.System);
            _tray?.SetThemePreference(AppThemePreference.System);
        });
    }

    private void WireTrayEvents()
    {
        if (_tray is null) return;
        _tray.OpenRequested += (_, _) => ShowPanelActivated();
        _tray.ToggleRequested += (_, _) => { if (_window?.IsVisible == true) HidePanel(); else ShowPanelActivated(); };
        _tray.RefreshRequested += async (_, _) =>
        {
            if (_viewModel is not null) await SafeRefreshAsync(true, false, _lifetime.Token);
        };
        _tray.SettingsRequested += (_, _) =>
        {
            _viewModel?.OpenSettings();
            ShowPanel(true, true);
        };
        _tray.ExitRequested += (_, _) => ExitApplication();
        _tray.PointerEntered += (_, _) => ShowPanelWithoutActivation();
        _tray.PointerLeft += (_, _) => { if (!_panelPinned) HidePanel(); };
    }

    private void ShowPanelActivated() => ShowPanel(true);
    private void ShowPanelWithoutActivation()
    {
        if (_window?.IsVisible == true && _panelPinned) return;
        ShowPanel(false);
    }
    private void ShowPanelFromExternalCommand() => ShowPanel(false, true);

    private void ShowPanel(bool activate, bool? pinned = null)
    {
        if (_window is null || _tray is null || _viewModel is null || _exiting) return;
        _logger.Info($"Showing panel (activate: {activate}).");
        _panelPinned = pinned ?? activate;
        _panelWasActivated = activate;
        _shownAt = DateTimeOffset.UtcNow;
        PositionPanel();
        _window.ShowActivated = activate;
        if (!_window.IsVisible) _window.Show();
        _window.UpdateLayout();
        PositionPanel();
        if (activate) { _window.Activate(); _window.Focus(); }
        else
        {
            var hwnd = new WindowInteropHelper(_window).EnsureHandle();
            if (!NativeMethods.SetWindowPos(
                    hwnd,
                    NativeMethods.HwndTopmost,
                    0,
                    0,
                    0,
                    0,
                    NativeMethods.SWP_NOMOVE |
                    NativeMethods.SWP_NOSIZE |
                    NativeMethods.SWP_NOACTIVATE |
                    NativeMethods.SWP_SHOWWINDOW))
                _logger.Warning("Hover panel could not be raised without activation.");
        }
        UpdatePanelBounds();
        StartPolling();
    }

    private void PositionPanel()
    {
        if (_window is null || _tray is null) return;
        var anchor = _tray.GetIconRect();
        var nativeAnchor = new NativeMethods.Rect { Left = anchor.Left, Top = anchor.Top, Right = anchor.Right, Bottom = anchor.Bottom };
        var monitorHandle = NativeMethods.MonitorFromRect(ref nativeAnchor, NativeMethods.MONITOR_DEFAULTTONEAREST);
        var info = new NativeMethods.MonitorInfo { cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf<NativeMethods.MonitorInfo>() };
        NativeMethods.GetMonitorInfo(monitorHandle, ref info);

        var hwnd = new WindowInteropHelper(_window).EnsureHandle();
        var dpiScale = Math.Max(1, NativeMethods.GetDpiForWindow(hwnd) / 96d);
        _window.Measure(new Size(_window.Width, double.PositiveInfinity));
        var measuredHeight = _window.IsVisible && _window.ActualHeight > 0 ? _window.ActualHeight : _window.DesiredSize.Height;
        var height = Math.Min(_window.MaxHeight, Math.Max(260, measuredHeight));
        var workArea = TaskbarWorkAreaService.Adjust(
            NativeMethods.ToNative(info.rcMonitor),
            NativeMethods.ToNative(info.rcWork),
            GetTaskbarRects(),
            anchor);
        var position = new PanelPlacementService().Calculate(anchor, workArea, _window.Width, height, dpiScale);
        _window.Left = position.LeftDip;
        _window.Top = position.TopDip;
    }

    private static IReadOnlyList<NativeRect> GetTaskbarRects()
    {
        var result = new List<NativeRect>();
        NativeMethods.EnumWindows((hwnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hwnd)) return true;
            var className = new StringBuilder(64);
            _ = NativeMethods.GetClassName(hwnd, className, className.Capacity);
            if (className.ToString() is not ("Shell_TrayWnd" or "Shell_SecondaryTrayWnd")) return true;
            if (NativeMethods.GetWindowRect(hwnd, out var rect)) result.Add(NativeMethods.ToNative(rect));
            return true;
        }, 0);
        return result;
    }

    private void UpdatePanelBounds()
    {
        if (_tray is null || _window is null || !_window.IsVisible) { _tray?.SetPanelBounds(null); return; }
        var dpi = VisualTreeHelper.GetDpi(_window);
        _tray.SetPanelBounds(new NativeRect(
            (int)Math.Round(_window.Left * dpi.DpiScaleX),
            (int)Math.Round(_window.Top * dpi.DpiScaleY),
            (int)Math.Round((_window.Left + _window.ActualWidth) * dpi.DpiScaleX),
            (int)Math.Round((_window.Top + _window.ActualHeight) * dpi.DpiScaleY)));
    }

    private void StartPolling()
    {
        _polling?.Cancel();
        _polling?.Dispose();
        _polling = CancellationTokenSource.CreateLinkedTokenSource(_lifetime.Token);
        _ = PollWhileVisibleAsync(_polling.Token);
    }

    private async Task PollWhileVisibleAsync(CancellationToken token)
    {
        if (_viewModel is null) return;
        try
        {
            await SafeRefreshAsync(false, false, token);
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            while (_window?.IsVisible == true && await timer.WaitForNextTickAsync(token))
                await SafeRefreshAsync(false, true, token);
        }
        catch (OperationCanceledException) { }
    }

    private async Task PollBatteryNotificationsAsync(CancellationToken token)
    {
        try
        {
            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
            while (await timer.WaitForNextTickAsync(token))
                await SafeRefreshAsync(false, true, token);
        }
        catch (OperationCanceledException) when (token.IsCancellationRequested) { }
    }

    private async Task SafeRefreshAsync(bool force, bool skipIfBusy, CancellationToken token)
    {
        try { if (_viewModel is not null) await _viewModel.RefreshAsync(force, skipIfBusy, token); }
        catch (OperationCanceledException) when (token.IsCancellationRequested) { }
        catch (Exception ex) { _logger.Warning("Refresh failed.", ex); }
    }

    private void OnDevicesInvalidated(object? sender, EventArgs e)
    {
        if (_exiting) return;
        _ = Dispatcher.BeginInvoke(() =>
        {
            _invalidationRefresh?.Cancel();
            _invalidationRefresh?.Dispose();
            _invalidationRefresh = CancellationTokenSource.CreateLinkedTokenSource(_lifetime.Token);
            _ = RefreshAfterInvalidationAsync(_invalidationRefresh.Token);
        });
    }

    private async Task RefreshAfterInvalidationAsync(CancellationToken token)
    {
        try
        {
            await Task.Delay(TimeSpan.FromMilliseconds(300), token);
            await SafeRefreshAsync(false, true, token);
        }
        catch (OperationCanceledException) when (token.IsCancellationRequested) { }
    }

    private async void OnConnectionChanged(object? sender, DeviceConnectionChangedEventArgs e)
    {
        try
        {
            if (_viewModel?.NotificationsEnabled != true || e.IsInitialSnapshot || _notifications is null) return;

            var notificationDevice = e.Device;
            if (_monitor is not null)
            {
                try
                {
                    var snapshot = await _monitor.RefreshAsync(true, _lifetime.Token);
                    notificationDevice = snapshot.Devices.FirstOrDefault(device =>
                        device.StableId.Equals(e.Device.StableId, StringComparison.OrdinalIgnoreCase)) ?? e.Device;
                }
                catch (OperationCanceledException) when (_lifetime.IsCancellationRequested) { throw; }
                catch (Exception ex) { _logger.Warning("Battery refresh before connection notification failed.", ex); }
            }
            await _notifications.ShowConnectedAsync(notificationDevice, _lifetime.Token);
        }
        catch (OperationCanceledException) when (_lifetime.IsCancellationRequested) { }
        catch (Exception ex) { _logger.Warning("Connection notification failed.", ex); }
    }

    private async void OnDevicesRefreshed(object? sender, DevicesRefreshedEventArgs e)
    {
        try
        {
            if (_viewModel is null) return;
            var options = new BatteryNotificationOptions(
                _viewModel.MediumBatteryNotificationEnabled,
                _viewModel.MediumBatteryThresholdPercent,
                _viewModel.LowBatteryNotificationEnabled,
                _viewModel.LowBatteryThresholdPercent);
            var alerts = _batteryNotificationTracker.Observe(e.Devices, options);
            if (!_viewModel.NotificationsEnabled || _notifications is null) return;

            foreach (var alert in alerts)
                await _notifications.ShowBatteryLevelAsync(alert, _lifetime.Token);
        }
        catch (OperationCanceledException) when (_lifetime.IsCancellationRequested) { }
        catch (Exception ex) { _logger.Warning("Battery threshold notification failed.", ex); }
    }

    private void HidePanel()
    {
        _panelPinned = false;
        _panelWasActivated = false;
        _polling?.Cancel();
        _polling?.Dispose();
        _polling = null;
        _window?.Hide();
        _viewModel?.CloseSettings();
        _tray?.SetPanelBounds(null);
    }

    private async void ExitApplication()
    {
        if (_exiting) return;
        _exiting = true;
        _lifetime.Cancel();
        _invalidationRefresh?.Cancel();
        _invalidationRefresh?.Dispose();
        _invalidationRefresh = null;
        HidePanel();
        _tray?.Dispose();
        _tray = null;
        _window?.CloseForExit();
        if (_monitor is not null) await _monitor.DisposeAsync();
        if (_notifications is not null) await _notifications.DisposeAsync();
        _battery?.Dispose();
        _singleInstance?.Dispose();
        SystemEvents.UserPreferenceChanged -= OnUserPreferenceChanged;
        _logger.Info("Application stopped.");
        Shutdown();
    }

    private void RegisterExceptionHandlers()
    {
        DispatcherUnhandledException += (_, args) => { _logger.Error("Unhandled dispatcher exception.", args.Exception); };
        AppDomain.CurrentDomain.UnhandledException += (_, args) => _logger.Error("Unhandled application exception.", args.ExceptionObject as Exception);
        TaskScheduler.UnobservedTaskException += (_, args) => { _logger.Error("Unobserved task exception.", args.Exception); args.SetObserved(); };
    }
}
