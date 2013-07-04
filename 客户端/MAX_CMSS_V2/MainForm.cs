namespace MAX_CMSS_V2
{
    using Logic;
    using MAX_CMSS_V2.Curve;
    using MAX_CMSS_V2.voice;
    using Sunisoft.IrisSkin;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Timers;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;

    public class MainForm : Form
    {
        public List_Alarm_Frame alarm_frame;

        public Thread alarmThread;
        public Thread sendThread;

        private IContainer components = null;
        private ClientConfig config;
        public Thread DispatchThread;
        public Thread DispatchThread2;
        private DateTime dt = DateTime.Now;
        private Form_commu fcm;
        private string filename = null;
        public Form_measure fmeasure;
        private Form_qurey form_qry_log;
        private Form_PreAlarm frm_preAlarm;
        private Form_parameter_config fpc;
        private Form_page_edit fpe;
        private List_All_Alarm frame_all_alalrm = new List_All_Alarm();
        public Form_control frm_ctlgx;
        private Form_curve frm_curve;
        private Form_list frm_fzsblist;
        private Form_mimic frm_monitu;
        private Form_substation fs = new Form_substation();
        private Form_switch fsw;
        private Form_system fsy;
        public FenZhanRTdata[] historys;
     
        public SystemInit init;
        public Thread InitThread;
        private bool IsStartFresh = false;
        public List_adjust la;
        private Label label1;
        private Label label2;
        private Label label3;
        public List_alarm_dual combinedAlarmFrm;
        public int list_view_id = 0;
        public DockPanel mainDockPanel;
        private CustomMenuStrip menuStrip1;
        private Panel panel1;
        public Thread serverThread;
        public SkinEngine skinEngine1;
        public Select_test st;
        private StreamWriter sw = null;
        private System.Timers.Timer sysTimer;
        private bool testDebug = false;
        private System.Timers.Timer tiaoJiaoTimer;

        private int timer_40s = 0;
        private int timer_10m = 0;
        private int timer_5s = 0;
        private int timer_1s = 0;

        private System.Windows.Forms.Timer timer1;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton tsbSwitchUser;
        private ToolStripButton tsbtnAbout;
        private ToolStripButton tsbtnAlarm;
        private ToolStripButton tsbtnAnalog;
        private ToolStripButton tsbtnControl;
        private ToolStripButton tsbtnExit;
        private ToolStripButton tsbtnHelp;
        private ToolStripButton tsbtnList;
        private ToolStripButton tsbtnNew;
        private ToolStripButton tsbtnPreAlarm;
        private ToolStripButton tsbtnSubstation;
        private ToolStripButton tsbtnSwitch;
        private ToolStripButton tsbtnTest;
        public voiceAlarmPro voice_alalrm_pro;
        public List<List_show> yeKuangs;
        private ToolStripMenuItem 帮助ToolStripMenuItem;
        private ToolStripMenuItem 参数设置ToolStripMenuItem;
        private ToolStripMenuItem 查询ToolStripMenuItem;
        private ToolStripMenuItem 打印ToolStripMenuItem;
        private ToolStripMenuItem 控制ToolStripMenuItem;
        private ToolStripMenuItem 列表显示ToolStripMenuItem;
        private ToolStripMenuItem 模拟图显示ToolStripMenuItem;
        private ToolStripMenuItem 曲线显示ToolStripMenuItem;
        private ToolStripMenuItem 日志查询ToolStripMenuItem;
        private ToolStripMenuItem 通讯管理ToolStripMenuItem;
        private ToolStripMenuItem 系统管理ToolStripMenuItem;
        private ToolStripMenuItem 页面编辑ToolStripMenuItem;
        private ToolStripMenuItem 状态图与柱状图显示ToolStripMenuItem;

        public MainForm()
        {


          // int[] scores = new int[] { 97, 92, 81, 60 };

            // Define the query expression.
            //IEnumerable<int> scoreQuery =
            //    from score in scores
            //    where score > 100
            //    select score;

            //// Execute the query.
            //foreach (int i in scoreQuery)
            //{
            //    Console.Write(i + " ");
            //}        

            this.InitializeComponent();
            this.historys = new FenZhanRTdata[0x3d];
            this.frm_preAlarm = new Form_PreAlarm(false);
            this.config = ClientConfig.CreateCommon();

            this.skinEngine1.SkinFile = this.config.get("skin");
            this.yeKuangs = new List<List_show>();

            this.frame_all_alalrm.FormBorderStyle = FormBorderStyle.None;
            this.frame_all_alalrm.Show(this.mainDockPanel, DockState.DockBottom);

            this.mainDockPanel.AllowDrop = false;
            this.tiaoJiaoTimer = new System.Timers.Timer();
          
            FenZhanRTdata[] pre = new FenZhanRTdata[0x3d];

            int[] lost_kj45 = new int[0];

            for (int i = 0; i < 0x3d; i++)
            {
                pre[i] = new FenZhanRTdata();
            }
            GlobalParams.AllfenZhans = new FenZhan[61];
            for (byte i = 1; i < 0x3d; i = (byte) (i + 1))
            {
                GlobalParams.AllfenZhans[i] = new FenZhan(i);
                int a = GlobalParams.AllfenZhans[i].GetChuanKouHaoByFenZhanHao(i);
               //int a = 0;
                GlobalParams.AllfenZhans[i].CommPort = a;
                if ((a > 0) && (a < 5))
                {
                    GlobalParams.AllfenZhans[i].commState = 0;
                    if (this.testDebug)
                    {
                        GlobalParams.AllfenZhans[i].commState = 1;
                    }
                }
                else if (a != 0)
                {
                    GlobalParams.AllfenZhans[i].commState = 4;
                }
            }
            for (int j = 0; j < 60; j++)
            {
                GlobalParams.IsModfyControl[j] = false;
            }
            MainFormRef.mainForm = this;
        }

        private void alarm_thread()
        {
            object[] warn_voice = new object[0];
            object[] cut_voice = new object[0];
            object[] feed_voice = new object[0];
            while (true)
            {
                Thread.Sleep(1000);
                try
                {
                    if (GlobalParams.warnList.Count > -1)
                    {
                        if (GlobalParams.warnList.Count > 30)
                        {
                            GlobalParams.warnList.Clear();
                            warn_voice = new object[0];
                        }
                        else
                        {
                            warn_voice = (object[]) GlobalParams.warnList.ToArray().Clone();
                        }
                    }
                    if (GlobalParams.cutList.Count > -1)
                    {
                        if (GlobalParams.cutList.Count > 20)
                        {
                            GlobalParams.cutList.Clear();
                            cut_voice = new object[0];
                        }
                        else
                        {
                            cut_voice = (object[]) GlobalParams.cutList.ToArray().Clone();
                        }
                    }
                    if (GlobalParams.replayList.Count > -1)
                    {
                        if (GlobalParams.replayList.Count > 10)
                        {
                            GlobalParams.replayList.Clear();
                            feed_voice = new object[0];
                        }
                        else
                        {
                            feed_voice = (object[]) GlobalParams.replayList.ToArray().Clone();
                        }
                    }
                }
                catch
                {
                }
                if (Convert.ToBoolean(this.config.get("yuYinBaoJing")))
                {
                    try
                    {
                        int i;
                        for (i = 0; i < cut_voice.Length; i++)
                        {
                            voiceAlarmClass.SaySome(cut_voice.GetValue(i).ToString().Trim());
                        }
                        for (i = 0; i < feed_voice.Length; i++)
                        {
                            voiceAlarmClass.SaySome(feed_voice.GetValue(i).ToString().Trim());
                        }
                        for (i = 0; i < warn_voice.Length; i++)
                        {
                            voiceAlarmClass.SaySome(warn_voice.GetValue(i).ToString().Trim());
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void CommWithServer()
        {
            int listenPort = 17429;

            UdpClient listener = new UdpClient(listenPort) {
                Client = { ReceiveTimeout = 50000 }
            };

            listener.Client.ReceiveBufferSize = 1024 * 2048;
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            byte con = 1;

            int recv_count = 1;

            while (true)
            {
                try
                {
                    byte[] data = listener.Receive(ref groupEP);
                 
                    int[] cmdcount = this.getBufferCmd(ref data);
                   
                   
                    for (int ii = 0; ii < (cmdcount.Length / 2); ii++)
                    {
                        DateTime dt = DateTime.Now;
                      //  Console.WriteLine(dt.ToLongTimeString() +" count  "+ (recv_count++));

                        int start = cmdcount[2 * ii];                        
                        int end = cmdcount[(2 * ii) + 1];
                        if ((data[start] == 0x7e) && (data[end] == 0x21))
                        {
                            byte[] buf = new byte[(end - start) + 1];
                            Array.Copy(data, start, buf, 0, buf.Length);
                            if ((buf.Length > 0) && GlobalParams.RecvRecord)
                            {
                                byte[] b = new byte[buf.Length];
                                Array.Copy(buf, b, buf.Length);
                                this.writebuff(b);
                            }
                            GlobalParams.Comm_state = 1;
                            int fno = buf[1];                         
                            GlobalParams.AllfenZhans[fno].Recv_state = 1;
                            GlobalParams.fzRtDt.CalculateData(buf);
                            if (GlobalParams.fzRtDt.fenZhanHao <= 60)
                            {
                                this.historys[GlobalParams.fzRtDt.fenZhanHao] = GlobalParams.fzRtDt;
                            }
                            if (!((this.fcm == null) || this.fcm.IsDisposed))
                            {
                                this.fcm.Dispatch(GlobalParams.fzRtDt);
                            }
                            if (con == 0)
                            {
                                foreach (List_show ls in this.yeKuangs)
                                {
                                    foreach (LieBiaoKuang lbk in ls.LieBiaoKuangs)
                                    {
                                        lbk.GetInfoFromDB();
                                    }
                                }
                                con = 1;
                            }
                            if ((buf[2] == 0x44) || (buf[2] == 0x45))
                            {
                                if (buf[2] == 0x44)
                                {
                                    //  DispatchAlarmAndPreAlarm(GlobalParams.fzRtDt.fenZhanHao);

                                    if (!((this.frm_monitu == null) || this.frm_monitu.IsDisposed))
                                    {
                                        this.frm_monitu.Dispatch(GlobalParams.fzRtDt);
                                    }
                                    if (!((this.frm_curve == null) || this.frm_curve.IsDisposed))
                                    {
                                        this.frm_curve.Dispatch(GlobalParams.fzRtDt);
                                    }
                                    if (!((this.frm_fzsblist == null) || this.frm_fzsblist.IsDisposed))
                                    {
                                        this.frm_fzsblist.Dispatch(GlobalParams.fzRtDt, GlobalParams.AllCeDianList.allcedianlist);
                                    }
                                }
                                else
                                {


                                }
                                if (GlobalParams.First_Show[GlobalParams.fzRtDt.fenZhanHao - 1])
                                {
                                    GlobalParams.First_Show[GlobalParams.fzRtDt.fenZhanHao - 1] = false;
                                }
                            }
                            else if (((buf[2] == 0x4b) && (this.frm_ctlgx != null)) && !this.frm_ctlgx.IsDisposed)
                            {
                                if (!((this.frm_ctlgx.op == null) || this.frm_ctlgx.op.IsDisposed))
                                {
                                    this.frm_ctlgx.op.Dispatch(GlobalParams.fzRtDt);
                                }
                            }
                            else if (((buf[2] != 80) && (buf[2] == 0x43)) && (this.init != null))
                            {
                                if (buf[3] == 1)
                                {
                                    this.init.showMessage("分站" + buf[1] + "初始化成功！");
                                }
                                else
                                {
                                    this.init.showMessage("分站" + buf[1] + "初始化失败！");
                                }
                            }
                        }
                    }
                }
                catch (SocketException e)
                {

                    GlobalParams.Comm_state = 0;
                    con = 0;
                    for (int i = 1; i < GlobalParams.AllfenZhans.Length; i++)
                    {
                        int fno = GlobalParams.AllfenZhans[i].FenZhanNum;
                        GlobalParams.AllfenZhans[i].setCommInter(fno);
                        GlobalParams.AllfenZhans[i].commState = 1;
                    }
                    //foreach (List_show ls in this.yeKuangs)
                    //{
                    //    if (this.list_view_id == ls.id)
                    //    {
                    //        foreach (LieBiaoKuang lbk in ls.LieBiaoKuangs)
                    //        {
                    //            if (lbk != null)
                    //            {
                    //                lbk.update_comm_interupt();
                    //            }
                    //        }
                    //    }
                    //}
                    if (!((this.frm_monitu == null) || this.frm_monitu.IsDisposed))
                    {
                        this.frm_monitu.Dispatch(null);
                    }
                    //if (Common.DEBUG == 1)
                    //{
                    //    Console.WriteLine(e.ToString());
                    //}
                }
                catch (Exception)
                {
                }
            }
        }

        public void configFig(byte chuankouhao)
        {
            byte[] fenzhanhao_arr = FenZhan.GetFenZhanHaoByChuanKouHao(chuankouhao);
            foreach (byte fenzhanhao in fenzhanhao_arr)
            {
                UDPComm.Send(FenZhan.GetFenZhanConfigInfo(fenzhanhao));
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

        private void fresh_LieBiaoKuang()
        {
            if (!this.IsStartFresh)
            {
                if (this.yeKuangs.Count > 0)
                {
                    this.yeKuangs[0].Activate();
                }
                this.IsStartFresh = !this.IsStartFresh;
            }
            foreach (List_show ls in this.yeKuangs)
            {
                if (this.list_view_id == ls.id)
                {
                    foreach (LieBiaoKuang lbk in ls.LieBiaoKuangs)
                    {
                        if (lbk != null)
                        {
                            lbk.freshData();
                        }
                    }
                }
            }
        }

        private int[] getBufferCmd(ref byte[] buff)
        {
            int i;
            int[] a = new int[20];
            int pos = 0;
            if (buff.Length < 5)
                return a;
            for (i = 0; i < buff.Length; i++)
            {
                if (buff[i] == 0x7e)
                {
                    a[pos++] = i;
                    byte cmd = buff[i + 2];
                    switch (cmd)
                    {
                        case 0x44:
                            if ((buff.Length - i) >= 43)
                            {
                                if (buff[i + 43] == (byte)0x21)
                                {
                                    a[pos++] = i + 43;
                                    i = i + 43;
                                }
                            }
                            break;
                        case 0x47:
                            if ((buff.Length - i) >= 21)
                            {
                                if (buff[i + 21] == (byte)0x21)
                                {
                                    a[pos++] = i + 21;
                                    i = i + 21;
                                }
                            }
                            break;
                        default:
                            if ((buff.Length - i) >= 4)
                            {
                                if (buff[i + 4] == (byte)0x21)
                                {
                                    a[pos++] = i + 4;
                                    i = i + 4;
                                }
                            }
                            break;
                    }

                    //for (int j = i + 1; j < buff.Length; j++)
                    //{
                    //    if (buff[j] == 0x21)
                    //    {
                    //        a[pos++] = j;
                    //        break;
                    //    }
                    //}
                }
            }
            int[] b = new int[pos];
            for (i = 0; i < pos; i++)
            {
                b[i] = a[i];
            }
            return b;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new MAX_CMSS_V2.CustomMenuStrip();
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.页面编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.控制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.列表显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.曲线显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.状态图与柱状图显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模拟图显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.日志查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通讯管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnNew = new System.Windows.Forms.ToolStripButton();
            this.tsbtnList = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAlarm = new System.Windows.Forms.ToolStripButton();
            this.tsbtnPreAlarm = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSubstation = new System.Windows.Forms.ToolStripButton();
            this.tsbtnTest = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAnalog = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSwitch = new System.Windows.Forms.ToolStripButton();
            this.tsbtnControl = new System.Windows.Forms.ToolStripButton();
            this.tsbtnHelp = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAbout = new System.Windows.Forms.ToolStripButton();
            this.tsbSwitchUser = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExit = new System.Windows.Forms.ToolStripButton();
            this.mainDockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(764, 71);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(664, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(730, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(506, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.DodgerBlue;
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.参数设置ToolStripMenuItem,
            this.页面编辑ToolStripMenuItem,
            this.控制ToolStripMenuItem,
            this.列表显示ToolStripMenuItem,
            this.曲线显示ToolStripMenuItem,
            this.状态图与柱状图显示ToolStripMenuItem,
            this.模拟图显示ToolStripMenuItem,
            this.打印ToolStripMenuItem,
            this.查询ToolStripMenuItem,
            this.日志查询ToolStripMenuItem,
            this.通讯管理ToolStripMenuItem,
            this.系统管理ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 71);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(1);
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(764, 27);
            this.menuStrip1.Stretch = false;
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ThemeColor = System.Drawing.Color.Gray;
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(81, 25);
            this.参数设置ToolStripMenuItem.Text = "参数设置";
            this.参数设置ToolStripMenuItem.Click += new System.EventHandler(this.参数设置ToolStripMenuItem_Click);
            // 
            // 页面编辑ToolStripMenuItem
            // 
            this.页面编辑ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.页面编辑ToolStripMenuItem.Name = "页面编辑ToolStripMenuItem";
            this.页面编辑ToolStripMenuItem.Size = new System.Drawing.Size(81, 25);
            this.页面编辑ToolStripMenuItem.Text = "页面编辑";
            this.页面编辑ToolStripMenuItem.Click += new System.EventHandler(this.页面编辑ToolStripMenuItem_Click);
            // 
            // 控制ToolStripMenuItem
            // 
            this.控制ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.控制ToolStripMenuItem.Name = "控制ToolStripMenuItem";
            this.控制ToolStripMenuItem.Size = new System.Drawing.Size(51, 25);
            this.控制ToolStripMenuItem.Text = "控制";
            this.控制ToolStripMenuItem.Click += new System.EventHandler(this.控制ToolStripMenuItem_Click);
            // 
            // 列表显示ToolStripMenuItem
            // 
            this.列表显示ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.列表显示ToolStripMenuItem.Name = "列表显示ToolStripMenuItem";
            this.列表显示ToolStripMenuItem.Size = new System.Drawing.Size(81, 25);
            this.列表显示ToolStripMenuItem.Text = "列表显示";
            this.列表显示ToolStripMenuItem.Click += new System.EventHandler(this.列表显示ToolStripMenuItem_Click);
            // 
            // 曲线显示ToolStripMenuItem
            // 
            this.曲线显示ToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.曲线显示ToolStripMenuItem.Name = "曲线显示ToolStripMenuItem";
            this.曲线显示ToolStripMenuItem.Size = new System.Drawing.Size(81, 25);
            this.曲线显示ToolStripMenuItem.Text = "曲线显示";
            this.曲线显示ToolStripMenuItem.Click += new System.EventHandler(this.曲线显示ToolStripMenuItem_Click);
            // 
            // 状态图与柱状图显示ToolStripMenuItem
            // 
            this.状态图与柱状图显示ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.状态图与柱状图显示ToolStripMenuItem.Name = "状态图与柱状图显示ToolStripMenuItem";
            this.状态图与柱状图显示ToolStripMenuItem.Size = new System.Drawing.Size(156, 25);
            this.状态图与柱状图显示ToolStripMenuItem.Text = "状态图与柱状图显示";
            this.状态图与柱状图显示ToolStripMenuItem.Click += new System.EventHandler(this.状态图与柱状图显示ToolStripMenuItem_Click);
            // 
            // 模拟图显示ToolStripMenuItem
            // 
            this.模拟图显示ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.模拟图显示ToolStripMenuItem.Name = "模拟图显示ToolStripMenuItem";
            this.模拟图显示ToolStripMenuItem.Size = new System.Drawing.Size(96, 25);
            this.模拟图显示ToolStripMenuItem.Text = "模拟图显示";
            this.模拟图显示ToolStripMenuItem.Click += new System.EventHandler(this.模拟图显示ToolStripMenuItem_Click);
            // 
            // 打印ToolStripMenuItem
            // 
            this.打印ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.打印ToolStripMenuItem.Name = "打印ToolStripMenuItem";
            this.打印ToolStripMenuItem.Size = new System.Drawing.Size(51, 25);
            this.打印ToolStripMenuItem.Text = "打印";
            this.打印ToolStripMenuItem.Click += new System.EventHandler(this.打印ToolStripMenuItem_Click);
            // 
            // 查询ToolStripMenuItem
            // 
            this.查询ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.查询ToolStripMenuItem.Name = "查询ToolStripMenuItem";
            this.查询ToolStripMenuItem.Size = new System.Drawing.Size(51, 25);
            this.查询ToolStripMenuItem.Text = "查询";
            this.查询ToolStripMenuItem.Click += new System.EventHandler(this.查询ToolStripMenuItem_Click);
            // 
            // 日志查询ToolStripMenuItem
            // 
            this.日志查询ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.日志查询ToolStripMenuItem.Name = "日志查询ToolStripMenuItem";
            this.日志查询ToolStripMenuItem.Size = new System.Drawing.Size(81, 25);
            this.日志查询ToolStripMenuItem.Text = "日志管理";
            this.日志查询ToolStripMenuItem.Click += new System.EventHandler(this.日志查询ToolStripMenuItem_Click);
            // 
            // 通讯管理ToolStripMenuItem
            // 
            this.通讯管理ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.通讯管理ToolStripMenuItem.Name = "通讯管理ToolStripMenuItem";
            this.通讯管理ToolStripMenuItem.Size = new System.Drawing.Size(81, 25);
            this.通讯管理ToolStripMenuItem.Text = "通讯管理";
            this.通讯管理ToolStripMenuItem.Click += new System.EventHandler(this.通讯管理ToolStripMenuItem_Click);
            // 
            // 系统管理ToolStripMenuItem
            // 
            this.系统管理ToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.系统管理ToolStripMenuItem.Name = "系统管理ToolStripMenuItem";
            this.系统管理ToolStripMenuItem.Size = new System.Drawing.Size(81, 25);
            this.系统管理ToolStripMenuItem.Text = "系统管理";
            this.系统管理ToolStripMenuItem.Click += new System.EventHandler(this.系统管理ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(51, 25);
            this.帮助ToolStripMenuItem.Text = "帮助";
            this.帮助ToolStripMenuItem.Click += new System.EventHandler(this.帮助ToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnNew,
            this.tsbtnList,
            this.tsbtnAlarm,
            this.tsbtnPreAlarm,
            this.tsbtnSubstation,
            this.tsbtnTest,
            this.toolStripButton1,
            this.tsbtnAnalog,
            this.tsbtnSwitch,
            this.tsbtnControl,
            this.tsbtnHelp,
            this.tsbtnAbout,
            this.tsbSwitchUser,
            this.tsbtnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 98);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(764, 32);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // tsbtnNew
            // 
            this.tsbtnNew.AutoSize = false;
            this.tsbtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNew.Image")));
            this.tsbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnNew.Name = "tsbtnNew";
            this.tsbtnNew.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tsbtnNew.Size = new System.Drawing.Size(50, 57);
            this.tsbtnNew.Text = "新建标签页";
            this.tsbtnNew.Click += new System.EventHandler(this.tsbtnNew_Click);
            // 
            // tsbtnList
            // 
            this.tsbtnList.AutoSize = false;
            this.tsbtnList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnList.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnList.Image")));
            this.tsbtnList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnList.Name = "tsbtnList";
            this.tsbtnList.Size = new System.Drawing.Size(50, 57);
            this.tsbtnList.Text = "列表显示设置";
            this.tsbtnList.Click += new System.EventHandler(this.tsbtnList_Click);
            // 
            // tsbtnAlarm
            // 
            this.tsbtnAlarm.AutoSize = false;
            this.tsbtnAlarm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAlarm.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAlarm.Image")));
            this.tsbtnAlarm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAlarm.Name = "tsbtnAlarm";
            this.tsbtnAlarm.Size = new System.Drawing.Size(50, 57);
            this.tsbtnAlarm.Text = "启动语音报警";
            this.tsbtnAlarm.Click += new System.EventHandler(this.tsbtnAlarm_Click);
            // 
            // tsbtnPreAlarm
            // 
            this.tsbtnPreAlarm.AutoSize = false;
            this.tsbtnPreAlarm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPreAlarm.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPreAlarm.Image")));
            this.tsbtnPreAlarm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPreAlarm.Name = "tsbtnPreAlarm";
            this.tsbtnPreAlarm.Size = new System.Drawing.Size(50, 57);
            this.tsbtnPreAlarm.Text = "预警窗口";
            this.tsbtnPreAlarm.Click += new System.EventHandler(this.tsbtnPreAlarm_Click);
            // 
            // tsbtnSubstation
            // 
            this.tsbtnSubstation.AutoSize = false;
            this.tsbtnSubstation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSubstation.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSubstation.Image")));
            this.tsbtnSubstation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSubstation.Name = "tsbtnSubstation";
            this.tsbtnSubstation.Size = new System.Drawing.Size(50, 57);
            this.tsbtnSubstation.Text = "分站状态";
            this.tsbtnSubstation.Click += new System.EventHandler(this.tsbtnSubstation_Click);
            // 
            // tsbtnTest
            // 
            this.tsbtnTest.AutoSize = false;
            this.tsbtnTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnTest.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnTest.Image")));
            this.tsbtnTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTest.Name = "tsbtnTest";
            this.tsbtnTest.Size = new System.Drawing.Size(50, 57);
            this.tsbtnTest.Text = "测点";
            this.tsbtnTest.Click += new System.EventHandler(this.tsbtnTest_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 29);
            this.toolStripButton1.Text = "报警使能";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbtnAnalog
            // 
            this.tsbtnAnalog.AutoSize = false;
            this.tsbtnAnalog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAnalog.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAnalog.Image")));
            this.tsbtnAnalog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAnalog.Name = "tsbtnAnalog";
            this.tsbtnAnalog.Size = new System.Drawing.Size(50, 57);
            this.tsbtnAnalog.Text = "模拟量";
            this.tsbtnAnalog.Click += new System.EventHandler(this.tsbtnAnalog_Click);
            // 
            // tsbtnSwitch
            // 
            this.tsbtnSwitch.AutoSize = false;
            this.tsbtnSwitch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSwitch.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSwitch.Image")));
            this.tsbtnSwitch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSwitch.Name = "tsbtnSwitch";
            this.tsbtnSwitch.RightToLeftAutoMirrorImage = true;
            this.tsbtnSwitch.Size = new System.Drawing.Size(50, 57);
            this.tsbtnSwitch.Text = "开关量";
            this.tsbtnSwitch.Click += new System.EventHandler(this.tsbtnSwitch_Click);
            // 
            // tsbtnControl
            // 
            this.tsbtnControl.AutoSize = false;
            this.tsbtnControl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnControl.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnControl.Image")));
            this.tsbtnControl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnControl.Name = "tsbtnControl";
            this.tsbtnControl.Size = new System.Drawing.Size(50, 57);
            this.tsbtnControl.Text = "控制逻辑";
            this.tsbtnControl.Click += new System.EventHandler(this.tsbtnControl_Click);
            // 
            // tsbtnHelp
            // 
            this.tsbtnHelp.AutoSize = false;
            this.tsbtnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnHelp.Image")));
            this.tsbtnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnHelp.Name = "tsbtnHelp";
            this.tsbtnHelp.Size = new System.Drawing.Size(50, 57);
            this.tsbtnHelp.Text = "帮助";
            this.tsbtnHelp.Click += new System.EventHandler(this.tsbtnHelp_Click);
            // 
            // tsbtnAbout
            // 
            this.tsbtnAbout.AutoSize = false;
            this.tsbtnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAbout.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAbout.Image")));
            this.tsbtnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAbout.Name = "tsbtnAbout";
            this.tsbtnAbout.Size = new System.Drawing.Size(50, 57);
            this.tsbtnAbout.Text = "关于";
            this.tsbtnAbout.Click += new System.EventHandler(this.tsbtnAbout_Click);
            // 
            // tsbSwitchUser
            // 
            this.tsbSwitchUser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSwitchUser.Image = ((System.Drawing.Image)(resources.GetObject("tsbSwitchUser.Image")));
            this.tsbSwitchUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSwitchUser.Name = "tsbSwitchUser";
            this.tsbSwitchUser.Size = new System.Drawing.Size(36, 29);
            this.tsbSwitchUser.Text = "切换用户";
            this.tsbSwitchUser.Click += new System.EventHandler(this.tsbSwitchUser_Click);
            // 
            // tsbtnExit
            // 
            this.tsbtnExit.AutoSize = false;
            this.tsbtnExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExit.Image")));
            this.tsbtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExit.Name = "tsbtnExit";
            this.tsbtnExit.Size = new System.Drawing.Size(50, 57);
            this.tsbtnExit.Text = "退出";
            this.tsbtnExit.Click += new System.EventHandler(this.tsbtnExit_Click);
            // 
            // mainDockPanel
            // 
            this.mainDockPanel.ActiveAutoHideContent = null;
            this.mainDockPanel.AllowEndUserDocking = false;
            this.mainDockPanel.AllowEndUserNestedDocking = false;
            this.mainDockPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mainDockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDockPanel.DockBackColor = System.Drawing.SystemColors.Control;
            this.mainDockPanel.ForeColor = System.Drawing.Color.Red;
            this.mainDockPanel.Location = new System.Drawing.Point(0, 130);
            this.mainDockPanel.MinimumSize = new System.Drawing.Size(75, 80);
            this.mainDockPanel.Name = "mainDockPanel";
            this.mainDockPanel.Size = new System.Drawing.Size(764, 600);
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            autoHideStripSkin1.TextFont = new System.Drawing.Font("宋体", 9F);
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            dockPaneStripSkin1.TextFont = new System.Drawing.Font("宋体", 9F);
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.InactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.InactiveCaptionText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.mainDockPanel.Skin = dockPanelSkin1;
            this.mainDockPanel.TabIndex = 8;
            // 
            // skinEngine1
            // 
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 730);
            this.Controls.Add(this.mainDockPanel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "KJ100NA";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void InitOldFenZhan()
        {
            int init_times = 0;
            int fenzhanNo = 1;
            while (true)
            {
                if (GlobalParams.AllfenZhans[fenzhanNo].commState == 0)
                {
                    fenzhanNo++;
                    init_times = 0;
                }
                else
                {
                    init_times++;
                    if (init_times > 1)
                    {
                        GlobalParams.AllfenZhans[fenzhanNo].commState = 0;
                        fenzhanNo++;
                        init_times = 0;
                    }
                }
                if (fenzhanNo > 60)
                {
                    return;
                }
                if ((GlobalParams.AllfenZhans[fenzhanNo].CommPort < 5) && (GlobalParams.AllfenZhans[fenzhanNo].CommPort > 0))
                {
                    UDPComm.Send(FenZhan.GetFenZhanConfigInfo((byte) fenzhanNo));
                    Thread.Sleep(50);
                }
            }
        }

        private void KaiGuanLiangBaoJingDispatch(FenZhanRTdata tmpud, CeDian cedian, int index)
        {
            string ceDianBianHao = cedian.CeDianBianHao;
            if (cedian.KaiGuanLiang.ShiFouBaoJing)
            {
                int temp = (int) tmpud.realValue[index];
                if (cedian.KaiGuanLiang.BaoJingZhuangTai == temp)
                {
                    if (YuJing.getValue(2))
                    {
                        base.Activate();
                    }
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
                    GlobalParams.warnList.Add("开关量，" + ceDianBianHao + "," + cedian.XiaoleiXing + "报警");
                }
                else if (GlobalParams.warnList.Count == 0)
                {
                    GlobalParams.alarm = false;
                }
            }
        }

        private void KaiGuanLiangDuanDianDispatch(FenZhanRTdata tmpud, CeDian cedian, int i, Dictionary<string, CeDian> allCeDian)
        {
            string ceDianBianHao = cedian.CeDianBianHao;
            string ZhuangTai = string.Empty;
            if (cedian.KaiGuanLiang.ShiFouDuanDian)
            {
                if (cedian.KaiGuanLiang.DuanDianZhuangTai == ((int) tmpud.realValue[i]))
                {
                    if (YuJing.getValue(4))
                    {
                        base.Activate();
                    }
                    string zhuangTai = "";
                    if (((int) tmpud.realValue[i]) == 0)
                    {
                        zhuangTai = cedian.KaiGuanLiang.LingTai;
                    }
                    else if (((int) tmpud.realValue[i]) == 1)
                    {
                        zhuangTai = cedian.KaiGuanLiang.YiTai;
                    }
                    else if (((int) tmpud.realValue[i]) == 2)
                    {
                        ZhuangTai = cedian.KaiGuanLiang.ErTai;
                    }
                    if (!allCeDian[ceDianBianHao].DuanDianFlag)
                    {
                        allCeDian[ceDianBianHao].DuanDianFlag = true;
                        allCeDian[ceDianBianHao].FuDianFlag = false;
                        DuanDianGuanXi.SendControlInfo(ceDianBianHao, 1, allCeDian);
                    }
                    if (bool.Parse(this.config.get("yuYinBaoJing")))
                    {
                        GlobalParams.alarm = true;
                        GlobalParams.cutList.Add("开关量，" + ceDianBianHao + "," + cedian.XiaoleiXing + "断电");
                    }
                }
                else
                {
                    allCeDian[ceDianBianHao].FuDianFlag = true;
                    if (allCeDian[ceDianBianHao].DuanDianFlag && DuanDianGuanXi.SendControlInfo(ceDianBianHao, 0, allCeDian))
                    {
                        allCeDian[ceDianBianHao].DuanDianFlag = false;
                    }
                }
            }
            if (cedian.KaiGuanLiang.ShiFouDuanDian && ((((tmpud.tongDaoZhuangTai[i] == 4) || (tmpud.tongDaoZhuangTai[i] == 5)) || (tmpud.tongDaoZhuangTai[i] == 6)) || (tmpud.tongDaoZhuangTai[i] == 7)))
            {
                if (YuJing.getValue(3))
                {
                    base.Activate();
                }
                if (!allCeDian[ceDianBianHao].DuanDianFlag)
                {
                    allCeDian[ceDianBianHao].DuanDianFlag = true;
                    allCeDian[ceDianBianHao].FuDianFlag = false;
                    DuanDianGuanXi.SendControlInfo(ceDianBianHao, 1, allCeDian);
                }
                if (bool.Parse(this.config.get("yuYinBaoJing")))
                {
                    GlobalParams.alarm = true;
                    GlobalParams.cutList.Add("开关量，" + ceDianBianHao + "," + cedian.XiaoleiXing + "断电");
                }
            }
        }

        private void KaiGuanLiangKuiDianYiChangDispatch(FenZhanRTdata tmpud, CeDian cedian, int index)
        {
            string ceDianBianHao = cedian.CeDianBianHao;
            int temp = (int) tmpud.realValue[index];
            if (tmpud.tongDaoZhuangTai[index] == 14)
            {
                if (YuJing.getValue(6))
                {
                    base.Activate();
                }
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
                if (bool.Parse(this.config.get("yuYinBaoJing")))
                {
                    GlobalParams.alarm = true;
                    GlobalParams.replayList.Add("开关量，" + ceDianBianHao + "," + cedian.XiaoleiXing + "馈电异常");
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Users.GlobalUserName != "")
            {
                if (!Logout.exit)
                {
                    e.Cancel = true;
                    new Logout().ShowDialog();
                }
            }
            else
            {
                Login.closeReason = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            GlobalParams.Initing = true;
            UDPComm.init(config.get("dbAddress"));
           
            Login lgn = new Login();
            if (lgn.ShowDialog() == DialogResult.Cancel)
            {
                base.Dispose();
            }
            else
            {
                GlobalParams.Comm_state = -1;
                string systemName = this.config.get("SystemName");
                if (systemName == null)
                {
                    systemName = "KJ100N(A)";
                }
                this.Text = systemName;
                string imagePath = this.config.get("MainFormImage");
                if (imagePath != null)
                {
                    string s1 = Application.StartupPath + @"\monitor\" + imagePath;
                    this.PictureAdd(s1);
                }


                GlobalParams.CreateDataStateTable();
                GlobalParams.DataState = GlobalParams.getDataParaTime();

                GlobalParams.allarea = new Area();
                GlobalParams.allarea.FillAddr();
                GlobalParams.AllkgLeiXing = new KaiGuanLiangList();
                GlobalParams.AllkgLeiXing.FillKaiGuanLiangLeiXing();
                GlobalParams.AllmnlLeiXing = new MoNiLiangLeiXingList();
                GlobalParams.AllmnlLeiXing.FillMoNiLiangLeiXing();
                GlobalParams.AllkzlLeiXing = new KongZhiLiangList();
                GlobalParams.AllkzlLeiXing.FillKongZhiLiang();
                GlobalParams.AllCeDianList = new CeDianList();
                GlobalParams.AllCeDianList.FillAllCeDian();
                GlobalParams.AllKeyWord = new KeyWordList();
                GlobalParams.AllKeyWord.FillKeyWord();
                GlobalParams.AllXianXingZhi = new XianXingZhiList();
                GlobalParams.AllXianXingZhi.FillXianXingZhi();
                DuanDianGuanXi.FillDuanDianGuanXi();
                KuiDianGuanXi.FillKuiDianGuanXi();
                GlobalParams.cedian_alarm = new CedianAlarm(GlobalParams.AllCeDianList);
                for (int kk = 0; kk < 60; kk++)
                {
                    GlobalParams.First_Show[kk] = true;
                }
                string s = "select * from YeKuang";
                DataTable dt = OperateDBAccess.Select(s);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    List_show ls = new List_show(this, dt.Rows[i]["name"].ToString(), true);
                    ls.Show(this.mainDockPanel, DockState.Document);
                    ls.id = i;
                    this.yeKuangs.Add(ls);
                }
                if (this.yeKuangs.Count > 0)
                {
                    this.yeKuangs[0].Activate();
                }
                DataTable dualTable = DuoSheBei.GetMulti();
                if ((dualTable != null) && (dualTable.Rows.Count > 0))
                {
                    foreach (DataRow row in dualTable.Rows)
                    {
                        int id = Convert.ToInt32(row["guanXiID"]);
                        string cdbh = row["ceDianBianHao"].ToString();
                        int state = Convert.ToInt32(row["baoJingZhuangTai"].ToString().Substring(0, 1));
                        List<int> list = null;
                        GlobalParams.dualAlarmInfo1.TryGetValue(cdbh, out list);
                        if (list == null)
                        {
                            list = new List<int> {
                                id
                            };
                            GlobalParams.dualAlarmInfo1.Add(cdbh, list);
                        }
                        else if (!list.Contains(id))
                        {
                            list.Add(id);
                        }
                        DualAlarmInfo info = new DualAlarmInfo {
                            Cedianbianhao = cdbh,
                            State = state,
                            Fenzhan = Convert.ToInt32(cdbh.Substring(0, 2)),
                            Tongdao = Convert.ToInt32(cdbh.Substring(3, 2))
                        };
                        List<DualAlarmInfo> list2 = null;
                        GlobalParams.dualAlarmInfo2.TryGetValue(id, out list2);
                        if (list2 == null)
                        {
                            list2 = new List<DualAlarmInfo> {
                                info
                            };
                            GlobalParams.dualAlarmInfo2.Add(id, list2);
                        }
                        else if (!list2.Contains(info))
                        {
                            list2.Add(info);
                        }

                        CeDian cd = GlobalParams.AllCeDianList.getCedianInfo(cdbh);
                        if (cd != null)
                        {
                            cd.IsMultiBaoji = true;
                        }
                    }
                }
               
                Dictionary<int, List<DualAlarmInfo>> temp1 = GlobalParams.dualAlarmInfo2;
                Dictionary<string, List<int>> temp2 = GlobalParams.dualAlarmInfo1;
                this.voice_alalrm_pro = new voiceAlarmPro();
                GlobalParams.yuyinAlalrm = Convert.ToBoolean(this.config.get("yuYinBaoJing"));
                if (this.config.get("sgBaojing") == null)
                {
                    this.config.add("sgBaojing", "False");
                    GlobalParams.sgAlarm = false;
                }
                else
                {
                    GlobalParams.sgAlarm = Convert.ToBoolean(this.config.get("sgBaojing"));
                }
              
                this.tsbtnAlarm.Checked = Convert.ToBoolean(this.config.get("yuYinBaoJing"));
                if ((Users.UserType == UserType.WATCHER) || MainFormRef.isOrdinaryVersion)
                {
                    this.参数设置ToolStripMenuItem.Enabled = false;
                    this.控制ToolStripMenuItem.Enabled = false;
                    this.通讯管理ToolStripMenuItem.Enabled = false;
                    this.系统管理ToolStripMenuItem.Enabled = false;
                    this.页面编辑ToolStripMenuItem.Enabled = false;
                    this.tsbtnAnalog.Enabled = false;
                    this.tsbtnControl.Enabled = false;
                    this.tsbtnNew.Enabled = false;
                    this.tsbtnSwitch.Enabled = false;
                    this.tsbtnTest.Enabled = false;
                }

                // 多设备组合报警
                combinedAlarmFrm = new List_alarm_dual();
                combinedAlarmFrm.Init();
                this.combinedAlarmFrm.Visible = true;

                this.InitThread = new Thread(new ThreadStart(this.InitOldFenZhan));
                this.InitThread.IsBackground = true;
                this.InitThread.Start();

                this.alarm_frame = new List_Alarm_Frame(GlobalParams.cedian_alarm);
                this.alarm_frame.Show();

                this.serverThread = new Thread(new ThreadStart(this.CommWithServer));
                this.serverThread.IsBackground = true;
                this.serverThread.Start();

                System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

                this.timer1.Enabled = true;

                this.sysTimer = new System.Timers.Timer(500.0);
                this.sysTimer.Elapsed += new ElapsedEventHandler(this.Timer_500ms);
                this.sysTimer.AutoReset = true;
                this.sysTimer.Enabled = true;

                this.alarmThread = new Thread(new ThreadStart(this.voice_alalrm_pro.voiceThread));
                this.alarmThread.IsBackground = true;
                this.alarmThread.Start();


                UDPComm u = new UDPComm(); 
                sendThread = new Thread(new ThreadStart(u.transThread));
                sendThread.IsBackground = true;
                sendThread.Start();

                GlobalParams.Initing = false;
            }
        }

        /// <summary>
        /// 重新初始化所有参数
        /// </summary>
        /// <returns></returns>
        public bool resetData()
        {
            GlobalParams.Initing = true;
            GlobalParams.allarea = new Area();
            GlobalParams.allarea.FillAddr();
            GlobalParams.AllkgLeiXing = new KaiGuanLiangList();
            GlobalParams.AllkgLeiXing.FillKaiGuanLiangLeiXing();
            GlobalParams.AllmnlLeiXing = new MoNiLiangLeiXingList();
            GlobalParams.AllmnlLeiXing.FillMoNiLiangLeiXing();
            GlobalParams.AllkzlLeiXing = new KongZhiLiangList();
            GlobalParams.AllkzlLeiXing.FillKongZhiLiang();
            GlobalParams.AllCeDianList = new CeDianList();
            GlobalParams.AllCeDianList.FillAllCeDian();
            GlobalParams.AllKeyWord = new KeyWordList();
            GlobalParams.AllKeyWord.FillKeyWord();
            GlobalParams.AllXianXingZhi = new XianXingZhiList();
            GlobalParams.AllXianXingZhi.FillXianXingZhi();
            DuanDianGuanXi.FillDuanDianGuanXi();
            KuiDianGuanXi.FillKuiDianGuanXi();
            GlobalParams.cedian_alarm = new CedianAlarm(GlobalParams.AllCeDianList);



            for (int i = 0; i < yeKuangs.Count; i++)
            {
                List_show ls = yeKuangs[i];
                ls.setInit();
            }
            
            DataTable dualTable = DuoSheBei.GetMulti();
            if ((dualTable != null) && (dualTable.Rows.Count > 0))
            {
                foreach (DataRow row in dualTable.Rows)
                {
                    int id = Convert.ToInt32(row["guanXiID"]);
                    string cdbh = row["ceDianBianHao"].ToString();
                    int state = Convert.ToInt32(row["baoJingZhuangTai"].ToString().Substring(0, 1));
                    List<int> list = null;
                    GlobalParams.dualAlarmInfo1.TryGetValue(cdbh, out list);
                    if (list == null)
                    {
                        list = new List<int> {
                                id
                            };
                        GlobalParams.dualAlarmInfo1.Add(cdbh, list);
                    }
                    else if (!list.Contains(id))
                    {
                        list.Add(id);
                    }
                    DualAlarmInfo info = new DualAlarmInfo
                    {
                        Cedianbianhao = cdbh,
                        State = state,
                        Fenzhan = Convert.ToInt32(cdbh.Substring(0, 2)),
                        Tongdao = Convert.ToInt32(cdbh.Substring(3, 2))
                    };
                    List<DualAlarmInfo> list2 = null;
                    GlobalParams.dualAlarmInfo2.TryGetValue(id, out list2);
                    if (list2 == null)
                    {
                        list2 = new List<DualAlarmInfo> {
                                info
                            };
                        GlobalParams.dualAlarmInfo2.Add(id, list2);
                    }
                    else if (!list2.Contains(info))
                    {
                        list2.Add(info);
                    }

                    CeDian cd = GlobalParams.AllCeDianList.getCedianInfo(cdbh);
                    if (cd != null)
                    {
                        cd.IsMultiBaoji = true;
                    }
                }
            }

            this.combinedAlarmFrm.Visible = true;
            Dictionary<int, List<DualAlarmInfo>> temp1 = GlobalParams.dualAlarmInfo2;
            Dictionary<string, List<int>> temp2 = GlobalParams.dualAlarmInfo1;
            this.voice_alalrm_pro = new voiceAlarmPro();
            GlobalParams.yuyinAlalrm = Convert.ToBoolean(this.config.get("yuYinBaoJing"));
            if (this.config.get("sgBaojing") == null)
            {
                this.config.add("sgBaojing", "False");
                GlobalParams.sgAlarm = false;
            }
            else
            {
                GlobalParams.sgAlarm = Convert.ToBoolean(this.config.get("sgBaojing"));
            }


            combinedAlarmFrm.Init();


            GlobalParams.Initing = false;
            return true;
        }

        

        
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void MoNiLiangBaoJingDispatch(FenZhanRTdata tmpud, CeDian cedian, int index)
        {
            string ceDianBianHao = cedian.CeDianBianHao;
            float temp = tmpud.realValue[index];
            if (((((cedian.MoNiLiang.BaoJingLeiXing == 0) && (temp > cedian.MoNiLiang.BaoJingZhiShangXian)) && (temp < cedian.MoNiLiang.DuanDianZhi)) || ((cedian.MoNiLiang.BaoJingLeiXing == 2) && (temp < cedian.MoNiLiang.BaoJingZhiXiaXian))) || (((cedian.MoNiLiang.BaoJingLeiXing == 1) && (cedian.MoNiLiang.BaoJingZhiShangXian > temp)) && (cedian.MoNiLiang.BaoJingZhiXiaXian < temp)))
            {
                if (YuJing.getValue(1))
                {
                    base.Activate();
                }
                if (bool.Parse(this.config.get("yuYinBaoJing")))
                {
                    GlobalParams.alarm = true;
                    GlobalParams.warnList.Add("模拟量，" + ceDianBianHao + "," + cedian.XiaoleiXing + "报警");
                }
            }
            else if (GlobalParams.warnList.Count == 0)
            {
                GlobalParams.alarm = false;
            }
        }

        private void MoNiLiangDuanDianDispatch(FenZhanRTdata tmpud, CeDian cedian, int i, Dictionary<string, CeDian> allCeDian)
        {
            string ceDianBianHao = cedian.CeDianBianHao;
            if (cedian.MoNiLiang.LeiXing != 2)
            {
                if (cedian.MoNiLiang.DuanDianZhi <= tmpud.realValue[i])
                {
                    if (YuJing.getValue(3))
                    {
                        base.Activate();
                    }
                    if (!allCeDian[ceDianBianHao].DuanDianFlag)
                    {
                        allCeDian[ceDianBianHao].DuanDianFlag = true;
                        allCeDian[ceDianBianHao].FuDianFlag = false;
                        DuanDianGuanXi.SendControlInfo(ceDianBianHao, 1, allCeDian);
                    }
                    if (bool.Parse(this.config.get("yuYinBaoJing")))
                    {
                        GlobalParams.alarm = true;
                        GlobalParams.cutList.Add("模拟量，" + ceDianBianHao + "," + cedian.XiaoleiXing + "断电");
                    }
                }
                else if (((cedian.MoNiLiang.FuDianZhi > tmpud.realValue[i]) && (tmpud.tongDaoZhuangTai[i] == 0)) && !allCeDian[ceDianBianHao].FuDianFlag)
                {
                    allCeDian[ceDianBianHao].FuDianFlag = true;
                    allCeDian[ceDianBianHao].DuanDianFlag = false;
                    if (DuanDianGuanXi.SendControlInfo(ceDianBianHao, 0, allCeDian))
                    {
                        allCeDian[ceDianBianHao].DuanDianFlag = false;
                    }
                }
            }
            if ((((tmpud.tongDaoZhuangTai[i] == 4) || (tmpud.tongDaoZhuangTai[i] == 5)) || (tmpud.tongDaoZhuangTai[i] == 6)) || (tmpud.tongDaoZhuangTai[i] == 7))
            {
                if (YuJing.getValue(3))
                {
                    base.Activate();
                }
                if (!allCeDian[ceDianBianHao].DuanDianFlag)
                {
                    allCeDian[ceDianBianHao].DuanDianFlag = true;
                    allCeDian[ceDianBianHao].FuDianFlag = false;
                    DuanDianGuanXi.SendControlInfo(ceDianBianHao, 1, allCeDian);
                }
                if (bool.Parse(this.config.get("yuYinBaoJing")))
                {
                    GlobalParams.alarm = true;
                    GlobalParams.cutList.Add("模拟量，" + ceDianBianHao + "," + cedian.XiaoleiXing + "断电");
                }
            }
        }

        private void MoNiLiangKuiDianYiChangDispatch(FenZhanRTdata tmpud, CeDian cedian, int index)
        {
            string ceDianBianHao = cedian.CeDianBianHao;
            float value = tmpud.realValue[index];
            if (ceDianBianHao == "03A01")
            {
            }
            if ((tmpud.tongDaoZhuangTai[index] == 14) && (value >= cedian.MoNiLiang.DuanDianZhi))
            {
                if (bool.Parse(this.config.get("yuYinBaoJing")))
                {
                }
                if (YuJing.getValue(5))
                {
                    base.Activate();
                }
                if (bool.Parse(this.config.get("yuYinBaoJing")))
                {
                    GlobalParams.alarm = true;
                    GlobalParams.replayList.Add("模拟量，" + ceDianBianHao + "," + cedian.XiaoleiXing + "馈电异常");
                }
            }
        }

        public void OpenCurveDisplay(string curveName, string ceDianBianHao)
        {
            if (this.frm_curve == null)
            {
                this.frm_curve = new Form_curve();
                this.frm_curve.Show();//(this.mainDockPanel, DockState.Document);
               // this.frm_curve.Show(this.mainDockPanel, DockState.Document);
                this.frm_curve.Cc.SetCurve(ceDianBianHao);
            }
            else if (this.frm_curve.IsDisposed)
            {
                this.frm_curve = new Form_curve(); 
                this.frm_curve.Show();
               // this.frm_curve.Show(this.mainDockPanel, DockState.Document);
                this.frm_curve.Cc.SetCurve(ceDianBianHao);
            }
            else
            {
                this.frm_curve.Activate();
                this.frm_curve.Cc.SetCurve(ceDianBianHao);
            }
            this.frm_curve.Tree.SelectedNode = this.frm_curve.Tree.Nodes.Find(curveName, true)[0];
        }

        public void OpenSwitchCurve(string curveName, string ceDianBianHao)
        {
            if (this.fsw == null)
            {
                this.fsw = new Form_switch();
                this.fsw.Show();//(this.mainDockPanel, DockState.Document);
                this.fsw.ZhuangTaiTu.SetCurve(ceDianBianHao);
            }
            else if (this.fsw.IsDisposed)
            {
                this.fsw = new Form_switch();
                this.fsw.Show();//(this.mainDockPanel, DockState.Document);
                this.fsw.ZhuangTaiTu.SetCurve(ceDianBianHao);
            }
            else
            {
                this.fsw.Activate();
            }
            this.fsw.Tree.SelectedNode = this.fsw.Tree.Nodes.Find(curveName, true)[0];
        }

        public void OpenTiaoJiao()
        {
            if (this.fpe == null)
            {
                this.fpe = new Form_page_edit(this);
                this.fpe.Show();
            }
            else if (this.fpe.IsDisposed)
            {
                this.fpe = new Form_page_edit(this);
                this.fpe.Show();
            }
            else
            {
                this.fpe.Activate();
            }
            this.fpe.Tree.SelectedNode = this.fpe.Tree.Nodes.Find("调校管理", true)[0];
        }

        public void PictureAdd(string s)
        {
            this.panel1.BackgroundImage = Image.FromFile(s);
        }

        private void refreashTime()
        {
            this.label1.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + " " + Users.GlobalUserName + "，你好！";
        }

        private void setFenZhanTimeOut()
        {
            for (int i = 1; i < 0x3d; i++)
            {
                if (((GlobalParams.AllfenZhans[i].commState == 2) || (GlobalParams.AllfenZhans[i].commState == 0)) && (GlobalParams.AllfenZhans[i].Recv_state == 0))
                {
                    GlobalParams.AllfenZhans[i].commState = 1;
                }
                GlobalParams.AllfenZhans[i].Recv_state = 0;
            }
        }

        public void SetTiaoJiaoCeDian(string ceDianBianHao)
        {
            if (this.fpe.Turn != null)
            {
                ComboBox box = this.fpe.Turn.CeDians;
                for (int i = 0; i < box.Items.Count; i++)
                {
                    if (box.Items[i].ToString().Contains(ceDianBianHao))
                    {
                        box.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        public void setYuYinBaoJin(bool yuYinBaoJing)
        {
            this.tsbtnAlarm.Checked = yuYinBaoJing;
        }

        private void TiaoJiaoEvent(object source, ElapsedEventArgs e)
        {
            int hour = e.SignalTime.Hour;
            int min = e.SignalTime.Minute;
            int sec = e.SignalTime.Second;
            if (((hour == 0x17) && (min == 0x3b)) && (sec == 0x3b))
            {
                TiaoJiao.DeleteAllTiaoJiao(e.SignalTime);
                CeDian[] cedians = GlobalParams.AllCeDianList.allcedianlist.Values.ToArray<CeDian>();
                for (int i = 0; i < cedians.Length; i++)
                {
                    if (cedians[i].TiaoJiao)
                    {
                        cedians[i].TiaoJiao = false;
                    }
                }
                this.tiaoJiaoTimer.Interval = 86340000.0;
            }
            else
            {
                this.tiaoJiaoTimer.Interval = 1000.0;
            }
        }

        public void Timer_500ms(object source, ElapsedEventArgs e)
        {
            if (base.IsDisposed)
            {
                this.sysTimer.Stop();
            }
            //if(!this.IsDisposed)
            //    this.label1.BeginInvoke(new RefreashTime(this.refreashTime));
          
            this.timer_5s++;
            bool beep = false;
            if (this.timer_5s >= 10)
            {
                this.timer_5s = 0;
                if (beep)
                {
                    if (GlobalParams.isBeep)
                    {
                        APIFunctions.CollateBeep();
                        GlobalParams.isBeep = false;
                    }
                    if ((GlobalParams.cedian_alarm.alarm_all_Dict != null) && (GlobalParams.cedian_alarm.alarm_all_Dict.Count > 0))
                    {
                        APIFunctions.CollateBeep();
                    }
                }

                DateTime dt = GlobalParams.getDataParaTime();
                if (!(dt == GlobalParams.DataState))
                {
                    GlobalParams.DataState = dt;
                    resetData();
                }
            }


            if (GlobalParams.sgAlarm && (GlobalParams.cedian_alarm.alarm_all_Dict != null))
            {
                string workdirectory;
                Exception eo;
                if (GlobalParams.cedian_alarm.alarm_all_Dict.Count > 0)
                {
                    if (!GlobalParams.sgAlarmState)
                    {
                        workdirectory = Application.StartupPath + @"\声光报警\AlarmLight.exe";
                        try
                        {
                            int myprocessID = Process.Start(workdirectory, @" 1").Id;
                            GlobalParams.sgAlarmState = true;
                        }
                        catch (Exception exception1)
                        {
                            eo = exception1;
                            GlobalParams.sgAlarmState = false;
                            MessageBox.Show("声光报警程序出错！原因" + eo.Message);
                        }
                    }
                }
                else if (GlobalParams.sgAlarmState)
                {
                    workdirectory = Application.StartupPath + @"\声光报警\AlarmLight.exe";
                    try
                    {
                        Process.Start(workdirectory, @" 0");
                        GlobalParams.sgAlarmState = false;
                    }
                    catch (Exception exception2)
                    {
                        eo = exception2;
                        GlobalParams.sgAlarmState = true;
                        MessageBox.Show("声光报警程序出错！原因" + eo.Message);
                    }
                }
            }

           
           
            this.timer_40s++;
            if (this.timer_40s >= 80)
            {               
                this.timer_40s = 0;
                this.setFenZhanTimeOut();
            }

            this.timer_10m++;
            if (timer_10m >= 720)
            {
                getLostCommKJ45();
                this.timer_10m = 0;

            }           
         
           timer_1s++ ;
           if (timer_1s >= 2)
           {
               timer_1s = 0;
               this.fresh_LieBiaoKuang();
               this.frame_all_alalrm.fresh();
               this.combinedAlarmFrm.Dispatch();

               if (this.tsbtnPreAlarm.Checked)
               {
                   frm_preAlarm.IsSetYJ = tsbtnPreAlarm.Checked;
                   if (frm_preAlarm.fresh())
                   {
                       frm_preAlarm.Show();
                       frm_preAlarm.Activate();
                   }
               }
           }

         
        }

        private int[] getLostCommKJ45()
        {
            int[] a = new int[60];
            int pos = 0;
            for (int i =1; i < 61; i++)
            {
                if (GlobalParams.AllfenZhans[i].CommPort < 5)
                {
                    if (GlobalParams.AllfenZhans[i].commState > 0)
                    {
                        a[pos++]=i;
                    }
                }
            }
            int[] b=new int[pos];
            Array.Copy(a, b, pos);           
            
            return b;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label1.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + " " + Users.GlobalUserName + "，你好！";     
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.toolStripButton1.Checked = !this.toolStripButton1.Checked;
            if (this.toolStripButton1.Checked)
            {
                YuJing.kk = false;
            }
            else
            {
                YuJing.kk = true;
            }
        }

        private void tsbSwitchUser_Click(object sender, EventArgs e)
        {
            new SwitchUser { StartPosition = FormStartPosition.CenterScreen }.ShowDialog();
        }

        private void tsbtnAbout_Click(object sender, EventArgs e)
        {
            new AboutForm { TopMost = true, StartPosition = FormStartPosition.CenterScreen }.Show();
        }

        private void tsbtnAlarm_Click(object sender, EventArgs e)
        {
            this.tsbtnAlarm.Checked = !this.tsbtnAlarm.Checked;
            this.config.set("yuYinBaoJing", this.tsbtnAlarm.Checked.ToString());
            GlobalParams.yuyinAlalrm = this.tsbtnAlarm.Checked;
        }

        private void tsbtnAnalog_Click(object sender, EventArgs e)
        {
            if (!((Users.UserType == UserType.WATCHER) && MainFormRef.isOrdinaryVersion))
            {
                this.参数设置ToolStripMenuItem_Click(sender, e);
                this.fpc.Tree.SelectedNode = this.fpc.Tree.Nodes.Find("一般模拟量", true)[0];
            }
        }

        private void tsbtnControl_Click(object sender, EventArgs e)
        {
            this.控制ToolStripMenuItem_Click(sender, e);
        }

        private void tsbtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsbtnHelp_Click(object sender, EventArgs e)
        {
            this.帮助ToolStripMenuItem_Click(sender, e);
        }

        private void tsbtnList_Click(object sender, EventArgs e)
        {
            this.列表显示ToolStripMenuItem_Click(sender, e);
        }

        private void tsbtnNew_Click(object sender, EventArgs e)
        {
            NewTab tab = new NewTab {
                TopMost = true
            };
            if (tab.ShowDialog() == DialogResult.OK)
            {
                string tabName = tab.TabName;
                if (tabName != string.Empty)
                {
                    foreach (List_show p in this.yeKuangs)
                    {
                        if (p.YeKuangMingCheng.Equals(tabName))
                        {
                            MessageBox.Show("名称为[" + tabName + "]的标签页已经存在！");
                            return;
                        }
                    }
                    List_show ls = new List_show(this, tabName, false);
                    ls.Show(this.mainDockPanel, DockState.Document);
                    ls.id = this.yeKuangs.Count;
                    this.yeKuangs.Add(ls);
                }
            }
        }

        private void tsbtnPreAlarm_Click(object sender, EventArgs e)
        {
            this.tsbtnPreAlarm.Checked = !this.tsbtnPreAlarm.Checked;
           // this.frm_preAlarm.TopMost = true;
            if (this.tsbtnPreAlarm.Checked)
            {
                if ((this.frm_preAlarm == null) || this.frm_preAlarm.IsDisposed)
                {
                    this.frm_preAlarm = new Form_PreAlarm(true);
                    this.frm_preAlarm.TopMost = true;
                }
                this.frm_preAlarm.Show();
            }
            else
            {
                this.frm_preAlarm.Hide();
            }
        }

        private void tsbtnSubstation_Click(object sender, EventArgs e)
        {
            this.tsbtnSubstation.Checked = !this.tsbtnSubstation.Checked;
            if ((this.fs == null) || this.fs.IsDisposed)
            {
                this.fs = new Form_substation();
            }
            this.fs.TopMost = true;
            if (this.tsbtnSubstation.Checked)
            {
                this.fs.Show();
            }
            else
            {
                this.fs.Hide();
            }
        }

        private void tsbtnSwitch_Click(object sender, EventArgs e)
        {
            if (!((Users.UserType == UserType.WATCHER) && MainFormRef.isOrdinaryVersion))
            {
                this.参数设置ToolStripMenuItem_Click(sender, e);
                this.fpc.Tree.SelectedNode = this.fpc.Tree.Nodes.Find("两态开关量", true)[0];
            }
        }

        private void tsbtnTest_Click(object sender, EventArgs e)
        {
            if (!((Users.UserType == UserType.WATCHER) && MainFormRef.isOrdinaryVersion))
            {
                this.参数设置ToolStripMenuItem_Click(sender, e);
                this.fpc.Tree.SelectedNode = this.fpc.Tree.Nodes.Find("模拟量测点", true)[0];
            }
        }

        public void writebuff(byte[] msg)
        {
            try
            {
                DateTime cur;
                string path;
                if (this.sw == null)
                {
                    cur = DateTime.Now;
                    path = Application.StartupPath.ToString();
                    this.filename = path + @"\" + string.Format("{0:yyyyMMdd}", cur);
                    this.sw = new StreamWriter(this.filename);
                }
                cur = DateTime.Now;
                if (cur.Day != this.dt.Day)
                {
                    path = Application.StartupPath.ToString();
                    this.filename = path + @"\" + string.Format("{0:yyyyMMdd}", cur);
                    this.sw = new StreamWriter(this.filename);
                    this.dt = cur;
                }
                string str = "";
                if (msg.Length > 0)
                {
                    for (int i = 0; i < msg.Length; i++)
                    {
                        str = str + " " + Convert.ToString(msg[i], 0x10).PadLeft(2, '0');
                    }
                }
                this.sw.WriteLine(cur.ToString() + "-" + str.ToUpper());
                this.sw.Flush();
            }
            catch
            {
            }
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "hh.exe ";
            p.StartInfo.Arguments = Application.StartupPath + @"\help.chm";
            p.Start();
        }

        private void 参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Users.UserType != UserType.WATCHER) || !MainFormRef.isOrdinaryVersion)
            {
                if (this.fpc == null)
                {
                    this.fpc = new Form_parameter_config(this);
                    this.fpc.Show();
                }
                else if (this.fpc.IsDisposed)
                {
                    this.fpc = new Form_parameter_config(this);
                    this.fpc.Show();
                }
                else
                {
                    this.fpc.Activate();
                }
            }
        }

        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string workdirectory = Application.StartupPath + @"\查询\查询.exe";
            try
            {
                Process.Start(workdirectory);
            }
            catch (Exception eo)
            {
                MessageBox.Show("启动应用程序出错！原因" + eo.Message);
            }
        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string workdirectory = Application.StartupPath + @"\报表\报表管理MIS.exe";
            try
            {
                Process.Start(workdirectory);
            }
            catch (Exception eo)
            {
                MessageBox.Show("启动应用程序出错！原因" + eo.Message);
            }
        }

        private void 控制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Users.UserType != UserType.WATCHER) || !MainFormRef.isOrdinaryVersion)
            {
                if (this.frm_ctlgx == null)
                {
                    this.frm_ctlgx = new Form_control();
                    this.frm_ctlgx.Show();
                }
                else if (this.frm_ctlgx.IsDisposed)
                {
                    this.frm_ctlgx = new Form_control();
                    this.frm_ctlgx.Show();
                }
                else
                {
                    this.frm_ctlgx.Activate();
                }
            }
        }

        private void 列表显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.frm_fzsblist == null)
            {
                this.frm_fzsblist = new Form_list();
                this.frm_fzsblist.Show();
            }
            else if (this.frm_fzsblist.IsDisposed)
            {
                this.frm_fzsblist = new Form_list();
                this.frm_fzsblist.Show();
            }
            else
            {
                this.frm_fzsblist.Activate();
            }
        }

        private void 模拟图显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.frm_monitu == null)
            {
                this.frm_monitu = new Form_mimic();
                this.frm_monitu.Show();
            }
            else if (this.frm_monitu.IsDisposed)
            {
                this.frm_monitu = new Form_mimic();
                this.frm_monitu.Show();
            }
            else
            {
                this.frm_monitu.Activate();
            }
        }

        private void 曲线显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.frm_curve == null)
            {
                this.frm_curve = new Form_curve();
                this.frm_curve.Show();
            }
            else if (this.frm_curve.IsDisposed)
            {
                this.frm_curve = new Form_curve();
                this.frm_curve.Show();
            }
            else
            {
                this.frm_curve.Activate();
            }
        }

        private void 日志查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.form_qry_log == null)
            {
                this.form_qry_log = new Form_qurey();
                this.form_qry_log.Show();
            }
            else if (this.form_qry_log.IsDisposed)
            {
                this.form_qry_log = new Form_qurey();
                this.form_qry_log.Show();
            }
            else
            {
                this.form_qry_log.Activate();
            }
        }

        private void 通讯管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Users.UserType != UserType.WATCHER) || !MainFormRef.isOrdinaryVersion)
            {
                if (this.fcm == null)
                {
                    this.fcm = new Form_commu();
                    this.fcm.Show();
                }
                else if (this.fcm.IsDisposed)
                {
                    this.fcm = new Form_commu();
                    this.fcm.Show();
                }
                else
                {
                    this.fcm.Activate();
                }
            }
        }

        private void 系统管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Users.UserType != UserType.WATCHER) || !MainFormRef.isOrdinaryVersion)
            {
                if (this.fsy == null)
                {
                    this.fsy = new Form_system(this);
                    this.fsy.Show();
                }
                else if (this.fsy.IsDisposed)
                {
                    this.fsy = new Form_system(this);
                    this.fsy.Show();
                }
                else
                {
                    this.fsy.Activate();
                }
            }
        }

        private void 页面编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Users.UserType != UserType.WATCHER) || !MainFormRef.isOrdinaryVersion)
            {
                if (this.fpe == null)
                {
                    this.fpe = new Form_page_edit(this);
                    this.fpe.Show();
                }
                else if (this.fpe.IsDisposed)
                {
                    this.fpe = new Form_page_edit(this);
                    this.fpe.Show();
                }
                else
                {
                    this.fpe.Activate();
                }
            }
        }

        private void 状态图与柱状图显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.fsw == null)
            {
                this.fsw = new Form_switch();
                this.fsw.Show();
            }
            else if (this.fsw.IsDisposed)
            {
                this.fsw = new Form_switch();
                this.fsw.Show();
            }
            else
            {
                this.fsw.Activate();
            }
        }

        private delegate void RefreashTime();
    }
}

