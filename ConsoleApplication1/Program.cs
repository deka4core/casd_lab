using System;

namespace ConsoleApplication1
{
    internal abstract class Program
    {
        public static void Main(string[] args)
        {
            // Тестирование
            var list = new MyArrayList<int>();
            for (var i = 1; i <= 10; i++)
            {
                list.Add(i);
            }
            
            Console.WriteLine(list.Get(4)); 
            // Console.WriteLine(list.Get(150));
            list.Remove(4);
            Console.WriteLine(list.Size());
            Console.WriteLine(list.IsEmpty());
            list.RemoveAll(new int[] { 2, 3 });
            var sublist = list.SubList(1, 5);
            list.RetainAll(new int[] { 1 });
            Console.WriteLine(list.Size());
            Console.WriteLine(list.Get(0));
            return;
        }
    }
}