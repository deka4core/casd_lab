using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{

public class MyArrayList<T>
{
        private T[] _elementData;
        private int _size;
        
        public MyArrayList()
        {
            _elementData = new T[10];
            _size = 0;
        }
        
        public MyArrayList(T[] a)
        {
            _elementData = new T[a.Length];
            Array.Copy(a, _elementData, a.Length);
            _size = a.Length;
        }

        public MyArrayList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Ёмкость не может быть меньше нуля.");
            _elementData = new T[capacity];
            _size = 0;
        }

        public void Add(T e)
        {
            if (_size == _elementData.Length)
            {
                Resize(_elementData.Length * 3 / 2 + 1);
            }
            _elementData[_size++] = e;
        }

        public void AddAll(IEnumerable<T> a)
        {
            foreach (var e in a)
            {
                Add(e);
            }
        }

        public void Clear()
        {
            Array.Clear(_elementData, 0, _size);
            _size = 0;
        }

        private bool Contains(object o)
        {
            for (var i = 0; i < _size; i++)
            {
                if (_elementData[i]?.Equals(o) == true)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsAll(IEnumerable<T> a)
        {
            return a.All(e => Contains(e)); // LINQ-выражение. Содержит в себе все элементы из массива а
        }

        private void Resize(int newCapacity)
        {
            var newArray = new T[newCapacity];
            Array.Copy(_elementData, newArray, _size);
            _elementData = newArray;
        }
        
        public bool IsEmpty()
        {
            return _size == 0;
        }

        private bool Remove(T o)
        {
            for (var i = 0; i < _size; i++)
            {
                if (!Equals(_elementData[i], o)) continue;
                Remove(i);
                return true;
            }
            return false;
        }

        public void RemoveAll(IEnumerable<T> a)
        {
            foreach (var item in a)
            {
                Remove(item);
            }
        }

        public void RetainAll(T[] a)
        {
            for (var i = _size - 1; i >= 0; i--)
            {
                if (Array.IndexOf(a, _elementData[i]) == -1)
                {
                    Remove(i);
                }
            }
        }
        
        public int Size()
        {
            return _size;
        }
        
        public T[] ToArray()
        {
            var result = new T[_size];
            Array.Copy(_elementData, result, _size);
            return result;
        }
        
        public T[] ToArray(T[] a)
        {
            if (a == null || a.Length < _size)
            {
                a = new T[_size];
            }
            Array.Copy(_elementData, a, _size);
            return a;
        }
        
        public void Add(int index, T e)
        {
            if (index < 0 || index > _size)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимых значений");
            }
            EnsureCapacity(_size + 1);
            for (var i = _size; i > index; i--)
            {
                _elementData[i] = _elementData[i - 1];
            }
            _elementData[index] = e;
            _size++;
        }

        public void AddAll(int index, T[] a)
        {
            if (index < 0 || index > _size)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимых значений");
            }
            EnsureCapacity(_size + a.Length);
            for (var i = _size - 1; i >= index; i--)
            {
                _elementData[i + a.Length] = _elementData[i];
            }
            for (var j = 0; j < a.Length; j++)
            {
                _elementData[index + j] = a[j];
            }
            _size += a.Length;
        }

        public T Get(int index)
        {
            if (index < 0 || index >= _size)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимых значений");
            }
            return _elementData[index];
        }

        public int IndexOf(T o)
        {
            for (var i = 0; i < _size; i++)
            {
                if (Equals(_elementData[i], o))
                {
                    return i;
                }
            }
            return -1;
        }

        public int LastIndexOf(T o)
        {
            for (var i = _size - 1; i >= 0; i--)
            {
                if (Equals(_elementData[i], o))
                {
                    return i;
                }
            }
            return -1;
        }
        
        public T Remove(int index)
        {
            if (index < 0 || index >= _size)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимых значений");
            }
            var removedElement = _elementData[index];
            for (var i = index; i < _size - 1; i++)
            {
                _elementData[i] = _elementData[i + 1];
            }
            _elementData[_size - 1] = default(T);
            _size--;
            return removedElement;
        }
        
        public T Set(int index, T e)
        {
            if (index < 0 || index >= _size)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимых значений");
            }
            var oldElement = _elementData[index];
            _elementData[index] = e;
            return oldElement;
        }

        private void EnsureCapacity(int min)
        {
            if (_elementData.Length >= min) return;
            var newCapacity = _elementData.Length * 2;
            if (newCapacity < min) newCapacity = min;
            var newArray = new T[newCapacity];
            Array.Copy(_elementData, newArray, _size);
            _elementData = newArray;
        }

        public MyArrayList<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex > _size || fromIndex >= toIndex)
                throw new ArgumentOutOfRangeException("Недопустимые индексы");

            var sublist = new MyArrayList<T>();
            for (var i = fromIndex; i < toIndex; i++)
            {
                sublist.Add(_elementData[i]);
            }

            return sublist;
        }
}
    
}