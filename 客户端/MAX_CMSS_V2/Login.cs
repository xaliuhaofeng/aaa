namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Login : Form
    {
        private Button button1;
        private Button button2;
        public static bool closeReason = false;
        private IContainer components = null;
        private byte flag = 0;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;

        public Login()
        {
            this.InitializeComponent();
            DuanDianGuanXi.getoldkjnacedian();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = Users.GetUsers();
            foreach (DataRow row in dt.Rows)
            {
                if ((row["Name"].ToString().TrimEnd(new char[0]) == this.textBox1.Text) && (row["Password"].ToString().TrimEnd(new char[0]) == this.textBox2.Text))
                {
                    this.flag = 1;
                    Users.GlobalUserName = this.textBox1.Text;
                    Users.UserType = (UserType) Convert.ToInt32(row["theLevel"]);
                    ClientConfig.CreateCommon().set("username", this.textBox1.Text);
                    Log.WriteLog(LogType.loginAndLogout, "用户" + this.textBox1.Text + "成功登录系统");
                    Log.WriteLog(LogType.SystemOpen, "系统开始运行，用户：" + this.textBox1.Text);
                    break;
                }
            }
            if (this.flag == 1)
            {
                closeReason = true;
                base.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码输入错误，请重新输入或退出");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Application.ExitThread();
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
            this.label2 = new Label();
            this.button1 = new Button();
            this.button2 = new Button();
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0x3a, 0x25);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x34, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new Point(0x3a, 0x56);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x25, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码";
            this.button1.BackColor = Color.Chocolate;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(0x3d, 0x83);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.BackColor = Color.Chocolate;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new Point(0xb5, 0x83);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 3;
            this.button2.Text = "退出";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.textBox1.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox1.Location = new Point(0x80, 0x21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x80, 0x17);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.textBox2.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox2.Location = new Point(0x80, 0x52);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new Size(0x80, 0x17);
            this.textBox2.TabIndex = 5;
            base.AcceptButton = this.button1;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.CancelButton = this.button2;
            base.ClientSize = new Size(0x141, 0xc7);
            base.ControlBox = false;
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "Login";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            base.Load += new EventHandler(this.Login_Load);
            base.FormClosing += new FormClosingEventHandler(this.Login_FormClosing);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closeReason)
            {
                e.Cancel = true;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}

