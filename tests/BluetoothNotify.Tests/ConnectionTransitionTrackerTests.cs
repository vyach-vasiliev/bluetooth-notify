using BluetoothNotify.App.Models;
using BluetoothNotify.App.Services;

namespace BluetoothNotify.Tests;

public sealed class ConnectionTransitionTrackerTests
{
    [Fact]
    public void InitialSnapshot_DoesNotNotify()
    {
        var tracker = new ConnectionTransitionTracker();
        var connected = Device(true);
        Assert.False(tracker.Observe(connected, DateTimeOffset.UtcNow));
        tracker.Seed([connected]);
        Assert.False(tracker.Observe(connected, DateTimeOffset.UtcNow.AddSeconds(1)));
    }

    [Fact]
    public void DisconnectThenConnect_NotifiesOnce()
    {
        var now = DateTimeOffset.UtcNow;
        var tracker = new ConnectionTransitionTracker();
        tracker.Seed([Device(false)]);

        Assert.True(tracker.Observe(Device(true), now));
        Assert.False(tracker.Observe(Device(true), now.AddSeconds(1)));
        Assert.False(tracker.Observe(Device(false), now.AddSeconds(2)));
        Assert.True(tracker.Observe(Device(true), now.AddSeconds(4)));
    }

    [Fact]
    public void Debounce_SuppressesRapidDuplicateTransition()
    {
        var now = DateTimeOffset.UtcNow;
        var tracker = new ConnectionTransitionTracker(TimeSpan.FromSeconds(3));
        tracker.Seed([Device(false)]);
        Assert.True(tracker.Observe(Device(true), now));
        tracker.Observe(Device(false), now.AddMilliseconds(100));
        Assert.False(tracker.Observe(Device(true), now.AddSeconds(1)));
    }

    private static BluetoothDeviceState Device(bool connected) => new(
        "device:1", "Device", true, connected, BluetoothTransport.LowEnergy, BluetoothDeviceKind.Mouse,
        null, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, DeviceDataState.Unavailable, ["endpoint"]);
}
