using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaBetaCut
{
    public partial class ABTree : UserControl
    {
        private int _layerCount = 6;

        public ABTreeLine[] ABTreeLines;

        public int LayerCount
        {
            get { return _layerCount; }
            set
            {
                _layerCount = value;
                UpdateSelf();
            }
        }

        public ABTree()
        {
            InitializeComponent();
            UpdateSelf();
        }

        private void UpdateSelf()
        {
            tableLayoutPanel.Controls.Clear();
            ABTreeLines = new ABTreeLine[LayerCount];

            tableLayoutPanel.RowCount = LayerCount;
            for (int i = 1; i < LayerCount; i++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            }
            for (int i = 0; i < LayerCount; i++)
            {
                ABTreeLines[i] = new ABTreeLine(i, i == LayerCount - 1)
                {
                    Dock = DockStyle.Fill
                };
                
            }
            tableLayoutPanel.Controls.AddRange(ABTreeLines);
        }
    }
}
