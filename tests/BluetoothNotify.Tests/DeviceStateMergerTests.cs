using BluetoothNotify.App.Models;
using BluetoothNotify.App.Services;

namespace BluetoothNotify.Tests;

public sealed class DeviceStateMergerTests
{
    [Fact]
    public void Merge_CombinesLeAndClassicByContainer()
    {
        var endpoints = new[]
        {
            Endpoint("le", "{AABBCCDD-0000-0000-0000-000000000001}", "Headset", true, BluetoothTransport.LowEnergy),
            Endpoint("classic", "aabbccdd-0000-0000-0000-000000000001", "Headset Stereo", false, BluetoothTransport.Classic)
        };

        var device = Assert.Single(DeviceStateMerger.Merge(endpoints));

        Assert.Equal(BluetoothTransport.LowEnergy | BluetoothTransport.Classic, device.Transport);
        Assert.True(device.IsConnected);
        Assert.Equal(2, device.EndpointIds.Count);
    }

    [Fact]
    public void StableId_PrefersContainerThenAddress()
    {
        var withContainer = Endpoint("le", "{abcd}", null, false, BluetoothTransport.LowEnergy) with { Address = 0x112233445566 };
        var withAddress = withContainer with { ContainerId = null };

        Assert.Equal("container:ABCD", DeviceStateMerger.CreateStableId(withContainer));
        Assert.Equal("address:112233445566", DeviceStateMerger.CreateStableId(withAddress));
    }

    [Fact]
    public void Merge_SortsConnectedFirstThenByName()
    {
        var endpoints = new[]
        {
            Endpoint("1", "1", "zeta", false, BluetoothTransport.Classic),
            Endpoint("2", "2", "beta", true, BluetoothTransport.Classic),
            Endpoint("3", "3", "Alpha", true, BluetoothTransport.LowEnergy)
        };

        var result = DeviceStateMerger.Merge(endpoints);

        Assert.Equal(["Alpha", "beta", "zeta"], result.Select(x => x.Name));
    }

    [Fact]
    public void Merge_UnknownBatteryRemainsNull()
    {
        var device = Assert.Single(DeviceStateMerger.Merge([Endpoint("1", "1", "Mouse", true, BluetoothTransport.LowEnergy)]));
        Assert.Null(device.BatteryPercent);
        Assert.Equal(DeviceDataState.Unavailable, device.DataState);
    }

    [Fact]
    public void Merge_PreservesLastBatteryAndMarksItStaleAfterSixtySeconds()
    {
        var endpoint = Endpoint("1", "1", "Mouse", true, BluetoothTransport.LowEnergy);
        var oldTime = DateTimeOffset.UtcNow.AddSeconds(-61);
        var old = new BluetoothDeviceState("container:1", "Mouse", true, true, BluetoothTransport.LowEnergy,
            BluetoothDeviceKind.Mouse, 72, oldTime, oldTime, DeviceDataState.Fresh, ["1"]);
        var result = Assert.Single(DeviceStateMerger.Merge([endpoint],
            new Dictionary<string, BatteryReading> { ["1"] = BatteryReading.Unavailable },
            DateTimeOffset.UtcNow,
            new Dictionary<string, BluetoothDeviceState> { [old.StableId] = old }));

        Assert.Equal(72, result.BatteryPercent);
        Assert.Equal(DeviceDataState.Stale, result.DataState);
    }

    private static BluetoothEndpointSnapshot Endpoint(string id, string? container, string? name, bool connected, BluetoothTransport transport) =>
        new(id, container, null, name, true, connected, transport, BluetoothDeviceKind.Unknown);
}
