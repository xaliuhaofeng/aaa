namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Logout : Form
    {
        private Button button1;
        private Button button2;
        private IContainer components = null;
        public static bool exit = false;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;

        public Logout()
        {
            this.InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = Users.GetUsers();
            foreach (DataRow row in dt.Rows)
            {
                if ((row["Name"].ToString().TrimEnd(new char[0]) == this.textBox1.Text) && (row["Password"].ToString().TrimEnd(new char[0]) == this.textBox2.Text))
                {
                    exit = true;
                    Users.GlobalUserName = this.textBox1.Text;
                    break;
                }
            }
            if (exit)
            {
                Log.WriteLog(LogType.loginAndLogout, "用户" + this.textBox1.Text + "成功退出系统");
                Log.WriteLog(LogType.SystemClose, "系统结束运行，用户：" + this.textBox1.Text);
                base.Close();
                Application.ExitThread();
            }
            else
            {
                MessageBox.Show("密码输入错误，请重新输入或取消");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
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
            this.textBox1 = new TextBox();
            this.label2 = new Label();
            this.textBox2 = new TextBox();
            this.button1 = new Button();
            this.button2 = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0x3a, 0x25);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x34, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            this.textBox1.Enabled = false;
            this.textBox1.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox1.Location = new Point(0x80, 0x21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x80, 0x17);
            this.textBox1.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new Point(0x3a, 0x56);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x25, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "密码";
            this.textBox2.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox2.Location = new Point(0x80, 0x52);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new Size(0x80, 0x17);
            this.textBox2.TabIndex = 3;
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(0x3d, 0x83);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 4;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.BackColor = Color.Chocolate;
            this.button2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new Point(0xb5, 0x83);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 5;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new EventHandler(this.button2_Click);
            base.AcceptButton = this.button1;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(0x141, 0xc7);
            base.ControlBox = false;
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label1);
            base.Name = "Logout";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "退出登录";
            base.Load += new EventHandler(this.Logout_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void Logout_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = ClientConfig.CreateCommon().get("username");
        }
    }
}

