using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class MyVector<T>
    {
        private MyArrayList<T> _elementData;
        private int _elementCount;
        private readonly int _capacityIncrement;
        
        public MyVector()
        {
            _elementData = new MyArrayList<T>(10);
            _elementCount = 0;
            _capacityIncrement = 0;
        }
        
        public MyVector(IReadOnlyList<T> a)
        {
            _elementData = new MyArrayList<T>(a.Count);
            for(var i = 0; i < a.Count; i++)
            {
                _elementData.Set(i, a[i]);
            }
            _capacityIncrement = 0;
        }
        
        public MyVector(int initialCapacity)
        {
            if (initialCapacity < 0)
                throw new ArgumentOutOfRangeException(nameof(initialCapacity),
                    "Ёмкость не может быть отрицательной.");
            _elementData = new MyArrayList<T>(initialCapacity);
            _elementCount = 0;
            _capacityIncrement = 0;
        }
        
        public MyVector(int initialCapacity, int capacityIncrement)
        {
            _elementCount = initialCapacity;
            this._capacityIncrement = capacityIncrement;
        }
        
        public void Add(T e)
        {
            if (_elementCount == _elementData.Size())
            {
                if(_capacityIncrement != 0)
                    Resize(_elementData.Size() + _capacityIncrement);
                else
                    Resize(_elementData.Size() * 2);
            }
            _elementData.Set(_elementCount++, e);
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
            _elementData.Clear();
            _elementCount = 0;
        }

        public bool Contains(object o)
        {
            for (int i = 0; i < _elementCount; i++)
            {
                if (_elementData.Get(i)?.Equals(o) == true)
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
        
        public void Resize(int newCapacity)
        {
            var newArray = new MyArrayList<T>(newCapacity);
            _elementData.Resize(newCapacity);
            _elementData = newArray;
        }
        
        public bool IsEmpty()
        {
            return _elementCount == 0;
        }
        
        public bool Remove(T o)
        {
            for (int i = 0; i < _elementCount; i++)
            {
                if (!Equals(_elementData.Get(i), o)) continue;
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
                if (Array.IndexOf(a, _elementData.Get(i)) == -1)
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
            return _elementData.ToArray();
        }
        
        public T[] ToArray(T[] a)
        {
            return _elementData.ToArray(a);
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
                _elementData.Set(i, _elementData.Get(i - 1));
            }
            _elementData.Set(index, e);
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
                _elementData.Set(i + a.Length, _elementData.Get(i));
            }
            for (int j = 0; j < a.Length; j++)
            {
                _elementData.Set(index + j, a[j]);
            }
            _elementCount += a.Length;
        }
        
        public T Get(int index)
        {
            if (index < 0 || index >= _elementCount)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимых значений");
            }
            return _elementData.Get(index);
        }
        
        public int IndexOf(T o)
        {
            for (int i = 0; i < _elementCount; i++)
            {
                if (Equals(_elementData.Get(i), o))
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
                if (Equals(_elementData.Get(i), o))
                {
                    return i;
                }
            }
            return -1;
        }

        public T Remove(int index)
        {
            if (index < 0 || index >= _elementCount)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимых значений");
            }
            var removedElement = _elementData.Get(index);
            for (int i = index; i < _elementCount - 1; i++)
            {
                _elementData.Set(i, _elementData.Get(i + 1));
            }
            _elementData.Set(_elementCount - 1, default(T));
            _elementCount--;
            return removedElement;
        }
        
        public T Set(int index, T e)
        {
            if (index < 0 || index >= _elementCount)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимых значений");
            }
            var oldElement = _elementData.Get(index);
            _elementData.Set(index, e);
            return oldElement;
        }
        
        public void EnsureCapacity(int min)
        {
            _elementData.EnsureCapacity(min);
        }
        
        public MyVector<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex > _elementCount || fromIndex >= toIndex)
                throw new ArgumentOutOfRangeException("Недопустимые индексы");

            var sublist = new MyVector<T>();
            for (int i = fromIndex; i < toIndex; i++)
            {
                sublist.Add(_elementData.Get(i));
            }
            return sublist;
        }

        public T FirstElement()
        {
            try
            {
                return _elementData.Get(0);
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
                return _elementData.Get(_elementCount - 1);
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
