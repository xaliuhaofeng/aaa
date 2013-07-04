namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class History_data : Form
    {
        private Button button1;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private IContainer components;
        private DateTimeChooser dateTimeChooser1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private int init_bianhao;
        private ListBox listBox1;
        private ListBox listBox2;
        private ListBox listBox3;
        private int pages;
        private int pageSize;
        private Panel panel_cut;
        private Panel panel_feed;
        private Panel panel_test;
        private Panel panel1;
        private PrintDialog printDialog1;
        private PrintDocument printDocument1;
        private Button prt_1;
        private Button prt_2;
        private Button prt_3;
        private ComboBox prt_cbx;
        private ListBox prt_lbx;
        private string type;

        public History_data(string s)
        {
            this.init_bianhao = 0;
            this.pages = 0;
            this.pageSize = 0xc9;
            this.prt_lbx = null;
            this.prt_cbx = null;
            this.type = "";
            this.components = null;
            this.InitializeComponent();
            this.dateTimeChooser1.StartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
            this.dateTimeChooser1.EndTime = DateTime.Now;
            this.init_bianhao = 0;
            this.initListBox(s);
        }

        public History_data(string s, DateTime start, DateTime end)
        {
            this.init_bianhao = 0;
            this.pages = 0;
            this.pageSize = 0xc9;
            this.prt_lbx = null;
            this.prt_cbx = null;
            this.type = "";
            this.components = null;
            this.InitializeComponent();
            this.dateTimeChooser1.StartTime = start;
            this.dateTimeChooser1.EndTime = end;
            this.init_bianhao = 0;
            this.initListBox(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dateTimeChooser1.StartTime >= this.dateTimeChooser1.EndTime)
            {
                MessageBox.Show("开始时间大于结束时间，请重新选择！");
            }
            else
            {
                if (this.comboBox1.SelectedIndex > -1)
                {
                    this.listBox1.Items.Clear();
                    this.listBox1.Items.AddRange(this.updateList(this.comboBox1.SelectedItem.ToString()));
                }
                if (this.comboBox3.SelectedIndex > -1)
                {
                    this.listBox3.Items.Clear();
                    this.listBox3.Items.AddRange(this.updateList(this.comboBox3.SelectedItem.ToString()));
                }
                if (this.comboBox2.SelectedIndex > -1)
                {
                    this.listBox2.Items.Clear();
                    this.listBox2.Items.AddRange(this.updateList(this.comboBox2.SelectedItem.ToString()));
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pages = 0;
                this.prt_lbx = this.listBox1;
                this.prt_cbx = this.comboBox1;
                this.type = "测点";
                try
                {
                    this.printDocument1.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "选中的打印机不是计算机默认的打印机");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.init_bianhao == 1)
            {
                string s = this.comboBox1.SelectedItem.ToString().Substring(0, 5);
                string[] ss = DuanDianGuanXi.getDuanDianCeDianBianHao(s);
                this.comboBox2.Items.Clear();
                this.comboBox2.Items.AddRange(ss);
                this.comboBox2.SelectedIndex = -1;
                string[] kuidian = KuiDianGuanXi.getKuiDianCeDianBianHao(s);
                this.comboBox3.Items.Clear();
                this.comboBox3.Items.AddRange(kuidian);
                this.comboBox3.SelectedIndex = -1;
                this.listBox2.Items.Clear();
                this.listBox3.Items.Clear();
            }
            else
            {
                this.init_bianhao = 1;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox2.SelectedIndex > -1)
            {
                this.listBox2.Items.Clear();
                this.listBox2.Items.AddRange(this.updateList(this.comboBox2.SelectedItem.ToString()));
                string s = this.comboBox2.SelectedItem.ToString();
                if (s[2] == 'C')
                {
                    this.comboBox3.Items.Clear();
                    this.listBox3.Items.Clear();
                    string[] kuidian = KuiDianGuanXi.getKuiDianCeDianBianHao(s.Substring(0, 5));
                    this.comboBox3.Items.AddRange(kuidian);
                    this.comboBox3.SelectedIndex = -1;
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox3.SelectedIndex > -1)
            {
                this.listBox3.Items.Clear();
                this.listBox3.Items.AddRange(this.updateList(this.comboBox3.SelectedItem.ToString()));
                string s = this.comboBox3.SelectedItem.ToString();
                if (s[2] == 'C')
                {
                    this.comboBox2.Items.Clear();
                    this.listBox2.Items.Clear();
                    string[] ss = DuanDianGuanXi.getDuanDianCeDianBianHao(s.Substring(0, 5));
                    this.comboBox2.Items.AddRange(ss);
                    this.comboBox2.SelectedIndex = -1;
                }
            }
        }

        private string controlValue(string ceDian, string value)
        {
            DataTable dt = KongZhiLiang.GetKongAlarm(ceDian);
            if (value == "0")
            {
                return dt.Rows[0]["lingTaiMingCheng"].ToString();
            }
            if (value == "1")
            {
                return dt.Rows[0]["yiTaiMingCheng"].ToString();
            }
            return "其它";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex > -1)
            {
                this.listBox1.Items.AddRange(this.updateList(this.comboBox1.SelectedItem.ToString()));
            }
            if (this.comboBox3.SelectedIndex > -1)
            {
                this.listBox3.Items.AddRange(this.updateList(this.comboBox3.SelectedItem.ToString()));
            }
            if (this.comboBox2.SelectedIndex > -1)
            {
                this.listBox2.Items.AddRange(this.updateList(this.comboBox2.SelectedItem.ToString()));
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex > -1)
            {
                this.listBox1.Items.AddRange(this.updateList(this.comboBox1.SelectedItem.ToString()));
            }
            if (this.comboBox3.SelectedIndex > -1)
            {
                this.listBox3.Items.AddRange(this.updateList(this.comboBox3.SelectedItem.ToString()));
            }
            if (this.comboBox2.SelectedIndex > -1)
            {
                this.listBox2.Items.AddRange(this.updateList(this.comboBox2.SelectedItem.ToString()));
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
            this.panel1 = new Panel();
            this.button1 = new Button();
            this.panel_test = new Panel();
            this.groupBox1 = new GroupBox();
            this.comboBox1 = new ComboBox();
            this.listBox1 = new ListBox();
            this.panel_cut = new Panel();
            this.groupBox2 = new GroupBox();
            this.comboBox2 = new ComboBox();
            this.listBox2 = new ListBox();
            this.panel_feed = new Panel();
            this.groupBox3 = new GroupBox();
            this.comboBox3 = new ComboBox();
            this.listBox3 = new ListBox();
            this.prt_1 = new Button();
            this.prt_2 = new Button();
            this.prt_3 = new Button();
            this.printDocument1 = new PrintDocument();
            this.printDialog1 = new PrintDialog();
            this.dateTimeChooser1 = new DateTimeChooser();
            this.panel1.SuspendLayout();
            this.panel_test.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel_cut.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel_feed.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink; 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.dateTimeChooser1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x3cd, 0x4e);
            this.panel1.TabIndex = 0;
            this.button1.Location = new Point(12, 0x34);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x1a);
            this.button1.TabIndex = 3;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.panel_test.Controls.Add(this.groupBox1);
            this.panel_test.Dock = DockStyle.Left;
            this.panel_test.Location = new Point(0, 0x4e);
            this.panel_test.Name = "panel_test";
            this.panel_test.Size = new Size(320, 0x178);
            this.panel_test.TabIndex = 1;
            this.groupBox1.Controls.Add(this.prt_1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.groupBox1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox1.Location = new Point(15, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x12b, 0x161);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测点";
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(8, 0x24);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x11d, 0x19);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.listBox1.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new Point(8, 80);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0x11d, 0xf4);
            this.listBox1.TabIndex = 0;
            this.panel_cut.Controls.Add(this.groupBox2);
            this.panel_cut.Dock = DockStyle.Left;
            this.panel_cut.Location = new Point(320, 0x4e);
            this.panel_cut.Name = "panel_cut";
            this.panel_cut.Size = new Size(320, 0x178);
            this.panel_cut.TabIndex = 2;
            this.groupBox2.Controls.Add(this.prt_2);
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.listBox2);
            this.groupBox2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.groupBox2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox2.Location = new Point(0x11, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x129, 0x161);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "断电关系";
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new Point(7, 0x24);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(0x11c, 0x19);
            this.comboBox2.TabIndex = 3;
            this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.listBox2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.ItemHeight = 15;
            this.listBox2.Location = new Point(7, 80);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new Size(0x11c, 0xf4);
            this.listBox2.TabIndex = 2;
            this.panel_feed.Controls.Add(this.groupBox3);
            this.panel_feed.Dock = DockStyle.Left;
            this.panel_feed.Location = new Point(640, 0x4e);
            this.panel_feed.Name = "panel_feed";
            this.panel_feed.Size = new Size(320, 0x178);
            this.panel_feed.TabIndex = 2;
            this.groupBox3.Controls.Add(this.prt_3);
            this.groupBox3.Controls.Add(this.comboBox3);
            this.groupBox3.Controls.Add(this.listBox3);
            this.groupBox3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.groupBox3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox3.Location = new Point(20, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x129, 0x161);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "馈电关系";
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.ItemHeight = 0x11;
            this.comboBox3.Location = new Point(7, 0x24);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new Size(0x11c, 0x19);
            this.comboBox3.TabIndex = 5;
            this.comboBox3.SelectedIndexChanged += new EventHandler(this.comboBox3_SelectedIndexChanged);
            this.listBox3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listBox3.FormattingEnabled = true;
            this.listBox3.HorizontalScrollbar = true;
            this.listBox3.ItemHeight = 15;
            this.listBox3.Location = new Point(7, 80);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new Size(0x11c, 0xf4);
            this.listBox3.TabIndex = 4;
            this.prt_1.Location = new Point(0xda, 0x145);
            this.prt_1.Name = "prt_1";
            this.prt_1.Size = new Size(0x4b, 0x19);
            this.prt_1.TabIndex = 4;
            this.prt_1.Text = "打印";
            this.prt_1.UseVisualStyleBackColor = true;
            this.prt_1.Click += new EventHandler(this.button2_Click);
            this.prt_2.Location = new Point(0xd8, 0x145);
            this.prt_2.Name = "prt_2";
            this.prt_2.Size = new Size(0x4b, 0x19);
            this.prt_2.TabIndex = 4;
            this.prt_2.Text = "打印";
            this.prt_2.UseVisualStyleBackColor = true;
            this.prt_2.Click += new EventHandler(this.prt_2_Click);
            this.prt_3.Location = new Point(0xd8, 0x145);
            this.prt_3.Name = "prt_3";
            this.prt_3.Size = new Size(0x4b, 0x19);
            this.prt_3.TabIndex = 4;
            this.prt_3.Text = "打印";
            this.prt_3.UseVisualStyleBackColor = true;
            this.prt_3.Click += new EventHandler(this.prt_3_Click);
            this.printDocument1.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            this.dateTimeChooser1.EndTime = new DateTime(0x7dc, 2, 0x18, 10, 20, 0, 0);
            this.dateTimeChooser1.Location = new Point(6, 12);
            this.dateTimeChooser1.Margin = new Padding(4);
            this.dateTimeChooser1.Name = "dateTimeChooser1";
            this.dateTimeChooser1.Size = new Size(0x3c6, 0x25);
            this.dateTimeChooser1.StartTime = new DateTime(0x7dc, 2, 0x18, 0, 0, 0, 0);
            this.dateTimeChooser1.TabIndex = 2;
            base.AutoScaleDimensions = new SizeF(9f, 17f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink; 
            base.ClientSize = new Size(0x3cd, 0x1c6);
            base.Controls.Add(this.panel_feed);
            base.Controls.Add(this.panel_cut);
            base.Controls.Add(this.panel_test);
            base.Controls.Add(this.panel1);
            this.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.ForeColor = System.Drawing.Color.Orange;
            base.Name = "History_data";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "历史数据";
            this.panel1.ResumeLayout(false);
            this.panel_test.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel_cut.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel_feed.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void initListBox(string s)
        {
            this.panel_feed.SendToBack();
            this.panel_cut.SendToBack();
            this.panel_test.SendToBack();
            if (GlobalParams.AllCeDianList.allcedianlist.ContainsKey(s))
            {
                CeDian cedian = GlobalParams.AllCeDianList.allcedianlist[s];
                foreach (CeDian cd in GlobalParams.AllCeDianList.allcedianlist.Values)
                {
                    if (cedian.DaLeiXing == cedian.DaLeiXing)
                    {
                        string info = cd.CeDianBianHao + " " + cd.CeDianWeiZhi + " " + cd.XiaoleiXing;
                        this.comboBox1.Items.Add(info);
                        if (cedian.CeDianBianHao == cd.CeDianBianHao)
                        {
                            this.comboBox1.SelectedItem = info;
                        }
                    }
                }
                this.listBox1.Items.AddRange(this.updateList(this.comboBox1.SelectedItem.ToString()));
                string[] ss = DuanDianGuanXi.getDuanDianCeDianBianHao(s);
                this.comboBox2.Items.AddRange(ss);
                this.comboBox2.SelectedIndex = -1;
                string[] kuidian = KuiDianGuanXi.getKuiDianCeDianBianHao(s);
                this.comboBox3.Items.AddRange(kuidian);
                this.comboBox3.SelectedIndex = -1;
                this.panel1.SendToBack();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                Font font = new Font("宋体", 10f, FontStyle.Regular);
                Brush bru = Brushes.Black;
                Pen pen = new Pen(bru) {
                    Width = 0f
                };
                int nLeft = 0;
                int nTop = 0;
                int nRight = 0;
                int nBottom = 0;
                int nWidth = (200 - nRight) - nLeft;
                int nHeight = (200 - nTop) - nBottom;
                e.Graphics.DrawString(string.Concat(new object[] { "历史数据，", this.type, "：", this.prt_cbx.SelectedItem, ",时间：", this.dateTimeChooser1.StartTime.ToString("yyyy年MM月dd日 HH时mm分"), " 至 ", this.dateTimeChooser1.EndTime.ToString("yyyy年MM月dd日 HH时mm分") }), font, bru, (float) 5f, (float) 5f);
                e.Graphics.DrawString("第" + (this.pages + 1) + "页", font, bru, (float) 400f, (float) 1130f);
                int n = this.pages * this.pageSize;
                int count = this.pageSize;
                if (this.prt_lbx.Items.Count < this.pageSize)
                {
                    count = this.prt_lbx.Items.Count;
                }
                else if ((n + this.pageSize) > this.prt_lbx.Items.Count)
                {
                    count = this.prt_lbx.Items.Count;
                }
                else
                {
                    count = n + this.pageSize;
                }
                string content = "";
                for (int i = n; i < count; i++)
                {
                    string item = this.prt_lbx.Items[i].ToString();
                    if ((i != 0) && ((i % 3) == 0))
                    {
                        content = content + "\n";
                    }
                    else if (i > 0)
                    {
                        content = content + "        ";
                    }
                    content = content + item;
                }
                e.Graphics.DrawString(content, font, bru, (float) (nLeft + 10), (float) (nTop + 30));
                this.pages++;
                if ((this.pages == 1) && (this.prt_lbx.Items.Count > (this.pages * this.pageSize)))
                {
                    e.HasMorePages = true;
                }
                else if ((this.prt_lbx.Items.Count > 30) && (this.prt_lbx.Items.Count > (this.pages * this.pageSize)))
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("打印出现错误，请检查是否安装打印机。");
            }
        }

        private void prt_2_Click(object sender, EventArgs e)
        {
            if (this.printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pages = 0;
                this.prt_lbx = this.listBox2;
                this.prt_cbx = this.comboBox2;
                this.type = "断电关系";
                try
                {
                    this.printDocument1.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "选中的打印机不是计算机默认的打印机");
                }
            }
        }

        private void prt_3_Click(object sender, EventArgs e)
        {
            if (this.printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pages = 0;
                this.prt_lbx = this.listBox3;
                this.prt_cbx = this.comboBox3;
                this.type = "馈电关系";
                try
                {
                    this.printDocument1.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "选中的打印机不是计算机默认的打印机");
                }
            }
        }

        private string switchValue(string ceDian, string value)
        {
            if (ceDian.Substring(2, 1).ToUpper() == "F")
            {
                string Reflector0002 = value;
                if ((Reflector0002 != null) && (Reflector0002 == "0"))
                {
                    return "交流";
                }
                return "直流";
            }
            KaiGuanLiangLeiXing kgl = GlobalParams.AllkgLeiXing.GetSwitchAlarm(ceDian);
            switch (value)
            {
                case "0":
                    return kgl.LingTai.Trim();

                case "1":
                    return kgl.YiTai.Trim();

                case "2":
                    return kgl.ErTai.Trim();
            }
            return "其它";
        }

        private string[] updateList(string ss)
        {
            int state;
            string s = ss.Substring(0, 5);
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            List<string> list = new List<string>();
            if (("D" == s.Substring(2, 1)) || ("F" == s.Substring(2, 1)))
            {
                ReportDataSuply.doKaiGuanLiangSelect(this.dateTimeChooser1.StartTime, this.dateTimeChooser1.EndTime, ref dt1, ref dt2, s);
                if (dt1 != null)
                {
                    foreach (DataRow row in dt1.Rows)
                    {
                        state = Convert.ToInt32(row["state"]);
                        if (state == 7)
                        {
                            list.Add("故障 " + row["uploadTime"].ToString() + " 故障");
                        }
                        else
                        {
                            string ss2 = state.ToString();
                            if ((state >= 0) && (state < GlobalParams.state.Length))
                            {
                                ss2 = GlobalParams.state[state];
                            }
                            list.Add(this.switchValue(s, row["uploadValue"].ToString()).PadRight(5) + " " + row["uploadTime"].ToString() + " " + ss2);
                        }
                    }
                }
            }
            else if ("A" == s.Substring(2, 1))
            {
                ReportDataSuply.doMoNiLiangSelect(this.dateTimeChooser1.StartTime, this.dateTimeChooser1.EndTime, ref dt1, ref dt2, s);
                if (dt1 != null)
                {
                    foreach (DataRow row in dt1.Rows)
                    {
                        state = Convert.ToInt32(row["state"]);
                        string ss1 = state.ToString();
                        if ((state < GlobalParams.state.Length) && (state >= 0))
                        {
                            ss1 = GlobalParams.state[state];
                        }
                        list.Add(row["uploadValue"].ToString().PadRight(8) + " " + row["uploadTime"].ToString() + " " + ss1);
                    }
                }
            }
            else if ("C" == s.Substring(2, 1))
            {
                string fenZhanHao = string.Empty;
                if (s.Substring(0, 1) == "0")
                {
                    fenZhanHao = s.Substring(1, 1);
                }
                else
                {
                    fenZhanHao = s.Substring(0, 2);
                }
                dt1 = OperateDB.Select(string.Concat(new object[] { "select * from KongZhiLiangValue where ceDianBianHao = ", s.Substring(4, 1), " and fenZhanHao= ", fenZhanHao, " and uploadTime between '", this.dateTimeChooser1.StartTime, "' and '", this.dateTimeChooser1.EndTime, "' order by ceDianBianHao, uploadTime" }));
                if (dt1 != null)
                {
                    foreach (DataRow row in dt1.Rows)
                    {
                        if ((row["state"])=="7")
                        {
                            list.Add("故障 " + row["uploadTime"].ToString());
                        }
                        else
                        {
                            list.Add(this.controlValue(s, row["uploadValue"].ToString()).PadRight(5) + " " + row["uploadTime"].ToString());
                        }
                    }
                }
            }
            return list.ToArray();
        }
    }
}

