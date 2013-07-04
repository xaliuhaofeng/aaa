namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;

    public class Form_measure : DockContentEx
    {
        private List_All_Alarm all_alarm_frame;
        private Button button1;
        private Button button2;
        private CeDianSelector ceDianSelector2;
        private ComboBox comboBox1;
        private IContainer components;
        private Device_error De;
        private byte Flag;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private List_alarm_analog Laa;
        private Label label1;
        private List_alarm_switch Las;
        private List_cut_analog Lca;
        private List_cut_switch Lcs;
        private List_feed_analog Lfa;
        private List_feed_switch Lfs;
        private RichTextBox richTextBox1;
        private DateTime Time;
        private byte Type;

        public Form_measure()
        {
            this.components = null;
            this.Flag = 0;
            this.InitializeComponent();
            this.ceDianSelector2.DisplayList = this.comboBox1;
            this.ceDianSelector2.setCeDianLeiXing(3);
        }

        public Form_measure(Device_error list, string s, DateTime time, byte flag)
        {
            this.components = null;
            this.Flag = 0;
            this.InitializeComponent();
            if ((s.Substring(2, 1) == "A") || (s.Substring(2, 1) == "D"))
            {
                this.comboBox1.Items.Clear();
                string[] ss = CeDian.getCeDianAllInfo(CeDian.GetCeDian13(s));
                this.comboBox1.Items.AddRange(ss);
                this.comboBox1.SelectedIndex = 0;
                this.ceDianSelector2.Enabled = false;
                this.comboBox1.Enabled = false;
            }
            this.Time = time;
            this.Type = flag;
            this.De = list;
            this.Flag = 6;
        }

        public Form_measure(List_alarm_analog list, string s, DateTime time, byte flag)
        {
            this.components = null;
            this.Flag = 0;
            this.InitializeComponent();
            if ((s.Substring(2, 1) == "A") || (s.Substring(2, 1) == "D"))
            {
                this.comboBox1.Items.Clear();
                string[] ss = CeDian.getCeDianAllInfo(CeDian.GetCeDian13(s));
                this.comboBox1.Items.AddRange(ss);
                this.comboBox1.SelectedIndex = 0;
                this.ceDianSelector2.Enabled = false;
                this.comboBox1.Enabled = false;
            }
            this.Time = time;
            this.Type = flag;
            this.Laa = list;
            this.Flag = 5;
        }

        public Form_measure(List_alarm_switch list, string s, DateTime time, byte flag)
        {
            this.components = null;
            this.Flag = 0;
            this.InitializeComponent();
            if ((s.Substring(2, 1) == "A") || (s.Substring(2, 1) == "D"))
            {
                this.comboBox1.Items.Clear();
                string[] ss = CeDian.getCeDianAllInfo(CeDian.GetCeDian13(s));
                this.comboBox1.Items.AddRange(ss);
                this.comboBox1.SelectedIndex = 0;
                this.ceDianSelector2.Enabled = false;
                this.comboBox1.Enabled = false;
            }
            this.Time = time;
            this.Type = flag;
            this.Las = list;
            this.Flag = 4;
        }

        public Form_measure(List_All_Alarm list, string s, DateTime time, byte flag)
        {
            this.components = null;
            this.Flag = 0;
            this.InitializeComponent();
            if ((s.Substring(2, 1) == "A") || (s.Substring(2, 1) == "D"))
            {
                this.comboBox1.Items.Clear();
                string[] ss = CeDian.getCeDianAllInfo(CeDian.GetCeDian13(s));
                this.comboBox1.Items.AddRange(ss);
                this.comboBox1.SelectedIndex = 0;
                this.ceDianSelector2.Enabled = false;
                this.comboBox1.Enabled = false;
            }
            this.Time = time;
            this.Type = flag;
            this.all_alarm_frame = list;
            this.Flag = 0;
        }

        public Form_measure(List_cut_analog list, string s, DateTime time, byte flag)
        {
            this.components = null;
            this.Flag = 0;
            this.InitializeComponent();
            if ((s.Substring(2, 1) == "A") || (s.Substring(2, 1) == "D"))
            {
                this.comboBox1.Items.Clear();
                string[] ss = CeDian.getCeDianAllInfo(CeDian.GetCeDian13(s));
                this.comboBox1.Items.AddRange(ss);
                this.comboBox1.SelectedIndex = 0;
                this.ceDianSelector2.Enabled = false;
                this.comboBox1.Enabled = false;
            }
            this.Time = time;
            this.Type = flag;
            this.Lca = list;
            this.Flag = 3;
        }

        public Form_measure(List_cut_switch list, string s, DateTime time, byte flag)
        {
            this.components = null;
            this.Flag = 0;
            this.InitializeComponent();
            if ((s.Substring(2, 1) == "A") || (s.Substring(2, 1) == "D"))
            {
                this.comboBox1.Items.Clear();
                string[] ss = CeDian.getCeDianAllInfo(CeDian.GetCeDian13(s));
                this.comboBox1.Items.AddRange(ss);
                this.comboBox1.SelectedIndex = 0;
                this.ceDianSelector2.Enabled = false;
                this.comboBox1.Enabled = false;
            }
            this.Time = time;
            this.Type = flag;
            this.Lcs = list;
            this.Flag = 2;
        }

        public Form_measure(List_feed_analog list, string s, DateTime time, byte flag)
        {
            this.components = null;
            this.Flag = 0;
            this.InitializeComponent();
            if ((s.Substring(2, 1) == "A") || (s.Substring(2, 1) == "D"))
            {
                this.comboBox1.Items.Clear();
                string[] ss = CeDian.getCeDianAllInfo(CeDian.GetCeDian13(s));
                this.comboBox1.Items.AddRange(ss);
                this.comboBox1.SelectedIndex = 0;
                this.ceDianSelector2.Enabled = false;
                this.comboBox1.Enabled = false;
            }
            this.Time = time;
            this.Type = flag;
            this.Lfa = list;
            this.Flag = 1;
        }

        public Form_measure(List_feed_switch list, string s, DateTime time, byte flag)
        {
            this.components = null;
            this.Flag = 0;
            this.InitializeComponent();
            if ((s.Substring(2, 1) == "A") || (s.Substring(2, 1) == "D"))
            {
                this.comboBox1.Items.Clear();
                string[] ss = CeDian.getCeDianAllInfo(CeDian.GetCeDian13(s));
                this.comboBox1.Items.AddRange(ss);
                this.comboBox1.SelectedIndex = 0;
                this.ceDianSelector2.Enabled = false;
                this.comboBox1.Enabled = false;
            }
            this.Time = time;
            this.Type = flag;
            this.Lfs = list;
            this.Flag = 0;
        }

        private bool arguCheck()
        {
            if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要采取措施的测点！");
                return false;
            }
            if (this.richTextBox1.Text == string.Empty)
            {
                MessageBox.Show("所采取的措施不能为空！");
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.arguCheck())
            {
                string cedianbianhao = this.comboBox1.SelectedItem.ToString().Substring(0, 5);
                string cuoshi = this.richTextBox1.Text;
                DateTime now = DateTime.Now;
                DataTable dt = CeDian.GetNotDelByCeDianBianHao(cedianbianhao);
                long id = 0L;
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    id = (long) dt.Rows[0]["id"];
                }
                new Measure(id, cedianbianhao, cuoshi, now, this.Time, this.Type).Insert();
                this.all_alarm_frame.UpdateL();
                switch (this.Flag)
                {
                    case 0:
                        YuJing.setValue(6);
                        break;

                    case 1:
                        YuJing.setValue(5);
                        break;

                    case 2:
                        YuJing.setValue(4);
                        break;

                    case 3:
                        YuJing.setValue(3);
                        break;

                    case 4:
                        YuJing.setValue(2);
                        break;

                    case 5:
                        YuJing.setValue(1);
                        break;
                }
                base.Close();
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

        private void Form_measure_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.ceDianSelector2 = new CeDianSelector();
            this.label1 = new Label();
            this.comboBox1 = new ComboBox();
            this.groupBox2 = new GroupBox();
            this.richTextBox1 = new RichTextBox();
            this.button1 = new Button();
            this.button2 = new Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.ceDianSelector2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new Point(0x25, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x30d, 0x59);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测点选择";
            this.ceDianSelector2.BackColor = Color.AliceBlue;
            this.ceDianSelector2.CeDianBianHaos = null;
            this.ceDianSelector2.DisplayList = null;
            this.ceDianSelector2.DisplayList2 = null;
            this.ceDianSelector2.Location = new Point(6, 0x12);
            this.ceDianSelector2.Margin = new Padding(4, 4, 4, 4);
            this.ceDianSelector2.Name = "ceDianSelector2";
            this.ceDianSelector2.Size = new Size(0x2fe, 0x21);
            this.ceDianSelector2.TabIndex = 3;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0xec, 0x3d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x54, 0x12);
            this.label1.TabIndex = 2;
            this.label1.Text = "选择测点";
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(0x142, 0x3b);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0xde, 0x17);
            this.comboBox1.TabIndex = 0;
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.groupBox2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox2.Location = new Point(0x110, 0x6b);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(290, 0xe7);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "采取措施";
            this.richTextBox1.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.richTextBox1.Location = new Point(7, 20);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new Size(0x115, 0xcc);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(0x147, 0x158);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 2;
            this.button1.Text = "记录";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.BackColor = Color.Chocolate;
            this.button2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new Point(0x1ab, 0x158);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 3;
            this.button2.Text = "退出";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new EventHandler(this.button2_Click);
            base.ClientSize = new Size(0x34d, 0x18a);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Name = "Form_measure";
            this.Text = "措施";
            base.Load += new EventHandler(this.Form_measure_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
        }
    }
}

