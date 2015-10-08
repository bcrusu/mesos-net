using System;
using com.bcrusu.mesosclr.Registry;
using mesos;

namespace com.bcrusu.mesosclr.Native
{
    internal class ExecutorAdapter
    {
        public static readonly ExecutorAdapter Instance = new ExecutorAdapter();

        private ExecutorAdapter()
        {
        }

        public void Registered(long managedDriverId, byte[] executorInfo, byte[] frameworkInfo, byte[] slaveInfo)
        {
            CallExecutor(managedDriverId,
                (driver, executor) => executor.Registered(driver,
                    ProtoBufHelper.Deserialize<ExecutorInfo>(executorInfo),
                    ProtoBufHelper.Deserialize<FrameworkInfo>(frameworkInfo),
                    ProtoBufHelper.Deserialize<SlaveInfo>(slaveInfo)));
        }

        public void Reregistered(long managedDriverId, byte[] slaveInfo)
        {
            CallExecutor(managedDriverId,
                (driver, executor) => executor.Reregistered(driver,
                    ProtoBufHelper.Deserialize<SlaveInfo>(slaveInfo)));
        }

        public void Disconnected(long managedDriverId)
        {
            CallExecutor(managedDriverId,
                (driver, executor) => executor.Disconnected(driver));
        }

        public void LaunchTask(long managedDriverId, byte[] taskInfo)
        {
            CallExecutor(managedDriverId,
                (driver, executor) => executor.LaunchTask(driver,
                    ProtoBufHelper.Deserialize<TaskInfo>(taskInfo)));
        }

        public void KillTask(long managedDriverId, byte[] taskId)
        {
            CallExecutor(managedDriverId,
                (driver, executor) => executor.KillTask(driver,
                    ProtoBufHelper.Deserialize<TaskID>(taskId)));
        }

        public void FrameworkMessage(long managedDriverId, byte[] data)
        {
            CallExecutor(managedDriverId,
                (driver, executor) => executor.FrameworkMessage(driver, data));
        }

        public void Shutdown(long managedDriverId)
        {
            CallExecutor(managedDriverId,
                (driver, executor) => executor.Shutdown(driver));
        }

        public void Error(long managedDriverId, string message)
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
