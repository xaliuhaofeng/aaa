namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class ReportConfig
    {
        private string[] ceDians;
        private List<bool> cols;
        private string qianMing = "";
        private string reportheader = "";
        private int reportName = -1;

        public ReportConfig()
        {
            this.CeDians = new string[0];
            this.cols = new List<bool>();
        }

        public string[] GetCeDianBianHao()
        {
            string[] result = new string[this.ceDians.Length];
            for (int i = 0; i < this.ceDians.Length; i++)
            {
                result[i] = this.ceDians[i].Substring(0, 5);
            }
            return result;
        }

        public static ReportConfig getConfigByName(int reportName)
        {
            DataTable dt = OperateDB.Select("select * from ReportConfig where ReportName = " + reportName);
            DBNull empty = DBNull.Value;
            ReportConfig config = new ReportConfig {
                ReportName = reportName
            };
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["ReportHeader"] != empty)
                {
                    config.ReportHeader = (string) dt.Rows[0]["ReportHeader"];
                }
                else
                {
                    config.ReportHeader = "";
                }
                if (dt.Rows[0]["QianMing"] != empty)
                {
                    config.QianMing = (string) dt.Rows[0]["QianMing"];
                }
                else
                {
                    config.QianMing = "";
                }
                if (dt.Rows[0]["Columns"] != empty)
                {
                    string column = (string) dt.Rows[0]["Columns"];
                    foreach (char ch in column)
                    {
                        switch (ch)
                        {
                            case '1':
                                config.Cols.Add(true);
                                break;

                            case '0':
                                config.Cols.Add(false);
                                break;
                        }
                    }
                }
                if (dt.Rows[0]["CeDians"] != empty)
                {
                    string[] temp = ((string) dt.Rows[0]["CeDians"]).Split(new char[] { ';' });
                    config.CeDians = new string[temp.Length - 1];
                    for (int i = 0; i < (temp.Length - 1); i++)
                    {
                        config.CeDians[i] = temp[i];
                    }
                }
            }
            return config;
        }

        public static List<bool> GetDefaultColValue(int count)
        {
            List<bool> list = new List<bool>();
            for (int i = 0; i < count; i++)
            {
                list.Add(true);
            }
            return list;
        }

        public static void InsertOrUpdateCeDian(ReportConfig config)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in config.CeDians)
            {
                if (s[2] != 'C')
                {
                    sb.Append(s + ";");
                }
            }
            string sql = "";
            if (isReportExist(config.ReportName))
            {
                sql = string.Concat(new object[] { "update ReportConfig set CeDians = '", sb.ToString(), "' where ReportName = ", config.ReportName });
            }
            else
            {
                sql = string.Concat(new object[] { "insert into ReportConfig(CeDians, ReportName) values('", sb.ToString(), "', ", config.ReportName, ")" });
            }
            OperateDB.Execute(sql);
        }

        public static void InsertOrUpdateCols(ReportConfig config)
        {
            string sql;
            StringBuilder sb2 = new StringBuilder();
            foreach (bool b in config.Cols)
            {
                sb2.Append(b ? '1' : '0');
            }
            if (isReportExist(config.ReportName))
            {
                sql = string.Concat(new object[] { "update ReportConfig set Columns = '", sb2.ToString(), "' where ReportName = ", config.ReportName });
            }
            else
            {
                sql = string.Concat(new object[] { "insert into ReportConfig(Columns, ReportName) values('", sb2.ToString(), "', ", config.ReportName, ")" });
            }
            OperateDB.Execute(sql);
        }

        public static void InsertOrUpdateConfig(ReportConfig config)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in config.CeDians)
            {
                sb.Append(s + ";");
            }
            StringBuilder sb2 = new StringBuilder();
            foreach (bool b in config.Cols)
            {
                sb.Append(b ? '1' : '0');
            }
            string sql = "";
            if (isReportExist(config.ReportName))
            {
                sql = string.Concat(new object[] { "update ReportConfig set ReportHeader = '", config.ReportHeader, "', QianMing = '", config.QianMing, "', CeDians = '", sb.ToString(), "', Columns = '", sb2.ToString(), " where ReportName = ", config.ReportName });
            }
            else
            {
                sql = string.Concat(new object[] { "insert into ReportConfig values('", config.ReportHeader, "', '", config.QianMing, "', '", sb2.ToString(), "', '", sb.ToString(), "', '", config.ReportName, "')" });
            }
            OperateDB.Execute(sql);
        }

        public static void InsertOrUpdateHeader(ReportConfig config)
        {
            string sql = "";
            if (isReportExist(config.ReportName))
            {
                sql = string.Concat(new object[] { "update ReportConfig set ReportHeader = '", config.ReportHeader, "', QianMing = '", config.QianMing, "' where ReportName = ", config.ReportName });
            }
            else
            {
                sql = string.Concat(new object[] { "insert into ReportConfig(ReportHeader, QianMing, ReportName) values('", config.ReportHeader, "', '", config.QianMing, "', ", config.ReportName, ")" });
            }
            OperateDB.Execute(sql);
        }

        public static bool isReportExist(int reportName)
        {
            if (OperateDB.Select("select * from ReportConfig where ReportName = " + reportName).Rows.Count == 0)
            {
                return false;
            }
            return true;
        }

        public string[] CeDians
        {
            get
            {
                return this.ceDians;
            }
            set
            {
                this.ceDians = value;
            }
        }

        public List<bool> Cols
        {
            get
            {
                return this.cols;
            }
            set
            {
                this.cols = value;
            }
        }

        public string QianMing
        {
            get
            {
                return this.qianMing;
            }
            set
            {
                this.qianMing = value;
            }
        }

        public string ReportHeader
        {
            get
            {
                return this.reportheader;
            }
            set
            {
                this.reportheader = value;
            }
        }

        public int ReportName
        {
            get
            {
                return this.reportName;
            }
            set
            {
                this.reportName = value;
            }
        }
    }
}

