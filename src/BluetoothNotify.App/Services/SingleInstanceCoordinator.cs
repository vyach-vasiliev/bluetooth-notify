using System.IO.Pipes;

namespace BluetoothNotify.App.Services;

public sealed class SingleInstanceCoordinator : IDisposable
{
    private const string MutexName = "Local\\BluetoothNotify.App.Singleton";
    private const string PipeName = "BluetoothNotify.App.CommandPipe";
    private readonly Mutex _mutex = new(false, MutexName);
    private readonly CancellationTokenSource _lifetime = new();
    private Task? _server;

    public bool TryAcquire()
    {
        try { return _mutex.WaitOne(TimeSpan.Zero, false); }
        catch (AbandonedMutexException) { return true; }
    }

    public void StartServer(Action showAction)
    {
        _server = Task.Run(async () =>
        {
            while (!_lifetime.IsCancellationRequested)
            {
                try
                {
                    await using var server = new NamedPipeServerStream(PipeName, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                    await server.WaitForConnectionAsync(_lifetime.Token).ConfigureAwait(false);
                    using var reader = new StreamReader(server);
                    if (await reader.ReadLineAsync(_lifetime.Token).ConfigureAwait(false) == "show") showAction();
                }
                catch (OperationCanceledException) { break; }
                catch { await Task.Delay(250, _lifetime.Token).ConfigureAwait(false); }
            }
        });
    }

    public static async Task SignalExistingAsync()
    {
        try
        {
            await using var client = new NamedPipeClientStream(".", PipeName, PipeDirection.Out, PipeOptions.Asynchronous);
            await client.ConnectAsync(800).ConfigureAwait(false);
            await using var writer = new StreamWriter(client) { AutoFlush = true };
            await writer.WriteLineAsync("show").ConfigureAwait(false);
        }
        catch { }
    }

    public void Dispose()
    {
        _lifetime.Cancel();
        _lifetime.Dispose();
        try { _mutex.ReleaseMutex(); } catch { }
        _mutex.Dispose();
    }
}
