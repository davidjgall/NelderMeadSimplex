
namespace NelderMeadSimplexTest
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SolveButton = new System.Windows.Forms.Button();
            this.DisplayTextBox = new System.Windows.Forms.TextBox();
            this.ReSolveButton = new System.Windows.Forms.Button();
            this.RandomizeButton = new System.Windows.Forms.Button();
            this.CoefficientsLabel = new System.Windows.Forms.Label();
            this.CoefficientsTextBox = new System.Windows.Forms.TextBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.ShowGraphButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SolveButton
            // 
            this.SolveButton.Location = new System.Drawing.Point(12, 12);
            this.SolveButton.Name = "SolveButton";
            this.SolveButton.Size = new System.Drawing.Size(81, 23);
            this.SolveButton.TabIndex = 0;
            this.SolveButton.Text = "Solve";
            this.SolveButton.UseVisualStyleBackColor = true;
            this.SolveButton.Click += new System.EventHandler(this.SolveButton_Click);
            // 
            // DisplayTextBox
            // 
            this.DisplayTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DisplayTextBox.Location = new System.Drawing.Point(12, 41);
            this.DisplayTextBox.Multiline = true;
            this.DisplayTextBox.Name = "DisplayTextBox";
            this.DisplayTextBox.Size = new System.Drawing.Size(776, 128);
            this.DisplayTextBox.TabIndex = 1;
            this.DisplayTextBox.TabStop = false;
            // 
            // ReSolveButton
            // 
            this.ReSolveButton.Location = new System.Drawing.Point(99, 12);
            this.ReSolveButton.Name = "ReSolveButton";
            this.ReSolveButton.Size = new System.Drawing.Size(81, 23);
            this.ReSolveButton.TabIndex = 1;
            this.ReSolveButton.Text = "Re-Solve";
            this.ReSolveButton.UseVisualStyleBackColor = true;
            this.ReSolveButton.Click += new System.EventHandler(this.ReSolveButton_Click);
            // 
            // RandomizeButton
            // 
            this.RandomizeButton.Location = new System.Drawing.Point(186, 12);
            this.RandomizeButton.Name = "RandomizeButton";
            this.RandomizeButton.Size = new System.Drawing.Size(81, 23);
            this.RandomizeButton.TabIndex = 2;
            this.RandomizeButton.Text = "Randomize";
            this.RandomizeButton.UseVisualStyleBackColor = true;
            this.RandomizeButton.Click += new System.EventHandler(this.RandomizeButton_Click);
            // 
            // CoefficientsLabel
            // 
            this.CoefficientsLabel.AutoSize = true;
            this.CoefficientsLabel.Location = new System.Drawing.Point(453, 17);
            this.CoefficientsLabel.Name = "CoefficientsLabel";
            this.CoefficientsLabel.Size = new System.Drawing.Size(65, 13);
            this.CoefficientsLabel.TabIndex = 4;
            this.CoefficientsLabel.Text = "Coefficients:";
            // 
            // CoefficientsTextBox
            // 
            this.CoefficientsTextBox.Location = new System.Drawing.Point(524, 14);
            this.CoefficientsTextBox.Name = "CoefficientsTextBox";
            this.CoefficientsTextBox.Size = new System.Drawing.Size(264, 20);
            this.CoefficientsTextBox.TabIndex = 5;
            this.CoefficientsTextBox.TabStop = false;
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(273, 12);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(81, 23);
            this.ResetButton.TabIndex = 3;
            this.ResetButton.Text = "Reset Coeffs.";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // ShowGraphButton
            // 
            this.ShowGraphButton.Location = new System.Drawing.Point(360, 12);
            this.ShowGraphButton.Name = "ShowGraphButton";
            this.ShowGraphButton.Size = new System.Drawing.Size(81, 23);
            this.ShowGraphButton.TabIndex = 6;
            this.ShowGraphButton.Text = "Show Graph";
            this.ShowGraphButton.UseVisualStyleBackColor = true;
            this.ShowGraphButton.Click += new System.EventHandler(this.ShowGraphButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 181);
            this.Controls.Add(this.ShowGraphButton);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.CoefficientsTextBox);
            this.Controls.Add(this.CoefficientsLabel);
            this.Controls.Add(this.RandomizeButton);
            this.Controls.Add(this.ReSolveButton);
            this.Controls.Add(this.DisplayTextBox);
            this.Controls.Add(this.SolveButton);
            this.Name = "MainForm";
            this.Text = "Nelder-Mead Simplex";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SolveButton;
        private System.Windows.Forms.TextBox DisplayTextBox;
        private System.Windows.Forms.Button ReSolveButton;
        private System.Windows.Forms.Button RandomizeButton;
        private System.Windows.Forms.Label CoefficientsLabel;
        private System.Windows.Forms.TextBox CoefficientsTextBox;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button ShowGraphButton;
    }
}

