using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace mesosclr.Native
{
	internal class PinnedObject : IDisposable
	{
		public static readonly PinnedObject Null = new PinnedObject ();

		private readonly IEnumerable<PinnedObject> _childObjects;
		private GCHandle _gcHandle;

		private PinnedObject ()
		{
			Ptr = IntPtr.Zero;
		}

		public PinnedObject (object obj, IEnumerable<PinnedObject> childObjects = null)
		{
			if (obj == null)
				throw new ArgumentNullException (nameof (obj));
			_childObjects = childObjects;

			_gcHandle = GCHandle.Alloc (obj, GCHandleType.Pinned);
			Ptr = _gcHandle.AddrOfPinnedObject ();
		}

		public IntPtr Ptr { get; private set; }

		public void Dispose ()
		{
			if (Ptr == IntPtr.Zero)
				return;
			
			if (_childObjects != null)
				foreach (var item in _childObjects)
					item.Dispose ();

			_gcHandle.Free ();
		}
	}
}
