using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MoeGoe_GUI
{
    public partial class LengthWin : Form
    {
        public LengthWin(decimal lengthScale, Action<decimal> SetLengthScale)
        {
            InitializeComponent();
            timesBox.Value = lengthScale;
            this.SetLengthScale = SetLengthScale;
        }

        private readonly Action<decimal> SetLengthScale;

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            SetLengthScale(timesBox.Value);
            Close();
        }
    }
}
