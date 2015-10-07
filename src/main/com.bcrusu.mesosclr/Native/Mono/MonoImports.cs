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
            public static extern int Start(IntPtr executorPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_ExecutorDriver_Stop")]
            public static extern int Stop(IntPtr executorPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_ExecutorDriver_Abort")]
            public static extern int Abort(IntPtr executorPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_ExecutorDriver_Join")]
            public static extern int Join(IntPtr executorPtr);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_ExecutorDriver_SendStatusUpdate")]
            public static extern int SendStatusUpdate(IntPtr executorPtr, byte[] status);

            [DllImport(NativeLibraryFileName, EntryPoint = "com_bcrusu_mesosclr_mono_ExecutorDriver_SendFrameworkMessage")]
            public static extern int SendFrameworkMessage(IntPtr executorPtr, byte[] data);
        }
    }
}
