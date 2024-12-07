using System;

namespace ca4
{
    public class Heap<T> where T : IComparable<T>
    {
        private T[] _elements;
        private int _size;
        private readonly bool _isMaxHeap;
        
        // Конструктор класса Heap.  Инициализирует кучу на основе входного массива.
        public Heap(T[] array, bool isMaxHeap=true)
        {
            this._isMaxHeap = isMaxHeap;
            this._size = array.Length;
            _elements = new T[_size];
            Array.Copy(array, _elements, _size);
            BuildHeap();
        }
        
        // Вспомогательный метод для построения кучи из массива.
        private void BuildHeap()
        {
            for (var i = _size / 2 - 1; i >= 0; i--)
            {
                Heapify(i);
            }
        }
        
        // Метод для извлечения максимального (или минимального) элемента из кучи.
        public T Peek()
        {
            if (_size == 0)
                throw new InvalidOperationException("Heap is empty.");
            return _elements[0];
        }
        
        // Метод для извлечения и удаления максимального (или минимального) элемента из кучи.
        public T Extract()
        {
            if (_size == 0)
                throw new InvalidOperationException("Heap is empty.");

            var root = _elements[0];
            _elements[0] = _elements[_size - 1];
            _size--;
            Heapify(0);
            return root;
        }
        
        public void IncreaseKey(int index, T newValue)
        {
            if (index < 0 || index >= _size)
                throw new ArgumentOutOfRangeException(nameof(index));

            if ((_isMaxHeap && newValue.CompareTo(_elements[index]) < 0) ||
                (!_isMaxHeap && newValue.CompareTo(_elements[index]) > 0))
                throw new ArgumentException("New value is not applicable.");

            _elements[index] = newValue;

            while (index > 0 && Compare(_elements[Parent(index)], _elements[index]) > 0)
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }
        }
        
        public void Add(T value)
        {
            if (_size == _elements.Length)
            {
                Array.Resize(ref _elements, _size * 2);
            }

            _elements[_size] = value;
            _size++;
            var index = _size - 1;

            while (index > 0 && Compare(_elements[Parent(index)], _elements[index]) > 0)
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }
        }
        
        public Heap<T> Merge(Heap<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            var mergedArray = new T[_size + other._size];
            Array.Copy(_elements, mergedArray, _size);
            Array.Copy(other._elements, 0, mergedArray, _size, other._size);
            return new Heap<T>(mergedArray, _isMaxHeap);
        }
        
        // Метод Heapify, восстанавливающий свойство кучи.
        private void Heapify(int index)
        {
            while (true)
            {
                var left = LeftChild(index);
                var right = RightChild(index);
                var extreme = index;

                if (left < _size && Compare(_elements[left], _elements[extreme]) < 0) extreme = left;

                if (right < _size && Compare(_elements[right], _elements[extreme]) < 0) extreme = right;

                if (extreme == index) return;
                Swap(index, extreme);
                index = extreme;
            }
        }

        private static int Parent(int index) => (index - 1) / 2;
        private static int LeftChild(int index) => 2 * index + 1;
        private static int RightChild(int index) => 2 * index + 2;

        private void Swap(int i, int j) => (_elements[i], _elements[j]) = (_elements[j], _elements[i]);
        
        private int Compare(T x, T y) => _isMaxHeap ? x.CompareTo(y) : y.CompareTo(x);
    }
}
