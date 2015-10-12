using System.Runtime.InteropServices;
using System.Security;

namespace com.bcrusu.mesosclr.Native
{
    internal unsafe class SchedulerCallbacksSig
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void Registered(long managedDriverId, NativeArray* frameworkId, NativeArray* masterInfo);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void Reregistered(long managedDriverId, NativeArray* masterInfo);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void ResourceOffers(long managedDriverId, NativeArray* offers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void OfferRescinded(long managedDriverId, NativeArray* offerId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void StatusUpdate(long managedDriverId, NativeArray* status);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void FrameworkMessage(long managedDriverId, NativeArray* executorId, NativeArray* slaveId, NativeArray* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void Disconnected(long managedDriverId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void SlaveLost(long managedDriverId, NativeArray* slaveId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void ExecutorLost(long managedDriverId, NativeArray* executorId, NativeArray* slaveId, int status);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void Error(long managedDriverId, [MarshalAs(UnmanagedType.LPStr)] string message);
    }
}
