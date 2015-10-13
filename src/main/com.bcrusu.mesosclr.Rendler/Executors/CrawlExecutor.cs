using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using com.bcrusu.mesosclr.Rendler.Executors.Messages;
using mesos;

namespace com.bcrusu.mesosclr.Rendler.Executors
{
    internal class CrawlExecutor : ExecutorBase
    {
        private static readonly Regex ExtractLinksRegex = new Regex("<a[^>]+href=[\"']?(?<link>[^\"'>]+)[\"']?[^>]*>(.+?)</a>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public override void Registered(IExecutorDriver driver, ExecutorInfo executorInfo, FrameworkInfo frameworkInfo, SlaveInfo slaveInfo)
        {
            Console.WriteLine($"Registered executor on '{slaveInfo.hostname}'.");
        }

        public override void LaunchTask(IExecutorDriver driver, TaskInfo taskInfo)
        {
            Console.WriteLine($"Launching crawl task '{taskInfo.task_id}'...");
            Task.Factory.StartNew(() => RunTask(driver, taskInfo));
        }

        private static void RunTask(IExecutorDriver driver, TaskInfo taskInfo)
        {
            driver.SendTaskRunningStatus(taskInfo.task_id);

            var url = Encoding.UTF8.GetString(taskInfo.data);

            var htmlContent = GetUrlContent(url);
            if (htmlContent != null)
            {
                var links = ExtractLinks(htmlContent);
                links = links
                    .Select(x => x.ToLower())
                    .Distinct(StringComparer.CurrentCultureIgnoreCase);

                SendCrawlResultMessage(driver, url, links.ToArray());
            }

            driver.SendTaskFinishedStatus(taskInfo.task_id);
        }

        private static IEnumerable<string> ExtractLinks(string htmlContent)
        {
            var match = ExtractLinksRegex.Match(htmlContent);
            while (match.Success)
            {
                yield return match.Groups["link"].Value.Trim();
                match = match.NextMatch();
            }
        }

        private static string GetUrlContent(string url)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("X-PoweredBy: minions");

                try
                {
                    return client.DownloadString(url);
                }
                catch (WebException e)
                {
                    Console.WriteLine($"Error fetching url '{url}'; Error: {e}");
                    return null;
                }
            }
        }

        private static void SendCrawlResultMessage(IExecutorDriver driver, string url, string[] links)
        {
            var message = new Message
            {
                Type = "CrawlResult",
                Body = JsonHelper.Serialize(new CrawlResultMessage
                {
                    Url = url,
                    Links = links
                })
            };

            driver.SendFrameworkMessage(JsonHelper.Serialize(message));
        }
    }
}
