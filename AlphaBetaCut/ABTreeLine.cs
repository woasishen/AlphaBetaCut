using System;
using System.Windows.Forms;

namespace AlphaBetaCut
{
    public partial class ABTreeLine : UserControl
    {
        public ABTreeItem[] ABTreeItems;
        public int LayerIndex
        {
            get { return Convert.ToInt32(label.Text); }
            set { label.Text = value.ToString(); }
        }
        public ABTreeLine(int layerIndex, bool enableText)
        {
            InitializeComponent();
            LayerIndex = layerIndex;

            tableLayoutPanel.ColumnCount = (int) Math.Pow(2, LayerIndex);
            for (int i = 1; i < tableLayoutPanel.ColumnCount; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            }

            ABTreeItems = new ABTreeItem[tableLayoutPanel.ColumnCount];
            for (int i = 0; i < ABTreeItems.Length; i++)
            {
                ABTreeItems[i] = new ABTreeItem(LayerIndex, i)
                {
                    Anchor = AnchorStyles.None,
                    TextEnable = enableText
                };
            }

            tableLayoutPanel.Controls.AddRange(ABTreeItems);
        }
    }
}
