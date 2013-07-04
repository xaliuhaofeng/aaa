namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;

    public class List_All_Alarm : DockContent
    {
        private List<string> alarmCeDians;
        private DataGridViewTextBoxColumn alarmType;
        private DataGridViewTextBoxColumn BaoJingShiKe;
        private DataGridViewTextBoxColumn CeDianBianHao;
        private IContainer components = null;
        private ClientConfig config;
        private DataGridViewTextBoxColumn CuoShi;
        private int currow = -1;
        private DateTime curtime;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn DiDian;
        private DataGridViewTextBoxColumn JianCeZhi;
        private DataGridViewTextBoxColumn MingCheng;
        private byte type = 0;

        public List_All_Alarm()
        {
            this.InitializeComponent();
            this.config = GlobalParams.config;
            this.alarmCeDians = new List<string>();
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = this.dataGridView1.HitTest(e.X, e.Y);
            if (hti.ColumnIndex == 6)
            {
                this.currow = hti.RowIndex;
                if (this.currow < 0)
                {
                    MessageBox.Show("请选择要输入措施的测点");
                }
                else
                {
                    this.curtime = DateTime.Parse(this.dataGridView1[5, this.currow].Value.ToString());
                    this.type = (byte) CedianAlarm.getAlalarm(this.dataGridView1[3, this.currow].Value.ToString());
                    new Form_measure(this, this.dataGridView1.Rows[this.currow].Cells["CeDianBianHao"].Value.ToString(), this.curtime, this.type).Show();
                }
            }
        }

        private void deleteRow(int index, string key)
        {
            int row = alarmCeDians.IndexOf(key);
            if (row < 0)
                return;
            if (dataGridView1.RowCount > row)
            {
                this.dataGridView1.Rows.RemoveAt(row);
                this.alarmCeDians.Remove(key);
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

        public void fresh()
        {
            Dictionary<string, string[]> dict = new Dictionary<string, string[]>();
            try
            {
                if (GlobalParams.cedian_alarm.alarm_all_Dict.Count >= -1)
                {
                    string key;
                    string[] str;
                    foreach (KeyValuePair<string, string[]> item in GlobalParams.cedian_alarm.alarm_all_Dict)
                    {
                        key = item.Key;
                        str = item.Value;
                        dict.Add(key, str);
                    }
                    foreach (KeyValuePair<string, string[]> item in dict)
                    {
                        key = item.Key;
                        str = item.Value;
                        CeDian cedian = GlobalParams.AllCeDianList.getCedianInfo(str[0]);
                        if (cedian != null)
                        {
                            int st = Convert.ToInt32(str[1].ToString());
                            int at = Convert.ToInt32(str[4]);
                            this.dataGridView1.BeginInvoke(new RefreshDataGridView(this.refreshDataGridView), new object[] { str[0], cedian, cedian.RtVal, st, cedian.Time, at });
                        }
                    }
                    for (int i = this.dataGridView1.Rows.Count; i >= 0; i--)
                    {
                     //   int a = CedianAlarm.getAlalarm(this.dataGridView1.Rows[i - 1].Cells["alarmType"].Value.ToString());
                        int rowno = i - 1;
                        if (rowno >= 0)
                        {
                            key = this.dataGridView1.Rows[rowno].Cells["CeDianBianHao"].Value.ToString();
                            if (key != null)
                            {
                                string[] item;
                                if (!dict.TryGetValue(key, out item))
                                {
                                    this.dataGridView1.BeginInvoke(new DeleteRow(this.deleteRow), new object[] { i - 1, key });
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ee)
            {
                Console.WriteLine(ee.ToString());
            }
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CeDianBianHao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiDian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MingCheng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarmType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JianCeZhi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BaoJingShiKe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CuoShi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Chocolate;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CeDianBianHao,
            this.DiDian,
            this.MingCheng,
            this.alarmType,
            this.JianCeZhi,
            this.BaoJingShiKe,
            this.CuoShi});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(894, 432);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            // 
            // CeDianBianHao
            // 
            this.CeDianBianHao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CeDianBianHao.FillWeight = 1.63192F;
            this.CeDianBianHao.Frozen = true;
            this.CeDianBianHao.HeaderText = "测点编号";
            this.CeDianBianHao.Name = "CeDianBianHao";
            // 
            // DiDian
            // 
            this.DiDian.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DiDian.FillWeight = 2.447879F;
            this.DiDian.Frozen = true;
            this.DiDian.HeaderText = "地点";
            this.DiDian.Name = "DiDian";
            this.DiDian.Width = 200;
            // 
            // MingCheng
            // 
            this.MingCheng.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MingCheng.FillWeight = 1.504185F;
            this.MingCheng.Frozen = true;
            this.MingCheng.HeaderText = "名称";
            this.MingCheng.Name = "MingCheng";
            this.MingCheng.Width = 150;
            // 
            // alarmType
            // 
            this.alarmType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.alarmType.FillWeight = 1.63192F;
            this.alarmType.Frozen = true;
            this.alarmType.HeaderText = "告警类型";
            this.alarmType.Name = "alarmType";
            this.alarmType.Width = 150;
            // 
            // JianCeZhi
            // 
            this.JianCeZhi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.JianCeZhi.FillWeight = 1.305536F;
            this.JianCeZhi.Frozen = true;
            this.JianCeZhi.HeaderText = "状态/监测值";
            this.JianCeZhi.Name = "JianCeZhi";
            this.JianCeZhi.Width = 120;
            // 
            // BaoJingShiKe
            // 
            this.BaoJingShiKe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BaoJingShiKe.FillWeight = 1.63192F;
            this.BaoJingShiKe.Frozen = true;
            this.BaoJingShiKe.HeaderText = "报警时刻";
            this.BaoJingShiKe.Name = "BaoJingShiKe";
            this.BaoJingShiKe.Width = 130;
            // 
            // CuoShi
            // 
            this.CuoShi.FillWeight = 58.62747F;
            this.CuoShi.HeaderText = "措施";
            this.CuoShi.Name = "CuoShi";
            // 
            // List_All_Alarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 432);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "List_All_Alarm";
            this.Text = "当前告警列表";
            this.Load += new System.EventHandler(this.List_All_Alarm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        private void List_All_Alarm_Load(object sender, EventArgs e)
        {
        }

        private void refreshDataGridView(string ceDianBianHao, CeDian cedian, float realValue, int state, DateTime startTime, int at)
        {
            try
            {
                string str = ceDianBianHao;
                if (this.alarmCeDians.Contains(str))
                {
                    int index = this.alarmCeDians.IndexOf(str);
                    if (index >= 0)
                    {
                        if (cedian.DaLeiXing == 0)
                        {
                            if ((state == 4) || (state == 7))
                            {
                                this.dataGridView1.Rows[index].Cells["JianCeZhi"].Value = this.State(state);
                            }
                            else
                            {
                                this.dataGridView1.Rows[index].Cells["JianCeZhi"].Value = this.State(state) + "  " + realValue;
                            }
                        }
                        else
                        {
                           
                            if (cedian.DaLeiXing == 1)
                                this.dataGridView1.Rows[index].Cells["JianCeZhi"].Value = cedian.getDigiVal((int)realValue);
                            else
                                this.dataGridView1.Rows[index].Cells["JianCeZhi"].Value = cedian.getCtlVal((int)realValue);
                        }

                        this.dataGridView1.Rows[index].Cells["alarmType"].Value = CedianAlarm.getAlalarm(at);
                    }
                }
                else
                {
                    int index2 = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index2].Cells["CeDianBianHao"].Value = ceDianBianHao;
                    this.dataGridView1.Rows[index2].Cells["DiDian"].Value = cedian.CeDianWeiZhi;
                    this.dataGridView1.Rows[index2].Cells["MingCheng"].Value = cedian.XiaoleiXing;
                    this.dataGridView1.Rows[index2].Cells["alarmType"].Value = CedianAlarm.getAlalarm(at);
                    if (cedian.DaLeiXing == 0)
                    {
                        if ((state == 4) || (state == 7))
                        {
                            this.dataGridView1.Rows[index2].Cells["JianCeZhi"].Value = this.State(state);
                        }
                        else
                        {
                            this.dataGridView1.Rows[index2].Cells["JianCeZhi"].Value = this.State(state) + "  " + realValue;
                        }
                    }
                    else 
                    {
                        if(cedian.DaLeiXing==1)
                              this.dataGridView1.Rows[index2].Cells["JianCeZhi"].Value = cedian.getDigiVal((int) realValue);
                        else
                            this.dataGridView1.Rows[index2].Cells["JianCeZhi"].Value = cedian.getCtlVal((int)realValue);

                    }
                    this.dataGridView1.Rows[index2].Cells["BaoJingShiKe"].Value = startTime;
                    this.dataGridView1.Rows[index2].Cells["CuoShi"].Value = string.Empty;
                    this.alarmCeDians.Add(ceDianBianHao);
                }
            }
            catch(Exception ee)
            {
                Console.WriteLine("tg" + ee.ToString());
            }
        }

        private string State(int s)
        {
            string result = "其他";
            switch (s)
            {
                case 0:
                    return "正常";

                case 1:
                    return "报警";

                case 2:
                    return "断电";

                case 3:
                    return "复电";

                case 4:
                    return "断线";

                case 5:
                    return "溢出";

                case 6:
                    return "负漂";

                case 7:
                    return "故障";

                case 8:
                    return "I/O错误";

                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                    return result;

                case 14:
                    return "馈电异常";
            }
            return result;
        }

        public void UpdateL()
        {
            if (this.currow > -1)
            {
                string str = Measure.GetMeasure(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString(), this.type, this.curtime);
                this.dataGridView1.Rows[this.currow].Cells["CuoShi"].Value = str;
            }
        }

        private delegate void DeleteRow(int index, string key);

        private delegate void RefreshDataGridView(string cedianbianhao, CeDian cedian, float realValue, int state, DateTime date, int at);
    }
}

