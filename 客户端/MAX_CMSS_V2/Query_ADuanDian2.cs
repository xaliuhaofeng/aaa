namespace MAX_CMSS_V2
{
    using Logic;
    using Microsoft.Reporting.WinForms;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class Query_ADuanDian2 : Query_Abstract
    {
        private IContainer components = null;

        public Query_ADuanDian2()
        {
            this.InitializeComponent();
            string[] cols = new string[] { "监测地点/名称", "单位", "断电门限", "复电门限", "断电区域", "断电次数", "累计断电", "最大值/时刻", "平均值", "断电时刻及复电时刻", "每次断电累计时间", "每次断电期间平均值和最大值及时刻", "断电区域|馈电状态|时刻|累计时间", "措施|时刻", "备注" };
            base.createCheckBoxGroup(cols);
            base.setAllCheckBox(true);
            base.checkBoxGroup[0].Enabled = false;
            base.checkBoxGroup[14].Enabled = false;
            base.ceDianSelectorListBox1.Selector.setCeDianLeiXing(0);
            base.InitReportView("MAX_CMSS_V2.MoNiLiangDuanDian.rdlc", "ADuanDian", "max_cmssDataSet_ADuanDian");
            string[] paramName = new string[] { "DiDian", "DanWei", "DuanDianMenXian", "FuDianMenXian", "DuanDianQuYu", "DuanDianCiShu", "LeiJiDuanDian", "Max", "Avg", "DuanDianShiKe", "LeiJiPerTime", "avgPerTime", "QuYu", "CuoShi", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.MoNiLiangDuanDian);
            base.reportViewer1.LocalReport.Refresh();
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            string[] paramName = new string[] { "DiDian", "DanWei", "DuanDianMenXian", "FuDianMenXian", "DuanDianQuYu", "DuanDianCiShu", "LeiJiDuanDian", "Max", "Avg", "DuanDianShiKe", "LeiJiPerTime", "avgPerTime", "QuYu", "CuoShi", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.MoNiLiangDuanDian);
            max_cmssDataSet.ADuanDianDataTable dt = ReportDataSuply.QueryADuanDianData(base.StartTime, base.EndTime, base.SelectedCeDian);
            base.reportViewer1.LocalReport.DataSources.Clear();
            base.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("max_cmssDataSet_ADuanDian", dt));
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
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.Name = "Query_ADuanDian2";
            base.ResumeLayout(false);
        }
    }
}

