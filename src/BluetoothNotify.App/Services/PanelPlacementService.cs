namespace BluetoothNotify.App.Services;

public sealed class PanelPlacementService : IPanelPlacementService
{
    private const double GapDip = 10;

    public PanelPosition Calculate(NativeRect anchorPx, NativeRect workAreaPx, double panelWidthDip, double panelHeightDip, double dpiScale)
    {
        dpiScale = dpiScale <= 0 ? 1 : dpiScale;
        var widthPx = panelWidthDip * dpiScale;
        var heightPx = panelHeightDip * dpiScale;
        var gapPx = GapDip * dpiScale;

        var distances = new[]
        {
            (Side.Bottom, Math.Abs(anchorPx.Bottom - workAreaPx.Bottom)),
            (Side.Top, Math.Abs(anchorPx.Top - workAreaPx.Top)),
            (Side.Right, Math.Abs(anchorPx.Right - workAreaPx.Right)),
            (Side.Left, Math.Abs(anchorPx.Left - workAreaPx.Left))
        };
        var side = distances.MinBy(x => x.Item2).Item1;

        double leftPx = side switch
        {
            Side.Left => anchorPx.Right + gapPx,
            Side.Right => anchorPx.Left - widthPx - gapPx,
            _ => anchorPx.Right - widthPx
        };
        double topPx = side switch
        {
            Side.Top => anchorPx.Bottom + gapPx,
            Side.Bottom => anchorPx.Top - heightPx - gapPx,
            _ => anchorPx.Bottom - heightPx
        };

        var minLeft = side == Side.Top || side == Side.Bottom || side == Side.Left ? workAreaPx.Left : workAreaPx.Left + gapPx;
        var maxLeft = Math.Max(minLeft, workAreaPx.Right - widthPx - (side == Side.Right ? gapPx : 0));
        var minTop = side == Side.Top ? workAreaPx.Top + gapPx : workAreaPx.Top;
        var maxTop = Math.Max(minTop, workAreaPx.Bottom - heightPx - (side == Side.Bottom ? gapPx : 0));
        leftPx = Math.Clamp(leftPx, minLeft, maxLeft);
        topPx = Math.Clamp(topPx, minTop, maxTop);
        return new PanelPosition(leftPx / dpiScale, topPx / dpiScale);
    }

    private enum Side { Left, Top, Right, Bottom }
}
