using System.Runtime.InteropServices;

namespace mesosclr.Native
{
    internal unsafe class ExecutorCallbacksSig
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Registered(long managedDriverId, NativeArray* executorInfo, NativeArray* frameworkInfo, NativeArray* slaveInfo);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Reregistered(long managedDriverId, NativeArray* slaveInfo);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Disconnected(long managedDriverId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void LaunchTask(long managedDriverId, NativeArray* taskInfo);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void KillTask(long managedDriverId, NativeArray* taskId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FrameworkMessage(long managedDriverId, NativeArray* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Shutdown(long managedDriverId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Error(long managedDriverId, [MarshalAs(UnmanagedType.LPStr)] string message);
    }
}
