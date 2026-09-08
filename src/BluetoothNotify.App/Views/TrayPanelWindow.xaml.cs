using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using BluetoothNotify.App.Interop;
using BluetoothNotify.App.ViewModels;

namespace BluetoothNotify.App.Views;

public partial class TrayPanelWindow : Window
{
    private bool _allowClose;

    public TrayPanelWindow()
    {
        InitializeComponent();
        SourceInitialized += (_, _) => ApplyWindowChrome();
        PreviewKeyDown += OnPreviewKeyDown;
        Deactivated += (_, _) => DeactivationRequested?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? DeactivationRequested;

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
