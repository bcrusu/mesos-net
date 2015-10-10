using System;
using System.Runtime.InteropServices;

namespace com.bcrusu.mesosclr.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeArray
    {
        public int Length;

        public IntPtr Items;
    }
}
