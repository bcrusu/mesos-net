using System;
using mesos;

namespace com.bcrusu.mesosclr.Native.Mono
{
    internal class MonoExecutorDriver : INativeExecutorDriver
    {
        private IntPtr _nativeDriverPtr;

        ~MonoExecutorDriver()
        {
            Dispose(false);
        }

        public Status Start()
        {
            return (Status)MonoImports.ExecutorDriver.Start(_nativeDriverPtr);
        }

        public Status Stop()
        {
            return (Status)MonoImports.ExecutorDriver.Stop(_nativeDriverPtr);
        }

        public Status Abort()
        {
            return (Status)MonoImports.ExecutorDriver.Abort(_nativeDriverPtr);
        }

        public Status Join()
        {
            return (Status)MonoImports.ExecutorDriver.Join(_nativeDriverPtr);
        }

        public Status Run()
        {
            throw new NotSupportedException();
        }

        public Status SendStatusUpdate(TaskStatus status)
        {
            var statusBytes = ProtoBufHelper.Serialize(status);
            return (Status)MonoImports.ExecutorDriver.SendStatusUpdate(_nativeDriverPtr, statusBytes);
        }

        public Status SendFrameworkMessage(byte[] data)
        {
            return (Status)MonoImports.ExecutorDriver.SendFrameworkMessage(_nativeDriverPtr, data);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Initialize(long managedDriverId)
        {
            _nativeDriverPtr = MonoImports.ExecutorDriver.Initialize(managedDriverId);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
                GC.SuppressFinalize(this);

            MonoImports.ExecutorDriver.Finalize(_nativeDriverPtr);
        }
    }
}
