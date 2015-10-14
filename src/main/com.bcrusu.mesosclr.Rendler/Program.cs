using System;
using com.bcrusu.mesosclr.Rendler.Executors;
using mesos;

namespace com.bcrusu.mesosclr.Rendler
{
    class Program
    {
        static int Main(string[] args)
		{
			var arguments = Arguments.Parse (args);
			if (arguments == null || !arguments.Validate ())
				return -1;

			switch (arguments.RunMode) {
			case RunMode.Scheduler:
				return RunScheduler (arguments.MesosMaster, arguments.StartUrl, arguments.OutputDir, arguments.RunAsUser);
			case RunMode.Executor:
				return RunExecutor (arguments.ExecutorName);
			default:
				return -1;
			}
		}

		private static int RunScheduler(string mesosMaster, string startUrl, string outputDir, string runAsUser)
        {
			var frameworkInfo = new FrameworkInfo {
				id = new FrameworkID {
					value = "Rendler"
				},
				name = "Rendler (C#)",
				failover_timeout = 5,  //seconds
				checkpoint = false,
				user = runAsUser
			};

			var scheduler = new RendlerScheduler(startUrl ?? "https://mesosphere.com", outputDir, runAsUser);
            var driver = new MesosSchedulerDriver(scheduler, frameworkInfo, mesosMaster);

			Console.WriteLine ("Running driver...");
			var result = driver.Run() == Status.DRIVER_STOPPED ? 0 : 1;
			Console.WriteLine ($"Driver finished with status {result}.");

			return result;
        }

        private static int RunExecutor(string executorName)
        {
            IExecutor executor;

            switch (executorName)
            {
                case "render":
                    executor = new RenderExecutor();
                    break;
                case "crawl":
                    executor = new CrawlExecutor();
                    break;
                default:
                    {
                        Console.WriteLine($"Invlaid executor provided: '{executorName}'.");
                        return -2;
                    }
            }

            var driver = new MesosExecutorDriver(executor);
            return driver.Run() == Status.DRIVER_STOPPED ? 0 : 1;
        }
    }
}
