namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Control : UserControl
    {
        private Button btnClear;
        private Button btnConfirm;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private ComboBox comboBox1;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private PictureBox pictureBox1;
        private string temp = "";
        private TextBox textBox1;
        private TextBox textBox2;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private CheckBox checkBox1;
        private TextBox textBox3;

        public Control()
        {
            this.InitializeComponent();
        }

        private bool arguCheck()
        {
            if (this.textBox3.Text.Length == 0)
            {
                MessageBox.Show("请输入控制量名称");
                return false;
            }
            if (this.textBox1.Text.Length == 0)
            {
                MessageBox.Show("请输入0态名称");
                return false;
            }
            if (this.textBox2.Text.Length == 0)
            {
                MessageBox.Show("请输入1态名称");
                return false;
            }
            if (this.comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("请选择控制量类型");
                return false;
            }
            return true;
        }

        private void boxClear()
        {
            this.textBox3.Clear();
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.pictureBox1.Image = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.boxClear();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.arguCheck())
            {
                if (GlobalParams.AllkzlLeiXing.IsExistKzl(this.textBox3.Text.ToString().Trim()))
                {
                    MessageBox.Show("该控制量名称已存在，请重新命名");
                    this.textBox3.Focus();
                }
                else
                {
                    GlobalParams.AllkzlLeiXing.InsertKzl(this.textBox3.Text.ToString().Trim(), this.textBox1.Text.ToString().Trim(), this.textBox2.Text.ToString().Trim(), (this.comboBox1.SelectedIndex + 1).ToString());
                    DataTable dt = GlobalParams.AllkzlLeiXing.ListConvertDataTable();
                    dt.Columns.Add("kongZhiLiangLeiXing2");
                    foreach (DataRow row in dt.Rows)
                    {
                        switch (row["kongZhiLiangLeiXing"].ToString())
                        {
                            case "1":
                                row["kongZhiLiangLeiXing2"] = "常开";
                                break;

                            case "2":
                                row["kongZhiLiangLeiXing2"] = "常闭";
                                break;

                            case "3":
                                row["kongZhiLiangLeiXing2"] = "电平";
                                break;
                        }
                    }
                    this.dataGridView1.DataSource = dt;
                    this.boxClear();
                    GlobalParams.setDataState();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgP = new OpenFileDialog {
                CheckFileExists = true,
                Filter = "jpg files (*.jpg)|*.jpg",
                DefaultExt = ".jpg",
                Title = "打开图片文件"
            };
            if (dlgP.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.Image = Image.FromFile(dlgP.FileName);
                this.pictureBox1.ImageLocation = dlgP.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.arguCheck())
            {
                if ((this.textBox3.Text != this.temp) && GlobalParams.AllkzlLeiXing.IsExistKzl(this.textBox3.Text.ToString().Trim()))
                {
                    MessageBox.Show("该控制量名称已存在，请重新命名");
                    this.textBox3.Focus();
                }
                else
                {
                    int Reflector0001 = this.comboBox1.SelectedIndex + 1;
                    GlobalParams.AllkzlLeiXing.UpdataKzl(this.textBox3.Text.ToString().Trim(), this.textBox1.Text.ToString().Trim(), this.textBox2.Text.ToString().Trim(), Reflector0001.ToString().Trim(), this.temp);
                    DataTable dt = GlobalParams.AllkzlLeiXing.ListConvertDataTable();
                    dt.Columns.Add("kongZhiLiangLeiXing2");
                    foreach (DataRow row in dt.Rows)
                    {
                        switch (row["kongZhiLiangLeiXing"].ToString())
                        {
                            case "1":
                                row["kongZhiLiangLeiXing2"] = "常开";
                                break;

                            case "2":
                                row["kongZhiLiangLeiXing2"] = "常闭";
                                break;

                            case "3":
                                row["kongZhiLiangLeiXing2"] = "电平";
                                break;
                        }
                    }
                    this.dataGridView1.DataSource = dt;
                    this.boxClear();
                    GlobalParams.setDataState();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow != null)
            {
                if (GlobalParams.AllkzlLeiXing.IsGuanLianCeDian(this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString().Trim()))
                {
                    MessageBox.Show("存在相关联的控制量测点，不能删除该类型！");
                }
                else if (MessageBox.Show("你确认要删除这个控制量类型吗？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    GlobalParams.AllkzlLeiXing.DeleteKzl(this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString());
                    DataTable dt = GlobalParams.AllkzlLeiXing.ListConvertDataTable();
                    dt.Columns.Add("kongZhiLiangLeiXing2");
                    foreach (DataRow row in dt.Rows)
                    {
                        switch (row["kongZhiLiangLeiXing"].ToString())
                        {
                            case "1":
                                row["kongZhiLiangLeiXing2"] = "常开";
                                break;

                            case "2":
                                row["kongZhiLiangLeiXing2"] = "常闭";
                                break;

                            case "3":
                                row["kongZhiLiangLeiXing2"] = "电平";
                                break;
                        }
                    }
                    this.dataGridView1.DataSource = dt;
                    this.boxClear();
                    GlobalParams.setDataState();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            base.Hide();
        }

        private void Control_Load(object sender, EventArgs e)
        {
            DataTable dt = GlobalParams.AllkzlLeiXing.ListConvertDataTable();
            dt.Columns.Add("kongZhiLiangLeiXing2");
            foreach (DataRow row in dt.Rows)
            {
                switch (row["kongZhiLiangLeiXing"].ToString())
                {
                    case "1":
                        row["kongZhiLiangLeiXing2"] = "常开";
                        break;

                    case "2":
                        row["kongZhiLiangLeiXing2"] = "常闭";
                        break;

                    case "3":
                        row["kongZhiLiangLeiXing2"] = "电平";
                        break;
                }
            }
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["mingCheng"].HeaderText = "控制量名称";
            this.dataGridView1.Columns["lingTaiMingCheng"].HeaderText = "0态名称";
            this.dataGridView1.Columns["yiTaiMingCheng"].HeaderText = "1态名称";
            this.dataGridView1.Columns["kongZhiLiangLeiXing2"].HeaderText = "控制量类型";
            this.dataGridView1.Columns["kongZhiLiangLeiXing"].Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Rows.Count != 0)
            {
                this.textBox3.Text = this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString();
                this.textBox1.Text = this.dataGridView1.CurrentRow.Cells["lingTaiMingCheng"].Value.ToString();
                this.textBox2.Text = this.dataGridView1.CurrentRow.Cells["yiTaiMingCheng"].Value.ToString();
                this.comboBox1.SelectedIndex = int.Parse(this.dataGridView1.CurrentRow.Cells["kongZhiLiangLeiXing"].Value.ToString()) - 1;
                this.temp = this.textBox3.Text;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(158, 336);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(736, 218);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(437, 157);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(59, 20);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "报警";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton2.Location = new System.Drawing.Point(621, 157);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(50, 20);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "1态";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(138, 149);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(63, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton1.Location = new System.Drawing.Point(520, 157);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(50, 20);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "0态";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Chocolate;
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(223, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 27);
            this.button1.TabIndex = 11;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(90, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "图标";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "常开",
            "常闭",
            "电平"});
            this.comboBox1.Location = new System.Drawing.Point(138, 93);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(227, 24);
            this.comboBox1.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(42, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "控制量类型";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(138, 38);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(227, 26);
            this.textBox3.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(90, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "名称";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(505, 92);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(191, 26);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(432, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "1态名称";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(505, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(191, 26);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(432, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "0态名称";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Chocolate;
            this.button4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(914, 577);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(87, 27);
            this.button4.TabIndex = 22;
            this.button4.Text = "关闭";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1042, 303);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Control;
            this.button3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button3.Location = new System.Drawing.Point(603, 577);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 30);
            this.button3.TabIndex = 21;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.SystemColors.Control;
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnConfirm.Location = new System.Drawing.Point(244, 577);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(87, 30);
            this.btnConfirm.TabIndex = 19;
            this.btnConfirm.Text = "添加";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.Control;
            this.btnClear.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnClear.Location = new System.Drawing.Point(783, 577);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 30);
            this.btnClear.TabIndex = 20;
            this.btnClear.Text = "重置";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Location = new System.Drawing.Point(423, 577);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 30);
            this.button2.TabIndex = 23;
            this.button2.Text = "修改";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Control";
            this.Size = new System.Drawing.Size(1042, 636);
            this.Load += new System.EventHandler(this.Control_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.radioButton1.Enabled = this.checkBox1.Checked;
            this.radioButton2.Enabled = this.checkBox1.Checked;
        }
    }
}

