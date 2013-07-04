namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;

    public class List_adjust : DockContentEx
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private CheckBox checkBox1;
        private ColorDialog colorDialog1;
        private ComboBox comboBox1;
        private IContainer components = null;
        private int currentLieBiaoKuang;
        private List_show grandFather;
        private Label label1;
        private Label label15;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private ListBox listBox1;
        private ListBox listBox2;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private TextBox textBox1;
        private string yeKuangMingCheng;

        public List_adjust(string yeKuangMingCheng, List_show grandFather)
        {
            this.yeKuangMingCheng = yeKuangMingCheng;
            this.InitializeComponent();
            this.label3.Text = this.yeKuangMingCheng;
            this.grandFather = grandFather;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object o = this.listBox1.SelectedItem;
            this.listBox1.Items.Remove(o);
            this.listBox2.Items.Add(o);
            this.UpdateSelectColumns();
            this.grandFather.ReGet(this.yeKuangMingCheng);
            base.Activate();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection u = this.listBox1.SelectedItems;
            if (u.Count > 0)
            {
                while (u.Count > 0)
                {
                    this.listBox2.Items.Add(u[0]);
                    this.listBox1.Items.Remove(u[0]);
                }
                this.UpdateSelectColumns();
                this.grandFather.ReGet(this.yeKuangMingCheng);
                base.Activate();
            }
            else
            {
                MessageBox.Show("请选择要添加的测点！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            object o = this.listBox2.SelectedItem;
            this.listBox2.Items.Remove(o);
            this.listBox1.Items.Add(o);
            this.UpdateSelectColumns();
            this.grandFather.ReGet(this.yeKuangMingCheng);
            base.Activate();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection u = this.listBox2.SelectedItems;
            if (u.Count > 0)
            {
                while (u.Count > 0)
                {
                    this.listBox1.Items.Add(u[0]);
                    this.listBox2.Items.Remove(u[0]);
                }
                this.UpdateSelectColumns();
                this.grandFather.ReGet(this.yeKuangMingCheng);
                base.Activate();
            }
            else
            {
                MessageBox.Show("请选择要添加的测点！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = this.listBox1.Items.Count - 1; i >= 0; i--)
            {
                object o = this.listBox1.Items[i];
                this.listBox1.Items.Remove(o);
                this.listBox2.Items.Add(o);
            }
            this.UpdateSelectColumns();
            this.grandFather.ReGet(this.yeKuangMingCheng);
            base.Activate();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            for (int i = this.listBox1.Items.Count - 1; i >= 0; i--)
            {
                object o = this.listBox1.Items[i];
                this.listBox1.Items.Remove(o);
                this.listBox2.Items.Add(o);
            }
            this.UpdateSelectColumns();
            this.grandFather.ReGet(this.yeKuangMingCheng);
            base.Activate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = this.listBox2.Items.Count - 1; i >= 0; i--)
            {
                object o = this.listBox2.Items[i];
                this.listBox2.Items.Remove(o);
                this.listBox1.Items.Add(o);
            }
            this.UpdateSelectColumns();
            this.grandFather.ReGet(this.yeKuangMingCheng);
            base.Activate();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            for (int i = this.listBox2.Items.Count - 1; i >= 0; i--)
            {
                object o = this.listBox2.Items[i];
                this.listBox2.Items.Remove(o);
                this.listBox1.Items.Add(o);
            }
            this.UpdateSelectColumns();
            this.grandFather.ReGet(this.yeKuangMingCheng);
            base.Activate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() != "")
            {
                OperateDBAccess.Execute("update YeKuang set name='" + this.textBox1.Text.Trim() + "' where name='" + this.yeKuangMingCheng + "'");
                OperateDBAccess.Execute("update LieBiaoKuang set yeKuangName='" + this.textBox1.Text.Trim() + "' where yeKuangName='" + this.yeKuangMingCheng + "'");
                OperateDBAccess.Execute("update LieBiaoAndCeDian set yeKuangName='" + this.textBox1.Text.Trim() + "' where yeKuangName='" + this.yeKuangMingCheng + "'");
                this.yeKuangMingCheng = this.textBox1.Text.Trim();
                this.label3.Text = this.yeKuangMingCheng;
                this.grandFather.ReGet(this.yeKuangMingCheng);
                base.Activate();
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() != "")
            {
                OperateDBAccess.Execute("update YeKuang set name='" + this.textBox1.Text.Trim() + "' where name='" + this.yeKuangMingCheng + "'");
                OperateDBAccess.Execute("update LieBiaoKuang set yeKuangName='" + this.textBox1.Text.Trim() + "' where yeKuangName='" + this.yeKuangMingCheng + "'");
                OperateDBAccess.Execute("update LieBiaoAndCeDian set yeKuangName='" + this.textBox1.Text.Trim() + "' where yeKuangName='" + this.yeKuangMingCheng + "'");
                this.yeKuangMingCheng = this.textBox1.Text.Trim();
                this.label3.Text = this.yeKuangMingCheng;
                this.grandFather.ReGet(this.yeKuangMingCheng);
                base.Activate();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请先选择要调整的列表框序号");
            }
            else if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.label5.BackColor = this.colorDialog1.Color;
                int m = Convert.ToInt32(this.comboBox1.SelectedItem);
                OperateDBAccess.Execute(string.Concat(new object[] { "update LieBiaoKuang set backColor='", ColorTranslator.ToHtml(this.label5.BackColor), "' where yeKuangName='", this.yeKuangMingCheng, "' and lieBiaoKuangXuHao=", m }));
                this.grandFather.LieBiaoKuangs[m - 1].GetInfoFromDB();
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请先选择要调整的列表框序号");
            }
            else if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.label5.BackColor = this.colorDialog1.Color;
                int m = Convert.ToInt32(this.comboBox1.SelectedItem);
                OperateDBAccess.Execute(string.Concat(new object[] { "update LieBiaoKuang set backColor='", ColorTranslator.ToHtml(this.label5.BackColor), "' where yeKuangName='", this.yeKuangMingCheng, "' and lieBiaoKuangXuHao=", m }));
                this.grandFather.LieBiaoKuangs[m - 1].GetInfoFromDB();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            int m = Convert.ToInt32(this.comboBox1.SelectedItem);
            OperateDBAccess.Execute(string.Concat(new object[] { "update LieBiaoKuang set xianShi=", Convert.ToInt32(this.checkBox1.Checked), " where yeKuangName='", this.yeKuangMingCheng, "' and lieBiaoKuangXuHao=", m }));
            this.grandFather.ReGet(this.yeKuangMingCheng);
            base.Activate();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            int m = Convert.ToInt32(this.comboBox1.SelectedItem);
            OperateDBAccess.Execute(string.Concat(new object[] { "update LieBiaoKuang set xianShi=", Convert.ToInt32(this.checkBox1.Checked), " where yeKuangName='", this.yeKuangMingCheng, "' and lieBiaoKuangXuHao=", m }));
            this.grandFather.ReGet(this.yeKuangMingCheng);
            base.Activate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBox2.Items.Clear();
            this.listBox1.Items.Clear();
            int m = Convert.ToInt32(this.comboBox1.SelectedItem);
            this.currentLieBiaoKuang = m;
            DataTable dt = OperateDBAccess.Select(string.Concat(new object[] { "select * from LieBiaoKuang where yeKuangName='", this.yeKuangMingCheng, "' and lieBiaoKuangXuHao=", m }));
            if (dt.Rows.Count > 0)
            {
                this.checkBox1.Checked = Convert.ToBoolean(dt.Rows[0]["xianShi"]);
                if (Convert.ToBoolean(dt.Rows[0]["zhuangTaiTuBiao"]))
                {
                    this.listBox2.Items.Add("状态图标");
                }
                else
                {
                    this.listBox1.Items.Add("状态图标");
                }
                if (Convert.ToBoolean(dt.Rows[0]["anZhuangMingCheng"]))
                {
                    this.listBox2.Items.Add("安装地点");
                }
                else
                {
                    this.listBox1.Items.Add("安装地点");
                }
                if (Convert.ToBoolean(dt.Rows[0]["zhiZhuangTai"]))
                {
                    this.listBox2.Items.Add("测值/状态");
                }
                else
                {
                    this.listBox1.Items.Add("测值/状态");
                }
                if (Convert.ToBoolean(dt.Rows[0]["duanDianQuYu"]))
                {
                    this.listBox2.Items.Add("断电区域");
                }
                else
                {
                    this.listBox1.Items.Add("断电区域");
                }
                if (Convert.ToBoolean(dt.Rows[0]["ceDianBianHao"]))
                {
                    this.listBox2.Items.Add("测点编号");
                }
                else
                {
                    this.listBox1.Items.Add("测点编号");
                }
                if (Convert.ToBoolean(dt.Rows[0]["baoJinsShangXian"]))
                {
                    this.listBox2.Items.Add("报警上限");
                }
                else
                {
                    this.listBox1.Items.Add("报警上限");
                }
                if (Convert.ToBoolean(dt.Rows[0]["baoJingXiaXian"]))
                {
                    this.listBox2.Items.Add("报警下限");
                }
                else
                {
                    this.listBox1.Items.Add("报警下限");
                }
                if (Convert.ToBoolean(dt.Rows[0]["liangChengGaoZhi"]))
                {
                    this.listBox2.Items.Add("量程高值");
                }
                else
                {
                    this.listBox1.Items.Add("量程高值");
                }
                if (Convert.ToBoolean(dt.Rows[0]["liangChengDiZhi"]))
                {
                    this.listBox2.Items.Add("量程低值");
                }
                else
                {
                    this.listBox1.Items.Add("量程低值");
                }
                if (Convert.ToBoolean(dt.Rows[0]["duanDianZhi"]))
                {
                    this.listBox2.Items.Add("断电值");
                }
                else
                {
                    this.listBox1.Items.Add("断电值");
                }
                if (Convert.ToBoolean(dt.Rows[0]["fuDianZhi"]))
                {
                    this.listBox2.Items.Add("复电值");
                }
                else
                {
                    this.listBox1.Items.Add("复电值");
                }
                if (Convert.ToBoolean(dt.Rows[0]["duanDianShiKe"]))
                {
                    this.listBox2.Items.Add("断电时刻");
                }
                else
                {
                    this.listBox1.Items.Add("断电时刻");
                }
                if (Convert.ToBoolean(dt.Rows[0]["fuDianShiKe"]))
                {
                    this.listBox2.Items.Add("复电时刻");
                }
                else
                {
                    this.listBox1.Items.Add("复电时刻");
                }
                if (Convert.ToBoolean(dt.Rows[0]["baoJingShiKe"]))
                {
                    this.listBox2.Items.Add("报警时刻");
                }
                else
                {
                    this.listBox1.Items.Add("报警时刻");
                }
                if (Convert.ToBoolean(dt.Rows[0]["danWei"]))
                {
                    this.listBox2.Items.Add("单位");
                }
                else
                {
                    this.listBox1.Items.Add("单位");
                }
                if (Convert.ToBoolean(dt.Rows[0]["kuiDianZhuangTai"]))
                {
                    this.listBox2.Items.Add("馈电状态");
                }
                else
                {
                    this.listBox1.Items.Add("馈电状态");
                }
                if (Convert.ToBoolean(dt.Rows[0]["zuiDaZhi"]))
                {
                    this.listBox2.Items.Add("最大值");
                }
                else
                {
                    this.listBox1.Items.Add("最大值");
                }
                if (Convert.ToBoolean(dt.Rows[0]["zuiXiaoZhi"]))
                {
                    this.listBox2.Items.Add("最小值");
                }
                else
                {
                    this.listBox1.Items.Add("最小值");
                }
                if (Convert.ToBoolean(dt.Rows[0]["pingJunZhi"]))
                {
                    this.listBox2.Items.Add("平均值");
                }
                else
                {
                    this.listBox1.Items.Add("平均值");
                }
                if (Convert.ToBoolean(dt.Rows[0]["duanDianFanWei"]))
                {
                    this.listBox2.Items.Add("断电范围");
                }
                else
                {
                    this.listBox1.Items.Add("断电范围");
                }
            }
            DataTable dt2 = OperateDBAccess.Select("select count(*) from LieBiaoKuang where yeKuangName='" + this.yeKuangMingCheng + "' and xianShi= true");
            if (dt2.Rows.Count > 0)
            {
                if ((Convert.ToInt32(dt2.Rows[0][0]) <= 1) && this.checkBox1.Checked)
                {
                    this.checkBox1.Enabled = false;
                }
                else
                {
                    this.checkBox1.Enabled = true;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            this.listBox2.Items.Clear();
            this.listBox1.Items.Clear();
            int m = Convert.ToInt32(this.comboBox1.SelectedItem);
            this.currentLieBiaoKuang = m;
            DataTable dt = OperateDBAccess.Select(string.Concat(new object[] { "select * from LieBiaoKuang where yeKuangName='", this.yeKuangMingCheng, "' and lieBiaoKuangXuHao=", m }));
            this.checkBox1.Checked = Convert.ToBoolean(dt.Rows[0]["xianShi"]);
            if ((dt.Rows.Count > 0) && Convert.ToBoolean(dt.Rows[0]["xianShi"]))
            {
                if (Convert.ToBoolean(dt.Rows[0]["zhuangTaiTuBiao"]))
                {
                    this.listBox2.Items.Add("状态图标");
                }
                else
                {
                    this.listBox1.Items.Add("状态图标");
                }
                if (Convert.ToBoolean(dt.Rows[0]["anZhuangMingCheng"]))
                {
                    this.listBox2.Items.Add("安装地点");
                }
                else
                {
                    this.listBox1.Items.Add("安装地点");
                }
                if (Convert.ToBoolean(dt.Rows[0]["zhiZhuangTai"]))
                {
                    this.listBox2.Items.Add("测值/状态");
                }
                else
                {
                    this.listBox1.Items.Add("测值/状态");
                }
                if (Convert.ToBoolean(dt.Rows[0]["duanDianQuYu"]))
                {
                    this.listBox2.Items.Add("断电区域");
                }
                else
                {
                    this.listBox1.Items.Add("断电区域");
                }
                if (Convert.ToBoolean(dt.Rows[0]["ceDianBianHao"]))
                {
                    this.listBox2.Items.Add("测点编号");
                }
                else
                {
                    this.listBox1.Items.Add("测点编号");
                }
                if (Convert.ToBoolean(dt.Rows[0]["baoJinsShangXian"]))
                {
                    this.listBox2.Items.Add("报警上限");
                }
                else
                {
                    this.listBox1.Items.Add("报警上限");
                }
                if (Convert.ToBoolean(dt.Rows[0]["baoJingXiaXian"]))
                {
                    this.listBox2.Items.Add("报警下限");
                }
                else
                {
                    this.listBox1.Items.Add("报警下限");
                }
                if (Convert.ToBoolean(dt.Rows[0]["liangChengGaoZhi"]))
                {
                    this.listBox2.Items.Add("量程高值");
                }
                else
                {
                    this.listBox1.Items.Add("量程高值");
                }
                if (Convert.ToBoolean(dt.Rows[0]["liangChengDiZhi"]))
                {
                    this.listBox2.Items.Add("量程低值");
                }
                else
                {
                    this.listBox1.Items.Add("量程低值");
                }
                if (Convert.ToBoolean(dt.Rows[0]["duanDianZhi"]))
                {
                    this.listBox2.Items.Add("断电值");
                }
                else
                {
                    this.listBox1.Items.Add("断电值");
                }
                if (Convert.ToBoolean(dt.Rows[0]["fuDianZhi"]))
                {
                    this.listBox2.Items.Add("复电值");
                }
                else
                {
                    this.listBox1.Items.Add("复电值");
                }
                if (Convert.ToBoolean(dt.Rows[0]["duanDianShiKe"]))
                {
                    this.listBox2.Items.Add("断电时刻");
                }
                else
                {
                    this.listBox1.Items.Add("断电时刻");
                }
                if (Convert.ToBoolean(dt.Rows[0]["fuDianShiKe"]))
                {
                    this.listBox2.Items.Add("复电时刻");
                }
                else
                {
                    this.listBox1.Items.Add("复电时刻");
                }
                if (Convert.ToBoolean(dt.Rows[0]["baoJingShiKe"]))
                {
                    this.listBox2.Items.Add("报警时刻");
                }
                else
                {
                    this.listBox1.Items.Add("报警时刻");
                }
                if (Convert.ToBoolean(dt.Rows[0]["danWei"]))
                {
                    this.listBox2.Items.Add("单位");
                }
                else
                {
                    this.listBox1.Items.Add("单位");
                }
                if (Convert.ToBoolean(dt.Rows[0]["kuiDianZhuangTai"]))
                {
                    this.listBox2.Items.Add("馈电状态");
                }
                else
                {
                    this.listBox1.Items.Add("馈电状态");
                }
                if (Convert.ToBoolean(dt.Rows[0]["zuiDaZhi"]))
                {
                    this.listBox2.Items.Add("最大值");
                }
                else
                {
                    this.listBox1.Items.Add("最大值");
                }
                if (Convert.ToBoolean(dt.Rows[0]["zuiXiaoZhi"]))
                {
                    this.listBox2.Items.Add("最小值");
                }
                else
                {
                    this.listBox1.Items.Add("最小值");
                }
                if (Convert.ToBoolean(dt.Rows[0]["pingJunZhi"]))
                {
                    this.listBox2.Items.Add("平均值");
                }
                else
                {
                    this.listBox1.Items.Add("平均值");
                }
                if (Convert.ToBoolean(dt.Rows[0]["duanDianFanWei"]))
                {
                    this.listBox2.Items.Add("断电范围");
                }
                else
                {
                    this.listBox1.Items.Add("断电范围");
                }
                if (Convert.ToBoolean(dt.Rows[0]["tftime"]))
                {
                    this.listBox2.Items.Add("变值时刻");
                }
                else
                {
                    this.listBox1.Items.Add("变值时刻");
                }
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
            this.colorDialog1 = new ColorDialog();
            this.panel2 = new Panel();
            this.label6 = new Label();
            this.button6 = new Button();
            this.label2 = new Label();
            this.label5 = new Label();
            this.comboBox1 = new ComboBox();
            this.label4 = new Label();
            this.label1 = new Label();
            this.button5 = new Button();
            this.checkBox1 = new CheckBox();
            this.textBox1 = new TextBox();
            this.label3 = new Label();
            this.panel1 = new Panel();
            this.listBox2 = new ListBox();
            this.listBox1 = new ListBox();
            this.button4 = new Button();
            this.button1 = new Button();
            this.button3 = new Button();
            this.button2 = new Button();
            this.panel3 = new Panel();
            this.label15 = new Label();
            this.panel4 = new Panel();
            this.label7 = new Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            base.SuspendLayout();
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.button6);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new Point(0x26, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x1b6, 0x1b7);
            this.panel2.TabIndex = 8;
            this.label6.AutoSize = true;
            this.label6.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label6.Location = new Point(0x27, 0x5b);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x43, 14);
            this.label6.TabIndex = 11;
            this.label6.Text = "页框原名";
            this.label6.Click += new EventHandler(this.label6_Click);
            this.button6.BackColor = Color.Chocolate;
            this.button6.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new Point(0x14d, 320);
            this.button6.Name = "button6";
            this.button6.Size = new Size(90, 0x17);
            this.button6.TabIndex = 10;
            this.button6.Text = "选择颜色";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new EventHandler(this.button6_Click_1);
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new Point(0x27, 0xa3);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x43, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "页框名称";
            this.label2.Click += new EventHandler(this.label2_Click);
            this.label5.BackColor = Color.White;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new Point(0x7a, 320);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0xc3, 0x17);
            this.label5.TabIndex = 9;
            this.label5.Text = "            ";
            this.comboBox1.BackColor = Color.White;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "1", "2", "3" });
            this.comboBox1.Location = new Point(0x7a, 0xee);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0xc3, 0x16);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged_1);
            this.label4.AutoSize = true;
            this.label4.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new Point(0x27, 320);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x43, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "背景颜色";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0x27, 0xf2);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x43, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择列表";
            this.button5.BackColor = Color.Chocolate;
            this.button5.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new Point(0x14d, 0x9f);
            this.button5.Name = "button5";
            this.button5.Size = new Size(90, 0x17);
            this.button5.TabIndex = 7;
            this.button5.Text = "更改名称";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new EventHandler(this.button5_Click_1);
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = CheckState.Checked;
            this.checkBox1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.checkBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.checkBox1.Location = new Point(0x14e, 0xee);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x59, 30);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "显示列表";
            this.checkBox1.TextAlign = ContentAlignment.MiddleCenter;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged_1);
            this.textBox1.BackColor = Color.White;
            this.textBox1.Location = new Point(0x7a, 0x9f);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0xc3, 0x17);
            this.textBox1.TabIndex = 6;
            this.label3.BackColor = SystemColors.ButtonFace;
            this.label3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label3.Location = new Point(0x77, 0x5b);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0xc6, 0x17);
            this.label3.TabIndex = 5;
            this.label3.Text = "显示页框的名称";
            this.label3.TextAlign = ContentAlignment.MiddleCenter;
            this.panel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.listBox2);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new Point(0x1f8, 0x27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(490, 440);
            this.panel1.TabIndex = 7;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 14;
            this.listBox2.Location = new Point(0x142, 0x40);
            this.listBox2.Name = "listBox2";
            this.listBox2.SelectionMode = SelectionMode.MultiExtended;
            this.listBox2.Size = new Size(0x8b, 0x162);
            this.listBox2.TabIndex = 1;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new Point(0x1f, 0x40);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = SelectionMode.MultiExtended;
            this.listBox1.Size = new Size(0x8b, 0x162);
            this.listBox1.TabIndex = 0;
            this.button4.BackColor = Color.Chocolate;
            this.button4.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new Point(0xc6, 300);
            this.button4.Name = "button4";
            this.button4.Size = new Size(100, 0x17);
            this.button4.TabIndex = 5;
            this.button4.Text = "全部移除 <<";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new EventHandler(this.button4_Click_1);
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(0xc6, 0x9b);
            this.button1.Name = "button1";
            this.button1.Size = new Size(100, 0x17);
            this.button1.TabIndex = 2;
            this.button1.Text = "添加 >";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click_1);
            this.button3.BackColor = Color.Chocolate;
            this.button3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new Point(0xc6, 0x10a);
            this.button3.Name = "button3";
            this.button3.Size = new Size(100, 0x17);
            this.button3.TabIndex = 4;
            this.button3.Text = "全部添加 >>";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new EventHandler(this.button3_Click_1);
            this.button2.BackColor = Color.Chocolate;
            this.button2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new Point(0xc6, 0xbc);
            this.button2.Name = "button2";
            this.button2.Size = new Size(100, 0x17);
            this.button2.TabIndex = 3;
            this.button2.Text = "移除 <";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new EventHandler(this.button2_Click_1);
            this.panel3.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel3.AutoSize = true;
            this.panel3.BackColor = Color.AliceBlue;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label15);
            this.panel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel3.Location = new Point(0x26, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x1b6, 30);
            this.panel3.TabIndex = 0x26;
            this.label15.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label15.Location = new Point(0x27, 2);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0xa8, 0x1a);
            this.label15.TabIndex = 8;
            this.label15.Text = "列表显示";
            this.label15.TextAlign = ContentAlignment.MiddleLeft;
            this.label15.Click += new EventHandler(this.label15_Click);
            this.panel4.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel4.AutoSize = true;
            this.panel4.BackColor = Color.AliceBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label7);
            this.panel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel4.Location = new Point(0x1f8, 40);
            this.panel4.Name = "panel4";
            this.panel4.Size = new Size(490, 30);
            this.panel4.TabIndex = 0x27;
            this.label7.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label7.Location = new Point(0x1c, 1);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0xa8, 0x1a);
            this.label7.TabIndex = 8;
            this.label7.Text = "表项显示";
            this.label7.TextAlign = ContentAlignment.MiddleLeft;
            base.AutoScaleDimensions = new SizeF(7f, 14f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(0x3dd, 0x1eb);
            base.Controls.Add(this.panel4);
            base.Controls.Add(this.panel3);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            this.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Name = "List_adjust";
            this.Text = "列表调整";
            base.Load += new EventHandler(this.List_adjust_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void label15_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void List_adjust_Load(object sender, EventArgs e)
        {
        }

        private void panel1_Validated(object sender, EventArgs e)
        {
        }

        private void UpdateSelectColumns()
        {
            string s = "update LieBiaoKuang set ";
            if (this.listBox2.Items.Contains("状态图标"))
            {
                s = s + "zhuangTaiTuBiao=1,";
            }
            else
            {
                s = s + "zhuangTaiTuBiao=0,";
            }
            if (this.listBox2.Items.Contains("安装地点"))
            {
                s = s + "anZhuangMingCheng=1,";
            }
            else
            {
                s = s + "anZhuangMingCheng=0,";
            }
            if (this.listBox2.Items.Contains("测值/状态"))
            {
                s = s + "zhiZhuangTai=1,";
            }
            else
            {
                s = s + "zhiZhuangTai=0,";
            }
            if (this.listBox2.Items.Contains("断电区域"))
            {
                s = s + "duanDianQuYu=1,";
            }
            else
            {
                s = s + "duanDianQuYu=0,";
            }
            if (this.listBox2.Items.Contains("测点编号"))
            {
                s = s + "ceDianBianHao=1,";
            }
            else
            {
                s = s + "ceDianBianHao=0,";
            }
            if (this.listBox2.Items.Contains("报警上限"))
            {
                s = s + "baoJinsShangXian=1,";
            }
            else
            {
                s = s + "baoJinsShangXian=0,";
            }
            if (this.listBox2.Items.Contains("报警下限"))
            {
                s = s + "baoJingXiaXian=1,";
            }
            else
            {
                s = s + "baoJingXiaXian=0,";
            }
            if (this.listBox2.Items.Contains("量程高值"))
            {
                s = s + "liangChengGaoZhi=1,";
            }
            else
            {
                s = s + "liangChengGaoZhi=0,";
            }
            if (this.listBox2.Items.Contains("量程低值"))
            {
                s = s + "liangChengDiZhi=1,";
            }
            else
            {
                s = s + "liangChengDiZhi=0,";
            }
            if (this.listBox2.Items.Contains("断电值"))
            {
                s = s + "duanDianZhi=1,";
            }
            else
            {
                s = s + "duanDianZhi=0,";
            }
            if (this.listBox2.Items.Contains("断电值"))
            {
                s = s + "fuDianZhi=1,";
            }
            else
            {
                s = s + "fuDianZhi=0,";
            }
            if (this.listBox2.Items.Contains("断电时刻"))
            {
                s = s + "duanDianShiKe=1,";
            }
            else
            {
                s = s + "duanDianShiKe=0,";
            }
            if (this.listBox2.Items.Contains("复电时刻"))
            {
                s = s + "fuDianShiKe=1,";
            }
            else
            {
                s = s + "fuDianShiKe=0,";
            }
            if (this.listBox2.Items.Contains("报警时刻"))
            {
                s = s + "baoJingShiKe=1,";
            }
            else
            {
                s = s + "baoJingShiKe=0,";
            }
            if (this.listBox2.Items.Contains("单位"))
            {
                s = s + "danWei=1,";
            }
            else
            {
                s = s + "danWei=0,";
            }
            if (this.listBox2.Items.Contains("馈电状态"))
            {
                s = s + "kuiDianZhuangTai=1,";
            }
            else
            {
                s = s + "kuiDianZhuangTai=0,";
            }
            if (this.listBox2.Items.Contains("最大值"))
            {
                s = s + "zuiDaZhi=1,";
            }
            else
            {
                s = s + "zuiDaZhi=0,";
            }
            if (this.listBox2.Items.Contains("最小值"))
            {
                s = s + "zuiXiaoZhi=1,";
            }
            else
            {
                s = s + "zuiXiaoZhi=0,";
            }
            if (this.listBox2.Items.Contains("平均值"))
            {
                s = s + "pingJunZhi=1,";
            }
            else
            {
                s = s + "pingJunZhi=0,";
            }
            if (this.listBox2.Items.Contains("断电范围"))
            {
                s = s + "duanDianFanWei=1, ";
            }
            else
            {
                s = s + "duanDianFanWei=0,";
            }
            if (this.listBox2.Items.Contains("变值时刻"))
            {
                s = s + "tftime=1 ";
            }
            else
            {
                s = s + "tftime=0 ";
            }
            object Reflector0001 = s;
            OperateDBAccess.Execute(string.Concat(new object[] { Reflector0001, "where yeKuangName='", this.yeKuangMingCheng, "' and lieBiaoKuangXuHao=", this.currentLieBiaoKuang }));
        }

        public List_show GrandFather
        {
            get
            {
                return this.grandFather;
            }
            set
            {
                this.grandFather = value;
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
                this.label3.Text = value;
            }
        }
    }
}

