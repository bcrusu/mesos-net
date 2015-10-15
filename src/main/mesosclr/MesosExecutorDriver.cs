using System;
using mesos;
using mesosclr.Native;
using mesosclr.Registry;

namespace mesosclr
{
    public sealed class MesosExecutorDriver : IExecutorDriver, IDisposable
    {
        private readonly ExecutorDriverBridge _bridge;

        public MesosExecutorDriver(IExecutor executor)
        {
            if (executor == null) throw new ArgumentNullException(nameof(executor));

            Executor = executor;
            Id = DriverRegistry.Register(this);
            _bridge = new ExecutorDriverBridge();
			_bridge.Initialize (Id);
        }

        ~MesosExecutorDriver()
        {
            Dispose(false);
        }

        internal long Id { get; }

        internal IExecutor Executor { get; }

        public Status Start()
        {
            return _bridge.Start();
        }

        public Status Stop()
        {
            return _bridge.Stop();
        }

        public Status Abort()
        {
            return _bridge.Abort();
        }

        public Status Join()
        {
            return _bridge.Join();
        }

        public Status Run()
        {
            return _bridge.Run();
        }

        public Status SendStatusUpdate(TaskStatus status)
        {
            return _bridge.SendStatusUpdate(status);
        }

        public Status SendFrameworkMessage(byte[] data)
        {
            return _bridge.SendFrameworkMessage(data);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
                GC.SuppressFinalize(this);

            _bridge.Dispose();
            DriverRegistry.Unregister(this);
        }
    }
}
