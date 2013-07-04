namespace MAX_CMSS_V2
{
    using Logic;
    using Microsoft.Reporting.WinForms;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Query_ABaoJing : Query_Abstract
    {
        private IContainer components = null;

        public Query_ABaoJing()
        {
            this.InitializeComponent();
            string[] cols = new string[] { "监测地点/名称", "报警次数", "报警起止时间", "累计报警时间", "报警最大值及对应时刻", "断电次数", "断电起止时间", "累计断电时间", "断电期间平均值、最大值及时刻", "馈电次数", "馈电起止时间", "累计馈电时间", "措施/时刻", "备注" };
            base.createCheckBoxGroup(cols);
            base.setAllCheckBox(true);
            base.checkBoxGroup[0].Enabled = false;
            base.checkBoxGroup[11].Enabled = false;
            base.ceDianSelectorListBox1.Selector.setCeDianLeiXing(0);
            base.InitReportView("MAX_CMSS_V2.MoNiLiangBaoJing.rdlc", "ABaoJing", "max_cmssDataSet_ABaoJing");
            string[] paramName = new string[] { "DanWei", "BaoJingMenXian", "BaoJingCiShu", "LeiJiBaoJing", "Max", "Avg", "BaoJingPerTime", "DuraPerTime", "avgPerTime", "CuoShi", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.MoNiLiangBaoJing);
            base.reportViewer1.LocalReport.Refresh();
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            string[] paramName = new string[] { "DanWei", "BaoJingMenXian", "BaoJingCiShu", "LeiJiBaoJing", "Max", "Avg", "BaoJingPerTime", "DuraPerTime", "avgPerTime", "CuoShi", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.MoNiLiangDuanDian);
            max_cmssDataSet.ABaoJingDataTable dt = ReportDataSuply.QueryABaoJingData(base.StartTime, base.EndTime, base.SelectedCeDian);
            base.reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            base.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("max_cmssDataSet_ABaoJing", dt));
            base.reportViewer1.RefreshReport();
            base.tabControl1.SelectedIndex = 2;
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
            this.components = new Container();
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Name = "Query_ABaoJing";
            base.Size = new Size(0x301, 0x167);
            base.ResumeLayout(false);
        }
    }
}

