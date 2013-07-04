using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic;
using System.Data;
using System.Drawing;
using ChartDirector;

namespace MAX_CMSS_V2.Curve
{
    public class CurveData
    {
        public static CeDian GetMKCedian(int fds, int chan)
        {
            CeDian showcd = null;
           CeDian[] List=  GlobalParams.AllCeDianList.allcedianlist.Values.ToArray<CeDian>();
           for (int i = 0; i < List.Length; i++)
           {
               CeDian cd = List[i];
               if (cd.FenZhanHao == fds && cd.TongDaoHao == chan && cd.DaLeiXing!=2)
               {
                   showcd = cd;
                   break;
               }
           
           }
            return showcd;
        }
        public static int GetLastCeDianId(string ceDianBianHao)
        {
            string SQl = "select Max(id)  from CeDian where ceDianBianHao='" + ceDianBianHao + "'";
            DataTable dt = OperateDB.Select(SQl);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
        public static int GetLastKZLCeDianId(string ceDianBianHao)
        {
            string SQl = "select Max(id)  from KongZhiLiangCeDian where ceDianBianHao='" + ceDianBianHao + "'";
            DataTable dt = OperateDB.Select(SQl);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
        /// <summary>
        /// 得到开关量列表
        /// </summary>
        /// <returns></returns>
        public static string[] GetKglPnts()
        {
            List<string> pntList = new List<string>();
            string[] pnts = GlobalParams.AllCeDianList.getCeDianAllInfo(3);
            for (int i = 0; i < pnts.Length; i++)
            {
                if (pnts[i].Contains("A")) continue;
                pntList.Add(pnts[i]);
            }
            return pntList.ToArray();
        }
        public static DataTable GetMeasure(DateTime start, DateTime end, Int32 ceDianId)
        {
            return OperateDB.Select("SELECT *  FROM Measure where CuoShiShiJian>='" + start + "' and  CuoShiShiJian<='" + end + "' and ceDianId=" + ceDianId);
        }
        /*
        //#region 调用历史曲线
        ///// <summary>
        ///// 5秒钟数据表
        ///// </summary>
        ///// <param name="start"></param>
        ///// <param name="end"></param>
        ///// <param name="ceDianBianHao"></param>
        ///// <param name="ceDianId"></param>
        ///// <returns></returns>
        //public static DataTable GetAllVlaueTable(DateTime start, DateTime end, string ceDianBianHao, int ceDianId)
        //{
        //    if (start > DateTime.Now) return null;
        //    if (end > DateTime.Now) end = DateTime.Now;
        //    if ((end - start).TotalDays< 1)
        //    {
        //        return GetHisTableMaxOneDay(start, end, ceDianBianHao, ceDianId);
        //    }
        //    else
        //    {
        //        return GetHisTableMaxOneMonth(start, end, ceDianBianHao, ceDianId);
        //    }          
        //}
        ///// <summary>
        ///// 历史曲线 
        ///// 取值区间
        ///// 0-----2 时    5秒   （历史记录值）
        ///// 2-----5 时    30秒     （历史记录值）
        ///// 5-----24时    60秒     （历史记录值）
        ///// 1-----3天     5分钟     （五分钟统计值）
        ///// 3-----10天    30分钟    （五分钟统计值）
        ///// 10----30天    1小时      （五分钟统计值）
        ///// </summary>
        //public static void GetHisCurveData(DataTable dt, DateTime start, DateTime end, ref CurveInfo CallData, ref CurveInfo MaxData, ref CurveInfo MinData, ref CurveInfo AvgData)
        //{
        //    if (start > DateTime.Now) return;
        //    if (end > DateTime.Now) end = DateTime.Now;
        //    if ((end - start).TotalDays < 1)
        //    {
        //        GetHisCurveMaxOneDay(dt,start, end, ref CallData, ref MaxData, ref  MinData, ref  AvgData);
        //    }
        //    else
        //    {
        //        GetHisCurveMaxOneMonth(dt, start, end, ref CallData, ref MaxData, ref  MinData, ref  AvgData);
        //    }  
        //}

        //#region 小于一天
        ///// <summary>
        ///// 小于一天的数据集
        ///// </summary>
        ///// <param name="start"></param>
        ///// <param name="end"></param>
        ///// <param name="ceDianBianHao"></param>
        ///// <param name="ceDianId"></param>
        ///// <returns></returns>
        //public static DataTable GetHisTableMaxOneDay(DateTime start, DateTime end, string ceDianBianHao, int ceDianId)
        //{
        //    string tableName = "";
        //    switch (ceDianBianHao[2])
        //    { 
        //        case 'A':
        //            tableName = "MoNiLiangValue";
        //            tableName += start.ToString("yyyy_MM");
        //            break;
        //        case 'D':
        //        case 'F':
        //            tableName = "KaiGuanLiangValue";
        //            tableName += start.ToString("yyyy_MM");
        //            break;
        //        case 'C':
        //            tableName = "KongZhiLiangValue";
        //            break;
        //    }
        //   //查询开始时间前一个值的时间
        //    string s = "select max(uploadTime) from " + tableName + " where uploadTime<='" + start + "' and ceDianId='" + ceDianId + "'";
        //    DataTable dt = OperateDB.Select(s);
        //    DateTime lasttime = start;
        //    if (dt == null)//发生异常：表名不存在，直接返回
        //    {
        //        return null;
        //    }
        //    if (dt.Rows.Count > 0)
        //    {
        //        if (dt.Rows[0][0] == DBNull.Value)//查询开始时间前测点不存在
        //        {
        //            s = "select min(uploadTime) from " + tableName + " where uploadTime<='" + end + "' and ceDianId='" + ceDianId + "'";
        //            dt = OperateDB.Select(s);
        //            if (dt == null) return null;
        //            if (dt.Rows[0][0] == DBNull.Value) return null;
        //        }
        //        lasttime = Convert.ToDateTime(dt.Rows[0][0]);
        //    }
        //    if (ceDianBianHao[2] != 'C')
        //    {
        //        s = "select uploadTime,uploadValue,kuiDianYiChang from " + tableName + " where ceDianId='" + ceDianId + "' and uploadTime>='" + lasttime + "' and uploadTime<='" + end + "' order by uploadTime";
        //    }
        //    else
        //    {
        //        s = "select uploadTime,uploadValue from " + tableName + " where ceDianId='" + ceDianId + "' and uploadTime>='" + lasttime + "' and uploadTime<='" + end + "' order by uploadTime";
        //    }
        //    return OperateDB.Select(s);
        //}
        ///// <summary>
        ///// 历史曲线 小于一天的数据
        ///// 取值区间
        ///// 0-----2 时    5秒   （历史记录值）
        ///// 2-----5 时    30秒     （历史记录值）
        ///// 5-----24时    60秒     （历史记录值）
        ///// </summary>  
        //public static void GetHisCurveMaxOneDay(DataTable dt, DateTime start, DateTime end, ref CurveInfo CallData, ref CurveInfo MaxData, ref CurveInfo MinData, ref CurveInfo AvgData)
        //{
        //    if (dt == null) return;
        //    int secondTick = 5;
        //    double TotalHours = (end - start).TotalHours;
        //    if (TotalHours < 2)
        //    {
        //        secondTick = 5;
        //    }
        //    else if (TotalHours > 2 && TotalHours <= 5)
        //    {
        //        secondTick = 30;
        //    }
        //    else
        //    {
        //        secondTick = 60;
        //    }
        //    int selectPointCount = (int)(TotalHours * 3600 / secondTick);
        //    selectPointCount = selectPointCount + 1;
        //    CallData.CurveData = new double[selectPointCount];
        //    CallData.KuiDianYiChang = new byte[selectPointCount];
        //    CallData.TimeStamps = new DateTime[selectPointCount];
        //    MaxData.CurveData = new double[selectPointCount];
        //    MinData.CurveData = new double[selectPointCount];
        //    AvgData.CurveData = new double[selectPointCount];


        //    //获取查询开始时间的前一个值
        //    object obj = dt.Compute("max(uploadTime)", " uploadTime<='" + start + "'");
        //    if (obj == DBNull.Value)
        //    {
        //        obj = dt.Compute("min(uploadTime)", " uploadTime<='" + end + "'");
        //    }
        //    DateTime PreDataTime = Convert.ToDateTime(obj);
        //    DataRow[] drs = dt.Select("uploadTime>='" + PreDataTime + "' and uploadTime<='" + end + "'");
        //    if (drs.Length == 0) return;
        //    int index = 0;
        //    ///分钟统计是当前显示区间的倍数
        //    int Num = 300 / secondTick;
        //    DateTime timeIndex = start;
        //    byte KuiDianYiChang = 0;
        //    double SpanMaxValue = double.MinValue, value = 0;
        //    double SumValue = 0, MaxValue = double.MinValue, MinValue = double.MaxValue, CountValue = 0, avgValue = 0;
        //    for (int i = 0; i < selectPointCount; i++)
        //    {
        //        MaxData.CurveData[i] = Chart.NoValue;
        //        MinData.CurveData[i] = Chart.NoValue;
        //        AvgData.CurveData[i] = Chart.NoValue;
        //        CallData.CurveData[i] = Chart.NoValue;
        //        DateTime time1 = Convert.ToDateTime(drs[index]["uploadTime"]);
        //        if (time1 > timeIndex)//前面没有记录
        //        {
        //            CallData.TimeStamps[i] = timeIndex;
        //            timeIndex = timeIndex.AddSeconds(secondTick);
        //            continue;
        //        }
        //        SpanMaxValue = Math.Round(Convert.ToSingle(drs[index]["uploadValue"]), 2);
        //        while (time1 < timeIndex)
        //        {
        //            value = Math.Round(Convert.ToSingle(drs[index]["uploadValue"]), 2);
        //            if (SpanMaxValue < value)
        //            {
        //                SpanMaxValue = value;
        //            }
        //            KuiDianYiChang = Convert.ToByte(KuiDianYiChang | Convert.ToByte(dt.Rows[index]["KuiDianYiChang"]));
        //            if (MaxValue < value)
        //            {
        //                MaxValue = value;
        //            }
        //            if (MinValue > value)
        //            {
        //                MinValue = value;
        //            }
        //            SumValue = SumValue + value;
        //            CountValue = CountValue + 1;
        //            if (index < dt.Rows.Count - 1)
        //            {
        //                index++;
        //            }
        //            else
        //            {
        //                break;
        //            }
        //            time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);
        //        }
        //        if (i != 0)
        //        {
        //            ///整除产生统计值
        //            if (i % (Num) == 0)
        //            {
        //                avgValue = Math.Round(SumValue / CountValue, 2);
        //                for (int j = (i / Num - 1) * Num + 1; j <= i; j++)
        //                {
        //                    MaxData.CurveData[j] = MaxValue;
        //                    MinData.CurveData[j] = MinValue;
        //                    AvgData.CurveData[j] = avgValue;
        //                }
        //                SumValue = 0;
        //                CountValue = 0;
        //                MaxValue = double.MinValue;
        //                MinValue = double.MaxValue;
        //            }
        //        }
        //        else
        //        {
        //            MaxData.CurveData[i] = SpanMaxValue;
        //            MinData.CurveData[i] = SpanMaxValue;
        //            AvgData.CurveData[i] = SpanMaxValue;
        //        }
        //        CallData.CurveData[i] = SpanMaxValue;
        //        CallData.TimeStamps[i] = timeIndex;
        //        CallData.KuiDianYiChang[i] = KuiDianYiChang;
        //        timeIndex = timeIndex.AddSeconds(secondTick);
        //        KuiDianYiChang = 0;
        //        if (time1 > timeIndex)
        //        {
        //            index--;
        //        }
        //    }
        //    if ((selectPointCount - 1) % Num != 0)
        //    {
        //        avgValue = Math.Round(SumValue / CountValue, 2);
        //        for (int j = (selectPointCount / Num) * Num + 1; j < selectPointCount; j++)
        //        {
        //            MaxData.CurveData[j] = MaxValue;
        //            MinData.CurveData[j] = MinValue;
        //            AvgData.CurveData[j] = avgValue;
        //        }
        //    }
        //}
        // #endregion

        //#region 小于一月
        ///// <summary>
        ///// 小于一月的数据集
        ///// </summary>
        ///// <param name="start"></param>
        ///// <param name="end"></param>
        ///// <param name="ceDianBianHao"></param>
        ///// <param name="ceDianId"></param>
        ///// <returns></returns>
        //public static DataTable GetHisTableMaxOneMonth(DateTime start, DateTime end, string ceDianBianHao, int ceDianId)
        //{
        //    string tableName = "";
        //    if (ceDianBianHao[2] == 'A')
        //    {
        //        tableName = "MoNiLiangCountValue";
        //    }
        //    tableName += start.ToString("yyyy_MM");
        //    string s = "select max(uploadTime) from " + tableName + " where uploadTime<='" + start + "' and ceDianId='" + ceDianId + "'";
        //    DataTable dt = OperateDB.Select(s);
        //    DateTime lasttime = start;
        //    if (dt == null)//发生异常：表名不存在，直接返回
        //    {
        //        return null;
        //    }
        //    if (dt.Rows.Count > 0)
        //    {
        //        if (dt.Rows[0][0] == DBNull.Value)
        //        {
        //            s = "select min(uploadTime) from " + tableName + " where uploadTime<='" + end + "' and ceDianId='" + ceDianId + "'";
        //            dt = OperateDB.Select(s);
        //            if (dt == null) return null;
        //            if (dt.Rows[0][0] == DBNull.Value) return null;
        //        }
        //        lasttime = Convert.ToDateTime(dt.Rows[0][0]);
        //    }
        //    s = "select uploadTime,MaxValue,MinValue,VagValue from " + tableName + " where ceDianId='" + ceDianId + "' and uploadTime>='" + lasttime + "' and uploadTime<='" + end + "' order by uploadTime";
        //    return OperateDB.Select(s);
        //}
        ///// <summary>
        ///// 历史曲线 
        ///// 取值区间
        ///// 1-----3天     5分钟     （五分钟统计值）
        ///// 3-----10天    30分钟    （五分钟统计值）
        ///// 10----30天    1小时      （五分钟统计值）
        ///// </summary>
        //public static void GetHisCurveMaxOneMonth(DataTable dt, DateTime start, DateTime end, ref CurveInfo CallData, ref CurveInfo MaxData, ref CurveInfo MinData, ref CurveInfo AvgData)
        //{
        //    if (dt == null) return;
        //    int dateTick = 5;
        //    double TotalDays = (end - start).TotalDays;
        //    if (TotalDays < 3)
        //    {
        //        dateTick = 5;
        //    }
        //    else if (TotalDays > 3 && TotalDays <= 10)
        //    {
        //        dateTick = 30;
        //    }
        //    else
        //    {
        //        dateTick = 60;
        //    }
        //    int selectPointCount = (int)(TotalDays * 1440 / dateTick);
        //    selectPointCount = selectPointCount + 1;
        //    CallData.CurveData = new double[selectPointCount];
        //    CallData.KuiDianYiChang = new byte[selectPointCount];
        //    CallData.TimeStamps = new DateTime[selectPointCount];
        //    MaxData.CurveData = new double[selectPointCount];
        //    MinData.CurveData = new double[selectPointCount];
        //    AvgData.CurveData = new double[selectPointCount];

        //    //获取查询开始时间的前一个值
        //    object obj = dt.Compute("max(uploadTime)", " uploadTime<='" + start + "'");
        //    if (obj == DBNull.Value)
        //    {
        //        obj = dt.Compute("min(uploadTime)", " uploadTime<='" + end + "'");
        //    }
        //    DateTime PreDataTime = Convert.ToDateTime(obj);
        //    DataRow[] drs = dt.Select("uploadTime>='" + PreDataTime + "' and uploadTime<='" + end + "'");
        //    if (drs.Length == 0) return;
        //    int index = 0;
        //    DateTime timeIndex = start;
        //    double maxvalue,minvalue,avgvalue;
        //    double SumValue = 0, MaxValue = double.MinValue, MinValue = double.MaxValue, CountValue = 0, avgValue = 0;
        //    for (int i = 0; i < selectPointCount; i++)
        //    {
        //        MaxData.CurveData[i] = Chart.NoValue;
        //        MinData.CurveData[i] = Chart.NoValue;
        //        AvgData.CurveData[i] = Chart.NoValue;
        //        DateTime time1 = Convert.ToDateTime(drs[index]["uploadTime"]);
        //        if (time1 > timeIndex)//前面没有记录
        //        {
        //            CallData.TimeStamps[i] = timeIndex;
        //            timeIndex = timeIndex.AddMinutes(dateTick);
        //            continue;
        //        }
        //        do
        //        {
        //            maxvalue = Math.Round(Convert.ToSingle(drs[index]["MaxValue"]), 2);
        //            minvalue = Math.Round(Convert.ToSingle(drs[index]["MinValue"]), 2);
        //            avgvalue = Math.Round(Convert.ToSingle(drs[index]["VagValue"]), 2);
        //            if (MaxValue < maxvalue)
        //            {
        //                MaxValue = maxvalue;
        //            }
        //            if (MinValue > minvalue)
        //            {
        //                MinValue = minvalue;
        //            }
        //            SumValue = SumValue + avgvalue;
        //            CountValue = CountValue + 1;
        //            if (index < dt.Rows.Count - 1)
        //            {
        //                index++;
        //            }
        //            else
        //            {
        //                break;
        //            }
        //            time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);
        //        }
        //        while (time1 < timeIndex);
        //        MaxData.CurveData[i] = MaxValue;
        //        MinData.CurveData[i] = MinValue;
        //        AvgData.CurveData[i] = Math.Round(SumValue / CountValue, 2); 
        //        SumValue = 0;
        //        CountValue = 0;    
        //        CallData.TimeStamps[i] = timeIndex;
        //        timeIndex = timeIndex.AddMinutes(dateTick);
        //        if (time1 > timeIndex)
        //        {
        //            index--;
        //        }
        //    }           
        //    CallData.CurveData = AvgData.CurveData;
        //}

        //#endregion
        //#endregion
         */

        #region 调用历史曲线
        /// <summary>
        /// 5秒钟数据表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ceDianBianHao"></param>
        /// <param name="ceDianId"></param>
        /// <returns></returns>
        public static DataTable GetAllVlaueTable(DateTime start, DateTime end, string ceDianBianHao, int ceDianId)
        {
            if (start > DateTime.Now) return null;
            if (end > DateTime.Now) end = DateTime.Now;
            return GetHisTableMaxOneDay(start, end, ceDianBianHao, ceDianId);
        }
        /// <summary>
        /// 历史曲线 
        /// 取值区间
        /// 0-----2 时    5秒   （历史记录值）
        /// 2-----5 时    30秒     （历史记录值）
        /// 5-----24时    60秒     （历史记录值）
        /// 1-----3天     5分钟     （历史记录值）
        /// 3-----10天    30分钟    （历史记录值）
        /// 10----30天    1小时      （历史记录值）
        /// </summary>
        public static void GetHisCurveData(DataTable dt, DateTime start, DateTime end, ref CurveInfo CallData, ref CurveInfo MaxData, ref CurveInfo MinData, ref CurveInfo AvgData)
        {
            if (start > DateTime.Now) return;
            if (end > DateTime.Now) end = DateTime.Now;
              GetHisCurveMaxOneDay(dt, start, end, ref CallData, ref MaxData, ref  MinData, ref  AvgData);
        }

        #region 小于一天
        /// <summary>
        /// 小于一天的数据集
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ceDianBianHao"></param>
        /// <param name="ceDianId"></param>
        /// <returns></returns>
        public static DataTable GetHisTableMaxOneDay(DateTime start, DateTime end, string ceDianBianHao, int ceDianId)
        {
            string tableName = "";
            switch (ceDianBianHao[2])
            {
                case 'A':
                    tableName = "MoNiLiangValue";
                    tableName += start.ToString("yyyy_MM");
                    break;
                case 'D':
                case 'F':
                    tableName = "KaiGuanLiangValue";
                    tableName += start.ToString("yyyy_MM");
                    break;
                case 'C':
                    tableName = "KongZhiLiangValue";
                    break;
            }
            //查询开始时间前一个值的时间
            string s = "select max(uploadTime) from " + tableName + " where uploadTime<='" + start + "' and ceDianId='" + ceDianId + "'";
            DataTable dt = OperateDB.Select(s);
            DateTime lasttime = start;
            if (dt == null)//发生异常：表名不存在，直接返回
            {
                return null;
            }
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0] == DBNull.Value)//查询开始时间前测点不存在
                {
                    s = "select min(uploadTime) from " + tableName + " where uploadTime<='" + end + "' and ceDianId='" + ceDianId + "'";
                    dt = OperateDB.Select(s);
                    if (dt == null) return null;
                    if (dt.Rows[0][0] == DBNull.Value) return null;
                }
                lasttime = Convert.ToDateTime(dt.Rows[0][0]);
            }
            if (ceDianBianHao[2] != 'C')
            {
                s = "select uploadTime,uploadValue,kuiDianYiChang from " + tableName + " where ceDianId='" + ceDianId + "' and uploadTime>='" + lasttime + "' and uploadTime<='" + end + "' order by uploadTime";
            }
            else
            {
                s = "select uploadTime,uploadValue from " + tableName + " where ceDianId='" + ceDianId + "' and uploadTime>='" + lasttime + "' and uploadTime<='" + end + "' order by uploadTime";
            }
            return OperateDB.Select(s);
        }
        /// <summary>
        /// 历史曲线 小于一天的数据
        /// 取值区间
        /// 0-----2 时    5秒   （历史记录值）
        /// 2-----5 时    30秒     （历史记录值）
        /// 5-----24时    60秒     （历史记录值）
        /// </summary>  
        public static void GetHisCurveMaxOneDay(DataTable dt, DateTime start, DateTime end, ref CurveInfo CallData, ref CurveInfo MaxData, ref CurveInfo MinData, ref CurveInfo AvgData)
        {
            if (dt == null) return;
            int secondTick = 5;
            ///分钟统计是当前显示区间的倍数
            int Num = 300 / secondTick;
            double TotalHours = (end - start).TotalHours;
            if (TotalHours < 2)
            {
                secondTick = 5;
            }
            else if (TotalHours > 2 && TotalHours <= 5)
            {
                secondTick = 30;
            }
            else if (TotalHours > 5 && TotalHours <= 24)
            {
                secondTick = 60;
            }
            else if (TotalHours > 24 && TotalHours <= 72)
            {
                secondTick = 300;
            }
            else if (TotalHours > 72 && TotalHours <= 240)
            {
                secondTick = 1800;
                Num = 1800 / secondTick;
            }
            else
            {
                secondTick = 3600;
                Num = 3600 / secondTick;
            }
            int selectPointCount = (int)(TotalHours * 3600 / secondTick);
            selectPointCount = selectPointCount + 1;
            CallData.CurveData = new double[selectPointCount];
            CallData.KuiDianYiChang = new byte[selectPointCount];
            CallData.TimeStamps = new DateTime[selectPointCount];
            MaxData.CurveData = new double[selectPointCount];
            MinData.CurveData = new double[selectPointCount];
            AvgData.CurveData = new double[selectPointCount];

            //获取查询开始时间的前一个值
            object obj = dt.Compute("max(uploadTime)", " uploadTime<='" + start + "'");
            if (obj == DBNull.Value)
            {
                obj = dt.Compute("min(uploadTime)", " uploadTime<='" + end + "'");
            }
            DateTime PreDataTime = Convert.ToDateTime(obj);
            DataRow[] drs = dt.Select("uploadTime>='" + PreDataTime + "' and uploadTime<='" + end + "'");
            if (drs.Length == 0) return;
            int index = 0;

            DateTime timeIndex = start;
            byte KuiDianYiChang = 0;
            double SpanMaxValue = double.MinValue, value = 0;
            double SumValue = 0, MaxValue = double.MinValue, MinValue = double.MaxValue, CountValue = 0, avgValue = 0;
            for (int i = 0; i < selectPointCount; i++)
            {
                MaxData.CurveData[i] = Chart.NoValue;
                MinData.CurveData[i] = Chart.NoValue;
                AvgData.CurveData[i] = Chart.NoValue;
                CallData.CurveData[i] = Chart.NoValue;
                DateTime time1 = Convert.ToDateTime(drs[index]["uploadTime"]);
                if (time1 > timeIndex)//前面没有记录
                {
                    CallData.TimeStamps[i] = timeIndex;
                    timeIndex = timeIndex.AddSeconds(secondTick);
                    continue;
                }
                SpanMaxValue = Math.Round(Convert.ToSingle(drs[index]["uploadValue"]), 2);
                while (time1 < timeIndex)
                {
                    value = Math.Round(Convert.ToSingle(drs[index]["uploadValue"]), 2);
                    if (SpanMaxValue < value)
                    {
                        SpanMaxValue = value;
                    }
                    KuiDianYiChang = Convert.ToByte(KuiDianYiChang | Convert.ToByte(dt.Rows[index]["KuiDianYiChang"]));
                    if (MaxValue < value)
                    {
                        MaxValue = value;
                    }
                    if (MinValue > value)
                    {
                        MinValue = value;
                    }
                    SumValue = SumValue + value;
                    CountValue = CountValue + 1;
                    if (index < dt.Rows.Count - 1)
                    {
                        index++;
                    }
                    else
                    {
                        break;
                    }
                    time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);
                }
                if (i != 0)
                {
                    ///整除产生统计值
                    if (i % (Num) == 0)
                    {
                        avgValue = Math.Round(SumValue / CountValue, 2);
                        for (int j = (i / Num - 1) * Num + 1; j <= i; j++)
                        {
                            MaxData.CurveData[j] = MaxValue;
                            MinData.CurveData[j] = MinValue;
                            AvgData.CurveData[j] = avgValue;
                        }
                        SumValue = 0;
                        CountValue = 0;
                        MaxValue = double.MinValue;
                        MinValue = double.MaxValue;
                    }
                }
                else
                {
                    MaxData.CurveData[i] = SpanMaxValue;
                    MinData.CurveData[i] = SpanMaxValue;
                    AvgData.CurveData[i] = SpanMaxValue;
                }
                CallData.CurveData[i] = SpanMaxValue;
                CallData.TimeStamps[i] = timeIndex;
                CallData.KuiDianYiChang[i] = KuiDianYiChang;
                timeIndex = timeIndex.AddSeconds(secondTick);
                KuiDianYiChang = 0;
                if (time1 > timeIndex)
                {
                    index--;
                }
            }
            if ((selectPointCount - 1) % Num != 0)
            {
                avgValue = Math.Round(SumValue / CountValue, 2);
                for (int j = (selectPointCount / Num) * Num + 1; j < selectPointCount; j++)
                {
                    MaxData.CurveData[j] = MaxValue;
                    MinData.CurveData[j] = MinValue;
                    AvgData.CurveData[j] = avgValue;
                }
            }
        }
        #endregion

        #endregion
        #region 报警曲线
        /// <summary>
        /// 数据集
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ceDianBianHao"></param>
        /// <param name="ceDianId"></param>
        /// <returns></returns>
        public static DataTable GetHisTableAlarm(DateTime start, DateTime end, string ceDianBianHao, int ceDianId)
        {
            if (start > DateTime.Now) return null;
            if (end > DateTime.Now) end = DateTime.Now;
            string tableName = "";
            if (ceDianBianHao[2] == 'A')
            {
                tableName = "MoNiLiangValue";
            }
            else if (ceDianBianHao[2] == 'D')
            {
                tableName = "KaiGuanLiangValue";
            }
            tableName += start.ToString("yyyy_MM");
            string s = "select max(uploadTime) from " + tableName + " where uploadTime<='" + start + "' and ceDianId='" + ceDianId + "'";
            DataTable dt = OperateDB.Select(s);
            DateTime lasttime = start;
            if (dt == null)//发生异常：表名不存在，直接返回
            {
                return null;
            }
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0] == DBNull.Value)
                {
                    s = "select min(uploadTime) from " + tableName + " where uploadTime<='" + end + "' and ceDianId='" + ceDianId + "'";
                    dt = OperateDB.Select(s);
                    if (dt == null) return null;
                    if (dt.Rows[0][0] == DBNull.Value) return null;
                }
                lasttime = Convert.ToDateTime(dt.Rows[0][0]);
            }
            s = "select uploadTime,uploadValue,kuiDianYiChang,state from " + tableName + " where ceDianId='" + ceDianId + "' and uploadTime>='" + lasttime + "' and uploadTime<='" + end + "' order by uploadTime";
            return OperateDB.Select(s);
        }
        /// <summary>
        /// 历史曲线 小于一天的数据
        /// 取值区间
        /// 0-----2 时    5秒   （历史记录值）
        /// 2-----5 时    30秒     （历史记录值）
        /// 5-----24时    60秒     （历史记录值）
        /// 1-----3天     5分钟     （历史记录值）
        /// 3-----10天    30分钟    （历史记录值）
        /// 10----30天    1小时      （历史记录值）
        /// </summary>
        public static void GetHisCurveAlarm(DataTable dt, DateTime start, DateTime end, int ceDianId, ref CurveInfo CallData, ref CurveInfo MaxData, ref CurveInfo MinData, ref CurveInfo AvgData, ref  List<AlarmInfo> alarmInfoList)
        {
            if (start > DateTime.Now) return;
            if (end > DateTime.Now) end = DateTime.Now;
            alarmInfoList.Clear();
            if (dt == null) return;
            int secondTick = 5;
            int Num = 300 / secondTick;
            double TotalHours = (end - start).TotalHours;
            if (TotalHours < 2)
            {
                secondTick = 5;
            }
            else if (TotalHours > 2 && TotalHours <= 5)
            {
                secondTick = 30;
            }
            else if (TotalHours > 5 && TotalHours <= 24)
            {
                secondTick = 60;
            }
            else if (TotalHours > 24 && TotalHours <= 72)
            {
                secondTick = 300;
            }
            else if (TotalHours > 72 && TotalHours <= 240)
            {
                secondTick = 1800;
                Num = 1800 / secondTick;
            }
            else
            {
                secondTick = 3600;
                Num = 3600 / secondTick;
            }
            int selectPointCount = (int)(TotalHours * 3600 / secondTick);
            selectPointCount = selectPointCount + 1;
            CallData.CurveData = new double[selectPointCount];
            CallData.KuiDianYiChang = new byte[selectPointCount];
            CallData.TimeStamps = new DateTime[selectPointCount];
            MaxData.CurveData = new double[selectPointCount];
            MinData.CurveData = new double[selectPointCount];
            AvgData.CurveData = new double[selectPointCount];
            CallData.AlarmIndex = new int[selectPointCount];
            int[] StateTmp = new int[selectPointCount];
            //获取查询开始时间的前一个值
            object obj = dt.Compute("max(uploadTime)", " uploadTime<='" + start + "'");
            if (obj == DBNull.Value)
            {
                obj = dt.Compute("min(uploadTime)", " uploadTime<='" + end + "'");
            }
            DateTime PreDataTime = Convert.ToDateTime(obj);
            DataRow[] drs = dt.Select("uploadTime>='" + PreDataTime + "' and uploadTime<='" + end + "'");
            if (drs.Length == 0) return;
            bool IsExistMeasure = false;
            DataTable Measuredt = OperateDB.Select("SELECT *  FROM Measure where CuoShiShiJian>='" + start + "' and  CuoShiShiJian<='" + end + "' and ceDianId="+ceDianId);
            if (Measuredt != null)
            {
                if (Measuredt.Rows.Count > 0)
                {
                    IsExistMeasure = true;
                }
            }
            int index = 0;
            ///分钟统计是当前显示区间的倍数            
            DateTime timeIndex = start;
            byte KuiDianYiChang = 0;
            double SpanMaxValue = double.MinValue, value = 0;
            double SumValue = 0, MaxValue = double.MinValue, MinValue = double.MaxValue, CountValue = 0, avgValue = 0;
            AlarmInfo curAlarmInfo = null;
            int state = 0;
            for (int i = 0; i < selectPointCount; i++)
            {
                MaxData.CurveData[i] = Chart.NoValue;
                MinData.CurveData[i] = Chart.NoValue;
                AvgData.CurveData[i] = Chart.NoValue;
                CallData.CurveData[i] = Chart.NoValue;
                CallData.TimeStamps[i] = timeIndex;
                CallData.AlarmIndex[i] = -1;

                DateTime time1 = Convert.ToDateTime(drs[index]["uploadTime"]);
                if (time1 > timeIndex)//前面没有记录
                {                   
                    timeIndex = timeIndex.AddSeconds(secondTick);
                    continue;
                }
                do
                {
                    value = Math.Round(Convert.ToSingle(drs[index]["uploadValue"]), 2);
                    KuiDianYiChang = Convert.ToByte(KuiDianYiChang | Convert.ToByte(dt.Rows[index]["KuiDianYiChang"]));
                    if (state == 1 || Convert.ToInt32(dt.Rows[index]["state"]) == 1)
                    {
                        state = 1;
                    }
                    else
                    {
                        state = Convert.ToInt32(dt.Rows[index]["state"]);
                    }
                    if (SpanMaxValue < value)
                    {
                        SpanMaxValue = value;
                    }
                    if (MaxValue < value)
                    {
                        MaxValue = value;
                    }
                    if (MinValue > value)
                    {
                        MinValue = value;
                    }
                    SumValue = SumValue + value;
                    CountValue = CountValue + 1;
                    if (index < dt.Rows.Count - 1)
                    {
                        index++;
                    }
                    else
                    {
                        break;
                    }
                    time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);
                }
                while (time1 < timeIndex);
                if (i != 0)
                {
                    ///整除产生统计值
                    if (i % (Num) == 0)
                    {
                        avgValue = Math.Round(SumValue / CountValue, 2);
                        for (int j = (i / Num - 1) * Num + 1; j <= i; j++)
                        {
                            MaxData.CurveData[j] = MaxValue;
                            MinData.CurveData[j] = MinValue;
                            AvgData.CurveData[j] = avgValue;
                        }
                        SumValue = 0;
                        CountValue = 0;
                        MaxValue = double.MinValue;
                        MinValue = double.MaxValue;
                    }
                    if (StateTmp[i - 1] != state)
                    {
                        if (state == 1)
                        {
                            curAlarmInfo = new AlarmInfo();
                            curAlarmInfo.startTime = timeIndex;
                            alarmInfoList.Add(curAlarmInfo);
                        }
                        else if (StateTmp[i - 1] == 1)//报警结束
                        {
                            curAlarmInfo.endTime = timeIndex;
                            if (IsExistMeasure)
                            {
                                DataRow[] drs2 = Measuredt.Select("CuoShiShiJian>='" + curAlarmInfo.startTime + "' and  CuoShiShiJian<='" + curAlarmInfo.endTime + "'");
                                foreach (DataRow dr in drs2)
                                {
                                    curAlarmInfo.Measure = curAlarmInfo.Measure + string.Format("{0} {1}   ", dr["CuoShiShiJian"].ToString(), dr["CuoShi"].ToString());
                                }
                            }
                        }
                    }
                    if (state == 1)
                    {
                        CallData.AlarmIndex[i] = alarmInfoList.Count - 1;
                    }
                }
                else
                {
                    MaxData.CurveData[i] = SpanMaxValue;
                    MinData.CurveData[i] = SpanMaxValue;
                    AvgData.CurveData[i] = SpanMaxValue;
                    SumValue = 0;
                    CountValue = 0;
                    MaxValue = double.MinValue;
                    MinValue = double.MaxValue;
                    if (state == 1)
                    {
                        curAlarmInfo = new AlarmInfo();
                        curAlarmInfo.startTime = timeIndex;
                        alarmInfoList.Add(curAlarmInfo);
                        CallData.AlarmIndex[i] = alarmInfoList.Count - 1;
                    }
                }
                if (state == 1)
                {
                    CallData.CurveData[i] = SpanMaxValue;
                }
                StateTmp[i] = state;
                CallData.KuiDianYiChang[i] = KuiDianYiChang;
                timeIndex = timeIndex.AddSeconds(secondTick);
                KuiDianYiChang = 0;
                SpanMaxValue = double.MinValue;
                state = 0;
                if (time1 > timeIndex)
                {
                    index--;
                }
            }
            if (StateTmp[selectPointCount - 1] == 1)
            {
                curAlarmInfo.endTime = end;
                if (IsExistMeasure)
                {
                    DataRow[] drs2 = Measuredt.Select("CuoShiShiJian>='" + curAlarmInfo.startTime + "' and  CuoShiShiJian<='" + curAlarmInfo.endTime + "'");
                    foreach (DataRow dr in drs2)
                    {
                        curAlarmInfo.Measure = curAlarmInfo.Measure + string.Format("{0} {1}   ", dr["CuoShiShiJian"].ToString(), dr["CuoShi"].ToString());
                    }
                }
            }
            if ((selectPointCount - 1) % Num != 0)
            {
                avgValue = Math.Round(SumValue / CountValue, 2);
                for (int j = (selectPointCount / Num) * Num + 1; j < selectPointCount; j++)
                {
                    MaxData.CurveData[j] = MaxValue;
                    MinData.CurveData[j] = MinValue;
                    AvgData.CurveData[j] = avgValue;
                }
            }
        }
        #endregion


        #region 断电曲线
        /// <summary>
        /// 数据集
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ceDianBianHao"></param>
        /// <param name="ceDianId"></param>
        /// <returns></returns>
        public static DataTable GetHisTableCut(DateTime start, DateTime end, string ceDianBianHao, int ceDianId)
        {
            if (start > DateTime.Now) return null;
            if (end > DateTime.Now) end = DateTime.Now;
            string tableName = "";
            if (ceDianBianHao[2] == 'A')
            {
                tableName = "MoNiLiangValue";
            }
            else if (ceDianBianHao[2] == 'D')
            {
                tableName = "KaiGuanLiangValue";
            }
            tableName += start.ToString("yyyy_MM");
            string s = "select max(uploadTime) from " + tableName + " where uploadTime<='" + start + "' and ceDianId='" + ceDianId + "'";
            DataTable dt = OperateDB.Select(s);
            DateTime lasttime = start;
            if (dt == null)//发生异常：表名不存在，直接返回
            {
                return null;
            }
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0] == DBNull.Value)
                {
                    s = "select min(uploadTime) from " + tableName + " where uploadTime<='" + end + "' and ceDianId='" + ceDianId + "'";
                    dt = OperateDB.Select(s);
                    if (dt == null) return null;
                    if (dt.Rows[0][0] == DBNull.Value) return null;
                }
                lasttime = Convert.ToDateTime(dt.Rows[0][0]);
            }
            s = "select uploadTime,uploadValue,kuiDianYiChang,state from " + tableName + " where ceDianId='" + ceDianId + "' and uploadTime>='" + lasttime + "' and uploadTime<='" + end + "' order by uploadTime";
            return OperateDB.Select(s);
        }
        /// <summary>
        /// 历史曲线 小于一天的数据
        /// 取值区间
        /// 0-----2 时    5秒   （历史记录值）
        /// 2-----5 时    30秒     （历史记录值）
        /// 5-----24时    60秒     （历史记录值）
        /// 1-----3天     5分钟     （历史记录值）
        /// 3-----10天    30分钟    （历史记录值）
        /// 10----30天    1小时      （历史记录值）
        /// </summary>
        public static void GetHisCurveCut(DataTable dt, DateTime start, DateTime end, int ceDianId, ref CurveInfo CallData, ref CurveInfo MaxData, ref CurveInfo MinData, ref CurveInfo AvgData, ref  List<AlarmInfo> alarmInfoList)
        {
            if (start > DateTime.Now) return;
            if (end > DateTime.Now) end = DateTime.Now;
            alarmInfoList.Clear();
            if (dt == null) return;
            int secondTick = 5;
            int Num = 300 / secondTick;
            double TotalHours = (end - start).TotalHours;
            if (TotalHours < 2)
            {
                secondTick = 5;
            }
            else if (TotalHours > 2 && TotalHours <= 5)
            {
                secondTick = 30;
            }
            else if (TotalHours > 5 && TotalHours <= 24)
            {
                secondTick = 60;
            }
            else if (TotalHours > 24 && TotalHours <= 72)
            {
                secondTick = 300;
            }
            else if (TotalHours > 72 && TotalHours <= 240)
            {
                secondTick = 1800;
                Num = 1800 / secondTick;
            }
            else
            {
                secondTick = 3600;
                Num = 3600 / secondTick;
            }
            int selectPointCount = (int)(TotalHours * 3600 / secondTick);
            selectPointCount = selectPointCount + 1;
            CallData.CurveData = new double[selectPointCount];
            CallData.KuiDianYiChang = new byte[selectPointCount];
            CallData.TimeStamps = new DateTime[selectPointCount];
            MaxData.CurveData = new double[selectPointCount];
            MinData.CurveData = new double[selectPointCount];
            AvgData.CurveData = new double[selectPointCount];
            CallData.AlarmIndex = new int[selectPointCount];
            int[] StateTmp = new int[selectPointCount];
            //获取查询开始时间的前一个值
            object obj = dt.Compute("max(uploadTime)", " uploadTime<='" + start + "'");
            if (obj == DBNull.Value)
            {
                obj = dt.Compute("min(uploadTime)", " uploadTime<='" + end + "'");
            }
            DateTime PreDataTime = Convert.ToDateTime(obj);
            DataRow[] drs = dt.Select("uploadTime>='" + PreDataTime + "' and uploadTime<='" + end + "'");
            if (drs.Length == 0) return;
            bool IsExistMeasure = false;
            DataTable Measuredt = OperateDB.Select("SELECT *  FROM Measure where CuoShiShiJian>='" + start + "' and  CuoShiShiJian<='" + end + "' and ceDianId=" + ceDianId);
            if (Measuredt != null)
            {
                if (Measuredt.Rows.Count > 0)
                {
                    IsExistMeasure = true;
                }
            }
            int index = 0;
            ///分钟统计是当前显示区间的倍数            
            DateTime timeIndex = start;
            byte KuiDianYiChang = 0;
            double SpanMaxValue = double.MinValue, value = 0;
            double SumValue = 0, MaxValue = double.MinValue, MinValue = double.MaxValue, CountValue = 0, avgValue = 0;
            AlarmInfo curAlarmInfo = null;
            int state = 0;
            for (int i = 0; i < selectPointCount; i++)
            {
                MaxData.CurveData[i] = Chart.NoValue;
                MinData.CurveData[i] = Chart.NoValue;
                AvgData.CurveData[i] = Chart.NoValue;
                CallData.CurveData[i] = Chart.NoValue;
                CallData.TimeStamps[i] = timeIndex;
                CallData.AlarmIndex[i] = -1;

                DateTime time1 = Convert.ToDateTime(drs[index]["uploadTime"]);
                if (time1 > timeIndex)//前面没有记录
                {
                    timeIndex = timeIndex.AddSeconds(secondTick);
                    continue;
                }
                do
                {
                    value = Math.Round(Convert.ToSingle(drs[index]["uploadValue"]), 2);
                    KuiDianYiChang = Convert.ToByte(KuiDianYiChang | Convert.ToByte(dt.Rows[index]["KuiDianYiChang"]));
                    if (state == 2 || Convert.ToInt32(dt.Rows[index]["state"]) == 2)
                    {
                        state = 2;
                    }
                    else
                    {
                        state = Convert.ToInt32(dt.Rows[index]["state"]);
                    }
                    if (SpanMaxValue < value)
                    {
                        SpanMaxValue = value;
                    }
                    if (MaxValue < value)
                    {
                        MaxValue = value;
                    }
                    if (MinValue > value)
                    {
                        MinValue = value;
                    }
                    SumValue = SumValue + value;
                    CountValue = CountValue + 1;
                    if (index < dt.Rows.Count - 1)
                    {
                        index++;
                    }
                    else
                    {
                        break;
                    }
                    time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);
                }
                while (time1 < timeIndex);
                if (i != 0)
                {
                    ///整除产生统计值
                    if (i % (Num) == 0)
                    {
                        avgValue = Math.Round(SumValue / CountValue, 2);
                        for (int j = (i / Num - 1) * Num + 1; j <= i; j++)
                        {
                            MaxData.CurveData[j] = MaxValue;
                            MinData.CurveData[j] = MinValue;
                            AvgData.CurveData[j] = avgValue;
                        }
                        SumValue = 0;
                        CountValue = 0;
                        MaxValue = double.MinValue;
                        MinValue = double.MaxValue;
                    }
                    if (StateTmp[i - 1] != state)
                    {
                        if (state == 2)
                        {
                            curAlarmInfo = new AlarmInfo();
                            curAlarmInfo.startTime = timeIndex;
                            alarmInfoList.Add(curAlarmInfo);
                        }
                        else if (StateTmp[i - 1] == 2)//报警结束
                        {
                            curAlarmInfo.endTime = timeIndex;
                            if (IsExistMeasure)
                            {
                                DataRow[] drs2 = Measuredt.Select("CuoShiShiJian>='" + curAlarmInfo.startTime + "' and  CuoShiShiJian<='" + curAlarmInfo.endTime + "'");
                                foreach (DataRow dr in drs2)
                                {
                                    curAlarmInfo.Measure = curAlarmInfo.Measure + string.Format("{0} {1}   ", dr["CuoShiShiJian"].ToString(), dr["CuoShi"].ToString());
                                }
                            }
                        }
                    }
                    if (state == 2)
                    {
                        CallData.AlarmIndex[i] = alarmInfoList.Count - 1;
                    }
                }
                else
                {
                    MaxData.CurveData[i] = SpanMaxValue;
                    MinData.CurveData[i] = SpanMaxValue;
                    AvgData.CurveData[i] = SpanMaxValue;
                    SumValue = 0;
                    CountValue = 0;
                    MaxValue = double.MinValue;
                    MinValue = double.MaxValue;
                    if (state == 2)
                    {
                        curAlarmInfo = new AlarmInfo();
                        curAlarmInfo.startTime = timeIndex;
                        alarmInfoList.Add(curAlarmInfo);
                        CallData.AlarmIndex[i] = alarmInfoList.Count - 1;
                    }
                }
                if (state == 2)
                {
                    CallData.CurveData[i] = SpanMaxValue;
                }
                StateTmp[i] = state;
                CallData.KuiDianYiChang[i] = KuiDianYiChang;
                timeIndex = timeIndex.AddSeconds(secondTick);
                KuiDianYiChang = 0;
                SpanMaxValue = double.MinValue;
                state = 0;
                if (time1 > timeIndex)
                {
                    index--;
                }
            }
            if (StateTmp[selectPointCount - 1] == 1)
            {
                curAlarmInfo.endTime = end;
                if (IsExistMeasure)
                {
                    DataRow[] drs2 = Measuredt.Select("CuoShiShiJian>='" + curAlarmInfo.startTime + "' and  CuoShiShiJian<='" + curAlarmInfo.endTime + "'");
                    foreach (DataRow dr in drs2)
                    {
                        curAlarmInfo.Measure = curAlarmInfo.Measure + string.Format("{0} {1}   ", dr["CuoShiShiJian"].ToString(), dr["CuoShi"].ToString());
                    }
                }
            }
            if ((selectPointCount - 1) % Num != 0)
            {
                avgValue = Math.Round(SumValue / CountValue, 2);
                for (int j = (selectPointCount / Num) * Num + 1; j < selectPointCount; j++)
                {
                    MaxData.CurveData[j] = MaxValue;
                    MinData.CurveData[j] = MinValue;
                    AvgData.CurveData[j] = avgValue;
                }
            }
        }
        #endregion


        #region 馈电曲线
        /// <summary>
        /// 数据集
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="ceDianBianHao"></param>
        /// <param name="ceDianId"></param>
        /// <returns></returns>
        public static DataTable GetHisTableFeed(DateTime start, DateTime end, string ceDianBianHao, int ceDianId)
        {
            if (start > DateTime.Now) return null;
            if (end > DateTime.Now) end = DateTime.Now;
            string tableName = "";
            if (ceDianBianHao[2] == 'A')
            {
                tableName = "MoNiLiangValue";
            }
            else if (ceDianBianHao[2] == 'D')
            {
                tableName = "KaiGuanLiangValue";
            }
            tableName += start.ToString("yyyy_MM");
            string s = "select max(uploadTime) from " + tableName + " where uploadTime<='" + start + "' and ceDianId='" + ceDianId + "'";
            DataTable dt = OperateDB.Select(s);
            DateTime lasttime = start;
            if (dt == null)//发生异常：表名不存在，直接返回
            {
                return null;
            }
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0] == DBNull.Value)
                {
                    s = "select min(uploadTime) from " + tableName + " where uploadTime<='" + end + "' and ceDianId='" + ceDianId + "'";
                    dt = OperateDB.Select(s);
                    if (dt == null) return null;
                    if (dt.Rows[0][0] == DBNull.Value) return null;
                }
                lasttime = Convert.ToDateTime(dt.Rows[0][0]);
            }
            s = "select uploadTime,uploadValue,kuiDianYiChang from " + tableName + " where ceDianId='" + ceDianId + "' and uploadTime>='" + lasttime + "' and uploadTime<='" + end + "' order by uploadTime";
            return OperateDB.Select(s);
        }
        /// <summary>
        /// 历史曲线 小于一天的数据
        /// 取值区间
        /// 0-----2 时    5秒   （历史记录值）
        /// 2-----5 时    30秒     （历史记录值）
        /// 5-----24时    60秒     （历史记录值）
        /// 1-----3天     5分钟     （历史记录值）
        /// 3-----10天    30分钟    （历史记录值）
        /// 10----30天    1小时      （历史记录值）
        /// </summary>
        public static void GetHisCurveFeed(DataTable dt, DateTime start, DateTime end, int ceDianId, ref CurveInfo CallData, ref CurveInfo MaxData, ref CurveInfo MinData, ref CurveInfo AvgData, ref  List<AlarmInfo> alarmInfoList)
        {
            if (start > DateTime.Now) return ;
            if (end > DateTime.Now) end = DateTime.Now;
            alarmInfoList.Clear();
            if (dt == null) return;
            int secondTick = 5;
            int Num = 300 / secondTick;
            double TotalHours = (end - start).TotalHours;
            if (TotalHours < 2)
            {
                secondTick = 5;
            }
            else if (TotalHours > 2 && TotalHours <= 5)
            {
                secondTick = 30;
            }
            else if (TotalHours > 5 && TotalHours <= 24)
            {
                secondTick = 60;
            }
            else if (TotalHours > 24 && TotalHours <= 72)
            {
                secondTick = 300;
            }
            else if (TotalHours > 72 && TotalHours <= 240)
            {
                secondTick = 1800;
                Num = 1800 / secondTick;
            }
            else
            {
                secondTick = 3600;
                Num = 3600 / secondTick;
            }
            int selectPointCount = (int)(TotalHours * 3600 / secondTick);
            selectPointCount = selectPointCount + 1;
            CallData.CurveData = new double[selectPointCount];
            CallData.KuiDianYiChang = new byte[selectPointCount];
            CallData.TimeStamps = new DateTime[selectPointCount];
            MaxData.CurveData = new double[selectPointCount];
            MinData.CurveData = new double[selectPointCount];
            AvgData.CurveData = new double[selectPointCount];
            CallData.AlarmIndex = new int[selectPointCount];
            //获取查询开始时间的前一个值
            object obj = dt.Compute("max(uploadTime)", " uploadTime<='" + start + "'");
            if (obj == DBNull.Value)
            {
                obj = dt.Compute("min(uploadTime)", " uploadTime<='" + end + "'");
            }
            DateTime PreDataTime = Convert.ToDateTime(obj);
            DataRow[] drs = dt.Select("uploadTime>='" + PreDataTime + "' and uploadTime<='" + end + "'");
            if (drs.Length == 0) return;
            bool IsExistMeasure = false;
            DataTable Measuredt = OperateDB.Select("SELECT *  FROM Measure where CuoShiShiJian>='" + start + "' and  CuoShiShiJian<='" + end + "' and ceDianId=" + ceDianId);
            if (Measuredt != null)
            {
                if (Measuredt.Rows.Count > 0)
                {
                    IsExistMeasure = true;
                }
            }
            int index = 0;
            ///分钟统计是当前显示区间的倍数            
            DateTime timeIndex = start;
            byte KuiDianYiChang = 0;
            double SpanMaxValue = double.MinValue, value = 0;
            double SumValue = 0, MaxValue = double.MinValue, MinValue = double.MaxValue, CountValue = 0, avgValue = 0;
            AlarmInfo curAlarmInfo = null;
            for (int i = 0; i < selectPointCount; i++)
            {
                MaxData.CurveData[i] = Chart.NoValue;
                MinData.CurveData[i] = Chart.NoValue;
                AvgData.CurveData[i] = Chart.NoValue;
                CallData.CurveData[i] = Chart.NoValue;
                CallData.TimeStamps[i] = timeIndex;
                CallData.AlarmIndex[i] = -1;

                DateTime time1 = Convert.ToDateTime(drs[index]["uploadTime"]);
                if (time1 > timeIndex)//前面没有记录
                {
                    timeIndex = timeIndex.AddSeconds(secondTick);
                    continue;
                }
                do
                {
                    value = Math.Round(Convert.ToSingle(drs[index]["uploadValue"]), 2);
                    KuiDianYiChang = Convert.ToByte(KuiDianYiChang | Convert.ToByte(dt.Rows[index]["KuiDianYiChang"]));
                    if (SpanMaxValue < value)
                    {
                        SpanMaxValue = value;
                    }
                    if (MaxValue < value)
                    {
                        MaxValue = value;
                    }
                    if (MinValue > value)
                    {
                        MinValue = value;
                    }
                    SumValue = SumValue + value;
                    CountValue = CountValue + 1;
                    if (index < dt.Rows.Count - 1)
                    {
                        index++;
                    }
                    else
                    {
                        break;
                    }
                    time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);
                }
                while (time1 < timeIndex);
                if (i != 0)
                {
                    ///整除产生统计值
                    if (i % (Num) == 0)
                    {
                        avgValue = Math.Round(SumValue / CountValue, 2);
                        for (int j = (i / Num - 1) * Num + 1; j <= i; j++)
                        {
                            MaxData.CurveData[j] = MaxValue;
                            MinData.CurveData[j] = MinValue;
                            AvgData.CurveData[j] = avgValue;
                        }
                        SumValue = 0;
                        CountValue = 0;
                        MaxValue = double.MinValue;
                        MinValue = double.MaxValue;
                    }
                    if (CallData.KuiDianYiChang[i - 1] != KuiDianYiChang)
                    {
                        if (KuiDianYiChang == 1)
                        {
                            curAlarmInfo = new AlarmInfo();
                            curAlarmInfo.startTime = timeIndex;
                            alarmInfoList.Add(curAlarmInfo);
                        }
                        else if (CallData.KuiDianYiChang[i - 1] == 2)//报警结束
                        {
                            curAlarmInfo.endTime = timeIndex;
                            if (IsExistMeasure)
                            {
                                DataRow[] drs2 = Measuredt.Select("CuoShiShiJian>='" + curAlarmInfo.startTime + "' and  CuoShiShiJian<='" + curAlarmInfo.endTime + "'");
                                foreach (DataRow dr in drs2)
                                {
                                    curAlarmInfo.Measure = curAlarmInfo.Measure + string.Format("{0} {1}   ", dr["CuoShiShiJian"].ToString(), dr["CuoShi"].ToString());
                                }
                            }
                        }
                    }
                    if (KuiDianYiChang == 1)
                    {
                        CallData.AlarmIndex[i] = alarmInfoList.Count - 1;
                    }
                }
                else
                {
                    MaxData.CurveData[i] = SpanMaxValue;
                    MinData.CurveData[i] = SpanMaxValue;
                    AvgData.CurveData[i] = SpanMaxValue;
                    SumValue = 0;
                    CountValue = 0;
                    MaxValue = double.MinValue;
                    MinValue = double.MaxValue;
                    if (KuiDianYiChang == 1)
                    {
                        curAlarmInfo = new AlarmInfo();
                        curAlarmInfo.startTime = timeIndex;
                        alarmInfoList.Add(curAlarmInfo);
                        CallData.AlarmIndex[i] = alarmInfoList.Count - 1;
                    }
                }
                if (KuiDianYiChang == 1)
                {
                    CallData.CurveData[i] = SpanMaxValue;
                }
    
                CallData.KuiDianYiChang[i] = KuiDianYiChang;
                timeIndex = timeIndex.AddSeconds(secondTick);
                KuiDianYiChang = 0;
                SpanMaxValue = double.MinValue;
                if (time1 > timeIndex)
                {
                    index--;
                }
            }
            if (CallData.KuiDianYiChang[selectPointCount - 1] == 1)
            {
                curAlarmInfo.endTime = end;
                if (IsExistMeasure)
                {
                    DataRow[] drs2 = Measuredt.Select("CuoShiShiJian>='" + curAlarmInfo.startTime + "' and  CuoShiShiJian<='" + curAlarmInfo.endTime + "'");
                    foreach (DataRow dr in drs2)
                    {
                        curAlarmInfo.Measure = curAlarmInfo.Measure + string.Format("{0} {1}   ", dr["CuoShiShiJian"].ToString(), dr["CuoShi"].ToString());
                    }
                }
            }
            if ((selectPointCount - 1) % Num != 0)
            {
                avgValue = Math.Round(SumValue / CountValue, 2);
                for (int j = (selectPointCount / Num) * Num + 1; j < selectPointCount; j++)
                {
                    MaxData.CurveData[j] = MaxValue;
                    MinData.CurveData[j] = MinValue;
                    AvgData.CurveData[j] = avgValue;
                }
            }
        }
        #endregion
        /*
        #region 多测点历史曲线
        /// <summary>
        /// 历史曲线 
        /// 取值区间
        /// 0-----2 时    5秒   （历史记录值）
        /// 2-----5 时    30秒     （历史记录值）
        /// 5-----24时    60秒     （历史记录值）
        /// 1-----3天     5分钟     （五分钟统计值）
        /// 3-----10天    30分钟    （五分钟统计值）
        /// 10----30天    1小时      （五分钟统计值）
        /// </summary>
        public static void GetHisCurveData(DataTable dt, DateTime start, DateTime end, ref CurveInfo CallData)
        {
            if ((end - start).TotalDays < 1)
            {
                GetHisCurveMaxOneDay(dt, start, end, ref CallData);
            }
            else
            {
                GetHisCurveMaxOneMonth(dt, start, end, ref CallData);
            }
        }

        #region 小于一天
        /// <summary>
        /// 历史曲线 小于一天的数据
        /// 取值区间
        /// 0-----2 时    5秒   （历史记录值）
        /// 2-----5 时    30秒     （历史记录值）
        /// 5-----24时    60秒     （历史记录值）
        /// </summary>
        public static void GetHisCurveMaxOneDay(DataTable dt, DateTime start, DateTime end, ref CurveInfo CallData)
        {
            if (dt == null) return;
            int secondTick = 5;
            double TotalHours = (end - start).TotalHours;
            if (TotalHours < 2)
            {
                secondTick = 5;
            }
            else if (TotalHours > 2 && TotalHours <= 5)
            {
                secondTick = 30;
            }
            else
            {
                secondTick = 60;
            }
            int selectPointCount = (int)(TotalHours * 3600 / secondTick);
            selectPointCount = selectPointCount + 1;
            CallData.CurveData = new double[selectPointCount];
            CallData.KuiDianYiChang = new byte[selectPointCount];
            CallData.TimeStamps = new DateTime[selectPointCount];

            //获取查询开始时间的前一个值
            object obj = dt.Compute("max(uploadTime)", " uploadTime<='" + start + "'");
            if (obj == DBNull.Value)
            {
                obj = dt.Compute("min(uploadTime)", " uploadTime<='" + end + "'");
            }
            DateTime PreDataTime = Convert.ToDateTime(obj);
            DataRow[] drs = dt.Select("uploadTime>='" + PreDataTime + "' and uploadTime<='" + end + "'");
            if (drs.Length == 0) return;
            int index = 0;
            ///分钟统计是当前显示区间的倍数
            int Num = 300 / secondTick;
            DateTime timeIndex = start;
            byte KuiDianYiChang = 0;
            double SpanMaxValue = double.MinValue, value = 0;
            for (int i = 0; i < selectPointCount; i++)
            {
                CallData.CurveData[i] = Chart.NoValue;
                DateTime time1 = Convert.ToDateTime(drs[index]["uploadTime"]);
                SpanMaxValue = Math.Round(Convert.ToSingle(drs[index]["uploadValue"]), 2);
                while (time1 < timeIndex)
                {
                    value = Math.Round(Convert.ToSingle(drs[index]["uploadValue"]), 2);
                    if (SpanMaxValue < value)
                    {
                        SpanMaxValue = value;
                    }
                    KuiDianYiChang = Convert.ToByte(KuiDianYiChang | Convert.ToByte(dt.Rows[index]["KuiDianYiChang"]));
                    index++;
                    if (index == dt.Rows.Count)
                    {
                        break;
                    }
                    time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);

                }
                if (index == 0)
                    index++;

                CallData.CurveData[i] = SpanMaxValue;
                CallData.TimeStamps[i] = timeIndex;
                CallData.KuiDianYiChang[i] = KuiDianYiChang;
                timeIndex = timeIndex.AddSeconds(secondTick);
                KuiDianYiChang = 0;
                index--;
            }
        }
        #endregion

        #region 小于一月
        /// <summary>
        /// 历史曲线 
        /// 取值区间
        /// 1-----3天     5分钟     （五分钟统计值）
        /// 3-----10天    30分钟    （五分钟统计值）
        /// 10----30天    1小时      （五分钟统计值）
        /// </summary>
        public static void GetHisCurveMaxOneMonth(DataTable dt, DateTime start, DateTime end, ref CurveInfo AvgData)
        {
            if (dt == null) return;
            int dateTick = 5;
            double TotalDays = (end - start).TotalDays;
            if (TotalDays < 3)
            {
                dateTick = 5;
            }
            else if (TotalDays > 3 && TotalDays <= 10)
            {
                dateTick = 30;
            }
            else
            {
                dateTick = 60;
            }
            int selectPointCount = (int)(TotalDays * 1440 / dateTick);
            selectPointCount = selectPointCount + 1;
            AvgData.CurveData = new double[selectPointCount];
            AvgData.KuiDianYiChang = new byte[selectPointCount];
            AvgData.TimeStamps = new DateTime[selectPointCount];
            //获取查询开始时间的前一个值
            object obj = dt.Compute("max(uploadTime)", " uploadTime<='" + start + "'");
            if (obj == DBNull.Value)
            {
                obj = dt.Compute("min(uploadTime)", " uploadTime<='" + end + "'");
            }
            DateTime PreDataTime = Convert.ToDateTime(obj);
            DataRow[] drs = dt.Select("uploadTime>='" + PreDataTime + "' and uploadTime<='" + end + "'");
            if (drs.Length == 0) return;
            int index = 0;
            ///分钟统计是当前显示区间的倍数
            int Num = dateTick / 5;
            DateTime timeIndex = start;
            byte KuiDianYiChang = 0;
            double avgvalue;
            double SumValue = 0, CountValue = 0;
            for (int i = 0; i < selectPointCount; i++)
            {
                AvgData.CurveData[i] = Chart.NoValue;
                DateTime time1 = Convert.ToDateTime(drs[index]["uploadTime"]);
                while (time1 < timeIndex)
                {
                    avgvalue = Math.Round(Convert.ToSingle(drs[index]["VagValue"]), 2);
                    SumValue = SumValue + avgvalue;
                    CountValue = CountValue + 1;
                    index++;
                    if (index == dt.Rows.Count)
                    {
                        break;
                    }
                    time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);

                }
                if (index == 0)
                    index++;
                if (CountValue != 0)
                {
                    AvgData.CurveData[i] = Math.Round(SumValue / CountValue, 2);
                }
                AvgData.TimeStamps[i] = timeIndex;
                AvgData.KuiDianYiChang[i] = KuiDianYiChang;
                timeIndex = timeIndex.AddMinutes(dateTick);
                index--;
                SumValue = 0;
                CountValue = 0;
            }
        }
        #endregion
        #endregion
        */
        #region 多测点历史曲线
        /// <summary>
        /// 历史曲线 
        /// 取值区间
        /// 0-----2 时    5秒   （历史记录值）
        /// 2-----5 时    30秒     （历史记录值）
        /// 5-----24时    60秒     （历史记录值）
        /// 1-----3天     5分钟     （历史记录值）
        /// 3-----10天    30分钟    （历史记录值）
        /// 10----30天    1小时      （历史记录值）
        /// </summary>
        public static void GetHisCurveData(DataTable dt, DateTime start, DateTime end, ref CurveInfo CallData)
        {
               GetHisCurveMaxOneDay(dt, start, end, ref CallData);
        }

        #region 小于一天
        /// <summary>
        /// 历史曲线 
        /// 取值区间
        /// 0-----2 时    5秒   （历史记录值）
        /// 2-----5 时    30秒     （历史记录值）
        /// 5-----24时    60秒     （历史记录值）
        /// 1-----3天     5分钟     （历史记录值）
        /// 3-----10天    30分钟    （历史记录值）
        /// 10----30天    1小时      （历史记录值）
        /// </summary>
        public static void GetHisCurveMaxOneDay(DataTable dt, DateTime start, DateTime end, ref CurveInfo CallData)
        {
            if (dt == null) return;
            int secondTick = 5;
            ///分钟统计是当前显示区间的倍数
            int Num = 300 / secondTick;
            double TotalHours = (end - start).TotalHours;
            if (TotalHours < 2)
            {
                secondTick = 5;
            }
            else if (TotalHours > 2 && TotalHours <= 5)
            {
                secondTick = 30;
            }
            else if (TotalHours > 5 && TotalHours <= 24)
            {
                secondTick = 60;
            }
            else if (TotalHours > 24 && TotalHours <= 72)
            {
                secondTick = 300;
            }
            else if (TotalHours > 72 && TotalHours <= 240)
            {
                secondTick = 1800;
                Num = 1800 / secondTick;
            }
            else
            {
                secondTick = 3600;
                Num = 3600 / secondTick;
            }
            int selectPointCount = (int)(TotalHours * 3600 / secondTick);
            selectPointCount = selectPointCount + 1;
            CallData.CurveData = new double[selectPointCount];
            CallData.KuiDianYiChang = new byte[selectPointCount];
            CallData.TimeStamps = new DateTime[selectPointCount];

            //获取查询开始时间的前一个值
            object obj = dt.Compute("max(uploadTime)", " uploadTime<='" + start + "'");
            if (obj == DBNull.Value)
            {
                obj = dt.Compute("min(uploadTime)", " uploadTime<='" + end + "'");
            }
            DateTime PreDataTime = Convert.ToDateTime(obj);
            DataRow[] drs = dt.Select("uploadTime>='" + PreDataTime + "' and uploadTime<='" + end + "'");
            if (drs.Length == 0) return;
            int index = 0;
            DateTime timeIndex = start;
            byte KuiDianYiChang = 0;
            double SpanMaxValue = double.MinValue, value = 0;
            for (int i = 0; i < selectPointCount; i++)
            {
                CallData.CurveData[i] = Chart.NoValue;
                DateTime time1 = Convert.ToDateTime(drs[index]["uploadTime"]);
                SpanMaxValue = Math.Round(Convert.ToSingle(drs[index]["uploadValue"]), 2);
                while (time1 < timeIndex)
                {
                    value = Math.Round(Convert.ToSingle(drs[index]["uploadValue"]), 2);
                    if (SpanMaxValue < value)
                    {
                        SpanMaxValue = value;
                    }
                    KuiDianYiChang = Convert.ToByte(KuiDianYiChang | Convert.ToByte(dt.Rows[index]["KuiDianYiChang"]));
                    index++;
                    if (index == dt.Rows.Count)
                    {
                        break;
                    }
                    time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);

                }
                if (index == 0)
                    index++;

                CallData.CurveData[i] = SpanMaxValue;
                CallData.TimeStamps[i] = timeIndex;
                CallData.KuiDianYiChang[i] = KuiDianYiChang;
                timeIndex = timeIndex.AddSeconds(secondTick);
                KuiDianYiChang = 0;
                index--;
            }
        }
        #endregion
        #endregion

        #region  状态图显示
        public static void KaiGuangLiangData(DateTime startTime, DateTime endTime, string ceDianBianHao, int ceDianId, ref KglZtuInfo kglZtuInfo)
        {
            if (startTime > DateTime.Now) return;
            if (endTime > DateTime.Now) endTime = DateTime.Now;
            DataTable dt = GetHisTableMaxOneDay(startTime, endTime, ceDianBianHao, ceDianId);
            if (dt == null)
            {
                kglZtuInfo = null;
                return;
            }
            CeDian cedian = new CeDian(ceDianBianHao);
            int baoJingState=0, duanDianState=0;
            string lingTaiMingCheng="", yiTaiMingCheng="", erTaiMingCheng="";
            bool ShiFouBaoJing = false;
            bool ShiFouDuanDian = false;
            if (!ceDianBianHao.Contains("C"))
            {
                baoJingState = cedian.KaiGuanLiang.BaoJingZhuangTai;
                duanDianState = cedian.KaiGuanLiang.DuanDianZhuangTai;
                //取得此测点的小类型的报警状态
                lingTaiMingCheng = cedian.KaiGuanLiang.LingTai.ToString().Trim();
                yiTaiMingCheng = cedian.KaiGuanLiang.YiTai.ToString().Trim();
                erTaiMingCheng = cedian.KaiGuanLiang.ErTai.ToString().Trim();
                ShiFouBaoJing = cedian.KaiGuanLiang.ShiFouBaoJing;
                ShiFouDuanDian = cedian.KaiGuanLiang.ShiFouDuanDian;
            }
            else
            {
              lingTaiMingCheng = cedian.KongZhiLiang.Litaimingcheng.Trim();
              yiTaiMingCheng = cedian.KongZhiLiang.Yitaimingcheng;
            }
            int secondTick = 5;
            double TotalHours = (endTime - startTime).TotalHours;
            if (TotalHours < 2)
            {
                secondTick = 5;
            }
            else if (TotalHours > 2 && TotalHours <= 5)
            {
                secondTick = 30;
            }
            else if (TotalHours > 5 && TotalHours <= 24)
            {
                secondTick = 60;
            }
            else if (TotalHours > 24 && TotalHours <= 72)
            {
                secondTick = 300;
            }
            else if (TotalHours > 72 && TotalHours <= 240)
            {
                secondTick = 1800;
            }
            else
            {
                secondTick = 3600;
            }
            int selectPointCount = (int)(TotalHours * 3600 / secondTick);
            selectPointCount = selectPointCount + 1;
            kglZtuInfo.value = new double[selectPointCount];
            kglZtuInfo.kuidian = new bool[selectPointCount];
            kglZtuInfo.date = new DateTime[selectPointCount];
            kglZtuInfo.baojing = new bool[selectPointCount];
            kglZtuInfo.duandian = new bool[selectPointCount];
            kglZtuInfo.status = new string[selectPointCount];
            int index = 0;
            DateTime timeIndex = startTime;
            bool KuiDianYiChang = false;
            double value = 0, SpanMaxValue;
            SpanMaxValue = double.MinValue;
            for (int i = 0; i < selectPointCount; i++)
            {
                kglZtuInfo.value[i] = Chart.NoValue;
                DateTime time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);
                kglZtuInfo.date[i] = timeIndex;
                kglZtuInfo.status[i] = "";
                if (time1 > timeIndex || timeIndex > DateTime.Now)//前面没有记录
                {
                    timeIndex = timeIndex.AddSeconds(secondTick);
                    continue;
                }
                do
                {
                    value = Convert.ToInt32(dt.Rows[index]["uploadValue"]);
                    if (ceDianBianHao.Contains("C"))
                    {
                        KuiDianYiChang = false;
                    }
                    else
                    {
                        KuiDianYiChang = KuiDianYiChang | Convert.ToBoolean(dt.Rows[index]["KuiDianYiChang"]);
                    }
                    if (SpanMaxValue < value)
                    {
                        SpanMaxValue = value;
                    }
                    if (index < dt.Rows.Count - 1)
                    {
                        index++;
                    }
                    else
                    {
                        break;
                    }
                    time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);
                }
                while (time1 < timeIndex);
                if (SpanMaxValue == 0)
                {
                    kglZtuInfo.status[i] = lingTaiMingCheng;
                }
                else if (SpanMaxValue == 1)
                {
                    kglZtuInfo.status[i] = yiTaiMingCheng;
                }
                else if (SpanMaxValue == 2)
                {
                    kglZtuInfo.status[i] = erTaiMingCheng;
                }
                else
                {
                    kglZtuInfo.status[i] = "其他";
                }
                if (SpanMaxValue == baoJingState && ShiFouBaoJing)
                    kglZtuInfo.baojing[i] = true;
                else
                    kglZtuInfo.baojing[i] = false;

                if (SpanMaxValue == duanDianState && ShiFouDuanDian)
                    kglZtuInfo.duandian[i] = true;
                else
                    kglZtuInfo.duandian[i] = false;

                kglZtuInfo.value[i] = SpanMaxValue;
                if (i != 0)
                {
                    if (kglZtuInfo.value[i - 1] != SpanMaxValue)
                    {
                        kglZtuInfo.date[i - 1] = timeIndex;
                    }
                }
                kglZtuInfo.kuidian[i] = KuiDianYiChang;
                timeIndex = timeIndex.AddSeconds(secondTick);
                KuiDianYiChang = false;
                SpanMaxValue = double.MinValue;
                if (time1 > timeIndex)
                {
                    index--;
                }
            }
        }
        #endregion

        #region  柱状图显示
        public static void ZhuZhuangTuData(DateTime date, string cedianbianhao, int ceDianId, ref KglZzuInfo kglZzuInfo)
        {
            if (date > DateTime.Now) return;
            DateTime startTime = date.Date;
            DateTime endTime = startTime.AddDays(1);
            DataTable dt = GetHisTableMaxOneDay(startTime, endTime, cedianbianhao, ceDianId);
            if (dt == null)
            {
                kglZzuInfo = null;
                return;
            }
            if (dt.Rows.Count == 0)
            {
                kglZzuInfo = null;
                return;
            }
            int selectPointCount = (int)(endTime - startTime).TotalHours;
            kglZzuInfo.date = new DateTime[selectPointCount];
            kglZzuInfo.kaiTingCiShu = new int[selectPointCount];
            kglZzuInfo.kaiTingShiJian = new TimeSpan[selectPointCount];
            kglZzuInfo.Value = new double[selectPointCount];

            int kaiZhuanTai = 0;//开关量的开状态      
            int currentState = -1;
            int kaiTingCiShu = 0;//给定时间段内开停次数
            TimeSpan kaiTingShiJian = TimeSpan.Zero;//给定时间段内累计开停时间，最长为1小时
            DateTime timeIndex = startTime.AddHours(1);//0--1小时内的值 统计在0上
            int index = 0;
            DateTime start = (DateTime)dt.Rows[0]["uploadTime"];//
            DateTime now = (DateTime)dt.Rows[0]["uploadTime"];
            for (int i = 0; i < selectPointCount; i++)
            {
                kglZzuInfo.date[i] = timeIndex.AddHours(-1);
                kglZzuInfo.kaiTingShiJian[i] = TimeSpan.Zero;
              
                DateTime time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);
                if (time1 > timeIndex || timeIndex > DateTime.Now)//前面没有记录
                {
                    timeIndex = timeIndex.AddHours(1);
                    continue;
                }
                do
                {
                    int state = Convert.ToInt32(dt.Rows[index]["uploadValue"]);
                    now = (DateTime)dt.Rows[index]["uploadTime"];
                    if (state != currentState)
                    {
                        if (now.Date != date.Date) now = date.Date;
                        if (currentState == kaiZhuanTai)
                        {
                            kaiTingShiJian += now - start;
                        }
                        else if (state == kaiZhuanTai)
                        {
                            start = now;
                            kaiTingCiShu++;
                        }
                        currentState = state;
                    }
                    if (index < dt.Rows.Count - 1)
                    {
                        index++;
                    }
                    else
                    {
                        break;
                    }
                    time1 = Convert.ToDateTime(dt.Rows[index]["uploadTime"]);
                }
                while (time1 < timeIndex);
                if (currentState == kaiZhuanTai)
                {
                    kaiTingCiShu++;
                    kaiTingShiJian += timeIndex - start;
                    start = timeIndex;
                }
                kglZzuInfo.kaiTingCiShu[i] = kaiTingCiShu;
                kglZzuInfo.Value[i] = (float)Math.Round((float)kaiTingShiJian.TotalSeconds / 3600 * 100, 1);
                kglZzuInfo.kaiTingShiJian[i] = kaiTingShiJian;
                kaiTingCiShu = 0;
                kaiTingShiJian = TimeSpan.Zero;
                timeIndex = timeIndex.AddHours(1);
                if (time1 > timeIndex)
                {
                    index--;
                }
            }
        }
        #endregion
    }

    public class KglZtuInfo
    {
        public double[] value;
        public DateTime[] date;
        public bool[] baojing;
        public bool[] duandian;
        public bool[] kuidian;
        public string[] status;
    }
    public class KglZzuInfo
    {
        public DateTime[] date;
        public double[] Value;
        public int[] kaiTingCiShu;
        public TimeSpan[]  kaiTingShiJian;
    }
    public class CurveInfo
    {
        public double[] CurveData;
        public byte[] KuiDianYiChang;
        public DateTime[] TimeStamps;
        public Color CurveColor = Color.Yellow;
        public string CurveTitle = "曲线显示";
        public bool IsShow=false;
        /// <summary>
        /// 处理措施信息索引号，-1 代码没有处理措施
        /// </summary>
        public int[] AlarmIndex;
    }
    public class AlarmInfo
    {
        public DateTime startTime;
        public DateTime endTime;
        public string Measure;    
    }
}
