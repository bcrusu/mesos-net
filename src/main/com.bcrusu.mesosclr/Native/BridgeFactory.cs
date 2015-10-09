using System;
using com.bcrusu.mesosclr.Native.Mono;

namespace com.bcrusu.mesosclr.Native
{
    internal static class BridgeFactory
    {
        public static ExecutorDriverBridge CreateExecutorDriverBridge()
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

			return new ExecutorDriverBridge(nativeExecutor);
        }

        public static SchedulerDriverBridge CreateSchedulerDriverBridge()
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

			return new SchedulerDriverBridge(nativeScheduler);
        }

        private static ClrFlavor DetectClrFlavor()
        {
            //TODO:
            return ClrFlavor.Mono;
        }
    }
}
