namespace BluetoothNotify.App.Models;

public enum AppLanguagePreference
{
    System,
    EnglishUnitedStates,
    Russian
}

public enum AppThemePreference
{
    System,
    Light,
    Dark
}

public sealed record AppSettings
{
    public const int CurrentSchemaVersion = 2;
    public int SchemaVersion { get; init; } = CurrentSchemaVersion;
    public bool NotificationsEnabled { get; init; } = true;
    public AppLanguagePreference Language { get; init; } = AppLanguagePreference.System;
    public AppThemePreference Theme { get; init; } = AppThemePreference.System;
}
