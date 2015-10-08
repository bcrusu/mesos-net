using System;
using com.bcrusu.mesosclr.Native.Mono;

namespace com.bcrusu.mesosclr.Native
{
    internal static class BridgeFactory
    {
        public static ExecutorDriverBridge CreateExecutorDriverBridge(long managedDriverId)
        {
            INativeExecutorDriver nativeExecutor;

            switch (DetectClrFlavor())
            {
                case ClrFlavor.Mono:
                    nativeExecutor = new MonoExecutorDriver();
                    break;
                case ClrFlavor.CoreClr:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var result = new ExecutorDriverBridge(nativeExecutor);
            result.Initialize(managedDriverId);
            return result;
        }

        public static SchedulerDriverBridge CreateSchedulerDriverBridge(long managedDriverId)
        {
            INativeSchedulerDriver nativeScheduler;

            switch (DetectClrFlavor())
            {
                case ClrFlavor.Mono:
                    nativeScheduler = new MonoSchedulerDriver();
                    break;
                case ClrFlavor.CoreClr:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var result = new SchedulerDriverBridge(nativeScheduler);
            result.Initialize(managedDriverId);
            return result;
        }

        private static ClrFlavor DetectClrFlavor()
        {
            //TODO:
            return ClrFlavor.Mono;
        }
    }
}
