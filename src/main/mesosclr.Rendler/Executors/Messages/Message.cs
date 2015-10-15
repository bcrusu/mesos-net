using System.Runtime.Serialization;

namespace mesosclr.Rendler.Executors.Messages
{
    [DataContract]
    internal class Message
    {
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public byte[] Body { get; set; }
    }
}
