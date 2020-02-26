using System;
using System.Collections.Generic;

namespace DotNet.DataStructure.Linear.Matrices
{
    public interface ISparceMatrix<T> : IEnumerable<(int width, int height, T element)>
    {
        T this[int width, int height] { get; set; }
        
        (int width, int height) GetMatrixOrder();

        void AddAt(T element, int width, int height);
        
        T GetAt(int width, int height);
        
        T RemoveAt(int width, int height);
        
        bool Remove(T element);

        IEnumerable<((int width, int height) position, T element)> GetFilledCells();
        
        IEnumerable<int> GetEmptyCells();
        
        bool Contains(T element);
        
        bool Contains(Predicate<T> predicate);
        
        bool TrueForAll(Predicate<T> predicate);
        
        T SearchFirst(Predicate<T> predicate);
        
        IEnumerable<((int width, int height) position, T element)> SearchAll(Predicate<T> predicate);
    }
}