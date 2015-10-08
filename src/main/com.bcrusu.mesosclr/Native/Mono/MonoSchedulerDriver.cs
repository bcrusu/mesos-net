using System;

namespace com.bcrusu.mesosclr.Native.Mono
{
    internal class MonoSchedulerDriver : INativeSchedulerDriver
    {
        public IntPtr Initialize(long managedDriverId)
        {
            return MonoImports.SchedulerDriver.Initialize(managedDriverId);
        }

        public void Finalize(IntPtr nativeDriverPtr)
        {
            MonoImports.SchedulerDriver.Finalize(nativeDriverPtr);
        }

        public int Start(IntPtr nativeDriverPtr)
        {
            return MonoImports.SchedulerDriver.Start(nativeDriverPtr);
        }

        public int Stop(IntPtr nativeDriverPtr, bool failover)
        {
            return MonoImports.SchedulerDriver.Stop(nativeDriverPtr, failover);
        }

        public int Abort(IntPtr nativeDriverPtr)
        {
            return MonoImports.SchedulerDriver.Abort(nativeDriverPtr);
        }

        public int Join(IntPtr nativeDriverPtr)
        {
            return MonoImports.SchedulerDriver.Join(nativeDriverPtr);
        }

        public int RequestResources(IntPtr nativeDriverPtr, IntPtr requests)
        {
            return MonoImports.SchedulerDriver.RequestResources(nativeDriverPtr, requests);
        }

        public int LaunchTasksForOffer(IntPtr nativeDriverPtr, IntPtr offerId, IntPtr tasks, IntPtr filters)
        {
            return MonoImports.SchedulerDriver.LaunchTasksForOffer(nativeDriverPtr, offerId, tasks, filters);
        }

        public int LaunchTasksForOffers(IntPtr nativeDriverPtr, IntPtr offerIds, IntPtr tasks, IntPtr filters)
        {
            return MonoImports.SchedulerDriver.LaunchTasksForOffers(nativeDriverPtr, offerIds, tasks, filters);
        }

        public int KillTask(IntPtr nativeDriverPtr, IntPtr taskId)
        {
            return MonoImports.SchedulerDriver.KillTask(nativeDriverPtr, taskId);
        }

        public int AcceptOffers(IntPtr nativeDriverPtr, IntPtr offerIds, IntPtr operations, IntPtr filters)
        {
            return MonoImports.SchedulerDriver.AcceptOffers(nativeDriverPtr, offerIds, operations, filters);
        }

        public int DeclineOffer(IntPtr nativeDriverPtr, IntPtr offerId, IntPtr filters)
        {
            return MonoImports.SchedulerDriver.DeclineOffer(nativeDriverPtr, offerId, filters);
        }

        public int ReviveOffers(IntPtr nativeDriverPtr)
        {
            return MonoImports.SchedulerDriver.ReviveOffers(nativeDriverPtr);
        }

        public int SuppressOffers(IntPtr nativeDriverPtr)
        {
            return MonoImports.SchedulerDriver.SuppressOffers(nativeDriverPtr);
        }

        public int AcknowledgeStatusUpdate(IntPtr nativeDriverPtr, IntPtr status)
        {
            return MonoImports.SchedulerDriver.AcknowledgeStatusUpdate(nativeDriverPtr, status);
        }

        public int SendFrameworkMessage(IntPtr nativeDriverPtr, IntPtr executorId, IntPtr slaveId, IntPtr data)
        {
            return MonoImports.SchedulerDriver.SendFrameworkMessage(nativeDriverPtr, executorId, slaveId, data);
        }

        public int ReconcileTasks(IntPtr nativeDriverPtr, IntPtr statuses)
        {
            return MonoImports.SchedulerDriver.ReconcileTasks(nativeDriverPtr, statuses);
        }
    }
}
