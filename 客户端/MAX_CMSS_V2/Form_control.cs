namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;
    using System.Collections.Generic;

    public class Form_control : DockContentEx
    {
        private Control_cut cc;
        private IContainer components = null;
        public byte flag = 0;
        private Color initColor;
        public Operation op;
        private TreeNode preSelectedNode;
        private SplitContainer splitContainer1;
        private TreeView treeView1;


       
        public Form_control()
        {
            this.InitializeComponent();
          //  this.ControlBox = false;  
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

        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("断电控制");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("馈电控制");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("逻辑控制", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("操作");
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
            this.splitContainer1.Size = new System.Drawing.Size(1008, 662);
            this.splitContainer1.SplitterDistance = 130;
            this.splitContainer1.TabIndex = 2;
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
            treeNode1.Name = "断电控制";
            treeNode1.Text = "断电控制";
            treeNode2.Name = "馈电控制";
            treeNode2.Text = "馈电控制";
            treeNode3.Name = "逻辑控制";
            treeNode3.Text = "逻辑控制";
            treeNode4.Name = "操作";
            treeNode4.Text = "操作";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            this.treeView1.Size = new System.Drawing.Size(130, 662);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // Form_control
            // 
            this.ClientSize = new System.Drawing.Size(1008, 662);
            this.Controls.Add(this.splitContainer1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Form_control";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "控制";
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

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
                if (!(Reflector0001 == "逻辑控制"))
                {
                    if (Reflector0001 == "断电控制")
                    {
                        this.splitContainer1.Panel2.Controls.Clear();
                        if (this.cc == null)
                        {
                            this.cc = new Control_cut();
                            this.cc.Dock = DockStyle.Fill;
                        }
                        this.splitContainer1.Panel2.Controls.Add(this.cc);
                    }
                    else if (Reflector0001 == "馈电控制")
                    {
                        this.splitContainer1.Panel2.Controls.Clear();
                        Control_feed cf = new Control_feed {
                            Dock = DockStyle.Fill
                        };
                        this.splitContainer1.Panel2.Controls.Add(cf);
                    }
                    else if (Reflector0001 == "操作")
                    {                      
                     
                        this.splitContainer1.Panel2.Controls.Clear();                       
                        this.op = new Operation(GlobalParams.Manula_Ctl_List);
                        this.op.Dock = DockStyle.Fill;
                        this.splitContainer1.Panel2.Controls.Add(this.op);
                    }
                }
                else
                {
                    e.Node.Expand();
                }
            }
        }

        public Control_cut Cc
        {
            get
            {
                return this.cc;
            }
            set
            {
                this.cc = value;
            }
        }

        public TreeView Tree
        {
            get
            {
                return this.treeView1;
            }
        }

     
    }
}

