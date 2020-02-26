using System;
using System.Collections.Generic;

namespace DotNet.DataStructure.Linear.Queues
{
    public interface IQueue<T>
    {
        int Count { get; }

        void Enqueue(T element);
        
        T Dequeue();
        
        bool IsEmpty { get; }

        IEnumerable<T> AsEnumerableWithoutDequeue();

        bool Contains(Predicate<T> predicate);
        
        bool Contains(T element);
    }
}