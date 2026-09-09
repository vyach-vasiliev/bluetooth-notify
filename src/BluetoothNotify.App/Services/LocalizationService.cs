using System.Globalization;
using BluetoothNotify.App.Models;

namespace BluetoothNotify.App.Services;

public static class LocalizationService
{
    public static CultureInfo ResolveCulture(AppLanguagePreference preference, CultureInfo? systemCulture = null)
    {
        systemCulture ??= CultureInfo.InstalledUICulture;
        return preference switch
        {
            AppLanguagePreference.EnglishUnitedStates => CultureInfo.GetCultureInfo("en-US"),
            AppLanguagePreference.Russian => CultureInfo.GetCultureInfo("ru-RU"),
            AppLanguagePreference.German => CultureInfo.GetCultureInfo("de-DE"),
            AppLanguagePreference.Japanese => CultureInfo.GetCultureInfo("ja-JP"),
            AppLanguagePreference.French => CultureInfo.GetCultureInfo("fr-FR"),
            AppLanguagePreference.Spanish => CultureInfo.GetCultureInfo("es-ES"),
            AppLanguagePreference.Portuguese => CultureInfo.GetCultureInfo("pt-PT"),
            AppLanguagePreference.Chinese => CultureInfo.GetCultureInfo("zh-CN"),
            AppLanguagePreference.Korean => CultureInfo.GetCultureInfo("ko-KR"),
            _ => ResolveSystemCulture(systemCulture)
        };
    }

    private static CultureInfo ResolveSystemCulture(CultureInfo systemCulture) =>
        systemCulture.TwoLetterISOLanguageName.ToLowerInvariant() switch
        {
            "ru" => CultureInfo.GetCultureInfo("ru-RU"),
            "de" => CultureInfo.GetCultureInfo("de-DE"),
            "ja" => CultureInfo.GetCultureInfo("ja-JP"),
            "fr" => CultureInfo.GetCultureInfo("fr-FR"),
            "es" => CultureInfo.GetCultureInfo("es-ES"),
            "pt" => CultureInfo.GetCultureInfo("pt-PT"),
            "zh" => CultureInfo.GetCultureInfo("zh-CN"),
            "ko" => CultureInfo.GetCultureInfo("ko-KR"),
            _ => CultureInfo.GetCultureInfo("en-US")
        };

    public static void Apply(AppLanguagePreference preference)
    {
        var culture = ResolveCulture(preference);
        Properties.Strings.UseCulture(culture);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}
