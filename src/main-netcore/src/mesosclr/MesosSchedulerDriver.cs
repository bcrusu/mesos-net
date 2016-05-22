using System;
using System.Collections.Generic;
using mesos;
using mesosclr.Native;
using mesosclr.Registry;

namespace mesosclr
{
    public sealed class MesosSchedulerDriver : ISchedulerDriver, IDisposable
    {
        private readonly SchedulerDriverBridge _bridge;

        public MesosSchedulerDriver(IScheduler scheduler, FrameworkInfo frameworkInfo, string masterAddress, bool implicitAcknowledgements, Credential credential)
        {
            if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));
            if (frameworkInfo == null) throw new ArgumentNullException(nameof(frameworkInfo));
            if (masterAddress == null) throw new ArgumentNullException(nameof(masterAddress));

            Scheduler = scheduler;
            Id = DriverRegistry.Register(this);
            _bridge = new SchedulerDriverBridge();
            _bridge.Initialize(Id, frameworkInfo, masterAddress, implicitAcknowledgements, credential);
        }

        public MesosSchedulerDriver(IScheduler scheduler, FrameworkInfo frameworkInfo, string masterAddress)
            : this(scheduler, frameworkInfo, masterAddress, true, null)
        {
        }

        public MesosSchedulerDriver(IScheduler scheduler, FrameworkInfo frameworkInfo, string masterAddress, Credential credential)
            : this(scheduler, frameworkInfo, masterAddress, true, credential)
        {
        }

        public MesosSchedulerDriver(IScheduler scheduler, FrameworkInfo frameworkInfo, string masterAddress, bool implicitAcknowledgements)
            : this(scheduler, frameworkInfo, masterAddress, implicitAcknowledgements, null)
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
