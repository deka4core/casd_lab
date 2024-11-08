using System;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp1
{
    public abstract class Arrays
    {
        public static int[] RandNum(ref int size, int modulus = 1000)
        {
            var array = new int[size];
            var rand = new Random();

            for (var i = 0; i < size; i++)
                array[i] = rand.Next(0, modulus);

            return array;
        }
        
        [SuppressMessage("ReSharper.DPA", "DPA0000: DPA issues")]
        public static int[] SortArrays(ref int size, int a = 1000)
        {
            var rand = new Random();
            var arrays = new int[size];

            var i = 0;
            // ReSharper disable once UselessBinaryOperation
            var restSize = size - i;
            while (i < size)
            {
                var subArraySize = rand.Next(0, restSize);
                var array = RandNum(ref subArraySize);
                Array.Sort(array);
                foreach (var t in array)
                {
                    arrays[i] = t;
                    i++;
                }
                restSize -= subArraySize;
            }
            return arrays;
        }
        
        public static int[] PermutationArray(ref int size, int modulus = 1000)
        {
            var rand = new Random();
            var array = RandNum(ref size);
            Array.Sort(array);

            var permutationCount = rand.Next(1, array.Length / 2);
            for (var i = 0; i < permutationCount; i++)
            {
                var a = rand.Next(0, size - 1);
                var b = rand.Next(0, size - 1);

                (array[a], array[b]) = (array[b], array[a]);
            }
            return array;
        }

        // Сортированный в прямом порядке
        public static int[] ForwardSortArray(ref int size, int modulus = 1000)
        {
            var rand = new Random();
            var array = RandNum(ref size);
            Array.Sort(array);
            return array;
        }

        // В обратном
        public static int[] ReverseSortArray(ref int size, int modulus = 1000)
        {
            var array = ForwardSortArray(ref size);
            Array.Reverse(array);
            return array;
        }

        // Случайно переставленные
        public static int[] ReplaceItemsArray(ref int size, int modulus = 1000)
        {
            var rand = new Random();
            var array = RandNum(ref size);
            Array.Sort(array);

            var replaceCount = rand.Next(1, array.Length / 2);
            for (var i = 0; i < replaceCount; i++)
            {
                var a = rand.Next(0, size - 1);
                var b = rand.Next(0, modulus);

                array[a] = b;
            }
            return array;
        }

        // Случайное кол-во повторений одного элемента
        public static int[] RepeatItemArray(ref int size, int modulus = 1000)
        {
            var rand = new Random();
            var array = new int[size];
            var element = rand.Next(0, modulus);
            var frequency = rand.Next(0, 100);
            var count = frequency / 100 * size;
            for (var i = 0; i < count; i++)
            {
                array[i] = element;
            }
            var restSize = size - count;
            var array2 = RandNum(ref restSize);

            var j = 0;
            for (var i = count; i < size; i++)
            {
                array[i] = array2[j];
                j++;
            }
            return array;
        }
    }
}

