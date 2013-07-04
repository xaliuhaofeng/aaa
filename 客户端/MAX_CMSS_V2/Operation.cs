namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class Operation : UserControl
    {
        private addBtnDelegate addbtn;
        private Button btnTemp;
        private Button button1;
        private Button button2;
        private string ceDianBianHao = "";
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private IContainer components = null;
        private List<int> controlList;
        private int fenzhan = 0;
        private byte flag = 0;
        public byte flag2 = 0;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListBox listBox1;
        private TableLayoutPanel tableLayoutPanel1;

        public Operation(List<int>  list)
        {
            this.InitializeComponent();
            this.controlList = list;// new List<int>();
            this.addbtn = new addBtnDelegate(this.addBtn);
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < controlList.Count; i++)
            {
                    fenzhan = controlList[i];
               
                    Button btn = new Button
                    {
                        Name = "btn" + this.fenzhan
                    };
                    string s = this.fenzhan.ToString();
                    if (this.fenzhan < 10)
                    {
                        s = "0" + s;
                    }
                    btn.Text = "解除对" + s + "号分站的手动控制";
                    btn.Size = new Size(100, 40);
                    btn.Click += new EventHandler(this.btn_Click);
                    int row = (this.fenzhan - 1) / 8;
                    int col = (this.fenzhan - 1) % 8;
                    this.tableLayoutPanel1.Controls.Add(btn, col, row);        // this.controlList.Add(this.fenzhan);
                

            }

        }
        public void addBtn()
        {
            lock (this)
            {
                if (!this.controlList.Contains(this.fenzhan))
                {
                    Button btn = new Button {
                        Name = "btn" + this.fenzhan
                    };
                    string s = this.fenzhan.ToString();
                    if (this.fenzhan < 10)
                    {
                        s = "0" + s;
                    }
                    btn.Text = "解除对" + s + "号分站的手动控制";
                    btn.Size = new Size(100, 40);
                    btn.Click += new EventHandler(this.btn_Click);
                    int row = (this.fenzhan - 1) / 8;
                    int col = (this.fenzhan - 1) % 8;
                    this.tableLayoutPanel1.Controls.Add(btn, col, row);
                    this.controlList.Add(this.fenzhan);
                    GlobalParams.Manula_Ctl_List = this.controlList;
                }
            }
        }

        private void addControlToListBox1()
        {
            this.listBox1.Items.Clear();
            if ((this.comboBox1.SelectedIndex != -1) && (this.comboBox2.SelectedIndex != -1))
            {
                this.fenzhan = Convert.ToInt32(this.comboBox1.SelectedItem);
                if (this.fenzhan < 10)
                {
                    this.ceDianBianHao = "0" + this.comboBox1.SelectedItem.ToString() + "C0" + this.comboBox2.SelectedItem.ToString();
                }
                else
                {
                    this.ceDianBianHao = this.comboBox1.SelectedItem.ToString() + "C0" + this.comboBox2.SelectedItem.ToString();
                }
                DataTable dt = CeDian.getControlInfo(this.ceDianBianHao, 0);
                if (dt.Rows.Count != 0)
                {
                    foreach (string s in CeDian.getCeDianAllInfo(dt))
                    {
                        if (!this.listBox1.Items.Contains(s))
                        {
                            this.listBox1.Items.Add(s);
                        }
                    }
                }
                dt = CeDian.getControlInfo(this.ceDianBianHao, 1);
                if (dt.Rows.Count != 0)
                {
                    foreach (string s in CeDian.getCeDianAllInfo(dt))
                    {
                        if (!this.listBox1.Items.Contains(s))
                        {
                            this.listBox1.Items.Add(s);
                        }
                    }
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            byte k = Convert.ToByte(((Button) sender).Text.Substring(3, 2));
            UDPComm.Send(new byte[] { LogicCommon.startByte, Convert.ToByte(k), 0x4b, 0xff, 2, LogicCommon.endByte });
            this.btnTemp = (Button) sender;
            int fenzhanhao = Convert.ToInt32(this.btnTemp.Name.Substring(3));
            this.flag = 1;
            lock (this)
            {
                this.controlList.Remove(fenzhanhao);
                GlobalParams.Manula_Ctl_List = this.controlList;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择分站号");
            }
            else if (this.comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("请选择通道号");
            }
            else
            {
                UDPComm.Send(this.shouDongControl(0));
                this.flag = 0;
                Log.WriteLog(LogType.ShouDongKongZhi, string.Concat(new object[] { "手动复电#$", this.comboBox1.SelectedItem, "#$", this.comboBox2.SelectedItem, "#$下发" }));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择分站号");
            }
            else if (this.comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("请选择通道号");
            }
            else
            {
                UDPComm.Send(this.shouDongControl(1));
                this.flag2 = 0;
                Log.WriteLog(LogType.ShouDongKongZhi, string.Concat(new object[] { "手动断电#$", this.comboBox1.SelectedItem, "#$", this.comboBox2.SelectedItem, "#$下发" }));
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            this.comboBox1.Items.Clear();
            string[] list = new string[60];
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = (i + 1).ToString();
            }
            this.comboBox1.Items.AddRange(list);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.addControlToListBox1();
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            this.comboBox2.Items.Clear();
            string[] list = new string[8];
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = (i + 1).ToString();
            }
            this.comboBox2.Items.AddRange(list);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.addControlToListBox1();
        }

        public void Dispatch(FenZhanRTdata ud)
        {
            if (ud.responseInfo.Contains("手动控制操作成功"))
            {
                if (this.flag == 0)
                {
                    MessageBox.Show(ud.responseInfo);
                    if (base.InvokeRequired)
                    {
                        base.Invoke(this.addbtn);
                    }
                    else
                    {
                        MessageBox.Show("未使用代理");
                    }
                }
                else if (this.flag == 1)
                {
                    MessageBox.Show(ud.responseInfo.Substring(0, 4) + "解除手动控制操作成功");
                    this.flag = 0;
                    if (!((this.btnTemp == null) || this.btnTemp.IsDisposed))
                    {
                        this.btnTemp.Dispose();
                    }
                }
            }
            else if (ud.responseInfo.Contains("手动控制操作失败"))
            {
                if (this.flag == 0)
                {
                    MessageBox.Show(ud.responseInfo);
                }
                else if (this.flag == 1)
                {
                    MessageBox.Show(ud.responseInfo.Substring(0, 4) + "解除手动控制操作失败");
                    this.flag = 0;
                    if (!((this.btnTemp == null) || this.btnTemp.IsDisposed))
                    {
                        this.btnTemp.Dispose();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(636, 168);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择继电器";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(312, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "相关受控测点";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(147, 128);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "手动断电";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(56, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "手动复电";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(312, 40);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(193, 112);
            this.listBox1.TabIndex = 4;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(101, 80);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 3;
            this.comboBox2.DropDown += new System.EventHandler(this.comboBox2_DropDown);
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "通道";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(101, 40);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "分站";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 168);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(636, 344);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // Operation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Operation";
            this.Size = new System.Drawing.Size(636, 512);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        private byte[] shouDongControl(byte type)
        {
            return new byte[] { LogicCommon.startByte, Convert.ToByte(this.comboBox1.SelectedItem), 0x4b, Convert.ToByte(this.comboBox2.SelectedItem), type, LogicCommon.endByte };
        }

        private delegate void addBtnDelegate();

      
    }
}

