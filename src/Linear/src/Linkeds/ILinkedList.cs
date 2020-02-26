using System;
using System.Collections.Generic;
using DotNet.DataStructure.Shared;

namespace DotNet.DataStructure.Linear.Linkeds
{
    public interface ILinkedList<T> : ICollection<T>
    {
        T First();

        T Last();
        
        bool IsEmpty { get; }
        
        bool AddAfter(T oldElement, T newElement);
        
        bool AddBefore(T oldElement, T newElement);
        
        bool Change(T oldElement, T newElement);
        
        T GetNext(T element);
        
        T GetPrevious(T element);

        void AddFirst(T element);
        
        void AddLast(T element);
        
        void AddRange(IEnumerable<T> collection);

        void Reverse();

        T SearchFirst(Predicate<T> predicate);
        
        T SearchLast(Predicate<T> predicate);
        
        IEnumerable<T> SearchAll(Predicate<T> predicate);
        
        void BubbleSort(IComparer<T> comparer);
        
        void InsertionSort(IComparer<T> comparer);
        
        void SelectionSort(IComparer<T> comparer);
        
        void MergeSort(IComparer<T> comparer);
        
        void QuickSort(IComparer<T> comparer);
        
        void HeapSort(IComparer<T> comparer);
    }
}