using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace BluetoothNotify.App.Controls;

public sealed class ConnectionStatusIndicator : Grid
{
    private readonly ToolTip _statusToolTip;

    public ConnectionStatusIndicator()
    {
        _statusToolTip = new ToolTip
        {
            Placement = PlacementMode.Bottom,
            PlacementTarget = this,
            HorizontalOffset = 0,
            VerticalOffset = 5,
            StaysOpen = true
        };
    }

    public static readonly DependencyProperty DeviceNameProperty = DependencyProperty.Register(
        nameof(DeviceName), typeof(string), typeof(ConnectionStatusIndicator), new FrameworkPropertyMetadata(string.Empty, OnAutomationValueChanged));

    public static readonly DependencyProperty IsConnectedStateProperty = DependencyProperty.Register(
        nameof(IsConnectedState), typeof(bool), typeof(ConnectionStatusIndicator), new FrameworkPropertyMetadata(false, OnAutomationValueChanged));

    public string DeviceName { get => (string)GetValue(DeviceNameProperty); set => SetValue(DeviceNameProperty, value); }
    public bool IsConnectedState { get => (bool)GetValue(IsConnectedStateProperty); set => SetValue(IsConnectedStateProperty, value); }

    protected override AutomationPeer OnCreateAutomationPeer() => new ConnectionStatusAutomationPeer(this);

    protected override void OnMouseEnter(MouseEventArgs e)
    {
        base.OnMouseEnter(e);
        _statusToolTip.Content = IsConnectedState ? Properties.Strings.Connected : Properties.Strings.Disconnected;
        _statusToolTip.IsOpen = true;
    }

    protected override void OnMouseLeave(MouseEventArgs e)
    {
        _statusToolTip.IsOpen = false;
        base.OnMouseLeave(e);
    }

    protected override void OnVisualParentChanged(DependencyObject oldParent)
    {
        if (VisualParent is null)
            _statusToolTip.IsOpen = false;

        base.OnVisualParentChanged(oldParent);
    }

    private static void OnAutomationValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        if (UIElementAutomationPeer.FromElement((UIElement)dependencyObject) is ConnectionStatusAutomationPeer peer)
            peer.RaiseValueChanged();
    }

    private sealed class ConnectionStatusAutomationPeer(ConnectionStatusIndicator owner) : FrameworkElementAutomationPeer(owner)
    {
        private ConnectionStatusIndicator Indicator => (ConnectionStatusIndicator)Owner;
        protected override string GetNameCore() => string.Format(Properties.Strings.StatusAutomationName, Indicator.DeviceName);
        protected override string GetHelpTextCore() => Indicator.IsConnectedState ? Properties.Strings.Connected : Properties.Strings.Disconnected;
        protected override AutomationControlType GetAutomationControlTypeCore() => AutomationControlType.Text;
        protected override string GetClassNameCore() => nameof(ConnectionStatusIndicator);
        protected override bool IsContentElementCore() => true;
        protected override bool IsControlElementCore() => true;

        public void RaiseValueChanged()
        {
            RaisePropertyChangedEvent(AutomationElementIdentifiers.NameProperty, null, GetNameCore());
            RaisePropertyChangedEvent(AutomationElementIdentifiers.HelpTextProperty, null, GetHelpTextCore());
        }
    }
}
