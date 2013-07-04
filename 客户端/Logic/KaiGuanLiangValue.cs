namespace Logic
{
    using System;
    using System.Data;

    public class KaiGuanLiangValue
    {
        public static DataTable getKaiGuanLiangLiangValue(DateTime beginDate, DateTime endDate, string ceDianBianHao)
        {
            try
            {
                string tableName = "KaiGuanLiangValue" + beginDate.ToString("yyyy_MM");
                return OperateDB.Select(string.Concat(new object[] { "select v.ceDianBianHao as CeDianBianHao, c.ceDianWeiZhi as DiDian,c.xiaoLeiXIng as xiaoLeiXing ,v.uploadTime as KaiTingShiKe,str(v.uploadValue) as ZhuangTai from ", tableName, " v left join CeDian c on c.ceDianBianHao=v.ceDianBianHao left join KaiGuanLiangLeiXing x on x.mingCheng=c.xiaoLeiXing and x.leiXing=c.chuanGanQiLeiXing where c.weiShanChu=1 and v.ceDianBianHao='", ceDianBianHao, "' and v.uploadTime>='", beginDate, "' and v.uploadTime<='", endDate, "'" }));
            }
            catch (Exception e)
            {
                //if (Common.DEBUG == 1)
                //{
                //    throw e;
                //}
            }
            return null;
        }
    }
}

