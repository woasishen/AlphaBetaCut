using System;
using System.Windows.Forms;

namespace AlphaBetaCut
{
    public partial class ABTreeLine : UserControl
    {
        public ABTreeItem[] ABTreeItems;
        public int LayerIndex
        {
            set { label.Text = value + Environment.NewLine + ((Configs.LAYER_COUNT - value)%2 == 0 ? "Min" : "Max"); }
        }
        public ABTreeLine(int layerIndex, bool enableText)
        {
            InitializeComponent();
            LayerIndex = layerIndex;

            tableLayoutPanel.ColumnCount = (int) Math.Pow(Configs.CHILD_COUNT, layerIndex);
            for (int i = 1; i < tableLayoutPanel.ColumnCount; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            }

            ABTreeItems = new ABTreeItem[tableLayoutPanel.ColumnCount];
            for (int i = 0; i < ABTreeItems.Length; i++)
            {
                ABTreeItems[i] = new ABTreeItem(layerIndex, i)
                {
                    Anchor = AnchorStyles.None,
                    TextEnable = enableText
                };
                if (ABTreeItems[i].TextEnable)
                {
                    ABTreeItems[i].Gole = Configs.InitGole.Count > i ? Configs.InitGole[i] : i + 1;
                }
            }

            tableLayoutPanel.Controls.AddRange(ABTreeItems);
        }
    }
}
