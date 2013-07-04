namespace Logic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;

    public class KuiDianGuanXi
    {
        public static List<KuiDianGuanXiClass> kuidianlist = new List<KuiDianGuanXiClass>();

        public static DataTable ConvertoDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("kuiDianGuanXiID");
            dt.Columns.Add("ceDianBianHao");
            dt.Columns.Add("kongZhiCeDianBianHao");
            try
            {
                IEnumerable<KuiDianGuanXiClass> query = from item in kuidianlist select item;
                foreach (KuiDianGuanXiClass item in query)
                {
                    DataRow dr = dt.NewRow();
                    dr["kuiDianGuanXiID"] = item.KuiDianGuanXiID;
                    dr["ceDianBianHao"] = item.CeDianBianHao;
                    dr["kongZhiCeDianBianHao"] = item.KongZhiCeDianBianHao;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return dt;
        }

        public static int Count(string ceDian)
        {
            return (from item in kuidianlist
                where item.CeDianBianHao == ceDian
                select item).Count<KuiDianGuanXiClass>();
        }

        public static int CountKong(string ceDian)
        {
            return (from item in kuidianlist
                where item.KongZhiCeDianBianHao == ceDian
                select item).Count<KuiDianGuanXiClass>();
        }

        public static bool CreateKuiDian(string KuiDianID, string CeDianBianHao, string KongZhiCeDianBianHao)
        {
            string sql = "insert into KuiDianGuanXi (kuidianguanxiid, cedianbianhao,kongzhicedianbianhao) values ('" + KuiDianID + "','" + CeDianBianHao + "','" + KongZhiCeDianBianHao + "')";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            KuiDianGuanXiClass kuidian = new KuiDianGuanXiClass(int.Parse(KuiDianID.Trim()));
            kuidianlist.Add(kuidian);
            return true;
        }

        public static bool DelKuiDian(string CeDianBianHao, string KongZhiCeDianBianHao)
        {
            bool flag = false;
            string sql = "delete from KuiDianGuanXi where cedianbianhao = '" + CeDianBianHao + "' and kongzhicedianbianhao ='" + KongZhiCeDianBianHao + "'";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            IEnumerable<KuiDianGuanXiClass> query = from item in kuidianlist
                where (item.CeDianBianHao == CeDianBianHao) && (item.KongZhiCeDianBianHao == KongZhiCeDianBianHao)
                select item;
            int id = -1;
            if (query.Count<KuiDianGuanXiClass>() > 0)
            {
                foreach (KuiDianGuanXiClass item in query)
                {
                    id = kuidianlist.IndexOf(item);
                }
                if (id != -1)
                {
                    kuidianlist.RemoveAt(id);
                }
            }
            return flag;
        }

        public static List<string> feikongzhi(string ceDian)
        {
            List<string> ret = new List<string>();
            IEnumerable<KuiDianGuanXiClass> query = from item in kuidianlist
                where item.CeDianBianHao == ceDian
                select item;
            if (query.Count<KuiDianGuanXiClass>() > 0)
            {
                foreach (KuiDianGuanXiClass item in query)
                {
                    string s = item.KongZhiCeDianBianHao.Trim();
                    if (GlobalParams.AllCeDianList.allcedianlist.ContainsKey(s))
                    {
                        CeDian cedian = GlobalParams.AllCeDianList.allcedianlist[s];
                        string info = cedian.CeDianBianHao + " " + cedian.CeDianWeiZhi + " " + cedian.XiaoleiXing;
                        ret.Add(info);
                    }
                }
            }
            return ret;
        }

        public static void FillKuiDianGuanXi()
        {
            kuidianlist = new List<KuiDianGuanXiClass>();
            string sql = "select * from KuiDianGuanXi";
            DataTable dt = OperateDB.Select(sql);
            if ((dt != null) && (dt.Rows.Count != 0))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    KuiDianGuanXiClass ddgxc = new KuiDianGuanXiClass {
                        KuiDianGuanXiID = Convert.ToInt32(dt.Rows[i]["kuiDianGuanXiID"]),
                        CeDianBianHao = dt.Rows[i]["ceDianBianHao"].ToString().Trim(),
                        KongZhiCeDianBianHao = dt.Rows[i]["kongZhiCeDianBianHao"].ToString().Trim()
                    };
                    kuidianlist.Add(ddgxc);
                }
            }
        }

        public static DataTable GetKuiDian()
        {
            return ConvertoDataTable();
        }

        public static string[] getKuiDianCeDianBianHao(string ceDian)
        {
            List<string> ret = new List<string>();
            if (ceDian[2] == 'C')
            {
                ret = kongzhi(ceDian);
            }
            else
            {
                ret = feikongzhi(ceDian);
            }
            return ret.ToArray();
        }

        public static bool hasKuiDianGuanXi(string ceDianBianHao, string kongZhiLiangCeDian)
        {
            return ((from item in kuidianlist
                where (item.CeDianBianHao == ceDianBianHao) && (item.KongZhiCeDianBianHao == kongZhiLiangCeDian)
                select item).Count<KuiDianGuanXiClass>() > 0);
        }

        public static List<string> kongzhi(string ceDian)
        {
            List<string> ret = new List<string>();
            IEnumerable<KuiDianGuanXiClass> query = from item in kuidianlist
                where item.KongZhiCeDianBianHao == ceDian
                select item;
            if (query.Count<KuiDianGuanXiClass>() > 0)
            {
                foreach (KuiDianGuanXiClass item in query)
                {
                    string s = item.CeDianBianHao.Trim();
                    if (GlobalParams.AllCeDianList.allcedianlist.ContainsKey(s))
                    {
                        CeDian cedian = GlobalParams.AllCeDianList.allcedianlist[s];
                        string info = cedian.CeDianBianHao + " " + cedian.CeDianWeiZhi + " " + cedian.XiaoleiXing;
                        ret.Add(info);
                    }
                }
            }
            return ret;
        }

        public static ArrayList KuiDian(FenZhanRTdata ud)
        {
            ArrayList al = new ArrayList();
            for (int i = 1; i < ud.realValue.Length; i++)
            {
                if (!ud.isMoNiLiang[i])
                {
                    char ch = 'D';
                    string fenZhanHao = ud.fenZhanHao.ToString();
                    if (fenZhanHao.Length == 1)
                    {
                        fenZhanHao = '0' + fenZhanHao;
                    }
                    string tongDaoHao = i.ToString();
                    if (tongDaoHao.Length == 1)
                    {
                        tongDaoHao = '0' + tongDaoHao;
                    }
                    DataTable dt = OperateDB.Select("select kongZhiCeDianBianHao from KuiDianGuanXi where ceDianBianHao='" + (fenZhanHao + ch + tongDaoHao) + "'");
                    if (dt.Rows.Count != 0)
                    {
                        string kongZhiCeDian = dt.Rows[0]["kongZhiCeDianBianHao"].ToString();
                        int kongTongDaoHao = int.Parse(kongZhiCeDian.Substring(4, 1));
                        if (ud.realValue[i].ToString() != ud.kongZhiLiangZhuangTai[kongTongDaoHao].ToString())
                        {
                            dt = OperateDB.Select("select cedianbianhao from DuanDianGuanXi where kongzhicedianbianhao ='" + kongZhiCeDian + "'");
                            if (dt != null)
                            {
                                for (int k = 0; k < dt.Rows.Count; k++)
                                {
                                    al.Add(dt.Rows[k]["ceDianBianHao"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            return al;
        }
    }
}

