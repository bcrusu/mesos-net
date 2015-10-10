using System;
using System.Runtime.InteropServices;

namespace com.bcrusu.mesosclr.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ExecutorInterface
    {
        public IntPtr Registered;
        public IntPtr Reregistered;
        public IntPtr Disconnected;
        public IntPtr LaunchTask;
        public IntPtr KillTask;
        public IntPtr FrameworkMessage;
        public IntPtr Shutdown;
        public IntPtr Error;
    }
}
