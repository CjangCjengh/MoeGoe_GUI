namespace MoeGoe_GUI
{
    partial class HAdvancedWin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HAdvancedWin));
            this.lengthBox = new System.Windows.Forms.NumericUpDown();
            this.confirmButton = new System.Windows.Forms.Button();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.noiseLabel = new System.Windows.Forms.Label();
            this.noiseBox = new System.Windows.Forms.NumericUpDown();
            this.noisewLabel = new System.Windows.Forms.Label();
            this.noisewBox = new System.Windows.Forms.NumericUpDown();
            this.f0Label = new System.Windows.Forms.Label();
            this.f0Box = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.lengthBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noiseBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noisewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.f0Box)).BeginInit();
            this.SuspendLayout();
            // 
            // lengthBox
            // 
            this.lengthBox.DecimalPlaces = 2;
            this.lengthBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.lengthBox.Location = new System.Drawing.Point(136, 25);
            this.lengthBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.lengthBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.lengthBox.Name = "lengthBox";
            this.lengthBox.Size = new System.Drawing.Size(86, 31);
            this.lengthBox.TabIndex = 1;
            this.lengthBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(73, 190);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(120, 35);
            this.confirmButton.TabIndex = 6;
            this.confirmButton.Text = "确定";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // lengthLabel
            // 
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.Location = new System.Drawing.Point(38, 28);
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size(82, 24);
            this.lengthLabel.TabIndex = 0;
            this.lengthLabel.Text = "时长比例";
            // 
            // noiseLabel
            // 
            this.noiseLabel.AutoSize = true;
            this.noiseLabel.Location = new System.Drawing.Point(38, 65);
            this.noiseLabel.Name = "noiseLabel";
            this.noiseLabel.Size = new System.Drawing.Size(82, 24);
            this.noiseLabel.TabIndex = 2;
            this.noiseLabel.Text = "噪声比例";
            // 
            // noiseBox
            // 
            this.noiseBox.DecimalPlaces = 3;
            this.noiseBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.noiseBox.Location = new System.Drawing.Point(136, 62);
            this.noiseBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.noiseBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.noiseBox.Name = "noiseBox";
            this.noiseBox.Size = new System.Drawing.Size(86, 31);
            this.noiseBox.TabIndex = 3;
            this.noiseBox.Value = new decimal(new int[] {
            667,
            0,
            0,
            196608});
            // 
            // noisewLabel
            // 
            this.noisewLabel.AutoSize = true;
            this.noisewLabel.Location = new System.Drawing.Point(38, 102);
            this.noisewLabel.Name = "noisewLabel";
            this.noisewLabel.Size = new System.Drawing.Size(82, 24);
            this.noisewLabel.TabIndex = 4;
            this.noisewLabel.Text = "噪声偏差";
            // 
            // noisewBox
            // 
            this.noisewBox.DecimalPlaces = 2;
            this.noisewBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.noisewBox.Location = new System.Drawing.Point(136, 99);
            this.noisewBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.noisewBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.noisewBox.Name = "noisewBox";
            this.noisewBox.Size = new System.Drawing.Size(86, 31);
            this.noisewBox.TabIndex = 5;
            this.noisewBox.Value = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            // 
            // f0Label
            // 
            this.f0Label.AutoSize = true;
            this.f0Label.Enabled = false;
            this.f0Label.Location = new System.Drawing.Point(38, 139);
            this.f0Label.Name = "f0Label";
            this.f0Label.Size = new System.Drawing.Size(82, 24);
            this.f0Label.TabIndex = 7;
            this.f0Label.Text = "基频比例";
            // 
            // f0Box
            // 
            this.f0Box.DecimalPlaces = 2;
            this.f0Box.Enabled = false;
            this.f0Box.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.f0Box.Location = new System.Drawing.Point(136, 136);
            this.f0Box.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.f0Box.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.f0Box.Name = "f0Box";
            this.f0Box.Size = new System.Drawing.Size(86, 31);
            this.f0Box.TabIndex = 8;
            this.f0Box.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // HAdvancedWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(267, 248);
            this.Controls.Add(this.f0Label);
            this.Controls.Add(this.f0Box);
            this.Controls.Add(this.noisewLabel);
            this.Controls.Add(this.noisewBox);
            this.Controls.Add(this.noiseLabel);
            this.Controls.Add(this.noiseBox);
            this.Controls.Add(this.lengthLabel);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.lengthBox);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HAdvancedWin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数设置";
            ((System.ComponentModel.ISupportInitialize)(this.lengthBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noiseBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noisewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.f0Box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown lengthBox;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.Label noiseLabel;
        private System.Windows.Forms.NumericUpDown noiseBox;
        private System.Windows.Forms.Label noisewLabel;
        private System.Windows.Forms.NumericUpDown noisewBox;
        private System.Windows.Forms.Label f0Label;
        private System.Windows.Forms.NumericUpDown f0Box;
    }
}