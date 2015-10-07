using System;
using System.IO;
using ProtoBuf;

namespace com.bcrusu.mesosclr
{
    internal static class ProtoBufHelper
    {
        public static byte[] Serialize<T>(T entity)
            where T : IExtensible
        {
            if (entity == null)
                return new byte[0];

            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, entity);
                return ms.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] bytes)
            where T : IExtensible
        {
            if (bytes == null || bytes.Length == 0)
                return default(T);

            using (var ms = new MemoryStream(bytes))
            {
                return Serializer.Deserialize<T>(ms);
            }
        }
    }
}
