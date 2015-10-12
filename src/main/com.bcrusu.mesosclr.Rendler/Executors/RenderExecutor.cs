using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using com.bcrusu.mesosclr.Rendler.Executors.Messages;
using mesos;

namespace com.bcrusu.mesosclr.Rendler.Executors
{
    class RenderExecutor : ExecutorBase
    {
        private string _outputDir;

        public override void Registered(IExecutorDriver driver, ExecutorInfo executorInfo, FrameworkInfo frameworkInfo, SlaveInfo slaveInfo)
        {
            Console.WriteLine("Registered executor on " + slaveInfo.hostname);
            _outputDir = Encoding.UTF8.GetString(executorInfo.data);
        }

        public override void LaunchTask(IExecutorDriver driver, TaskInfo taskInfo)
        {
            Console.WriteLine($"Launching render task '{taskInfo.task_id}'...");
            Task.Factory.StartNew(() => RunTask(driver, taskInfo));
        }

        private static void RunTask(IExecutorDriver driver, TaskInfo taskInfo)
        {
            driver.SendTaskRunningStatus(taskInfo.task_id);

            var url = Encoding.UTF8.GetString(taskInfo.data);
            var fileName = "todo";

            //TODO: run phantomjs
            //TODO: send success status
            Thread.Sleep(5000);

            SendRenderResultMessage(driver, url, fileName);
            driver.SendTaskFinishedStatus(taskInfo.task_id);
        }

        private static void SendRenderResultMessage(IExecutorDriver driver, string url, string fileName)
        {
            var message = new Message
            {
                Type = "RenderResult",
                Body = JsonHelper.Serialize(new RenderResultMessage
                {
                    Url = url,
                    FileName = fileName
                })
            };

            driver.SendFrameworkMessage(JsonHelper.Serialize(message));
        }
    }
}
