namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Switch_status : UserControl
    {
        private DataGridViewTextBoxColumn BaoJingShiKe;
        private DataGridViewTextBoxColumn BaoJingZhuangTai;
        private DataGridViewTextBoxColumn CeDianBianHao;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private int count = 0;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn DiDian;
        private DataGridViewTextBoxColumn DuanDianQuYu;
        private DataGridViewTextBoxColumn DuanDianShiKe;
        private DataGridViewTextBoxColumn DuanDianZhuangTai;
        private DataGridViewTextBoxColumn KuiDianShiKe;
        private DataGridViewTextBoxColumn KuiDianZhuangTai;
        private DataGridViewTextBoxColumn MingCheng;
        private DataGridViewTextBoxColumn SheBeiZhuangTai;
        private DataGridViewTextBoxColumn SheBeiZhuangTaiShiKe;
        private ToolStripMenuItem 打印ToolStripMenuItem;
        private ToolStripMenuItem 上一页ToolStripMenuItem;
        private ToolStripMenuItem 下一页ToolStripMenuItem;

        public Switch_status()
        {
            this.InitializeComponent();
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DiDian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MingCheng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CeDianBianHao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SheBeiZhuangTai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SheBeiZhuangTaiShiKe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BaoJingZhuangTai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BaoJingShiKe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuanDianZhuangTai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuanDianShiKe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuanDianQuYu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KuiDianZhuangTai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KuiDianShiKe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DiDian,
            this.MingCheng,
            this.CeDianBianHao,
            this.SheBeiZhuangTai,
            this.SheBeiZhuangTaiShiKe,
            this.BaoJingZhuangTai,
            this.BaoJingShiKe,
            this.DuanDianZhuangTai,
            this.DuanDianShiKe,
            this.DuanDianQuYu,
            this.KuiDianZhuangTai,
            this.KuiDianShiKe});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(908, 528);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            // 
            // DiDian
            // 
            this.DiDian.HeaderText = "地点";
            this.DiDian.Name = "DiDian";
            // 
            // MingCheng
            // 
            this.MingCheng.HeaderText = "名称";
            this.MingCheng.Name = "MingCheng";
            // 
            // CeDianBianHao
            // 
            this.CeDianBianHao.HeaderText = "测点编号";
            this.CeDianBianHao.Name = "CeDianBianHao";
            // 
            // SheBeiZhuangTai
            // 
            this.SheBeiZhuangTai.HeaderText = "设备状态";
            this.SheBeiZhuangTai.Name = "SheBeiZhuangTai";
            // 
            // SheBeiZhuangTaiShiKe
            // 
            this.SheBeiZhuangTaiShiKe.HeaderText = "设备状态时刻";
            this.SheBeiZhuangTaiShiKe.Name = "SheBeiZhuangTaiShiKe";
            // 
            // BaoJingZhuangTai
            // 
            this.BaoJingZhuangTai.HeaderText = "报警状态";
            this.BaoJingZhuangTai.Name = "BaoJingZhuangTai";
            // 
            // BaoJingShiKe
            // 
            this.BaoJingShiKe.HeaderText = "报警时刻";
            this.BaoJingShiKe.Name = "BaoJingShiKe";
            // 
            // DuanDianZhuangTai
            // 
            this.DuanDianZhuangTai.HeaderText = "断电状态";
            this.DuanDianZhuangTai.Name = "DuanDianZhuangTai";
            // 
            // DuanDianShiKe
            // 
            this.DuanDianShiKe.HeaderText = "断电时刻";
            this.DuanDianShiKe.Name = "DuanDianShiKe";
            // 
            // DuanDianQuYu
            // 
            this.DuanDianQuYu.HeaderText = "断电区域";
            this.DuanDianQuYu.Name = "DuanDianQuYu";
            // 
            // KuiDianZhuangTai
            // 
            this.KuiDianZhuangTai.HeaderText = "馈电状态";
            this.KuiDianZhuangTai.Name = "KuiDianZhuangTai";
            // 
            // KuiDianShiKe
            // 
            this.KuiDianShiKe.HeaderText = "馈电时刻";
            this.KuiDianShiKe.Name = "KuiDianShiKe";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打印ToolStripMenuItem,
            this.下一页ToolStripMenuItem,
            this.上一页ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(113, 70);
            // 
            // 打印ToolStripMenuItem
            // 
            this.打印ToolStripMenuItem.Name = "打印ToolStripMenuItem";
            this.打印ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.打印ToolStripMenuItem.Text = "打印";
            this.打印ToolStripMenuItem.Click += new System.EventHandler(this.打印ToolStripMenuItem_Click);
            // 
            // 下一页ToolStripMenuItem
            // 
            this.下一页ToolStripMenuItem.Name = "下一页ToolStripMenuItem";
            this.下一页ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.下一页ToolStripMenuItem.Text = "下一页";
            this.下一页ToolStripMenuItem.Click += new System.EventHandler(this.下一页ToolStripMenuItem_Click);
            // 
            // 上一页ToolStripMenuItem
            // 
            this.上一页ToolStripMenuItem.Name = "上一页ToolStripMenuItem";
            this.上一页ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.上一页ToolStripMenuItem.Text = "上一页";
            this.上一页ToolStripMenuItem.Click += new System.EventHandler(this.上一页ToolStripMenuItem_Click);
            // 
            // Switch_status
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "Switch_status";
            this.Size = new System.Drawing.Size(908, 528);
            this.Load += new System.EventHandler(this.Switch_status_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void Loadk(int k)
        {
            this.dataGridView1.Rows.Clear();
            string tableN = "KaiGuanLiangValue" + DateTime.Now.ToString("yyyy-MM").Substring(0, 7).Replace('-', '_');
            if (OperateDB.IsTableExist("max_cmss", tableN))
            {
                DataTable Kdt = KaiGuanLiangBaoJing.SwitchStatusData(tableN, DateTime.Now);
                foreach (DataRow row in Kdt.Rows)
                {
                    int index = this.dataGridView1.Rows.Add();
                    string ceDianBianHao = row["ceDianBianHao"].ToString();
                    CeDian cedian = new CeDian();
                    if (GlobalParams.AllCeDianList.allcedianlist.ContainsKey(ceDianBianHao))
                    {
                        cedian = GlobalParams.AllCeDianList.allcedianlist[ceDianBianHao];
                        this.dataGridView1.Rows[index].Cells["CeDianBianHao"].Value = row["ceDianBianHao"].ToString();
                        this.dataGridView1.Rows[index].Cells["SheBeiZhuangTaiShiKe"].Value = row["uploadTime"].ToString();
                        int state = int.Parse(row["state"].ToString());
                        if (row["uploadValue"].ToString() == "0")
                        {
                            if (state != 0)
                            {
                                string s = GlobalParams.stateTranslate(state);
                                this.dataGridView1.Rows[index].Cells["SheBeiZhuangTai"].Value = s;
                            }
                            else
                            {
                                this.dataGridView1.Rows[index].Cells["SheBeiZhuangTai"].Value = cedian.KaiGuanLiang.LingTai;
                            }
                        }
                        else if (row["uploadValue"].ToString() == "1")
                        {
                            this.dataGridView1.Rows[index].Cells["SheBeiZhuangTai"].Value = cedian.KaiGuanLiang.YiTai;
                        }
                        else if (row["uploadValue"].ToString() == "2")
                        {
                            this.dataGridView1.Rows[index].Cells["SheBeiZhuangTai"].Value = cedian.KaiGuanLiang.ErTai;
                        }
                        DataTable dt = CeDian.GetCeDian13(row["ceDianBianHao"].ToString());
                        this.dataGridView1.Rows[index].Cells["DiDian"].Value = dt.Rows[0]["ceDianWeiZhi"].ToString();
                        this.dataGridView1.Rows[index].Cells["MingCheng"].Value = dt.Rows[0]["xiaoLeiXing"].ToString();
                        if (cedian.KaiGuanLiang.ShiFouBaoJing)
                        {
                            if (cedian.KaiGuanLiang.BaoJingZhuangTai == 0)
                            {
                                this.dataGridView1.Rows[index].Cells["BaoJingZhuangTai"].Value = cedian.KaiGuanLiang.LingTai;
                            }
                            else if (cedian.KaiGuanLiang.BaoJingZhuangTai == 1)
                            {
                                this.dataGridView1.Rows[index].Cells["BaoJingZhuangTai"].Value = cedian.KaiGuanLiang.YiTai;
                            }
                            else if (cedian.KaiGuanLiang.BaoJingZhuangTai == 2)
                            {
                                this.dataGridView1.Rows[index].Cells["BaoJingZhuangTai"].Value = cedian.KaiGuanLiang.ErTai;
                            }
                            if (state == 1)
                            {
                                this.dataGridView1.Rows[index].Cells["BaoJingShiKe"].Value = this.dataGridView1.Rows[index].Cells["SheBeiZhuangTaiShiKe"].Value;
                            }
                        }
                        if (cedian.KaiGuanLiang.ShiFouDuanDian)
                        {
                            if (cedian.KaiGuanLiang.DuanDianZhuangTai == 0)
                            {
                                this.dataGridView1.Rows[index].Cells["DuanDianZhuangTai"].Value = cedian.KaiGuanLiang.LingTai;
                            }
                            else if (cedian.KaiGuanLiang.DuanDianZhuangTai == 1)
                            {
                                this.dataGridView1.Rows[index].Cells["DuanDianZhuangTai"].Value = cedian.KaiGuanLiang.YiTai;
                            }
                            else if (cedian.KaiGuanLiang.DuanDianZhuangTai == 2)
                            {
                                this.dataGridView1.Rows[index].Cells["DuanDianZhuangTai"].Value = cedian.KaiGuanLiang.ErTai;
                            }
                            if (state == 2)
                            {
                                this.dataGridView1.Rows[index].Cells["DuanDianShiKe"].Value = this.dataGridView1.Rows[index].Cells["SheBeiZhuangTaiShiKe"].Value;
                            }
                            this.dataGridView1.Rows[index].Cells["DuanDianQuYu"].Value = GlobalParams.AllCeDianList.GetDuanDianQuYu(row["ceDianBianHao"].ToString());
                        }
                        if (row["kuiDianYiChang"].ToString() == "True")
                        {
                            this.dataGridView1.Rows[index].Cells["KuiDianZhuangTai"].Value = this.dataGridView1.Rows[index].Cells["SheBeiZhuangTai"].Value;
                            this.dataGridView1.Rows[index].Cells["KuiDianShiKe"].Value = this.dataGridView1.Rows[index].Cells["SheBeiZhuangTaiShiKe"].Value;
                        }
                    }
                }
            }
        }

        private void Switch_status_Load(object sender, EventArgs e)
        {
            this.count = 1;
            this.Loadk(this.count);
        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalParams.PrintDataGridView(this.dataGridView1, "开关量状态显示列表");
        }

        private void 上一页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Loadk(--this.count);
        }

        private void 输入措施ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 下一页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Loadk(++this.count);
        }
    }
}

