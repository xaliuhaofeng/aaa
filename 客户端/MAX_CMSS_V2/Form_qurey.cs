namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;

    public class Form_qurey : DockContentEx
    {
        private IContainer components = null;
        private Color initColor;
        private TreeNode preSelectedNode;
        private SplitContainer splitContainer1;
        private TreeView treeView1;

        public Form_qurey()
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

        private void Form_qurey_FormClosed(object sender, FormClosedEventArgs e)
        {
            FlushMemory();
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("调教记录表");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("操作日志");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("运行日志");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("日志查询", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new System.Drawing.Size(1084, 592);
            this.splitContainer1.SplitterDistance = 139;
            this.splitContainer1.TabIndex = 4;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ForeColor = System.Drawing.SystemColors.Control;
            this.treeView1.Indent = 20;
            this.treeView1.ItemHeight = 25;
            this.treeView1.LineColor = System.Drawing.Color.White;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "调教记录表";
            treeNode1.Text = "调教记录表";
            treeNode2.Name = "操作日志";
            treeNode2.Text = "操作日志";
            treeNode3.Name = "运行日志";
            treeNode3.Text = "运行日志";
            treeNode4.Name = "日志查询";
            treeNode4.Text = "日志查询";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treeView1.Size = new System.Drawing.Size(139, 592);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // Form_qurey
            // 
            this.ClientSize = new System.Drawing.Size(1084, 592);
            this.Controls.Add(this.splitContainer1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Form_qurey";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_qurey_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

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
                case "调教记录表":
                {
                    Query_TiaoJiao tiaoJiao = new Query_TiaoJiao {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(tiaoJiao);
                    break;
                }
                case "操作日志":
                {
                    Query_OperatorLog opLog = new Query_OperatorLog {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(opLog);
                    break;
                }
                case "运行日志":
                {
                    Query_SystemRunLog srLog = new Query_SystemRunLog {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(srLog);
                    break;
                }
                case "多设备报警历史记录查询":
                {
                    QueryMultiAlarm multiAlarm = new QueryMultiAlarm {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(multiAlarm);
                    break;
                }
                case "模拟量报警查询":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Query_ABaoJing qABaoJing = new Query_ABaoJing {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(qABaoJing);
                    break;
                }
                case "模拟量断电查询":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Query_ADuanDian2 qADuanDian = new Query_ADuanDian2 {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(qADuanDian);
                    break;
                }
                case "模拟量馈电异常查询":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Query_AKuiDian qAKuiDian = new Query_AKuiDian();
                    this.splitContainer1.Panel2.Controls.Add(qAKuiDian);
                    break;
                }
                case "开关量报警及断电查询":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Query_DBaoJing dBaoJing = new Query_DBaoJing {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(dBaoJing);
                    break;
                }
                case "开关量馈电异常查询":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Query_DKuiDianYiChang qKuiDianYiChang = new Query_DKuiDianYiChang {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(qKuiDianYiChang);
                    break;
                }
                case "开关量状态变动查询":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Query_DState qDState = new Query_DState {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(qDState);
                    break;
                }
                case "监控设备故障查询":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Query_SheBeiGuZhang qGuZhang = new Query_SheBeiGuZhang {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(qGuZhang);
                    break;
                }
                case "模拟量统计值历史记录查询":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Query_ATongJiZhi qATongJi = new Query_ATongJiZhi {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(qATongJi);
                    break;
                }
                case "报警断电记录查询":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Query_YueBaoBiao qYueBaoBiao = new Query_YueBaoBiao {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(qYueBaoBiao);
                    break;
                }
            }
        }
    }
}

