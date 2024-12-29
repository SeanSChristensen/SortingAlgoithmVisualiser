namespace Algorithms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SortButton = new Button();
            SortSelectionComboBox = new ComboBox();
            SortingLogTextRichBox = new RichTextBox();
            SuspendLayout();
            // 
            // SortButton
            // 
            SortButton.Location = new Point(12, 12);
            SortButton.Name = "SortButton";
            SortButton.Size = new Size(93, 23);
            SortButton.TabIndex = 0;
            SortButton.Text = "Start Sorting";
            SortButton.UseVisualStyleBackColor = true;
            SortButton.Click += SortingStartButton_Click;
            // 
            // SortSelectionComboBox
            // 
            SortSelectionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            SortSelectionComboBox.FormattingEnabled = true;
            SortSelectionComboBox.Location = new Point(111, 12);
            SortSelectionComboBox.Name = "SortSelectionComboBox";
            SortSelectionComboBox.Size = new Size(121, 23);
            SortSelectionComboBox.TabIndex = 1;
            // 
            // SortingLogTextRichBox
            // 
            SortingLogTextRichBox.Location = new Point(604, 68);
            SortingLogTextRichBox.Name = "SortingLogTextRichBox";
            SortingLogTextRichBox.Size = new Size(168, 381);
            SortingLogTextRichBox.TabIndex = 2;
            SortingLogTextRichBox.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 461);
            Controls.Add(SortingLogTextRichBox);
            Controls.Add(SortSelectionComboBox);
            Controls.Add(SortButton);
            Name = "Form1";
            Text = "Sorting Algorithm Visualizer";
            Paint += MainForm_Paint;
            ResumeLayout(false);
        }

        #endregion

        private Button SortButton;
        private ComboBox SortSelectionComboBox;
        private RichTextBox SortingLogTextRichBox;
    }
}
