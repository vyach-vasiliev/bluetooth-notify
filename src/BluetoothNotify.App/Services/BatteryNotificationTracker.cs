using BluetoothNotify.App.Models;

namespace BluetoothNotify.App.Services;

public enum BatteryNotificationLevel
{
    Medium,
    Low
}

public sealed record BatteryNotificationAlert(
    BluetoothDeviceState Device,
    BatteryNotificationLevel Level,
    int ThresholdPercent);

public sealed record BatteryNotificationOptions(
    bool MediumEnabled,
    int MediumThresholdPercent,
    bool LowEnabled,
    int LowThresholdPercent);

public sealed class BatteryNotificationTracker
{
    private readonly Dictionary<string, Observation> _observations = new(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyList<BatteryNotificationAlert> Observe(
        IEnumerable<BluetoothDeviceState> devices,
        BatteryNotificationOptions options)
    {
        var alerts = new List<BatteryNotificationAlert>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var device in devices)
        {
            seen.Add(device.StableId);
            if (!device.IsConnected)
            {
                if (_observations.TryGetValue(device.StableId, out var disconnected))
                    _observations[device.StableId] = disconnected with { IsConnected = false };
                continue;
            }

            if (device.BatteryPercent is not { } current || device.DataState != DeviceDataState.Fresh)
                continue;

            if (!_observations.TryGetValue(device.StableId, out var previous) || !previous.IsConnected)
            {
                _observations[device.StableId] = new Observation(current, true);
                continue;
            }

            var crossedLow = options.LowEnabled &&
                previous.Percent > options.LowThresholdPercent && current <= options.LowThresholdPercent;
            var crossedMedium = options.MediumEnabled &&
                previous.Percent > options.MediumThresholdPercent && current <= options.MediumThresholdPercent;

            if (crossedLow)
                alerts.Add(new BatteryNotificationAlert(device, BatteryNotificationLevel.Low, options.LowThresholdPercent));
            else if (crossedMedium)
                alerts.Add(new BatteryNotificationAlert(device, BatteryNotificationLevel.Medium, options.MediumThresholdPercent));

            _observations[device.StableId] = new Observation(current, true);
        }

        foreach (var id in _observations.Keys.Where(id => !seen.Contains(id)).ToArray())
            _observations[id] = _observations[id] with { IsConnected = false };

        return alerts;
    }

    private sealed record Observation(int Percent, bool IsConnected);
}
