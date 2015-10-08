using System;

namespace com.bcrusu.mesosclr.Native
{
    internal interface INativeExecutorDriver
    {
        IntPtr Initialize(long managedDriverId);

        void Finalize(IntPtr nativeDriverPtr);

        int Start(IntPtr nativeDriverPtr);

        int Stop(IntPtr nativeDriverPtr);

        int Abort(IntPtr nativeDriverPtr);

        int Join(IntPtr nativeDriverPtr);

        int SendStatusUpdate(IntPtr nativeDriverPtr, IntPtr status);

        int SendFrameworkMessage(IntPtr nativeDriverPtr, IntPtr data);
    }
}
