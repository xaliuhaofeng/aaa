namespace WeifenLuo.WinFormsUI.Docking
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class DockContentEx2 : DockContent
    {
        protected ToolStripMenuItem tsmiClose;
        protected ToolStripMenuItem tsmiRename;

        public DockContentEx2()
        {
            ContextMenuStrip cms = new ContextMenuStrip();
            this.tsmiClose = new ToolStripMenuItem();
            this.tsmiRename = new ToolStripMenuItem();
            this.tsmiClose.Name = "cms";
            this.tsmiClose.Size = new Size(0x62, 0x16);
            this.tsmiClose.Text = "关闭";
            this.tsmiClose.Click += new EventHandler(this.tsmiClose_Click);
            this.tsmiRename.Name = "cms";
            this.tsmiRename.Size = new Size(0x62, 0x16);
            this.tsmiRename.Text = "重命名";
            this.tsmiRename.Click += new EventHandler(this.tsmiRename_Click);
            cms.Items.AddRange(new ToolStripItem[] { this.tsmiClose, this.tsmiRename });
            cms.Name = "tsmiCloseRename";
            cms.Size = new Size(0x63, 0x1a);
            base.TabPageContextMenuStrip = cms;
        }

        private void DockContentEx_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.ClientSize = new Size(0x11c, 0x106);
            this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Name = "DockContentEx2";
            base.Load += new EventHandler(this.DockContentEx_Load);
            base.ResumeLayout(false);
        }

        public virtual void tsmiClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        public virtual void tsmiRename_Click(object sender, EventArgs e)
        {
        }
    }
}

