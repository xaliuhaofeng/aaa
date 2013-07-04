namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Windows.Forms;

    public class Database_manage : UserControl
    {
        private Button button1;
        private Button button2;
        private IContainer components = null;
        private GroupBox groupBox2;
        private Label label15;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Panel panel2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private TextBox textBox6;

        public Database_manage()
        {
            this.InitializeComponent();
        }

        private bool arguCheck()
        {
            if (this.textBox3.Text == string.Empty)
            {
                MessageBox.Show("数据库名称不能为空！");
                this.textBox3.Focus();
                return false;
            }
            if (this.textBox4.Text == string.Empty)
            {
                MessageBox.Show("数据库地址不能为空！");
                this.textBox4.Focus();
                return false;
            }
            if (this.textBox5.Text == string.Empty)
            {
                MessageBox.Show("数据库用户名不能为空！");
                this.textBox5.Focus();
                return false;
            }
            if (this.textBox6.Text == string.Empty)
            {
                MessageBox.Show("数据库密码不能为空！");
                this.textBox6.Focus();
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.checkConnection())
            {
                MessageBox.Show("成功连上数据库！恭喜！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.checkConnection())
            {
                this.saveConnection();
            }
        }

        private bool checkConnection()
        {
            if (!this.arguCheck())
            {
                return false;
            }
            string dbName = this.textBox4.Text.Trim();
            string dbAddress = this.textBox6.Text.Trim();
            string dbUserName = this.textBox3.Text.Trim();
            string dbPassword = this.textBox5.Text.Trim();
            string connectionString = "server=" + dbAddress + ";database=" + dbName + ";uid=" + dbUserName + ";pwd=" + dbPassword;
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("给定的数据库连接配置无法连上数据库，请检查参数！" + e.ToString());
                return false;
            }
            return true;
        }

        private void Database_manage_Load(object sender, EventArgs e)
        {
            ClientConfig config = ClientConfig.CreateCommon();
            this.textBox3.Text = config.get("dbUserName");
            this.textBox4.Text = config.get("dbName");
            this.textBox5.Text = config.get("dbPassword");
            this.textBox6.Text = config.get("dbAddress");
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
            this.groupBox2 = new GroupBox();
            this.button1 = new Button();
            this.button2 = new Button();
            this.textBox5 = new TextBox();
            this.label5 = new Label();
            this.label6 = new Label();
            this.textBox6 = new TextBox();
            this.textBox3 = new TextBox();
            this.label4 = new Label();
            this.label3 = new Label();
            this.textBox4 = new TextBox();
            this.panel2 = new Panel();
            this.label15 = new Label();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBox6);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Location = new Point(0, 0x68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x360, 0x159);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(0x150, 0x132);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 14;
            this.button1.Text = "测试连接";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.BackColor = Color.Chocolate;
            this.button2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new Point(0x1c8, 0x132);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 15;
            this.button2.Text = "确定";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.textBox5.Location = new Point(0x150, 0xe3);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Size(450, 0x15);
            this.textBox5.TabIndex = 13;
            this.textBox5.UseSystemPasswordChar = true;
            this.label5.AutoSize = true;
            this.label5.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label5.Location = new Point(0xc0, 0x6c);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x7f, 14);
            this.label5.TabIndex = 13;
            this.label5.Text = "数据库服务器地址";
            this.label6.AutoSize = true;
            this.label6.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label6.Location = new Point(0xc0, 0xe7);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x52, 14);
            this.label6.TabIndex = 15;
            this.label6.Text = "数据库密码";
            this.textBox6.Location = new Point(0x150, 0x68);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Size(450, 0x15);
            this.textBox6.TabIndex = 11;
            this.textBox3.Location = new Point(0x150, 0xa5);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(450, 0x15);
            this.textBox3.TabIndex = 12;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new Point(0xc0, 50);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x52, 14);
            this.label4.TabIndex = 9;
            this.label4.Text = "数据库名称";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new Point(0xc0, 0xa9);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x61, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "数据库用户名";
            this.textBox4.Location = new Point(0x150, 0x2e);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Size(450, 0x15);
            this.textBox4.TabIndex = 10;
            this.panel2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel2.AutoSize = true;
            this.panel2.BackColor = Color.AliceBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label15);
            this.panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel2.Location = new Point(0, 0x30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x363, 0x1d);
            this.panel2.TabIndex = 40;
            this.label15.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label15.Location = new Point(0x29, 1);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x89, 0x1a);
            this.label15.TabIndex = 8;
            this.label15.Text = "数据库配置管理";
            this.label15.TextAlign = ContentAlignment.MiddleLeft;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.groupBox2);
            base.Name = "Database_manage";
            base.Size = new Size(0x363, 0x1d0);
            base.Load += new EventHandler(this.Database_manage_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void saveConnection()
        {
            string dbName = this.textBox4.Text.Trim();
            string dbAddress = this.textBox6.Text.Trim();
            string dbUserName = this.textBox3.Text.Trim();
            string dbPassword = this.textBox5.Text.Trim();
            ClientConfig config = ClientConfig.CreateCommon();
            config.add("dbName", dbName);
            config.add("dbAddress", dbAddress);
            config.add("dbUserName", dbUserName);
            config.add("dbPassword", dbPassword);
            MessageBox.Show("数据库配置保存成功！");
        }
    }
}

