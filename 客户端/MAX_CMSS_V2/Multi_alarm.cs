namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Multi_alarm : UserControl
    {
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private byte flag = 0;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label15;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label7;
        private ListBox listBox1;
        private ListBox listBox2;
        private Panel panel2;
        private Panel panel3;
        private ComboBox siteBox;

        public Multi_alarm()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            this.listBox1.Items.Clear();
            if ((this.siteBox.SelectedIndex != -1) && (this.comboBox2.SelectedIndex == -1))
            {
                dt = CeDian.GetCeDian4(this.siteBox.SelectedItem.ToString());
                foreach (DataRow row in dt.Rows)
                {
                    this.listBox1.Items.Add(row["ceDianBianHao"].ToString() + " " + row["ceDianWeiZhi"].ToString() + " " + row["xiaoLeiXing"].ToString());
                }
            }
            else if ((this.siteBox.SelectedIndex == -1) && (this.comboBox2.SelectedIndex != -1))
            {
                dt = CeDian.GetCeDian5(this.comboBox2.SelectedItem.ToString());
                this.listBox1.Items.AddRange(CeDian.getCeDianAllInfo(dt));
            }
            else if ((this.siteBox.SelectedIndex != -1) && (this.comboBox2.SelectedIndex != -1))
            {
                dt = CeDian.GetCeDian6(this.comboBox2.SelectedItem.ToString(), this.siteBox.SelectedItem.ToString());
                this.listBox1.Items.AddRange(CeDian.getCeDianAllInfo(dt));
            }
            else
            {
                dt = GlobalParams.AllCeDianList.ListConvertDataTable(1);
                this.listBox1.Items.AddRange(CeDian.getCeDianAllInfo(dt));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((this.listBox1.SelectedIndex == -1) || (this.comboBox1.SelectedIndex == -1))
            {
                MessageBox.Show("请选择要添加的测点和报警状态");
            }
            else if (this.listBox2.Items.Count == 0)
            {
                this.listBox2.Items.Add(this.listBox1.SelectedItem.ToString().TrimEnd(new char[0]) + " " + this.comboBox1.SelectedItem.ToString());
            }
            else
            {
                int flag = 0;
                for (int i = 0; i < this.listBox2.Items.Count; i++)
                {
                    if (this.listBox1.SelectedItem.ToString().Substring(0, 5) == this.listBox2.Items[i].ToString().Substring(0, 5))
                    {
                        flag = 1;
                        MessageBox.Show("该测点在报警关系已存在，请重新选择");
                        break;
                    }
                }
                if (flag != 1)
                {
                    this.listBox2.Items.Add(this.listBox1.SelectedItem.ToString().TrimEnd(new char[0]) + " " + this.comboBox1.SelectedItem.ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.listBox2.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要删除的报警关系");
            }
            else
            {
                this.listBox2.Items.Remove(this.listBox2.SelectedItem);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.listBox2.Items.Count == 0)
            {
                MessageBox.Show("请先添加报警关系");
            }
            else
            {
                DataTable dt = DuoSheBei.GetMax();
                int baojingId = 0;
                if (dt.Rows[0][0].ToString().Trim().Length != 0)
                {
                    baojingId = int.Parse(dt.Rows[0][0].ToString()) + 1;
                }
                List<DualAlarmInfo> list2 = new List<DualAlarmInfo>();
                string cedians = string.Empty;
                string leixing = string.Empty;
                string didian = string.Empty;
                string zhuangtai = string.Empty;
                foreach (string s in this.listBox2.Items)
                {
                    string ceDianBianHao = s.Substring(0, 5);
                    List<int> list = null;
                    GlobalParams.dualAlarmInfo1.TryGetValue(ceDianBianHao, out list);
                    if (list == null)
                    {
                        list = new List<int> {
                            baojingId
                        };
                        GlobalParams.dualAlarmInfo1.Add(ceDianBianHao, list);
                    }
                    else
                    {
                        list.Add(baojingId);
                    }
                    DualAlarmInfo info = new DualAlarmInfo {
                        Cedianbianhao = ceDianBianHao,
                        Fenzhan = Convert.ToInt32(ceDianBianHao.Substring(0, 2)),
                        Tongdao = Convert.ToInt32(ceDianBianHao.Substring(3, 2))
                    };
                    CeDian cedian = GlobalParams.AllCeDianList.getCedianInfo(ceDianBianHao);
                    cedians = cedians + ceDianBianHao + ";";
                    leixing = leixing + cedian.XiaoleiXing + ";";
                    didian = didian + cedian.CeDianWeiZhi + ";";
                    if (s.IndexOf("0态") > -1)
                    {
                        zhuangtai = zhuangtai + s.Substring(s.IndexOf("0态")) + ";";
                        info.State = 0;
                        OperateDB.Execute(DuoSheBei.CreateMulti(baojingId.ToString(), s.Substring(0, 5), s.Substring(s.IndexOf("0态"))));
                    }
                    else if (s.IndexOf("1态") > -1)
                    {
                        zhuangtai = zhuangtai + s.Substring(s.IndexOf("1态")) + ";";
                        info.State = 1;
                        OperateDB.Execute(DuoSheBei.CreateMulti(baojingId.ToString(), s.Substring(0, 5), s.Substring(s.IndexOf("1态"))));
                    }
                    else if (s.IndexOf("2态") > -1)
                    {
                        zhuangtai = zhuangtai + s.Substring(s.IndexOf("2态")) + ";";
                        info.State = 2;
                        OperateDB.Execute(DuoSheBei.CreateMulti(baojingId.ToString(), s.Substring(0, 5), s.Substring(s.IndexOf("2态"))));
                    }
                    list2.Add(info);

                    cedian.IsMultiBaoji = true;
                    GlobalParams.AllCeDianList.updateCedian(ceDianBianHao,cedian);

                }
                GlobalParams.dualAlarmInfo2.Add(baojingId, list2);
                Log.WriteLog(LogType.DuoSheBei, "添加#$" + cedians + "#$" + leixing + "#$" + didian + "#$" + zhuangtai);
                this.listBox2.Items.Clear();
                this.dataGridView1.DataSource = DuoSheBei.GetMulti();

                GlobalParams.setDataState();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要删除的报警关系");
            }
            else
            {
                if (MessageBox.Show("确认要删除该组报警关系?", "确认?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OperateDB.Execute(DuoSheBei.DelMulti(this.dataGridView1.SelectedRows[0].Cells["guanXiID"].Value.ToString()));
                    int id = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells["guanXiID"].Value);
                    string cedians = string.Empty;
                    string leixing = string.Empty;
                    string didian = string.Empty;
                    string zhuangtai = string.Empty;
                    foreach (KeyValuePair<string , List<int>> pair in GlobalParams.dualAlarmInfo1)
                    {
                        string cdbh = pair.Key.ToString();
                        CeDian cedian = GlobalParams.AllCeDianList.getCedianInfo(cdbh);
                        
                        pair.Value.Remove(id);
                        List<int> list = (List<int>)pair.Value;
                        if (list.Count == 0)
                            cedian.IsMultiBaoji = false;
                    }
                    if (GlobalParams.dualAlarmInfo2.ContainsKey(id))
                    {
                        List<DualAlarmInfo> list2 = GlobalParams.dualAlarmInfo2[id];
                        foreach (DualAlarmInfo info in list2)
                        {
                            cedians = cedians + info.Cedianbianhao + ";";
                            CeDian cedian = GlobalParams.AllCeDianList.allcedianlist[info.Cedianbianhao];
                            leixing = leixing + cedian.XiaoleiXing + ";";
                            didian = didian + cedian.CeDianWeiZhi + ";";
                            zhuangtai = zhuangtai + info.State + ";";
                        }
                        GlobalParams.dualAlarmInfo2.Remove(id);
                    }

                    //foreach (KeyValuePair<int, List<DualAlarmInfo>> pair in GlobalParams.dualAlarmInfo2)
                    //{
                    //    int key = pair.Key;
                    //    List<DualAlarmInfo> list2 = (List<DualAlarmInfo>)pair.Value;
                    //    foreach (DualAlarmInfo info in list2)
                    //    {
                    //        CeDian cedian = GlobalParams.AllCeDianList.allcedianlist[info.Cedianbianhao];
                    //        cedian.IsMultiBaoji = true;
                    //    }
                    //}
                    

                    MainFormRef.mainForm.combinedAlarmFrm.deleteSpecifiedAlarmId(id);
                    Log.WriteLog(LogType.DuoSheBei, "删除#$" + cedians + "#$" + leixing + "#$" + didian + "#$" + zhuangtai);
                    this.dataGridView1.DataSource = DuoSheBei.GetMulti();

                    GlobalParams.setDataState();
                }
                this.flag = 0;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.listBox2.Items.Clear();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择测点");
            }
            else
            {
                this.comboBox1.Items.Clear();
                string ceDianBianHao = this.listBox1.SelectedItem.ToString().Substring(0, 5);
                CeDian cedian = GlobalParams.AllCeDianList.allcedianlist[ceDianBianHao];
                if (cedian.KaiGuanLiang != null)
                {
                    this.comboBox1.Items.Add("0态——" + cedian.KaiGuanLiang.LingTai);
                    this.comboBox1.Items.Add("1态——" + cedian.KaiGuanLiang.YiTai);
                    if (cedian.KaiGuanLiang.ErTai != "")
                    {
                        this.comboBox1.Items.Add("2态——" + cedian.KaiGuanLiang.ErTai);
                    }
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.updateListbox();
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

        private void initComboBox2()
        {
            this.comboBox2.Items.Clear();
            this.comboBox2.Items.Add("全部");
            List<KaiGuanLiangLeiXing> kgllist = GlobalParams.AllkgLeiXing.GetSwitch2();
            foreach (KaiGuanLiangLeiXing item in kgllist)
            {
                this.comboBox2.Items.Add(item.MingCheng.ToString().Trim());
            }
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.siteBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(907, 202);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.siteBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 208);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 291);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.AutoSize = true;
            this.panel3.BackColor = System.Drawing.Color.AliceBlue;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label15);
            this.panel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel3.Location = new System.Drawing.Point(0, 11);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(241, 30);
            this.panel3.TabIndex = 40;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label15.Location = new System.Drawing.Point(26, 2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(127, 26);
            this.label15.TabIndex = 8;
            this.label15.Text = "开关测点信息";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(96, 132);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(126, 22);
            this.comboBox2.TabIndex = 7;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // siteBox
            // 
            this.siteBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.siteBox.FormattingEnabled = true;
            this.siteBox.Location = new System.Drawing.Point(96, 83);
            this.siteBox.Name = "siteBox";
            this.siteBox.Size = new System.Drawing.Size(126, 22);
            this.siteBox.TabIndex = 6;
            this.siteBox.SelectedIndexChanged += new System.EventHandler(this.siteBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(11, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "测点类型";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(12, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "安装地点";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.listBox2);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Location = new System.Drawing.Point(260, 208);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(634, 255);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSize = true;
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(0, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(634, 31);
            this.panel2.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label7.Location = new System.Drawing.Point(28, -1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(152, 30);
            this.label7.TabIndex = 8;
            this.label7.Text = "多设备报警关系";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label5.Location = new System.Drawing.Point(359, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "报警关系";
            // 
            // listBox2
            // 
            this.listBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 14;
            this.listBox2.Location = new System.Drawing.Point(356, 73);
            this.listBox2.MultiColumn = true;
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(257, 158);
            this.listBox2.TabIndex = 6;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Chocolate;
            this.button3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(232, 187);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 27);
            this.button3.TabIndex = 5;
            this.button3.Text = "移除 <";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Chocolate;
            this.button2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(232, 154);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 27);
            this.button2.TabIndex = 4;
            this.button2.Text = "添加 >";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new System.Drawing.Point(238, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "报警状态";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(206, 103);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(144, 24);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(29, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "测点选择";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(12, 73);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(193, 158);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Chocolate;
            this.button4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(340, 472);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(160, 27);
            this.button4.TabIndex = 3;
            this.button4.Text = "添加报警关系";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Chocolate;
            this.button5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(537, 472);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(160, 27);
            this.button5.TabIndex = 4;
            this.button5.Text = "删除报警关系";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Chocolate;
            this.button6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(738, 472);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(160, 27);
            this.button6.TabIndex = 5;
            this.button6.Text = "取消";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // Multi_alarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Multi_alarm";
            this.Size = new System.Drawing.Size(907, 518);
            this.Load += new System.EventHandler(this.Multi_alarm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void initSiteBox()
        {
            this.siteBox.Items.Clear();
            this.siteBox.Items.Add("全部");
            DataTable dt = InstallationSite.GetAllLocation();
            foreach (DataRow row in dt.Rows)
            {
                this.siteBox.Items.Add(row["DiDian"].ToString());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox1.Items.Clear();
        }

        private void Multi_alarm_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = CeDian.GetSwicthBaoJingCeDian();
            foreach (DataRow row in dt.Rows)
            {
                this.listBox1.Items.Add(row["ceDianBianHao"].ToString() + " " + row["ceDianWeiZhi"].ToString() + " " + row["xiaoLeiXing"].ToString());
            }
            this.initSiteBox();
            this.initComboBox2();
            this.dataGridView1.DataSource = DuoSheBei.GetMulti();
            this.dataGridView1.Columns["guanXiID"].HeaderText = "关系编号";
            this.dataGridView1.Columns["ceDianBianHao"].HeaderText = "测点编号";
            this.dataGridView1.Columns["baoJingZhuangTai"].HeaderText = "报警状态";
            this.dataGridView1.ClearSelection();
        }

        private void siteBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.updateListbox();
        }

        private void updateListbox()
        {
            DataTable dt;
            this.listBox1.Items.Clear();
            if (((this.siteBox.SelectedIndex == 0) || (this.siteBox.SelectedIndex == -1)) && ((this.comboBox2.SelectedIndex == 0) || (this.comboBox2.SelectedIndex == -1)))
            {
                dt = CeDian.GetSwicthBaoJingCeDian();
                foreach (DataRow row in dt.Rows)
                {
                    this.listBox1.Items.Add(row["ceDianBianHao"].ToString() + " " + row["ceDianWeiZhi"].ToString() + " " + row["xiaoLeiXing"].ToString());
                }
            }
            else if (((this.siteBox.SelectedIndex == 0) || (this.siteBox.SelectedIndex == -1)) && ((this.comboBox2.SelectedIndex != 0) && (this.comboBox2.SelectedIndex != -1)))
            {
                dt = CeDian.GetSwicthBaoJingCeDian(this.comboBox2.SelectedItem.ToString(), (byte) 0);
                foreach (DataRow row in dt.Rows)
                {
                    this.listBox1.Items.Add(row["ceDianBianHao"].ToString() + " " + row["ceDianWeiZhi"].ToString() + " " + row["xiaoLeiXing"].ToString());
                }
            }
            else if (((this.siteBox.SelectedIndex != 0) && (this.siteBox.SelectedIndex != -1)) && ((this.comboBox2.SelectedIndex == 0) || (this.comboBox2.SelectedIndex == -1)))
            {
                dt = CeDian.GetSwicthBaoJingCeDian(this.siteBox.SelectedItem.ToString(), (byte) 1);
                foreach (DataRow row in dt.Rows)
                {
                    this.listBox1.Items.Add(row["ceDianBianHao"].ToString() + " " + row["ceDianWeiZhi"].ToString() + " " + row["xiaoLeiXing"].ToString());
                }
            }
            else
            {
                dt = CeDian.GetSwicthBaoJingCeDian(this.comboBox2.SelectedItem.ToString(), this.siteBox.SelectedItem.ToString());
                foreach (DataRow row in dt.Rows)
                {
                    this.listBox1.Items.Add(row["ceDianBianHao"].ToString() + " " + row["ceDianWeiZhi"].ToString() + " " + row["xiaoLeiXing"].ToString());
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }
    }
}

