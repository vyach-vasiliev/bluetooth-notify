using System.Globalization;
using System.Resources;

namespace BluetoothNotify.App.Properties;

public static class Strings
{
    private static readonly ResourceManager Manager = new("BluetoothNotify.App.Properties.Strings", typeof(Strings).Assembly);
    private static CultureInfo? _selectedCulture;
    private static string Get(string name) => Manager.GetString(name, _selectedCulture ?? CultureInfo.CurrentUICulture) ?? name;

    public static void UseCulture(CultureInfo culture) => _selectedCulture = culture;

    public static string AppTitle => Get(nameof(AppTitle));
    public static string Subtitle => Get(nameof(Subtitle));
    public static string Open => Get(nameof(Open));
    public static string Refresh => Get(nameof(Refresh));
    public static string Exit => Get(nameof(Exit));
    public static string Settings => Get(nameof(Settings));
    public static string SettingsDescription => Get(nameof(SettingsDescription));
    public static string Language => Get(nameof(Language));
    public static string Theme => Get(nameof(Theme));
    public static string SystemDefault => Get(nameof(SystemDefault));
    public static string EnglishUnitedStates => Get(nameof(EnglishUnitedStates));
    public static string Russian => Get(nameof(Russian));
    public static string German => Get(nameof(German));
    public static string Japanese => Get(nameof(Japanese));
    public static string French => Get(nameof(French));
    public static string Spanish => Get(nameof(Spanish));
    public static string Portuguese => Get(nameof(Portuguese));
    public static string Chinese => Get(nameof(Chinese));
    public static string Korean => Get(nameof(Korean));
    public static string LightTheme => Get(nameof(LightTheme));
    public static string DarkTheme => Get(nameof(DarkTheme));
    public static string Back => Get(nameof(Back));
    public static string Notifications => Get(nameof(Notifications));
    public static string NotificationsEnabled => Get(nameof(NotificationsEnabled));
    public static string Updated => Get(nameof(Updated));
    public static string DevicesWithBattery => Get(nameof(DevicesWithBattery));
    public static string DeviceFallbackName => Get(nameof(DeviceFallbackName));
    public static string Connected => Get(nameof(Connected));
    public static string Disconnected => Get(nameof(Disconnected));
    public static string BatteryUnavailable => Get(nameof(BatteryUnavailable));
    public static string BatteryStale => Get(nameof(BatteryStale));
    public static string DisconnectedBatteryStale => Get(nameof(DisconnectedBatteryStale));
    public static string NoDevices => Get(nameof(NoDevices));
    public static string BluetoothUnavailable => Get(nameof(BluetoothUnavailable));
    public static string PartialRefreshWarning => Get(nameof(PartialRefreshWarning));
    public static string BatteryReadTimeout => Get(nameof(BatteryReadTimeout));
    public static string BatteryReadError => Get(nameof(BatteryReadError));
    public static string DeviceConnectedNotification => Get(nameof(DeviceConnectedNotification));
    public static string BatteryNotification => Get(nameof(BatteryNotification));
    public static string BatteryNotifications => Get(nameof(BatteryNotifications));
    public static string BatteryNotificationsDescription => Get(nameof(BatteryNotificationsDescription));
    public static string MediumBatteryLevel => Get(nameof(MediumBatteryLevel));
    public static string MediumBatteryLevelDescription => Get(nameof(MediumBatteryLevelDescription));
    public static string MediumBatteryThreshold => Get(nameof(MediumBatteryThreshold));
    public static string MediumBatteryNotificationToggle => Get(nameof(MediumBatteryNotificationToggle));
    public static string LowBatteryLevel => Get(nameof(LowBatteryLevel));
    public static string LowBatteryLevelDescription => Get(nameof(LowBatteryLevelDescription));
    public static string LowBatteryThreshold => Get(nameof(LowBatteryThreshold));
    public static string LowBatteryNotificationToggle => Get(nameof(LowBatteryNotificationToggle));
    public static string MediumBatteryNotification => Get(nameof(MediumBatteryNotification));
    public static string LowBatteryNotification => Get(nameof(LowBatteryNotification));
    public static string StatusAutomationName => Get(nameof(StatusAutomationName));
    public static string Loading => Get(nameof(Loading));
    public static string LegalInformation => Get(nameof(LegalInformation));
    public static string LegalInformationDescription => Get(nameof(LegalInformationDescription));
    public static string PrivacyPolicy => Get(nameof(PrivacyPolicy));
    public static string TermsOfUse => Get(nameof(TermsOfUse));
    public static string Disclaimer => Get(nameof(Disclaimer));
    public static string LegalDocumentOpenError => Get(nameof(LegalDocumentOpenError));
}
