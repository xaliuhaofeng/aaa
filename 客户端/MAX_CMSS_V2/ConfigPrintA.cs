namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ConfigPrintA : ConfigPrintAbstract
    {
        private IContainer components = null;

        public ConfigPrintA()
        {
            this.InitializeComponent();
        }

        private void ConfigPrintA_Load(object sender, EventArgs e)
        {
            base.SetReportType(ReportType.MoNiLiangRiBaoBiao);
            base.checkboxGroup = new CheckBox[] { base.checkbox1, base.checkbox2, base.checkbox3, base.checkbox4, base.checkbox5, base.checkbox6, base.checkbox7, base.checkbox8, base.checkbox9, base.checkbox10, base.checkbox11, base.checkbox12, base.checkbox13, base.checkbox14 };
            string[] cols = new string[] { "监测地点/名称", "单位", "报警门限", "断电门限", "复电门限", "平均值", "最大值及时刻", "报警次数", "累计报警", "断电次数", "累计断电", "馈电异常次数", "异常累计", "备注" };
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
            base.AutoScaleDimensions = new SizeF(8f, 15f);
            base.Margin = new Padding(5, 5, 5, 5);
            base.Name = "ConfigPrintA";
            base.Load += new EventHandler(this.ConfigPrintA_Load);
            base.ResumeLayout(false);
        }
    }
}

