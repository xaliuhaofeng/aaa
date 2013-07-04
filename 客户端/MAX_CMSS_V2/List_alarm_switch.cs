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

    public class List_alarm_switch : DockContent
    {
        private List<string> alarmCeDians;
        private DataGridViewTextBoxColumn CeDianBianHao;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private int count = 0;
        private DataGridViewTextBoxColumn CuoShi;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn DiDian;
        private DataGridViewTextBoxColumn JianCeZhuangTai;
        private DataGridViewTextBoxColumn KaiTingShiKe;
        private DataGridViewTextBoxColumn MingCheng;
        private DateTime Ti;
        private byte Type;
        private ToolStripMenuItem 打印ToolStripMenuItem;
        private ToolStripMenuItem 上一页ToolStripMenuItem;
        private ToolStripMenuItem 输入措施ToolStripMenuItem;
        private ToolStripMenuItem 下一页ToolStripMenuItem;

        public List_alarm_switch(byte type)
        {
            this.Type = type;
            this.InitializeComponent();
            this.alarmCeDians = new List<string>();
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
            YuJing.setValue(2);
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
            if (cedian.KaiGuanLiang.ShiFouBaoJing)
            {
                int temp = (int) tmpud.realValue[index];
                if (cedian.KaiGuanLiang.BaoJingZhuangTai == temp)
                {
                    string zhuangTai = "";
                    if (((int) tmpud.realValue[index]) == 0)
                    {
                        zhuangTai = cedian.KaiGuanLiang.LingTai;
                    }
                    else if (((int) tmpud.realValue[index]) == 1)
                    {
                        zhuangTai = cedian.KaiGuanLiang.YiTai;
                    }
                    else if (((int) tmpud.realValue[index]) == 2)
                    {
                        zhuangTai = cedian.KaiGuanLiang.ErTai;
                    }
                    this.dataGridView1.BeginInvoke(new RefreshDataGridView(this.refreshDataGridView), new object[] { ceDianBianHao, cedian, zhuangTai, tmpud.uploadTime });
                    GlobalParams.warnList.Add("开关量，" + ceDianBianHao + "," + cedian.XiaoleiXing + "报警");
                }
                else
                {
                    int i = this.alarmCeDians.IndexOf(ceDianBianHao);
                    if ((i >= 0) && (i < this.alarmCeDians.Count))
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(List_alarm_switch));
            this.dataGridView1 = new DataGridView();
            this.DiDian = new DataGridViewTextBoxColumn();
            this.CeDianBianHao = new DataGridViewTextBoxColumn();
            this.MingCheng = new DataGridViewTextBoxColumn();
            this.JianCeZhuangTai = new DataGridViewTextBoxColumn();
            this.KaiTingShiKe = new DataGridViewTextBoxColumn();
            this.CuoShi = new DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.打印ToolStripMenuItem = new ToolStripMenuItem();
            this.输入措施ToolStripMenuItem = new ToolStripMenuItem();
            this.下一页ToolStripMenuItem = new ToolStripMenuItem();
            this.上一页ToolStripMenuItem = new ToolStripMenuItem();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
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
            this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.DiDian, this.CeDianBianHao, this.MingCheng, this.JianCeZhuangTai, this.KaiTingShiKe, this.CuoShi });
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Dock = DockStyle.Fill;
            this.dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 0x17;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new Size(0x380, 0x1cb);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.MouseEnter += new EventHandler(this.dataGridView1_MouseEnter);
            this.dataGridView1.MouseLeave += new EventHandler(this.dataGridView1_MouseLeave);
            this.DiDian.HeaderText = "地点";
            this.DiDian.Name = "DiDian";
            this.CeDianBianHao.HeaderText = "测点编号";
            this.CeDianBianHao.Name = "CeDianBianHao";
            this.MingCheng.HeaderText = "名称";
            this.MingCheng.Name = "MingCheng";
            this.JianCeZhuangTai.HeaderText = "监测状态";
            this.JianCeZhuangTai.Name = "JianCeZhuangTai";
            this.KaiTingShiKe.HeaderText = "开/停时刻";
            this.KaiTingShiKe.Name = "KaiTingShiKe";
            this.CuoShi.HeaderText = "措施";
            this.CuoShi.Name = "CuoShi";
            this.CuoShi.ReadOnly = true;
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.打印ToolStripMenuItem, this.输入措施ToolStripMenuItem, this.下一页ToolStripMenuItem, this.上一页ToolStripMenuItem });
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
            this.下一页ToolStripMenuItem.Name = "下一页ToolStripMenuItem";
            this.下一页ToolStripMenuItem.Size = new Size(0x7c, 0x16);
            this.下一页ToolStripMenuItem.Text = "下一页";
            this.下一页ToolStripMenuItem.Click += new EventHandler(this.下一页ToolStripMenuItem_Click);
            this.上一页ToolStripMenuItem.Name = "上一页ToolStripMenuItem";
            this.上一页ToolStripMenuItem.Size = new Size(0x7c, 0x16);
            this.上一页ToolStripMenuItem.Text = "上一页";
            this.上一页ToolStripMenuItem.Click += new EventHandler(this.上一页ToolStripMenuItem_Click);
            base.ClientSize = new Size(0x380, 0x1cb);
            base.CloseButton = false;
            base.CloseButtonVisible = false;
            base.Controls.Add(this.dataGridView1);
            this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.Name = "List_alarm_switch";
            this.Text = "开关量报警列表";
            base.Load += new EventHandler(this.List_alarm_switch_Load);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void List_alarm_switch_Load(object sender, EventArgs e)
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
            DataTable Adt = KaiGuanLiangBaoJing.GetBaoJingCeDian(k);
            if ((Adt != null) && (Adt.Rows.Count != 0))
            {
                foreach (DataRow row in Adt.Rows)
                {
                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells["CeDianBianHao"].Value = row["ceDianBianHao"].ToString();
                    this.dataGridView1.Rows[index].Cells["KaiTingShiKe"].Value = row["uploadTime"].ToString();
                    this.dataGridView1.Rows[index].Cells["JianCeZhuangTai"].Value = row["zhuangTai"].ToString();
                    DataTable dt = CeDian.GetCeDian13(row["ceDianBianHao"].ToString());
                    this.dataGridView1.Rows[index].Cells["DiDian"].Value = dt.Rows[0]["ceDianWeiZhi"].ToString();
                    this.dataGridView1.Rows[index].Cells["MingCheng"].Value = dt.Rows[0]["xiaoLeiXing"].ToString();
                    this.dataGridView1.Rows[index].Cells["CuoShi"].Value = Measure.GetMeasure(row["ceDianBianHao"].ToString(), (byte) 3, DateTime.Parse(row["uploadTime"].ToString()));
                }
            }
        }

        private void refreshDataGridView(string cedianbianhao, CeDian cedian, string state, DateTime date)
        {
            int index;
            if (this.alarmCeDians.Contains(cedianbianhao))
            {
                index = this.alarmCeDians.IndexOf(cedianbianhao);
                this.dataGridView1[3, index].Value = state;
            }
            else
            {
                index = this.dataGridView1.Rows.Add();
                this.dataGridView1[0, index].Value = cedian.CeDianWeiZhi;
                this.dataGridView1[1, index].Value = cedianbianhao;
                this.dataGridView1[2, index].Value = cedian.XiaoleiXing;
                this.dataGridView1[3, index].Value = state;
                this.dataGridView1[4, index].Value = date;
                this.alarmCeDians.Add(cedianbianhao);
            }
        }

        public void UpdateL()
        {
            this.dataGridView1.CurrentRow.Cells["CuoShi"].Value = Measure.GetMeasure(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString(), (byte) 3, this.Ti);
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
                this.Ti = DateTime.Parse(this.dataGridView1.CurrentRow.Cells["KaiTingShiKe"].Value.ToString());
                new Form_measure(this, this.dataGridView1.CurrentRow.Cells["CeDianBianHao"].Value.ToString(), DateTime.Parse(this.dataGridView1.CurrentRow.Cells["KaiTingShiKe"].Value.ToString()), 3).Show();
            }
        }

        private void 下一页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Loadk(++this.count);
        }

        private delegate void DeleteRow(int index);

        private delegate void RefreshDataGridView(string cedianbianhao, CeDian cedian, string state, DateTime date);
    }
}

