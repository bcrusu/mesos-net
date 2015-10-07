using System;
using System.Collections.Generic;
using System.Linq;
using mesos;

namespace com.bcrusu.mesosclr.Native.Mono
{
    internal class MonoSchedulerDriver : INativeSchedulerDriver
    {
        private IntPtr _nativeDriverPtr;

        ~MonoSchedulerDriver()
        {
            Dispose(false);
        }

        public Status Start()
        {
            return (Status)MonoImports.SchedulerDriver.Start(_nativeDriverPtr);
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
            return (Status)MonoImports.SchedulerDriver.Abort(_nativeDriverPtr);
        }

        public Status Join()
        {
            return (Status)MonoImports.SchedulerDriver.Join(_nativeDriverPtr);
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
            var taskIdBytes = ProtoBufHelper.Serialize(taskId);

            using (var pinned = MarshalHelper.CreatePinnedObject(taskIdBytes))
                return (Status)MonoImports.SchedulerDriver.KillTask(_nativeDriverPtr, pinned.Ptr);
        }

        public Status AcceptOffers(IEnumerable<OfferID> offerIds, IEnumerable<Offer.Operation> operations, Filters filters)
        {
            var offerIdsArrays = offerIds.Select(ProtoBufHelper.Serialize);
            var operationsArrays = operations.Select(ProtoBufHelper.Serialize);
            var filtersBytes = ProtoBufHelper.Serialize(filters);

            using (var pinnedOfferIdsArrays = MarshalHelper.CreatePinnedObject(offerIdsArrays))
            using (var pinnedOperationsArrays = MarshalHelper.CreatePinnedObject(operationsArrays))
            using (var pinnedFiltersBytes = MarshalHelper.CreatePinnedObject(filtersBytes))
                return (Status)MonoImports.SchedulerDriver.AcceptOffers(_nativeDriverPtr, pinnedOfferIdsArrays.Ptr, pinnedOperationsArrays.Ptr, pinnedFiltersBytes.Ptr);
        }

        public Status DeclineOffer(OfferID offerId, Filters filters)
        {
            var offerIdBytes = ProtoBufHelper.Serialize(offerId);
            var filtersBytes = ProtoBufHelper.Serialize(filters);

            using (var pinnedOfferId = MarshalHelper.CreatePinnedObject(offerIdBytes))
            using (var pinnedFilters = MarshalHelper.CreatePinnedObject(filtersBytes))
                return (Status)MonoImports.SchedulerDriver.DeclineOffer(_nativeDriverPtr, pinnedOfferId.Ptr, pinnedFilters.Ptr);
        }

        public Status DeclineOffer(OfferID offerId)
        {
            return DeclineOffer(offerId, new Filters());
        }

        public Status ReviveOffers()
        {
            return (Status)MonoImports.SchedulerDriver.ReviveOffers(_nativeDriverPtr);
        }

        public Status SuppressOffers()
        {
            return (Status)MonoImports.SchedulerDriver.SuppressOffers(_nativeDriverPtr);
        }

        public Status AcknowledgeStatusUpdate(TaskStatus status)
        {
            var statusBytes = ProtoBufHelper.Serialize(status);

            using (var pinned = MarshalHelper.CreatePinnedObject(statusBytes))
                return (Status)MonoImports.SchedulerDriver.AcknowledgeStatusUpdate(_nativeDriverPtr, pinned.Ptr);
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
            _nativeDriverPtr = MonoImports.SchedulerDriver.Initialize(managedDriverId);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
                GC.SuppressFinalize(this);

            MonoImports.SchedulerDriver.Finalize(_nativeDriverPtr);
        }
    }
}
