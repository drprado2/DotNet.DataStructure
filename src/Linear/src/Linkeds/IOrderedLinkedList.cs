using System;
using System.Collections.Generic;
using DotNet.DataStructure.Shared;

namespace DotNet.DataStructure.Linear.Linkeds
{
    public interface IOrderedLinkedList<T> : ICollection<T> where T : IMathComparable<T>
    {
        T First();

        T Last();
        
        bool IsEmpty { get; }
        
        T GetNext(T element);
        
        T GetPrevious(T element);

        void AddRange(IEnumerable<T> collection);

        T SearchFirst(Predicate<T> predicate);
        
        T SearchLast(Predicate<T> predicate);
        
        IEnumerable<T> SearchAll(Predicate<T> predicate);
    }
}