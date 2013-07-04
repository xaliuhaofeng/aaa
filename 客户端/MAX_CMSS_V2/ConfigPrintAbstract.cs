namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ConfigPrintAbstract : UserControl
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private CeDianSelectorListBox ceDianSelectorListBox1;
        public CheckBox checkbox1;
        public CheckBox checkbox10;
        public CheckBox checkbox11;
        public CheckBox checkbox12;
        public CheckBox checkbox13;
        public CheckBox checkbox14;
        public CheckBox checkbox15;
        public CheckBox checkbox2;
        public CheckBox checkbox3;
        public CheckBox checkbox4;
        public CheckBox checkbox5;
        public CheckBox checkbox6;
        public CheckBox checkbox7;
        public CheckBox checkbox8;
        public CheckBox checkbox9;
        public CheckBox[] checkboxGroup;
        private IContainer components = null;
        public FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private ReportType type;

        public ConfigPrintAbstract()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportConfig config = new ReportConfig {
                ReportName = (int) this.type,
                ReportHeader = this.textBox1.Text,
                QianMing = this.textBox2.Text
            };
            ReportConfig.InsertOrUpdateHeader(config);
            MessageBox.Show("表头和签名保存成功！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReportConfig config = new ReportConfig {
                ReportName = (int) this.type
            };
            List<bool> list = new List<bool>();
            foreach (CheckBox box in this.checkboxGroup)
            {
                list.Add(box.Checked);
            }
            config.Cols = list;
            ReportConfig.InsertOrUpdateCols(config);
            MessageBox.Show("输出列保存成功！");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReportConfig config = new ReportConfig {
                ReportName = (int) this.type,
                CeDians = this.ColumnSelector.getSelectedCeDian()
            };
            ReportConfig.InsertOrUpdateCeDian(config);
            MessageBox.Show("输出测点保存成功！");
        }

        private void ConfigPrintAbstract_Load(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public List<bool> getColumnValue()
        {
            List<bool> colValue = new List<bool>();
            bool needInit = true;
            if (ReportConfig.isReportExist((int) this.type))
            {
                colValue = ReportConfig.getConfigByName((int) this.type).Cols;
                if (colValue.Count > 0)
                {
                    needInit = false;
                }
            }
            if (needInit)
            {
                for (int i = 0; i < this.checkboxGroup.Length; i++)
                {
                    colValue.Add(true);
                }
            }
            return colValue;
        }

        public void initialCheckBox(string[] cols, List<bool> value)
        {
            for (int i = 0; i < this.checkboxGroup.Length; i++)
            {
                this.checkboxGroup[i] = new CheckBox();
                this.checkboxGroup[i].Width = 120;
                if (cols[i].Length > 12)
                {
                    this.checkboxGroup[i].Width = 150;
                }
                this.checkboxGroup[i].Height = 40;
                this.checkboxGroup[i].Text = cols[i];
                this.checkboxGroup[i].Checked = value[i];
                this.flowLayoutPanel1.Controls.Add(this.checkboxGroup[i]);
            }
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.button1 = new Button();
            this.textBox2 = new TextBox();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.button2 = new Button();
            this.flowLayoutPanel1 = new FlowLayoutPanel();
            this.groupBox3 = new GroupBox();
            this.ceDianSelectorListBox1 = new CeDianSelectorListBox();
            this.button3 = new Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.groupBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x3df, 0x44);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "表头表尾设置";
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(0x37c, 0x29);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 4;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.textBox2.Location = new Point(0x218, 0x12);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0x1b4, 0x17);
            this.textBox2.TabIndex = 3;
            this.textBox2.TextChanged += new EventHandler(this.textBox2_TextChanged);
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new Point(0x1cf, 0x15);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x43, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "页脚签名";
            this.textBox1.Location = new Point(60, 0x11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x15d, 0x17);
            this.textBox1.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0x11, 0x15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x25, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "表头";
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.flowLayoutPanel1);
            this.groupBox2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.groupBox2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox2.Location = new Point(8, 0x4e);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x3df, 0x8e);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输出列控制";
            this.button2.BackColor = Color.Chocolate;
            this.button2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new Point(0x37c, 0x74);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 1;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.flowLayoutPanel1.Location = new Point(6, 20);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new Size(0x3d3, 0x5f);
            this.flowLayoutPanel1.TabIndex = 0;
            this.groupBox3.Controls.Add(this.ceDianSelectorListBox1);
            this.groupBox3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.groupBox3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox3.Location = new Point(8, 0xe2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x418, 0x14e);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "测点输出控制";
            this.ceDianSelectorListBox1.Location = new Point(8, 0x16);
            this.ceDianSelectorListBox1.Margin = new Padding(4, 3, 4, 3);
            this.ceDianSelectorListBox1.Name = "ceDianSelectorListBox1";
            this.ceDianSelectorListBox1.Size = new Size(0x409, 0x134);
            this.ceDianSelectorListBox1.TabIndex = 0;
            this.button3.BackColor = Color.Chocolate;
            this.button3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new Point(900, 0x235);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 3;
            this.button3.Text = "保存";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new EventHandler(this.button3_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.button3);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "ConfigPrintAbstract";
            base.Size = new Size(0x423, 0x252);
            base.Load += new EventHandler(this.ConfigPrintAbstract_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void SetReportType(ReportType type)
        {
            this.type = type;
            if ((((type == ReportType.MoNiLiangBaoJing) || (type == ReportType.MoNiLiangDuanDian)) || ((type == ReportType.MoNiLiangKuDian) || (type == ReportType.MoNiLiangRiBaoBiao))) || (type == ReportType.MoNiLiangTongJi))
            {
                this.ceDianSelectorListBox1.Selector.setCeDianLeiXing(0);
            }
            else if (((type == ReportType.KaiGuanLiangBaoJing) || (type == ReportType.KaiGuanLiangKuiDian)) || (type == ReportType.KaiGuanLiangState))
            {
                this.ceDianSelectorListBox1.Selector.setCeDianLeiXing(1);
            }
            else
            {
                this.ceDianSelectorListBox1.Selector.setCeDianLeiXing(3);
            }
            if (ReportConfig.isReportExist((int) type))
            {
                ReportConfig config = ReportConfig.getConfigByName((int) type);
                this.textBox1.Text = config.ReportHeader;
                this.textBox2.Text = config.QianMing;
                List<string> list2 = new List<string>();
                foreach (string s in config.CeDians)
                {
                    string cedianbianhao = s.Substring(0, 5);
                    if (GlobalParams.AllCeDianList.allcedianlist.ContainsKey(cedianbianhao))
                    {
                        CeDian cedian = GlobalParams.AllCeDianList.allcedianlist[cedianbianhao];
                        string info = cedianbianhao + " " + cedian.CeDianWeiZhi;
                        if (cedian.MoNiLiang != null)
                        {
                            string Reflector0003 = info;
                            info = Reflector0003 + " " + cedian.MoNiLiang.GuanJianZi + "-" + cedian.MoNiLiang.MingCheng;
                        }
                        else if (cedian.KaiGuanLiang != null)
                        {
                            info = info + " " + cedian.KaiGuanLiang.MingCheng;
                        }
                        list2.Add(info);
                    }
                }
                this.ceDianSelectorListBox1.ColSelector.initListBox(list2.ToArray(), 2);
                string[] selectedCeDian = list2.ToArray();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        public ColSelect2 ColumnSelector
        {
            get
            {
                return this.ceDianSelectorListBox1.ColSelector;
            }
        }
    }
}

