namespace Logic
{
    using System;
    using System.Data;

    public class KongZhiLiangCeDian
    {
        public static DataTable GetKongZhiLiangCeDian(int fenzhanhao, int tongdaohao)
        {
            return OperateDB.Select(string.Concat(new object[] { "select * from KongZhiLiangCeDian where weishanchu=1 and fenZhanHao = ", fenzhanhao, " and kongZhiLiangBianHao = ", tongdaohao }));
        }

        public static string[] GetKongZhiLiangCeDianByFenZhan(int fenZhanHao)
        {
            DataTable dt = OperateDB.Select("select ceDianBianHao from KongZhiLiangCeDian where weishanchu=1 and fenZhanHao=" + fenZhanHao);
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

        public static byte[] GetKongZhiLiangConfInfo(byte fenZhanHao)
        {
            byte[] b = new byte[2];
            DataTable dt = OperateDB.Select("select ceDianBianHao,kongZhiLiangLeiXing, kongZhiLiangBianHao from KongZhiLiang,KongZhiLiangCeDian where KongZhiLiang.mingCheng=KongZhiLiangCeDian.mingCheng and weishanchu=1 and fenZhanHao=" + fenZhanHao);
            int m = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                m |= Convert.ToByte(dt.Rows[i]["kongZhiLiangLeiXing"]) << ((2 * Convert.ToByte(dt.Rows[i]["kongZhiLiangBianHao"])) - 2);
            }
            b[0] = (byte) (m & 0xff);
            b[1] = (byte) (m >> 8);
            return b;
        }
    }
}

