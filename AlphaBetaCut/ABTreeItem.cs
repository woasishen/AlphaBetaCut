using System.Windows.Forms;

namespace AlphaBetaCut
{
    public partial class ABTreeItem : UserControl
    {
        private const string BEST = "B：{0}";
        private const string ALPHA = "α:{0}";
        private const string BETA = "β:{0}";

        private int _best, _alpha, _beta, _gole;

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
            bestLabel.Text = $@"[{_layer},{_index}]";
        }

        public override string ToString()
        {
            return $@"[{_layer},{_index}]";
        }

        public bool TextEnable
        {
            get { return textBox.Enabled; }
            set { textBox.Enabled = value; }
        }

        public int Best
        {
            set
            {
                _best = value;
                bestLabel.Text = string.Format(BEST, _best);
            }
            get { return _best; }
        }

        public int Alpha
        {
            set
            {
                _alpha = value;
                alphaLabel.Text = string.Format(ALPHA, _alpha);
            }
            get { return _alpha; }
        }

        public int Beta
        {
            set
            {
                _beta = value;
                betaLabel.Text = string.Format(BETA, _beta);
            }
            get { return _beta; }
        }

        public int Gole
        {
            set
            {
                _gole = value;
                textBox.Text = _gole.ToString();
            }
            get { return _gole; }
        }
    }
}
