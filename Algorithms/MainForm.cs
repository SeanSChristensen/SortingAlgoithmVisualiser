namespace Algorithms
{
    public partial class MainForm : Form
    {
        private int[] values;
        private int[] highlights;

        private readonly Dictionary<string, Func<int[], IEnumerable<Tuple<int[], int[], string>>>> sortMethods = new Dictionary<string, Func<int[], IEnumerable<Tuple<int[], int[], string>>>>()     
        {
            { "Selection Sort", SelectionSort },
            { "Insertion Sort", InsertionSort },
            { "Bubble Sort", BubbleSort },
            { "Merge Sort", MergeSort }
        };

        public MainForm()
        {
            values = [80, 20, 50, 30, 40, 90, 10];
            highlights = [];
            InitializeComponent();
            SortSelectionComboBox.Items.Add("Selection Sort");
            SortSelectionComboBox.Items.Add("Insertion Sort");
            SortSelectionComboBox.Items.Add("Bubble Sort");
            SortSelectionComboBox.Items.Add("Merge Sort");
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Brush brush = new SolidBrush(Color.Black);
            Font font = new Font("Arial", 10); // Font for the text
            Brush textBrush = new SolidBrush(Color.Black); // Brush for the text

            for (int i = 0; i < values.Length; i++)
            {
                int x = 40 + (i * 40); // x-coordinate
                int height = values[i]; // height of the rectangle
                int y = 400 - height;   // align rectangles on the y-axis based on height

                // Highlight specific rectangles
                if (highlights.Contains(i))
                {
                    brush = new SolidBrush(Color.Red);
                }
                else
                {
                    brush = new SolidBrush(Color.Black);
                }

                // Draw the rectangle
                graphics.FillRectangle(brush, x, y, 20, height);

                // Draw the value of the rectangle below it
                string valueText = values[i].ToString();
                graphics.DrawString(valueText, font, textBrush, x, 400 + 5);

                // Draw the index of the rectangle below the value
                string indexText = i.ToString();
                graphics.DrawString(indexText, font, textBrush, x, 400 + 20);
            }
        }

        private void SortingStartButton_Click(object sender, EventArgs e)
        {
            if (SortSelectionComboBox.SelectedItem != null && sortMethods.TryGetValue(SortSelectionComboBox.SelectedItem.ToString(), out var sortMethod))
            {
                foreach (var step in sortMethod(values))
                {
                    highlights = step.Item2;
                    SortingLogTextRichBox.Text += step.Item3 + "\n\n";
                    Thread.Sleep(1000); 
                    this.Refresh();    
                }
            }
            else
            {
                MessageBox.Show("Please select a valid sorting algorithm.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static IEnumerable<Tuple<int[], int[], string>> SelectionSort(int[] arr)
        {
            int n = arr.Length;

            // One by one move the boundary of the unsorted subarray
            for (int i = 0; i < n - 1; i++)
            {
                // Find the minimum element in the unsorted subarray
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

                // Swap the found minimum element with the first element of the subarray
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

        //TODO, When comparing it returns the 2 values as the same value?? FIX
        static IEnumerable<Tuple<int[], int[], string>> InsertionSort(int[] arr)
        {
            int n = arr.Length;

            for (int i = 1; i < n; i++)
            {
                int key = arr[i];
                int j = i - 1;

                // Move elements of arr[0..i-1] that are greater than key
                // to one position ahead of their current position
                while (j >= 0)
                {
                    // Yield each comparison
                    yield return Tuple.Create(
                        (int[])arr.Clone(),
                        new int[] { j, j + 1 },
                        $"Comparing elements at indices {j} and {i} ({arr[j]} and {key})"
                    );

                    if (arr[j] > key)
                    {
                        arr[j + 1] = arr[j];
                        // Yield after moving the element
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

                arr[j + 1] = key;
                if (j + 1 != i)
                {
                    // Yield after placing the key
                    yield return Tuple.Create(
                        (int[])arr.Clone(),
                        new int[] { j + 1, i },
                        $"Inserted key {key} at index {j + 1}"
                    );
                }
            }
        }

        static IEnumerable<Tuple<int[], int[], string>> BubbleSort(int[] arr)
        {
            int n = arr.Length;

            for (int i = 0; i < n - 1; i++)
            {
                // Traverse the array up to n-i-1
                for (int j = 0; j < n - i - 1; j++)
                {
                    // Yield the comparison
                    yield return Tuple.Create(
                        (int[])arr.Clone(),
                        new int[] { j, j + 1 },
                        $"Comparing elements at indices {j} and {j + 1} ({arr[j]} and {arr[j + 1]})"
                    );

                    // Swap if the element is greater than the next element
                    if (arr[j] > arr[j + 1])
                    {
                        // Perform the swap
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;

                        // Yield the current state of the array and swapped indices
                        yield return Tuple.Create(
                            (int[])arr.Clone(),
                            new int[] { j, j + 1 },
                            $"Swapping elements at indices {j} and {j + 1} ({arr[j]} and {arr[j + 1]})"
                        );
                    }
                }
            }
        }

        static IEnumerable<Tuple<int[], int[], string>> MergeSort(int[] arr)
        {
            // Helper function for merging two subarrays
            IEnumerable<Tuple<int[], int[], string>> Merge(int[] array, int left, int mid, int right)
            {
                int n1 = mid - left + 1;
                int n2 = right - mid;

                int[] leftArr = new int[n1];
                int[] rightArr = new int[n2];

                // Copy data to temp arrays
                for (int i = 0; i < n1; i++)
                    leftArr[i] = array[left + i];
                for (int j = 0; j < n2; j++)
                    rightArr[j] = array[mid + 1 + j];

                int iIndex = 0, jIndex = 0;
                int kIndex = left;

                // Merge the temp arrays back into the main array
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

            // Main recursive logic for Merge Sort
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
