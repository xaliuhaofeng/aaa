namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Turn_management : UserControl
    {
        private BindingSource bindingSource1;
        private Button button2;
        private Button button3;
        private CeDianSelector ceDianSelector1;
        private ComboBox comboBox1;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ContextMenuStrip contextMenuStrip2;
        private int currentRow = -1;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Panel panel1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem 查看历史数据ToolStripMenuItem;

        public Turn_management()
        {
            this.InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请先选择测点！");
            }
            else
            {
                int fenZhan;
                int tongDao;
                GlobalParams.AllCeDianList.ParseCeDianAllInfo(this.comboBox1.SelectedItem.ToString().Substring(0, 5), out fenZhan, out tongDao);
                CeDian cedian = GlobalParams.AllCeDianList.allcedianlist[this.comboBox1.SelectedItem.ToString().Substring(0, 5)];
                if (cedian.TiaoJiao)
                {
                    MessageBox.Show("该测点已经开始了调校，请重新选择测点");
                    this.comboBox1.Focus();
                }
                else
                {
                    string bianhao = cedian.CeDianBianHao;
                    DateTime now = DateTime.Now;
                    DateTime? temp = null;
                    new TiaoJiao(bianhao, fenZhan, tongDao, now, temp).InsertIntoDB();
                    cedian.TiaoJiao = true;
                    this.setDataSource(fenZhan, tongDao);
                    this.setAllTiaoJiaoCeDian();
                    Log.WriteLog(LogType.TiaoJiao, "测点" + cedian.CeDianBianHao + "#$" + cedian.CeDianWeiZhi + "#$" + cedian.MoNiLiang.MingCheng + "#$启动调教");
                    MessageBox.Show("启动调教成功！");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请先选择测点！");
            }
            else
            {
                int fenzhan;
                int tongdao;
                GlobalParams.AllCeDianList.ParseCeDianAllInfo(this.comboBox1.SelectedItem.ToString().Substring(0, 5), out fenzhan, out tongdao);
                CeDian cedian = GlobalParams.AllCeDianList.allcedianlist[this.comboBox1.SelectedItem.ToString().Substring(0, 5)];
                if (!cedian.TiaoJiao)
                {
                    MessageBox.Show("该测点并没有开始调校，请重新选择测点！");
                    this.comboBox1.Focus();
                }
                else
                {
                    int id = Convert.ToInt32(this.dataGridView1.Rows[0].Cells[0].Value);
                    DateTime now = DateTime.Now;
                    TiaoJiao.updateFinishTime(id, now);
                    cedian.TiaoJiao = false;
                    TiaoJiao.updateHistoryDataState(id, cedian.CeDianBianHao);
                    Log.WriteLog(LogType.TiaoJiao, "测点" + cedian.CeDianBianHao + "#$" + cedian.CeDianWeiZhi + "#$" + cedian.MoNiLiang.MingCheng + "#$停止调教");
                    this.setDataSource(fenzhan, tongdao);
                    this.setAllTiaoJiaoCeDian();
                    MessageBox.Show("停止调教成功！");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex != -1)
            {
                int fenZhan;
                int tongDao;
                GlobalParams.AllCeDianList.ParseCeDianAllInfo(this.comboBox1.SelectedItem.ToString().Substring(0, 5), out fenZhan, out tongDao);
                this.setDataSource(fenZhan, tongDao);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = this.dataGridView1.HitTest(e.X, e.Y);
            switch (hti.Type)
            {
                case DataGridViewHitTestType.None:
                    this.dataGridView1.ContextMenuStrip = null;
                    break;

                case DataGridViewHitTestType.Cell:
                    this.currentRow = hti.RowIndex;
                    this.dataGridView1.ContextMenuStrip = this.contextMenuStrip2;
                    break;

                case DataGridViewHitTestType.ColumnHeader:
                    this.dataGridView1.ContextMenuStrip = null;
                    break;

                case DataGridViewHitTestType.RowHeader:
                    this.dataGridView1.ContextMenuStrip = null;
                    break;

                default:
                    this.dataGridView1.ContextMenuStrip = null;
                    break;
            }
        }

        private void dataGridView2_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = this.dataGridView2.HitTest(e.X, e.Y);
            switch (hti.Type)
            {
                case DataGridViewHitTestType.None:
                    this.dataGridView2.ContextMenuStrip = null;
                    break;

                case DataGridViewHitTestType.Cell:
                    this.currentRow = hti.RowIndex;
                    this.dataGridView2.ContextMenuStrip = this.contextMenuStrip1;
                    break;

                case DataGridViewHitTestType.ColumnHeader:
                    this.dataGridView2.ContextMenuStrip = null;
                    break;

                case DataGridViewHitTestType.RowHeader:
                    this.dataGridView2.ContextMenuStrip = null;
                    break;

                default:
                    this.dataGridView2.ContextMenuStrip = null;
                    break;
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ceDianSelector1 = new MAX_CMSS_V2.CeDianSelector();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看历史数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(933, 280);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Location = new System.Drawing.Point(0, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(930, 188);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "全部调校测点浏览";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.ColumnHeadersHeight = 30;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.Location = new System.Drawing.Point(3, 17);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(924, 168);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView2_MouseDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ceDianSelector1);
            this.groupBox1.Location = new System.Drawing.Point(3, 197);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(927, 81);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模拟量测点选择";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Location = new System.Drawing.Point(329, 55);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(205, 20);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(256, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择测点";
            // 
            // ceDianSelector1
            // 
            this.ceDianSelector1.BackColor = System.Drawing.SystemColors.Control;
            this.ceDianSelector1.CeDianBianHaos = null;
            this.ceDianSelector1.DisplayList = null;
            this.ceDianSelector1.DisplayList2 = null;
            this.ceDianSelector1.Location = new System.Drawing.Point(7, 25);
            this.ceDianSelector1.Name = "ceDianSelector1";
            this.ceDianSelector1.Size = new System.Drawing.Size(740, 33);
            this.ceDianSelector1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 280);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(933, 174);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Chocolate;
            this.button2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(320, 469);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "启动调校";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Chocolate;
            this.button3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(444, 469);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "停止调校";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem1.Text = "取消调教";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem2.Text = "全部取消调教";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看历史数据ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(149, 26);
            // 
            // 查看历史数据ToolStripMenuItem
            // 
            this.查看历史数据ToolStripMenuItem.Name = "查看历史数据ToolStripMenuItem";
            this.查看历史数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.查看历史数据ToolStripMenuItem.Text = "查看历史数据";
            this.查看历史数据ToolStripMenuItem.Click += new System.EventHandler(this.查看历史数据ToolStripMenuItem_Click_1);
            // 
            // Turn_management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "Turn_management";
            this.Size = new System.Drawing.Size(933, 500);
            this.Load += new System.EventHandler(this.Turn_management_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        private void setAllTiaoJiaoCeDian()
        {
            DataTable dt = CeDian.GetAllTiaoJiaoCeDian();
            if (dt != null)
            {
                this.dataGridView2.DataSource = dt;
                this.dataGridView2.Columns["ceDianBianHao"].HeaderText = "测点编号";
                this.dataGridView2.Columns["ceDianWeiZhi"].HeaderText = "安装地点";
                this.dataGridView2.Columns["xiaoLeiXing"].HeaderText = "名称";
            }
        }

        private void setDataSource(int fenZhan, int tongDao)
        {
            DataTable table = TiaoJiao.GetAllTiaoJiaoById(fenZhan, tongDao);
            if (table != null)
            {
                this.dataGridView1.DataSource = table;
                this.dataGridView1.Columns["id"].Visible = false;
                this.dataGridView1.Columns["fenZhanHao"].Visible = false;
                this.dataGridView1.Columns["tongDaoHao"].Visible = false;
                this.dataGridView1.Columns["ceDianBianHao"].HeaderText = "测点编号";
                this.dataGridView1.Columns["startTime"].HeaderText = "调校开始时间";
                this.dataGridView1.Columns["finishTime"].HeaderText = "调校结束时间";
                this.dataGridView1.Columns["startTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                this.dataGridView1.Columns["finishTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
                {
                    this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                this.dataGridView1.Update();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string ceDianBianHao = this.dataGridView2[0, this.currentRow].Value.ToString();
            CeDian cedian = new CeDian(ceDianBianHao);
            DateTime date = DateTime.Now;
            TiaoJiao.DeleteTiaoJiao(cedian.FenZhanHao, cedian.TongDaoHao, date);
            cedian.TiaoJiao = false;
            Log.WriteLog(LogType.TiaoJiao, "测点" + ceDianBianHao + "#$" + cedian.CeDianWeiZhi + "#$" + cedian.XiaoleiXing + "#$停止调教");
            this.setAllTiaoJiaoCeDian();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            TiaoJiao.DeleteAllTiaoJiao(DateTime.Now);
            this.setAllTiaoJiaoCeDian();
        }

        private void Turn_management_Load(object sender, EventArgs e)
        {
            this.ceDianSelector1.DisplayList = this.comboBox1;
            this.ceDianSelector1.setCeDianLeiXing(0);
            this.setAllTiaoJiaoCeDian();
        }

        private void 查看历史数据ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string ceDianBianHao = this.dataGridView1[1, this.currentRow].Value.ToString();
            DateTime start = Convert.ToDateTime(this.dataGridView1[4, this.currentRow].Value.ToString());
            DateTime end = DateTime.Now;
            if (this.dataGridView1[5, this.currentRow].Value.ToString() == "")
            {
                end = new DateTime(start.Year, start.Month, start.Day, 0x17, 0x3b, 0x3b);
            }
            else
            {
                end = Convert.ToDateTime(this.dataGridView1[5, this.currentRow].Value.ToString());
            }
            new History_data(ceDianBianHao, start, end).ShowDialog();
        }

        public ComboBox CeDians
        {
            get
            {
                return this.comboBox1;
            }
        }
    }
}

