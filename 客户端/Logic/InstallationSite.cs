namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class InstallationSite
    {
        private string location;

        public InstallationSite()
        {
        }

        public InstallationSite(string s)
        {
            this.location = s;
        }

        public static string CountLocation(string location1)
        {
            return ("select * from AnZhuangDiDian where DiDian= '" + location1 + "'");
        }

        public string CreateLocation()
        {
            return ("insert into AnZhuangDiDian values( '" + this.location + "')");
        }

        public string DeleteLocation(string original)
        {
            return ("delete from AnZhuangDiDian where DiDian = '" + original + "'");
        }

        public static DataTable GetAllLocation()
        {
            string sql = "select * from AnZhuangDiDian order by DiDian";
            return OperateDB.Select(sql);
        }

        public static string[] GetAllLocationAsArray()
        {
            List<string> list = new List<string>();
            DataTable dt = GetAllLocation();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row["DiDian"].ToString());
                }
            }
            return list.ToArray();
        }

        public string UpdateLocation(string original)
        {
            return ("update AnZhuangDiDian set DiDian = '" + this.location + "' where DiDian = '" + original + "'");
        }
    }
}

