namespace MAX_CMSS_V2
{
    using Logic;
    using Microsoft.Reporting.WinForms;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class Query_SheBeiGuZhang : Query_Abstract
    {
        private IContainer components = null;

        public Query_SheBeiGuZhang()
        {
            this.InitializeComponent();
            string[] cols = new string[] { "安装地点/名称", "累计次数", "累计时间", "每次累计时间及起止时刻", "状态", "安全措施|时刻", "备注" };
            base.createCheckBoxGroup(cols);
            base.setAllCheckBox(true);
            base.checkBoxGroup[0].Enabled = false;
            base.checkBoxGroup[6].Enabled = false;
            base.ceDianSelectorListBox1.Selector.setCeDianLeiXing(3);
            base.InitReportView("MAX_CMSS_V2.SheBeiGuZhang.rdlc", "SheBeiGuZhang", "max_cmssDataSet_SheBeiGuZhang");
            string[] paramName = new string[] { "DiDian", "LeiJiCiShu", "LeiJiShiJian", "ShiJianPerTime", "ZhuangTai", "AnQuanCuoShi", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.JianKongSheBeiGuZhang);
            base.reportViewer1.LocalReport.Refresh();
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            string[] paramName = new string[] { "DiDian", "LeiJiCiShu", "LeiJiShiJian", "ShiJianPerTime", "ZhuangTai", "AnQuanCuoShi", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.JianKongSheBeiGuZhang);
            max_cmssDataSet.SheBeiGuZhangDataTable dt = ReportDataSuply.QuerySheBeiGuZhangData(base.StartTime, base.EndTime, base.SelectedCeDian);
            base.reportViewer1.LocalReport.DataSources.Clear();
            base.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("max_cmssDataSet_SheBeiGuZhang", dt));
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

