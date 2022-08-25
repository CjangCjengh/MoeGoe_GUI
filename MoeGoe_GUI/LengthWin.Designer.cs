namespace MoeGoe_GUI
{
    partial class LengthWin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LengthWin));
            this.timesBox = new System.Windows.Forms.NumericUpDown();
            this.confirmButton = new System.Windows.Forms.Button();
            this.timesLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.timesBox)).BeginInit();
            this.SuspendLayout();
            // 
            // timesBox
            // 
            this.timesBox.DecimalPlaces = 1;
            this.timesBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.timesBox.Location = new System.Drawing.Point(71, 25);
            this.timesBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.timesBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.timesBox.Name = "timesBox";
            this.timesBox.Size = new System.Drawing.Size(86, 31);
            this.timesBox.TabIndex = 0;
            this.timesBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(71, 76);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(120, 35);
            this.confirmButton.TabIndex = 4;
            this.confirmButton.Text = "确定";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // timesLabel
            // 
            this.timesLabel.AutoSize = true;
            this.timesLabel.Location = new System.Drawing.Point(163, 28);
            this.timesLabel.Name = "timesLabel";
            this.timesLabel.Size = new System.Drawing.Size(28, 24);
            this.timesLabel.TabIndex = 5;
            this.timesLabel.Text = "倍";
            // 
            // LengthWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(261, 135);
            this.Controls.Add(this.timesLabel);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.timesBox);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LengthWin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "调整时长";
            ((System.ComponentModel.ISupportInitialize)(this.timesBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown timesBox;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Label timesLabel;
    }
}