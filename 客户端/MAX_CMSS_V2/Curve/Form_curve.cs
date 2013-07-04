using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Logic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using MAX_CMSS_V2.Curve;
namespace MAX_CMSS_V2.Curve
{
    public partial class Form_curve : WeifenLuo.WinFormsUI.Docking.DockContentEx
    {
        private bool showRealTime;
        private string currentCeDian;
        RealTime_curve rtc;
        Alarm_curve ac;
        Call_curve cc;

        public Call_curve Cc
        {
            get { return cc; }
            set { cc = value; }
        }


       

        public Alarm_curve AlarmCurve
        {
            get { return ac; }
        }

        public TreeView Tree
        {
            get
            {
                return this.treeView1;
            }
        }
        public Form_curve()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Name)
            {
                case "报警":
                    this.showRealTime = false;
                    this.splitContainer1.Panel2.Controls.Clear();
                    ac = new Alarm_curve();
                    ac.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(ac);
                    break;
                case "调用":
                    this.showRealTime = false;
                    this.splitContainer1.Panel2.Controls.Clear();
                     Cc = new Call_curve();
                     Cc.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(cc);
                    break;
                case "断电":
                    this.showRealTime = false;
                    this.splitContainer1.Panel2.Controls.Clear();
                    Cut_curve cutc = new Cut_curve();
                    cutc.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(cutc);
                    break;
                case "馈电异常":
                    this.showRealTime = false;
                    this.splitContainer1.Panel2.Controls.Clear();
                    Feed_curve fc = new Feed_curve();
                    fc.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(fc);
                    break;
                case "实时曲线":
                    this.showRealTime = true;
                    this.splitContainer1.Panel2.Controls.Clear();
                    rtc = new RealTime_curve(this.currentCeDian);
                    rtc.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(rtc);
                    break;
                case "多曲线比较":
                    this.showRealTime = false;
                    this.splitContainer1.Panel2.Controls.Clear();
                    Multi_Curve mulc = new Multi_Curve();
                    mulc.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(mulc);
                    break;
            }
            this.splitContainer1.Panel2.Refresh();
        }
        public void ShowRealTimeCurve(string ceDianBianHao)
        {
            this.currentCeDian = ceDianBianHao;
            this.treeView1.SelectedNode = this.treeView1.Nodes["实时曲线"];
            //this.splitContainer1.Panel2.Controls.Clear();
            //RealTime_curve rtc = new RealTime_curve(ceDianBianHao);
            //rtc.Dock = DockStyle.Fill;
            //this.splitContainer1.Panel2.Controls.Add(rtc);
        }
        public void Dispatch(FenZhanRTdata ud)
        {
            if (!showRealTime)
            {
                return;
            }
            if ((rtc != null) && (!rtc.IsDisposed))
            {
                rtc.Dispatch(ud);
            }
        }

        private void Form_curve_Load(object sender, EventArgs e)
        {

        }
        [DllImport("kernel32.dll")]
        private static extern bool SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        private static void FlushMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
        } 
        private void Form_curve_FormClosed(object sender, FormClosedEventArgs e)
        {
            FlushMemory();   
        }
    }
}
