using BluetoothNotify.App.Models;
using BluetoothNotify.App.Services;

namespace BluetoothNotify.Tests;

public sealed class SettingsStoreTests : IDisposable
{
    private readonly string _directory = Path.Combine(Path.GetTempPath(), $"BluetoothNotify.Tests.{Guid.NewGuid():N}");

    [Fact]
    public async Task SaveAndLoad_RoundTrips()
    {
        var store = new SettingsStore(_directory);
        await store.SaveAsync(new AppSettings
        {
            NotificationsEnabled = false,
            Language = AppLanguagePreference.EnglishUnitedStates,
            Theme = AppThemePreference.Light
        });
        var result = await store.LoadAsync();
        Assert.False(result.NotificationsEnabled);
        Assert.Equal(AppLanguagePreference.EnglishUnitedStates, result.Language);
        Assert.Equal(AppThemePreference.Light, result.Theme);
        Assert.Equal(AppSettings.CurrentSchemaVersion, result.SchemaVersion);
    }

    [Fact]
    public async Task CorruptJson_IsBackedUpAndDefaultsRestored()
    {
        Directory.CreateDirectory(_directory);
        await File.WriteAllTextAsync(Path.Combine(_directory, "settings.json"), "{ definitely broken");
        var result = await new SettingsStore(_directory).LoadAsync();
        Assert.True(result.NotificationsEnabled);
        Assert.Single(Directory.GetFiles(_directory, "settings.corrupt-*.json"));
    }

    [Fact]
    public async Task OlderSchema_IsMigrated()
    {
        Directory.CreateDirectory(_directory);
        await File.WriteAllTextAsync(Path.Combine(_directory, "settings.json"), "{\"SchemaVersion\":0,\"NotificationsEnabled\":false}");
        var result = await new SettingsStore(_directory).LoadAsync();
        Assert.False(result.NotificationsEnabled);
        Assert.Equal(AppLanguagePreference.System, result.Language);
        Assert.Equal(AppThemePreference.System, result.Theme);
        Assert.Equal(AppSettings.CurrentSchemaVersion, result.SchemaVersion);
    }

    [Fact]
    public async Task InvalidPreferenceValues_FallBackToSystem()
    {
        Directory.CreateDirectory(_directory);
        await File.WriteAllTextAsync(Path.Combine(_directory, "settings.json"),
            "{\"SchemaVersion\":2,\"Language\":999,\"Theme\":999}");

        var result = await new SettingsStore(_directory).LoadAsync();

        Assert.Equal(AppLanguagePreference.System, result.Language);
        Assert.Equal(AppThemePreference.System, result.Theme);
    }

    public void Dispose()
    {
        if (Directory.Exists(_directory)) Directory.Delete(_directory, true);
    }
}
