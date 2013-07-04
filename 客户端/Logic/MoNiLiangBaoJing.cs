namespace Logic
{
    using System;
    using System.Data;

    public class MoNiLiangBaoJing
    {
        public static int CountBaoJingCeDian(string ceDian, string value)
        {
            return OperateDB.Select("select * from MoNiLiangBaoJing where ceDianBianHao = '" + ceDian + "' and valueReceive = " + value + " and UserName = '" + Users.GlobalUserName + "'").Rows.Count;
        }

        public static int CountDuanDianCeDian(string ceDian, byte Type)
        {
            return OperateDB.Select(string.Concat(new object[] { "select * from MoNiLiangDuanDian where ceDianBianHao = '", ceDian, "'and shiFouDuanDian =", Type, " and UserName = '", Users.GlobalUserName, "'" })).Rows.Count;
        }

        public static int CountDuanDianCeDian(string ceDian, string value)
        {
            return OperateDB.Select("select * from MoNiLiangDuanDian where ceDianBianHao = '" + ceDian + "' and valueReceive = " + value + " and UserName = '" + Users.GlobalUserName + "'").Rows.Count;
        }

        public static void CreateBaoJingCeDian(string ceDian, string value, string time, string State)
        {
            OperateDB.Execute("insert into MoNiLiangBaoJing(ceDianBianHao, valueReceive, uploadTime, state, UserName) values ('" + ceDian + "'," + value + ", '" + time + "'," + State + ",'" + Users.GlobalUserName + "')");
        }

        public static void CreateDuanDianCeDian(string ceDian, string value, byte Type, string time, string State)
        {
            OperateDB.Execute(string.Concat(new object[] { "insert into MoNiLiangDuanDian(ceDianBianHao, valueReceive, shiFouDuanDian, uploadTime, state, UserName) values ('", ceDian, "',", value, ",", Type, ", '", time, "',", State, ",'", Users.GlobalUserName, "')" }));
        }

        public static DataTable GetBaoJingCeDian(int i)
        {
            string tableName = "MoNiLiangValue" + DateTime.Now.ToString("yyyy-MM").Substring(0, 7).Replace('-', '_');
            if (OperateDB.IsTableExist("max_cmss", tableName))
            {
                string[] Reflector0003 = new string[] { "select top 50 * from (select top ", (i * 50).ToString(), " * from ", tableName, " where state = 1 order by uploadTime desc) a order by uploadTime asc" };
                return OperateDB.Select(string.Concat(Reflector0003));
            }
            return null;
        }

        public static DataTable GetDuanDianCeDian(int i)
        {
            string tableName = "MoNiLiangValue" + DateTime.Now.ToString("yyyy-MM").Substring(0, 7).Replace('-', '_');
            if (OperateDB.IsTableExist("max_cmss", tableName))
            {
                string[] Reflector0003 = new string[] { "select top 50 * from (select top ", (i * 50).ToString(), " * from ", tableName, " where state = 2 order by uploadTime desc) a order by uploadTime asc" };
                return OperateDB.Select(string.Concat(Reflector0003));
            }
            return null;
        }

        public static DataTable GetKuiDianYiChangCeDian(string Name, int i)
        {
            string[] CS= new string[] { "select top 100 * from (select top ", (i * 50).ToString(), " * from ", Name, " where kuiDianYiChang = 1 order by uploadTime desc) a order by uploadTime asc" };
            return OperateDB.Select(string.Concat(CS));
        }

        public static void UpdateDuanDian(string ceDian, byte Type, byte OType)
        {
            string s = string.Concat(new object[] { "update MoNiLiangDuanDian set shiFouDuanDian =", Type, "where ceDianBianHao = '", ceDian, "'and shiFouDuanDian = ", OType, " and UserName = '", Users.GlobalUserName, "'" });
        }
    }
}

