using System;
using System.Collections.Generic;

namespace ca4
{
    public class MyStack<T> : MyVector<T>
    {
        public void Push(T item)
        {
            Add(item);
        }

        public T Pop()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Stack is Empty, cannot pop.");
            }
            return base.Remove(Size() - 1);
        }

        public T Peek()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Stack is Empty, cannot peek.");
            }

            return Get(Size() - 1);
        }

        public bool Empty()
        {
            return Size() == 0;
        }

        public int Search(T item)
        {
            for (int i = 0; i < Size(); i++)
            {
                if (EqualityComparer<T>.Default.Equals(Get(Size() - i - 1), item))
                {
                    return i + 1; 
                }
            }

            return -1; 
        }
    }
}
