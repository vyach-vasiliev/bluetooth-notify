using System.Diagnostics;
using System.Globalization;

namespace BluetoothNotify.App.Services;

public static class LegalDocumentService
{
    private static readonly HashSet<string> SupportedLanguages =
        new(StringComparer.OrdinalIgnoreCase) { "en", "ru", "de", "fr", "es", "pt", "ja", "ko", "zh" };

    public static Uri BuildDocumentUri(string section, CultureInfo? culture = null, string? baseDirectory = null)
    {
        var normalizedSection = section.ToLowerInvariant() switch
        {
            "privacy" => "privacy",
            "terms" => "terms",
            "disclaimer" => "disclaimer",
            _ => throw new ArgumentOutOfRangeException(nameof(section))
        };
        var language = (culture ?? CultureInfo.CurrentUICulture).TwoLetterISOLanguageName;
        if (!SupportedLanguages.Contains(language)) language = "en";

        var path = Path.Combine(baseDirectory ?? AppContext.BaseDirectory, "Legal", "index.html");
        var builder = new UriBuilder(new Uri(path))
        {
            Query = $"lang={language}",
            Fragment = normalizedSection
        };
        return builder.Uri;
    }

    public static bool TryOpen(string section)
    {
        try
        {
            var uri = BuildDocumentUri(section);
            if (!File.Exists(uri.LocalPath)) return false;
            Process.Start(new ProcessStartInfo(uri.AbsoluteUri) { UseShellExecute = true });
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
