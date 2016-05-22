using System;
using System.IO;
using ProtoBuf;
using System.Collections.Generic;
using mesosclr.Native;

namespace mesosclr
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

        public static unsafe T Deserialize<T>(NativeArray* bytes)
            where T : IExtensible
        {
            var length = (*bytes).Length;
            var data = (*bytes).Items;

            if (length == 0 || data == IntPtr.Zero)
                return default(T);

            using (var ms = new UnmanagedMemoryStream((byte*)data.ToPointer(), length))
            {
                return Serializer.Deserialize<T>(ms);
            }
        }

        public static unsafe IEnumerable<TItem> DeserializeCollection<TItem>(NativeArray* collection)
            where TItem : IExtensible
        {
            var length = (*collection).Length;
            var items = (NativeArray**)(*collection).Items;

            var result = new List<TItem>();
            for (var i = 0; i < length; i++)
                result.Add(Deserialize<TItem>(items[i]));

            return result;
        }
    }
}
