namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Linear_manage : UserControl
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private GroupBox groupBox4;
        private Label label1;
        private Label label15;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Panel panel1;
        private Panel panel2;
        private TextBox textBox1;
        private TextBox textBox10;
        private TextBox textBox11;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private TextBox textBox6;
        private TextBox textBox7;
        private TextBox textBox8;
        private TextBox textBox9;

        public Linear_manage()
        {
            this.InitializeComponent();
        }

        private bool argCheck()
        {
            if (this.textBox9.Text.Length == 0)
            {
                MessageBox.Show("非线性关系名称不可为空");
                return false;
            }
            if (((((((this.textBox10.Text.Length == 0) && (this.textBox11.Text.Length != 0)) || ((this.textBox11.Text.Length == 0) && (this.textBox10.Text.Length != 0))) || (((this.textBox1.Text.Length == 0) && (this.textBox2.Text.Length != 0)) || ((this.textBox2.Text.Length == 0) && (this.textBox1.Text.Length != 0)))) || ((((this.textBox3.Text.Length == 0) && (this.textBox4.Text.Length != 0)) || ((this.textBox4.Text.Length == 0) && (this.textBox3.Text.Length != 0))) || (((this.textBox5.Text.Length == 0) && (this.textBox6.Text.Length != 0)) || ((this.textBox6.Text.Length == 0) && (this.textBox5.Text.Length != 0))))) || ((this.textBox7.Text.Length == 0) && (this.textBox8.Text.Length != 0))) || ((this.textBox8.Text.Length == 0) && (this.textBox7.Text.Length != 0)))
            {
                MessageBox.Show("对应的分段量程与分段测值必须同为空或同有值");
                return false;
            }
            if ((((this.textBox11.Text.Length == 0) && (this.textBox2.Text.Length == 0)) && ((this.textBox4.Text.Length == 0) && (this.textBox6.Text.Length == 0))) && (this.textBox8.Text.Length == 0))
            {
                MessageBox.Show("分段量程与分段测值不可全为空");
                return false;
            }
            if ((((this.textBox11.Text.Length == 0) || ((this.textBox2.Text.Length == 0) && (this.textBox4.Text.Length != 0))) || ((this.textBox4.Text.Length == 0) && (this.textBox6.Text.Length != 0))) || ((this.textBox6.Text.Length == 0) && (this.textBox8.Text.Length != 0)))
            {
                MessageBox.Show("中间值不可为空");
                return false;
            }
            if (this.textBox1.Text.Length == 0)
            {
                this.textBox1.Text = this.textBox2.Text = this.textBox3.Text = this.textBox4.Text = this.textBox5.Text = this.textBox6.Text = this.textBox7.Text = this.textBox8.Text = "100000";
            }
            else if (this.textBox3.Text.Length == 0)
            {
                this.textBox3.Text = this.textBox4.Text = this.textBox5.Text = this.textBox6.Text = this.textBox7.Text = this.textBox8.Text = "100000";
            }
            else if (this.textBox5.Text.Length == 0)
            {
                this.textBox5.Text = this.textBox6.Text = this.textBox7.Text = this.textBox8.Text = "100000";
            }
            else if (this.textBox7.Text.Length == 0)
            {
                this.textBox7.Text = this.textBox8.Text = "100000";
            }
            else
            {
                if ((((int.Parse(this.textBox10.Text) > int.Parse(this.textBox1.Text)) || (int.Parse(this.textBox1.Text) > int.Parse(this.textBox3.Text))) || (int.Parse(this.textBox3.Text) > int.Parse(this.textBox5.Text))) || (int.Parse(this.textBox5.Text) > int.Parse(this.textBox7.Text)))
                {
                    MessageBox.Show("分段量程低值不能高于分段量程高值");
                    return false;
                }
                if ((((int.Parse(this.textBox11.Text) > int.Parse(this.textBox2.Text)) || (int.Parse(this.textBox2.Text) > int.Parse(this.textBox4.Text))) || (int.Parse(this.textBox4.Text) > int.Parse(this.textBox6.Text))) || (int.Parse(this.textBox6.Text) > int.Parse(this.textBox8.Text)))
                {
                    MessageBox.Show("分段测值低值不能高于分段测值高值");
                    return false;
                }
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.argCheck())
            {
                if (GlobalParams.AllXianXingZhi.IsExist(this.textBox9.Text.ToString().Trim()))
                {
                    MessageBox.Show("该非线性关系名称已存在，请重新命名");
                }
                else
                {
                    GlobalParams.AllXianXingZhi.InsertMnlMC(this.textBox9.Text, this.textBox10.Text, this.textBox1.Text, this.textBox3.Text, this.textBox5.Text, this.textBox7.Text, this.textBox11.Text, this.textBox2.Text, this.textBox4.Text, this.textBox6.Text, this.textBox8.Text);
                    DataTable dt = GlobalParams.AllXianXingZhi.GetFeiXianXing();
                    this.dataGridView1.DataSource = dt;
                    this.cleartext();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请选择要修改的非线性关系");
            }
            else if (this.argCheck())
            {
                if (!(!(this.textBox9.Text != this.dataGridView1.CurrentRow.Cells["moNiLiangMingCheng"].Value.ToString()) || this.CanDel(this.dataGridView1.CurrentRow.Cells["moNiLiangMingCheng"].Value.ToString())))
                {
                    MessageBox.Show("该非线性关系已被引用，不可重新命名");
                }
                else if ((this.textBox9.Text != this.dataGridView1.CurrentRow.Cells["moNiLiangMingCheng"].Value.ToString()) && GlobalParams.AllXianXingZhi.IsExist(this.textBox9.Text.ToString()))
                {
                    MessageBox.Show("该非线性关系名称已存在，请重新命名");
                }
                else
                {
                    GlobalParams.AllXianXingZhi.UpdataXianXingZhi(this.textBox9.Text, this.textBox10.Text, this.textBox1.Text, this.textBox3.Text, this.textBox5.Text, this.textBox7.Text, this.textBox11.Text, this.textBox2.Text, this.textBox4.Text, this.textBox6.Text, this.textBox8.Text, this.dataGridView1.CurrentRow.Cells["moNiLiangMingCheng"].Value.ToString());
                    DataTable dt = GlobalParams.AllXianXingZhi.GetFeiXianXing();
                    this.dataGridView1.DataSource = dt;
                    this.cleartext();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请选择要删除的非线性关系");
            }
            else if (!this.CanDel(this.dataGridView1.CurrentRow.Cells["moNiLiangMingCheng"].Value.ToString()))
            {
                MessageBox.Show("该非线性关系已被引用，不可删除");
            }
            else
            {
                GlobalParams.AllXianXingZhi.DeleteXianXingZhi(this.dataGridView1.CurrentRow.Cells["moNiLiangMingCheng"].Value.ToString());
                DataTable dt = GlobalParams.AllXianXingZhi.GetFeiXianXing();
                this.dataGridView1.DataSource = dt;
                this.cleartext();
            }
        }

        private bool CanDel(string Original)
        {
            return (MoNiLiangLeiXing.CountXianXing(Original) == 0);
        }

        private void cleartext()
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.textBox4.Clear();
            this.textBox5.Clear();
            this.textBox6.Clear();
            this.textBox7.Clear();
            this.textBox8.Clear();
            this.textBox9.Clear();
            this.textBox10.Clear();
            this.textBox11.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.textBox9.Text = this.dataGridView1.CurrentRow.Cells["moNiLiangMingCheng"].Value.ToString();
            this.textBox10.Text = this.dataGridView1.CurrentRow.Cells["liangCheng0"].Value.ToString();
            this.textBox1.Text = this.dataGridView1.CurrentRow.Cells["liangCheng1"].Value.ToString();
            this.textBox3.Text = this.dataGridView1.CurrentRow.Cells["liangCheng2"].Value.ToString();
            this.textBox5.Text = this.dataGridView1.CurrentRow.Cells["liangCheng3"].Value.ToString();
            this.textBox7.Text = this.dataGridView1.CurrentRow.Cells["liangCheng4"].Value.ToString();
            this.textBox11.Text = this.dataGridView1.CurrentRow.Cells["value0"].Value.ToString();
            this.textBox2.Text = this.dataGridView1.CurrentRow.Cells["value1"].Value.ToString();
            this.textBox4.Text = this.dataGridView1.CurrentRow.Cells["value2"].Value.ToString();
            this.textBox6.Text = this.dataGridView1.CurrentRow.Cells["value3"].Value.ToString();
            this.textBox8.Text = this.dataGridView1.CurrentRow.Cells["value4"].Value.ToString();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.panel2);
            this.groupBox4.Controls.Add(this.panel1);
            this.groupBox4.Location = new System.Drawing.Point(0, 223);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(880, 348);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSize = true;
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label15);
            this.panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel2.Location = new System.Drawing.Point(0, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(880, 29);
            this.panel2.TabIndex = 36;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label15.Location = new System.Drawing.Point(41, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(111, 26);
            this.label15.TabIndex = 8;
            this.label15.Text = "线性值管理";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.textBox11);
            this.panel1.Controls.Add(this.textBox10);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.textBox9);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox7);
            this.panel1.Controls.Add(this.textBox8);
            this.panel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(45, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 258);
            this.panel1.TabIndex = 22;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(457, 73);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(270, 21);
            this.textBox11.TabIndex = 21;
            this.textBox11.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox11_KeyPress);
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(126, 72);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(270, 21);
            this.textBox10.TabIndex = 20;
            this.textBox10.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox10_KeyPress);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(197, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "分段量程";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label8.Location = new System.Drawing.Point(49, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 14);
            this.label8.TabIndex = 19;
            this.label8.Text = "段0";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(536, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "分段测值";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Chocolate;
            this.button3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button3.Location = new System.Drawing.Point(490, 222);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 18;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(126, 99);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(270, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Chocolate;
            this.button2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.Location = new System.Drawing.Point(385, 222);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "修改";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(49, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "段1";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Chocolate;
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(279, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "添加";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(457, 100);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(270, 21);
            this.textBox2.TabIndex = 4;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(199, 14);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(528, 21);
            this.textBox9.TabIndex = 15;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(126, 126);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(270, 21);
            this.textBox3.TabIndex = 5;
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label7.Location = new System.Drawing.Point(49, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 25);
            this.label7.TabIndex = 14;
            this.label7.Text = "非线性关系名称";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(457, 127);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(270, 21);
            this.textBox4.TabIndex = 6;
            this.textBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox4_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label6.Location = new System.Drawing.Point(49, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 14);
            this.label6.TabIndex = 13;
            this.label6.Text = "段4";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(126, 155);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(270, 21);
            this.textBox5.TabIndex = 7;
            this.textBox5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox5_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label5.Location = new System.Drawing.Point(49, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 14);
            this.label5.TabIndex = 12;
            this.label5.Text = "段3";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(457, 156);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(270, 21);
            this.textBox6.TabIndex = 8;
            this.textBox6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox6_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new System.Drawing.Point(49, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 14);
            this.label4.TabIndex = 11;
            this.label4.Text = "段2";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(126, 182);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(270, 21);
            this.textBox7.TabIndex = 9;
            this.textBox7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox7_KeyPress);
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(457, 183);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(270, 21);
            this.textBox8.TabIndex = 10;
            this.textBox8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox8_KeyPress);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(880, 208);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Linear_manage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox4);
            this.Name = "Linear_manage";
            this.Size = new System.Drawing.Size(880, 539);
            this.Load += new System.EventHandler(this.Linear_manage_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        private void Linear_manage_Load(object sender, EventArgs e)
        {
            DataTable dt = GlobalParams.AllXianXingZhi.GetFeiXianXing();
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["moNiLiangMingCheng"].HeaderText = "名称";
            this.dataGridView1.Columns["liangCheng0"].HeaderText = "段0量程";
            this.dataGridView1.Columns["liangCheng1"].HeaderText = "段1量程";
            this.dataGridView1.Columns["liangCheng2"].HeaderText = "段2量程";
            this.dataGridView1.Columns["liangCheng3"].HeaderText = "段3量程";
            this.dataGridView1.Columns["liangCheng4"].HeaderText = "段4量程";
            this.dataGridView1.Columns["value0"].HeaderText = "段0测值";
            this.dataGridView1.Columns["value1"].HeaderText = "段1测值";
            this.dataGridView1.Columns["value2"].HeaderText = "段2测值";
            this.dataGridView1.Columns["value3"].HeaderText = "段3测值";
            this.dataGridView1.Columns["value4"].HeaderText = "段4测值";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
    }
}

