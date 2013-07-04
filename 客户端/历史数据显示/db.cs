using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace 历史数据显示
{
    public class db
    {
        private static SqlConnection conn;
        public static string sqlcon;
        public static void Connet(string s)//连接数据库，返回SqlConnection对象
        {
            //MessageBox.Show(s);
            conn = new SqlConnection(s);
            conn.Open();
        
        }
        /// <summary>
        /// 执行带参的查询存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <returns>返回结果集DataTable</returns>
        public static DataTable Select(string strSql)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter com = new SqlDataAdapter(strSql,conn);
                DataSet ds = new DataSet();
                com.Fill(ds);
                return ds.Tables[0];

            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString().Trim());
                return new DataTable();
                
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
 
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="strSql">合法的sql查询语句</param>
        /// <returns>返回查询的结果集数组集合</returns>
        public static ArrayList array(string strsql)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                }
                SqlCommand sqlCmd = new SqlCommand(strsql, conn);
                //做update delete insert 时，才会有影响行数
                SqlDataReader sqlDr = sqlCmd.ExecuteReader();
                //获取当前行中的列数
                int fieldCount = sqlDr.FieldCount;
                ArrayList al = new ArrayList();
                while (sqlDr.Read())
                {
                    for (int i = 0; i < fieldCount; i++)
                    {
                        al.Add(Convert.ToString(sqlDr.GetValue(i)));
                    }

                }
                return al;


            }
            catch (SqlException ex)
            {
                throw (ex);

            }
            finally
            {
                conn.Close();
            }
        
        }
        

    }
}
