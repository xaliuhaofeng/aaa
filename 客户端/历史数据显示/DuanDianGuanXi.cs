using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace 历史数据显示
{
    public  class DuanDianGuanXi
    {
        public static string[] getDuanDianCeDianBianHao(string ceDian)
        {
            string sql = string.Empty;
            List<string> ret = new List<string>();
            if (ceDian[2] == 'C')
            {
                sql = "select ceDianBianHao from DuanDianGuanXi where kongZhiCeDianBianHao = '" + ceDian + "'";
            }
            else
            {
                sql = "select kongZhiCeDianBianHao from DuanDianGuanXi where ceDianBianHao = '" + ceDian + "'";
            }
            DataTable dt = db.Select(sql);
            if (dt != null&&dt.Rows.Count>0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string sql2=String.Empty;
                    string s = row[0].ToString().Trim();
                    if (s[2] == 'C')
                    {
                         sql2 = "select ceDianBianHao,ceDianWeiZhi,mingCheng from KongZhiLiangCeDian where weiShanChu=1 and ceDianBianHao='" + s + "'";
                    }
                    else
                    {
                         sql2 = "select ceDianBianHao,ceDianWeiZhi,xiaoLeiXing from CeDian where weiShanChu=1 and ceDianBianHao='" + s + "'";
                    }
                    DataTable dt2 = db.Select(sql2);
                    if(dt2!=null && dt2.Rows.Count>0)
                    {
                        string info = dt2.Rows[0]["ceDianBianHao"].ToString().Trim() + "  " + dt2.Rows[0]["ceDianWeiZhi"].ToString().Trim() + "  " + dt2.Rows[0][2].ToString().Trim();
                        ret.Add(info);
                    
                    }
                }
            }
            return ret.ToArray();
        }
        public static string[] getKuiDianCeDianBianHao(string ceDian)
        {
            string sql = string.Empty;
            List<string> ret=new List<string>();
            if (ceDian[2] == 'C')
            {
                sql = "select ceDianBianHao from KuiDianGuanXi where kongZhiCeDianBianHao='" + ceDian + "'";
            }
            else
            {
                sql = "select kongZhiCeDianBianHao from KuiDianGuanXi where ceDianBianHao='" + ceDian + "'";
            }
            DataTable dt = db.Select(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string sql2 = String.Empty;
                    string s = row[0].ToString().Trim();
                    if (s[2] == 'C')
                    {
                        sql2 = "select ceDianBianHao,ceDianWeiZhi,mingCheng from KongZhiLiangCeDian where weiShanChu=1 and ceDianBianHao='" + s + "'";

                    }
                    else
                    {
                        sql2 = "select ceDianBianHao,ceDianWeiZhi,xiaoLeiXing from CeDian where weiShanChu=1 and ceDianBianHao='" + s + "'";
                    }
                    DataTable dt2 = db.Select(sql2);
                    if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        string info = dt2.Rows[0][0].ToString().Trim() + "  " + dt2.Rows[0][1].ToString().Trim() + "  " + dt2.Rows[0][2].ToString().Trim();
                       
                        ret.Add(info);
                    }
                
                }
            
            }

            return ret.ToArray();
        }
        public static DataTable GetSwitchAlarm(string ceDianBianHao)
        {
            string sql = "select mingCheng, leiXing, lingTaiMingCheng, yiTaiMingCheng, erTaiMingCheng, baoJingZhuangTai, shiFouBaoJing, duanDianZhuangTai, shiFouDuanDian from KaiGuanLiangLeiXing where mingCheng in(select xiaoLeiXing from CeDian where weishanchu=1 and ceDianBianHao = '" + ceDianBianHao + "')";
            return db.Select(sql);
        }
        public static DataTable GetKongAlarm(string ceDianBianHao)
        {
            string sql = "select mingCheng, kongZhiLiangLeiXing, lingTaiMingCheng, yiTaiMingCheng from KongZhiLiang where mingCheng in(select mingCheng from KongZhiLiangCeDian where ceDianBianHao = '" + ceDianBianHao + "')";
            return db.Select(sql);
        }
        
    }
}
