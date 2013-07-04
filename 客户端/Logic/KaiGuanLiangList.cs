namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;

    public class KaiGuanLiangList
    {
        public Dictionary<string, KaiGuanLiangLeiXing> allkaiguanleixinglist = new Dictionary<string, KaiGuanLiangLeiXing>();
        public List<string> list = new List<string>();

        public KaiGuanLiangList()
        {
            this.allkaiguanleixinglist.Clear();
            this.list.Clear();
        }

        public void Clear()
        {
            this.allkaiguanleixinglist.Clear();
            this.list.Clear();
        }

        public bool DeleteKglLx(string lxname)
        {
            if (!this.IsExistKgl(lxname))
            {
                MessageBox.Show("无法删除该开关量，该开关量不存在");
                return false;
            }
            if (this.IsGLCeDian(lxname))
            {
                MessageBox.Show("无法删除该开关量，存在测点关联");
                return false;
            }
            string sql = "delete from KaiGuanLiangLeiXing where mingCheng = '" + lxname + "'";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.allkaiguanleixinglist.Remove(lxname);
            this.list.Remove(lxname);
            return true;
        }

        public void FillKaiGuanLiangLeiXing()
        {
            string sql = "select * from KaiGuanLiangLeiXing order by id desc";
            DataTable dt = OperateDB.Select(sql);
            if (dt == null) 
            {
               
                    throw new Exception("不存在的开关量类型！");
                
            }
            else if (dt.Rows.Count == 0)
            {
               
                    throw new Exception("不存在的开关量类型！");               

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    KaiGuanLiangLeiXing temp;
                    KaiGuanLiangLeiXing m_kgl = new KaiGuanLiangLeiXing
                    {
                        MingCheng = dt.Rows[i]["mingCheng"].ToString(),
                        LeiXing = Convert.ToInt32(dt.Rows[i]["leiXIng"]),
                        LingTai = dt.Rows[i]["lingTaiMingCheng"].ToString(),
                        YiTai = dt.Rows[i]["yiTaiMingCheng"].ToString(),
                        Id = Convert.ToInt32(dt.Rows[i]["id"]),
                        ZhengChangTuBiao = dt.Rows[i]["zhengChangTuBiao"].ToString(),
                        BaoJingShengYin = dt.Rows[i]["baoJingShengYin"].ToString()
                    };
                    if (dt.Rows[i]["erTaiMingCheng"] == null)
                    {
                        m_kgl.ErTai = string.Empty;
                    }
                    else
                    {
                        m_kgl.ErTai = dt.Rows[i]["erTaiMingCheng"].ToString();
                    }
                    m_kgl.ShiFouBaoJing = Convert.ToBoolean(dt.Rows[i]["shiFouBaoJing"]);
                    if (dt.Rows[i]["baoJingZhuangTai"] == null)
                    {
                        m_kgl.BaoJingZhuangTai = -1;
                    }
                    else
                    {
                        m_kgl.BaoJingZhuangTai = Convert.ToInt32(dt.Rows[i]["baoJingZhuangTai"]);
                    }
                    m_kgl.ShiFouDuanDian = Convert.ToBoolean(dt.Rows[i]["shiFouDuanDian"]);
                    if (dt.Rows[i]["duanDianZhuangTai"] == null)
                    {
                        m_kgl.DuanDianZhuangTai = -1;
                    }
                    else
                    {
                        m_kgl.DuanDianZhuangTai = Convert.ToInt32(dt.Rows[i]["duanDianZhuangTai"]);
                    }
                    if (!this.allkaiguanleixinglist.TryGetValue(m_kgl.MingCheng, out temp))
                    {
                        this.allkaiguanleixinglist.Add(m_kgl.MingCheng, m_kgl);
                        this.list.Add(m_kgl.MingCheng);
                    }
                }
            }
        }

        public string[] GetAllMingCheng()
        {
            return this.list.ToArray();
        }

        public byte[] GetConfigInfo(string xiaoLeiXing, byte chuanGanQiLeiXing, string ceDianBianHao)
        {
            byte[] b = new byte[10];
            KaiGuanLiangLeiXing m_kgllx = null;
            m_kgllx = this.GetKaiGuanLiang(xiaoLeiXing);
            byte leixing = Convert.ToByte(m_kgllx.LeiXing);
            bool baojing = Convert.ToBoolean(m_kgllx.ShiFouBaoJing);
            bool duandian = Convert.ToBoolean(m_kgllx.ShiFouDuanDian);
            b[0] = 0;
            if (leixing == 4)
            {
                b[1] = 2;
            }
            else
            {
                b[1] = 1;
            }
            if (baojing)
            {
                byte baojingstate = Convert.ToByte(m_kgllx.BaoJingZhuangTai);
                b[2] = 0;
                b[3] = baojingstate;
            }
            else
            {
                b[2] = 0xff;
                b[3] = 0xff;
            }
            if (duandian)
            {
                byte duandianstate = Convert.ToByte(m_kgllx.DuanDianZhuangTai);
                b[4] = 0;
                b[5] = duandianstate;
            }
            else
            {
                b[4] = 0xff;
                b[5] = 0xff;
            }
            b[6] = 0xff;
            b[7] = 0xff;
            b[8] = DuanDianGuanXi.GetConfInfoByCeDianBianHao(ceDianBianHao);
            b[9] = (byte) ((Convert.ToByte(m_kgllx.LeiXing) << 5) | chuanGanQiLeiXing);
            return b;
        }

        public KaiGuanLiangLeiXing GetKaiGuanLiang(string name)
        {
            KaiGuanLiangLeiXing m_kgllx = null;
            if (this.allkaiguanleixinglist.ContainsKey(name))
            {
                IOrderedEnumerable<KeyValuePair<string, KaiGuanLiangLeiXing>> query = from item in this.allkaiguanleixinglist
                    where item.Key == name
                    orderby item.Key
                    select item;
                foreach (KeyValuePair<string, KaiGuanLiangLeiXing> item in query)
                {
                    m_kgllx = item.Value;
                }
            }
            return m_kgllx;
        }

        public int GetLeiXingByMingCheng(string mingcheng)
        {
            int chuanGanLeiXing = 0;
            IEnumerable<KeyValuePair<string, KaiGuanLiangLeiXing>> query = from item in this.allkaiguanleixinglist
                where item.Value.MingCheng == mingcheng
                select item;
            foreach (KeyValuePair<string, KaiGuanLiangLeiXing> item in query)
            {
                chuanGanLeiXing = item.Value.LeiXing;
            }
            return chuanGanLeiXing;
        }

        public List<KaiGuanLiangLeiXing> GetSwitch(byte LeiXing)
        {
            IEnumerable<KeyValuePair<string, KaiGuanLiangLeiXing>> query;
            List<KaiGuanLiangLeiXing> kgllx = new List<KaiGuanLiangLeiXing>();
            if ((LeiXing == 3) || (LeiXing == 7))
            {
                query = from item in this.allkaiguanleixinglist
                    where item.Value.LeiXing == LeiXing
                    select item;
                foreach (KeyValuePair<string, KaiGuanLiangLeiXing> item in query)
                {
                    kgllx.Add(item.Value);
                }
                return kgllx;
            }
            if (LeiXing == 4)
            {
                query = from item in this.allkaiguanleixinglist
                    where item.Value.LeiXing == LeiXing
                    select item;
                foreach (KeyValuePair<string, KaiGuanLiangLeiXing> item in query)
                {
                    kgllx.Add(item.Value);
                }
                return kgllx;
            }
            query = from item in this.allkaiguanleixinglist
                where item.Value.LeiXing == LeiXing
                select item;
            foreach (KeyValuePair<string, KaiGuanLiangLeiXing> item in query)
            {
                kgllx.Add(item.Value);
            }
            return kgllx;
        }

        public List<KaiGuanLiangLeiXing> GetSwitch2()
        {
            List<KaiGuanLiangLeiXing> all_KGLleiXing = new List<KaiGuanLiangLeiXing>();
            IEnumerable<KeyValuePair<string, KaiGuanLiangLeiXing>> query = from item in GlobalParams.AllkgLeiXing.allkaiguanleixinglist select item;
            foreach (KeyValuePair<string, KaiGuanLiangLeiXing> item in query)
            {
                all_KGLleiXing.Add(item.Value);
            }
            return all_KGLleiXing;
        }

        public KaiGuanLiangLeiXing GetSwitchAlarm(string ceDianBianHao)
        {
            KaiGuanLiangLeiXing m_kgllx = null;
            bool tmp_flag = GlobalParams.AllCeDianList.allcedianlist.ContainsKey(ceDianBianHao);
            if (tmp_flag)
            {
                var query = from item in GlobalParams.AllCeDianList.allcedianlist
                            where item.Value.CeDianBianHao == ceDianBianHao
                            select item;
                foreach (var item in query)
                {
                    var qu = from a in allkaiguanleixinglist
                             where a.Key == item.Value.XiaoleiXing
                             select a;
                    foreach (var a in qu)
                    {
                        m_kgllx = a.Value;
                    }

                }
            }
            return m_kgllx;
        }

        public bool InsertKglLx(byte LeiXing, string TuBiao, string MingCheng, string LingTai, string YiTai, string ErTai, string BaoJingZhuangTai, string shiFouBaoJing, string ShiFouDuanDian, string DuanDianZhuangTai)
        {
            if (this.IsExistKgl(MingCheng))
            {
                MessageBox.Show("该测点已经存在。");
                return false;
            }
            string sql = string.Concat(new object[] { 
                "insert into KaiGuanLiangLeiXing (leiXing,zhengchangtubiao,mingcheng,lingtaimingcheng,yitaimingcheng,ertaimingcheng,baojingzhuangtai,shifoubaojing, shifouduandian,duandianzhuangtai) values ('", LeiXing, "', '", TuBiao, "', '", MingCheng, "', '", LingTai, "', '", YiTai, "', '", ErTai, "', '", BaoJingZhuangTai, "', '", shiFouBaoJing, 
                "', '", ShiFouDuanDian, "', '", DuanDianZhuangTai, "')"
             });
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            KaiGuanLiangLeiXing kgllx = new KaiGuanLiangLeiXing(MingCheng);
            this.allkaiguanleixinglist.Add(MingCheng, kgllx);
            this.list.Add(MingCheng);
            return true;
        }

        public bool IsExistKgl(string lxname)
        {
            return this.allkaiguanleixinglist.ContainsKey(lxname);
        }

        public bool IsGLCeDian(string lxname)
        {
            return ((from item in GlobalParams.AllCeDianList.allcedianlist
                where item.Value.XiaoleiXing == lxname
                select item).Count<KeyValuePair<string, CeDian>>() > 0);
        }

        public DataTable ListConvertDataTable()
        {
            List<KaiGuanLiangLeiXing> kgl_list = this.GetSwitch2();
            DataTable dt = new DataTable();
            dt.Columns.Add("mingCheng");
            dt.Columns.Add("leiXIng");
            dt.Columns.Add("lingTaiMingCheng");
            dt.Columns.Add("yiTaiMingCheng");
            dt.Columns.Add("erTaiMingCheng");
            dt.Columns.Add("baoJingZhuangTai");
            dt.Columns.Add("duanDianZhuangTai");
            try
            {
                KaiGuanLiangLeiXing kgl = new KaiGuanLiangLeiXing();
                if ((kgl_list != null) || (kgl_list.Count > 0))
                {
                    for (int i = 0; i < kgl_list.Count; i++)
                    {
                        kgl = kgl_list[i];
                        DataRow dr = dt.NewRow();
                        dr["mingCheng"] = kgl.MingCheng;
                        dr["leiXIng"] = kgl.LeiXing;
                        dr["lingTaiMingCheng"] = kgl.LingTai;
                        dr["yiTaiMingCheng"] = kgl.YiTai;
                        dr["erTaiMingCheng"] = kgl.ErTai;
                        dr["baoJingZhuangTai"] = kgl.BaoJingZhuangTai;
                        dr["duanDianZhuangTai"] = kgl.DuanDianZhuangTai;
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            dt.Dispose();
            return dt;
        }

        public bool XiuGaiLX(string TuBiao, string MingCheng, string LingTai, string YiTai, string ErTai, string BaoJingZhuangTai, string shiFouBaoJing, string ShiFouDuanDian, string DuanDianZhuangTai, string OriginalMingCheng)
        {
            bool flag = this.IsExistKgl(OriginalMingCheng);
            if (flag)
            {
                string sql = "update KaiGuanLiangLeiXing set zhengChangTuBiao = '" + TuBiao + "', mingCheng = '" + MingCheng + "',lingtaimingcheng = '" + LingTai + "',yitaimingcheng = '" + YiTai + "',ertaimingcheng= '" + ErTai + "',baojingzhuangtai= '" + BaoJingZhuangTai + "',shifoubaojing = '" + shiFouBaoJing + "',shifouduandian = '" + ShiFouDuanDian + "',duandianzhuangtai= '" + DuanDianZhuangTai + "' where mingCheng = '" + OriginalMingCheng + "'";
                try
                {
                    OperateDB.Execute(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                KaiGuanLiangLeiXing kgl = new KaiGuanLiangLeiXing(MingCheng);
                this.allkaiguanleixinglist.Remove(OriginalMingCheng);
                this.allkaiguanleixinglist.Add(MingCheng, kgl);
                this.list.Remove(OriginalMingCheng);
                this.list.Add(MingCheng);
                return flag;
            }
            MessageBox.Show("数据库中没有该名称");
            return flag;
        }
    }
}

