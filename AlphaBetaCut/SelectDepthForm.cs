using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaBetaCut
{
    public partial class SelectDepthForm : Form
    {
        public SelectDepthForm()
        {
            InitializeComponent();
            depthTextBox.Text = Configs.Depth.ToString();
            childCountTextBox.Text = Configs.ChildCount.ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Configs.Depth = Convert.ToInt32(depthTextBox.Text);
            Configs.ChildCount = Convert.ToInt32(childCountTextBox.Text);
        }
    }
}
