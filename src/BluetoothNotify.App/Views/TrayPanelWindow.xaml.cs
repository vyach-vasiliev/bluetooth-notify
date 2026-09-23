using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using BluetoothNotify.App.Controls;
using BluetoothNotify.App.Interop;
using BluetoothNotify.App.ViewModels;

namespace BluetoothNotify.App.Views;

public partial class TrayPanelWindow : Window
{
    private bool _allowClose;
    private bool _smoothScrollingAttached;

    public TrayPanelWindow()
    {
        InitializeComponent();
        Loaded += (_, _) => AttachSmoothScrolling();
        SourceInitialized += (_, _) => ApplyWindowChrome();
        PreviewKeyDown += OnPreviewKeyDown;
        Deactivated += (_, _) => DeactivationRequested?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? DeactivationRequested;

    private void AttachSmoothScrolling()
    {
        if (_smoothScrollingAttached)
            return;

        SmoothScrollBehavior.Attach(SettingsScrollViewer);

        DeviceList.ApplyTemplate();
        if (FindVisualChild<ScrollViewer>(DeviceList) is { } scrollViewer)
            SmoothScrollBehavior.Attach(scrollViewer);

        _smoothScrollingAttached = true;
    }

    private static T? FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
    {
        for (var index = 0; index < VisualTreeHelper.GetChildrenCount(parent); index++)
        {
            var child = VisualTreeHelper.GetChild(parent, index);
            if (child is T match)
                return match;

            if (FindVisualChild<T>(child) is { } descendant)
                return descendant;
        }

        return null;
    }

    private void ApplyWindowChrome()
    {
        var handle = new WindowInteropHelper(this).Handle;
        var preference = NativeMethods.DWMWCP_ROUND;
        _ = NativeMethods.DwmSetWindowAttribute(handle, NativeMethods.DWMWA_WINDOW_CORNER_PREFERENCE, ref preference, sizeof(int));
    }

    private void OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Escape) return;
        e.Handled = true;
        if (DataContext is TrayPanelViewModel { IsSettingsOpen: true } viewModel)
        {
            viewModel.CloseSettings();
            return;
        }
        HideRequested?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? HideRequested;

    public void CloseForExit()
    {
        _allowClose = true;
        Close();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        if (!_allowClose)
        {
            e.Cancel = true;
            Hide();
            HideRequested?.Invoke(this, EventArgs.Empty);
        }
        base.OnClosing(e);
    }
}
