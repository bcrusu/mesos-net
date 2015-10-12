using System;
using mesos;

namespace com.bcrusu.mesosclr.Rendler.Executors
{
    internal class CrawlExecutor : IExecutor
    {
        public void Registered(IExecutorDriver driver, ExecutorInfo executorInfo, FrameworkInfo frameworkInfo, SlaveInfo slaveInfo)
        {
            Console.WriteLine("Registered executor on " + slaveInfo.hostname);
        }

        public void Reregistered(IExecutorDriver driver, SlaveInfo slaveInfo)
        {
        }

        public void Disconnected(IExecutorDriver driver)
        {
        }

        public void LaunchTask(IExecutorDriver driver, TaskInfo taskInfo)
        {
            driver.SendStatusUpdate(new TaskStatus
            {
                task_id = taskInfo.task_id,
                state = TaskState.TASK_RUNNING
            });


            driver.SendStatusUpdate(new TaskStatus
            {
                task_id = taskInfo.task_id,
                state = TaskState.TASK_FINISHED
            });
        }

        public void KillTask(IExecutorDriver driver, TaskID taskId)
        {
            throw new NotImplementedException();
        }

        public void FrameworkMessage(IExecutorDriver driver, byte[] data)
        {
            throw new NotImplementedException();
        }

        public void Shutdown(IExecutorDriver driver)
        {
            throw new NotImplementedException();
        }

        public void Error(IExecutorDriver driver, string message)
        {
            throw new NotImplementedException();
        }
    }
}
