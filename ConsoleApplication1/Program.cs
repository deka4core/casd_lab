using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class Heap<T> where T : IComparable<T>
    {
        private List<T> _elements;
        private bool _isMaxHeap;
        
        public Heap(T[] items, bool isMaxHeap = true)
        {
            this._isMaxHeap = isMaxHeap;
            _elements = new List<T>(items);
            for (int i = _elements.Count / 2 - 1; i >= 0; i--)
            {
                HeapifyDown(i);
            }
        }
        
        public T GetExtreme() // max min
        {
            if (_elements.Count == 0)
                throw new InvalidOperationException("Куча пустая");
            return _elements[0];
        }
        public T RemoveExtreme() // max min
        {
            if (_elements.Count == 0)
                throw new InvalidOperationException("Куча пустая");
            T extreme = _elements[0];
            _elements[0] = _elements[_elements.Count - 1];
            _elements.RemoveAt(_elements.Count - 1);
            HeapifyDown(0);
            return extreme;
        }
        public void ChangeKey(int index, T newValue)
        {
            if (index < 0 || index >= _elements.Count)
                throw new ArgumentOutOfRangeException("Индекс вне диапазона");
        
            if ((_isMaxHeap && newValue.CompareTo(_elements[index]) < 0) ||
                (!_isMaxHeap && newValue.CompareTo(_elements[index]) > 0))
            {
                throw new ArgumentException("Новое значение не подходит к текущему типу кучи.");
            }
            _elements[index] = newValue;
            if (_isMaxHeap)
            {
                HeapifyUp(index);
            }
            else
            {
                HeapifyDown(index);
            }
        }
        public void Add(T item)
        {
            _elements.Add(item);
            if (_isMaxHeap)
            {
                HeapifyUp(_elements.Count - 1);
            }
            else
            {
                HeapifyDown(_elements.Count - 1);
            }
        }
        public Heap<T> Merge(Heap<T> otherHeap)
        {
            if (this._isMaxHeap != otherHeap._isMaxHeap)
                throw new InvalidOperationException("Невозможно провести слияние куч разных типов");
        
            Heap<T> newHeap = new Heap<T>(_elements.ToArray(), _isMaxHeap);
            foreach (var item in otherHeap._elements)
            {
                newHeap.Add(item);
            }
            return newHeap;
        }
        private void HeapifyUp(int index)
        {
            int parentIndex = (index - 1) / 2;
            while (index > 0 && ((_isMaxHeap && _elements[index].CompareTo(_elements[parentIndex]) > 0) ||
                                 (!_isMaxHeap && _elements[index].CompareTo(_elements[parentIndex]) < 0)))
            {
                Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
        }
        private void HeapifyDown(int index)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int extremeIndex = index;
            if (leftChildIndex < _elements.Count && ((_isMaxHeap && _elements[leftChildIndex].CompareTo(_elements[extremeIndex]) > 0) ||
                                                    (!_isMaxHeap && _elements[leftChildIndex].CompareTo(_elements[extremeIndex]) < 0)))
            {
                extremeIndex = leftChildIndex;
            }
            if (rightChildIndex < _elements.Count && ((_isMaxHeap && _elements[rightChildIndex].CompareTo(_elements[extremeIndex]) > 0) ||
                                                     (!_isMaxHeap && _elements[rightChildIndex].CompareTo(_elements[extremeIndex]) < 0)))
            {
                extremeIndex = rightChildIndex;
            }
            if (extremeIndex != index)
            {
                Swap(index, extremeIndex);
                HeapifyDown(extremeIndex);
            }
        }
        private void Swap(int index1, int index2)
        {
            (_elements[index1], _elements[index2]) = (_elements[index2], _elements[index1]);
        }
    }

    internal abstract class Program
    {
        static void Main(string[] args)
        {
            int[] maxArray = { 10, 15, 20, 17, 25 };
            Heap<int> maxHeap = new Heap<int>(maxArray, true);
            Console.WriteLine("Максимум: " + maxHeap.GetExtreme());
            Console.WriteLine("Удаление максимума: " + maxHeap.RemoveExtreme());
            Console.WriteLine("Новый максимум: " + maxHeap.GetExtreme());
            maxHeap.Add(30);
            Console.WriteLine("Добавление 30, новый максимум: " + maxHeap.GetExtreme());
            maxHeap.ChangeKey(1, 22);
            Console.WriteLine("Увеличение ключа на индексе 1 до 22, новый максимум: " + maxHeap.GetExtreme());
            int[] minArray = { 10, 15, 20, 17, 25 };
            Heap<int> minHeap = new Heap<int>(minArray, false);
            Console.WriteLine("Минимум: " + minHeap.GetExtreme());
            Console.WriteLine("Удаление минимума: " + minHeap.RemoveExtreme());
            Console.WriteLine("Новый минимум: " + minHeap.GetExtreme());
            minHeap.Add(5);
            Console.WriteLine("Добавление 5, новый минимум: " + minHeap.GetExtreme());
            minHeap.ChangeKey(1, 12);
            Console.WriteLine("Уменьшение ключа на индексе 1 до 12, новый минимум: " + minHeap.GetExtreme());
        }
    }
}