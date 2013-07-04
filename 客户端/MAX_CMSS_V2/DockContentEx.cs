namespace WeifenLuo.WinFormsUI.Docking
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class DockContentEx : DockContent
    {
        public DockContentEx()
        {
            ContextMenuStrip cms = new ContextMenuStrip();
            ToolStripMenuItem tsmiClose = new ToolStripMenuItem {
                Name = "cms",
                Size = new Size(0x62, 0x16),
                Text = "关闭"
            };
            tsmiClose.Click += new EventHandler(this.tsmiClose_Click);
            cms.Items.AddRange(new ToolStripItem[] { tsmiClose });
            cms.Name = "tsmiClose";
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
            base.Name = "DockContentEx";
            base.Load += new EventHandler(this.DockContentEx_Load);
            base.ResumeLayout(false);
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}

