using System;
using System.Diagnostics;
using System.IO;
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

        private void RunTask(IExecutorDriver driver, TaskInfo taskInfo)
        {
            driver.SendTaskRunningStatus(taskInfo.task_id);

            var url = Encoding.UTF8.GetString(taskInfo.data);
            var imageFileName = RunRendering(taskInfo.task_id, url);

            SendRenderResultMessage(driver, url, imageFileName);
            driver.SendTaskFinishedStatus(taskInfo.task_id);
        }

        private string RunRendering(TaskID taskId, string url)
        {
            var programDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var renderJsPath = Path.Combine(programDir, "render.js");
            var imagesDir = Path.Combine(_outputDir, "images");
            var imagePath = Path.Combine(imagesDir, $"{taskId.value}.png");

            var startInfo = new ProcessStartInfo("phantomjs");
            startInfo.Arguments = $"\"{renderJsPath}\" \"{url}\" \"{imagePath}\"";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.WorkingDirectory = imagesDir;

            var process = Process.Start(startInfo);
            process.WaitForExit();

            return imagePath;
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
