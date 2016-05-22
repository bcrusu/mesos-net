using System.Collections.Generic;
using mesos;

namespace mesosclr
{
    public interface IScheduler
    {
        void Registered(ISchedulerDriver driver, FrameworkID frameworkId, MasterInfo masterInfo);

        void Reregistered(ISchedulerDriver driver, MasterInfo masterInfo);

        void ResourceOffers(ISchedulerDriver driver, IEnumerable<Offer> offers);

        void OfferRescinded(ISchedulerDriver driver, OfferID offerId);

        void StatusUpdate(ISchedulerDriver driver, TaskStatus status);

        void FrameworkMessage(ISchedulerDriver driver, ExecutorID executorId, SlaveID slaveId, byte[] data);

        void Disconnected(ISchedulerDriver driver);

        void SlaveLost(ISchedulerDriver driver, SlaveID slaveId);

        void ExecutorLost(ISchedulerDriver driver, ExecutorID executorId, SlaveID slaveId, int status);

        void Error(ISchedulerDriver driver, string message);
    }
}
