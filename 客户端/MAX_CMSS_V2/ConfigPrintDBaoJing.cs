namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ConfigPrintDBaoJing : ConfigPrintAbstract
    {
        private IContainer components = null;

        public ConfigPrintDBaoJing()
        {
            this.InitializeComponent();
        }

        private void ConfigPrintDBaoJing_Load(object sender, EventArgs e)
        {
            base.SetReportType(ReportType.KaiGuanLiangBaoJing);
            base.checkboxGroup = new CheckBox[] { base.checkbox1, base.checkbox2, base.checkbox3, base.checkbox4, base.checkbox5, base.checkbox6, base.checkbox7, base.checkbox8, base.checkbox9 };
            string[] cols = new string[] { "监测地点/名称", "报警/断电状态", "累计时间", "累计次数", "每次累计时间及起止时刻", "断电区域", "馈电状态及起止时刻|累计时刻", "安全措施|时刻", "备注" };
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
            base.Name = "ConfigPrintDBaoJing";
            base.Load += new EventHandler(this.ConfigPrintDBaoJing_Load);
            base.ResumeLayout(false);
        }
    }
}

