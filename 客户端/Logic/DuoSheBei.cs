namespace Logic
{
    using System;
    using System.Data;

    public class DuoSheBei
    {
        public static int CountMulti(string BianHao)
        {
            return OperateDB.Select("select * from DuoSheBei where ceDianBianHao = '" + BianHao + "'").Rows.Count;
        }

        public static string CreateMulti(string ID, string BianHao, string ZhuangTai)
        {
            return ("insert into DuoSheBei( guanXiID, ceDianBianHao, baoJingZhuangTai) values (" + ID + ",'" + BianHao + "','" + ZhuangTai + "')");
        }

        public static string DelMulti(string ID)
        {
            return ("delete from DuoSheBei where guanXiID = " + ID);
        }

        public static DataTable GetMax()
        {
            string sql = "select max(guanXiID) from DuoSheBei";
            return OperateDB.Select(sql);
        }

        public static DataTable GetMulti()
        {
            string sql = "select guanXiID,ceDianBianHao,baoJingZhuangTai from DuoSheBei";
            return OperateDB.Select(sql);
        }
    }
}

