using System;
using System.Runtime.InteropServices;
using mesos;
using mesosclr.Registry;

namespace mesosclr.Native
{
    internal unsafe class ExecutorCallbacks
    {
        private static readonly ExecutorCallbacksSig.Registered RegisteredDelegate = Registered;
        private static readonly ExecutorCallbacksSig.Reregistered ReregisteredDelegate = Reregistered;
        private static readonly ExecutorCallbacksSig.Disconnected DisconnectedDelegate = Disconnected;
        private static readonly ExecutorCallbacksSig.LaunchTask LaunchTaskDelegate = LaunchTask;
        private static readonly ExecutorCallbacksSig.KillTask KillTaskDelegate = KillTask;
        private static readonly ExecutorCallbacksSig.FrameworkMessage FrameworkMessageDelegate = FrameworkMessage;
        private static readonly ExecutorCallbacksSig.Shutdown ShutdownDelegate = Shutdown;
        private static readonly ExecutorCallbacksSig.Error ErrorDelegate = Error;

        private static readonly IntPtr RegisteredPtr = Marshal.GetFunctionPointerForDelegate(RegisteredDelegate);
        private static readonly IntPtr ReregisteredPtr = Marshal.GetFunctionPointerForDelegate(ReregisteredDelegate);
        private static readonly IntPtr DisconnectedPtr = Marshal.GetFunctionPointerForDelegate(DisconnectedDelegate);
        private static readonly IntPtr LaunchTaskPtr = Marshal.GetFunctionPointerForDelegate(LaunchTaskDelegate);
        private static readonly IntPtr KillTaskPtr = Marshal.GetFunctionPointerForDelegate(KillTaskDelegate);
        private static readonly IntPtr FrameworkMessagePtr = Marshal.GetFunctionPointerForDelegate(FrameworkMessageDelegate);
        private static readonly IntPtr ShutdownPtr = Marshal.GetFunctionPointerForDelegate(ShutdownDelegate);
        private static readonly IntPtr ErrorPtr = Marshal.GetFunctionPointerForDelegate(ErrorDelegate);

        public static ExecutorInterface GetExecutorInterface()
        {
            ExecutorInterface result;
            result.Registered = RegisteredPtr;
            result.Reregistered = ReregisteredPtr;
            result.Disconnected = DisconnectedPtr;
            result.LaunchTask = LaunchTaskPtr;
            result.KillTask = KillTaskPtr;
            result.FrameworkMessage = FrameworkMessagePtr;
            result.Shutdown = ShutdownPtr;
            result.Error = ErrorPtr;

            return result;
        }

        private static void Registered(long managedDriverId, NativeArray* executorInfo, NativeArray* frameworkInfo, NativeArray* slaveInfo)
        {
            CallExecutor(managedDriverId,
                (driver, executor) => executor.Registered(driver,
                    ProtoBufHelper.Deserialize<ExecutorInfo>(executorInfo),
                    ProtoBufHelper.Deserialize<FrameworkInfo>(frameworkInfo),
                    ProtoBufHelper.Deserialize<SlaveInfo>(slaveInfo)));
        }

        private static void Reregistered(long managedDriverId, NativeArray* slaveInfo)
        {
            CallExecutor(managedDriverId,
                (driver, executor) => executor.Reregistered(driver,
                    ProtoBufHelper.Deserialize<SlaveInfo>(slaveInfo)));
        }

        private static void Disconnected(long managedDriverId)
        {
            CallExecutor(managedDriverId,
                (driver, executor) => executor.Disconnected(driver));
        }

        private static void LaunchTask(long managedDriverId, NativeArray* taskInfo)
        {
            CallExecutor(managedDriverId,
                (driver, executor) => executor.LaunchTask(driver,
                    ProtoBufHelper.Deserialize<TaskInfo>(taskInfo)));
        }

        private static void KillTask(long managedDriverId, NativeArray* taskId)
        {
            CallExecutor(managedDriverId,
                (driver, executor) => executor.KillTask(driver,
                    ProtoBufHelper.Deserialize<TaskID>(taskId)));
        }

        private static void FrameworkMessage(long managedDriverId, NativeArray* data)
        {
            CallExecutor(managedDriverId,
                (driver, executor) => executor.FrameworkMessage(driver,
                    MarshalHelper.ToMangedByteArray(data)));
        }

        private static void Shutdown(long managedDriverId)
        {
            CallExecutor(managedDriverId,
                (driver, executor) => executor.Shutdown(driver));
        }

		private static void Error(long managedDriverId, string message)
        {
            CallExecutor(managedDriverId,
                (driver, executor) => executor.Error(driver, message));
        }

        private static void CallExecutor(long managedDriverId, Action<MesosExecutorDriver, IExecutor> action)
        {
            var driver = DriverRegistry.GetExecutorDriver(managedDriverId);
            var executor = driver.Executor;

            action(driver, executor);
        }
    }
}
