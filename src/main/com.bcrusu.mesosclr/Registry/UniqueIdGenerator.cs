using System;
using System.Collections.Concurrent;
using System.Threading;

namespace com.bcrusu.mesosclr.Registry
{
    internal class UniqueIdGenerator
    {
        private static readonly ConcurrentDictionary<Type, Counter> CounterMap = new ConcurrentDictionary<Type,Counter>();

        public static long GetNextId<T>()
            where T : class
        {
            var type = typeof(T);

            if (!CounterMap.ContainsKey(type))
                CounterMap.TryAdd(type, new Counter());

            var counter = CounterMap[type];
            return counter.Increment();
        }

        private sealed class Counter
        {
            private long _currentValue;

            public long Increment()
            {
                return Interlocked.Increment(ref _currentValue);
            }
        }
    }
}
