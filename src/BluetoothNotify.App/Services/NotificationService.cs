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

    public static AppNotification BuildConnectedNotification(BluetoothDeviceState device)
    {
        var builder = new AppNotificationBuilder()
            // The first ToastGeneric text element is rendered as the title by Windows.
            .AddText(device.Name)
            .AddText(Properties.Strings.Connected);
        if (device.BatteryPercent is { } battery)
            builder.AddText($"{GetBatteryIndicator(battery)} {string.Format(Properties.Strings.BatteryNotification, battery)}");

        var logoPath = Path.Combine(AppContext.BaseDirectory, "Assets", "BluetoothNotify.Notification.png");
        if (File.Exists(logoPath))
            builder.SetAppLogoOverride(new Uri(logoPath), AppNotificationImageCrop.Default, Properties.Strings.AppTitle);

        return builder.BuildNotification();
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
