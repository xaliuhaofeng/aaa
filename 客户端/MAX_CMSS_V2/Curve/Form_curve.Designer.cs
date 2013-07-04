namespace MAX_CMSS_V2.Curve
{
    partial class Form_curve
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("调用");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("报警");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("断电");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("馈电异常");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("实时曲线");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("多曲线比较");
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
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new System.Drawing.Size(1193, 542);
            this.splitContainer1.SplitterDistance = 108;
            this.splitContainer1.TabIndex = 3;
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
            treeNode1.Name = "调用";
            treeNode1.Text = "调用";
            treeNode2.Name = "报警";
            treeNode2.Text = "报警";
            treeNode3.Name = "断电";
            treeNode3.Text = "断电";
            treeNode4.Name = "馈电异常";
            treeNode4.Text = "馈电异常";
            treeNode5.Name = "实时曲线";
            treeNode5.Text = "实时曲线";
            treeNode6.Name = "多曲线比较";
            treeNode6.Text = "多曲线比较";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            this.treeView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.treeView1.Size = new System.Drawing.Size(108, 542);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // Form_curve
            // 
            this.ClientSize = new System.Drawing.Size(1193, 542);
            this.Controls.Add(this.splitContainer1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Form_curve";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "曲线显示";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_curve_FormClosed);
            this.Load += new System.EventHandler(this.Form_curve_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
    }
}
