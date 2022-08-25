using System;
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
            textBox.Text = box.Text;
            cmd.OutputHandler += Cmd_OutputHandler;
        }

        private readonly TextBox parentBox;
        private readonly CommandLine cmd;

        private void Cmd_OutputHandler(CommandLine sender, string e)
        {
            Invoke(new Action(() => cleanedBox.Text = e));
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            parentBox.Text = "[CLEANED]" + cleanedBox.Text;
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
