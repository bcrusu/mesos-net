using System;
using System.Text;
using mesos;

namespace com.bcrusu.mesosclr.Rendler.Executors
{
    class RenderExecutor : IExecutor
    {
        private string _outputDir;

        public void Registered(IExecutorDriver driver, ExecutorInfo executorInfo, FrameworkInfo frameworkInfo, SlaveInfo slaveInfo)
        {
            Console.WriteLine("Registered executor on " + slaveInfo.hostname);
            _outputDir = Encoding.UTF8.GetString(executorInfo.data);
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

            var url = Encoding.UTF8.GetString(taskInfo.data);

            //TODO: run phantomjs
            //TODO: send success status

            driver.SendStatusUpdate(new TaskStatus
            {
                task_id = taskInfo.task_id,
                state = TaskState.TASK_FINISHED
            });
        }

        public void KillTask(IExecutorDriver driver, TaskID taskId)
        {
        }

        public void FrameworkMessage(IExecutorDriver driver, byte[] data)
        {
        }

        public void Shutdown(IExecutorDriver driver)
        {
        }

        public void Error(IExecutorDriver driver, string message)
        {
        }
    }
}
