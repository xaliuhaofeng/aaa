namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;

    public class Form_commu : DockContentEx
    {
        private FenZhanConfig cc;
        private Comm_ChuanKou chuanKou;
        private IContainer components = null;
        private Color initColor;
        private TreeNode preSelectedNode;
        private SplitContainer splitContainer1;
        private TreeView treeView1;

        public Form_commu()
        {
            this.InitializeComponent();
            this.initColor = this.treeView1.BackColor;
        }

        public void Dispatch(FenZhanRTdata ud)
        {
            if (!((this.cc == null) || this.cc.IsDisposed))
            {
                this.cc.ShowInListView(ud);
            }
            if (!((this.chuanKou == null) || this.chuanKou.IsDisposed))
            {
                this.chuanKou.Dispatch(ud);
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

        private static void FlushMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        private void Form_commu_FormClosed(object sender, FormClosedEventArgs e)
        {
            FlushMemory();
        }

        private void Form_commu_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            TreeNode treeNode1 = new TreeNode("分站配置");
            TreeNode treeNode2 = new TreeNode("串口配置");
            TreeNode treeNode3 = new TreeNode("风电瓦斯配置");
            TreeNode treeNode4 = new TreeNode("故障闭锁");
            TreeNode treeNode5 = new TreeNode("校时");
            TreeNode treeNode6 = new TreeNode("通讯测试");
            TreeNode treeNode7 = new TreeNode("远程电源管理");
            this.splitContainer1 = new SplitContainer();
            this.treeView1 = new TreeView();
            this.splitContainer1.BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            base.SuspendLayout();
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new Size(0x424, 0x1ed);
            this.splitContainer1.SplitterDistance = 0xbd;
            this.splitContainer1.TabIndex = 4;
            this.treeView1.BackColor = Color.CornflowerBlue;
            this.treeView1.Dock = DockStyle.Fill;
            this.treeView1.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.treeView1.ForeColor = SystemColors.Control;
            this.treeView1.Indent = 20;
            this.treeView1.ItemHeight = 0x19;
            this.treeView1.LineColor = Color.White;
            this.treeView1.Location = new Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "分站配置";
            treeNode1.Text = "分站配置";
            treeNode2.Name = "串口配置";
            treeNode2.Text = "串口配置";
            treeNode3.Name = "风电瓦斯配置";
            treeNode3.Text = "风电瓦斯配置";
            treeNode4.Name = "故障闭锁";
            treeNode4.Text = "故障闭锁";
            treeNode5.Name = "校时";
            treeNode5.Text = "校时";
            treeNode6.Name = "通讯测试";
            treeNode6.Text = "通讯测试";
            treeNode7.Name = "远程电源管理";
            treeNode7.Text = "远程电源管理";
            this.treeView1.Nodes.AddRange(new TreeNode[] { treeNode1, treeNode2, treeNode3, treeNode4, treeNode5, treeNode6, treeNode7 });
            this.treeView1.Size = new Size(0xbd, 0x1ed);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            base.ClientSize = new Size(0x424, 0x1ed);
            base.Controls.Add(this.splitContainer1);
             base.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Name = "Form_commu";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通讯管理";
            base.FormClosed += new FormClosedEventHandler(this.Form_commu_FormClosed);
            base.Load += new EventHandler(this.Form_commu_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.EndInit();
            this.splitContainer1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        [DllImport("kernel32.dll")]
        private static extern bool SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.preSelectedNode != null)
            {
                this.preSelectedNode.BackColor = this.initColor;
            }
            e.Node.BackColor = Color.Gray;
            this.preSelectedNode = e.Node;
            switch (e.Node.Name)
            {
                case "分站配置":
                    this.splitContainer1.Panel2.Controls.Clear();
                    this.cc = new FenZhanConfig("确定");
                    this.cc.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(this.cc);
                    break;

                case "校时":
                    this.splitContainer1.Panel2.Controls.Clear();
                    this.cc = new FenZhanConfig("校时");
                    this.cc.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(this.cc);
                    break;

                case "通讯测试":
                    this.splitContainer1.Panel2.Controls.Clear();
                    this.cc = new FenZhanConfig("通信测试");
                    this.cc.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(this.cc);
                    break;

                case "风电瓦斯配置":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Comm_FengDian fengDian = new Comm_FengDian {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(fengDian);
                    break;
                }
                case "故障闭锁":
                    this.splitContainer1.Panel2.Controls.Clear();
                    this.cc = new FenZhanConfig("设置");
                    this.cc.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(this.cc);
                    break;

                case "远程电源管理":
                    this.splitContainer1.Panel2.Controls.Clear();
                    this.cc = new FenZhanConfig("重启");
                    this.cc.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(this.cc);
                    break;

                case "串口配置":
                    this.splitContainer1.Panel2.Controls.Clear();
                    this.chuanKou = new Comm_ChuanKou();
                    this.chuanKou.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(this.chuanKou);
                    break;
            }
        }
    }
}

