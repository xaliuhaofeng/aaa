namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Query_SystemRunLog : UserControl
    {
        private Button button1;
        private ComboBox cbx_type;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private DateTimeChooser dateTimeChooser1;
        private DataGridViewTextBoxColumn eventName;
        private GroupBox group_search;
        private Label label1;
        private DataGridViewTextBoxColumn logTime;
        private Panel panel1;
        private Panel panel2;
        private PrintButton printButton1;

        public Query_SystemRunLog()
        {
            this.InitializeComponent();
            this.cbx_type.Items.Add("全部");
            this.cbx_type.Items.Add("系统开启");
            this.cbx_type.Items.Add("系统关闭");
            this.cbx_type.Items.Add("用户切换");
            this.cbx_type.Items.Add("双机切换");
            this.cbx_type.SelectedIndex = -1;
            this.loadData();
            this.printButton1.DataGridView1 = this.dataGridView1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.loadData();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.eventName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.group_search = new System.Windows.Forms.GroupBox();
            this.printButton1 = new MAX_CMSS_V2.PrintButton();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimeChooser1 = new MAX_CMSS_V2.DateTimeChooser();
            this.cbx_type = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.group_search.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(0, 80);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(958, 520);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.eventName,
            this.logTime});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(958, 520);
            this.dataGridView1.TabIndex = 4;
            // 
            // eventName
            // 
            this.eventName.HeaderText = "运行事件";
            this.eventName.Name = "eventName";
            this.eventName.ReadOnly = true;
            // 
            // logTime
            // 
            this.logTime.HeaderText = "时间";
            this.logTime.Name = "logTime";
            this.logTime.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.group_search);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(961, 80);
            this.panel2.TabIndex = 1;
            // 
            // group_search
            // 
            this.group_search.Controls.Add(this.printButton1);
            this.group_search.Controls.Add(this.button1);
            this.group_search.Controls.Add(this.dateTimeChooser1);
            this.group_search.Controls.Add(this.cbx_type);
            this.group_search.Controls.Add(this.label1);
            this.group_search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.group_search.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.group_search.Location = new System.Drawing.Point(0, 0);
            this.group_search.Margin = new System.Windows.Forms.Padding(2);
            this.group_search.Name = "group_search";
            this.group_search.Padding = new System.Windows.Forms.Padding(2);
            this.group_search.Size = new System.Drawing.Size(961, 80);
            this.group_search.TabIndex = 0;
            this.group_search.TabStop = false;
            this.group_search.Text = "查询";
            // 
            // printButton1
            // 
            this.printButton1.Footer = "页脚";
            this.printButton1.Location = new System.Drawing.Point(408, 49);
            this.printButton1.Name = "printButton1";
            this.printButton1.Size = new System.Drawing.Size(81, 28);
            this.printButton1.SubTitle = "日志查询打印";
            this.printButton1.TabIndex = 12;
            this.printButton1.Title = "DataGridView打印";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(301, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimeChooser1
            // 
            this.dateTimeChooser1.EndTime =DateTime.Now;
            this.dateTimeChooser1.Location = new System.Drawing.Point(7, 18);
            this.dateTimeChooser1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimeChooser1.Name = "dateTimeChooser1";
            this.dateTimeChooser1.Size = new System.Drawing.Size(948, 34);
            DateTime dt = DateTime.Now;
            this.dateTimeChooser1.StartTime = new DateTime(dt.Year,dt.Month,dt.Day,0,0,0);
            this.dateTimeChooser1.TabIndex = 10;
            // 
            // cbx_type
            // 
            this.cbx_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_type.FormattingEnabled = true;
            this.cbx_type.Location = new System.Drawing.Point(82, 56);
            this.cbx_type.Margin = new System.Windows.Forms.Padding(2);
            this.cbx_type.Name = "cbx_type";
            this.cbx_type.Size = new System.Drawing.Size(162, 24);
            this.cbx_type.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(9, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "日志类型";
            // 
            // Query_SystemRunLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Query_SystemRunLog";
            this.Size = new System.Drawing.Size(961, 600);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.group_search.ResumeLayout(false);
            this.group_search.PerformLayout();
            this.ResumeLayout(false);

        }

        private void loadData()
        {
            DateTime start = this.dateTimeChooser1.StartTime;
            DateTime end = this.dateTimeChooser1.EndTime;
            LogType[] types = null;
            if (this.cbx_type.SelectedIndex == 0)
            {
                types = new LogType[] { LogType.SystemOpen, LogType.SystemClose, LogType.SystemUserChange, LogType.SystemChange };
            }
            else
            {
                types = new LogType[1];
                if (this.cbx_type.Text == "系统开启")
                {
                    types[0] = LogType.SystemOpen;
                }
                else if (this.cbx_type.Text == "系统关闭")
                {
                    types[0] = LogType.SystemClose;
                }
                else if (this.cbx_type.Text == "用户切换")
                {
                    types[0] = LogType.SystemUserChange;
                }
                else if (this.cbx_type.Text == "双机切换")
                {
                    types[0] = LogType.SystemChange;
                }
            }
            Log[] logs = Log.GetLogsByTypes(types, new DateTime?(start), new DateTime?(end));
            this.dataGridView1.Rows.Clear();
            foreach (Log log in logs)
            {
                int i = this.dataGridView1.Rows.Add();
                this.dataGridView1[0, i].Value = log.Operation;
                this.dataGridView1[1, i].Value = log.LogTime;
            }
        }

       
    }
}

