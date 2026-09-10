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
    [InlineData(AppLanguagePreference.System, "de-AT", "de-DE")]
    [InlineData(AppLanguagePreference.System, "ja-JP", "ja-JP")]
    [InlineData(AppLanguagePreference.System, "fr-CA", "fr-FR")]
    [InlineData(AppLanguagePreference.System, "es-MX", "es-ES")]
    [InlineData(AppLanguagePreference.System, "pt-BR", "pt-PT")]
    [InlineData(AppLanguagePreference.System, "zh-TW", "zh-CN")]
    [InlineData(AppLanguagePreference.System, "ko-KR", "ko-KR")]
    [InlineData(AppLanguagePreference.System, "it-IT", "en-US")]
    [InlineData(AppLanguagePreference.EnglishUnitedStates, "ru-RU", "en-US")]
    [InlineData(AppLanguagePreference.Russian, "en-US", "ru-RU")]
    [InlineData(AppLanguagePreference.German, "en-US", "de-DE")]
    [InlineData(AppLanguagePreference.Japanese, "en-US", "ja-JP")]
    [InlineData(AppLanguagePreference.French, "en-US", "fr-FR")]
    [InlineData(AppLanguagePreference.Spanish, "en-US", "es-ES")]
    [InlineData(AppLanguagePreference.Portuguese, "en-US", "pt-PT")]
    [InlineData(AppLanguagePreference.Chinese, "en-US", "zh-CN")]
    [InlineData(AppLanguagePreference.Korean, "en-US", "ko-KR")]
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
    public void LocalizedResources_ContainEveryUserFacingString()
    {
        var manager = new ResourceManager("BluetoothNotify.App.Properties.Strings", typeof(Strings).Assembly);
        var neutral = manager.GetResourceSet(CultureInfo.InvariantCulture, true, false);

        Assert.NotNull(neutral);
        var neutralKeys = neutral!.Cast<DictionaryEntry>().Select(item => (string)item.Key).Order().ToArray();
        foreach (var cultureName in new[] { "en-US", "de-DE", "ja-JP", "fr-FR", "es-ES", "pt-PT", "zh-CN", "ko-KR" })
        {
            var localized = manager.GetResourceSet(CultureInfo.GetCultureInfo(cultureName), true, false);
            Assert.NotNull(localized);
            var localizedKeys = localized!.Cast<DictionaryEntry>().Select(item => (string)item.Key).Order().ToArray();
            Assert.Equal(neutralKeys, localizedKeys);
        }
        Assert.Equal("Settings", manager.GetString(nameof(Strings.Settings), CultureInfo.GetCultureInfo("en-US")));
        Assert.Equal("Einstellungen", manager.GetString(nameof(Strings.Settings), CultureInfo.GetCultureInfo("de-DE")));
        Assert.Equal("設定", manager.GetString(nameof(Strings.Settings), CultureInfo.GetCultureInfo("ja-JP")));
        Assert.Equal("Paramètres", manager.GetString(nameof(Strings.Settings), CultureInfo.GetCultureInfo("fr-FR")));
        Assert.Equal("Configuración", manager.GetString(nameof(Strings.Settings), CultureInfo.GetCultureInfo("es-ES")));
        Assert.Equal("Definições", manager.GetString(nameof(Strings.Settings), CultureInfo.GetCultureInfo("pt-PT")));
        Assert.Equal("设置", manager.GetString(nameof(Strings.Settings), CultureInfo.GetCultureInfo("zh-CN")));
        Assert.Equal("설정", manager.GetString(nameof(Strings.Settings), CultureInfo.GetCultureInfo("ko-KR")));

        var expectedLanguageNames = new Dictionary<string, string>
        {
            [nameof(Strings.EnglishUnitedStates)] = "English (English)",
            [nameof(Strings.Russian)] = "Русский (Russian)",
            [nameof(Strings.German)] = "Deutsch (German)",
            [nameof(Strings.Japanese)] = "日本語 (Japanese)",
            [nameof(Strings.French)] = "Français (French)",
            [nameof(Strings.Spanish)] = "Español (Spanish)",
            [nameof(Strings.Portuguese)] = "Português (Portuguese)",
            [nameof(Strings.Chinese)] = "简体中文 (Chinese)",
            [nameof(Strings.Korean)] = "한국어 (Korean)"
        };
        foreach (var cultureName in new[] { "ru-RU", "en-US", "de-DE", "ja-JP", "fr-FR", "es-ES", "pt-PT", "zh-CN", "ko-KR" })
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            foreach (var (resourceKey, expectedLanguageName) in expectedLanguageNames)
                Assert.Equal(expectedLanguageName, manager.GetString(resourceKey, culture));
        }
    }
}
