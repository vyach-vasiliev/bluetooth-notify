using BluetoothNotify.App.Services;

namespace BluetoothNotify.Tests;

public sealed class PanelPlacementServiceTests
{
    private readonly PanelPlacementService _service = new();

    [Theory]
    [InlineData(1.0)]
    [InlineData(1.25)]
    [InlineData(1.5)]
    [InlineData(2.0)]
    public void BottomTaskbar_PanelStaysInsideWorkArea(double scale)
    {
        var work = new NativeRect(0, 0, (int)(1920 * scale), (int)(1040 * scale));
        var anchor = new NativeRect((int)(1840 * scale), (int)(1050 * scale), (int)(1870 * scale), (int)(1080 * scale));
        var result = _service.Calculate(anchor, work, 448, 600, scale);
        Assert.InRange(result.LeftDip, 0, 1920 - 448);
        Assert.InRange(result.TopDip, 0, 1040 - 600);
    }

    [Fact]
    public void LeftTaskbar_PanelAppearsToTheRight()
    {
        var result = _service.Calculate(new NativeRect(4, 800, 44, 840), new NativeRect(48, 0, 1920, 1080), 448, 600, 1);
        Assert.True(result.LeftDip >= 48);
        Assert.InRange(result.TopDip, 0, 480);
    }

    [Fact]
    public void NegativeMonitorCoordinates_ArePreservedAndClamped()
    {
        var result = _service.Calculate(new NativeRect(-80, 1000, -48, 1032), new NativeRect(-1920, 0, 0, 1040), 448, 600, 1.5);
        Assert.InRange(result.LeftDip, -1280, -448);
        Assert.InRange(result.TopDip, 0, 693.34 - 400);
    }
}
