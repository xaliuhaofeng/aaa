namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Timers;
    using System.Windows.Forms;

    public class FenZhanZhuanTai : UserControl
    {
        private byte[] AC;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private string[] fenzhans;
        private Dictionary<byte, FenZhanRTdata> predata;
        private int[] state;
        private System.Windows.Forms.Timer timer1;
        private System.Timers.Timer[] timers;

        public FenZhanZhuanTai()
        {
            this.InitializeComponent();
            this.dataGridView2.Visible = false;
            this.predata = new Dictionary<byte, FenZhanRTdata>();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FenZhanRTdata ud = null;
            if (e.RowIndex >= 0)
            {
                int i;
                int index;
                this.dataGridView1.Visible = false;
                this.dataGridView2.Visible = true;
                this.dataGridView2.Rows.Clear();
                string fenzhanhao = this.dataGridView1[0, e.RowIndex].Value.ToString();
                if (fenzhanhao.Length == 1)
                {
                    fenzhanhao = "0" + fenzhanhao;
                }
                if (this.predata.ContainsKey(Convert.ToByte(fenzhanhao)))
                {
                    ud = this.predata[Convert.ToByte(fenzhanhao)];
                }
                for (i = 1; i <= 0x10; i++)
                {
                    index = this.dataGridView2.Rows.Add();
                    this.dataGridView2[0, index].Value = i;
                    this.dataGridView2[1, index].Value = "IO通道";
                    string tongdaohao = i.ToString();
                    if (i < 10)
                    {
                        tongdaohao = "0" + tongdaohao;
                    }
                    CeDian cedian = null;
                    if (GlobalParams.AllCeDianList.allcedianlist.ContainsKey(fenzhanhao + "A" + tongdaohao))
                    {
                        cedian = GlobalParams.AllCeDianList.allcedianlist[fenzhanhao + "A" + tongdaohao];
                    }
                    else if (GlobalParams.AllCeDianList.allcedianlist.ContainsKey(fenzhanhao + "D" + tongdaohao))
                    {
                        cedian = GlobalParams.AllCeDianList.allcedianlist[fenzhanhao + "D" + tongdaohao];
                    }
                    if (cedian != null)
                    {
                        this.dataGridView2[2, index].Value = cedian.CeDianWeiZhi;
                        this.dataGridView2[3, index].Value = cedian.XiaoleiXing;
                        this.dataGridView2[4, index].Value = cedian.CeDianBianHao;
                        if (ud != null)
                        {
                            if ((ud.tongDaoZhuangTai[i] != 6) && (ud.realValue[i] < 0f))
                            {
                                ud.realValue[i] = 0f;
                            }
                            this.dataGridView2[5, index].Value = ud.realValue[i];
                            this.dataGridView2[6, index].Value = this.getState(ud, i, cedian);
                        }
                    }
                }
                for (i = 1; i <= 8; i++)
                {
                    index = this.dataGridView2.Rows.Add();
                    this.dataGridView2[0, index].Value = i;
                    this.dataGridView2[1, index].Value = "控制通道";
                    DataTable dt = KongZhiLiangCeDian.GetKongZhiLiangCeDian(Convert.ToInt32(fenzhanhao), i);
                    if ((dt != null) && (dt.Rows.Count != 0))
                    {
                        this.dataGridView2[2, index].Value = dt.Rows[0]["ceDianWeiZhi"];
                        this.dataGridView2[3, index].Value = dt.Rows[0]["mingCheng"];
                        this.dataGridView2[4, index].Value = dt.Rows[0]["ceDianBianHao"];
                        if (ud != null)
                        {
                            this.dataGridView2[5, index].Value = ud.kongZhiLiangZhuangTai[i];
                        }
                    }
                }
            }
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            this.dataGridView2.Visible = false;
            this.dataGridView1.Visible = true;
        }

        public void Dispatch(FenZhanRTdata ud, Dictionary<string, CeDian> allCeDian)
        {
            if (!ud.isResponse)
            {
                int index = -1;
                this.predata[ud.fenZhanHao] = ud;
                string fenzhanhao = ud.fenZhanHao.ToString();
                for (int i = 0; i < this.fenzhans.Length; i++)
                {
                    if (this.fenzhans[i] == fenzhanhao)
                    {
                        index = i;
                        break;
                    }
                }
                if ((index >= 0) && (index < this.state.Length))
                {
                    this.state[index] = 0;
                    this.AC[index] = ud.AC;
                    this.timers[index].Stop();
                    this.timers[index].Start();
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

        private void FenZhanZhuanTai_Load(object sender, EventArgs e)
        {
            int i;
            this.fenzhans = FenZhan.GetAllConfigedFenZhan();
            this.timers = new System.Timers.Timer[this.fenzhans.Length];
            for (i = 0; i < this.timers.Length; i++)
            {
                this.timers[i] = new System.Timers.Timer(6000.0);
                this.timers[i].Elapsed += new ElapsedEventHandler(this.timers_Elapsed);
                this.timers[i].Enabled = true;
            }
            this.state = new int[this.fenzhans.Length];
            for (i = 0; i < this.fenzhans.Length; i++)
            {
                int index = this.dataGridView1.Rows.Add();
                this.state[i] = 1;
                this.dataGridView1[0, index].Value = this.fenzhans[i];
                this.dataGridView1[2, index].Value = "等待。。。";
            }
            this.AC = new byte[this.fenzhans.Length];
            for (i = 0; i < this.fenzhans.Length; i++)
            {
                this.AC[i] = 0;
            }
        }

        private string getState(FenZhanRTdata ud, int index, CeDian cedian)
        {
            string sstate = string.Empty;
            if (ud.isMoNiLiang[index])
            {
                switch (ud.tongDaoZhuangTai[index])
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
                }
                return sstate;
            }
            switch (((byte) ud.realValue[index]))
            {
                case 0:
                    return cedian.KaiGuanLiang.LingTai;

                case 1:
                    return cedian.KaiGuanLiang.YiTai;

                case 2:
                    return cedian.KaiGuanLiang.ErTai;
            }
            return sstate;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column10,
            this.Column2});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(713, 417);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "分站号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "供电状态";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "通信状态";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(713, 417);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.DoubleClick += new System.EventHandler(this.dataGridView2_DoubleClick);
            // 
            // Column3
            // 
            this.Column3.HeaderText = "通道号";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "通道类型";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "安装地点";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "名称";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "测点编号";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "测值";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "状态";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // FenZhanZhuanTai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FenZhanZhuanTai";
            this.Size = new System.Drawing.Size(713, 417);
            this.Load += new System.EventHandler(this.FenZhanZhuanTai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < this.fenzhans.Length; i++)
            {
                int b = this.state[i];
                string s = "等待。。。";
                string a = "未知";
                switch (b)
                {
                    case 0:
                        s = "正常";
                        a = "交流供电";
                        if (this.AC[i] == 1)
                        {
                            a = "直流供电";
                        }
                        break;

                    case 1:
                        s = "超时";
                        break;

                    case 2:
                        s = "通信失败";
                        break;
                }
                this.dataGridView1[1, i].Value = a;
                this.dataGridView1[2, i].Value = s;
            }
            this.dataGridView1.Refresh();
        }

        private void timers_Elapsed(object sender, ElapsedEventArgs e)
        {
            System.Timers.Timer timer = (System.Timers.Timer) sender;
            for (int i = 0; i < this.timers.Length; i++)
            {
                if (timer == this.timers[i])
                {
                    this.state[i] = 1;
                }
            }
        }
    }
}

