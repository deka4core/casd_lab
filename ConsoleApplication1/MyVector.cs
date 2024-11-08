using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{

    public class MyVector<T>
    {
        private T[] _elementData;
        private int _elementCount;
        private readonly int _capacityIncrement;
        
        public MyVector()
        {
            _elementData = new T[10];
            _elementCount = 0;
            _capacityIncrement = 0;
        }
        
        public MyVector(IReadOnlyList<T> a)
        {
            _elementData = new T[a.Count];
            for(var i = 0; i < a.Count; i++)
            {
                _elementData[i] = a[i];
            }
            _capacityIncrement = 0;
        }
        
        public MyVector(int initialCapacity)
        {
            if (initialCapacity < 0)
                throw new ArgumentOutOfRangeException(nameof(initialCapacity),
                    "Ёмкость не может быть отрицательной.");
            _elementData = new T[initialCapacity];
            _elementCount = 0;
            _capacityIncrement = 0;
        }
        
        //new
        public MyVector(int initialCapacity, int capacityIncrement)
        {
            _elementCount = initialCapacity;
            this._capacityIncrement = capacityIncrement;
        }
        
        private void Add(T e)
        {
            if (_elementCount == _elementData.Length)
            {
                if(_capacityIncrement != 0)
                    Resize(_elementData.Length + _capacityIncrement);
                else
                    Resize(_elementData.Length * 2);
            }
            _elementData[_elementCount++] = e;
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
            Array.Clear(_elementData, 0, _elementCount);
            _elementCount = 0;
        }

        private bool Contains(object o)
        {
            for (int i = 0; i < _elementCount; i++)
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
            return a.All(e => Contains(e));
        }
        
        private void Resize(int newCapacity)
        {
            var newArray = new T[newCapacity];
            Array.Copy(_elementData, newArray, _elementCount);
            _elementData = newArray;
        }
        
        public bool IsEmpty()
        {
            return _elementCount == 0;
        }
        
        private bool Remove(T o)
        {
            for (int i = 0; i < _elementCount; i++)
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
            for (int i = _elementCount - 1; i >= 0; i--)
            {
                if (Array.IndexOf(a, _elementData[i]) == -1)
                {
                    Remove(i);
                }
            }
        }
        
        public int Size()
        {
            return _elementCount;
        }
        
        
        public T[] ToArray()
        {
            var result = new T[_elementCount];
            Array.Copy(_elementData, result, _elementCount);
            return result;
        }
        
        public T[] ToArray(T[] a)
        {
            if (a == null || a.Length < _elementCount)
            {
                a = new T[_elementCount];
            }
            Array.Copy(_elementData, a, _elementCount);
            return a;
        }
        
        public void Add(int index, T e)
        {
            if (index < 0 || index > _elementCount)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимых значений");
            }
            EnsureCapacity(_elementCount + 1);
            for (int i = _elementCount; i > index; i--)
            {
                _elementData[i] = _elementData[i - 1];
            }
            _elementData[index] = e;
            _elementCount++;
        }

        public void AddAll(int index, T[] a)
        {
            if (index < 0 || index > _elementCount)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимых значений");
            }
            EnsureCapacity(_elementCount + a.Length);
            for (int i = _elementCount - 1; i >= index; i--)
            {
                _elementData[i + a.Length] = _elementData[i];
            }
            for (int j = 0; j < a.Length; j++)
            {
                _elementData[index + j] = a[j];
            }
            _elementCount += a.Length;
        }
        
        public T Get(int index)
        {
            if (index < 0 || index >= _elementCount)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимых значений");
            }
            return _elementData[index];
        }
        
        public int IndexOf(T o)
        {
            for (int i = 0; i < _elementCount; i++)
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
            for (int i = _elementCount - 1; i >= 0; i--)
            {
                if (Equals(_elementData[i], o))
                {
                    return i;
                }
            }
            return -1;
        }

        private T Remove(int index)
        {
            if (index < 0 || index >= _elementCount)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимых значений");
            }
            var removedElement = _elementData[index];
            for (int i = index; i < _elementCount - 1; i++)
            {
                _elementData[i] = _elementData[i + 1];
            }
            _elementData[_elementCount - 1] = default(T);
            _elementCount--;
            return removedElement;
        }
        
        public T Set(int index, T e)
        {
            if (index < 0 || index >= _elementCount)
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
            int newCapacity;
            if (_capacityIncrement !=  0)
                newCapacity = _elementData.Length + _capacityIncrement;
            else
                newCapacity = _elementData.Length * 2;
            if (newCapacity < min) newCapacity = min;
            var newArray = new T[newCapacity];
            Array.Copy(_elementData, newArray, _elementCount);
            _elementData = newArray;
        }
        
        public MyVector<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex > _elementCount || fromIndex >= toIndex)
                throw new ArgumentOutOfRangeException("Недопустимые индексы");

            var sublist = new MyVector<T>();
            for (int i = fromIndex; i < toIndex; i++)
            {
                sublist.Add(_elementData[i]);
            }
            return sublist;
        }

        public T FirstElement()
        {
            try
            {
                return _elementData[0];
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException("Элемент вне индекса");
            }
        }

        public T LastElement()
        {
            try
            {
                return _elementData[_elementCount - 1];
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException("Элемент вне индекса");
            }
        }
        
        public void RemoveElementAt(int pos)
        {
            try
            {
                this.Remove(pos);
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException("Элемент вне индекса");
            }
        }
        
        public void RemoveRange(int begin, int end)
        {
            try
            {
                for (int i = begin; i < end; i++)
                {
                    this.Remove(i);
                }
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException("Элемент вне диапазона");
            }
        }
    }
}