using BluetoothNotify.App.Services;
using BluetoothNotify.App.ViewModels;

namespace BluetoothNotify.Tests;

public sealed class UpdateViewModelTests
{
    private static readonly UpdateRelease Release = new(new Version(1, 4, 0),
        new Uri("https://github.com/vyach-vasiliev/bluetooth-notify/releases/tag/v1.4.0"),
        new Uri("https://github.com/vyach-vasiliev/bluetooth-notify/releases/download/v1.4.0/BluetoothNotify-Setup-1.4.0-x64.exe"));

    [Fact]
    public async Task Check_OffersUpdateButOpensInstallerOnlyOnClick()
    {
        var opened = new List<Uri>();
        var model = new UpdateViewModel(new Stub(_ => Task.FromResult<UpdateRelease?>(Release)), opened.Add);
        await model.CheckAsync();

        Assert.True(model.CanCheck);
        Assert.True(model.CanDownload);
        Assert.Contains("1.4.0", model.StatusText);
        Assert.Contains("1.3.0", model.CurrentVersionText);
        Assert.Empty(opened);
        model.DownloadCommand.Execute(null);
        Assert.Equal(Release.InstallerUri, Assert.Single(opened));
    }

    [Fact]
    public async Task Check_UpToDateDisablesDownload()
    {
        var model = new UpdateViewModel(new Stub(_ => Task.FromResult<UpdateRelease?>(null)));
        await model.CheckAsync();
        Assert.True(model.CanCheck);
        Assert.False(model.CanDownload);
        Assert.False(model.DownloadCommand.CanExecute(null));
    }

    [Fact]
    public async Task Check_FailureKeepsAppResponsiveAndCanRetry()
    {
        var attempts = 0;
        var model = new UpdateViewModel(new Stub(_ => ++attempts == 1
            ? Task.FromException<UpdateRelease?>(new HttpRequestException())
            : Task.FromResult<UpdateRelease?>(Release)));
        await model.CheckAsync();
        Assert.True(model.CanCheck);
        Assert.False(model.CanDownload);
        await model.CheckAsync();
        Assert.True(model.CanDownload);
    }

    [Fact]
    public async Task Check_StartupAndManualRequestsDoNotOverlap()
    {
        var completion = new TaskCompletionSource<UpdateRelease?>();
        var calls = 0;
        var model = new UpdateViewModel(new Stub(_ => { calls++; return completion.Task; }));
        var first = model.CheckAsync();
        Assert.False(model.CanCheck);
        await model.CheckAsync();
        Assert.Equal(1, calls);
        completion.SetResult(Release);
        await first;
        Assert.True(model.CanCheck);
    }

    [Fact]
    public async Task Check_CancellationDoesNotLeaveBusyState()
    {
        using var cancellation = new CancellationTokenSource();
        var model = new UpdateViewModel(new Stub(token =>
        {
            cancellation.Cancel();
            return Task.FromCanceled<UpdateRelease?>(token);
        }));
        await model.CheckAsync(cancellation.Token);
        Assert.True(model.CanCheck);
        Assert.False(model.CanDownload);
    }

    [Fact]
    public async Task StartupCheck_ReenablesManualCommandWhenCompleted()
    {
        var completion = new TaskCompletionSource<UpdateRelease?>();
        var model = new UpdateViewModel(new Stub(_ => completion.Task));
        var states = new List<bool>();
        model.CheckCommand.CanExecuteChanged += (_, _) => states.Add(model.CheckCommand.CanExecute(null));

        var pending = model.CheckAsync();
        completion.SetResult(null);
        await pending;

        Assert.Equal(new[] { false, true }, states);
    }

    [Fact]
    public async Task Download_FailureDoesNotCrashApp()
    {
        var model = new UpdateViewModel(new Stub(_ => Task.FromResult<UpdateRelease?>(Release)),
            _ => throw new InvalidOperationException());
        await model.CheckAsync();
        model.DownloadCommand.Execute(null);
        Assert.True(model.CanDownload);
        Assert.True(model.CanCheck);
    }

    private sealed class Stub(Func<CancellationToken, Task<UpdateRelease?>> check) : IUpdateService
    {
        public Version CurrentVersion => new(1, 3, 0);
        public Task<UpdateRelease?> CheckAsync(CancellationToken cancellationToken = default) => check(cancellationToken);
    }
}
