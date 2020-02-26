using System;
using System.Collections.Generic;
using DotNet.DataStructure.Shared;

namespace DotNet.DataStructure.Linear.Arrays
{
    public interface IArrayList<T> : ICollection<T>
    {
        T First();

        T Last();
        
        T AtPosition(int pos);

        void ChangeAtPosition(T element, int pos);
        
        void PushAtPosition(T element, int pos);
        
        bool RemoveAt(int pos);

        void AddFirst(T element);
        
        void AddLast(T element);

        void AddRange(IEnumerable<T> collection);

        void Reverse();
        
        T this[int index] { get; set; }

        int IndexOf(T item);

        bool IsEmpty { get; }

        IArrayList<T> GetSubArrayList(int startPostion, int endPosition=-1);

        (int position, T element) SearchFirst(Predicate<T> predicate);
        
        (int position, T element) SearchLast(Predicate<T> predicate);
        
        IEnumerable<T> SearchAll(Predicate<T> predicate);

        void BubbleSort(IComparer<T> comparer);
        
        void InsertionSort(IComparer<T> comparer);
        
        void SelectionSort(IComparer<T> comparer);
        
        void MergeSort(IComparer<T> comparer);
        
        void QuickSort(IComparer<T> comparer);
        
        void HeapSort(IComparer<T> comparer);
    }
}