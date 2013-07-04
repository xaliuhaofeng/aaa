namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Config_curve : UserControl
    {
        private Button btnAvg;
        private Button btnBaojing;
        private Button btnDuanDian;
        private Button btnFuDian;
        private Button btnMax;
        private Button btnMin;
        private Button btnMoNi;
        private Button button3;
        private ClientConfig clientConfig;
        private ColorDialog colorDialog1;
        private IContainer components = null;
        private GroupBox groupBox1;
        private Label label1;
        private Label label15;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblAvg;
        private Label lblBaoJing;
        private Label lblDuanDian;
        private Label lblFuDian;
        private Label lblMax;
        private Label lblMin;
        private Label lblMoNi;
        private Panel panel2;

        public Config_curve()
        {
            this.InitializeComponent();
            this.clientConfig = ClientConfig.CreateCommon();
            this.InitializeBackColor();
            this.Dock = DockStyle.Fill;
        }

        private void btnAvg_Click_1(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblAvg.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("pingJunZhiColor", ColorTranslator.ToHtml(this.lblAvg.BackColor));
            }
        }

        private void btnBaoJing_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblBaoJing.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("baoJingZhiColor", ColorTranslator.ToHtml(this.lblBaoJing.BackColor));
            }
        }

        private void btnDuanDian_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblDuanDian.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("duanDianZhiColor", ColorTranslator.ToHtml(this.lblDuanDian.BackColor));
            }
        }

        private void btnFuDian_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblFuDian.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("fuDianZhiColor", ColorTranslator.ToHtml(this.lblFuDian.BackColor));
            }
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblMax.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("zuiDaZhiColor", ColorTranslator.ToHtml(this.lblMax.BackColor));
            }
        }

        private void btnMin_Click_1(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblMin.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("zuiXiaoZhiColor", ColorTranslator.ToHtml(this.lblMin.BackColor));
            }
        }

        private void btnMoNi_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblMoNi.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("moNiLiangColor", ColorTranslator.ToHtml(this.lblMoNi.BackColor));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.SetColor();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.resetBackColor();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.label9.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("quXianBeiJingColor", ColorTranslator.ToHtml(this.label9.BackColor));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void InitializeBackColor()
        {
            this.lblBaoJing.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("baoJingZhiColor"));
            this.lblDuanDian.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("duanDianZhiColor"));
            this.lblFuDian.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("fuDianZhiColor"));
            this.lblMoNi.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("moNiLiangColor"));
            this.lblMax.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("zuiDaZhiColor"));
            this.lblMin.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("zuiXiaoZhiColor"));
            this.lblAvg.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("pingJunZhiColor"));
            this.label9.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("quXianBeiJingColor"));
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.label9 = new Label();
            this.label8 = new Label();
            this.button3 = new Button();
            this.btnAvg = new Button();
            this.btnMin = new Button();
            this.lblAvg = new Label();
            this.lblMin = new Label();
            this.label7 = new Label();
            this.label6 = new Label();
            this.lblMax = new Label();
            this.btnMax = new Button();
            this.lblMoNi = new Label();
            this.btnMoNi = new Button();
            this.lblFuDian = new Label();
            this.btnFuDian = new Button();
            this.lblDuanDian = new Label();
            this.btnDuanDian = new Button();
            this.lblBaoJing = new Label();
            this.btnBaojing = new Button();
            this.label5 = new Label();
            this.label2 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label1 = new Label();
            this.colorDialog1 = new ColorDialog();
            this.panel2 = new Panel();
            this.label15 = new Label();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.btnAvg);
            this.groupBox1.Controls.Add(this.btnMin);
            this.groupBox1.Controls.Add(this.lblAvg);
            this.groupBox1.Controls.Add(this.lblMin);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblMax);
            this.groupBox1.Controls.Add(this.btnMax);
            this.groupBox1.Controls.Add(this.lblMoNi);
            this.groupBox1.Controls.Add(this.btnMoNi);
            this.groupBox1.Controls.Add(this.lblFuDian);
            this.groupBox1.Controls.Add(this.btnFuDian);
            this.groupBox1.Controls.Add(this.lblDuanDian);
            this.groupBox1.Controls.Add(this.btnDuanDian);
            this.groupBox1.Controls.Add(this.lblBaoJing);
            this.groupBox1.Controls.Add(this.btnBaojing);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.groupBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox1.Location = new Point(0x4a, 0x6a);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x281, 0x111);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new EventHandler(this.groupBox1_Enter);
            this.label9.BackColor = Color.White;
            this.label9.Location = new Point(0x7a, 0xed);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x156, 20);
            this.label9.TabIndex = 0x2c;
            this.label9.Text = "    ";
            this.label8.AutoSize = true;
            this.label8.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label8.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label8.Location = new Point(0x24, 0xed);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x52, 14);
            this.label8.TabIndex = 0x2b;
            this.label8.Text = "曲线图背景";
            this.button3.BackColor = Color.Chocolate;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new Point(0x1e7, 0xed);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 0x2a;
            this.button3.Text = "选择颜色";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new EventHandler(this.button3_Click);
            this.btnAvg.BackColor = Color.Chocolate;
            this.btnAvg.ForeColor = System.Drawing.Color.White;
            this.btnAvg.Location = new Point(0x1e6, 0xd0);
            this.btnAvg.Name = "btnAvg";
            this.btnAvg.Size = new Size(0x4b, 0x17);
            this.btnAvg.TabIndex = 0x29;
            this.btnAvg.Text = "选择颜色";
            this.btnAvg.UseVisualStyleBackColor = false;
            this.btnAvg.Click += new EventHandler(this.btnAvg_Click_1);
            this.btnMin.BackColor = Color.Chocolate;
            this.btnMin.ForeColor = System.Drawing.Color.White;
            this.btnMin.Location = new Point(0x1e7, 0xb5);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new Size(0x4b, 0x17);
            this.btnMin.TabIndex = 40;
            this.btnMin.Text = "选择颜色";
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new EventHandler(this.btnMin_Click_1);
            this.lblAvg.BackColor = Color.White;
            this.lblAvg.Location = new Point(0x79, 0xce);
            this.lblAvg.Name = "lblAvg";
            this.lblAvg.Size = new Size(0x156, 20);
            this.lblAvg.TabIndex = 0x27;
            this.lblAvg.Text = "    ";
            this.lblMin.BackColor = Color.White;
            this.lblMin.Location = new Point(0x79, 180);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new Size(0x156, 20);
            this.lblMin.TabIndex = 0x26;
            this.lblMin.Text = "    ";
            this.label7.AutoSize = true;
            this.label7.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label7.Location = new Point(0x25, 0xcf);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x34, 14);
            this.label7.TabIndex = 0x25;
            this.label7.Text = "平均值";
            this.label6.AutoSize = true;
            this.label6.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label6.Location = new Point(0x25, 0xb5);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x34, 14);
            this.label6.TabIndex = 0x24;
            this.label6.Text = "最小值";
            this.label6.Click += new EventHandler(this.label6_Click);
            this.lblMax.BackColor = Color.White;
            this.lblMax.Location = new Point(0x79, 0x98);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new Size(0x156, 20);
            this.lblMax.TabIndex = 0x23;
            this.lblMax.Text = "    ";
            this.btnMax.BackColor = Color.Chocolate;
            this.btnMax.ForeColor = System.Drawing.Color.White;
            this.btnMax.Location = new Point(0x1e7, 0x98);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new Size(0x4b, 0x17);
            this.btnMax.TabIndex = 0x22;
            this.btnMax.Text = "选择颜色";
            this.btnMax.UseVisualStyleBackColor = false;
            this.btnMax.Click += new EventHandler(this.btnMax_Click);
            this.lblMoNi.BackColor = Color.White;
            this.lblMoNi.Location = new Point(0x79, 120);
            this.lblMoNi.Name = "lblMoNi";
            this.lblMoNi.Size = new Size(0x156, 20);
            this.lblMoNi.TabIndex = 0x21;
            this.lblMoNi.Text = "    ";
            this.btnMoNi.BackColor = Color.Chocolate;
            this.btnMoNi.ForeColor = System.Drawing.Color.White;
            this.btnMoNi.Location = new Point(0x1e7, 120);
            this.btnMoNi.Name = "btnMoNi";
            this.btnMoNi.Size = new Size(0x4b, 0x17);
            this.btnMoNi.TabIndex = 0x20;
            this.btnMoNi.Text = "选择颜色";
            this.btnMoNi.UseVisualStyleBackColor = false;
            this.btnMoNi.Click += new EventHandler(this.btnMoNi_Click);
            this.lblFuDian.BackColor = Color.White;
            this.lblFuDian.Location = new Point(0x79, 0x5b);
            this.lblFuDian.Name = "lblFuDian";
            this.lblFuDian.Size = new Size(0x156, 20);
            this.lblFuDian.TabIndex = 0x1f;
            this.lblFuDian.Text = "    ";
            this.btnFuDian.BackColor = Color.Chocolate;
            this.btnFuDian.ForeColor = System.Drawing.Color.White;
            this.btnFuDian.Location = new Point(0x1e7, 0x5b);
            this.btnFuDian.Name = "btnFuDian";
            this.btnFuDian.Size = new Size(0x4b, 0x17);
            this.btnFuDian.TabIndex = 30;
            this.btnFuDian.Text = "选择颜色";
            this.btnFuDian.UseVisualStyleBackColor = false;
            this.btnFuDian.Click += new EventHandler(this.btnFuDian_Click);
            this.lblDuanDian.BackColor = Color.White;
            this.lblDuanDian.Location = new Point(0x79, 0x3d);
            this.lblDuanDian.Name = "lblDuanDian";
            this.lblDuanDian.Size = new Size(0x156, 20);
            this.lblDuanDian.TabIndex = 0x1d;
            this.lblDuanDian.Text = "    ";
            this.btnDuanDian.BackColor = Color.Chocolate;
            this.btnDuanDian.ForeColor = System.Drawing.Color.White;
            this.btnDuanDian.Location = new Point(0x1e7, 0x3d);
            this.btnDuanDian.Name = "btnDuanDian";
            this.btnDuanDian.Size = new Size(0x4b, 0x17);
            this.btnDuanDian.TabIndex = 0x1c;
            this.btnDuanDian.Text = "选择颜色";
            this.btnDuanDian.UseVisualStyleBackColor = false;
            this.btnDuanDian.Click += new EventHandler(this.btnDuanDian_Click);
            this.lblBaoJing.BackColor = Color.White;
            this.lblBaoJing.Location = new Point(0x79, 30);
            this.lblBaoJing.Name = "lblBaoJing";
            this.lblBaoJing.Size = new Size(0x156, 20);
            this.lblBaoJing.TabIndex = 0x1b;
            this.lblBaoJing.Text = "    ";
            this.btnBaojing.BackColor = Color.Chocolate;
            this.btnBaojing.ForeColor = System.Drawing.Color.White;
            this.btnBaojing.Location = new Point(0x1e7, 30);
            this.btnBaojing.Name = "btnBaojing";
            this.btnBaojing.Size = new Size(0x4b, 0x17);
            this.btnBaojing.TabIndex = 0x1a;
            this.btnBaojing.Text = "选择颜色";
            this.btnBaojing.UseVisualStyleBackColor = false;
            this.btnBaojing.Click += new EventHandler(this.btnBaoJing_Click);
            this.label5.AutoSize = true;
            this.label5.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label5.Location = new Point(0x24, 0x98);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x34, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "最大值";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new Point(0x24, 0x3d);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x43, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "断电门限";
            this.label4.AutoSize = true;
            this.label4.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new Point(0x24, 120);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x43, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "曲线颜色";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new Point(0x24, 0x5b);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x43, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "复电门限";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0x24, 30);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x43, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "报警门限";
            this.panel2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel2.AutoSize = true;
            this.panel2.BackColor = Color.AliceBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label15);
            this.panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel2.Location = new Point(0, 0x2d);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x322, 0x1d);
            this.panel2.TabIndex = 0x25;
            this.label15.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label15.Location = new Point(70, 1);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x6f, 0x1a);
            this.label15.TabIndex = 8;
            this.label15.Text = "曲线设置";
            this.label15.TextAlign = ContentAlignment.MiddleLeft;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.groupBox1);
            base.Name = "Config_curve";
            base.Size = new Size(0x321, 0x1d3);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void resetBackColor()
        {
            this.InitializeBackColor();
        }

        private void SetColor()
        {
            this.clientConfig.set("baoJingZhiColor", ColorTranslator.ToHtml(this.lblBaoJing.BackColor));
            this.clientConfig.set("duanDianZhiColor", ColorTranslator.ToHtml(this.lblDuanDian.BackColor));
            this.clientConfig.set("fuDianZhiColor", ColorTranslator.ToHtml(this.lblFuDian.BackColor));
            this.clientConfig.set("moNiLiangColor", ColorTranslator.ToHtml(this.lblMoNi.BackColor));
            this.clientConfig.set("zuiDaZhiColor", ColorTranslator.ToHtml(this.lblMax.BackColor));
            this.clientConfig.set("zuiXiaoZhiColor", ColorTranslator.ToHtml(this.lblMin.BackColor));
            this.clientConfig.set("pingJunZhiColor", ColorTranslator.ToHtml(this.lblAvg.BackColor));
            this.clientConfig.set("quXianBeiJingColor", ColorTranslator.ToHtml(this.label9.BackColor));
        }
    }
}

