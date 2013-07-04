namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class Form_PreAlarm : Form
    {
       // private ToolStripButton button;
        public bool IsSetYJ = false;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private Timer timer1;
        private List<string> YuJingCeDians;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        bool isTop = false;

        public Form_PreAlarm(bool b)        
        {
            IsSetYJ = b;
            this.InitializeComponent();;
            this.YuJingCeDians = new List<string>();
        }

        private void AddRowToDataGrid(string cedianbianhao, CeDian cedian, float realValue, string state)
        {
            int index = this.dataGridView1.Rows.Add();           
            this.dataGridView1[0, index].Value = cedianbianhao;
            this.dataGridView1[1, index].Value = cedian.CeDianWeiZhi + "/" + cedian.XiaoleiXing;
            this.dataGridView1[2, index].Value = Math.Round((double) realValue, 2);
            this.dataGridView1[3, index].Value = state;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void deleteRow(int index)
        {
            if(index<dataGridView1.Rows.Count)
            this.dataGridView1.Rows.RemoveAt(index);
        }

        public void Dispach(FenZhanRTdata tmpud, FenZhanRTdata pre, CeDian cedian, int i)
        {
            string cedianbianhao = cedian.CeDianBianHao;
            bool isYuJing = false;
            try
            {
                if (cedian.MoNiLiang.BaoJingZhiShangXian > tmpud.realValue[i])
                {
                    YuJing yuJing = new YuJing(tmpud.fenZhanHao, i);
                    if (yuJing.Exist())
                    {
                        if ((yuJing.YuJingValue > 0f) && (tmpud.realValue[i] > yuJing.YuJingValue))
                        {
                            while (!this.dataGridView1.IsHandleCreated)
                            {
                            }
                            this.dataGridView1.BeginInvoke(new RefreshDataGridView(this.processingPreAlarm), new object[] { cedianbianhao, cedian, tmpud.realValue[i], tmpud.tongDaoZhuangTai[i] });
                            isYuJing = true;
                        }
                        else if (((yuJing.ChangeRate > 0f) && (pre.realValue[i] > 0f)) && (((tmpud.realValue[i] - pre.realValue[i]) / pre.realValue[i]) > yuJing.ChangeRate))
                        {
                            while (!this.dataGridView1.IsHandleCreated)
                            {
                            }
                            this.dataGridView1.BeginInvoke(new RefreshDataGridView(this.processingPreAlarm), new object[] { cedianbianhao, cedian, tmpud.realValue[i], tmpud.tongDaoZhuangTai[i] });
                            isYuJing = true;
                        }
                    }
                }
            }
            catch
            {
            }
            if (!isYuJing && this.YuJingCeDians.Contains(cedianbianhao))
            {
                int index = this.YuJingCeDians.IndexOf(cedianbianhao);
                if ((index >= 0) && (index < this.dataGridView1.Rows.Count))
                {
                    while (!this.dataGridView1.IsHandleCreated)
                    {
                    }
                    this.dataGridView1.BeginInvoke(new DeleteRow(this.deleteRow), new object[] { index });
                    this.YuJingCeDians.RemoveAt(index);
                    if (this.YuJingCeDians.Count == 0)
                    {
                    //    this.button.Checked = false;
                        base.Hide();
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

        private void Form_PreAlarm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Form_PreAlarm_Load(object sender, EventArgs e)
        {
        }

        public bool fresh()
        {
            if (GlobalParams.cedian_alarm.alarm_yujing_Dict.Count == 0)
            {
                //dataGridView1.Rows.Clear();
                while (this.dataGridView1.Rows.Count > 0)
                {
                    this.dataGridView1.Invoke(new DeleteRow(this.deleteRow), new object[] { 0 });
                }
               // this.button.Checked = false;
                YuJingCeDians.Clear();
                base.Hide();
                return false;
            }
            else
            {
                int i;
                Dictionary<string, string[]> yj_tmp_Dict = new Dictionary<string, string[]>();
                List<string> index = new List<string>(); ;


                foreach (KeyValuePair<string, string[]> item in GlobalParams.cedian_alarm.alarm_yujing_Dict)
                {
                    index.Add(item.Key);
                    yj_tmp_Dict.Add(item.Key, item.Value);
                }

                isTop = false;

                foreach (KeyValuePair<string, string[]> item in yj_tmp_Dict)
                {
                    string key = item.Key;
                    CeDian cedian = GlobalParams.AllCeDianList.getCedianInfo(key);
                    if(cedian!=null)
                        dataGridView1.Invoke(new RefreshDataGridView(this.processingPreAlarm), new object[] { key, cedian, cedian.RtVal, cedian.RtState });
                }

                int count = YuJingCeDians.Count;
                for (i = count - 1; i > -1; i--)
                {
                    string cdbh = YuJingCeDians[i];
                    int pos = index.IndexOf(cdbh);
                    if (pos<0)
                    {
                        this.dataGridView1.Invoke(new DeleteRow(this.deleteRow), new object[] { i });
                        YuJingCeDians.Remove(cdbh);
                    }
                }

                if (IsSetYJ)
                {
                    //  this.button.Checked = true;
                    //if (isTop)
                    //{
                    //    base.TopMost = true;
                    //    base.Show();
                    //}
                    return isTop;
                }
                else
                    return isTop;
                //this.YuJingCeDians.Clear();
                //for (i = 0; i < this.dataGridView1.Rows.Count; i++)
                //{
                //    this.YuJingCeDians.Add(this.dataGridView1[1, i].Value.ToString());
                //}
               
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_PreAlarm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LightGray;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Single;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Linen;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(483, 266);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 74.70753F;
            this.Column1.HeaderText = "测点编号";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 170.1733F;
            this.Column2.HeaderText = "安装地点/名称";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.FillWeight = 73.90091F;
            this.Column3.HeaderText = "测量值";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.FillWeight = 81.21828F;
            this.Column4.HeaderText = "状态";
            this.Column4.Name = "Column4";
            // 
            // Form_PreAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 266);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_PreAlarm";
            this.Text = "预警窗口";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_PreAlarm_FormClosing);
            this.Load += new System.EventHandler(this.Form_PreAlarm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        private void processingPreAlarm(string cedianbianhao, CeDian cedian, float realValue, int state)
        {
            if (this.YuJingCeDians.Contains(cedianbianhao))
            {
                this.UpdateRowOfDataGrid(cedianbianhao, realValue, GlobalParams.stateTranslate(state));
            }
            else
            {
                this.AddRowToDataGrid(cedianbianhao, cedian, realValue, GlobalParams.stateTranslate(state));
                this.YuJingCeDians.Add(cedianbianhao);

                isTop = true; ;
            }
            
        }

        private void processingPreAlarmold(string cedianbianhao, CeDian cedian, float realValue, int state)
        {
            if (this.YuJingCeDians.Contains(cedianbianhao))
            {
                this.UpdateRowOfDataGrid(cedianbianhao, realValue, GlobalParams.stateTranslate(state));
            }
            else
            {
                this.AddRowToDataGrid(cedianbianhao, cedian, realValue, GlobalParams.stateTranslate(state));
                this.YuJingCeDians.Add(cedianbianhao);
            }
            //if (!this.button.Checked)
            //{
            //    this.button.Checked = true;
            //    if (!base.Visible)
            //    {
            //        base.TopMost = true;
            //        base.Show();
            //    }
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void UpdateRowOfDataGrid(string cedianbianhao, float realValue, string state)
        {
            int index = this.YuJingCeDians.IndexOf(cedianbianhao);
            this.dataGridView1[2, index].Value = Math.Round((double) realValue, 2);
            this.dataGridView1[3, index].Value = state;
        }

        private delegate void DeleteRow(int index);

        private delegate void RefreshDataGridView(string cedianbianhao, CeDian cedian, float realValue, int state);
    }
}

