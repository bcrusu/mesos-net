using System;
using System.Runtime.InteropServices;

namespace mesosclr.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SchedulerInterface
    {
        public IntPtr Registered;
        public IntPtr Reregistered;
        public IntPtr ResourceOffers;
        public IntPtr OfferRescinded;
        public IntPtr StatusUpdate;
        public IntPtr FrameworkMessage;
        public IntPtr Disconnected;
        public IntPtr SlaveLost;
        public IntPtr ExecutorLost;
        public IntPtr Error;
    }
}
