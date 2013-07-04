namespace MAX_CMSS_V2
{
    using Logic;
    using MAX_CMSS_V2.voice;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Diagnostics;

    public class voiceAlarmUC : UserControl
    {
        private Button button1;
        private Button button2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private IContainer components = null;
        private ClientConfig config;
        private MainForm fm;
        private Label label1;
        private TextBox textBox1;

        public voiceAlarmUC(MainForm fm)
        {
            this.InitializeComponent();
            this.config = ClientConfig.CreateCommon();
            this.fm = fm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s;
            if (this.checkBox1.Checked)
            {
                s = "true";
            }
            else
            {
                s = "false";
            }
            this.config.set("yuYinBaoJing", s);
            GlobalParams.yuyinAlalrm = this.checkBox1.Checked;
            this.fm.setYuYinBaoJin(this.checkBox1.Checked);

            bool isPreYY = GlobalParams.sgAlarm;
            if (this.checkBox2.Checked)
            {
                s = "true";
            }
            else
            {
                s = "false";
            }
            this.config.set("sgBaojing", s);
            GlobalParams.sgAlarm = this.checkBox2.Checked;
            if ((isPreYY) && (!GlobalParams.sgAlarm))
            {
                if (GlobalParams.sgAlarmState)
                {
                    string workdirectory;
                    Exception eo;
                    workdirectory = Application.StartupPath + @"\声光报警\AlarmLight.exe";
                    try
                    {
                        Process.Start(workdirectory, @" 0");
                        GlobalParams.sgAlarmState = false;
                    }
                    catch (Exception exception2)
                    {
                        eo = exception2;
                        GlobalParams.sgAlarmState = true;
                        MessageBox.Show("声光报警程序出错！原因" + eo.Message);
                    }
                }

            }
            MessageBox.Show("报警设置成功！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string say = this.textBox1.Text.Trim();
            if (!(say == ""))
            {
                voiceAlarmClass.SaySome(say);
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
            this.button2 = new Button();
            this.button1 = new Button();
            this.checkBox1 = new CheckBox();
            this.textBox1 = new TextBox();
            this.label1 = new Label();
            this.checkBox2 = new CheckBox();
            base.SuspendLayout();
            this.button2.BackColor = Color.Chocolate;
            this.button2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button2.ForeColor = SystemColors.ButtonFace;
            this.button2.Location = new Point(0x20e, 0xa8);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 5;
            this.button2.Text = "测试";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = SystemColors.ButtonFace;
            this.button1.Location = new Point(0x180, 0x113);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 4;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.checkBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.checkBox1.Location = new Point(0xc1, 0xfc);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x74, 0x12);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "启动语音报警";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.textBox1.Location = new Point(0xb8, 0xa9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x13d, 0x15);
            this.textBox1.TabIndex = 6;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0x75, 0xad);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x43, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "效果测试";
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.checkBox2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.checkBox2.Location = new Point(0xc1, 0x125);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new Size(0x74, 0x12);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "启动声光告警";
            this.checkBox2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.checkBox2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.checkBox1);
            base.Name = "voiceAlarmUC";
            base.Size = new Size(0x2d8, 0x1d5);
            base.Load += new EventHandler(this.voiceAlarm_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void voiceAlarm_Load(object sender, EventArgs e)
        {
            this.checkBox1.Checked = Convert.ToBoolean(this.config.get("yuYinBaoJing"));
            this.checkBox2.Checked = GlobalParams.sgAlarm;
        }
    }
}

