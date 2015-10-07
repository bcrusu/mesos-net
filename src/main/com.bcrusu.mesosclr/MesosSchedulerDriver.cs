using System;
using com.bcrusu.mesosclr.Native;
using com.bcrusu.mesosclr.Registry;

namespace com.bcrusu.mesosclr
{
    public class MesosSchedulerDriver
    {
        private IScheduler _scheduler;

        public MesosSchedulerDriver(IScheduler scheduler)
        {
            if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));
            _scheduler = scheduler;

            Id = DriverRegistry.Register(this);
        }

        ~MesosSchedulerDriver()
        {
            DriverRegistry.Unregister(this);
        }

        internal long Id { get; private set; }
    }
}
