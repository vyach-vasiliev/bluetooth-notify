using BluetoothNotify.App.Models;

namespace BluetoothNotify.App.Services;

public sealed class ConnectionTransitionTracker(TimeSpan? debounce = null)
{
    private readonly TimeSpan _debounce = debounce ?? TimeSpan.FromSeconds(3);
    private readonly Dictionary<string, bool> _states = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, DateTimeOffset> _lastNotifications = new(StringComparer.OrdinalIgnoreCase);
    private bool _initialized;

    public void Seed(IEnumerable<BluetoothDeviceState> devices)
    {
        _states.Clear();
        foreach (var device in devices) _states[device.StableId] = device.IsConnected;
        _initialized = true;
    }

    public bool Observe(BluetoothDeviceState device, DateTimeOffset now)
    {
        if (!_initialized)
        {
            _states[device.StableId] = device.IsConnected;
            return false;
        }

        var wasConnected = _states.GetValueOrDefault(device.StableId);
        _states[device.StableId] = device.IsConnected;
        if (wasConnected || !device.IsConnected) return false;
        if (_lastNotifications.TryGetValue(device.StableId, out var last) && now - last < _debounce) return false;
        _lastNotifications[device.StableId] = now;
        return true;
    }
}
