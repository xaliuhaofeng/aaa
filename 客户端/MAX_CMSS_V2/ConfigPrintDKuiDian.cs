namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ConfigPrintDKuiDian : ConfigPrintAbstract
    {
        private IContainer components = null;

        public ConfigPrintDKuiDian()
        {
            this.InitializeComponent();
        }

        private void ConfigPrintDKuiDian_Load(object sender, EventArgs e)
        {
            base.SetReportType(ReportType.KaiGuanLiangKuiDian);
            base.checkboxGroup = new CheckBox[] { base.checkbox1, base.checkbox2, base.checkbox3, base.checkbox4, base.checkbox5, base.checkbox6, base.checkbox7 };
            string[] cols = new string[] { "监测地点/名称", "断电区域", "累计次数", "累计时间", "每次馈电状态|每次累计时间及起止时刻", "措施及时刻", "备注" };
            List<bool> colValue = base.getColumnValue();
            base.initialCheckBox(cols, colValue);
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
            base.Name = "ConfigPrintDKuiDian";
            base.Load += new EventHandler(this.ConfigPrintDKuiDian_Load);
            base.ResumeLayout(false);
        }
    }
}

