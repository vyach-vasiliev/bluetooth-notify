using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace BluetoothNotify.App.Services;

public sealed record UpdateRelease(Version Version, Uri ReleaseUri, Uri InstallerUri);

public interface IUpdateService
{
    Version CurrentVersion { get; }
    Task<UpdateRelease?> CheckAsync(CancellationToken cancellationToken = default);
}

public sealed class GitHubUpdateService : IUpdateService
{
    private const string RepositoryUrl = "https://github.com/vyach-vasiliev/bluetooth-notify";
    internal static readonly Uri LatestReleaseApi =
        new("https://api.github.com/repos/vyach-vasiliev/bluetooth-notify/releases/latest");
    internal static readonly Uri LatestReleaseManifest =
        new($"{RepositoryUrl}/releases/latest/download/update.json");
    private static readonly HttpClient SharedClient = new()
    {
        Timeout = TimeSpan.FromSeconds(10),
        MaxResponseContentBufferSize = 1024 * 1024
    };
    private readonly HttpClient _client;

    public GitHubUpdateService(HttpClient? client = null, Version? currentVersion = null)
    {
        _client = client ?? SharedClient;
        var assemblyVersion = currentVersion ?? typeof(GitHubUpdateService).Assembly.GetName().Version
            ?? new Version(0, 0, 0);
        CurrentVersion = new Version(assemblyVersion.Major, assemblyVersion.Minor, Math.Max(0, assemblyVersion.Build));
    }

    public Version CurrentVersion { get; }

    public async Task<UpdateRelease?> CheckAsync(CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, LatestReleaseApi);
        request.Headers.UserAgent.ParseAdd($"BluetoothNotify/{CurrentVersion}");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
        request.Headers.Add("X-GitHub-Api-Version", "2022-11-28");
        using var response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        string json;
        if (response.StatusCode is HttpStatusCode.Forbidden or HttpStatusCode.TooManyRequests)
        {
            // Release assets remain public when a shared IP exhausts GitHub's anonymous API quota.
            using var manifestRequest = new HttpRequestMessage(HttpMethod.Get, LatestReleaseManifest);
            manifestRequest.Headers.UserAgent.ParseAdd($"BluetoothNotify/{CurrentVersion}");
            using var manifestResponse = await _client.SendAsync(manifestRequest, cancellationToken).ConfigureAwait(false);
            manifestResponse.EnsureSuccessStatusCode();
            json = await manifestResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        }
        else
        {
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();
            json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        }
        var release = JsonSerializer.Deserialize<ReleaseResponse>(json)
            ?? throw new InvalidDataException("GitHub returned an empty release.");
        if (release.Draft || release.Prerelease) return null;
        if (release.Tag is null || !Regex.IsMatch(release.Tag, @"^v?\d+\.\d+\.\d+$") ||
            !Version.TryParse(release.Tag.TrimStart('v'), out var version))
            throw new InvalidDataException("GitHub returned an unsupported release version.");
        if (version <= CurrentVersion) return null;

        var installerName = $"BluetoothNotify-Setup-{version}-x64.exe";
        var installerUri = new Uri($"{RepositoryUrl}/releases/download/{release.Tag}/{installerName}");
        var asset = release.Assets?.FirstOrDefault(asset => asset.Name == installerName &&
            asset.State == "uploaded" && asset.Size > 0 && asset.DownloadUrl == installerUri.AbsoluteUri);
        if (asset is null) throw new InvalidDataException("The release does not have a ready Windows x64 installer.");
        return new UpdateRelease(version, new Uri($"{RepositoryUrl}/releases/tag/{release.Tag}"), installerUri);
    }

    private sealed record ReleaseResponse(
        [property: JsonPropertyName("tag_name")] string? Tag,
        [property: JsonPropertyName("draft")] bool Draft,
        [property: JsonPropertyName("prerelease")] bool Prerelease,
        [property: JsonPropertyName("assets")] AssetResponse[]? Assets);

    private sealed record AssetResponse(
        [property: JsonPropertyName("name")] string? Name,
        [property: JsonPropertyName("state")] string? State,
        [property: JsonPropertyName("size")] long Size,
        [property: JsonPropertyName("browser_download_url")] string? DownloadUrl);
}
