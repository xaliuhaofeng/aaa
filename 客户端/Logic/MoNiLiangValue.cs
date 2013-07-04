namespace Logic
{
    using System;
    using System.Data;

    public class MoNiLiangValue
    {
        public static DataTable getMoNiLiangValue(DateTime beginDate, DateTime endDate, string ceDianBianHao)
        {

            try
            {
                string tableName = "MoNiLiangValue" + beginDate.ToString("yyyy_MM");
                return OperateDB.Select(string.Concat(new object[] { "select  v.ceDianBianHao as CeDianBianHao, c.ceDianWeiZhi as DiDian,c.xiaoLeiXIng as xiaoLeiXing , x.danWei as danWei, x.baoJingZhiShangXian as baoJingZhiShangXian,x.baoJingZhiXiaXian as baoJingZhiXiaXian,x.duanDianZhi as DuanDianMenXian,x.fuDianZhi as FuDianMenXian , v.uploadTime as uploadTime,v.uploadValue as JianCeZhi,v.state from ", tableName, " v left join CeDian c on c.ceDianBianHao=v.ceDianBianHao left join MoNiLiangLeiXing x on x.mingCheng=c.xiaoLeiXing where c.weiShanChu=1 and v.ceDianBianHao='", ceDianBianHao, "' and v.uploadTime>='", beginDate, "' and v.uploadTime<='", endDate, "'" }));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

