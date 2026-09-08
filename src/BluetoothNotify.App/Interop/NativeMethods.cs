using System.Runtime.InteropServices;
using System.Text;
using BluetoothNotify.App.Services;

namespace BluetoothNotify.App.Interop;

internal static class NativeMethods
{
    internal const int WM_APP = 0x8000;
    internal const int WM_USER = 0x0400;
    internal const int WM_MOUSEMOVE = 0x0200;
    internal const int WM_LBUTTONUP = 0x0202;
    internal const int WM_RBUTTONUP = 0x0205;
    internal const int WM_CONTEXTMENU = 0x007B;
    internal const int NIN_SELECT = WM_USER;
    internal const int NIN_KEYSELECT = WM_USER + 1;
    internal const uint NIM_ADD = 0;
    internal const uint NIM_MODIFY = 1;
    internal const uint NIM_DELETE = 2;
    internal const uint NIM_SETVERSION = 4;
    internal const uint NOTIFYICON_VERSION_4 = 4;
    internal const uint NOTIFYICON_VERSION_3 = 3;
    internal const uint NIF_MESSAGE = 0x1;
    internal const uint NIF_ICON = 0x2;
    internal const uint NIF_TIP = 0x4;
    internal const uint NIF_SHOWTIP = 0x80;
    internal const uint TPM_RIGHTBUTTON = 0x0002;
    internal const uint TPM_RETURNCMD = 0x0100;
    internal const uint MF_STRING = 0x0000;
    internal const uint MF_SEPARATOR = 0x0800;
    internal const uint MONITOR_DEFAULTTONEAREST = 2;
    internal const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
    internal const int DWMWCP_ROUND = 2;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct NotifyIconData
    {
        internal uint cbSize;
        internal nint hWnd;
        internal uint uID;
        internal uint uFlags;
        internal uint uCallbackMessage;
        internal nint hIcon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal string szTip;
        internal uint dwState;
        internal uint dwStateMask;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] internal string szInfo;
        internal uint uVersion;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)] internal string szInfoTitle;
        internal uint dwInfoFlags;
        internal Guid guidItem;
        internal nint hBalloonIcon;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct NotifyIconIdentifier
    {
        internal uint cbSize;
        internal nint hWnd;
        internal uint uID;
        internal Guid guidItem;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Rect { internal int Left, Top, Right, Bottom; }
    [StructLayout(LayoutKind.Sequential)]
    internal struct Point { internal int X, Y; }
    [StructLayout(LayoutKind.Sequential)]
    internal struct MonitorInfo { internal uint cbSize; internal Rect rcMonitor; internal Rect rcWork; internal uint dwFlags; }
    internal delegate bool EnumWindowsProc(nint hwnd, nint lParam);

    [DllImport("shell32.dll", CharSet = CharSet.Unicode)] [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool Shell_NotifyIcon(uint message, ref NotifyIconData data);
    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    internal static extern int SetCurrentProcessExplicitAppUserModelID(string appId);
    [DllImport("shell32.dll")] internal static extern int Shell_NotifyIconGetRect(ref NotifyIconIdentifier identifier, out Rect iconLocation);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] internal static extern uint RegisterWindowMessage(string message);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static extern bool GetCursorPos(out Point point);
    [DllImport("user32.dll")] internal static extern nint CreatePopupMenu();
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] [return: MarshalAs(UnmanagedType.Bool)] internal static extern bool AppendMenu(nint menu, uint flags, nuint id, string? text);
    [DllImport("user32.dll")] internal static extern uint TrackPopupMenuEx(nint menu, uint flags, int x, int y, nint owner, nint parameters);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static extern bool DestroyMenu(nint menu);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static extern bool SetForegroundWindow(nint hwnd);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static extern bool DestroyIcon(nint icon);
    [DllImport("user32.dll")] internal static extern nint MonitorFromRect(ref Rect rect, uint flags);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static extern bool GetMonitorInfo(nint monitor, ref MonitorInfo info);
    [DllImport("user32.dll")] internal static extern uint GetDpiForWindow(nint hwnd);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static extern bool EnumWindows(EnumWindowsProc callback, nint lParam);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static extern bool IsWindowVisible(nint hwnd);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static extern bool GetWindowRect(nint hwnd, out Rect rect);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] internal static extern int GetClassName(nint hwnd, StringBuilder className, int maxCount);
    [DllImport("dwmapi.dll")] internal static extern int DwmSetWindowAttribute(nint hwnd, int attribute, ref int value, int valueSize);

    internal static NativeRect ToNative(Rect rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);
}
