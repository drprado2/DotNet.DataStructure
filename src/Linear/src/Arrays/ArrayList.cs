using System;
using System.Collections;
using System.Collections.Generic;
using DotNet.DataStructure.Shared;

namespace DotNet.DataStructure.Linear.Arrays
{
    public class ArrayList<T> : IArrayList<T>, IRepeatControllable
    {
        private const int DefaultAddElementQuantity = 4;
        private const int SentinelPosition = 1;
        private readonly bool _canRepeat;
        private int _currentMaxElements = DefaultAddElementQuantity;
        private T[] _array = new T[DefaultAddElementQuantity + SentinelPosition];
        private int _count;

        private void UpdateCurrentMaxElements() => _currentMaxElements = _array.Length - SentinelPosition;

        private void CheckArrayPosition(int pos)
        {
            if (pos < 0 || pos >= _count) throw new IndexOutOfRangeException();
        }

        private void ExpandArray(int? quantity=null)
        {
            var newArray = quantity.HasValue ? new T[_array.Length + quantity.Value + SentinelPosition] : new T[_array.Length * 2 + SentinelPosition];
            _array.CopyTo(newArray, 0);
            _array = newArray;
            UpdateCurrentMaxElements();
        }

        private void CheckRepetition(T newElement)
        {
            if (_canRepeat)
                return;

            for (var i = 0; i < Count; i++)
                if (_array[i].Equals(newElement))
                    throw new ElementAlreadyExistsException(newElement);
        }

        private void BeforeAddElement(T element)
        {
            CheckRepetition(element);
            if (Count == _currentMaxElements)
                ExpandArray();
        }

        public ArrayList(bool canRepeat = true)
        {
            _canRepeat = canRepeat;
        }

        public ArrayList(T[] array, bool canRepeat = true) : this(canRepeat)
        {
            if (array.Length >= _array.Length)
                ExpandArray(array.Length);
            if (!canRepeat)
                for (var i = 0; i < array.Length; i++)
                {
                    for (var i1 = i + 1; i1 < array.Length; i1++)
                    {
                        if (array[i].Equals(array[i1]))
                            throw new ElementAlreadyExistsException(array[i1]);
                    }
                }

            array.CopyTo(_array, 0);
            _count = array.Length;
        }

        public ArrayList(IEnumerable<T> enumerable, bool canRepeat = true) : this(canRepeat)
        {
            AddRange(enumerable);
        }

        public ArrayList(bool canRepeat = true, params T[] elements) : this(elements, canRepeat)
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < _count; i++)
                yield return _array[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            BeforeAddElement(item);
            _array[_count] = item;
            _count++;
        }

        public void Clear()
        {
            _array = new T[DefaultAddElementQuantity + SentinelPosition];
            _count = 0;
            _currentMaxElements = DefaultAddElementQuantity;
        }

        public bool Contains(T item) => IndexOf(item) > -1;

        public void CopyTo(T[] array, int arrayIndex)
        {
            CheckArrayPosition(arrayIndex);
            if (array == null || array.Length < _count - arrayIndex)
                throw new ArgumentException("The array argument should be not null and have the length to support the transfer");
            
            var currentPos = 0;
            for (var i = arrayIndex; i < _count; i++)
            {
                array[currentPos] = _array[i];
                currentPos++;
            }
        }

        public void PullUntilPosition(int pos)
        {
            for (var i = pos; i < _count - 1; i++)
                _array[i] = _array[i + 1];
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index < 0)
                return false;
            
            PullUntilPosition(index);

            _count--;
            return true;
        }

        public int Count => _count;

        public bool IsReadOnly => false;

        public T First() => _count == 0 ? throw new IndexOutOfRangeException() : _array[0];

        public T FirstOrDefault() => _array[0];
        
        public T Last() => _count == 0 ? throw new IndexOutOfRangeException() : _array[_count - 1];

        public T LastOrDefault() => _count > 0 ? _array[_count - 1] : default;

        public T GetAt(int pos)
        {
            CheckArrayPosition(pos);
            return _array[pos];
        }

        public T GetAt(Index index) => GetAt(index.Value);

        public void ChangeAt(T element, int pos)
        {
            CheckArrayPosition(pos);
            CheckRepetition(element);
            _array[pos] = element;
        }

        public void ChangeAt(T element, Index pos) => ChangeAt(element, pos.Value);

        public void PushAt(T element, int pos)
        {
            CheckArrayPosition(pos);
            BeforeAddElement(element);

            for (var i = _count; i > pos; i--)
                _array[i] = _array[i - 1];

            _count++;
            _array[pos] = element;
        }

        public void RemoveAt(int pos)
        {
            CheckArrayPosition(pos);
            
            PullUntilPosition(pos);
            _count--;
        }

        public void AddFirst(T element)
        {
            if (IsEmpty)
            {
                Add(element);
                return;
            }
                
            PushAt(element, 0);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var el in collection)
                Add(el);
        }

        public void Reverse()
        {
            var midPos = _count / 2;
            for (var i = 0; i < midPos; i++)
            {
                var temp = _array[i];
                _array[i] = _array[_count - 1 - i];
                _array[_count - 1 - i] = temp;
            }
        }

        public T this[int index]
        {
            get => GetAt(index);
            set => ChangeAt(value, index);
        }
        
        public T this[Index index]
        {
            get => GetAt(index);
            set => ChangeAt(value, index);
        }

        public int IndexOf(T item)
        {
            if (item == null || _count == 0)
                return -1;

            var sentinelPosition = _count;
            _array[sentinelPosition] = item;
            var currentIndex = 0;
            var currentEl = _array[currentIndex];
            while (!currentEl.Equals(item))
            {
                currentIndex++;
                currentEl = _array[currentIndex];
            }

            return currentIndex == sentinelPosition ? -1 : currentIndex;
        }

        public bool IsEmpty => _count <= 0;

        public IArrayList<T> Slice(int startPostion, int quantity = -1)
        {
            var finalPos = quantity == -1 ? _count : startPostion + quantity;
            var areArgumentsInvalid = quantity < -1 || startPostion < 0 || startPostion >= _count || finalPos > _count;
            if(areArgumentsInvalid)
                throw new IndexOutOfRangeException();
            return new ArrayList<T>(_array[startPostion..finalPos]);
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
    }
}