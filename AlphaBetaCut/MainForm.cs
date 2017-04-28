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

            using (var form = new SelectDepthForm())
            {
                form.ShowDialog();
            }

            Configs.UseAbCut = UseAbCut.Checked;
            abTree.LayerCount = Configs.Depth;
            _maxMin = new MaxMin(abTree);
            Configs.LogMsg += s =>
            {
                HandleCtrlInOtherThread.HandleCtrlInBackGroundThread(this, arg =>
                {
                    richTextBox.AppendText(arg);
                    richTextBox.AppendText(Environment.NewLine);
                }, s);
            };
            Configs.EnableNextStep += () =>
            {
                HandleCtrlInOtherThread.HandleCtrlInBackGroundThread(this, () => button.Enabled = true);
            };
            Configs.ComputeFinished += () =>
            {
                HandleCtrlInOtherThread.HandleCtrlInBackGroundThread(this, () => startButton.Enabled = true);
            };
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            _maxMin.StartFind();
            startButton.Enabled = false;
            button.Show();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Configs.ExcutSemaphore.Release();
            button.Enabled = false;
        }

        private void UseAbCut_CheckedChanged(object sender, EventArgs e)
        {
            Configs.UseAbCut = UseAbCut.Checked;
        }
    }
}
