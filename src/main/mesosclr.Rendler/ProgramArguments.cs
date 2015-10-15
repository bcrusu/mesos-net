using System;
using System.IO;

namespace mesosclr.Rendler
{
    internal class Arguments
    {
        public RunMode RunMode { get; private set; }

        public string MesosMaster { get; private set; }

        public string ExecutorName { get; private set; }

        public string StartUrl { get; private set; }

        public string OutputDir { get; private set; }

		public string RunAsUser { get; private set; }

		public static Arguments Parse(string[] args)
        {
            var runMode = RunMode.Default;
            string mesosMaster = null;
			string executor = null;
			string outputDir = null;
			string startUrl = null;
			string runAsUser = null;

            foreach (var arg in args)
            {
                if (arg.StartsWith("-executor="))
                {
					if (runMode == RunMode.Executor) {
						Console.WriteLine("Executor option can be specified only once.");
						return null;
					}
                    if (runMode == RunMode.Scheduler)
                    {
                        Console.WriteLine("Scheduler and Executor run modes are mutually exclusive.");
                        return null;
                    }

                    executor = arg.Substring("-executor=".Length);
                    runMode = RunMode.Executor;
                }
                else if (arg.Equals("-scheduler"))
                {
					if (runMode == RunMode.Scheduler) {
						Console.WriteLine("Scheduler option can be specified only once.");
						return null;
					}
					if (runMode == RunMode.Executor)
                    {
                        Console.WriteLine("Scheduler and Executor run modes are mutually exclusive.");
                        return null;
                    }

                    runMode = RunMode.Scheduler;
                }
                else if (arg.StartsWith("-master="))
                {
					if (mesosMaster != null) {
						Console.WriteLine("Mesos master option can be specified only once.");
						return null;
					}

                    mesosMaster = arg.Substring("-master=".Length);
                }
                else if (arg.StartsWith("-output="))
                {
					if (outputDir != null) {
						Console.WriteLine("Output directory option can be specified only once.");
						return null;
					}

                    outputDir = arg.Substring("-output=".Length);
                }
                else if (arg.StartsWith("-starturl="))
                {
					if (startUrl != null) {
						Console.WriteLine("Start URL option can be specified only once.");
						return null;
					}

                    startUrl = arg.Substring("-starturl=".Length);
                }
				else if (arg.StartsWith("-user="))
				{
					if (startUrl != null) {
						Console.WriteLine("User option can be specified only once.");
						return null;
					}

					runAsUser = arg.Substring("-user=".Length);
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
                StartUrl = startUrl,
				RunAsUser = runAsUser
            };
        }

		public bool Validate ()
		{
			switch (RunMode) {
			case RunMode.Executor: 
				if (string.IsNullOrWhiteSpace (ExecutorName)) {
					Console.WriteLine ("Invalid executor name.");	
					return false;
				}
				break;
			case RunMode.Scheduler: 
				if (string.IsNullOrWhiteSpace (MesosMaster)) {
					Console.WriteLine ("Invalid Mesos master address.");	
					return false;
				}
				if (string.IsNullOrWhiteSpace (OutputDir)) {
					Console.WriteLine ("Invalid output directory.");	
					return false;
				}
				if (!Directory.Exists(OutputDir)){
					Console.WriteLine ("Output directory does not exist.");	
					return false;
				}
				break;
			default:
				Console.WriteLine ("Run mode was not specified.");
				return false;
			}

			return true;
		}
    }
}
