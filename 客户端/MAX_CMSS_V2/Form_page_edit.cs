namespace MAX_CMSS_V2
{
    using MyPictureBox;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;
    using WeifenLuo.WinFormsUI.Docking;

    public class Form_page_edit : DockContentEx
    {
        private Button button1;
        private Button button2;
        private IContainer components = null;
        private Color initColor;
        private MainForm mf;
        private TreeNode preSelectedNode;
        private SplitContainer splitContainer1;
        private TextBox textBox1;
        private Turn_management tm;
        private TreeView treeView1;

        public Form_page_edit(MainForm m)
        {
            this.InitializeComponent();
            this.initColor = this.treeView1.BackColor;
            this.mf = m;
            this.InitListView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("请输入自定义模拟图名称");
            }
            else
            {
                string newname = this.textBox1.Text.Trim();
                string fileName = Application.StartupPath + @"\monitor\cfg.xml";
                XmlDocument mydoc = new XmlDocument();
                mydoc.Load(fileName);
                XmlNode rootNode = mydoc.GetElementsByTagName("root").Item(0);
                XmlNodeList groupNodes = rootNode.ChildNodes;
                for (int i = 0; i < groupNodes.Count; i++)
                {
                    if (groupNodes.Item(i)["fullpic"].InnerText.Trim() == newname)
                    {
                        MessageBox.Show("名称已存在");
                        return;
                    }
                }
                XmlNode newGroup = mydoc.CreateNode(XmlNodeType.Element, "group", "");
                XmlNode fileName1 = mydoc.CreateNode(XmlNodeType.Element, "fullpic", "");
                fileName1.InnerText = newname;
                newGroup.AppendChild(fileName1);
                rootNode.AppendChild(newGroup);
                mydoc.Save(fileName);
                this.treeView1.Nodes["模拟图"].Nodes.Add(newname);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i;
            this.button2.Visible = false;
            string newname = this.textBox1.Text.Trim();
            string fileName = Application.StartupPath + @"\monitor\cfg.xml";
            XmlDocument mydoc = new XmlDocument();
            mydoc.Load(fileName);
            XmlNode rootNode = mydoc.GetElementsByTagName("root").Item(0);
            XmlNodeList groupNodes = rootNode.ChildNodes;
            for (i = 0; i < groupNodes.Count; i++)
            {
                XmlNode n = groupNodes.Item(i);
                if (n["fullpic"].InnerText.Trim() == newname)
                {
                    rootNode.RemoveChild(n);
                    break;
                }
            }
            mydoc.Save(fileName);
            for (i = 0; i < this.treeView1.Nodes["模拟图"].Nodes.Count; i++)
            {
                TreeNode tn = this.treeView1.Nodes["模拟图"].Nodes[i];
                if (newname == tn.Text)
                {
                    this.treeView1.Nodes["模拟图"].Nodes.Remove(tn);
                    break;
                }
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

        private void Form_page_edit_DoubleClick(object sender, EventArgs e)
        {
        }

        private void Form_page_edit_FormClosed(object sender, FormClosedEventArgs e)
        {
            FlushMemory();
        }

        private void Form_page_edit_Load(object sender, EventArgs e)
        {
        }

        private void Form_page_edit_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("双击");
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("曲线");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("通风系统");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("瓦斯抽采（放）");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("监控系统自检");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("模拟图", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("预警设置");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("配色方案");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("更换皮肤");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("语音报警");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("调校管理");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("多设备报警逻辑管理");
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
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
            treeNode1.Name = "曲线";
            treeNode1.Text = "曲线";
            treeNode2.Name = "通风系统";
            treeNode2.Text = "通风系统";
            treeNode3.Name = "瓦斯抽采";
            treeNode3.Text = "瓦斯抽采（放）";
            treeNode4.Name = "监控系统自检";
            treeNode4.Text = "监控系统自检";
            treeNode5.Name = "模拟图";
            treeNode5.Text = "模拟图";
            treeNode6.Name = "预警窗口";
            treeNode6.Text = "预警设置";
            treeNode7.Name = "配色方案";
            treeNode7.Text = "配色方案";
            treeNode8.Name = "更换皮肤";
            treeNode8.Text = "更换皮肤";
            treeNode9.Name = "语音报警";
            treeNode9.Text = "语音报警";
            treeNode10.Name = "调校管理";
            treeNode10.Text = "调校管理";
            treeNode11.Name = "多设备报警逻辑管理";
            treeNode11.Text = "多设备报警逻辑管理";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            this.treeView1.Size = new System.Drawing.Size(192, 461);
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
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new System.Drawing.Size(1084, 562);
            this.splitContainer1.SplitterDistance = 192;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(12, 530);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(168, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "删除自定义模拟图";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(12, 501);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "添加自定义模拟图";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 474);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(168, 21);
            this.textBox1.TabIndex = 4;
            // 
            // Form_page_edit
            // 
            this.ClientSize = new System.Drawing.Size(1084, 562);
            this.Controls.Add(this.splitContainer1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MinimizeBox = false;
            this.Name = "Form_page_edit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "页面编辑";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_page_edit_FormClosed);
            this.Load += new System.EventHandler(this.Form_page_edit_Load);
            this.DoubleClick += new System.EventHandler(this.Form_page_edit_DoubleClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Form_page_edit_MouseDoubleClick);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
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
                    this.treeView1.Nodes["模拟图"].Nodes.Add(name);
                }
            }
        }

        [DllImport("kernel32.dll")]
        private static extern bool SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            EditMoNiTu editMoNiTu;
            if (this.preSelectedNode != null)
            {
                this.preSelectedNode.BackColor = this.initColor;
            }
            e.Node.BackColor = Color.Gray;
            this.preSelectedNode = e.Node;
            this.button1.Visible = this.button2.Visible = this.textBox1.Visible = false;
            switch (e.Node.Name)
            {
                case "曲线":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Config_curve cc = new Config_curve {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(cc);
                    break;
                }
                case "通风系统":
                    this.splitContainer1.Panel2.Controls.Clear();
                    editMoNiTu = new EditMoNiTu(e.Node.Name) {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(editMoNiTu);
                    this.button1.Visible = this.textBox1.Visible = true;
                    break;

                case "瓦斯抽采":
                    this.splitContainer1.Panel2.Controls.Clear();
                    editMoNiTu = new EditMoNiTu(e.Node.Name) {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(editMoNiTu);
                    this.button1.Visible = this.textBox1.Visible = true;
                    break;

                case "监控系统自检":
                    this.splitContainer1.Panel2.Controls.Clear();
                    editMoNiTu = new EditMoNiTu(e.Node.Name) {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(editMoNiTu);
                    this.button1.Visible = this.textBox1.Visible = true;
                    break;

                case "预警窗口":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Config_Prealarm cp = new Config_Prealarm {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(cp);
                    break;
                }
                case "配色方案":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Config_color ccolor = new Config_color {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(ccolor);
                    break;
                }
                case "更换皮肤":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    changSkin cskin = new changSkin(this.mf) {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(cskin);
                    break;
                }
                case "语音报警":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    voiceAlarmUC va = new voiceAlarmUC(this.mf) {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(va);
                    break;
                }
                case "调校管理":
                    this.splitContainer1.Panel2.Controls.Clear();
                    this.tm = new Turn_management();
                    this.tm.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(this.tm);
                    break;

                case "多设备报警逻辑管理":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Multi_alarm ma = new Multi_alarm {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(ma);
                    break;
                }
                case "报表打印设置":
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    Config_Print cprint = new Config_Print {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(cprint);
                    break;
                }
                case "模拟图":
                    break;

                default:
                    this.splitContainer1.Panel2.Controls.Clear();
                    editMoNiTu = new EditMoNiTu(e.Node.Text) {
                        Dock = DockStyle.Fill
                    };
                    this.splitContainer1.Panel2.Controls.Add(editMoNiTu);
                    this.button1.Visible = this.button2.Visible = this.textBox1.Visible = true;
                    this.textBox1.Text = e.Node.Text;
                    break;
            }
        }

        public TreeView Tree
        {
            get
            {
                return this.treeView1;
            }
        }

        public Turn_management Turn
        {
            get
            {
                return this.tm;
            }
        }
    }
}

