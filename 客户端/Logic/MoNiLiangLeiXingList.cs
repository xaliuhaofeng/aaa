namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;

    public class MoNiLiangLeiXingList
    {
        public Dictionary<string, MoNiLiangLeiXing> allmonileixinglist = new Dictionary<string, MoNiLiangLeiXing>();
        public List<string> list = new List<string>();

        public MoNiLiangLeiXingList()
        {
            this.allmonileixinglist.Clear();
            this.list.Clear();
        }

        public DataTable ConvertoDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("mingCheng");
            dt.Columns.Add("chuanGanQiLeiXing2");
            dt.Columns.Add("leiXing");
            dt.Columns.Add("danWei");
            dt.Columns.Add("guanJianZi");
            dt.Columns.Add("baoJingZhiShangXian");
            dt.Columns.Add("duanDianZhi");
            dt.Columns.Add("fuDianZhi");
            dt.Columns.Add("liangChengDi");
            dt.Columns.Add("liangChengGao");
            try
            {
                IEnumerable<KeyValuePair<string, MoNiLiangLeiXing>> query = from item in this.allmonileixinglist select item;
                foreach (KeyValuePair<string, MoNiLiangLeiXing> item in query)
                {
                    DataRow dr = dt.NewRow();
                    dr["mingCheng"] = item.Value.MingCheng;
                    dr["leiXing"] = item.Value.LeiXing;
                    dr["danWei"] = item.Value.DanWei;
                    dr["guanJianZi"] = item.Value.GuanJianZi;
                    dr["baoJingZhiShangXian"] = item.Value.BaoJingZhiShangXian;
                    dr["duanDianZhi"] = item.Value.DuanDianZhi;
                    dr["fuDianZhi"] = item.Value.FuDianZhi;
                    dr["liangChengDi"] = item.Value.LiangChengDi;
                    dr["liangChengGao"] = item.Value.LiangChengGao;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return dt;
        }

        public bool DeleteMnlLx(string mnlleixing)
        {
            if (!this.IsExistMnlLx(mnlleixing))
            {
                MessageBox.Show("无法删除该模拟量，该模拟量不存在");
                return false;
            }
            if (this.IsGLCeDian(mnlleixing))
            {
                MessageBox.Show("无法删除该模拟量，该模拟量存在关联");
                return false;
            }
            string sql = "delete from MoNiLiangLeiXing where mingCheng = '" + mnlleixing + "'";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            this.allmonileixinglist.Remove(mnlleixing);
            this.list.Remove(mnlleixing);
            return true;
        }

        public void FillMoNiLiangLeiXing()
        {
            string sql = "select * from MoNiLiangLeiXing order by id desc";
            DataTable dt = OperateDB.Select(sql);
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                Console.WriteLine("不存在的模拟量类型！");
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MoNiLiangLeiXing temp;
                    MoNiLiangLeiXing mnlx = new MoNiLiangLeiXing {
                        MingCheng = dt.Rows[i]["mingCheng"].ToString().Trim(),
                        Id = Convert.ToInt32(dt.Rows[i]["id"]),
                        LeiXing = Convert.ToInt32(dt.Rows[i]["leiXing"]),
                        DanWei = dt.Rows[i]["danWei"].ToString().Trim(),
                        GuanJianZi = dt.Rows[i]["guanJianZi"].ToString().Trim(),
                        XianXingShuXing = Convert.ToBoolean(dt.Rows[i]["xianXingShuXing"]),
                        LiangChengDi = Convert.ToSingle(dt.Rows[i]["liangChengDi"]),
                        LiangChengGao = Convert.ToSingle(dt.Rows[i]["liangChengGao"]),
                        BaoJingZhiShangXian = Convert.ToSingle(dt.Rows[i]["baoJingZhiShangXian"]),
                        BaoJingZhiXiaXian = Convert.ToSingle(dt.Rows[i]["baoJingZhiXiaXian"]),
                        DuanDianZhi = Convert.ToSingle(dt.Rows[i]["duanDianZhi"]),
                        FuDianZhi = Convert.ToSingle(dt.Rows[i]["fuDianZhi"]),
                        BaoJingLeiXing = Convert.ToByte(dt.Rows[i]["baoJingLeiXing"]),
                        FeiXianXingGuanXi = dt.Rows[i]["feiXianXingGuanXi"].ToString().Trim(),
                        WuChaDai = Convert.ToInt32(dt.Rows[i]["wuChaDai"]),
                        BaoJingShengYin = dt.Rows[i]["baoJingShengYin"].ToString().Trim(),
                        BaoJingTuBiao = dt.Rows[i]["baoJingTuBiao"].ToString().Trim()
                    };
                    if (!this.allmonileixinglist.TryGetValue(mnlx.MingCheng, out temp))
                    {
                        this.allmonileixinglist.Add(mnlx.MingCheng, mnlx);
                        this.list.Add(mnlx.MingCheng);
                    }
                }
            }
        }

        public int GetLeiXingByMingCheng(string mingcheng)
        {
            int chuanGanLeiXing = 0;
            IEnumerable<KeyValuePair<string, MoNiLiangLeiXing>> query = from item in this.allmonileixinglist
                where item.Value.MingCheng == mingcheng
                select item;
            foreach (KeyValuePair<string, MoNiLiangLeiXing> item in query)
            {
                chuanGanLeiXing = item.Value.LeiXing;
            }
            return chuanGanLeiXing;
        }

        public DataTable GetMo()
        {
            Dictionary<string, MoNiLiangLeiXing> list = new Dictionary<string, MoNiLiangLeiXing>();
            IEnumerable<KeyValuePair<string, MoNiLiangLeiXing>> query = from item in GlobalParams.AllmnlLeiXing.allmonileixinglist
                where (item.Value.LeiXing == 1) || (item.Value.LeiXing == 5)
                select item;
            if (query.Count<KeyValuePair<string, MoNiLiangLeiXing>>() > 0)
            {
                foreach (KeyValuePair<string, MoNiLiangLeiXing> item in query)
                {
                    list.Add(item.Key, item.Value);
                }
                return this.ListtoDataTable(list);
            }
            return new DataTable();
        }

        public MoNiLiangLeiXing GetMoNiLiang(string name)
        {
            MoNiLiangLeiXing m_mnllx = null;
            if (this.allmonileixinglist.ContainsKey(name))
            {
                IOrderedEnumerable<KeyValuePair<string, MoNiLiangLeiXing>> query = from item in this.allmonileixinglist
                    where item.Key == name
                    orderby item.Key
                    select item;
                foreach (KeyValuePair<string, MoNiLiangLeiXing> item in query)
                {
                    m_mnllx = item.Value;
                }
            }
            return m_mnllx;
        }

        public List<MoNiLiangLeiXing> GetMoNiLiangList(byte LeiXing)
        {
            IEnumerable<KeyValuePair<string, MoNiLiangLeiXing>> query;
            List<MoNiLiangLeiXing> mnl_list = new List<MoNiLiangLeiXing>();
            if (LeiXing == 0)
            {
                query = from item in this.allmonileixinglist
                    where (item.Value.LeiXing == 1) || (item.Value.LeiXing == 5)
                    select item;
                if (query.Count<KeyValuePair<string, MoNiLiangLeiXing>>() > 0)
                {
                    foreach (KeyValuePair<string, MoNiLiangLeiXing> item in query)
                    {
                        mnl_list.Add(item.Value);
                    }
                }
                return mnl_list;
            }
            query = from item in this.allmonileixinglist
                where item.Value.LeiXing == 2
                select item;
            foreach (KeyValuePair<string, MoNiLiangLeiXing> item in query)
            {
                mnl_list.Add(item.Value);
            }
            return mnl_list;
        }

        public bool InsertMnlLx(byte LeiXing, string TuBiao, string MingCheng, string DanWei, string GuanJianZi, string XianXingShuXing, string FeiXianXingGuanXi, string WuChaDai, string BaoJingZhiShangXian, string BaoJingZhiXiaXian, string BaoJingLeiXing, string LiangChengGao, string LiangChengDi, string DuanDianZhi, string FuDianZhi)
        {
            if (this.IsExistMnlLx(MingCheng))
            {
                MessageBox.Show("该模拟量名称已存在，请重新命名");
                return false;
            }
            string sql = string.Concat(new object[] { 
                "insert into MoNiLiangLeiXing ( leiXing, baoJingTuBiao, mingCheng, danWei, guanJianZi, xianXingShuXing,feiXianXingGuanXi, wuChaDai, baoJingZhiShangXian,baoJingZhiXiaXian, baoJingLeiXing, liangChengGao, liangChengDi, duanDianZhi, fuDianZhi) values ('", LeiXing, "', '", TuBiao, "', '", MingCheng, "', '", DanWei, "', '", GuanJianZi, "', '", XianXingShuXing, "', '", FeiXianXingGuanXi, "', '", WuChaDai, 
                "', '", BaoJingZhiShangXian, "', '", BaoJingZhiXiaXian, "', '", BaoJingLeiXing, "', '", LiangChengGao, "', '", LiangChengDi, "', '", DuanDianZhi, "', '", FuDianZhi, "')"
             });
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            MoNiLiangLeiXing mnl = new MoNiLiangLeiXing(MingCheng);
            this.allmonileixinglist.Add(MingCheng, mnl);
            this.list.Add(MingCheng);
            return true;
        }

        public bool IsExistMnlLx(string mnlleixing)
        {
            return this.allmonileixinglist.ContainsKey(mnlleixing);
        }

        public bool IsGLCeDian(string mnlleixing)
        {
            return ((from item in GlobalParams.AllCeDianList.allcedianlist
                where item.Value.XiaoleiXing == mnlleixing
                select item).Count<KeyValuePair<string, CeDian>>() > 0);
        }

        public DataTable ListtoDataTable(Dictionary<string, MoNiLiangLeiXing> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("mingCheng");
            dt.Columns.Add("duanDianZhi");
            dt.Columns.Add("fuDianZhi");
            try
            {
                IEnumerable<KeyValuePair<string, MoNiLiangLeiXing>> query = from item in list select item;
                foreach (KeyValuePair<string, MoNiLiangLeiXing> item in query)
                {
                    DataRow dr = dt.NewRow();
                    dr["mingCheng"] = item.Value.MingCheng;
                    dr["duanDianZhi"] = item.Value.DuanDianZhi;
                    dr["fuDianZhi"] = item.Value.FuDianZhi;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return dt;
        }

        public bool UpdateMnlLx(string TuBiao, string MingCheng, string DanWei, string GuanJianZi, string XianXingShuXing, string FeiXianXingGuanXi, string WuChaDai, string BaoJingZhiShangXian, string BaoJingZhiXiaXian, string BaoJingLeiXing, string LiangChengGao, string LiangChengDi, string DuanDianZhi, string FuDianZhi, string OriginalMingCheng)
        {
            bool flag = this.IsExistMnlLx(OriginalMingCheng);
            if (flag)
            {
                string sql = "update MoNiLiangLeiXing set baoJingTuBiao = '" + TuBiao + "', mingCheng = '" + MingCheng + "',danWei = '" + DanWei + "',guanJianZi = '" + GuanJianZi + "',xianXingShuXing= '" + XianXingShuXing + "',feiXianXingGuanXi= '" + FeiXianXingGuanXi + "',wuChaDai= '" + WuChaDai + "',baoJingZhiShangXian = '" + BaoJingZhiShangXian + "',baoJingZhiXiaXian= '" + BaoJingZhiXiaXian + "',baoJingLeiXing= '" + BaoJingLeiXing + "',liangChengGao= '" + LiangChengGao + "',liangChengDi= '" + LiangChengDi + "',duanDianZhi= '" + DuanDianZhi + "',fuDianZhi= '" + FuDianZhi + "' where mingCheng = '" + OriginalMingCheng + "'";
                try
                {
                    OperateDB.Execute(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                MoNiLiangLeiXing mnl = new MoNiLiangLeiXing(MingCheng);
                this.allmonileixinglist.Remove(OriginalMingCheng);
                this.allmonileixinglist.Add(MingCheng, mnl);
                this.list.Remove(OriginalMingCheng);
                this.list.Add(MingCheng);
                return flag;
            }
            MessageBox.Show("数据库中没有该名称");
            return flag;
        }
    }
}

