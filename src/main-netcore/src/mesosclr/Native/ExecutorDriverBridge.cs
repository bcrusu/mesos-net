using System;
using mesos;

namespace mesosclr.Native
{
    internal class ExecutorDriverBridge : IExecutorDriver, IDisposable
    {
        private IntPtr _nativeDriverPtr;

        public Status Start()
        {
            return (Status)NativeImports.ExecutorDriver.Start(_nativeDriverPtr);
        }

        public Status Stop()
        {
            return (Status)NativeImports.ExecutorDriver.Stop(_nativeDriverPtr);
        }

        public Status Abort()
        {
            return (Status)NativeImports.ExecutorDriver.Abort(_nativeDriverPtr);
        }

        public Status Join()
        {
            return (Status)NativeImports.ExecutorDriver.Join(_nativeDriverPtr);
        }

        public Status Run()
        {
			return (Status)NativeImports.ExecutorDriver.Run(_nativeDriverPtr);
        }

        public Status SendStatusUpdate(TaskStatus status)
        {
            var statusBytes = ProtoBufHelper.Serialize(status);

            using (var pinned = MarshalHelper.CreatePinnedObject(statusBytes))
                return (Status)NativeImports.ExecutorDriver.SendStatusUpdate(_nativeDriverPtr, pinned.Ptr);
        }

        public Status SendFrameworkMessage(byte[] data)
        {
            using (var pinned = MarshalHelper.CreatePinnedObject(data))
                return (Status)NativeImports.ExecutorDriver.SendFrameworkMessage(_nativeDriverPtr, pinned.Ptr);
        }

        public void Initialize(long managedDriverId)
        {
            var executorInterface = ExecutorCallbacks.GetExecutorInterface();

            unsafe
            {
                _nativeDriverPtr = NativeImports.ExecutorDriver.Initialize(managedDriverId, &executorInterface);
            }
        }

        public void Dispose()
        {
            NativeImports.ExecutorDriver.Finalize(_nativeDriverPtr);
        }
    }
}
