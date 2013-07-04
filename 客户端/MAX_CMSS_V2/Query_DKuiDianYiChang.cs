namespace MAX_CMSS_V2
{
    using Logic;
    using Microsoft.Reporting.WinForms;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Query_DKuiDianYiChang : Query_Abstract
    {
        private IContainer components = null;

        public Query_DKuiDianYiChang()
        {
            this.InitializeComponent();
            string[] cols = new string[] { "地点/名称", "断电区域", "累计次数", "累计时间", "每次馈电状态|每次累计时间及起止时刻", "措施及时刻", "备注" };
            base.createCheckBoxGroup(cols);
            base.setAllCheckBox(true);
            base.checkBoxGroup[0].Enabled = false;
            base.checkBoxGroup[6].Enabled = false;
            base.ceDianSelectorListBox1.Selector.setCeDianLeiXing(1);
            base.InitReportView("MAX_CMSS_V2.KaiGuanLiangKuiDian.rdlc", "DKuiDian", "max_cmssDataSet_DKuiDian");
            string[] paramName = new string[] { "DiDian", "DuanDianQuYu", "LeiJiCiShu", "LeiJiShiJian", "KuiDianPerTime", "CuoShi", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.KaiGuanLiangKuiDian);
            base.reportViewer1.LocalReport.Refresh();
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            string[] paramName = new string[] { "DiDian", "DuanDianQuYu", "LeiJiCiShu", "LeiJiShiJian", "KuiDianPerTime", "CuoShi", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.KaiGuanLiangKuiDian);
            max_cmssDataSet.DKuiDianDataTable dt = ReportDataSuply.QueryDKuiDianData(base.StartTime, base.EndTime, base.SelectedCeDian);
            base.reportViewer1.LocalReport.DataSources.Clear();
            base.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("max_cmssDataSet_DKuiDian", dt));
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
            base.Name = "Query_DKuiDianYiChang";
            base.Size = new Size(0x3ea, 0x1a8);
            base.ResumeLayout(false);
        }
    }
}

