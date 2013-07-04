namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class User_manage : UserControl
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private ComboBox comboBox1;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private byte flag = 0;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private DataGridViewTextBoxColumn Password;
        private string temp = "";
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private DataGridViewTextBoxColumn theLevel;
        private DataGridViewTextBoxColumn theLevel2;
        private DataGridViewTextBoxColumn UserName;

        public User_manage()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length == 0)
            {
                MessageBox.Show("请输入用户名");
            }
            else if (this.textBox2.Text.Length == 0)
            {
                MessageBox.Show("请输入密码");
            }
            else if (this.textBox3.Text != this.textBox2.Text)
            {
                MessageBox.Show("前后密码不一致，请重新输入");
            }
            else if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择用户权限");
            }
            else if (OperateDB.Select(Users.CountUsers(this.textBox1.Text)).Rows.Count > 0)
            {
                MessageBox.Show("该用户名已存在，请重新命名");
                this.textBox1.Focus();
            }
            else
            {
                OperateDB.Execute(Users.CreateUsers(this.textBox1.Text, this.textBox2.Text, this.comboBox1.SelectedIndex.ToString()));
                this.textBox1.Clear();
                this.textBox2.Clear();
                this.textBox3.Clear();
                this.comboBox1.SelectedIndex = -1;
                this.dataLoad();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.flag == 0)
            {
                MessageBox.Show("请选择要修改的用户");
            }
            else if (this.textBox1.Text.Length == 0)
            {
                MessageBox.Show("请输入用户名");
            }
            else if (this.textBox2.Text.Length == 0)
            {
                MessageBox.Show("请输入密码");
            }
            else if (this.textBox3.Text != this.textBox2.Text)
            {
                MessageBox.Show("前后密码不一致，请重新输入");
            }
            else if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择用户权限");
            }
            else if ((OperateDB.Select(Users.CountUsers(this.textBox1.Text)).Rows.Count > 0) && (this.textBox1.Text != this.temp))
            {
                MessageBox.Show("该用户名已存在，请重新命名");
            }
            else
            {
                if ((this.temp == Users.GlobalUserName) && (this.textBox1.Text != this.temp))
                {
                    MessageBox.Show("无法更改当前用户的名称");
                }
                else
                {
                    OperateDB.Execute(Users.UpdateUsers(this.textBox1.Text, this.textBox2.Text, this.comboBox1.SelectedIndex.ToString(), this.temp));
                }
                this.dataLoad();
                this.textBox1.Clear();
                this.textBox2.Clear();
                this.textBox3.Clear();
                this.comboBox1.SelectedIndex = -1;
                this.flag = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.flag == 0)
            {
                MessageBox.Show("请选择要删除的用户");
            }
            else if (this.flag == 1)
            {
                if (this.dataGridView1.CurrentRow.Cells["UserName"].Value.ToString() == Users.GlobalUserName)
                {
                    MessageBox.Show("无法删除当前用户");
                }
                else
                {
                    OperateDB.Execute(Users.DelUsers(this.dataGridView1.CurrentRow.Cells["UserName"].Value.ToString()));
                    this.dataLoad();
                }
                this.flag = 0;
                this.textBox3.Clear();
                this.textBox1.Clear();
                this.textBox2.Clear();
                this.comboBox1.SelectedIndex = -1;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.CurrentRow != null)
            {
                this.flag = 1;
                this.textBox1.Text = this.dataGridView1.CurrentRow.Cells["UserName"].Value.ToString();
                this.textBox2.Text = this.dataGridView1.CurrentRow.Cells["Password"].Value.ToString();
                this.textBox3.Text = this.dataGridView1.CurrentRow.Cells["Password"].Value.ToString();
                this.comboBox1.SelectedIndex = int.Parse(this.dataGridView1.CurrentRow.Cells["theLevel"].Value.ToString());
                this.temp = this.textBox1.Text;
            }
        }

        private void dataLoad()
        {
            this.dataGridView1.Rows.Clear();
            DataTable dt1 = Users.GetLevel(Users.GlobalUserName);
            if (dt1.Rows.Count != 0)
            {
                int index2;
                if (dt1.Rows[0]["theLevel"].ToString() == "0")
                {
                    this.button1.Enabled = true;
                    this.button3.Enabled = true;
                    this.comboBox1.Enabled = true;
                    DataTable dt = Users.GetUsers();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Name"].ToString() != "admin")
                        {
                            index2 = this.dataGridView1.Rows.Add();
                            this.dataGridView1.Rows[index2].Cells["UserName"].Value = row["Name"].ToString();
                            this.dataGridView1.Rows[index2].Cells["Password"].Value = row["Password"].ToString();
                            this.dataGridView1.Rows[index2].Cells["theLevel"].Value = row["theLevel"].ToString();
                            if (row["theLevel"].ToString() == "0")
                            {
                                this.dataGridView1.Rows[index2].Cells["theLevel2"].Value = "超级管理员";
                            }
                            else if (row["theLevel"].ToString() == "1")
                            {
                                this.dataGridView1.Rows[index2].Cells["theLevel2"].Value = "监控员";
                            }
                            else
                            {
                                this.dataGridView1.Rows[index2].Cells["theLevel2"].Value = "浏览客户端";
                            }
                        }
                    }
                }
                else
                {
                    this.comboBox1.SelectedIndex = int.Parse(dt1.Rows[0]["theLevel"].ToString());
                    index2 = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index2].Cells["UserName"].Value = dt1.Rows[0]["Name"].ToString();
                    this.dataGridView1.Rows[index2].Cells["Password"].Value = dt1.Rows[0]["Password"].ToString();
                    this.dataGridView1.Rows[index2].Cells["theLevel"].Value = dt1.Rows[0]["theLevel"].ToString();
                    if (dt1.Rows[0]["theLevel"].ToString() == "0")
                    {
                        this.dataGridView1.Rows[index2].Cells["theLevel2"].Value = "超级管理员";
                    }
                    else if (dt1.Rows[0]["theLevel"].ToString() == "1")
                    {
                        this.dataGridView1.Rows[index2].Cells["theLevel2"].Value = "监控员";
                    }
                    else
                    {
                        this.dataGridView1.Rows[index2].Cells["theLevel2"].Value = "浏览客户端";
                    }
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.theLevel2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.theLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserName,
            this.Password,
            this.theLevel2,
            this.theLevel});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(821, 270);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // UserName
            // 
            this.UserName.HeaderText = "用户名";
            this.UserName.Name = "UserName";
            this.UserName.ReadOnly = true;
            // 
            // Password
            // 
            this.Password.HeaderText = "密码";
            this.Password.Name = "Password";
            this.Password.ReadOnly = true;
            this.Password.Visible = false;
            // 
            // theLevel2
            // 
            this.theLevel2.HeaderText = "权限";
            this.theLevel2.Name = "theLevel2";
            this.theLevel2.ReadOnly = true;
            // 
            // theLevel
            // 
            this.theLevel.HeaderText = "权限";
            this.theLevel.Name = "theLevel";
            this.theLevel.ReadOnly = true;
            this.theLevel.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox1.Location = new System.Drawing.Point(0, 307);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(804, 162);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用户信息";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "超级管理员",
            "监控员",
            "浏览客户端"});
            this.comboBox1.Location = new System.Drawing.Point(141, 112);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(202, 24);
            this.comboBox1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new System.Drawing.Point(64, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "用户权限";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(419, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "重复用户密码";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(531, 111);
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '*';
            this.textBox3.Size = new System.Drawing.Size(192, 26);
            this.textBox3.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(419, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "用户密码";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(531, 60);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(192, 26);
            this.textBox2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(64, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(140, 60);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(203, 26);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Chocolate;
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(249, 483);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "添加";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Chocolate;
            this.button2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.Location = new System.Drawing.Point(364, 483);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "修改";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Chocolate;
            this.button3.Enabled = false;
            this.button3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button3.Location = new System.Drawing.Point(481, 483);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // User_manage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "User_manage";
            this.Size = new System.Drawing.Size(821, 528);
            this.Load += new System.EventHandler(this.User_manage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void User_manage_Load(object sender, EventArgs e)
        {
            this.dataLoad();
        }
    }
}

