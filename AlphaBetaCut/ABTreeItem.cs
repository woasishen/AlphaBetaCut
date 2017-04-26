using System;
using System.Windows.Forms;

namespace AlphaBetaCut
{
    public sealed partial class ABTreeItem : UserControl
    {
        private const string BEST = "B：{0}";
        private const string ALPHA = "α:{0}";
        private const string BETA = "β:{0}";

        private int? _alpha, _beta, _best;
        private int _gole;

        private readonly int _layer, _index;

        public ABTreeItem(int layer, int index)
        {
            _layer = layer;
            _index = index;
            InitializeComponent();
            Best = Best;
            Alpha = Alpha;
            Beta = Beta;
            Gole = Gole;

            BackColor = Configs.OriginColor(_index);
        }

        public void ShowAbCut()
        {
            HandleCtrlInOtherThread.HandleCtrlInBackGroundThread(this, () =>
            {
                showAbCutLabel.Show();
            });
        }

        public bool Handling
        {
            set
            {
                HandleCtrlInOtherThread.HandleCtrlInBackGroundThread(this, () =>
                {
                    BackColor = value ? Configs.SelectedColor : Configs.OriginColor(_index);
                });
            }
        }

        public override string ToString()
        {
            return $@"[{_layer},{_index}]";
        }

        public bool TextEnable
        {
            get { return textBox.Enabled; }
            set
            {
                HandleCtrlInOtherThread.HandleCtrlInBackGroundThread(this, () =>
                {
                    textBox.Enabled = value;
                });
            }
        }

        public int? Best
        {
            set
            {
                HandleCtrlInOtherThread.HandleCtrlInBackGroundThread(this, () =>
                {
                    _best = value;
                    CheckAndSetString(BEST, bestLabel, _best);
                });
            }
            get { return _best; }
        }

        public int? Alpha
        {
            set
            {
                HandleCtrlInOtherThread.HandleCtrlInBackGroundThread(this, () =>
                {
                    _alpha = value;
                    CheckAndSetString(ALPHA, alphaLabel, _alpha);
                });
            }
            get { return _alpha; }
        }

        public int? Beta
        {
            set
            {
                HandleCtrlInOtherThread.HandleCtrlInBackGroundThread(this, () =>
                {
                    _beta = value;
                    CheckAndSetString(BETA, betaLabel, _beta);
                });
            }
            get { return _beta; }
        }

        public int Gole
        {
            set
            {
                HandleCtrlInOtherThread.HandleCtrlInBackGroundThread(this, () =>
                {
                    _gole = value;
                    textBox.Text = _gole.ToString();
                });
            }
            get { return _gole; }
        }

        private void CheckAndSetString(string str, Label label, int? gole)
        {
            if (gole == null)
            {
                label.Text = string.Format(str, "X");
                return;
            }

            if (gole == Configs.MAX)
            {
                label.Text = string.Format(str, "M");
                return;
            }
            if (gole == Configs.MIN)
            {
                label.Text = string.Format(str, "-M");
                return;
            }
            label.Text = string.Format(str, gole);
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox.Text, out _gole);
        }
    }
}
