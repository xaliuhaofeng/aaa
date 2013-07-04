namespace MyPictureBox
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class YeKuang : UserControl
    {
        private IContainer components = null;

        public YeKuang()
        {
            this.InitializeComponent();
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
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "YeKuang";
            base.Size = new Size(0x340, 0x16f);
            base.ResumeLayout(false);
        }
    }
}

