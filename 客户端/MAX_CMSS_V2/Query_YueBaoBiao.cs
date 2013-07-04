namespace MAX_CMSS_V2
{
    using Logic;
    using Microsoft.Reporting.WinForms;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Windows.Forms;

    public class Query_YueBaoBiao : Query_Abstract
    {
        private IContainer components = null;

        public Query_YueBaoBiao()
        {
            this.InitializeComponent();
            string[] cols = new string[] { "监测地点/名称", "报警次数", "报警起止时间", "累计报警时间", "报警最大值及对应时刻", "断电次数", "断电起止时间", "累计断电时间", "断电期间平均值、最大值及时刻", "馈电次数", "馈电起止时间", "累计馈电时间", "措施/时刻", "备注" };
            base.createCheckBoxGroup(cols);
            base.setAllCheckBox(true);
            base.checkBoxGroup[0].Enabled = false;
            base.checkBoxGroup[13].Enabled = false;
            base.ceDianSelectorListBox1.Selector.setCeDianLeiXing(0);
            base.InitReportView("MAX_CMSS_V2.YueBaoBiao.rdlc", "BaoJingDuanDian", "max_cmssDataSet_BaoJingDuanDian");
            string[] paramName = new string[] { "BaoJingCiShu", "BaoJingQiZhiShiJian", "LeiJiBaoJing", "BaoJingMax", "DuanDianCiShu", "DuanDianQiZhiShiJian", "LeiJiDuanDian", "DuanDianMax", "KuiDianCiShu", "KuiDianQiZhiShiJian", "LeiJiKuiDian", "CuoShi", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.YueBaoBiao);
            base.reportViewer1.LocalReport.Refresh();
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            string[] paramName = new string[] { "BaoJingCiShu", "BaoJingQiZhiShiJian", "LeiJiBaoJing", "BaoJingMax", "DuanDianCiShu", "DuanDianQiZhiShiJian", "LeiJiDuanDian", "DuanDianMax", "KuiDianCiShu", "KuiDianQiZhiShiJian", "LeiJiKuiDian", "CuoShi", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.YueBaoBiao);
            DataTable cedianDt = CeDian.GetCeDianInfoByBianHaos(base.SelectedCeDian);
            long[] cedianIds = new long[cedianDt.Rows.Count];
            for (int i = 0; i < cedianDt.Rows.Count; i++)
            {
                cedianIds[i] = (long) cedianDt.Rows[i]["id"];
            }
            max_cmssDataSet.BaoJingDuanDianDataTable dt = ReportDataSuply.QueryBaoJingDuanDianData(base.StartTime, base.EndTime, base.SelectedCeDian, cedianIds);
            base.reportViewer1.LocalReport.DataSources.Clear();
            base.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("max_cmssDataSet_BaoJingDuanDian", dt));
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
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }
    }
}

