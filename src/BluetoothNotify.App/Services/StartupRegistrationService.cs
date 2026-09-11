using Microsoft.Win32;

namespace BluetoothNotify.App.Services;

public sealed class StartupRegistrationService(string? executablePath = null) : IStartupRegistrationService
{
    private const string RunKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private const string PreferencesKeyPath = @"Software\BluetoothNotify";
    private const string ValueName = "Bluetooth Notify";
    private const string PreferenceValueName = "RunAtStartup";
    private readonly string _command = BuildCommand(executablePath ?? Environment.ProcessPath);

    public bool IsEnabled()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RunKeyPath, writable: false);
        return key?.GetValue(ValueName) is string value &&
            string.Equals(value, _command, StringComparison.OrdinalIgnoreCase);
    }

    public void SetEnabled(bool enabled)
    {
        using var runKey = Registry.CurrentUser.CreateSubKey(RunKeyPath, writable: true)
            ?? throw new InvalidOperationException("The current-user startup registry key could not be opened.");

        if (enabled)
            runKey.SetValue(ValueName, _command, RegistryValueKind.String);
        else
            runKey.DeleteValue(ValueName, throwOnMissingValue: false);

        using var preferencesKey = Registry.CurrentUser.CreateSubKey(PreferencesKeyPath, writable: true)
            ?? throw new InvalidOperationException("The Bluetooth Notify preferences registry key could not be opened.");
        preferencesKey.SetValue(PreferenceValueName, enabled ? 1 : 0, RegistryValueKind.DWord);
    }

    internal static string BuildCommand(string? executablePath)
    {
        if (string.IsNullOrWhiteSpace(executablePath))
            throw new InvalidOperationException("The application executable path is unavailable.");

        return $"\"{executablePath}\" --autostart";
    }
}
