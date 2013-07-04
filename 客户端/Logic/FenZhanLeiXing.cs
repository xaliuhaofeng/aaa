namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class FenZhanLeiXing
    {
        public static string CountType(string type)
        {
            return ("select * from FenZhanLeiXing where leiXing= '" + type + "'");
        }

        public static string CreateType(string type)
        {
            return ("insert into FenZhanLeiXing values( '" + type + "')");
        }

        public static string DeleteType(string original)
        {
            return ("delete from FenZhanLeiXing where leiXing = '" + original + "'");
        }

        public static string[] GetAllFenZhanLeiXing()
        {
            List<string> list = new List<string>();
            string sql = "select * from FenZhanLeiXing";
            DataTable dt = OperateDB.Select(sql);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row["leiXing"].ToString());
                }
            }
            return list.ToArray();
        }

        public static DataTable GetFenZhanType()
        {
            string sql = "select * from FenZhanLeiXing";
            return OperateDB.Select(sql);
        }

        public static string UpdateType(string type, string original)
        {
            return ("update FenZhanLeiXing set leiXing = '" + type + "' where leiXing = '" + original + "'");
        }
    }
}

