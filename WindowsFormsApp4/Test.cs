using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Test<T>
    {
        private static T[][] array1 = null;
        private static T[][] array2 = null;
        private static T[][] array3 = null;
        private static T[][] array4 = null;

        public delegate void GroupDelegate(T[] array, Comparison<T> comparer);

        List<GroupDelegate> firstGroup = new List<GroupDelegate>
        {
            SortingAlgorithms<T>.BubbleSort,      
            SortingAlgorithms<T>.InsertionSort,   
            SortingAlgorithms<T>.SelectionSort,   
            SortingAlgorithms<T>.ShakerSort,      
            SortingAlgorithms<T>.GnomeSort,       
        };

        List<GroupDelegate> secondGroup = new List<GroupDelegate>
        {
            SortingAlgorithms < T >.BitonicSort,     
            SortingAlgorithms < T >.ShellSort,       
            SortingAlgorithms < T >.TreeSort,        
        };
        List<GroupDelegate> thirdGroup = new List<GroupDelegate>
        {
            SortingAlgorithms < T >.CombSort,        
            SortingAlgorithms < T >.HeapSort,        
            SortingAlgorithms < T >.QuickSort,       
            SortingAlgorithms < T >.MergeSort,       
            SortingAlgorithms < T >.CountingSort,    
            SortingAlgorithms < T >.BucketSort
        };

        private delegate T[] TestDataDelegate(int size, T min, T max, Comparison<T> comparer);

        List<TestDataDelegate> firstTestData = new List<TestDataDelegate>
        {
            Arrays<T>.RandNum,
        };

        List<TestDataDelegate> secondTestData = new List<TestDataDelegate>
        {
            Arrays<T>.SortArrays,
        };

        List<TestDataDelegate> thirdTestData = new List<TestDataDelegate>
        {
            Arrays<T>.PermutationArray
        };
        List<TestDataDelegate> fourthTestData = new List<TestDataDelegate>
        {
            Arrays<T>.ForwardSortArray,
        };
        List<TestDataDelegate> fifthTestData = new List<TestDataDelegate> 
        {
            Arrays<T>.ReverseSortArray
        };
        List<TestDataDelegate> sixthTestData = new List<TestDataDelegate> 
        {
            Arrays<T>.ReplaceElementsArray
        };
        List<TestDataDelegate> seventhTestData = new List<TestDataDelegate>
        {
            Arrays<T>.RepeatElArray
        };


        List<GroupDelegate> groupDelegate = null;
        List<TestDataDelegate> testDataDelegate = null;
        int groupNumber = 0;
        int testNumber = 0;
        int divisor = 1;
        int size = 10000;

        public void InitialTest(int groupNumber_, int testNumber_, T min, T max, Comparison< T> comparer)
        {
            groupNumber = groupNumber_ + 1;
            testNumber = testNumber_ + 1;
            
            switch (groupNumber)
            {
                case 1:
                    groupDelegate = new List<GroupDelegate>(firstGroup);                   
                    size = 10000;
                    break;
                case 2:
                    groupDelegate = new List<GroupDelegate>(secondGroup);
                    size = 100000;
                    break;
                case 3:
                    groupDelegate = new List<GroupDelegate>(thirdGroup);
                    size = 1000000;
                    break;
                default:
                    groupDelegate = new List<GroupDelegate>(firstGroup);
                    size = 10000;
                    break;
            }
            switch (testNumber)
            {
                case 1:
                    testDataDelegate = new List<TestDataDelegate>(firstTestData);
                    break;
                case 2:
                    testDataDelegate = new List<TestDataDelegate>(secondTestData);
                    break;
                case 3:
                    testDataDelegate = new List<TestDataDelegate>(thirdTestData);
                    break;
                case 4:
                    testDataDelegate = new List<TestDataDelegate>(fourthTestData);
                    break;
                case 5:
                    testDataDelegate = new List<TestDataDelegate>(fifthTestData);
                    break;
                case 6:
                    testDataDelegate = new List<TestDataDelegate>(sixthTestData);
                    break;
                case 7:
                    testDataDelegate = new List<TestDataDelegate>(seventhTestData);
                    break;
                default:
                    testDataDelegate = new List<TestDataDelegate>(firstTestData);
                    divisor = 4;
                    break;
            }
            
            GenerateArrays(min, max, comparer);
        }

        private void GenerateArrays(T min, T max, Comparison<T> comparer)
        {
            switch (testDataDelegate.Count)
            {
                case 1:
                {
                    array1 = new T[(int)Math.Log(size, 10)][];
                    var c = 0;
                    for (var i = 10; i < size + 1; i *= 10)
                    {
                        array1[c] = new T[i];
                        array1[c] = testDataDelegate[0](i, min, max, comparer);
                        c++;
                    }

                    break;
                }
                case 4:
                {
                    array1 = new T[(int)Math.Log(size, 10)][];
                    array2 = new T[(int)Math.Log(size, 10)][];
                    array3 = new T[(int)Math.Log(size, 10)][];
                    array4 = new T[(int)Math.Log(size, 10)][];
                    var c = 0;

                    for (var i = 10; i < size + 1; i *= 10)
                    {
                        array1[c] = new T[i];
                        array2[c] = new T[i];
                        array3[c] = new T[i];
                        array4[c] = new T[i];

                        array1[c] = testDataDelegate[0](i, min, max, comparer);
                        array2[c] = testDataDelegate[1](i, min, max, comparer);
                        array3[c] = testDataDelegate[2](i, min, max, comparer);
                        array4[c] = testDataDelegate[3](i, min, max, comparer);

                        c++;
                    }

                    break;
                }
            }
        }
        
        public void StartTest(Comparison<T> comparer)
        {
            var x = new double[array1.Length];
            var y = new long[groupDelegate.Count][];

            if (groupDelegate == null) return;
            switch (testDataDelegate.Count)
            {
                case 1:
                {
                    for (var sortInd = 0; sortInd != groupDelegate.Count; sortInd++)
                    {
                        y[sortInd] = new long[array1.Length];
                        for (var i = 0; i < array1.Length; i++)
                        {
                            {
                                x[i] = array1[i].Length;
                                long time = 0;
                                var ind = sortInd;
                                var i1 = i;
                                Parallel.For(0, 20, j =>
                                {
                                    var stopwatch = new Stopwatch();
                                    stopwatch.Start();

                                    groupDelegate[ind](array1[i1], comparer);

                                    stopwatch.Stop();
                                    time += stopwatch.ElapsedMilliseconds;
                                });
                                time /= 20;
                                y[sortInd][i] = time;
                            }
                        }
                    }
                    var graph = new ConsoleApp1.Graph(groupNumber, testNumber, size, x, y);
                    graph.ShowDialog();
                    break;
                }
                case 4:
                    break;
            }
        }
    }
}
    
