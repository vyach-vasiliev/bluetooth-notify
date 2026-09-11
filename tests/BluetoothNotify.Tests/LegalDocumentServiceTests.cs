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
            CultureInfo.GetCultureInfo(cultureName));

        Assert.Equal("https", uri.Scheme);
        Assert.Equal("bluetooth-notify.onrender.com", uri.Host);
        Assert.Equal("/legal/", uri.AbsolutePath);
        Assert.Equal($"?lang={expectedLanguage}", uri.Query);
        Assert.Equal("#privacy", uri.Fragment);
    }

    [Fact]
    public void BuildDocumentUri_RejectsUnknownSection()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            LegalDocumentService.BuildDocumentUri("unknown", CultureInfo.InvariantCulture));
    }

    [Fact]
    public void ProjectRepositoryUri_PointsToOfficialGitHubRepository()
    {
        Assert.Equal(
            "https://github.com/vyach-vasiliev/bluetooth-notify/",
            LegalDocumentService.ProjectRepositoryUri.AbsoluteUri);
    }
}
