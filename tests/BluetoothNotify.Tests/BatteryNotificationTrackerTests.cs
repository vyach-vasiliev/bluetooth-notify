using BluetoothNotify.App.Models;
using BluetoothNotify.App.Services;

namespace BluetoothNotify.Tests;

public sealed class BatteryNotificationTrackerTests
{
    private static readonly BatteryNotificationOptions Options = new(true, 30, true, 15);

    [Fact]
    public void FirstReading_SeedsStateWithoutNotification()
    {
        var tracker = new BatteryNotificationTracker();

        var alerts = tracker.Observe([Device(10)], Options);

        Assert.Empty(alerts);
    }

    [Fact]
    public void CrossingMediumThreshold_NotifiesOnce()
    {
        var tracker = new BatteryNotificationTracker();
        tracker.Observe([Device(31)], Options);

        var first = tracker.Observe([Device(30)], Options);
        var repeated = tracker.Observe([Device(29)], Options);

        var alert = Assert.Single(first);
        Assert.Equal(BatteryNotificationLevel.Medium, alert.Level);
        Assert.Equal(30, alert.ThresholdPercent);
        Assert.Empty(repeated);
    }

    [Fact]
    public void CrossingBothThresholds_OnlyNotifiesLow()
    {
        var tracker = new BatteryNotificationTracker();
        tracker.Observe([Device(60)], Options);

        var alerts = tracker.Observe([Device(10)], Options);

        Assert.Equal(BatteryNotificationLevel.Low, Assert.Single(alerts).Level);
    }

    [Fact]
    public void DisabledThreshold_DoesNotNotify()
    {
        var tracker = new BatteryNotificationTracker();
        tracker.Observe([Device(40)], Options);

        var alerts = tracker.Observe([Device(25)], Options with { MediumEnabled = false });

        Assert.Empty(alerts);
    }

    [Fact]
    public void Reconnection_SeedsStateWithoutDuplicateNotification()
    {
        var tracker = new BatteryNotificationTracker();
        tracker.Observe([Device(40)], Options);
        tracker.Observe([Device(40, connected: false)], Options);

        var alerts = tracker.Observe([Device(12)], Options);

        Assert.Empty(alerts);
    }

    private static BluetoothDeviceState Device(int battery, bool connected = true) => new(
        "device", "Test Headset", true, connected, BluetoothTransport.Classic, BluetoothDeviceKind.Headphones,
        battery, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, DeviceDataState.Fresh, ["endpoint"]);
}
