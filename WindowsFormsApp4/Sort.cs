using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public abstract class SortingAlgorithms
    {

        public static void BubbleSort(int[] array)
        {
            for (var i = 0; i < array.Length - 1; i++)
            {
                for (var j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j] <= array[j + 1]) continue;
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                }
            }
        }

        public static void ShakerSort(int[] array)
        {
            var swapped = true;
            var start = 0;
            var end = array.Length;

            while (swapped == true)
            {
                swapped = false;
                for (var i = start; i < end - 1; ++i)
                {
                    if (array[i] <= array[i + 1]) continue;
                    (array[i], array[i + 1]) = (array[i + 1], array[i]);
                    swapped = true;
                }
                if (swapped == false)
                    break;
                swapped = false;
                end = end - 1;
                for (var i = end - 1; i >= start; i--)
                {
                    if (array[i] <= array[i + 1]) continue;
                    (array[i], array[i + 1]) = (array[i + 1], array[i]);
                    swapped = true;
                }
                start = start + 1;
            }
        }

        public static void CombSort(int[] array)
        {
            var length = array.Length;
            var gap = length;
            var swapped = true;

            while (gap != 1 || swapped == true)
            {
                gap = GetNextGap(gap);
                swapped = false;
                for (var i = 0; i < length - gap; i++)
                {
                    if (array[i] <= array[i + gap]) continue;
                    (array[i], array[i + gap]) = (array[i + gap], array[i]);

                    swapped = true;
                }
            }
        }

        private static int GetNextGap(int gap)
        {
            gap = (gap * 10) / 13;
            return gap < 1 ? 1 : gap;
        }

        public static void InsertionSort(int[] array)
        {
            var n = array.Length;
            for (var i = 1; i < n; ++i)
            {
                var key = array[i];
                var j = i - 1;
                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j = j - 1;
                }
                array[j + 1] = key;
            }
        }

        public static void ShellSort(int[] array)
        {
            var inc = 3;
            while (inc > 0)
            {
                int i;
                for (i = 0; i < array.Length; i++)
                {
                    var j = i;
                    var temp = array[i];
                    while ((j >= inc) && (array[j - inc] > temp))
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

        private class Node
        {
            public readonly int Data;
            public Node Left;
            public Node Right;

            public Node(int data)
            {
                Data = data;
                Left = null;
                Right = null;
            }
        }

        private class BinarySearchTree
        {
            public Node Root;

            public BinarySearchTree()
            {
                Root = null;
            }

            public void Insert(int data)
            {
                Root = InsertRec(Root, data);
            }

            private static Node InsertRec(Node root, int data)
            {
                if (root == null)
                {
                    root = new Node(data);
                    return root;
                }

                if (data < root.Data)
                    root.Left = InsertRec(root.Left, data);
                else
                    root.Right = InsertRec(root.Right, data);

                return root;
            }

            public static void InOrderTraversal(Node root, ICollection<int> result)
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

        public static void TreeSort(int[] array)
        {
            var bst = new BinarySearchTree();
            foreach (var value in array)
            {
                bst.Insert(value);
            }
            var sortedList = new List<int>();
            BinarySearchTree.InOrderTraversal(bst.Root, sortedList);
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = sortedList[i];
            }
        }

        public static void GnomeSort(int[] array)
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
                else if (array[index] >= array[index - 1])
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

        public static void SelectionSort(int[] array)
        {
            var n = array.Length;
            for (var i = 0; i < n - 1; i++)
            {
                var minIdx = i;
                for (var j = i + 1; j < n; j++)
                    if (array[j] < array[minIdx])
                        minIdx = j;
                (array[minIdx], array[i]) = (array[i], array[minIdx]);
            }
        }

        public static void HeapSort(int[] array)
        {
            var n = array.Length;
            for (var i = n / 2 - 1; i >= 0; i--)
                HeapMove(array, n, i);
            for (var i = n - 1; i > 0; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                HeapMove(array, i, 0);
            }
        }

        private static void HeapMove(int[] array, int n, int i)
        {
            while (true)
            {
                if (array == null) throw new ArgumentNullException(nameof(array));
                var largest = i;
                var l = 2 * i + 1;
                var r = 2 * i + 2;
                if (l < n && array[l] > array[largest]) largest = l;
                if (r < n && array[r] > array[largest]) largest = r;
                if (largest == i) return;
                (array[i], array[largest]) = (array[largest], array[i]);
                i = largest;
            }
        }

        private static void QuickSort(int[] array, int left, int right)
        {
            while (true)
            {
                if (left >= right) return;
                var pivotIndex = Partition(array, left, right);
                QuickSort(array, left, pivotIndex - 1);
                left = pivotIndex + 1;
            }
        }

        public static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }

        private static int Partition(IList<int> array, int left, int right)
        {
            var pivot = array[right];
            var i = left - 1;

            for (var j = left; j < right; j++)
            {
                if (array[j] > pivot) continue;
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }

            (array[i + 1], array[right]) = (array[right], array[i + 1]);

            return i + 1;
        }

        private static void Merge(IList<int> array, int l, int m, int r)
        {
            var n1 = m - l + 1;
            var n2 = r - m;
            var left = new int[n1];
            var right = new int[n2];
            int i, j;
            for (i = 0; i < n1; ++i)
                left[i] = array[l + i];
            for (j = 0; j < n2; ++j)
                right[j] = array[m + 1 + j];
            i = 0;
            j = 0;
            var k = l;
            while (i < n1 && j < n2)
            {
                if (left[i] <= right[j])
                {
                    array[k] = left[i];
                    i++;
                }
                else
                {
                    array[k] = right[j];
                    j++;
                }
                k++;
            }
            while (i < n1)
            {
                array[k] = left[i];
                i++;
                k++;
            }
            while (j < n2)
            {
                array[k] = right[j];
                j++;
                k++;
            }
        }

        private static void MergeSort(IList<int> array, int l, int r)
        {
            if (l >= r) return;
            var m = l + (r - l) / 2;
            MergeSort(array, l, m);
            MergeSort(array, m + 1, r);
            Merge(array, l, m, r);
        }

        public static void MergeSort(int[] array)
        {
            MergeSort(array, 0, array.Length - 1);
        }

        public static void CountingSort(int[] array)
        {
            if (array.Length == 0) return;

            int FindMaxValue(IReadOnlyList<int> arr)
            {
                if (arr.Count == 0)
                {
                    throw new ArgumentException("Array is empty.");
                }

                var maxValue = arr[0];
                for (var i = 1; i < arr.Count; i++)
                {
                    if (arr[i] > maxValue)
                    {
                        maxValue = arr[i];
                    }
                }

                return maxValue;
            }

            int k;
            try
            {
                k = FindMaxValue(array);
            }
            catch
            {
                return;
            }

            var count = new int[k + 1];
            foreach (var t in array)
            {
                count[t]++;
            }

            var index = 0;
            for (var i = 0; i < count.Length; i++)
            {
                for (var j = 0; j < count[i]; j++)
                {
                    array[index] = i;
                    index++;
                }
            }
        }

        private static void BucketSort(IList<int> array, int bucketCount)
        {
            if (array.Count <= 1)
            {
                return;
            }

            var buckets = new List<int>[bucketCount];
            for (var i = 0; i < bucketCount; i++)
                buckets[i] = new List<int>();

            var min = double.MaxValue;
            var max = -double.MaxValue;

            foreach (var t in array)
            {
                min = Math.Min(min, t);
                max = Math.Max(max, t);
            }

            foreach (var t in array)
            {
                var idx = max == min ? 0 : Math.Min(bucketCount - 1, (int)(bucketCount * (t - min) / (max - min)));
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

        public static void BucketSort(int[] array)
        {
            BucketSort(array, array.Length + 1);
        }
        public static void RadiaxSort(int[] arr)
        {
            var tmp = new int[arr.Length];
            for (var shift = 31; shift > -1; --shift)
            {
                var j = 0;
                int i;
                for (i = 0; i < arr.Length; ++i)
                {
                    var move = (arr[i] << shift) >= 0;
                    if (shift == 0 ? !move : move)
                        arr[i - j] = arr[i];
                    else
                        tmp[j++] = arr[i];
                }
                Array.Copy(tmp, 0, arr, arr.Length - j, j);
            }
        }

        private static void BitSeqSort(IList<int> arr, int left, int right, bool inv)
        {
            while (true)
            {
                if (right - left <= 1) return;
                var mid = left + (right - left) / 2;

                for (int i = left, j = mid; i < mid && j < right; i++, j++)
                {
                    if (inv ^ (arr[i] > arr[j]))
                    {
                        (arr[i], arr[j]) = (arr[j], arr[i]);
                    }
                }

                BitSeqSort(arr, left, mid, inv);
                left = mid;
            }
        }

        private static void MakeBitonic(int[] arr, int left, int right)
        {
            if (right - left <= 1) return;
            var mid = left + (right - left) / 2;

            MakeBitonic(arr, left, mid);
            BitSeqSort(arr, left, mid, false);
            MakeBitonic(arr, mid, right);
            BitSeqSort(arr, mid, right, true);
        }

        public static void BitonicSort(int[] arr)
        {
            if (arr.Length == 0) return;
            var n = 1;
            var inf = arr.Max() + 1;
            var length = arr.Length;

            while (n < length) n *= 2;

            var temp = new int[n];
            Array.Copy(arr, temp, length);

            for (var i = length; i < n; i++)
            {
                temp[i] = inf;
            }

            MakeBitonic(temp, 0, n);
            BitSeqSort(temp, 0, n, false);

            Array.Copy(temp, arr, length);
        }
    }
}