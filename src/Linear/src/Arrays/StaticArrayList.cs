using System;
using System.Collections;
using System.Collections.Generic;
using DotNet.DataStructure.Shared;

namespace DotNet.DataStructure.Linear.Arrays
{
    public class StaticArrayList<T> : IArrayList<T>, IRepeatControllable, IStaticDataStructure
    {
        private readonly bool _canRepeat;
        private int _maxSize;
        
        public StaticArrayList(int maxSize, bool canRepeat=true)
        {
            _canRepeat = canRepeat;
            _maxSize = maxSize;
        }

        public StaticArrayList(T[] array, int maxSize, bool canRepeat=true) : this(maxSize, canRepeat)
        {
            
        }

        public StaticArrayList(IEnumerable<T> enumerable, int maxSize, bool canRepeat=true) : this(maxSize, canRepeat)
        {
            
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
        public T First()
        {
            throw new NotImplementedException();
        }

        public T Last()
        {
            throw new NotImplementedException();
        }

        public T GetAt(int pos)
        {
            throw new NotImplementedException();
        }

        public void ChangeAt(T element, int pos)
        {
            throw new NotImplementedException();
        }

        public void PushAt(T element, int pos)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAt(int pos)
        {
            throw new NotImplementedException();
        }

        public void AddFirst(T element)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<T> collection)
        {
            throw new NotImplementedException();
        }

        public void Reverse()
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty { get; }
        public IArrayList<T> GetSubArrayList(int startPostion, int endPosition = -1)
        {
            throw new NotImplementedException();
        }

        public (int position, T element) SearchFirst(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public (int position, T element) SearchLast(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> SearchAll(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public void BubbleSort(Func<T, T, int> funcComparer)
        {
            throw new NotImplementedException();
        }

        public void InsertionSort(Func<T, T, int> funcComparer)
        {
            throw new NotImplementedException();
        }

        public void SelectionSort(Func<T, T, int> funcComparer)
        {
            throw new NotImplementedException();
        }

        public void MergeSort(Func<T, T, int> funcComparer)
        {
            throw new NotImplementedException();
        }

        public void QuickSort(Func<T, T, int> funcComparer)
        {
            throw new NotImplementedException();
        }

        public void HeapSort(Func<T, T, int> funcComparer)
        {
            throw new NotImplementedException();
        }

        public bool CanRepeat => _canRepeat;
        public int MaxSize => _maxSize;
        
        public void Resize(int size)
        {
            throw new NotImplementedException();
        }

        public bool IsFull => Count == MaxSize;
    }
}