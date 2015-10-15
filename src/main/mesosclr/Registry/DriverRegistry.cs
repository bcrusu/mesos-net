using System;
using System.Collections.Concurrent;

namespace mesosclr.Registry
{
    internal static class DriverRegistry
    {
        private static readonly ConcurrentDictionary<long, WeakReference> ExecutorDriverMap =
            new ConcurrentDictionary<long, WeakReference>();

        private static readonly ConcurrentDictionary<long, WeakReference> SchedulerDriverMap =
            new ConcurrentDictionary<long, WeakReference>();

        public static long Register(MesosExecutorDriver executorDriver)
        {
            var id = UniqueIdGenerator.GetNextId<MesosExecutorDriver>();
            ExecutorDriverMap[id] = new WeakReference(executorDriver, false);
            return id;
        }

        public static long Register(MesosSchedulerDriver schedulerDriver)
        {
            var id = UniqueIdGenerator.GetNextId<MesosSchedulerDriver>();
            SchedulerDriverMap[id] = new WeakReference(schedulerDriver, false);
            return id;
        }

        public static void Unregister(MesosExecutorDriver executorDriver)
        {
            WeakReference tmp;
            ExecutorDriverMap.TryRemove(executorDriver.Id, out tmp);
        }

        public static void Unregister(MesosSchedulerDriver schedulerDriver)
        {
            WeakReference tmp;
            SchedulerDriverMap.TryRemove(schedulerDriver.Id, out tmp);
        }

        public static MesosExecutorDriver GetExecutorDriver(long id)
        {
            return ExecutorDriverMap[id].Target as MesosExecutorDriver;
        }

        public static MesosSchedulerDriver GetSchedulerDriver(long id)
        {
            return SchedulerDriverMap[id].Target as MesosSchedulerDriver;
        }
    }
}
