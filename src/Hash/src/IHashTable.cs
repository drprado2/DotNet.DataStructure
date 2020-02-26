using System;
using System.Collections.Generic;
using DotNet.DataStructure.Shared;

namespace DotNet.DataStructure.Hash
{
    public interface IHashTable<T>  : ICollection<T>, IRepeatControllable
    {
        bool IsEmpty { get; }
        
        T  SearchFirst(Predicate<T> predicate);
        
        IEnumerable<T> SearchAll(Predicate<T> predicate);
    }
}