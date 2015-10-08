using System;

namespace com.bcrusu.mesosclr.Native.Mono
{
    internal class MonoExecutorDriver : INativeExecutorDriver
    {
        public IntPtr Initialize(long managedDriverId)
        {
            return MonoImports.ExecutorDriver.Initialize(managedDriverId);
        }

        public void Finalize(IntPtr nativeDriverPtr)
        {
            MonoImports.ExecutorDriver.Finalize(nativeDriverPtr);
        }

        public int Start(IntPtr nativeDriverPtr)
        {
            return MonoImports.ExecutorDriver.Start(nativeDriverPtr);
        }

        public int Stop(IntPtr nativeDriverPtr)
        {
            return MonoImports.ExecutorDriver.Stop(nativeDriverPtr);
        }

        public int Abort(IntPtr nativeDriverPtr)
        {
            return MonoImports.ExecutorDriver.Abort(nativeDriverPtr);
        }

        public int Join(IntPtr nativeDriverPtr)
        {
            return MonoImports.ExecutorDriver.Join(nativeDriverPtr);
        }

        public int SendStatusUpdate(IntPtr nativeDriverPtr, IntPtr status)
        {
            return MonoImports.ExecutorDriver.SendStatusUpdate(nativeDriverPtr, status);
        }

        public int SendFrameworkMessage(IntPtr nativeDriverPtr, IntPtr data)
        {
            return MonoImports.ExecutorDriver.SendFrameworkMessage(nativeDriverPtr, data);
        }
    }
}
