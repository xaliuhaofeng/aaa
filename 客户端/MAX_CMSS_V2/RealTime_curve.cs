namespace MAX_CMSS_V2
{
    using Logic;
    using MyPictureBox;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class RealTime_curve : UserControl
    {
        private Button button1;
        private Button button2;
        private RealTimeCurveControl cc;
        private ClientConfig cConf;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private CheckBox checkBox8;
        private ComboBox comboBox1;
        private IContainer components = null;
        private string currentCeDianBianHao;
        private byte fenZhanHao;
        private GroupBox groupBox1;
        private Label label1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private PrintDialog printDialog1;
        private PrintDocument printDocument1;
        private PrintPreviewDialog printPreviewDialog1;
        private byte tongDaoHao;

        public RealTime_curve(string ceDianBianHao)
        {
            this.InitializeComponent();
            this.cConf = ClientConfig.CreateCommon();
            this.cc = new RealTimeCurveControl(this.panel2.Width, this.panel2.Height);
            this.cc.Dock = DockStyle.Fill;
            this.cc.BackColor = Color.Transparent;
            this.cc.CarveCurveRemove = 10;
            this.cc.CarveCoordinate = 60f;
            this.cc.CarveTitle = "实时曲线数据";
            this.cc.CurveBackGroundColor = ColorTranslator.FromHtml(this.cConf.get("quXianBeiJingColor"));
            this.panel2.Controls.Add(this.cc);
            this.cc.JustifyOldSize();
            this.comboBox1.DataSource = GlobalParams.AllCeDianList.getCeDianAllInfo((byte) 0);
            this.checkBox1.Checked = Convert.ToBoolean(this.cConf.get("shiShiShowBaoJingMenXian"));
            this.checkBox2.Checked = Convert.ToBoolean(this.cConf.get("shiShiShowDuanDianMenXian"));
            this.checkBox3.Checked = Convert.ToBoolean(this.cConf.get("shiShiShowFuDianMenXian"));
            if (ceDianBianHao == null)
            {
                if (this.comboBox1.SelectedIndex != -1)
                {
                    this.currentCeDianBianHao = this.comboBox1.SelectedItem.ToString().Substring(0, 5);
                }
            }
            else if (ceDianBianHao != "")
            {
                this.currentCeDianBianHao = ceDianBianHao;
                for (int i = 0; i < this.comboBox1.Items.Count; i++)
                {
                    if (this.comboBox1.Items[i].ToString().Substring(0, 5) == ceDianBianHao)
                    {
                        this.comboBox1.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex != -1)
            {
                this.currentCeDianBianHao = this.comboBox1.SelectedItem.ToString().Substring(0, 5);
                this.fenZhanHao = Convert.ToByte(this.currentCeDianBianHao.Substring(0, 2));
                this.tongDaoHao = Convert.ToByte(this.currentCeDianBianHao.Substring(3, 2));
                float[] info = CeDian.GetMoNiLiangInfoByCeDiaanBianHao(this.currentCeDianBianHao);
                this.cc.ShowPingJunZhiQuXian = true;
                this.cc.CarveYMaxValue = info[1];
                this.cc.CarveYMinValue = info[0];
                this.cc.CarveYMaxString = "最高量程";
                this.cc.CarveYMinString = "最低量程";
                this.cc.BaoJingMenXianString = "报警门限";
                this.cc.BaoJingMenXian = info[2];
                this.cc.DuanDianMenXianString = "断电门限";
                this.cc.DuanDianMenXian = info[3];
                this.cc.FuDianMenXianString = "复电门限";
                this.cc.FuanDianMenXian = info[4];
                this.cc.ClearCurve();
                this.cc.ShowCurve();
                this.SetDataGridView();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            Graphics.FromImage(bmp).CopyFromScreen(0, 0, 0, 0, new Size(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height));
            new frmScreen(bmp).Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.cc.ShowBaoJingMenXian = this.checkBox1.Checked;
            this.cConf.set("shiShiShowBaoJingMenXian", this.checkBox1.Checked.ToString());
        }

        private void checkBox1_ForeColorChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.ForeColor != ColorTranslator.FromHtml(this.cConf.get("baoJingZhiColor")))
            {
                this.checkBox1.ForeColor = ColorTranslator.FromHtml(this.cConf.get("baoJingZhiColor"));
            }
            this.cc.BaoJingMenXianColor = ColorTranslator.FromHtml(this.cConf.get("baoJingZhiColor"));
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.cc.ShowDuanDianMenXian = this.checkBox2.Checked;
            this.cConf.set("shiShiShowDuanDianMenXian", this.checkBox2.Checked.ToString());
        }

        private void checkBox2_ForeColorChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.ForeColor != ColorTranslator.FromHtml(this.cConf.get("duanDianZhiColor")))
            {
                this.checkBox2.ForeColor = ColorTranslator.FromHtml(this.cConf.get("duanDianZhiColor"));
            }
            this.cc.DuanDianMenXianColor = ColorTranslator.FromHtml(this.cConf.get("duanDianZhiColor"));
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            this.cc.ShowFuDianMenXian = this.checkBox3.Checked;
            this.cConf.set("shiShiShowFuDianMenXian", this.checkBox3.Checked.ToString());
        }

        private void checkBox3_ForeColorChanged(object sender, EventArgs e)
        {
            if (this.checkBox3.ForeColor != ColorTranslator.FromHtml(this.cConf.get("fuDianZhiColor")))
            {
                this.checkBox3.ForeColor = ColorTranslator.FromHtml(this.cConf.get("fuDianZhiColor"));
            }
            this.cc.FuDianMenXianColor = ColorTranslator.FromHtml(this.cConf.get("fuDianZhiColor"));
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            this.cc.ShowBgGrid = this.checkBox8.Checked;
            this.cConf.set("showShiShiBgGrid", this.checkBox8.Checked.ToString());
        }

        public void Click()
        {
            if (this.comboBox1.SelectedIndex != -1)
            {
                this.currentCeDianBianHao = this.comboBox1.SelectedItem.ToString().Substring(0, 5);
                this.fenZhanHao = Convert.ToByte(this.currentCeDianBianHao.Substring(0, 2));
                this.tongDaoHao = Convert.ToByte(this.currentCeDianBianHao.Substring(3, 2));
                float[] info = CeDian.GetMoNiLiangInfoByCeDiaanBianHao(this.currentCeDianBianHao);
                this.cc.ShowPingJunZhiQuXian = true;
                this.cc.CarveYMaxValue = info[1];
                this.cc.CarveYMinValue = info[0];
                this.cc.CarveYMaxString = "最高量程";
                this.cc.CarveYMinString = "最低量程";
                this.cc.BaoJingMenXianString = "报警门限";
                this.cc.BaoJingMenXian = info[2];
                this.cc.DuanDianMenXianString = "断电门限";
                this.cc.DuanDianMenXian = info[3];
                this.cc.FuDianMenXianString = "复电门限";
                this.cc.FuanDianMenXian = info[4];
                this.cc.ShowCurve();
                this.SetDataGridView();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void Dispatch(FenZhanRTdata ud)
        {
            if (this.fenZhanHao == ud.fenZhanHao)
            {
                while (!this.cc.IsHandleCreated)
                {
                }
                this.cc.BeginInvoke(new RefreshCurve(this.refreshCurve), new object[] { ud });
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

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(RealTime_curve));
            this.panel1 = new Panel();
            this.panel4 = new Panel();
            this.button2 = new Button();
            this.label1 = new Label();
            this.button1 = new Button();
            this.comboBox1 = new ComboBox();
            this.panel2 = new Panel();
            this.panel3 = new Panel();
            this.groupBox1 = new GroupBox();
            this.checkBox8 = new CheckBox();
            this.checkBox3 = new CheckBox();
            this.checkBox2 = new CheckBox();
            this.checkBox1 = new CheckBox();
            this.printDocument1 = new PrintDocument();
            this.printDialog1 = new PrintDialog();
            this.printPreviewDialog1 = new PrintPreviewDialog();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x379, 0x27);
            this.panel1.TabIndex = 0;
            this.panel4.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel4.AutoSize = true;
            this.panel4.BackColor = Color.AliceBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.comboBox1);
            this.panel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel4.Location = new Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new Size(0x379, 0x20);
            this.panel4.TabIndex = 0x27;
            this.button2.BackColor = Color.Chocolate;
            this.button2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new Point(0x28e, 2);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x7d, 0x17);
            this.button2.TabIndex = 7;
            this.button2.Text = "打印曲线";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0x23, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x47, 30);
            this.label1.TabIndex = 14;
            this.label1.Text = "测点选择";
            this.label1.TextAlign = ContentAlignment.MiddleLeft;
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(0x1cf, 3);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x7d, 0x17);
            this.button1.TabIndex = 6;
            this.button1.Text = "添加曲线";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.Location = new Point(0x79, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x11c, 20);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
            this.panel2.Location = new Point(0, 0x26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x2de, 480);
            this.panel2.TabIndex = 3;
            this.panel3.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Location = new Point(0x2de, 0x26);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x9b, 480);
            this.panel3.TabIndex = 4;
            this.groupBox1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.checkBox8);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new Point(9, 0x12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x87, 0x175);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "曲线选择";
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new Point(10, 0x91);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new Size(0x6c, 0x10);
            this.checkBox8.TabIndex = 9;
            this.checkBox8.Text = "—显示背景网格";
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.CheckedChanged += new EventHandler(this.checkBox8_CheckedChanged);
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new Point(10, 0x6f);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new Size(0x54, 0x10);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "--复电门限";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.ForeColorChanged += new EventHandler(this.checkBox3_ForeColorChanged);
            this.checkBox3.CheckedChanged += new EventHandler(this.checkBox3_CheckedChanged);
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new Point(10, 0x4e);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new Size(0x54, 0x10);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "--断电门限";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.ForeColorChanged += new EventHandler(this.checkBox2_ForeColorChanged);
            this.checkBox2.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(10, 0x2c);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x54, 0x10);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "--报警门限";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.ForeColorChanged += new EventHandler(this.checkBox1_ForeColorChanged);
            this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            this.printDocument1.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            this.printPreviewDialog1.AutoScrollMargin = new Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
            this.printPreviewDialog1.ClientSize = new Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = (Icon) resources.GetObject("printPreviewDialog1.Icon");
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.panel3);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "RealTime_curve";
            base.Size = new Size(0x379, 0x206);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.CopyFromScreen(base.Left, base.Top, 0, 0, base.Size);
        }

        private void refreshCurve(FenZhanRTdata ud)
        {
            this.cc.AddPingJunZhiNewValue(ud.realValue[this.tongDaoHao], ud.uploadTime);
            this.cc.ShowCurve();
        }

        private void SetDataGridView()
        {
        }

        private delegate void RefreshCurve(FenZhanRTdata ud);
    }
}

