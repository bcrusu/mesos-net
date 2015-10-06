using System;
using System.Collections.Generic;
using mesos;

namespace com.bcrusu.mesosclr.Native.Mono
{
    internal class MonoSchedulerDriver : INativeSchedulerDriver
    {
        ~MonoSchedulerDriver()
        {
            Dispose(false);
        }

        public Status Start()
        {
            throw new NotImplementedException();
        }

        public Status Stop(bool failover)
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

        public Status RequestResources(IEnumerable<Request> requests)
        {
            throw new NotImplementedException();
        }

        public Status LaunchTasks(IEnumerable<OfferID> offerIds, IEnumerable<TaskInfo> tasks, Filters filters)
        {
            throw new NotImplementedException();
        }

        public Status LaunchTasks(IEnumerable<OfferID> offerIds, IEnumerable<TaskInfo> tasks)
        {
            throw new NotImplementedException();
        }

        public Status LaunchTasks(OfferID offerId, IEnumerable<TaskInfo> tasks, Filters filters)
        {
            throw new NotImplementedException();
        }

        public Status LaunchTasks(OfferID offerId, IEnumerable<TaskInfo> tasks)
        {
            throw new NotImplementedException();
        }

        public Status KillTask(TaskID taskId)
        {
            throw new NotImplementedException();
        }

        public Status AcceptOffers(IEnumerable<OfferID> offerIds, IEnumerable<Offer.Operation> operations, Filters filters)
        {
            throw new NotImplementedException();
        }

        public Status DeclineOffer(OfferID offerId, Filters filters)
        {
            throw new NotImplementedException();
        }

        public Status DeclineOffer(OfferID offerId)
        {
            throw new NotImplementedException();
        }

        public Status ReviveOffers()
        {
            throw new NotImplementedException();
        }

        public Status SuppressOffers()
        {
            throw new NotImplementedException();
        }

        public Status AcknowledgeStatusUpdate(TaskStatus status)
        {
            throw new NotImplementedException();
        }

        public Status SendFrameworkMessage(ExecutorID executorId, SlaveID slaveId, byte[] data)
        {
            throw new NotImplementedException();
        }

        public Status ReconcileTasks(IEnumerable<TaskStatus> statuses)
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
