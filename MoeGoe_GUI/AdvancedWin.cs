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
            returnBox = box;
            this.cmd = cmd;
            Regex regexCleaned = new Regex(@"\[CLEANED\]");
            Match match = Regex.Match(box.Text,@"\[CLEANED\]");
            if (match.Success)
                textBox.Text = "";
            else
                textBox.Text = box.Text;
            cmd.OutputHandler += Cmd_OutputHandler;
        }

        private readonly TextBox returnBox;
        private readonly CommandLine cmd;

        private void Cmd_OutputHandler(CommandLine sender, string e)
        {
            Invoke(new Action(() => cleanedBox.Text = e));
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            returnBox.Text = "[CLEANED]" + cleanedBox.Text;
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
