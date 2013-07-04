namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class List_Alarm_Frame : Form
    {
        private List<string> alarm_ana_cutoff_data;
        private List<string> alarm_ana_feed_data;
        private List<string> alarm_analog_data;
        private List<string> alarm_digi_cutoff_data;
        private List<string> alarm_digi_data;
        private List<string> alarm_digi_feed_data;
        private DataGridViewTextBoxColumn alarmType;
        private List<string> AllalarmCeDians;
        private DataGridViewTextBoxColumn BaoJingShiKe;
        private CedianAlarm cedianalarm;
        private DataGridViewTextBoxColumn CeDianBianHao;
        private IContainer components = null;
        private DataGridViewTextBoxColumn CuoShi;
      
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private DataGridView dataGridView3;
        private DataGridView dataGridView4;
        private DataGridView dataGridView5;
        private DataGridView dataGridView6;
        private DataGridView dataGridView7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn41;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private DataGridViewTextBoxColumn DiDian;
        private int index_select = -1;
        private DataGridViewTextBoxColumn JianCeZhi;
        private DataGridViewTextBoxColumn JianCeZhuangTai;
        private DataGridViewTextBoxColumn KaiTingShiKe;
        private DataGridViewTextBoxColumn KuiDianYiChangShiKe;
        private DataGridViewTextBoxColumn MingCheng;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewTextBoxColumn DuanDianShiKe;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn DanWei;
        private DataGridViewTextBoxColumn BaoJingMenXian;
        private DataGridViewTextBoxColumn DuanDianMenXian;
        private DataGridViewTextBoxColumn FuDianMenXian;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn GongZuoZhuangTai;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn all_cdbh;
        private DataGridViewTextBoxColumn all_didian;
        private DataGridViewTextBoxColumn all_name;
        private DataGridViewTextBoxColumn all_type;
        private DataGridViewTextBoxColumn all_val;
        private DataGridViewTextBoxColumn all_time;
        private DataGridViewTextBoxColumn all_measure;
        private Timer timer1;

        public List_Alarm_Frame(CedianAlarm ca)
        {
            this.InitializeComponent();
            this.cedianalarm = ca;
            this.AllalarmCeDians = new List<string>();
            this.alarm_analog_data = new List<string>();
            this.alarm_ana_cutoff_data = new List<string>();
            this.alarm_ana_feed_data = new List<string>();
            this.alarm_digi_data = new List<string>();
            this.alarm_digi_cutoff_data = new List<string>();
            this.alarm_digi_feed_data = new List<string>();
        }

        private bool compare(Dictionary<string, string[]> dicta, Dictionary<string, string[]> dictb)
        {
            try
            {
                string key;
                foreach (KeyValuePair<string, string[]> item in dictb)
                {
                    key = item.Key;
                    if (!dicta.ContainsKey(key))
                    {
                        return true;
                    }
                }
                foreach (KeyValuePair<string, string[]> item in dicta)
                {
                    key = item.Key;
                    if (!dictb.ContainsKey(key))
                    {
                        return true;
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        private Dictionary<string, string[]> data_pro(Dictionary<string, string[]> dicta, Dictionary<string, string[]> dictb)
        {
            try
            {
                string key;
                foreach (KeyValuePair<string, string[]> item in dictb)
                {
                    key = item.Key;
                    if (!dicta.ContainsKey(key))
                    {
                        dictb.Remove(key);
                    }
                }
                foreach (KeyValuePair<string, string[]> item in dicta)
                {
                    key = item.Key;
                    if (!dictb.ContainsKey(key))
                    {
                        string[] a = (string[]) item.Value.Clone();
                        dictb.Add(key.Clone().ToString(), a);
                    }
                }
            }
            catch
            {
            }
            return dictb;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void deleteRow(int index, int row, string key)
        {
            DataGridView dgv = this.getDataView(index);
            if ((dgv != null) && (dgv.Rows.Count > row))
            {
                dgv.Rows.RemoveAt(row);
                switch (index)
                {
                    case 0:
                        this.AllalarmCeDians.Remove(key);
                        break;

                    case 1:
                        this.alarm_analog_data.Remove(key);
                        break;

                    case 2:
                        this.alarm_ana_cutoff_data.Remove(key);
                        break;

                    case 3:
                        this.alarm_ana_feed_data.Remove(key);
                        break;

                    case 4:
                        this.alarm_digi_data.Remove(key);
                        break;

                    case 5:
                        this.alarm_digi_cutoff_data.Remove(key);
                        break;

                    case 6:
                        this.alarm_digi_feed_data.Remove(key);
                        break;
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

        public void fresh_data()
        {
            if (base.Visible)
            {
                Dictionary<string, string[]> dict = new Dictionary<string, string[]>();
                try
                {
                    int at;
                    string key;
                    string[] str;
                    CeDian cedian;
                    int i;
                    switch (this.index_select)
                    {
                        case 0:
                            if (GlobalParams.cedian_alarm.alarm_all_Dict != null)
                            {
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
                                    cedian = GlobalParams.AllCeDianList.allcedianlist[str[0]];
                                    at = Convert.ToInt32(str[4]);
                                    int stt = Convert.ToInt32(str[1]);
                                    this.dataGridView1.BeginInvoke(new RefreshDataGridView(this.modifyDataView), new object[] { 0, str[0], cedian, str[2], stt, str[3], at });
                                }
                                for (i = this.dataGridView1.Rows.Count; i > 0; i--)
                                {
                                    int a = CedianAlarm.getAlalarm(this.dataGridView1[3, i - 1].Value.ToString());
                                    key = this.dataGridView1[0, i - 1].Value.ToString();
                                    if (!GlobalParams.cedian_alarm.alarm_all_Dict.ContainsKey(key))
                                    {
                                        this.dataGridView1.BeginInvoke(new DeleteRow(this.deleteRow), new object[] { 0, i - 1, key });
                                    }
                                }
                            }
                            return;

                        case 1:
                            foreach (KeyValuePair<string, string[]> item in GlobalParams.cedian_alarm.alarm_analog_Dict)
                            {
                                key = item.Key;
                                str = item.Value;
                                dict.Add(key, str);
                            }
                            foreach (KeyValuePair<string, string[]> item in dict)
                            {
                                key = item.Key;
                                str = item.Value;
                                cedian = GlobalParams.AllCeDianList.allcedianlist[str[0]];
                                at = Convert.ToInt32(str[1]);
                                this.dataGridView2.BeginInvoke(new RefreshDataGridView(this.modifyDataView), new object[] { 1, str[0], cedian, str[2], at, str[3], 1 });
                            }
                            for (i = this.dataGridView2.Rows.Count; i > 0; i--)
                            {
                                key = this.dataGridView2[1, i - 1].Value.ToString();
                                if (!GlobalParams.cedian_alarm.alarm_analog_Dict.ContainsKey(key))
                                {
                                    this.dataGridView2.BeginInvoke(new DeleteRow(this.deleteRow), new object[] { 1, i - 1, key });
                                }
                            }
                            return;

                        case 2:
                            foreach (KeyValuePair<string, string[]> item in GlobalParams.cedian_alarm.alarm_ana_cutoff_Dict)
                            {
                                key = item.Key;
                                str = item.Value;
                                dict.Add(key, str);
                            }
                            foreach (KeyValuePair<string, string[]> item in dict)
                            {
                                key = item.Key;
                                str = item.Value;
                                cedian = GlobalParams.AllCeDianList.allcedianlist[str[0]];
                                at = Convert.ToInt32(str[1]);
                                this.dataGridView3.BeginInvoke(new RefreshDataGridView(this.modifyDataView), new object[] { 2, str[0], cedian, str[2], at, str[3], 2 });
                            }
                            for (i = this.dataGridView3.Rows.Count; i > 0; i--)
                            {
                                key = this.dataGridView3[1, i - 1].Value.ToString();
                                if (!GlobalParams.cedian_alarm.alarm_ana_cutoff_Dict.ContainsKey(key))
                                {
                                    this.dataGridView3.BeginInvoke(new DeleteRow(this.deleteRow), new object[] { 2, i - 1, key });
                                }
                            }
                            return;

                        case 3:
                            foreach (KeyValuePair<string, string[]> item in GlobalParams.cedian_alarm.alarm_ana_feed_Dict)
                            {
                                key = item.Key;
                                str = item.Value;
                                dict.Add(key, str);
                            }
                            foreach (KeyValuePair<string, string[]> item in dict)
                            {
                                key = item.Key;
                                str = item.Value;
                                cedian = GlobalParams.AllCeDianList.allcedianlist[str[0]];
                                at = Convert.ToInt32(str[1]);
                                this.dataGridView4.BeginInvoke(new RefreshDataGridView(this.modifyDataView), new object[] { 3, str[0], cedian, str[2], at, str[3], 3 });
                            }
                            for (i = this.dataGridView4.Rows.Count; i > 0; i--)
                            {
                                key = this.dataGridView4[1, i - 1].Value.ToString();
                                if (!GlobalParams.cedian_alarm.alarm_ana_feed_Dict.ContainsKey(key))
                                {
                                    this.dataGridView4.BeginInvoke(new DeleteRow(this.deleteRow), new object[] { 3, i - 1, key });
                                }
                            }
                            return;

                        case 4:
                            foreach (KeyValuePair<string, string[]> item in GlobalParams.cedian_alarm.alarm_digi_Dict)
                            {
                                key = item.Key;
                                str = item.Value;
                                dict.Add(key, str);
                            }
                            foreach (KeyValuePair<string, string[]> item in dict)
                            {
                                key = item.Key;
                                str = item.Value;
                                cedian = GlobalParams.AllCeDianList.allcedianlist[str[0]];
                                at = Convert.ToInt32(str[1]);
                                this.dataGridView5.BeginInvoke(new RefreshDataGridView(this.modifyDataView), new object[] { 4, str[0], cedian, str[2], at, str[3], 4 });
                            }
                            for (i = this.dataGridView5.Rows.Count; i > 0; i--)
                            {
                                key = this.dataGridView5[1, i - 1].Value.ToString();
                                if (!GlobalParams.cedian_alarm.alarm_digi_Dict.ContainsKey(key))
                                {
                                    this.dataGridView5.BeginInvoke(new DeleteRow(this.deleteRow), new object[] { 4, i - 1, key });
                                }
                            }
                            return;

                        case 5:
                            foreach (KeyValuePair<string, string[]> item in GlobalParams.cedian_alarm.alarm_digi_cutoff_Dict)
                            {
                                key = item.Key;
                                str = item.Value;
                                dict.Add(key, str);
                            }
                            foreach (KeyValuePair<string, string[]> item in dict)
                            {
                                key = item.Key;
                                str = item.Value;
                                cedian = GlobalParams.AllCeDianList.allcedianlist[str[0]];
                                at = Convert.ToInt32(str[1]);
                                this.dataGridView6.BeginInvoke(new RefreshDataGridView(this.modifyDataView), new object[] { 5, str[0], cedian, str[2], at, str[3], 5 });
                            }
                            for (i = this.dataGridView6.Rows.Count; i > 0; i--)
                            {
                                key = this.dataGridView6[1, i - 1].Value.ToString();
                                if (!GlobalParams.cedian_alarm.alarm_digi_cutoff_Dict.ContainsKey(key))
                                {
                                    this.dataGridView6.BeginInvoke(new DeleteRow(this.deleteRow), new object[] { 5, i - 1, key });
                                }
                            }
                            return;

                        case 6:
                            foreach (KeyValuePair<string, string[]> item in GlobalParams.cedian_alarm.alarm_digi_feed_Dict)
                            {
                                key = item.Key;
                                str = item.Value;
                                dict.Add(key, str);
                            }
                            foreach (KeyValuePair<string, string[]> item in dict)
                            {
                                key = item.Key;
                                str = item.Value;
                                cedian = GlobalParams.AllCeDianList.allcedianlist[str[0]];
                                at = Convert.ToInt32(str[5]);
                                this.dataGridView7.BeginInvoke(new RefreshDataGridView(this.modifyDataView), new object[] { 6, str[0], cedian, str[2], at, str[3], 6 });
                            }
                            for (i = this.dataGridView7.Rows.Count; i > 0; i--)
                            {
                                key = this.dataGridView7[1, i - 1].Value.ToString();
                                if (!GlobalParams.cedian_alarm.alarm_digi_feed_Dict.ContainsKey(key))
                                {
                                    this.dataGridView7.BeginInvoke(new DeleteRow(this.deleteRow), new object[] { 6, i - 1, key });
                                }
                            }
                            return;
                    }
                }
                catch
                {
                }
            }
        }

        private DataGridView getDataView(int a)
        {
            switch (a)
            {
                case 0:
                    return this.dataGridView1;

                case 1:
                    return this.dataGridView2;

                case 2:
                    return this.dataGridView3;

                case 3:
                    return this.dataGridView4;

                case 4:
                    return this.dataGridView5;

                case 5:
                    return this.dataGridView6;

                case 6:
                    return this.dataGridView7;
            }
            return null;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.dataGridView7 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JianCeZhuangTai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KaiTingShiKe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KuiDianYiChangShiKe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuanDianShiKe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DanWei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BaoJingMenXian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuanDianMenXian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FuDianMenXian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GongZuoZhuangTai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.all_cdbh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.all_didian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.all_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.all_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.all_val = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.all_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.all_measure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CuoShi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BaoJingShiKe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JianCeZhi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarmType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MingCheng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiDian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CeDianBianHao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView7)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.dataGridView7);
            this.tabPage7.Location = new System.Drawing.Point(4, 34);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(927, 399);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "开关量馈电异常列表";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // dataGridView7
            // 
            this.dataGridView7.AllowUserToAddRows = false;
            this.dataGridView7.AllowUserToDeleteRows = false;
            this.dataGridView7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView7.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView7.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView7.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView7.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView7.ColumnHeadersHeight = 30;
            this.dataGridView7.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn38,
            this.dataGridViewTextBoxColumn37,
            this.dataGridViewTextBoxColumn39,
            this.dataGridViewTextBoxColumn40,
            this.dataGridViewTextBoxColumn41,
            this.dataGridViewTextBoxColumn42});
            this.dataGridView7.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView7.EnableHeadersVisualStyles = false;
            this.dataGridView7.Location = new System.Drawing.Point(3, 3);
            this.dataGridView7.MultiSelect = false;
            this.dataGridView7.Name = "dataGridView7";
            this.dataGridView7.ReadOnly = true;
            this.dataGridView7.RowHeadersVisible = false;
            this.dataGridView7.RowTemplate.Height = 23;
            this.dataGridView7.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView7.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView7.Size = new System.Drawing.Size(928, 412);
            this.dataGridView7.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn38
            // 
            this.dataGridViewTextBoxColumn38.HeaderText = "测点编号";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn37
            // 
            this.dataGridViewTextBoxColumn37.HeaderText = "地点";
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            this.dataGridViewTextBoxColumn37.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn39
            // 
            this.dataGridViewTextBoxColumn39.HeaderText = "名称";
            this.dataGridViewTextBoxColumn39.Name = "dataGridViewTextBoxColumn39";
            this.dataGridViewTextBoxColumn39.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn40
            // 
            this.dataGridViewTextBoxColumn40.HeaderText = "开/停时刻";
            this.dataGridViewTextBoxColumn40.Name = "dataGridViewTextBoxColumn40";
            this.dataGridViewTextBoxColumn40.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn41
            // 
            this.dataGridViewTextBoxColumn41.HeaderText = "监测状态";
            this.dataGridViewTextBoxColumn41.Name = "dataGridViewTextBoxColumn41";
            this.dataGridViewTextBoxColumn41.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn42
            // 
            this.dataGridViewTextBoxColumn42.HeaderText = "措施";
            this.dataGridViewTextBoxColumn42.Name = "dataGridViewTextBoxColumn42";
            this.dataGridViewTextBoxColumn42.ReadOnly = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.dataGridView6);
            this.tabPage6.Location = new System.Drawing.Point(4, 34);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(927, 399);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "开关量断电列表";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // dataGridView6
            // 
            this.dataGridView6.AllowUserToAddRows = false;
            this.dataGridView6.AllowUserToDeleteRows = false;
            this.dataGridView6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView6.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView6.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView6.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView6.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView6.ColumnHeadersHeight = 30;
            this.dataGridView6.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn32,
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewTextBoxColumn33,
            this.dataGridViewTextBoxColumn35,
            this.dataGridViewTextBoxColumn34,
            this.dataGridViewTextBoxColumn36});
            this.dataGridView6.EnableHeadersVisualStyles = false;
            this.dataGridView6.Location = new System.Drawing.Point(3, 3);
            this.dataGridView6.MultiSelect = false;
            this.dataGridView6.Name = "dataGridView6";
            this.dataGridView6.ReadOnly = true;
            this.dataGridView6.RowHeadersVisible = false;
            this.dataGridView6.RowTemplate.Height = 23;
            this.dataGridView6.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView6.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView6.Size = new System.Drawing.Size(921, 412);
            this.dataGridView6.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn32
            // 
            this.dataGridViewTextBoxColumn32.HeaderText = "测点编号";
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn31
            // 
            this.dataGridViewTextBoxColumn31.HeaderText = "地点";
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            this.dataGridViewTextBoxColumn31.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.HeaderText = "名称";
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn35
            // 
            this.dataGridViewTextBoxColumn35.HeaderText = "监测状态";
            this.dataGridViewTextBoxColumn35.Name = "dataGridViewTextBoxColumn35";
            this.dataGridViewTextBoxColumn35.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn34
            // 
            this.dataGridViewTextBoxColumn34.HeaderText = "开/停时刻";
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            this.dataGridViewTextBoxColumn34.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn36
            // 
            this.dataGridViewTextBoxColumn36.HeaderText = "措施";
            this.dataGridViewTextBoxColumn36.Name = "dataGridViewTextBoxColumn36";
            this.dataGridViewTextBoxColumn36.ReadOnly = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dataGridView5);
            this.tabPage5.Location = new System.Drawing.Point(4, 34);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(927, 399);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "开关量报警列表";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dataGridView5
            // 
            this.dataGridView5.AllowUserToAddRows = false;
            this.dataGridView5.AllowUserToDeleteRows = false;
            this.dataGridView5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView5.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView5.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView5.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView5.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView5.ColumnHeadersHeight = 30;
            this.dataGridView5.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn28,
            this.dataGridViewTextBoxColumn27,
            this.dataGridViewTextBoxColumn29,
            this.JianCeZhuangTai,
            this.KaiTingShiKe,
            this.dataGridViewTextBoxColumn30});
            this.dataGridView5.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView5.EnableHeadersVisualStyles = false;
            this.dataGridView5.Location = new System.Drawing.Point(3, 3);
            this.dataGridView5.MultiSelect = false;
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.RowHeadersVisible = false;
            this.dataGridView5.RowTemplate.Height = 23;
            this.dataGridView5.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView5.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView5.Size = new System.Drawing.Size(945, 415);
            this.dataGridView5.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn28
            // 
            this.dataGridViewTextBoxColumn28.HeaderText = "测点编号";
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.HeaderText = "地点";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            // 
            // dataGridViewTextBoxColumn29
            // 
            this.dataGridViewTextBoxColumn29.HeaderText = "名称";
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            // 
            // JianCeZhuangTai
            // 
            this.JianCeZhuangTai.HeaderText = "监测状态";
            this.JianCeZhuangTai.Name = "JianCeZhuangTai";
            // 
            // KaiTingShiKe
            // 
            this.KaiTingShiKe.HeaderText = "开/停时刻";
            this.KaiTingShiKe.Name = "KaiTingShiKe";
            // 
            // dataGridViewTextBoxColumn30
            // 
            this.dataGridViewTextBoxColumn30.HeaderText = "措施";
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.ReadOnly = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridView4);
            this.tabPage4.Location = new System.Drawing.Point(4, 34);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(927, 399);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "模拟量馈电异常列表";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView4.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView4.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView4.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView4.ColumnHeadersHeight = 30;
            this.dataGridView4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24,
            this.KuiDianYiChangShiKe,
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn26});
            this.dataGridView4.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView4.EnableHeadersVisualStyles = false;
            this.dataGridView4.Location = new System.Drawing.Point(3, 3);
            this.dataGridView4.MultiSelect = false;
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.RowHeadersVisible = false;
            this.dataGridView4.RowTemplate.Height = 23;
            this.dataGridView4.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView4.Size = new System.Drawing.Size(941, 415);
            this.dataGridView4.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.HeaderText = "测点编号";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.HeaderText = "地点";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.HeaderText = "名称";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.HeaderText = "单位";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.HeaderText = "报警门限";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.HeaderText = "断电门限";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.HeaderText = "复电门限";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.HeaderText = "监测值";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            // 
            // KuiDianYiChangShiKe
            // 
            this.KuiDianYiChangShiKe.HeaderText = "馈电异常时刻";
            this.KuiDianYiChangShiKe.Name = "KuiDianYiChangShiKe";
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.HeaderText = "工作状态";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.HeaderText = "措施";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView3);
            this.tabPage3.Location = new System.Drawing.Point(4, 34);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(927, 399);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "模拟量断电列表";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView3.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView3.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView3.ColumnHeadersHeight = 30;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.DuanDianShiKe,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16});
            this.dataGridView3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView3.EnableHeadersVisualStyles = false;
            this.dataGridView3.Location = new System.Drawing.Point(3, 3);
            this.dataGridView3.MultiSelect = false;
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.RowTemplate.Height = 23;
            this.dataGridView3.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3.Size = new System.Drawing.Size(928, 415);
            this.dataGridView3.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "测点编号";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "地点";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "名称";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "单位";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "报警门限";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "断电门限";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "复电门限";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "监测值";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            // 
            // DuanDianShiKe
            // 
            this.DuanDianShiKe.HeaderText = "断电时刻";
            this.DuanDianShiKe.Name = "DuanDianShiKe";
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.HeaderText = "工作状态";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.HeaderText = "措施";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(927, 399);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "模拟量报警列表";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView2.ColumnHeadersHeight = 30;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3,
            this.DanWei,
            this.BaoJingMenXian,
            this.DuanDianMenXian,
            this.FuDianMenXian,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.GongZuoZhuangTai,
            this.dataGridViewTextBoxColumn6});
            this.dataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(921, 421);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 170.7976F;
            this.dataGridViewTextBoxColumn2.HeaderText = "测点编号";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 154.1068F;
            this.dataGridViewTextBoxColumn1.HeaderText = "地点";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 124.1424F;
            this.dataGridViewTextBoxColumn3.HeaderText = "名称";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // DanWei
            // 
            this.DanWei.FillWeight = 115.6706F;
            this.DanWei.HeaderText = "单位";
            this.DanWei.Name = "DanWei";
            // 
            // BaoJingMenXian
            // 
            this.BaoJingMenXian.FillWeight = 126.2592F;
            this.BaoJingMenXian.HeaderText = "报警门限";
            this.BaoJingMenXian.Name = "BaoJingMenXian";
            // 
            // DuanDianMenXian
            // 
            this.DuanDianMenXian.FillWeight = 116.1716F;
            this.DuanDianMenXian.HeaderText = "断电门限";
            this.DuanDianMenXian.Name = "DuanDianMenXian";
            // 
            // FuDianMenXian
            // 
            this.FuDianMenXian.FillWeight = 104.4621F;
            this.FuDianMenXian.HeaderText = "复电门限";
            this.FuDianMenXian.Name = "FuDianMenXian";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.FillWeight = 99.57573F;
            this.dataGridViewTextBoxColumn4.HeaderText = "监测值";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.FillWeight = 88.74565F;
            this.dataGridViewTextBoxColumn5.HeaderText = "报警时刻";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // GongZuoZhuangTai
            // 
            this.GongZuoZhuangTai.FillWeight = 76.77388F;
            this.GongZuoZhuangTai.HeaderText = "工作状态";
            this.GongZuoZhuangTai.Name = "GongZuoZhuangTai";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.FillWeight = 93.67615F;
            this.dataGridViewTextBoxColumn6.HeaderText = "措施";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.ItemSize = new System.Drawing.Size(120, 30);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(935, 437);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(927, 399);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "全部告警列表";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.all_cdbh,
            this.all_didian,
            this.all_name,
            this.all_type,
            this.all_val,
            this.all_time,
            this.all_measure});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(918, 393);
            this.dataGridView1.TabIndex = 5;
            // 
            // all_cdbh
            // 
            this.all_cdbh.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.all_cdbh.FillWeight = 1.63192F;
            this.all_cdbh.Frozen = true;
            this.all_cdbh.HeaderText = "测点编号";
            this.all_cdbh.Name = "all_cdbh";
            this.all_cdbh.ReadOnly = true;
            this.all_cdbh.Width = 90;
            // 
            // all_didian
            // 
            this.all_didian.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.all_didian.FillWeight = 2.447879F;
            this.all_didian.Frozen = true;
            this.all_didian.HeaderText = "地点";
            this.all_didian.Name = "all_didian";
            this.all_didian.ReadOnly = true;
            this.all_didian.Width = 200;
            // 
            // all_name
            // 
            this.all_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.all_name.FillWeight = 1.504185F;
            this.all_name.Frozen = true;
            this.all_name.HeaderText = "名称";
            this.all_name.Name = "all_name";
            this.all_name.ReadOnly = true;
            // 
            // all_type
            // 
            this.all_type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.all_type.FillWeight = 1.63192F;
            this.all_type.Frozen = true;
            this.all_type.HeaderText = "告警类型";
            this.all_type.Name = "all_type";
            this.all_type.ReadOnly = true;
            this.all_type.Width = 150;
            // 
            // all_val
            // 
            this.all_val.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.all_val.FillWeight = 1.305536F;
            this.all_val.Frozen = true;
            this.all_val.HeaderText = "状态/监测值";
            this.all_val.Name = "all_val";
            this.all_val.ReadOnly = true;
            this.all_val.Width = 120;
            // 
            // all_time
            // 
            this.all_time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.all_time.FillWeight = 1.63192F;
            this.all_time.Frozen = true;
            this.all_time.HeaderText = "报警时刻";
            this.all_time.Name = "all_time";
            this.all_time.ReadOnly = true;
            this.all_time.Width = 180;
            // 
            // all_measure
            // 
            this.all_measure.FillWeight = 58.62747F;
            this.all_measure.HeaderText = "措施";
            this.all_measure.Name = "all_measure";
            this.all_measure.ReadOnly = true;
            // 
            // CuoShi
            // 
            this.CuoShi.FillWeight = 58.62747F;
            this.CuoShi.HeaderText = "措施";
            this.CuoShi.Name = "CuoShi";
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
            // JianCeZhi
            // 
            this.JianCeZhi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.JianCeZhi.FillWeight = 1.305536F;
            this.JianCeZhi.Frozen = true;
            this.JianCeZhi.HeaderText = "监测值/状态";
            this.JianCeZhi.Name = "JianCeZhi";
            this.JianCeZhi.Width = 120;
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
            // MingCheng
            // 
            this.MingCheng.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MingCheng.FillWeight = 1.504185F;
            this.MingCheng.Frozen = true;
            this.MingCheng.HeaderText = "名称";
            this.MingCheng.Name = "MingCheng";
            this.MingCheng.Width = 150;
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
            // CeDianBianHao
            // 
            this.CeDianBianHao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CeDianBianHao.FillWeight = 1.63192F;
            this.CeDianBianHao.Frozen = true;
            this.CeDianBianHao.HeaderText = "测点编号";
            this.CeDianBianHao.Name = "CeDianBianHao";
            // 
            // List_Alarm_Frame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 437);
            this.Controls.Add(this.tabControl1);
            this.Name = "List_Alarm_Frame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "实时告警列表";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.List_Alarm_Frame_FormClosing);
            this.Load += new System.EventHandler(this.List_Alarm_Frame_Load);
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView7)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        private void List_Alarm_Frame_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer1.Stop();
        }

        private void List_Alarm_Frame_Load(object sender, EventArgs e)
        {
            this.index_select = 0;
            this.timer1.Start();
        }
        private void modifyDataView(int alarmtype, string cedianbianhao, CeDian cedian, string realValue, int state, string startTime, int at)
        {
            int pos;
            int i;
            string str;
            float f;
            int index2;
            DataTable dt;
            bool flag;
            bool flag1;
            bool flag2;
            bool flag3;
            bool flag4;
            bool flag5;
            bool flag6;
            bool flag7;
            DataGridView dgv = this.getDataView(alarmtype);
            bool count = dgv != null;
            if (count)
            {
                int rowno = -1;
                try
                {
                    int num = alarmtype;
                    switch (num)
                    {
                        case 0:
                            {
                                i = 0;
                                while (true)
                                {
                                    count = i < dgv.Rows.Count;
                                    if (!count)
                                    {
                                        break;
                                    }
                                    count = !dgv[0, i].Value.ToString().Equals(cedianbianhao);
                                    if (count)
                                    {
                                        i++;
                                    }
                                    else
                                    {
                                        rowno = i;
                                        break;
                                    }
                                }
                                count = rowno < 0;
                                if (count)
                                {
                                    index2 = this.dataGridView1.Rows.Add();
                                    pos = 0;
                                    int num1 = pos;
                                    pos = num1 + 1;
                                    dgv[num1, index2].Value = cedianbianhao;
                                    int num2 = pos;
                                    pos = num2 + 1;
                                    dgv[num2, index2].Value = cedian.CeDianWeiZhi;
                                    int num3 = pos;
                                    pos = num3 + 1;
                                    dgv[num3, index2].Value = cedian.XiaoleiXing;
                                    int num4 = pos;
                                    pos = num4 + 1;
                                    dgv[num4, index2].Value = CedianAlarm.getAlalarm(at);
                                    str = "";
                                    count = cedian.DaLeiXing != 0;
                                    if (count)
                                    {
                                        f = float.Parse(realValue);
                                        if (cedian.DaLeiXing == 1)
                                        {
                                            str = cedian.getDigiVal((int)f);
                                        }
                                        else if (cedian.DaLeiXing == 2)
                                        {
                                            str = cedian.getCtlVal((int)f);
                                        }      
                                    }
                                    else
                                    {
                                        flag = (state == 4 ? false : state != 7);
                                        count = flag;
                                        str = (count ? string.Concat(this.State(state), "  ", realValue) : this.State(state));
                                    }
                                    int num5 = pos;
                                    pos = num5 + 1;
                                    dgv[num5, index2].Value = str;
                                    int num6 = pos;
                                    pos = num6 + 1;
                                    dgv[num6, index2].Value = startTime;
                                    int num7 = pos;
                                    pos = num7 + 1;
                                    dgv[num7, index2].Value = string.Empty;
                                    this.AllalarmCeDians.Add(cedianbianhao);
                                }
                                else
                                {
                                    str = "";
                                    count = cedian.DaLeiXing != 0;
                                    if (count)
                                    {
                                        f = float.Parse(realValue);
                                        if (cedian.DaLeiXing == 1) 
                                        {                                     
                                            str = cedian.getDigiVal((int)f);      
                                        }
                                        else if (cedian.DaLeiXing == 2)
                                        {
                                            str = cedian.getCtlVal((int)f);     
                                        }                                      
                                    }
                                    else
                                    {
                                        flag1 = (state == 4 ? false : state != 7);
                                        count = flag1;
                                        str = (count ? string.Concat(this.State(state), "  ", realValue) : this.State(state));
                                    }
                                    dgv[4, rowno].Value = str;
                                }
                                break;
                            }
                        case 1:
                            {
                                i = 0;
                                while (true)
                                {
                                    count = i < dgv.Rows.Count;
                                    if (!count)
                                    {
                                        break;
                                    }
                                    count = !dgv[1, i].Value.ToString().Equals(cedianbianhao);
                                    if (count)
                                    {
                                        i++;
                                    }
                                    else
                                    {
                                        rowno = i;
                                        break;
                                    }
                                }
                                count = rowno < 0;
                                if (count)
                                {
                                    index2 = dgv.Rows.Add();
                                    dt = MoNiLiangLeiXing.GetAnalogAlarm(cedianbianhao);
                                    count = dt.Rows.Count <= 0;
                                    if (!count)
                                    {
                                        pos = 0;
                                        int num8 = pos;
                                        pos = num8 + 1;
                                        dgv[num8, index2].Value = cedianbianhao;
                                        int num9 = pos;
                                        pos = num9 + 1;
                                        dgv[num9, index2].Value = cedian.CeDianWeiZhi;
                                        int num10 = pos;
                                        pos = num10 + 1;
                                        dgv[num10, index2].Value = cedian.XiaoleiXing;
                                        int num11 = pos;
                                        pos = num11 + 1;
                                        dgv[num11, index2].Value = dt.Rows[0]["danWei"].ToString();
                                        int num12 = pos;
                                        pos = num12 + 1;
                                        dgv[num12, index2].Value = string.Concat(dt.Rows[0]["baoJingZhiXiaXian"].ToString(), "~", dt.Rows[0]["baoJingZhiShangXian"].ToString());
                                        int num13 = pos;
                                        pos = num13 + 1;
                                        dgv[num13, index2].Value = dt.Rows[0]["duanDianZhi"].ToString();
                                        int num14 = pos;
                                        pos = num14 + 1;
                                        dgv[num14, index2].Value = dt.Rows[0]["fuDianZhi"].ToString();
                                        str = "";
                                        flag2 = (state == 4 ? false : state != 7);
                                        count = flag2;
                                        str = (count ? realValue.ToString() : "");
                                        int num15 = pos;
                                        pos = num15 + 1;
                                        dgv[num15, index2].Value = str;
                                        int num16 = pos;
                                        pos = num16 + 1;
                                        dgv[num16, index2].Value = startTime;
                                        int num17 = pos;
                                        pos = num17 + 1;
                                        dgv[num17, index2].Value = this.State(state);
                                        int num18 = pos;
                                        pos = num18 + 1;
                                        dgv[num18, index2].Value = string.Empty;
                                        this.alarm_analog_data.Add(cedianbianhao);
                                    }
                                }
                                else
                                {
                                    pos = 7;
                                    str = "";
                                    flag3 = (state == 4 ? false : state != 7);
                                    count = flag3;
                                    str = (count ? realValue.ToString() : "");
                                    int num19 = pos;
                                    pos = num19 + 1;
                                    dgv[num19, rowno].Value = str;
                                    pos++;
                                    int num20 = pos;
                                    pos = num20 + 1;
                                    dgv[num20, rowno].Value = this.State(state);
                                }
                                break;
                            }
                        case 2:
                            {
                                i = 0;
                                while (true)
                                {
                                    count = i < dgv.Rows.Count;
                                    if (!count)
                                    {
                                        break;
                                    }
                                    count = !dgv[1, i].Value.ToString().Equals(cedianbianhao);
                                    if (count)
                                    {
                                        i++;
                                    }
                                    else
                                    {
                                        rowno = i;
                                        break;
                                    }
                                }
                                count = rowno < 0;
                                if (count)
                                {
                                    index2 = dgv.Rows.Add();
                                    dt = MoNiLiangLeiXing.GetAnalogAlarm(cedianbianhao);
                                    count = dt.Rows.Count <= 0;
                                    if (!count)
                                    {
                                        pos = 0;
                                        int num21 = pos;
                                        pos = num21 + 1;
                                        dgv[num21, index2].Value = cedianbianhao;
                                        int num22 = pos;
                                        pos = num22 + 1;
                                        dgv[num22, index2].Value = cedian.CeDianWeiZhi;
                                        int num23 = pos;
                                        pos = num23 + 1;
                                        dgv[num23, index2].Value = cedian.XiaoleiXing;
                                        int num24 = pos;
                                        pos = num24 + 1;
                                        dgv[num24, index2].Value = dt.Rows[0]["danWei"].ToString();
                                        int num25 = pos;
                                        pos = num25 + 1;
                                        dgv[num25, index2].Value = string.Concat(dt.Rows[0]["baoJingZhiXiaXian"].ToString(), "~", dt.Rows[0]["baoJingZhiShangXian"].ToString());
                                        int num26 = pos;
                                        pos = num26 + 1;
                                        dgv[num26, index2].Value = dt.Rows[0]["duanDianZhi"].ToString();
                                        int num27 = pos;
                                        pos = num27 + 1;
                                        dgv[num27, index2].Value = dt.Rows[0]["fuDianZhi"].ToString();
                                        str = "";
                                        flag4 = (state == 4 ? false : state != 7);
                                        count = flag4;
                                        str = (count ? realValue.ToString() : "");
                                        int num28 = pos;
                                        pos = num28 + 1;
                                        dgv[num28, index2].Value = str;
                                        int num29 = pos;
                                        pos = num29 + 1;
                                        dgv[num29, index2].Value = startTime;
                                        int num30 = pos;
                                        pos = num30 + 1;
                                        dgv[num30, index2].Value = this.State(state);
                                        int num31 = pos;
                                        pos = num31 + 1;
                                        dgv[num31, index2].Value = string.Empty;
                                        this.alarm_ana_cutoff_data.Add(cedianbianhao);
                                    }
                                }
                                else
                                {
                                    str = "";
                                    flag5 = (state == 4 ? false : state != 7);
                                    count = flag5;
                                    str = (count ? realValue.ToString() : "");
                                    pos = 7;
                                    int num32 = pos;
                                    pos = num32 + 1;
                                    dgv[num32, rowno].Value = str;
                                    pos++;
                                    int num33 = pos;
                                    pos = num33 + 1;
                                    dgv[num33, rowno].Value = this.State(state);
                                }
                                break;
                            }
                        case 3:
                            {
                                i = 0;
                                while (true)
                                {
                                    count = i < dgv.Rows.Count;
                                    if (!count)
                                    {
                                        break;
                                    }
                                    count = !dgv[1, i].Value.ToString().Equals(cedianbianhao);
                                    if (count)
                                    {
                                        i++;
                                    }
                                    else
                                    {
                                        rowno = i;
                                        break;
                                    }
                                }
                                count = rowno < 0;
                                if (count)
                                {
                                    index2 = dgv.Rows.Add();
                                    dt = MoNiLiangLeiXing.GetAnalogAlarm(cedianbianhao);
                                    count = dt.Rows.Count <= 0;
                                    if (!count)
                                    {
                                        pos = 0;
                                        int num34 = pos;
                                        pos = num34 + 1;
                                        dgv[num34, index2].Value = cedianbianhao;
                                        int num35 = pos;
                                        pos = num35 + 1;
                                        dgv[num35, index2].Value = cedian.CeDianWeiZhi;
                                        int num36 = pos;
                                        pos = num36 + 1;
                                        dgv[num36, index2].Value = cedian.XiaoleiXing;
                                        int num37 = pos;
                                        pos = num37 + 1;
                                        dgv[num37, index2].Value = dt.Rows[0]["danWei"].ToString();
                                        int num38 = pos;
                                        pos = num38 + 1;
                                        dgv[num38, index2].Value = string.Concat(dt.Rows[0]["baoJingZhiXiaXian"].ToString(), "~", dt.Rows[0]["baoJingZhiShangXian"].ToString());
                                        int num39 = pos;
                                        pos = num39 + 1;
                                        dgv[num39, index2].Value = dt.Rows[0]["duanDianZhi"].ToString();
                                        int num40 = pos;
                                        pos = num40 + 1;
                                        dgv[num40, index2].Value = dt.Rows[0]["fuDianZhi"].ToString();
                                        str = "";
                                        flag6 = (state == 4 ? false : state != 7);
                                        count = flag6;
                                        str = (count ? realValue.ToString() : "");
                                        int num41 = pos;
                                        pos = num41 + 1;
                                        dgv[num41, index2].Value = str;
                                        int num42 = pos;
                                        pos = num42 + 1;
                                        dgv[num42, index2].Value = startTime;
                                        int num43 = pos;
                                        pos = num43 + 1;
                                        dgv[num43, index2].Value = this.State(state);
                                        int num44 = pos;
                                        pos = num44 + 1;
                                        dgv[num44, index2].Value = string.Empty;
                                        this.alarm_ana_feed_data.Add(cedianbianhao);
                                    }
                                }
                                else
                                {
                                    pos = 7;
                                    str = "";
                                    flag7 = (state == 4 ? false : state != 7);
                                    count = flag7;
                                    str = (count ? realValue.ToString() : "");
                                    int num45 = pos;
                                    pos = num45 + 1;
                                    dgv[num45, rowno].Value = str;
                                    pos++;
                                    int num46 = pos;
                                    pos = num46 + 1;
                                    dgv[num46, rowno].Value = this.State(state);
                                }
                                break;
                            }
                        case 4:
                            {
                                i = 0;
                                while (true)
                                {
                                    count = i < dgv.Rows.Count;
                                    if (!count)
                                    {
                                        break;
                                    }
                                    count = !dgv[1, i].Value.ToString().Equals(cedianbianhao);
                                    if (count)
                                    {
                                        i++;
                                    }
                                    else
                                    {
                                        rowno = i;
                                        break;
                                    }
                                }
                                count = rowno < 0;
                                if (count)
                                {
                                    index2 = dgv.Rows.Add();
                                    pos = 0;
                                    int num47 = pos;
                                    pos = num47 + 1;
                                    dgv[num47, index2].Value = cedianbianhao;
                                    int num48 = pos;
                                    pos = num48 + 1;
                                    dgv[num48, index2].Value = cedian.CeDianWeiZhi;
                                    int num49 = pos;
                                    pos = num49 + 1;
                                    dgv[num49, index2].Value = cedian.XiaoleiXing;
                                    f = float.Parse(realValue);
                                    int num50 = pos;
                                    pos = num50 + 1;
                                    dgv[num50, index2].Value = cedian.getDigiVal((int)f);
                                    int num51 = pos;
                                    pos = num51 + 1;
                                    dgv[num51, index2].Value = startTime;
                                    int num52 = pos;
                                    pos = num52 + 1;
                                    dgv[num52, index2].Value = string.Empty;
                                    this.alarm_digi_data.Add(cedianbianhao);
                                }
                                else
                                {
                                    dgv[3, rowno].Value = this.State(state);
                                }
                                break;
                            }
                        case 5:
                            {
                                i = 0;
                                while (true)
                                {
                                    count = i < dgv.Rows.Count;
                                    if (!count)
                                    {
                                        break;
                                    }
                                    count = !dgv[1, i].Value.ToString().Equals(cedianbianhao);
                                    if (count)
                                    {
                                        i++;
                                    }
                                    else
                                    {
                                        rowno = i;
                                        break;
                                    }
                                }
                                count = rowno < 0;
                                if (count)
                                {
                                    index2 = dgv.Rows.Add();
                                    pos = 0;
                                    int num53 = pos;
                                    pos = num53 + 1;
                                    dgv[num53, index2].Value = cedianbianhao;
                                    int num54 = pos;
                                    pos = num54 + 1;
                                    dgv[num54, index2].Value = cedian.CeDianWeiZhi;
                                    int num55 = pos;
                                    pos = num55 + 1;
                                    dgv[num55, index2].Value = cedian.XiaoleiXing;
                                    f = float.Parse(realValue);
                                    int num56 = pos;
                                    pos = num56 + 1;
                                    dgv[num56, index2].Value = cedian.getDigiVal((int)f);
                                    int num57 = pos;
                                    pos = num57 + 1;
                                    dgv[num57, index2].Value = startTime;
                                    int num58 = pos;
                                    pos = num58 + 1;
                                    dgv[num58, index2].Value = string.Empty;
                                    this.alarm_digi_cutoff_data.Add(cedianbianhao);
                                }
                                else
                                {
                                    dgv[3, rowno].Value = this.State(state);
                                }
                                break;
                            }
                        case 6:
                            {
                                i = 0;
                                while (true)
                                {
                                    count = i < dgv.Rows.Count;
                                    if (!count)
                                    {
                                        break;
                                    }
                                    count = !dgv[1, i].Value.ToString().Equals(cedianbianhao);
                                    if (count)
                                    {
                                        i++;
                                    }
                                    else
                                    {
                                        rowno = i;
                                        break;
                                    }
                                }
                                count = rowno < 0;
                                if (count)
                                {
                                    index2 = dgv.Rows.Add();
                                    pos = 0;
                                    int num59 = pos;
                                    pos = num59 + 1;
                                    dgv[num59, index2].Value = cedianbianhao;
                                    int num60 = pos;
                                    pos = num60 + 1;
                                    dgv[num60, index2].Value = cedian.CeDianWeiZhi;
                                    int num61 = pos;
                                    pos = num61 + 1;
                                    dgv[num61, index2].Value = cedian.XiaoleiXing;
                                    f = float.Parse(realValue);
                                    int num62 = pos;
                                    pos = num62 + 1;
                                    dgv[num62, index2].Value = cedian.getDigiVal((int)f);
                                    int num63 = pos;
                                    pos = num63 + 1;
                                    dgv[num63, index2].Value = startTime;
                                    int num64 = pos;
                                    pos = num64 + 1;
                                    dgv[num64, index2].Value = string.Empty;
                                    this.alarm_digi_feed_data.Add(cedianbianhao);
                                }
                                else
                                {
                                    dgv[3, rowno].Value = this.State(state);
                                }
                                break;
                            }
                    }
                }
                catch
                {
                }
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.index_select = this.tabControl1.SelectedIndex;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.fresh_data();
        }
        
        
        private delegate void DeleteRow(int index, int row, string key);

        private delegate void RefreshDataGridView(int index, string cedianbianhao, CeDian cedian, string val, int state, string date, int at);
    }
}

