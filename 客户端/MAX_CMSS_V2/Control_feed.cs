namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Control_feed : UserControl
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
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

        public Control_feed()
        {
            this.InitializeComponent();
            this.printButton1.DataGridView1 = this.dataGridView1;
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
                DataTable dtt = KuiDianGuanXi.ConvertoDataTable();
                foreach (DataRow row in dtt.Rows)
                {
                    if (row["kuiDianGuanXiID"].ToString().Length != 0)
                    {
                        if (int.Parse(row["kuiDianGuanXiID"].ToString()) > Temp)
                        {
                            Temp = int.Parse(row["kuiDianGuanXiID"].ToString());
                        }
                        Temp++;
                    }
                }
                for (int m = 0; m < this.listBox2.Items.Count; m++)
                {
                    for (int n = 0; n < this.listBox3.Items.Count; n++)
                    {
                        string temp1 = this.listBox2.Items[m].ToString().Substring(0, 5);
                        string temp2 = this.listBox3.Items[n].ToString().Substring(0, 5);
                        if (KuiDianGuanXi.hasKuiDianGuanXi(temp1, temp2))
                        {
                            MessageBox.Show("已经添加了相同的馈电关系，请不要重要添加！");
                            return;
                        }
                        bool success = KuiDianGuanXi.CreateKuiDian(Temp.ToString(), temp1, temp2);
                        char[] ch = new char[] { ' ' };
                        string[] wp = this.listBox3.Items[n].ToString().Split(ch);
                        string ff = temp2 + "#$" + wp[1] + "#$" + wp[2] + "#$" + Temp.ToString();
                        if (success)
                        {
                            Log.WriteLog(LogType.KuiDianGuanXi, "添加#$" + ff);
                        }
                    }
                }
            }
            DataTable dt = new DataTable();
            dt = this.MakeTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1 = GlobalParams.AllCeDianList.GetCeDian3();
            dt2 = KuiDianGuanXi.GetKuiDian();
            this.SwitchDuanDian(dt, dt1, dt2);
            this.dataGridView1.DataSource = dt;
            this.listBox2.Items.Clear();
            this.listBox3.Items.Clear();

            GlobalParams.setDataState();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.flag == 0)
            {
                MessageBox.Show("请选择要删除的馈电关系");
            }
            else if ((MessageBox.Show("请慎重操作！！！是否继续馈电关系的删除操作？", null, MessageBoxButtons.OKCancel) == DialogResult.OK) && (this.dataGridView1.CurrentRow != null))
            {
                string kongZhi = this.dataGridView1.CurrentRow.Cells["KongZhi"].Value.ToString();
                string shouKong = this.dataGridView1.CurrentRow.Cells["ShouKong"].Value.ToString();
                int fenzhan1 = Convert.ToInt32(kongZhi.Substring(0, 2));
                int tongdao1 = Convert.ToInt32(kongZhi.Substring(3, 2));
                int fenzhan2 = Convert.ToInt32(shouKong.Substring(0, 2));
                int tongdao2 = Convert.ToInt32(shouKong.Substring(3, 2));
                if (((MainFormRef.mainForm.historys[fenzhan1] != null) && (MainFormRef.mainForm.historys[fenzhan2] != null)) && ((MainFormRef.mainForm.historys[fenzhan1].kongZhiLiangZhuangTai[tongdao1] != MainFormRef.mainForm.historys[fenzhan2].realValue[tongdao2]) && (MainFormRef.mainForm.historys[fenzhan2].realValue[tongdao2] != 2f)))
                {
                    MessageBox.Show("该馈电关系目前处于使能状态，请稍后删除");
                }
                else
                {
                    KuiDianGuanXi.DelKuiDian(shouKong, kongZhi);
                    string leiXing = this.dataGridView1.CurrentRow.Cells["LeiXing"].Value.ToString();
                    string ID = this.dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                    string QuYu = this.dataGridView1.CurrentRow.Cells["QuYu"].Value.ToString();
                    Log.WriteLog(LogType.KuiDianGuanXi, "删除#$" + (kongZhi + "#$" + QuYu + "#$" + leiXing + "#$" + this.dataGridView1.CurrentRow.Cells["ShouKong"].Value.ToString()));
                    DataTable dt = new DataTable();
                    dt = this.MakeTable();
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    dt1 = GlobalParams.AllCeDianList.GetCeDian3();
                    dt2 = KuiDianGuanXi.GetKuiDian();
                    this.SwitchDuanDian(dt, dt1, dt2);
                    this.dataGridView1.DataSource = dt;
                    this.listBox2.Items.Clear();
                    this.listBox3.Items.Clear();
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

        private void Control_feed_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GlobalParams.AllCeDianList.GetCeDian3();
            foreach (DataRow row in dt.Rows)
            {
                this.listBox1.Items.Add(row["ceDianBianHao"].ToString() + " " + row["ceDianWeiZhi"].ToString() + " " + row["xiaoLeiXing"].ToString());
            }
            dt = GlobalParams.AllCeDianList.ListConvertDataTable(2);
            foreach (DataRow row in dt.Rows)
            {
                this.listBox4.Items.Add(row["ceDianBianHao"].ToString() + " " + row["ceDianWeiZhi"].ToString() + " " + row["xiaoLeiXing"].ToString());
            }
            dt = this.MakeTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1 = GlobalParams.AllCeDianList.GetCeDian3();
            dt2 = KuiDianGuanXi.GetKuiDian();
            this.SwitchDuanDian(dt, dt1, dt2);
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["ID"].HeaderText = "馈电关系ID";
            this.dataGridView1.Columns["ShouKong"].HeaderText = "受控测点";
            this.dataGridView1.Columns["KongZhi"].HeaderText = "控制测点";
            this.dataGridView1.Columns["LeiXing"].HeaderText = "受控测点类型";
            this.dataGridView1.Columns["QuYu"].HeaderText = "安装地点";
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.printButton1 = new MAX_CMSS_V2.PrintButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
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
            this.dataGridView1.Size = new System.Drawing.Size(866, 194);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.listBox3);
            this.groupBox2.Controls.Add(this.listBox4);
            this.groupBox2.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 421);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(866, 204);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "断电传感器选择";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Control;
            this.button3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(385, 134);
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
            this.button4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(385, 57);
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
            this.listBox3.Location = new System.Drawing.Point(507, 26);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(300, 164);
            this.listBox3.TabIndex = 2;
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.ItemHeight = 16;
            this.listBox4.Location = new System.Drawing.Point(45, 26);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(300, 164);
            this.listBox4.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.listBox2);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 200);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(866, 204);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "馈电传感器选择";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(385, 125);
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
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(385, 50);
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
            this.listBox2.Location = new System.Drawing.Point(507, 24);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(300, 164);
            this.listBox2.TabIndex = 2;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(45, 24);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(300, 164);
            this.listBox1.TabIndex = 0;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.Control;
            this.button7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button7.ForeColor = System.Drawing.Color.Black;
            this.button7.Location = new System.Drawing.Point(584, 632);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 33);
            this.button7.TabIndex = 11;
            this.button7.Text = "取消";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.Control;
            this.button6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.ForeColor = System.Drawing.Color.Black;
            this.button6.Location = new System.Drawing.Point(415, 632);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(123, 33);
            this.button6.TabIndex = 10;
            this.button6.Text = "删除馈电关系";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.Control;
            this.button5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Location = new System.Drawing.Point(248, 632);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(119, 33);
            this.button5.TabIndex = 9;
            this.button5.Text = "添加馈电关系";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // printButton1
            // 
            this.printButton1.Footer = "页脚";
            this.printButton1.Location = new System.Drawing.Point(4, 630);
            this.printButton1.Margin = new System.Windows.Forms.Padding(4);
            this.printButton1.Name = "printButton1";
            this.printButton1.Size = new System.Drawing.Size(130, 42);
            this.printButton1.SubTitle = "";
            this.printButton1.TabIndex = 12;
            this.printButton1.Title = "馈电关系打印";
            // 
            // Control_feed
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
            this.Name = "Control_feed";
            this.Size = new System.Drawing.Size(866, 680);
            this.Load += new System.EventHandler(this.Control_feed_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

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
            DataColumn QuYu = new DataColumn("QuYu", System.Type.GetType("System.String"));
            JoinTable.Columns.Add(QuYu);
            return JoinTable;
        }

        private void SwitchDuanDian(DataTable dt, DataTable dt1, DataTable dt2)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    if (dt1.Rows[i]["ceDianBianHao"].ToString().TrimEnd(new char[0]) == dt2.Rows[j]["ceDianBianHao"].ToString().TrimEnd(new char[0]))
                    {
                        DataRow dr = dt.NewRow();
                        dr["ID"] = dt2.Rows[j]["kuiDianGuanXiID"];
                        dr["ShouKong"] = dt2.Rows[j]["ceDianBianHao"];
                        dr["LeiXing"] = dt1.Rows[i]["xiaoLeiXing"];
                        dr["KongZhi"] = dt2.Rows[j]["kongZhiCeDianBianHao"];
                        dr["QuYu"] = dt1.Rows[i]["ceDianWeiZhi"];
                        dt.Rows.Add(dr);
                    }
                }
            }
        }
    }
}

