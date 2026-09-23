using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BluetoothNotify.App.Controls;

internal static class SmoothScrollBehavior
{
    private const double PixelsPerWheelNotch = 64;
    private const double SmoothingMilliseconds = 75;

    public static void Attach(ScrollViewer scrollViewer)
    {
        scrollViewer.PreviewMouseWheel += OnPreviewMouseWheel;
    }

    private static readonly ConditionalWeakTable<ScrollViewer, AnimationState> States = new();

    private static void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (sender is not ScrollViewer scrollViewer || scrollViewer.ScrollableHeight <= 0)
            return;

        var state = States.GetValue(scrollViewer, _ => new AnimationState());
        var currentOffset = scrollViewer.VerticalOffset;
        var startingOffset = state.IsAnimating ? state.TargetOffset : currentOffset;
        state.TargetOffset = Math.Clamp(
            startingOffset - e.Delta / 120d * PixelsPerWheelNotch,
            0,
            scrollViewer.ScrollableHeight);

        if (!state.IsAnimating)
        {
            state.IsAnimating = true;
            state.Clock.Restart();
            CompositionTarget.Rendering += state.RenderingHandler ??= (_, _) => Animate(scrollViewer, state);
        }

        e.Handled = true;
    }

    private static void Animate(ScrollViewer scrollViewer, AnimationState state)
    {
        var elapsedMilliseconds = state.Clock.Elapsed.TotalMilliseconds;
        state.Clock.Restart();

        var currentOffset = scrollViewer.VerticalOffset;
        var amount = 1 - Math.Exp(-elapsedMilliseconds / SmoothingMilliseconds);
        var nextOffset = currentOffset + (state.TargetOffset - currentOffset) * amount;

        if (Math.Abs(state.TargetOffset - nextOffset) < 0.5)
        {
            nextOffset = state.TargetOffset;
            state.IsAnimating = false;
            CompositionTarget.Rendering -= state.RenderingHandler;
        }

        scrollViewer.ScrollToVerticalOffset(nextOffset);
    }

    private sealed class AnimationState
    {
        public Stopwatch Clock { get; } = new();
        public double TargetOffset { get; set; }
        public bool IsAnimating { get; set; }
        public EventHandler? RenderingHandler { get; set; }
    }
}
