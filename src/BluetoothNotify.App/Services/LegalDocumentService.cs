using System.Diagnostics;
using System.Globalization;

namespace BluetoothNotify.App.Services;

public static class LegalDocumentService
{
    public static Uri PublishedLegalPageUri { get; } =
        new("https://bluetooth-notify.onrender.com/legal/");

    public static Uri ProjectRepositoryUri { get; } =
        new("https://github.com/vyach-vasiliev/bluetooth-notify/");

    private static readonly HashSet<string> SupportedLanguages =
        new(StringComparer.OrdinalIgnoreCase) { "en", "ru", "de", "fr", "es", "pt", "ja", "ko", "zh" };

    public static Uri BuildDocumentUri(string section, CultureInfo? culture = null)
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

        var builder = new UriBuilder(PublishedLegalPageUri)
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
            return TryOpenUri(uri);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool TryOpenProjectRepository() => TryOpenUri(ProjectRepositoryUri);

    private static bool TryOpenUri(Uri uri)
    {
        try
        {
            Process.Start(new ProcessStartInfo(uri.AbsoluteUri) { UseShellExecute = true });
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
