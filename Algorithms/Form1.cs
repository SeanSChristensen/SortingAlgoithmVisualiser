namespace Algorithms
{
    public partial class Form1 : Form
    {
        private int[] values;
        private int[] highlights;
        public Form1()
        {
            values = [80, 20, 50, 30, 40];
            highlights = [];
            InitializeComponent();
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
            foreach (var step in SelectionSort(values))
            {
                highlights = step.Item2;
                Thread.Sleep(1000);
                this.Refresh();
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
    }
}
