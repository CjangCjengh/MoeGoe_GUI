using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MoeGoe_GUI
{
    public partial class LengthWin : Form
    {
        public LengthWin(TextBox box)
        {
            InitializeComponent();
            this.box = box;
            Regex regex = new Regex(@"\[LENGTH=(.+?)\]");
            Match match = regex.Match(box.Text);
            if (match.Success)
            {
                text = regex.Replace(box.Text, "");
                try
                {
                    timesBox.Value = decimal.Parse(match.Groups[1].Value);
                }
                catch
                {
                    timesBox.Value = 1;
                }
            }
            else
                text = box.Text;
        }

        private readonly TextBox box;
        private readonly string text;

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            box.Text = "[LENGTH=" + timesBox.Value.ToString() + "]" + text;
            Close();
        }
    }
}
