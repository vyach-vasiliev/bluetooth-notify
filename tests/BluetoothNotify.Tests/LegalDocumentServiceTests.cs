using System.Globalization;
using BluetoothNotify.App.Services;

namespace BluetoothNotify.Tests;

public sealed class LegalDocumentServiceTests
{
    [Theory]
    [InlineData("ru-RU", "ru")]
    [InlineData("de-DE", "de")]
    [InlineData("ja-JP", "ja")]
    [InlineData("zh-CN", "zh")]
    [InlineData("it-IT", "en")]
    public void BuildDocumentUri_SelectsSupportedLanguage(string cultureName, string expectedLanguage)
    {
        var uri = LegalDocumentService.BuildDocumentUri(
            "privacy",
            CultureInfo.GetCultureInfo(cultureName),
            Path.Combine(Path.GetTempPath(), "BluetoothNotify-Legal-Test"));

        Assert.Equal($"?lang={expectedLanguage}", uri.Query);
        Assert.Equal("#privacy", uri.Fragment);
        Assert.EndsWith("/Legal/index.html", uri.AbsolutePath, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void BuildDocumentUri_RejectsUnknownSection()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            LegalDocumentService.BuildDocumentUri("unknown", CultureInfo.InvariantCulture));
    }
}
