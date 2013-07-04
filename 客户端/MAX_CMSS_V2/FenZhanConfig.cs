namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class FenZhanConfig : UserControl
    {
        private Button button1;
        private Button button2;
        private ComboBox comboBox1;
        private IContainer components = null;
        public Thread InitThread;
        private Label label1;
        private Label label2;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RichTextBox richTextBox1;

        public FenZhanConfig(string buttonText)
        {
            this.InitializeComponent();
            this.button1.Text = buttonText;
            if (buttonText == "设置")
            {
                this.radioButton1.Visible = this.radioButton2.Visible = this.label2.Visible = true;
            }
            else
            {
                this.radioButton1.Visible = this.radioButton2.Visible = this.label2.Visible = false;
            }
            if (buttonText == "确定")
            {
                this.button2.Visible = true;
            }
            else
            {
                this.button2.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte fenZhanHao;
            if (this.button1.Text == "确定")
            {
                fenZhanHao = Convert.ToByte(this.comboBox1.SelectedItem);
                UDPComm.Send(FenZhan.GetFenZhanConfigInfo(fenZhanHao));
                GlobalParams.AllfenZhans[fenZhanHao].commState = 3;
                string sql = "update UpLoadSwitch set IsUpdate=1";
                OperateDB.Execute(sql);
            }
            else
            {
                byte[] b;
                if (this.button1.Text == "校时")
                {
                    fenZhanHao = Convert.ToByte(this.comboBox1.SelectedItem);
                    b = FenZhan.JiaoShi(fenZhanHao);
                    Log.WriteLog(LogType.JiaoShi, string.Concat(new object[] { fenZhanHao, "#$", DateTime.Now, "#$启动校时" }));
                    UDPComm.Send(b);
                }
                else if (this.button1.Text == "通信测试")
                {
                    UDPComm.Send(FenZhan.CommTest(Convert.ToByte(this.comboBox1.SelectedItem)));
                }
                else if (this.button1.Text == "设置")
                {
                    fenZhanHao = Convert.ToByte(this.comboBox1.SelectedItem);
                    b = FenZhan.GuZhangBiSuo(fenZhanHao, this.radioButton1.Checked);
                    if (this.radioButton1.Checked)
                    {
                        if (OperateDB.Select("select * from GuZhangBiSuo where fenZhanHao=" + fenZhanHao).Rows.Count <= 0)
                        {
                            OperateDB.Execute("insert into GuZhangBiSuo(fenZhanHao) values(" + fenZhanHao + ")");
                        }
                        this.radioButton2.Checked = true;
                        this.radioButton1.Checked = false;
                        Log.WriteLog(LogType.GuZhangBiSuo, fenZhanHao + "#$使能故障闭锁#$");
                    }
                    else
                    {
                        if (OperateDB.Select("select * from GuZhangBiSuo where fenZhanHao=" + fenZhanHao).Rows.Count > 0)
                        {
                            OperateDB.Execute("delete from GuZhangBiSuo where fenZhanHao=" + fenZhanHao);
                        }
                        this.radioButton1.Checked = true;
                        this.radioButton2.Checked = false;
                        Log.WriteLog(LogType.GuZhangBiSuo, fenZhanHao + "#$屏蔽故障闭锁#$");
                    }
                    UDPComm.Send(b);
                }
                else if (this.button1.Text == "重启")
                {
                    UDPComm.Send(FenZhan.YuanChengDianYuanGuanLi(Convert.ToByte(this.comboBox1.SelectedItem)));
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.InitThread = new Thread(new ThreadStart(this.InitAllOldFenZhan));
            this.InitThread.IsBackground = true;
            this.InitThread.Start();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.button1.Text == "设置")
            {
                byte fenZhanHao = Convert.ToByte(this.comboBox1.SelectedItem);
                if (OperateDB.Select("select * from GuZhangBiSuo where fenZhanHao=" + fenZhanHao).Rows.Count > 0)
                {
                    this.radioButton2.Checked = true;
                    this.radioButton1.Checked = false;
                }
                else
                {
                    this.radioButton1.Checked = true;
                    this.radioButton2.Checked = false;
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

        private void FenZhanConfig_Load(object sender, EventArgs e)
        {
            string[] fenzhans = FenZhan.GetAllConfigedFenZhan();
            this.comboBox1.Items.AddRange(fenzhans);
            this.comboBox1.SelectedIndex = 0;
        }

        private void InitAllOldFenZhan()
        {
            int fenzhanNo = 0;
            while (true)
            {
                fenzhanNo++;
                if (fenzhanNo > 60)
                {
                    return;
                }
                if ((GlobalParams.AllfenZhans[fenzhanNo].CommPort < 5) && (GlobalParams.AllfenZhans[fenzhanNo].CommPort > 0))
                {
                    UDPComm.Send(FenZhan.GetFenZhanConfigInfo((byte) fenzhanNo));
                    Thread.Sleep(600);
                }
            }
        }

        private void InitializeComponent()
        {
            this.comboBox1 = new ComboBox();
            this.button1 = new Button();
            this.label1 = new Label();
            this.radioButton1 = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.richTextBox1 = new RichTextBox();
            this.label2 = new Label();
            this.button2 = new Button();
            base.SuspendLayout();
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(0xd7, 0x61);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x79, 20);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(0x15d, 0x5f);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x6a, 0x17);
            this.button1.TabIndex = 1;
            this.button1.Text = "初始化分站";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0x7f, 0x63);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x52, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "选择分站号";
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Enabled = false;
            this.radioButton1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.radioButton1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.radioButton1.Location = new Point(0x81, 0x93);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0x73, 0x12);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "使能故障闭锁";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
            this.radioButton2.AutoSize = true;
            this.radioButton2.Enabled = false;
            this.radioButton2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.radioButton2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.radioButton2.Location = new Point(0x15d, 0x93);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x73, 0x12);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.Text = "关闭故障闭锁";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.richTextBox1.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
            this.richTextBox1.Location = new Point(3, 0xab);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new Size(0x274, 0xaf);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 10.5f, FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new Point(0x1dc, 100);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x70, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "当前状态：关闭";
            this.button2.BackColor = Color.Chocolate;
            this.button2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new Point(0x1f3, 0x5e);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x84, 0x1b);
            this.button2.TabIndex = 8;
            this.button2.Text = "初始化所有分站";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            this.button2.Click += new EventHandler(this.button2_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.button2);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.richTextBox1);
            base.Controls.Add(this.radioButton2);
            base.Controls.Add(this.radioButton1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.comboBox1);
            base.Name = "FenZhanConfig";
            base.Size = new Size(0x27a, 0x15d);
            base.Load += new EventHandler(this.FenZhanConfig_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
            {
                this.label2.Text = "当前状态：关闭";
            }
            else
            {
                this.label2.Text = "当前状态：开启";
            }
        }

        public void ShowInListView(FenZhanRTdata ud)
        {
            if (ud.isResponse)
            {
                if (this.richTextBox1.Text.Length > 200)
                {
                    this.richTextBox1.Text = "";
                }
                if (ud.responseInfo.Length > 0)
                {
                    this.richTextBox1.Text = this.richTextBox1.Text + ud.responseInfo;
                    this.richTextBox1.Text = this.richTextBox1.Text + "\r\n";
                    ud.responseInfo = "";
                }
            }
        }
    }
}

