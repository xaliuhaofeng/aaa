namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Timers;
    using System.Windows.Forms;

    public class Comm_ChuanKou : UserControl
    {
        private Button button1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewComboBoxColumn Column2;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private string fail_comm = "              通讯失败";
        private string normal = "正常";
        private string timeout = "        超时";
        private System.Windows.Forms.Timer timer1;
        private string wait = "等待中......... ";
        private string weipeizhi = "                           未配置";
        private DataGridViewTextBoxColumn 通信状态;

        public Comm_ChuanKou()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] b = new byte[60];
            for (int i = 0; i < 60; i++)
            {
                b[i] = Convert.ToByte(((DataGridViewComboBoxCell) this.dataGridView1.Rows[i].Cells[1]).Value);
            }
            UDPComm.Send(FenZhan.ChuanKouPeiZhi(b));
        }

        private void Comm_ChuanKou_Load(object sender, EventArgs e)
        {
            int i;
            DataTable dt = new DataTable();
            dt.Columns.Add("aaa");
            for (i = 0; i < 20; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i.ToString();
                dt.Rows.Add(dr);
            }
            ((DataGridViewComboBoxColumn) this.dataGridView1.Columns[1]).DataSource = dt;
            ((DataGridViewComboBoxColumn) this.dataGridView1.Columns[1]).ValueMember = "aaa";
            ((DataGridViewComboBoxColumn) this.dataGridView1.Columns[1]).DisplayMember = "aaa";
            byte[] chuanKou = FenZhan.GetAllChuanKouHao();
            for (i = 0; i < 60; i++)
            {
                DataGridViewRow dgvr = new DataGridViewRow();
                this.dataGridView1.Rows.Add(dgvr);
                this.dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
                ((DataGridViewComboBoxCell) this.dataGridView1.Rows[i].Cells[1]).Value = chuanKou[i].ToString();
                if (chuanKou[i] > 0)
                {
                    this.dataGridView1.Rows[i].Cells[2].Value = this.wait;
                }
                else
                {
                    this.dataGridView1.Rows[i].Cells[2].Value = this.weipeizhi;
                }
            }
        }

        public void Dispatch(FenZhanRTdata ud)
        {
            if ((ud.fenZhanHao <= 60) && (Convert.ToByte(this.dataGridView1.Rows[ud.fenZhanHao - 1].Cells[1].Value) != 0))
            {
                GlobalParams.AllfenZhans[ud.fenZhanHao].Dispatch(ud);
                GlobalParams.AllfenZhans[ud.fenZhanHao].Recv_state = 1;
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.通信状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.通信状态});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(69, 89);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(472, 227);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "分站号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Column2.HeaderText = "串口号";
            this.Column2.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.Column2.MaxDropDownItems = 100;
            this.Column2.Name = "Column2";
            // 
            // 通信状态
            // 
            this.通信状态.HeaderText = "通信状态";
            this.通信状态.Name = "通信状态";
            this.通信状态.ReadOnly = true;
            this.通信状态.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Chocolate;
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(466, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Comm_ChuanKou
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Comm_ChuanKou";
            this.Size = new System.Drawing.Size(615, 394);
            this.Load += new System.EventHandler(this.Comm_ChuanKou_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 60; i++)
            {
                if (Convert.ToByte(this.dataGridView1.Rows[i].Cells[1].Value) == 0)
                {
                    this.dataGridView1.Rows[i].Cells[2].Value = this.weipeizhi;
                    continue;
                }
                byte b = GlobalParams.AllfenZhans[i + 1].commState;
                string s = this.wait;
                switch (b)
                {
                    case 0:
                        s = this.normal;
                        break;

                    case 1:
                        s = this.timeout;
                        break;

                    case 2:
                        s = this.fail_comm;
                        break;

                    default:
                        s = this.fail_comm;
                        break;
                }
                this.dataGridView1.Rows[i].Cells[2].Value = s;
            }
            this.dataGridView1.Refresh();
        }

        //private void timers_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    for (int i = 1; i < 0x3d; i++)
        //    {
        //        if (GlobalParams.AllfenZhans[i].Recv_state == 0)
        //        {
        //            GlobalParams.AllfenZhans[i].commState = 1;
        //        }
        //        GlobalParams.AllfenZhans[i].Recv_state = 0;
        //    }
        //}
    }
}

