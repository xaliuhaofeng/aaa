namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;

    public class Form_list : DockContentEx
    {
        private IContainer components = null;
        private FenZhanZhuanTai cs;
        private Color initColor;
        private TreeNode preSelectedNode;
        private SplitContainer splitContainer1;
        private TreeView treeView1;

        public Form_list()
        {
            this.InitializeComponent();
            this.initColor = this.treeView1.BackColor;
        }

        public void Dispatch(FenZhanRTdata ud, Dictionary<string, CeDian> allCeDian)
        {
            if (!((this.cs == null) || this.cs.IsDisposed))
            {
                this.cs.Dispatch(ud, allCeDian);
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

        private void Form_list_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("设备故障");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("开关量状态显示");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("分站状态");
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
            this.splitContainer1.Size = new System.Drawing.Size(1008, 562);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.TabIndex = 2;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ForeColor = System.Drawing.SystemColors.Control;
            this.treeView1.Indent = 20;
            this.treeView1.ItemHeight = 25;
            this.treeView1.LineColor = System.Drawing.Color.White;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode4.Name = "设备故障";
            treeNode4.Text = "设备故障";
            treeNode5.Name = "开关量状态显示";
            treeNode5.Text = "开关量状态显示";
            treeNode6.Name = "分站状态";
            treeNode6.Text = "分站状态";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6});
            this.treeView1.Size = new System.Drawing.Size(180, 562);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // Form_list
            // 
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.splitContainer1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Form_list";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "列表显示";
            this.Load += new System.EventHandler(this.Form_list_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.preSelectedNode != null)
            {
                this.preSelectedNode.BackColor = this.initColor;
            }
            e.Node.BackColor = Color.Gray;
            this.preSelectedNode = e.Node;
            string Reflector0001 = e.Node.Name;
            if (Reflector0001 != null)
            {
                if (!(Reflector0001 == "设备故障"))
                {
                    if (Reflector0001 == "开关量状态显示")
                    {
                        this.splitContainer1.Panel2.Controls.Clear();
                        Switch_status ss = new Switch_status {
                            Dock = DockStyle.Fill
                        };
                        this.splitContainer1.Panel2.Controls.Add(ss);
                    }
                    else if (Reflector0001 == "分站状态")
                    {
                        this.splitContainer1.Panel2.Controls.Clear();
                        this.cs = new FenZhanZhuanTai();
                        this.cs.Dock = DockStyle.Fill;
                        this.splitContainer1.Panel2.Controls.Add(this.cs);
                    }
                }
                else
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Device_error dc = new Device_error {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(dc);
                }
            }
        }
    }
}

