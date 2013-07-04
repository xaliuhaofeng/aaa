namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CustomMenuStrip : MenuStrip
    {
        private Color _themeColor = Color.Gray;
        private IContainer components = null;

        public CustomMenuStrip()
        {
            this.InitializeComponent();
            base.Renderer = new CustomProfessionalRenderer(this._themeColor);
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

        public Color ThemeColor
        {
            get
            {
                return this._themeColor;
            }
            set
            {
                this._themeColor = value;
                base.Renderer = new CustomProfessionalRenderer(this._themeColor);
            }
        }
    }
}

