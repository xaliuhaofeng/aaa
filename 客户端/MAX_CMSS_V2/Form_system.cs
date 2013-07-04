namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;

    public class Form_system : DockContentEx
    {
        private IContainer components = null;
        private Color initColor;
        private MainForm MF;
        private TreeNode preSelectedNode;
        private SplitContainer splitContainer1;
        private TreeView treeView1;

        public Form_system(MainForm mf)
        {
            this.InitializeComponent();
            this.initColor = this.treeView1.BackColor;
            this.MF = mf;
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

        private void Form_system_FormClosed(object sender, FormClosedEventArgs e)
        {
            FlushMemory();
        }

        private void InitializeComponent()
        {
            TreeNode treeNode1 = new TreeNode("用户管理");
            TreeNode treeNode2 = new TreeNode("系统配置管理");
            TreeNode treeNode3 = new TreeNode("数据库配置管理");
            TreeNode treeNode4 = new TreeNode("关键字管理");
            TreeNode treeNode5 = new TreeNode("线性值管理");
            TreeNode treeNode6 = new TreeNode("测点图标管理");
            TreeNode treeNode7 = new TreeNode("分站工作类型");
            TreeNode treeNode8 = new TreeNode("双机热备");
            TreeNode treeNode9 = new TreeNode("系统初始化");
            this.treeView1 = new TreeView();
            this.splitContainer1 = new SplitContainer();
            this.splitContainer1.BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            base.SuspendLayout();
            this.treeView1.BackColor = Color.CornflowerBlue;
            this.treeView1.Dock = DockStyle.Fill;
            this.treeView1.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.treeView1.ForeColor = SystemColors.Control;
            this.treeView1.Indent = 20;
            this.treeView1.ItemHeight = 0x19;
            this.treeView1.LineColor = Color.White;
            this.treeView1.Location = new Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "用户管理";
            treeNode1.Text = "用户管理";
            treeNode2.Name = "系统配置";
            treeNode2.Text = "系统配置管理";
            treeNode3.Name = "数据库配置";
            treeNode3.Text = "数据库配置管理";
            treeNode4.Name = "关键字管理";
            treeNode4.Text = "关键字管理";
            treeNode5.Name = "线性值管理";
            treeNode5.Text = "线性值管理";
            treeNode6.Name = "测点图标管理";
            treeNode6.Text = "测点图标管理";
            treeNode7.Name = "分站工作类型";
            treeNode7.Text = "分站工作类型";
            treeNode8.Name = "双机热备";
            treeNode8.Text = "双机热备";
            treeNode9.Name = "系统初始化";
            treeNode9.Text = "系统初始化";
            this.treeView1.Nodes.AddRange(new TreeNode[] { treeNode1, treeNode2, treeNode3, treeNode4, treeNode5, treeNode6, treeNode7, treeNode8, treeNode9 });
            this.treeView1.Size = new Size(0xc1, 0x204);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new Size(0x43a, 0x204);
            this.splitContainer1.SplitterDistance = 0xc1;
            this.splitContainer1.TabIndex = 5;
            base.ClientSize = new Size(0x43a, 0x204);
            base.Controls.Add(this.splitContainer1);
             base.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
           
            this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Name = "Form_system";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统管理";
            base.FormClosed += new FormClosedEventHandler(this.Form_system_FormClosed);
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
                case "用户管理":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    User_manage um = new User_manage {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(um);
                    break;
                }
                case "系统配置":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    System_manage sm = new System_manage(this.MF) {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(sm);
                    break;
                }
                case "数据库配置":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Database_manage dm = new Database_manage {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(dm);
                    break;
                }
                case "关键字管理":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Keywords_manage km = new Keywords_manage {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(km);
                    break;
                }
                case "线性值管理":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Linear_manage lm = new Linear_manage {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(lm);
                    break;
                }
                case "测点图标管理":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    TestpointPicture_manage tm = new TestpointPicture_manage {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(tm);
                    break;
                }
                case "分站工作类型":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Substation_type st = new Substation_type {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(st);
                    break;
                }
                case "双机热备":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Dual_hot dh = new Dual_hot {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(dh);
                    break;
                }
                case "系统初始化":
                    this.splitContainer1.Panel2.Controls.Clear();
                    this.MF.init = new SystemInit();
                    this.MF.init.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(this.MF.init);
                    break;
            }
        }
    }
}

