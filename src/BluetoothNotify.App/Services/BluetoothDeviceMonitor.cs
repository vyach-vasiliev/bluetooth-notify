using System.Collections.Concurrent;
using System.Runtime.InteropServices.WindowsRuntime;
using BluetoothNotify.App.Models;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;
using Windows.Devices.Radios;

namespace BluetoothNotify.App.Services;

public sealed class BluetoothDeviceMonitor(IBatteryReader batteryReader, AppLogger logger) : IBluetoothDeviceMonitor
{
    private static readonly string[] RequestedProperties =
    [
        "System.Devices.Aep.IsConnected",
        "System.Devices.Aep.ContainerId",
        "System.Devices.Aep.DeviceAddress",
        "System.Devices.Aep.Category",
        "System.Devices.ClassGuid",
        "System.ItemNameDisplay",
        "System.Devices.BatteryLife"
    ];

    private readonly ConcurrentDictionary<string, BluetoothEndpointSnapshot> _endpoints = new(StringComparer.OrdinalIgnoreCase);
    private readonly SemaphoreSlim _refreshLock = new(1, 1);
    private readonly ConnectionTransitionTracker _transitions = new();
    private readonly object _transitionSync = new();
    private DeviceWatcher? _leWatcher;
    private DeviceWatcher? _classicWatcher;
    private CancellationTokenSource? _lifetime;
    private int _completedWatchers;
    private bool _initialSnapshotCompleted;
    private IReadOnlyDictionary<string, BluetoothDeviceState> _lastDevices = new Dictionary<string, BluetoothDeviceState>();

    public event EventHandler<DeviceConnectionChangedEventArgs>? ConnectionChanged;
    public event EventHandler? DevicesInvalidated;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (_lifetime is not null) return Task.CompletedTask;
        _lifetime = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        _leWatcher = CreateWatcher(BluetoothLEDevice.GetDeviceSelectorFromPairingState(true), BluetoothTransport.LowEnergy);
        _classicWatcher = CreateWatcher(BluetoothDevice.GetDeviceSelectorFromPairingState(true), BluetoothTransport.Classic);
        _leWatcher.Start();
        _classicWatcher.Start();
        return Task.CompletedTask;
    }

    private DeviceWatcher CreateWatcher(string selector, BluetoothTransport transport)
    {
        var watcher = DeviceInformation.CreateWatcher(selector, RequestedProperties, DeviceInformationKind.AssociationEndpoint);
        watcher.Added += (_, info) => HandleAdded(info, transport);
        watcher.Updated += (sender, update) => _ = HandleUpdatedAsync(update.Id, transport);
        watcher.Removed += (_, update) => HandleRemoved(update.Id);
        watcher.EnumerationCompleted += (_, _) => HandleEnumerationCompleted();
        watcher.Stopped += (_, _) => DevicesInvalidated?.Invoke(this, EventArgs.Empty);
        return watcher;
    }

    private void HandleAdded(DeviceInformation info, BluetoothTransport transport)
    {
        _endpoints[info.Id] = Map(info, transport);
        EvaluateTransitions();
        DevicesInvalidated?.Invoke(this, EventArgs.Empty);
    }

    private async Task HandleUpdatedAsync(string id, BluetoothTransport transport)
    {
        try
        {
            var token = _lifetime?.Token ?? CancellationToken.None;
            var info = await DeviceInformation.CreateFromIdAsync(id, RequestedProperties, DeviceInformationKind.AssociationEndpoint)
                .AsTask(token).ConfigureAwait(false);
            if (info is not null) _endpoints[id] = Map(info, transport);
            EvaluateTransitions();
            DevicesInvalidated?.Invoke(this, EventArgs.Empty);
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) { logger.Warning("Bluetooth watcher update failed.", ex); }
    }

    private void HandleRemoved(string id)
    {
        if (_endpoints.TryRemove(id, out var removed) && _initialSnapshotCompleted)
        {
            var stableId = DeviceStateMerger.CreateStableId(removed);
            if (_lastDevices.TryGetValue(stableId, out var old) && old.IsConnected)
                _transitions.Observe(old with { IsConnected = false }, DateTimeOffset.UtcNow);
        }
        DevicesInvalidated?.Invoke(this, EventArgs.Empty);
    }

    private void HandleEnumerationCompleted()
    {
        if (Interlocked.Increment(ref _completedWatchers) < 2) return;
        lock (_transitionSync)
        {
            var devices = DeviceStateMerger.Merge(_endpoints.Values);
            _lastDevices = devices.ToDictionary(x => x.StableId, StringComparer.OrdinalIgnoreCase);
            _transitions.Seed(devices);
            _initialSnapshotCompleted = true;
        }
        DevicesInvalidated?.Invoke(this, EventArgs.Empty);
    }

    private void EvaluateTransitions()
    {
        List<BluetoothDeviceState> connected = [];
        lock (_transitionSync)
        {
            if (!_initialSnapshotCompleted) return;
            var devices = DeviceStateMerger.Merge(_endpoints.Values, previous: _lastDevices);
            _lastDevices = devices.ToDictionary(x => x.StableId, StringComparer.OrdinalIgnoreCase);
            connected.AddRange(devices.Where(device => _transitions.Observe(device, DateTimeOffset.UtcNow)));
        }
        foreach (var device in connected) ConnectionChanged?.Invoke(this, new DeviceConnectionChangedEventArgs(device, false));
    }

    public async Task<MonitorSnapshot> RefreshAsync(bool forceBatteryRead, CancellationToken cancellationToken)
    {
        await _refreshLock.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            BluetoothAdapter? adapter;
            try { adapter = await BluetoothAdapter.GetDefaultAsync().AsTask(cancellationToken).ConfigureAwait(false); }
            catch (Exception ex)
            {
                logger.Warning("Bluetooth adapter query failed.", ex);
                return new MonitorSnapshot(_lastDevices.Values.ToArray(), false, Properties.Strings.BluetoothUnavailable, DateTimeOffset.Now);
            }
            if (adapter is null)
                return new MonitorSnapshot(_lastDevices.Values.ToArray(), false, Properties.Strings.BluetoothUnavailable, DateTimeOffset.Now);
            try
            {
                var radio = await adapter.GetRadioAsync().AsTask(cancellationToken).ConfigureAwait(false);
                if (radio.State != RadioState.On)
                    return new MonitorSnapshot(_lastDevices.Values.ToArray(), false, Properties.Strings.BluetoothUnavailable, DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                logger.Warning("Bluetooth radio state query failed.", ex);
            }

            var found = new List<BluetoothEndpointSnapshot>();
            var warning = default(string);
            await EnumerateAsync(BluetoothLEDevice.GetDeviceSelectorFromPairingState(true), BluetoothTransport.LowEnergy, found, cancellationToken).ConfigureAwait(false);
            await EnumerateAsync(BluetoothDevice.GetDeviceSelectorFromPairingState(true), BluetoothTransport.Classic, found, cancellationToken).ConfigureAwait(false);
            foreach (var endpoint in found) _endpoints[endpoint.EndpointId] = endpoint;
            var liveIds = found.Select(x => x.EndpointId).ToHashSet(StringComparer.OrdinalIgnoreCase);
            foreach (var existing in _endpoints.Keys.Where(x => !liveIds.Contains(x))) _endpoints.TryRemove(existing, out _);

            // Windows may retain a last-known battery value for disconnected paired devices.
            // BatteryReader limits disconnected reads to endpoint/PnP properties and never opens GATT.
            var batteryTasks = found.ToDictionary(
                x => x.EndpointId,
                x => batteryReader.ReadAsync(x, forceBatteryRead, cancellationToken),
                StringComparer.OrdinalIgnoreCase);
            var batteries = new Dictionary<string, BatteryReading>(StringComparer.OrdinalIgnoreCase);
            foreach (var (id, task) in batteryTasks)
            {
                try
                {
                    var reading = await task.ConfigureAwait(false);
                    batteries[id] = reading;
                    warning ??= reading.Warning;
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { throw; }
                catch (Exception ex)
                {
                    logger.Warning("A device battery task failed.", ex);
                    batteries[id] = new BatteryReading(null, DeviceDataState.Error, Properties.Strings.PartialRefreshWarning);
                    warning ??= Properties.Strings.PartialRefreshWarning;
                }
            }

            var devices = DeviceStateMerger.Merge(found, batteries, previous: _lastDevices);
            lock (_transitionSync) _lastDevices = devices.ToDictionary(x => x.StableId, StringComparer.OrdinalIgnoreCase);
            return new MonitorSnapshot(devices, true, warning, DateTimeOffset.Now);
        }
        finally { _refreshLock.Release(); }
    }

    private static async Task EnumerateAsync(string selector, BluetoothTransport transport, List<BluetoothEndpointSnapshot> target, CancellationToken token)
    {
        var items = await DeviceInformation.FindAllAsync(selector, RequestedProperties, DeviceInformationKind.AssociationEndpoint)
            .AsTask(token).ConfigureAwait(false);
        target.AddRange(items.Select(x => Map(x, transport)));
    }

    private static BluetoothEndpointSnapshot Map(DeviceInformation info, BluetoothTransport transport)
    {
        var container = ReadString(info, "System.Devices.Aep.ContainerId");
        var address = ParseAddress(ReadString(info, "System.Devices.Aep.DeviceAddress"));
        var connected = ReadBool(info, "System.Devices.Aep.IsConnected");
        var classId = ReadString(info, "System.Devices.ClassGuid");
        var categories = ReadStrings(info, "System.Devices.Aep.Category");
        var batteryPercent = ReadPercent(info, "System.Devices.BatteryLife");
        return new BluetoothEndpointSnapshot(info.Id, container, address, info.Name, info.Pairing.IsPaired, connected, transport,
            DeviceIconResolver.Infer(classId, categories, info.Name), classId, batteryPercent);
    }

    private static object? Read(DeviceInformation info, string key) => info.Properties.TryGetValue(key, out var value) ? value : null;
    private static string? ReadString(DeviceInformation info, string key) => Read(info, key)?.ToString();
    private static bool ReadBool(DeviceInformation info, string key) => Read(info, key) is bool value && value;
    private static int? ReadPercent(DeviceInformation info, string key)
    {
        var value = Read(info, key);
        try
        {
            var percent = value is null ? -1 : Convert.ToInt32(value, System.Globalization.CultureInfo.InvariantCulture);
            return percent is >= 0 and <= 100 ? percent : null;
        }
        catch (Exception) when (value is not null) { return null; }
    }
    private static IEnumerable<string> ReadStrings(DeviceInformation info, string key) => Read(info, key) switch
    {
        string value => [value],
        string[] values => values,
        IEnumerable<string> values => values,
        _ => []
    };

    private static ulong? ParseAddress(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        var normalized = value.Replace(":", string.Empty).Replace("-", string.Empty);
        return ulong.TryParse(normalized, System.Globalization.NumberStyles.HexNumber, null, out var result) ? result : null;
    }

    public ValueTask DisposeAsync()
    {
        _lifetime?.Cancel();
        StopWatcher(_leWatcher);
        StopWatcher(_classicWatcher);
        _lifetime?.Dispose();
        _refreshLock.Dispose();
        return ValueTask.CompletedTask;
    }

    private static void StopWatcher(DeviceWatcher? watcher)
    {
        if (watcher is null) return;
        try
        {
            if (watcher.Status is DeviceWatcherStatus.Started or DeviceWatcherStatus.EnumerationCompleted) watcher.Stop();
        }
        catch { }
    }
}
