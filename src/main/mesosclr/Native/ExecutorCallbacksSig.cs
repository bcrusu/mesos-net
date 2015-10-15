using System.Runtime.InteropServices;
using System.Security;

namespace mesosclr.Native
{
    internal unsafe class ExecutorCallbacksSig
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void Registered(long managedDriverId, NativeArray* executorInfo, NativeArray* frameworkInfo, NativeArray* slaveInfo);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void Reregistered(long managedDriverId, NativeArray* slaveInfo);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void Disconnected(long managedDriverId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void LaunchTask(long managedDriverId, NativeArray* taskInfo);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void KillTask(long managedDriverId, NativeArray* taskId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void FrameworkMessage(long managedDriverId, NativeArray* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void Shutdown(long managedDriverId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public delegate void Error(long managedDriverId, [MarshalAs(UnmanagedType.LPStr)] string message);
    }
}
