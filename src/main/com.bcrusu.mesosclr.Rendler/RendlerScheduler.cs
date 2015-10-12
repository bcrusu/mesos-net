using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using com.bcrusu.mesosclr.Rendler.Executors;
using mesos;

namespace com.bcrusu.mesosclr.Rendler
{
    internal class RendlerScheduler : IScheduler
    {
        private const double Render_CPUS = 1d;
        private const double Render_MEM = 128d;
        private const double Crawl_CPUS = 0.5d;
        private const double Crawl_MEM = 64d;

        private readonly string _startUrl;
        private readonly string _outputDir;

        private int _launchedTasks;
        private int _finishedTasks;
        private readonly ConcurrentQueue<string> _crawlQueue = new ConcurrentQueue<string>();
        private readonly ConcurrentQueue<string> _renderQueue = new ConcurrentQueue<string>();
        private ISet<string> _crawled = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

        public RendlerScheduler(string startUrl, string outputDir)
        {
            if (startUrl == null) throw new ArgumentNullException(nameof(startUrl));
            if (outputDir == null) throw new ArgumentNullException(nameof(outputDir));
            _startUrl = startUrl;
            _outputDir = outputDir;
        }

        public void Registered(ISchedulerDriver driver, FrameworkID frameworkId, MasterInfo masterInfo)
        {
            Console.WriteLine($"Registered with Mesos master. FrameworkId='{frameworkId.value}'.");
        }

        public void Reregistered(ISchedulerDriver driver, MasterInfo masterInfo)
        {
        }

        public void ResourceOffers(ISchedulerDriver driver, IEnumerable<Offer> offers)
        {
            foreach (var offer in offers)
            {
                var tasks = new List<TaskInfo>();
                var resourcesCounter = new ResourcesCounter(offer);
                var done = true;
                do
                {
                    string renderUrl;
                    if (resourcesCounter.HasRenderTaskResources() && _renderQueue.TryDequeue(out renderUrl))
                    {
                        tasks.Add(GetRenderTaskInfo(offer, ++_launchedTasks, renderUrl));
                        resourcesCounter.SubstractRenderResources();
                        done = false;
                    }

                    string crawlUrl;
                    if (resourcesCounter.HasCrawlTaskResources() && _crawlQueue.TryDequeue(out crawlUrl))
                    {
                        tasks.Add(GetCrawlTaskInfo(offer, ++_launchedTasks, crawlUrl));
                        resourcesCounter.SubstractCrawlResources();
                        done = false;
                    }
                } while (!done);

                if (tasks.Any())
                    driver.LaunchTasks(new[] { offer.id }, tasks);
                else
                    driver.DeclineOffer(offer.id);
            }
        }

        public void OfferRescinded(ISchedulerDriver driver, OfferID offerId)
        {
        }

        public void StatusUpdate(ISchedulerDriver driver, TaskStatus status)
        {
        }

        public void FrameworkMessage(ISchedulerDriver driver, ExecutorID executorId, SlaveID slaveId, byte[] data)
        {
        }

        public void Disconnected(ISchedulerDriver driver)
        {
        }

        public void SlaveLost(ISchedulerDriver driver, SlaveID slaveId)
        {
        }

        public void ExecutorLost(ISchedulerDriver driver, ExecutorID executorId, SlaveID slaveId, int status)
        {
        }

        public void Error(ISchedulerDriver driver, string message)
        {
        }

        private TaskInfo GetRenderTaskInfo(Offer offer, int uniqueId, string url)
        {
            return new TaskInfo
            {
                name = "Rendler.Render_" + uniqueId,
                task_id = new TaskID { value = uniqueId.ToString() },
                slave_id = offer.slave_id,
                resources =
                {
                    new Resource {name = "cpus", type = Value.Type.SCALAR, scalar = new Value.Scalar {value = Render_CPUS}},
                    new Resource {name = "mem", type = Value.Type.SCALAR, scalar = new Value.Scalar {value = Render_MEM}}
                },
                executor = new ExecutorInfo
                {
                    executor_id = new ExecutorID { value = "RenderExecutor" },
                    command = new CommandInfo { value = "mono rendler.exe -executor=render" },
                    data = Encoding.UTF8.GetBytes(_outputDir)
                },
                data = Encoding.UTF8.GetBytes(url)
            };
        }

        private static TaskInfo GetCrawlTaskInfo(Offer offer, int uniqueId, string url)
        {
            return new TaskInfo
            {
                name = "Rendler.Crawl_" + uniqueId,
                task_id = new TaskID { value = uniqueId.ToString() },
                slave_id = offer.slave_id,
                resources =
                {
                    new Resource {name = "cpus", type = Value.Type.SCALAR, scalar = new Value.Scalar {value = Crawl_CPUS}},
                    new Resource {name = "mem", type = Value.Type.SCALAR, scalar = new Value.Scalar {value = Crawl_MEM}}
                },
                executor = new ExecutorInfo
                {
                    executor_id = new ExecutorID { value = "CrawlExecutor" },
                    command = new CommandInfo { value = "mono rendler.exe -executor=crawl" }
                },
                data = Encoding.UTF8.GetBytes(url)
            };
        }

        private class ResourcesCounter
        {
            private double _cpus;
            private double _mem;

            public ResourcesCounter(Offer offer)
            {
                var cpusResource = offer.resources.SingleOrDefault(x => x.name == "cpus");
                var memResource = offer.resources.SingleOrDefault(x => x.name == "mem");
                _cpus = cpusResource?.scalar.value ?? 0d;
                _mem = memResource?.scalar.value ?? 0d;
            }

            private void Substract(double cpus, double mem)
            {
                _cpus = _cpus - cpus;
                _mem = _mem - mem;
            }

            public bool HasRenderTaskResources()
            {
                return HasResources(Render_CPUS, Render_MEM);
            }

            public bool HasCrawlTaskResources()
            {
                return HasResources(Crawl_CPUS, Crawl_MEM);
            }

            public void SubstractRenderResources()
            {
                Substract(Render_CPUS, Render_MEM);
            }

            public void SubstractCrawlResources()
            {
                Substract(Crawl_CPUS, Crawl_MEM);
            }

            private bool HasResources(double cpus, double mem)
            {
                return _cpus >= cpus && _mem >= mem;
            }
        }
    }
}
