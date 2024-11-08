using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Test
    {
        private static int[][] _array1 = null;
        private static int[][] _array2 = null;
        private static int[][] _array3 = null;
        private static int[][] _array4 = null;
        
        private delegate void GroupDelegate(int[] array);

        private readonly List<GroupDelegate> _firstGroup = new List<GroupDelegate>
        {
            SortingAlgorithms.BubbleSort,
            SortingAlgorithms.InsertionSort,
            SortingAlgorithms.SelectionSort,
            SortingAlgorithms.ShakerSort,
            SortingAlgorithms.GnomeSort
        };

        private readonly List<GroupDelegate> _secondGroup = new List<GroupDelegate>
        {
            SortingAlgorithms.BitonicSort,
            SortingAlgorithms.ShellSort,
            SortingAlgorithms.TreeSort
        };

        private readonly List<GroupDelegate> _thirdGroup = new List<GroupDelegate>
        {
            SortingAlgorithms.CombSort,
            SortingAlgorithms.HeapSort,
            SortingAlgorithms.QuickSort,
            SortingAlgorithms.MergeSort,
            SortingAlgorithms.CountingSort,
            SortingAlgorithms.BucketSort,
            SortingAlgorithms.RadiaxSort
        };
        
        private delegate int[] TestDataDelegate(ref int size, int modulus = 1000);

        private readonly List<TestDataDelegate> _testData1 = new List<TestDataDelegate>
        {
            Arrays.RandNum,
        };

        private readonly List<TestDataDelegate> _testData2 = new List<TestDataDelegate>
        {
            Arrays.SortArrays,
        };

        private readonly List<TestDataDelegate> _testData3 = new List<TestDataDelegate>
        {
            Arrays.PermutationArray
        };

        private readonly List<TestDataDelegate> _testData4 = new List<TestDataDelegate>
        {
            Arrays.ForwardSortArray,
        };

        private readonly List<TestDataDelegate> _testData5 = new List<TestDataDelegate> 
        {
            Arrays.ReverseSortArray
        };

        private readonly List<TestDataDelegate> _testData6 = new List<TestDataDelegate> 
        {
            Arrays.ReplaceItemsArray
        };

        private readonly List<TestDataDelegate> _testData7 = new List<TestDataDelegate>
        {
            Arrays.RepeatItemArray
        };


        private List<GroupDelegate> _groupDelegate = null;
        private List<TestDataDelegate> _testDataDelegate = null;
        private int _groupNumber = 0;
        private int _testNumber = 0;
        private int divisor = 1;
        private int size = 10000;

        [SuppressMessage("ReSharper.DPA", "DPA0000: DPA issues")]
        public void InitialTest(int groupNumber, int testNumber)
        {
            _groupNumber = groupNumber + 1;
            _testNumber = testNumber + 1;
            
            switch (_groupNumber)
            {
                case 1:
                    _groupDelegate = new List<GroupDelegate>(_firstGroup);                   
                    size = 10000;
                    break;
                case 2:
                    _groupDelegate = new List<GroupDelegate>(_secondGroup);
                    size = 100000;
                    break;
                case 3:
                    _groupDelegate = new List<GroupDelegate>(_thirdGroup);
                    size = 1000000;
                    break;
                default:
                    _groupDelegate = new List<GroupDelegate>(_firstGroup);
                    size = 10000;
                    break;
            }
            switch (_testNumber)
            {
                case 1:
                    _testDataDelegate = new List<TestDataDelegate>(_testData1);
                    break;
                case 2:
                    _testDataDelegate = new List<TestDataDelegate>(_testData2);
                    break;
                case 3:
                    _testDataDelegate = new List<TestDataDelegate>(_testData3);
                    break;
                case 4:
                    _testDataDelegate = new List<TestDataDelegate>(_testData4);
                    break;
                case 5:
                    _testDataDelegate = new List<TestDataDelegate>(_testData5);
                    break;
                case 6:
                    _testDataDelegate = new List<TestDataDelegate>(_testData6);
                    break;
                case 7:
                    _testDataDelegate = new List<TestDataDelegate>(_testData7);
                    break;
                default:
                    _testDataDelegate = new List<TestDataDelegate>(_testData1);
                    divisor = 4;
                    break;
            }
            
            GenerateArrays();
        }
        
        private void GenerateArrays()
        {
            switch (_testDataDelegate.Count)
            {
                case 1:
                {
                    _array1 = new int[(int)Math.Log(size, 10)][];
                    var c = 0;
                    for (var i = 10; i < size + 1; i *= 10)
                    {
                        _array1[c] = new int[i];
                        _array1[c] = _testDataDelegate[0](ref i);
                        c++;
                    }

                    break;
                }
                case 4:
                {
                    _array1 = new int[(int)Math.Log(size, 10)][];
                    _array2 = new int[(int)Math.Log(size, 10)][];
                    _array3 = new int[(int)Math.Log(size, 10)][];
                    _array4 = new int[(int)Math.Log(size, 10)][];
                    var c = 0;
                    for (var i = 10; i < size + 1; i *= 10)
                    {
                        _array1[c] = new int[i];
                        _array2[c] = new int[i];
                        _array3[c] = new int[i];
                        _array4[c] = new int[i];

                        _array1[c] = _testDataDelegate[0](ref i);
                        _array2[c] = _testDataDelegate[1](ref i);
                        _array3[c] = _testDataDelegate[2](ref i);
                        _array4[c] = _testDataDelegate[3](ref i);

                        c++;
                    }

                    break;
                }
            }
        }
        public void StartTest()
        {
            var x = new double[_array1.Length];
            var y = new long[_groupDelegate.Count][];

            if (_groupDelegate == null) return;
            switch (_testDataDelegate.Count)
            {
                case 1:
                {
                    for (var sortInd = 0; sortInd != _groupDelegate.Count; sortInd++)
                    {
                        y[sortInd] = new long[_array1.Length];
                        for (var i = 0; i < _array1.Length; i++)
                        {
                            {
                                x[i] = _array1[i].Length;
                                long time = 0;
                                var ind = sortInd;
                                var i1 = i;
                                Parallel.For(0, 20, j =>
                                {
                                    var stopwatch = new Stopwatch();
                                    stopwatch.Start();

                                    _groupDelegate[ind](_array1[i1]);

                                    stopwatch.Stop();
                                    time += stopwatch.ElapsedMilliseconds;
                                });
                                time /= 20;
                                y[sortInd][i] = time;
                            }
                        }
                    }
                    var graph = new ConsoleApp1.Graph(_groupNumber, _testNumber, size, x, y);
                    graph.ShowDialog();
                    break;
                }
                case 4:
                    break;
            }
        }
    }
}
    