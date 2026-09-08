using System.Windows;
using BluetoothNotify.App.Models;
using Microsoft.Win32;

namespace BluetoothNotify.App.Services;

public static class ThemeManager
{
    private const string PersonalizeKey = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

    public static bool ShouldUseLightTheme(AppThemePreference preference, bool systemUsesLightTheme) => preference switch
    {
        AppThemePreference.Light => true,
        AppThemePreference.Dark => false,
        _ => systemUsesLightTheme
    };

    public static bool IsWindowsAppLightTheme()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(PersonalizeKey);
            return key?.GetValue("AppsUseLightTheme") is int value && value != 0;
        }
        catch { return false; }
    }

    public static void Apply(AppThemePreference preference)
    {
        if (Application.Current is null) return;
        var light = ShouldUseLightTheme(preference, IsWindowsAppLightTheme());
        var source = new Uri(light ? "Themes/LightTheme.xaml" : "Themes/DarkTheme.xaml", UriKind.Relative);
        var dictionaries = Application.Current.Resources.MergedDictionaries;
        var currentIndex = dictionaries
            .Select((dictionary, index) => (dictionary, index))
            .FirstOrDefault(item => item.dictionary.Source?.OriginalString.EndsWith("Theme.xaml", StringComparison.OrdinalIgnoreCase) == true)
            .index;
        var replacement = new ResourceDictionary { Source = source };
        if (dictionaries.Count == 0) dictionaries.Add(replacement);
        else dictionaries[currentIndex] = replacement;
    }
}
