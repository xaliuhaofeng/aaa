namespace MAX_CMSS_V2
{
    using Logic;
    using MAX_CMSS_V2.Curve;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;

    public class LieBiaoKuang : DataGridView
    {
        private delegate void RefreshDataGridView(int rowno, string stateVal, string color, string time, string cutarea, string cdbh);

        private ArrayList ceDianBianHaos;
        private ClientConfig clientConfig;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ContextMenuStrip contextMenuStrip2;
        public int currentRow;
        private List_show father;
        private int lieBiaoKuangBianHao;
        private MainForm mf_g;
       
        
        private RealTimeCurveForm realCurve;
        private ToolStripMenuItem Substation_ToolStripMenuItem;
        public bool xianShi;
        public string yeKuangMingCheng;
        private DataGridViewTextBoxColumn 报警上限;
        private ToolStripMenuItem 报警上限ToolStripMenuItem;
        private DataGridViewTextBoxColumn 报警时刻;
        private ToolStripMenuItem 报警时刻ToolStripMenuItem;
        private DataGridViewTextBoxColumn 报警下限;
        private ToolStripMenuItem 报警下限ToolStripMenuItem;
        private DataGridViewTextBoxColumn 测点编号;
        private ToolStripMenuItem 测点编号ToolStripMenuItem;
        private ToolStripMenuItem 措施ToolStripMenuItem;
        private ToolStripMenuItem 打印ToolStripMenuItem;
        private DataGridViewTextBoxColumn 单位;
        private ToolStripMenuItem 单位ToolStripMenuItem;
        private DataGridViewTextBoxColumn 地点名称;
        private ToolStripMenuItem 地点名称ToolStripMenuItem;
        private ToolStripMenuItem 断点区域ToolStripMenuItem;
        private DataGridViewTextBoxColumn 断电范围;
        private ToolStripMenuItem 断电范围ToolStripMenuItem;
        private DataGridViewTextBoxColumn 断电区域;
        private DataGridViewTextBoxColumn 断电时刻;
        private ToolStripMenuItem 断电时刻ToolStripMenuItem;
        private DataGridViewTextBoxColumn 断电值;
        private ToolStripMenuItem 断电值ToolStripMenuItem;
        private DataGridViewTextBoxColumn 复电时刻;
        private ToolStripMenuItem 复电时刻ToolStripMenuItem;
        private DataGridViewTextBoxColumn 复电值;
        private ToolStripMenuItem 复电值ToolStripMenuItem;
        private DataGridViewTextBoxColumn 馈电状态及时刻;
        private ToolStripMenuItem 馈电状态及时刻ToolStripMenuItem;
        private ToolStripMenuItem 历史数据ToolStripMenuItem;
        private DataGridViewTextBoxColumn 量程低值;
        private ToolStripMenuItem 量程低值ToolStripMenuItem;
        private DataGridViewTextBoxColumn 量程高值;
        private ToolStripMenuItem 量程高值ToolStripMenuItem;
        private ToolStripMenuItem 列表标头设置ToolStripMenuItem;
        private ToolStripMenuItem 列表调整toolStripMenuItem;
        private DataGridViewTextBoxColumn 平均值;
        private ToolStripMenuItem 平均值ToolStripMenuItem;
        private ToolStripMenuItem 曲线显示ToolStripMenuItem;
        private ToolStripMenuItem 删除ToolStripMenuItem;
        private DataGridViewTextBoxColumn 变值时刻;
        private ToolStripMenuItem 时间toolStripMenuItem;
        private ToolStripMenuItem 实时曲线ToolStripMenuItem;
        private ToolStripMenuItem 添加控制通道ToolStripMenuItem;
        private ToolStripMenuItem 调教toolStripMenuItem;
        private DataGridViewImageColumn 图标;
        private ToolStripMenuItem 图标ToolStripMenuItem;
        private ToolStripMenuItem 修改ToolStripMenuItem;
        private ToolStripMenuItem 选择显示测点ToolStripMenuItem;
        private DataGridViewTextBoxColumn 值状态;
        private ToolStripMenuItem 值状态ToolStripMenuItem;
        private ToolStripMenuItem 状态图及柱状图ToolStripMenuItem;
        private DataGridViewTextBoxColumn 最大值;
        private ToolStripMenuItem 最大值ToolStripMenuItem;
        private DataGridViewTextBoxColumn 最小值;
        private ToolStripMenuItem 最小值ToolStripMenuItem;

        public LieBiaoKuang(string yeKuangMingCheng, int lieBiaoKuangBianHao, MainForm mf, bool isOpen, List_show father)
        {
            int i;
            this.components = null;
            this.InitializeComponent();
            this.mf_g = mf;
            this.father = father;
            this.yeKuangMingCheng = yeKuangMingCheng;
            this.realCurve = null;
            base.DefaultCellStyle.Font = new Font("宋体", 14f, FontStyle.Bold);
            base.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14f, FontStyle.Bold);

          //  base.Columns["地点名称"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; 

           
            this.lieBiaoKuangBianHao = lieBiaoKuangBianHao;
            if(ceDianBianHaos==null)
                this.ceDianBianHaos = new ArrayList();
            this.clientConfig = ClientConfig.CreateCommon();
            
            if (isOpen)
            {
                this.GetInfoFromDB();
            }
            else
            {
                this.xianShi = true;
                this.地点名称ToolStripMenuItem.Checked = base.Columns["地点名称"].Visible = this.值状态ToolStripMenuItem.Checked = base.Columns["值状态"].Visible = this.断点区域ToolStripMenuItem.Checked = base.Columns["断电区域"].Visible = this.测点编号ToolStripMenuItem.Checked = base.Columns["测点编号"].Visible = this.时间toolStripMenuItem.Checked = base.Columns["时间"].Visible = true;
                this.图标ToolStripMenuItem.Checked = base.Columns["图标"].Visible = this.报警上限ToolStripMenuItem.Checked = base.Columns["报警上限"].Visible = this.断电范围ToolStripMenuItem.Checked = base.Columns["断电范围"].Visible = this.报警下限ToolStripMenuItem.Checked = base.Columns["报警下限"].Visible = this.量程高值ToolStripMenuItem.Checked = base.Columns["量程高值"].Visible = this.量程低值ToolStripMenuItem.Checked = base.Columns["量程低值"].Visible = this.断电值ToolStripMenuItem.Checked = base.Columns["断电值"].Visible = this.复电值ToolStripMenuItem.Checked = base.Columns["复电值"].Visible = this.断电时刻ToolStripMenuItem.Checked = base.Columns["断电时刻"].Visible = this.复电时刻ToolStripMenuItem.Checked = base.Columns["复电时刻"].Visible = this.报警时刻ToolStripMenuItem.Checked = base.Columns["报警时刻"].Visible = this.单位ToolStripMenuItem.Checked = base.Columns["单位"].Visible = this.馈电状态及时刻ToolStripMenuItem.Checked = base.Columns["馈电状态及时刻"].Visible = this.最大值ToolStripMenuItem.Checked = base.Columns["最大值"].Visible = this.最小值ToolStripMenuItem.Checked = base.Columns["最小值"].Visible = this.平均值ToolStripMenuItem.Checked = base.Columns["平均值"].Visible = false;
                this.StoreInfoToDB();
            }
            this.打印ToolStripMenuItem.Click += new EventHandler(this.打印ToolStripMenuItem_Click);
        }

        private void CallCurve(string type, string curveName)
        {
            string ceDianBianHao = base.Rows[this.currentRow].Cells["测点编号"].Value.ToString();
            if (ceDianBianHao.Contains(type))
            {
                if (type == "A")
                {
                    this.mf_g.OpenCurveDisplay(curveName, ceDianBianHao);
                }
                else
                {
                    this.mf_g.OpenSwitchCurve(curveName, ceDianBianHao);
                }
            }
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
        }

        private void contextMenuStrip1_MouseClick(object sender, MouseEventArgs e)
        {
            if ((Users.UserType == UserType.WATCHER) || MainFormRef.isOrdinaryVersion)
            {
                this.删除ToolStripMenuItem.Enabled = false;
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

       public void freshData()
        {
            //bool updateDDGX;
           // IEnumerator enumerator = this.ceDianBianHaos.GetEnumerator();
            try
            {
                for (int i = 0; i < ceDianBianHaos.Count; i++)
                {
                                     
                    string cdbh = (string)ceDianBianHaos[i];

                    CeDian cedian = GlobalParams.AllCeDianList.getCedianInfo(cdbh);

                    if (!(cedian == null))
                    {

                        int lineNum = this.ceDianBianHaos.IndexOf(cdbh);
                        int stno = cedian.FenZhanHao;
                        string staval = null;
                        string color = null;
                        string time = null;
                        string area = null;

                        if (cedian.UpdateDDGX)
                        {
                            area = GlobalParams.AllCeDianList.GetDuanDianQuYu(cdbh);
                        }

                        int allfenZhans = GlobalParams.AllfenZhans[stno].commState;
                        switch (allfenZhans)
                        {
                            case 0:
                                {
                                    int dlx = cedian.DaLeiXing;
                                   
                                    if (cdbh[2] == 'F')
                                    {
                                        dlx = 3;
                                    }
                                    allfenZhans = dlx;
                                    switch (allfenZhans)
                                    {
                                        case 0:
                                            {
                                                int st = cedian.RtState;
                                                //updateDDGX = !cedian.TiaoJiao;
                                                if (cedian.TiaoJiao)
                                                {
                                                  
                                                    if ((cedian.RtVal < 0f))
                                                    {
                                                        cedian.RtVal = 0f;
                                                    }
                                                    st = -1;
                                                }
                                                color = this.get_ana_color(st);
                                               // if ((color == "Black") && (cedian.IsAlarm))
                                                //    color = "red";

                                                float a = cedian.RtVal;
                                                staval = this.get_ana_value(st, a);
                                                break;
                                            }
                                        case 1:
                                            {
                                                // color = this.get_kg_color(cedian.RtState, (int)cedian.RtVal);
                                                color = this.get_kg_color(cedian.IsAlarm, cedian.RtState);
                                                string strval = null;
                                                allfenZhans = (int)cedian.RtVal;
                                                switch (allfenZhans)
                                                {
                                                    case 0:
                                                        {
                                                            strval = cedian.KaiGuanLiang.LingTai;
                                                            break;
                                                        }
                                                    case 1:
                                                        {
                                                            strval = cedian.KaiGuanLiang.YiTai;
                                                            break;
                                                        }
                                                    case 2:
                                                        {
                                                            strval = cedian.KaiGuanLiang.ErTai;
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            strval = "断线";
                                                            break;
                                                        }
                                                }
                                                string val = this.get_kg_value(cedian.RtState);
                                                
                                                if (!(val == null))
                                                {
                                                    strval = val;
                                                }
                                                staval = strval;
                                                break;
                                            }
                                        case 2:
                                            {
                                                //updateDDGX = cedian.RtState != 0;
                                                if (!(cedian.RtState == 0))
                                                {
                                                    color = this.clientConfig.@get("kYiTaiColor");
                                                    staval = cedian.KongZhiLiang.Yitaimingcheng;
                                                }
                                                else
                                                {
                                                    color = this.clientConfig.@get("kLingTaiColor");
                                                    staval = cedian.KongZhiLiang.Litaimingcheng;
                                                }
                                                break;
                                            }
                                        case 3:
                                            {
                                               // updateDDGX = cedian.RtState != 0;
                                                if (cedian.RtState == 0)
                                                {
                                                    color = this.get_kg_color(cedian.IsAlarm, 0);
                                                    staval = "交流";                                                   
                                                }
                                                else
                                                {
                                                    color = this.get_kg_color(cedian.IsAlarm, 1);
                                                    staval = "直流";
                                                }
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    color = this.clientConfig.@get("mCommFailColor");
                                    staval = "超时";
                                    if (GlobalParams.Comm_state == 0)
                                    {
                                        staval = "通讯中断";
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    color = this.clientConfig.@get("mCommFailColor");
                                    staval = "通讯失败";
                                    break;
                                }
                            case 3:
                                {
                                    staval = "初始化";
                                    break;
                                }
                        }

                        DateTime dateTime = cedian.Time;
                        if (dateTime == DateTime.MinValue)
                            time = "";
                        else
                            time = dateTime.ToString("yy/MM/dd HH:mm:ss");
                        GlobalParams.AllCeDianList.allcedianlist[cdbh].UpdateDDGX = false;

                        //object[] pre = new object[4];

                        //  if (!(base.Rows[lineNum].Cells["值状态"].Value == null)  && (base.Rows[lineNum].Cells["时间"].Value == null) )
                        //  {

                        //    pre[0] = base.Rows[lineNum].Cells["值状态"].Value;
                        //    pre[1] = base.Rows[lineNum].DefaultCellStyle.ForeColor;
                        //    pre[2] = base.Rows[lineNum].Cells["时间"].Value;
                        //    pre[3] = base.Rows[lineNum].Cells["断电区域"].Value;

                        //  }

                        bool fresh = false;
                        //if ((pre[0] != null) && (pre[2] != null) && (staval != null) && (time != null))
                        //{
                        //    fresh = ((pre[0].ToString() == staval) && (pre[2].ToString() == time));
                        //}

                        if (!fresh)
                        {
                            object[] objArray = new object[] { lineNum, staval, color, time, area };
                            if (!base.IsDisposed)
                                    base.BeginInvoke(new RefreshDataGridView(this.refreshDataGridView), new object[] { lineNum, staval, color, time, area, cdbh });
                            else
                               Console.WriteLine("invoeke is null");
                        }


                        // base.BeginInvoke(new RefreshDataGridView(this.refreshDataGridView), new object[] { lineNum, staval, color, time, area, cdbh });

                    }
                }

            }
            finally
            {
                //IDisposable disposable = enumerator as IDisposable;
                //updateDDGX = disposable == null;
                //if (!updateDDGX)
                //{
                //    disposable.Dispose();
                //}
            }
        }
        private string get_ana_color(int state)
        {
            string color = "red";
            switch (state)
            {
                case -1:
                    return this.clientConfig.get("mTiaoJiao");

                case 0:
                    return this.clientConfig.get("mZhengChangColor");

                case 1:
                    return this.clientConfig.get("mBaoJingColor");

                case 2:
                case 14:
                    return this.clientConfig.get("mDuanDianColor");

                case 3:
                    return "Red";

                case 4:
                    return this.clientConfig.get("mDuanXianColor");

                case 5:
                    return this.clientConfig.get("mYiChuColor");

                case 6:
                    return this.clientConfig.get("mFuPiaoColor");

                case 7:
                    return this.clientConfig.get("mGuZhangColor");

                case 8:
                    return this.clientConfig.get("mIOCommFailColor");

                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                    return color;
            }
            return color;
        }

        private string get_ana_value(int state, float val)
        {
            bool beep = !bool.Parse(this.clientConfig.get("yuYinBaoJing"));
            string ret = val.ToString("f2");
            switch (state)
            {
                case 1:
                case 2:
                case 14:
                    if (beep)
                    {
                        GlobalParams.isBeep = true;
                    }
                    return ret;

                case 3:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                    return ret;

                case 4:
                    ret = "断线";
                    if (beep)
                    {
                        GlobalParams.isBeep = true;
                    }
                    return ret;

                case 5:
                    ret = "溢出";
                    GlobalParams.isBeep = true;
                    return ret;

                case 6:
                    ret = "负漂";
                    if (beep)
                    {
                        GlobalParams.isBeep = true;
                    }
                    return ret;

                case 7:
                    ret = "故障";
                    if (beep)
                    {
                        GlobalParams.isBeep = true;
                    }
                    return ret;

                case 8:
                    ret = "I/O错误";
                    if (beep)
                    {
                        GlobalParams.isBeep = true;
                    }
                    return ret;
            }
            return ret;
        }

        private string get_kg_color(int state, int val)
        {
            string color = "Black";
            switch (state)
            {
                case 0:
                    switch (val)
                    {
                        case 0:
                            return this.clientConfig.get("kLingTaiColor");

                        case 1:
                            return this.clientConfig.get("kYiTaiColor");

                        case 2:
                            return this.clientConfig.get("kErTaiColor");
                    }
                    return color;

                case 1:
                    return this.clientConfig.get("mBaoJingColor");

                case 2:
                    return this.clientConfig.get("mDuanDianColor");

                case 3:
                    return "Red";

                case 4:
                    return this.clientConfig.get("mDuanXianColor");

                case 5:
                case 6:
                    return color;

                case 7:
                    return this.clientConfig.get("mGuZhangColor");

                case 8:
                    return this.clientConfig.get("mIOCommFailColor");
            }
            return color;
        }
        private string get_kg_color(bool b, int state)
        {
            if (state == 8)
                return this.clientConfig.get("kIOCommFailColor");

            if (b)
                return this.clientConfig.get("kYiTaiColor");
            else
                return this.clientConfig.get("kLingTaiColor");
        }
           
           
        
        private string get_kg_value(int state)
        {
            bool beep = !bool.Parse(this.clientConfig.get("yuYinBaoJing"));
            string ret = null;
            switch (state)
            {
                case 0:
                case 2:
                    return ret;

                case 1:
                case 3:
                    if (beep)
                    {
                        GlobalParams.isBeep = true;
                    }
                    return ret;

                case 4:
                    ret = "断线";
                    if (beep)
                    {
                        GlobalParams.isBeep = true;
                    }
                    return ret;

                case 5:
                case 6:
                    return ret;

                case 7:
                    ret = "故障";
                    if (beep)
                    {
                        GlobalParams.isBeep = true;
                    }
                    return ret;

                case 8:
                    ret = "I/O错误";
                    if (beep)
                    {
                        GlobalParams.isBeep = true;
                    }
                    return ret;
            }
            return ret;
        }

        public void GetInfoFromDB()
        {
            string s = string.Concat(new object[] { "select * from LieBiaoKuang where LieBiaoKuang.yeKuangName='", this.yeKuangMingCheng, "' and LieBiaoKuang.lieBiaoKuangXuHao=", this.lieBiaoKuangBianHao });
            DataTable dt = OperateDBAccess.Select(s);
            for (int count = 0; (dt.Rows.Count == 0) && (count < 5); count++)
            {
                Thread.Sleep(100);
                dt = OperateDBAccess.Select(s);
            }
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("标签页" + this.yeKuangMingCheng + "可能已经被删除，请刷新确认后重试");
            }
            else
            {
                int i;
                string cedianbianhao;
                //base.Columns["测点编号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;             
                //base.Columns["值状态"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
               
                this.xianShi = Convert.ToBoolean(dt.Rows[0]["xianShi"]);
                this.图标ToolStripMenuItem.Checked = Convert.ToBoolean(dt.Rows[0]["zhuangTaiTuBiao"]);
                base.Columns["图标"].Visible = Convert.ToBoolean(dt.Rows[0]["zhuangTaiTuBiao"]);
                this.地点名称ToolStripMenuItem.Checked = base.Columns["地点名称"].Visible = Convert.ToBoolean(dt.Rows[0]["anZhuangMingCheng"]);
                this.值状态ToolStripMenuItem.Checked = base.Columns["值状态"].Visible = Convert.ToBoolean(dt.Rows[0]["zhiZhuangTai"]);
                this.断点区域ToolStripMenuItem.Checked = base.Columns["断电区域"].Visible = Convert.ToBoolean(dt.Rows[0]["duanDianQuYu"]);
                this.测点编号ToolStripMenuItem.Checked = base.Columns["测点编号"].Visible = Convert.ToBoolean(dt.Rows[0]["ceDianBianHao"]);
                this.报警上限ToolStripMenuItem.Checked = base.Columns["报警上限"].Visible = Convert.ToBoolean(dt.Rows[0]["baoJinsShangXian"]);
                this.断电范围ToolStripMenuItem.Checked = base.Columns["断电范围"].Visible = Convert.ToBoolean(dt.Rows[0]["duanDianFanWei"]);
                this.报警下限ToolStripMenuItem.Checked = base.Columns["报警下限"].Visible = Convert.ToBoolean(dt.Rows[0]["baoJingXiaXian"]);
                this.量程高值ToolStripMenuItem.Checked = base.Columns["量程高值"].Visible = Convert.ToBoolean(dt.Rows[0]["liangChengGaoZhi"]);
                this.量程低值ToolStripMenuItem.Checked = base.Columns["量程低值"].Visible = Convert.ToBoolean(dt.Rows[0]["liangChengDiZhi"]);
                this.断电值ToolStripMenuItem.Checked = base.Columns["断电值"].Visible = Convert.ToBoolean(dt.Rows[0]["duanDianZhi"]);
                this.复电值ToolStripMenuItem.Checked = base.Columns["复电值"].Visible = Convert.ToBoolean(dt.Rows[0]["fuDianZhi"]);
                this.断电时刻ToolStripMenuItem.Checked = base.Columns["断电时刻"].Visible = Convert.ToBoolean(dt.Rows[0]["duanDianShiKe"]);
                this.复电时刻ToolStripMenuItem.Checked = base.Columns["复电时刻"].Visible = Convert.ToBoolean(dt.Rows[0]["fuDianShiKe"]);
                this.报警时刻ToolStripMenuItem.Checked = base.Columns["报警时刻"].Visible = Convert.ToBoolean(dt.Rows[0]["baoJingShiKe"]);
                this.单位ToolStripMenuItem.Checked = base.Columns["单位"].Visible = Convert.ToBoolean(dt.Rows[0]["danWei"]);
                this.馈电状态及时刻ToolStripMenuItem.Checked = base.Columns["馈电状态及时刻"].Visible = Convert.ToBoolean(dt.Rows[0]["kuiDianZhuangTai"]);
                this.最大值ToolStripMenuItem.Checked = base.Columns["最大值"].Visible = Convert.ToBoolean(dt.Rows[0]["zuiDaZhi"]);
                this.最小值ToolStripMenuItem.Checked = base.Columns["最小值"].Visible = Convert.ToBoolean(dt.Rows[0]["zuiXiaoZhi"]);
                this.平均值ToolStripMenuItem.Checked = base.Columns["平均值"].Visible = Convert.ToBoolean(dt.Rows[0]["pingJunZhi"]);
                this.时间toolStripMenuItem.Checked = base.Columns["时间"].Visible = Convert.ToBoolean(dt.Rows[0]["tftime"]);
                Color color = ColorTranslator.FromHtml(dt.Rows[0]["backColor"].ToString());
                if (!color.IsEmpty)
                {
                    base.BackgroundColor = color;
                }
                dt = OperateDBAccess.Select(string.Concat(new object[] { "select * from LieBiaoAndCeDian where yeKuangName='", this.yeKuangMingCheng, "' and lieBiaoKuangXuHao=", this.lieBiaoKuangBianHao }));
                this.ceDianBianHaos.Clear();
                base.Rows.Clear();
                for (i = dt.Rows.Count; i > 0; i--)
                {
                    cedianbianhao = dt.Rows[i - 1]["ceDianBianHao"].ToString().Substring(0, 5);
                    if (!GlobalParams.AllCeDianList.allcedianlist.ContainsKey(cedianbianhao))
                    {
                        dt.Rows.Remove(dt.Rows[i - 1]);
                    }
                }
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    cedianbianhao = dt.Rows[i]["ceDianBianHao"].ToString().Substring(0, 5);
                    if (cedianbianhao.Length > 0)
                    {
                        CeDian cedian = GlobalParams.AllCeDianList.getCedianInfo(cedianbianhao); //GlobalParams.AllCeDianList.allcedianlist[cedianbianhao];
                        if (cedian == null)
                            continue;
                        if (cedian.CeDianWeiZhi != null)
                        {
                            int index = base.Rows.Add();
                            this.ceDianBianHaos.Add(cedianbianhao);
                            if (!color.IsEmpty)
                            {
                                DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle
                                {
                                    BackColor = color
                                };
                                base.Rows[index].DefaultCellStyle = dataGridViewCellStyle1;
                            }
                            base.Rows[index].Cells["测点编号"].Value = cedian.CeDianBianHao;
                            base.Rows[index].Cells["地点名称"].Value = cedian.CeDianWeiZhi + "/" + cedian.XiaoleiXing;
                            if ((cedian.DaLeiXing == 0) && (cedian.MoNiLiang != null))
                            {
                                base.Rows[index].Cells["报警上限"].Value = cedian.MoNiLiang.BaoJingZhiShangXian;
                                base.Rows[index].Cells["报警下限"].Value = cedian.MoNiLiang.BaoJingZhiXiaXian;
                                base.Rows[index].Cells["单位"].Value = cedian.MoNiLiang.DanWei;
                                base.Rows[index].Cells["量程高值"].Value = cedian.MoNiLiang.LiangChengGao;
                                base.Rows[index].Cells["量程低值"].Value = cedian.MoNiLiang.LiangChengDi;
                                base.Rows[index].Cells["断电值"].Value = cedian.MoNiLiang.DuanDianZhi;
                                base.Rows[index].Cells["复电值"].Value = cedian.MoNiLiang.FuDianZhi;
                                try
                                {
                                    string image = cedian.MoNiLiang.BaoJingTuBiao;
                                    image = Application.StartupPath + @"\monitor\" + image;
                                    base.Rows[index].Cells["图标"].Value = Image.FromFile(image);
                                }
                                catch (Exception ee)
                                {
                                }
                            }
                            base.Rows[index].Cells["断电区域"].Value = GlobalParams.AllCeDianList.GetDuanDianQuYu(cedianbianhao);
                        }
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            this.contextMenuStrip2 = new ContextMenuStrip(this.components);
            this.列表调整toolStripMenuItem = new ToolStripMenuItem();
            this.列表标头设置ToolStripMenuItem = new ToolStripMenuItem();
            this.图标ToolStripMenuItem = new ToolStripMenuItem();
            this.地点名称ToolStripMenuItem = new ToolStripMenuItem();
            this.值状态ToolStripMenuItem = new ToolStripMenuItem();
            this.断点区域ToolStripMenuItem = new ToolStripMenuItem();
            this.测点编号ToolStripMenuItem = new ToolStripMenuItem();
            this.报警上限ToolStripMenuItem = new ToolStripMenuItem();
            this.报警下限ToolStripMenuItem = new ToolStripMenuItem();
            this.量程高值ToolStripMenuItem = new ToolStripMenuItem();
            this.量程低值ToolStripMenuItem = new ToolStripMenuItem();
            this.断电值ToolStripMenuItem = new ToolStripMenuItem();
            this.复电值ToolStripMenuItem = new ToolStripMenuItem();
            this.断电时刻ToolStripMenuItem = new ToolStripMenuItem();
            this.复电时刻ToolStripMenuItem = new ToolStripMenuItem();
            this.报警时刻ToolStripMenuItem = new ToolStripMenuItem();
            this.单位ToolStripMenuItem = new ToolStripMenuItem();
            this.馈电状态及时刻ToolStripMenuItem = new ToolStripMenuItem();
            this.最大值ToolStripMenuItem = new ToolStripMenuItem();
            this.最小值ToolStripMenuItem = new ToolStripMenuItem();
            this.平均值ToolStripMenuItem = new ToolStripMenuItem();
            this.断电范围ToolStripMenuItem = new ToolStripMenuItem();
            this.Substation_ToolStripMenuItem = new ToolStripMenuItem();
            this.选择显示测点ToolStripMenuItem = new ToolStripMenuItem();
            this.措施ToolStripMenuItem = new ToolStripMenuItem();
            this.打印ToolStripMenuItem = new ToolStripMenuItem();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.修改ToolStripMenuItem = new ToolStripMenuItem();
            this.删除ToolStripMenuItem = new ToolStripMenuItem();
            this.调教toolStripMenuItem = new ToolStripMenuItem();
            this.添加控制通道ToolStripMenuItem = new ToolStripMenuItem();
            this.实时曲线ToolStripMenuItem = new ToolStripMenuItem();
            this.历史数据ToolStripMenuItem = new ToolStripMenuItem();
            this.曲线显示ToolStripMenuItem = new ToolStripMenuItem();
            this.状态图及柱状图ToolStripMenuItem = new ToolStripMenuItem();
            this.图标 = new DataGridViewImageColumn();
            this.地点名称 = new DataGridViewTextBoxColumn();
            this.值状态 = new DataGridViewTextBoxColumn();
            this.断电区域 = new DataGridViewTextBoxColumn();
            this.测点编号 = new DataGridViewTextBoxColumn();
            this.报警上限 = new DataGridViewTextBoxColumn();
            this.断电范围 = new DataGridViewTextBoxColumn();
            this.报警下限 = new DataGridViewTextBoxColumn();
            this.量程高值 = new DataGridViewTextBoxColumn();
            this.量程低值 = new DataGridViewTextBoxColumn();
            this.断电值 = new DataGridViewTextBoxColumn();
            this.复电值 = new DataGridViewTextBoxColumn();
            this.断电时刻 = new DataGridViewTextBoxColumn();
            this.复电时刻 = new DataGridViewTextBoxColumn();
            this.报警时刻 = new DataGridViewTextBoxColumn();
            this.单位 = new DataGridViewTextBoxColumn();
            this.馈电状态及时刻 = new DataGridViewTextBoxColumn();
            this.最大值 = new DataGridViewTextBoxColumn();
            this.最小值 = new DataGridViewTextBoxColumn();
            this.平均值 = new DataGridViewTextBoxColumn();
            this.变值时刻 = new DataGridViewTextBoxColumn();
            this.时间toolStripMenuItem = new ToolStripMenuItem();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((ISupportInitialize)this).BeginInit();
            base.SuspendLayout();
            this.contextMenuStrip2.Items.AddRange(new ToolStripItem[] { this.列表调整toolStripMenuItem, this.列表标头设置ToolStripMenuItem, this.Substation_ToolStripMenuItem, this.选择显示测点ToolStripMenuItem, this.措施ToolStripMenuItem, this.打印ToolStripMenuItem });
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new Size(0xc1, 0x9e);
            this.列表调整toolStripMenuItem.Name = "列表调整toolStripMenuItem";
            this.列表调整toolStripMenuItem.Size = new Size(0xc0, 0x16);
            this.列表调整toolStripMenuItem.Text = "列表调整";
            this.列表调整toolStripMenuItem.Click += new EventHandler(this.列表调整toolStripMenuItem_Click);
            this.列表标头设置ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 
                this.图标ToolStripMenuItem, this.地点名称ToolStripMenuItem, this.值状态ToolStripMenuItem, this.断点区域ToolStripMenuItem, this.测点编号ToolStripMenuItem, this.报警上限ToolStripMenuItem, this.报警下限ToolStripMenuItem, this.量程高值ToolStripMenuItem, this.量程低值ToolStripMenuItem, this.断电值ToolStripMenuItem, this.复电值ToolStripMenuItem, this.断电时刻ToolStripMenuItem, this.复电时刻ToolStripMenuItem, this.报警时刻ToolStripMenuItem, this.单位ToolStripMenuItem, this.馈电状态及时刻ToolStripMenuItem, 
                this.最大值ToolStripMenuItem, this.最小值ToolStripMenuItem, this.平均值ToolStripMenuItem, this.断电范围ToolStripMenuItem, this.时间toolStripMenuItem
             });
            this.列表标头设置ToolStripMenuItem.Name = "列表标头设置ToolStripMenuItem";
            this.列表标头设置ToolStripMenuItem.Size = new Size(0xc0, 0x16);
            this.列表标头设置ToolStripMenuItem.Text = "列表标头设置";
            this.图标ToolStripMenuItem.Checked = true;
            this.图标ToolStripMenuItem.CheckState = CheckState.Checked;
            this.图标ToolStripMenuItem.Name = "图标ToolStripMenuItem";
            this.图标ToolStripMenuItem.Size = new Size(160, 0x16);
            this.图标ToolStripMenuItem.Text = "图标";
            this.图标ToolStripMenuItem.Click += new EventHandler(this.图标ToolStripMenuItem_Click);
            this.地点名称ToolStripMenuItem.Checked = true;
            this.地点名称ToolStripMenuItem.CheckState = CheckState.Checked;
            this.地点名称ToolStripMenuItem.Name = "地点名称ToolStripMenuItem";
            this.地点名称ToolStripMenuItem.Size = new Size(160, 0x16);
            this.地点名称ToolStripMenuItem.Text = "地点/名称";
            this.地点名称ToolStripMenuItem.Click += new EventHandler(this.地点名称ToolStripMenuItem_Click);
            this.值状态ToolStripMenuItem.Checked = true;
            this.值状态ToolStripMenuItem.CheckState = CheckState.Checked;
            this.值状态ToolStripMenuItem.Name = "值状态ToolStripMenuItem";
            this.值状态ToolStripMenuItem.Size = new Size(160, 0x16);
            this.值状态ToolStripMenuItem.Text = "值/状态";
            this.值状态ToolStripMenuItem.Click += new EventHandler(this.值状态ToolStripMenuItem_Click);
            this.断点区域ToolStripMenuItem.Checked = true;
            this.断点区域ToolStripMenuItem.CheckState = CheckState.Checked;
            this.断点区域ToolStripMenuItem.Name = "断点区域ToolStripMenuItem";
            this.断点区域ToolStripMenuItem.Size = new Size(160, 0x16);
            this.断点区域ToolStripMenuItem.Text = "断电区域";
            this.断点区域ToolStripMenuItem.Click += new EventHandler(this.断点区域ToolStripMenuItem_Click);
            this.测点编号ToolStripMenuItem.Checked = true;
            this.测点编号ToolStripMenuItem.CheckState = CheckState.Checked;
            this.测点编号ToolStripMenuItem.Name = "测点编号ToolStripMenuItem";
            this.测点编号ToolStripMenuItem.Size = new Size(160, 0x16);
            this.测点编号ToolStripMenuItem.Text = "测点编号";
            this.测点编号ToolStripMenuItem.Click += new EventHandler(this.测点编号ToolStripMenuItem_Click);
            this.报警上限ToolStripMenuItem.Name = "报警上限ToolStripMenuItem";
            this.报警上限ToolStripMenuItem.Size = new Size(160, 0x16);
            this.报警上限ToolStripMenuItem.Text = "报警上限";
            this.报警上限ToolStripMenuItem.Click += new EventHandler(this.报警上限ToolStripMenuItem_Click);
            this.报警下限ToolStripMenuItem.Name = "报警下限ToolStripMenuItem";
            this.报警下限ToolStripMenuItem.Size = new Size(160, 0x16);
            this.报警下限ToolStripMenuItem.Text = "报警下限";
            this.报警下限ToolStripMenuItem.Click += new EventHandler(this.报警下限ToolStripMenuItem_Click);
            this.量程高值ToolStripMenuItem.Name = "量程高值ToolStripMenuItem";
            this.量程高值ToolStripMenuItem.Size = new Size(160, 0x16);
            this.量程高值ToolStripMenuItem.Text = "量程高值";
            this.量程高值ToolStripMenuItem.Click += new EventHandler(this.量程高值ToolStripMenuItem_Click);
            this.量程低值ToolStripMenuItem.Name = "量程低值ToolStripMenuItem";
            this.量程低值ToolStripMenuItem.Size = new Size(160, 0x16);
            this.量程低值ToolStripMenuItem.Text = "量程低值";
            this.量程低值ToolStripMenuItem.Click += new EventHandler(this.量程低值ToolStripMenuItem_Click);
            this.断电值ToolStripMenuItem.Name = "断电值ToolStripMenuItem";
            this.断电值ToolStripMenuItem.Size = new Size(160, 0x16);
            this.断电值ToolStripMenuItem.Text = "断电值";
            this.断电值ToolStripMenuItem.Click += new EventHandler(this.断电值ToolStripMenuItem_Click);
            this.复电值ToolStripMenuItem.Name = "复电值ToolStripMenuItem";
            this.复电值ToolStripMenuItem.Size = new Size(160, 0x16);
            this.复电值ToolStripMenuItem.Text = "复电值";
            this.复电值ToolStripMenuItem.Click += new EventHandler(this.复电值ToolStripMenuItem_Click);
            this.断电时刻ToolStripMenuItem.Name = "断电时刻ToolStripMenuItem";
            this.断电时刻ToolStripMenuItem.Size = new Size(160, 0x16);
            this.断电时刻ToolStripMenuItem.Text = "断电时刻";
            this.断电时刻ToolStripMenuItem.Click += new EventHandler(this.断电时刻ToolStripMenuItem_Click);
            this.复电时刻ToolStripMenuItem.Name = "复电时刻ToolStripMenuItem";
            this.复电时刻ToolStripMenuItem.Size = new Size(160, 0x16);
            this.复电时刻ToolStripMenuItem.Text = "复电时刻";
            this.复电时刻ToolStripMenuItem.Click += new EventHandler(this.复电时刻ToolStripMenuItem_Click);
            this.报警时刻ToolStripMenuItem.Name = "报警时刻ToolStripMenuItem";
            this.报警时刻ToolStripMenuItem.Size = new Size(160, 0x16);
            this.报警时刻ToolStripMenuItem.Text = "报警时刻";
            this.报警时刻ToolStripMenuItem.Click += new EventHandler(this.报警时刻ToolStripMenuItem_Click);
            this.单位ToolStripMenuItem.Name = "单位ToolStripMenuItem";
            this.单位ToolStripMenuItem.Size = new Size(160, 0x16);
            this.单位ToolStripMenuItem.Text = "单位";
            this.单位ToolStripMenuItem.Click += new EventHandler(this.单位ToolStripMenuItem_Click);
            this.馈电状态及时刻ToolStripMenuItem.Name = "馈电状态及时刻ToolStripMenuItem";
            this.馈电状态及时刻ToolStripMenuItem.Size = new Size(160, 0x16);
            this.馈电状态及时刻ToolStripMenuItem.Text = "馈电状态及时刻";
            this.馈电状态及时刻ToolStripMenuItem.Click += new EventHandler(this.馈电状态及时刻ToolStripMenuItem_Click);
            this.最大值ToolStripMenuItem.Name = "最大值ToolStripMenuItem";
            this.最大值ToolStripMenuItem.Size = new Size(160, 0x16);
            this.最大值ToolStripMenuItem.Text = "最大值";
            this.最大值ToolStripMenuItem.Click += new EventHandler(this.最大值ToolStripMenuItem_Click);
            this.最小值ToolStripMenuItem.Name = "最小值ToolStripMenuItem";
            this.最小值ToolStripMenuItem.Size = new Size(160, 0x16);
            this.最小值ToolStripMenuItem.Text = "最小值";
            this.最小值ToolStripMenuItem.Click += new EventHandler(this.最小值ToolStripMenuItem_Click);
            this.平均值ToolStripMenuItem.Name = "平均值ToolStripMenuItem";
            this.平均值ToolStripMenuItem.Size = new Size(160, 0x16);
            this.平均值ToolStripMenuItem.Text = "平均值";
            this.平均值ToolStripMenuItem.Click += new EventHandler(this.平均值ToolStripMenuItem_Click);
            this.断电范围ToolStripMenuItem.Name = "断电范围ToolStripMenuItem";
            this.断电范围ToolStripMenuItem.Size = new Size(160, 0x16);
            this.断电范围ToolStripMenuItem.Text = "断电范围";
            this.断电范围ToolStripMenuItem.Click += new EventHandler(this.断电范围ToolStripMenuItem_Click);
            this.时间toolStripMenuItem.Name = "时间toolStripMenuItem";
            this.时间toolStripMenuItem.Size = new Size(160, 0x16);
            this.时间toolStripMenuItem.Text = "变值时刻";
            this.时间toolStripMenuItem.Click += new EventHandler(this.时间toolStripMenuItem_Click);
            this.Substation_ToolStripMenuItem.Name = "Substation_ToolStripMenuItem";
            this.Substation_ToolStripMenuItem.Size = new Size(0xc0, 0x16);
            this.Substation_ToolStripMenuItem.Text = "分站信息浏览";
            this.Substation_ToolStripMenuItem.Click += new EventHandler(this.Substation_ToolStripMenuItem_Click);
            this.选择显示测点ToolStripMenuItem.Name = "选择显示测点ToolStripMenuItem";
            this.选择显示测点ToolStripMenuItem.Size = new Size(0xc0, 0x16);
            this.选择显示测点ToolStripMenuItem.Text = "选择显示测点";
            this.选择显示测点ToolStripMenuItem.Click += new EventHandler(this.选择显示测点ToolStripMenuItem_Click);
            this.措施ToolStripMenuItem.Name = "措施ToolStripMenuItem";
            this.措施ToolStripMenuItem.Size = new Size(0xc0, 0x16);
            this.措施ToolStripMenuItem.Text = "措施";
            this.措施ToolStripMenuItem.Click += new EventHandler(this.措施ToolStripMenuItem_Click);
            this.打印ToolStripMenuItem.Name = "打印ToolStripMenuItem";
            this.打印ToolStripMenuItem.Size = new Size(0xc0, 0x16);
            this.打印ToolStripMenuItem.Text = "打印";


            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.修改ToolStripMenuItem, this.删除ToolStripMenuItem, this.调教toolStripMenuItem, this.添加控制通道ToolStripMenuItem, this.实时曲线ToolStripMenuItem, this.历史数据ToolStripMenuItem, this.曲线显示ToolStripMenuItem, this.状态图及柱状图ToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0xa1, 180);
            this.contextMenuStrip1.Click += new EventHandler(this.contextMenuStrip1_Click);
            this.contextMenuStrip1.MouseClick += new MouseEventHandler(this.contextMenuStrip1_MouseClick);
            this.修改ToolStripMenuItem.Name = "修改ToolStripMenuItem";
            this.修改ToolStripMenuItem.Size = new Size(160, 0x16);
            this.修改ToolStripMenuItem.Text = "修改";
            this.修改ToolStripMenuItem.Click += new EventHandler(this.修改ToolStripMenuItem_Click);
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new Size(160, 0x16);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new EventHandler(this.删除ToolStripMenuItem_Click);
            this.调教toolStripMenuItem.Name = "调教toolStripMenuItem";
            this.调教toolStripMenuItem.Size = new Size(160, 0x16);
            this.调教toolStripMenuItem.Text = "调校/取消调教";
            this.调教toolStripMenuItem.Click += new EventHandler(this.调教toolStripMenuItem_Click);
            this.添加控制通道ToolStripMenuItem.Name = "添加控制通道ToolStripMenuItem";
            this.添加控制通道ToolStripMenuItem.Size = new Size(160, 0x16);
            this.添加控制通道ToolStripMenuItem.Text = "添加控制通道";
            this.添加控制通道ToolStripMenuItem.Click += new EventHandler(this.添加控制通道ToolStripMenuItem_Click);
            this.实时曲线ToolStripMenuItem.Name = "实时曲线ToolStripMenuItem";
            this.实时曲线ToolStripMenuItem.Size = new Size(160, 0x16);
            this.实时曲线ToolStripMenuItem.Text = "实时曲线";
            this.实时曲线ToolStripMenuItem.Click += new EventHandler(this.实时曲线ToolStripMenuItem_Click);
            this.历史数据ToolStripMenuItem.Name = "历史数据ToolStripMenuItem";
            this.历史数据ToolStripMenuItem.Size = new Size(160, 0x16);
            this.历史数据ToolStripMenuItem.Text = "历史数据";
            this.历史数据ToolStripMenuItem.Click += new EventHandler(this.历史数据ToolStripMenuItem_Click);
            this.曲线显示ToolStripMenuItem.Name = "曲线显示ToolStripMenuItem";
            this.曲线显示ToolStripMenuItem.Size = new Size(160, 0x16);
            this.曲线显示ToolStripMenuItem.Text = "曲线显示";
            this.曲线显示ToolStripMenuItem.Click += new EventHandler(this.曲线显示ToolStripMenuItem_Click);
            this.状态图及柱状图ToolStripMenuItem.Name = "状态图及柱状图ToolStripMenuItem";
            this.状态图及柱状图ToolStripMenuItem.Size = new Size(160, 0x16);
            this.状态图及柱状图ToolStripMenuItem.Text = "状态图及柱状图";
            this.状态图及柱状图ToolStripMenuItem.Click += new EventHandler(this.状态图及柱状图ToolStripMenuItem_Click);

            this.图标.HeaderText = "图标";
            this.图标.Name = "图标";
            this.图标.ReadOnly = true;
            //this.图标.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            图标.ImageLayout = DataGridViewImageCellLayout.Zoom;
           // this.图标.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;

            this.地点名称.HeaderText = "地点/名称";
            this.地点名称.Name = "地点名称";
            this.地点名称.ReadOnly = true;
            this.地点名称.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.地点名称.MinimumWidth = 85;
            this.值状态.HeaderText = "值状态";
            this.值状态.Name = "值状态";
            this.值状态.ReadOnly = true;
            this.值状态.SortMode = DataGridViewColumnSortMode.NotSortable;
           
           
            this.断电区域.HeaderText = "断电区域";
            this.断电区域.Name = "断电区域";
            this.断电区域.ReadOnly = true;
            this.断电区域.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.测点编号.HeaderText = "测点编号";
            this.测点编号.Name = "测点编号";
            this.测点编号.ReadOnly = true;
            this.测点编号.SortMode = DataGridViewColumnSortMode.NotSortable;
           // this.测点编号.MinimumWidth = 85;
           // this.测点编号.Width = 85;
            this.报警上限.HeaderText = "报警上限";
            this.报警上限.Name = "报警上限";
            this.报警上限.ReadOnly = true;
            this.报警上限.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.断电范围.HeaderText = "断电范围";
            this.断电范围.Name = "断电范围";
            this.断电范围.ReadOnly = true;
            this.断电范围.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.报警下限.HeaderText = "报警下限";
            this.报警下限.Name = "报警下限";
            this.报警下限.ReadOnly = true;
            this.报警下限.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.量程高值.HeaderText = "量程高值";
            this.量程高值.Name = "量程高值";
            this.量程高值.ReadOnly = true;
            this.量程高值.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.量程低值.HeaderText = "量程低值";
            this.量程低值.Name = "量程低值";
            this.量程低值.ReadOnly = true;
            this.量程低值.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.断电值.HeaderText = "断电值";
            this.断电值.Name = "断电值";
            this.断电值.ReadOnly = true;
            this.断电值.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.复电值.HeaderText = "复电值";
            this.复电值.Name = "复电值";
            this.复电值.ReadOnly = true;
            this.复电值.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.断电时刻.HeaderText = "断电时刻";
            this.断电时刻.Name = "断电时刻";
            this.断电时刻.ReadOnly = true;
            this.断电时刻.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.复电时刻.HeaderText = "复电时刻";
            this.复电时刻.Name = "复电时刻";
            this.复电时刻.ReadOnly = true;
            this.复电时刻.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.报警时刻.HeaderText = "报警时刻";
            this.报警时刻.Name = "报警时刻";
            this.报警时刻.ReadOnly = true;
            this.报警时刻.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.单位.HeaderText = "单位";
            this.单位.Name = "单位";
            this.单位.ReadOnly = true;
            this.单位.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.馈电状态及时刻.HeaderText = "馈电状态及时刻";
            this.馈电状态及时刻.Name = "馈电状态及时刻";
            this.馈电状态及时刻.ReadOnly = true;
            this.馈电状态及时刻.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.最大值.HeaderText = "最大值";
            this.最大值.Name = "最大值";
            this.最大值.ReadOnly = true;
            this.最大值.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.最小值.HeaderText = "最小值";
            this.最小值.Name = "最小值";
            this.最小值.ReadOnly = true;
            this.最小值.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.平均值.HeaderText = "平均值";
            this.平均值.Name = "平均值";
            this.平均值.ReadOnly = true;
            this.平均值.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.变值时刻.HeaderText = "变值时刻";
            this.变值时刻.Name = "时间";
            this.变值时刻.ReadOnly = true;
            this.变值时刻.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.时间toolStripMenuItem.Name = "时间toolStripMenuItem";
            this.时间toolStripMenuItem.Size = new Size(160, 0x16);
            this.时间toolStripMenuItem.Text = "变值时刻";
            base.AllowUserToAddRows = false;
            base.AllowUserToDeleteRows = false;
            base.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            base.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.LightGray;
            dataGridViewCellStyle1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            base.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            base.ColumnHeadersHeight = 30;
            base.Columns.AddRange(new DataGridViewColumn[] { 
                this.图标, this.地点名称, this.值状态, this.断电区域, this.测点编号 , this.报警上限, this.断电范围, this.报警下限, this.量程高值, this.量程低值, this.断电值, this.复电值, this.断电时刻, this.复电时刻, this.报警时刻, this.单位, 
                this.馈电状态及时刻, this.最大值, this.最小值, this.平均值, this.变值时刻
             });
            base.EnableHeadersVisualStyles = false;
            base.RowHeadersVisible = false;
            base.RowTemplate.Height = 0x17;
            base.AllowUserToResizeRows = false;
            base.CellDoubleClick += new DataGridViewCellEventHandler(this.LieBiaoKuang_CellDoubleClick);
            base.MouseDown += new MouseEventHandler(this.LieBiaoKuang_MouseDown);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((ISupportInitialize)this).EndInit();

            this.AllowUserToOrderColumns = true;
            base.ResumeLayout(false);
        }

        private void LieBiaoKuang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((Users.UserType != UserType.WATCHER) || !MainFormRef.isOrdinaryVersion)
            {
                int rowIndex = e.RowIndex;
                if (rowIndex > -1)
                {
                    this.currentRow = rowIndex;
                    this.ModifyCeDian();
                }
            }
        }

        private void LieBiaoKuang_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = base.HitTest(e.X, e.Y);
            switch (hti.Type)
            {
                case DataGridViewHitTestType.None:
                    this.ContextMenuStrip = this.contextMenuStrip2;
                    break;

                case DataGridViewHitTestType.Cell:
                    this.currentRow = hti.RowIndex;
                    if ((Users.UserType == UserType.WATCHER) || MainFormRef.isOrdinaryVersion)
                    {
                        this.删除ToolStripMenuItem.Enabled = false;
                        this.添加控制通道ToolStripMenuItem.Enabled = false;
                        this.调教toolStripMenuItem.Enabled = false;
                        this.修改ToolStripMenuItem.Enabled = false;
                    }
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

        private void ModifyCeDian()
        {
            string cedianbianhao = base.Rows[this.currentRow].Cells["测点编号"].Value.ToString();
            if (cedianbianhao[2] != 'F')
            {
                if (CeDian.hasKuiDianOrDianDian(cedianbianhao))
                {
                    MessageBox.Show("该测点存在关联的馈电关系或断电关系，不能被修改");
                }
                else
                {
                    Modify_test mt = new Modify_test(this, cedianbianhao)
                    {
                        CeDianBianHao = cedianbianhao
                    };
                    CeDian cedian = new CeDian(cedianbianhao);
                    mt.AnZhuangDiDian.Items.AddRange(InstallationSite.GetAllLocationAsArray());
                    for (int i = 0; i < mt.AnZhuangDiDian.Items.Count; i++)
                    {
                        if (cedian.CeDianWeiZhi == mt.AnZhuangDiDian.Items[i].ToString())
                        {
                            mt.AnZhuangDiDian.SelectedIndex = i;
                            break;
                        }
                    }
                    mt.CeDianLeiXing.Items.Clear();
                    if (cedian.DaLeiXing == 0)
                        mt.CeDianLeiXing.Items.Add(cedian.XiaoleiXing);
                    else if (cedian.DaLeiXing == 1)
                        mt.CeDianLeiXing.Items.Add(cedian.KaiGuanLiang.MingCheng);
                    else if (cedian.DaLeiXing == 2)
                        mt.CeDianLeiXing.Items.Add(cedian.KongZhiLiang.Mingcheng);

                    mt.CeDianLeiXing.SelectedIndex = 0;
                    mt.CeDianLeiXing.Enabled = false;
                    mt.FenZhanHao.Items.Add(cedian.FenZhanHao);
                    mt.FenZhanHao.SelectedIndex = 0;
                    mt.TongDao.Items.AddRange(TongDao.GetUnUsedTongDao(cedian.FenZhanHao));
                    mt.BaoJing.Checked = cedian.BaoJing;
                    mt.ShowDialog();
                }
            }
        }

      
       

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
            }
            catch
            {
                Invalidate();
            }
        }

        private void refreshDataGridView(int rowno, string stateVal, string color, string time, string cutarea, string cdbh)
        {

            try
            {

                rowno = ceDianBianHaos.IndexOf(cdbh); ;
                if (rowno < 0)
                    return;
                if (rowno > base.Rows.Count)
                    return;


                if (stateVal == null)
                    return;
                if (stateVal.Equals("通讯中断"))
                {
                    base.Rows[rowno].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml(color);
                    base.Rows[rowno].Cells["值状态"].Value = stateVal;
                    return;
                }

              
                  base.Rows[rowno].Cells["时间"].Value = time;
                

                if (color != null)
                {
                    base.Rows[rowno].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml(color);
                }
                if (stateVal != null)
                {
                    base.Rows[rowno].Cells["值状态"].Value = stateVal;
                }
                if (cutarea != null)
                {
                    base.Rows[rowno].Cells["断电区域"].Value = cutarea;
                }
            }
            catch (Exception ee)
            {
               

            }
        }

        private void StoreInfoToDB()
        {
            OperateDBAccess.Execute(string.Concat(new object[] { 
                "insert into LieBiaoKuang(yeKuangName,lieBiaoKuangXuHao,xianShi,zhuangTaiTuBiao,anZhuangMingCheng,zhiZhuangTai,duanDianQuYu,ceDianBianHao,baoJinsShangXian,baoJingXiaXian,liangChengGaoZhi,liangChengDiZhi,duanDianZhi,fuDianZhi,duanDianShiKe,fuDianShiKe,baoJingShiKe,danWei,kuiDianZhuangTai,zuiDaZhi,zuiXiaoZhi,pingJunZhi,duanDianFanWei) values('", this.yeKuangMingCheng, "',", this.lieBiaoKuangBianHao, ",", Convert.ToInt32(this.xianShi), ",", Convert.ToInt32(this.图标ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.地点名称ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.值状态ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.断点区域ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.测点编号ToolStripMenuItem.Checked), 
                ",", Convert.ToInt32(this.报警上限ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.报警下限ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.量程高值ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.量程低值ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.断电值ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.复电值ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.断电时刻ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.复电时刻ToolStripMenuItem.Checked), 
                ",", Convert.ToInt32(this.报警时刻ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.单位ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.馈电状态及时刻ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.最大值ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.最小值ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.平均值ToolStripMenuItem.Checked), ",", Convert.ToInt32(this.断电范围ToolStripMenuItem.Checked), ")"
             }));
            for (int i = 0; i < this.ceDianBianHaos.Count; i++)
            {
                OperateDBAccess.Execute(string.Concat(new object[] { "insert into LieBiaoAndCeDian(yeKuangName,lieBiaoKuangXuHao,ceDianBianHao) values('", this.yeKuangMingCheng, "',", this.lieBiaoKuangBianHao, ",'", this.ceDianBianHaos[i].ToString(), "')" }));
            }
        }

        private void Substation_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Substation().ShowDialog();
        }

        

        private void 报警上限ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.报警上限ToolStripMenuItem.Checked = !this.报警上限ToolStripMenuItem.Checked;
            base.Columns["报警上限"].Visible = this.报警上限ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 报警时刻ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.报警时刻ToolStripMenuItem.Checked = !this.报警时刻ToolStripMenuItem.Checked;
            base.Columns["报警时刻"].Visible = this.报警时刻ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 报警下限ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.报警下限ToolStripMenuItem.Checked = !this.报警下限ToolStripMenuItem.Checked;
            base.Columns["报警下限"].Visible = this.报警下限ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 测点编号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.测点编号ToolStripMenuItem.Checked = !this.测点编号ToolStripMenuItem.Checked;
            base.Columns["测点编号"].Visible = this.测点编号ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 措施ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mf_g.fmeasure == null)
            {
                this.mf_g.fmeasure = new Form_measure();
                this.mf_g.fmeasure.Show(this.mf_g.mainDockPanel, DockState.Document);
            }
            else if (this.mf_g.fmeasure.IsDisposed)
            {
                this.mf_g.fmeasure = new Form_measure();
                this.mf_g.fmeasure.Show(this.mf_g.mainDockPanel, DockState.Document);
            }
            else
            {
                this.mf_g.fmeasure.Activate();
            }
        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (base.Rows.Count != 0)
            {
                new DGVPrinter { Title = this.yeKuangMingCheng, PageNumbers = true, ShowTotalPageNumber = true, PageNumberInHeader = false, PorportionalColumns = true, HeaderCellAlignment = StringAlignment.Near, Footer = this.yeKuangMingCheng, FooterSpacing = 15f, PageSeparator = " / ", PageText = "页" }.PrintPreviewDataGridView(this);
            }
        }

        private void 单位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.单位ToolStripMenuItem.Checked = !this.单位ToolStripMenuItem.Checked;
            base.Columns["单位"].Visible = this.单位ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 地点名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.地点名称ToolStripMenuItem.Checked = !this.地点名称ToolStripMenuItem.Checked;
            base.Columns["地点名称"].Visible = this.地点名称ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 断点区域ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.断点区域ToolStripMenuItem.Checked = !this.断点区域ToolStripMenuItem.Checked;
            base.Columns["断电区域"].Visible = this.断点区域ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 断电范围ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.断电范围ToolStripMenuItem.Checked = !this.断电范围ToolStripMenuItem.Checked;
            base.Columns["断电范围"].Visible = this.断电范围ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 断电时刻ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.断电时刻ToolStripMenuItem.Checked = !this.断电时刻ToolStripMenuItem.Checked;
            base.Columns["断电时刻"].Visible = this.断电时刻ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 断电值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.断电值ToolStripMenuItem.Checked = !this.断电值ToolStripMenuItem.Checked;
            base.Columns["断电值"].Visible = this.断电值ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 复电时刻ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.复电时刻ToolStripMenuItem.Checked = !this.复电时刻ToolStripMenuItem.Checked;
            base.Columns["复电时刻"].Visible = this.复电时刻ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 复电值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.复电值ToolStripMenuItem.Checked = !this.复电值ToolStripMenuItem.Checked;
            base.Columns["复电值"].Visible = this.复电值ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 馈电状态及时刻ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.馈电状态及时刻ToolStripMenuItem.Checked = !this.馈电状态及时刻ToolStripMenuItem.Checked;
            base.Columns["馈电状态及时刻"].Visible = this.馈电状态及时刻ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 历史数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // currentRow = -1;
            string ss = null;
            string args =  base.Rows[this.currentRow].Cells["测点编号"].Value.ToString();

            string workdirectory = Application.StartupPath + @"\Debug\历史数据显示.exe";
            try
            {
                string[] argss = new string[] { args };
                Process.Start(workdirectory, args);
            }
            catch (Exception eo)
            {
                MessageBox.Show("启动应用程序出错！原因" + eo.Message);
            }
        }

        private void 量程低值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.量程低值ToolStripMenuItem.Checked = !this.量程低值ToolStripMenuItem.Checked;
            base.Columns["量程低值"].Visible = this.量程低值ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 量程高值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.量程高值ToolStripMenuItem.Checked = !this.量程高值ToolStripMenuItem.Checked;
            base.Columns["量程高值"].Visible = this.量程高值ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 列表调整toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mf_g.la == null)
            {
                this.mf_g.la = new List_adjust(this.yeKuangMingCheng, this.father);
                this.mf_g.la.Show(this.mf_g.mainDockPanel, DockState.Document);
            }
            else if (this.mf_g.la.IsDisposed)
            {
                this.mf_g.la = new List_adjust(this.yeKuangMingCheng, this.father);
                this.mf_g.la.Show(this.mf_g.mainDockPanel, DockState.Document);
            }
            else
            {
                this.mf_g.la.YeKuangMingCheng = this.yeKuangMingCheng;
                this.mf_g.la.GrandFather = this.father;
                this.mf_g.la.Activate();
            }
        }

        private void 平均值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.平均值ToolStripMenuItem.Checked = !this.平均值ToolStripMenuItem.Checked;
            base.Columns["平均值"].Visible = this.平均值ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 曲线显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CallCurve("A", "调用");
            //this.callCurve = new CallCurve("A", "报警"); ;
            //this.callCurve.Show();
            //this.callCurve
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Users.UserType == UserType.WATCHER) || !MainFormRef.isOrdinaryVersion)
            {
                string cedianbianhao = base.Rows[this.currentRow].Cells["测点编号"].Value.ToString();
                if (CeDian.hasKuiDianOrDianDian(cedianbianhao))
                {
                    MessageBox.Show("该测点存在关联的馈电关系或断电关系，不能被修改");
                }
                else if (MessageBox.Show("你确定要删除该测点吗？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CeDian cedian = GlobalParams.AllCeDianList.allcedianlist[cedianbianhao];
                    string type = string.Empty;
                    if (cedianbianhao[2] == 'A')
                    {
                        type = "模拟量";
                    }
                    else if (cedianbianhao[2] == 'D')
                    {
                        type = "开关量";
                    }
                    else
                    {
                        type = "控制量";
                    }
                    string weizhi = cedian.CeDianWeiZhi;
                    int fenzhan = cedian.FenZhanHao;
                    int tongdao = cedian.TongDaoHao;
                    bool success = false;
                    if (cedianbianhao[2] != 'C')
                    {
                        success = GlobalParams.AllCeDianList.DelCeDian(cedianbianhao);
                    }
                    else
                    {
                        success = GlobalParams.AllCeDianList.DelCeDianKZL(cedianbianhao);
                    }
                    OperateDBAccess.Execute("delete from LieBiaoAndCeDian where ceDianBianHao like '" + cedianbianhao + "%'");
                    if (success)
                    {
                        Logic.Log.WriteLog(LogType.CeDianPeiZhi, string.Concat(new object[] { "删除测点#$", type, "#$", cedianbianhao, "#$", weizhi, "#$", fenzhan, "#$", tongdao }));
                    }

                    GlobalParams.AllCeDianList.FillAllCeDian();
                    MainFormRef.updateMainForm();
                    GlobalParams.setDataState();
                }
            }
        }

        private void 时间toolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.时间toolStripMenuItem.Checked = !this.时间toolStripMenuItem.Checked;
            base.Columns["时间"].Visible = this.时间toolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 实时曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ceDianBianHao = base.Rows[this.currentRow].Cells["测点编号"].Value.ToString();
            if (ceDianBianHao[2] == 'A')
            {
                this.realCurve = new RealTimeCurveForm(ceDianBianHao);
                this.realCurve.Show();
                this.realCurve.Curve.click();
            }
        }

        private void 添加控制通道ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Users.UserType != UserType.WATCHER) || !MainFormRef.isOrdinaryVersion)
            {
                string cedianbianhao = base.Rows[this.currentRow].Cells["测点编号"].Value.ToString();
                if ((cedianbianhao[2] != 'F') && (cedianbianhao[2] != 'C'))
                {
                    if (this.mf_g.frm_ctlgx == null)
                    {
                        this.mf_g.frm_ctlgx = new Form_control();
                        this.mf_g.frm_ctlgx.Show();
                    }
                    else if (this.mf_g.frm_ctlgx.IsDisposed)
                    {
                        this.mf_g.frm_ctlgx = new Form_control();
                        this.mf_g.frm_ctlgx.Show();
                    }
                    else
                    {
                        this.mf_g.frm_ctlgx.Activate();
                    }
                    this.mf_g.frm_ctlgx.Tree.SelectedNode = this.mf_g.frm_ctlgx.Tree.Nodes.Find("断电控制", true)[0];
                    this.mf_g.frm_ctlgx.Cc.AddCeDian(base.Rows[this.currentRow].Cells["测点编号"].Value.ToString());
                }
            }
        }

        private void 调教toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Users.UserType == UserType.WATCHER) || !MainFormRef.isOrdinaryVersion)
            {
                string ceDianBianHao = base.Rows[this.currentRow].Cells["测点编号"].Value.ToString();
                if (ceDianBianHao[2] == 'A')
                {
                    DateTime now;
                    CeDian cedian = GlobalParams.AllCeDianList.allcedianlist[ceDianBianHao];
                    if (cedian.TiaoJiao)
                    {
                        now = DateTime.Now;
                        TiaoJiao.DeleteTiaoJiao(cedian.FenZhanHao, cedian.TongDaoHao, now);
                        cedian.TiaoJiao = false;
                        TiaoJiao.uploadHistoryDataState(cedian, now);
                        Logic.Log.WriteLog(LogType.TiaoJiao, "测点" + ceDianBianHao + "#$" + cedian.CeDianWeiZhi + "#$" + cedian.XiaoleiXing + "#$停止调教");
                        MessageBox.Show("您取消了测点" + ceDianBianHao + "的调校！");
                    }
                    else
                    {
                        now = DateTime.Now;
                        DateTime? temp = null;
                        new TiaoJiao(ceDianBianHao, cedian.FenZhanHao, cedian.TongDaoHao, now, temp).InsertIntoDB();
                        cedian.TiaoJiao = true;
                        Logic.Log.WriteLog(LogType.TiaoJiao, "测点" + ceDianBianHao + "#$" + cedian.CeDianWeiZhi + "#$" + cedian.XiaoleiXing + "#$启动调教");
                        MessageBox.Show("您启动了测点" + ceDianBianHao + "的调校！");
                    }
                }
            }
        }

        private void 图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.图标ToolStripMenuItem.Checked = !this.图标ToolStripMenuItem.Checked;
            base.Columns["图标"].Visible = this.图标ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!((Users.UserType != UserType.WATCHER) && MainFormRef.isOrdinaryVersion))
            {
                this.ModifyCeDian();
            }
        }

        private void 选择显示测点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mf_g.st == null)
            {
                this.mf_g.st = new Select_test(this.yeKuangMingCheng, this.lieBiaoKuangBianHao, this.father, this.mf_g);
                this.mf_g.st.Show(this.mf_g.mainDockPanel, DockState.Document);
            }
            else if (this.mf_g.st.IsDisposed)
            {
                this.mf_g.st = new Select_test(this.yeKuangMingCheng, this.lieBiaoKuangBianHao, this.father, this.mf_g);
                this.mf_g.st.Show(this.mf_g.mainDockPanel, DockState.Document);
            }
            else
            {
                this.mf_g.st.YeKuangMingCheng = this.yeKuangMingCheng;
                this.mf_g.st.LieBiaoKuangBianHao = this.lieBiaoKuangBianHao;
                this.mf_g.st.Father = this.father;
                this.mf_g.st.Activate();
            }
        }

        private void 值状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.值状态ToolStripMenuItem.Checked = !this.值状态ToolStripMenuItem.Checked;
            base.Columns["值状态"].Visible = this.值状态ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 状态图及柱状图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CallCurve("D", "开关量状态图");
        }

        private void 最大值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.最大值ToolStripMenuItem.Checked = !this.最大值ToolStripMenuItem.Checked;
            base.Columns["最大值"].Visible = this.最大值ToolStripMenuItem.Checked;
            this.Refresh();
        }

        private void 最小值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.最小值ToolStripMenuItem.Checked = !this.最小值ToolStripMenuItem.Checked;
            base.Columns["最小值"].Visible = this.最小值ToolStripMenuItem.Checked;
            this.Refresh();
        }
    }
      
}

