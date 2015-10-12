using System;
using com.bcrusu.mesosclr.Rendler.Executors;
using mesos;

namespace com.bcrusu.mesosclr.Rendler
{
    class Program
    {
        static int Main(string[] args)
        {
            var arguments = Arguments.Parse(args);
            if (arguments == null)
                return -1;

            switch (arguments.RunMode)
            {
                case RunMode.Scheduler:
                    return RunScheduler(arguments.MesosMaster, arguments.StartUrl, arguments.OutputDir);
                case RunMode.Executor:
                    return RunExecutor(arguments.ExecutorName);
                default:
                    {
                        Console.WriteLine("Run mode was not provided. Exiting...");
                        return -1;
                    }
            }
        }

        private static int RunScheduler(string mesosMaster, string startUrl, string outputDir)
        {
            if (string.IsNullOrWhiteSpace(mesosMaster))
            {
                Console.WriteLine("Mesos master address was not provided.");
                return -3;
            }

            if (string.IsNullOrWhiteSpace(outputDir))
            {
                Console.WriteLine("Output directory was not provided.");
                return -3;
            }


            var frameworkInfo = new FrameworkInfo
            {
                name = "Rendler (C#)",
                failover_timeout = 5,  //seconds
                checkpoint = false
            };

            var scheduler = new RendlerScheduler(startUrl ?? "https://mesosphere.com", outputDir);
            var driver = new MesosSchedulerDriver(scheduler, frameworkInfo, mesosMaster);

            return driver.Run() == Status.DRIVER_STOPPED ? 0 : 1;
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
