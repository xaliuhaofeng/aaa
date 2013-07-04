namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ConfigPrintDState : ConfigPrintAbstract
    {
        private IContainer components = null;

        public ConfigPrintDState()
        {
            this.InitializeComponent();
        }

        private void ConfigPrintDState_Load(object sender, EventArgs e)
        {
            base.SetReportType(ReportType.KaiGuanLiangState);
            base.checkboxGroup = new CheckBox[] { base.checkbox1, base.checkbox2, base.checkbox3, base.checkbox4, base.checkbox5 };
            string[] cols = new string[] { "监测地点/名称", "累计运行时间", "累计变动次数", "状态变动状况及时刻", "备注" };
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
            base.Name = "ConfigPrintDState";
            base.Load += new EventHandler(this.ConfigPrintDState_Load);
            base.ResumeLayout(false);
        }
    }
}

