using System.Collections.Generic;
using System.Linq;

namespace com.bcrusu.mesosclr.Native
{
    internal static class MarshalHelper
    {
        public static PinnedObject CreatePinnedObject(byte[] bytes)
        {
            var bytesPinned = new PinnedObject(bytes);

            var byteArray = new Array
            {
                Length = bytes.Length,
                Items = bytesPinned.Ptr
            };

            return new PinnedObject(byteArray, new[] { bytesPinned });
        }

        public static PinnedObject CreatePinnedObject(IEnumerable<byte[]> arrays)
        {
            var pinnedArrays = arrays.Select(CreatePinnedObject).ToList();

            var arrayPtrs = pinnedArrays.Select(x => x.Ptr).ToArray();
            var pinnedArrayPtrs = new PinnedObject(arrayPtrs, pinnedArrays);

            var array = new Array
            {
                Length = pinnedArrays.Count,
                Items = pinnedArrayPtrs.Ptr
            };

            return new PinnedObject(array, new[] { pinnedArrayPtrs });
        }
    }
}
