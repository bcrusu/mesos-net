using System;
using System.Runtime.InteropServices;
using mesos;
using mesosclr.Registry;

namespace mesosclr.Native
{
    internal unsafe class SchedulerCallbacks
    {
        private static readonly SchedulerCallbacksSig.Registered RegisteredDelegate = Registered;
        private static readonly SchedulerCallbacksSig.Reregistered ReregisteredDelegate = Reregistered;
        private static readonly SchedulerCallbacksSig.ResourceOffers ResourceOffersDelegate = ResourceOffers;
        private static readonly SchedulerCallbacksSig.OfferRescinded OfferRescindedDelegate = OfferRescinded;
        private static readonly SchedulerCallbacksSig.StatusUpdate StatusUpdateDelegate = StatusUpdate;
        private static readonly SchedulerCallbacksSig.FrameworkMessage FrameworkMessageDelegate = FrameworkMessage;
        private static readonly SchedulerCallbacksSig.Disconnected DisconnectedDelegate = Disconnected;
        private static readonly SchedulerCallbacksSig.SlaveLost SlaveLostDelegate = SlaveLost;
        private static readonly SchedulerCallbacksSig.ExecutorLost ExecutorLostDelegate = ExecutorLost;
        private static readonly SchedulerCallbacksSig.Error ErrorDelegate = Error;

        private static readonly IntPtr RegisteredPtr = Marshal.GetFunctionPointerForDelegate(RegisteredDelegate);
        private static readonly IntPtr ReregisteredPtr = Marshal.GetFunctionPointerForDelegate(ReregisteredDelegate);
        private static readonly IntPtr ResourceOffersPtr = Marshal.GetFunctionPointerForDelegate(ResourceOffersDelegate);
        private static readonly IntPtr OfferRescindedPtr = Marshal.GetFunctionPointerForDelegate(OfferRescindedDelegate);
        private static readonly IntPtr StatusUpdatePtr = Marshal.GetFunctionPointerForDelegate(StatusUpdateDelegate);
        private static readonly IntPtr FrameworkMessagePtr = Marshal.GetFunctionPointerForDelegate(FrameworkMessageDelegate);
        private static readonly IntPtr DisconnectedPtr = Marshal.GetFunctionPointerForDelegate(DisconnectedDelegate);
        private static readonly IntPtr SlaveLostPtr = Marshal.GetFunctionPointerForDelegate(SlaveLostDelegate);
        private static readonly IntPtr ExecutorLostPtr = Marshal.GetFunctionPointerForDelegate(ExecutorLostDelegate);
        private static readonly IntPtr ErrorPtr = Marshal.GetFunctionPointerForDelegate(ErrorDelegate);

        public static SchedulerInterface GetSchedulerInterface()
        {
            SchedulerInterface result;
            result.Registered = RegisteredPtr;
            result.Reregistered = ReregisteredPtr;
            result.ResourceOffers = ResourceOffersPtr;
            result.OfferRescinded = OfferRescindedPtr;
            result.StatusUpdate = StatusUpdatePtr;
            result.FrameworkMessage = FrameworkMessagePtr;
            result.Disconnected = DisconnectedPtr;
            result.SlaveLost = SlaveLostPtr;
            result.ExecutorLost = ExecutorLostPtr;
            result.Error = ErrorPtr;

            return result;
        }

        private static void Registered(long managedDriverId, NativeArray* frameworkId, NativeArray* masterInfo)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.Registered(driver,
                    ProtoBufHelper.Deserialize<FrameworkID>(frameworkId),
                    ProtoBufHelper.Deserialize<MasterInfo>(masterInfo)));
        }

        private static void Reregistered(long managedDriverId, NativeArray* masterInfo)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.Reregistered(driver,
                    ProtoBufHelper.Deserialize<MasterInfo>(masterInfo)));
        }

        private static void ResourceOffers(long managedDriverId, NativeArray* offers)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.ResourceOffers(driver,
                    ProtoBufHelper.DeserializeCollection<Offer>(offers)));
        }

        private static void OfferRescinded(long managedDriverId, NativeArray* offerId)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.OfferRescinded(driver,
                    ProtoBufHelper.Deserialize<OfferID>(offerId)));
        }

        private static void StatusUpdate(long managedDriverId, NativeArray* status)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.StatusUpdate(driver,
                    ProtoBufHelper.Deserialize<TaskStatus>(status)));
        }

        private static void FrameworkMessage(long managedDriverId, NativeArray* executorId, NativeArray* slaveId, NativeArray* data)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.FrameworkMessage(driver,
                    ProtoBufHelper.Deserialize<ExecutorID>(executorId),
                    ProtoBufHelper.Deserialize<SlaveID>(slaveId),
                    MarshalHelper.ToMangedByteArray(data)));
        }

        private static void Disconnected(long managedDriverId)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.Disconnected(driver));
        }

        private static void SlaveLost(long managedDriverId, NativeArray* slaveId)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.SlaveLost(driver,
                    ProtoBufHelper.Deserialize<SlaveID>(slaveId)));
        }

        private static void ExecutorLost(long managedDriverId, NativeArray* executorId, NativeArray* slaveId, int status)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.ExecutorLost(driver,
                    ProtoBufHelper.Deserialize<ExecutorID>(executorId),
                    ProtoBufHelper.Deserialize<SlaveID>(slaveId),
                    status));
        }

        private static void Error(long managedDriverId, string message)
        {
            CallScheduler(managedDriverId,
                (driver, executor) => executor.Error(driver, message));
        }

        private static void CallScheduler(long managedDriverId, Action<MesosSchedulerDriver, IScheduler> action)
        {
            var driver = DriverRegistry.GetSchedulerDriver(managedDriverId);
            var scheduler = driver.Scheduler;

            action(driver, scheduler);
        }
    }
}
