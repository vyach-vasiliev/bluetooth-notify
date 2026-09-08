namespace BluetoothNotify.App.Services;

public sealed class AppLogger
{
    private const int MaxFiles = 5;
    private const long MaxBytes = 512 * 1024;
    private readonly object _sync = new();
    private readonly string _directory;
    private string CurrentPath => Path.Combine(_directory, "bluetooth-notify.log");

    public AppLogger(string? baseDirectory = null)
    {
        _directory = Path.Combine(baseDirectory ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BluetoothNotify"), "logs");
    }

    public void Info(string message) => Write("INF", message);
    public void Warning(string message, Exception? exception = null) => Write("WRN", message, exception);
    public void Error(string message, Exception? exception = null) => Write("ERR", message, exception);

    private void Write(string level, string message, Exception? exception = null)
    {
        try
        {
            lock (_sync)
            {
                Directory.CreateDirectory(_directory);
                RotateIfNeeded();
                var line = $"{DateTimeOffset.Now:O} [{level}] {message}";
                if (exception is not null) line += $" | {exception.GetType().Name}: {exception.Message}";
                File.AppendAllText(CurrentPath, line + Environment.NewLine);
            }
        }
        catch { /* Logging must not crash a tray process. */ }
    }

    private void RotateIfNeeded()
    {
        if (!File.Exists(CurrentPath) || new FileInfo(CurrentPath).Length < MaxBytes) return;
        for (var i = MaxFiles - 1; i >= 1; i--)
        {
            var source = i == 1 ? CurrentPath : Path.Combine(_directory, $"bluetooth-notify.{i - 1}.log");
            var target = Path.Combine(_directory, $"bluetooth-notify.{i}.log");
            if (File.Exists(source)) File.Move(source, target, true);
        }
    }
}
