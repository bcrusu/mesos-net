using System;

namespace com.bcrusu.mesosclr.Native
{
    internal interface INativeDriver : IDisposable
    {
        void Initialize(long managedDriverId);
    }
}
