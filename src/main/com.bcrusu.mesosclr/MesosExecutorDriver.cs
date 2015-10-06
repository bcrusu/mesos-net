using System;
using com.bcrusu.mesosclr.Native;
using com.bcrusu.mesosclr.Registry;
using mesos;

namespace com.bcrusu.mesosclr
{
    public class MesosExecutorDriver : IExecutorDriver
    {
        private readonly IExecutor _executor;
        private readonly long _id;
        private readonly INativeExecutorDriver _nativeExecutorDriver;

        public MesosExecutorDriver(IExecutor executor)
        {
            if (executor == null) throw new ArgumentNullException(nameof(executor));

            _executor = executor;

            _id = DriverRegistry.Register(this);
            _nativeExecutorDriver = NativeDriverFactory.CreateExecutorDriver();

            _nativeExecutorDriver.Initialize(_id);
        }

        public Status Start()
        {
            return _nativeExecutorDriver.Start();
        }

        public Status Stop()
        {
            return _nativeExecutorDriver.Stop();
        }

        public Status Abort()
        {
            return _nativeExecutorDriver.Abort();
        }

        public Status Join()
        {
            return _nativeExecutorDriver.Join();
        }

        public Status Run()
        {
            var status = Start();
            status = status != Status.DRIVER_RUNNING ? status : Join();

            return status;
        }

        public Status SendStatusUpdate(TaskStatus status)
        {
            return _nativeExecutorDriver.SendStatusUpdate(status);
        }

        public Status SendFrameworkMessage(byte[] data)
        {
            return _nativeExecutorDriver.SendFrameworkMessage(data);
        }
    }
}
