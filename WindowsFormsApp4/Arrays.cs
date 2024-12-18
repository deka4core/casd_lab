using System;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp1
{
    public abstract class Arrays<T>
    {
        private static T GenerateRandom(T min, T max, Comparison<T> comparer)
        {
            var rand = new Random();
            T n;
            do
            {
                n = (T)(object)rand.Next();
            }
            while (comparer(n, max) > 0 || comparer(n, min) < 0);
            return n;
        }

        public static T[] RandNum(int size, T min, T max, Comparison<T> comparer)
        {
            var array = new T[size];
            var rand = new Random();

            for (var i = 0; i < size; i++)
                array[i] = GenerateRandom(min, max, comparer);

            return array;
        }
        
        public static T[] SortArrays(int size, T min, T max, Comparison<T> comparer)
        {
            var rand = new Random();
            var arrays = new T[size];

            var i = 0;
            var restSize = size;
            while (i < size)
            {
                var subArraySize = rand.Next(0, restSize);
                var array = RandNum(subArraySize, min, max, comparer);
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

        public static T[] PermutationArray(int size, T min, T max, Comparison<T> comparer)
        {
            var rand = new Random();
            var array = RandNum(size, min, max, comparer);
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

        public static T[] ForwardSortArray(int size, T min, T max, Comparison<T> comparer)
        {
            var rand = new Random();
            var array = RandNum(size, min, max, comparer);
            Array.Sort(array);
            return array;
        }

        public static T[] ReverseSortArray(int size, T min, T max, Comparison<T> comparer)
        { 
            var array = ForwardSortArray(size, min, max, comparer);
            Array.Reverse(array);
            return array;
        }

        public static T[] ReplaceElementsArray(int size, T min, T max, Comparison<T> comparer)
        { 
            var rand = new Random();
            var array = RandNum(size, min, max, comparer);
            Array.Sort(array);

            var replaceCount = rand.Next(1, array.Length / 2);
            for (var i = 0; i < replaceCount; i++)
            {
                var a = rand.Next(0, size - 1);

                var b = rand.Next(0, size - 1);

                (array[a], array[b]) = (array[b], array[a]);
            }
            return array;
        }

        public static T[] RepeatElArray(int size, T min, T max, Comparison<T> comparer)
        {
            var rand = new Random();
            var array = new T[size];
            var element = GenerateRandom(min, max, comparer);
            var frequency = rand.Next(0, 100);
            var count = frequency / 100 * size;
            for (var i = 0; i < count; i++)
            {
                array[i] = element;
            }
            var restSize = size - count;
            var array2 = RandNum(restSize, min, max, comparer);

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

