namespace MAX_CMSS_V2
{
    using Logic;
    using Microsoft.Reporting.WinForms;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Query_DState : Query_Abstract
    {
        private IContainer components = null;

        public Query_DState()
        {
            this.InitializeComponent();
            string[] cols = new string[] { "监测地点/名称", "累计运行时间", "累计变动次数", "状态变动状况及时刻", "备注" };
            base.createCheckBoxGroup(cols);
            base.setAllCheckBox(true);
            base.checkBoxGroup[0].Enabled = false;
            base.checkBoxGroup[4].Enabled = false;
            base.ceDianSelectorListBox1.Selector.setCeDianLeiXing(1);
            base.InitReportView("MAX_CMSS_V2.KaiGuangLiangZhangTai.rdlc", "DState", "max_cmssDataSet_DState");
            string[] paramName = new string[] { "DiDian", "LeiJiShiJian", "LeiJiBianDong", "ZhuangTaiBianDong", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.KaiGuanLiangState);
            base.reportViewer1.LocalReport.Refresh();
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            string[] paramName = new string[] { "DiDian", "LeiJiShiJian", "LeiJiBianDong", "ZhuangTaiBianDong", "BeiZhu" };
            base.SetReportViewParam(paramName, ReportType.KaiGuanLiangBaoJing);
            max_cmssDataSet.DStateDataTable dt = ReportDataSuply.QueryDStateData(base.StartTime, base.EndTime, base.SelectedCeDian);
            base.reportViewer1.LocalReport.DataSources.Clear();
            base.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("max_cmssDataSet_DState", dt));
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
            base.Name = "Query_DState";
            base.Size = new Size(0x3a1, 0x14f);
            base.ResumeLayout(false);
        }
    }
}

