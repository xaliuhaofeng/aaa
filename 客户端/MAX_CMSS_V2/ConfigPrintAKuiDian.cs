namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ConfigPrintAKuiDian : ConfigPrintAbstract
    {
        private IContainer components = null;

        public ConfigPrintAKuiDian()
        {
            this.InitializeComponent();
        }

        private void ConfigPrintAKuiDian_Load(object sender, EventArgs e)
        {
            base.SetReportType(ReportType.MoNiLiangKuDian);
            base.checkboxGroup = new CheckBox[] { base.checkbox1, base.checkbox2, base.checkbox3, base.checkbox4, base.checkbox5, base.checkbox6, base.checkbox7 };
            string[] cols = new string[] { "监测地点/名称", "断电区域", "累计次数", "异常馈电累计", "每次馈电状态累计时间及起止时刻", "措施|时刻", "备注" };
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
            base.Name = "ConfigPrintAKuiDian";
            base.Load += new EventHandler(this.ConfigPrintAKuiDian_Load);
            base.ResumeLayout(false);
        }
    }
}

