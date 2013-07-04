namespace MAX_CMSS_V2
{
    using Logic;
    using Microsoft.Reporting.WinForms;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Print_YueBaoBiao : UserControl
    {
        private BindingSource bindingSource1;
        private Button button1;
        private Button button2;
        private IContainer components = null;
        private DateTimePicker dateTimePicker1;
        private Label label1;
        private Logic.max_cmssDataSet max_cmssDataSet;
        private ReportViewer reportViewer1;

        public Print_YueBaoBiao()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalParams.PrintReport(this.reportViewer1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime date = this.dateTimePicker1.Value;
            this.setReportDataSource(date);
            this.reportViewer1.RefreshReport();
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
            this.button2 = new Button();
            this.button1 = new Button();
            this.label1 = new Label();
            this.dateTimePicker1 = new DateTimePicker();
            this.reportViewer1 = new ReportViewer();
            base.SuspendLayout();
            this.button2.BackColor = Color.Chocolate;
            this.button2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new Point(0x20b, 2);
            this.button2.Name = "button2";
            this.button2.Size = new Size(90, 0x17);
            this.button2.TabIndex = 8;
            this.button2.Text = "生成报表";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(0x278, 2);
            this.button1.Name = "button1";
            this.button1.Size = new Size(120, 0x17);
            this.button1.TabIndex = 7;
            this.button1.Text = "导出为PDF";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0xd3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x61, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "报表打印日期";
            this.dateTimePicker1.Location = new Point(0x13a, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(200, 0x15);
            this.dateTimePicker1.TabIndex = 5;
            this.reportViewer1.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
            this.reportViewer1.Location = new Point(3, 30);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new Size(0x383, 0x1ec);
            this.reportViewer1.TabIndex = 9;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.reportViewer1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.dateTimePicker1);
            base.Name = "Print_YueBaoBiao";
            base.Size = new Size(0x389, 0x20d);
            base.Load += new EventHandler(this.Print_YueBaoBiao_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void InitReportView(string rdlcFile, string dataMember, string dataSourceName)
        {
            this.max_cmssDataSet = new Logic.max_cmssDataSet();
            this.max_cmssDataSet.DataSetName = "max_cmssDataSet";
            this.max_cmssDataSet.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = rdlcFile;
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ShowExportButton = false;
            ReportDataSource reportDataSource2 = new ReportDataSource();
            this.bindingSource1 = new BindingSource();
            this.bindingSource1.DataMember = dataMember;
            this.bindingSource1.DataSource = this.max_cmssDataSet;
            reportDataSource2.Name = dataSourceName;
            reportDataSource2.Value = this.bindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
        }

        private void Print_YueBaoBiao_Load(object sender, EventArgs e)
        {
            this.InitReportView("MAX_CMSS_V2.YueBaoBiao.rdlc", "BaoJingDuanDian", "max_cmssDataSet_BaoJingDuanDian");
            DateTime date = DateTime.Today;
            this.reportViewer1.ShowExportButton = false;
            this.setReportParam(date);
            this.reportViewer1.RefreshReport();
        }

        private void setReportDataSource(DateTime date)
        {
            List<ReportParameter> list = new List<ReportParameter>();
            DateTime startTime = new DateTime(date.Year, date.Month, 1, 0, 0, 0);
            ReportParameter param1 = new ReportParameter("startTime", startTime.ToString("yyyy年MM月dd日") + " 00:00:00");
            list.Add(param1);
            ReportParameter param2 = new ReportParameter("endTime", date.ToString("yyyy年MM月dd日") + " 23:59:59");
            list.Add(param2);
            this.reportViewer1.LocalReport.SetParameters(list.ToArray());
            Logic.max_cmssDataSet.BaoJingDuanDianDataTable dt = ReportDataSuply.GetBaoJingDuanDianData(date);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("max_cmssDataSet_BaoJingDuanDian", dt));
        }

        private void setReportParam(DateTime date)
        {
            List<ReportParameter> list = new List<ReportParameter>();
            DateTime startTime = new DateTime(date.Year, date.Month, 1, 0, 0, 0);
            ReportParameter param1 = new ReportParameter("startTime", startTime.ToString("yyyy年MM月dd日") + " 00:00:00");
            list.Add(param1);
            ReportParameter param2 = new ReportParameter("endTime", date.ToString("yyyy年MM月dd日") + " 23:59:59");
            list.Add(param2);
            ReportConfig config = ReportConfig.getConfigByName(9);
            ReportParameter param3 = new ReportParameter("BiaoTou", config.ReportHeader);
            list.Add(param3);
            ReportParameter param4 = new ReportParameter("QianMing", config.QianMing);
            list.Add(param4);
            string[] paramName = new string[] { "BaoJingCiShu", "BaoJingQiZhiShiJian", "LeiJiBaoJing", "BaoJingMax", "DuanDianCiShu", "DuanDianQiZhiShiJian", "LeiJiDuanDian", "DuanDianMax", "KuiDianCiShu", "KuiDianQiZhiShiJian", "LeiJiKuiDian", "CuoShi", "BeiZhu" };
            List<bool> colValue = config.Cols;
            if (colValue.Count < paramName.Length)
            {
                colValue = ReportConfig.GetDefaultColValue(paramName.Length);
            }
            for (int i = 0; i < paramName.Length; i++)
            {
                ReportParameter param = new ReportParameter(paramName[i], Convert.ToString(!colValue[i]));
                list.Add(param);
            }
            this.reportViewer1.LocalReport.SetParameters(list.ToArray());
        }
    }
}

