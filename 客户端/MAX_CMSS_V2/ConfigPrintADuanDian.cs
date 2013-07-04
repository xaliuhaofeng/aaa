namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ConfigPrintADuanDian : ConfigPrintAbstract
    {
        private IContainer components = null;

        public ConfigPrintADuanDian()
        {
            this.InitializeComponent();
        }

        private void ConfigPrintADuanDian_Load(object sender, EventArgs e)
        {
            base.SetReportType(ReportType.MoNiLiangDuanDian);
            base.checkboxGroup = new CheckBox[] { base.checkbox1, base.checkbox2, base.checkbox3, base.checkbox4, base.checkbox5, base.checkbox6, base.checkbox7, base.checkbox8, base.checkbox9, base.checkbox10, base.checkbox11, base.checkbox12, base.checkbox13, base.checkbox14, base.checkbox15 };
            string[] cols = new string[] { "监测地点/名称", "单位", "断电门限", "复电门限", "断电区域", "断电次数", "累计断电", "最大值/时刻", "平均值", "断电时刻及复电时刻", "每次断电累计时间", "每次断电期间平均值和最大值及时刻", "断电区域|馈电状态|时刻|累计时间", "措施|时刻", "备注" };
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
            base.Name = "ConfigPrintADuanDian";
            base.Load += new EventHandler(this.ConfigPrintADuanDian_Load);
            base.ResumeLayout(false);
        }
    }
}

