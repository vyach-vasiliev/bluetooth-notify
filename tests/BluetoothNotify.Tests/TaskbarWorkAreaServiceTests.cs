using BluetoothNotify.App.Services;

namespace BluetoothNotify.Tests;

public sealed class TaskbarWorkAreaServiceTests
{
    [Fact]
    public void ExpandedAutoHideTaskbar_ReducesReportedWorkArea()
    {
        var monitor = new NativeRect(0, 0, 3120, 2080);
        var reported = new NativeRect(0, 0, 3120, 2062);
        var expandedTaskbar = new NativeRect(0, 1984, 3120, 2080);
        var anchor = new NativeRect(2950, 2005, 2990, 2045);

        var result = TaskbarWorkAreaService.Adjust(monitor, reported, [expandedTaskbar], anchor);

        Assert.Equal(1984, result.Bottom);
    }

    [Fact]
    public void LeftTaskbar_RaisesWorkAreaLeftEdge()
    {
        var result = TaskbarWorkAreaService.Adjust(
            new NativeRect(-1920, 0, 0, 1080),
            new NativeRect(-1918, 0, 0, 1080),
            [new NativeRect(-1920, 0, -1840, 1080)],
            new NativeRect(-1900, 500, -1860, 540));

        Assert.Equal(-1840, result.Left);
    }
}
