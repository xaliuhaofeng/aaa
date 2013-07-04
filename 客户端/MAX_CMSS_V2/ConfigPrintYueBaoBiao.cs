namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ConfigPrintYueBaoBiao : ConfigPrintAbstract
    {
        private IContainer components = null;

        public ConfigPrintYueBaoBiao()
        {
            this.InitializeComponent();
        }

        private void ConfigPrintYueBaoBiao_Load(object sender, EventArgs e)
        {
            base.SetReportType(ReportType.YueBaoBiao);
            base.checkboxGroup = new CheckBox[] { base.checkbox1, base.checkbox2, base.checkbox3, base.checkbox4, base.checkbox5, base.checkbox6, base.checkbox7, base.checkbox8, base.checkbox9, base.checkbox10, base.checkbox11, base.checkbox12, base.checkbox13, base.checkbox14 };
            string[] cols = new string[] { "监测地点/名称", "报警次数", "报警起止时间", "累计报警时间", "报警最大值及对应时刻", "断电次数", "断电起止时间", "累计断电时间", "断电期间平均值、最大值及时刻", "馈电次数", "馈电起止时间", "累计馈电时间", "措施/时刻", "备注" };
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
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Name = "ConfigPrintYueBaoBiao";
            base.Load += new EventHandler(this.ConfigPrintYueBaoBiao_Load);
            base.ResumeLayout(false);
        }
    }
}

