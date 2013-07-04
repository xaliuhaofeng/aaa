namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
  
    public class DuanDianGuanXi
    {
        public static List<DuanDianGuanXiClass> ddgxc = new List<DuanDianGuanXiClass>();
        public static kjnafenzhan[] oldfenzhan;
        public static List<byte> ret;

        private static void configoldkjna(byte fenzhanhao, byte kongzhinumber, ref kjnafenzhan[] oldfenzhan)
        {
            for (int i = 0; i < oldfenzhan.Length; i++)
            {
                if (oldfenzhan[i].number == fenzhanhao)
                {
                    oldfenzhan[i].isuse = true;
                    int kongzhi = Convert.ToInt32(kongzhinumber);
                    int sum = Convert.ToInt32(Math.Pow(2.0, (double) (kongzhi - 1)));
                    oldfenzhan[i].one = Convert.ToByte((int) (oldfenzhan[i].one | sum));
                }
            }
        }

        public static int Count(string ceDian)
        {
            return OperateDB.Select("select * from DuanDianGuanXi where ceDianBianHao = '" + ceDian + "'").Rows.Count;
        }

        public static int CountKong(string ceDian)
        {
            return OperateDB.Select("select * from DuanDianGuanXi where kongZhiCeDianBianHao = '" + ceDian + "'").Rows.Count;
        }

        public static bool CreateDuanDian(string DuanDianGuanXiId, string CeDianBianHao, string KongZhiCeDianBianHao, string KongZhiFangShi)
        {
            string sql = "insert into DuanDianGuanXi (duandianguanxiid, cedianbianhao,kongzhicedianbianhao,kongzhifangshi) values ('" + DuanDianGuanXiId + "','" + CeDianBianHao + "','" + KongZhiCeDianBianHao + "','" + KongZhiFangShi + "')";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            DuanDianGuanXiClass du = new DuanDianGuanXiClass(int.Parse(DuanDianGuanXiId.Trim()), CeDianBianHao, KongZhiCeDianBianHao);
            return true;
        }

        public static bool DelDuanDian(string CeDianBianHao, string KongZhiCeDianBianHao)
        {
            string sql = "delete from DuanDianGuanXi where cedianbianhao = '" + CeDianBianHao + "' and kongzhicedianbianhao ='" + KongZhiCeDianBianHao + "'";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            IEnumerable<DuanDianGuanXiClass> query = from item in ddgxc
                where (item.CeDianBianHao == CeDianBianHao) && (item.KongZhiCeDianBianHao == KongZhiCeDianBianHao)
                select item;
            int a = query.Count<DuanDianGuanXiClass>();
            if (query.Count<DuanDianGuanXiClass>() == 1)
            {
                foreach (DuanDianGuanXiClass item in query)
                {
                    ddgxc.Remove(item);
                    break;
                }
            }
            return true;
        }

        public static void FillDuanDianGuanXi()
        {
            ddgxc = new List<DuanDianGuanXiClass>();

            string sql = "select * from DuanDianGuanXi";
            DataTable dt = OperateDB.Select(sql);
            if ((dt != null) && (dt.Rows.Count != 0))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DuanDianGuanXiClass ddgxcc = new DuanDianGuanXiClass {
                        DuanDianGuanXiID = Convert.ToInt32(dt.Rows[i]["duanDianGuanXiID"]),
                        CeDianBianHao = dt.Rows[i]["ceDianBianHao"].ToString().Trim(),
                        KongZhiFangShi = Convert.ToBoolean(dt.Rows[i]["kongZhiFangShi"]),
                        KongZhiCeDianBianHao = dt.Rows[i]["kongZhiCeDianBianHao"].ToString().Trim()
                    };
                    DuanDianGuanXi.ddgxc.Add(ddgxcc);
                }
            }
        }

        private static void fudiankjna(byte fenzhanhao, byte kongzhinumber, ref kjnafenzhan[] oldfenzhan)
        {
            for (int i = 0; i < oldfenzhan.Length; i++)
            {
                if (oldfenzhan[i].number == fenzhanhao)
                {
                    oldfenzhan[i].isuse = true;
                    int kongzhi = Convert.ToInt32(kongzhinumber);
                    int sum = Convert.ToInt32(Math.Pow(2.0, (double) (kongzhi - 1)));
                    oldfenzhan[i].one = Convert.ToByte((int) (oldfenzhan[i].one & ~sum));
                }
            }
        }

        public static byte GetConfInfoByCeDianBianHao(string ceDianBianHao)
        {
            byte b = 0;
            int fenZhanHao = Convert.ToInt32(ceDianBianHao.Substring(0, 2));
            DataTable dt = OperateDB.Select("select duanDianGuanXiID from DuanDianGuanXi where ceDianBianHao='" + ceDianBianHao + "'");
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable dt1 = OperateDB.Select("select kongZhiCeDianBianHao from DuanDianGuanXi where duanDianGuanXiID=" + Convert.ToInt32(dt.Rows[i]["duanDianGuanXiID"]));
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        int newFenZhanHao = Convert.ToInt32(dt1.Rows[j]["kongZhiCeDianBianHao"].ToString().Substring(0, 2));
                        int kongZhiLiangBianHao = Convert.ToInt32(dt1.Rows[j]["kongZhiCeDianBianHao"].ToString().Substring(3, 2));
                        if (fenZhanHao == newFenZhanHao)
                        {
                            b = (byte) (b | (((int) 1) << (kongZhiLiangBianHao - 1)));
                        }
                    }
                }
            }
            return b;
        }

        public static DataTable GetDuanDian()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("duandianguanxiid");
            dt.Columns.Add("cedianbianhao");
            dt.Columns.Add("kongzhicedianbianhao");
            dt.Columns.Add("kongzhifangshi");
            dt.Columns.Add("cedianweizhi");
            dt.Columns["kongzhifangshi"].DataType = System.Type.GetType("System.Boolean");
            var query = from item in DuanDianGuanXi.ddgxc
                        select item;
            if (query.Count() > 0)
            {
                foreach (var item in query)
                {
                    var qry = from items in GlobalParams.AllCeDianList.allcedianlist
                              where items.Value.WeiShanChu == true && items.Value.CeDianBianHao == item.KongZhiCeDianBianHao
                              select items;
                    foreach (var items in qry)
                    {
                        DataRow dr = dt.NewRow();
                        dr["duandianguanxiid"] = item.DuanDianGuanXiID;
                        dr["cedianbianhao"] = item.CeDianBianHao;
                        dr["kongzhicedianbianhao"] = item.KongZhiCeDianBianHao;
                        dr["kongzhifangshi"] = item.KongZhiFangShi;
                        dr["cedianweizhi"] = items.Value.CeDianWeiZhi;
                        dt.Rows.Add(dr);
                    }

                }
                return dt;
            }
            else
            { return dt; }
        }

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
            DataTable dt = OperateDB.Select(sql);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string s = row[0].ToString();
                    if (GlobalParams.AllCeDianList.allcedianlist.ContainsKey(s))
                    {
                        CeDian cedian = GlobalParams.AllCeDianList.allcedianlist[s];
                        string info = cedian.CeDianBianHao + " " + cedian.CeDianWeiZhi + " " + cedian.XiaoleiXing;
                        ret.Add(info);
                    }
                }
            }
            return ret.ToArray();
        }

        public static DataTable GetMaxDuanDianID()
        {
            string sql = "select duanDianGuanXiID from DuanDianGuanXi";
            return OperateDB.Select(sql);
        }

        public static void getoldkjnacedian()
        {
            string sql = "select fenZhanHao from FenZhanChuanKou where chuanKouHao > 0 and chuanKouHao<1 order by fenZhanHao";
            DataTable dtsql = OperateDB.Select(sql);
            ret = new List<byte>();
            oldfenzhan = new kjnafenzhan[dtsql.Rows.Count];
            for (int i = 0; i < dtsql.Rows.Count; i++)
            {
                ret.Add(Convert.ToByte(dtsql.Rows[i]["fenZhanHao"]));
                oldfenzhan[i].number = Convert.ToByte(dtsql.Rows[i]["fenZhanHao"]);
            }
        }

        public static bool hasDuanDianGuanXi(string ceDianBianHao, string kongZhiLiangCeDian)
        {
            DataTable dt = OperateDB.Select("select * from DuanDianGuanXi where ceDianBianHao = '" + ceDianBianHao + "' and kongZhiCeDianBianHao = '" + kongZhiLiangCeDian + "'");
            return ((dt != null) && (dt.Rows.Count > 0));
        }

        public static bool SendControlInfo(string CeDianBianHao, byte Type, Dictionary<string, CeDian> allCedian)
        {
            DataTable dt;
            byte kkg;
            int i;
            byte flag;
            int duanDianGuanXiID;
            DataTable dt1;
            int j;
            DataTable dt2;
            int k;
            byte newFenZhanHao;
            byte kongZhiLiangBianHao;
            if (Type == 1)
            {
                dt = OperateDB.Select("select duanDianGuanXiID,KongZhiFangShi from DuanDianGuanXi where ceDianBianHao='" + CeDianBianHao + "'");
                if (dt.Rows.Count != 0)
                {
                    kkg = 0;
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        flag = 0;
                        if (!Convert.ToBoolean(dt.Rows[i]["KongZhiFangShi"]))
                        {
                            string kongzhicedianbianhao = string.Empty;
                            duanDianGuanXiID = Convert.ToInt32(dt.Rows[i]["duanDianGuanXiID"]);
                            dt1 = OperateDB.Select("select ceDianBianHao from DuanDianGuanXi where duanDianGuanXiID=" + duanDianGuanXiID);
                            j = 0;
                            while (j < dt1.Rows.Count)
                            {
                                if (allCedian[dt1.Rows[j]["ceDianBianHao"].ToString()].DuanDianFlag)
                                {
                                    flag = 1;
                                    break;
                                }
                                j++;
                            }
                            if (flag == 1)
                            {
                                dt2 = OperateDB.Select("select kongZhiCeDianBianHao from DuanDianGuanXi where duanDianGuanXiID=" + duanDianGuanXiID);
                                k = 0;
                                while (k < dt2.Rows.Count)
                                {
                                    newFenZhanHao = Convert.ToByte(dt2.Rows[k]["kongZhiCeDianBianHao"].ToString().Substring(0, 2));
                                    kongZhiLiangBianHao = Convert.ToByte(dt2.Rows[k]["kongZhiCeDianBianHao"].ToString().Substring(3, 2));
                                    kongzhicedianbianhao = dt2.Rows[k]["kongZhiCeDianBianHao"].ToString().Trim();
                                    if (ret.Contains(newFenZhanHao))
                                    {
                                        configoldkjna(newFenZhanHao, kongZhiLiangBianHao, ref oldfenzhan);
                                    }
                                    else
                                    {
                                      
                                        UDPComm.Send(ziDongControl(Type, newFenZhanHao, kongZhiLiangBianHao));
                                        GlobalParams.lastCutFenZhan = newFenZhanHao+"";
                                        GlobalParams.lastCutTongDao = kongZhiLiangBianHao+"";
                                        GlobalParams.lastCutType = 1;
                                        kkg = 1;
                                       
                                    }
                                    k++;
                                }
                            }
                        }
                    }
                    if (oldfenzhan != null)
                    {
                        for (i = 0; i < oldfenzhan.Length; i++)
                        {
                            if (oldfenzhan[i].isuse)
                            {
                                UDPComm.Send(ziDongControlold(Type, oldfenzhan[i].number, oldfenzhan[i].one));
                                oldfenzhan[i].isuse = false;
                            }
                        }
                    }
                    if (kkg == 1)
                    {
                        return true;
                    }
                }
                return false;
            }
            if (Type == 0)
            {
                dt = OperateDB.Select("select duanDianGuanXiID,KongZhiFangShi from DuanDianGuanXi where ceDianBianHao='" + CeDianBianHao + "'");
                if (dt.Rows.Count != 0)
                {
                    kkg = 0;
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        flag = 0;
                        if (!Convert.ToBoolean(dt.Rows[i]["KongZhiFangShi"]))
                        {
                            duanDianGuanXiID = Convert.ToInt32(dt.Rows[i]["duanDianGuanXiID"]);
                            dt1 = OperateDB.Select("select ceDianBianHao from DuanDianGuanXi where duanDianGuanXiID=" + duanDianGuanXiID);
                            for (j = 0; j < dt1.Rows.Count; j++)
                            {
                                if (allCedian[dt1.Rows[j]["ceDianBianHao"].ToString()].FuDianFlag)
                                {
                                    flag = 1;
                                    break;
                                }
                            }
                            if (flag == 1)
                            {
                                dt2 = OperateDB.Select("select kongZhiCeDianBianHao from DuanDianGuanXi where duanDianGuanXiID=" + duanDianGuanXiID);
                                for (k = 0; k < dt2.Rows.Count; k++)
                                {
                                    newFenZhanHao = Convert.ToByte(dt2.Rows[k]["kongZhiCeDianBianHao"].ToString().Substring(0, 2));
                                    kongZhiLiangBianHao = Convert.ToByte(dt2.Rows[k]["kongZhiCeDianBianHao"].ToString().Substring(3, 2));
                                    if (ret.Contains(newFenZhanHao))
                                    {
                                        fudiankjna(newFenZhanHao, kongZhiLiangBianHao, ref oldfenzhan);
                                    }
                                    else
                                    {
                                        UDPComm.Send(ziDongControl(Type, newFenZhanHao, kongZhiLiangBianHao));
                                        GlobalParams.lastCutFenZhan = newFenZhanHao+"";
                                        GlobalParams.lastCutTongDao = kongZhiLiangBianHao+"";
                                        GlobalParams.lastCutType = 0;
                                        kkg = 1;

                                    }
                                }
                            }
                        }
                    }
                    if (oldfenzhan != null)
                    {
                        for (i = 0; i < oldfenzhan.Length; i++)
                        {
                            if (oldfenzhan[i].isuse)
                            {
                                UDPComm.Send(ziDongControlold(Type, oldfenzhan[i].number, oldfenzhan[i].one));
                                oldfenzhan[i].isuse = false;
                            }
                        }
                    }
                    if (kkg == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static byte[] ziDongControl(byte type, byte FenZhan, byte DuanKou)
        {
            return new byte[] { LogicCommon.startByte, FenZhan, 0x42, DuanKou, type, LogicCommon.endByte };
        }

        private static byte[] ziDongControlold(byte type, byte FenZhan, byte DuanKou)
        {
            return new byte[] { LogicCommon.startByte, FenZhan, 0x42, DuanKou, type, LogicCommon.endByte };
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct kjnafenzhan
        {
            public byte number;
            public bool isuse;
            public byte one;
        }
    }
}

