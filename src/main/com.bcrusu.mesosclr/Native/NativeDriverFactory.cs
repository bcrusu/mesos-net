using System;
using com.bcrusu.mesosclr.Native.Mono;

namespace com.bcrusu.mesosclr.Native
{
    internal static class NativeDriverFactory
    {
        public static INativeExecutorDriver CreateExecutorDriver()
        {
            switch (DetectClrType())
            {
                case ClrType.Mono:
                    return new MonoExecutorDriver();
                case ClrType.CoreClr:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static INativeSchedulerDriver CreateSchedulerDriver()
        {
            switch (DetectClrType())
            {
                case ClrType.Mono:
                    return new MonoSchedulerDriver();
                case ClrType.CoreClr:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static ClrType DetectClrType()
        {
            //TODO:
            return ClrType.Mono;
        }
    }
}
