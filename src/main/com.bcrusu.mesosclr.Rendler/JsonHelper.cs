using System.IO;
using System.Runtime.Serialization.Json;

namespace com.bcrusu.mesosclr.Rendler
{
    internal static class JsonHelper
    {
        public static byte[] Serialize(object obj)
        {
            var dcs = new DataContractJsonSerializer(obj.GetType());
            using (var ms = new MemoryStream())
            {
                dcs.WriteObject(ms, obj);
                return ms.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] bytes)
        {
            var dcs = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream(bytes))
            {
                return (T)dcs.ReadObject(ms);
            }
        }
    }
}
