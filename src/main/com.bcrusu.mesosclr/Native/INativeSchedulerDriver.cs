using System;

namespace com.bcrusu.mesosclr.Native
{
    internal interface INativeSchedulerDriver
    {
		IntPtr Initialize(long managedDriverId, IntPtr frameworkInfo, IntPtr masterAddress, bool implicitAcknowledgements, IntPtr credential);

        void Finalize(IntPtr nativeDriverPtr);

        int Start(IntPtr nativeDriverPtr);

        int Stop(IntPtr nativeDriverPtr, bool failover);

        int Abort(IntPtr nativeDriverPtr);

        int Join(IntPtr nativeDriverPtr);

        int RequestResources(IntPtr nativeDriverPtr, IntPtr requests);

        int LaunchTasksForOffer(IntPtr nativeDriverPtr, IntPtr offerId, IntPtr tasks, IntPtr filters);

        int LaunchTasksForOffers(IntPtr nativeDriverPtr, IntPtr offerIds, IntPtr tasks, IntPtr filters);

        int KillTask(IntPtr nativeDriverPtr, IntPtr taskId);

        int AcceptOffers(IntPtr nativeDriverPtr, IntPtr offerIds, IntPtr operations, IntPtr filters);

        int DeclineOffer(IntPtr nativeDriverPtr, IntPtr offerId, IntPtr filters);

        int ReviveOffers(IntPtr nativeDriverPtr);

        int SuppressOffers(IntPtr nativeDriverPtr);

        int AcknowledgeStatusUpdate(IntPtr nativeDriverPtr, IntPtr status);

        int SendFrameworkMessage(IntPtr nativeDriverPtr, IntPtr executorId, IntPtr slaveId, IntPtr data);

        int ReconcileTasks(IntPtr nativeDriverPtr, IntPtr statuses);
    }
}
