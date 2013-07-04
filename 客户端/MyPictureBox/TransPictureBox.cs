namespace MyPictureBox
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class TransPictureBox : PictureBox
    {
        private IContainer components = null;

        public TransPictureBox()
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
            this.components = new Container();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                base.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
                System.Windows.Forms.CreateParams result = base.CreateParams;
                result.ExStyle |= 0x20;
                return result;
            }
        }
    }
}

