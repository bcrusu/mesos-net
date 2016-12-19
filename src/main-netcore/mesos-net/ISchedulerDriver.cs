using System.Collections.Generic;

namespace mesos
{
    public interface ISchedulerDriver
    {
        Status Start();

        Status Stop(bool failover);

        Status Stop();

        Status Abort();

        Status Join();

        Status Run();

        Status RequestResources(IEnumerable<Request> requests);

        Status LaunchTasks(IEnumerable<OfferID> offerIds, IEnumerable<TaskInfo> tasks, Filters filters);

        Status LaunchTasks(IEnumerable<OfferID> offerIds, IEnumerable<TaskInfo> tasks);

        Status KillTask(TaskID taskId);

        Status AcceptOffers(IEnumerable<OfferID> offerIds, IEnumerable<Offer.Operation> operations, Filters filters);

        Status DeclineOffer(OfferID offerId, Filters filters);

        Status DeclineOffer(OfferID offerId);

        Status ReviveOffers();

        Status SuppressOffers();

        Status AcknowledgeStatusUpdate(TaskStatus status);

        Status SendFrameworkMessage(ExecutorID executorId, SlaveID slaveId, byte[] data);

        Status ReconcileTasks(IEnumerable<TaskStatus> statuses);
    }
}
