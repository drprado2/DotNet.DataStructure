using System;
using System.Collections.Generic;
using DotNet.DataStructure.Shared;

namespace DotNet.DataStructure.Linear.Arrays
{
    public interface IOrderedArrayList<T> : ICollection<T> where T : IMathComparable<T>
    {
        T First();

        T Last();
        
        bool IsEmpty { get; }
        
        T this[int index] { get; set; }

        int IndexOf(T item);
        
        T AtPosition(int pos);
        
        bool RemoveAt(int pos);
    
        void AddRange(IEnumerable<T> collection);

        IOrderedArrayList<T> GetSubArrayList(int startPostion, int endPosition=-1);
        
        (int position, T element) BinarySearch(IMathComparable<T> comparator);
        
        (int position, T element) SearchFirst(Predicate<T> predicate);
        
        (int position, T element) SearchLast(Predicate<T> predicate);
        
        IEnumerable<T> SearchAll(Predicate<T> predicate);
    }
}