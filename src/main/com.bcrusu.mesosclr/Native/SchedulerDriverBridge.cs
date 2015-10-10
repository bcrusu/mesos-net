using System;
using System.Collections.Generic;
using System.Linq;
using mesos;

namespace com.bcrusu.mesosclr.Native
{
    internal class SchedulerDriverBridge : ISchedulerDriver, IDisposable
    {
        private IntPtr _nativeDriverPtr;

        public Status Start()
        {
            return (Status)NativeImports.SchedulerDriver.Start(_nativeDriverPtr);
        }

        public Status Stop(bool failover)
        {
            return (Status)NativeImports.SchedulerDriver.Stop(_nativeDriverPtr, failover);
        }

        public Status Stop()
        {
            return Stop(false);
        }

        public Status Abort()
        {
            return (Status)NativeImports.SchedulerDriver.Abort(_nativeDriverPtr);
        }

        public Status Join()
        {
            return (Status)NativeImports.SchedulerDriver.Join(_nativeDriverPtr);
        }

        public Status Run()
        {
            var status = Start();
            return status != Status.DRIVER_RUNNING ? status : Join();
        }

        public Status RequestResources(IEnumerable<Request> requests)
        {
            var requestsBytes = requests.Select(ProtoBufHelper.Serialize);

            using (var pinned = MarshalHelper.CreatePinnedObject(requestsBytes))
                return (Status)NativeImports.SchedulerDriver.KillTask(_nativeDriverPtr, pinned.Ptr);
        }

        public Status LaunchTasks(IEnumerable<OfferID> offerIds, IEnumerable<TaskInfo> tasks, Filters filters)
        {
            var offerIdsArrays = offerIds.Select(ProtoBufHelper.Serialize);
            var tasksArrays = tasks.Select(ProtoBufHelper.Serialize);
            var filtersBytes = ProtoBufHelper.Serialize(filters);

            using (var pinnedOfferIdsArrays = MarshalHelper.CreatePinnedObject(offerIdsArrays))
            using (var pinnedTasksArrays = MarshalHelper.CreatePinnedObject(tasksArrays))
            using (var pinnedFiltersBytes = MarshalHelper.CreatePinnedObject(filtersBytes))
                return (Status)NativeImports.SchedulerDriver.LaunchTasksForOffers(_nativeDriverPtr, pinnedOfferIdsArrays.Ptr, pinnedTasksArrays.Ptr, pinnedFiltersBytes.Ptr);
        }

        public Status LaunchTasks(IEnumerable<OfferID> offerIds, IEnumerable<TaskInfo> tasks)
        {
            return LaunchTasks(offerIds, tasks, new Filters());
        }

        public Status LaunchTasks(OfferID offerId, IEnumerable<TaskInfo> tasks, Filters filters)
        {
            var offerIdBytes = ProtoBufHelper.Serialize(offerId);
            var tasksArrays = tasks.Select(ProtoBufHelper.Serialize);
            var filtersBytes = ProtoBufHelper.Serialize(filters);

            using (var pinnedOfferId = MarshalHelper.CreatePinnedObject(offerIdBytes))
            using (var pinnedTasksArrays = MarshalHelper.CreatePinnedObject(tasksArrays))
            using (var pinnedFiltersBytes = MarshalHelper.CreatePinnedObject(filtersBytes))
                return (Status)NativeImports.SchedulerDriver.LaunchTasksForOffer(_nativeDriverPtr, pinnedOfferId.Ptr, pinnedTasksArrays.Ptr, pinnedFiltersBytes.Ptr);
        }

        public Status LaunchTasks(OfferID offerId, IEnumerable<TaskInfo> tasks)
        {
            return LaunchTasks(offerId, tasks, new Filters());
        }

        public Status KillTask(TaskID taskId)
        {
            var taskIdBytes = ProtoBufHelper.Serialize(taskId);

            using (var pinned = MarshalHelper.CreatePinnedObject(taskIdBytes))
                return (Status)NativeImports.SchedulerDriver.KillTask(_nativeDriverPtr, pinned.Ptr);
        }

        public Status AcceptOffers(IEnumerable<OfferID> offerIds, IEnumerable<Offer.Operation> operations, Filters filters)
        {
            var offerIdsArrays = offerIds.Select(ProtoBufHelper.Serialize);
            var operationsArrays = operations.Select(ProtoBufHelper.Serialize);
            var filtersBytes = ProtoBufHelper.Serialize(filters);

            using (var pinnedOfferIdsArrays = MarshalHelper.CreatePinnedObject(offerIdsArrays))
            using (var pinnedOperationsArrays = MarshalHelper.CreatePinnedObject(operationsArrays))
            using (var pinnedFiltersBytes = MarshalHelper.CreatePinnedObject(filtersBytes))
                return (Status)NativeImports.SchedulerDriver.AcceptOffers(_nativeDriverPtr, pinnedOfferIdsArrays.Ptr, pinnedOperationsArrays.Ptr, pinnedFiltersBytes.Ptr);
        }

        public Status DeclineOffer(OfferID offerId, Filters filters)
        {
            var offerIdBytes = ProtoBufHelper.Serialize(offerId);
            var filtersBytes = ProtoBufHelper.Serialize(filters);

            using (var pinnedOfferId = MarshalHelper.CreatePinnedObject(offerIdBytes))
            using (var pinnedFilters = MarshalHelper.CreatePinnedObject(filtersBytes))
                return (Status)NativeImports.SchedulerDriver.DeclineOffer(_nativeDriverPtr, pinnedOfferId.Ptr, pinnedFilters.Ptr);
        }

        public Status DeclineOffer(OfferID offerId)
        {
            return DeclineOffer(offerId, new Filters());
        }

        public Status ReviveOffers()
        {
            return (Status)NativeImports.SchedulerDriver.ReviveOffers(_nativeDriverPtr);
        }

        public Status SuppressOffers()
        {
            return (Status)NativeImports.SchedulerDriver.SuppressOffers(_nativeDriverPtr);
        }

        public Status AcknowledgeStatusUpdate(TaskStatus status)
        {
            var statusBytes = ProtoBufHelper.Serialize(status);

            using (var pinned = MarshalHelper.CreatePinnedObject(statusBytes))
                return (Status)NativeImports.SchedulerDriver.AcknowledgeStatusUpdate(_nativeDriverPtr, pinned.Ptr);
        }

        public Status SendFrameworkMessage(ExecutorID executorId, SlaveID slaveId, byte[] data)
        {
            var executorIdBytes = ProtoBufHelper.Serialize(executorId);
            var slaveIdBytes = ProtoBufHelper.Serialize(slaveId);

            using (var pinnedExecutorId = MarshalHelper.CreatePinnedObject(executorIdBytes))
            using (var pinnedSlaveId = MarshalHelper.CreatePinnedObject(slaveIdBytes))
            using (var pinnedData = MarshalHelper.CreatePinnedObject(data))
                return (Status)NativeImports.SchedulerDriver.SendFrameworkMessage(_nativeDriverPtr, pinnedExecutorId.Ptr, pinnedSlaveId.Ptr, pinnedData.Ptr);
        }

        public Status ReconcileTasks(IEnumerable<TaskStatus> statuses)
        {
            var statusesArrays = statuses.Select(ProtoBufHelper.Serialize);

            using (var pinnedStatuses = MarshalHelper.CreatePinnedObject(statusesArrays))
                return (Status)NativeImports.SchedulerDriver.ReconcileTasks(_nativeDriverPtr, pinnedStatuses.Ptr);
        }

        public void Initialize(long managedDriverId, FrameworkInfo frameworkInfo, string masterAddress, bool implicitAcknowledgements, Credential credential)
        {
            var schedulerInterface = SchedulerCallbacks.GetSchedulerInterface();

            var frameworkInfoBytes = ProtoBufHelper.Serialize(frameworkInfo);
            byte[] credentialBytes = null;
            if (credential != null)
                credentialBytes = ProtoBufHelper.Serialize(credential);

            //TODO: string marshalling

            unsafe
            {
                using (var pinnedFrameworkInfo = MarshalHelper.CreatePinnedObject(frameworkInfoBytes))
                using (var pinnedCredential = MarshalHelper.CreatePinnedObject(credentialBytes))
                    _nativeDriverPtr = NativeImports.SchedulerDriver.Initialize(managedDriverId, &schedulerInterface,
                        pinnedFrameworkInfo.Ptr, IntPtr.Zero, implicitAcknowledgements, pinnedCredential.Ptr);
            }
        }

        public void Dispose()
        {
            NativeImports.SchedulerDriver.Finalize(_nativeDriverPtr);
        }
    }
}
