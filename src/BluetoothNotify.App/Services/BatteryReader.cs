using System.Collections.Concurrent;
using System.Runtime.InteropServices.WindowsRuntime;
using BluetoothNotify.App.Models;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Storage.Streams;

namespace BluetoothNotify.App.Services;

public sealed class BatteryReader(AppLogger logger, IPnpBatteryReader? pnpBatteryReader = null) : IBatteryReader, IDisposable
{
    private static readonly Guid BatteryServiceUuid = GattServiceUuids.Battery;
    private static readonly Guid BatteryLevelUuid = GattCharacteristicUuids.BatteryLevel;
    private static readonly TimeSpan CacheTtl = TimeSpan.FromSeconds(20);
    private static readonly TimeSpan OperationTimeout = TimeSpan.FromSeconds(5);
    private readonly ConcurrentDictionary<string, CacheEntry> _cache = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, Lazy<Task<BatteryReading>>> _inflight = new(StringComparer.OrdinalIgnoreCase);
    private readonly SemaphoreSlim _parallelism = new(4, 4);
    private readonly IPnpBatteryReader _pnpBatteryReader = pnpBatteryReader ?? new PnpBatteryReader(logger);

    public Task<BatteryReading> ReadAsync(BluetoothEndpointSnapshot endpoint, bool force, CancellationToken cancellationToken)
    {
        if (endpoint.ReportedBatteryPercent is >= 0 and <= 100)
            return Task.FromResult(ForConnectionState(
                new BatteryReading(endpoint.ReportedBatteryPercent, DeviceDataState.Fresh), endpoint.IsConnected));

        if (!force && _cache.TryGetValue(endpoint.EndpointId, out var cached) && DateTimeOffset.UtcNow - cached.At < CacheTtl)
            return Task.FromResult(ForConnectionState(cached.Reading, endpoint.IsConnected));

        var operation = _inflight.GetOrAdd(endpoint.EndpointId, _ => new Lazy<Task<BatteryReading>>(
            () => ReadCoreAsync(endpoint, cancellationToken), LazyThreadSafetyMode.ExecutionAndPublication));
        return CompleteAsync(endpoint, operation.Value);
    }

    private async Task<BatteryReading> CompleteAsync(BluetoothEndpointSnapshot endpoint, Task<BatteryReading> task)
    {
        try
        {
            var result = await task.ConfigureAwait(false);
            _cache[endpoint.EndpointId] = new CacheEntry(result, DateTimeOffset.UtcNow);
            return ForConnectionState(result, endpoint.IsConnected);
        }
        finally { _inflight.TryRemove(endpoint.EndpointId, out _); }
    }

    private async Task<BatteryReading> ReadCoreAsync(BluetoothEndpointSnapshot endpoint, CancellationToken cancellationToken)
    {
        await _parallelism.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeout.CancelAfter(OperationTimeout);
            BluetoothLEDevice? device = null;
            try
            {
                if (_pnpBatteryReader.TryRead(endpoint.Address, endpoint.ContainerId) is { } pnpPercent)
                    return new BatteryReading(pnpPercent, DeviceDataState.Fresh);

                // Association endpoints can retain Windows' last-known battery while disconnected.
                // Do not fall back to GATT here: opening a GATT device may initiate a connection.
                if (!endpoint.IsConnected) return BatteryReading.Unavailable;

                if (endpoint.Transport.HasFlag(BluetoothTransport.LowEnergy))
                    device = await BluetoothLEDevice.FromIdAsync(endpoint.EndpointId).AsTask(timeout.Token).ConfigureAwait(false);
                if (device is null && endpoint.Address is { } address)
                    device = await BluetoothLEDevice.FromBluetoothAddressAsync(address).AsTask(timeout.Token).ConfigureAwait(false);
                if (device is null) return BatteryReading.Unavailable;
                var serviceResult = await device.GetGattServicesForUuidAsync(BatteryServiceUuid, BluetoothCacheMode.Uncached)
                    .AsTask(timeout.Token).ConfigureAwait(false);
                if (serviceResult.Status != GattCommunicationStatus.Success || serviceResult.Services.Count == 0)
                    return BatteryReading.Unavailable;

                foreach (var service in serviceResult.Services)
                {
                    using (service)
                    {
                        var characteristicResult = await service.GetCharacteristicsForUuidAsync(BatteryLevelUuid, BluetoothCacheMode.Uncached)
                            .AsTask(timeout.Token).ConfigureAwait(false);
                        if (characteristicResult.Status != GattCommunicationStatus.Success) continue;
                        foreach (var characteristic in characteristicResult.Characteristics)
                        {
                            var valueResult = await characteristic.ReadValueAsync(BluetoothCacheMode.Uncached)
                                .AsTask(timeout.Token).ConfigureAwait(false);
                            if (valueResult.Status != GattCommunicationStatus.Success || valueResult.Value.Length < 1) continue;
                            using var reader = DataReader.FromBuffer(valueResult.Value);
                            var value = reader.ReadByte();
                            if (value <= 100) return new BatteryReading(value, DeviceDataState.Fresh);
                        }
                    }
                }
                return BatteryReading.Unavailable;
            }
            catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
            {
                return new BatteryReading(null, DeviceDataState.Error, Properties.Strings.BatteryReadTimeout);
            }
            catch (Exception ex)
            {
                logger.Warning("Battery read failed.", ex);
                return new BatteryReading(null, DeviceDataState.Error, Properties.Strings.BatteryReadError);
            }
            finally { device?.Dispose(); }
        }
        finally { _parallelism.Release(); }
    }

    private static BatteryReading ForConnectionState(BatteryReading reading, bool isConnected) =>
        !isConnected && reading.Percent is not null
            ? reading with { State = DeviceDataState.Stale }
            : reading;

    public void Dispose() => _parallelism.Dispose();
    private sealed record CacheEntry(BatteryReading Reading, DateTimeOffset At);
}
