namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Log
    {
        private string logTime;
        private int logtype;
        private string operation;
        private string userName;

        public Log(string userName, string logTime, string operation, int logType)
        {
            this.userName = userName;
            this.logTime = logTime;
            this.operation = operation;
            this.logtype = logType;
        }

        public static Log[] GetLogsByTypes(LogType[] types, DateTime? start, DateTime? end)
        {
            object Reflector0004;
            string type_str = "";
            foreach (LogType logtype in types)
            {
                if (type_str.Length > 0)
                {
                    type_str = type_str + ",";
                }
                type_str = type_str + ((int) logtype);
            }
            string sql = "select * from log where LogType in (" + type_str + ")";
            if (start.HasValue)
            {
                Reflector0004 = sql;
                sql = string.Concat(new object[] { Reflector0004, " and logTime>='", start, "'" });
            }
            if (end.HasValue)
            {
                Reflector0004 = sql;
                sql = string.Concat(new object[] { Reflector0004, " and logTime<='", end, "'" });
            }
            DataTable dt = OperateDB.Select(sql);
            if (dt == null)
            {
                return new Log[0];
            }
            List<Log> logs = new List<Log>();
            foreach (DataRow row in dt.Rows)
            {
                Log log = new Log(row["userName"].ToString(), row["logTime"].ToString(), row["operation"].ToString(), (int) row["Logtype"]);
                logs.Add(log);
            }
            return logs.ToArray();
        }

        public static LogType getLogType(int type)
        {
            if (type != 1)
            {
                if (type == 2)
                {
                    return LogType.FenZhanPeiZhi;
                }
                if (type == 3)
                {
                    return LogType.GuZhangBiSuo;
                }
                if (type == 4)
                {
                    return LogType.FengDianWaSiBiSuo;
                }
                if (type == 5)
                {
                    return LogType.ShouDongKongZhi;
                }
                if (type == 6)
                {
                    return LogType.JiaoShi;
                }
                if (type == 7)
                {
                    return LogType.MoNiLiang;
                }
                if (type == 8)
                {
                    return LogType.KaiGuanLiang;
                }
                if (type == 9)
                {
                    return LogType.DuoSheBei;
                }
                if (type == 10)
                {
                    return LogType.KuiDianGuanXi;
                }
                if (type == 11)
                {
                    return LogType.KongZhiLuoJi;
                }
            }
            return LogType.CeDianPeiZhi;
        }

        public static string getLogTypeName(int type)
        {
            if (type == 1)
            {
                return "测点配置";
            }
            if (type == 2)
            {
                return "分站配置";
            }
            if (type == 3)
            {
                return "故障闭锁";
            }
            if (type == 4)
            {
                return "风电瓦斯闭锁";
            }
            if (type == 5)
            {
                return "手动控制";
            }
            if (type == 6)
            {
                return "校时操作";
            }
            if (type == 7)
            {
                return "模拟量";
            }
            if (type == 8)
            {
                return "开关量";
            }
            if (type == 9)
            {
                return "多设备联动报警";
            }
            if (type == 10)
            {
                return "馈电关系";
            }
            if (type == 11)
            {
                return "控制逻辑关系";
            }
            return "";
        }

        public static Log[] GetSelectedLogs(string user, LogType type, DateTime? start, DateTime? end)
        {
            string sql;
            object Reflector0003;
            if (user != "全部")
            {
                sql = string.Concat(new object[] { "select * from log where userName = '", user, "' and LogType = ", (int) type });
            }
            else
            {
                sql = "select * from log where LogType = " + ((int) type);
            }
            if (start.HasValue && end.HasValue)
            {
                Reflector0003 = sql;
                sql = string.Concat(new object[] { Reflector0003, " and logTime between '", start, "' and '", end, "'" });
            }
            else if (start.HasValue)
            {
                Reflector0003 = sql;
                sql = string.Concat(new object[] { Reflector0003, " and logTime>='", start, "'" });
            }
            else if (end.HasValue)
            {
                Reflector0003 = sql;
                sql = string.Concat(new object[] { Reflector0003, " and logTime<='", end, "'" });
            }
            DataTable dt = OperateDB.Select(sql);
            if (dt == null)
            {
                return new Log[0];
            }
            List<Log> logs = new List<Log>();
            foreach (DataRow row in dt.Rows)
            {
                Log log = new Log(row["userName"].ToString(), row["logTime"].ToString(), row["operation"].ToString(), (int) row["Logtype"]);
                logs.Add(log);
            }
            return logs.ToArray();
        }

        public static void WriteLog(LogType type, string operation)
        {
            string username = ClientConfig.CreateCommon().get("username");
            string logTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            OperateDB.Execute(string.Concat(new object[] { "insert into Log values('", username, "', '", logTime, "', '", operation, "', ", (int) type, ")" }));
        }

        public string LogTime
        {
            get
            {
                return this.logTime;
            }
            set
            {
                this.logTime = value;
            }
        }

        public int Logtype
        {
            get
            {
                return this.logtype;
            }
            set
            {
                this.logtype = value;
            }
        }

        public string Operation
        {
            get
            {
                return this.operation;
            }
            set
            {
                this.operation = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }
    }
}

