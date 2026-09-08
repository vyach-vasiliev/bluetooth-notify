using BluetoothNotify.App.Models;

namespace BluetoothNotify.App.Services;

public static class DeviceStateMerger
{
    public static string CreateStableId(BluetoothEndpointSnapshot endpoint)
    {
        if (!string.IsNullOrWhiteSpace(endpoint.ContainerId))
            return $"container:{endpoint.ContainerId.Trim().Trim('{', '}').ToUpperInvariant()}";
        if (endpoint.Address is { } address)
            return $"address:{address:X12}";
        return $"endpoint:{endpoint.EndpointId.Trim().ToUpperInvariant()}";
    }

    public static IReadOnlyList<BluetoothDeviceState> Merge(
        IEnumerable<BluetoothEndpointSnapshot> endpoints,
        IReadOnlyDictionary<string, BatteryReading>? batteries = null,
        DateTimeOffset? now = null,
        IReadOnlyDictionary<string, BluetoothDeviceState>? previous = null)
    {
        var timestamp = now ?? DateTimeOffset.UtcNow;
        var result = new List<BluetoothDeviceState>();

        foreach (var group in endpoints.Where(x => x.IsPaired).GroupBy(CreateStableId, StringComparer.OrdinalIgnoreCase))
        {
            var entries = group.ToList();
            var name = entries.Select(x => x.Name?.Trim()).FirstOrDefault(x => !string.IsNullOrWhiteSpace(x))
                ?? Properties.Strings.DeviceFallbackName;
            var old = previous?.GetValueOrDefault(group.Key);
            var battery = entries.Select(x => batteries?.GetValueOrDefault(x.EndpointId)).FirstOrDefault(x => x?.Percent is not null)
                ?? entries.Select(x => batteries?.GetValueOrDefault(x.EndpointId)).FirstOrDefault(x => x is not null);

            int? percent = battery?.Percent ?? old?.BatteryPercent;
            var dataState = battery?.Percent is not null
                ? battery.State
                : old?.BatteryPercent is not null
                    ? timestamp - old.LastUpdatedAt > TimeSpan.FromSeconds(60) ? DeviceDataState.Stale : old.DataState
                    : battery?.State ?? DeviceDataState.Unavailable;
            var lastUpdated = battery?.Percent is not null ? timestamp : old?.LastUpdatedAt ?? timestamp;

            result.Add(new BluetoothDeviceState(
                group.Key,
                name,
                entries.All(x => x.IsPaired),
                entries.Any(x => x.IsConnected),
                entries.Aggregate(BluetoothTransport.None, (value, x) => value | x.Transport),
                entries.Select(x => x.Kind).FirstOrDefault(x => x != BluetoothDeviceKind.Unknown),
                percent is >= 0 and <= 100 ? percent : null,
                lastUpdated,
                timestamp,
                dataState,
                entries.Select(x => x.EndpointId).Distinct(StringComparer.OrdinalIgnoreCase).ToArray()));
        }

        return result
            .OrderByDescending(x => x.IsConnected)
            .ThenBy(x => x.Name, StringComparer.CurrentCultureIgnoreCase)
            .ToArray();
    }
}
