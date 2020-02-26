using System;
using System.Collections.Generic;

namespace DotNet.DataStructure.Linear.Decks
{
    public interface IDeck<T>
    {
        int Count { get; }

        void PushAtStart(T element);
        
        void PushAtEnd(T element);
        
        T PopAtStart();
        
        T PopAtEnd();
        
        bool IsEmpty { get; }

        IEnumerable<T> AsEnumerableWithoutPop();

        bool Contains(Predicate<T> predicate);
        
        bool Contains(T element);
    }
}