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
			_outputDir = Encoding.UTF8.GetString (executorInfo.data);
			Console.WriteLine ($"Registered executor on host '{slaveInfo.hostname}'. Output dir is '{_outputDir}'.");
		}

        public override void LaunchTask(IExecutorDriver driver, TaskInfo taskInfo)
        {
            Console.WriteLine($"Launching render task '{taskInfo.task_id.value}'...");

			Task.Factory.StartNew (() => {
				try {
					RunTask (driver, taskInfo);
				} catch (Exception e) {
					Console.WriteLine ($"Exception during render operation: {e}");
					driver.SendTaskErrorStatus (taskInfo.task_id);
				}
			});
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
			var imagePath = Path.Combine(_outputDir, $"{taskId.value}.png");

			var startInfo = new ProcessStartInfo("phantomjs");
			startInfo.Arguments = $"render.js \"{url}\" \"{imagePath}\"";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.UseShellExecute = false;

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
