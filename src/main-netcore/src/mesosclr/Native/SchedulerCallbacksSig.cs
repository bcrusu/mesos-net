using System.Runtime.InteropServices;

namespace mesosclr.Native
{
    internal unsafe class SchedulerCallbacksSig
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Registered(long managedDriverId, NativeArray* frameworkId, NativeArray* masterInfo);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Reregistered(long managedDriverId, NativeArray* masterInfo);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ResourceOffers(long managedDriverId, NativeArray* offers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void OfferRescinded(long managedDriverId, NativeArray* offerId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void StatusUpdate(long managedDriverId, NativeArray* status);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FrameworkMessage(long managedDriverId, NativeArray* executorId, NativeArray* slaveId, NativeArray* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Disconnected(long managedDriverId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SlaveLost(long managedDriverId, NativeArray* slaveId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ExecutorLost(long managedDriverId, NativeArray* executorId, NativeArray* slaveId, int status);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Error(long managedDriverId, [MarshalAs(UnmanagedType.LPStr)] string message);
    }
}
