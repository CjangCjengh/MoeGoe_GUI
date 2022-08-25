namespace MoeGoe_GUI
{
    partial class MainWin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
            this.EXEPanel = new System.Windows.Forms.GroupBox();
            this.EXEPath = new System.Windows.Forms.TextBox();
            this.openEXE = new System.Windows.Forms.Button();
            this.modelPanel = new System.Windows.Forms.GroupBox();
            this.configPath = new System.Windows.Forms.TextBox();
            this.openConfig = new System.Windows.Forms.Button();
            this.modelPath = new System.Windows.Forms.TextBox();
            this.openModel = new System.Windows.Forms.Button();
            this.modeControl = new System.Windows.Forms.TabControl();
            this.TTSPage = new System.Windows.Forms.TabPage();
            this.speakerBox = new System.Windows.Forms.ComboBox();
            this.speakerLabel = new System.Windows.Forms.Label();
            this.textLabel = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.menuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cleanButton = new System.Windows.Forms.ToolStripMenuItem();
            this.lengthButton = new System.Windows.Forms.ToolStripMenuItem();
            this.VCPage = new System.Windows.Forms.TabPage();
            this.openOrigin = new System.Windows.Forms.Button();
            this.originPath = new System.Windows.Forms.TextBox();
            this.originBox = new System.Windows.Forms.ComboBox();
            this.originLabel = new System.Windows.Forms.Label();
            this.targetBox = new System.Windows.Forms.ComboBox();
            this.targetLabel = new System.Windows.Forms.Label();
            this.savePanel = new System.Windows.Forms.GroupBox();
            this.savePath = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.consoleBox = new System.Windows.Forms.TextBox();
            this.EXEPanel.SuspendLayout();
            this.modelPanel.SuspendLayout();
            this.modeControl.SuspendLayout();
            this.TTSPage.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.VCPage.SuspendLayout();
            this.savePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // EXEPanel
            // 
            this.EXEPanel.Controls.Add(this.EXEPath);
            this.EXEPanel.Controls.Add(this.openEXE);
            this.EXEPanel.Location = new System.Drawing.Point(12, 12);
            this.EXEPanel.Name = "EXEPanel";
            this.EXEPanel.Size = new System.Drawing.Size(485, 85);
            this.EXEPanel.TabIndex = 0;
            this.EXEPanel.TabStop = false;
            this.EXEPanel.Text = "MoeGoe.exe";
            // 
            // EXEPath
            // 
            this.EXEPath.Location = new System.Drawing.Point(132, 32);
            this.EXEPath.Name = "EXEPath";
            this.EXEPath.Size = new System.Drawing.Size(347, 31);
            this.EXEPath.TabIndex = 1;
            this.EXEPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EXEPath_KeyPress);
            // 
            // openEXE
            // 
            this.openEXE.Location = new System.Drawing.Point(6, 30);
            this.openEXE.Name = "openEXE";
            this.openEXE.Size = new System.Drawing.Size(120, 35);
            this.openEXE.TabIndex = 0;
            this.openEXE.Text = "打开文件";
            this.openEXE.UseVisualStyleBackColor = true;
            this.openEXE.Click += new System.EventHandler(this.OpenEXE_Click);
            // 
            // modelPanel
            // 
            this.modelPanel.Controls.Add(this.configPath);
            this.modelPanel.Controls.Add(this.openConfig);
            this.modelPanel.Controls.Add(this.modelPath);
            this.modelPanel.Controls.Add(this.openModel);
            this.modelPanel.Enabled = false;
            this.modelPanel.Location = new System.Drawing.Point(12, 103);
            this.modelPanel.Name = "modelPanel";
            this.modelPanel.Size = new System.Drawing.Size(485, 125);
            this.modelPanel.TabIndex = 1;
            this.modelPanel.TabStop = false;
            this.modelPanel.Text = "模型文件";
            // 
            // configPath
            // 
            this.configPath.Location = new System.Drawing.Point(132, 73);
            this.configPath.Name = "configPath";
            this.configPath.Size = new System.Drawing.Size(347, 31);
            this.configPath.TabIndex = 3;
            this.configPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConfigPath_KeyPress);
            // 
            // openConfig
            // 
            this.openConfig.Location = new System.Drawing.Point(6, 71);
            this.openConfig.Name = "openConfig";
            this.openConfig.Size = new System.Drawing.Size(120, 35);
            this.openConfig.TabIndex = 2;
            this.openConfig.Text = "打开配置";
            this.openConfig.UseVisualStyleBackColor = true;
            this.openConfig.Click += new System.EventHandler(this.OpenConfig_Click);
            // 
            // modelPath
            // 
            this.modelPath.Location = new System.Drawing.Point(132, 32);
            this.modelPath.Name = "modelPath";
            this.modelPath.Size = new System.Drawing.Size(347, 31);
            this.modelPath.TabIndex = 1;
            this.modelPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ModelPath_KeyPress);
            // 
            // openModel
            // 
            this.openModel.Location = new System.Drawing.Point(6, 30);
            this.openModel.Name = "openModel";
            this.openModel.Size = new System.Drawing.Size(120, 35);
            this.openModel.TabIndex = 0;
            this.openModel.Text = "打开模型";
            this.openModel.UseVisualStyleBackColor = true;
            this.openModel.Click += new System.EventHandler(this.OpenModel_Click);
            // 
            // modeControl
            // 
            this.modeControl.Controls.Add(this.TTSPage);
            this.modeControl.Controls.Add(this.VCPage);
            this.modeControl.Enabled = false;
            this.modeControl.Location = new System.Drawing.Point(12, 234);
            this.modeControl.Name = "modeControl";
            this.modeControl.SelectedIndex = 0;
            this.modeControl.Size = new System.Drawing.Size(487, 189);
            this.modeControl.TabIndex = 2;
            // 
            // TTSPage
            // 
            this.TTSPage.Controls.Add(this.speakerBox);
            this.TTSPage.Controls.Add(this.speakerLabel);
            this.TTSPage.Controls.Add(this.textLabel);
            this.TTSPage.Controls.Add(this.textBox);
            this.TTSPage.Location = new System.Drawing.Point(4, 33);
            this.TTSPage.Name = "TTSPage";
            this.TTSPage.Padding = new System.Windows.Forms.Padding(3);
            this.TTSPage.Size = new System.Drawing.Size(479, 152);
            this.TTSPage.TabIndex = 0;
            this.TTSPage.Text = "语音合成";
            this.TTSPage.UseVisualStyleBackColor = true;
            // 
            // speakerBox
            // 
            this.speakerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.speakerBox.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.speakerBox.FormattingEnabled = true;
            this.speakerBox.Location = new System.Drawing.Point(129, 109);
            this.speakerBox.Name = "speakerBox";
            this.speakerBox.Size = new System.Drawing.Size(344, 33);
            this.speakerBox.TabIndex = 3;
            // 
            // speakerLabel
            // 
            this.speakerLabel.AutoSize = true;
            this.speakerLabel.Location = new System.Drawing.Point(34, 113);
            this.speakerLabel.Name = "speakerLabel";
            this.speakerLabel.Size = new System.Drawing.Size(64, 24);
            this.speakerLabel.TabIndex = 2;
            this.speakerLabel.Text = "说话人";
            // 
            // textLabel
            // 
            this.textLabel.AutoSize = true;
            this.textLabel.Location = new System.Drawing.Point(43, 44);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(46, 24);
            this.textLabel.TabIndex = 0;
            this.textLabel.Text = "文本";
            // 
            // textBox
            // 
            this.textBox.ContextMenuStrip = this.menuStrip;
            this.textBox.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox.Location = new System.Drawing.Point(129, 10);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(344, 93);
            this.textBox.TabIndex = 1;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cleanButton,
            this.lengthButton});
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(153, 64);
            // 
            // cleanButton
            // 
            this.cleanButton.Name = "cleanButton";
            this.cleanButton.Size = new System.Drawing.Size(152, 30);
            this.cleanButton.Text = "清理文本";
            this.cleanButton.Click += new System.EventHandler(this.CleanButton_Click);
            // 
            // lengthButton
            // 
            this.lengthButton.Name = "lengthButton";
            this.lengthButton.Size = new System.Drawing.Size(152, 30);
            this.lengthButton.Text = "调整时长";
            this.lengthButton.Click += new System.EventHandler(this.LengthButton_Click);
            // 
            // VCPage
            // 
            this.VCPage.Controls.Add(this.openOrigin);
            this.VCPage.Controls.Add(this.originPath);
            this.VCPage.Controls.Add(this.originBox);
            this.VCPage.Controls.Add(this.originLabel);
            this.VCPage.Controls.Add(this.targetBox);
            this.VCPage.Controls.Add(this.targetLabel);
            this.VCPage.Location = new System.Drawing.Point(4, 33);
            this.VCPage.Name = "VCPage";
            this.VCPage.Padding = new System.Windows.Forms.Padding(3);
            this.VCPage.Size = new System.Drawing.Size(479, 152);
            this.VCPage.TabIndex = 1;
            this.VCPage.Text = "语音转换";
            this.VCPage.UseVisualStyleBackColor = true;
            // 
            // openOrigin
            // 
            this.openOrigin.Location = new System.Drawing.Point(12, 8);
            this.openOrigin.Name = "openOrigin";
            this.openOrigin.Size = new System.Drawing.Size(108, 35);
            this.openOrigin.TabIndex = 4;
            this.openOrigin.Text = "打开原音频";
            this.openOrigin.UseVisualStyleBackColor = true;
            this.openOrigin.Click += new System.EventHandler(this.OpenOrigin_Click);
            // 
            // originPath
            // 
            this.originPath.Location = new System.Drawing.Point(128, 10);
            this.originPath.Name = "originPath";
            this.originPath.Size = new System.Drawing.Size(344, 31);
            this.originPath.TabIndex = 12;
            this.originPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OriginPath_KeyPress);
            // 
            // originBox
            // 
            this.originBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.originBox.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.originBox.FormattingEnabled = true;
            this.originBox.Location = new System.Drawing.Point(128, 71);
            this.originBox.Name = "originBox";
            this.originBox.Size = new System.Drawing.Size(344, 33);
            this.originBox.TabIndex = 10;
            // 
            // originLabel
            // 
            this.originLabel.AutoSize = true;
            this.originLabel.Location = new System.Drawing.Point(25, 75);
            this.originLabel.Name = "originLabel";
            this.originLabel.Size = new System.Drawing.Size(82, 24);
            this.originLabel.TabIndex = 9;
            this.originLabel.Text = "原说话人";
            // 
            // targetBox
            // 
            this.targetBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetBox.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.targetBox.FormattingEnabled = true;
            this.targetBox.Location = new System.Drawing.Point(128, 109);
            this.targetBox.Name = "targetBox";
            this.targetBox.Size = new System.Drawing.Size(344, 33);
            this.targetBox.TabIndex = 8;
            // 
            // targetLabel
            // 
            this.targetLabel.AutoSize = true;
            this.targetLabel.Location = new System.Drawing.Point(16, 113);
            this.targetLabel.Name = "targetLabel";
            this.targetLabel.Size = new System.Drawing.Size(100, 24);
            this.targetLabel.TabIndex = 7;
            this.targetLabel.Text = "目标说话人";
            // 
            // savePanel
            // 
            this.savePanel.Controls.Add(this.savePath);
            this.savePanel.Controls.Add(this.saveButton);
            this.savePanel.Enabled = false;
            this.savePanel.Location = new System.Drawing.Point(12, 429);
            this.savePanel.Name = "savePanel";
            this.savePanel.Size = new System.Drawing.Size(485, 85);
            this.savePanel.TabIndex = 3;
            this.savePanel.TabStop = false;
            this.savePanel.Text = "保存文件";
            // 
            // savePath
            // 
            this.savePath.Location = new System.Drawing.Point(132, 32);
            this.savePath.Name = "savePath";
            this.savePath.ReadOnly = true;
            this.savePath.Size = new System.Drawing.Size(347, 31);
            this.savePath.TabIndex = 1;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(6, 30);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(120, 35);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "保存结果";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // consoleBox
            // 
            this.consoleBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.consoleBox.Location = new System.Drawing.Point(514, 24);
            this.consoleBox.Multiline = true;
            this.consoleBox.Name = "consoleBox";
            this.consoleBox.ReadOnly = true;
            this.consoleBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.consoleBox.Size = new System.Drawing.Size(436, 490);
            this.consoleBox.TabIndex = 4;
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(964, 534);
            this.Controls.Add(this.consoleBox);
            this.Controls.Add(this.savePanel);
            this.Controls.Add(this.modeControl);
            this.Controls.Add(this.modelPanel);
            this.Controls.Add(this.EXEPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainWin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MoeGoe";
            this.EXEPanel.ResumeLayout(false);
            this.EXEPanel.PerformLayout();
            this.modelPanel.ResumeLayout(false);
            this.modelPanel.PerformLayout();
            this.modeControl.ResumeLayout(false);
            this.TTSPage.ResumeLayout(false);
            this.TTSPage.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.VCPage.ResumeLayout(false);
            this.VCPage.PerformLayout();
            this.savePanel.ResumeLayout(false);
            this.savePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox EXEPanel;
        private System.Windows.Forms.TextBox EXEPath;
        private System.Windows.Forms.Button openEXE;
        private System.Windows.Forms.GroupBox modelPanel;
        private System.Windows.Forms.TextBox configPath;
        private System.Windows.Forms.Button openConfig;
        private System.Windows.Forms.TextBox modelPath;
        private System.Windows.Forms.Button openModel;
        private System.Windows.Forms.TabControl modeControl;
        private System.Windows.Forms.TabPage TTSPage;
        private System.Windows.Forms.TabPage VCPage;
        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ComboBox speakerBox;
        private System.Windows.Forms.Label speakerLabel;
        private System.Windows.Forms.TextBox originPath;
        private System.Windows.Forms.ComboBox originBox;
        private System.Windows.Forms.Label originLabel;
        private System.Windows.Forms.ComboBox targetBox;
        private System.Windows.Forms.Label targetLabel;
        private System.Windows.Forms.GroupBox savePanel;
        private System.Windows.Forms.TextBox savePath;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox consoleBox;
        private System.Windows.Forms.Button openOrigin;
        private System.Windows.Forms.ContextMenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem cleanButton;
        private System.Windows.Forms.ToolStripMenuItem lengthButton;
    }
}

