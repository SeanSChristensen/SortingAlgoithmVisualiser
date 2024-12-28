namespace Algorithms
{
    public partial class Form1 : Form
    {
        private int[] values;
        private int[] highlights;
        public Form1()
        {
            values = [80, 20, 50, 30, 40, 90, 10];
            highlights = [];
            InitializeComponent();
            comboBox1.Items.Add("Selection Sort");
            comboBox1.Items.Add("Insertion Sort");
            comboBox1.Items.Add("Bubble Sort");
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);
            Brush brush = new SolidBrush(Color.Black);

            for (int i = 0; i < values.Length; i++)
            {
                int x = 40 + (i * 40); // x-coordinate
                int height = values[i]; // height of the rectangle
                int y = 400 - height;   // align rectangles on the y-axis based on height
                if (highlights.Contains(i)) 
                {
                    brush = new SolidBrush(Color.Red);
                }
                else
                {
                    brush = new SolidBrush(Color.Black);
                }
                g.FillRectangle(brush, x, y, 20, height);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TODO make this switch statement
            if(comboBox1.SelectedItem == "Selection Sort")
            {
                foreach (var step in SelectionSort(values))
                {
                    highlights = step.Item2;
                    Thread.Sleep(1000);
                    this.Refresh();
                }
                return;
            }
            if (comboBox1.SelectedItem == "Insertion Sort")
            {
                foreach (var step in InsertionSort(values))
                {
                    highlights = step.Item2;
                    Thread.Sleep(1000);
                    this.Refresh();
                }
                return;
            }
            if (comboBox1.SelectedItem == "Bubble Sort")
            {
                foreach (var step in BubbleSort(values))
                {
                    highlights = step.Item2;
                    Thread.Sleep(1000);
                    this.Refresh();
                }
                return;
            }

        }

        static IEnumerable<Tuple<int[], int[]>> SelectionSort(int[] arr)
        {
            int n = arr.Length;

            // One by one move the boundary of the unsorted subarray
            for (int i = 0; i < n - 1; i++)
            {
                // Find the minimum element in the unsorted subarray
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
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

                    // Yield the current state of the array along with the swapped indices as an int array
                    yield return Tuple.Create((int[])arr.Clone(), new int[] { i, minIndex });
                }
            }
        }

        //TODO, When comparing it returns the 2 values as the same value?? FIX
        static IEnumerable<Tuple<int[], int[]>> InsertionSort(int[] arr)
        {
            int n = arr.Length;

            for (int i = 1; i < n; i++)
            {
                int key = arr[i];
                int j = i - 1;

                // Move elements of arr[0..i-1] that are greater than key
                // to one position ahead of their current position
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    yield return Tuple.Create((int[])arr.Clone(), new int[] { j, j + 1 });
                    j--;
                }

                arr[j + 1] = key;
                if (j + 1 != i)
                {
                    yield return Tuple.Create((int[])arr.Clone(), new int[] { j + 1, i });
                }
            }
        }

        static IEnumerable<Tuple<int[], int[]>> BubbleSort(int[] arr)
        {
            int n = arr.Length;

            for (int i = 0; i < n - 1; i++)
            {
                // Traverse the array up to n-i-1
                for (int j = 0; j < n - i - 1; j++)
                {
                    // Yield the comparison before the swap
                    yield return Tuple.Create((int[])arr.Clone(), new int[] { j, j + 1 });

                    // Swap if the element is greater than the next element
                    if (arr[j] > arr[j + 1])
                    {
                        // Perform the swap
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;

                        // Yield the current state of the array and swapped indices
                        yield return Tuple.Create((int[])arr.Clone(), new int[] { j, j + 1 });
                    }
                }
            }
        }
    }
}
