namespace Logic
{
    using System;

    public class ZhuangTaiTuData
    {
        private static int timeSpan = 300;

        public static ZhuangTaiTuValue[] GetZhuangTaiTuData(string ceDianBianHao, DateTime start, DateTime end)
        {
            DateTime startTime = start;
            DateTime endTime = end;
            TimeSpan ts = (TimeSpan) (endTime - startTime);
            int count = ((int) ts.TotalSeconds) / 300;
            ZhuangTaiTuValue[] values = new ZhuangTaiTuValue[count];
            if (ceDianBianHao[2] != 'F')
            {
                CeDian cedian = new CeDian(ceDianBianHao);
                int baoJingState = cedian.KaiGuanLiang.BaoJingZhuangTai;
                int duanDianState = cedian.KaiGuanLiang.DuanDianZhuangTai;
                HistoryData[] history = CeDian.GetAllValues(startTime, endTime, ceDianBianHao);
                if (history == null)
                {
                    return null;
                }
                KaiGuanLiangLeiXing kgl = GlobalParams.AllkgLeiXing.GetSwitchAlarm(cedian.CeDianBianHao);
                string lingTaiMingCheng = kgl.LingTai.ToString().Trim();
                string yiTaiMingCheng = kgl.YiTai.ToString().Trim();
                string erTaiMingCheng = kgl.ErTai.ToString().Trim();
                int index = 0;
                for (int i = 0; (i < count) && (index < history.Length); i++)
                {
                    values[i].value = (int) history[index].value;
                    values[i].date = history[index].time;
                    index += timeSpan / 5;
                    if (((ceDianBianHao[2] == 'D') || (ceDianBianHao[2] == 'C')) || (ceDianBianHao[2] == 'F'))
                    {
                        if (values[i].value == 0)
                        {
                            values[i].status = lingTaiMingCheng;
                        }
                        else if (values[i].value == 1)
                        {
                            values[i].status = yiTaiMingCheng;
                        }
                        else if (values[i].value == 2)
                        {
                            values[i].status = erTaiMingCheng;
                        }
                        else
                        {
                            values[i].status = "其他";
                        }
                    }
                    else
                    {
                        values[i].status = "";
                    }
                    if (values[i].value == baoJingState)
                    {
                        values[i].baojing = 1;
                    }
                    else
                    {
                        values[i].baojing = 0;
                    }
                    if (values[i].value == duanDianState)
                    {
                        values[i].duandian = 1;
                    }
                    else
                    {
                        values[i].duandian = 0;
                    }
                }
            }
            return values;
        }
    }
}

