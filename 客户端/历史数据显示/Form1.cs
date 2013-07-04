using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace 历史数据显示
{
    public partial class Form1 : Form
    {
        private string ServerIP;
        private string DataBaseConnect;
     
        //存放模拟量和开关量原始数据的结构体
        public struct mokai
        {
            public float uploadvalue;
            public DateTime time;
            public int state;

        }
        //存放控制量原始数据的结构体
        public struct kongzhi
        {
            public int uploadvalue;
            public DateTime time;
            public int state;
        
        }
        //存放数据库数据
        public mokai[] MoKai;
        public kongzhi[] KongZhi;
        //存放当前月份数据
        public  mokai[] month = new mokai[2709400];
        public mokai[] monshuju;
        public kongzhi[] kmonth = new kongzhi[2709400];
        public kongzhi[] kmonshuju;
        //存放所查一段时间数据
        public  mokai[] d;
        public mokai[] c;
        public kongzhi[] kd;
        public kongzhi[] kc;
        private int init_bianhao = 0;
        public static string Current_Param;
        //cedian控件中所存的测点信息和测点id号码
        public List<ListItem> listitem = new List<ListItem>();
        public Dictionary<string , ListItem> cediancom = new Dictionary<string, ListItem>();
        //duandian控件中所存的测点信息和测点id号码
        public List<ListItem> duandianc = new List<ListItem>();
       
        //kuidian控件中所存的测点信息和测点id号码
        public List<ListItem> kuidianc = new List<ListItem>();
       
        public Form1(string s)
        {
            InitializeComponent();
            this.dateTimeChooser1.StartTime = new System.DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0,0);
            this.dateTimeChooser1.EndTime = DateTime.Now;
            this.init_bianhao = 0;
            Current_Param = s;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetCfgInfo();
            db.Connet(this.DataBaseConnect);
        
            cedianxianshi();

            if (Current_Param != "")
            {
                Init_Start();
            }

        }
        private void GetCfgInfo()
        {
            string[] lines = File.ReadAllLines(System.Environment.CurrentDirectory + "\\cfg.txt");
            for (int i = 0; i < lines.Length; i++)
            { 
                string temp=lines[i];
                int m = temp.IndexOf('%');
                string key = temp.Substring(0, m);
                string value = temp.Substring(m + 1);
                if (key == "ServerIP")
                    this.ServerIP = value;
                if (key == "DataBaseConnect")
                    this.DataBaseConnect = value;

            
            }

        }

        //取出所有测点
        private void cedianxianshi()
        {
            cediancom.Clear();
            string sql = string.Format(@"select cedianbianhao ,cedianweizhi,xiaoleixing,id from cedian where weishanchu=1 order by cedianbianhao");
            DataTable dt=db.Select(sql);
            List<string> ret = new List<string>();           
            for (int i = 0; i < dt.Rows.Count; i++)
            { 

             dt.Rows[i]["cedianbianhao"]=dt.Rows[i]["cedianbianhao"].ToString().Trim()+" "+dt.Rows[i]["cedianweizhi"].ToString().Trim()+" "+dt.Rows[i]["xiaoleixing"].ToString().Trim();
             string info = dt.Rows[i]["cedianbianhao"].ToString().Trim();
             int  id =Convert.ToInt32(dt.Rows[i]["id"].ToString().Trim());
             ListItem list = new ListItem(info, id);
             cediancom.Add(list.ToString(), list);
             ret.Add(list.ToString());
         
            }
            string sql2 = string.Format(@"select ceDianBianHao,ceDianWeiZhi,mingCheng,id from KongZhiLiangCeDian where weiShanChu=1 order by cedianbianhao");
            DataTable dt2 = db.Select(sql2);
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                dt2.Rows[i]["ceDianBianHao"] = dt2.Rows[i]["ceDianBianHao"].ToString().Trim() + " " + dt2.Rows[i]["ceDianWeiZhi"].ToString().Trim() + " " + dt2.Rows[i]["mingCheng"].ToString().Trim();
                string info = dt2.Rows[i]["cedianbianhao"].ToString().Trim();
                int id = Convert.ToInt32(dt2.Rows[i]["id"].ToString().Trim());
                ListItem list = new ListItem(info, id);
                cediancom.Add(list.ToString(), list);
                ret.Add(list.ToString());
            }
            dt.Columns.Remove("cedianweizhi");
            dt.Columns.Remove("xiaoleixing");
            ret.Sort();
            string[] ss = ret.ToArray();
            this.cedainkongjian.Items.Clear();
            this.cedainkongjian.Items.AddRange(ss);      
        }

     
        //获得开关量原始数据并把原始数据转化成所需要的数据
        private mokai[] getkaiguan( string cdbh)
        {
            int id = 0;
            string cedianbianhaohao = cdbh.Substring(0, 5);
            string yue = String.Empty;
            MoKai = null;
            if ( this.dateTimeChooser1.StartTime.Month != this.dateTimeChooser1.EndTime.Month)
            {
                MessageBox.Show("查询时间不能跨月！");
                return c ;            
            }
            if (this.dateTimeChooser1.StartTime >= this.dateTimeChooser1.EndTime)
            {
                MessageBox.Show("开始时间大于结束时间，请重新选择！");
                return c;
            }
            if(this.dateTimeChooser1.EndTime>System.DateTime.Now)
            {
            
                
              MessageBox.Show("结束时间大于当前时间，请重新选择！");
                return c ;
            }
            
            if ( this.dateTimeChooser1.StartTime.Month < 10)
            {
                yue = '0' + this.dateTimeChooser1.StartTime.Month.ToString().Trim();
            }
            else
                yue = this.dateTimeChooser1.StartTime.Month.ToString().Trim();
            
            string sql2 = "select id from cedian where cedianbianhao='" + cedianbianhaohao + "'and weishanchu=1";
            DataTable dt2=db.Select(sql2);
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                id = Convert.ToInt32(dt2.Rows[0][0].ToString().Trim());
            }
            else
            {
                return c;
            }
            string sql = "select uploadvalue,uploadtime,state from KaiGuanLiangValue" + this.dateTimeChooser1.StartTime.ToString().Trim().Substring(0, 4) + "_" + yue + " where uploadtime >= '" + this.dateTimeChooser1.StartTime + "' and uploadtime<='" + this.dateTimeChooser1.EndTime + "' and ceDianID="+id+" order by uploadtime";
            DataTable dt=db.Select(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                //从数据库中取出的数据放入结构体shu中
                //StartTime = Convert.ToDateTime(dt.Rows[0]["uploadtime"].ToString());
                MoKai = new mokai[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //if (i == 0)
                    //{
                    //    StartTime = Convert.ToDateTime(dt.Rows[i]["uploadtime"].ToString());
                    //}
                    MoKai[i].uploadvalue = Convert.ToSingle(dt.Rows[i]["uploadvalue"].ToString());
                    MoKai[i].time = Convert.ToDateTime(dt.Rows[i]["uploadtime"].ToString());
                    MoKai[i].state = Convert.ToInt32(dt.Rows[i]["state"].ToString());
                
                    
                }
            }
            if (MoKai == null) return MoKai;
            DateTime StartTime;
            TimeSpan EndTime;
            List<mokai> in_mokai = new List<mokai>();
            mokai tmp_mokai = new mokai();
            int span_fen;
            if (MoKai.Length > 0)
            {
                in_mokai.Add(MoKai[0]);
                StartTime = MoKai[0].time;
                StartTime = StartTime.AddMinutes(30);
                for (int j = 0; j < MoKai.Length; j++)
                {
                    if (j == 0)
                    {
                        tmp_mokai.state = MoKai[0].state;
                        tmp_mokai.time = StartTime;
                        tmp_mokai.uploadvalue = MoKai[0].uploadvalue;
                        continue;
                    }
                    if (MoKai[j].time > StartTime)
                    {
                        in_mokai.Add(tmp_mokai);
                        EndTime = MoKai[j].time - StartTime;
                        //span_fen = EndTime.TotalMinutes;
                        span_fen = (int)EndTime.TotalMinutes / 30;
                        StartTime = StartTime.AddMinutes(30);
                        tmp_mokai.time = StartTime;
                        for (int k = 0; k < span_fen; k++)
                        {
                            in_mokai.Add(tmp_mokai);
                            StartTime = StartTime.AddMinutes(30);
                            tmp_mokai.time = StartTime;
                        }
                        tmp_mokai.time = MoKai[j].time;
                        tmp_mokai.state = MoKai[j].state;
                        tmp_mokai.uploadvalue = MoKai[j].uploadvalue;
                        in_mokai.Add(tmp_mokai);
                    }
                    else
                    {
                        in_mokai.Add(MoKai[j]);
                        tmp_mokai.state = MoKai[j].state;
                        tmp_mokai.uploadvalue = MoKai[j].uploadvalue;
                        tmp_mokai.time = StartTime;
                    }
                }
                int fenzhong = StartTime.Hour;
                int miao = StartTime.Minute;
                if (StartTime <= this.dateTimeChooser1.EndTime)
                {
                    while (fenzhong <= 23)
                    {
                        in_mokai.Add(tmp_mokai);
                        StartTime = StartTime.AddMinutes(30);
                        tmp_mokai.time = StartTime;
                        fenzhong = StartTime.Hour;
                        miao = StartTime.Minute;
                        if (StartTime > this.dateTimeChooser1.EndTime) break;
                    }
                }
            }
            MoKai = new mokai[in_mokai.Count];
            for (int m = 0; m < in_mokai.Count; m++)
            {
                MoKai[m].time = in_mokai[m].time;
                MoKai[m].state = in_mokai[m].state;
                MoKai[m].uploadvalue = in_mokai[m].uploadvalue;
            }
                return MoKai;

            //    if (dt.Rows.Count > 50)
            //    {
            //        return MoKai;
            //    }
            //    else
            //    {
            //        //新建一个结构体存放当前查询月份的数据
            //        DateTime monthstart = new DateTime(this.dateTimeChooser1.StartTime.Year, this.dateTimeChooser1.StartTime.Month, 1, 0, 0, 0);
            //        string sql3 = "select uploadvalue,uploadtime,state from KaiGuanLiangValue" + this.dateTimeChooser1.StartTime.ToString().Trim().Substring(0, 4) + "_" + yue + " where cedianid=" + id + " order by uploadtime";
            //        DataTable dt3 = db.Select(sql3);
            //        monshuju = new mokai[dt3.Rows.Count];
            //        for (int i = 0; i < dt3.Rows.Count; i++)
            //        {
            //            monshuju[i].uploadvalue = Convert.ToSingle(dt3.Rows[i]["uploadvalue"].ToString());
            //            monshuju[i].time = Convert.ToDateTime(dt3.Rows[i]["uploadtime"].ToString());
            //            monshuju[i].state = Convert.ToInt32(dt3.Rows[i]["state"].ToString());

            //        }
            //        for (int i = 0; i < 2709400; i++)
            //        {
            //            month[i].time = monthstart.AddSeconds(i);
            //        }
            //        //新建一个结构体存放每秒中的数据
            //        int lengh = (int)this.dateTimeChooser1.EndTime.Subtract(this.dateTimeChooser1.StartTime).TotalSeconds + 1;
            //        mokai[] d = new mokai[lengh];
            //        for (int i = 0; i < lengh; i++)
            //        {
            //            d[i].time = this.dateTimeChooser1.StartTime.AddSeconds(i);

            //        }
            //        //假如一次查询只有一个数据
            //        if (dt3.Rows.Count == 1)
            //        {
            //            for (int i = 0; i < d.Length; i++)
            //            {
            //                month[i].state = MoKai[0].state;
            //                month[i].uploadvalue = MoKai[0].uploadvalue;
            //            }
            //        }
            //        else
            //        {
            //            //给一段时间赋值开始的位置
            //            int start = 0;
            //            //给一段时间赋值结束的位置
            //            int end = 0;
            //            //假如一天有两个以上的数据
            //            for (int j = 0; j < monshuju.Length; j++)
            //            {
            //                for (int i = end; i < month.Length; i++)
            //                {
            //                    if (monshuju[j].time == month[i].time)
            //                    {
            //                        //相等的数据是第一条数据
            //                        if (j == 0 && i == 0)
            //                        {
            //                            month[i].state = monshuju[j].state;
            //                            month[i].uploadvalue = monshuju[j].uploadvalue;
            //                            end = i + 1;
            //                        }
            //                        //相等的数据不是month结构体里的第一条数据，但是是数据库查询的第一个条数据
            //                        if (j == 0 && i != 0)
            //                        {
            //                            start = 0;
            //                            end = i;
            //                            for (int k = 0; k < end; k++)
            //                            {
            //                                month[k].state = monshuju[j].state;
            //                                month[k].uploadvalue = monshuju[j].uploadvalue;

            //                            }
            //                        }
            //                        //相等的数据不是数据库中查询的第一条数据，也不是month结构体里的第一条数据,也不是数据库中查询的最后一条数据
            //                        if (j != 0 && i != 0 && j != monshuju.Length - 1)
            //                        {
            //                            start = end;
            //                            end = i;
            //                            for (int k = start; k < end; k++)
            //                            {
            //                                month[k].state = monshuju[j - 1].state;
            //                                month[k].uploadvalue = monshuju[j - 1].uploadvalue;
            //                            }


            //                        }
            //                        //相等的数据不是数据库中查询的第一条数据，也不是month结构体里的第一条数据,是数据库中查询的最后一条数据
            //                        if (j != 0 && i != 0 && j == monshuju.Length - 1)
            //                        {
            //                            start = end;
            //                            end = i;
            //                            for (int k = start; k < end; k++)
            //                            {
            //                                month[k].state = monshuju[j - 1].state;
            //                                month[k].uploadvalue = monshuju[j - 1].uploadvalue;

            //                            }
            //                            for (int n = end; n < month.Length; n++)
            //                            {
            //                                month[n].state = monshuju[j].state;
            //                                month[n].uploadvalue = monshuju[j].uploadvalue;
            //                            }
            //                        }
                                   



            //                    }
            //                    break;

            //                }

            //            }


            //        }
            //        //把整月的历史数据存放到查询时间段的历史数据结构体中
            //        for (int i = 0; i < month.Length; i++)
            //        {
            //            if (month[i].time == d[0].time)
            //            {
            //                for (int k = 0; k < lengh; k++)
            //                {
            //                    d[k].state = month[i + k].state;
            //                    d[k].uploadvalue = month[i + k].uploadvalue;
            //                }
            //            }
            //        }
            //        //删除不需要的数据
            //        List<mokai> zong = new List<mokai>();
            //        List<mokai> shujuku = MoKai.ToList<mokai>();
            //        //标志变量赋值为true，表明是数据库中的数据。
            //        bool[] flag = new bool[d.Length];
            //        for (int i = 0; i < d.Length; i++)
            //        {
            //            flag[i] = shujuku.Contains(d[i])  ;
            //            if (flag[i] == true || ((d[i].time.Minute == 30 || d[i].time.Minute == 0) && d[i].time.Second == 0))
            //            {
            //                zong.Add(d[i]);
            //            }

            //        }
            //        //把符合要求的泛型转化成数组
            //        d = zong.ToArray();

            //        return d;

            //    }            
            //}
            //else
            //{
            //    return c;
            //}
            
        }
        //获得模拟量原始数据并把原始数据转化成所需的数据
        private mokai[] getmoni(string cdbh )
        {
            int id = 0;
            string cedianbianhaohao = cdbh.Substring(0, 5);
            string yue = String.Empty;
            MoKai = null;
            if (this.dateTimeChooser1.StartTime.Month != this.dateTimeChooser1.EndTime.Month)
            {
                MessageBox.Show("查询时间不能跨月！");
                return c;
            }
            if (this.dateTimeChooser1.StartTime >= this.dateTimeChooser1.EndTime)
            {
                MessageBox.Show("开始时间大于结束时间，请重新选择！");
                return c;
            }
            if (this.dateTimeChooser1.EndTime > System.DateTime.Now)
            {


                MessageBox.Show("结束时间大于当前时间，请重新选择！");
                return c;
            }

            if (this.dateTimeChooser1.StartTime.Month < 10)
            {
                yue = '0' + this.dateTimeChooser1.StartTime.Month.ToString().Trim();
            }
            else
                yue = this.dateTimeChooser1.StartTime.Month.ToString().Trim();
            string sql2 = "select id from cedian where cedianbianhao='" + cedianbianhaohao + "'and weishanchu=1";
            DataTable dt2 = db.Select(sql2);
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                id = Convert.ToInt32(dt2.Rows[0][0].ToString().Trim());
            }
            else
            {
                return c;
            }
            string sql = "select uploadvalue,uploadtime,state from MoNiLiangValue" + this.dateTimeChooser1.StartTime.ToString().Trim().Substring(0, 4) + "_" + yue + " where uploadtime >= '" + this.dateTimeChooser1.StartTime + "' and uploadtime<='" + this.dateTimeChooser1.EndTime + "' and ceDianID=" + id + " order by uploadtime";
            DataTable dt = db.Select(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                //从数据库中取出的数据放入结构体shu中
                
                MoKai = new mokai[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MoKai[i].uploadvalue = Convert.ToSingle(dt.Rows[i]["uploadvalue"].ToString());
                    MoKai[i].time = Convert.ToDateTime(dt.Rows[i]["uploadtime"].ToString());
                    MoKai[i].state = Convert.ToInt32(dt.Rows[i]["state"].ToString());


                }
            }
                //if (dt.Rows.Count > 50)
                //{
            if (MoKai == null) return MoKai;
            DateTime StartTime;
            TimeSpan EndTime;
            List<mokai> in_mokai = new List<mokai>();
            mokai tmp_mokai = new mokai();
            int span_fen;
            if (MoKai.Length > 0)
            {
                in_mokai.Add(MoKai[0]);
                StartTime = MoKai[0].time;
                StartTime = StartTime.AddMinutes(30);
                for (int j = 0; j < MoKai.Length; j++)
                {
                    if (j == 0)
                    {
                        tmp_mokai.state = MoKai[0].state;
                        tmp_mokai.time = StartTime;
                        tmp_mokai.uploadvalue = MoKai[0].uploadvalue;
                        continue;
                    }
                    if (MoKai[j].time > StartTime)
                    {
                        in_mokai.Add(tmp_mokai);
                        EndTime = MoKai[j].time - StartTime;
                        //span_fen = EndTime.TotalMinutes;
                        span_fen = (int)EndTime.TotalMinutes / 30;
                        StartTime = StartTime.AddMinutes(30);
                        tmp_mokai.time = StartTime;
                        for (int k = 0; k < span_fen; k++)
                        {
                            in_mokai.Add(tmp_mokai);
                            StartTime = StartTime.AddMinutes(30);
                            tmp_mokai.time = StartTime;
                        }
                        
                        tmp_mokai.time = MoKai[j].time;
                        tmp_mokai.state = MoKai[j].state;
                        tmp_mokai.uploadvalue = MoKai[j].uploadvalue;
                        in_mokai.Add(tmp_mokai);
                        
                    }
                    else
                    {
                        in_mokai.Add(MoKai[j]);
                        tmp_mokai.state = MoKai[j].state;
                        tmp_mokai.uploadvalue = MoKai[j].uploadvalue;
                        tmp_mokai.time = StartTime;
                    }
                }
                int fenzhong = StartTime.Hour;
                int miao = StartTime.Minute;
                if (StartTime <= this.dateTimeChooser1.EndTime)
                {
                    while (fenzhong <= 23)
                    {
                        in_mokai.Add(tmp_mokai);
                        StartTime = StartTime.AddMinutes(30);
                        tmp_mokai.time = StartTime;
                        fenzhong = StartTime.Hour;
                        miao = StartTime.Minute;
                        if (StartTime > this.dateTimeChooser1.EndTime) break;
                    }
                }
            }
            MoKai = new mokai[in_mokai.Count];
            for (int m = 0; m < in_mokai.Count; m++)
            {
                MoKai[m].time = in_mokai[m].time;
                MoKai[m].state = in_mokai[m].state;
                MoKai[m].uploadvalue = in_mokai[m].uploadvalue;
            }
                    return MoKai;
                //}
            //    else
            //    {
            //        //新建一个结构体存放当前查询月份的数据
            //        DateTime monthstart = new DateTime(this.dateTimeChooser1.StartTime.Year, this.dateTimeChooser1.StartTime.Month, 1, 0, 0, 0);
            //        string sql3 = "select uploadvalue,uploadtime,state from MoNiLiangValue" + this.dateTimeChooser1.StartTime.ToString().Trim().Substring(0, 4) + "_" + yue + " where cedianid=" + id + " order by uploadtime";
            //        DataTable dt3 = db.Select(sql3);
            //        monshuju = new mokai[dt3.Rows.Count];
            //        for (int i = 0; i < dt3.Rows.Count; i++)
            //        {
            //            monshuju[i].uploadvalue = Convert.ToSingle(dt3.Rows[i]["uploadvalue"].ToString());
            //            monshuju[i].time = Convert.ToDateTime(dt3.Rows[i]["uploadtime"].ToString());
            //            monshuju[i].state = Convert.ToInt32(dt3.Rows[i]["state"].ToString());

            //        }
            //        for (int i = 0; i < 2709400; i++)
            //        {
            //            month[i].time = monthstart.AddSeconds(i);
            //        }
            //        //新建一个结构体存放每秒中的数据
            //        int lengh = (int)this.dateTimeChooser1.EndTime.Subtract(this.dateTimeChooser1.StartTime).TotalSeconds + 1;
            //        mokai[] d = new mokai[lengh];
            //        for (int i = 0; i < lengh; i++)
            //        {
            //            d[i].time = this.dateTimeChooser1.StartTime.AddSeconds(i);

            //        }
            //        //假如一次查询只有一个数据
            //        if (dt3.Rows.Count == 1)
            //        {
            //            for (int i = 0; i < d.Length; i++)
            //            {
            //                month[i].state = MoKai[0].state;
            //                month[i].uploadvalue = MoKai[0].uploadvalue;
            //            }
            //        }
            //        else
            //        {
            //            //给一段时间赋值开始的位置
            //            int start = 0;
            //            //给一段时间赋值结束的位置
            //            int end = 0;
            //            //假如一天有两个以上的数据
            //            for (int j = 0; j < monshuju.Length; j++)
            //            {
            //                for (int i = end; i < month.Length; i++)
            //                {
            //                    if (monshuju[j].time == month[i].time)
            //                    {
            //                        //相等的数据是第一条数据
            //                        if (j == 0 && i == 0)
            //                        {
            //                            month[i].state = monshuju[j].state;
            //                            month[i].uploadvalue = monshuju[j].uploadvalue;
            //                            end = i + 1;
            //                        }
            //                        //相等的数据不是month结构体里的第一条数据，但是是数据库查询的第一个条数据
            //                        if (j == 0 && i != 0)
            //                        {
            //                            start = 0;
            //                            end = i;
            //                            for (int k = 0; k < end; k++)
            //                            {
            //                                month[k].state = monshuju[j].state;
            //                                month[k].uploadvalue = monshuju[j].uploadvalue;

            //                            }
            //                        }
            //                        //相等的数据不是数据库中查询的第一条数据，也不是month结构体里的第一条数据,也不是数据库中查询的最后一条数据
            //                        if (j != 0 && i != 0 && j != monshuju.Length - 1)
            //                        {
            //                            start = end;
            //                            end = i;
            //                            for (int k = start; k < end; k++)
            //                            {
            //                                month[k].state = monshuju[j - 1].state;
            //                                month[k].uploadvalue = monshuju[j - 1].uploadvalue;
            //                            }


            //                        }
            //                        //相等的数据不是数据库中查询的第一条数据，也不是month结构体里的第一条数据,是数据库中查询的最后一条数据
            //                        if (j != 0 && i != 0 && j == monshuju.Length - 1)
            //                        {
            //                            start = end;
            //                            end = i;
            //                            for (int k = start; k < end; k++)
            //                            {
            //                                month[k].state = monshuju[j - 1].state;
            //                                month[k].uploadvalue = monshuju[j - 1].uploadvalue;

            //                            }
            //                            for (int n = end; n < month.Length; n++)
            //                            {
            //                                month[n].state = monshuju[j].state;
            //                                month[n].uploadvalue = monshuju[j].uploadvalue;
            //                            }
            //                        }
                              


            //                    }
            //                    break;

            //                }

            //            }


            //        }
            //        //把整月的历史数据存放到查询时间段的历史数据结构体中
            //        for (int i = 0; i < month.Length; i++)
            //        {
            //            if (month[i].time == d[0].time)
            //            {
            //                for (int k = 0; k < lengh; k++)
            //                {
            //                    d[k].state = month[i + k].state;
            //                    d[k].uploadvalue = month[i + k].uploadvalue;
            //                }
            //            }
            //        }
            //        //删除不需要的数据
            //        List<mokai> zong = new List<mokai>();
            //        List<mokai> shujuku = MoKai.ToList<mokai>();
            //        //标志变量赋值为true，表明是数据库中的数据。
            //        bool[] flag = new bool[d.Length];
            //        for (int i = 0; i < d.Length; i++)
            //        {
            //            flag[i] = shujuku.Contains(d[i]);
            //            if (flag[i] == true || ((d[i].time.Minute == 30 || d[i].time.Minute == 0) && d[i].time.Second == 0))
            //            {
            //                zong.Add(d[i]);
            //            }

            //        }
            //        //把符合要求的泛型转化成数组
            //        d = zong.ToArray();

            //        return d;


            //    }
            //}
            //else
            //{
            //    return c;
            //}
        
        }
        //获得控制量原始数据并转化成所需要的数据
        private kongzhi[] getkongzhi(string cdbh)
        {
            int fenzhanhao = 0;
            string cedianbianhaohao = cdbh.Substring(0, 5);
            string kongzhiliangbianhao = cdbh.Substring(4, 1);
            string yue = String.Empty;
            KongZhi = null;
            if (this.dateTimeChooser1.StartTime.Month != this.dateTimeChooser1.EndTime.Month)
            {
                MessageBox.Show("查询时间不能跨月！");
                return kc;
            }
            if (this.dateTimeChooser1.StartTime >= this.dateTimeChooser1.EndTime)
            {
                MessageBox.Show("开始时间大于结束时间，请重新选择！");
                return kc;
            }
            if (this.dateTimeChooser1.EndTime > System.DateTime.Now)
            {


                MessageBox.Show("结束时间大于当前时间，请重新选择！");
                return kc;
            }
            string sql2 = "select fenZhanHao,kongZhiLiangBianHao from KongZhiLiangCeDian where cedianbianhao='" + cedianbianhaohao + "'and weishanchu=1";
            DataTable dt2 = db.Select(sql2);
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                fenzhanhao = Convert.ToInt32(dt2.Rows[0][0].ToString().Trim());
            }
            else
            {
                return kc;
            }
            string sql = "select uploadvalue,uploadtime,state from KongZhiLiangValue  where uploadtime >= '" + this.dateTimeChooser1.StartTime + "' and uploadtime<='" + this.dateTimeChooser1.EndTime + "' and fenZhanHao="+fenzhanhao+" and ceDianBianHao="+kongzhiliangbianhao+"  order by uploadtime";
            DataTable dt = db.Select(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                //从数据库中取出的数据放入结构体shu中
                KongZhi = new kongzhi[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    KongZhi[i].uploadvalue = Convert.ToInt32(dt.Rows[i]["uploadvalue"].ToString());
                    KongZhi[i].time = Convert.ToDateTime(dt.Rows[i]["uploadtime"].ToString());
                    if (dt.Rows[i]["state"].ToString().Trim() == string.Empty)
                    {
                        KongZhi[i].state = 0;
                    }
                    else
                    {
                        KongZhi[i].state = Convert.ToInt32(dt.Rows[i]["state"].ToString());
                    }

                }
                //if (dt.Rows.Count > 50)
                //{
                
            }
            if (KongZhi == null) return KongZhi;
            DateTime StartTime;
            TimeSpan EndTime;
            List<kongzhi> in_mokai = new List<kongzhi>();
            kongzhi tmp_mokai = new kongzhi();
            int span_fen;
            if (KongZhi.Length > 0)
            {
                in_mokai.Add(KongZhi[0]);
                StartTime = KongZhi[0].time;
                StartTime = StartTime.AddMinutes(30);
                for (int j = 0; j < KongZhi.Length; j++)
                {
                    if (j == 0)
                    {
                        tmp_mokai.state = KongZhi[0].state;
                        tmp_mokai.time = StartTime;
                        tmp_mokai.uploadvalue = KongZhi[0].uploadvalue;
                        continue;
                    }
                    if (KongZhi[j].time > StartTime)
                    {
                        in_mokai.Add(tmp_mokai);
                        EndTime = KongZhi[j].time - StartTime;
                        //span_fen = EndTime.TotalMinutes;
                        span_fen = (int)EndTime.TotalMinutes / 30;
                        StartTime = StartTime.AddMinutes(30);
                        tmp_mokai.time = StartTime;
                        for (int k = 0; k < span_fen; k++)
                        {
                            in_mokai.Add(tmp_mokai);
                            StartTime = StartTime.AddMinutes(30);
                            tmp_mokai.time = StartTime;
                        }
                        tmp_mokai.time = KongZhi[j].time;
                        tmp_mokai.state = KongZhi[j].state;
                        tmp_mokai.uploadvalue = KongZhi[j].uploadvalue;
                        in_mokai.Add(tmp_mokai);
                    }
                    else
                    {
                        in_mokai.Add(KongZhi[j]);
                        tmp_mokai.state = KongZhi[j].state;
                        tmp_mokai.uploadvalue = KongZhi[j].uploadvalue;
                        tmp_mokai.time = StartTime;
                    }
                }
                int fenzhong = StartTime.Hour;
                int miao = StartTime.Minute;
                if (StartTime <= this.dateTimeChooser1.EndTime)
                {
                    while (fenzhong <= 23)
                    {
                        in_mokai.Add(tmp_mokai);
                        StartTime = StartTime.AddMinutes(30);
                        tmp_mokai.time = StartTime;
                        fenzhong = StartTime.Hour;
                        miao = StartTime.Minute;
                        if (StartTime > this.dateTimeChooser1.EndTime) break;
                    }
                }
            }
            KongZhi = new kongzhi[in_mokai.Count];
            for (int m = 0; m < in_mokai.Count; m++)
            {
                KongZhi[m].time = in_mokai[m].time;
                KongZhi[m].state = in_mokai[m].state;
                KongZhi[m].uploadvalue = in_mokai[m].uploadvalue;
            }

            return KongZhi;    //}
                //else
                //{
                //    //新建一个结构体存放当前查询月份的数据
                //    DateTime monthstart = new DateTime(this.dateTimeChooser1.StartTime.Year, this.dateTimeChooser1.StartTime.Month, 1, 0, 0, 0);
                //    DateTime monthend = monthstart.AddDays(31);
                //    string sql3 = "select uploadvalue,uploadtime,state from KongZhiLiangValue where fenZhanHao=" + fenzhanhao + " and ceDianBianHao=" + kongzhiliangbianhao + "  and  uploadtime >= '" + monthstart + "' and uploadtime<='" + monthend + "' order by uploadtime";
                //    DataTable dt3 = db.Select(sql3);
                //    kmonshuju = new kongzhi[dt3.Rows.Count];
                //    for (int i = 0; i < dt3.Rows.Count; i++)
                //    {
                //        kmonshuju[i].uploadvalue = Convert.ToInt32(dt3.Rows[i]["uploadvalue"]);
                //        kmonshuju[i].time = Convert.ToDateTime(dt3.Rows[i]["uploadtime"].ToString());
                //        if (dt3.Rows[i]["state"].ToString().Trim() == string.Empty)
                //        {
                //            kmonshuju[i].state = 0;
                //        }
                //        else
                //        {
                //            kmonshuju[i].state = Convert.ToInt32(dt3.Rows[i]["state"]);
                //        }

                //    }
                //    for (int i = 0; i < 2709400; i++)
                //    {
                //        kmonth[i].time = monthstart.AddSeconds(i);
                //    }
                //    //新建一个结构体存放每秒中的数据
                //    int lengh = (int)this.dateTimeChooser1.EndTime.Subtract(this.dateTimeChooser1.StartTime).TotalSeconds + 1;
                //    kongzhi[]  kd = new kongzhi[lengh];
                //    for (int i = 0; i < lengh; i++)
                //    {
                //        kd[i].time = this.dateTimeChooser1.StartTime.AddSeconds(i);

                //    }
                //    //假如一次查询只有一个数据
                //    if (dt3.Rows.Count == 1)
                //    {
                //        for (int i = 0; i < kd.Length; i++)
                //        {
                //            kmonth[i].state = KongZhi[0].state;
                //            kmonth[i].uploadvalue = KongZhi[0].uploadvalue;
                //        }
                //    }
                //    else
                //    {
                //        //给一段时间赋值开始的位置
                //        int start = 0;
                //        //给一段时间赋值结束的位置
                //        int end = 0;
                //        //假如一天有两个以上的数据
                //        for (int j = 0; j < kmonshuju.Length; j++)
                //        {
                //            for (int i = end; i < kmonth.Length; i++)
                //            {
                //                if (kmonshuju[j].time == kmonth[i].time)
                //                {
                //                    //相等的数据是第一条数据
                //                    if (j == 0 && i == 0)
                //                    {
                //                        kmonth[i].state = kmonshuju[j].state;
                //                        kmonth[i].uploadvalue = kmonshuju[j].uploadvalue;
                //                        end = i + 1;
                //                    }
                //                    //相等的数据不是month结构体里的第一条数据，但是是数据库查询的第一个条数据
                //                    if (j == 0 && i != 0)
                //                    {
                //                        start = 0;
                //                        end = i;
                //                        for (int k = 0; k < end; k++)
                //                        {
                //                            kmonth[k].state = kmonshuju[j].state;
                //                            kmonth[k].uploadvalue = kmonshuju[j].uploadvalue;

                //                        }
                //                    }
                //                    //相等的数据不是数据库中查询的第一条数据，也不是month结构体里的第一条数据,也不是数据库中查询的最后一条数据
                //                    if (j != 0 && i != 0 && j != kmonshuju.Length - 1)
                //                    {
                //                        start = end;
                //                        end = i;
                //                        for (int k = start; k < end; k++)
                //                        {
                //                            kmonth[k].state = kmonshuju[j - 1].state;
                //                            kmonth[k].uploadvalue = kmonshuju[j - 1].uploadvalue;
                //                        }


                //                    }
                //                    //相等的数据不是数据库中查询的第一条数据，也不是month结构体里的第一条数据,是数据库中查询的最后一条数据
                //                    if (j != 0 && i != 0 && j == kmonshuju.Length - 1)
                //                    {
                //                        start = end;
                //                        end = i;
                //                        for (int k = start; k < end; k++)
                //                        {
                //                            kmonth[k].state = kmonshuju[j - 1].state;
                //                            kmonth[k].uploadvalue = kmonshuju[j - 1].uploadvalue;

                //                        }
                //                        for (int n = end; n < kmonth.Length; n++)
                //                        {
                //                            kmonth[n].state = kmonshuju[j].state;
                //                            kmonth[n].uploadvalue = kmonshuju[j].uploadvalue;
                //                        }
                //                    }
                //                    break;
                //                }
                                
                //            }
                //        }
                //    }
                    //把整月的历史数据存放到查询时间段的历史数据结构体中
            //        for (int i = 0; i < kmonth.Length; i++)
            //        {
            //            if (kmonth[i].time == kd[0].time)
            //            {
            //                for (int k = 0; k < lengh; k++)
            //                {
            //                    kd[k].state = kmonth[i + k].state;
            //                    kd[k].uploadvalue = kmonth[i + k].uploadvalue;
            //                }
            //            }
            //        }
            //        //删除不需要的数据
            //        List<kongzhi> zong = new List<kongzhi>();
            //        List<kongzhi> shujuku = KongZhi.ToList<kongzhi>();
            //        //标志变量赋值为true，表明是数据库中的数据。
            //        bool[] flag = new bool[kd.Length];
            //        for (int i = 0; i < kd.Length; i++)
            //        {
            //            flag[i] = shujuku.Contains(kd[i]);
            //            if (flag[i] == true || ((kd[i].time.Minute == 30 || kd[i].time.Minute == 0) && kd[i].time.Second == 0))
            //            {
            //                zong.Add(kd[i]);
            //            }

            //        }
            //        //把符合要求的泛型转化成数组
            //        kd = zong.ToArray();

            //        return kd;


            //    }
            //}
            //else
            //{
            //    return kc;
            //}
        
        }
            

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dateTimeChooser1.StartTime >= this.dateTimeChooser1.EndTime)
            {
                MessageBox.Show("开始时间大于结束时间，请重新选择！");
                return;
            }
            else
            {
                if (cedainkongjian.SelectedIndex > -1)
                {
                    listBox1.Items.Clear();
                    listBox1.Items.AddRange(updateList(cedainkongjian.SelectedItem.ToString()));
                }
                
                if (kuidian.SelectedIndex > -1)
                {
                    listBox3.Items.Clear();
                    listBox3.Items.AddRange(updateList(kuidian.SelectedItem.ToString()));
                }
                if (duandian.SelectedIndex > -1)
                {
                    listBox2.Items.Clear();
                    listBox2.Items.AddRange(updateList(duandian.SelectedItem.ToString()));
                }
            }
        }

        //cedian控件更改触发
        private void cedain_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.init_bianhao == 1)
            {
                string s = Convert.ToString(cedainkongjian.SelectedItem);

                s = s.Substring(0, 5);

                string[] ss = DuanDianGuanXi.getDuanDianCeDianBianHao(s);
                this.duandian.Items.Clear();
                this.duandian.Items.AddRange(ss);
                this.duandian.SelectedIndex = -1;
                string[] kuidian = DuanDianGuanXi.getKuiDianCeDianBianHao(s);
                this.kuidian.Items.Clear();
                this.kuidian.Items.AddRange(kuidian);
                this.kuidian.SelectedIndex = -1;
                listBox2.Items.Clear();
                listBox3.Items.Clear();
            }
            else
            {
                this.init_bianhao = 1;
            }
        }
        //断电控件更改触发
        private void duandian_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (duandian.SelectedIndex > -1)
            {
                listBox2.Items.Clear();
                listBox2.Items.AddRange(updateList(duandian.SelectedItem.ToString()));

                string s = duandian.SelectedItem.ToString();
                if (s[2] == 'C')
                {
                    this.kuidian.Items.Clear();
                    this.listBox3.Items.Clear();
                    string[] kuidian = DuanDianGuanXi.getKuiDianCeDianBianHao(s.Substring(0,5));
                    this.kuidian.Items.AddRange(kuidian);
                    this.kuidian.SelectedIndex = -1;
                }
            }
        }
        private string[] updateList(string ss)
        {
            string s = ss.Substring(0, 5);
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DateTime StartTime;
            DateTime EndTime;
            List<string> list = new List<string>();
            if ("D" == s.Substring(2, 1) || "F" == s.Substring(2, 1))
            {
                
                d=getkaiguan(ss);
                if (d != null)
                {
                    StartTime = d[0].time;
                    foreach (mokai row in d)
                    {
                        int state = row.state;

                        if (state == 7)
                        {
                            if ("F" == s.Substring(2, 1))
                            {
                                list.Add("通讯中断 " + row.time.ToString() + " 故障");
                            }
                            else
                            {
                                list.Add("通讯中断 " + row.time.ToString() + " 故障");
                            }
                            continue;
                        }
                        string ss2 = "";
                        if ("F" != s.Substring(2, 1))
                        {
                            ss2 = state.ToString();
                            if (state >= 0 && state < GlobalParams.state.Length)
                                ss2 = GlobalParams.state[state];
                        }
                        else
                        {
                            int tmpbj = Convert.ToInt32(row.uploadvalue);
                            if (tmpbj > 15) tmpbj = 15;
                            ss2 = GlobalParams.state[tmpbj];
                        }
                        


                        list.Add(switchValue(s, row.uploadvalue.ToString()).PadRight(5) + " " + row.time.ToString() + " " + ss2);

                    }
                }
            }
            else if ("A" == s.Substring(2, 1))
            {
                d=getmoni(ss);
                if (d != null)
                {
                    
                    foreach (mokai row in d)
                    {
                        int state = row.state;
                        string ss1 = state.ToString();
                        if (state < GlobalParams.state.Length && state >= 0)
                            ss1 = GlobalParams.state[state];
                        list.Add(row.uploadvalue.ToString().PadRight(8) + " " + row.time.ToString() + " " + ss1);
                    }
                }
            }
            else if ("C" == s.Substring(2, 1))
            {
              
                kd=getkongzhi(ss);
      
               if (kd != null)
                {
                    foreach (kongzhi row in kd)
                    {
                        if (row.state == 7)
                        {
                            list.Add("通讯中断 " + row.time.ToString() + " 故障");
                            continue;
                        }
                        else
                        {
                            list.Add(controlValue(s, row.uploadvalue.ToString()).PadRight(5) + " " + row.time.ToString()+" ");
                       }
                    }
                }
            }
            return list.ToArray();
        }
        private string switchValue(string ceDian, string value)
        {
            if (ceDian.Substring(2, 1).ToUpper() == "F")
            {
                switch (value)
                {
                    case "0":
                        return "交流";
                    default:
                        return "直流";
                }
            }
            else
            {

                DataTable dt = DuanDianGuanXi.GetSwitchAlarm(ceDian);
                switch (value)
                {
                    case "0":
                        return dt.Rows[0]["lingTaiMingCheng"].ToString();
                    case "1":
                        return dt.Rows[0]["yiTaiMingCheng"].ToString();
                    case "2":
                        return dt.Rows[0]["erTaiMingCheng"].ToString();
                    default:
                        return "其它";
                }
            }
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index > -1)
            {
                string s = this.listBox1.Items[e.Index].ToString();
                if (s.Contains("报警"))
                {
                    e.Graphics.DrawString(s, this.Font, Brushes.Red, e.Bounds);
                }
                else if (s.Contains("断电"))
                {

                    e.Graphics.DrawString(s, this.Font, Brushes.Green, e.Bounds);
                }
                else if (s.Contains("故障"))
                {
                    e.Graphics.DrawString(s, this.Font, Brushes.Blue, e.Bounds);
                }
                else if (s.Contains("断线"))
                {
                    e.Graphics.DrawString(s, this.Font, Brushes.Brown, e.Bounds);
                }
                else
                {
                    e.Graphics.DrawString(s, this.Font, Brushes.Black, e.Bounds);
                }
            }
        }

        private void listBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index > -1)
            {
                string s = this.listBox2.Items[e.Index].ToString();
                if (s.Contains("报警"))
                {
                    e.Graphics.DrawString(s, this.Font, Brushes.Red, e.Bounds);
                }
                else if (s.Contains("断电"))
                {

                    e.Graphics.DrawString(s, this.Font, Brushes.Green, e.Bounds);
                }
                else if (s.Contains("故障"))
                {
                    e.Graphics.DrawString(s, this.Font, Brushes.Blue, e.Bounds);
                }
                else if (s.Contains("断线"))
                {
                    e.Graphics.DrawString(s, this.Font, Brushes.Yellow, e.Bounds);
                }
                else
                {
                    e.Graphics.DrawString(s, this.Font, Brushes.Black, e.Bounds);
                }
            }
        }

        private void listBox3_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index > -1)
            {
                string s = this.listBox3.Items[e.Index].ToString();
                if (s.Contains("报警"))
                {
                    e.Graphics.DrawString(s, this.Font, Brushes.Red, e.Bounds);
                }
                else if (s.Contains("断电"))
                {

                    e.Graphics.DrawString(s, this.Font, Brushes.Green, e.Bounds);
                }
                else if (s.Contains("故障"))
                {
                    e.Graphics.DrawString(s, this.Font, Brushes.Blue, e.Bounds);
                }
                else if (s.Contains("断线"))
                {
                    e.Graphics.DrawString(s, this.Font, Brushes.Yellow, e.Bounds);
                }
                else
                {
                    e.Graphics.DrawString(s, this.Font, Brushes.Black, e.Bounds);
                }
            }
        }
        private string controlValue(string ceDian, string value)
        {
            DataTable dt = DuanDianGuanXi.GetKongAlarm(ceDian);
            if (value == "0")
            {
                return dt.Rows[0]["lingTaiMingCheng"].ToString();
            }
            else if (value == "1")
            {
                return dt.Rows[0]["yiTaiMingCheng"].ToString();
            }
            else
            {               
                    return "其它";            
            }
               
        }

        int pages = 0;
        int pageSize = 201;
        ListBox prt_lbx = null;
        ComboBox prt_cbx = null;
        string type = "";
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.printDialog1.ShowDialog() == DialogResult.OK)
            {
                pages = 0;
                prt_lbx = this.listBox1;
                prt_cbx = this.cedainkongjian;
                type = "测点";
                try
                {
                    this.printDocument1.Print();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "选中的打印机不是计算机默认的打印机");

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.printDialog1.ShowDialog() == DialogResult.OK)
            {
                pages = 0;
                prt_lbx = this.listBox2;
                prt_cbx = this.duandian;
                type = "断电关系";
                try
                {
                    this.printDocument1.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "选中的打印机不是计算机默认的打印机");

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.printDialog1.ShowDialog() == DialogResult.OK)
            {
                pages = 0;
                prt_lbx = this.listBox3;
                prt_cbx = this.kuidian;
                type = "馈电关系";
                try
                {
                    this.printDocument1.Print();
                }
                catch (Exception ex)
                {
                     MessageBox.Show(ex.ToString(),"选中的打印机不是计算机默认的打印机");

                }
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                Font font = new Font("宋体", 10, FontStyle.Regular);//设置画笔 
                Brush bru = Brushes.Black;
                Pen pen = new Pen(bru);
                pen.Width = 0;
                //设置各边距 
                int nLeft = 0;
                int nTop = 0;
                int nRight = 0;
                int nBottom = 0;
                int nWidth = 200 - nRight - nLeft;
                int nHeight = 200 - nTop - nBottom;
                ////打印各边距 
                //e.Graphics.DrawLine(pen, nLeft, nTop, nLeft, nTop + nHeight);
                //e.Graphics.DrawLine(pen, nLeft + nWidth, nTop, nLeft + nWidth, nTop + nHeight);
                //e.Graphics.DrawLine(pen, nLeft, nTop, nLeft + nWidth, nTop);
                //e.Graphics.DrawLine(pen, nLeft, nTop + nHeight, nLeft + nWidth, nTop + nHeight);

                e.Graphics.DrawString("历史数据，" + type + "：" + prt_cbx.SelectedItem + ",时间：" + dateTimeChooser1.StartTime.ToString("yyyy年MM月dd日 HH时mm分") + " 至 " + dateTimeChooser1.EndTime.ToString("yyyy年MM月dd日 HH时mm分"), font, bru, 5, 5);
                e.Graphics.DrawString("第" + (pages + 1) + "页", font, bru, 400, 1130);

                int n = pages * pageSize;

                int count = pageSize;


                if (this.prt_lbx.Items.Count < pageSize)
                {
                    count = this.prt_lbx.Items.Count;
                }
                else if (n + pageSize > this.prt_lbx.Items.Count)
                {
                    count = this.prt_lbx.Items.Count;
                }
                else
                {
                    count = n + pageSize;
                }

                string content = "";
                for (int i = n; i < count; i++)
                {
                    string item = this.prt_lbx.Items[i].ToString();

                    if (i != 0 && i % 3 == 0)
                    {
                        content += "\n";
                    }
                    else if (i > 0)
                    {
                        content += "        ";
                    }

                    content += item;
                }

                e.Graphics.DrawString(content, font, bru, nLeft + 10, nTop + 30);

                pages++;
                if (pages == 1 && this.prt_lbx.Items.Count > pages * pageSize)
                {

                    e.HasMorePages = true;
                }
                else if (this.prt_lbx.Items.Count > 30 && this.prt_lbx.Items.Count > pages * pageSize)
                {

                    e.HasMorePages = true;
                }
                else
                {

                    e.HasMorePages = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印出现错误，请检查是否安装打印机。");
                throw (ex);
            }


        }
        //kuidian控件触发
        private void kuidian_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (kuidian.SelectedIndex > -1)
            {
                listBox3.Items.Clear();
                listBox3.Items.AddRange(updateList(kuidian.SelectedItem.ToString()));
            
            }
        }



        private void Init_Start()
        {

            string s = Current_Param; ;

            s = s.Substring(0, 5);

            string[] ss = DuanDianGuanXi.getDuanDianCeDianBianHao(s);
            this.duandian.Items.Clear();
            this.duandian.Items.AddRange(ss);
            this.duandian.SelectedIndex = -1;
            string[] kuidian = DuanDianGuanXi.getKuiDianCeDianBianHao(s);
            this.kuidian.Items.Clear();
            this.kuidian.Items.AddRange(kuidian);
            this.kuidian.SelectedIndex = -1;
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            cedainkongjian.SelectedIndex = GetCDIndex(s) ;
            listBox1.Items.Clear();
            listBox1.Items.AddRange(updateList(s));
        }
        private int GetCDIndex(string cedianhao)
        {
            int ret = -1;
            string querystr;
            for(int i=0 ; i<cedainkongjian.Items.Count;i++)
            {
                querystr = cedainkongjian.Items[i].ToString();
                if (querystr.Contains(cedianhao))
                {
                    ret = i;
                    break;
                }
            }
            return ret;
        }
        


        
    }
}
