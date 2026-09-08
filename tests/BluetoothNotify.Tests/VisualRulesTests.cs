using BluetoothNotify.App.Models;
using BluetoothNotify.App.Services;
using BluetoothNotify.App.ViewModels;

namespace BluetoothNotify.Tests;

public sealed class VisualRulesTests
{
    [Theory]
    [InlineData(null, BatteryColorCategory.Unknown)]
    [InlineData(0, BatteryColorCategory.Critical)]
    [InlineData(15, BatteryColorCategory.Critical)]
    [InlineData(16, BatteryColorCategory.Low)]
    [InlineData(29, BatteryColorCategory.Low)]
    [InlineData(30, BatteryColorCategory.Normal)]
    [InlineData(100, BatteryColorCategory.Normal)]
    public void BatteryThresholds_AreCorrect(int? value, BatteryColorCategory expected) =>
        Assert.Equal(expected, BluetoothDeviceViewModel.GetBatteryCategory(value));

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

        Assert.Equal(BatteryColorCategory.Unknown, viewModel.BatteryCategory);
        Assert.Contains("64%", viewModel.BatteryDescription);
        Assert.Contains("последний известный", viewModel.BatteryDescription, StringComparison.CurrentCultureIgnoreCase);
    }

    [Theory]
    [InlineData(true, "\uF2A3")]
    [InlineData(false, "\uF285")]
    public void NotificationButton_UsesStateSpecificFluentGlyph(bool enabled, string expected) =>
        Assert.Equal(expected, TrayPanelViewModel.GetNotificationsIconGlyph(enabled));
}
