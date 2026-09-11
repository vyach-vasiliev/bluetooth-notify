using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Interop;
using System.Windows.Threading;
using BluetoothNotify.App.Interop;
using BluetoothNotify.App.Models;

namespace BluetoothNotify.App.Services;

public sealed class TrayIconService(AppLogger logger, AppThemePreference themePreference = AppThemePreference.System) : ITrayIconService
{
    private const uint IconId = 1;
    private const int CallbackMessage = NativeMethods.WM_APP + 42;
    private readonly DispatcherTimer _pointerTimer = new(DispatcherPriority.Input) { Interval = TimeSpan.FromMilliseconds(50) };
    private readonly PrivateFontCollection _bluetoothIconFont = new();
    private HwndSource? _source;
    private NativeMethods.NotifyIconData _data;
    private readonly Dictionary<(bool LightTheme, BatteryLevelCategory Category, BluetoothIconState BluetoothState), nint> _icons = [];
    private uint _taskbarCreated;
    private NativeRect? _panelBounds;
    private DateTimeOffset? _enteredAt;
    private DateTimeOffset? _leftAt;
    private DateTimeOffset? _lastTrayMouseAt;
    private NativeRect _lastIconRect;
    private bool _hoverRaised;
    private bool _usesLightTheme;
    private BatteryLevelCategory _batteryCategory = BatteryLevelCategory.Unknown;
    private BluetoothIconState _bluetoothState = BluetoothIconState.Available;
    private DateTimeOffset _lastThemeCheckAt;
    private AppThemePreference _themePreference = themePreference;

    public event EventHandler? OpenRequested;
    public event EventHandler? ToggleRequested;
    public event EventHandler? SettingsRequested;
    public event EventHandler? ExitRequested;
    public event EventHandler? PointerEntered;
    public event EventHandler? PointerLeft;

    public void Initialize()
    {
        var parameters = new HwndSourceParameters("BluetoothNotify.TrayMessageWindow")
        {
            Width = 0,
            Height = 0,
            WindowStyle = unchecked((int)0x80000000),
            ParentWindow = new nint(-3)
        };
        _source = new HwndSource(parameters);
        _source.AddHook(WndProc);
        _taskbarCreated = NativeMethods.RegisterWindowMessage("TaskbarCreated");
        _bluetoothIconFont.AddFontFile(Path.Combine(AppContext.BaseDirectory, "Assets", "FluentSystemIcons-Bluetooth.ttf"));
        foreach (var lightTheme in new[] { false, true })
        foreach (var category in new[] { BatteryLevelCategory.Normal, BatteryLevelCategory.Low, BatteryLevelCategory.Critical })
        foreach (var bluetoothState in Enum.GetValues<BluetoothIconState>())
            _icons[(lightTheme, category, bluetoothState)] = CreateBluetoothIcon(lightTheme, category, bluetoothState);
        _usesLightTheme = ResolveUsesLightTheme();
        AddIcon();
        _pointerTimer.Tick += OnPointerTick;
        _pointerTimer.Start();
    }

    private void AddIcon()
    {
        if (_source is null) return;
        _data = new NativeMethods.NotifyIconData
        {
            cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf<NativeMethods.NotifyIconData>(),
            hWnd = _source.Handle,
            uID = IconId,
            uFlags = NativeMethods.NIF_MESSAGE | NativeMethods.NIF_ICON,
            uCallbackMessage = CallbackMessage,
            hIcon = ActiveIcon,
            szTip = string.Empty,
            szInfo = string.Empty,
            szInfoTitle = string.Empty
        };
        if (!NativeMethods.Shell_NotifyIcon(NativeMethods.NIM_ADD, ref _data)) logger.Warning("Shell_NotifyIcon(NIM_ADD) failed.");
        // Version 4 requests a rich Shell hover popup even when no tooltip text is supplied,
        // which leaves a blank region behind the panel. Version 3 keeps the icon silent.
        _data.uVersion = NativeMethods.NOTIFYICON_VERSION_3;
        NativeMethods.Shell_NotifyIcon(NativeMethods.NIM_SETVERSION, ref _data);
    }

    private nint WndProc(nint hwnd, int message, nint wParam, nint lParam, ref bool handled)
    {
        if ((uint)message == _taskbarCreated)
        {
            AddIcon();
            handled = true;
            return 0;
        }
        if (message != CallbackMessage) return 0;

        var notification = unchecked((ushort)((long)lParam & 0xFFFF));
        switch (notification)
        {
            case NativeMethods.NIN_SELECT:
            case NativeMethods.NIN_KEYSELECT:
            case NativeMethods.WM_LBUTTONUP:
                ToggleRequested?.Invoke(this, EventArgs.Empty);
                break;
            case NativeMethods.WM_CONTEXTMENU:
            case NativeMethods.WM_RBUTTONUP:
                ShowContextMenu();
                break;
            case NativeMethods.WM_MOUSEMOVE:
                _lastTrayMouseAt = DateTimeOffset.UtcNow;
                _enteredAt ??= DateTimeOffset.UtcNow;
                _leftAt = null;
                break;
        }
        handled = true;
        return 0;
    }

    private void OnPointerTick(object? sender, EventArgs e)
    {
        UpdateIconForTheme();
        if (!NativeMethods.GetCursorPos(out var point)) return;
        var now = DateTimeOffset.UtcNow;
        var overIcon = GetIconRect().Inflate(4).Contains(point.X, point.Y)
            || (_lastTrayMouseAt is { } lastTrayMouse && now - lastTrayMouse < TimeSpan.FromMilliseconds(150));
        var overPanel = _panelBounds?.Contains(point.X, point.Y) == true;
        if (overIcon)
        {
            _enteredAt ??= now;
            _leftAt = null;
            if (!_hoverRaised && now - _enteredAt >= TimeSpan.FromMilliseconds(300))
            {
                _hoverRaised = true;
                PointerEntered?.Invoke(this, EventArgs.Empty);
            }
            return;
        }
        _enteredAt = null;
        if (overPanel) { _leftAt = null; return; }
        if (!_hoverRaised && _panelBounds is null) return;
        _leftAt ??= now;
        if (now - _leftAt >= TimeSpan.FromMilliseconds(425))
        {
            _hoverRaised = false;
            _leftAt = null;
            PointerLeft?.Invoke(this, EventArgs.Empty);
        }
    }

    private void ShowContextMenu()
    {
        if (_source is null || !NativeMethods.GetCursorPos(out var point)) return;
        var menu = NativeMethods.CreatePopupMenu();
        try
        {
            NativeMethods.AppendMenu(menu, NativeMethods.MF_STRING, 1, Properties.Strings.Open);
            NativeMethods.AppendMenu(menu, NativeMethods.MF_STRING, 4, Properties.Strings.Settings);
            NativeMethods.AppendMenu(menu, NativeMethods.MF_SEPARATOR, 0, null);
            NativeMethods.AppendMenu(menu, NativeMethods.MF_STRING, 3, Properties.Strings.Exit);
            NativeMethods.SetForegroundWindow(_source.Handle);
            var command = NativeMethods.TrackPopupMenuEx(menu, NativeMethods.TPM_RIGHTBUTTON | NativeMethods.TPM_RETURNCMD, point.X, point.Y, _source.Handle, 0);
            if (command == 1) OpenRequested?.Invoke(this, EventArgs.Empty);
            else if (command == 4) SettingsRequested?.Invoke(this, EventArgs.Empty);
            else if (command == 3) ExitRequested?.Invoke(this, EventArgs.Empty);
        }
        finally { NativeMethods.DestroyMenu(menu); }
    }

    public NativeRect GetIconRect()
    {
        if (_source is null) return default;
        var id = new NativeMethods.NotifyIconIdentifier
        {
            cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf<NativeMethods.NotifyIconIdentifier>(),
            hWnd = _source.Handle,
            uID = IconId
        };
        if (NativeMethods.Shell_NotifyIconGetRect(ref id, out var rect) == 0)
            _lastIconRect = NativeMethods.ToNative(rect);
        return _lastIconRect;
    }

    public void SetPanelBounds(NativeRect? bounds) => _panelBounds = bounds;

    public void SetBatteryStatus(int? minimumBatteryPercent)
    {
        var category = BatteryLevelClassifier.GetCategory(minimumBatteryPercent);
        if (category == _batteryCategory) return;
        _batteryCategory = category;
        UpdateDisplayedIcon();
    }

    public void SetBluetoothStatus(bool isBluetoothAvailable, bool hasConnectedDevices)
    {
        var state = ResolveBluetoothIconState(isBluetoothAvailable, hasConnectedDevices);
        if (state == _bluetoothState) return;
        _bluetoothState = state;
        UpdateDisplayedIcon();
    }

    internal static BluetoothIconState ResolveBluetoothIconState(bool isBluetoothAvailable, bool hasConnectedDevices) =>
        !isBluetoothAvailable
            ? BluetoothIconState.Disconnected
            : hasConnectedDevices
                ? BluetoothIconState.Connected
                : BluetoothIconState.Available;

    internal static string ResolveBluetoothIconGlyph(BluetoothIconState state) => state switch
    {
        BluetoothIconState.Connected => "\uF1E0", // Bluetooth Connected
        BluetoothIconState.Disconnected => "\uF1E1", // Bluetooth Disabled
        _ => "\uE702" // Bluetooth
    };

    public void SetThemePreference(AppThemePreference preference)
    {
        _themePreference = preference;
        _lastThemeCheckAt = DateTimeOffset.MinValue;
        UpdateIconForTheme();
    }

    private BatteryLevelCategory IconBatteryCategory => _batteryCategory is BatteryLevelCategory.Low or BatteryLevelCategory.Critical
        ? _batteryCategory
        : BatteryLevelCategory.Normal;

    private nint ActiveIcon => _icons.GetValueOrDefault((_usesLightTheme, IconBatteryCategory, _bluetoothState));

    private void UpdateIconForTheme()
    {
        var now = DateTimeOffset.UtcNow;
        if (now - _lastThemeCheckAt < TimeSpan.FromSeconds(2)) return;
        _lastThemeCheckAt = now;
        var usesLightTheme = ResolveUsesLightTheme();
        if (usesLightTheme == _usesLightTheme) return;

        _usesLightTheme = usesLightTheme;
        UpdateDisplayedIcon();
    }

    private void UpdateDisplayedIcon()
    {
        if (_source is null) return;
        var update = _data;
        update.hIcon = ActiveIcon;
        update.uFlags = NativeMethods.NIF_ICON;
        if (!NativeMethods.Shell_NotifyIcon(NativeMethods.NIM_MODIFY, ref update))
            logger.Warning("Tray icon appearance update failed.");
        else
            _data.hIcon = update.hIcon;
    }

    private bool ResolveUsesLightTheme() =>
        ThemeManager.ShouldUseLightTheme(_themePreference, ThemeManager.IsWindowsAppLightTheme());

    private nint CreateBluetoothIcon(
        bool lightTheme,
        BatteryLevelCategory batteryCategory,
        BluetoothIconState bluetoothState)
    {
        const int size = 64;
        using var bitmap = new Bitmap(size, size, PixelFormat.Format32bppArgb);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        graphics.Clear(Color.Transparent);
        using var foreground = new SolidBrush(lightTheme
            ? Color.FromArgb(255, 16, 82, 166)
            : Color.FromArgb(255, 76, 147, 255));
        using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        var bounds = new RectangleF(0, -2, size, size + 4);
        DrawBluetoothGlyph(graphics, foreground, bluetoothState, size);

        if (batteryCategory is BatteryLevelCategory.Low or BatteryLevelCategory.Critical)
        {
            using var status = new SolidBrush(GetStatusDotColor(lightTheme, batteryCategory));
            using var statusFont = new Font("Segoe Fluent Icons", 56, FontStyle.Regular, GraphicsUnit.Pixel);
            // UpdateStatusDot2 is designed to align with another Fluent icon when both
            // glyphs share the same font size and bounds.
            graphics.DrawString("\uEC83", statusFont, status, bounds, format);
        }
        return bitmap.GetHicon();
    }

    private void DrawBluetoothGlyph(
        Graphics graphics,
        Brush foreground,
        BluetoothIconState bluetoothState,
        int canvasSize)
    {
        const float targetHeight = 56;
        var familyName = bluetoothState == BluetoothIconState.Available
            ? "Segoe Fluent Icons"
            : _bluetoothIconFont.Families[0].Name;
        var fontCollection = bluetoothState == BluetoothIconState.Available ? null : _bluetoothIconFont;
        using var family = fontCollection is null
            ? new FontFamily(familyName)
            : new FontFamily(familyName, fontCollection);
        using var path = new GraphicsPath();
        using var glyphFormat = (StringFormat)StringFormat.GenericTypographic.Clone();
        path.AddString(
            ResolveBluetoothIconGlyph(bluetoothState),
            family,
            (int)FontStyle.Regular,
            canvasSize,
            PointF.Empty,
            glyphFormat);

        var glyphBounds = path.GetBounds();
        var scale = targetHeight / glyphBounds.Height;
        var left = (canvasSize - glyphBounds.Width * scale) / 2;
        var top = (canvasSize - targetHeight) / 2;
        using var transform = new Matrix(
            scale,
            0,
            0,
            scale,
            left - glyphBounds.Left * scale,
            top - glyphBounds.Top * scale);
        path.Transform(transform);
        graphics.FillPath(foreground, path);
    }

    private static Color GetStatusDotColor(bool lightTheme, BatteryLevelCategory category) => category switch
    {
        BatteryLevelCategory.Critical => lightTheme
            ? Color.FromArgb(255, 200, 58, 71)
            : Color.FromArgb(255, 240, 90, 103),
        _ => Color.FromArgb(255, 232, 192, 0)
    };

    public void Dispose()
    {
        _pointerTimer.Stop();
        if (_source is not null)
        {
            NativeMethods.Shell_NotifyIcon(NativeMethods.NIM_DELETE, ref _data);
            _source.RemoveHook(WndProc);
            _source.Dispose();
            _source = null;
        }
        foreach (var icon in _icons.Values)
            if (icon != 0) NativeMethods.DestroyIcon(icon);
        _icons.Clear();
        _bluetoothIconFont.Dispose();
    }
}
