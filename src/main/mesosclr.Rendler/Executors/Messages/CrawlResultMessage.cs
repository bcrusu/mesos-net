using System.Runtime.Serialization;

namespace mesosclr.Rendler.Executors.Messages
{
    [DataContract]
    public class CrawlResultMessage
    {
        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string[] Links { get; set; }
    }
}
