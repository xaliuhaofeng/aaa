namespace Logic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    public class CeDian
    {
        private bool baoJing;
        private string ceDianBianHao;
        private string ceDianWeiZhi;
        private int chuanGanQiLeiXing;
        private string chuanGanQiZhiShi;
        private DateTime createDate;
        private bool isMultiBaoji;

      


        //  0----模拟量  1----开关量(包括分站量）   2----控制量

        private int daLeiXing;
        private DateTime deleteDate;
        private bool duanDianFlag;
        private int fenZhanHao;
        private bool fuDianFlag;
        private int id;
        private bool isAlarm;
        private bool isAlarm_pre;
        private KaiGuanLiangLeiXing kaiGuanLiang;
        private Logic.KongZhiLiang kongZhiLiang;
        private MoNiLiangLeiXing moNiLiang;

            //    0000:	 0   正常
            //    0001:	1   报警
            //    0010:	 2  断电
            //    0011:	3   复电
            //    0100:	4   断线
            //    0101:	5   溢出
            //    0110:	6   负漂
            //    0111:	7   故障
            //    1000:	8   标校
            //          14  馈电异常
            
           //    人为为增加  100000   未配置 
          //        0x10000 通讯中断
           
        private int reSta_pre;
        private int rtState;

        private float rtVal;
        private float rtVal_pre;
        private bool tiaoJiao;
        private DateTime time;
        private DateTime time_Pre;
        private int tongDaoHao;
        private bool updateDDGX;
        private bool weiShanChu;
        private string xiaoleiXing;

        public CeDian()
        {
        }

        public CeDian(string ceDianBianHao)
        {
            DataTable dt;
            this.ceDianBianHao = ceDianBianHao;
            if (ceDianBianHao[2] != 'C')
            {
                dt = OperateDB.Select("select * from CeDian where weishanchu=1 and ceDianBianHao = '" + ceDianBianHao + "'");
                if ((dt != null) && (dt.Rows.Count != 0))
                {
                    this.daLeiXing = Convert.ToInt32(dt.Rows[0]["daLeiXing"]);
                    this.xiaoleiXing = dt.Rows[0]["xiaoLeiXing"].ToString();
                    this.chuanGanQiLeiXing = Convert.ToInt32(dt.Rows[0]["chuanGanQiLeiXing"]);
                    this.ceDianWeiZhi = dt.Rows[0]["ceDianWeiZhi"].ToString();
                    this.fenZhanHao = Convert.ToInt32(dt.Rows[0]["fenZhanHao"]);
                    this.tongDaoHao = Convert.ToInt32(dt.Rows[0]["tongDaoHao"]);
                    this.weiShanChu = Convert.ToBoolean(dt.Rows[0]["weiShanChu"]);
                    this.tiaoJiao = Convert.ToBoolean(dt.Rows[0]["tiaoJiao"]);
                    this.baoJing = Convert.ToBoolean(dt.Rows[0]["baoJing"]);
                    this.chuanGanQiZhiShi = dt.Rows[0]["chuanGanQiZhiShi"].ToString();
                    this.createDate = Convert.ToDateTime(dt.Rows[0]["createDate"]);
                    this.id = Convert.ToInt32(dt.Rows[0]["id"]);
                    this.duanDianFlag = false;
                    this.fuDianFlag = true;
                    if (ceDianBianHao[2] == 'A')
                    {
                        this.moNiLiang = new MoNiLiangLeiXing(this.xiaoleiXing);
                    }
                    else if ((ceDianBianHao[2] == 'D') || (ceDianBianHao[2] == 'F'))
                    {
                        this.kaiGuanLiang = new KaiGuanLiangLeiXing(this.xiaoleiXing);
                    }
                }
            }
            else
            {
                dt = OperateDB.Select("select * from KongZhiLiangCeDian where weishanchu=1 and ceDianBianHao = '" + ceDianBianHao + "'");
                if ((dt != null) && (dt.Rows.Count != 0))
                {
                    this.fenZhanHao = Convert.ToInt32(dt.Rows[0]["fenZhanHao"]);
                    this.tongDaoHao = Convert.ToInt32(dt.Rows[0]["kongZhiLiangBianHao"]);
                    this.xiaoleiXing = dt.Rows[0]["mingCheng"].ToString();
                    this.ceDianWeiZhi = dt.Rows[0]["ceDianWeiZhi"].ToString();
                    this.id = Convert.ToInt32(dt.Rows[0]["id"]);
                    this.createDate = Convert.ToDateTime(dt.Rows[0]["createDate"]);
                    this.daLeiXing = 2;
                    this.weiShanChu = Convert.ToBoolean(dt.Rows[0]["weiShanChu"]);
                    this.kongZhiLiang = new Logic.KongZhiLiang(dt.Rows[0]["mingCheng"].ToString());
                    this.ChuanGanQiLeiXing = 9;
                }
            }
        }

        public CeDian(int fenzhan, int tongdao)
        {
            this.fenZhanHao = fenzhan;
            this.tongDaoHao = tongdao;
            this.getCeDianById(fenzhan, tongdao);
        }

        public static string CountCeDian(string XiaoLeiXing)
        {
            return ("select xiaoLeiXing from CeDian where weishanchu=1 and xiaoLeiXing = '" + XiaoLeiXing + "'");
        }

        public static string CountCeDian2(string WeiZhi)
        {
            return ("select cedianweizhi from CeDian where weishanchu=1 and cedianweizhi = '" + WeiZhi + "'");
        }

        public static string DelKongCeDian(string cedianbianhao)
        {
            return ("update KongZhiLiangCeDian set weishanchu=0,deleteDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where cedianbianhao = '" + cedianbianhao + "'");
        }

        public static ArrayList GetAllCeDianBianHao(string zz)
        {
            int i;
            ArrayList a = new ArrayList();
            string s = "select ceDianBianHao from CeDian where weishanchu=1 ";
            DataTable dt = OperateDB.Select(s);
            for (i = 0; i < dt.Rows.Count; i++)
            {
                string ceDianBianHao = Convert.ToString(dt.Rows[i][0]);
                if (zz == "ALL")
                {
                    a.Add(ceDianBianHao);
                }
                else if (ceDianBianHao[2] == zz[0])
                {
                    a.Add(ceDianBianHao);
                }
                else if ((zz.Length > 1) && (ceDianBianHao[2] == zz[1]))
                {
                    a.Add(ceDianBianHao);
                }
            }
            if ((zz == "ALL") || (zz == "C"))
            {
                s = "select ceDianBianHao from KongZhiLiangCeDian where weishanchu=1";
                dt = OperateDB.Select(s);
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        string cedian = dt.Rows[i][0].ToString();
                        a.Add(cedian);
                    }
                }
            }
            a.Sort();
            return a;
        }

        public static long[] GetAllids()
        {
            string sql = "select id from CeDian";
            DataTable dt = OperateDB.Select(sql);
            long[] ids = new long[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                ids[i] = (long) row["id"];
                i++;
            }
            return ids;
        }

        public static ArrayList GetAllLeiXing()
        {
            int i;
            ArrayList a = new ArrayList();
            string s = "select mingCheng from MoNiLiangLeiXing";
            DataTable dt = OperateDB.Select(s);
            for (i = 0; i < dt.Rows.Count; i++)
            {
                a.Add(dt.Rows[i][0]);
            }
            s = "select mingCheng from KaiGuanLiangLeiXing";
            dt = OperateDB.Select(s);
            for (i = 0; i < dt.Rows.Count; i++)
            {
                a.Add(dt.Rows[i][0]);
            }
            return a;
        }

        public static DataTable GetAllTiaoJiaoCeDian()
        {
            string sql = "select ceDianBianHao, ceDianWeiZhi, xiaoLeiXIng from CeDian where weishanchu=1 and tiaoJiao = 'true'";
            return OperateDB.Select(sql);
        }

        public static HistoryData[] GetAllValues(DateTime start, DateTime end, string ceDianBianHao)
        {
            int i;
            bool needClear = false;
            bool first = true;
            int clearIndex = 0;
            if (start > end)
            {
                return null;
            }
            string tableName = "";
            if (ceDianBianHao[2] == 'A')
            {
                tableName = "MoNiLiangValue";
            }
            else if (ceDianBianHao[2] == 'D')
            {
                tableName = "KaiGuanLiangValue";
            }
            tableName = tableName + start.ToString("yyyy_MM");
            DataTable dt = OperateDB.Select(string.Concat(new object[] { "select max(uploadTime) from ", tableName, " where uploadTime<='", start, "' and ceDianBianHao='", ceDianBianHao, "'" }));
            DateTime lasttime = start;
            if (dt == null)
            {
                return null;
            }
            if ((dt.Rows.Count > 0) && (dt.Rows[0][0] != DBNull.Value))
            {
                lasttime = Convert.ToDateTime(dt.Rows[0][0]);
            }
            if (lasttime.Date != start.Date)
            {
                needClear = true;
            }
            dt = OperateDB.Select(string.Concat(new object[] { "select uploadTime,uploadValue,state from ", tableName, " where ceDianBianHao='", ceDianBianHao, "' and uploadTime>='", lasttime, "' and uploadTime<='", end, "'" }));
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                return null;
            }
            TimeSpan Reflector0003 = (TimeSpan) (Convert.ToDateTime(dt.Rows[0]["uploadTime"]) - start);
            if (Reflector0003.TotalSeconds > 5.0)
            {
                needClear = true;
            }
            TimeSpan ts1 = new TimeSpan(start.Ticks);
            TimeSpan ts2 = new TimeSpan(end.Ticks);
            int selectPointCount = (int) (ts2.Subtract(ts1).Duration().TotalSeconds / 5.0);
            HistoryData[] historyData = new HistoryData[selectPointCount];
            for (i = 0; i < historyData.Length; i++)
            {
                historyData[i] = new HistoryData();
            }
            int index = 0;
            DateTime timeIndex = start;
            for (i = 0; i < selectPointCount; i++)
            {
                DateTime time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);
                while (time1 < timeIndex)
                {
                    index++;
                    if (index == dt.Rows.Count)
                    {
                        break;
                    }
                    time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);
                }
                if ((first && needClear) && ((Reflector0003 = (TimeSpan) (time1 - timeIndex)).TotalSeconds <= 5.0))
                {
                    clearIndex = i;
                    first = false;
                }
                if (index == 0)
                {
                    index++;
                }
                historyData[i].value = (float) Math.Round((double) Convert.ToSingle(dt.Rows[index - 1]["uploadValue"]), 2);
                historyData[i].time = timeIndex;
                historyData[i].state = Convert.ToByte(dt.Rows[index - 1]["state"]);
                timeIndex = timeIndex.AddSeconds(5.0);
                index--;
            }
            if (needClear && (clearIndex < selectPointCount))
            {
                for (i = 0; i <= clearIndex; i++)
                {
                    historyData[i].value = 0f;
                }
            }
            return historyData;
        }

        public static HistoryData[] GetAllValuesTrue(DateTime start, DateTime end, string ceDianBianHao)
        {
            int i;
            bool needClear = false;
            bool first = true;
            int clearIndex = 0;
            if (start > end)
            {
                return null;
            }
            string tableName = "";
            if (ceDianBianHao[2] == 'A')
            {
                tableName = "MoNiLiangValue";
            }
            else if (ceDianBianHao[2] == 'D')
            {
                tableName = "KaiGuanLiangValue";
            }
            tableName = tableName + start.ToString("yyyy_MM");
            DataTable dt = OperateDB.Select(string.Concat(new object[] { "select max(uploadTime) from ", tableName, " where uploadTime<='", start, "' and ceDianBianHao='", ceDianBianHao, "'" }));
            DateTime lasttime = start;
            if (dt == null)
            {
                return null;
            }
            if ((dt.Rows.Count > 0) && (dt.Rows[0][0] != DBNull.Value))
            {
                lasttime = Convert.ToDateTime(dt.Rows[0][0]);
            }
            if (lasttime.Date != start.Date)
            {
                needClear = true;
            }
            dt = OperateDB.Select(string.Concat(new object[] { "select uploadTime,uploadValue,state from ", tableName, " where ceDianBianHao='", ceDianBianHao, "' and uploadTime>='", lasttime, "' and uploadTime<='", end, "'" }));
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                return null;
            }
            TimeSpan Reflector0003 = (TimeSpan) (Convert.ToDateTime(dt.Rows[0]["uploadTime"]) - start);
            if (Reflector0003.TotalSeconds > 0.5)
            {
                needClear = true;
            }
            TimeSpan ts1 = new TimeSpan(start.Ticks);
            TimeSpan ts2 = new TimeSpan(end.Ticks);
            int selectPointCount = (int) (ts2.Subtract(ts1).Duration().TotalSeconds / 0.5);
            HistoryData[] historyData = new HistoryData[selectPointCount];
            for (i = 0; i < historyData.Length; i++)
            {
                historyData[i] = new HistoryData();
            }
            int index = 0;
            DateTime timeIndex = start;
            for (i = 0; i < selectPointCount; i++)
            {
                DateTime time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);
                while (time1 < timeIndex)
                {
                    index++;
                    if (index == dt.Rows.Count)
                    {
                        break;
                    }
                    time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);
                }
                if ((first && needClear) && ((Reflector0003 = (TimeSpan) (time1 - timeIndex)).TotalSeconds <= 0.5))
                {
                    clearIndex = i;
                    first = false;
                }
                if (index == 0)
                {
                    index++;
                }
                historyData[i].value = (float) Math.Round((double) Convert.ToSingle(dt.Rows[index - 1]["uploadValue"]), 2);
                historyData[i].time = timeIndex;
                historyData[i].state = Convert.ToByte(dt.Rows[index - 1]["state"]);
                timeIndex = timeIndex.AddSeconds(0.5);
                index--;
            }
            if (needClear && (clearIndex < selectPointCount))
            {
                for (i = 0; i <= clearIndex; i++)
                {
                    historyData[i].value = 0f;
                }
            }
            return historyData;
        }

        public static float[] GetAverageMaxMinCurrValue(DateTime start, DateTime end, string ceDianBianHao)
        {
            float[] f = new float[4];
            float sum = 0f;
            f[1] = 999999f;
            f[2] = 0f;
            HistoryData[] data = GetAllValues(start, end, ceDianBianHao);
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i].value;
                f[3] = data[i].value;
                if (data[i].value < f[1])
                {
                    f[1] = data[i].value;
                }
                if (data[i].value > f[2])
                {
                    f[2] = data[i].value;
                }
            }
            if (data.Length > 0)
            {
                f[0] = sum / ((float) data.Length);
            }
            else
            {
                f[0] = 0f;
            }
            f[0] = (float) Math.Round((double) f[0], 2);
            f[1] = (float) Math.Round((double) f[1], 2);
            f[2] = (float) Math.Round((double) f[2], 2);
            f[3] = (float) Math.Round((double) f[3], 2);
            return f;
        }

        public static HistoryData[] GetAverageValues(HistoryData[] historyData, int secondTick)
        {
            int i;
            if (historyData == null)
            {
                return null;
            }
            int pointCount = (historyData.Length * 5) / secondTick;
            HistoryData[] temp = new HistoryData[pointCount];
            for (i = 0; i < temp.Length; i++)
            {
                temp[i] = new HistoryData();
            }
            float sum = 0f;
            for (i = 0; i < temp.Length; i++)
            {
                for (int j = 0; j < (secondTick / 5); j++)
                {
                    float tempFloat = historyData[((i * secondTick) / 5) + j].value;
                    sum += tempFloat;
                }
                temp[i].value = (float) Math.Round((double) (sum / ((float) (secondTick / 5))), 2);
                temp[i].time = historyData[(i * secondTick) / 5].time;
                temp[i].state = historyData[(i * secondTick) / 5].state;
                sum = 0f;
            }
            return temp;
        }

        public static float[] GetBaoJingValues(DateTime start, DateTime end, string ceDianBianHao, int minutesTick)
        {
            Random r = new Random();
            float[] temp = new float[200];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = r.Next(50, 70);
            }
            return temp;
        }

        public static HistoryData[] GetCallValues(HistoryData[] historyData, int secondTick)
        {
            int i;
            if (historyData == null)
            {
                return null;
            }
            int pointCount = (historyData.Length * 5) / secondTick;
            HistoryData[] temp = new HistoryData[pointCount];
            for (i = 0; i < temp.Length; i++)
            {
                temp[i] = new HistoryData();
            }
            for (i = 0; i < temp.Length; i++)
            {
                temp[i].value = historyData[(i * secondTick) / 5].value;
                temp[i].time = historyData[(i * secondTick) / 5].time;
                temp[i].state = historyData[(i * secondTick) / 5].state;
            }
            return temp;
        }

        public static DataTable GetCeDian()
        {
            string sql = "select cedianweizhi, xiaoleixing, cedianbianhao from CeDian where weishanchu=1 order by cedianbianhao";
            return OperateDB.Select(sql);
        }

        public static DataTable GetCeDian10(string xiaoleixing)
        {
            return OperateDB.Select("select cedianbianhao,cedianweizhi,mingCheng from KongZhiLiangCeDian where weishanchu=1 and mingCheng = '" + xiaoleixing + "' order by cedianbianhao");
        }

        public static DataTable GetCeDian11(string xiaoleixing, string weizhi)
        {
            return OperateDB.Select("select cedianbianhao,cedianweizhi,xiaoleixing from CeDian where weishanchu=1 and xiaoleixing = '" + xiaoleixing + "' and cedianweizhi = '" + weizhi + "' and daleixing = 0 order by cedianbianhao");
        }

        public static DataTable GetCeDian12(string xiaoleixing, string weizhi)
        {
            return OperateDB.Select("select cedianbianhao,cedianweizhi,mingCheng from KongZhiLiangCeDian where weishanchu=1 and mingCheng = '" + xiaoleixing + "' and cedianweizhi = '" + weizhi + "' order by cedianbianhao");
        }

        public static DataTable GetCeDian13(string cedianbianhao)
        {
            return OperateDB.Select("select cedianbianhao,cedianweizhi,xiaoleixing from CeDian where weishanchu=1 and cedianbianhao = '" + cedianbianhao + "'");
        }

        public static DataTable GetCeDian14(string cedianbianhao)
        {
            return OperateDB.Select("select cedianbianhao,cedianweizhi,mingCheng from KongZhiLiangCeDian where weishanchu=1 and cedianbianhao = '" + cedianbianhao + "'");
        }

        public static DataTable GetCeDian4(string weizhi)
        {
            return OperateDB.Select("select cedianbianhao,cedianweizhi,xiaoleixing from CeDian where weishanchu=1 and cedianweizhi = '" + weizhi + "' and daleixing = 1 order by cedianbianhao");
        }

        public static DataTable GetCeDian5(string xiaoleixing)
        {
            return OperateDB.Select("select cedianbianhao,cedianweizhi,xiaoleixing from CeDian where weishanchu=1 and xiaoleixing = '" + xiaoleixing + "' and daleixing = 1 order by cedianbianhao");
        }

        public static DataTable GetCeDian6(string xiaoleixing, string weizhi)
        {
            return OperateDB.Select("select cedianbianhao,cedianweizhi,xiaoleixing from CeDian where weishanchu=1 and xiaoleixing = '" + xiaoleixing + "' and cedianweizhi = '" + weizhi + "' and daleixing = 1 order by cedianbianhao");
        }

        public static DataTable GetCeDian7(string weizhi)
        {
            return OperateDB.Select("select cedianbianhao,cedianweizhi,xiaoleixing from CeDian where weishanchu=1 and cedianweizhi = '" + weizhi + "' and daleixing = 0 order by cedianbianhao");
        }

        public static DataTable GetCeDian8(string weizhi)
        {
            return OperateDB.Select("select cedianbianhao,cedianweizhi,MingCheng from KongZhiLiangCeDian where weishanchu=1 and cedianweizhi = '" + weizhi + "' order by cedianbianhao");
        }

        public static DataTable GetCeDian9(string xiaoleixing)
        {
            return OperateDB.Select("select cedianbianhao,cedianweizhi,xiaoleixing from CeDian where weishanchu=1 and xiaoleixing = '" + xiaoleixing + "' and daleixing = 0 order by cedianbianhao");
        }

        public static string[] getCeDianAllInfo(DataTable dt)
        {
            List<string> list = new List<string>();
            if (dt.Columns.Contains("xiaoLeiXing"))
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row["ceDianBianHao"].ToString() + " " + row["ceDianWeiZhi"].ToString() + " " + row["xiaoLeiXing"].ToString());
                }
            }
            else if (dt.Columns.Contains("mingCheng"))
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row["ceDianBianHao"].ToString() + " " + row["ceDianWeiZhi"].ToString() + " " + row["mingCheng"].ToString());
                }
            }
            return list.ToArray();
        }

        private void getCeDianById(int fenzhan, int tongdao)
        {
            DataTable table = OperateDB.Select(string.Concat(new object[] { "select * from CeDian where weishanchu=1 and fenZhanHao = '", fenzhan, "' and tongDaoHao = '", tongdao, "';" }));
            if (table.Rows.Count != 0)
            {
                DataRow row = table.Rows[0];
                this.ceDianBianHao = (string) row["ceDianBianHao"];
                this.tiaoJiao = (bool) row["tiaoJiao"];
            }
        }

        public static DataTable GetCeDianInfoByBianHao(string ceDianBianHao)
        {
            return OperateDB.Select("select * from CeDian where weishanchu=1 and ceDianBianHao='" + ceDianBianHao + "'");
        }

        public static DataTable GetCeDianInfoByBianHaos(string[] ceDianbianHaos)
        {
            string bhs = "(";
            foreach (string s in ceDianbianHaos)
            {
                bhs = bhs + "'" + s + "',";
            }
            bhs = bhs.Remove(bhs.Length - 1) + ")";
            return OperateDB.Select("select * from CeDian where weishanchu=1 and ceDianBianHao in " + bhs);
        }

        public static DataTable GetChannel(string Sub)
        {
            return OperateDB.Select("select tongdaohao from CeDian where weishanchu=1 and fenzhanhao = '" + Sub + "'");
        }

        public static DataTable getControlInfo(string KongZhiCeDianBianHao, byte type)
        {
            string sql = "";
            if (type == 0)
            {
                sql = "select cedianbianhao,cedianweizhi,xiaoleixing from CeDian where weishanchu=1 and cedianbianhao  in(select cedianbianhao from DuanDianGuanXi where kongZhiCeDianBianHao ='" + KongZhiCeDianBianHao + "' )";
            }
            else if (type == 1)
            {
                sql = "select cedianbianhao,cedianweizhi,xiaoleixing from CeDian where weishanchu=1 and cedianbianhao  in(select cedianbianhao from KuiDianGuanXi where kongZhiCeDianBianHao ='" + KongZhiCeDianBianHao + "' )";
            }
            return OperateDB.Select(sql);
        }

        public static HistoryData[] GetCurrentValues(HistoryData[] historyData, int secondTick)
        {
            int i;
            if (historyData == null)
            {
                return null;
            }
            int pointCount = (historyData.Length * 5) / secondTick;
            HistoryData[] temp = new HistoryData[pointCount];
            for (i = 0; i < temp.Length; i++)
            {
                temp[i] = new HistoryData();
            }
            float tempFloat = 0f;
            for (i = 0; i < temp.Length; i++)
            {
                for (int j = 0; j < (secondTick / 5); j++)
                {
                    tempFloat = historyData[((i * secondTick) / 5) + j].value;
                }
                temp[i].value = tempFloat;
                temp[i].time = historyData[(i * secondTick) / 5].time;
                temp[i].state = historyData[(i * secondTick) / 5].state;
            }
            return temp;
        }

        public string getDigiVal(int a)
        {
            switch (a)
            {
                case 0:
                    return this.KaiGuanLiang.LingTai;

                case 1:
                    return this.KaiGuanLiang.YiTai;

                case 2:
                    return this.KaiGuanLiang.ErTai;
            }
            return "";
        }

        public string getCtlVal(int a)
        {
            switch (a)
            {
                case 0:
                    return KongZhiLiang.Litaimingcheng;

                case 1:
                    return KongZhiLiang.Yitaimingcheng;

               
            }
            return "";
        }

        public static DataTable GetInfoByCeDianBianHao(string ceDianBianHao)
        {
            string s;
            char ch = ceDianBianHao[2];
            if (ch == 'A')
            {
                s = "select * from CeDian,MoNiLiangLeiXing where weishanchu=1 and ceDianBianHao='" + ceDianBianHao + "' and mingCheng=xiaoLeiXing";
            }
            else if ((ch == 'D') || (ch == 'F'))
            {
                s = "select * from CeDian where weishanchu=1 and ceDianBianHao='" + ceDianBianHao + "'";
            }
            else
            {
                s = "select ceDianBianHao, mingCheng as xiaoLeiXIng, ceDianWeiZhi from KongZhiLiangCeDian where weishanchu=1 and ceDianBianHao = '" + ceDianBianHao + "'";
            }
            return OperateDB.Select(s);
        }

        public static string[] GetKaiGuanLiangCeDianByFenZhan(int fenZhanHao)
        {
            DataTable dt = OperateDB.Select("select ceDianBianHao from CeDian where weishanchu=1 and daLeiXIng=1 and weiShanChu=1 and fenZhanHao=" + fenZhanHao);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            string[] s1 = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                s1[i] = dt.Rows[i]["ceDianBianHao"].ToString();
            }
            return s1;
        }

        public static DataTable GetKongCeDian()
        {
            string sql = "select cedianweizhi, mingcheng, cedianbianhao from KongZhiLiangCeDian where weishanchu=1 order by cedianbianhao";
            return OperateDB.Select(sql);
        }

        public static DataTable GetKongChannel(string Sub)
        {
            return OperateDB.Select("select kongZhiLiangBianHao from KongZhiLiangCeDian where weishanchu=1 and fenzhanhao = '" + Sub + "'");
        }

        public static HistoryData[] GetMaxValues(HistoryData[] historyData, int secondTick)
        {
            int i;
            if (historyData == null)
            {
                return null;
            }
            int pointCount = (historyData.Length * 5) / secondTick;
            HistoryData[] temp = new HistoryData[pointCount];
            for (i = 0; i < temp.Length; i++)
            {
                temp[i] = new HistoryData();
            }
            float max = 0f;
            for (i = 0; i < temp.Length; i++)
            {
                for (int j = 0; j < (secondTick / 5); j++)
                {
                    float tempFloat = historyData[((i * secondTick) / 5) + j].value;
                    if (tempFloat > max)
                    {
                        max = tempFloat;
                    }
                }
                temp[i].value = max;
                temp[i].time = historyData[(i * secondTick) / 5].time;
                temp[i].state = historyData[(i * secondTick) / 5].state;
                max = 0f;
            }
            return temp;
        }

        public static HistoryData[] GetMinValues(HistoryData[] historyData, int secondTick)
        {
            int i;
            if (historyData == null)
            {
                return null;
            }
            int pointCount = (historyData.Length * 5) / secondTick;
            HistoryData[] temp = new HistoryData[pointCount];
            for (i = 0; i < temp.Length; i++)
            {
                temp[i] = new HistoryData();
            }
            float min = 1E+08f;
            for (i = 0; i < temp.Length; i++)
            {
                for (int j = 0; j < (secondTick / 5); j++)
                {
                    float tempFloat = historyData[((i * secondTick) / 5) + j].value;
                    if (tempFloat < min)
                    {
                        min = tempFloat;
                    }
                }
                if (min > 0f)
                {
                    temp[i].value = min;
                }
                else
                {
                    temp[i].value = 0f;
                }
                temp[i].time = historyData[(i * secondTick) / 5].time;
                temp[i].state = historyData[(i * secondTick) / 5].state;
                min = 999999f;
            }
            return temp;
        }

        public static string[] GetMoNiLiangCeDianByFenZhan(int fenZhanHao)
        {
            DataTable dt = OperateDB.Select("select ceDianBianHao from CeDian where weishanchu=1 and daLeiXIng=0 and weiShanChu=1 and fenZhanHao=" + fenZhanHao);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            string[] s1 = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                s1[i] = dt.Rows[i]["ceDianBianHao"].ToString();
            }
            return s1;
        }

        public static float[] GetMoNiLiangInfoByCeDiaanBianHao(string ceDianBianHao)
        {
            float[] f = new float[5];
            DataTable dt = OperateDB.Select("select * from CeDian,MoNiLiangLeiXing where weishanchu=1 and ceDianBianHao='" + ceDianBianHao + "' and CeDian.XiaoLeiXing=mingCheng");
            if (dt.Rows.Count > 0)
            {
                f[0] = Convert.ToSingle(dt.Rows[0]["liangChengDi"]);
                f[1] = Convert.ToSingle(dt.Rows[0]["liangChengGao"]);
                f[2] = Convert.ToSingle(dt.Rows[0]["baoJingZhiShangXian"]);
                f[3] = Convert.ToSingle(dt.Rows[0]["duanDianZhi"]);
                f[4] = Convert.ToSingle(dt.Rows[0]["fuDianZhi"]);
            }
            return f;
        }

        public static DataTable GetMoNiLiangLeiXingInfo(string ceDianBianHao)
        {
            string xiaoLeiXing = GetCeDianInfoByBianHao(ceDianBianHao).Rows[0]["xiaoLeiXIng"].ToString();
            return OperateDB.Select("select * from MoNiLiangLeiXing where mingCheng='" + xiaoLeiXing + "'");
        }

        public static DataTable GetNotDelByCeDianBianHao(string cedianbianhao)
        {
            return OperateDB.Select("select * from CeDian where weishanchu=1 and cedianbianhao = '" + cedianbianhao + "'");
        }

        public static DataTable GetSwicthBaoJingCeDian()
        {
            string sql = "select * from CeDian,KaiGuanLiangLeiXing where weishanchu=1 and daLeiXing = 1 and xiaoLeiXing = mingCheng";
            return OperateDB.Select(sql);
        }

        public static DataTable GetSwicthBaoJingCeDian(string s, byte type)
        {
            string sql = string.Empty;
            if (type == 0)
            {
                sql = "select * from CeDian,KaiGuanLiangLeiXing where weishanchu=1 and daLeiXing = 1 and xiaoLeiXing = mingCheng and mingCheng = '" + s + "'";
            }
            else
            {
                sql = "select * from CeDian,KaiGuanLiangLeiXing where weishanchu=1 and daLeiXing = 1 and xiaoLeiXing = mingCheng and ceDianWeiZhi = '" + s + "'";
            }
            return OperateDB.Select(sql);
        }

        public static DataTable GetSwicthBaoJingCeDian(string mingCheng, string weiZhi)
        {
            return OperateDB.Select("select * from CeDian,KaiGuanLiangLeiXing where weishanchu=1 and daLeiXing = 1 and xiaoLeiXing = mingCheng and shiFouBaoJing = 1 and ceDianWeiZhi = '" + weiZhi + "' and mingCheng = '" + mingCheng + "'");
        }

        public static bool hasKuiDianOrDianDian(string cedianbianhao)
        {
            DataTable dt = OperateDB.Select("select * from KuiDianGuanXi where ceDianBianHao = '" + cedianbianhao + "'");
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                return true;
            }
            dt = OperateDB.Select("select * from DuanDianGuanXi where ceDianBianHao = '" + cedianbianhao + "'");
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                return true;
            }
            dt = OperateDB.Select("select * from DuanDianGuanXi where kongZhiCeDianBianHao = '" + cedianbianhao + "'");
            return ((dt != null) && (dt.Rows.Count > 0));
        }

        public bool IsAnaAlarm(float a)
        {
            return ((((this.MoNiLiang.BaoJingLeiXing == 0) && (a > this.MoNiLiang.BaoJingZhiShangXian)) || ((this.MoNiLiang.BaoJingLeiXing == 2) && (a < this.MoNiLiang.BaoJingZhiXiaXian))) || ((this.MoNiLiang.BaoJingLeiXing == 1) && ((this.MoNiLiang.BaoJingZhiShangXian >= a) || (this.MoNiLiang.BaoJingZhiXiaXian <= a))));
        }

        public bool IsAnaCutAlarm(float a)
        {
            return (this.MoNiLiang.DuanDianZhi <= a);
        }

        public bool IsAnaFeedAlarm(float a)
        {
            return (this.RtState == 14);
        }

        public bool isAnaYuJing(float YuJingValue, float ChangeRate, float val)
        {
            return (((YuJingValue > 0f) && (val > YuJingValue)) || (((ChangeRate > 0f) && (this.RtVal_pre > 0f)) && ((this.RtVal_pre - (val / this.RtVal_pre)) > ChangeRate)));
        }

        public bool IsDigiAlarm(int a)
        {
            return (this.KaiGuanLiang.ShiFouBaoJing && (a == this.KaiGuanLiang.BaoJingZhuangTai));
        }

        public bool IsDigiCut(int a)
        {
            return ( (this.KaiGuanLiang.DuanDianZhuangTai == a));
           // return (this.KaiGuanLiang.ShiFouDuanDian && (this.KaiGuanLiang.DuanDianZhuangTai == a));
        }

        public bool IsDigiFeed(int a)
        {
            return (this.RtState == 14);
        }

        public static bool ReKongCeDian(string ceDianBianHao)
        {
            return (OperateDB.Select("select * from KongZhiLiangCeDian where weishanchu=1 and cedianbianhao = '" + ceDianBianHao + "'").Rows.Count > 0);
        }

        public static string UpdateCeDian(string WeiZhi, string XiaoLeiXing, string ChuanGanQiLeiXing, string FenZhanHao, string TongDaoHao, string CeDianBianHao, string fenZhanLeiXing, string Original)
        {
            return ("update CeDian set cedianweizhi = '" + WeiZhi + "', xiaoleixing = '" + XiaoLeiXing + "',chuanganqileixing = '" + ChuanGanQiLeiXing + "',chuanganqizhishi = '" + fenZhanLeiXing + "',fenzhanhao = '" + FenZhanHao + "',tongdaohao= '" + TongDaoHao + "',cedianbianhao= '" + CeDianBianHao + "' where cedianbianhao = '" + Original + "'");
        }

        public static string UpdateCeDianWeiZhi(string WeiZhi, string Original)
        {
            return ("update CeDian set cedianweizhi = '" + WeiZhi + "' where cedianweizhi = '" + Original + "'");
        }

        public static string UpdateKongCeDian(string WeiZhi, string XiaoLeiXing, string FenZhanHao, string TongDaoHao, string CeDianBianHao, string Original)
        {
            return ("update KongZhiLiangCeDian set cedianweizhi = '" + WeiZhi + "',mingCheng = '" + XiaoLeiXing + "',fenzhanhao = '" + FenZhanHao + "',kongZhiLiangBianHao= '" + TongDaoHao + "',cedianbianhao= '" + CeDianBianHao + "' where cedianbianhao = '" + Original + "'");
        }

        private void UpdateTiaoJiaoValue(bool tiaojiao)
        {
            OperateDB.Execute(string.Concat(new object[] { "update CeDian set TiaoJiao = '", tiaojiao, "' where fenZhanHao = '", this.FenZhanHao, "' and tongDaoHao = '", this.TongDaoHao, "';" }));
        }

        public bool BaoJing
        {
            get
            {
                return this.baoJing;
            }
            set
            {
                this.baoJing = value;
            }
        }

        public string CeDianBianHao
        {
            get
            {
                return this.ceDianBianHao;
            }
            set
            {
                this.ceDianBianHao = value;
            }
        }

        public string CeDianWeiZhi
        {
            get
            {
                return this.ceDianWeiZhi;
            }
            set
            {
                this.ceDianWeiZhi = value;
            }
        }

        public int ChuanGanQiLeiXing
        {
            get
            {
                return this.chuanGanQiLeiXing;
            }
            set
            {
                this.chuanGanQiLeiXing = value;
            }
        }

        public string ChuanGanQiZhiShi
        {
            get
            {
                return this.chuanGanQiZhiShi;
            }
            set
            {
                this.chuanGanQiZhiShi = value;
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return this.createDate;
            }
            set
            {
                this.createDate = value;
            }
        }

        public int DaLeiXing
        {
            get
            {
                return this.daLeiXing;
            }
            set
            {
                this.daLeiXing = value;
            }
        }

        public DateTime DeleteDate
        {
            get
            {
                return this.deleteDate;
            }
            set
            {
                this.deleteDate = value;
            }
        }

        public bool DuanDianFlag
        {
            get
            {
                return this.duanDianFlag;
            }
            set
            {
                this.duanDianFlag = value;
            }
        }

        public int FenZhanHao
        {
            get
            {
                return this.fenZhanHao;
            }
            set
            {
                this.fenZhanHao = value;
            }
        }

        public bool FuDianFlag
        {
            get
            {
                return this.fuDianFlag;
            }
            set
            {
                this.fuDianFlag = value;
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public bool IsAlarm
        {
            get
            {
                return this.isAlarm;
            }
            set
            {
                this.isAlarm = value;
            }
        }

        public KaiGuanLiangLeiXing KaiGuanLiang
        {
            get
            {
                return this.kaiGuanLiang;
            }
            set
            {
                this.kaiGuanLiang = value;
            }
        }

        public Logic.KongZhiLiang KongZhiLiang
        {
            get
            {
                return this.kongZhiLiang;
            }
            set
            {
                this.kongZhiLiang = value;
            }
        }

        public MoNiLiangLeiXing MoNiLiang
        {
            get
            {
                return this.moNiLiang;
            }
            set
            {
                this.moNiLiang = value;
            }
        }

        public int RtState
        {
            get
            {
                return this.rtState;
            }
            set
            {
                this.rtState = value;
            }
        }

        public int RtState_pre
        {
            get
            {
                return this.reSta_pre;
            }
            set
            {
                this.reSta_pre = value;
            }
        }

        public float RtVal
        {
            get
            {
                return this.rtVal;
            }
            set
            {
                this.rtVal = value;
            }
        }

        public float RtVal_pre
        {
            get
            {
                return this.rtVal_pre;
            }
            set
            {
                this.rtVal_pre = value;
            }
        }

        public bool TiaoJiao
        {
            get
            {
                return this.tiaoJiao;
            }
            set
            {
                this.tiaoJiao = value;
                this.UpdateTiaoJiaoValue(value);
            }
        }

        public DateTime Time
        {
            get
            {
                return this.time;
            }
            set
            {
                this.time = value;
            }
        }

        public DateTime Time_Pre
        {
            get
            {
                return this.time_Pre;
            }
            set
            {
                this.time_Pre = value;
            }
        }

        public int TongDaoHao
        {
            get
            {
                return this.tongDaoHao;
            }
            set
            {
                this.tongDaoHao = value;
            }
        }

        public bool UpdateDDGX
        {
            get
            {
                return this.updateDDGX;
            }
            set
            {
                this.updateDDGX = value;
            }
        }

        public bool WeiShanChu
        {
            get
            {
                return this.weiShanChu;
            }
            set
            {
                this.weiShanChu = value;
            }
        }

        public string XiaoleiXing
        {
            get
            {
                return this.xiaoleiXing;
            }
            set
            {
                this.xiaoleiXing = value;
            }
        }

        public bool IsMultiBaoji
        {
            get { return isMultiBaoji; }
            set { isMultiBaoji = value; }
        }
    }
}

