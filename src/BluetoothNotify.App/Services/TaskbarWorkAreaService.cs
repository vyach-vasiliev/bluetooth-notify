namespace BluetoothNotify.App.Services;

public static class TaskbarWorkAreaService
{
    public static NativeRect Adjust(NativeRect monitor, NativeRect reportedWorkArea, IEnumerable<NativeRect> taskbars, NativeRect anchor)
    {
        var candidates = taskbars.Where(x => Intersects(x, monitor)).ToArray();
        if (candidates.Length == 0) return reportedWorkArea;
        var centerX = anchor.Left + anchor.Width / 2;
        var centerY = anchor.Top + anchor.Height / 2;
        var taskbar = candidates.FirstOrDefault(x => x.Contains(centerX, centerY));
        if (taskbar.Width == 0 || taskbar.Height == 0)
            taskbar = candidates.MinBy(x => DistanceTo(x, centerX, centerY));

        var work = reportedWorkArea;
        if (taskbar.Width >= monitor.Width / 2)
        {
            if (taskbar.Top >= monitor.Top + monitor.Height / 2)
                work = work with { Bottom = Math.Min(work.Bottom, taskbar.Top) };
            else
                work = work with { Top = Math.Max(work.Top, taskbar.Bottom) };
        }
        else
        {
            if (taskbar.Left >= monitor.Left + monitor.Width / 2)
                work = work with { Right = Math.Min(work.Right, taskbar.Left) };
            else
                work = work with { Left = Math.Max(work.Left, taskbar.Right) };
        }
        return work.Width > 0 && work.Height > 0 ? work : reportedWorkArea;
    }

    private static bool Intersects(NativeRect a, NativeRect b) => a.Left < b.Right && a.Right > b.Left && a.Top < b.Bottom && a.Bottom > b.Top;

    private static double DistanceTo(NativeRect rect, int x, int y)
    {
        var dx = Math.Max(rect.Left - x, Math.Max(0, x - rect.Right));
        var dy = Math.Max(rect.Top - y, Math.Max(0, y - rect.Bottom));
        return Math.Sqrt(dx * dx + dy * dy);
    }
}
