namespace Logic
{
    using System;
    using System.Data;

    public class ZhuZhuangTuData
    {
        private static ZhuZhuangTuCoordinatesValue[] doGetData(DataTable dt)
        {
            ZhuZhuangTuCoordinatesValue[] values = new ZhuZhuangTuCoordinatesValue[0x18];
            int kaiZhuanTai = 0;
            int count = 0;
            int currentState = -1;
            int kaiTingCiShu = 0;
            TimeSpan kaiTingShiJian = TimeSpan.Zero;
            DateTime start = (DateTime) dt.Rows[0]["uploadTime"];
            DateTime now = (DateTime) dt.Rows[0]["uploadTime"];
            foreach (DataRow row in dt.Rows)
            {
                now = (DateTime) row["uploadTime"];
                if (now.Hour == count)
                {
                    int state = Convert.ToInt32(row["uploadValue"]);
                    if (state != currentState)
                    {
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
                }
                else
                {
                    if (currentState == kaiZhuanTai)
                    {
                        kaiTingCiShu++;
                        DateTime tempEnd = new DateTime(start.Year, start.Month, start.Day, start.Hour + 1, 0, 0);
                        kaiTingShiJian += tempEnd - start;
                        start = tempEnd;
                    }
                    values[count].kaiTingCiShu = kaiTingCiShu;
                    float value = (((float) kaiTingShiJian.TotalSeconds) / 3600f) * 100f;
                    values[count].Value = (float) Math.Round((double) value, 2);
                    values[count].kaiTingShiJian = kaiTingShiJian;
                    if (currentState == kaiZhuanTai)
                    {
                        DateTime end = new DateTime(now.Year, now.Month, now.Day, count + 1, 0, 0);
                        kaiTingShiJian += new TimeSpan(1, 0, 0);
                        value = 100f;
                        kaiTingCiShu = 1;
                    }
                    else
                    {
                        value = 0f;
                        kaiTingCiShu = 0;
                        kaiTingShiJian = TimeSpan.Zero;
                    }
                    for (int i = count + 1; i < now.Hour; i++)
                    {
                        values[i].Value = value;
                        values[i].kaiTingCiShu = kaiTingCiShu;
                        values[i].kaiTingShiJian = kaiTingShiJian;
                    }
                    if (currentState == kaiZhuanTai)
                    {
                        kaiTingCiShu = 1;
                        DateTime startTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
                        kaiTingShiJian = (TimeSpan) (now - startTime);
                    }
                    else
                    {
                        kaiTingCiShu = 0;
                        kaiTingShiJian = TimeSpan.Zero;
                    }
                    count = now.Hour;
                    currentState = Convert.ToInt32(row["uploadValue"]);
                    start = now;
                }
            }
            if (currentState == kaiZhuanTai)
            {
                kaiTingCiShu++;
                kaiTingShiJian += now - start;
            }
            values[count].kaiTingCiShu = kaiTingCiShu;
            values[count].Value = (float) Math.Round((double) ((kaiTingShiJian.TotalSeconds / 3600.0) * 100.0));
            values[count].kaiTingShiJian = kaiTingShiJian;
            return values;
        }

        public static ZhuZhuangTuCoordinatesValue[] GetZhuZhuangTuData(DateTime date, string cedianbianhao)
        {
            ZhuZhuangTuCoordinatesValue[] values = new ZhuZhuangTuCoordinatesValue[0x18];
            string tableName = "KaiGuanLiangValue" + date.ToString("yyyy_MM");
            string startTime = date.ToString("yyyy-MM-dd") + " 00:00:00";
            string endTime = date.ToString("yyyy-MM-dd") + " 23:59:59";
            if (OperateDB.IsTableExist("max_cmss", tableName))
            {
                DataTable dt = OperateDB.Select("select * from " + tableName + " where ceDianBianHao = '" + cedianbianhao + "' and uploadTime between '" + startTime + "' and '" + endTime + "'");
                if ((dt != null) && (dt.Rows.Count != 0))
                {
                    values = doGetData(dt);
                }
            }
            return values;
        }
    }
}

