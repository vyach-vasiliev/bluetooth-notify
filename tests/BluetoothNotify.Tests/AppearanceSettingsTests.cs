using System.Collections;
using System.Globalization;
using System.Resources;
using BluetoothNotify.App.Models;
using BluetoothNotify.App.Properties;
using BluetoothNotify.App.Services;

namespace BluetoothNotify.Tests;

public sealed class AppearanceSettingsTests
{
    [Theory]
    [InlineData(AppLanguagePreference.System, "ru-RU", "ru-RU")]
    [InlineData(AppLanguagePreference.System, "de-DE", "en-US")]
    [InlineData(AppLanguagePreference.EnglishUnitedStates, "ru-RU", "en-US")]
    [InlineData(AppLanguagePreference.Russian, "en-US", "ru-RU")]
    public void LanguagePreference_ResolvesSupportedCulture(
        AppLanguagePreference preference, string systemCulture, string expected)
    {
        var result = LocalizationService.ResolveCulture(preference, CultureInfo.GetCultureInfo(systemCulture));

        Assert.Equal(expected, result.Name);
    }

    [Theory]
    [InlineData(AppThemePreference.System, true, true)]
    [InlineData(AppThemePreference.System, false, false)]
    [InlineData(AppThemePreference.Light, false, true)]
    [InlineData(AppThemePreference.Dark, true, false)]
    public void ThemePreference_ResolvesExpectedPalette(
        AppThemePreference preference, bool systemLight, bool expected) =>
        Assert.Equal(expected, ThemeManager.ShouldUseLightTheme(preference, systemLight));

    [Fact]
    public void EnglishResources_ContainEveryUserFacingString()
    {
        var manager = new ResourceManager("BluetoothNotify.App.Properties.Strings", typeof(Strings).Assembly);
        var neutral = manager.GetResourceSet(CultureInfo.InvariantCulture, true, false);
        var english = manager.GetResourceSet(CultureInfo.GetCultureInfo("en-US"), true, false);

        Assert.NotNull(neutral);
        Assert.NotNull(english);
        var neutralKeys = neutral!.Cast<DictionaryEntry>().Select(item => (string)item.Key).Order().ToArray();
        var englishKeys = english!.Cast<DictionaryEntry>().Select(item => (string)item.Key).Order().ToArray();
        Assert.Equal(neutralKeys, englishKeys);
        Assert.Equal("Settings", manager.GetString(nameof(Strings.Settings), CultureInfo.GetCultureInfo("en-US")));
    }
}
