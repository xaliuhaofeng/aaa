using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Logic;

namespace Logic
{
    public class ReportDataSuply
    {
        private static string dateString = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 从MoNiLiangValue表中提取模拟量的数据
        /// </summary>
        /// <param name="startTime">查询开始时间的字符串：yyyy-MM-dd HH:mm:ss</param>
        /// <param name="endTime">查询结束时间的字符串：yyyy-MM-dd HH:mm:ss</param>
        /// <param name="dt1">引用参数，返回查询所得的数据</param>
        /// <param name="dt2">引用参数，返回测点的数</param>
        /// <returns>是否查询到相关数据，true:有数，false:没有数据</returns>
        public static bool doMoNiLiangSelect(DateTime startTime, DateTime endTime, ref DataTable dt1, ref DataTable dt2, string[] cedians)
        {
            return doDataSelect(startTime, endTime, ref dt1, ref dt2, "MoNiLiangValue", cedians);
        }

        public static bool doMoNiLiangSelect_AllCeDianID(DateTime startTime, DateTime endTime, ref DataTable dt1, ref DataTable dt2, long[] cedianIDs)
        {
            return doDataSelectt_AllCeDianID(startTime, endTime, ref dt1, ref dt2, "MoNiLiangValue", cedianIDs);
        }

        public static bool doKaiGuanLiangSelect(DateTime startTime, DateTime endTime, ref DataTable dt1, ref DataTable dt2, string[] cedians)
        {
            return doDataSelect(startTime, endTime, ref dt1, ref dt2, "KaiGuanLiangValue", cedians);
        }



        public static bool doMoNiLiangSelect(DateTime startTime, DateTime endTime, ref DataTable dt1, ref DataTable dt2, string ceDianBianHao)
        {
            string[] cedians = new string[1];
            cedians[0] = ceDianBianHao;
            return doDataSelect(startTime, endTime, ref dt1, ref dt2, "MoNiLiangValue", cedians);
        }

        public static bool doKaiGuanLiangSelect(DateTime startTime, DateTime endTime, ref DataTable dt1, ref DataTable dt2, string ceDianBianHao)
        {
            string[] cedians = new string[1];
            cedians[0] = ceDianBianHao;
            return doDataSelect(startTime, endTime, ref dt1, ref dt2, "KaiGuanLiangValue", cedians);
        }

        private static bool doDataSelect(DateTime startTime, DateTime endTime, ref DataTable dt1, ref DataTable dt2, string tableNamePrefix, string[] ceDianBianHao)
        {
            bool hasData = false;
            string tableName1 = tableNamePrefix + startTime.ToString("yyyy_MM");
            string tableName2 = tableNamePrefix + endTime.ToString("yyyy_MM");
            string sql;
            DataTable temp;

            string cedians = "(";
            foreach (string s in ceDianBianHao)
            {
                if (cedians != "(")
                    cedians = cedians + ", '" + s + "'";
                else
                    cedians = cedians + "'" + s + "'";
            }
            cedians = cedians + ")";

            //根据编号查询出测点ID
            string cedian_sql = "select id from CeDian where weishanchu=1 and cedianbianhao in " + cedians;
            DataTable cedianId_dt = OperateDB.Select(cedian_sql);

            string cedianIds = "(";
            if (cedianId_dt != null && cedianId_dt.Rows.Count > 0)
            {
                foreach (DataRow dr in cedianId_dt.Rows)
                {
                    if (cedianIds != "(")
                        cedianIds = cedianIds + ", '" + dr["id"] + "'";
                    else
                        cedianIds = cedianIds + "'" + dr["id"] + "'";
                }
            }
            cedianIds += ")";

            if (tableName1 == tableName2)
            {
                if (OperateDB.IsTableExist("max_cmss", tableName1)) //所查找的数据存在
                {
                    sql = "select * from " + tableName1 + " where uploadTime between '" + startTime + "' and '" + endTime + "' and cedianId in " + cedianIds;
                    dt1 = OperateDB.Select(sql);
                    hasData = true;
                }
            }
            else
            {
                if (OperateDB.IsTableExist("max_cmss", tableName1))
                {
                    sql = "select * from " + tableName1 + " where uploadTime > '" + startTime + "' and cedianId in " + cedianIds;
                    dt1 = OperateDB.Select(sql);
                    hasData = true;
                }
                startTime = startTime.AddMonths(1);

                while (startTime.Year < endTime.Year || (startTime.Year == endTime.Year && startTime.Month < endTime.Month))
                {
                    string tableName = tableNamePrefix + startTime.ToString("yyyy_MM");
                    if (OperateDB.IsTableExist("max_cmss", tableName))
                    {
                        if (ceDianBianHao == null)
                            sql = "select * from " + tableName1;
                        else
                            sql = "select * from " + tableName1 + " where cedianIds in " + cedianIds;
                        if (hasData)
                        {
                            temp = OperateDB.Select(sql);
                            if (dt1 != null && temp != null)
                            {
                                dt1.Merge(temp);
                            }
                        }
                        else
                        {
                            dt1 = OperateDB.Select(sql);
                            hasData = true;
                        }
                    }
                    startTime = startTime.AddMonths(1);
                }
                if (OperateDB.IsTableExist("max_cmss", tableName2))
                {
                    sql = "select * from " + tableName2 + " where uploadTime < '" + endTime + "' and cedianIds in " + cedianIds;
                    if (hasData)
                    {
                        temp = OperateDB.Select(sql);
                        if (dt1 != null && temp != null)
                        {
                            dt1.Merge(temp);
                        }
                    }
                    else
                    {
                        dt1 = OperateDB.Select(sql);
                        hasData = true;
                    }
                }
            }

            if (dt1 != null)
            {
                DataView dv = new DataView(dt1);

                if (dv.Count > 0)
                {
                    dv.Sort = "ceDianBianHao, cedianID,uploadTime";
                    dt1 = dv.ToTable();
                }
            }

            if (tableNamePrefix == "MoNiLiangValue")
            {
                sql = "select CeDian.id as id, CeDianBianHao, xiaoLeiXing, ceDianWeiZhi, danWei, baoJingZhiShangXian, duanDianZhi, fuDianZhi from CeDian, MoNiLiangLeiXing where CeDian.xiaoLeiXing = MoNiLiangLeiXing.mingcheng and CeDian.weishanchu=1;";
                dt2 = OperateDB.Select(sql);
            }
            else
            {
                sql = "select CeDian.id as id, CeDianBianHao, xiaoLeiXing, ceDianWeiZhi, lingTaiMingCheng, yiTaiMingCheng, erTaiMingCheng, baoJingZhuangTai, duanDianZhuangTai from CeDian, KaiGuanLiangLeiXing where CeDian.xiaoLeiXing = KaiGuanLiangLeiXing.mingcheng and CeDian.weishanchu=1;";
                dt2 = OperateDB.Select(sql);
            }

            if (dt1 == null || dt1.Rows.Count == 0)
                hasData = false;
            return hasData;
        }



        private static bool doDataSelectt_AllCeDianID(DateTime startTime, DateTime endTime, ref DataTable dt1, ref DataTable dt2, string tableNamePrefix, long[] ceDianId_arr)
        {
            bool hasData = false;
            string tableName1 = tableNamePrefix + startTime.ToString("yyyy_MM");
            string tableName2 = tableNamePrefix + endTime.ToString("yyyy_MM");
            string sql;
            DataTable temp;


            string cedianIds = "(";
            if (ceDianId_arr.Length > 0)
            {
                foreach (long id in ceDianId_arr)
                {
                    if (cedianIds != "(")
                        cedianIds = cedianIds + ", '" + id + "'";
                    else
                        cedianIds = cedianIds + "'" + id + "'";
                }
            }
            cedianIds += ")";

            if (tableName1 == tableName2)
            {
                if (OperateDB.IsTableExist("max_cmss", tableName1)) //所查找的数据存在
                {
                    sql = "select * from " + tableName1 + " where uploadTime between '" + startTime + "' and '" + endTime + "' and cedianId in " + cedianIds;
                    dt1 = OperateDB.Select(sql);
                    hasData = true;
                }
            }
            else
            {
                if (OperateDB.IsTableExist("max_cmss", tableName1))
                {
                    sql = "select * from " + tableName1 + " where uploadTime > '" + startTime + "' and cedianId in " + cedianIds;
                    dt1 = OperateDB.Select(sql);
                    hasData = true;
                }
                startTime = startTime.AddMonths(1);

                while (startTime.Year < endTime.Year || (startTime.Year == endTime.Year && startTime.Month < endTime.Month))
                {
                    string tableName = tableNamePrefix + startTime.ToString("yyyy_MM");
                    if (OperateDB.IsTableExist("max_cmss", tableName))
                    {
                        if (ceDianId_arr == null)
                            sql = "select * from " + tableName1;
                        else
                            sql = "select * from " + tableName1 + " where cedianIds in " + cedianIds;
                        if (hasData)
                        {
                            temp = OperateDB.Select(sql);
                            if (dt1 != null && temp != null)
                            {
                                dt1.Merge(temp);
                            }
                        }
                        else
                        {
                            dt1 = OperateDB.Select(sql);
                            hasData = true;
                        }
                    }
                    startTime = startTime.AddMonths(1);
                }
                if (OperateDB.IsTableExist("max_cmss", tableName2))
                {
                    sql = "select * from " + tableName2 + " where uploadTime < '" + endTime + "' and cedianIds in " + cedianIds;
                    if (hasData)
                    {
                        temp = OperateDB.Select(sql);
                        if (dt1 != null && temp != null)
                        {
                            dt1.Merge(temp);
                        }
                    }
                    else
                    {
                        dt1 = OperateDB.Select(sql);
                        hasData = true;
                    }
                }
            }

            if (dt1 != null)
            {
                DataView dv = new DataView(dt1);

                if (dv.Count > 0)
                {
                    dv.Sort = "ceDianBianHao,cedianID, uploadTime";
                    dt1 = dv.ToTable();
                }
            }

            if (tableNamePrefix == "MoNiLiangValue")
            {
                sql = "select CeDian.id as id, CeDianBianHao, xiaoLeiXing, ceDianWeiZhi, danWei, baoJingZhiShangXian, duanDianZhi, fuDianZhi from CeDian, MoNiLiangLeiXing where CeDian.xiaoLeiXing = MoNiLiangLeiXing.mingcheng;";
                dt2 = OperateDB.Select(sql);
            }
            else
            {
                sql = "select CeDian.id as id, CeDianBianHao, xiaoLeiXing, ceDianWeiZhi, lingTaiMingCheng, yiTaiMingCheng, erTaiMingCheng, baoJingZhuangTai, duanDianZhuangTai from CeDian, KaiGuanLiangLeiXing where CeDian.xiaoLeiXing = KaiGuanLiangLeiXing.mingcheng;";
                dt2 = OperateDB.Select(sql);
            }

            if (dt1 == null || dt1.Rows.Count == 0)
                hasData = false;
            return hasData;
        }

        #region //模拟量日报表
        /// <summary>
        /// 打印：模拟量日报表
        /// </summary>
        /// <param name="date">所要打印数据的日期</param>
        /// <returns>含有报表数据的表，如果没有数据，返回该表结构的空表</returns>
        public static max_cmssDataSet.ADaylyReportDataTable GetADailyReportData(DateTime date)
        {
            DateTime startTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime endTime = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            string[] ceDianPaiXu = ReportConfig.getConfigByName((int)ReportType.MoNiLiangRiBaoBiao).GetCeDianBianHao();

            dateString = "HH:mm:ss";
            max_cmssDataSet.ADaylyReportDataTable dt = QueryADailyReportData(startTime, endTime, ceDianPaiXu);
            dateString = "yyyy-MM-dd HH:mm:ss";
            return dt;
        }

        /// <summary>
        /// 查询：模拟量报表
        /// </summary>
        /// <param name="startTime">查询开始时间</param>
        /// <param name="endTime">查询结束时间</param>
        /// <param name="cedians">要输出的测点序列</param>
        /// <returns>含有报表数据的表，如果没有数据，返回该表结构的空表</returns>
        public static max_cmssDataSet.ADaylyReportDataTable QueryADailyReportData(DateTime startTime, DateTime endTime, string[] cedians)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            if (!doMoNiLiangSelect(startTime, endTime, ref dt1, ref dt2, cedians))//所查找的数据为空
                return new max_cmssDataSet.ADaylyReportDataTable();

            int count = dt2.Rows.Count;

            if (dt1 == null || dt1.Rows.Count == 0)//dt2.Rows.Count == 0?
            {
                return new max_cmssDataSet.ADaylyReportDataTable();
            }
            else
            {
                max_cmssDataSet.ADaylyReportDataTable dt = ReportDataSuply.doGetADailyReportData(dt1, ref dt2);
                max_cmssDataSet.ADaylyReportDataTable resultTable = new max_cmssDataSet.ADaylyReportDataTable();

                foreach (string s in cedians)
                {
                    max_cmssDataSet.ADaylyReportRow row = dt.FindByCeDianBianHao(s);
                    if (row != null)
                    {
                        resultTable.ImportRow(row);
                    }
                    else
                    {
                        addADailyReportRow(ref resultTable, 0, "", 0, "", 0, "", ref dt2, s, 0, "");
                    }
                }
                return resultTable;
            }
        }

        /// <summary>
        /// 分析查询出的原始数据，得出模拟量日报表的数据
        /// </summary>
        /// <param name="value">查询出的原始数据</param>
        /// <param name="cedian"></param>
        /// <returns>模拟量报表的数据</returns>
        private static max_cmssDataSet.ADaylyReportDataTable doGetADailyReportData(DataTable value, ref DataTable cedian)
        {
            max_cmssDataSet.ADaylyReportDataTable dt = new max_cmssDataSet.ADaylyReportDataTable();

            bool baojing = false;
            bool firstbaojing = true;
            DateTime baojingStartTime = DateTime.Now;
            TimeSpan leijibaojing = TimeSpan.Zero;
            int baoJingCiShu = 0;

            bool duandian = false;
            bool firstduandian = true;
            DateTime duandianStartTime = DateTime.Now;
            TimeSpan leijiduandian = TimeSpan.Zero;
            int duanDianCiShu = 0;

            bool firstOther = true;

            double sum = 0;
            int count = 0;
            double avgValue = 0f;
            double maxValue = -1000f;
            double currentValue = 0;
            DateTime currentValueStartTime = DateTime.Now;
            TimeSpan currentValueTime = TimeSpan.Zero;
            string maxValueTime = string.Empty;

            //馈电异常
            int kuiDianYiChangCiShu = 0;
            int preKuiDian = -1;
            int curKuiDian = -1;
            TimeSpan kuiDianSpan = TimeSpan.Zero;
            DateTime currentKuiDianStartTime = DateTime.Now;
            DateTime currentTime = DateTime.Now;

            int baojingcount = 0;
            int duandiancount = 0;

            string cedianbianhao = value.Rows[0]["ceDianBianHao"].ToString();
            currentValueStartTime = (DateTime)value.Rows[0]["uploadTime"];
            foreach (DataRow row in value.Rows)
            {
            newcedian:
                if (cedianbianhao == row["ceDianBianHao"].ToString())
                {
                    currentTime = (DateTime)row["uploadTime"];
                    float uploadValueThisRow = (float)row["uploadValue"];

                    if (currentValue != uploadValueThisRow)
                    {
                        if (sum == 0.0)
                        {
                            currentValueTime = currentTime - new DateTime(currentValueStartTime.Year, currentValueStartTime.Month, currentValueStartTime.Day, 0, 0, 0);
                        }
                        else
                        {
                            currentValueTime = currentTime - currentValueStartTime;
                        }
                        sum += currentValue * currentValueTime.TotalMilliseconds;
                        currentValue = uploadValueThisRow;
                        currentValueStartTime = currentTime;
                    }

                    if (currentValue > maxValue)
                    {
                        maxValue = currentValue;
                        maxValueTime = currentTime.ToString(dateString);
                    }

                    //判断馈电
                    curKuiDian = Convert.ToInt32(row["kuiDianYiChang"]);
                    if (curKuiDian != preKuiDian)
                    {
                        if (preKuiDian == 1)
                        {
                            kuiDianYiChangCiShu++;
                            kuiDianSpan += currentTime - currentKuiDianStartTime;
                        }
                        preKuiDian = curKuiDian;
                        currentKuiDianStartTime = currentTime;
                    }


                    //判断测点的状态变化
                    MoNiLiangState state = (MoNiLiangState)Convert.ToInt32(row["state"]);
                    if (state == MoNiLiangState.BaoJing || state == MoNiLiangState.DuanDian)
                    {
                        if (firstbaojing)//首次碰到报警状态
                        {
                            //处理报警
                            firstbaojing = false; baojing = true; firstOther = true;
                            baojingStartTime = currentTime;
                        }
                        baojingcount++;
                        if (state == MoNiLiangState.DuanDian)
                        {
                            if (firstduandian)
                            {
                                firstbaojing = firstduandian = false; duandian = true; firstOther = true;
                                duandianStartTime = currentTime;
                            }
                            duandiancount++;
                        }
                    }
                    else if (firstOther == true)
                    {
                        firstOther = false; firstbaojing = firstduandian = true;
                        if (baojing)
                        {
                            TimeSpan temp = currentTime - baojingStartTime;
                            if (!(baojingcount < 2 && temp.TotalSeconds < 4))//数据库中单个点，并且持续时间少于3秒，则过滤该报警
                            {
                                baoJingCiShu++;
                                leijibaojing += temp;
                            }
                            baojingcount = 0;
                            baojing = false;
                        }
                        if (duandian)
                        {
                            TimeSpan temp = currentTime - duandianStartTime;
                            if (!(duandiancount < 2 && temp.TotalSeconds < 4))//数据库中单个点，并且持续时间少于3秒，则过滤该断电
                            {
                                duanDianCiShu++;
                                leijiduandian += temp;
                            }
                            duandiancount = 0;
                            duandian = false;
                        }
                    }
                }
                else
                {
                    currentValueTime = new DateTime(currentValueStartTime.Year, currentValueStartTime.Month, currentValueStartTime.Day, 23, 59, 59) - currentValueStartTime;
                    sum += currentValue * currentValueTime.TotalMilliseconds;
                    //if (curKuiDian == 1)//该天最后一段的馈电
                    //{
                    //    kuiDianYiChangCiShu++;
                    //    kuiDianSpan += new DateTime(currentValueStartTime.Year, currentValueStartTime.Month, currentValueStartTime.Day, 23, 59, 59) - currentKuiDianStartTime;
                    //}
                    //if (baojing)//该天最后一段的报警
                    //{
                    //    TimeSpan temp = currentTime - baojingStartTime;
                    //    if (!(baojingcount < 2 && temp.TotalSeconds < 4))//数据库中单个点，并且持续时间少于3秒，则过滤该报警
                    //    {
                    //        baoJingCiShu++;
                    //        leijibaojing += new DateTime(currentValueStartTime.Year, currentValueStartTime.Month, currentValueStartTime.Day, 23, 59, 59) - baojingStartTime;
                    //    }
                    //    baojingcount = 0;
                    //    baojing = false;
                    //}
                    //if (duandian)//该天最后一段的断电
                    //{
                    //    TimeSpan temp = currentTime - duandianStartTime;
                    //    if (!(duandiancount < 2 && temp.TotalSeconds < 4))//数据库中单个点，并且持续时间少于3秒，则过滤该断电
                    //    {
                    //        duanDianCiShu++;
                    //        leijiduandian += new DateTime(currentValueStartTime.Year, currentValueStartTime.Month, currentValueStartTime.Day, 23, 59, 59) - duandianStartTime;
                    //    }
                    //    duandiancount = 0;
                    //    duandian = false;
                    //}
                    addADailyReportRow(ref dt, sum / (24 * 3600000), maxValue.ToString("f2") + "/" + maxValueTime, baoJingCiShu, GlobalParams.TimeSpanString(leijibaojing), duanDianCiShu,
                        GlobalParams.TimeSpanString(leijiduandian), ref cedian, cedianbianhao, kuiDianYiChangCiShu, GlobalParams.TimeSpanString(kuiDianSpan));

                    //重新初始化
                    cedianbianhao = row["ceDianBianHao"].ToString();
                    currentValueStartTime = (DateTime)row["uploadTime"];
                    sum = count = baoJingCiShu = duanDianCiShu = 0;
                    avgValue = maxValue = currentValue = 0;
                    maxValueTime = string.Empty;
                    leijibaojing = TimeSpan.Zero;
                    leijiduandian = TimeSpan.Zero;
                    firstbaojing = firstduandian = firstOther = true;
                    baojing = duandian = false;
                    curKuiDian = preKuiDian = -1;
                    kuiDianYiChangCiShu = 0;
                    kuiDianSpan = TimeSpan.Zero;

                    goto newcedian;
                }
            }
            currentValueTime = new DateTime(currentValueStartTime.Year, currentValueStartTime.Month, currentValueStartTime.Day, 23, 59, 59) - currentValueStartTime;
            sum += currentValue * currentValueTime.TotalMilliseconds;
            addADailyReportRow(ref dt, sum / (24 * 3600000), maxValue.ToString("f2") + "/" + maxValueTime, baoJingCiShu, GlobalParams.TimeSpanString(leijibaojing), duanDianCiShu,
                GlobalParams.TimeSpanString(leijiduandian), ref cedian, cedianbianhao, kuiDianYiChangCiShu, GlobalParams.TimeSpanString(kuiDianSpan));

            return dt;
        }
        /// <summary>
        /// 往模拟量报表中添加一行数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="avgValue"></param>
        /// <param name="max"></param>
        /// <param name="baoJingCiShu"></param>
        /// <param name="leiJiBaoJing"></param>
        /// <param name="duanDianCiShu"></param>
        /// <param name="leiJiDuanDian"></param>
        /// <param name="ceDian"></param>
        /// <param name="ceDianBianHao"></param>
        private static void addADailyReportRow(ref max_cmssDataSet.ADaylyReportDataTable dt, double avgValue, string max, int baoJingCiShu, string leiJiBaoJing,
            int duanDianCiShu, string leiJiDuanDian, ref DataTable ceDian, string ceDianBianHao, int kuiDianYiChangCiShu, string leiJiKuiDian)
        {
            max_cmssDataSet.ADaylyReportRow resultRow = (max_cmssDataSet.ADaylyReportRow)dt.NewRow();
            resultRow.CeDianBianHao = ceDianBianHao;
            //DataRow row = ceDian.Rows.Find(ceDianBianHao);
            //ceDian.Rows.
            DataRow selectCeDian = ceDian.NewRow();
            foreach (DataRow row in ceDian.Rows)
            {
                string bianHao = row["ceDianBianHao"].ToString().Trim();
                if (bianHao == ceDianBianHao.Trim())
                {
                    selectCeDian = row;
                    resultRow.DiDian = ceDianBianHao + "/" + selectCeDian["ceDianWeiZhi"] + "/" + selectCeDian["xiaoLeiXing"];
                    resultRow.DanWei = selectCeDian["danWei"].ToString();
                    resultRow.BaoJingMenXian = Math.Round(Convert.ToSingle(selectCeDian["baoJingZhiShangXian"]), 2);
                    resultRow.DuanDianMenXian = Math.Round(Convert.ToSingle(selectCeDian["duanDianZhi"]), 2);
                    resultRow.FuDianMenXian = Math.Round(Convert.ToDouble(selectCeDian["fuDianZhi"]), 2);
                    break;
                }
            }

            try
            {
                if (resultRow.DiDian == null || resultRow.DiDian == "")
                {
                    return;
                }
            }
            catch (Exception e)
            {
                return;
            }

            resultRow.AvgValue = Math.Round(avgValue, 2);
            resultRow.MaxValue = max;
            resultRow.BaoJingCiShu = baoJingCiShu;
            resultRow.LeiJiBaoJing = leiJiBaoJing;
            resultRow.DuanDianCiShu = duanDianCiShu;
            resultRow.LeiJiDuanDian = leiJiDuanDian;

            resultRow.KuiDianYiChang = kuiDianYiChangCiShu;
            resultRow.YiChangLeiJi = leiJiKuiDian;


            dt.Rows.Add(resultRow);
        }

        //首次进入某状态的操作, 参数必须传引用
        private static void firstState(ref bool firstsource, ref bool dest1, ref bool dest2, ref bool source, ref int count, ref DateTime startTime, DateTime value)
        {
            firstsource = false;
            dest1 = true;
            dest2 = true;
            source = true;
            count++;
            startTime = value;
        }

        //状态翻转， 参数必须传引用
        private static void stateChange(ref bool stateFlag, DateTime startTime, DateTime endTime, ref TimeSpan leiJiShiJian)
        {
            stateFlag = !stateFlag;
            leiJiShiJian += endTime - startTime;
        }
        #endregion

        #region//模拟量报警日报表
        /// <summary>
        /// 打印：模拟量报警日报表
        /// </summary>
        /// <param name="date">要打印数据的日期</param>
        /// <returns>模拟量报警日报表的数据表</returns>
        public static max_cmssDataSet.ABaoJingDataTable GetABaoJingData(DateTime date)
        {
            DateTime startTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime endTime = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            string[] ceDianPaiXu = ReportConfig.getConfigByName((int)ReportType.MoNiLiangBaoJing).GetCeDianBianHao();

            dateString = "HH:mm:ss";

            max_cmssDataSet.ABaoJingDataTable dt = QueryABaoJingData(startTime, endTime, ceDianPaiXu);
            dateString = "yyyy-MM-dd HH:mm:ss";
            return dt;
        }
        /// <summary>
        /// 查询：模拟量报警报表
        /// </summary>
        /// <param name="startTime">查询的开始时间</param>
        /// <param name="endTime">查询的结束时间</param>
        /// <param name="cedians">待输出的测点序列</param>
        /// <returns></returns>
        public static max_cmssDataSet.ABaoJingDataTable QueryABaoJingData(DateTime startTime, DateTime endTime, string[] cedians)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            max_cmssDataSet.ABaoJingDataTable dt = new max_cmssDataSet.ABaoJingDataTable();
            //max_cmssDataSet.ABaoJingDataTable resultTable = new max_cmssDataSet.ABaoJingDataTable();



            //cedians = cedians.Distinct().ToArray<string>();

            if (doMoNiLiangSelect(startTime, endTime, ref dt1, ref dt2, cedians))
                dt = doGetABaoJingReportData(dt1, dt2);

            foreach (max_cmssDataSet.ABaoJingRow dr in dt.Rows)
            {
                dr.CuoShi = Measure.GetMeasure(long.Parse(dr["CeDianID"] + ""), startTime, endTime);

            }
            return dt;

        }
        /// <summary>
        /// 处理查询所得的原始数据，模拟量报警报表
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cedian"></param>
        /// <returns></returns>
        private static max_cmssDataSet.ABaoJingDataTable doGetABaoJingReportData(DataTable value, DataTable cedian)
        {
            max_cmssDataSet.ABaoJingDataTable dt = new max_cmssDataSet.ABaoJingDataTable();

            double sum = 0f;
            double avg = 0f;
            double max = -1000f;
            DateTime maxValueTime = DateTime.Now; ;
            double currentValue = 0f;
            DateTime currentValueStartTime;
            TimeSpan currentValueSpan = TimeSpan.Zero;

            double sumPerTime = 0f;
            double avgPerTime = 0f;
            double maxPerTime = -1000f;
            double currentBaoJingValue = 0f;
            DateTime maxDatePerTime = DateTime.Now;
            DateTime currentBaoJingValueStartTime = DateTime.Now;
            TimeSpan currentBaoJingValueSpan = TimeSpan.Zero;
            string avgAndMaxPerTime = string.Empty;

            bool baojing = false;
            bool firstbaojing = true;
            DateTime baojingStartTime = DateTime.Now;
            DateTime baojingEndTime = DateTime.Now;
            TimeSpan duraTime = TimeSpan.Zero;
            string startAndEnd = string.Empty;
            string timePerBaoJing = string.Empty;
            TimeSpan leijibaojing = TimeSpan.Zero;
            int baoJingCiShu = 0;
            int baoJingCount = 0;

            bool firstother = true;

            string ceDianBianHao = value.Rows[0]["ceDianBianHao"].ToString().Trim();
            long cedianId = (long)value.Rows[0]["cedianID"];
            currentValueStartTime = (DateTime)value.Rows[0]["uploadTime"];
            int tempId = 999999;
            foreach (DataRow row in value.Rows)
            {
            newcedian:

                if (cedianId == (long)row["cedianID"])
                {
                    float uploadValueThisRow = (float)row["uploadValue"];
                    DateTime uploadTimeThisRow = (DateTime)row["uploadTime"];
                    if (uploadValueThisRow > max)//最大值
                    {
                        max = uploadValueThisRow;
                        maxValueTime = uploadTimeThisRow;
                    }

                    if (currentValue != uploadValueThisRow)//平均值
                    {
                        if (sum == 0.0)
                        {
                            currentValueSpan = uploadTimeThisRow - new DateTime(currentValueStartTime.Year, currentValueStartTime.Month, currentValueStartTime.Day, 0, 0, 0);
                        }
                        else
                        {
                            currentValueSpan = uploadTimeThisRow - currentValueStartTime;
                        }
                        //currentValueSpan = uploadTimeThisRow - currentValueStartTime;
                        sum += currentValue * currentValueSpan.TotalMilliseconds;
                        currentValue = uploadValueThisRow;
                        currentValueStartTime = uploadTimeThisRow;
                    }

                    MoNiLiangState currentState = (MoNiLiangState)Convert.ToInt32(row["state"]);
                    if (currentState == MoNiLiangState.BaoJing || currentState == MoNiLiangState.DuanDian)
                    {
                        baoJingCount++;
                        if (firstbaojing)
                        {
                            firstbaojing = false;
                            firstother = true;
                            baojing = true;
                            //baoJingCiShu++;
                            baojingStartTime = uploadTimeThisRow;
                            currentBaoJingValue = uploadValueThisRow;
                            currentBaoJingValueStartTime = baojingStartTime;
                            sumPerTime = avgPerTime = 0f;
                            maxPerTime = -1000f;
                        }
                        if (uploadValueThisRow > maxPerTime)
                        {
                            maxPerTime = uploadValueThisRow;
                            maxDatePerTime = uploadTimeThisRow;
                        }
                        if (currentBaoJingValue != uploadValueThisRow)
                        {
                            currentBaoJingValueSpan = uploadTimeThisRow - currentBaoJingValueStartTime;
                            sumPerTime += currentBaoJingValue * currentBaoJingValueSpan.TotalMilliseconds;
                            currentBaoJingValue = uploadValueThisRow;
                            currentBaoJingValueStartTime = uploadTimeThisRow;
                        }
                    }
                    else
                    {
                        if (firstother)
                        {
                            firstbaojing = true;
                            firstother = false;
                            if (baojing)
                            {
                                baojing = false;
                                baojingEndTime = uploadTimeThisRow;
                                duraTime = baojingEndTime - baojingStartTime;
                                if (!(baoJingCount < 2 && duraTime.TotalSeconds < 4))
                                {
                                    baoJingCiShu++;
                                    startAndEnd += baojingStartTime.ToString(dateString) + "/" + baojingEndTime.ToString(dateString) + "\n";
                                    timePerBaoJing += duraTime.Hours + ":" + duraTime.Minutes + ":" + duraTime.Seconds + "\n";
                                    leijibaojing += duraTime;

                                    currentBaoJingValueSpan = baojingEndTime - currentBaoJingValueStartTime;
                                    sumPerTime += currentBaoJingValue * currentBaoJingValueSpan.TotalMilliseconds;
                                    avgPerTime = sumPerTime / duraTime.TotalMilliseconds;

                                    avgAndMaxPerTime += Math.Round(avgPerTime, 2) + "," + Math.Round(maxPerTime, 2) + "," + maxDatePerTime.ToString(dateString) + "\n";

                                    addABaoJingReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, -1, duraTime.Hours + ":" + duraTime.Minutes + ":" + duraTime.Seconds, -1, "", baojingStartTime.ToString(dateString) + "/" + baojingEndTime.ToString(dateString), duraTime.Hours + ":" + duraTime.Minutes + ":" + duraTime.Seconds, Math.Round(avgPerTime, 2) + "," + Math.Round(maxPerTime, 2) + "," + maxDatePerTime.ToString(dateString));

                                }
                                baoJingCount = 0;
                                maxPerTime = -1000; avgPerTime = 0; sumPerTime = 0;




                            }
                        }
                    }
                }
                else
                {
                    currentValueSpan = new DateTime(currentValueStartTime.Year, currentValueStartTime.Month, currentValueStartTime.Day, 23, 59, 59) - currentValueStartTime;
                    sum += currentValue * currentValueSpan.TotalMilliseconds;
                    avg = sum / (24 * 3600000);
                    if (baoJingCiShu > 0)
                    {
                        addABaoJingReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, baoJingCiShu, leijibaojing.Hours + ":" + leijibaojing.Minutes + ":" + leijibaojing.Seconds, Math.Round(avg, 2), Math.Round(max, 2) + "/"
                            + maxValueTime.ToString(dateString), "", "", "");
                    }

                    startAndEnd = timePerBaoJing = avgAndMaxPerTime = string.Empty;
                    sum = avg = currentValue = 0f;
                    max = -1000f;
                    baoJingCiShu = 0;
                    duraTime = leijibaojing = TimeSpan.Zero;
                    ceDianBianHao = row["ceDianBianHao"].ToString().Trim();
                    cedianId = (long)row["cedianID"];
                    currentValueStartTime = (DateTime)row["uploadTime"];
                    firstbaojing = firstother = true;
                    baojing = false;

                    goto newcedian;
                }
            }
            currentValueSpan = (DateTime)value.Rows[value.Rows.Count - 1]["uploadTime"] - currentValueStartTime;
            sum += currentValue * currentValueSpan.TotalMilliseconds;
            avg = sum / (24 * 3600000);
            if (baoJingCiShu > 0)
            {
                addABaoJingReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, baoJingCiShu, leijibaojing.Hours + ":" + leijibaojing.Minutes + ":" + leijibaojing.Seconds, Math.Round(avg, 2), Math.Round(max, 2) + "/"
                    + maxValueTime.ToString(dateString), "", "", "");
            }

            return dt;
        }
        /// <summary>
        /// 往模拟量报警报表中添加一行数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ceDian"></param>
        /// <param name="ceDianBianHao"></param>
        /// <param name="baojingcishu"></param>
        /// <param name="leijibaojing"></param>
        /// <param name="avgValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="startAndEnd"></param>
        /// <param name="timePerBaoJing"></param>
        /// <param name="minMaxPerTime"></param>
        private static void addABaoJingReportRow(ref max_cmssDataSet.ABaoJingDataTable dt, DataTable ceDian, string ceDianBianHao, long cedianId, long tempid, int baojingcishu, string leijibaojing,
            double avgValue, string maxValue, string startAndEnd, string timePerBaoJing, string minMaxPerTime)
        {
            max_cmssDataSet.ABaoJingRow resultRow = (max_cmssDataSet.ABaoJingRow)dt.NewRow();
            resultRow.CeDianBianHao = ceDianBianHao;

            foreach (DataRow row in ceDian.Rows)
            {
                string bianHao = row["ceDianBianHao"].ToString().Trim();
                long cdid = (long)row["id"];

                if (cdid == cedianId)
                {
                    resultRow.DiDian = bianHao + "/" + row["ceDianWeiZhi"] + "/" + row["xiaoLeiXing"];
                    resultRow.DanWei = row["danWei"].ToString();
                    resultRow.BaoJingMenXian = Math.Round(Convert.ToSingle(row["baoJingZhiShangXian"]), 2);
                    break;
                }
            }

            if (baojingcishu >= 0)
            {
                resultRow.BaoJingCiShu = baojingcishu;
            }
            resultRow.LeiJiBaoJing = leijibaojing;

            if (avgValue >= 0)
            {
                resultRow.AvgValue = Math.Round(avgValue, 2);
            }
            resultRow.CeDianID = tempid + "";
            resultRow.BaoJingPerTime = startAndEnd;
            resultRow.DuraPerTime = timePerBaoJing;
            resultRow.AvgPerTime = minMaxPerTime;
            resultRow.MaxValue = maxValue;
            dt.Rows.Add(resultRow);
        }

        #endregion//模拟量报警日报表

        #region//模拟量断电日报表
        /// <summary>
        /// 打印：模拟量断电日报表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static max_cmssDataSet.ADuanDianDataTable GetADuanDianData(DateTime date)
        {
            DateTime startTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime endTime = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            string[] ceDianPaiXu = ReportConfig.getConfigByName((int)ReportType.MoNiLiangDuanDian).GetCeDianBianHao();

            dateString = "HH:mm:ss";

            max_cmssDataSet.ADuanDianDataTable dt = QueryADuanDianData(startTime, endTime, ceDianPaiXu);
            dateString = "yyyy-MM-dd HH:mm:ss";
            return dt;
        }
        /// <summary>
        /// 查询：模拟量断电报表
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="cedians"></param>
        /// <returns></returns>
        public static max_cmssDataSet.ADuanDianDataTable QueryADuanDianData(DateTime startTime, DateTime endTime, string[] cedians)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            max_cmssDataSet.ADuanDianDataTable dt = doGetADuanDianReportData(dt1, dt2);

            if (doMoNiLiangSelect(startTime, endTime, ref dt1, ref dt2, cedians))
                dt = doGetADuanDianReportData(dt1, dt2);

            foreach (max_cmssDataSet.ADuanDianRow dr in dt.Rows)
            {
                dr.CuoShi = Measure.GetMeasure(long.Parse(dr["CeDianID"] + ""), startTime, endTime);

            }
            return dt;
        }
        /// <summary>
        /// 处理模拟量断电报表查询所得的原始数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cedian"></param>
        /// <returns></returns>
        private static max_cmssDataSet.ADuanDianDataTable doGetADuanDianReportData(DataTable value, DataTable cedian)
        {
            max_cmssDataSet.ADuanDianDataTable dt = new max_cmssDataSet.ADuanDianDataTable();

            if (value.Rows.Count <= 0)
            {
                return dt;
            }

            double sum = 0f;
            double avg = 0f;
            double max = -1000f;
            DateTime maxValueTime = DateTime.Now; ;
            double currentValue = 0f;
            DateTime currentValueStartTime = DateTime.Now;
            TimeSpan currentValueSpan = TimeSpan.Zero;

            double sumPerTime = 0f;
            double avgPerTime = 0f;
            double maxPerTime = -1000f;
            double currentDuanDianValue = 0f;
            DateTime maxDatePerTime = DateTime.Now;
            DateTime currentDuanDianValueStartTime = DateTime.Now;
            TimeSpan currentDuanDianValueSpan = TimeSpan.Zero;
            string avgAndMaxPerTime = string.Empty;

            bool duandian = false;
            bool firstduandian = true;
            DateTime duandianStartTime = DateTime.Now;
            DateTime duandianEndTime = DateTime.Now;
            TimeSpan duraTime = TimeSpan.Zero;
            string startAndEnd = string.Empty;
            string timePerDuanDian = string.Empty;
            TimeSpan leijiduandian = TimeSpan.Zero;
            int duanDianCiShu = 0;

            int preKuiDian = -1;
            int curKuiDian = -1;
            DateTime curKuiDianStartTime = DateTime.Now;
            string kuiDianStartAndEnd = string.Empty;
            DateTime currentTime = DateTime.Now;
            TimeSpan leiJiKuiDian = TimeSpan.Zero;

            bool firstother = true;
            int duandianCount = 0;

            string ceDianBianHao = value.Rows[0]["ceDianBianHao"].ToString().Trim();
            long cedianId = (long)value.Rows[0]["cedianID"];



            int tempId = 999999;

            foreach (DataRow row in value.Rows)
            {
            newcedian:
                if (ceDianBianHao == row["ceDianBianHao"].ToString().Trim())
                {
                    currentTime = (DateTime)row["uploadTime"];
                    float uploadValueThisRow = (float)row["uploadValue"];
                    if (uploadValueThisRow > max)//最大值
                    {
                        max = uploadValueThisRow;
                        maxValueTime = currentTime;
                    }

                    if (currentValue != uploadValueThisRow)//平均值
                    {
                        if (sum == 0.0)
                        {
                            currentValueSpan = currentTime - new DateTime(currentValueStartTime.Year, currentValueStartTime.Month, currentValueStartTime.Day, 0, 0, 0);
                        }
                        else
                        {
                            currentValueSpan = currentTime - currentValueStartTime;
                        }
                        //currentValueSpan = currentTime - currentValueStartTime;
                        sum += currentValue * currentValueSpan.TotalMilliseconds;
                        currentValue = uploadValueThisRow;
                        currentValueStartTime = currentTime;
                    }
                    curKuiDian = Convert.ToInt32(row["kuiDianYiChang"]);
                    if (curKuiDian != preKuiDian)
                    {
                        if (preKuiDian == 0)
                        {
                            kuiDianStartAndEnd = curKuiDianStartTime.ToString(dateString) + "," + currentTime.ToString(dateString) + "\n";
                            leiJiKuiDian += currentTime - curKuiDianStartTime;
                        }

                        preKuiDian = curKuiDian;
                        curKuiDianStartTime = currentTime;
                    }

                    MoNiLiangState currentState = (MoNiLiangState)Convert.ToInt32(row["state"]);
                    if (currentState == MoNiLiangState.DuanDian)
                    {
                        if (firstduandian)
                        {
                            firstduandian = false;
                            firstother = true;
                            duandian = true;
                            //duanDianCiShu++;
                            duandianStartTime = currentTime;

                            currentDuanDianValue = uploadValueThisRow;
                            currentDuanDianValueStartTime = duandianStartTime;
                            sumPerTime = 0f;
                            maxPerTime = -1000f;
                        }
                        duandianCount++;
                        if (uploadValueThisRow > maxPerTime)
                        {
                            maxPerTime = currentDuanDianValue;
                            maxDatePerTime = currentTime;
                        }
                        if (currentDuanDianValue != uploadValueThisRow)
                        {
                            currentDuanDianValueSpan = currentTime - currentDuanDianValueStartTime;
                            sumPerTime += currentDuanDianValue * currentDuanDianValueSpan.TotalMilliseconds;
                            currentDuanDianValue = uploadValueThisRow;
                            currentDuanDianValueStartTime = currentTime;
                        }
                    }
                    else
                    {
                        if (firstother)
                        {
                            firstduandian = true;
                            firstother = false;
                            if (duandian)
                            {
                                duandian = false;
                                duandianEndTime = currentTime;
                                duraTime = duandianEndTime - duandianStartTime;
                                if (!(duandianCount < 2 && duraTime.TotalSeconds < 4))
                                {
                                    duanDianCiShu++;
                                    startAndEnd += duandianStartTime.ToString(dateString) + "," + duandianEndTime.ToString(dateString) + "\n";
                                    timePerDuanDian += GlobalParams.TimeSpanString(duraTime) + "\n";
                                    leijiduandian += duraTime;

                                    currentDuanDianValueSpan = duandianEndTime - currentDuanDianValueStartTime;
                                    sumPerTime += currentDuanDianValue * currentDuanDianValueSpan.TotalMilliseconds;
                                    avgPerTime = sumPerTime / duraTime.TotalMilliseconds;

                                    avgAndMaxPerTime += Math.Round(avgPerTime, 2) + "," + Math.Round(maxPerTime, 2) + "," + maxDatePerTime.ToString(dateString) + "\n";

                                    addADuanDianReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, -1, GlobalParams.TimeSpanString(duraTime), -1, "", duandianStartTime.ToString(dateString) + "," + duandianEndTime.ToString(dateString), GlobalParams.TimeSpanString(duraTime), Math.Round(avgPerTime, 2) + "," + Math.Round(maxPerTime, 2) + "," + maxDatePerTime.ToString(dateString), "");

                                }
                                duandianCount = 0;
                            }
                        }
                    }
                }
                else
                {
                    currentValueSpan = new DateTime(currentValueStartTime.Year, currentValueStartTime.Month, currentValueStartTime.Day, 23, 59, 59) - currentValueStartTime;
                    sum += currentValue * currentValueSpan.TotalMilliseconds;
                    avg = sum / (24 * 3600000);
                    if (duanDianCiShu > 0)
                    {
                        addADuanDianReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, duanDianCiShu, GlobalParams.TimeSpanString(leijiduandian), avg, Math.Round(max, 2) + "/"
                            + maxValueTime.ToString(dateString), "", "", "", GlobalParams.TimeSpanString(leiJiKuiDian) + "," + kuiDianStartAndEnd);
                    }
                    startAndEnd = timePerDuanDian = avgAndMaxPerTime = string.Empty;
                    sum = avg = max = currentValue = sumPerTime = avgPerTime = maxPerTime = currentDuanDianValue = 0;
                    duanDianCiShu = 0;
                    duraTime = leijiduandian = TimeSpan.Zero;
                    ceDianBianHao = row["ceDianBianHao"].ToString().Trim();
                    cedianId = (long)row["cedianID"];
                    firstduandian = firstother = true;
                    duandian = false;

                    goto newcedian;
                }
            }
            currentValueSpan = (DateTime)value.Rows[value.Rows.Count - 1]["uploadTime"] - currentValueStartTime;
            sum += currentValue * currentValueSpan.TotalMilliseconds;
            avg = sum / (24 * 3600000);
            if (duanDianCiShu > 0)
            {
                addADuanDianReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, duanDianCiShu, GlobalParams.TimeSpanString(leijiduandian), avg, Math.Round(max, 2) + "/"
                    + maxValueTime.ToString(dateString), "", "", "", GlobalParams.TimeSpanString(leiJiKuiDian) + "," + kuiDianStartAndEnd);
            }
            return dt;
        }

        private static void addADuanDianReportRow(ref max_cmssDataSet.ADuanDianDataTable dt, DataTable ceDian, string ceDianBianHao, long cedianId, long tempid, int duanDianCiShu, string leiJiDuanDian,
            double avgValue, string maxValue, string startAndEnd, string timePerDuanDian, string avgMaxPerTime, string kuiDian)
        {
            max_cmssDataSet.ADuanDianRow resultRow = (max_cmssDataSet.ADuanDianRow)dt.NewRow();
            resultRow.CeDianBianHao = ceDianBianHao;
            foreach (DataRow row in ceDian.Rows)
            {
                string bianhao = row["ceDianBianHao"].ToString().Trim();
                long cdid = (long)row["id"];

                if (cdid == cedianId)
                {
                    resultRow.DiDian = bianhao + "/" + row["ceDianWeiZhi"] + "/" + row["xiaoLeiXing"];
                    resultRow.DanWei = row["danWei"].ToString();
                    resultRow.DuanDianMenXian = Math.Round(Convert.ToSingle(row["duanDianZhi"]), 2).ToString();
                    resultRow.FuDianMenXian = Math.Round(Convert.ToSingle(row["fuDianZhi"]), 2).ToString();
                    break;
                }
            }

            if (duanDianCiShu >= 0)
            {

                resultRow.DuanDianCiShu = duanDianCiShu;
            }
            resultRow.LeiJiDuanDian = leiJiDuanDian;

            resultRow.MaxValue = maxValue;
            if (avgValue >= 0)
            {
                resultRow.AvgValue = Math.Round(avgValue, 2);
            }
            resultRow.DuanDianShiKe = startAndEnd;
            resultRow.AvgPerTime = avgMaxPerTime;
            resultRow.LeiJiPerTime = timePerDuanDian;
            resultRow.QuYu = kuiDian;
            resultRow.DuanDianQuYu = GlobalParams.AllCeDianList.GetDuanDianQuYu(ceDianBianHao);
            resultRow.CeDianID = tempid + "";
            dt.Rows.Add(resultRow);
        }
        #endregion//模拟量断电日报表

        #region //模拟量馈电日报表

        /// <summary>
        /// 打印：模拟量馈电日报表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static max_cmssDataSet.AKuiDianDataTable GetAKuiDianData(DateTime date)
        {
            DateTime startTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime endTime = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            string[] ceDianPaiXu = ReportConfig.getConfigByName((int)ReportType.MoNiLiangKuDian).GetCeDianBianHao();

            dateString = "HH:mm:ss";

            max_cmssDataSet.AKuiDianDataTable dt = QueryAKuiDianData(startTime, endTime, ceDianPaiXu);
            dateString = "yyyy-MM-dd HH:mm:ss";
            return dt;
        }

        /// <summary>
        /// 查询：模拟量馈电报表
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="cedians"></param>
        /// <returns></returns>
        public static max_cmssDataSet.AKuiDianDataTable QueryAKuiDianData(DateTime startTime, DateTime endTime, string[] cedians)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            max_cmssDataSet.AKuiDianDataTable dt = new max_cmssDataSet.AKuiDianDataTable();


            if (doMoNiLiangSelect(startTime, endTime, ref dt1, ref dt2, cedians))
                dt = doGetAKuiDianReportData(dt1, dt2);

            foreach (max_cmssDataSet.AKuiDianRow dr in dt.Rows)
            {
                dr.CuoShi = Measure.GetMeasure(long.Parse(dr["CeDianID"] + ""), startTime, endTime);

            }
            return dt;


        }

        /// <summary>
        /// 处理查询所得的原始数据，模拟量馈电报表
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cedian"></param>
        /// <returns></returns>
        private static max_cmssDataSet.AKuiDianDataTable doGetAKuiDianReportData(DataTable value, DataTable cedian)
        {
            max_cmssDataSet.AKuiDianDataTable dt = new max_cmssDataSet.AKuiDianDataTable();

            DateTime currentValueStartTime;
            TimeSpan currentValueSpan = TimeSpan.Zero;

            DateTime currentBaoJingValueStartTime = DateTime.Now;
            TimeSpan currentBaoJingValueSpan = TimeSpan.Zero;
            string avgAndMaxPerTime = string.Empty;

            bool kuiDianYiChang = false;
            bool firstKuiDianYiChang = true;
            DateTime kuiDianStartTime = DateTime.Now;
            DateTime kuiDianEndTime = DateTime.Now;
            TimeSpan duraTime = TimeSpan.Zero;
            string startAndEnd = string.Empty;
            string timePerKuiDian = string.Empty;
            TimeSpan leiJiKuiDian = TimeSpan.Zero;
            int kuiDianYiChangCiShu = 0;

            bool firstother = true;

            string ceDianBianHao = value.Rows[0]["ceDianBianHao"].ToString().Trim();
            long cedianId = (long)value.Rows[0]["cedianID"];
            int tempId = 999999;
            currentValueStartTime = (DateTime)value.Rows[0]["uploadTime"];

            foreach (DataRow row in value.Rows)
            {
            newcedian:
                if (cedianId == (long)row["cedianID"])
                {
                    MoNiLiangState currentState = (MoNiLiangState)Convert.ToInt32(row["state"]);
                    int kuiDian = Convert.ToInt32(row["kuiDianYiChang"]);
                    if (kuiDian == 1)
                    {
                        if (firstKuiDianYiChang)
                        {
                            firstKuiDianYiChang = false;
                            firstother = true;
                            kuiDianYiChang = true;
                            kuiDianYiChangCiShu++;
                            kuiDianStartTime = (DateTime)row["uploadTime"];
                        }
                    }
                    else
                    {
                        if (firstother)
                        {
                            firstKuiDianYiChang = true;
                            firstother = false;
                            if (kuiDianYiChang)
                            {
                                kuiDianYiChang = false;
                                kuiDianEndTime = (DateTime)row["uploadTime"];
                                duraTime = kuiDianEndTime - kuiDianStartTime;
                                startAndEnd += GlobalParams.TimeSpanString(duraTime) + ", " + kuiDianStartTime.ToString(dateString) + ", " + kuiDianEndTime.ToString(dateString) + "\n";
                                leiJiKuiDian += duraTime;


                                addAKuiDianReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, -1, GlobalParams.TimeSpanString(duraTime), GlobalParams.TimeSpanString(duraTime) + ", " + kuiDianStartTime.ToString(dateString) + ", " + kuiDianEndTime.ToString(dateString));


                            }
                        }
                    }
                }
                else
                {
                    if (kuiDianYiChang)
                    {
                        kuiDianEndTime = new DateTime(kuiDianStartTime.Year, kuiDianStartTime.Month, kuiDianStartTime.Day, 23, 59, 59);
                        duraTime = kuiDianEndTime - kuiDianStartTime;
                        leiJiKuiDian += duraTime;
                        startAndEnd += duraTime.ToString() + ", " + kuiDianStartTime.ToString(dateString) + ", " + kuiDianEndTime.ToString(dateString) + "\n";

                        addAKuiDianReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, -1, GlobalParams.TimeSpanString(duraTime), GlobalParams.TimeSpanString(duraTime) + ", " + kuiDianStartTime.ToString(dateString) + ", " + kuiDianEndTime.ToString(dateString));


                    }
                    if (kuiDianYiChangCiShu > 0)
                    {
                        addAKuiDianReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, kuiDianYiChangCiShu, GlobalParams.TimeSpanString(leiJiKuiDian), "");
                    }

                    startAndEnd = timePerKuiDian = avgAndMaxPerTime = string.Empty;
                    duraTime = leiJiKuiDian = TimeSpan.Zero;
                    kuiDianYiChangCiShu = 0;
                    ceDianBianHao = row["ceDianBianHao"].ToString().Trim();
                    cedianId = (long)row["cedianID"];
                    firstKuiDianYiChang = firstother = true;
                    kuiDianYiChang = false;

                    goto newcedian;
                }
            }

            if (kuiDianYiChangCiShu > 0)
            {
                addAKuiDianReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, kuiDianYiChangCiShu, GlobalParams.TimeSpanString(leiJiKuiDian), startAndEnd);
            }
            return dt;
        }

        //往模拟量馈电报表中添加一行数据
        private static void addAKuiDianReportRow(ref max_cmssDataSet.AKuiDianDataTable dt, DataTable ceDian, string ceDianBianHao, long cedianId, long tempid, int kuiDianYiChangCiShu, string leijikuidian,
            string startAndEnd)
        {
            max_cmssDataSet.AKuiDianRow resultRow = (max_cmssDataSet.AKuiDianRow)dt.NewRow();
            resultRow.CeDianBianHao = ceDianBianHao;
            foreach (DataRow row in ceDian.Rows)
            {
                string bianhao = row["ceDianBianHao"].ToString().Trim();
                long cdid = (long)row["id"];

                if (cdid == cedianId)
                {
                    resultRow.DiDian = bianhao + "/" + row["ceDianWeiZhi"] + "/" + row["xiaoLeiXing"];
                    break;
                }
            }
            if (kuiDianYiChangCiShu >= 0)
            {
                resultRow.LeiJiCiShu = kuiDianYiChangCiShu;
            }
            resultRow.KuiDianYiChangLeiJi = leijikuidian;
            resultRow.KuiDianPerTime = startAndEnd;
            resultRow.DuanDianQuYu = GlobalParams.AllCeDianList.GetDuanDianQuYu(ceDianBianHao);
            resultRow.CeDianID = tempid + "";
            dt.Rows.Add(resultRow);
        }
        #endregion//模拟量馈电日报表

        #region//开关量报警断电日报表
        /// <summary>
        /// 打印：开关量报警断电日报表
        /// </summary>
        /// <param name="date">所打印数据的日期</param>
        /// <returns></returns>
        public static max_cmssDataSet.DBaoJingDataTable GetDBaoJingData(DateTime date)
        {
            DateTime startTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            DateTime endTime = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            string[] ceDianPaiXu = ReportConfig.getConfigByName((int)ReportType.KaiGuanLiangBaoJing).GetCeDianBianHao();

            dateString = "HH:mm:ss";

            max_cmssDataSet.DBaoJingDataTable dt = QueryDBaoJingData(startTime, endTime, ceDianPaiXu);
            dateString = "yyyy-MM-dd HH:mm:ss";
            return dt;
        }
        public static max_cmssDataSet.DBaoJingDataTable QueryDBaoJingData(DateTime startTime, DateTime endTime, string[] cedians)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            max_cmssDataSet.DBaoJingDataTable dt = new max_cmssDataSet.DBaoJingDataTable();

            if (doKaiGuanLiangSelect(startTime, endTime, ref dt1, ref dt2, cedians))
                dt = doGetDBaoJingReportData(dt1, dt2);

            foreach (max_cmssDataSet.DBaoJingRow dr in dt.Rows)
            {

                dr.AnQuanCuoShi = Measure.GetMeasure(long.Parse(dr["CeDianID"] + ""), startTime, endTime);
            }
            return dt;

            //max_cmssDataSet.DBaoJingDataTable resultTable = new max_cmssDataSet.DBaoJingDataTable();

            //if (doKaiGuanLiangSelect(startTime, endTime, ref dt1, ref dt2, cedians))
            //    dt = doGetDBaoJingReportData(dt1, dt2);

            //foreach (string s in cedians)
            //{
            //    max_cmssDataSet.DBaoJingRow row = dt.FindByCeDianBianHao(s);
            //    if (row != null && row.LeiJiCiShu > 0)
            //    {
            //        resultTable.ImportRow(row);
            //        row.AnQuanCuoShi = Measure.GetMeasure(s, startTime, endTime);
            //    }
            //    else
            //    {
            //        //int baojingState = -1;
            //        //int duandianState = -1;
            //        //string baoJingDuanDianZhuangTai = "";
            //        //getBaoJingDuanDianState(dt2, s, ref baojingState, ref duandianState, ref baoJingDuanDianZhuangTai);
            //        //addDBaoJingReportRow(ref resultTable, dt2, s, 0, "", "", baoJingDuanDianZhuangTai, "");
            //    }
            //}
            //return resultTable;
        }

        private static max_cmssDataSet.DBaoJingDataTable doGetDBaoJingReportData(DataTable value, DataTable cedian)
        {
            max_cmssDataSet.DBaoJingDataTable dt = new max_cmssDataSet.DBaoJingDataTable();

            int baojingState = -1;
            int duandianState = -1;
            string baoJingDuanDianZhuangTai = string.Empty;



            string startAndEnd = string.Empty;
            TimeSpan leijishijian = TimeSpan.Zero;
            TimeSpan duraSpan = TimeSpan.Zero;
            int baoJingDuanDianCiShu = 0;

            int preKuiDian = -1;
            int curKuiDian = -1;
            int kuiDianYiChangCiShu = 0;
            TimeSpan curKuiDianSpan = TimeSpan.Zero;
            TimeSpan leiJiKuiDian = TimeSpan.Zero;

            string kuiDianStartAndEnd = string.Empty;

            string ceDianBianHao = value.Rows[0]["ceDianBianHao"].ToString().Trim();
            long cedianId = (long)value.Rows[0]["cedianID"];
            int tempId = 999999;
            getBaoJingDuanDianState(cedian, ceDianBianHao, ref baojingState, ref duandianState, ref baoJingDuanDianZhuangTai);
            int curState = Convert.ToInt32(value.Rows[0]["uploadValue"]);
            int preState = Convert.ToInt32(value.Rows[0]["uploadValue"]);
            DateTime currentKuiDianStartTime = DateTime.Now;

            DateTime currentTime = (DateTime)value.Rows[0]["uploadTime"];
            DateTime currentStateStartTime = (DateTime)value.Rows[0]["uploadTime"];


            foreach (DataRow row in value.Rows)
            {
            newcedian:
                if (cedianId == (long)row["cedianID"])
                {
                    curState = Convert.ToInt32(row["uploadValue"]);
                    currentTime = (DateTime)row["uploadTime"];
                    curKuiDian = Convert.ToInt32(row["kuiDianYiChang"]);

                    if (curState != preState)
                    {
                        bool baojingorduandian = doStateChange(preState, baojingState, duandianState, currentStateStartTime, currentTime, ref leijishijian, ref startAndEnd, ref baoJingDuanDianCiShu);
                        if (baojingorduandian)
                        {
                            duraSpan = currentTime - currentStateStartTime;

                            addDBaoJingReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, -1, GlobalParams.TimeSpanString(duraSpan), GlobalParams.TimeSpanString(duraSpan) + "," + currentStateStartTime.ToString(dateString) + "," + currentTime.ToString(dateString), baoJingDuanDianZhuangTai, "");
                        }
                        preState = curState;
                        currentStateStartTime = currentTime;


                    }

                    if (curKuiDian != preState)
                    {
                        if (preKuiDian == 0)
                        {
                            kuiDianYiChangCiShu++;
                            curKuiDianSpan = currentTime - currentKuiDianStartTime;
                            leiJiKuiDian += curKuiDianSpan;
                            kuiDianStartAndEnd += curKuiDianSpan.ToString() + "," + currentKuiDianStartTime.ToString(dateString) + "," + currentTime.ToString(dateString) + "\n";


                        }


                    }
                }
                else
                {
                    if (currentStateStartTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        currentTime = DateTime.Now;
                    }
                    else
                    {
                        currentTime = new DateTime(currentStateStartTime.Year, currentStateStartTime.Month, currentStateStartTime.Day, 23, 59, 59);
                    }


                    doStateChange(preState, baojingState, duandianState, currentStateStartTime, currentTime, ref leijishijian, ref startAndEnd, ref baoJingDuanDianCiShu);
                    if (preKuiDian == 0)
                    {
                        kuiDianYiChangCiShu++;
                        curKuiDianSpan = currentTime - currentKuiDianStartTime;
                        leiJiKuiDian += curKuiDianSpan;
                        kuiDianStartAndEnd += GlobalParams.TimeSpanString(curKuiDianSpan) + "," + currentKuiDianStartTime.ToString(dateString) + "," + currentTime.ToString(dateString) + "\n";
                    }


                    duraSpan = currentTime - currentStateStartTime;

                    addDBaoJingReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, -1, GlobalParams.TimeSpanString(duraSpan), GlobalParams.TimeSpanString(duraSpan) + "," + currentStateStartTime.ToString(dateString) + "," + currentTime.ToString(dateString), baoJingDuanDianZhuangTai, "");

                    addDBaoJingReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, baoJingDuanDianCiShu, GlobalParams.TimeSpanString(leijishijian), "", baoJingDuanDianZhuangTai, GlobalParams.TimeSpanString(leiJiKuiDian) + "\n" + kuiDianStartAndEnd);

                    startAndEnd = string.Empty;
                    leijishijian = TimeSpan.Zero;
                    baoJingDuanDianCiShu = 0;
                    preState = -1;
                    preKuiDian = -1;
                    kuiDianStartAndEnd = string.Empty;
                    kuiDianYiChangCiShu = 0;
                    leiJiKuiDian = TimeSpan.Zero;
                    ceDianBianHao = row["ceDianBianHao"].ToString().Trim();
                    cedianId = (long)row["cedianID"];
                    getBaoJingDuanDianState(cedian, ceDianBianHao, ref baojingState, ref duandianState, ref baoJingDuanDianZhuangTai);

                    goto newcedian;
                }
            }

            if (currentStateStartTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                currentTime = DateTime.Now;
            }
            else
            {
                currentTime = new DateTime(currentStateStartTime.Year, currentStateStartTime.Month, currentStateStartTime.Day, 23, 59, 59);
            }
            bool baojingorduandian2 = doStateChange(preState, baojingState, duandianState, currentStateStartTime, currentTime, ref leijishijian, ref startAndEnd, ref baoJingDuanDianCiShu);
            if (preKuiDian == 0)
            {
                kuiDianYiChangCiShu++;
                curKuiDianSpan = currentTime - currentKuiDianStartTime;
                leiJiKuiDian += curKuiDianSpan;
                kuiDianStartAndEnd += curKuiDianSpan.ToString() + "," + currentKuiDianStartTime.ToString(dateString) + "," + currentTime.ToString(dateString) + "\n";
            }

            addDBaoJingReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, -1, GlobalParams.TimeSpanString(duraSpan), GlobalParams.TimeSpanString(duraSpan) + "," + currentStateStartTime.ToString(dateString) + "," + currentTime.ToString(dateString), baoJingDuanDianZhuangTai, "");

            addDBaoJingReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, baoJingDuanDianCiShu, GlobalParams.TimeSpanString(leijishijian), "", baoJingDuanDianZhuangTai, GlobalParams.TimeSpanString(leiJiKuiDian) + "\n" + kuiDianStartAndEnd);


            return dt;
        }
        private static void getBaoJingDuanDianState(DataTable dt, string cedianbianhao, ref int baoJingState, ref int duanDianState, ref string zhuangTaiMingCheng)
        {
            zhuangTaiMingCheng = string.Empty;
            baoJingState = -1;
            duanDianState = -1;

            foreach (DataRow row in dt.Rows)
            {
                if (row["ceDianBianHao"].ToString().Trim() == cedianbianhao)
                {
                    baoJingState = Convert.ToInt32(row["baoJingZhuangTai"]);
                    duanDianState = Convert.ToInt32(row["duanDianZhuangTai"]);
                    string baojing = string.Empty;
                    string duandian = string.Empty;
                    if (baoJingState == 0)
                        baojing = row["lingTaiMingCheng"].ToString();
                    else if (baoJingState == 1)
                        baojing = row["yiTaiMingCheng"].ToString();
                    else if (baoJingState == 2)
                        baojing = row["erTaiMingCheng"].ToString();
                    if (duanDianState == 0)
                        duandian = row["lingTaiMingCheng"].ToString();
                    else if (duanDianState == 1)
                        duandian = row["yiTaiMingCheng"].ToString();
                    else if (duanDianState == 2)
                        duandian = row["erTaiMingCheng"].ToString();

                    zhuangTaiMingCheng = baojing + "/" + duandian;
                }
            }
        }
        private static bool doStateChange(int state, int baojingState, int duandianState, DateTime startTime, DateTime endTime, ref TimeSpan leijishijian, ref string startAndEnd, ref int count)
        {
            TimeSpan duraSpan = TimeSpan.Zero;

            bool baijingorduandian = false;
            if (state == baojingState)
            {
                count++;
                duraSpan = endTime - startTime;
                leijishijian += duraSpan;
                startAndEnd += GlobalParams.TimeSpanString(duraSpan) + "," + startTime.ToString(dateString) + "," + endTime.ToString(dateString) + "\n";
                baijingorduandian = true;
            }
            else if (baojingState != duandianState && state == duandianState)
            {
                count++;
                duraSpan = endTime - startTime;
                leijishijian += duraSpan;
                startAndEnd += GlobalParams.TimeSpanString(duraSpan) + "," + startTime.ToString(dateString) + "," + endTime.ToString(dateString) + "\n";
                baijingorduandian = true;
            }

            return baijingorduandian;
        }
        private static void addDBaoJingReportRow(ref max_cmssDataSet.DBaoJingDataTable dt, DataTable ceDian, string ceDianBianHao, long cedianId, long tempid, int baoJingDuanDianCiShu, string leiJiShiJian,
            string startAndEnd, string baoJingDuanDianZhuangTai, string kuiDian)
        {
            max_cmssDataSet.DBaoJingRow resultRow = (max_cmssDataSet.DBaoJingRow)dt.NewRow();
            resultRow.CeDianBianHao = ceDianBianHao;
            foreach (DataRow row in ceDian.Rows)
            {
                string bianHao = row["ceDianBianHao"].ToString().Trim();
                long cdid = (long)row["id"];
                if (cdid == cedianId)
                {
                    resultRow.DiDian = bianHao + "/" + row["ceDianWeiZhi"] + "/" + row["xiaoLeiXing"];
                    resultRow.BaoJingZhuangTai = baoJingDuanDianZhuangTai;
                    break;
                }
            }

            if (baoJingDuanDianCiShu >= 0)
            {
                resultRow.LeiJiCiShu = baoJingDuanDianCiShu;
            }
            resultRow.LeiJiShiJian = leiJiShiJian;
            resultRow.ShiJianPerTime = startAndEnd;
            resultRow.DuanDianQuYu = GlobalParams.AllCeDianList.GetDuanDianQuYu(ceDianBianHao);
            resultRow.KuiDianZhuanTai = kuiDian;
            resultRow.CeDianID = tempid + "";

            dt.Rows.Add(resultRow);
        }
        #endregion//开关量报警断电日报表

        #region//开关量馈电异常日报表
        /// <summary>
        /// 打印：开关量馈电异常日报表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static max_cmssDataSet.DKuiDianDataTable GetDKuiDianData(DateTime date)
        {
            DateTime startTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime endTime = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            string[] ceDianPaiXu = ReportConfig.getConfigByName((int)ReportType.KaiGuanLiangKuiDian).GetCeDianBianHao();

            return QueryDKuiDianData(startTime, endTime, ceDianPaiXu);
        }
        /// <summary>
        /// 查询：开关量馈电异常报表
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="cedians"></param>
        /// <returns></returns>
        public static max_cmssDataSet.DKuiDianDataTable QueryDKuiDianData(DateTime startTime, DateTime endTime, string[] cedians)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            max_cmssDataSet.DKuiDianDataTable dt = new max_cmssDataSet.DKuiDianDataTable();

            if (doKaiGuanLiangSelect(startTime, endTime, ref dt1, ref dt2, cedians))
                dt = doGetDKuiDianReportData(dt1, dt2);

            foreach (max_cmssDataSet.DKuiDianRow dr in dt.Rows)
            {
                dr.CuoShi = Measure.GetMeasure(long.Parse(dr["CeDianID"] + ""), startTime, endTime);

            }
            return dt;

            //long[] cedianIDs = CeDian.GetAllids();

            //max_cmssDataSet.DKuiDianDataTable resultTable = new max_cmssDataSet.DKuiDianDataTable();

            //if (doKaiGuanLiangSelect(startTime, endTime, ref dt1, ref dt2, cedians))
            //    dt = doGetDKuiDianReportData(dt1, dt2);

            //foreach (string s in cedians)
            //{
            //    max_cmssDataSet.DKuiDianRow row = dt.FindByCeDianBianHao(s);
            //    if (row != null && row.LeiJiCiShu > 0)
            //    {
            //        resultTable.ImportRow(row);
            //        row.CuoShi = Measure.GetMeasure(s, startTime, endTime);
            //    }
            //    else
            //    {
            //        // addDKuiDianReportRow(ref resultTable, dt2, s, 0, "", "");
            //    }
            //}
            //return resultTable;
        }
        private static max_cmssDataSet.DKuiDianDataTable doGetDKuiDianReportData(DataTable value, DataTable cedian)
        {
            max_cmssDataSet.DKuiDianDataTable dt = new max_cmssDataSet.DKuiDianDataTable();

            bool kuiDianYiChang = false;
            bool firstKuiDianYiChang = true;
            DateTime kuiDianStartTime = DateTime.Now;
            DateTime kuiDianEndTime = DateTime.Now;
            TimeSpan duraTime = TimeSpan.Zero;
            string startAndEnd = string.Empty;
            string timePerKuiDian = string.Empty;
            TimeSpan leiJiKuiDian = TimeSpan.Zero;
            int kuiDianYiChangCiShu = 0;

            bool firstother = true;

            string ceDianBianHao = value.Rows[0]["ceDianBianHao"].ToString().Trim();
            long cedianId = (long)value.Rows[0]["cedianID"];
            int tempId = 999999;

            foreach (DataRow row in value.Rows)
            {
            newcedian:
                if (ceDianBianHao == row["ceDianBianHao"].ToString().Trim())
                {
                    int kuiDian = Convert.ToInt32(row["kuiDianYiChang"]);
                    if (kuiDian == 1)
                    {
                        if (firstKuiDianYiChang)
                        {
                            firstKuiDianYiChang = false;
                            firstother = true;
                            kuiDianYiChang = true;
                            kuiDianYiChangCiShu++;
                            kuiDianStartTime = (DateTime)row["uploadTime"];
                        }
                    }
                    else
                    {
                        if (firstother)
                        {
                            firstKuiDianYiChang = true;
                            firstother = false;
                            if (kuiDianYiChang)
                            {
                                kuiDianYiChang = false;
                                kuiDianEndTime = (DateTime)row["uploadTime"];
                                duraTime = kuiDianEndTime - kuiDianStartTime;
                                startAndEnd += GlobalParams.TimeSpanString(duraTime) + "," + kuiDianStartTime.ToString(dateString) + "," + kuiDianEndTime.ToString(dateString) + "\n";
                                leiJiKuiDian += duraTime;
                                addDKuiDianReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, -1, GlobalParams.TimeSpanString(duraTime), GlobalParams.TimeSpanString(duraTime) + "," + kuiDianStartTime.ToString(dateString) + "," + kuiDianEndTime.ToString(dateString));
                            }
                        }
                    }
                }
                else
                {
                    addDKuiDianReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, kuiDianYiChangCiShu, GlobalParams.TimeSpanString(leiJiKuiDian), "");

                    startAndEnd = timePerKuiDian = string.Empty;
                    duraTime = leiJiKuiDian = TimeSpan.Zero;
                    kuiDianYiChangCiShu = 0;
                    firstKuiDianYiChang = firstother = false;
                    kuiDianYiChang = true;
                    ceDianBianHao = row["ceDianBianHao"].ToString().Trim();
                    cedianId = (long)row["cedianID"];
                    goto newcedian;
                }
            }

            addDKuiDianReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, kuiDianYiChangCiShu, GlobalParams.TimeSpanString(leiJiKuiDian), "");

            return dt;
        }

        //往开关量馈电报表中添加一行数据
        private static void addDKuiDianReportRow(ref max_cmssDataSet.DKuiDianDataTable dt, DataTable ceDian, string ceDianBianHao, long cedianId, long tempid, int kuiDianYiChangCiShu, string leijikuidian,
            string startAndEnd)
        {
            max_cmssDataSet.DKuiDianRow resultRow = (max_cmssDataSet.DKuiDianRow)dt.NewRow();
            resultRow.CeDianBianHao = ceDianBianHao;
            foreach (DataRow row in ceDian.Rows)
            {
                string bianHao = row["ceDianBianHao"].ToString().Trim();
                long cdid = (long)row["id"];

                if (cdid == cedianId)
                {
                    resultRow.DiDian = bianHao + "/" + row["ceDianWeiZhi"] + "/" + row["xiaoLeiXing"];
                    break;
                }
            }

            resultRow.LeiJiCiShu = kuiDianYiChangCiShu;
            resultRow.LeiJiShiJian = leijikuidian;
            resultRow.KuiDianPetTime = startAndEnd;
            resultRow.DuanDianQuYu = GlobalParams.AllCeDianList.GetDuanDianQuYu(ceDianBianHao);
            resultRow.CeDianID = tempid + "";
            dt.Rows.Add(resultRow);
        }
        #endregion//开关量馈电异常日报表

        #region//开关量状态变动日报表
        /// <summary>
        /// 打印：开关量状态变动日报表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static max_cmssDataSet.DStateDataTable GetDStateData(DateTime date)
        {
            DateTime startTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime endTime = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            string[] ceDianPaiXu = ReportConfig.getConfigByName((int)ReportType.KaiGuanLiangState).GetCeDianBianHao();

            return QueryDStateData(startTime, endTime, ceDianPaiXu);
        }
        public static max_cmssDataSet.DStateDataTable QueryDStateData(DateTime startTime, DateTime endTime, string[] cedians)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            max_cmssDataSet.DStateDataTable dt = new max_cmssDataSet.DStateDataTable();

            max_cmssDataSet.DStateDataTable resultTable = new max_cmssDataSet.DStateDataTable();

            if (doKaiGuanLiangSelect(startTime, endTime, ref dt1, ref dt2, cedians))
                dt = doGetDStateReportData(dt1, dt2);

            foreach (string s in cedians)
            {
                max_cmssDataSet.DStateRow row = dt.FindByCeDianBianHao(s);
                if (row != null)
                {
                    resultTable.ImportRow(row);
                }
                else
                {
                    //addDStateReportRow(ref resultTable, dt2, s, 0, "", "");
                }
            }
            return resultTable;
        }
        private static max_cmssDataSet.DStateDataTable doGetDStateReportData(DataTable value, DataTable cedian)
        {
            max_cmssDataSet.DStateDataTable dt = new max_cmssDataSet.DStateDataTable();

            int currentState;
            DateTime currentStateStartTime;
            DateTime currentStateEndTime;
            string stateName = string.Empty;
            string stateChange = string.Empty;

            int leiJiCiShu = 0;

            DateTime startTime;
            TimeSpan runTime;

            DataRow lastRow = value.Rows[0];

            string ceDianBianHao = value.Rows[0]["ceDianBianHao"].ToString().Trim();
            currentStateStartTime = (DateTime)value.Rows[0]["uploadTime"];
            startTime = currentStateStartTime;
            currentState = Convert.ToInt32(value.Rows[0]["state"]);

            foreach (DataRow row in value.Rows)
            {
            newcedian:
                if (ceDianBianHao == row["ceDianBianHao"].ToString().Trim())
                {
                    int state = Convert.ToInt32(row["uploadValue"]);
                    if (state != currentState)
                    {
                        leiJiCiShu++;
                        currentStateEndTime = (DateTime)row["uploadTime"];
                        addStateChange(cedian, ceDianBianHao, ref stateChange, currentState, state, currentStateEndTime);
                        currentState = state;
                    }
                    lastRow = row;
                }
                else
                {
                    //addStateChange(cedian, ceDianBianHao, ref stateChange, currentStateStartTime, (DateTime)lastRow["uploadTime"], currentState);
                    runTime = (DateTime)lastRow["uploadTime"] - startTime;
                    addDStateReportRow(ref dt, cedian, ceDianBianHao, leiJiCiShu, GlobalParams.TimeSpanString(runTime), stateChange);

                    ceDianBianHao = row["ceDianBianHao"].ToString().Trim();
                    currentState = Convert.ToInt32(row["state"]);
                    startTime = (DateTime)row["uploadTime"];
                    currentStateStartTime = startTime;
                    stateChange = string.Empty;
                    leiJiCiShu = 0;

                    goto newcedian;
                }
            }
            //addStateChange(cedian, ceDianBianHao, ref stateChange, currentStateStartTime, (DateTime)lastRow["uploadTime"], currentState);
            runTime = (DateTime)lastRow["uploadTime"] - startTime;
            addDStateReportRow(ref dt, cedian, ceDianBianHao, leiJiCiShu, GlobalParams.TimeSpanString(runTime), stateChange);

            return dt;
        }
        private static void addStateChange(DataTable cedian, string cedianbianhao, ref string stateChange, int preState, int currentState, DateTime changeTime)
        {
            DataRow row = cedian.NewRow();
            foreach (DataRow r in cedian.Rows)
            {
                if (r["ceDianBianHao"].ToString().Trim() == cedianbianhao)
                {
                    row = r;
                    break;
                }
            }

            string preStateName = getStateName(preState, row);
            string currentStateName = getStateName(currentState, row);

            if (stateChange != string.Empty)
                stateChange += "; ";
            stateChange += preStateName + "->" + currentStateName + ", " + changeTime.ToString("hh:mm:ss");
        }
        private static string getStateName(int state, DataRow row)
        {
            string stateName = string.Empty;
            if (state == 0)
                stateName = row["lingTaiMingCheng"].ToString();
            else if (state == 1)
                stateName = row["yiTaiMingCheng"].ToString();
            else if (state == 2)
                stateName = row["erTaiMingCheng"].ToString();

            return stateName;
        }
        private static void addDStateReportRow(ref max_cmssDataSet.DStateDataTable dt, DataTable ceDian, string ceDianBianHao, int leiJiCiShu, string leiJiShiJian,
            string stateChange)
        {
            max_cmssDataSet.DStateRow resultRow = (max_cmssDataSet.DStateRow)dt.NewRow();
            resultRow.CeDianBianHao = ceDianBianHao;
            foreach (DataRow row in ceDian.Rows)
            {
                if (ceDianBianHao == row["ceDianBianHao"].ToString().Trim())
                {
                    resultRow.DiDian = ceDianBianHao + "/" + row["ceDianWeiZhi"] + "/" + row["xiaoLeiXing"];
                    break;
                }
            }

            //if(Convert.DBNull == resultRow.DiDian)
            //    resultRow.DiDian = ceDianBianHao;
            resultRow.LeiJiBianDongCiShu = leiJiCiShu;
            resultRow.LeiJiShiJian = leiJiShiJian;
            resultRow.ZhuangTaiBianDong = stateChange;

            dt.Rows.Add(resultRow);
        }
        #endregion//开关量状态变动日报表

        #region//监控设备故障日报表
        /// <summary>
        /// 打印：监控设备故障日报表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static max_cmssDataSet.SheBeiGuZhangDataTable GetSheBeiGuZhangData(DateTime date)
        {
            DateTime startTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime endTime = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            string[] ceDianPaiXu = ReportConfig.getConfigByName((int)ReportType.JianKongSheBeiGuZhang).GetCeDianBianHao();

            dateString = "HH:mm:ss";
            max_cmssDataSet.SheBeiGuZhangDataTable dt = QuerySheBeiGuZhangData(startTime, endTime, ceDianPaiXu);
            dateString = "yyyy-MM-dd HH:mm:ss";
            return dt;
        }
        public static max_cmssDataSet.SheBeiGuZhangDataTable QuerySheBeiGuZhangData(DateTime startTime, DateTime endTime, string[] cedians)
        {
            DataTable dt1 = new DataTable();//模拟量数据
            DataTable dt2 = new DataTable();//开关量数据
            DataTable dt3 = new DataTable();//模拟量类型信息
            DataTable dt4 = new DataTable();//开关量类型信息
            max_cmssDataSet.SheBeiGuZhangDataTable moNiLiang = new max_cmssDataSet.SheBeiGuZhangDataTable();
            max_cmssDataSet.SheBeiGuZhangDataTable kaiGuanLiang = new max_cmssDataSet.SheBeiGuZhangDataTable();
            max_cmssDataSet.SheBeiGuZhangDataTable resultTable = new max_cmssDataSet.SheBeiGuZhangDataTable();

            if (doMoNiLiangSelect(startTime, endTime, ref dt1, ref dt3, cedians))//得到模拟量的设备故障数据
            {
                moNiLiang = doGetSheBeiGuZhangDataMoNiLiang(dt1, dt3);
            }
            if (doKaiGuanLiangSelect(startTime, endTime, ref dt2, ref dt4, cedians))//得到开关量的设备故障数据
            {
                kaiGuanLiang = doGetSheBeiGuZhangDataKaiGuanLiang(dt2, dt4);
            }
            moNiLiang.Merge(kaiGuanLiang);

            foreach (max_cmssDataSet.SheBeiGuZhangRow dr in moNiLiang.Rows)
            {
                dr.AnQuanCuoShi = Measure.GetMeasure(long.Parse(dr["CeDianID"] + ""), startTime, endTime);

            }
            return moNiLiang;

            //foreach (string s in cedians)
            //{
            //    max_cmssDataSet.SheBeiGuZhangRow row = moNiLiang.FindByCeDianBianHao(s);
            //    if (row != null)
            //    {
            //        resultTable.ImportRow(row);
            //        row.AnQuanCuoShi = Measure.GetMeasure(s, startTime, endTime);
            //    }
            //    else
            //    {
            //        //addSheBeiGuZhangReportRow(ref resultTable, dt2, s, 0, "", "");
            //    }
            //}
            //return resultTable;
        }

        private static max_cmssDataSet.SheBeiGuZhangDataTable doGetSheBeiGuZhangDataMoNiLiang(DataTable value, DataTable cedian)
        {
            max_cmssDataSet.SheBeiGuZhangDataTable dt = new max_cmssDataSet.SheBeiGuZhangDataTable();

            bool guZhang = false;
            bool firstGuZhang = true;
            DateTime guZhangStartTime = DateTime.Now;
            DateTime guZhangEndTime = DateTime.Now;
            TimeSpan duraTime = TimeSpan.Zero;
            string startAndEnd = string.Empty;
            string timePerGuZhang = string.Empty;
            TimeSpan leiJiGuZhang = TimeSpan.Zero;
            int guZhangCiShu = 0;

            bool firstother = true;

            string ceDianBianHao = value.Rows[0]["ceDianBianHao"].ToString().Trim();
            long cedianId = (long)value.Rows[0]["cedianID"];
            int tempId = 999999;


            foreach (DataRow row in value.Rows)
            {
            newcedian:
                if (cedianId == (long)row["cedianID"])
                {
                    MoNiLiangState currentState = (MoNiLiangState)Convert.ToInt32(row["state"]);
                    if (isGuZhangState(currentState, ceDianBianHao))
                    {
                        if (firstGuZhang)
                        {
                            firstGuZhang = false;
                            firstother = true;
                            guZhang = true;
                            guZhangCiShu++;
                            guZhangStartTime = (DateTime)row["uploadTime"];
                        }
                    }
                    else
                    {
                        if (firstother)
                        {
                            firstGuZhang = true;
                            firstother = false;
                            if (guZhang)
                            {
                                guZhang = false;
                                guZhangEndTime = (DateTime)row["uploadTime"];
                                duraTime = guZhangEndTime - guZhangStartTime;
                                startAndEnd += GlobalParams.TimeSpanString(duraTime) + ", " + guZhangStartTime.ToString(dateString) + ", " + guZhangEndTime.ToString(dateString) + "\n";
                                leiJiGuZhang += duraTime;


                                addSheBeiGuZhangReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, -1, GlobalParams.TimeSpanString(duraTime), GlobalParams.TimeSpanString(duraTime) + ", " + guZhangStartTime.ToString(dateString) + ", " + guZhangEndTime.ToString(dateString));
                            }
                        }
                    }
                }
                else
                {
                    if (guZhang)
                    {
                        if (guZhangStartTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            guZhangEndTime = new DateTime(guZhangStartTime.Year, guZhangStartTime.Month, guZhangStartTime.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                        }
                        else
                        {
                            guZhangEndTime = new DateTime(guZhangStartTime.Year, guZhangStartTime.Month, guZhangStartTime.Day, 23, 59, 59);
                        }
                        duraTime = guZhangEndTime - guZhangStartTime;
                        startAndEnd += GlobalParams.TimeSpanString(duraTime) + ", " + guZhangStartTime.ToString(dateString) + ", " + guZhangEndTime.ToString(dateString) + "\n";
                        leiJiGuZhang += duraTime;
                    }
                    if (guZhangCiShu > 0)
                    {
                        addSheBeiGuZhangReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, guZhangCiShu, GlobalParams.TimeSpanString(leiJiGuZhang), "");
                    }
                    startAndEnd = timePerGuZhang = string.Empty;
                    duraTime = leiJiGuZhang = TimeSpan.Zero;
                    guZhangCiShu = 0;
                    ceDianBianHao = row["ceDianBianHao"].ToString().Trim();
                    cedianId = (long)row["cedianID"];
                    firstGuZhang = firstother = true;
                    guZhang = false;

                    goto newcedian;
                }
            }
            if (guZhangCiShu > 0)
            {
                addSheBeiGuZhangReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, guZhangCiShu, GlobalParams.TimeSpanString(leiJiGuZhang), "");
            }
            return dt;
        }
        private static bool isGuZhangState(MoNiLiangState state, string ceDianBianHao)
        {
            if (ceDianBianHao[2] == 'A')
            {
                if (state == MoNiLiangState.DuanXian || state == MoNiLiangState.FuPiao || state == MoNiLiangState.GuZhang || state == MoNiLiangState.YiChu)
                    return true;
                else
                    return false;
            }
            else
            {
                if (state == MoNiLiangState.DuanDian)
                    return true;
                else
                    return false;
            }
        }
        private static max_cmssDataSet.SheBeiGuZhangDataTable doGetSheBeiGuZhangDataKaiGuanLiang(DataTable value, DataTable cedian)
        {
            max_cmssDataSet.SheBeiGuZhangDataTable dt = new max_cmssDataSet.SheBeiGuZhangDataTable();

            bool guZhang = false;
            bool firstGuZhang = true;
            DateTime guZhangStartTime = DateTime.Now;
            DateTime guZhangEndTime = DateTime.Now;
            TimeSpan duraTime = TimeSpan.Zero;
            string startAndEnd = string.Empty;
            string timePerGuZhang = string.Empty;
            TimeSpan leiJiGuZhang = TimeSpan.Zero;
            int guZhangCiShu = 0;

            bool firstother = true;

            string ceDianBianHao = value.Rows[0]["ceDianBianHao"].ToString().Trim();

            long cedianId = (long)value.Rows[0]["cedianID"];
            int tempId = 999999;
            //currentValueStartTime = (DateTime)value.Rows[0]["uploadTime"];
            foreach (DataRow row in value.Rows)
            {
            newcedian:
                if (cedianId == (long)row["cedianID"])
                {
                    int state = Convert.ToInt32(row["uploadValue"]);
                    if (state == 2)//开关量的故障态为2
                    {
                        if (firstGuZhang)
                        {
                            firstGuZhang = false;
                            firstother = true;
                            guZhang = true;
                            guZhangCiShu++;
                            guZhangStartTime = (DateTime)row["uploadTime"];
                        }
                    }
                    else
                    {
                        if (firstother)
                        {
                            firstGuZhang = true;
                            firstother = false;
                            if (guZhang)
                            {
                                guZhang = false;
                                guZhangEndTime = (DateTime)row["uploadTime"];
                                duraTime = guZhangEndTime - guZhangStartTime;
                                startAndEnd += GlobalParams.TimeSpanString(duraTime) + ", " + guZhangStartTime.ToString(dateString) + ", " + guZhangEndTime.ToString(dateString) + "\n";
                                leiJiGuZhang += duraTime;

                                addSheBeiGuZhangReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, -1, GlobalParams.TimeSpanString(duraTime), GlobalParams.TimeSpanString(duraTime) + ", " + guZhangStartTime.ToString(dateString) + ", " + guZhangEndTime.ToString(dateString));
                            }
                        }
                    }
                }
                else
                {
                    if (guZhang)
                    {
                        if (guZhangStartTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            guZhangEndTime = new DateTime(guZhangStartTime.Year, guZhangStartTime.Month, guZhangStartTime.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                        }
                        else
                        {
                            guZhangEndTime = new DateTime(guZhangStartTime.Year, guZhangStartTime.Month, guZhangStartTime.Day, 23, 59, 59);
                        }
                        duraTime = guZhangEndTime - guZhangStartTime;
                        startAndEnd += GlobalParams.TimeSpanString(duraTime) + ", " + guZhangStartTime.ToString(dateString) + ", " + guZhangEndTime.ToString(dateString) + "\n";
                        leiJiGuZhang += duraTime;
                    }
                    if (guZhangCiShu > 0)
                    {
                        addSheBeiGuZhangReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, guZhangCiShu, GlobalParams.TimeSpanString(leiJiGuZhang), "");
                    }
                    startAndEnd = timePerGuZhang = string.Empty;
                    duraTime = leiJiGuZhang = TimeSpan.Zero;
                    guZhangCiShu = 0;
                    guZhang = false;
                    firstGuZhang = firstother = true;
                    ceDianBianHao = row["ceDianBianHao"].ToString().Trim();
                    cedianId = (long)row["cedianID"];
                    goto newcedian;
                }
            }
            if (guZhangCiShu > 0)
            {
                addSheBeiGuZhangReportRow(ref dt, cedian, ceDianBianHao, cedianId, ++tempId, guZhangCiShu, GlobalParams.TimeSpanString(leiJiGuZhang), "");
            }
            return dt;
        }

        //往设备故障报表中添加一行数据
        private static void addSheBeiGuZhangReportRow(ref max_cmssDataSet.SheBeiGuZhangDataTable dt, DataTable ceDian, string ceDianBianHao, long cedianId, long tempid, int kuiDianYiChangCiShu, string leijikuidian,
            string startAndEnd)
        {
            max_cmssDataSet.SheBeiGuZhangRow resultRow = (max_cmssDataSet.SheBeiGuZhangRow)dt.NewRow();
            resultRow.CeDianBianHao = ceDianBianHao;
            bool bd = false;
            foreach (DataRow row in ceDian.Rows)
            {

                string bianHao = row["ceDianBianHao"].ToString().Trim();
                long cdid = (long)row["id"];

                if (cdid == cedianId)
                {
                    resultRow.DiDian = bianHao + "/" + row["ceDianWeiZhi"] + "/" + row["xiaoLeiXing"];
                    bd = true;
                    break;
                }
            }

            if (!bd)
            {
                return;
            }
            if (kuiDianYiChangCiShu > 0)
            {
                resultRow.LeiJiCiShu = kuiDianYiChangCiShu;
            }
            resultRow.LeiJiShiJian = leijikuidian;
            resultRow.ShiJianPerTime = startAndEnd;
            resultRow.CeDianID = tempid + "";


            dt.Rows.Add(resultRow);
        }
        #endregion

        #region//模拟量统计值报表
        public static max_cmssDataSet.ATongJiZhiDataTable GetATongJiZhiData(DateTime date, int minites)
        {
            DateTime start = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime end = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            string[] ceDianPaiXu = ReportConfig.getConfigByName((int)ReportType.MoNiLiangTongJi).GetCeDianBianHao();

            return QueryATongJiZhiData(start, end, ceDianPaiXu, minites);

        }
        public static max_cmssDataSet.ATongJiZhiDataTable QueryATongJiZhiData(DateTime startTime, DateTime endTime, string[] cedians, int minites)
        {
            max_cmssDataSet.ATongJiZhiDataTable result = new max_cmssDataSet.ATongJiZhiDataTable();
            float max = 0;
            DateTime maxTime = endTime;
            float sumPerInterval = 0;
            float sum = 0;
            float maxPerInterval = 0;
            string maxAndAvgPerInterval = string.Empty;
            TimeSpan span = new TimeSpan(0, minites, 0);
            string shiJianJianGe = span.ToString() + "/" + startTime.ToString("HH:mm:ss") + "~" + startTime.AddMinutes(minites).ToString("HH:mm:ss") + " "
                + endTime.AddMinutes(0 - minites).ToString("HH:mm:ss") + "~" + endTime.ToString("HH:mm:ss");

            int point = minites * 60 / 5;
            int count = 0;

            foreach (string cedian in cedians)
            {
                HistoryData[] rawData = CeDian.GetAllValues(startTime, endTime, cedian);
                if (rawData != null)
                {
                    count = 0;
                    int len = (rawData.Length - 1) / point + 1;
                    foreach (HistoryData data in rawData)
                    {
                        if (data.value > max)
                        {
                            max = data.value;
                            maxTime = data.time;
                        }

                        if (data.value > maxPerInterval) maxPerInterval = data.value;
                        sumPerInterval += data.value;

                        if (++count >= point)
                        {
                            if (maxAndAvgPerInterval != string.Empty)
                                maxAndAvgPerInterval += "/";
                            maxAndAvgPerInterval += Math.Round(maxPerInterval, 2) + "," + Math.Round(sumPerInterval / point, 2);
                            sum += sumPerInterval;
                            sumPerInterval = maxPerInterval = 0;

                            count = 0;
                        }
                    }

                    max_cmssDataSet.ATongJiZhiRow row = (max_cmssDataSet.ATongJiZhiRow)result.NewRow();
                    CeDian cd = new CeDian(cedian);
                    row.ShiJianJianGe = shiJianJianGe;
                    row.CeDianBianHao = cedian;
                    row.DiDian = cedian + "/" + cd.CeDianWeiZhi + "/" + cd.XiaoleiXing;
                    row.MaxValue = Math.Round(max, 2) + "/" + maxTime.ToString() + "/" + Math.Round(sum / rawData.Length, 2);
                    row.AvgPerTime = maxAndAvgPerInterval;
                    if (cd.MoNiLiang != null)
                    {
                        row.DanWei = cd.MoNiLiang.DanWei;
                        row.BaoJingNongDu = Math.Round(cd.MoNiLiang.BaoJingZhiShangXian, 2);
                        row.DuanDianNongDu = Math.Round(cd.MoNiLiang.DuanDianZhi, 2);
                        row.FuDianNongDu = Math.Round(cd.MoNiLiang.FuDianZhi, 2);
                    }
                    result.Rows.Add(row);
                }

                max = sum = 0;
                maxAndAvgPerInterval = string.Empty;
            }

            return result;
        }
        #endregion//模拟量统计值报表

        #region//模拟量报警断电月报表
        public static max_cmssDataSet.BaoJingDuanDianDataTable GetBaoJingDuanDianData(DateTime month)
        {
            DateTime start = new DateTime(month.Year, month.Month, 1, 0, 0, 0);
            DateTime end = new DateTime(month.Year, month.Month, getLastDayOfMonth(month.Year, month.Month), 23, 59, 59);
            string[] ceDianPaiXu = ReportConfig.getConfigByName((int)ReportType.YueBaoBiao).GetCeDianBianHao();

            long[] cedianIDs = CeDian.GetAllids();

            dateString = "yyyy-MM-dd HH:mm:ss";
            max_cmssDataSet.BaoJingDuanDianDataTable dt = QueryBaoJingDuanDianData(start, end, ceDianPaiXu, cedianIDs);



            return dt;
        }

        public static max_cmssDataSet.BaoJingDuanDianDataTable QueryBaoJingDuanDianData(DateTime startTime, DateTime endTime, string[] cedians, long[] cedianIDs)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            if (cedianIDs != null && cedianIDs.Length > 0)
            {
                if (!doMoNiLiangSelect_AllCeDianID(startTime, endTime, ref dt1, ref dt2, cedianIDs))
                {
                    return new max_cmssDataSet.BaoJingDuanDianDataTable();
                }

            }
            else if (!doMoNiLiangSelect(startTime, endTime, ref dt1, ref dt2, cedians))//所查找的数据为空
                return new max_cmssDataSet.BaoJingDuanDianDataTable();

            int count = dt2.Rows.Count;

            if (dt1 == null || dt1.Rows.Count == 0)//dt2.Rows.Count == 0?
            {
                return new max_cmssDataSet.BaoJingDuanDianDataTable();
            }
            else
            {
                max_cmssDataSet.BaoJingDuanDianDataTable dt = ReportDataSuply.doGetBaoJingDuanDianData(dt1, ref dt2);
                //max_cmssDataSet.BaoJingDuanDianDataTable resultTable = new max_cmssDataSet.BaoJingDuanDianDataTable();

                foreach (max_cmssDataSet.BaoJingDuanDianRow dr in dt.Rows)
                {
                    dr.CuoShi = Measure.GetMeasure(long.Parse(dr["CeDianID"] + ""), startTime, endTime);

                }
                return dt;
                //foreach (long id in cedianIDs)
                //{

                //    max_cmssDataSet.BaoJingDuanDianRow row = dt.FindByCeDianID(id+"");
                //    if (row != null)
                //    {
                //        resultTable.ImportRow(row);
                //        row.CuoShi = Measure.GetMeasure(id, startTime, endTime);
                //    }
                //    else
                //    {
                //        //addBaoJingDuanDianRow(ref resultTable, ref dt2, s, 0, "", "", 0, "", "", "", "", 0, "", "");
                //    }
                //}
                //return resultTable;
            }
        }



        //private static max_cmssDataSet.BaoJingDuanDianDataTable doGetBaoJingDuanDianData(DataTable value, ref DataTable dt2)
        //{
        //    max_cmssDataSet.BaoJingDuanDianDataTable result = new max_cmssDataSet.BaoJingDuanDianDataTable();

        //    int baoJingCiShu = 0;
        //    int duanDianCiShu = 0;
        //    TimeSpan baoJingSpan = TimeSpan.Zero;
        //    string baoJingQiZhi = string.Empty;
        //    TimeSpan duanDianSpan = TimeSpan.Zero;
        //    string duanDianQiZhi = string.Empty;
        //    string avgAndMaxBaoJing = string.Empty;
        //    string avgAndMaxDuanDian = string.Empty;

        //    float maxBaoJing = 0;
        //    DateTime maxBaoJingTime = DateTime.Now;
        //    float avgBaoJing = 0;

        //    float maxDuanDian = 0;
        //    DateTime maxDuanDianTime = DateTime.Now;
        //    float avgDuanDian = 0;

        //    float preValue = 0;
        //    float currentValue = 0;
        //    int preState = -1;
        //    int curState = -1;
        //    DateTime currentTime;
        //    DateTime currentValueStartTime = (DateTime)value.Rows[0]["uploadTime"];
        //    TimeSpan currentValueSpan = TimeSpan.Zero;
        //    DateTime currentStateStartTime = (DateTime)value.Rows[0]["uploadTime"];
        //    TimeSpan currentStateSpan = TimeSpan.Zero;

        //    int preKuiDian = -1;
        //    int curKuiDian = -1;
        //    int kuiDianYiChangCiShu = 0;
        //    TimeSpan leiJiKuiDian = TimeSpan.Zero;
        //    TimeSpan curKuiDianSpan = TimeSpan.Zero;
        //    string kuiDianStartAndEnd = string.Empty;
        //    DateTime curKuiDianStartTime = DateTime.Now;

        //    string cedianbianhao = value.Rows[0]["ceDianBianHao"].ToString().Trim();
        //    long cedianId = (long)value.Rows[0]["cedianID"];
        //    currentValueStartTime = (DateTime)value.Rows[0]["uploadTime"];
        //    foreach (DataRow row in value.Rows)
        //    {
        //    newcedian:
        //        if (cedianId == (long)row["cedianID"])
        //        {
        //            curState = Convert.ToInt32(row["state"]);
        //            currentTime = (DateTime)row["uploadTime"];
        //            currentValue = Convert.ToSingle(row["uploadValue"]);
        //            curKuiDian = Convert.ToInt32(row["kuiDianYiChang"]);

        //            //值变化
        //            if (currentValue != preValue)
        //            {
        //                float sum = preValue * (float)(currentTime - currentValueStartTime).TotalSeconds;
        //                if (preState == (int)MoNiLiangState.BaoJing)
        //                    avgBaoJing += sum;
        //                if (preState == (int)MoNiLiangState.DuanDian)
        //                    avgDuanDian += sum;

        //                preValue = currentValue;
        //                currentValueStartTime = currentTime;
        //            }

        //            //状态变化
        //            if (preState == (int)MoNiLiangState.BaoJing && currentValue > maxBaoJing)
        //            {
        //                maxBaoJing = currentValue;
        //                maxBaoJingTime = currentTime;
        //            }

        //            if (preState == (int)MoNiLiangState.DuanDian && currentValue > maxDuanDian)
        //            {
        //                maxDuanDian = currentValue;
        //                maxDuanDianTime = currentTime;
        //            }

        //            if (curState != preState)
        //            {
        //                doStateChange(preState, ref baoJingCiShu, ref baoJingSpan, ref baoJingQiZhi, ref duanDianCiShu, ref duanDianSpan, ref duanDianQiZhi, currentTime, currentStateStartTime);
        //                preState = curState;
        //                currentStateStartTime = currentTime;
        //            }

        //            //馈电变化
        //            if (curKuiDian != preKuiDian)
        //            {
        //                if (preKuiDian == 0)
        //                {
        //                    kuiDianYiChangCiShu++;
        //                    curKuiDianSpan = currentTime - curKuiDianStartTime;
        //                    leiJiKuiDian += curKuiDianSpan;
        //                    kuiDianStartAndEnd += GlobalParams.TimeSpanString(curKuiDianSpan) + "," + curKuiDianStartTime.ToString(dateString) + "," + currentTime.ToString(dateString) + "\n";
        //                }

        //                preKuiDian = curKuiDian;
        //                curKuiDianStartTime = currentTime;
        //            }
        //        }
        //        else
        //        {

        //            currentTime = new DateTime(currentStateStartTime.Year, currentStateStartTime.Month, currentStateStartTime.Day, 23, 59, 59);
        //            doStateChange(preState, ref baoJingCiShu, ref baoJingSpan, ref baoJingQiZhi, ref duanDianCiShu, ref duanDianSpan, ref duanDianQiZhi, currentTime, currentStateStartTime);
        //            if (currentValue != preValue)
        //            {
        //                float sum = preValue * (float)(currentTime - currentValueStartTime).TotalSeconds;
        //                if (preState == (int)MoNiLiangState.BaoJing)
        //                    avgBaoJing += sum;
        //                if (preState == (int)MoNiLiangState.DuanDian)
        //                    avgDuanDian += sum;

        //                preValue = currentValue;
        //                currentValueStartTime = currentTime;
        //            }
        //            if (curKuiDian == 0)
        //            {
        //                kuiDianYiChangCiShu++;
        //                curKuiDianSpan = currentTime - curKuiDianStartTime;
        //                leiJiKuiDian += curKuiDianSpan;
        //                kuiDianStartAndEnd += curKuiDianStartTime.ToString(dateString) + "," + currentTime.ToString(dateString) + "\n";
        //            }

        //            if (baoJingCiShu != 0)
        //                avgAndMaxBaoJing = Math.Round(avgBaoJing / baoJingSpan.TotalSeconds, 2) + "," + maxBaoJing + "," + maxBaoJingTime.ToString(dateString);
        //            if (duanDianCiShu != 0)
        //                avgAndMaxDuanDian = Math.Round(avgDuanDian / duanDianSpan.TotalSeconds, 2) + "," + maxDuanDian + "," + maxDuanDianTime.ToString(dateString);
        //            addBaoJingDuanDianRow(ref result, ref dt2, cedianId,cedianId, cedianbianhao, baoJingCiShu, baoJingQiZhi, GlobalParams.TimeSpanString(baoJingSpan), duanDianCiShu,
        //                duanDianQiZhi, GlobalParams.TimeSpanString(duanDianSpan), avgAndMaxBaoJing, avgAndMaxDuanDian, kuiDianYiChangCiShu, GlobalParams.TimeSpanString(leiJiKuiDian), kuiDianStartAndEnd);

        //            //重新初始化
        //            preState = curState = preKuiDian = curKuiDian = -1;
        //            baoJingCiShu = duanDianCiShu = kuiDianYiChangCiShu = 0;
        //            maxBaoJing = avgBaoJing = maxDuanDian = avgDuanDian = 0;
        //            baoJingQiZhi = duanDianQiZhi = avgAndMaxBaoJing = avgAndMaxDuanDian = kuiDianStartAndEnd = string.Empty;
        //            baoJingSpan = duanDianSpan = currentStateSpan = leiJiKuiDian = TimeSpan.Zero;
        //            currentStateStartTime = DateTime.Now;
        //            cedianbianhao = row["ceDianBianHao"].ToString().Trim();
        //            cedianId = (long)row["cedianID"];

        //            goto newcedian;
        //        }
        //    }

        //    currentTime = new DateTime(currentStateStartTime.Year, currentStateStartTime.Month, currentStateStartTime.Day, 23, 59, 59);
        //    doStateChange(preState, ref baoJingCiShu, ref baoJingSpan, ref baoJingQiZhi, ref duanDianCiShu, ref duanDianSpan, ref duanDianQiZhi, currentTime, currentStateStartTime);

        //    if (baoJingCiShu != 0)
        //        avgAndMaxBaoJing = Math.Round(avgBaoJing / baoJingSpan.TotalSeconds, 2) + "," + maxBaoJing + "," + maxBaoJingTime.ToString(dateString);
        //    if (duanDianCiShu != 0)
        //        avgAndMaxDuanDian = Math.Round(avgDuanDian / duanDianSpan.TotalSeconds, 2) + "," + maxDuanDian + "," + maxDuanDianTime.ToString(dateString);
        //    addBaoJingDuanDianRow(ref result, ref dt2, cedianId,cedianId, cedianbianhao, baoJingCiShu, baoJingQiZhi, GlobalParams.TimeSpanString(baoJingSpan), duanDianCiShu,
        //        duanDianQiZhi, GlobalParams.TimeSpanString(duanDianSpan), avgAndMaxBaoJing, avgAndMaxDuanDian, kuiDianYiChangCiShu, GlobalParams.TimeSpanString(leiJiKuiDian), kuiDianStartAndEnd);


        //    return result;
        //}


        private static max_cmssDataSet.BaoJingDuanDianDataTable doGetBaoJingDuanDianData(DataTable value, ref DataTable dt2)
        {
            max_cmssDataSet.BaoJingDuanDianDataTable result = new max_cmssDataSet.BaoJingDuanDianDataTable();

            int baoJingCiShu = 0;
            int duanDianCiShu = 0;
            TimeSpan baoJingSpan = TimeSpan.Zero;
            string baoJingQiZhi = string.Empty;
            TimeSpan duanDianSpan = TimeSpan.Zero;
            string duanDianQiZhi = string.Empty;
            string avgAndMaxBaoJing = string.Empty;
            string avgAndMaxDuanDian = string.Empty;

            float maxBaoJing = 0;
            DateTime maxBaoJingTime = DateTime.Now;
            float avgBaoJing = 0;

            float maxDuanDian = 0;
            DateTime maxDuanDianTime = DateTime.Now;
            float avgDuanDian = 0;

            float preValue = 0;
            float currentValue = 0;
            int preState = -1;
            int curState = -1;
            DateTime currentTime;
            DateTime currentValueStartTime = (DateTime)value.Rows[0]["uploadTime"];
            TimeSpan currentValueSpan = TimeSpan.Zero;
            DateTime currentStateStartTime = (DateTime)value.Rows[0]["uploadTime"];
            TimeSpan currentStateSpan = TimeSpan.Zero;

            int preKuiDian = -1;
            int curKuiDian = -1;
            int kuiDianYiChangCiShu = 0;
            TimeSpan leiJiKuiDian = TimeSpan.Zero;
            TimeSpan curKuiDianSpan = TimeSpan.Zero;
            string kuiDianStartAndEnd = string.Empty;
            DateTime curKuiDianStartTime = DateTime.Now;

            string cedianbianhao = value.Rows[0]["ceDianBianHao"].ToString().Trim();
            long cedianId = (long)value.Rows[0]["cedianID"];
            currentValueStartTime = (DateTime)value.Rows[0]["uploadTime"];

            int tempId = 999999;

            foreach (DataRow row in value.Rows)
            {
            newcedian:
                if (cedianId == (long)row["cedianID"])
                {
                    curState = Convert.ToInt32(row["state"]);
                    currentTime = (DateTime)row["uploadTime"];
                    currentValue = Convert.ToSingle(row["uploadValue"]);
                    curKuiDian = Convert.ToInt32(row["kuiDianYiChang"]);

                    //值变化
                    if (currentValue != preValue)
                    {
                        float sum = preValue * (float)(currentTime - currentValueStartTime).TotalSeconds;
                        if (preState == (int)MoNiLiangState.BaoJing)
                            avgBaoJing += sum;
                        if (preState == (int)MoNiLiangState.DuanDian)
                            avgDuanDian += sum;

                        preValue = currentValue;
                        currentValueStartTime = currentTime;
                    }

                    //状态变化
                    if (preState == (int)MoNiLiangState.BaoJing && currentValue > maxBaoJing)
                    {
                        maxBaoJing = currentValue;
                        maxBaoJingTime = currentTime;
                    }

                    if (preState == (int)MoNiLiangState.DuanDian && currentValue > maxDuanDian)
                    {
                        maxDuanDian = currentValue;
                        maxDuanDianTime = currentTime;
                    }


                    string kuidian = "";
                    //馈电变化
                    if (curKuiDian != preKuiDian)
                    {
                        if (preKuiDian == 0)
                        {
                            kuiDianYiChangCiShu++;
                            curKuiDianSpan = currentTime - curKuiDianStartTime;
                            leiJiKuiDian += curKuiDianSpan;
                            kuiDianStartAndEnd += GlobalParams.TimeSpanString(curKuiDianSpan) + "," + curKuiDianStartTime.ToString(dateString) + "," + currentTime.ToString(dateString) + "\n";
                            kuidian = GlobalParams.TimeSpanString(curKuiDianSpan) + "," + curKuiDianStartTime.ToString(dateString) + "," + currentTime.ToString(dateString);
                        }

                        preKuiDian = curKuiDian;
                        curKuiDianStartTime = currentTime;
                    }

                    if (curState != preState)
                    {
                        doStateChange(preState, ref baoJingCiShu, ref baoJingSpan, ref baoJingQiZhi, ref duanDianCiShu, ref duanDianSpan, ref duanDianQiZhi, currentTime, currentStateStartTime);


                        if (preState > -1 && (curState == (int)MoNiLiangState.BaoJing || curState == (int)MoNiLiangState.DuanDian || kuidian != ""))
                        {
                            addBaoJingDuanDianRow(ref result, ref dt2, cedianId, ++tempId, cedianbianhao, -1, (curState == (int)MoNiLiangState.BaoJing) ? currentStateStartTime.ToString(dateString) + "," + currentTime.ToString(dateString) : "", "", -1,
                                    (curState == (int)MoNiLiangState.DuanDian) ? currentStateStartTime.ToString(dateString) + "," + currentTime.ToString(dateString) : "", "", "", "", -1, "", kuidian);
                        }
                        preState = curState;
                        currentStateStartTime = currentTime;


                    }



                }
                else
                {

                    currentTime = new DateTime(currentStateStartTime.Year, currentStateStartTime.Month, currentStateStartTime.Day, 23, 59, 59);
                    doStateChange(preState, ref baoJingCiShu, ref baoJingSpan, ref baoJingQiZhi, ref duanDianCiShu, ref duanDianSpan, ref duanDianQiZhi, currentTime, currentStateStartTime);
                    if (currentValue != preValue)
                    {
                        float sum = preValue * (float)(currentTime - currentValueStartTime).TotalSeconds;
                        if (preState == (int)MoNiLiangState.BaoJing)
                            avgBaoJing += sum;
                        if (preState == (int)MoNiLiangState.DuanDian)
                            avgDuanDian += sum;

                        preValue = currentValue;
                        currentValueStartTime = currentTime;
                    }
                    if (curKuiDian == 0)
                    {
                        kuiDianYiChangCiShu++;
                        curKuiDianSpan = currentTime - curKuiDianStartTime;
                        leiJiKuiDian += curKuiDianSpan;
                        kuiDianStartAndEnd += curKuiDianStartTime.ToString(dateString) + "," + currentTime.ToString(dateString) + "\n";
                    }

                    if (baoJingCiShu != 0)
                        avgAndMaxBaoJing = Math.Round(avgBaoJing / baoJingSpan.TotalSeconds, 2) + "," + maxBaoJing + "," + maxBaoJingTime.ToString(dateString);
                    if (duanDianCiShu != 0)
                        avgAndMaxDuanDian = Math.Round(avgDuanDian / duanDianSpan.TotalSeconds, 2) + "," + maxDuanDian + "," + maxDuanDianTime.ToString(dateString);
                    addBaoJingDuanDianRow(ref result, ref dt2, cedianId, ++tempId, cedianbianhao, baoJingCiShu, "", GlobalParams.TimeSpanString(baoJingSpan), duanDianCiShu,
                        "", GlobalParams.TimeSpanString(duanDianSpan), avgAndMaxBaoJing, avgAndMaxDuanDian, kuiDianYiChangCiShu, GlobalParams.TimeSpanString(leiJiKuiDian), "");

                    //重新初始化
                    preState = curState = preKuiDian = curKuiDian = -1;
                    baoJingCiShu = duanDianCiShu = kuiDianYiChangCiShu = 0;
                    maxBaoJing = avgBaoJing = maxDuanDian = avgDuanDian = 0;
                    baoJingQiZhi = duanDianQiZhi = avgAndMaxBaoJing = avgAndMaxDuanDian = kuiDianStartAndEnd = string.Empty;
                    baoJingSpan = duanDianSpan = currentStateSpan = leiJiKuiDian = TimeSpan.Zero;
                    currentStateStartTime = DateTime.Now;
                    cedianbianhao = row["ceDianBianHao"].ToString().Trim();
                    cedianId = (long)row["cedianID"];

                    goto newcedian;
                }
            }

            currentTime = new DateTime(currentStateStartTime.Year, currentStateStartTime.Month, currentStateStartTime.Day, 23, 59, 59);
            doStateChange(preState, ref baoJingCiShu, ref baoJingSpan, ref baoJingQiZhi, ref duanDianCiShu, ref duanDianSpan, ref duanDianQiZhi, currentTime, currentStateStartTime);

            if (baoJingCiShu != 0)
                avgAndMaxBaoJing = Math.Round(avgBaoJing / baoJingSpan.TotalSeconds, 2) + "," + maxBaoJing + "," + maxBaoJingTime.ToString(dateString);
            if (duanDianCiShu != 0)
                avgAndMaxDuanDian = Math.Round(avgDuanDian / duanDianSpan.TotalSeconds, 2) + "," + maxDuanDian + "," + maxDuanDianTime.ToString(dateString);

            addBaoJingDuanDianRow(ref result, ref dt2, cedianId, ++tempId, cedianbianhao, baoJingCiShu, "", GlobalParams.TimeSpanString(baoJingSpan), duanDianCiShu,
                "", GlobalParams.TimeSpanString(duanDianSpan), avgAndMaxBaoJing, avgAndMaxDuanDian, kuiDianYiChangCiShu, GlobalParams.TimeSpanString(leiJiKuiDian), "");


            return result;
        }


        private static void doStateChange(int state, ref int baoJingCiShu, ref TimeSpan baoJingSpan, ref string baoJingQiZhi,
            ref int duanDianCiShu, ref TimeSpan duanDianSpan, ref string duanDianQiZhi, DateTime currentTime, DateTime currentStateStartTime)
        {
            if (state == (int)MoNiLiangState.BaoJing)
            {
                baoJingCiShu++;
                baoJingSpan += currentTime - currentStateStartTime;
                baoJingQiZhi += currentStateStartTime.ToString(dateString) + "," + currentTime.ToString(dateString) + "\n";
            }
            else if (state == (int)MoNiLiangState.DuanDian)
            {
                duanDianCiShu++;
                duanDianSpan += currentTime - currentStateStartTime;
                duanDianQiZhi += currentStateStartTime.ToString(dateString) + "," + currentTime.ToString(dateString) + "\n";
            }
        }
        private static void addBaoJingDuanDianRow(ref max_cmssDataSet.BaoJingDuanDianDataTable resultTable, ref DataTable ceDian, long cedianId, long tempId, string ceDianBianHao,
            int baoJingCiShu, string baoJingQiZhi, string leiJiBaoJing, int duanDianCiShu, string duanDianQiZhi, string leiJiDuanDian, string avgAndMaxBaoJing,
            string avgAndMaxDuanDian, int kuidiancishu, string leijikuidian, string kuiDianStartAndEnd)
        {
            max_cmssDataSet.BaoJingDuanDianRow resultRow = (max_cmssDataSet.BaoJingDuanDianRow)resultTable.NewRow();
            resultRow.CeDianBianHao = ceDianBianHao;


            foreach (DataRow row in ceDian.Rows)
            {
                string bianHao = row["ceDianBianHao"].ToString().Trim();
                long cdid = (long)row["id"];
                //if (bianHao == ceDianBianHao.Trim())
                if (cdid == cedianId)
                {
                    resultRow.DiDian = bianHao + "/" + row["ceDianWeiZhi"] + "/" + row["xiaoLeiXing"];
                    break;
                }
            }

            resultRow.CeDianID = tempId + "";

            if (baoJingCiShu >= 0)
            {
                resultRow.BaoJingCiShu = baoJingCiShu.ToString();
            }
            resultRow.BaoJingQiZhiShiJian = baoJingQiZhi;
            resultRow.LeiJiBaoJing = leiJiBaoJing;
            if (duanDianCiShu >= 0)
            {
                resultRow.DuanDianCiShu = duanDianCiShu.ToString();
            }
            resultRow.DuanDianQiZhiShiJian = duanDianQiZhi;
            resultRow.LeiJiDuanDian = leiJiDuanDian;
            resultRow.DuanDianMax = avgAndMaxDuanDian;
            resultRow.BaoJingMax = avgAndMaxBaoJing;

            if (kuidiancishu >= 0)
            {
                resultRow.KuiDianCiShu = kuidiancishu.ToString();
            }
            resultRow.LeiJiKuiDian = leijikuidian;
            resultRow.KuiDianQiZhiShiJian = kuiDianStartAndEnd;


            resultTable.Rows.Add(resultRow);

        }
        private static int getLastDayOfMonth(int year, int month)
        {
            int[] a = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            return a[month];
        }
        #endregion
    }
}
