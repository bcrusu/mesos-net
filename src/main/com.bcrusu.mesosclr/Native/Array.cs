using System;
using System.Runtime.InteropServices;

namespace com.bcrusu.mesosclr.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Array
    {
        public int Length;

        public IntPtr Items;
    }
}
