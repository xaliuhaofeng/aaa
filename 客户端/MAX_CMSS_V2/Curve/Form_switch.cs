using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyPictureBox;
using System.Runtime.InteropServices;
using System.Diagnostics;
using MAX_CMSS_V2.Curve;
namespace MAX_CMSS_V2.Curve
{
    public partial class Form_switch : WeifenLuo.WinFormsUI.Docking.DockContentEx
    {
        private TreeNode preSelectedNode;
        private Color initColor;

        private ZhuangTaiTu_Form zhuangTaiTu;
        public ZhuangTaiTu_Form ZhuangTaiTu
        {
            get { return zhuangTaiTu; }
            set { this.zhuangTaiTu = value; }
        }

        public TreeView Tree
        {
            get
            {
                return this.treeView1;
            }
        }
        public Form_switch()
        {
            InitializeComponent();
            initColor = this.treeView1.BackColor;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (preSelectedNode != null)
                preSelectedNode.BackColor = initColor;
            e.Node.BackColor = Color.Gray;
            preSelectedNode = e.Node;

            switch (e.Node.Name)
            {
                case "开关量状态图":
                    this.splitContainer1.Panel2.Controls.Clear();
                    zhuangTaiTu = new ZhuangTaiTu_Form();
                    zhuangTaiTu.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(zhuangTaiTu);
                    break;
                case "开关量柱状图":
                    this.splitContainer1.Panel2.Controls.Clear();
                    ZhuZhuangTu_Form zhuZhuangTu = new ZhuZhuangTu_Form("");
                    zhuZhuangTu.Dock = DockStyle.Fill;                    
                    this.splitContainer1.Panel2.Controls.Add(zhuZhuangTu);
                    break;
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
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
        private void Form_switch_FormClosed(object sender, FormClosedEventArgs e)
        {
            FlushMemory();
        }
    }
}
