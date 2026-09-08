using BluetoothNotify.App.Models;
using BluetoothNotify.App.Services;

namespace BluetoothNotify.Tests;

public sealed class BatteryReaderTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(54)]
    [InlineData(100)]
    public async Task ReportedWindowsBattery_IsReturnedWithoutGattRead(int percent)
    {
        using var reader = new BatteryReader(new AppLogger(Path.Combine(Path.GetTempPath(), "BluetoothNotify.Tests")));
        var endpoint = new BluetoothEndpointSnapshot(
            "classic", "container", 0x112233445566, "Headset", true, true,
            BluetoothTransport.Classic, BluetoothDeviceKind.Headphones, ReportedBatteryPercent: percent);

        var result = await reader.ReadAsync(endpoint, false, CancellationToken.None);

        Assert.Equal(percent, result.Percent);
        Assert.Equal(DeviceDataState.Fresh, result.State);
    }

    [Fact]
    public async Task PnpHfpBattery_IsUsedWhenWindowsEndpointPropertyIsMissing()
    {
        using var reader = new BatteryReader(
            new AppLogger(Path.Combine(Path.GetTempPath(), "BluetoothNotify.Tests")),
            new StubPnpBatteryReader(90));
        var endpoint = new BluetoothEndpointSnapshot(
            "classic", "container", 0xAD0300007FD6, "Headset", true, true,
            BluetoothTransport.Classic, BluetoothDeviceKind.Headphones);

        var result = await reader.ReadAsync(endpoint, false, CancellationToken.None);

        Assert.Equal(90, result.Percent);
        Assert.Equal(DeviceDataState.Fresh, result.State);
    }

    [Fact]
    public async Task DisconnectedWindowsBattery_IsMarkedAsLastKnown()
    {
        using var reader = new BatteryReader(new AppLogger(Path.Combine(Path.GetTempPath(), "BluetoothNotify.Tests")));
        var endpoint = new BluetoothEndpointSnapshot(
            "classic", "container", 0x112233445566, "Headset", true, false,
            BluetoothTransport.Classic, BluetoothDeviceKind.Headphones, ReportedBatteryPercent: 72);

        var result = await reader.ReadAsync(endpoint, false, CancellationToken.None);

        Assert.Equal(72, result.Percent);
        Assert.Equal(DeviceDataState.Stale, result.State);
    }

    [Fact]
    public async Task DisconnectedPnpBattery_IsMarkedAsLastKnown()
    {
        using var reader = new BatteryReader(
            new AppLogger(Path.Combine(Path.GetTempPath(), "BluetoothNotify.Tests")),
            new StubPnpBatteryReader(41));
        var endpoint = new BluetoothEndpointSnapshot(
            "classic", "container", 0x112233445566, "Headset", true, false,
            BluetoothTransport.Classic, BluetoothDeviceKind.Headphones);

        var result = await reader.ReadAsync(endpoint, false, CancellationToken.None);

        Assert.Equal(41, result.Percent);
        Assert.Equal(DeviceDataState.Stale, result.State);
    }

    [Fact]
    public async Task DisconnectedDeviceWithoutWindowsBattery_DoesNotOpenGatt()
    {
        using var reader = new BatteryReader(
            new AppLogger(Path.Combine(Path.GetTempPath(), "BluetoothNotify.Tests")),
            new StubPnpBatteryReader(null));
        var endpoint = new BluetoothEndpointSnapshot(
            "classic", "container", 0x112233445566, "Headset", true, false,
            BluetoothTransport.Classic, BluetoothDeviceKind.Headphones);

        var result = await reader.ReadAsync(endpoint, true, CancellationToken.None);

        Assert.Null(result.Percent);
        Assert.Equal(DeviceDataState.Unavailable, result.State);
    }

    private sealed class StubPnpBatteryReader(int? value) : IPnpBatteryReader
    {
        public int? TryRead(ulong? bluetoothAddress, string? containerId) => value;
    }
}
