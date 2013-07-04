namespace MAX_CMSS_V2
{
    using Logic;
    using Microsoft.Reporting.WinForms;
    using System;
    using System.ComponentModel;

    public class Query_ATongJiZhi : Query_Abstract
    {
        private IContainer components = null;

        public Query_ATongJiZhi()
        {
            this.InitializeComponent();
            string[] cols = new string[] { "时间间隔及每一时间间隔的起止时刻", "安装地点/名称", "单位", "报警浓度", "断电浓度", "复电浓度", "最大值及时刻|平均值", "每段时间内的平均值|最大值", "备注" };
            base.createCheckBoxGroup(cols);
            base.setAllCheckBox(true);
            base.checkBoxGroup[0].Enabled = false;
            base.checkBoxGroup[8].Enabled = false;
            base.ceDianSelectorListBox1.Selector.setCeDianLeiXing(0);
            base.InitReportView("MAX_CMSS_V2.MoNiLiangTongJiZhi.rdlc", "ATongJi", "max_cmssDataSet_ATongJiZhi");
            string[] paramName = new string[] { "ShiJianJianGe", "DiDian", "DanWei", "BaoJingNongDu", "DuanDianNongDu", "FuDianNongDu", "Max", "avgPerTime", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.MoNiLiangTongJi);
            base.reportViewer1.LocalReport.Refresh();
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            string[] paramName = new string[] { "ShiJianJianGe", "DiDian", "DanWei", "BaoJingNongDu", "DuanDianNongDu", "FuDianNongDu", "Max", "avgPerTime", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.MoNiLiangTongJi);
            max_cmssDataSet.ATongJiZhiDataTable dt = ReportDataSuply.QueryATongJiZhiData(base.StartTime, base.EndTime, base.SelectedCeDian, 5);
            base.reportViewer1.LocalReport.DataSources.Clear();
            base.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("max_cmssDataSet_ATongJiZhi", dt));
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
        }
    }
}

