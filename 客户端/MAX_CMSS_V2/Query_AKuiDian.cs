namespace MAX_CMSS_V2
{
    using Logic;
    using Microsoft.Reporting.WinForms;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class Query_AKuiDian : Query_Abstract
    {
        private IContainer components = null;

        public Query_AKuiDian()
        {
            this.InitializeComponent();
            string[] cols = new string[] { "地点/名称", "断电区域", "累计次数", "异常馈电累计", "每次馈电状态累计时间及起止时刻", "措施|时刻", "备注" };
            base.createCheckBoxGroup(cols);
            base.setAllCheckBox(true);
            base.checkBoxGroup[0].Enabled = false;
            base.checkBoxGroup[6].Enabled = false;
            base.ceDianSelectorListBox1.Selector.setCeDianLeiXing(0);
            base.InitReportView("MAX_CMSS_V2.MoNiLiangKuiDian.rdlc", "AKuiDian", "max_cmssDataSet_AKuiDian");
            string[] paramName = new string[] { "DiDian", "DuanDianQuYu", "LeiJiCiShu", "KuiDianYiChang", "KuiDianPerTime", "CuoShi", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.MoNiLiangKuDian);
            base.reportViewer1.LocalReport.Refresh();
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            string[] paramName = { "DiDian", "DuanDianQuYu", "LeiJiCiShu", "KuiDianYiChang", "KuiDianPerTime", "CuoShi", "BeiZhu" };
            this.SetReportViewParam(paramName, Logic.ReportType.MoNiLiangKuDian);



            max_cmssDataSet.AKuiDianDataTable dt = ReportDataSuply.QueryAKuiDianData(StartTime, EndTime, SelectedCeDian);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("max_cmssDataSet_AKuiDian", dt));
            this.reportViewer1.RefreshReport();

            this.tabControl1.SelectedIndex = 2;
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

