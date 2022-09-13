using System;
using System.Windows.Forms;

namespace MoeGoe_GUI
{
    public partial class HAdvancedWin : Form
    {
        public HAdvancedWin(Func<decimal[]> GetParameters,
            Action<decimal, decimal, decimal, decimal> SetParameters, bool f0Enabled)
        {
            InitializeComponent();
            decimal[] parameters = GetParameters();
            lengthBox.Value = parameters[0];
            noiseBox.Value = parameters[1];
            noisewBox.Value = parameters[2];
            this.SetParameters = SetParameters;
            if (f0Enabled)
            {
                f0Box.Value = parameters[3];
                f0Label.Enabled = true;
                f0Box.Enabled = true;
            }
        }

        private readonly Action<decimal, decimal, decimal, decimal> SetParameters;

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            SetParameters(lengthBox.Value, noiseBox.Value, noisewBox.Value, f0Box.Value);
            Close();
        }
    }
}
