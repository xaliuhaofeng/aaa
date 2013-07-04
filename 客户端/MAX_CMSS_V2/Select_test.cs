namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;

    public class Select_test : DockContentEx
    {
        private Button button1;
        private CeDianSelectorListBox2 ceDianSelectorListBox1;
        private IContainer components = null;
        private List_show father;
        private bool init = true;
        private Label label1;
        private Label label2;
        private int lieBiaoKuangBianHao;
        private MainForm mainForm;
        private string yeKuangMingCheng;
        private ComboBox yekuangname;
        private ComboBox yekuangnum;

        public Select_test(string yeKuangMingCheng, int lieBiaoKuangBianHao, List_show father, MainForm main)
        {
            this.InitializeComponent();
            string s = "select * from YeKuang";
            DataTable dt = OperateDBAccess.Select(s);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.yekuangname.Items.Add(dt.Rows[i]["name"].ToString());
                }
            }
            this.yeKuangMingCheng = yeKuangMingCheng;
            this.lieBiaoKuangBianHao = lieBiaoKuangBianHao;
            this.father = father;
            this.mainForm = main;
            this.ceDianSelectorListBox1.YeKuangMingCheng = yeKuangMingCheng;
            int index = this.yekuangname.Items.IndexOf(yeKuangMingCheng);
            this.yekuangname.SelectedIndex = index;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.UpdateDBInfo();
            if (!base.IsDisposed)
            {
                base.Dispose();
            }
        }

        private void ceDianSelectorListBox1_Load(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lieBiaoKuangBianHao = this.yekuangnum.SelectedIndex + 1;
            this.ceDianSelectorListBox1.Selector.DisplayList2.Items.Clear();
            this.initListBox2();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.yeKuangMingCheng = this.yekuangname.SelectedItem.ToString();
            this.ceDianSelectorListBox1.YeKuangMingCheng = this.yeKuangMingCheng;
            this.father = this.mainForm.yeKuangs[this.yekuangname.SelectedIndex];
            this.ceDianSelectorListBox1.Selector.DisplayList2.Items.Clear();
            this.initListBox2();
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
            this.label1 = new Label();
            this.ceDianSelectorListBox1 = new CeDianSelectorListBox2();
            this.button1 = new Button();
            this.label2 = new Label();
            this.yekuangnum = new ComboBox();
            this.yekuangname = new ComboBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(150, 0x27);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x43, 14);
            this.label1.TabIndex = 30;
            this.label1.Text = "页框名称";
            this.ceDianSelectorListBox1.Location = new Point(12, 0x66);
            this.ceDianSelectorListBox1.Margin = new Padding(4);
            this.ceDianSelectorListBox1.Name = "ceDianSelectorListBox1";
            this.ceDianSelectorListBox1.Size = new Size(0x41a, 0x180);
            this.ceDianSelectorListBox1.TabIndex = 0x20;
            this.ceDianSelectorListBox1.YeKuangMingCheng = null;
            this.ceDianSelectorListBox1.Load += new EventHandler(this.ceDianSelectorListBox1_Load);
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(0x21f, 0x24);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0x21;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click_1);
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new Point(0x99, 0x49);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x34, 14);
            this.label2.TabIndex = 0x22;
            this.label2.Text = "页框号";
            this.yekuangnum.DropDownStyle = ComboBoxStyle.DropDownList;
            this.yekuangnum.FormattingEnabled = true;
            this.yekuangnum.Items.AddRange(new object[] { "1", "2", "3" });
            this.yekuangnum.Location = new Point(0xe3, 70);
            this.yekuangnum.Name = "yekuangnum";
            this.yekuangnum.Size = new Size(0x79, 20);
            this.yekuangnum.TabIndex = 0x23;
            this.yekuangnum.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.yekuangname.FormattingEnabled = true;
            this.yekuangname.Location = new Point(0xe3, 0x26);
            this.yekuangname.Name = "yekuangname";
            this.yekuangname.Size = new Size(0x79, 20);
            this.yekuangname.TabIndex = 0x24;
            this.yekuangname.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.AutoSize = true;
            base.ClientSize = new Size(0x45b, 0x1f2);
            base.Controls.Add(this.yekuangname);
            base.Controls.Add(this.yekuangnum);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.ceDianSelectorListBox1);
            base.Controls.Add(this.label1);
            this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Name = "Select_test";
            this.Text = "选择显示测点";
            base.Load += new EventHandler(this.Select_test_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void initListBox2()
        {
            DataTable dt = OperateDBAccess.Select(string.Concat(new object[] { "select * from LieBiaoAndCeDian where yeKuangName='", this.yeKuangMingCheng, "' and lieBiaoKuangXuHao=", this.lieBiaoKuangBianHao }));
            foreach (DataRow row in dt.Rows)
            {
                string s = row["ceDianBianHao"].ToString();
                string cedianbianhao = s.Substring(0, 5);
                if (GlobalParams.AllCeDianList.allcedianlist.ContainsKey(s.Substring(0, 5)))
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
                    this.ceDianSelectorListBox1.Selector.DisplayList2.Items.Add(info);
                }
            }
        }

        private void Select_test_Load(object sender, EventArgs e)
        {
            this.initListBox2();
            this.yekuangnum.SelectedIndex = this.lieBiaoKuangBianHao - 1;
            this.ceDianSelectorListBox1.Selector.setCeDianLeiXing(3);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void UpdateDBInfo()
        {
            OperateDBAccess.Execute(string.Concat(new object[] { "delete from LieBiaoAndCeDian where yeKuangName='", this.yeKuangMingCheng, "' and lieBiaoKuangXuHao=", this.lieBiaoKuangBianHao }));
            string[] cedians = this.ceDianSelectorListBox1.GetSeletedCeDianAllInfo();
            for (int i = 0; (i < cedians.Length) && (i < 100); i++)
            {
                string ceDianBianHao = cedians[i];
                OperateDBAccess.Execute(string.Concat(new object[] { "insert into LieBiaoAndCeDian(yeKuangName,lieBiaoKuangXuHao,ceDianBianHao) values('", this.yeKuangMingCheng, "',", this.lieBiaoKuangBianHao, ",'", ceDianBianHao, "')" }));
            }
            List_show ls = this.father;
            this.father.LieBiaoKuangs[this.lieBiaoKuangBianHao - 1].GetInfoFromDB();
            this.father.Activate();
        }

        public List_show Father
        {
            get
            {
                return this.father;
            }
            set
            {
                this.father = value;
            }
        }

        public int LieBiaoKuangBianHao
        {
            get
            {
                return this.lieBiaoKuangBianHao;
            }
            set
            {
                this.lieBiaoKuangBianHao = value;
                this.yekuangnum.SelectedIndex = value - 1;
            }
        }

        public string YeKuangMingCheng
        {
            get
            {
                return this.yeKuangMingCheng;
            }
            set
            {
                this.yeKuangMingCheng = value;
                this.yekuangname.Items.Clear();
                string s = "select * from YeKuang";
                DataTable dt = OperateDBAccess.Select(s);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.yekuangname.Items.Add(dt.Rows[i]["name"].ToString());
                    }
                }
                int index = this.yekuangname.Items.IndexOf(value);
                this.yekuangname.SelectedIndex = index;
                this.ceDianSelectorListBox1.YeKuangMingCheng = value;
            }
        }
    }
}

