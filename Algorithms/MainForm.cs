namespace Algorithms
{
    public partial class MainForm : Form
    {
        private int[] values;
        private int[] highlights;

        private readonly Dictionary<string, Func<int[], IEnumerable<Tuple<int[], int[], string>>>> sortMethods = new Dictionary<string, Func<int[], IEnumerable<Tuple<int[], int[], string>>>>()
        {
            { "Selection Sort", Algorithms.SelectionSort },
            { "Insertion Sort", Algorithms.InsertionSort },
            { "Bubble Sort", Algorithms.BubbleSort },
            { "Merge Sort", Algorithms.MergeSort }
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
            Font font = new Font("Arial", 10);
            Brush textBrush = new SolidBrush(Color.Black); 

            for (int i = 0; i < values.Length; i++)
            {
                int x = 40 + (i * 40);
                int height = values[i];
                int y = 400 - height;

                //If highlights contains the index of the currently being drawn rectangle then use colour red
                if (highlights.Contains(i))
                {
                    brush = new SolidBrush(Color.Red);
                }
                else
                {
                    brush = new SolidBrush(Color.Black);
                }

                //Draw the rectangle, value and index
                graphics.FillRectangle(brush, x, y, 20, height);

                string valueText = values[i].ToString();
                graphics.DrawString(valueText, font, textBrush, x, 400 + 5);

                string indexText = i.ToString();
                graphics.DrawString(indexText, font, textBrush, x, 400 + 20);
            }
        }

        private void SortingStartButton_Click(object sender, EventArgs e)
        {
            if (SortSelectionComboBox.SelectedItem != null && sortMethods.TryGetValue(SortSelectionComboBox.SelectedItem.ToString(), out var sortMethod))
            {
                SortButton.Enabled = false;
                ResetButton.Enabled = false;
                foreach (var step in sortMethod(values))
                {
                    highlights = step.Item2;
                    SortingLogTextRichBox.Text += step.Item3 + "\n\n";
                    Thread.Sleep(500);
                    this.Refresh();
                }
                highlights = [];
                this.Refresh();
                SortButton.Enabled = true;
                ResetButton.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please select a valid sorting algorithm.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ResetButton_Click(object sender, EventArgs e)
        {
            values = [80, 20, 50, 30, 40, 90, 10];
            highlights = [];
            SortingLogTextRichBox.Text = "";
            this.Refresh();
        }
    }
}
