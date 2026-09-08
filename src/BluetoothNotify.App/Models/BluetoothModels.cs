namespace BluetoothNotify.App.Models;

[Flags]
public enum BluetoothTransport
{
    None = 0,
    LowEnergy = 1,
    Classic = 2
}

public enum DeviceDataState
{
    Fresh,
    Stale,
    Unavailable,
    Error
}

public enum BluetoothDeviceKind
{
    Unknown,
    Headphones,
    Mouse,
    Keyboard,
    Speaker,
    Gamepad,
    Phone,
    Pen,
    Wearable
}

public sealed record BluetoothEndpointSnapshot(
    string EndpointId,
    string? ContainerId,
    ulong? Address,
    string? Name,
    bool IsPaired,
    bool IsConnected,
    BluetoothTransport Transport,
    BluetoothDeviceKind Kind,
    string? ClassId = null,
    int? ReportedBatteryPercent = null);

public sealed record BluetoothDeviceState(
    string StableId,
    string Name,
    bool IsPaired,
    bool IsConnected,
    BluetoothTransport Transport,
    BluetoothDeviceKind Kind,
    int? BatteryPercent,
    DateTimeOffset LastUpdatedAt,
    DateTimeOffset LastSeenAt,
    DeviceDataState DataState,
    IReadOnlyList<string> EndpointIds)
{
    public bool HasBattery => BatteryPercent is >= 0 and <= 100;
}

public sealed record MonitorSnapshot(
    IReadOnlyList<BluetoothDeviceState> Devices,
    bool IsBluetoothAvailable,
    string? Warning,
    DateTimeOffset CompletedAt);

public sealed record BatteryReading(int? Percent, DeviceDataState State, string? Warning = null)
{
    public static BatteryReading Unavailable { get; } = new(null, DeviceDataState.Unavailable);
}
