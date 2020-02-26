using System;
using System.Collections.Generic;

namespace DotNet.DataStructure.Linear.Stacks
{
    public interface IStack<T> 
    {
        int Count { get; }

        void Push(T element);
        
        T Pop();
        
        bool IsEmpty { get; }

        IEnumerable<T> AsEnumerableWithoutPop();

        bool Contains(Predicate<T> predicate);
        
        bool Contains(T element);
    }
}