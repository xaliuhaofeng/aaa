namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Delete_Reason : Form
    {
        private ContextMenuStrip cms_g;
        private IContainer components = null;
        private TextBox textBox1;

        public Delete_Reason(ContextMenuStrip CMS)
        {
            this.InitializeComponent();
            this.cms_g = CMS;
        }

        private void Delete_Reason_Load(object sender, EventArgs e)
        {
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
            this.textBox1 = new TextBox();
            base.SuspendLayout();
            this.textBox1.Location = new Point(0x1b, 0x25);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0xe1, 0xc0);
            this.textBox1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(0x11c, 0x106);
            base.Controls.Add(this.textBox1);
            base.Name = "Delete_Reason";
            this.Text = "删除原因";
            base.Load += new EventHandler(this.Delete_Reason_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

