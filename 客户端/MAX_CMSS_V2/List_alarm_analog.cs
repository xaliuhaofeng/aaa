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
    using WeifenLuo.WinFormsUI.Docking;

    public class List_alarm_analog : DockContent
    {
        private List<string> alarmCeDians;
        private DataGridViewTextBoxColumn BaoJingMenXian;
        private DataGridViewTextBoxColumn BaoJingShiKe;
        private DataGridViewTextBoxColumn CeDianBianHao;
        private IContainer components = null;
        private ClientConfig config;
        private ContextMenuStrip contextMenuStrip1;
        private int count = 0;
        private DataGridViewTextBoxColumn CuoShi;
        private DataGridViewTextBoxColumn DanWei;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn DiDian;
        private DataGridViewTextBoxColumn DuanDianMenXian;
        private DataGridViewTextBoxColumn FuDianMenXian;
        private DataGridViewTextBoxColumn GongZuoZhuangTai;
        private DataGridViewTextBoxColumn JianCeZhi;
        private DateTime lastAlarm = DateTime.Now;
        private DataGridViewTextBoxColumn MingCheng;
        private DateTime Ti;
        private byte Type;
        private ToolStripMenuItem 查找下一页ToolStripMenuItem;
        private ToolStripMenuItem 打印ToolStripMenuItem;
        private ToolStripMenuItem 上一页ToolStripMenuItem;
        private ToolStripMenuItem 输入措施ToolStripMenuItem;

        public List_alarm_analog(byte type)
        {
            this.InitializeComponent();
            this.config = ClientConfig.CreateCommon();
            this.alarmCeDians = new List<string>();
            this.Type = type;
            if (this.Type == 0)
            {
                this.contextMenuStrip1.Items[0].Visible = false;
                this.contextMenuStrip1.Items[2].Visible = false;
                this.contextMenuStrip1.Items[3].Visible = false;
            }
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

        private void dataGridView1_MouseEnter(object sender, EventArgs e)
        {
            YuJing.setValue(1);
        }

        private void dataGridView1_MouseLeave(object sender, EventArgs e)
        {
            YuJing.setValue(0);
        }

        private void deleteRow(int index)
        {
            this.dataGridView1.Rows.RemoveAt(index);
        }

        internal void Dispatch(FenZhanRTdata tmpud, CeDian cedian, int index)
        {
            string ceDianBianHao = cedian.CeDianBianHao;
            float temp = tmpud.realValue[index];
            if ((((cedian.MoNiLiang.BaoJingLeiXing == 0) && (temp > cedian.MoNiLiang.BaoJingZhiShangXian)) || ((cedian.MoNiLiang.BaoJingLeiXing == 2) && (temp < cedian.MoNiLiang.BaoJingZhiXiaXian))) || (((cedian.MoNiLiang.BaoJingLeiXing == 1) && (cedian.MoNiLiang.BaoJingZhiShangXian > temp)) && (cedian.MoNiLiang.BaoJingZhiXiaXian < temp)))
            {
                this.dataGridView1.BeginInvoke(new RefreshDataGridView(this.refreshDataGridView), new object[] { ceDianBianHao, cedian, tmpud.realValue[index], tmpud.tongDaoZhuangTai[index], tmpud.uploadTime });
                GlobalParams.alarm = true;
                GlobalParams.warnList.Add("模拟量，" + ceDianBianHao + "," + cedian.XiaoleiXing + "报警");
            }
            else
            {
                int i = this.alarmCeDians.IndexOf(ceDianBianHao);
                if ((i >= 0) && (i < this.dataGridView1.Rows.Count))
                {
                    this.dataGridView1.BeginInvoke(new DeleteRow(this.deleteRow), new object[] { i });
                    this.alarmCeDians.RemoveAt(i);
                }
                if (this.dataGridView1.Rows.Count == 0)
                {
                    GlobalParams.alarm = false;
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
            this.components = new Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(List_alarm_analog));
            this.dataGridView1 = new DataGridView();
            this.DiDian = new DataGridViewTextBoxColumn();
            this.CeDianBianHao = new DataGridViewTextBoxColumn();
            this.MingCheng = new DataGridViewTextBoxColumn();
            this.DanWei = new DataGridViewTextBoxColumn();
            this.BaoJingMenXian = new DataGridViewTextBoxColumn();
            this.DuanDianMenXian = new DataGridViewTextBoxColumn();
            this.FuDianMenXian = new DataGridViewTextBoxColumn();
            this.JianCeZhi = new DataGridViewTextBoxColumn();
            this.BaoJingShiKe = new DataGridViewTextBoxColumn();
            this.GongZuoZhuangTai = new DataGridViewTextBoxColumn();
            this.CuoShi = new DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.打印ToolStripMenuItem = new ToolStripMenuItem();
            this.输入措施ToolStripMenuItem = new ToolStripMenuItem();
            this.查找下一页ToolStripMenuItem = new ToolStripMenuItem();
            this.上一页ToolStripMenuItem = new ToolStripMenuItem();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Chocolate;
            dataGridViewCellStyle1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            dataGridViewCellStyle1.ForeColor = SystemColors.ButtonFace;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.DiDian, this.CeDianBianHao, this.MingCheng, this.DanWei, this.BaoJingMenXian, this.DuanDianMenXian, this.FuDianMenXian, this.JianCeZhi, this.BaoJingShiKe, this.GongZuoZhuangTai, this.CuoShi });
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 0x17;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new Size(0x371, 0x2e6);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.MouseEnter += new EventHandler(this.dataGridView1_MouseEnter);
            this.dataGridView1.MouseLeave += new EventHandler(this.dataGridView1_MouseLeave);
            this.DiDian.FillWeight = 154.1068f;
            this.DiDian.HeaderText = "地点";
            this.DiDian.Name = "DiDian";
            this.CeDianBianHao.FillWeight = 170.7976f;
            this.CeDianBianHao.HeaderText = "测点编号";
            this.CeDianBianHao.Name = "CeDianBianHao";
            this.MingCheng.FillWeight = 124.1424f;
            this.MingCheng.HeaderText = "名称";
            this.MingCheng.Name = "MingCheng";
            this.DanWei.FillWeight = 115.6706f;
            this.DanWei.HeaderText = "单位";
            this.DanWei.Name = "DanWei";
            this.BaoJingMenXian.FillWeight = 126.2592f;
            this.BaoJingMenXian.HeaderText = "报警门限";
            this.BaoJingMenXian.Name = "BaoJingMenXian";
            this.DuanDianMenXian.FillWeight = 116.1716f;
            this.DuanDianMenXian.HeaderText = "断电门限";
            this.DuanDianMenXian.Name = "DuanDianMenXian";
            this.FuDianMenXian.FillWeight = 104.4621f;
            this.FuDianMenXian.HeaderText = "复电门限";
            this.FuDianMenXian.Name = "FuDianMenXian";
            this.JianCeZhi.FillWeight = 99.57573f;
            this.JianCeZhi.HeaderText = "监测值";
            this.JianCeZhi.Name = "JianCeZhi";
            this.BaoJingShiKe.FillWeight = 88.74565f;
            this.BaoJingShiKe.HeaderText = "报警时刻";
            this.BaoJingShiKe.Name = "BaoJingShiKe";
            this.GongZuoZhuangTai.FillWeight = 76.77388f;
            this.GongZuoZhuangTai.HeaderText = "工作状态";
            this.GongZuoZhuangTai.Name = "GongZuoZhuangTai";
            this.CuoShi.FillWeight = 93.67615f;
            this.CuoShi.HeaderText = "措施";
            this.CuoShi.Name = "CuoShi";
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.打印ToolStripMenuItem, this.输入措施ToolStripMenuItem, this.查找下一页ToolStripMenuItem, this.上一页ToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x7d, 0x5c);
            this.打印ToolStripMenuItem.Name = "打印ToolStripMenuItem";
            this.打印ToolStripMenuItem.Size = new Size(0x7c, 0x16);
            this.打印ToolStripMenuItem.Text = "打印";
            this.打印ToolStripMenuItem.Click += new EventHandler(this.打印ToolStripMenuItem_Click);
            this.输入措施ToolStripMenuItem.Name = "输入措施ToolStripMenuItem";
            this.输入措施ToolStripMenuItem.Size = new Size(0x7c, 0x16);
            this.输入措施ToolStripMenuItem.Text = "输入措施";
            this.输入措施ToolStripMenuItem.Click += new EventHandler(this.输入措施ToolStripMenuItem_Click);
            this.查找下一页ToolStripMenuItem.Name = "查找下一页ToolStripMenuItem";
            this.查找下一页ToolStripMenuItem.Size = new Size(0x7c, 0x16);
            this.查找下一页ToolStripMenuItem.Text = "下一页";
            this.查找下一页ToolStripMenuItem.Click += new EventHandler(this.下一页ToolStripMenuItem_Click);
            this.上一页ToolStripMenuItem.Name = "上一页ToolStripMenuItem";
            this.上一页ToolStripMenuItem.Size = new Size(0x7c, 0x16);
            this.上一页ToolStripMenuItem.Text = "上一页";
            this.上一页ToolStripMenuItem.Click += new EventHandler(this.上一页ToolStripMenuItem_Click);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit; 
            this.AutoScroll = true;
            base.ClientSize = new Size(0x371, 0x2e6);
            base.CloseButton = false;
            base.CloseButtonVisible = false;
            base.Controls.Add(this.dataGridView1);
            this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.Name = "List_alarm_analog";
            this.Text = "模拟量报警列表";
            base.Load += new EventHandler(this.List_alarm_analog_Load);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void List_alarm_analog_Load(object sender, EventArgs e)
        {
            if (this.Type != 0)
            {
                this.count = 1;
                this.Loadk(this.count);
            }
        }

        private void Loadk(int k)
        {
            this.dataGridView1.Rows.Clear();
            DataTable Mdt = MoNiLiangBaoJing.GetBaoJingCeDian(k);
            if ((Mdt != null) && (Mdt.Rows.Count != 0))
            {
                foreach (DataRow row in Mdt.Rows)
                {
                    int index = this.dataGridView1.Rows.Add();
                    DataTable dt = MoNiLiangLeiXing.GetAnalogAlarm(row["ceDianBianHao"].ToString());
                    this.dataGridView1.Rows[index].Cells["CeDianBianHao"].Value = row["ceDianBianHao"].ToString();
                    this.dataGridView1.Rows[index].Cells["GongZuoZhuangTai"].Value = this.State((byte) row["state"]);
                    this.dataGridView1.Rows[index].Cells["DanWei"].Value = dt.Rows[0]["danWei"].ToString();
                    this.dataGridView1.Rows[index].Cells["BaoJingMenXian"].Value = dt.Rows[0]["baoJingZhiXiaXian"].ToString() + "~" + dt.Rows[0]["baoJingZhiShangXian"].ToString();
                    this.dataGridView1.Rows[index].Cells["DuanDianMenXian"].Value = dt.Rows[0]["duanDianZhi"].ToString();
                    this.dataGridView1.Rows[index].Cells["FuDianMenXian"].Value = dt.Rows[0]["fuDianZhi"].ToString();
                    this.dataGridView1.Rows[index].Cells["BaoJingShiKe"].Value = row["uploadTime"].ToString();
                    this.dataGridView1.Rows[index].Cells["JianCeZhi"].Value = row["valueReceive"].ToString();
                    dt = CeDian.GetCeDian13(row["ceDianBianHao"].ToString());
                    this.dataGridView1.Rows[index].Cells["DiDian"].Value = dt.Rows[0]["ceDianWeiZhi"].ToString();
                    this.dataGridView1.Rows[index].Cells["MingCheng"].Value = dt.Rows[0]["xiaoLeiXing"].ToString();
                    this.dataGridView1.Rows[index].Cells["CuoShi"].Value = Measure.GetMeasure(row["ceDianBianHao"].ToString(), (byte) 1, DateTime.Parse(row["uploadTime"].ToString()));
                }
            }
        }

        private void refreshDataGridView(string ceDianBianHao, CeDian cedian, float realValue, byte state, DateTime uploadTime)
        {
            if (this.alarmCeDians.Contains(ceDianBianHao))
            {
                int index = this.alarmCeDians.IndexOf(ceDianBianHao);
                this.dataGridView1[7, index].Value = realValue;
                this.dataGridView1[9, index].Value = this.State(state);
            }
            else
            {
                int index2 = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index2].Cells["JianCeZhi"].Value = realValue;
                this.dataGridView1.Rows[index2].Cells["CeDianBianHao"].Value = ceDianBianHao;
                this.dataGridView1.Rows[index2].Cells["DiDian"].Value = cedian.CeDianWeiZhi;
                this.dataGridView1.Rows[index2].Cells["MingCheng"].Value = cedian.XiaoleiXing;
                this.dataGridView1.Rows[index2].Cells["DanWei"].Value = cedian.MoNiLiang.DanWei;
                this.dataGridView1.Rows[index2].Cells["BaoJingMenXian"].Value = cedian.MoNiLiang.BaoJingZhiXiaXian + "~" + cedian.MoNiLiang.BaoJingZhiShangXian;
                this.dataGridView1.Rows[index2].Cells["DuanDianMenXian"].Value = cedian.MoNiLiang.DuanDianZhi;
                this.dataGridView1.Rows[index2].Cells["FuDianMenXian"].Value = cedian.MoNiLiang.FuDianZhi;
                this.dataGridView1.Rows[index2].Cells["BaoJingShiKe"].Value = uploadTime;
                this.dataGridView1.Rows[index2].Cells["GongZuoZhuangTai"].Value = this.State(state);
                this.dataGridView1.Rows[index2].Cells["CuoShi"].Value = string.Empty;
                this.alarmCeDians.Add(ceDianBianHao);
            }
        }

        private string State(byte s)
        {
            switch (s)
            {
                case 0:
                    return "正常";

                case 1:
                    return "报警";

                case 2:
                    return "断电";

                case 3:
                    return "复电";

                case 4:
                    return "断线";

                case 5:
                    return "溢出";

                case 6:
                    return "负漂";

                case 7:
                    return "故障";

                case 8:
                    return "I/O错误";
            }
            return "其它";
        }

        public void UpdateL()
        {
            this.dataGridView1.CurrentRow.Cells["CuoShi"].Value = Measure.GetMeasure(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString(), (byte) 1, this.Ti);
        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalParams.PrintDataGridView(this.dataGridView1, "模拟量报警列表");
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
                this.Ti = DateTime.Parse(this.dataGridView1.CurrentRow.Cells["BaoJingShiKe"].Value.ToString());
                new Form_measure(this, this.dataGridView1.CurrentRow.Cells["CeDianBianHao"].Value.ToString(), this.Ti, 1).Show();
            }
        }

        private void 下一页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Loadk(++this.count);
        }

        private delegate void DeleteRow(int index);

        private delegate void RefreshDataGridView(string cedianbianhao, CeDian cedian, float realValue, byte state, DateTime date);
    }
}

