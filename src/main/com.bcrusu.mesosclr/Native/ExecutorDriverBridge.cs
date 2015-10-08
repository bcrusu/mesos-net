using System;
using mesos;

namespace com.bcrusu.mesosclr.Native
{
    internal class ExecutorDriverBridge : IExecutorDriver, IDisposable
    {
        private readonly INativeExecutorDriver _nativeExecutorDriver;
        private IntPtr _nativeDriverPtr;

        public ExecutorDriverBridge(INativeExecutorDriver nativeExecutorDriver)
        {
            if (nativeExecutorDriver == null) throw new ArgumentNullException(nameof(nativeExecutorDriver));
            _nativeExecutorDriver = nativeExecutorDriver;
        }

        public Status Start()
        {
            return (Status)_nativeExecutorDriver.Start(_nativeDriverPtr);
        }

        public Status Stop()
        {
            return (Status)_nativeExecutorDriver.Stop(_nativeDriverPtr);
        }

        public Status Abort()
        {
            return (Status)_nativeExecutorDriver.Abort(_nativeDriverPtr);
        }

        public Status Join()
        {
            return (Status)_nativeExecutorDriver.Join(_nativeDriverPtr);
        }

        public Status Run()
        {
            var status = Start();
            status = status != Status.DRIVER_RUNNING ? status : Join();
            return status;
        }

        public Status SendStatusUpdate(TaskStatus status)
        {
            var statusBytes = ProtoBufHelper.Serialize(status);

            using (var pinned = MarshalHelper.CreatePinnedObject(statusBytes))
                return (Status)_nativeExecutorDriver.SendStatusUpdate(_nativeDriverPtr, pinned.Ptr);
        }

        public Status SendFrameworkMessage(byte[] data)
        {
            using (var pinned = MarshalHelper.CreatePinnedObject(data))
                return (Status)_nativeExecutorDriver.SendFrameworkMessage(_nativeDriverPtr, pinned.Ptr);
        }

        public void Initialize(long managedDriverId)
        {
            _nativeDriverPtr = _nativeExecutorDriver.Initialize(managedDriverId);
        }

        public void Dispose()
        {
            _nativeExecutorDriver.Finalize(_nativeDriverPtr);
        }
    }
}
