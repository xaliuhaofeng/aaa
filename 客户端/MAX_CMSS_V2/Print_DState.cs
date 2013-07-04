namespace MAX_CMSS_V2
{
    using Logic;
    using Microsoft.Reporting.WinForms;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class Print_DState : UserControl
    {
        private Button button1;
        private Button button2;
        private IContainer components = null;
        private DateTimePicker dateTimePicker1;
        private BindingSource DStateBindingSource;
        private Label label1;
        private Logic.max_cmssDataSet max_cmssDataSet;
        private ReportViewer reportViewer1;
        private SaveFileDialog saveFileDialog1;

        public Print_DState()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Warning[] Warnings;
            string[] strStreamIds;
            string strMimeType;
            string strEncoding;
            string strFileNameExtension;
            byte[] bytes = this.reportViewer1.LocalReport.Render("Pdf", null, out strMimeType, out strEncoding, out strFileNameExtension, out strStreamIds, out Warnings);
            string strFilePath = string.Empty;
            this.saveFileDialog1.Filter = "PDF文件|*.pdf";
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                strFilePath = this.saveFileDialog1.FileName;
                try
                {
                    using (FileStream fs = new FileStream(strFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("文件被占用！");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime date = this.dateTimePicker1.Value;
            this.setReportDataSourceAndParam(date);
            this.reportViewer1.RefreshReport();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
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
            ReportDataSource reportDataSource1 = new ReportDataSource();
            this.DStateBindingSource = new BindingSource(this.components);
            this.max_cmssDataSet = new Logic.max_cmssDataSet();
            this.label1 = new Label();
            this.dateTimePicker1 = new DateTimePicker();
            this.reportViewer1 = new ReportViewer();
            this.button1 = new Button();
            this.saveFileDialog1 = new SaveFileDialog();
            this.button2 = new Button();
            ((ISupportInitialize) this.DStateBindingSource).BeginInit();
            this.max_cmssDataSet.BeginInit();
            base.SuspendLayout();
            this.DStateBindingSource.DataMember = "DState";
            this.DStateBindingSource.DataSource = this.max_cmssDataSet;
            this.max_cmssDataSet.DataSetName = "max_cmssDataSet";
            this.max_cmssDataSet.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0xd3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x61, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "报表打印日期";
            this.dateTimePicker1.Location = new Point(0x13a, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(200, 0x15);
            this.dateTimePicker1.TabIndex = 3;
            this.dateTimePicker1.ValueChanged += new EventHandler(this.dateTimePicker1_ValueChanged);
            this.reportViewer1.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
            reportDataSource1.Name = "max_cmssDataSet_DState";
            reportDataSource1.Value = this.DStateBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "MAX_CMSS_V2.KaiGuangLiangZhangTai.rdlc";
            this.reportViewer1.Location = new Point(3, 30);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new Size(0x3e7, 0x1d7);
            this.reportViewer1.TabIndex = 5;
            this.reportViewer1.Load += new EventHandler(this.reportViewer1_Load_1);
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(0x278, 2);
            this.button1.Name = "button1";
            this.button1.Size = new Size(120, 0x17);
            this.button1.TabIndex = 6;
            this.button1.Text = "导出为PDF";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
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
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.reportViewer1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.dateTimePicker1);
            base.Name = "Print_DState";
            base.Size = new Size(0x3ea, 0x1f9);
            ((ISupportInitialize) this.DStateBindingSource).EndInit();
            this.max_cmssDataSet.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void reportViewer1_Load_1(object sender, EventArgs e)
        {
            DateTime date = DateTime.Today;
            this.reportViewer1.ShowExportButton = false;
            this.setReportParam(date);
            this.reportViewer1.RefreshReport();
        }

        private void setReportDataSourceAndParam(DateTime date)
        {
            List<ReportParameter> list = new List<ReportParameter>();
            ReportParameter param1 = new ReportParameter("startTime", date.ToString("yyyy年MM月dd日") + " 00:00:00");
            list.Add(param1);
            ReportParameter param2 = new ReportParameter("endTime", date.ToString("yyyy年MM月dd日") + " 23:59:59");
            list.Add(param2);
            this.reportViewer1.LocalReport.SetParameters(list.ToArray());
            Logic.max_cmssDataSet.DStateDataTable dt = ReportDataSuply.GetDStateData(date);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("max_cmssDataSet_DState", dt));
        }

        private void setReportParam(DateTime date)
        {
            List<ReportParameter> list = new List<ReportParameter>();
            ReportParameter param1 = new ReportParameter("startTime", date.ToString("yyyy年MM月dd日") + " 00:00:00");
            list.Add(param1);
            ReportParameter param2 = new ReportParameter("endTime", date.ToString("yyyy年MM月dd日") + " 23:59:59");
            list.Add(param2);
            ReportConfig config = ReportConfig.getConfigByName(7);
            ReportParameter param3 = new ReportParameter("BiaoTou", config.ReportHeader);
            list.Add(param3);
            ReportParameter param4 = new ReportParameter("QianMing", config.QianMing);
            list.Add(param4);
            string[] paramName = new string[] { "DiDian", "LeiJiShiJian", "LeiJiBianDong", "ZhuangTaiBianDong", "BeiZhu" };
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

