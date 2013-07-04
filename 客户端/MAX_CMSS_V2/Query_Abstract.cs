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

    public abstract class Query_Abstract : UserControl
    {
        private BindingSource bindingSource1;
        private Button button1;
        private Button button2;
        protected CeDianSelectorListBox ceDianSelectorListBox1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        public CheckBox[] checkBoxGroup;
        private IContainer components = null;
        private DateTimeChooser dateTimeChooser1;
        protected FlowLayoutPanel flowLayoutPanel1;
        private Logic.max_cmssDataSet max_cmssDataSet;
        protected ReportViewer reportViewer1;
        private SaveFileDialog saveFileDialog1;
        protected TabControl tabControl1;
        protected TabPage tabPage1;
        protected TabPage tabPage2;
        protected TabPage tabPage3;

        public Query_Abstract()
        {
            this.InitializeComponent();
        }

        public bool ArguCheck()
        {
            if (this.StartTime >= this.EndTime)
            {
                MessageBox.Show("你所选择的开始时间大于等于结束时间，请重新选择！");
                return false;
            }
            if (this.ceDianSelectorListBox1.GetSelectedCeDian().Length == 0)
            {
                MessageBox.Show("请选择查询的测点！");
                return false;
            }
            return true;
        }

        public abstract void button1_Click(object sender, EventArgs e);
        public void button1_Click2(object sender, EventArgs e)
        {
            if (this.ArguCheck())
            {
                this.button1_Click(sender, e);
            }
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.setAllCheckBox(true);
                this.checkBox2.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked)
            {
                this.setAllCheckBox(false);
                this.checkBox1.Checked = false;
            }
        }

        public void createCheckBoxGroup(string[] names)
        {
            this.checkBoxGroup = new CheckBox[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                this.checkBoxGroup[i] = new CheckBox();
                this.checkBoxGroup[i].Width = 120;
                if (names[i].Length > 12)
                {
                    this.checkBoxGroup[i].Width = 150;
                }
                this.checkBoxGroup[i].Height = 40;
                this.checkBoxGroup[i].Text = names[i];
                this.flowLayoutPanel1.Controls.Add(this.checkBoxGroup[i]);
            }
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
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.flowLayoutPanel1 = new FlowLayoutPanel();
            this.checkBox2 = new CheckBox();
            this.checkBox1 = new CheckBox();
            this.tabPage3 = new TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.button1 = new Button();
            this.button2 = new Button();
            this.saveFileDialog1 = new SaveFileDialog();
            this.ceDianSelectorListBox1 = new CeDianSelectorListBox();
            this.dateTimeChooser1 = new DateTimeChooser();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x422, 0x1df);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Controls.Add(this.ceDianSelectorListBox1);
            this.tabPage1.Controls.Add(this.dateTimeChooser1);
            this.tabPage1.Location = new Point(4, 0x16);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x41a, 0x1c5);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "查询条件";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new EventHandler(this.tabPage1_Click);
            this.tabPage2.Controls.Add(this.flowLayoutPanel1);
            this.tabPage2.Controls.Add(this.checkBox2);
            this.tabPage2.Controls.Add(this.checkBox1);
            this.tabPage2.Location = new Point(4, 0x16);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x41a, 0x1c5);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "输出列控制";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.flowLayoutPanel1.Location = new Point(6, 0x1c);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new Size(0x40e, 0x7e);
            this.flowLayoutPanel1.TabIndex = 4;
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new Point(0x65, 6);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new Size(60, 0x10);
            this.checkBox2.TabIndex = 3;
            this.checkBox2.Text = "全不选";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(0x10, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x30, 0x10);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "全选";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            this.tabPage3.Controls.Add(this.reportViewer1);
            this.tabPage3.Location = new Point(4, 0x16);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new Padding(3);
            this.tabPage3.Size = new Size(0x41a, 0x1c5);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "报表";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.reportViewer1.Dock = DockStyle.Fill;
            this.reportViewer1.Location = new Point(3, 3);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new Size(0x414, 0x1bf);
            this.reportViewer1.TabIndex = 0;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.Location = new Point(0x192, 0x1eb);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x45, 0x17);
            this.button1.TabIndex = 6;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click2);
            this.button2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button2.Location = new Point(0x1eb, 0x1eb);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x66, 0x17);
            this.button2.TabIndex = 7;
            this.button2.Text = "导出为PDF";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.ceDianSelectorListBox1.Location = new Point(0x65, 0x6b);
            this.ceDianSelectorListBox1.Name = "ceDianSelectorListBox1";
            this.ceDianSelectorListBox1.Size = new Size(0x32b, 0x134);
            this.ceDianSelectorListBox1.TabIndex = 10;
            DateTime now = DateTime.Now;
            this.dateTimeChooser1.EndTime = now;
            this.dateTimeChooser1.Location = new Point(0x95, 0x31);
            this.dateTimeChooser1.Name = "dateTimeChooser1";
            this.dateTimeChooser1.Size = new Size(0x29b, 0x22);
            this.dateTimeChooser1.StartTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0);
            this.dateTimeChooser1.TabIndex = 9;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.tabControl1);
            base.Name = "Query_Abstract";
            base.Size = new Size(0x428, 0x210);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            base.ResumeLayout(false);
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

        public void setAllCheckBox(bool state)
        {
            foreach (CheckBox box in this.checkBoxGroup)
            {
                if (box.Enabled)
                {
                    box.Checked = state;
                }
            }
        }

        public void SetReportViewParam(string[] paramName, ReportType type)
        {
            List<ReportParameter> list = new List<ReportParameter>();
            ReportParameter param1 = new ReportParameter("startTime", this.dateTimeChooser1.StartTime.ToString());
            list.Add(param1);
            ReportParameter param2 = new ReportParameter("endTime", this.dateTimeChooser1.EndTime.ToString());
            list.Add(param2);
            ReportConfig config = ReportConfig.getConfigByName((int) type);
            ReportParameter param3 = new ReportParameter("BiaoTou", config.ReportHeader);
            list.Add(param3);
            ReportParameter param4 = new ReportParameter("QianMing", config.QianMing);
            list.Add(param4);
            for (int i = 0; i < paramName.Length; i++)
            {
                ReportParameter param = new ReportParameter(paramName[i], Convert.ToString(!this.checkBoxGroup[i].Checked));
                list.Add(param);
            }
            this.reportViewer1.LocalReport.SetParameters(list.ToArray());
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        public DateTime EndTime
        {
            get
            {
                return this.dateTimeChooser1.EndTime;
            }
        }

        public string[] SelectedCeDian
        {
            get
            {
                return this.ceDianSelectorListBox1.GetSelectedCeDian();
            }
        }

        public DateTime StartTime
        {
            get
            {
                return this.dateTimeChooser1.StartTime;
            }
        }
    }
}

