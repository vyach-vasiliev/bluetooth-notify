using System.Net;
using System.Net.Http;
using System.Text.Json;
using BluetoothNotify.App.Services;

namespace BluetoothNotify.Tests;

public sealed class GitHubUpdateServiceTests
{
    [Theory]
    [InlineData("1.2.0", "v1.3.0", true)]
    [InlineData("1.3.0", "v1.3.0", false)]
    [InlineData("1.3.0", "v1.2.0", false)]
    [InlineData("1.9.0", "v1.10.0", true)]
    [InlineData("1.3.0", "1.3.1", true)]
    public async Task CheckAsync_ComparesNumericVersions(string current, string tag, bool available)
    {
        using var client = CreateClient(ReleaseJson(tag));
        var service = new GitHubUpdateService(client, Version.Parse(current));

        var release = await service.CheckAsync();

        Assert.Equal(available, release is not null);
        if (available)
        {
            Assert.Equal(Version.Parse(tag.TrimStart('v')), release!.Version);
            Assert.Equal($"https://github.com/vyach-vasiliev/bluetooth-notify/releases/tag/{tag}", release.ReleaseUri.AbsoluteUri);
            Assert.Equal($"https://github.com/vyach-vasiliev/bluetooth-notify/releases/download/{tag}/BluetoothNotify-Setup-{release.Version}-x64.exe", release.InstallerUri.AbsoluteUri);
        }
    }

    [Fact]
    public async Task CheckAsync_UsesAnonymousOfficialApiWithoutDeviceData()
    {
        using var client = new HttpClient(new Handler((request, _) =>
        {
            Assert.Equal(GitHubUpdateService.LatestReleaseApi, request.RequestUri);
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("BluetoothNotify/1.2.0", request.Headers.UserAgent.ToString());
            Assert.Null(request.Headers.Authorization);
            Assert.Null(request.Content);
            Assert.Empty(request.RequestUri!.Query);
            return Task.FromResult(Response(ReleaseJson("v1.3.0")));
        }));
        await new GitHubUpdateService(client, new Version(1, 2, 0, 0)).CheckAsync();
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public async Task CheckAsync_IgnoresDraftsAndPrereleases(bool draft, bool prerelease)
    {
        using var client = CreateClient(ReleaseJson("v1.4.0", draft: draft, prerelease: prerelease));
        Assert.Null(await new GitHubUpdateService(client, new Version(1, 3, 0)).CheckAsync());
    }

    [Theory]
    [InlineData("v1.4.0-beta.1")]
    [InlineData("latest")]
    [InlineData("v1.4")]
    [InlineData("v1.4.0/evil")]
    public async Task CheckAsync_RejectsUnsupportedTags(string tag)
    {
        using var client = CreateClient(ReleaseJson(tag));
        await Assert.ThrowsAsync<InvalidDataException>(() =>
            new GitHubUpdateService(client, new Version(1, 3, 0)).CheckAsync());
    }

    [Theory]
    [InlineData("https://example.com/installer.exe", "uploaded", 100)]
    [InlineData("https://github.com/other/project/releases/download/v1.4.0/BluetoothNotify-Setup-1.4.0-x64.exe", "uploaded", 100)]
    [InlineData(null, "new", 100)]
    [InlineData(null, "uploaded", 0)]
    public async Task CheckAsync_RejectsUntrustedOrIncompleteInstaller(string? url, string state, long size)
    {
        using var client = CreateClient(ReleaseJson("v1.4.0", url, state, size));
        await Assert.ThrowsAsync<InvalidDataException>(() =>
            new GitHubUpdateService(client, new Version(1, 3, 0)).CheckAsync());
    }

    [Fact]
    public async Task CheckAsync_RejectsReleaseWithoutInstaller()
    {
        using var client = CreateClient("""{"tag_name":"v1.4.0","assets":[]}""");
        await Assert.ThrowsAsync<InvalidDataException>(() =>
            new GitHubUpdateService(client, new Version(1, 3, 0)).CheckAsync());
    }

    [Theory]
    [InlineData(HttpStatusCode.Forbidden)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public async Task CheckAsync_SurfacesApiFailures(HttpStatusCode status)
    {
        using var client = CreateClient("{}", status);
        await Assert.ThrowsAsync<HttpRequestException>(() =>
            new GitHubUpdateService(client, new Version(1, 3, 0)).CheckAsync());
    }

    [Fact]
    public async Task CheckAsync_NoPublishedReleaseDoesNotOfferUpdate()
    {
        using var client = CreateClient("{}", HttpStatusCode.NotFound);
        Assert.Null(await new GitHubUpdateService(client, new Version(1, 3, 0)).CheckAsync());
    }

    [Fact]
    public async Task CheckAsync_RejectsMalformedJson()
    {
        using var client = CreateClient("not json");
        await Assert.ThrowsAsync<JsonException>(() =>
            new GitHubUpdateService(client, new Version(1, 3, 0)).CheckAsync());
    }

    [Fact]
    public async Task CheckAsync_HonorsCancellation()
    {
        using var cancellation = new CancellationTokenSource();
        using var client = new HttpClient(new Handler(async (_, token) =>
        {
            cancellation.Cancel();
            await Task.Delay(Timeout.Infinite, token);
            return Response("{}");
        }));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            new GitHubUpdateService(client, new Version(1, 3, 0)).CheckAsync(cancellation.Token));
    }

    [Theory]
    [InlineData(HttpStatusCode.Forbidden)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    public async Task CheckAsync_UsesPublicReleaseManifestWhenApiQuotaIsExhausted(HttpStatusCode status)
    {
        var calls = 0;
        using var client = new HttpClient(new Handler((request, _) =>
        {
            calls++;
            Assert.Null(request.Headers.Authorization);
            if (request.RequestUri == GitHubUpdateService.LatestReleaseApi)
                return Task.FromResult(Response("{}", status));
            Assert.Equal(GitHubUpdateService.LatestReleaseManifest, request.RequestUri);
            return Task.FromResult(Response(ReleaseJson("v1.4.0")));
        }));

        var release = await new GitHubUpdateService(client, new Version(1, 3, 0)).CheckAsync();

        Assert.Equal(new Version(1, 4, 0), release!.Version);
        Assert.Equal(2, calls);
    }

    private static HttpClient CreateClient(string json, HttpStatusCode status = HttpStatusCode.OK) =>
        new(new Handler((_, _) => Task.FromResult(Response(json, status))));

    private static HttpResponseMessage Response(string json, HttpStatusCode status = HttpStatusCode.OK) =>
        new(status) { Content = new StringContent(json) };

    private static string ReleaseJson(string tag, string? url = null, string state = "uploaded", long size = 100,
        bool draft = false, bool prerelease = false)
    {
        var version = tag.TrimStart('v');
        var name = $"BluetoothNotify-Setup-{version}-x64.exe";
        return JsonSerializer.Serialize(new
        {
            tag_name = tag, draft, prerelease,
            assets = new[] { new { name, state, size, browser_download_url = url ??
                $"https://github.com/vyach-vasiliev/bluetooth-notify/releases/download/{tag}/{name}" } }
        });
    }

    private sealed class Handler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> send) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
            send(request, cancellationToken);
    }
}
