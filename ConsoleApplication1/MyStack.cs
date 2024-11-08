using System;

namespace ConsoleApplication1
{

    public class MyStack<T> : MyVector<T>
    {
        public void Push(T item)
        {
            base.Add(item);
        }

        public T Pop()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Стэк пуст");
            }
            return base.Remove(Size() - 1);
        }

        public T Peek()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Стек пуст");
            }

            return Get(Size() - 1);
        }

        public bool Empty()
        {
            return Size() == 0;
        }

        public int Search(T item)
        {
            for (int i = Size() - 1; i >= 0; i--)
            {
                if (Get(i).Equals(item))
                {
                    return Size() - i;
                }
            }
            return -1;
        }
    }
}