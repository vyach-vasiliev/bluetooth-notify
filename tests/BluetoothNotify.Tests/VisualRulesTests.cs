using BluetoothNotify.App.Models;
using BluetoothNotify.App.Services;
using BluetoothNotify.App.ViewModels;

namespace BluetoothNotify.Tests;

public sealed class VisualRulesTests
{
    [Theory]
    [InlineData(null, BatteryLevelCategory.Unknown)]
    [InlineData(0, BatteryLevelCategory.Critical)]
    [InlineData(15, BatteryLevelCategory.Critical)]
    [InlineData(16, BatteryLevelCategory.Low)]
    [InlineData(29, BatteryLevelCategory.Low)]
    [InlineData(30, BatteryLevelCategory.Normal)]
    [InlineData(100, BatteryLevelCategory.Normal)]
    public void BatteryThresholds_AreCorrect(int? value, BatteryLevelCategory expected) =>
        Assert.Equal(expected, BatteryLevelClassifier.GetCategory(value));

    [Theory]
    [InlineData(BluetoothDeviceKind.Mouse)]
    [InlineData(BluetoothDeviceKind.Keyboard)]
    [InlineData(BluetoothDeviceKind.Headphones)]
    [InlineData(BluetoothDeviceKind.Speaker)]
    [InlineData(BluetoothDeviceKind.Gamepad)]
    [InlineData(BluetoothDeviceKind.Phone)]
    [InlineData(BluetoothDeviceKind.Pen)]
    [InlineData(BluetoothDeviceKind.Wearable)]
    [InlineData(BluetoothDeviceKind.Unknown)]
    public void IconResolver_ReturnsGlyph(BluetoothDeviceKind kind) =>
        Assert.False(string.IsNullOrWhiteSpace(new DeviceIconResolver().Resolve(kind)));

    [Fact]
    public void KindInference_PrefersMetadataOverMisleadingName() =>
        Assert.Equal(BluetoothDeviceKind.Keyboard, DeviceIconResolver.Infer(null, ["Keyboard"], "Office Mouse"));

    [Fact]
    public void DisconnectedBattery_UsesGrayCategoryAndExplainsLastKnownValue()
    {
        var state = new BluetoothDeviceState(
            "device", "Headset", true, false, BluetoothTransport.Classic, BluetoothDeviceKind.Headphones,
            64, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, DeviceDataState.Stale, ["endpoint"]);
        var viewModel = new BluetoothDeviceViewModel(state, new DeviceIconResolver());

        Assert.Equal(BatteryLevelCategory.Unknown, viewModel.BatteryCategory);
        Assert.Contains("64%", viewModel.BatteryDescription);
        Assert.Contains("последний известный", viewModel.BatteryDescription, StringComparison.CurrentCultureIgnoreCase);
    }

    [Theory]
    [InlineData(true, "\uF2A3")]
    [InlineData(false, "\uF285")]
    public void NotificationButton_UsesStateSpecificFluentGlyph(bool enabled, string expected) =>
        Assert.Equal(expected, TrayPanelViewModel.GetNotificationsIconGlyph(enabled));

    [Theory]
    [InlineData(15, "🔴")]
    [InlineData(16, "🟡")]
    [InlineData(29, "🟡")]
    [InlineData(30, "🟢")]
    [InlineData(100, "🟢")]
    public void NotificationBatteryIndicator_UsesBatteryThresholds(int battery, string expected) =>
        Assert.Equal(expected, NotificationService.GetBatteryIndicator(battery));

    [Fact]
    public void ConnectionNotification_ContainsTitleBatteryIndicatorAndAppLogo()
    {
        var notification = NotificationService.BuildConnectedNotification(new BluetoothDeviceState(
            "device", "Test Headset", true, true, BluetoothTransport.Classic, BluetoothDeviceKind.Headphones,
            22, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, DeviceDataState.Fresh, ["endpoint"]));

        Assert.Contains("<text>Test Headset</text>", notification.Payload);
        Assert.Contains("🟡", notification.Payload);
        Assert.Contains("placement='appLogoOverride'", notification.Payload);
        Assert.Contains("BluetoothNotify.Notification.png", notification.Payload);
    }

    [Theory]
    [InlineData(BatteryNotificationLevel.Medium, "🟡")]
    [InlineData(BatteryNotificationLevel.Low, "🔴")]
    public void ThresholdNotification_ContainsDeviceBatteryAndSeverity(BatteryNotificationLevel level, string indicator)
    {
        var device = new BluetoothDeviceState(
            "device", "Test Headset", true, true, BluetoothTransport.Classic, BluetoothDeviceKind.Headphones,
            12, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, DeviceDataState.Fresh, ["endpoint"]);

        var notification = NotificationService.BuildBatteryLevelNotification(new BatteryNotificationAlert(device, level, 15));

        Assert.Contains("<text>Test Headset</text>", notification.Payload);
        Assert.Contains(indicator, notification.Payload);
        Assert.Contains("12%", notification.Payload);
        Assert.Contains("BluetoothNotify.Notification.png", notification.Payload);
    }

    [Fact]
    public void BatterySummary_ExcludesDisconnectedDevices()
    {
        var devices = new[]
        {
            ViewModel("Connected", true, 44),
            ViewModel("Disconnected", false, 90),
            ViewModel("Unknown", true, null)
        };

        Assert.Equal(1, TrayPanelViewModel.CountConnectedBatteryDevices(devices));
        Assert.Equal(44, TrayPanelViewModel.GetMinimumConnectedBattery(devices));
    }

    private static BluetoothDeviceViewModel ViewModel(string name, bool connected, int? battery) => new(
        new BluetoothDeviceState(
            name, name, true, connected, BluetoothTransport.Classic, BluetoothDeviceKind.Headphones,
            battery, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow,
            connected ? DeviceDataState.Fresh : DeviceDataState.Stale, [name]),
        new DeviceIconResolver());
}
