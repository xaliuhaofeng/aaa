namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Query_TiaoJiao : UserControl
    {
        private IContainer components = null;
        private QueryPanel queryPanel1;

        public Query_TiaoJiao()
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
            this.queryPanel1 = new QueryPanel();
            base.SuspendLayout();
            this.queryPanel1.Location = new Point(3, 3);
            this.queryPanel1.LogType = LogType.TiaoJiao;
            this.queryPanel1.Name = "queryPanel1";
            this.queryPanel1.Size = new Size(0x401, 0x204);
            this.queryPanel1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.queryPanel1);
            base.Name = "Query_TiaoJiao";
            base.Size = new Size(0x418, 0x207);
            base.ResumeLayout(false);
        }
    }
}

