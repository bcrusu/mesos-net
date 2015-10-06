using System;
using mesos;

namespace com.bcrusu.mesosclr.Native.Mono
{
    internal class MonoExecutorDriver : INativeExecutorDriver
    {
        ~MonoExecutorDriver()
        {
            Dispose(false);            
        }

        public Status Start()
        {
            throw new NotImplementedException();
        }

        public Status Stop()
        {
            throw new NotImplementedException();
        }

        public Status Abort()
        {
            throw new NotImplementedException();
        }

        public Status Join()
        {
            throw new NotImplementedException();
        }

        public Status Run()
        {
            throw new NotImplementedException();
        }

        public Status SendStatusUpdate(TaskStatus status)
        {
            throw new NotImplementedException();
        }

        public Status SendFrameworkMessage(byte[] data)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Initialize(long managedDriverId)
        {
            throw new NotImplementedException();
        }

        private void Dispose(bool disposing)
        {
            //TODO
        }
    }
}
