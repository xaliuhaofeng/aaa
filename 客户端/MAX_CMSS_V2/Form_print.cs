namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;

    public class Form_print : DockContentEx
    {
        private IContainer components = null;
        private Color initColor;
        private TreeNode preSelectedNode;
        private SplitContainer splitContainer1;
        private TreeView treeView1;

        public Form_print()
        {
            this.InitializeComponent();
            this.initColor = this.treeView1.BackColor;
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

        private void Form_print_FormClosed(object sender, FormClosedEventArgs e)
        {
            FlushMemory();
        }

        private void InitializeComponent()
        {
            TreeNode treeNode1 = new TreeNode("模拟量日报表");
            TreeNode treeNode2 = new TreeNode("模拟量报警日报表");
            TreeNode treeNode3 = new TreeNode("模拟量断电日报表");
            TreeNode treeNode4 = new TreeNode("模拟量馈电异常日报表");
            TreeNode treeNode5 = new TreeNode("开关量报警及断电日报表");
            TreeNode treeNode6 = new TreeNode("开关量馈电异常日报表");
            TreeNode treeNode7 = new TreeNode("开关量状态变动日报表");
            TreeNode treeNode8 = new TreeNode("监控设备故障日报表");
            TreeNode treeNode9 = new TreeNode("模拟量统计值历史记录查询报表");
            TreeNode treeNode10 = new TreeNode("报警断电记录月报表");
            this.splitContainer1 = new SplitContainer();
            this.treeView1 = new TreeView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            base.SuspendLayout();
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new Size(0x3f0, 0x19c);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.TabIndex = 3;
            this.treeView1.BackColor = Color.CornflowerBlue;
            this.treeView1.Dock = DockStyle.Fill;
            this.treeView1.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.treeView1.ForeColor = SystemColors.Control;
            this.treeView1.Indent = 20;
            this.treeView1.ItemHeight = 0x19;
            this.treeView1.LineColor = Color.White;
            this.treeView1.Location = new Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "模拟量日报表";
            treeNode1.Text = "模拟量日报表";
            treeNode2.Name = "模拟量报警日报表";
            treeNode2.Text = "模拟量报警日报表";
            treeNode3.Name = "模拟量断电日报表";
            treeNode3.Text = "模拟量断电日报表";
            treeNode4.Name = "模拟量馈电异常日报表";
            treeNode4.Text = "模拟量馈电异常日报表";
            treeNode5.Name = "开关量报警及断电日报表";
            treeNode5.Text = "开关量报警及断电日报表";
            treeNode6.Name = "开关量馈电异常日报表";
            treeNode6.Text = "开关量馈电异常日报表";
            treeNode7.Name = "开关量状态变动日报表";
            treeNode7.Text = "开关量状态变动日报表";
            treeNode8.Name = "监控设备故障日报表";
            treeNode8.Text = "监控设备故障日报表";
            treeNode9.Name = "模拟量统计值历史记录查询报表";
            treeNode9.Text = "模拟量统计值历史记录查询报表";
            treeNode10.Name = "报警断电记录月报表";
            treeNode10.Text = "报警断电记录月报表";
            this.treeView1.Nodes.AddRange(new TreeNode[] { treeNode1, treeNode2, treeNode3, treeNode4, treeNode5, treeNode6, treeNode7, treeNode8, treeNode9, treeNode10 });
            this.treeView1.Size = new Size(180, 0x19c);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            base.ClientSize = new Size(0x3f0, 0x19c);
            base.Controls.Add(this.splitContainer1);
             base.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Name = "Form_print";
            this.Text = "打印";
            base.FormClosed += new FormClosedEventHandler(this.Form_print_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
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
            this.splitContainer1.Panel2.Controls.Clear();
            switch (e.Node.Name)
            {
                case "模拟量日报表":
                {
                    Print_A pA = new Print_A {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(pA);
                    break;
                }
                case "模拟量报警日报表":
                {
                    Print_ABaoJing pABaoJing = new Print_ABaoJing {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(pABaoJing);
                    break;
                }
                case "模拟量断电日报表":
                {
                    Print_ADuanDian pADuanDian = new Print_ADuanDian {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(pADuanDian);
                    break;
                }
                case "模拟量馈电异常日报表":
                {
                    Print_AKuiDian pAKuiDian = new Print_AKuiDian {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(pAKuiDian);
                    break;
                }
                case "开关量报警及断电日报表":
                {
                    Print_DBaoJing pDBaoJing = new Print_DBaoJing {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(pDBaoJing);
                    break;
                }
                case "开关量馈电异常日报表":
                {
                    Print_DKuiDian pDKuiDian = new Print_DKuiDian {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(pDKuiDian);
                    break;
                }
                case "开关量状态变动日报表":
                {
                    Print_DState pDState = new Print_DState {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(pDState);
                    break;
                }
                case "监控设备故障日报表":
                {
                    Print_SheBeiGuZhang pGuZhang = new Print_SheBeiGuZhang {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(pGuZhang);
                    break;
                }
                case "模拟量统计值历史记录查询报表":
                {
                    Print_ATongJiZhi pATongJiZhi = new Print_ATongJiZhi {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(pATongJiZhi);
                    break;
                }
                case "报警断电记录月报表":
                {
                    Print_YueBaoBiao pAYueBaoBiao = new Print_YueBaoBiao {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(pAYueBaoBiao);
                    break;
                }
            }
        }
    }
}

