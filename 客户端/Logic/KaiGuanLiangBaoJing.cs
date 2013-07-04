namespace Logic
{
    using System;
    using System.Data;

    public class KaiGuanLiangBaoJing
    {
        public static int CountBaoJingCeDian(string ceDian, string value)
        {
            return OperateDB.Select("select * from KaiGuanLiangBaoJing where ceDianBianHao = '" + ceDian + "' and valueReceive = " + value + " and UserName = '" + Users.GlobalUserName + "'").Rows.Count;
        }

        public static int CountDuanDianCeDian(string ceDian, byte Type)
        {
            return OperateDB.Select(string.Concat(new object[] { "select * from KaiGuanLiangDuanDian where ceDianBianHao = '", ceDian, "'and shiFouDuanDian = ", Type, " and UserName = '", Users.GlobalUserName, "'" })).Rows.Count;
        }

        public static int CountDuanDianCeDian(string ceDian, string value)
        {
            return OperateDB.Select("select * from KaiGuanLiangDuanDian where ceDianBianHao = '" + ceDian + "' and valueReceive = " + value + " and UserName = '" + Users.GlobalUserName + "'").Rows.Count;
        }

        public static void CreateBaoJingCeDian(string ceDian, string value, string zhuangTai, string time)
        {
            OperateDB.Execute("insert into KaiGuanLiangBaoJing(ceDianBianHao, valueReceive,zhuangTai, uploadTime, UserName) values ('" + ceDian + "'," + value + ",'" + zhuangTai + "', '" + time + "','" + Users.GlobalUserName + "')");
        }

        public static void CreateDuanDianCeDian(string ceDian, string value, string zhuangTai, byte Type, string time)
        {
            OperateDB.Execute(string.Concat(new object[] { "insert into KaiGuanLiangDuanDian(ceDianBianHao, valueReceive,zhuangTai, shiFouDuanDian, uploadTime, UserName) values ('", ceDian, "',", value, ",'", zhuangTai, "', ", Type, ", '", time, "','", Users.GlobalUserName, "')" }));
        }

        public static DataTable DeviceError(string Name, int i)
        {
            string[] Reflector0001 = new string[] { "select top 100 * from (select top ", (i * 50).ToString(), " k.* from ", Name, " as k left join CeDian c on c.id=k.cedianID where c.weishanchu=1 and uploadValue = 2) a order by uploadTime asc" };
            return OperateDB.Select(string.Concat(Reflector0001));
        }

        public static DataTable GetBaoJingCeDian(int i)
        {
            string tableName = "KaiGuanLiangValue" + DateTime.Now.ToString("yyyy-MM").Substring(0, 7).Replace('-', '_');
            if (OperateDB.IsTableExist("max_cmss", tableName))
            {
                string[] Reflector0003 = new string[] { "select top 50 * from (select top ", (i * 50).ToString(), " * from ", tableName, " where state = 1 order by uploadTime desc) a order by uploadTime asc" };
                return OperateDB.Select(string.Concat(Reflector0003));
            }
            return null;
        }

        public static DataTable GetDuanDianCeDian(int i)
        {
            string tableName = "KaiGuanLiangValue" + DateTime.Now.ToString("yyyy-MM").Substring(0, 7).Replace('-', '_');
            if (OperateDB.IsTableExist("max_cmss", tableName))
            {
                string[] Reflector0003 = new string[] { "select top 50 * from (select top ", (i * 50).ToString(), " * from ", tableName, " where state = 2 order by uploadTime desc) a order by uploadTime asc" };
                return OperateDB.Select(string.Concat(Reflector0003));
            }
            return null;
        }

        public static DataTable GetKuiDianYiChangCeDian(string Name, int i)
        {
            string[] Reflector0001 = new string[] { "select top 100 * from (select top ", (i * 50).ToString(), " * from ", Name, " where kuiDianYiChang = 1 order by uploadTime desc) a order by uploadTime asc" };
            return OperateDB.Select(string.Concat(Reflector0001));
        }

        public static DataTable MoNiLiangDeviceError(string tableName, int i)
        {
            string[] Reflector0001 = new string[] { "select top 100 * from (select top ", (i * 50).ToString(), " m.* from ", tableName, " as m left join CeDian c on c.id=m.cedianID where c.weishanchu=1 and (m.state = 4 or m.state = 5 or m.state = 6 or m.state = 7) ) a order by uploadTime asc" };
            return OperateDB.Select(string.Concat(Reflector0001));
        }

        public static DataTable SwitchStatusData(string tableName, DateTime now)
        {
            DateTime start = now.AddMinutes(-5.0);
            string str = @"with tmp_rn as( 
                    SELECT * , 
                    rn = ROW_NUMBER() OVER (PARTITION BY ceDianBianHao ORDER BY uploadTime DESC) 
                     FROM {0}  where uploadTime>'{1}'
                    ) 
                    SELECT * FROM tmp_rn WHERE rn = 1   order by ceDianBianHao";
            str = string.Format(str, tableName, now);

            return OperateDB.Select(str);
            //return OperateDB.Select(string.Concat(new object[] { "select * from ", tableName, " where uploadTime > '", start, "' order by uploadTime asc" }));
        }

        public static void UpdateDuanDian(string ceDian, byte Type, byte OType)
        {
            string s = string.Concat(new object[] { "update KaiGuanLiangDuanDian set shiFouDuanDian =", Type, "where ceDianBianHao = '", ceDian, "'and shiFouDuanDian = ", OType, " and UserName = '", Users.GlobalUserName, "'" });
        }
    }
}

