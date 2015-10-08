using System;
using System.Runtime.InteropServices;

namespace com.bcrusu.mesosclr.Native.Mono
{
    internal class MonoImports
    {
        private const string NativeLibraryFileName = "mesosclr.so";

        public static class ExecutorDriver
        {
            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_ExecutorDriver_Initialize")]
            public static extern IntPtr Initialize(long managedDriverId);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_ExecutorDriver_Finalize")]
            public static extern void Finalize(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_ExecutorDriver_Start")]
            public static extern int Start(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_ExecutorDriver_Stop")]
            public static extern int Stop(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_ExecutorDriver_Abort")]
            public static extern int Abort(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_ExecutorDriver_Join")]
            public static extern int Join(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_ExecutorDriver_SendStatusUpdate")]
            public static extern int SendStatusUpdate(IntPtr nativeDriverPtr, IntPtr status);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_ExecutorDriver_SendFrameworkMessage")]
            public static extern int SendFrameworkMessage(IntPtr nativeDriverPtr, IntPtr data);
        }

        public static class SchedulerDriver
        {
            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_Initialize")]
            public static extern IntPtr Initialize(long managedDriverId);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_Finalize")]
            public static extern void Finalize(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_Start")]
            public static extern int Start(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_Stop")]
            public static extern int Stop(IntPtr nativeDriverPtr, bool failover);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_Abort")]
            public static extern int Abort(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_Join")]
            public static extern int Join(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_RequestResources")]
            public static extern int RequestResources(IntPtr nativeDriverPtr, IntPtr requests);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_LaunchTasksForOffer")]
            public static extern int LaunchTasksForOffer(IntPtr nativeDriverPtr, IntPtr offerId, IntPtr tasks, IntPtr filters);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_LaunchTasksForOffers")]
            public static extern int LaunchTasksForOffers(IntPtr nativeDriverPtr, IntPtr offerIds, IntPtr tasks, IntPtr filters);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_KillTask")]
            public static extern int KillTask(IntPtr nativeDriverPtr, IntPtr taskId);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_AcceptOffers")]
            public static extern int AcceptOffers(IntPtr nativeDriverPtr, IntPtr offerIds, IntPtr operations, IntPtr filters);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_DeclineOffer")]
            public static extern int DeclineOffer(IntPtr nativeDriverPtr, IntPtr offerId, IntPtr filters);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_ReviveOffers")]
            public static extern int ReviveOffers(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_SuppressOffers")]
            public static extern int SuppressOffers(IntPtr nativeDriverPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_AcknowledgeStatusUpdate")]
            public static extern int AcknowledgeStatusUpdate(IntPtr nativeDriverPtr, IntPtr status);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_SendFrameworkMessage")]
            public static extern int SendFrameworkMessage(IntPtr nativeDriverPtr, IntPtr executorId, IntPtr slaveId, IntPtr data);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_SchedulerDriver_ReconcileTasks")]
            public static extern int ReconcileTasks(IntPtr nativeDriverPtr, IntPtr statuses);
        }
    }
}
