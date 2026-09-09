using System.Text.Json;
using BluetoothNotify.App.Models;

namespace BluetoothNotify.App.Services;

public sealed class SettingsStore(string? baseDirectory = null) : ISettingsStore
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
    private readonly string _directory = baseDirectory ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BluetoothNotify");
    private string SettingsPath => Path.Combine(_directory, "settings.json");

    public async Task<AppSettings> LoadAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (!File.Exists(SettingsPath)) return new AppSettings();
            await using var stream = File.OpenRead(SettingsPath);
            var value = await JsonSerializer.DeserializeAsync<AppSettings>(stream, JsonOptions, cancellationToken);
            return Migrate(value);
        }
        catch (Exception ex) when (ex is JsonException or IOException or UnauthorizedAccessException)
        {
            TryBackupCorruptFile();
            return new AppSettings();
        }
    }

    public async Task SaveAsync(AppSettings settings, CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_directory);
        var temporary = Path.Combine(_directory, $"settings.{Guid.NewGuid():N}.tmp");
        try
        {
            await using (var stream = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, FileOptions.WriteThrough))
            {
                await JsonSerializer.SerializeAsync(stream, settings with { SchemaVersion = AppSettings.CurrentSchemaVersion }, JsonOptions, cancellationToken);
                await stream.FlushAsync(cancellationToken);
            }
            File.Move(temporary, SettingsPath, true);
        }
        finally
        {
            if (File.Exists(temporary)) File.Delete(temporary);
        }
    }

    private static AppSettings Migrate(AppSettings? value) => new()
    {
        NotificationsEnabled = value?.NotificationsEnabled ?? true,
        Language = Enum.IsDefined(value?.Language ?? AppLanguagePreference.System)
            ? value?.Language ?? AppLanguagePreference.System
            : AppLanguagePreference.System,
        Theme = Enum.IsDefined(value?.Theme ?? AppThemePreference.System)
            ? value?.Theme ?? AppThemePreference.System
            : AppThemePreference.System,
        MediumBatteryNotificationEnabled = value?.MediumBatteryNotificationEnabled ?? true,
        MediumBatteryThresholdPercent = NormalizeMediumThreshold(value?.MediumBatteryThresholdPercent ?? 30),
        LowBatteryNotificationEnabled = value?.LowBatteryNotificationEnabled ?? true,
        LowBatteryThresholdPercent = NormalizeLowThreshold(value?.LowBatteryThresholdPercent ?? 15)
    };

    private static int NormalizeMediumThreshold(int value) =>
        value is >= 20 and <= 50 && value % 5 == 0 ? value : 30;

    private static int NormalizeLowThreshold(int value) =>
        value is >= 5 and <= 15 && value % 5 == 0 ? value : 15;

    private void TryBackupCorruptFile()
    {
        try
        {
            if (!File.Exists(SettingsPath)) return;
            Directory.CreateDirectory(_directory);
            File.Move(SettingsPath, Path.Combine(_directory, $"settings.corrupt-{DateTime.UtcNow:yyyyMMddHHmmss}.json"), true);
        }
        catch { /* Recovery must never prevent startup. */ }
    }
}
