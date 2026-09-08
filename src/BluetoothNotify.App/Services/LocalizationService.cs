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
            _ when systemCulture.TwoLetterISOLanguageName.Equals("ru", StringComparison.OrdinalIgnoreCase) =>
                CultureInfo.GetCultureInfo("ru-RU"),
            _ => CultureInfo.GetCultureInfo("en-US")
        };
    }

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
