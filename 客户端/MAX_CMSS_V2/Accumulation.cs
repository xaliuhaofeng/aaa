namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Accumulation : UserControl
    {
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private IContainer components = null;
        private DataGridView dataGridView1;

        public Accumulation()
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
            this.dataGridView1 = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            base.SuspendLayout();
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
            this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.Column1, this.Column2, this.Column3, this.Column4 });
            this.dataGridView1.Dock = DockStyle.Top;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 0x17;
            this.dataGridView1.Size = new Size(0x2fb, 0xbc);
            this.dataGridView1.TabIndex = 0;
            this.Column1.HeaderText = "图标";
            this.Column1.Name = "Column1";
            this.Column2.HeaderText = "名称";
            this.Column2.Name = "Column2";
            this.Column3.HeaderText = "单位";
            this.Column3.Name = "Column3";
            this.Column4.HeaderText = "地点";
            this.Column4.Name = "Column4";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.dataGridView1);
            base.Name = "Accumulation";
            base.Size = new Size(0x2fb, 0x1b1);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            base.ResumeLayout(false);
        }
    }
}

