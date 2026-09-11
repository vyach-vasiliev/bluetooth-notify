using BluetoothNotify.App.Services;

namespace BluetoothNotify.Tests;

public sealed class StartupRegistrationServiceTests
{
    [Fact]
    public void BuildCommand_QuotesExecutablePathAndAddsStartupArgument()
    {
        const string executablePath = @"C:\Users\Example User\AppData\Local\Programs\Bluetooth Notify\BluetoothNotify.App.exe";

        var result = StartupRegistrationService.BuildCommand(executablePath);

        Assert.Equal($"\"{executablePath}\" --autostart", result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void BuildCommand_RejectsMissingExecutablePath(string? executablePath)
    {
        Assert.Throws<InvalidOperationException>(() => StartupRegistrationService.BuildCommand(executablePath));
    }
}
