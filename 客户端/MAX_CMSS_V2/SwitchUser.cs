namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class SwitchUser : Form
    {
        private Button button1;
        private Button button2;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;

        public SwitchUser()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = this.textBox1.Text;
            if (user == string.Empty)
            {
                MessageBox.Show("请输入用户名后再点击确定！");
            }
            else
            {
                string password = this.textBox1.Text;
                if (password == string.Empty)
                {
                    MessageBox.Show("请输入密码后再点击确定！");
                }
                else
                {
                    DataTable dt = OperateDB.Select("select * from USERS where Name = '" + user + "' and Password = '" + password + "'");
                    if ((dt != null) && (dt.Rows.Count > 0))
                    {
                        Log.WriteLog(LogType.loginAndLogout, "用户" + Users.GlobalUserName + "成功退出系统");
                        string oldUserName = Users.GlobalUserName;
                        Users.GlobalUserName = user;
                        Users.UserType = (UserType) Convert.ToInt32(dt.Rows[0]["theLevel"]);
                        Log.WriteLog(LogType.loginAndLogout, "用户" + user + "成功登录系统");
                        Log.WriteLog(LogType.SystemUserChange, "用户切换成功！从" + oldUserName + "切换到" + user);
                        ClientConfig.CreateCommon().set("username", user);
                        MessageBox.Show("用户切换成功！");
                        base.Close();
                    }
                    else
                    {
                        MessageBox.Show("用户切换失败！");
                    }
                }
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
            this.button1 = new Button();
            this.textBox1 = new TextBox();
            this.label2 = new Label();
            this.textBox2 = new TextBox();
            this.button2 = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(60, 0x2b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            this.button1.Location = new Point(0x49, 0x89);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.textBox1.Location = new Point(0x74, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x7c, 0x15);
            this.textBox1.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x3e, 0x53);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "密码";
            this.textBox2.Location = new Point(0x74, 80);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new Size(0x7c, 0x15);
            this.textBox2.TabIndex = 4;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new Point(0xa5, 0x89);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 5;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            base.AcceptButton = this.button1;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.CancelButton = this.button2;
            base.ClientSize = new Size(0x121, 0xbd);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.label1);
            base.Name = "SwitchUser";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

