namespace Logic
{
    using System;
    using System.Data;

    public class KeyWords
    {
        public static string CountKey(string type)
        {
            return ("select * from GuanJianZi where guanJianZi = '" + type + "'");
        }

        public static string CreateKey(string type)
        {
            return ("insert into GuanJianZi values( '" + type + "')");
        }

        public static bool DelEnable(string s)
        {
            if (OperateDB.Select("select mingCheng from MoNiLiangLeiXing where guanJianZi = '" + s + "'").Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public static string DeleteKey(string original)
        {
            return ("delete from GuanJianZi where guanJianZi = '" + original + "'");
        }

        public static DataTable GetKey()
        {
            string sql = "select * from GuanJianZi";
            return OperateDB.Select(sql);
        }

        public static string UpdateKey(string type, string original)
        {
            return ("update GuanJianZi set guanJianZi = '" + type + "' where guanJianZi = '" + original + "'");
        }
    }
}

