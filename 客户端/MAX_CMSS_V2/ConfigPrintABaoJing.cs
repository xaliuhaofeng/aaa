namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ConfigPrintABaoJing : ConfigPrintAbstract
    {
        private IContainer components = null;

        public ConfigPrintABaoJing()
        {
            this.InitializeComponent();
        }

        private void ConfigPrintABaoJing_Load(object sender, EventArgs e)
        {
            base.SetReportType(ReportType.MoNiLiangBaoJing);
            base.checkboxGroup = new CheckBox[] { base.checkbox1, base.checkbox2, base.checkbox3, base.checkbox4, base.checkbox5, base.checkbox6, base.checkbox7, base.checkbox8, base.checkbox9, base.checkbox10, base.checkbox11, base.checkbox12 };
            string[] cols = new string[] { "监测地点/名称", "单位", "报警门限", "报警次数", "累计报警", "最大值及时刻", "平均值", "每次报警时刻及解除报警时刻", "每次报警时间", "每次报警期间平均值和最大值及时刻", "措施及时刻", "备注" };
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
            base.Name = "ConfigPrintABaoJing";
            base.Load += new EventHandler(this.ConfigPrintABaoJing_Load);
            base.ResumeLayout(false);
        }
    }
}

