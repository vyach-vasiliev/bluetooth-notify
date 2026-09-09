namespace BluetoothNotify.App.Models;

public enum AppLanguagePreference
{
    System,
    EnglishUnitedStates,
    Russian,
    German,
    Japanese,
    French,
    Spanish,
    Portuguese,
    Chinese,
    Korean
}

public enum AppThemePreference
{
    System,
    Light,
    Dark
}

public sealed record AppSettings
{
    public const int CurrentSchemaVersion = 3;
    public int SchemaVersion { get; init; } = CurrentSchemaVersion;
    public bool NotificationsEnabled { get; init; } = true;
    public AppLanguagePreference Language { get; init; } = AppLanguagePreference.System;
    public AppThemePreference Theme { get; init; } = AppThemePreference.System;
    public bool MediumBatteryNotificationEnabled { get; init; } = true;
    public int MediumBatteryThresholdPercent { get; init; } = 30;
    public bool LowBatteryNotificationEnabled { get; init; } = true;
    public int LowBatteryThresholdPercent { get; init; } = 15;
}
