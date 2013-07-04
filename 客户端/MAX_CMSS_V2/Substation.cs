namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Substation : Form
    {
        private IContainer components = null;

        public Substation()
        {
            this.InitializeComponent();
            Config_substation substation = new Config_substation {
                Dock = DockStyle.Fill
            };
            base.Controls.Add(substation);
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
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(0x32e, 0x1df);
            base.Name = "Substation";
            this.Text = "分站信息浏览";
            base.ResumeLayout(false);
        }
    }
}

