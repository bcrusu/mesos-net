using System;
using System.Runtime.InteropServices;

namespace mesosclr.Native
{
    internal class NativeImports
    {
		private const string NativeLibraryFileName = "libmesosclr.so";

        public static class ExecutorDriver
        {
            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_ExecutorDriver_Initialize")]
            public static extern unsafe IntPtr Initialize(long managedDriverId, ExecutorInterface* executorInterface);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_ExecutorDriver_Finalize")]
            public static extern void Finalize(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_ExecutorDriver_Start")]
            public static extern int Start(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_ExecutorDriver_Stop")]
            public static extern int Stop(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_ExecutorDriver_Abort")]
            public static extern int Abort(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_ExecutorDriver_Join")]
            public static extern int Join(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_ExecutorDriver_Run")]
            public static extern int Run(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_ExecutorDriver_SendStatusUpdate")]
            public static extern int SendStatusUpdate(IntPtr nativeDriverPtr, IntPtr status);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_ExecutorDriver_SendFrameworkMessage")]
            public static extern int SendFrameworkMessage(IntPtr nativeDriverPtr, IntPtr data);
        }

        public static class SchedulerDriver
        {
            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_Initialize")]
            public static extern unsafe IntPtr Initialize(long managedDriverId, SchedulerInterface* schedulerInterface, IntPtr frameworkInfo,
                [MarshalAs(UnmanagedType.LPStr)] string masterAddress, bool implicitAcknowledgements, IntPtr credential);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_Finalize")]
            public static extern void Finalize(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_Start")]
            public static extern int Start(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_Stop")]
            public static extern int Stop(IntPtr nativeDriverPtr, bool failover);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_Abort")]
            public static extern int Abort(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_Join")]
            public static extern int Join(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_Run")]
            public static extern int Run(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_RequestResources")]
            public static extern int RequestResources(IntPtr nativeDriverPtr, IntPtr requests);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_LaunchTasks")]
            public static extern int LaunchTasks(IntPtr nativeDriverPtr, IntPtr offerIds, IntPtr tasks, IntPtr filters);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_KillTask")]
            public static extern int KillTask(IntPtr nativeDriverPtr, IntPtr taskId);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_AcceptOffers")]
            public static extern int AcceptOffers(IntPtr nativeDriverPtr, IntPtr offerIds, IntPtr operations, IntPtr filters);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_DeclineOffer")]
            public static extern int DeclineOffer(IntPtr nativeDriverPtr, IntPtr offerId, IntPtr filters);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_ReviveOffers")]
            public static extern int ReviveOffers(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_SuppressOffers")]
            public static extern int SuppressOffers(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_AcknowledgeStatusUpdate")]
            public static extern int AcknowledgeStatusUpdate(IntPtr nativeDriverPtr, IntPtr status);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_SendFrameworkMessage")]
            public static extern int SendFrameworkMessage(IntPtr nativeDriverPtr, IntPtr executorId, IntPtr slaveId, IntPtr data);

            [DllImport(NativeLibraryFileName, EntryPoint = "mesosclr_SchedulerDriver_ReconcileTasks")]
            public static extern int ReconcileTasks(IntPtr nativeDriverPtr, IntPtr statuses);
        }
    }
}
