namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ConfigPrintATongJi : ConfigPrintAbstract
    {
        private IContainer components = null;

        public ConfigPrintATongJi()
        {
            this.InitializeComponent();
        }

        private void ConfigPrintATongJi_Load(object sender, EventArgs e)
        {
            base.SetReportType(ReportType.MoNiLiangTongJi);
            base.checkboxGroup = new CheckBox[] { base.checkbox1, base.checkbox2, base.checkbox3, base.checkbox4, base.checkbox5, base.checkbox6, base.checkbox7, base.checkbox8, base.checkbox9 };
            string[] cols = new string[] { "时间间隔及每一时间间隔的起止时刻", "安装地点/名称", "单位", "报警浓度", "断电浓度", "复电浓度", "最大值及时刻|平均值", "每段时间内的平均值|最大值", "备注" };
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
            base.Name = "ConfigPrintATongJi";
            base.Load += new EventHandler(this.ConfigPrintATongJi_Load);
            base.ResumeLayout(false);
        }
    }
}

