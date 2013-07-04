namespace Logic
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;

    public class OperateDB
    {
        private static SqlConnection con;
        private static object selectObject = new object();

        public static void Close()
        {
            con.Close();
        }

        public static void Connect()
        {
            try
            {
                if (con != null)
                {
                    con.Open();
                }
                else
                {
                    ClientConfig config = ClientConfig.CreateCommon();
                    con = new SqlConnection("server=" + config.get("dbAddress") + ";database=" + config.get("dbName") + ";uid=" + config.get("dbUserName") + ";pwd=" + config.get("dbPassword") + ";Connect Timeout=10;");
                    con.Open();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void Execute(string s)
        {
            if ((con == null) || (con.State == ConnectionState.Closed))
            {
                Connect();
            }
            lock (con)
            {
                try
                {
                    new SqlCommand(s, con).ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //if (Common.DEBUG == 1)
                    //{
                    //    throw new Exception("数据库写操作失败：" + e.ToString());
                    //}
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public static void Execute(string s, Hashtable paramList)
        {
            if ((con == null) || (con.State == ConnectionState.Closed))
            {
                Connect();
            }
            lock (con)
            {
                try
                {
                    SqlCommand com = new SqlCommand(s, con);
                    if ((paramList != null) && (paramList.Count > 0))
                    {
                        foreach (DictionaryEntry de in paramList)
                        {
                            string parameterName = de.Key.ToString();
                            com.Parameters.Add(new SqlParameter(parameterName, de.Value));
                        }
                    }
                    com.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //if (Common.DEBUG == 1)
                    //{
                    //    throw new Exception("数据库写操作失败：" + e.ToString());
                    //}
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public static bool ExecuteSql(string s)
        {
            if ((con == null) || (con.State == ConnectionState.Closed))
            {
                Connect();
            }
            lock (con)
            {
                try
                {
                    new SqlCommand(s, con).ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //if (Common.DEBUG == 1)
                    //{
                    //    throw new Exception("数据库写操作失败：" + e.ToString());
                    //}
                    Console.WriteLine(e.ToString());
                    return false;
                }
                return true;
            }
        }

        public static bool IsTableExist(string DBName, string tablename)
        {
            if ((con == null) || (con.State == ConnectionState.Closed))
            {
                Connect();
            }
            lock (con)
            {
                bool exist;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataReader sdr = new SqlCommand(("use " + DBName + "\n") + "select * from dbo.sysobjects where id = object_id(N'[dbo].[" + tablename + "]') and OBJECTPROPERTY(id, N'IsUserTable')= 1", con).ExecuteReader();
                if (sdr.HasRows)
                {
                    exist = true;
                }
                else
                {
                    exist = false;
                }
                sdr.Close();
                return exist;
            }
        }

        public static DataTable Select(string s)
        {
             if (con == null || con.State == ConnectionState.Closed)
            {
                Connect();
            }
           lock (con)//并发处理
           {
                try
                {                   
                    SqlDataAdapter com = new SqlDataAdapter(s, con);
                    DataSet ds = new DataSet();
                    com.Fill(ds);
                    return ds.Tables[0];
                }
                catch (Exception e)
                {                    
                    Console.WriteLine(e.ToString());
                    return null;
                }
            }
        }

        public static void WebConnect(string connectionStr)
        {
            try
            {
                if (con != null)
                {
                    con.Open();
                }
                else
                {
                    con = new SqlConnection(connectionStr);
                    con.Open();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataTable WebSelect(string s)
        {
            if ((con == null) || (con.State == ConnectionState.Closed))
            {
                Connect();
            }
            lock (con)
            {
                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter com = new SqlDataAdapter(s, con);
                    DataSet ds = new DataSet();
                    com.Fill(ds);
                    Close();
                    return ds.Tables[0];
                }
                catch (Exception e)
                {
                    //if (Common.DEBUG == 1)
                    //{
                    //    throw new Exception("数据库操作失败:" + e.ToString());
                    //}
                    Console.WriteLine(e.ToString());
                    return  null;
                }
            }
        }
    }
}

