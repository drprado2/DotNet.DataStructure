﻿using System;
using System.Collections.Generic;
using DotNet.DataStructure.Shared;

namespace DotNet.DataStructure.Linear.Arrays
{
    public interface IArrayList<T> : ICollection<T>
    {
        T First();

        T Last();
        
        T GetAt(int pos);

        void ChangeAt(T element, int pos);
        
        void PushAt(T element, int pos);
        
        bool RemoveAt(int pos);

        void AddFirst(T element);
        
        void AddRange(IEnumerable<T> collection);

        void Reverse();
        
        T this[int index] { get; set; }

        int IndexOf(T item);

        bool IsEmpty { get; }

        IArrayList<T> GetSubArrayList(int startPostion, int quantity=-1);

        (int position, T element) SearchFirst(Predicate<T> predicate);
        
        (int position, T element) SearchLast(Predicate<T> predicate);
        
        IEnumerable<T> SearchAll(Predicate<T> predicate);

        void BubbleSort(Func<T, T, int> funcComparer);
        
        void InsertionSort(Func<T, T, int> funcComparer);
        
        void SelectionSort(Func<T, T, int> funcComparer);
        
        void MergeSort(Func<T, T, int> funcComparer);
        
        void QuickSort(Func<T, T, int> funcComparer);
        
        void HeapSort(Func<T, T, int> funcComparer);
    }
}