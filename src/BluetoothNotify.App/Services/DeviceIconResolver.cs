using BluetoothNotify.App.Models;

namespace BluetoothNotify.App.Services;

public sealed class DeviceIconResolver : IDeviceIconResolver
{
    // Segoe Fluent Icons glyphs available in Windows 11.
    public string Resolve(BluetoothDeviceKind kind) => kind switch
    {
        BluetoothDeviceKind.Headphones => "\uE7F6",
        BluetoothDeviceKind.Mouse => "\uE962",
        BluetoothDeviceKind.Keyboard => "\uE765",
        BluetoothDeviceKind.Speaker => "\uE767",
        BluetoothDeviceKind.Gamepad => "\uE7FC",
        BluetoothDeviceKind.Phone => "\uE8EA",
        BluetoothDeviceKind.Pen => "\uED5B",
        BluetoothDeviceKind.Wearable => "\uE916",
        _ => "\uE702"
    };

    public static BluetoothDeviceKind Infer(string? classId, IEnumerable<string>? categories, string? name)
    {
        var metadata = string.Join(' ', categories ?? []).ToLowerInvariant();
        var classValue = classId?.ToLowerInvariant() ?? string.Empty;
        if (metadata.Contains("keyboard") || classValue.Contains("keyboard")) return BluetoothDeviceKind.Keyboard;
        if (metadata.Contains("mouse") || classValue.Contains("mouse")) return BluetoothDeviceKind.Mouse;
        if (metadata.Contains("headset") || metadata.Contains("headphone") || classValue.Contains("audioendpoint")) return BluetoothDeviceKind.Headphones;
        if (metadata.Contains("speaker") || metadata.Contains("audio")) return BluetoothDeviceKind.Speaker;
        if (metadata.Contains("game") || classValue.Contains("gamecontroller")) return BluetoothDeviceKind.Gamepad;
        if (metadata.Contains("phone") || classValue.Contains("phone")) return BluetoothDeviceKind.Phone;
        if (metadata.Contains("pen") || classValue.Contains("pen")) return BluetoothDeviceKind.Pen;
        if (metadata.Contains("wearable") || metadata.Contains("watch")) return BluetoothDeviceKind.Wearable;

        var fallback = name?.ToLowerInvariant() ?? string.Empty;
        if (fallback.Contains("keyboard") || fallback.Contains("клавиат")) return BluetoothDeviceKind.Keyboard;
        if (fallback.Contains("mouse") || fallback.Contains("мыш")) return BluetoothDeviceKind.Mouse;
        if (fallback.Contains("head") || fallback.Contains("buds") || fallback.Contains("науш")) return BluetoothDeviceKind.Headphones;
        if (fallback.Contains("speaker") || fallback.Contains("колон")) return BluetoothDeviceKind.Speaker;
        if (fallback.Contains("gamepad") || fallback.Contains("controller")) return BluetoothDeviceKind.Gamepad;
        if (fallback.Contains("phone") || fallback.Contains("телефон")) return BluetoothDeviceKind.Phone;
        if (fallback.Contains("watch") || fallback.Contains("часы")) return BluetoothDeviceKind.Wearable;
        return BluetoothDeviceKind.Unknown;
    }
}
