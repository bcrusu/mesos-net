using System;
using System.Collections.Generic;
using System.Linq;
using com.bcrusu.mesosclr.Registry;
using mesos;

namespace com.bcrusu.mesosclr.Native
{
    internal class SchedulerAdapter
    {
        public static readonly SchedulerAdapter Instance = new SchedulerAdapter();

        private SchedulerAdapter()
        {
        }

        public void Registered(long managedDriverId, byte[] frameworkId, byte[] masterInfo)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.Registered(driver,
                    ProtoBufHelper.Deserialize<FrameworkID>(frameworkId),
                    ProtoBufHelper.Deserialize<MasterInfo>(masterInfo)));
        }

        public void Reregistered(long managedDriverId, byte[] masterInfo)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.Reregistered(driver,
                    ProtoBufHelper.Deserialize<MasterInfo>(masterInfo)));
        }

        public void ResourceOffers(long managedDriverId, List<byte[]> offers)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.ResourceOffers(driver,
                    offers.Select(ProtoBufHelper.Deserialize<Offer>).ToArray()));
        }

        public void OfferRescinded(long managedDriverId, byte[] offerId)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.OfferRescinded(driver,
                    ProtoBufHelper.Deserialize<OfferID>(offerId)));
        }

        public void StatusUpdate(long managedDriverId, byte[] status)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.StatusUpdate(driver,
                    ProtoBufHelper.Deserialize<TaskStatus>(status)));
        }

        public void FrameworkMessage(long managedDriverId, byte[] executorId, byte[] slaveId, byte[] data)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.FrameworkMessage(driver,
                    ProtoBufHelper.Deserialize<ExecutorID>(executorId),
                    ProtoBufHelper.Deserialize<SlaveID>(slaveId),
                    data));
        }

        public void Disconnected(long managedDriverId)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.Disconnected(driver));
        }

        public void SlaveLost(long managedDriverId, byte[] slaveId)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.SlaveLost(driver,
                    ProtoBufHelper.Deserialize<SlaveID>(slaveId)));
        }

        public void ExecutorLost(long managedDriverId, byte[] executorId, byte[] slaveId, int status)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.ExecutorLost(driver,
                    ProtoBufHelper.Deserialize<ExecutorID>(executorId),
                    ProtoBufHelper.Deserialize<SlaveID>(slaveId),
                    status));
        }

        public void Error(long managedDriverId, string message)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.Error(driver,
                    message));
        }

        private static void CallScheduler(long managedDriverId, Action<MesosSchedulerDriver, IScheduler> action)
        {
            var driver = DriverRegistry.GetSchedulerDriver(managedDriverId);
            var scheduler = driver.Scheduler;

            action(driver, scheduler);
        }
    }
}
