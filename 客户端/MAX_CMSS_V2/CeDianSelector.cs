namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CeDianSelector : UserControl
    {
        private List<string> ceDianBianHaos;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private ComboBox comboBox4;
        private bool combox1Init;
        private bool combox2Init;
        private bool combox3Init;
        private bool combox4Init;
        private IContainer components;
        private ListControl displayList;
        private ListBox displayList2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;

        public CeDianSelector()
        {
            this.components = null;
            this.combox1Init = false;
            this.combox2Init = false;
            this.combox3Init = false;
            this.combox4Init = false;
            this.InitializeComponent();
        }

        public CeDianSelector(int cedianleixing, ListControl display)
        {
            this.components = null;
            this.combox1Init = false;
            this.combox2Init = false;
            this.combox3Init = false;
            this.combox4Init = false;
            this.InitializeComponent();
            if (this.ceDianBianHaos == null)
            {
                this.ceDianBianHaos = new List<string>();
            }
            this.displayList = display;
            this.setCeDianLeiXing(cedianleixing);
        }

        private void CeDianSelector_Load(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.combox1Init)
            {
                this.combox1Init = true;
            }
            else
            {
                this.doSelectedIndexChanged();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.combox2Init)
            {
                this.combox2Init = true;
            }
            else
            {
                string s = this.comboBox2.SelectedItem.ToString();
                this.comboBox3.Items.Clear();
                switch (s)
                {
                    case "全部":
                        this.comboBox3.Items.AddRange(new object[] { "全部", "模拟量频率型", "模拟量电流电压型", "累计量频率型", "二态量", "三态量", "通断量", "分站量" });
                        break;

                    case "模拟量":
                        this.comboBox3.Items.AddRange(new object[] { "全部", "模拟量频率型", "模拟量电流电压型", "累计量频率型" });
                        break;

                    case "开关量":
                        this.comboBox3.Items.AddRange(new object[] { "全部", "二态量", "三态量", "通断量", "分站量" });
                        break;

                    case "控制量":
                        this.comboBox3.Items.AddRange(new object[] { "全部" });
                        break;
                }
                this.combox3Init = false;
                this.comboBox3.SelectedIndex = 0;
                this.doSelectedIndexChanged();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.combox3Init)
            {
                this.combox3Init = true;
            }
            else
            {
                this.doSelectedIndexChanged();
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.combox4Init)
            {
                this.combox4Init = true;
            }
            else
            {
                this.doSelectedIndexChanged();
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

        private void doSelectedIndexChanged()
        {
            int fenZhanHao = this.comboBox1.SelectedIndex;
            if (fenZhanHao > 0)
            {
                fenZhanHao = Convert.ToInt32(this.comboBox1.SelectedItem);
            }
            int ceDianLeiXing = 3;
            string s1 = this.comboBox2.SelectedItem.ToString();
            int tongDaoLeiXing = -1;
            string s2 = this.comboBox3.SelectedItem.ToString();
            string xiaoLeiXing = this.comboBox4.SelectedItem.ToString();
            switch (s1)
            {
                case "模拟量":
                    ceDianLeiXing = 0;
                    break;

                case "开关量":
                    ceDianLeiXing = 1;
                    break;

                case "控制量":
                    ceDianLeiXing = 2;
                    break;
            }
            if (s2 == "模拟量频率型")
            {
                tongDaoLeiXing = 5;
            }
            else if (s2 == "模拟量电流电压型")
            {
                tongDaoLeiXing = 1;
            }
            else if (s2 == "累计量频率型")
            {
                tongDaoLeiXing = 2;
            }
            else if (s2 == "二态量")
            {
                tongDaoLeiXing = 3;
            }
            else if (s2 == "三态量")
            {
                tongDaoLeiXing = 4;
            }
            else if (s2 == "通断量")
            {
                tongDaoLeiXing = 7;
            }
            else if (s2 == "分站量")
            {
                tongDaoLeiXing = 8;
            }
            this.ceDianBianHaos = GlobalParams.AllCeDianList.getCeDianAllInfo(fenZhanHao, ceDianLeiXing, tongDaoLeiXing, xiaoLeiXing);
            this.setDataSource(this.ceDianBianHaos);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.comboBox1 = new ComboBox();
            this.comboBox2 = new ComboBox();
            this.comboBox3 = new ComboBox();
            this.label4 = new Label();
            this.comboBox4 = new ComboBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x34, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "分站号";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new Point(0xb8, 10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x43, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "测点类型";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new Point(0x17b, 11);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x43, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "通道类型";
            this.comboBox1.BackColor = Color.White;
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(0x36, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x79, 20);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new Point(0xfc, 7);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(0x79, 20);
            this.comboBox2.TabIndex = 7;
            this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new Point(0x1be, 7);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new Size(0x79, 20);
            this.comboBox3.TabIndex = 8;
            this.comboBox3.SelectedIndexChanged += new EventHandler(this.comboBox3_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new Point(0x23e, 11);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x25, 14);
            this.label4.TabIndex = 9;
            this.label4.Text = "类型";
            this.comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new Point(0x263, 8);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new Size(0x79, 20);
            this.comboBox4.TabIndex = 10;
            this.comboBox4.SelectedIndexChanged += new EventHandler(this.comboBox4_SelectedIndexChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.comboBox4);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.comboBox3);
            base.Controls.Add(this.comboBox2);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "CeDianSelector";
            base.Size = new Size(0x2e5, 0x21);
            base.Load += new EventHandler(this.CeDianSelector_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void setCeDianLeiXing(int cedianleixing)
        {
            string[] cedians;
            if (this.ceDianBianHaos == null)
            {
                this.ceDianBianHaos = new List<string>();
            }
            this.comboBox1.Items.Add("全部");
            string[] fenZhan = FenZhan.GetAllConfigedFenZhan2();
            this.comboBox1.Items.AddRange(fenZhan);
            if (cedianleixing == 0)
            {
                this.comboBox2.Items.AddRange(new string[] { "模拟量" });
                this.comboBox3.Items.AddRange(new string[] { "全部", "模拟量频率型", "模拟量电流电压型", "累计量频率型" });
                this.comboBox4.Items.Add("全部");
                this.comboBox4.Items.AddRange(MoNiLiangLeiXing.GetAllMingCheng());
                cedians = GlobalParams.AllCeDianList.getCeDianAllInfo((byte) 0);
            }
            else if (cedianleixing == 1)
            {
                this.comboBox2.Items.AddRange(new string[] { "开关量" });
                this.comboBox3.Items.AddRange(new string[] { "全部", "二态量", "三态量", "通断量", "分站量" });
                this.comboBox4.Items.Add("全部");
                this.comboBox4.Items.AddRange(GlobalParams.AllkgLeiXing.GetAllMingCheng());
                cedians = GlobalParams.AllCeDianList.getCeDianAllInfo((byte) 1);
            }
            else if (cedianleixing == 2)
            {
                this.comboBox2.Items.AddRange(new string[] { "控制量" });
                this.comboBox3.Items.AddRange(new string[] { "全部" });
                this.comboBox4.Items.Add("全部");
                this.comboBox4.Items.AddRange(GlobalParams.AllkzlLeiXing.GetAllMingCheng());
                cedians = GlobalParams.AllCeDianList.getCeDianAllInfo((byte) 2);
            }
            else
            {
                this.comboBox2.Items.AddRange(new string[] { "全部", "模拟量", "开关量", "控制量" });
                this.comboBox3.Items.AddRange(new object[] { "全部", "模拟量频率型", "模拟量电流电压型", "累计量频率型", "二态量", "三态量", "通断量", "分站量" });
                this.comboBox4.Items.Add("全部");
                this.comboBox4.Items.AddRange(MoNiLiangLeiXing.GetAllMingCheng());
                this.comboBox4.Items.AddRange(GlobalParams.AllkgLeiXing.GetAllMingCheng());
                this.comboBox4.Items.AddRange(GlobalParams.AllkzlLeiXing.GetAllMingCheng());
                cedians = GlobalParams.AllCeDianList.getCeDianAllInfo((byte) 3);
            }
            foreach (string s in cedians)
            {
                this.ceDianBianHaos.Add(s);
            }
            this.setDataSource(this.ceDianBianHaos);
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;
            this.comboBox3.SelectedIndex = 0;
            this.comboBox4.SelectedIndex = 0;
        }

        private void setDataSource(List<string> list)
        {
            if (this.displayList != null)
            {
                if (this.displayList is ListBox)
                {
                    ListBox a = (ListBox) this.displayList;
                    a.Items.Clear();
                    foreach (string s in this.ceDianBianHaos)
                    {
                        a.Items.Add(s);
                    }
                }
                else
                {
                    ComboBox a = (ComboBox) this.displayList;
                    a.Items.Clear();
                    foreach (string s in this.ceDianBianHaos)
                    {
                        a.Items.Add(s);
                    }
                }
                this.displayList.SelectedIndex = -1;
            }
        }

        public List<string> CeDianBianHaos
        {
            get
            {
                return this.ceDianBianHaos;
            }
            set
            {
                this.ceDianBianHaos = value;
            }
        }

        public ListControl DisplayList
        {
            get
            {
                return this.displayList;
            }
            set
            {
                this.displayList = value;
            }
        }

        public ListBox DisplayList2
        {
            get
            {
                return this.displayList2;
            }
            set
            {
                this.displayList2 = value;
            }
        }
    }
}

