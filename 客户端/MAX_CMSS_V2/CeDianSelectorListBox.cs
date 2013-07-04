namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CeDianSelectorListBox : UserControl
    {
        private CeDianSelector ceDianSelector1;
        private ColSelect2 colSelect1;
        private IContainer components = null;

        public CeDianSelectorListBox()
        {
            this.InitializeComponent();
            this.ceDianSelector1.DisplayList = this.colSelect1.ListBox1;
            this.ceDianSelector1.DisplayList2 = this.colSelect1.ListBox2;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public string[] GetSelectedCeDian()
        {
            string[] items = this.colSelect1.getSelectedCeDian();
            string[] ceDian = new string[items.Length];
            for (int i = 0; i < ceDian.Length; i++)
            {
                ceDian[i] = items[i].Substring(0, 5);
            }
            return ceDian;
        }

        public string[] GetSeletedCeDianAllInfo()
        {
            return this.colSelect1.getSelectedCeDian();
        }

        private void InitializeComponent()
        {
            this.colSelect1 = new ColSelect2();
            this.ceDianSelector1 = new CeDianSelector();
            base.SuspendLayout();
            this.colSelect1.Location = new Point(0x23, 0x2a);
            this.colSelect1.Name = "colSelect1";
            this.colSelect1.Size = new Size(740, 0x103);
            this.colSelect1.TabIndex = 1;
            this.ceDianSelector1.BackColor = Color.AliceBlue;
            this.ceDianSelector1.CeDianBianHaos = null;
            this.ceDianSelector1.DisplayList = null;
            this.ceDianSelector1.DisplayList2 = null;
            this.ceDianSelector1.Location = new Point(0x23, 3);
            this.ceDianSelector1.Name = "ceDianSelector1";
            this.ceDianSelector1.Size = new Size(740, 0x21);
            this.ceDianSelector1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.colSelect1);
            base.Controls.Add(this.ceDianSelector1);
            base.Name = "CeDianSelectorListBox";
            base.Size = new Size(0x323, 0x12d);
            base.ResumeLayout(false);
        }

        public ColSelect2 ColSelector
        {
            get
            {
                return this.colSelect1;
            }
        }

        public CeDianSelector Selector
        {
            get
            {
                return this.ceDianSelector1;
            }
        }
    }
}

