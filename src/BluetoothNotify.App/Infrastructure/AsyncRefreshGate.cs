namespace BluetoothNotify.App.Infrastructure;

public sealed class AsyncRefreshGate : IDisposable
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public async Task<bool> RunAsync(Func<CancellationToken, Task> operation, bool skipIfBusy, CancellationToken cancellationToken)
    {
        if (skipIfBusy)
        {
            if (!await _semaphore.WaitAsync(0, cancellationToken).ConfigureAwait(false)) return false;
        }
        else await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            await operation(cancellationToken).ConfigureAwait(false);
            return true;
        }
        finally { _semaphore.Release(); }
    }

    public void Dispose() => _semaphore.Dispose();
}
