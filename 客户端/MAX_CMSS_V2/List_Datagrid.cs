namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class List_Datagrid : UserControl
    {
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column14;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column16;
        private DataGridViewTextBoxColumn Column17;
        private DataGridViewTextBoxColumn Column18;
        private DataGridViewTextBoxColumn Column19;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column20;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private IContainer components = null;
        private GridView gridView1;

        public List_Datagrid()
        {
            this.InitializeComponent();
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
            this.gridView1 = new GridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.Column10 = new DataGridViewTextBoxColumn();
            this.Column11 = new DataGridViewTextBoxColumn();
            this.Column12 = new DataGridViewTextBoxColumn();
            this.Column13 = new DataGridViewTextBoxColumn();
            this.Column14 = new DataGridViewTextBoxColumn();
            this.Column15 = new DataGridViewTextBoxColumn();
            this.Column16 = new DataGridViewTextBoxColumn();
            this.Column17 = new DataGridViewTextBoxColumn();
            this.Column18 = new DataGridViewTextBoxColumn();
            this.Column19 = new DataGridViewTextBoxColumn();
            this.Column20 = new DataGridViewTextBoxColumn();
            ((ISupportInitialize) this.gridView1).BeginInit();
            base.SuspendLayout();
            this.gridView1.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
            this.gridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridView1.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Chocolate;
            dataGridViewCellStyle1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            this.gridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridView1.ColumnHeadersHeight = 30;
            this.gridView1.Columns.AddRange(new DataGridViewColumn[] { 
                this.Column1, this.Column2, this.Column3, this.Column4, this.Column5, this.Column6, this.Column7, this.Column8, this.Column9, this.Column10, this.Column11, this.Column12, this.Column13, this.Column14, this.Column15, this.Column16, 
                this.Column17, this.Column18, this.Column19, this.Column20
             });
            this.gridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.gridView1.EnableHeadersVisualStyles = false;
            this.gridView1.Location = new Point(0, 0);
            this.gridView1.Name = "gridView1";
            this.gridView1.RowHeadersVisible = false;
            this.gridView1.RowTemplate.Height = 0x17;
            this.gridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridView1.Size = new Size(0x39e, 0x120);
            this.gridView1.TabIndex = 0;
            this.gridView1.UseMouseOverStyle = true;
            this.Column1.HeaderText = "图标";
            this.Column1.Name = "Column1";
            this.Column2.HeaderText = "地点/名称";
            this.Column2.Name = "Column2";
            this.Column3.HeaderText = "值/状态";
            this.Column3.Name = "Column3";
            this.Column4.HeaderText = "断点区域";
            this.Column4.Name = "Column4";
            this.Column5.HeaderText = "测点编号";
            this.Column5.Name = "Column5";
            this.Column6.HeaderText = "报警上限";
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            this.Column7.HeaderText = "报警下限";
            this.Column7.Name = "Column7";
            this.Column7.Visible = false;
            this.Column8.HeaderText = "量程高值";
            this.Column8.Name = "Column8";
            this.Column8.Visible = false;
            this.Column9.HeaderText = "量程低值";
            this.Column9.Name = "Column9";
            this.Column9.Visible = false;
            this.Column10.HeaderText = "断电值";
            this.Column10.Name = "Column10";
            this.Column10.Visible = false;
            this.Column11.HeaderText = "复电值";
            this.Column11.Name = "Column11";
            this.Column11.Visible = false;
            this.Column12.HeaderText = "断电时刻";
            this.Column12.Name = "Column12";
            this.Column12.Visible = false;
            this.Column13.HeaderText = "复电时刻";
            this.Column13.Name = "Column13";
            this.Column13.Visible = false;
            this.Column14.HeaderText = "报警时刻";
            this.Column14.Name = "Column14";
            this.Column14.Visible = false;
            this.Column15.HeaderText = "单位";
            this.Column15.Name = "Column15";
            this.Column15.Visible = false;
            this.Column16.HeaderText = "馈电状态及时刻";
            this.Column16.Name = "Column16";
            this.Column16.Visible = false;
            this.Column17.HeaderText = "最大值";
            this.Column17.Name = "Column17";
            this.Column17.Visible = false;
            this.Column18.HeaderText = "最小值";
            this.Column18.Name = "Column18";
            this.Column18.Visible = false;
            this.Column19.HeaderText = "平均值";
            this.Column19.Name = "Column19";
            this.Column19.Visible = false;
            this.Column20.HeaderText = "断电范围";
            this.Column20.Name = "Column20";
            this.Column20.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.gridView1);
            base.Name = "List_Datagrid";
            base.Size = new Size(0x39e, 0x120);
            ((ISupportInitialize) this.gridView1).EndInit();
            base.ResumeLayout(false);
        }
    }
}

