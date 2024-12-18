using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class SortingAlgorithms<T>
    {

        public static void BubbleSort(T[] array, Comparison<T> comparer)
        {
            T temp;
            for (var i = 0; i < array.Length - 1; i++)
            {
                for (var j = 0; j < array.Length - i - 1; j++)
                {
                    if (comparer(array[i], array[i - 1]) <= 0) continue;
                    temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }

        public static void ShakerSort(T[] array, Comparison<T> comparer)
        {
            var swapped = true;
            var start = 0;
            var end = array.Length;

            while (swapped == true)
            {
                swapped = false;
                for (var i = start; i < end - 1; ++i)
                {
                    if (comparer(array[i], array[i + 1]) <= 0) continue;
                    (array[i], array[i + 1]) = (array[i + 1], array[i]);
                    swapped = true;
                }
                if (swapped == false)
                    break;
                swapped = false;
                end = end - 1;
                for (var i = end - 1; i >= start; i--)
                {
                    if (comparer(array[i], array[i + 1]) <= 0) continue;
                    (array[i], array[i + 1]) = (array[i + 1], array[i]);
                    swapped = true;
                }
                start = start + 1;
            }
        }

        public static void CombSort(T[] array, Comparison<T> comparer)
        {
            var length = array.Length;
            var gap = length;
            var swapped = true;

            while (gap != 1 || swapped == true)
            {
                gap = GetNextGap(gap);
                swapped = false;
                for (int i = 0; i < length - gap; i++)
                {
                    if (comparer(array[i], array[i + gap]) > 0)
                    {
                        (array[i], array[i + gap]) = (array[i + gap], array[i]);

                        swapped = true;
                    }
                }
            }
        }

        static int GetNextGap(int gap)
        {
            gap = (gap * 10) / 13;
            return gap < 1 ? 1 : gap;
        }

        public static void InsertionSort(T[] array, Comparison<T> comparer)
        {
            var n = array.Length;
            for (var i = 1; i < n; ++i)
            {
                var key = array[i];
                var j = i - 1;
                while (j >= 0 && comparer(array[j], key) > 0)
                {
                    array[j + 1] = array[j];
                    j = j - 1;
                }
                array[j + 1] = key;
            }
        }

        public static void ShellSort(T[] array, Comparison<T> comparer)
        {
            int i, j, inc;
            T temp;
            inc = 3;
            while (inc > 0)
            {
                for (i = 0; i < array.Length; i++)
                {
                    j = i;
                    temp = array[i];
                    while ((j >= inc) && comparer(array[j - inc], temp) > 0)
                    {
                        array[j] = array[j - inc];
                        j = j - inc;
                    }
                    array[j] = temp;
                }
                if (inc / 2 != 0)
                    inc = inc / 2;
                else if (inc == 1)
                    inc = 0;
                else
                    inc = 1;
            }
        }

        class Node<T>
        {
            public T Data;
            public Node<T> Left;
            public Node<T> Right;

            public Node(T data)
            {
                Data = data;
                Left = null;
                Right = null;
            }
        }

        class BinarySearchTree
        {
            public Node<T> Root;

            public BinarySearchTree()
            {
                Root = null;
            }

            public void Insert(T data, Comparison<T> comparer)
            {
                Root = InsertRec(Root, data, comparer);
            }

            private static Node<T> InsertRec(Node<T> root, T data, Comparison<T> comparer)
            {
                if (root == null)
                {
                    root = new Node<T>(data);
                    return root;
                }

                if (comparer(data,root.Data) < 0)
                    root.Left = InsertRec(root.Left, data, comparer);
                else
                    root.Right = InsertRec(root.Right, data, comparer);

                return root;
            }

            public static void InOrderTraversal(Node<T> root, ICollection<T> result)
            {
                while (true)
                {
                    if (root == null) return;
                    InOrderTraversal(root.Left, result);
                    result.Add(root.Data);
                    root = root.Right;
                }
            }
        }

        public static void TreeSort(T[] array, Comparison<T> comparer)
        {
            var bst = new BinarySearchTree();
            foreach (var value in array)
            {
                bst.Insert(value, comparer);
            }
            var sortedList = new List<T>();
            BinarySearchTree.InOrderTraversal(bst.Root, sortedList);
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = sortedList[i];
            }
        }

        public static void GnomeSort(T[] array, Comparison<T> comparer)
        {
            if (array.Length <= 1)
            {
                return;
            }

            var index = 0;

            while (index < array.Length)
            {
                if (index == 0)
                {
                    index++;
                }
                else if (comparer(array[index], array[index - 1]) >= 0)
                {
                    index++;
                }
                else
                {
                    (array[index], array[index - 1]) = (array[index - 1], array[index]);
                    index--;
                }
            }
        }

        public static void SelectionSort(T[] array, Comparison<T> comparer)
        {
            var n = array.Length;
            for (var i = 0; i < n - 1; i++)
            {
                var min_idx = i;
                for (var j = i + 1; j < n; j++)
                    if (comparer(array[j], array[min_idx]) < 0)
                        min_idx = j;
                (array[min_idx], array[i]) = (array[i], array[min_idx]);
            }
        }

        public static void HeapSort(T[] array, Comparison<T> comparer)
        {
            var n = array.Length;
            for (var i = n / 2 - 1; i >= 0; i--)
                Heapify(array, n, i, comparer);
            for (var i = n - 1; i > 0; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                Heapify(array, i, 0, comparer);
            }
        }

        static void Heapify(T[] array, int N, int i, Comparison<T> comparer)
        {
            while (true)
            {
                var largest = i;
                var l = 2 * i + 1;
                var r = 2 * i + 2;
                if (l < N && (comparer(array[l], array[largest]) > 0)) largest = l;
                if (r < N && comparer(array[r], array[largest]) > 0) largest = r;
                if (largest != i)
                {
                    (array[i], array[largest]) = (array[largest], array[i]);
                    i = largest;
                    continue;
                }

                break;
            }
        }

        public static void QuickSort(T[] array, int left, int right, Comparison<T> comparer)
        {
            while (true)
            {
                if (left < right)
                {
                    var pivotIndex = Partition(array, left, right, comparer);
                    QuickSort(array, left, pivotIndex - 1, comparer);
                    left = pivotIndex + 1;
                    continue;
                }

                break;
            }
        }

        public static void QuickSort(T[] array, Comparison<T> comparer)
        {
            QuickSort(array, 0, array.Length - 1, comparer);
        }

        static int Partition(IList<T> array, int left, int right, Comparison<T> comparer)
        {
            var pivot = array[right];
            var i = left - 1;

            for (var j = left; j < right; j++)
            {
                if (comparer(array[j], pivot) > 0) continue;
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }

            (array[i + 1], array[right]) = (array[right], array[i + 1]);

            return i + 1;
        }

        private static void Merge(IList<T> array, int l, int m, int r, Comparison<T> comparer)
        {
            var n1 = m - l + 1;
            var n2 = r - m;
            var L = new T[n1];
            var R = new T[n2];
            int i, j;
            for (i = 0; i < n1; ++i)
                L[i] = array[l + i];
            for (j = 0; j < n2; ++j)
                R[j] = array[m + 1 + j];
            i = 0;
            j = 0;
            var k = l;
            while (i < n1 && j < n2)
            {
                if (comparer(L[i], R[j]) <= 0)
                {
                    array[k] = L[i];
                    i++;
                }
                else
                {
                    array[k] = R[j];
                    j++;
                }
                k++;
            }
            while (i < n1)
            {
                array[k] = L[i];
                i++;
                k++;
            }
            while (j < n2)
            {
                array[k] = R[j];
                j++;
                k++;
            }
        }

        public static void MergeSort(T[] array, int l, int r, Comparison<T> comparer)
        {
            if (l >= r) return;
            var m = l + (r - l) / 2;
            MergeSort(array, l, m, comparer);
            MergeSort(array, m + 1, r, comparer);
            Merge(array, l, m, r, comparer);
        }

        public static void MergeSort(T[] array, Comparison<T> comparer)
        {
            MergeSort(array, 0, array.Length - 1, comparer);
        }

        public static void CountingSort(T[] array, Comparison<T> comparer)
        {
            if (array.Length == 0) return;

            T FindMaxValue(IReadOnlyList<T> arr)
            {
                if (arr.Count == 0)
                {
                    throw new ArgumentException("Array is empty.");
                }

                var maxValue = arr[0];
                for (var i = 1; i < arr.Count; i++)
                {
                    if (comparer(arr[i], maxValue) > 0)
                    {
                        maxValue = arr[i];
                    }
                }

                return maxValue;
            }

            T k;
            try
            {
                k = FindMaxValue(array);
            }
            catch
            {
                return;
            }

            var count = new int[Convert.ToInt32(k) + 1];
            foreach (var t in array)
            {
                count[Convert.ToInt32(t)]++;
            }

            var index = 0;
            foreach (var t in count)
            {
                for (var j = 0; j < t; j++)
                {
                    //array[index] = i;
                    index++;
                }
            }
        }

        public static void BucketSort(T[] array, int bucketCount, Comparison<T> comparer)
        {
            if (array.Length <= 1)
            {
                return;
            }

            var buckets = new List<T>[bucketCount];
            for (var i = 0; i < bucketCount; i++)
                buckets[i] = new List<T>();

            var min = double.MaxValue;
            var max = -double.MaxValue;

            foreach (var t in array)
            {
                min = Math.Min(min, Convert.ToSByte(t));
                max = Math.Max(max, Convert.ToSByte(t));
            }

            foreach (var t in array)
            {
                int idx;
                idx = Math.Abs(max - min) < 0 ? 0 : Math.Min(bucketCount - 1, (int)(bucketCount * (Convert.ToDouble(t) - min) / (max - min)));

                buckets[idx].Add(t);
            }

            var index = 0;
            for (var i = 0; i < bucketCount; i++)
            {
                buckets[i].Sort();

                for (var j = 0; j < buckets[i].Count; j++)
                {
                    array[index++] = buckets[i][j];
                }
            }
        }

        public static void BucketSort(T[] array, Comparison<T> comparer)
        {
            BucketSort(array, array.Length + 1, comparer);
        }
        public static void RadiaxSort(T[] arr, Comparison<T> comparer)
        {
            int i, j;
            var tmp = new T[arr.Length];
        }

        static void BitSeqSort(T[] arr, int left, int right, bool inv, Comparison<T> comparer)
        {
            while (true)
            {
                if (right - left <= 1) return;
                var mid = left + (right - left) / 2;

                for (int i = left, j = mid; i < mid && j < right; i++, j++)
                {
                    if (inv ^ (comparer(arr[i], arr[j]) > 0))
                    {
                        Swap(ref arr[i], ref arr[j]);
                    }
                }

                BitSeqSort(arr, left, mid, inv, comparer);
                left = mid;
            }
        }

        static void MakeBitonic(T[] arr, int left, int right, Comparison<T> comparer)
        {
            if (right - left <= 1) return;
            var mid = left + (right - left) / 2;

            MakeBitonic(arr, left, mid, comparer);
            BitSeqSort(arr, left, mid, false, comparer);
            MakeBitonic(arr, mid, right, comparer);
            BitSeqSort(arr, mid, right, true, comparer);
        }

        public static void BitonicSort(T[] arr, Comparison<T> comparer)
        {
            if (arr.Length == 0) return;
            var n = 1;

            var inf = Convert.ToInt32(arr.Max()) + 1;
            var length = arr.Length;

            while (n < length) n *= 2;

            var temp = new T[n];
            Array.Copy(arr, temp, length);

            MakeBitonic(temp, 0, n, comparer);
            BitSeqSort(temp, 0, n, false, comparer);

            Array.Copy(temp, arr, length);
        }

        static void Swap(ref T a, ref T b)
        {
            (a, b) = (b, a);
        }
    }
}
