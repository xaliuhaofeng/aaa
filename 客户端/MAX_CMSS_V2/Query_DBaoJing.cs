namespace MAX_CMSS_V2
{
    using Logic;
    using Microsoft.Reporting.WinForms;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Query_DBaoJing : Query_Abstract
    {
        private IContainer components = null;

        public Query_DBaoJing()
        {
            this.InitializeComponent();
            string[] cols = new string[] { "地点/名称", "报警/断电状态", "累计时间", "累计次数", "每次累计时间及起止时刻", "断电区域", "馈电状态及起止时刻|累计时刻", "安全措施|时刻", "备注" };
            base.createCheckBoxGroup(cols);
            base.setAllCheckBox(true);
            base.checkBoxGroup[0].Enabled = false;
            base.checkBoxGroup[8].Enabled = false;
            base.ceDianSelectorListBox1.Selector.setCeDianLeiXing(1);
            base.InitReportView("MAX_CMSS_V2.KaiGuanLiangBaoJing.rdlc", "DBaoJing", "max_cmssDataSet_DBaoJing");
            string[] paramName = new string[] { "DiDian", "BaoJingZhuangTai", "LeiJiShiJian", "LeiJiCiShu", "ShiJianPerTime", "DuanDianQuYu", "KuiDianZhuangTai", "AnQuanCuoShi", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.KaiGuanLiangBaoJing);
            base.reportViewer1.LocalReport.Refresh();
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            string[] paramName = new string[] { "DiDian", "BaoJingZhuangTai", "LeiJiShiJian", "LeiJiCiShu", "ShiJianPerTime", "DuanDianQuYu", "KuiDianZhuangTai", "AnQuanCuoShi", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.KaiGuanLiangBaoJing);
            max_cmssDataSet.DBaoJingDataTable dt = ReportDataSuply.QueryDBaoJingData(base.StartTime, base.EndTime, base.SelectedCeDian);
            base.reportViewer1.LocalReport.DataSources.Clear();
            base.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("max_cmssDataSet_DBaoJing", dt));
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
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Name = "Query_DBaoJing";
            base.Size = new Size(0x2ad, 0x198);
            base.ResumeLayout(false);
        }
    }
}

