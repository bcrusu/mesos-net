using System;

namespace com.bcrusu.mesosclr.Rendler
{
    internal class Arguments
    {
        public RunMode RunMode { get; private set; }

        public string MesosMaster { get; private set; }

        public string ExecutorName { get; private set; }

        public string StartUrl { get; private set; }

        public string OutputDir { get; private set; }

        public static Arguments Parse(string[] args)
        {
            var runMode = RunMode.Default;
            var mesosMaster = string.Empty;
            var executor = string.Empty;
            var outputDir = string.Empty;
            var startUrl = string.Empty;

            foreach (var arg in args)
            {
                if (arg.StartsWith("-executor="))
                {
                    if (runMode != RunMode.Default)
                    {
                        Console.WriteLine("Scheduler and Executor run modes are mutually exclusive.");
                        return null;
                    }

                    executor = arg.Substring("-executor=".Length);
                    runMode = RunMode.Executor;
                }
                else if (arg.Equals("-scheduler"))
                {
                    if (runMode != RunMode.Default)
                    {
                        Console.WriteLine("Scheduler and Executor run modes are mutually exclusive.");
                        return null;
                    }

                    runMode = RunMode.Scheduler;
                }
                else if (arg.StartsWith("-master="))
                {
                    mesosMaster = arg.Substring("-master=".Length);
                }
                else if (arg.StartsWith("-output="))
                {
                    outputDir = arg.Substring("-output=".Length);
                }
                else if (arg.StartsWith("-starturl="))
                {
                    startUrl = arg.Substring("-starturl=".Length);
                }
                else
                {
                    Console.WriteLine($"Unknown argument detected: '{arg}'.");
                }
            }

            return new Arguments
            {
                RunMode = runMode,
                ExecutorName = executor,
                MesosMaster = mesosMaster,
                OutputDir = outputDir,
                StartUrl = startUrl
            };
        }
    }
}
