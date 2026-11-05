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
        Assert.Equal("/legal/index.html", uri.AbsolutePath);
        Assert.Equal($"?lang={expectedLanguage}", uri.Query);
        Assert.Equal("#privacy", uri.Fragment);
    }

    [Theory]
    [InlineData("privacy")]
    [InlineData("terms")]
    [InlineData("disclaimer")]
    public void BuildDocumentUri_PointsToPublishedDocumentSection(string section)
    {
        var uri = LegalDocumentService.BuildDocumentUri(
            section,
            CultureInfo.GetCultureInfo("ru-RU"));

        Assert.Equal(
            $"https://bluetooth-notify.onrender.com/legal/index.html?lang=ru#{section}",
            uri.AbsoluteUri);
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
