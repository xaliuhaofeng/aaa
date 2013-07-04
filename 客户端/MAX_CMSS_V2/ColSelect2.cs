namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class ColSelect2 : UserControl
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private List<string> cedians;
        private IContainer components = null;
        private List<string> list1;
        private List<string> list2;
        private ListBox listBox1;
        private ListBox listBox2;
        private string yeKuangMingCheng;

        public ColSelect2()
        {
            this.InitializeComponent();
            this.list1 = new List<string>();
            this.list2 = new List<string>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection u = this.listBox1.SelectedItems;
            if (this.cedians == null)
            {
                this.cedians = this.getAllCeDianInThisYeKuang();
            }
            if (u.Count > 0)
            {
                for (int i = 0; i < u.Count; i++)
                {
                    if (this.cedians.Contains(u[i].ToString().Substring(0, 5)) || this.list2.Contains(u[i].ToString().Substring(0, 5)))
                    {
                        MessageBox.Show("请不要重复添加测点！");
                    }
                    else
                    {
                        this.listBox2.Items.Add(u[i]);
                        this.list2.Add(u[i].ToString().Substring(0, 5));
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要添加的测点！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListBox.ObjectCollection items = this.listBox1.Items;
            if (this.cedians == null)
            {
                this.cedians = this.getAllCeDianInThisYeKuang();
            }
            foreach (object item in items)
            {
                if (!(this.cedians.Contains(item.ToString().Substring(0, 5)) || this.list2.Contains(item.ToString().Substring(0, 5))))
                {
                    this.listBox2.Items.Add(item);
                    this.list2.Add(item.ToString().Substring(0, 5));
                }
                else
                {
                    MessageBox.Show("请不要重复添加测点！");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection u = this.listBox2.SelectedItems;
            if (this.cedians == null)
            {
                this.cedians = this.getAllCeDianInThisYeKuang();
            }
            if (u.Count > 0)
            {
                while (u.Count > 0)
                {
                    this.cedians.Remove(u[0].ToString().Substring(0, 5));
                    this.list2.Remove(u[0].ToString().Substring(0, 5));
                    this.listBox2.Items.Remove(u[0]);
                }
            }
            else
            {
                MessageBox.Show("请选择要添加的测点！");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ListBox.ObjectCollection s = this.listBox2.Items;
            if (this.cedians == null)
            {
                this.cedians = this.getAllCeDianInThisYeKuang();
            }
            foreach (object cedian in s)
            {
                this.cedians.Remove(cedian.ToString().Substring(0, 5));
            }
            this.listBox2.Items.Clear();
            this.list2.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = this.listBox2.SelectedIndex;
            ListBox.SelectedObjectCollection items = this.listBox2.SelectedItems;
            if (index > 0)
            {
                object obj = this.listBox2.Items[index - 1];
                int count = this.listBox2.SelectedItems.Count;
                this.listBox2.Items.Remove(obj);
                this.listBox2.Items.Insert((index + count) - 1, obj);
            }
            else if (index == -1)
            {
                MessageBox.Show("请选择要上移的测点！");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int index = this.listBox2.SelectedIndex;
            int count = this.listBox2.SelectedItems.Count;
            if ((index != -1) && ((index + count) < this.listBox2.Items.Count))
            {
                object obj = this.listBox2.Items[index + count];
                this.listBox2.Items.Remove(obj);
                this.listBox2.Items.Insert(index, obj);
            }
            else if (index == -1)
            {
                MessageBox.Show("请选择要下移的测点！");
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

        private List<string> getAllCeDianInThisYeKuang()
        {
            DataTable dt = OperateDBAccess.Select("select * from LieBiaoAndCeDian where yeKuangName='" + this.yeKuangMingCheng + "'");
            List<string> ret = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                string s = row["ceDianBianHao"].ToString();
                if (GlobalParams.AllCeDianList.allcedianlist.ContainsKey(s.Substring(0, 5)))
                {
                    ret.Add(s.Substring(0, 5));
                }
            }
            return ret;
        }

        public string[] getSelectedCeDian()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < this.listBox2.Items.Count; i++)
            {
                list.Add(this.listBox2.Items[i].ToString().Trim().Substring(0, 5));
            }
            return list.ToArray();
        }

        private void InitializeComponent()
        {
            this.listBox1 = new ListBox();
            this.button1 = new Button();
            this.button2 = new Button();
            this.button3 = new Button();
            this.button4 = new Button();
            this.listBox2 = new ListBox();
            this.button5 = new Button();
            this.button6 = new Button();
            base.SuspendLayout();
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(0x1a, 6);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = SelectionMode.MultiExtended;
            this.listBox1.Size = new Size(0xef, 0xf4);
            this.listBox1.TabIndex = 0;
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(0x124, 40);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x41, 0x17);
            this.button1.TabIndex = 1;
            this.button1.Text = "->";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.BackColor = Color.Chocolate;
            this.button2.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new Point(0x124, 0x45);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x41, 0x17);
            this.button2.TabIndex = 2;
            this.button2.Text = ">>";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.button3.BackColor = Color.Chocolate;
            this.button3.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new Point(0x124, 0xa6);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x41, 0x17);
            this.button3.TabIndex = 3;
            this.button3.Text = "<-";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new EventHandler(this.button3_Click);
            this.button4.BackColor = Color.Chocolate;
            this.button4.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new Point(0x124, 0xc3);
            this.button4.Name = "button4";
            this.button4.Size = new Size(0x41, 0x17);
            this.button4.TabIndex = 4;
            this.button4.Text = "<<";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new EventHandler(this.button4_Click);
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new Point(0x180, 6);
            this.listBox2.Name = "listBox2";
            this.listBox2.SelectionMode = SelectionMode.MultiExtended;
            this.listBox2.Size = new Size(0xef, 0xf4);
            this.listBox2.TabIndex = 5;
            this.button5.BackColor = Color.Chocolate;
            this.button5.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new Point(0x286, 0x44);
            this.button5.Name = "button5";
            this.button5.Size = new Size(0x4b, 0x17);
            this.button5.TabIndex = 6;
            this.button5.Text = "上移";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new EventHandler(this.button5_Click);
            this.button6.BackColor = Color.Chocolate;
            this.button6.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new Point(0x286, 0xb0);
            this.button6.Name = "button6";
            this.button6.Size = new Size(0x4b, 0x17);
            this.button6.TabIndex = 7;
            this.button6.Text = "下移";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new EventHandler(this.button6_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.button6);
            base.Controls.Add(this.button5);
            base.Controls.Add(this.listBox2);
            base.Controls.Add(this.button4);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.listBox1);
            base.Name = "ColSelect2";
            base.Size = new Size(0x2e5, 0xfe);
            base.ResumeLayout(false);
        }

        public void initListBox(string[] list, int listbox)
        {
            foreach (string s in list)
            {
                if (listbox == 1)
                {
                    this.listBox1.Items.Add(s);
                    this.list1.Add(s.Substring(0, 5));
                }
                else
                {
                    this.listBox2.Items.Add(s);
                    this.list2.Add(s.Substring(0, 5));
                }
            }
        }

        public ListBox ListBox1
        {
            get
            {
                return this.listBox1;
            }
        }

        public ListBox ListBox2
        {
            get
            {
                return this.listBox2;
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
            }
        }
    }
}

