using System;
using mesos;

namespace mesosclr.Rendler.Executors
{
    abstract class ExecutorBase : IExecutor
    {
        public virtual void Registered(IExecutorDriver driver, ExecutorInfo executorInfo, FrameworkInfo frameworkInfo, SlaveInfo slaveInfo)
        {
        }

        public virtual void Reregistered(IExecutorDriver driver, SlaveInfo slaveInfo)
        {
        }

        public virtual void Disconnected(IExecutorDriver driver)
        {
        }

        public virtual void LaunchTask(IExecutorDriver driver, TaskInfo taskInfo)
        {
        }

        public virtual void KillTask(IExecutorDriver driver, TaskID taskId)
        {
        }

        public virtual void FrameworkMessage(IExecutorDriver driver, byte[] data)
        {
        }

        public virtual void Shutdown(IExecutorDriver driver)
        {
        }

        public virtual void Error(IExecutorDriver driver, string message)
        {
            Console.WriteLine($"Error: '{message}'.");
        }
    }
}
