using System;
using System.Windows.Forms;

namespace MoeGoe_GUI
{
    public partial class AdvancedWin : Form
    {
        public AdvancedWin(Func<decimal[]> GetParameters,
            Action<decimal, decimal, decimal> SetParameters)
        {
            InitializeComponent();
            decimal[] parameters = GetParameters();
            lengthBox.Value = parameters[0];
            noiseBox.Value = parameters[1];
            noisewBox.Value = parameters[2];
            this.SetParameters = SetParameters;
        }

        private readonly Action<decimal, decimal, decimal> SetParameters;

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            SetParameters(lengthBox.Value, noiseBox.Value, noisewBox.Value);
            Close();
        }
    }
}
