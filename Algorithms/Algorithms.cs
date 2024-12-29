using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Algorithms
    {
        public static IEnumerable<Tuple<int[], int[], string>> SelectionSort(int[] arr)
        {
            int n = arr.Length;

            //Iterate through unsorted portion
            for (int i = 0; i < n - 1; i++)
            {
                //Find minimum value's index
                int minIndex = i;

                for (int j = i + 1; j < n; j++)
                {
                    // Yield each comparison
                    yield return Tuple.Create(
                        (int[])arr.Clone(),
                        new int[] { minIndex, j },
                        $"Comparing elements at indices {minIndex} and {j} ({arr[minIndex]} and {arr[j]})"
                    );

                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }

                //Swap minimum value with value in unsorted portion 
                if (minIndex != i)
                {
                    int temp = arr[minIndex];
                    arr[minIndex] = arr[i];
                    arr[i] = temp;

                    // Yield the current state of the array along with the swapped indices
                    yield return Tuple.Create(
                        (int[])arr.Clone(),
                        new int[] { i, minIndex },
                        $"Swapping elements at indices {i} and {minIndex} ({arr[i]} and {arr[minIndex]})"
                    );
                }
            }
        }


        public static IEnumerable<Tuple<int[], int[], string>> InsertionSort(int[] arr)
        {
            int n = arr.Length;

            for (int i = 1; i < n; i++)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= 0)
                {
                    yield return Tuple.Create(
                        (int[])arr.Clone(),
                        new int[] { j, j + 1 },
                        $"Comparing elements at indices {j} and {i} ({arr[j]} and {key})"
                    );

                    //If key is larger than the last sorted value in array
                    if (arr[j] > key)
                    {
                        //Shift all elements to the right to make space for insertion
                        arr[j + 1] = arr[j];
                        yield return Tuple.Create(
                            (int[])arr.Clone(),
                            new int[] { j, j + 1 },
                            $"Moved element {arr[j]} from index {j} to {j + 1}"
                        );
                        j--;
                    }
                    else
                    {
                        break;
                    }
                }

                //Insert key
                arr[j + 1] = key;
                if (j + 1 != i)
                {
                    yield return Tuple.Create(
                        (int[])arr.Clone(),
                        new int[] { j + 1, i },
                        $"Inserted key {key} at index {j + 1}"
                    );
                }
            }
        }

        public static IEnumerable<Tuple<int[], int[], string>> BubbleSort(int[] arr)
        {
            int n = arr.Length;

            //Iterate array
            for (int i = 0; i < n - 1; i++)
            {
                //Iterate each value being swapped in array
                for (int j = 0; j < n - i - 1; j++)
                {
                    yield return Tuple.Create(
                        (int[])arr.Clone(),
                        new int[] { j, j + 1 },
                        $"Comparing elements at indices {j} and {j + 1} ({arr[j]} and {arr[j + 1]})"
                    );

                    //Swap values if next value is larger
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;

                        yield return Tuple.Create(
                            (int[])arr.Clone(),
                            new int[] { j, j + 1 },
                            $"Swapping elements at indices {j} and {j + 1} ({arr[j]} and {arr[j + 1]})"
                        );
                    }
                }
            }
        }

        public static IEnumerable<Tuple<int[], int[], string>> MergeSort(int[] arr)
        {
            //Helper function for MergeSort
            IEnumerable<Tuple<int[], int[], string>> Merge(int[] array, int left, int mid, int right)
            {
                int n1 = mid - left + 1;
                int n2 = right - mid;

                int[] leftArr = new int[n1];
                int[] rightArr = new int[n2];

                for (int i = 0; i < n1; i++)
                    leftArr[i] = array[left + i];
                for (int j = 0; j < n2; j++)
                    rightArr[j] = array[mid + 1 + j];

                int iIndex = 0, jIndex = 0;
                int kIndex = left;

                //Merge arrays together
                while (iIndex < n1 && jIndex < n2)
                {
                    string description = $"Comparing indexes {left + iIndex} ({leftArr[iIndex]}) and {mid + 1 + jIndex} ({rightArr[jIndex]}). " +
                                         $"Left subarray: [{string.Join(", ", leftArr)}], Right subarray: [{string.Join(", ", rightArr)}]";

                    yield return Tuple.Create(
                        (int[])array.Clone(),
                        new int[] { kIndex, left + iIndex, mid + 1 + jIndex },
                        description
                    );

                    if (leftArr[iIndex] <= rightArr[jIndex])
                    {
                        array[kIndex] = leftArr[iIndex];
                        iIndex++;
                    }
                    else
                    {
                        array[kIndex] = rightArr[jIndex];
                        jIndex++;
                    }

                    yield return Tuple.Create(
                        (int[])array.Clone(),
                        new int[] { kIndex },
                        $"Placed value {array[kIndex]} at index {kIndex}. Current merged array: [{string.Join(", ", array[left..(right + 1)])}]"
                    );

                    kIndex++;
                }

                // Copy the remaining elements of leftArr[], if any
                while (iIndex < n1)
                {
                    array[kIndex] = leftArr[iIndex];
                    yield return Tuple.Create(
                        (int[])array.Clone(),
                        new int[] { kIndex },
                        $"Placed remaining value {array[kIndex]} from left subarray at index {kIndex}. Current merged array: [{string.Join(", ", array[left..(right + 1)])}]"
                    );
                    iIndex++;
                    kIndex++;
                }

                // Copy the remaining elements of rightArr[], if any
                while (jIndex < n2)
                {
                    array[kIndex] = rightArr[jIndex];
                    yield return Tuple.Create(
                        (int[])array.Clone(),
                        new int[] { kIndex },
                        $"Placed remaining value {array[kIndex]} from right subarray at index {kIndex}. Current merged array: [{string.Join(", ", array[left..(right + 1)])}]"
                    );
                    jIndex++;
                    kIndex++;
                }
            }

            //Recursively merge sort array
            IEnumerable<Tuple<int[], int[], string>> MergeSortRecursive(int[] array, int left, int right)
            {
                if (left < right)
                {
                    int mid = left + (right - left) / 2;

                    // Sort the first and second halves
                    foreach (var step in MergeSortRecursive(array, left, mid))
                        yield return step;

                    foreach (var step in MergeSortRecursive(array, mid + 1, right))
                        yield return step;

                    // Merge the sorted halves
                    foreach (var step in Merge(array, left, mid, right))
                        yield return step;
                }
            }

            // Start the sorting process
            foreach (var step in MergeSortRecursive(arr, 0, arr.Length - 1))
                yield return step;
        }
    }
}
