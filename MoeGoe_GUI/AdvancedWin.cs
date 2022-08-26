using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MoeGoe_GUI
{
    public partial class AdvancedWin : Form
    {
        public AdvancedWin(TextBox box, CommandLine cmd)
        {
            InitializeComponent();
            parentBox = box;
            this.cmd = cmd;
            Regex regexLength = new Regex(@"\[LENGTH=.+?\]");
            Regex regexCleaned = new Regex(@"\[CLEANED\]");
            Match match = regexLength.Match(box.Text);
            if (match.Success)
            {
                lengthScale = match.Value;
                textBox.Text = regexCleaned.Replace(regexLength.Replace(box.Text, ""), "");
            }
            else
                textBox.Text = regexCleaned.Replace(box.Text, "");
            cmd.OutputHandler += Cmd_OutputHandler;
        }

        private readonly TextBox parentBox;
        private readonly CommandLine cmd;
        private readonly string lengthScale;

        private void Cmd_OutputHandler(CommandLine sender, string e)
        {
            Invoke(new Action(() => cleanedBox.Text = e));
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (lengthScale == null)
                parentBox.Text = "[CLEANED]" + cleanedBox.Text;
            else
                parentBox.Text = lengthScale + "[CLEANED]" + cleanedBox.Text;
            Close();
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            cmd.Write("t");
            cmd.Write("[ADVANCED]");
            cmd.Write(textBox.Text);
        }

        private void AdvancedWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            cmd.OutputHandler -= Cmd_OutputHandler;
        }
    }
}
