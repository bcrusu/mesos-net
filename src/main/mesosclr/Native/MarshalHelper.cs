using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace mesosclr.Native
{
	internal static class MarshalHelper
	{
		public static PinnedObject CreatePinnedObject (byte[] bytes)
		{
			if (bytes == null)
				return PinnedObject.Null;
			
			var bytesPinned = new PinnedObject (bytes);

			var byteArray = new NativeArray {
				Length = bytes.Length,
				Items = bytesPinned.Ptr
			};

			return new PinnedObject (byteArray, new[] { bytesPinned });
		}

		public static PinnedObject CreatePinnedObject (IEnumerable<byte[]> arrays)
		{
			if (arrays == null)
				return PinnedObject.Null;
			
			var pinnedArrays = arrays.Select (CreatePinnedObject).ToList ();

			var arrayPtrs = pinnedArrays.Select (x => x.Ptr).ToArray ();
			var pinnedArrayPtrs = new PinnedObject (arrayPtrs, pinnedArrays);

			var array = new NativeArray {
				Length = pinnedArrays.Count,
				Items = pinnedArrayPtrs.Ptr
			};

			return new PinnedObject (array, new[] { pinnedArrayPtrs });
		}

	    public unsafe static byte[] ToMangedByteArray(NativeArray* bytes)
	    {
            var length = (*bytes).Length;
            var data = (*bytes).Items;

	        var result = new byte[length];
	        Marshal.Copy(data, result, 0, length);

	        return result;
	    }
	}
}
