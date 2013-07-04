namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;

    public class List_feed_analog : DockContent
    {
        private List<string> alarmCeDians;
        private DataGridViewTextBoxColumn BaoJingMenXian;
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
        private DataGridViewTextBoxColumn KuiDianYiChangShiKe;
        private DataGridViewTextBoxColumn MingCheng;
        private DateTime Ti;
        private byte Type;
        private ToolStripMenuItem 打印ToolStripMenuItem;
        private ToolStripMenuItem 上一页ToolStripMenuItem;
        private ToolStripMenuItem 输入措施ToolStripMenuItem;
        private ToolStripMenuItem 下一页ToolStripMenuItem;

        public List_feed_analog(byte type)
        {
            this.Type = type;
            this.InitializeComponent();
            this.alarmCeDians = new List<string>();
            this.config = ClientConfig.CreateCommon();
            if (this.Type == 0)
            {
                this.contextMenuStrip1.Items[0].Visible = false;
                this.contextMenuStrip1.Items[2].Visible = false;
                this.contextMenuStrip1.Items[3].Visible = false;
            }
        }

        private void dataGridView1_MouseEnter(object sender, EventArgs e)
        {
            YuJing.setValue(5);
        }

        private void dataGridView1_MouseLeave(object sender, EventArgs e)
        {
            YuJing.setValue(0);
        }

        private void deleteRow(int index)
        {
            this.dataGridView1.Rows.RemoveAt(index);
        }

        internal void Dispatch(ArrayList al)
        {
            foreach (string ceDian in al)
            {
                if (ceDian.Substring(2, 1) == "A")
                {
                    this.dataGridView1.Rows.Clear();
                    if (YuJing.getValue(5))
                    {
                        base.Activate();
                    }
                    DataTable dt = MoNiLiangLeiXing.GetAnalogAlarm(ceDian);
                    if (dt.Rows.Count != 0)
                    {
                        string tableName = "MoNiLiangValue" + DateTime.Now.ToString("yyyy-MM").Substring(0, 7).Replace('-', '_');
                        if (OperateDB.IsTableExist("max_cmss", tableName))
                        {
                            DataTable dtt = OperateDB.Select("select top 1 * from " + tableName + " where ceDianBianHao = '" + ceDian + "'order by uploadTime desc");
                            if (dtt.Rows.Count != 0)
                            {
                                int index = this.dataGridView1.Rows.Add();
                                this.dataGridView1.Rows[index].Cells["JianCeZhi"].Value = float.Parse(dtt.Rows[0]["uploadValue"].ToString()).ToString("#$0.00");
                                this.dataGridView1.Rows[index].Cells["GongZuoZhuangTai"].Value = this.State((byte) dtt.Rows[0]["state"]);
                                this.dataGridView1.Rows[index].Cells["KuiDianYiChangShiKe"].Value = dtt.Rows[0]["uploadTime"].ToString();
                                this.dataGridView1.Rows[index].Cells["CeDianBianHao"].Value = ceDian;
                                DataTable dt1 = CeDian.GetCeDian13(ceDian);
                                this.dataGridView1.Rows[index].Cells["DiDian"].Value = dt1.Rows[0]["ceDianWeiZhi"].ToString();
                                this.dataGridView1.Rows[index].Cells["MingCheng"].Value = dt1.Rows[0]["xiaoLeiXing"].ToString();
                                this.dataGridView1.Rows[index].Cells["DanWei"].Value = dt.Rows[0]["danWei"].ToString();
                                this.dataGridView1.Rows[index].Cells["BaoJingMenXian"].Value = dt.Rows[0]["baoJingZhiXiaXian"].ToString() + "~" + dt.Rows[0]["baoJingZhiShangXian"].ToString();
                                this.dataGridView1.Rows[index].Cells["DuanDianMenXian"].Value = dt.Rows[0]["duanDianZhi"].ToString();
                                this.dataGridView1.Rows[index].Cells["FuDianMenXian"].Value = dt.Rows[0]["fuDianZhi"].ToString();
                                this.dataGridView1.Rows[index].Cells["CuoShi"].Value = string.Empty;
                            }
                        }
                    }
                }
            }
        }

        internal void Dispatch(FenZhanRTdata tmpud, CeDian cedian, int index)
        {
            string ceDianBianHao = cedian.CeDianBianHao;
            float value = tmpud.realValue[index];
            if (ceDianBianHao == "03A01")
            {
            }
            if ((tmpud.tongDaoZhuangTai[index] == 14) && (value >= cedian.MoNiLiang.DuanDianZhi))
            {
                if (YuJing.getValue(5))
                {
                    base.Activate();
                }
                this.dataGridView1.BeginInvoke(new RefreshDataGridView(this.refreshDataGridView), new object[] { ceDianBianHao, cedian, tmpud.realValue[index], tmpud.tongDaoZhuangTai[index], tmpud.uploadTime });
                GlobalParams.alarm = true;
                GlobalParams.replayList.Add("模拟量，" + ceDianBianHao + "," + cedian.XiaoleiXing + "馈电异常");
            }
            else
            {
                int i = this.alarmCeDians.IndexOf(ceDianBianHao);
                if ((i >= 0) && (i < this.dataGridView1.Rows.Count))
                {
                    this.dataGridView1.BeginInvoke(new DeleteRow(this.deleteRow), new object[] { i });
                    this.alarmCeDians.RemoveAt(i);
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(List_feed_analog));
            this.dataGridView1 = new DataGridView();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.打印ToolStripMenuItem = new ToolStripMenuItem();
            this.输入措施ToolStripMenuItem = new ToolStripMenuItem();
            this.下一页ToolStripMenuItem = new ToolStripMenuItem();
            this.上一页ToolStripMenuItem = new ToolStripMenuItem();
            this.DiDian = new DataGridViewTextBoxColumn();
            this.CeDianBianHao = new DataGridViewTextBoxColumn();
            this.MingCheng = new DataGridViewTextBoxColumn();
            this.DanWei = new DataGridViewTextBoxColumn();
            this.BaoJingMenXian = new DataGridViewTextBoxColumn();
            this.DuanDianMenXian = new DataGridViewTextBoxColumn();
            this.FuDianMenXian = new DataGridViewTextBoxColumn();
            this.JianCeZhi = new DataGridViewTextBoxColumn();
            this.KuiDianYiChangShiKe = new DataGridViewTextBoxColumn();
            this.GongZuoZhuangTai = new DataGridViewTextBoxColumn();
            this.CuoShi = new DataGridViewTextBoxColumn();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Chocolate;
            dataGridViewCellStyle1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.DiDian, this.CeDianBianHao, this.MingCheng, this.DanWei, this.BaoJingMenXian, this.DuanDianMenXian, this.FuDianMenXian, this.JianCeZhi, this.KuiDianYiChangShiKe, this.GongZuoZhuangTai, this.CuoShi });
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 0x17;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new Size(0x37e, 380);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.MouseLeave += new EventHandler(this.dataGridView1_MouseLeave);
            this.dataGridView1.MouseEnter += new EventHandler(this.dataGridView1_MouseEnter);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.打印ToolStripMenuItem, this.输入措施ToolStripMenuItem, this.下一页ToolStripMenuItem, this.上一页ToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x77, 0x5c);
            this.打印ToolStripMenuItem.Name = "打印ToolStripMenuItem";
            this.打印ToolStripMenuItem.Size = new Size(0x76, 0x16);
            this.打印ToolStripMenuItem.Text = "打印";
            this.打印ToolStripMenuItem.Click += new EventHandler(this.打印ToolStripMenuItem_Click);
            this.输入措施ToolStripMenuItem.Name = "输入措施ToolStripMenuItem";
            this.输入措施ToolStripMenuItem.Size = new Size(0x76, 0x16);
            this.输入措施ToolStripMenuItem.Text = "输入措施";
            this.输入措施ToolStripMenuItem.Click += new EventHandler(this.输入措施ToolStripMenuItem_Click);
            this.下一页ToolStripMenuItem.Name = "下一页ToolStripMenuItem";
            this.下一页ToolStripMenuItem.Size = new Size(0x76, 0x16);
            this.下一页ToolStripMenuItem.Text = "下一页";
            this.下一页ToolStripMenuItem.Click += new EventHandler(this.下一页ToolStripMenuItem_Click);
            this.上一页ToolStripMenuItem.Name = "上一页ToolStripMenuItem";
            this.上一页ToolStripMenuItem.Size = new Size(0x76, 0x16);
            this.上一页ToolStripMenuItem.Text = "上一页";
            this.上一页ToolStripMenuItem.Click += new EventHandler(this.上一页ToolStripMenuItem_Click);
            this.DiDian.HeaderText = "地点";
            this.DiDian.Name = "DiDian";
            this.CeDianBianHao.HeaderText = "测点编号";
            this.CeDianBianHao.Name = "CeDianBianHao";
            this.MingCheng.HeaderText = "名称";
            this.MingCheng.Name = "MingCheng";
            this.DanWei.HeaderText = "单位";
            this.DanWei.Name = "DanWei";
            this.BaoJingMenXian.HeaderText = "报警门限";
            this.BaoJingMenXian.Name = "BaoJingMenXian";
            this.DuanDianMenXian.HeaderText = "断电门限";
            this.DuanDianMenXian.Name = "DuanDianMenXian";
            this.FuDianMenXian.HeaderText = "复电门限";
            this.FuDianMenXian.Name = "FuDianMenXian";
            this.JianCeZhi.HeaderText = "监测值";
            this.JianCeZhi.Name = "JianCeZhi";
            this.KuiDianYiChangShiKe.HeaderText = "馈电异常时刻";
            this.KuiDianYiChangShiKe.Name = "KuiDianYiChangShiKe";
            this.GongZuoZhuangTai.HeaderText = "工作状态";
            this.GongZuoZhuangTai.Name = "GongZuoZhuangTai";
            this.CuoShi.HeaderText = "措施";
            this.CuoShi.Name = "CuoShi";
            base.ClientSize = new Size(0x37e, 380);
            base.CloseButton = false;
            base.CloseButtonVisible = false;
            base.Controls.Add(this.dataGridView1);
            this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.Name = "List_feed_analog";
            this.Text = "模拟量馈电异常列表";
            base.Load += new EventHandler(this.List_feed_analog_Load);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void List_feed_analog_Load(object sender, EventArgs e)
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
            string tableN = "MoNiLiangValue" + DateTime.Now.ToString("yyyy-MM").Substring(0, 7).Replace('-', '_');
            if (OperateDB.IsTableExist("max_cmss", tableN))
            {
                DataTable Kdt = MoNiLiangBaoJing.GetKuiDianYiChangCeDian(tableN, k);
                foreach (DataRow row in Kdt.Rows)
                {
                    int index = this.dataGridView1.Rows.Add();
                    DataTable dt = MoNiLiangLeiXing.GetAnalogAlarm(row["ceDianBianHao"].ToString());
                    this.dataGridView1.Rows[index].Cells["CeDianBianHao"].Value = row["ceDianBianHao"].ToString();
                    this.dataGridView1.Rows[index].Cells["DanWei"].Value = dt.Rows[0]["danWei"].ToString();
                    this.dataGridView1.Rows[index].Cells["BaoJingMenXian"].Value = dt.Rows[0]["baoJingZhiXiaXian"].ToString() + "~" + dt.Rows[0]["baoJingZhiShangXian"].ToString();
                    this.dataGridView1.Rows[index].Cells["DuanDianMenXian"].Value = dt.Rows[0]["duanDianZhi"].ToString();
                    this.dataGridView1.Rows[index].Cells["FuDianMenXian"].Value = dt.Rows[0]["fuDianZhi"].ToString();
                    this.dataGridView1.Rows[index].Cells["KuiDianYiChangShiKe"].Value = row["uploadTime"].ToString();
                    this.dataGridView1.Rows[index].Cells["JianCeZhi"].Value = row["uploadValue"].ToString();
                    this.dataGridView1.Rows[index].Cells["GongZuoZhuangTai"].Value = this.State((byte) row["state"]);
                    dt = CeDian.GetCeDian13(row["ceDianBianHao"].ToString());
                    this.dataGridView1.Rows[index].Cells["DiDian"].Value = dt.Rows[0]["ceDianWeiZhi"].ToString();
                    this.dataGridView1.Rows[index].Cells["MingCheng"].Value = dt.Rows[0]["xiaoLeiXing"].ToString();
                    this.dataGridView1.Rows[index].Cells["CuoShi"].Value = Measure.GetMeasure(row["ceDianBianHao"].ToString(), (byte) 0, DateTime.Parse(row["uploadTime"].ToString()));
                }
            }
        }

        private void refreshDataGridView(string ceDianBianHao, CeDian cedian, float realValue, byte state, DateTime uploadTime)
        {
            if (this.alarmCeDians.Contains(ceDianBianHao))
            {
                int index = this.alarmCeDians.IndexOf(ceDianBianHao);
                this.dataGridView1[7, index].Value = realValue;
                this.dataGridView1[8, index].Value = uploadTime;
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
                this.dataGridView1.Rows[index2].Cells["KuiDianYiChangShiKe"].Value = uploadTime;
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
            this.dataGridView1.CurrentRow.Cells["CuoShi"].Value = Measure.GetMeasure(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString(), (byte) 0, this.Ti);
        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalParams.PrintDataGridView(this.dataGridView1, "模拟量馈电异常列表");
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
                this.Ti = DateTime.Parse(this.dataGridView1.CurrentRow.Cells["KuiDianYiChangShiKe"].Value.ToString());
                new Form_measure(this, this.dataGridView1.CurrentRow.Cells["CeDianBianHao"].Value.ToString(), this.Ti, 0).Show();
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

