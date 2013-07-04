namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class List_alarm_dual : Form
    {
        private List<int> alarmId;
        private DataGridViewTextBoxColumn column1;
        private DataGridViewTextBoxColumn Column2;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private Dictionary<int, string> mAlarm_dict = new Dictionary<int, string>();

        public List_alarm_dual()
        {
            this.InitializeComponent();
           
            
        }

        private void deleteRow(int index, int id)
        {
          //  lock (this.alarmId)
            {
                if ((index >= 0) && (index < this.dataGridView1.Rows.Count))
                {
                    this.dataGridView1.Rows.RemoveAt(index);
                   // this.alarmId.Remove(id);
                    OperateDB.Execute(string.Concat(new object[] { "update MultiAlarm set endTime = '", DateTime.Now, "' where id = ", id, " and endTime is null" }));
                }
            }
        }

       

        internal void Dispatch()
        {
            List<int> multi_alarm = GlobalParams.cedian_alarm.multi_alarm_list;

            List<int> alist2 = new List<int>(multi_alarm.ToArray()); ;

            List<int> mmList=new List<int>(alarmId.ToArray());

            if (mmList.Count == 0)
            {
                for(int i=0;i<dataGridView1.Rows.Count;i++){
                    this.dataGridView1.BeginInvoke(new DeleteRow(this.deleteRow), new object[] { 0, 2 });
                }
                
            }
            for (int i = 0; i < mmList.Count; i++)
            {
                int rowno = mmList.Count - i - 1;
                int id = mmList[rowno];
                int index = alist2.IndexOf(id);
                if (index < 0)
                {
                    this.dataGridView1.BeginInvoke(new DeleteRow(this.deleteRow), new object[] { rowno, id });
                    mmList.Remove(id);
                }             
            }

            foreach (int id in alist2)
            {
                int index = mmList.IndexOf(id);
                if (index < 0)
                {
                    string alarmInfo = string.Empty;
                    if(mAlarm_dict.TryGetValue(id, out alarmInfo))
                    {
                        this.dataGridView1.BeginInvoke(new RefreshDataGridView(this.refreshDataGridView), new object[] { id, alarmInfo });
                        mmList.Add(id);
                    }
                    
                }                
            }
            alarmId=new List<int>(mmList.ToArray());           

        }

        public void deleteSpecifiedAlarmId(int id)
        {
            int index = this.alarmId.IndexOf(id);
            this.dataGridView1.BeginInvoke(new DeleteRow(this.deleteRow), new object[] { index, id });
        }

        public void Init()
        {
            this.alarmId = new List<int>();
            mAlarm_dict.Clear();
            foreach (KeyValuePair<int, List<DualAlarmInfo>> pair in GlobalParams.dualAlarmInfo2)
            {

                int id = pair.Key;
                List<DualAlarmInfo> list2 = (List<DualAlarmInfo>)pair.Value;

                GlobalParams.dualAlarmInfo2.TryGetValue(id, out list2);
                string str = string.Empty;
                foreach (DualAlarmInfo info in list2)
                {
                    string s = string.Empty;
                    string icdbh = info.Cedianbianhao;
                    CeDian cedian2 = GlobalParams.AllCeDianList.getCedianInfo(icdbh);
                    if (info.State == 0)
                    {
                        s = cedian2.KaiGuanLiang.LingTai;
                    }
                    else if (info.State == 1)
                    {
                        s = cedian2.KaiGuanLiang.YiTai;
                    }
                    else if (info.State == 2)
                    {
                        s = cedian2.KaiGuanLiang.ErTai;
                    }
                    str += info.Cedianbianhao + ": " + s + "; ";
                }
                mAlarm_dict.Add(id, str);
            }
        }

                //string cdbh = pair.Key.ToString();
                //if (cedian.CeDianBianHao != cdbh)
                //    continue;
                //List<int> listid = (List<int>)pair.Value;
                //for (int i = 0; i < listid.Count; i++)
                //{
                //    int id = listid[i];
                //    List<DualAlarmInfo> list2;
                //    bool baojing = false;
                //    GlobalParams.dualAlarmInfo2.TryGetValue(id, out list2);
                //    foreach (DualAlarmInfo info in list2)
                //    {
                //        string icdbh = info.Cedianbianhao;
                //        CeDian icedian = GlobalParams.AllCeDianList.getCedianInfo(icdbh);
                //    }
                //}
          

        internal void Dispatch2(FenZhanRTdata ud, CeDian cedian, FenZhanRTdata[] other)
        {
            string ceDianBianHao = cedian.CeDianBianHao;
            other[ud.fenZhanHao] = ud;
            List<int> list = null;
            GlobalParams.dualAlarmInfo1.TryGetValue(ceDianBianHao, out list);
            if (list != null)
            {
                foreach (int id in list)
                {
                    List<DualAlarmInfo> list2 = null;
                    GlobalParams.dualAlarmInfo2.TryGetValue(id, out list2);
                    bool alarm = true;
                    string alarmInfo = string.Empty;
                    if (list2 != null)
                    {
                        foreach (DualAlarmInfo info in list2)
                        {
                            if (other[info.Fenzhan].tongDaoZhuangTai[info.Tongdao] != info.State)
                            {
                                alarm = false;
                                break;
                            }
                            CeDian cedian2 = GlobalParams.AllCeDianList.allcedianlist[info.Cedianbianhao];
                            string s = "";
                            if (info.State == 0)
                            {
                                s = cedian2.KaiGuanLiang.LingTai;
                            }
                            else if (info.State == 1)
                            {
                                s = cedian2.KaiGuanLiang.YiTai;
                            }
                            else if (info.State == 2)
                            {
                                s = cedian2.KaiGuanLiang.ErTai;
                            }
                            string Reflector0003 = alarmInfo;
                            alarmInfo = Reflector0003 + info.Cedianbianhao + ": " + s + "; ";
                        }
                        lock (this.alarmId)
                        {
                            if (alarm)
                            {
                                this.dataGridView1.BeginInvoke(new RefreshDataGridView(this.refreshDataGridView), new object[] { id, alarmInfo });
                            }
                            else if (!alarm)
                            {
                                int index = this.alarmId.IndexOf(id);
                                this.dataGridView1.BeginInvoke(new DeleteRow(this.deleteRow), new object[] { index, id });
                            }
                        }
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(List_alarm_dual));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column1,
            this.Column2});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(498, 262);
            this.dataGridView1.TabIndex = 0;
            // 
            // column1
            // 
            this.column1.FillWeight = 26.5252F;
            this.column1.HeaderText = "编号";
            this.column1.Name = "column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 173.4748F;
            this.Column2.HeaderText = "报警内容";
            this.Column2.Name = "Column2";
            // 
            // List_alarm_dual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 262);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "List_alarm_dual";
            this.Text = "多设备报警列表";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        private void refreshDataGridView(int id, string alarmInfo)
        {
          //  lock (this.alarmId)
            {
               // if (!this.alarmId.Contains(id))
                {
                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells[0].Value = id;
                    this.dataGridView1.Rows[index].Cells[1].Value = alarmInfo;
                  //  this.alarmId.Add(id);
                    OperateDB.Execute(string.Concat(new object[] { "insert into MultiAlarm values(", id, ", '", DateTime.Now, "', null , '", alarmInfo, "')" }));
                }
            }
        }

        private delegate void DeleteRow(int index, int id);

        private delegate void RefreshDataGridView(int id, string alarmInfo);
    }
}

