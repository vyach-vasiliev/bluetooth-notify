using System.Windows;
using System.Windows.Media;
using BluetoothNotify.App.Infrastructure;
using BluetoothNotify.App.Models;
using BluetoothNotify.App.Services;

namespace BluetoothNotify.App.ViewModels;

public sealed class BluetoothDeviceViewModel : ObservableObject
{
    private BluetoothDeviceState _state;
    private readonly IDeviceIconResolver _icons;

    public BluetoothDeviceViewModel(BluetoothDeviceState state, IDeviceIconResolver icons) { _state = state; _icons = icons; }
    public string StableId => _state.StableId;
    public string Name => _state.Name;
    public bool IsConnected => _state.IsConnected;
    public string ConnectionText => IsConnected ? Properties.Strings.Connected : Properties.Strings.Disconnected;
    public string TransportText => _state.Transport switch
    {
        BluetoothTransport.LowEnergy => "Bluetooth LE",
        BluetoothTransport.Classic => "Bluetooth Classic",
        BluetoothTransport.LowEnergy | BluetoothTransport.Classic => "Bluetooth LE + Classic",
        _ => "Bluetooth"
    };
    public string IconGlyph => _icons.Resolve(_state.Kind);
    public int? BatteryPercent => _state.BatteryPercent;
    public double BatteryBarValue => BatteryPercent ?? 0;
    public string BatteryText => BatteryPercent is { } value ? $"{value}%" : "—";
    public string BatteryDescription => BatteryPercent is { } value
        ? !IsConnected
            ? string.Format(Properties.Strings.DisconnectedBatteryStale, value)
            : _state.DataState == DeviceDataState.Stale ? Properties.Strings.BatteryStale : BatteryText
        : Properties.Strings.BatteryUnavailable;
    public bool HasBattery => BatteryPercent is not null;
    public bool IsStale => _state.DataState == DeviceDataState.Stale;
    public string AutomationName => string.Format(Properties.Strings.StatusAutomationName, Name);
    public BatteryLevelCategory BatteryCategory => IsConnected ? BatteryLevelClassifier.GetCategory(BatteryPercent) : BatteryLevelCategory.Unknown;
    public Brush BatteryBrush => (Brush)(Application.Current?.TryFindResource($"Battery.{BatteryCategory}") ?? Brushes.Gray);

    public void Update(BluetoothDeviceState state)
    {
        _state = state;
        OnPropertyChanged(string.Empty);
    }
}
