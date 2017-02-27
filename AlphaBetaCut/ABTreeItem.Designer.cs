namespace AlphaBetaCut
{
    sealed partial class ABTreeItem
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox = new System.Windows.Forms.TextBox();
            this.alphaLabel = new System.Windows.Forms.Label();
            this.betaLabel = new System.Windows.Forms.Label();
            this.bestLabel = new System.Windows.Forms.Label();
            this.showAbCutLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox.Location = new System.Drawing.Point(0, 36);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(43, 21);
            this.textBox.TabIndex = 0;
            this.textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // alphaLabel
            // 
            this.alphaLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.alphaLabel.Location = new System.Drawing.Point(0, 12);
            this.alphaLabel.Name = "alphaLabel";
            this.alphaLabel.Size = new System.Drawing.Size(43, 12);
            this.alphaLabel.TabIndex = 1;
            this.alphaLabel.Text = "α:999";
            // 
            // betaLabel
            // 
            this.betaLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.betaLabel.Location = new System.Drawing.Point(0, 24);
            this.betaLabel.Name = "betaLabel";
            this.betaLabel.Size = new System.Drawing.Size(43, 12);
            this.betaLabel.TabIndex = 2;
            this.betaLabel.Text = "β:999";
            // 
            // bestLabel
            // 
            this.bestLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.bestLabel.Location = new System.Drawing.Point(0, 0);
            this.bestLabel.Name = "bestLabel";
            this.bestLabel.Size = new System.Drawing.Size(43, 12);
            this.bestLabel.TabIndex = 3;
            this.bestLabel.Text = "B：999";
            // 
            // showAbCutLabel
            // 
            this.showAbCutLabel.BackColor = System.Drawing.Color.DarkOrange;
            this.showAbCutLabel.Location = new System.Drawing.Point(0, 0);
            this.showAbCutLabel.Name = "showAbCutLabel";
            this.showAbCutLabel.Size = new System.Drawing.Size(43, 57);
            this.showAbCutLabel.TabIndex = 4;
            this.showAbCutLabel.Text = "\r\nAB_CUT";
            this.showAbCutLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.showAbCutLabel.Visible = false;
            // 
            // ABTreeItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Moccasin;
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.showAbCutLabel);
            this.Controls.Add(this.betaLabel);
            this.Controls.Add(this.alphaLabel);
            this.Controls.Add(this.bestLabel);
            this.MaximumSize = new System.Drawing.Size(43, 57);
            this.MinimumSize = new System.Drawing.Size(43, 57);
            this.Name = "ABTreeItem";
            this.Size = new System.Drawing.Size(43, 57);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label alphaLabel;
        private System.Windows.Forms.Label betaLabel;
        private System.Windows.Forms.Label bestLabel;
        private System.Windows.Forms.Label showAbCutLabel;
    }
}
