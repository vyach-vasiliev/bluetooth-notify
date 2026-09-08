using BluetoothNotify.App.Infrastructure;

namespace BluetoothNotify.Tests;

public sealed class AsyncRefreshGateTests
{
    [Fact]
    public async Task AutomaticRefresh_IsSkippedWhileRefreshRuns()
    {
        using var gate = new AsyncRefreshGate();
        var entered = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var release = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var executions = 0;
        var first = gate.RunAsync(async _ =>
        {
            Interlocked.Increment(ref executions);
            entered.SetResult();
            await release.Task;
        }, false, CancellationToken.None);
        await entered.Task;

        var skipped = await gate.RunAsync(_ => { Interlocked.Increment(ref executions); return Task.CompletedTask; }, true, CancellationToken.None);
        release.SetResult();
        await first;

        Assert.False(skipped);
        Assert.Equal(1, executions);
    }

    [Fact]
    public async Task ManualRefresh_WaitsInsteadOfOverlapping()
    {
        using var gate = new AsyncRefreshGate();
        var release = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var firstEntered = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var active = 0;
        var peak = 0;
        var first = gate.RunAsync(async _ =>
        {
            peak = Math.Max(peak, Interlocked.Increment(ref active));
            firstEntered.SetResult();
            await release.Task;
            Interlocked.Decrement(ref active);
        }, false, CancellationToken.None);
        await firstEntered.Task;
        var second = gate.RunAsync(_ =>
        {
            peak = Math.Max(peak, Interlocked.Increment(ref active));
            Interlocked.Decrement(ref active);
            return Task.CompletedTask;
        }, false, CancellationToken.None);
        release.SetResult();
        await Task.WhenAll(first, second);
        Assert.Equal(1, peak);
    }
}
