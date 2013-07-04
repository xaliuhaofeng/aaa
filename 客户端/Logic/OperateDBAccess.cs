namespace Logic
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Windows.Forms;

    public class OperateDBAccess
    {
        private static OleDbConnection con;

        public static void Close()
        {
            con.Close();
        }

        public static void Connect()
        {
            con = new OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=" + Application.StartupPath + "/MAXSafe.mdb");
            con.Open();
        }

        public static void Execute(string s)
        {
            Connect();
            new OleDbCommand(s, con).ExecuteNonQuery();
            Close();
        }

        public int getRecordCount(string s)
        {
            OleDbDataAdapter com = new OleDbDataAdapter(s, con);
            DataSet ds = new DataSet();
            com.Fill(ds);
            return ds.Tables[0].Rows.Count;
        }

        public static DataTable Select(string s)
        {
            Connect();
            OleDbDataAdapter com = new OleDbDataAdapter(s, con);
            DataSet ds = new DataSet();
            com.Fill(ds);
            Close();
            return ds.Tables[0];
        }

        public DataTable SelectWithoutID(string s)
        {
            OleDbDataAdapter com = new OleDbDataAdapter(s, con);
            DataSet ds = new DataSet();
            com.Fill(ds);
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j].ToString() == "")
                    {
                        if (dt.Columns[j].DataType.ToString() == "System.String")
                        {
                            dt.Rows[i][j] = "";
                        }
                        else
                        {
                            dt.Rows[i][j] = 0.0;
                        }
                    }
                }
            }
            dt.Columns.Remove("ID");
            return dt;
        }
    }
}

