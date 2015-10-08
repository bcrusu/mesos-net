﻿using System;
using System.Collections.Generic;
using com.bcrusu.mesosclr.Native;
using com.bcrusu.mesosclr.Registry;
using mesos;

namespace com.bcrusu.mesosclr
{
    public sealed class MesosSchedulerDriver : ISchedulerDriver, IDisposable
    {
        private readonly SchedulerDriverBridge _bridge;

        //TODO: pass ctor params to native driver
        public MesosSchedulerDriver(IScheduler scheduler, FrameworkInfo frameworkInfo, string masterAddress, Credential credential, bool implicitAcknowledgements)
        {
            if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));
            if (frameworkInfo == null) throw new ArgumentNullException(nameof(frameworkInfo));
            if (masterAddress == null) throw new ArgumentNullException(nameof(masterAddress));

            Scheduler = scheduler;
            Id = DriverRegistry.Register(this);
            _bridge = BridgeFactory.CreateSchedulerDriverBridge(Id);
        }

        public MesosSchedulerDriver(IScheduler scheduler, FrameworkInfo frameworkInfo, string masterAddress)
            : this(scheduler, frameworkInfo, masterAddress, null, true)
        {
        }

        public MesosSchedulerDriver(IScheduler scheduler, FrameworkInfo frameworkInfo, string masterAddress, Credential credential)
            : this(scheduler, frameworkInfo, masterAddress, credential, true)
        {
        }

        public MesosSchedulerDriver(IScheduler scheduler, FrameworkInfo frameworkInfo, string masterAddress, bool implicitAcknowledgements)
            : this(scheduler, frameworkInfo, masterAddress, null, implicitAcknowledgements)
        {
        }

        ~MesosSchedulerDriver()
        {
            Dispose(false);
        }

        internal long Id { get; }

        internal IScheduler Scheduler { get; }

        public Status Start()
        {
            return _bridge.Start();
        }

        public Status Stop(bool failover)
        {
            return _bridge.Stop(failover);
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

        public Status RequestResources(IEnumerable<Request> requests)
        {
            return _bridge.RequestResources(requests);
        }

        public Status LaunchTasks(IEnumerable<OfferID> offerIds, IEnumerable<TaskInfo> tasks, Filters filters)
        {
            return _bridge.LaunchTasks(offerIds, tasks, filters);
        }

        public Status LaunchTasks(IEnumerable<OfferID> offerIds, IEnumerable<TaskInfo> tasks)
        {
            return _bridge.LaunchTasks(offerIds, tasks);
        }

        public Status LaunchTasks(OfferID offerId, IEnumerable<TaskInfo> tasks, Filters filters)
        {
            return _bridge.LaunchTasks(offerId, tasks, filters);
        }

        public Status LaunchTasks(OfferID offerId, IEnumerable<TaskInfo> tasks)
        {
            return _bridge.LaunchTasks(offerId, tasks);
        }

        public Status KillTask(TaskID taskId)
        {
            return _bridge.KillTask(taskId);
        }

        public Status AcceptOffers(IEnumerable<OfferID> offerIds, IEnumerable<Offer.Operation> operations, Filters filters)
        {
            return _bridge.AcceptOffers(offerIds, operations, filters);
        }

        public Status DeclineOffer(OfferID offerId, Filters filters)
        {
            return _bridge.DeclineOffer(offerId, filters);
        }

        public Status DeclineOffer(OfferID offerId)
        {
            return _bridge.DeclineOffer(offerId);
        }

        public Status ReviveOffers()
        {
            return _bridge.ReviveOffers();
        }

        public Status SuppressOffers()
        {
            return _bridge.SuppressOffers();
        }

        public Status AcknowledgeStatusUpdate(TaskStatus status)
        {
            return _bridge.AcknowledgeStatusUpdate(status);
        }

        public Status SendFrameworkMessage(ExecutorID executorId, SlaveID slaveId, byte[] data)
        {
            return _bridge.SendFrameworkMessage(executorId, slaveId, data);
        }

        public Status ReconcileTasks(IEnumerable<TaskStatus> statuses)
        {
            return _bridge.ReconcileTasks(statuses);
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
