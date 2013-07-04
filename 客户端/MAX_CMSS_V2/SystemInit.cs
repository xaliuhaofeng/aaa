namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class SystemInit : UserControl
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private ComboBox comboBox1;
        private IContainer components = null;
        private Label label1;
        private RichTextBox richTextBox1;

        public SystemInit()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("清除硬件所有配置，是否确定执行硬件初始化？", null, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (this.comboBox1.SelectedIndex >= 0)
                {
                    byte fenZhanHao = Convert.ToByte(this.comboBox1.SelectedItem);
                    this.initHardware(fenZhanHao);
                }
                else
                {
                    MessageBox.Show("请选择要初始化的分站！");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("清除数据库中所有此分站的配置，是否继续？", null, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (this.comboBox1.SelectedIndex >= 0)
                {
                    byte fenZhanHao = Convert.ToByte(this.comboBox1.SelectedItem);
                    this.initFenZhan(fenZhanHao);
                    MainFormRef.updateMainForm();
                    MessageBox.Show("执行软件初始化完成！");
                }
                else
                {
                    MessageBox.Show("请选择要初始化的分站！");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("系统初始化将删除所有数据，是否继续？", null, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.richTextBox1.Text = this.richTextBox1.Text + "开始初始化。。。。。。\n";
                this.richTextBox1.Text = this.richTextBox1.Text + "停止与服务器交互。。。。\n";
                this.richTextBox1.Text = this.richTextBox1.Text + "清除历史数据。。。。。\n";
                this.deleteHistoryData();
                this.richTextBox1.Text = this.richTextBox1.Text + "清除断电关系和馈电关系。。。。。\n";
                this.deleteDuanDianAndKuiDian();
                this.richTextBox1.Text = this.richTextBox1.Text + "清除控制量测点。。。。。\n";
                this.deleteKongZhiLiangCeDian();
                this.richTextBox1.Text = this.richTextBox1.Text + "清除控制量类型。。。。。\n";
                this.deleteKongZhiLiangLeiXing();
                this.richTextBox1.Text = this.richTextBox1.Text + "清除测点信息。。。。\n";
                this.deleteCeDian();
                this.richTextBox1.Text = this.richTextBox1.Text + "清除测点类型。。。。。\n";
                this.deleteCeDianLeiXing();
                this.richTextBox1.Text = this.richTextBox1.Text + "清除客户端的配置信息。。。。。\n";
                this.deleteClientConfig();
                this.richTextBox1.Text = this.richTextBox1.Text + "清除其它信息。。。。。。\n";
                this.deleteOther();
                this.richTextBox1.Text = this.richTextBox1.Text + "初始化系统用户。。。。。。\n";
                this.InitUser();
                File.Delete(Application.StartupPath + @"\monitor\cfg.xml");
                File.Copy(Application.StartupPath + @"\monitor\cfg1.xml", Application.StartupPath + @"\monitor\cfg.xml");
                GlobalParams.AllCeDianList.allcedianlist.Clear();
                for (int i = 0; i < this.comboBox1.Items.Count; i++)
                {
                    byte fenzhan = Convert.ToByte(this.comboBox1.Items[i]);
                    this.initHardware(fenzhan);
                }
                MainFormRef.updateMainForm();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void deleteCeDian()
        {
            string sql = "truncate table CeDian";
            OperateDB.Execute(sql);
        }

        private void deleteCeDianLeiXing()
        {
            string sql = "truncate table MoNiLiangLeiXing";
            OperateDB.Execute(sql);
            sql = "truncate table KaiGuanLiangLeiXing";
            OperateDB.Execute(sql);
        }

        private void deleteClientConfig()
        {
            string sql = "truncate table LieBiaoAndCeDian";
            OperateDBAccess.Execute(sql);
            sql = "truncate table YuJing";
            OperateDBAccess.Execute(sql);
        }

        private void deleteDuanDianAndKuiDian()
        {
            string sql = "truncate table DuanDianGuanXi";
            OperateDB.Execute(sql);
            sql = "truncate table KuiDianGuanXi";
            OperateDB.Execute(sql);
        }

        private void deleteHistoryData()
        {
            DateTime start = new DateTime(0x7db, 1, 1, 0, 0, 0);
            DateTime end = DateTime.Now;
            while (start.Date < end.Date)
            {
                string tableName1 = "MoNiLiangValue" + start.ToString("yyyy_MM");
                string tableName2 = "KaiGuanLiangValue" + start.ToString("yyyy_MM");
                if (OperateDB.IsTableExist("max_cmss", tableName1))
                {
                    OperateDB.Execute("truncate table " + tableName1);
                }
                if (OperateDB.IsTableExist("max_cmss", tableName2))
                {
                    OperateDB.Execute("truncate table " + tableName2);
                }
                start = start.AddMonths(1);
            }
        }

        private void deleteKongZhiLiangCeDian()
        {
            string sql = "truncate table KongZhiLiangCeDian";
            OperateDB.Execute(sql);
        }

        private void deleteKongZhiLiangLeiXing()
        {
            string sql = "truncate table KongZhiLiang";
            OperateDB.Execute(sql);
        }

        private void deleteOther()
        {
            string sql = "truncate table AnZhuangDiDian";
            OperateDB.Execute(sql);
            sql = "truncate table DuoSheBei";
            OperateDB.Execute(sql);
            sql = "truncate table GuanJianZi";
            OperateDB.Execute(sql);
            sql = "truncate table FenZhanLeiXing";
            OperateDB.Execute(sql);
            sql = "truncate table FengDianWaSi";
            OperateDB.Execute(sql);
            sql = "truncate table FenZhanChuanKou";
            OperateDB.Execute(sql);
            sql = "truncate table YuJing";
            OperateDB.Execute(sql);
            sql = "truncate table XianXingZhi";
            OperateDB.Execute(sql);
            sql = "truncate table TiaoJiao";
            OperateDB.Execute(sql);
            sql = "truncate table Log";
            OperateDB.Execute(sql);
            sql = "truncate table Measure";
            OperateDB.Execute(sql);
            sql = "truncate table ReportConfig";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void initFenZhan(byte fenZhanHao)
        {
            string fenZhan = fenZhanHao.ToString();
            if (fenZhan.Length == 1)
            {
                fenZhan = "0" + fenZhan;
            }
            this.richTextBox1.Text = this.richTextBox1.Text + "删除该分站的断电关系。。。。。\n";
            OperateDB.Execute("delete from DuanDianGuanXi where ceDianBianHao like '" + fenZhan + "%'");
            this.richTextBox1.Text = this.richTextBox1.Text + "删除该分站的馈电关系。。。。。\n";
            OperateDB.Execute("delete from KuiDianGuanXi where ceDianBianHao like '" + fenZhan + "%'");
            this.richTextBox1.Text = this.richTextBox1.Text + "删除该分站的预警和调校配置。。。。\n";
            OperateDB.Execute("delete from YuJing where fenZhanHao = " + fenZhanHao);
            OperateDB.Execute("delete from TiaoJiao where fenZhanHao = " + fenZhanHao);
            this.richTextBox1.Text = this.richTextBox1.Text + "删除该分站的风电瓦斯闭锁。。。。\n";
            OperateDB.Execute("delete from FengDianWaSi where fenZhanHao = " + fenZhanHao);
            this.richTextBox1.Text = this.richTextBox1.Text + "删除该分站的测点信息。。。。。\n";
            OperateDB.Execute(string.Concat(new object[] { "update CeDian set weishanchu=0,deleteDate='", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "' where fenZhanHao=", fenZhanHao }));
            OperateDB.Execute(string.Concat(new object[] { "update KongZhiLiangCeDian set weishanchu=0,deleteDate='", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "' where fenZhanHao=", fenZhanHao }));
            OperateDBAccess.Execute("delete from LieBiaoAndCeDian where ceDianBianHao like '" + fenZhan + "%'");
            List<string> list = new List<string>();
            foreach (CeDian cedian in GlobalParams.AllCeDianList.allcedianlist.Values)
            {
                if (cedian.FenZhanHao == fenZhanHao)
                {
                    list.Add(cedian.CeDianBianHao);
                }
            }
            foreach (string key in list)
            {
                GlobalParams.AllCeDianList.allcedianlist.Remove(key);
            }
        }

        private void initHardware(byte fenZhanHao)
        {
            byte[] buf = new byte[0xa6];
            buf[0] = 0x7e;
            buf[1] = fenZhanHao;
            buf[2] = 0x43;
            for (int i = 3; i < 0xa3; i++)
            {
                buf[i] = 0xff;
            }
            buf[0xa3] = 0;
            buf[0xa4] = 0;
            buf[0xa5] = 0x21;
            UDPComm.Send(buf);
        }

        private void InitializeComponent()
        {
            this.button1 = new Button();
            this.richTextBox1 = new RichTextBox();
            this.label1 = new Label();
            this.comboBox1 = new ComboBox();
            this.button2 = new Button();
            this.button3 = new Button();
            base.SuspendLayout();
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = SystemColors.ButtonFace;
            this.button1.Location = new Point(0x145, 11);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x80, 0x17);
            this.button1.TabIndex = 0;
            this.button1.Text = "硬件初始化";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.richTextBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left;
            this.richTextBox1.Location = new Point(3, 0x29);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new Size(630, 0x162);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x8f, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "分站";
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(0xb6, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x79, 20);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.button2.Location = new Point(0x1cb, 11);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x76, 0x17);
            this.button2.TabIndex = 4;
            this.button2.Text = "软件初始化";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.button3.Location = new Point(0x15, 11);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x6c, 0x17);
            this.button3.TabIndex = 5;
            this.button3.Text = "系统初始化";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new EventHandler(this.button3_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.button3);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.richTextBox1);
            base.Controls.Add(this.button1);
            base.Name = "SystemInit";
            base.Size = new Size(0x27c, 0x18e);
            base.Load += new EventHandler(this.SystemInit_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitUser()
        {
            string sql = "truncate table USERS";
            OperateDB.Execute(sql);
            Users.CreateUsersByZhangJin("admin", "admin", "0");
        }

        public void showMessage(string message)
        {
            this.richTextBox1.Text = this.richTextBox1.Text + message + "\n";
        }

        private void SystemInit_Load(object sender, EventArgs e)
        {
            string[] fenzhans = FenZhan.GetAllConfigedFenZhan();
            if (Users.GlobalUserName != "admin")
            {
                this.button3.Visible = false;
            }
            else
            {
                this.button3.Visible = true;
            }
            this.comboBox1.Items.AddRange(fenzhans);
            this.comboBox1.SelectedIndex = 0;
        }
    }
}

