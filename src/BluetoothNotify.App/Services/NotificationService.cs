using BluetoothNotify.App.Models;
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

namespace BluetoothNotify.App.Services;

public sealed class NotificationService(AppLogger logger) : INotificationService
{
    private bool _registered;

    public void Initialize()
    {
        try
        {
            AppNotificationManager.Default.Register();
            _registered = true;
        }
        catch (Exception ex) { logger.Warning("App notification registration failed.", ex); }
    }

    public Task ShowConnectedAsync(BluetoothDeviceState device, CancellationToken cancellationToken = default)
    {
        if (!_registered || cancellationToken.IsCancellationRequested) return Task.CompletedTask;
        try
        {
            AppNotificationManager.Default.Show(BuildConnectedNotification(device));
        }
        catch (Exception ex) { logger.Warning("App notification could not be shown.", ex); }
        return Task.CompletedTask;
    }

    public Task ShowBatteryLevelAsync(BatteryNotificationAlert alert, CancellationToken cancellationToken = default)
    {
        if (!_registered || cancellationToken.IsCancellationRequested) return Task.CompletedTask;
        try
        {
            AppNotificationManager.Default.Show(BuildBatteryLevelNotification(alert));
        }
        catch (Exception ex) { logger.Warning("Battery notification could not be shown.", ex); }
        return Task.CompletedTask;
    }

    public static AppNotification BuildConnectedNotification(BluetoothDeviceState device)
    {
        var builder = new AppNotificationBuilder()
            // The first ToastGeneric text element is rendered as the title by Windows.
            .AddText(device.Name)
            .AddText(Properties.Strings.Connected);
        if (device.BatteryPercent is { } battery)
            builder.AddText($"{GetBatteryIndicator(battery)} {string.Format(Properties.Strings.BatteryNotification, battery)}");

        AddAppLogo(builder);

        return builder.BuildNotification();
    }

    public static AppNotification BuildBatteryLevelNotification(BatteryNotificationAlert alert)
    {
        var levelText = alert.Level == BatteryNotificationLevel.Low
            ? Properties.Strings.LowBatteryNotification
            : Properties.Strings.MediumBatteryNotification;
        var indicator = alert.Level == BatteryNotificationLevel.Low ? "🔴" : "🟡";
        var builder = new AppNotificationBuilder()
            .AddText(alert.Device.Name)
            .AddText(levelText)
            .AddText($"{indicator} {string.Format(Properties.Strings.BatteryNotification, alert.Device.BatteryPercent)}");
        AddAppLogo(builder);
        return builder.BuildNotification();
    }

    private static void AddAppLogo(AppNotificationBuilder builder)
    {
        var logoPath = Path.Combine(AppContext.BaseDirectory, "Assets", "BluetoothNotify.Notification.png");
        if (File.Exists(logoPath))
            builder.SetAppLogoOverride(new Uri(logoPath), AppNotificationImageCrop.Default, Properties.Strings.AppTitle);
    }

    public static string GetBatteryIndicator(int batteryPercent) =>
        BatteryLevelClassifier.GetCategory(batteryPercent) switch
        {
            BatteryLevelCategory.Critical => "🔴",
            BatteryLevelCategory.Low => "🟡",
            _ => "🟢"
        };

    public ValueTask DisposeAsync()
    {
        if (_registered)
        {
            try { AppNotificationManager.Default.Unregister(); }
            catch (Exception ex) { logger.Warning("App notification unregistration failed.", ex); }
        }
        return ValueTask.CompletedTask;
    }
}
