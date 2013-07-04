namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class QueryMultiAlarm : UserControl
    {
        private Button button1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private DateTimeChooser dateTimeChooser1;

        public QueryMultiAlarm()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime start = this.dateTimeChooser1.StartTime;
            DateTime end = this.dateTimeChooser1.EndTime;
            if (end < start)
            {
                MessageBox.Show("开始时间应小于结束时间。");
            }
            else
            {
                DataTable dt = OperateDB.Select(string.Concat(new object[] { "select * from MultiAlarm where startTime > '", start, "' and startTime < '", end, "'" }));
                this.dataGridView1.Rows.Clear();
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        int index = this.dataGridView1.Rows.Add();
                        this.dataGridView1.Rows[index].Cells[0].Value = row["startTime"].ToString();
                        this.dataGridView1.Rows[index].Cells[1].Value = row["endTime"].ToString();
                        this.dataGridView1.Rows[index].Cells[2].Value = row["alarmContent"].ToString();
                    }
                }
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
            this.button1 = new Button();
            this.dataGridView1 = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.dateTimeChooser1 = new DateTimeChooser();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            base.SuspendLayout();
            this.button1.Location = new Point(0x2b6, 20);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 1;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.Column1, this.Column2, this.Column3 });
            this.dataGridView1.Location = new Point(3, 0x31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 0x17;
            this.dataGridView1.Size = new Size(0x31e, 0x164);
            this.dataGridView1.TabIndex = 2;
            this.Column1.HeaderText = "开始时间";
            this.Column1.Name = "Column1";
            this.Column2.HeaderText = "结束时间";
            this.Column2.Name = "Column2";
            this.Column3.HeaderText = "报警内容";
            this.Column3.Name = "Column3";
            DateTime now = DateTime.Now;
            this.dateTimeChooser1.EndTime = now;
            this.dateTimeChooser1.Location = new Point(0x1a, 13);
            this.dateTimeChooser1.Name = "dateTimeChooser1";
            this.dateTimeChooser1.Size = new Size(0x296, 30);
            this.dateTimeChooser1.StartTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0);
            this.dateTimeChooser1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            base.Controls.Add(this.dataGridView1);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.dateTimeChooser1);
            base.Name = "QueryMultiAlarm";
            base.Size = new Size(0x324, 0x198);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            base.ResumeLayout(false);
        }
    }
}

