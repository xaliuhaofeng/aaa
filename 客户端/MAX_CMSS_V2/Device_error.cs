namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Device_error : UserControl
    {
        private DataGridViewTextBoxColumn CeDianBianHao;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private int count = 0;
        private DataGridViewTextBoxColumn CuoShiJiShiKe;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn DiDian;
        private DataGridViewTextBoxColumn GuZhangShiKe;
        private DataGridViewTextBoxColumn GuZhangZhuangTai;
        private DataGridViewTextBoxColumn MingCheng;
        private ToolStripMenuItem 打印ToolStripMenuItem;
        private ToolStripMenuItem 上一页ToolStripMenuItem;
        private ToolStripMenuItem 输入措施ToolStripMenuItem;
        private ToolStripMenuItem 下一页ToolStripMenuItem;

        public Device_error()
        {
            this.InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (this.dataGridView1.HitTest(e.X, e.Y).Type)
            {
                case DataGridViewHitTestType.None:
                    this.ContextMenuStrip = null;
                    break;

                case DataGridViewHitTestType.Cell:
                    this.ContextMenuStrip = this.contextMenuStrip1;
                    break;

                case DataGridViewHitTestType.ColumnHeader:
                    this.ContextMenuStrip = null;
                    break;

                case DataGridViewHitTestType.RowHeader:
                    this.ContextMenuStrip = null;
                    break;

                default:
                    this.ContextMenuStrip = null;
                    break;
            }
        }

        private void Device_error_Load(object sender, EventArgs e)
        {
            this.count = 1;
            this.Loadk(this.count);
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DiDian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CeDianBianHao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MingCheng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GuZhangZhuangTai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GuZhangShiKe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CuoShiJiShiKe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.输入措施ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下一页ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上一页ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DiDian,
            this.CeDianBianHao,
            this.MingCheng,
            this.GuZhangZhuangTai,
            this.GuZhangShiKe,
            this.CuoShiJiShiKe});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(601, 490);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            // 
            // DiDian
            // 
            this.DiDian.HeaderText = "地点";
            this.DiDian.Name = "DiDian";
            // 
            // CeDianBianHao
            // 
            this.CeDianBianHao.HeaderText = "测点编号";
            this.CeDianBianHao.Name = "CeDianBianHao";
            // 
            // MingCheng
            // 
            this.MingCheng.HeaderText = "名称";
            this.MingCheng.Name = "MingCheng";
            // 
            // GuZhangZhuangTai
            // 
            this.GuZhangZhuangTai.HeaderText = "故障状态";
            this.GuZhangZhuangTai.Name = "GuZhangZhuangTai";
            // 
            // GuZhangShiKe
            // 
            this.GuZhangShiKe.HeaderText = "故障时刻";
            this.GuZhangShiKe.Name = "GuZhangShiKe";
            // 
            // CuoShiJiShiKe
            // 
            this.CuoShiJiShiKe.HeaderText = "措施及时刻";
            this.CuoShiJiShiKe.Name = "CuoShiJiShiKe";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打印ToolStripMenuItem,
            this.输入措施ToolStripMenuItem,
            this.下一页ToolStripMenuItem,
            this.上一页ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 92);
            // 
            // 打印ToolStripMenuItem
            // 
            this.打印ToolStripMenuItem.Name = "打印ToolStripMenuItem";
            this.打印ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.打印ToolStripMenuItem.Text = "打印";
            this.打印ToolStripMenuItem.Click += new System.EventHandler(this.打印ToolStripMenuItem_Click);
            // 
            // 输入措施ToolStripMenuItem
            // 
            this.输入措施ToolStripMenuItem.Name = "输入措施ToolStripMenuItem";
            this.输入措施ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.输入措施ToolStripMenuItem.Text = "输入措施";
            this.输入措施ToolStripMenuItem.Click += new System.EventHandler(this.输入措施ToolStripMenuItem_Click);
            // 
            // 下一页ToolStripMenuItem
            // 
            this.下一页ToolStripMenuItem.Name = "下一页ToolStripMenuItem";
            this.下一页ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.下一页ToolStripMenuItem.Text = "下一页";
            this.下一页ToolStripMenuItem.Click += new System.EventHandler(this.下一页ToolStripMenuItem_Click);
            // 
            // 上一页ToolStripMenuItem
            // 
            this.上一页ToolStripMenuItem.Name = "上一页ToolStripMenuItem";
            this.上一页ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.上一页ToolStripMenuItem.Text = "上一页";
            this.上一页ToolStripMenuItem.Click += new System.EventHandler(this.上一页ToolStripMenuItem_Click);
            // 
            // Device_error
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "Device_error";
            this.Size = new System.Drawing.Size(601, 490);
            this.Load += new System.EventHandler(this.Device_error_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void Loadk(int k)
        {
            DataTable Kdt;
            int index;
            DataTable dt;
            this.dataGridView1.Rows.Clear();
            string tableN = "KaiGuanLiangValue" + DateTime.Now.ToString("yyyy-MM").Substring(0, 7).Replace('-', '_');
            if (OperateDB.IsTableExist("max_cmss", tableN))
            {
                Kdt = KaiGuanLiangBaoJing.DeviceError(tableN, k);
                foreach (DataRow row in Kdt.Rows)
                {
                    index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells["CeDianBianHao"].Value = row["ceDianBianHao"].ToString();
                    this.dataGridView1.Rows[index].Cells["GuZhangShiKe"].Value = row["uploadTime"].ToString();
                    if (row["uploadValue"].ToString() == "2")
                    {
                        this.dataGridView1.Rows[index].Cells["GuZhangZhuangTai"].Value = "故障";
                    }
                    dt = CeDian.GetCeDian13(row["ceDianBianHao"].ToString());
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            this.dataGridView1.Rows[index].Cells["DiDian"].Value = dt.Rows[0]["ceDianWeiZhi"].ToString();
                            this.dataGridView1.Rows[index].Cells["MingCheng"].Value = dt.Rows[0]["xiaoLeiXing"].ToString();
                            this.dataGridView1.Rows[index].Cells["CuoShiJiShiKe"].Value = Measure.GetMeasure(row["ceDianBianHao"].ToString(), (byte)0, DateTime.Parse(row["uploadTime"].ToString()));
                        }
                    }
                }
            }
            tableN = "MoNiLiangValue" + DateTime.Now.ToString("yyyy-MM").Substring(0, 7).Replace('-', '_');
            if (OperateDB.IsTableExist("max_cmss", tableN))
            {
                Kdt = KaiGuanLiangBaoJing.MoNiLiangDeviceError(tableN, k);
                foreach (DataRow row in Kdt.Rows)
                {
                    index = this.dataGridView1.Rows.Add();
                    dt = MoNiLiangLeiXing.GetAnalogAlarm(row["ceDianBianHao"].ToString());
                    this.dataGridView1.Rows[index].Cells["CeDianBianHao"].Value = row["ceDianBianHao"].ToString();
                    this.dataGridView1.Rows[index].Cells["GuZhangShiKe"].Value = row["uploadTime"].ToString();
                    if (row["state"].ToString() == "4")
                    {
                        this.dataGridView1.Rows[index].Cells["GuZhangZhuangTai"].Value = "断线";
                    }
                    else if (row["state"].ToString() == "5")
                    {
                        this.dataGridView1.Rows[index].Cells["GuZhangZhuangTai"].Value = "溢出";
                    }
                    else if (row["state"].ToString() == "6")
                    {
                        this.dataGridView1.Rows[index].Cells["GuZhangZhuangTai"].Value = "负漂";
                    }
                    else if (row["state"].ToString() == "7")
                    {
                        this.dataGridView1.Rows[index].Cells["GuZhangZhuangTai"].Value = "故障";
                    }
                    dt = CeDian.GetCeDian13(row["ceDianBianHao"].ToString());
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            this.dataGridView1.Rows[index].Cells["DiDian"].Value = dt.Rows[0]["ceDianWeiZhi"].ToString();
                            this.dataGridView1.Rows[index].Cells["MingCheng"].Value = dt.Rows[0]["xiaoLeiXing"].ToString();
                        }
                    }
                    this.dataGridView1.Rows[index].Cells["CuoShiJiShiKe"].Value = Measure.GetMeasure(row["ceDianBianHao"].ToString(), (byte) 0, DateTime.Parse(row["uploadTime"].ToString()));
                }
            }
            this.dataGridView1.Sort(this.dataGridView1.Columns[4], ListSortDirection.Ascending);
        }

        public void UpdateL()
        {
            this.dataGridView1.CurrentRow.Cells["CuoShi"].Value = Measure.GetMeasure(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString(), (byte) 0, DateTime.Parse(this.dataGridView1.CurrentRow.Cells["GuZhangShiKe"].Value.ToString()));
        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalParams.PrintDataGridView(this.dataGridView1, "监控设备故障列表");
        }

        private void 上一页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Loadk(--this.count);
        }

        private void 输入措施ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请选择要输入措施的测点");
            }
            else
            {
                new Form_measure(this, this.dataGridView1.CurrentRow.Cells["CeDianBianHao"].Value.ToString(), DateTime.Parse(this.dataGridView1.CurrentRow.Cells["GuZhangShiKe"].Value.ToString()), 0).Show();
            }
        }

        private void 下一页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Loadk(++this.count);
        }
    }
}

