using System;
using System.Collections.Generic;
using System.Linq;

namespace ca4
{
    public class MyArrayDeque<T>
    {
        private T[] elements;
        private int head;
        private int tail;
        private int size;

        private const int DefaultCapacity = 16;

        public MyArrayDeque()
        {
            elements = new T[DefaultCapacity];
            head = 0;
            tail = 0;
            size = 0;
        }
        
        public MyArrayDeque(T[] a)
        {
            elements = new T[Math.Max(a.Length, DefaultCapacity)];
            Array.Copy(a, 0, elements, head, a.Length);
            tail = a.Length;
            size = a.Length;
        }
        
        public MyArrayDeque(int numElements)
        {
            if (numElements <= 0)
                throw new ArgumentException("Capacity must be greater than zero.");

            elements = new T[numElements];
            head = 0;
            tail = 0;
            size = 0;
        }
        
        void Add(T e)
        {
            EnsureCapacity(size + 1);
            elements[tail] = e;
            tail = (tail + 1) % elements.Length;
            size++;
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
            head = 0;
            tail = 0;
            size = 0;
        }
        
        private bool Contains(object o)
        {
            for (var i = 0; i < size; i++)
            {
                if (elements[(head + i) % elements.Length]?.Equals(o) == true)
                {
                    return true;
                }
            }
            return false;
        }
        public bool ContainsAll(IEnumerable<T> a)
        {
            return a.All(item => Contains(item));
        }
        
        bool IsEmpty()
        {
            return size == 0;
        }
        
        public bool Remove(object o)
        {
            for (var i = 0; i < size; i++)
            {
                var index = (head + i) % elements.Length;
                if (elements[index]?.Equals(o) != true) continue;
                RemoveAt(index);
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
            for (var i = size - 1; i >= 0; i--)
            {
                if (!Array.Exists(a, e => e.Equals(elements[(head + i) % elements.Length])))
                {
                    RemoveAt((head + i) % elements.Length);
                }
            }
        }
        
        public int Size()
        {
            return size;
        }
        
        public T[] ToArray()
        {
            var newArray = new T[size];
            for (var i = 0; i < size; i++)
            {
                newArray[i] = elements[(head + i) % elements.Length];
            }
            return newArray;
        }
        
        public T[] ToArray(T[] a)
        {
            if (a == null || a.Length < size)
            {
                return ToArray();
            }
            for (var i = 0; i < size; i++)
            {
                a[i] = elements[(head + i) % elements.Length];
            }
            if (a.Length > size)
            {
                a[size] = default;
            }
            return a;
        }
        
        public T Element()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Deque is empty.");
            return elements[head];
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
        
        public T Peek()
        {
            return IsEmpty() ? default(T) : elements[head];
        }
        
        public T Poll()
        {
            if (IsEmpty())
                return default(T);
            var item = elements[head];
            head = (head + 1) % elements.Length;
            size--;
            return item;
        }
        
        public void AddFirst(T obj)
        {
            EnsureCapacity(size + 1);
            head = (head - 1 + elements.Length) % elements.Length;
            elements[head] = obj;
            size++;
        }
        
        public void AddLast(T obj)
        {
            Add(obj);
        }
        
        public T GetFirst()
        {
            return Element();
        }
        
        public T GetLast()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Deque is empty.");
            return elements[(tail - 1 + elements.Length) % elements.Length];
        }
        
        public bool OfferFirst(T obj)
        {
            if (size >= elements.Length) return false;
            AddFirst(obj);
            return true;
        }
        
        public bool OfferLast(T obj)
        {
            return Offer(obj);
        }
        
        public T Pop()
        {
            return Poll();
        }
        
        public void Push(T obj)
        {
            EnsureCapacity(size + 1); 
            head = (head - 1 + elements.Length) % elements.Length; 
            elements[head] = obj; 
            size++; 
        }


        public T PeekFirst()
        {
            return Peek();
        }
        
        public T PeekLast()
        {
            return IsEmpty() ? default(T) : elements[(tail - 1 + elements.Length) % elements.Length];
        }
        
        public T PollFirst()
        {
            return Poll();
        }
        
        public T PollLast()
        {
            if (IsEmpty())
                return default(T);

            tail = (tail - 1 + elements.Length) % elements.Length;
            var item = elements[tail];
            elements[tail] = default(T); 
            size--;
            return item;
        }
        
        public T RemoveLast()
        {
            return PollLast();
        }
        
        public T RemoveFirst()
        {
            return Poll();
        }
        
        public bool RemoveLastOccurrence(object obj)
        {
            for (var i = size - 1; i >= 0; i--)
            {
                var index = (head + i) % elements.Length;
                if (elements[index]?.Equals(obj) != true) continue;
                RemoveAt(index);
                return true;
            }
            return false;
        }
        
        public bool RemoveFirstOccurrence(object obj)
        {
            for (var i = 0; i < size; i++)
            {
                var index = (head + i) % elements.Length;
                if (elements[index]?.Equals(obj) != true) continue;
                RemoveAt(index);
                return true;
            }
            return false;
        }

        private void EnsureCapacity(int minCapacity)
        {
            if (minCapacity <= elements.Length) return;
            var newCapacity = elements.Length * 2;
            var newElements = new T[newCapacity];
            for (var i = 0; i < size; i++)
            {
                newElements[i] = elements[(head + i) % elements.Length];
            }
            elements = newElements;
            head = 0;
            tail = size;
        }

        private void RemoveAt(int index)
        {
            var actualIndex = (head + index) % elements.Length;
            for (var i = index; i < size - 1; i++)
            {
                var nextIndex = (head + i + 1) % elements.Length;
                elements[actualIndex] = elements[nextIndex];
                actualIndex = nextIndex;
            }
            elements[(head + size - 1) % elements.Length] = default(T);
            size--;
            tail = (tail - 1 + elements.Length) % elements.Length;
        }
    }
}
