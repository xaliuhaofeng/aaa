namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Comm_FengDian : UserControl
    {
        private Button button1;
        private Button button2;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private ComboBox comboBox4;
        private ComboBox comboBox5;
        private ComboBox comboBox6;
        private ComboBox comboBox7;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;

        public Comm_FengDian()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (((((this.comboBox1.SelectedItem == null) || (this.comboBox2.SelectedItem == null)) || ((this.comboBox3.SelectedItem == null) || (this.comboBox4.SelectedItem == null))) || ((this.comboBox5.SelectedItem == null) || (this.comboBox6.SelectedItem == null))) || (this.comboBox7.SelectedItem == null))
            {
                MessageBox.Show("参数选择错误");
            }
            else
            {
                UDPComm.Send(FenZhan.FengDianWaSiBiSuo(Convert.ToByte(this.comboBox1.SelectedItem), Convert.ToByte(this.comboBox2.SelectedItem.ToString().Substring(3, 2)), Convert.ToByte(this.comboBox3.SelectedItem.ToString().Substring(3, 2)), Convert.ToByte(this.comboBox4.SelectedItem.ToString().Substring(3, 2)), Convert.ToByte(this.comboBox5.SelectedItem.ToString().Substring(3, 2)), Convert.ToByte(this.comboBox6.SelectedItem.ToString().Substring(3, 2)), Convert.ToByte(this.comboBox7.SelectedItem.ToString().Substring(3, 2))));
                FenZhan.SaveFengDianWaSi(Convert.ToByte(this.comboBox1.SelectedItem), this.comboBox2.SelectedItem.ToString(), this.comboBox3.SelectedItem.ToString(), this.comboBox4.SelectedItem.ToString(), this.comboBox5.SelectedItem.ToString(), this.comboBox6.SelectedItem.ToString(), this.comboBox7.SelectedItem.ToString());
                MessageBox.Show("风电瓦斯闭锁配置成功！");
                DateTime dt = DateTime.Now;
                Log.WriteLog(LogType.FengDianWaSiBiSuo, this.comboBox2.SelectedItem.ToString() + ", " + this.comboBox3.SelectedItem.ToString() + ", " + this.comboBox4.SelectedItem.ToString() + ", " + this.comboBox5.SelectedItem.ToString() + ", " + this.comboBox6.SelectedItem.ToString() + ", " + this.comboBox7.SelectedItem.ToString() + "#$" + dt.ToString() + "#$下发成功");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedItem == null)
            {
                MessageBox.Show("请选择分站");
            }
            else
            {
                byte fenzhan = Convert.ToByte(this.comboBox1.SelectedItem);
                UDPComm.Send(FenZhan.FengDianWaSiBiSuo(fenzhan, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff));
                FenZhan.deleteFengDianWaSiBiSuo(fenzhan);
                this.comboBox2.SelectedIndex = this.comboBox3.SelectedIndex = this.comboBox4.SelectedIndex = this.comboBox5.SelectedIndex = this.comboBox6.SelectedIndex = this.comboBox7.SelectedIndex = -1;
                DateTime dt = DateTime.Now;
                MessageBox.Show("风电瓦斯闭锁删除成功！");
                Log.WriteLog(LogType.FengDianWaSiBiSuo, string.Concat(new object[] { fenzhan, "号分站#$", dt.ToString(), "#$删除成功" }));
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i;
            this.comboBox2.Items.Clear();
            this.comboBox3.Items.Clear();
            this.comboBox4.Items.Clear();
            this.comboBox5.Items.Clear();
            this.comboBox6.Items.Clear();
            this.comboBox7.Items.Clear();
            byte fenZhanHao = Convert.ToByte(this.comboBox1.SelectedItem);
            DataTable dt = OperateDB.Select("select * from FengDianWaSi where fenZhanHao=" + fenZhanHao);
            string[] s = CeDian.GetMoNiLiangCeDianByFenZhan(fenZhanHao);
            if (s != null)
            {
                for (i = 0; i < s.Length; i++)
                {
                    this.comboBox2.Items.Add(s[i]);
                    this.comboBox3.Items.Add(s[i]);
                    this.comboBox4.Items.Add(s[i]);
                }
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i < this.comboBox2.Items.Count; i++)
                    {
                        if (this.comboBox2.Items[i].ToString().Trim() == dt.Rows[0]["jinFengWaSi"].ToString())
                        {
                            this.comboBox2.SelectedIndex = i;
                            break;
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i < this.comboBox3.Items.Count; i++)
                    {
                        if (this.comboBox3.Items[i].ToString().Trim() == dt.Rows[0]["huiFengWaSi"].ToString())
                        {
                            this.comboBox3.SelectedIndex = i;
                            break;
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i < this.comboBox4.Items.Count; i++)
                    {
                        if (this.comboBox4.Items[i].ToString().Trim() == dt.Rows[0]["chuanLianTongFeng"].ToString())
                        {
                            this.comboBox4.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            s = CeDian.GetKaiGuanLiangCeDianByFenZhan(fenZhanHao);
            if (s != null)
            {
                for (i = 0; i < s.Length; i++)
                {
                    this.comboBox5.Items.Add(s[i]);
                    this.comboBox6.Items.Add(s[i]);
                }
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i < this.comboBox5.Items.Count; i++)
                    {
                        if (this.comboBox5.Items[i].ToString().Trim() == dt.Rows[0]["juShan"].ToString())
                        {
                            this.comboBox5.SelectedIndex = i;
                            break;
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i < this.comboBox6.Items.Count; i++)
                    {
                        if (this.comboBox6.Items[i].ToString().Trim() == dt.Rows[0]["fengTongFengLiang"].ToString())
                        {
                            this.comboBox6.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            s = KongZhiLiangCeDian.GetKongZhiLiangCeDianByFenZhan(fenZhanHao);
            if (s != null)
            {
                for (i = 0; i < s.Length; i++)
                {
                    this.comboBox7.Items.Add(s[i]);
                }
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i < this.comboBox7.Items.Count; i++)
                    {
                        if (this.comboBox7.Items[i].ToString().Trim() == dt.Rows[0]["kongZhiLiang"].ToString())
                        {
                            this.comboBox7.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        private void Comm_FengDian_Load(object sender, EventArgs e)
        {
            string[] fenzhans = FenZhan.GetAllConfigedFenZhan();
            this.comboBox1.Items.AddRange(fenzhans);
            this.comboBox1.SelectedIndex = 0;
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
            this.button1 = new Button();
            this.comboBox1 = new ComboBox();
            this.comboBox2 = new ComboBox();
            this.comboBox3 = new ComboBox();
            this.comboBox4 = new ComboBox();
            this.comboBox5 = new ComboBox();
            this.comboBox6 = new ComboBox();
            this.comboBox7 = new ComboBox();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.label6 = new Label();
            this.label7 = new Label();
            this.button2 = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0x3b, 0x69);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x52, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择分站号";
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(0x285, 0x11e);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 4;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(0xa5, 0x66);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0xe8, 20);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new Point(0xa5, 0xa6);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(0xe8, 20);
            this.comboBox2.TabIndex = 6;
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new Point(0x23c, 0x66);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new Size(0xe8, 20);
            this.comboBox3.TabIndex = 7;
            this.comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new Point(0xa5, 0xe5);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new Size(0xe8, 20);
            this.comboBox4.TabIndex = 9;
            this.comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new Point(0x23c, 0xa6);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new Size(0xe8, 20);
            this.comboBox5.TabIndex = 8;
            this.comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new Point(0xa5, 0x124);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new Size(0xe8, 20);
            this.comboBox6.TabIndex = 11;
            this.comboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Location = new Point(0x23c, 0xe5);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new Size(0xe8, 20);
            this.comboBox7.TabIndex = 10;
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.Transparent;
            this.label2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new Point(0x3b, 0xa9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x61, 14);
            this.label2.TabIndex = 12;
            this.label2.Text = "选择进风瓦斯";
            this.label3.AutoSize = true;
            this.label3.BackColor = Color.Transparent;
            this.label3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new Point(0x1d3, 0x69);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x61, 14);
            this.label3.TabIndex = 13;
            this.label3.Text = "选择回风瓦斯";
            this.label4.AutoSize = true;
            this.label4.BackColor = Color.Transparent;
            this.label4.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new Point(0x3b, 0xe8);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x61, 14);
            this.label4.TabIndex = 14;
            this.label4.Text = "选择串联通风";
            this.label5.AutoSize = true;
            this.label5.BackColor = Color.Transparent;
            this.label5.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label5.Location = new Point(0x1d3, 0xa9);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x43, 14);
            this.label5.TabIndex = 15;
            this.label5.Text = "选择局扇";
            this.label6.AutoSize = true;
            this.label6.BackColor = Color.Transparent;
            this.label6.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label6.Location = new Point(0x3b, 0x127);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x61, 14);
            this.label6.TabIndex = 0x10;
            this.label6.Text = "选择风筒风量";
            this.label7.AutoSize = true;
            this.label7.BackColor = Color.Transparent;
            this.label7.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label7.Location = new Point(0x1d3, 0xe8);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x52, 14);
            this.label7.TabIndex = 0x11;
            this.label7.Text = "选择控制量";
            this.button2.Location = new Point(0x2de, 0x11d);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 0x12;
            this.button2.Text = "清除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.button2);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.comboBox6);
            base.Controls.Add(this.comboBox7);
            base.Controls.Add(this.comboBox4);
            base.Controls.Add(this.comboBox5);
            base.Controls.Add(this.comboBox3);
            base.Controls.Add(this.comboBox2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.comboBox1);
            base.Name = "Comm_FengDian";
            base.Size = new Size(0x36e, 0x199);
            base.Load += new EventHandler(this.Comm_FengDian_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

