using System.Runtime.Serialization;

namespace mesosclr.Rendler.Executors.Messages
{
    [DataContract]
    public class RenderResultMessage
    {
        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string FileName { get; set; }
    }
}
