namespace MAX_CMSS_V2
{
    using Logic;
    using MyPictureBox;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;
    using WeifenLuo.WinFormsUI.Docking;

    public class Form_mimic : DockContentEx
    {
        private FullPicture ac;
        private IContainer components = null;
        private Color initColor;
        private TreeNode preSelectedNode;
        private SplitContainer splitContainer1;
        private TreeView treeView1;

        public Form_mimic()
        {
            this.InitializeComponent();
            this.initColor = this.treeView1.BackColor;
            this.ac = null;
            this.InitListView();
        }

        internal void Dispatch(FenZhanRTdata ud)
        {
            if (!((this.ac == null) || this.ac.IsDisposed))
            {
                this.ac.Dispatch(ud);
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

        private void Form_mimic_FormClosed(object sender, FormClosedEventArgs e)
        {
            FlushMemory();
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("通风系统");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("瓦斯抽采");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("监控系统自检");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("系统模拟图", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ForeColor = System.Drawing.SystemColors.Control;
            this.treeView1.Indent = 20;
            this.treeView1.ItemHeight = 25;
            this.treeView1.LineColor = System.Drawing.Color.White;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "通风系统";
            treeNode1.Text = "通风系统";
            treeNode2.Name = "瓦斯抽采";
            treeNode2.Text = "瓦斯抽采";
            treeNode3.Name = "监控系统自检";
            treeNode3.Text = "监控系统自检";
            treeNode4.Name = "系统模拟图";
            treeNode4.Text = "系统模拟图";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treeView1.Size = new System.Drawing.Size(180, 344);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
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
            this.splitContainer1.Size = new System.Drawing.Size(1008, 412);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.TabIndex = 3;
            // 
            // Form_mimic
            // 
            this.ClientSize = new System.Drawing.Size(1008, 412);
            this.Controls.Add(this.splitContainer1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Form_mimic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模拟图显示";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_mimic_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void InitListView()
        {
            string fileName = Application.StartupPath + @"\monitor\cfg.xml";
            XmlDocument mydoc = new XmlDocument();
            mydoc.Load(fileName);
            XmlNodeList groupNodes = mydoc.GetElementsByTagName("root").Item(0).ChildNodes;
            for (int i = 0; i < groupNodes.Count; i++)
            {
                string name = groupNodes.Item(i)["fullpic"].InnerText.Trim();
                if (((name != "监控系统自检") && (name != "瓦斯抽采")) && !(name == "通风系统"))
                {
                    this.treeView1.Nodes.Add(name);
                }
            }
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
            string Reflector0001 = e.Node.Name;
            if ((Reflector0001 != null) && (Reflector0001 == "系统模拟图"))
            {
                e.Node.Expand();
            }
            else
            {
                this.splitContainer1.Panel2.Controls.Clear();
                this.ac = new FullPicture();
                this.ac.Dock = DockStyle.Fill;
                this.splitContainer1.Panel2.Controls.Add(this.ac);
                this.ac.OpenImg(e.Node.Text, false);
            }
        }
    }
}

