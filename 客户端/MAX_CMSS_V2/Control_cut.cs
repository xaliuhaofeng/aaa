namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Control_cut : UserControl
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private ComboBox comboBox1;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private byte flag = 0;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ListBox listBox1;
        private ListBox listBox2;
        private ListBox listBox3;
        private PrintButton printButton1;
        private ListBox listBox4;

        public Control_cut()
        {
            this.InitializeComponent();


            this.printButton1.DataGridView1 = this.dataGridView1;
        }

        public void AddCeDian(string ceDianBianHao)
        {
            for (int i = 0; i < this.listBox1.Items.Count; i++)
            {
                if (this.listBox1.Items[i].ToString().Substring(0, 5) == ceDianBianHao)
                {
                    this.listBox2.Items.Add(this.listBox1.Items[i]);
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择受控测点");
            }
            else if (this.listBox2.Items.Contains(this.listBox1.SelectedItem))
            {
                MessageBox.Show("请不要重复添加测点！");
            }
            else
            {
                this.listBox2.Items.Add(this.listBox1.SelectedItem);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox2.SelectedIndex == -1)
            {
                MessageBox.Show("请选择受控测点");
            }
            else
            {
                this.listBox2.Items.Remove(this.listBox2.SelectedItem);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.listBox3.SelectedIndex == -1)
            {
                MessageBox.Show("请选择被控测点");
            }
            else
            {
                this.listBox3.Items.Remove(this.listBox3.SelectedItem);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.listBox4.SelectedIndex == -1)
            {
                MessageBox.Show("请选择被控测点");
            }
            else if (this.listBox3.Items.Contains(this.listBox4.SelectedItem))
            {
                MessageBox.Show("请不要重复添加测点！");
            }
            else
            {
                this.listBox3.Items.Add(this.listBox4.SelectedItem);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if ((this.listBox2.Items.Count < 1) || (this.listBox3.Items.Count < 1))
            {
                MessageBox.Show("控制关系一方不可为空，请添加测点");
            }
            else if ((this.listBox2.Items.Count > 1) && (this.listBox3.Items.Count > 1))
            {
                MessageBox.Show("控制关系不可多对多，请删除多余测点");
            }
            else
            {
                int Temp = 0;
                DataTable dtt = DuanDianGuanXi.GetMaxDuanDianID();
                foreach (DataRow row in dtt.Rows)
                {
                    if (row["duanDianGuanXiID"].ToString().Length != 0)
                    {
                        if (int.Parse(row["duanDianGuanXiID"].ToString()) > Temp)
                        {
                            Temp = int.Parse(row["duanDianGuanXiID"].ToString());
                        }
                        Temp++;
                    }
                }
                for (int m = 0; m < this.listBox2.Items.Count; m++)
                {
                    for (int n = 0; n < this.listBox3.Items.Count; n++)
                    {
                        string temp = "";
                        string temp1 = this.listBox2.Items[m].ToString().Trim().Substring(0, 5);
                        string temp2 = this.listBox3.Items[n].ToString().Trim().Substring(0, 5);
                        if (DuanDianGuanXi.hasDuanDianGuanXi(temp1, temp2))
                        {
                            MessageBox.Show("已经添加了相同的断电关系，请不要重复添加！");
                            return;
                        }
                        if (temp1.Substring(0, 2) == temp2.Substring(0, 2))
                        {
                            temp = "true";
                        }
                        else
                        {
                            temp = "false";
                        }
                        DuanDianGuanXi.CreateDuanDian(Temp.ToString(), temp1, temp2, temp);
                        char[] ch = new char[] { ' ' };
                        string[] wp = this.listBox2.Items[m].ToString().Split(ch);
                        string ff = temp1 + "#$" + wp[1] + "#$" + wp[2] + "#$";
                        wp = this.listBox3.Items[n].ToString().Split(ch);
                        string Reflector0005 = ff;
                        Log.WriteLog(LogType.KongZhiLuoJi, "添加#$" + (Reflector0005 + temp2 + "#$" + wp[1] + "#$" + wp[2]));
                        GlobalParams.AllCeDianList.allcedianlist[temp1].UpdateDDGX = true;
                    }
                   
                }
            }
            DataTable dt = new DataTable();
            dt = this.MakeTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            dt1 = GlobalParams.AllCeDianList.ListConvertDataTable(0);
            dt2 = DuanDianGuanXi.GetDuanDian();
            dt3 = GlobalParams.AllmnlLeiXing.GetMo();
            this.MoNiDuanDian(dt, dt1, dt2, dt3);
            dt1 = GlobalParams.AllCeDianList.ListConvertDataTable(1);
            dt3 = GlobalParams.AllkgLeiXing.ListConvertDataTable();
            this.SwitchDuanDian(dt, dt1, dt2, dt3);
            this.dataGridView1.DataSource = dt;
            this.listBox2.Items.Clear();
            this.listBox3.Items.Clear();
            GlobalParams.setDataState();
            MainFormRef.updateMainForm();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.flag == 0)
            {
                MessageBox.Show("请选择要删除的断电关系");
            }
            else if ((MessageBox.Show("请慎重操作！！！是否继续断电关系的删除操作？", null, MessageBoxButtons.OKCancel) == DialogResult.OK) && (this.dataGridView1.CurrentRow != null))
            {
                string shouKongCeDian = this.dataGridView1.CurrentRow.Cells["ShouKong"].Value.ToString();
                string kongZhiCeDian = this.dataGridView1.CurrentRow.Cells["KongZhi"].Value.ToString();
                int fenzhan = Convert.ToInt32(shouKongCeDian.Substring(0, 2));
                int tongdao = Convert.ToInt32(shouKongCeDian.Substring(3, 2));
                FenZhanRTdata ud = MainFormRef.mainForm.historys[fenzhan];
                if ((ud != null) && ((ud.tongDaoZhuangTai[tongdao] == 2) || (ud.tongDaoZhuangTai[tongdao] == 14)))
                {
                    MessageBox.Show("该断电关系处于使能状态，请稍后再删除！");
                }
                else
                {
                    GlobalParams.AllCeDianList.allcedianlist[shouKongCeDian].UpdateDDGX = true;
                    DuanDianGuanXi.DelDuanDian(shouKongCeDian, kongZhiCeDian);
                    string kongZhiQuYu = this.dataGridView1.CurrentRow.Cells["QuYu"].Value.ToString();
                    string leiXing = this.dataGridView1.CurrentRow.Cells["LeiXing"].Value.ToString();
                    DataTable dt11 = CeDian.GetInfoByCeDianBianHao(kongZhiCeDian);
                    string weiZhi1 = "";
                    string mingCheng1 = "";
                    if (dt11.Rows.Count > 0)
                    {
                        weiZhi1 = dt11.Rows[0]["ceDianWeiZhi"].ToString();
                        mingCheng1 = dt11.Rows[0]["xiaoLeiXing"].ToString();
                    }
                    string Reflector0002 = shouKongCeDian + "#$" + kongZhiQuYu + "#$" + leiXing + "#$";
                    Log.WriteLog(LogType.KongZhiLuoJi, "删除#$" + (Reflector0002 + kongZhiCeDian + "#$" + weiZhi1 + "#$" + mingCheng1));
                    DataTable dt = new DataTable();
                    dt = this.MakeTable();
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();
                    dt1 = GlobalParams.AllCeDianList.ListConvertDataTable(0);
                    dt2 = DuanDianGuanXi.GetDuanDian();
                    dt3 = GlobalParams.AllmnlLeiXing.GetMo();
                    this.MoNiDuanDian(dt, dt1, dt2, dt3);
                    dt1 = GlobalParams.AllCeDianList.ListConvertDataTable(1);
                    dt3 = GlobalParams.AllkgLeiXing.ListConvertDataTable();
                    this.SwitchDuanDian(dt, dt1, dt2, dt3);
                    this.dataGridView1.DataSource = dt;
                  
                    this.flag = 0;

                    GlobalParams.setDataState();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.listBox2.Items.Clear();
            this.listBox3.Items.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            DataTable dt4 = new DataTable();
            if (this.comboBox1.SelectedIndex == -1)
            {
                dt4 = CeDian.GetCeDian();
                foreach (DataRow row in dt4.Rows)
                {
                    this.listBox1.Items.Add(row["ceDianBianHao"].ToString() + " " + row["ceDianWeiZhi"].ToString() + " " + row["xiaoLeiXing"].ToString());
                }
            }
            else if (this.comboBox1.SelectedIndex == 0)
            {
                Dictionary<string, CeDian> list = GlobalParams.AllCeDianList.GetCeDian2();
                if (list.Count > 0)
                {
                    foreach (KeyValuePair<string, CeDian> item in list)
                    {
                        this.listBox1.Items.Add(item.Value.CeDianBianHao.ToString() + " " + item.Value.CeDianWeiZhi.ToString() + " " + item.Value.XiaoleiXing.ToString());
                    }
                }
            }
            else
            {
                List<CeDian> cdlist = GlobalParams.AllCeDianList.GetCeDian(1);
                foreach (CeDian item in cdlist)
                {
                    this.listBox1.Items.Add(item.CeDianBianHao.Trim() + " " + item.CeDianWeiZhi.Trim() + " " + item.XiaoleiXing.Trim());
                }
            }
        }

        private void Control_cut_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Dictionary<string, CeDian> list = GlobalParams.AllCeDianList.GetCeDian2();
            if (list.Count > 0)
            {
                foreach (KeyValuePair<string, CeDian> item in list)
                {
                    this.listBox1.Items.Add(item.Value.CeDianBianHao.ToString() + " " + item.Value.CeDianWeiZhi.ToString() + " " + item.Value.XiaoleiXing.ToString());
                }
            }
            List<CeDian> cdlist = GlobalParams.AllCeDianList.GetCeDian(1);
            if (cdlist.Count > 0)
            {
                foreach (CeDian item in cdlist)
                {
                    this.listBox1.Items.Add(item.CeDianBianHao.Trim() + " " + item.CeDianWeiZhi.Trim() + " " + item.XiaoleiXing.Trim());
                }
            }
            cdlist = GlobalParams.AllCeDianList.GetCeDian(2);
            if (cdlist.Count > 0)
            {
                foreach (CeDian item in cdlist)
                {
                    this.listBox4.Items.Add(item.CeDianBianHao.Trim() + " " + item.CeDianWeiZhi.Trim() + " " + item.XiaoleiXing.Trim());
                }
            }
            dt = this.MakeTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            dt1 = GlobalParams.AllCeDianList.ListConvertDataTable(0);
            dt2 = DuanDianGuanXi.GetDuanDian();
            dt3 = GlobalParams.AllmnlLeiXing.GetMo();
            this.MoNiDuanDian(dt, dt1, dt2, dt3);
            dt1 = GlobalParams.AllCeDianList.ListConvertDataTable(1);
            dt3 = GlobalParams.AllkgLeiXing.ListConvertDataTable();
            this.SwitchDuanDian(dt, dt1, dt2, dt3);
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["ID"].HeaderText = "断电关系ID";
            this.dataGridView1.Columns["ShouKong"].HeaderText = "受控测点";
            this.dataGridView1.Columns["KongZhi"].HeaderText = "控制测点";
            this.dataGridView1.Columns["LeiXing"].HeaderText = "受控测点类型";
            this.dataGridView1.Columns["DuanDian"].HeaderText = "断电";
            this.dataGridView1.Columns["FuDian"].HeaderText = "复电";
            this.dataGridView1.Columns["QuYu"].HeaderText = "控制区域";
            this.dataGridView1.Columns["FangShi"].HeaderText = "控制方式";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.flag = 1;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.printButton1 = new MAX_CMSS_V2.PrintButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(863, 207);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.listBox2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 213);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(866, 208);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "受控测点选择";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(385, 139);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "移除 <";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(385, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "添加 >";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(507, 52);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(300, 148);
            this.listBox2.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "模拟量",
            "开关量"});
            this.comboBox1.Location = new System.Drawing.Point(196, 22);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(146, 24);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(45, 52);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(300, 148);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.listBox3);
            this.groupBox2.Controls.Add(this.listBox4);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 433);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(866, 182);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "被控测点选择";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Control;
            this.button3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(385, 123);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "移除 <";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Control;
            this.button4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(385, 58);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "添加 >";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 16;
            this.listBox3.Location = new System.Drawing.Point(507, 28);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(300, 148);
            this.listBox3.TabIndex = 2;
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.ItemHeight = 16;
            this.listBox4.Location = new System.Drawing.Point(45, 28);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(300, 148);
            this.listBox4.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.Control;
            this.button5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Location = new System.Drawing.Point(253, 626);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(120, 33);
            this.button5.TabIndex = 6;
            this.button5.Text = "添加控制关系";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.Control;
            this.button6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.ForeColor = System.Drawing.Color.Black;
            this.button6.Location = new System.Drawing.Point(420, 626);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(120, 33);
            this.button6.TabIndex = 7;
            this.button6.Text = "删除断电关系";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.Control;
            this.button7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button7.ForeColor = System.Drawing.Color.Black;
            this.button7.Location = new System.Drawing.Point(588, 626);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 33);
            this.button7.TabIndex = 8;
            this.button7.Text = "取消";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // printButton1
            // 
            this.printButton1.Footer = "页脚";
            this.printButton1.Location = new System.Drawing.Point(14, 621);
            this.printButton1.Margin = new System.Windows.Forms.Padding(4);
            this.printButton1.Name = "printButton1";
            this.printButton1.Size = new System.Drawing.Size(130, 42);
            this.printButton1.SubTitle = "";
            this.printButton1.TabIndex = 12;
            this.printButton1.Title = "断点关系打印";
            // 
            // Control_cut
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.printButton1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Control_cut";
            this.Size = new System.Drawing.Size(866, 680);
            this.Load += new System.EventHandler(this.Control_cut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private DataTable MakeTable()
        {
            DataTable JoinTable = new DataTable("Join");
            DataColumn ID = new DataColumn("ID", System.Type.GetType("System.String"));
            JoinTable.Columns.Add(ID);
            DataColumn ShouKong = new DataColumn("ShouKong", System.Type.GetType("System.String"));
            JoinTable.Columns.Add(ShouKong);
            DataColumn LeiXing = new DataColumn("LeiXing", System.Type.GetType("System.String"));
            JoinTable.Columns.Add(LeiXing);
            DataColumn KongZhi = new DataColumn("KongZhi", System.Type.GetType("System.String"));
            JoinTable.Columns.Add(KongZhi);
            DataColumn DuanDian = new DataColumn("DuanDian", System.Type.GetType("System.String"));
            JoinTable.Columns.Add(DuanDian);
            DataColumn FuDian = new DataColumn("FuDian", System.Type.GetType("System.String"));
            JoinTable.Columns.Add(FuDian);
            DataColumn QuYu = new DataColumn("QuYu", System.Type.GetType("System.String"));
            JoinTable.Columns.Add(QuYu);
            DataColumn FangShi = new DataColumn("FangShi", System.Type.GetType("System.String"));
            JoinTable.Columns.Add(FangShi);
            return JoinTable;
        }

        private void MoNiDuanDian(DataTable dt, DataTable dt1, DataTable dt2, DataTable dt3)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    for (int k = 0; k < dt3.Rows.Count; k++)
                    {
                        if ((dt1.Rows[i]["ceDianBianHao"].ToString().TrimEnd(new char[0]) == dt2.Rows[j]["ceDianBianHao"].ToString().TrimEnd(new char[0])) && (dt1.Rows[i]["xiaoLeiXing"].ToString().TrimEnd(new char[0]) == dt3.Rows[k]["mingCheng"].ToString().TrimEnd(new char[0])))
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = dt2.Rows[j]["duanDianGuanXiID"];
                            dr["ShouKong"] = dt2.Rows[j]["ceDianBianHao"];
                            dr["LeiXing"] = dt1.Rows[i]["xiaoLeiXing"];
                            dr["KongZhi"] = dt2.Rows[j]["kongZhiCeDianBianHao"];
                            dr["DuanDian"] = dt3.Rows[k]["duanDianZhi"].ToString();
                            dr["FuDian"] = dt3.Rows[k]["fuDianZhi"].ToString();
                            dr["QuYu"] = dt2.Rows[j]["ceDianWeiZhi"];
                            if ((bool) dt2.Rows[j]["kongZhiFangShi"])
                            {
                                dr["FangShi"] = "本地";
                            }
                            else
                            {
                                dr["FangShi"] = "异地";
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                }
            }
        }

        private void SwitchDuanDian(DataTable dt, DataTable dt1, DataTable dt2, DataTable dt3)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    for (int k = 0; k < dt3.Rows.Count; k++)
                    {
                        if ((dt1.Rows[i]["ceDianBianHao"].ToString().TrimEnd(new char[0]) == dt2.Rows[j]["ceDianBianHao"].ToString().TrimEnd(new char[0])) && (dt1.Rows[i]["xiaoLeiXing"].ToString().TrimEnd(new char[0]) == dt3.Rows[k]["mingCheng"].ToString().TrimEnd(new char[0])))
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = dt2.Rows[j]["duanDianGuanXiID"];
                            dr["ShouKong"] = dt2.Rows[j]["ceDianBianHao"];
                            dr["LeiXing"] = dt1.Rows[i]["xiaoLeiXing"];
                            dr["KongZhi"] = dt2.Rows[j]["kongZhiCeDianBianHao"];
                            if (dt3.Rows[k]["duanDianZhuangTai"].ToString() == "0")
                            {
                                dr["DuanDian"] = dt3.Rows[k]["lingTaiMingCheng"].ToString();
                            }
                            else if (dt3.Rows[k]["duanDianZhuangTai"].ToString() == "1")
                            {
                                dr["DuanDian"] = dt3.Rows[k]["yiTaiMingCheng"].ToString();
                            }
                            else if (dt3.Rows[k]["duanDianZhuangTai"].ToString() == "2")
                            {
                                dr["DuanDian"] = dt3.Rows[k]["erTaiMingCheng"].ToString();
                            }
                            else
                            {
                                dr["DuanDian"] = "";
                            }
                            dr["FuDian"] = "";
                            dr["QuYu"] = dt2.Rows[j]["ceDianWeiZhi"];
                            if ((bool) dt2.Rows[j]["kongZhiFangShi"])
                            {
                                dr["FangShi"] = "本地";
                            }
                            else
                            {
                                dr["FangShi"] = "异地";
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                }
            }
        }

      
    }
}

