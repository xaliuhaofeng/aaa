namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Form_substation : Form
    {
        private IContainer components = null;

        public Form_substation()
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

        private void Form_substation_Load(object sender, EventArgs e)
        {
            Config_substation substation = new Config_substation {
                Dock = DockStyle.Fill
            };
            base.Controls.Add(substation);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_substation));
            this.SuspendLayout();
            // 
            // Form_substation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 287);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_substation";
            this.Text = "分站状态";
            this.Load += new System.EventHandler(this.Form_substation_Load);
            this.ResumeLayout(false);

        }
    }
}

