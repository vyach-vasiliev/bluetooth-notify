using BluetoothNotify.App.Models;

namespace BluetoothNotify.App.Services;

public interface IBluetoothDeviceMonitor : IAsyncDisposable
{
    event EventHandler<DeviceConnectionChangedEventArgs>? ConnectionChanged;
    event EventHandler? DevicesInvalidated;
    Task StartAsync(CancellationToken cancellationToken);
    Task<MonitorSnapshot> RefreshAsync(bool forceBatteryRead, CancellationToken cancellationToken);
}

public sealed class DeviceConnectionChangedEventArgs(BluetoothDeviceState device, bool isInitialSnapshot) : EventArgs
{
    public BluetoothDeviceState Device { get; } = device;
    public bool IsInitialSnapshot { get; } = isInitialSnapshot;
}

public interface IBatteryReader
{
    Task<BatteryReading> ReadAsync(BluetoothEndpointSnapshot endpoint, bool force, CancellationToken cancellationToken);
}

public interface IPnpBatteryReader
{
    int? TryRead(ulong? bluetoothAddress, string? containerId);
}

public interface INotificationService : IAsyncDisposable
{
    void Initialize();
    Task ShowConnectedAsync(BluetoothDeviceState device, CancellationToken cancellationToken = default);
    Task ShowBatteryLevelAsync(BatteryNotificationAlert alert, CancellationToken cancellationToken = default);
}

public interface ISettingsStore
{
    Task<AppSettings> LoadAsync(CancellationToken cancellationToken = default);
    Task SaveAsync(AppSettings settings, CancellationToken cancellationToken = default);
}

public interface ITrayIconService : IDisposable
{
    event EventHandler? OpenRequested;
    event EventHandler? ToggleRequested;
    event EventHandler? SettingsRequested;
    event EventHandler? ExitRequested;
    event EventHandler? PointerEntered;
    event EventHandler? PointerLeft;
    NativeRect GetIconRect();
    void Initialize();
    void SetBatteryStatus(int? minimumBatteryPercent);
    void SetPanelBounds(NativeRect? bounds);
    void SetThemePreference(AppThemePreference preference);
}

public interface IDeviceIconResolver
{
    string Resolve(BluetoothDeviceKind kind);
}

public interface IPanelPlacementService
{
    PanelPosition Calculate(NativeRect anchorPx, NativeRect workAreaPx, double panelWidthDip, double panelHeightDip, double dpiScale);
}

public readonly record struct NativeRect(int Left, int Top, int Right, int Bottom)
{
    public int Width => Right - Left;
    public int Height => Bottom - Top;
    public bool Contains(int x, int y) => x >= Left && x < Right && y >= Top && y < Bottom;
    public NativeRect Inflate(int amount) => new(Left - amount, Top - amount, Right + amount, Bottom + amount);
}

public readonly record struct PanelPosition(double LeftDip, double TopDip);
