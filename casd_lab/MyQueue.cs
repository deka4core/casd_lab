using System;
using System.Collections.Generic;
using System.Linq;

namespace ca4
{
    public class MyPriorityQueue<T>
    {
        private T[] _queue;
        private int _size;
        private readonly IComparer<T> _comparator;
        
        public MyPriorityQueue() : this(11) { }

        public MyPriorityQueue(T[] a)
        {
            _queue = new T[a.Length];
            Array.Copy(a, _queue, a.Length);
            _size = a.Length;
            _comparator = Comparer<T>.Default;
            BuildHeap();
        }

        public MyPriorityQueue(int initialCapacity, IComparer<T> comparator=null)
        {
            this._queue = new T[initialCapacity];
            this._size = 0;
            this._comparator = comparator ?? Comparer<T>.Default;
        }

        public MyPriorityQueue(MyPriorityQueue<T> c)
        {
            _queue = new T[c._size];
            Array.Copy(c._queue, _queue, c._size);
            _size = c._size;
            _comparator = c._comparator;
            BuildHeap();
        }

        private void BuildHeap()
        {
            for (var i = _size / 2 - 1; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        private void Heapify(int index)
        {
            while (true)
            {
                var left = LeftChild(index);
                var right = RightChild(index);
                var extreme = index;

                if (left < _size && Compare(_queue[left], _queue[extreme]) < 0) extreme = left;
                if (right < _size && Compare(_queue[right], _queue[extreme]) < 0) extreme = right;

                if (extreme == index) return;
                Swap(index, extreme);
                index = extreme;
            }
        }

        private static int LeftChild(int index) => 2 * index + 1;
        private static int RightChild(int index) => 2 * index + 2;

        private void Swap(int i, int j) => (_queue[i], _queue[j]) = (_queue[j], _queue[i]);

        private int Compare(T x, T y) => _comparator.Compare(x, y);
        
        public void Add(T e)
        {
            if (_size >= _queue.Length)
                Resize();

            _queue[_size] = e;
            _size++;
            HeapifyUp(_size - 1);
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                var parent = (index - 1) / 2;
                if (Compare(_queue[parent], _queue[index]) <= 0) break;
                Swap(parent, index);
                index = parent;
            }
        }

        private void Resize()
        {
            var newCapacity = _queue.Length < 64 ? _queue.Length + 2 : (int)(_queue.Length * 1.5);
            Array.Resize(ref _queue, newCapacity);
        }
        
        public void AddAll(IEnumerable<T> a)
        {
            foreach (var item in a)
            {
                Add(item);
            }
        }
        
        public void Clear()
        {
            _queue = new T[_queue.Length];
            _size = 0;
        }
        
        public bool Contains(object o)
        {
            if (!(o is T item)) return false;
            for (var i = 0; i < _size; i++)
            {
                if (_queue[i].Equals(item))
                    return true;
            }
            return false;
        }

        public bool ContainsAll(IEnumerable<T> a) => a.All(item => Contains(item));

        public bool IsEmpty() => _size == 0;
        
        public bool Remove(object o)
        {
            if (!(o is T item)) return false;
            for (var i = 0; i < _size; i++)
            {
                if (!_queue[i].Equals(item)) continue;
                RemoveAt(i);
                return true;
            }
            return false;
        }

        private void RemoveAt(int index)
        {
            if (index < 0 || index >= _size)
                throw new ArgumentOutOfRangeException(nameof(index));

            _queue[index] = _queue[_size - 1];
            _size--;
            Heapify(index);
        }

        public void RemoveAll(IEnumerable<T> a)
        {
            foreach (var item in a)
            {
                Remove(item);
            }
        }

        public void RetainAll(IEnumerable<T> a)
        {
            var toRetain = new HashSet<T>(a);
            for (var i = 0; i < _size; i++)
            {
                if (toRetain.Contains(_queue[i])) continue;
                RemoveAt(i);
                i--;
            }
        }

        public int Size() => _size;

        public T[] ToArray()
        {
            var result = new T[_size];
            Array.Copy(_queue, result, _size);
            return result;
        }
        
        public T[] ToArray(T[] a)
        {
            if (a == null || a.Length < _size)
                a = new T[_size];
            Array.Copy(_queue, a, _size);
            return a;
        }
        
        public T Element()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Priority queue is empty.");
            return _queue[0];
        }
        
        public bool Offer(T obj)
        {
            try
            {
                Add(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public T Peek() => IsEmpty() ? default(T) : _queue[0];

        public T Poll()
        {
            if (IsEmpty())
                return default(T);
            var result = _queue[0];
            RemoveAt(0);
            return result;
        }
    }
}
