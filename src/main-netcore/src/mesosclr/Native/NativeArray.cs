using System;
using System.Runtime.InteropServices;

namespace mesosclr.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeArray
    {
        public int Length;

        public IntPtr Items;
    }
}
