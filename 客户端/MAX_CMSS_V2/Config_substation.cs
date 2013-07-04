namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Config_substation : UserControl
    {
        private IContainer components = null;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private DataGridView dataGridView3;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;

        public Config_substation()
        {
            this.InitializeComponent();
        }

        private void Config_substation_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = FenZhan.GetCeDianTongJiByFenZhan();
            this.dataGridView2.DataSource = FenZhan.GetCeDianTongJiByTongDao();
            this.dataGridView3.DataSource = FenZhan.GetCeDianTongJiByLeiXing(FenZhan.GetAllXiaoLeiXing());
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.dataGridView1.RowHeadersWidth = 70;
            int count = this.dataGridView1.Rows.Count;
            this.dataGridView1.Rows[count - 1].HeaderCell.Value = "总计";
        }

        private void dataGridView3_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.dataGridView1 = new DataGridView();
            this.tabPage2 = new TabPage();
            this.dataGridView2 = new DataGridView();
            this.tabPage3 = new TabPage();
            this.dataGridView3 = new DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            this.tabPage2.SuspendLayout();
            ((ISupportInitialize) this.dataGridView2).BeginInit();
            this.tabPage3.SuspendLayout();
            ((ISupportInitialize) this.dataGridView3).BeginInit();
            base.SuspendLayout();
            this.tabControl1.Alignment = TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x301, 0x1a7);
            this.tabControl1.TabIndex = 1;
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new Point(0x16, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x2e7, 0x19f);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "全部统计";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Chocolate;
            dataGridViewCellStyle1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Dock = DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 0x17;
            this.dataGridView1.Size = new Size(0x2e1, 0x199);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new Point(0x16, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x2e7, 0x19f);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "类型统计";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = Color.White;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.Chocolate;
            dataGridViewCellStyle2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.ColumnHeadersHeight = 30;
            this.dataGridView2.Dock = DockStyle.Fill;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.Location = new Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 0x17;
            this.dataGridView2.Size = new Size(0x2e1, 0x199);
            this.dataGridView2.TabIndex = 1;
            this.tabPage3.Controls.Add(this.dataGridView3);
            this.tabPage3.Location = new Point(0x16, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new Size(0x2e7, 0x19f);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "分站统计";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView3.BackgroundColor = Color.White;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.Chocolate;
            dataGridViewCellStyle3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            this.dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView3.ColumnHeadersHeight = 30;
            this.dataGridView3.Dock = DockStyle.Fill;
            this.dataGridView3.EnableHeadersVisualStyles = false;
            this.dataGridView3.Location = new Point(0, 0);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.RowTemplate.Height = 0x17;
            this.dataGridView3.Size = new Size(0x2e7, 0x19f);
            this.dataGridView3.TabIndex = 0;
            this.dataGridView3.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.dataGridView3_DataBindingComplete);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.tabControl1);
            base.Name = "Config_substation";
            base.Size = new Size(0x301, 0x1a7);
            base.Load += new EventHandler(this.Config_substation_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView2).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView3).EndInit();
            base.ResumeLayout(false);
        }
    }
}

