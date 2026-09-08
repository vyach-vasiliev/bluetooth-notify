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
            var builder = new AppNotificationBuilder()
                .AddText(string.Format(Properties.Strings.DeviceConnectedNotification, device.Name));
            if (device.BatteryPercent is { } battery)
                builder.AddText(string.Format(Properties.Strings.BatteryNotification, battery));
            AppNotificationManager.Default.Show(builder.BuildNotification());
        }
        catch (Exception ex) { logger.Warning("App notification could not be shown.", ex); }
        return Task.CompletedTask;
    }

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
