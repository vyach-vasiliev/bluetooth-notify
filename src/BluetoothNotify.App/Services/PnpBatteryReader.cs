using System.Runtime.InteropServices;
using System.Text;

namespace BluetoothNotify.App.Services;

/// <summary>
/// Best-effort fallback for the PnP battery value used by Windows Settings.
/// The property key is not part of Microsoft's public API surface, so every
/// failure is treated as "unavailable" and never affects the documented paths.
/// </summary>
public sealed class PnpBatteryReader(AppLogger logger) : IPnpBatteryReader
{
    private const uint DigcfPresent = 0x00000002;
    private const uint DigcfAllClasses = 0x00000004;
    private const uint DevpropTypeMask = 0x00000FFF;
    private const uint DevpropTypeByte = 0x00000003;
    private const uint DevpropTypeGuid = 0x0000000D;
    private const int ErrorNoMoreItems = 259;
    private static readonly nint InvalidHandleValue = new(-1);
    private static DevPropKey DeviceBatteryLevel = new(new Guid("104EA319-6EE2-4701-BD47-8DDBF425BBE5"), 2);
    private static DevPropKey DeviceContainerId = new(new Guid("8C7ED206-3F8A-4827-B3AB-AE9E1FAEFC6C"), 2);

    public int? TryRead(ulong? bluetoothAddress, string? containerId)
    {
        var deviceSet = SetupDiGetClassDevs(0, null, 0, DigcfPresent | DigcfAllClasses);
        if (deviceSet == InvalidHandleValue)
        {
            logger.Warning($"PnP battery enumeration failed (Win32 {Marshal.GetLastWin32Error()}).");
            return null;
        }

        try
        {
            var address = bluetoothAddress?.ToString("X12", System.Globalization.CultureInfo.InvariantCulture);
            var hasContainer = Guid.TryParse(containerId, out var containerGuid);
            if (address is null && !hasContainer) return null;
            var values = new List<int>();
            for (uint index = 0; ; index++)
            {
                var device = new SpDevInfoData { cbSize = (uint)Marshal.SizeOf<SpDevInfoData>() };
                if (!SetupDiEnumDeviceInfo(deviceSet, index, ref device))
                {
                    var error = Marshal.GetLastWin32Error();
                    if (error != ErrorNoMoreItems)
                        logger.Warning($"PnP battery enumeration stopped unexpectedly (Win32 {error}).");
                    break;
                }

                var instanceId = new StringBuilder(512);
                var addressMatches = address is not null
                    && SetupDiGetDeviceInstanceId(deviceSet, ref device, instanceId, instanceId.Capacity, out _)
                    && instanceId.ToString().IndexOf(address, StringComparison.OrdinalIgnoreCase) >= 0;
                if (!addressMatches && (!hasContainer || !HasContainer(deviceSet, ref device, containerGuid)))
                    continue;

                var buffer = new byte[1];
                var key = DeviceBatteryLevel;
                if (!SetupDiGetDeviceProperty(deviceSet, ref device, ref key, out var propertyType,
                        buffer, (uint)buffer.Length, out var requiredSize, 0)
                    || (propertyType & DevpropTypeMask) != DevpropTypeByte
                    || requiredSize < 1
                    || buffer[0] > 100)
                    continue;

                values.Add(buffer[0]);
            }

            // A coordinated set may expose more than one matching node. The minimum
            // is the safest single value for a compact one-row representation.
            return values.Count == 0 ? null : values.Min();
        }
        catch (Exception ex)
        {
            logger.Warning("PnP battery read failed.", ex);
            return null;
        }
        finally
        {
            _ = SetupDiDestroyDeviceInfoList(deviceSet);
        }
    }

    private static bool HasContainer(nint deviceSet, ref SpDevInfoData device, Guid expected)
    {
        var buffer = new byte[16];
        var key = DeviceContainerId;
        return SetupDiGetDeviceProperty(deviceSet, ref device, ref key, out var propertyType,
                   buffer, (uint)buffer.Length, out var requiredSize, 0)
               && (propertyType & DevpropTypeMask) == DevpropTypeGuid
               && requiredSize >= buffer.Length
               && new Guid(buffer) == expected;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct DevPropKey(Guid formatId, uint propertyId)
    {
        internal Guid FormatId = formatId;
        internal uint PropertyId = propertyId;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct SpDevInfoData
    {
        internal uint cbSize;
        internal Guid ClassGuid;
        internal uint DevInst;
        internal nuint Reserved;
    }

    [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern nint SetupDiGetClassDevs(nint classGuid, string? enumerator, nint parentWindow, uint flags);

    [DllImport("setupapi.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetupDiEnumDeviceInfo(nint deviceInfoSet, uint memberIndex, ref SpDevInfoData deviceInfoData);

    [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetupDiGetDeviceInstanceId(nint deviceInfoSet, ref SpDevInfoData deviceInfoData,
        StringBuilder deviceInstanceId, int deviceInstanceIdSize, out int requiredSize);

    [DllImport("setupapi.dll", EntryPoint = "SetupDiGetDevicePropertyW", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetupDiGetDeviceProperty(nint deviceInfoSet, ref SpDevInfoData deviceInfoData,
        ref DevPropKey propertyKey, out uint propertyType, [Out] byte[] propertyBuffer,
        uint propertyBufferSize, out uint requiredSize, uint flags);

    [DllImport("setupapi.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetupDiDestroyDeviceInfoList(nint deviceInfoSet);
}
