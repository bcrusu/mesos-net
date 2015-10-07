using System;
using com.bcrusu.mesosclr.Native.Mono;

namespace com.bcrusu.mesosclr.Native
{
    internal static class NativeDriverFactory
    {
        public static INativeExecutorDriver CreateExecutorDriver()
        {
            switch (DetectClrFlavor())
            {
                case ClrFlavor.Mono:
                    return new MonoExecutorDriver();
                case ClrFlavor.CoreClr:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static INativeSchedulerDriver CreateSchedulerDriver()
        {
            switch (DetectClrFlavor())
            {
                case ClrFlavor.Mono:
                    return new MonoSchedulerDriver();
                case ClrFlavor.CoreClr:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static ClrFlavor DetectClrFlavor()
        {
            //TODO:
            return ClrFlavor.Mono;
        }
    }
}
