using System.Collections.Generic;
using System.Windows.Forms;

namespace MoeGoe_GUI
{
    public partial class SymbolsWin : Form
    {
        public SymbolsWin(List<string> symbols, TextBox box)
        {
            InitializeComponent();
            foreach (string symbol in symbols)
            {
                Button button = new Button
                {
                    Width = 50,
                    Height = 50,
                    Text = symbol
                };
                button.Click += (sender, e) => box.Paste(((Button)sender).Text);
                symbolPanel.Controls.Add(button);
            }
        }
    }
}
