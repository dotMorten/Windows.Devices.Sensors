// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Runtime.InteropServices;

namespace Windows.Devices.Sensors.Interop
{

    internal static class SensorNativeMethods
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SystemTimeToFileTime(
            ref SystemTime lpSystemTime,
            out System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime);
    }
}
