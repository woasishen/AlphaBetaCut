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
    public partial class MainForm : Form
    {
        private readonly MaxMin _maxMin;
        public MainForm()
        {
            InitializeComponent();
            _maxMin = new MaxMin(abTree);
        }
    }
}
