using System;

namespace ConsoleApplication1
{
    internal abstract class IpAddressExtractor
    {
        private static void Main()
        {
            var stack = new MyStack<int>();
            for (int i = 0; i <= 5; i++)
            {
                stack.Add(i);
            }
            Console.WriteLine(stack.Get(3));
            Console.WriteLine(stack.Peek());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Peek());
            Console.WriteLine(stack.Search(2));
        }
    }
}
