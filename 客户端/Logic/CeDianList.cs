namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class CeDianList
    {
        public Dictionary<string, CeDian> allcedianlist = new Dictionary<string, CeDian>();
        public List<string> bhlist = new List<string>();

        public CeDianList()
        {
            this.allcedianlist.Clear();
            this.bhlist.Clear();
        }

        public bool CanDelete(string xiaoLeiXing)
        {
            return ((from item in GlobalParams.AllCeDianList.allcedianlist
                where item.Value.XiaoleiXing == xiaoLeiXing
                select item).Count<KeyValuePair<string, CeDian>>() > 0);
        }

        public string CeDianLeiXingStr(string cedianbianhao)
        {
            CeDian cedian = this.allcedianlist[cedianbianhao];
            return cedian.XiaoleiXing.ToString();
        }

        public string ComposeCeDianBianHao(int fenzhan, int tongdao, int type)
        {
            string fenZhan = fenzhan.ToString();
            if (fenZhan.Length == 1)
            {
                fenZhan = "0" + fenZhan;
            }
            string tongDao = tongdao.ToString();
            if (tongDao.Length == 1)
            {
                tongDao = "0" + tongDao;
            }
            if (type == 0)
            {
                return (fenZhan + "A" + tongDao);
            }
            if (type == 1)
            {
                return (fenZhan + "D" + tongDao);
            }
            if (type == 2)
            {
                return (fenZhan + "C" + tongDao);
            }
            return (fenZhan + "F" + tongDao);
        }

        public bool CreateCeDian(string WeiZhi, string XiaoLeiXing, string FenZhanHao, string TongDaoHao, string CeDianBianHao)
        {
            if (this.ReCeDian(CeDianBianHao))
            {
                MessageBox.Show("已经存在此测点编号，不能插入。");
                return false;
            }
            string sql = "insert into KongZhiLiangCeDian (cedianweizhi, mingCheng, fenzhanhao, kongZhiLiangBianHao,cedianbianhao,weishanchu,createDate) values ('" + WeiZhi + "', '" + XiaoLeiXing + "', '" + FenZhanHao + "', '" + TongDaoHao + "', '" + CeDianBianHao + "',1,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            CeDian cedian = new CeDian(CeDianBianHao);
            this.allcedianlist.Add(CeDianBianHao, cedian);
            this.bhlist.Add(CeDianBianHao);
            return true;
        }

        public bool CreateCeDian(string LeiXing, string WeiZhi, string XiaoLeiXing, string ChuanGanQiLeiXing, string FenZhanHao, string TongDaoHao, string CeDianBianHao, string fenZhanLeiXing)
        {
            if (this.ReCeDian(CeDianBianHao))
            {
                MessageBox.Show("已经存在此测点编号，不能插入。");
                return false;
            }
            string sql = "insert into CeDian (daleiXing,cedianweizhi, xiaoleixing, chuanganqileixing, fenzhanhao, tongdaohao, cedianbianhao, chuanGanQiZhiShi,weishanchu,createDate) values ('" + LeiXing + "', '" + WeiZhi + "', '" + XiaoLeiXing + "', '" + ChuanGanQiLeiXing + "', '" + FenZhanHao + "', '" + TongDaoHao + "', '" + CeDianBianHao + "','" + fenZhanLeiXing + "',1,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            CeDian cedian = new CeDian(CeDianBianHao);
            this.allcedianlist.Add(CeDianBianHao, cedian);
            this.bhlist.Add(CeDianBianHao);
            return true;
        }

        public bool DelCeDian(string cedianbianhao)
        {
            bool flag = this.ReCeDian(cedianbianhao);
            if (flag)
            {
                string sql = "update CeDian set weishanchu=0,deleteDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where cedianbianhao = '" + cedianbianhao + "'";
                try
                {
                    OperateDB.Execute(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                this.allcedianlist.Remove(cedianbianhao);
                this.bhlist.Remove(cedianbianhao);
                return flag;
            }
            return flag;
        }

        public bool DelCeDianKZL(string cedianbianhao)
        {
            bool flag = this.ReCeDian(cedianbianhao);
            if (flag)
            {
                string sql = "update KongZhiLiangCeDian set weishanchu=0,deleteDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where cedianbianhao = '" + cedianbianhao + "'";
                try
                {
                    OperateDB.Execute(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                this.allcedianlist.Remove(cedianbianhao);
                this.bhlist.Remove(cedianbianhao);
                return flag;
            }
            MessageBox.Show("此测点编号不存在，无法删除");
            return flag;
        }

        public void FillAllCeDian()
        {
            string sql = "select * from CeDian where weishanchu=1 order by cedianbianhao,id desc";
            DataTable dt = OperateDB.Select(sql);
            if ((dt != null) && (dt.Rows.Count != 0))
            {
                int tmpLx;
                int i;
                string tmpLxName;
                string bh;
                CeDian CurCeDian;
                CeDian ctemp;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    CurCeDian = new CeDian();
                    tmpLx = Convert.ToInt32(dt.Rows[i]["daLeiXing"]);
                    CurCeDian.DaLeiXing = tmpLx;
                    tmpLxName = dt.Rows[i]["xiaoLeiXing"].ToString();
                    CurCeDian.XiaoleiXing = tmpLxName;
                    CurCeDian.ChuanGanQiLeiXing = Convert.ToInt32(dt.Rows[i]["chuanGanQiLeiXing"]);
                    CurCeDian.ChuanGanQiZhiShi = dt.Rows[i]["chuanGanQiZhiShi"].ToString();
                    CurCeDian.CeDianWeiZhi = dt.Rows[i]["ceDianWeiZhi"].ToString();
                    CurCeDian.FenZhanHao = Convert.ToInt32(dt.Rows[i]["fenZhanHao"]);
                    CurCeDian.TongDaoHao = Convert.ToInt32(dt.Rows[i]["tongDaoHao"]);
                    CurCeDian.WeiShanChu = Convert.ToBoolean(dt.Rows[i]["weiShanChu"]);
                    CurCeDian.TiaoJiao = Convert.ToBoolean(dt.Rows[i]["tiaoJiao"]);
                    CurCeDian.BaoJing = Convert.ToBoolean(dt.Rows[i]["baoJing"]);
                    CurCeDian.CreateDate = Convert.ToDateTime(dt.Rows[i]["createDate"]);
                    CurCeDian.Id = Convert.ToInt32(dt.Rows[i]["id"]);
                    bh = dt.Rows[i]["cedianbianhao"].ToString().Trim();
                    CurCeDian.CeDianBianHao = bh;
                    CurCeDian.DuanDianFlag = false;
                    CurCeDian.FuDianFlag = true;
                    CurCeDian.IsAlarm = false;
                    CurCeDian.RtState_pre = 100000;
                    CurCeDian.RtVal_pre = 10000f;
                    CurCeDian.UpdateDDGX = false;
                    CurCeDian.IsMultiBaoji = false;
                    if (tmpLx == 0)
                    {
                        CurCeDian.MoNiLiang = GlobalParams.AllmnlLeiXing.GetMoNiLiang(tmpLxName);
                    }
                    else
                    {
                        CurCeDian.KaiGuanLiang = GlobalParams.AllkgLeiXing.GetKaiGuanLiang(tmpLxName);
                    }
                    if (!this.allcedianlist.TryGetValue(bh, out ctemp))
                    {
                        this.allcedianlist.Add(bh, CurCeDian);
                        this.bhlist.Add(bh);
                    }
                    else
                    {
                        ctemp.RtState_pre = 100000;
                        ctemp.RtVal_pre = 10000f;                        
                    }

                }
                i = 0;
                sql = "select * from KongZhiLiangCeDian where weiShanChu=1 order by ceDianBianHao,id desc";
                dt = OperateDB.Select(sql);
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    CurCeDian = new CeDian();
                    tmpLx = 2;
                    CurCeDian.DaLeiXing = tmpLx;
                    tmpLxName = dt.Rows[i]["mingCheng"].ToString();
                    CurCeDian.XiaoleiXing = tmpLxName;
                    CurCeDian.Id = Convert.ToInt32(dt.Rows[i]["id"]);
                    CurCeDian.CeDianWeiZhi = dt.Rows[i]["ceDianWeiZhi"].ToString();
                    CurCeDian.FenZhanHao = Convert.ToInt32(dt.Rows[i]["fenZhanHao"]);
                    CurCeDian.TongDaoHao = Convert.ToInt32(dt.Rows[i]["kongZhiLiangBianHao"]);
                    CurCeDian.WeiShanChu = Convert.ToBoolean(dt.Rows[i]["weiShanChu"]);
                    CurCeDian.CreateDate = Convert.ToDateTime(dt.Rows[i]["createDate"]);
                    CurCeDian.ChuanGanQiLeiXing = 9;
                    bh = dt.Rows[i]["cedianbianhao"].ToString().Trim();
                    CurCeDian.CeDianBianHao = bh;
                    CurCeDian.KongZhiLiang = GlobalParams.AllkzlLeiXing.GetKongZhiLiang(tmpLxName);
                    CurCeDian.RtState_pre = 100000;
                    CurCeDian.RtVal_pre = 10000f;
                    if (!this.allcedianlist.TryGetValue(bh, out ctemp))
                    {
                        this.allcedianlist.Add(bh, CurCeDian);
                        this.bhlist.Add(bh);
                    }
                    else
                    {
                        ctemp.RtState_pre = 100000;
                        ctemp.RtVal_pre = 10000f;
                    }

                }
            }
        }

        public byte[] GetAllConfInfoByFenZhan(int fenZhanNum)
        {
            byte[] b = new byte[160];
            for (int i = 1; i <= 0x10; i++)
            {
                byte[] temp = this.GetConInfoByTongDao(fenZhanNum, i);
                for (int j = 0; j < 10; j++)
                {
                    b[((i - 1) * 10) + j] = temp[j];
                }
            }
            return b;
        }

        public List<CeDian> GetCeDian(byte LeiXing)
        {
            IOrderedEnumerable<KeyValuePair<string, CeDian>> query;
            List<CeDian> cedianlist = new List<CeDian>();
            if ((LeiXing == 0) || (LeiXing == 1))
            {
                query = from item in this.allcedianlist
                    where (item.Value.DaLeiXing == LeiXing) && item.Value.WeiShanChu
                    orderby item.Value.CeDianBianHao
                    select item;
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    cedianlist.Add(item.Value);
                }
                return cedianlist;
            }
            if (LeiXing == 3)
            {
                query = from item in this.allcedianlist
                    where (item.Value.DaLeiXing == 0) || ((item.Value.DaLeiXing == 1) && item.Value.WeiShanChu)
                    orderby item.Value.CeDianBianHao
                    select item;
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    cedianlist.Add(item.Value);
                }
                return cedianlist;
            }
            try
            {
                query = from item in this.allcedianlist
                        where (item.Value.DaLeiXing == 2) && item.Value.WeiShanChu
                        orderby item.Value.CeDianBianHao
                        select item;
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    cedianlist.Add(item.Value);
                }
            }catch(Exception e){

            }
            return cedianlist;
        }

        public Dictionary<string, CeDian> GetCeDian2()
        {
            Dictionary<string, CeDian> list = new Dictionary<string, CeDian>();
            IOrderedEnumerable<KeyValuePair<string, CeDian>> query = from item in GlobalParams.AllCeDianList.allcedianlist
                where (item.Value.ChuanGanQiLeiXing == 1) || (item.Value.ChuanGanQiLeiXing == 5)
                orderby item.Value.CeDianBianHao
                select item;
            if (query.Count<KeyValuePair<string, CeDian>>() > 0)
            {
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    list.Add(item.Key, item.Value);
                }
                return list;
            }
            return list;
        }

        public DataTable GetCeDian3()
        {

            // 类型中没有通端量，此为临时添加的
            Dictionary<string, CeDian> list = new Dictionary<string, CeDian>();
            IOrderedEnumerable<KeyValuePair<string, CeDian>> query = from item in GlobalParams.AllCeDianList.allcedianlist
             where ((item.Value.ChuanGanQiLeiXing == 3) || (item.Value.ChuanGanQiLeiXing == 4) || (item.Value.ChuanGanQiLeiXing == 7))
                orderby item.Value.CeDianBianHao
                select item;
            if (query.Count<KeyValuePair<string, CeDian>>() > 0)
            {
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    list.Add(item.Key, item.Value);
                }
                return this.listtoDatatable(list);
            }
            return new DataTable();
        }

        public string[] getCeDianAllInfo(byte daLeiXing)
        {
            DataTable dt;
            string cedianbianhao;
            string info;
            List<string> list = new List<string>();
            if ((daLeiXing < 0) || (daLeiXing > 3))
            {
                throw new Exception("测点类型不对");
            }
            if ((daLeiXing == 0) || (daLeiXing == 1))
            {
                dt = this.ListConvertDataTable(daLeiXing);
                foreach (DataRow row in dt.Rows)
                {
                    cedianbianhao = row["ceDianBianHao"].ToString();
                    info = this.getCeDianAllInfo(cedianbianhao);
                    if (info != null)
                    {
                        list.Add(info);
                    }
                }
            }
            else if (daLeiXing == 2)
            {
                dt = this.ListConvertDataTable(daLeiXing);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row["ceDianBianHao"].ToString() + " " + row["ceDianWeiZhi"].ToString() + " " + row["mingCheng"].ToString());
                }
            }
            else if (daLeiXing == 3)
            {
                DataTable dt1 = this.ListConvertDataTable(3);
                foreach (DataRow row in dt1.Rows)
                {
                    cedianbianhao = row["ceDianBianHao"].ToString();
                    info = this.getCeDianAllInfo(cedianbianhao);
                    if (info != null)
                    {
                        list.Add(info);
                    }
                }
                DataTable dt2 = this.ListConvertDataTable(2);
                foreach (DataRow row in dt2.Rows)
                {
                    list.Add(row["ceDianBianHao"].ToString() + " " + row["ceDianWeiZhi"].ToString() + " " + row["xiaoleixing"].ToString());
                }
            }
            return list.ToArray();
        }

        public string getCeDianAllInfo(string cedianbianhao)
        {
            string info = null;
            if (GlobalParams.AllCeDianList.allcedianlist.ContainsKey(cedianbianhao))
            {
                CeDian cedian = GlobalParams.AllCeDianList.allcedianlist[cedianbianhao];
                info = cedianbianhao + " " + cedian.CeDianWeiZhi + " ";
                if (cedian.MoNiLiang != null)
                {
                    return (info + cedian.MoNiLiang.GuanJianZi + "-" + cedian.MoNiLiang.MingCheng);
                }
                if (cedian.KaiGuanLiang != null)
                {
                    return (info + cedian.KaiGuanLiang.MingCheng);
                }
                if (cedian.KongZhiLiang != null)
                {
                    info = info + cedian.KongZhiLiang.Mingcheng;
                }
            }
            return info;
        }

        public List<string> getCeDianAllInfo(int fenZhanHao, int daLeiXing, int tongDaoLeiXing, string xiaoLeiXing)
        {
            int fzh;
            int tdlx;
            int xlx;
            IOrderedEnumerable<KeyValuePair<string, CeDian>> query;
            string cedianbianhao;
            string info;
            List<string> cedianlist = new List<string>();
            if (daLeiXing != 3)
            {
                fzh = 0;
                tdlx = 0;
                xlx = 0;
                if (fenZhanHao != 0)
                {
                    fzh = 1;
                }
                if (tongDaoLeiXing != -1)
                {
                    tdlx = 1;
                }
                if (xiaoLeiXing != "全部")
                {
                    xlx = 1;
                }
                if (((fzh == 0) && (tdlx == 0)) && (xlx == 0))
                {
                    query = from item in this.allcedianlist
                        where (item.Value.DaLeiXing == daLeiXing) && item.Value.WeiShanChu
                        orderby item.Value.CeDianBianHao
                        select item;
                    foreach (KeyValuePair<string, CeDian> item in query)
                    {
                        cedianbianhao = item.Value.CeDianBianHao.Trim();
                        info = this.getCeDianAllInfo(cedianbianhao);
                        if (info != null)
                        {
                            cedianlist.Add(info);
                        }
                    }
                }
                if (((fzh == 0) && (tdlx == 0)) && (xlx == 1))
                {
                    query = from item in this.allcedianlist
                        where ((item.Value.DaLeiXing == daLeiXing) && item.Value.WeiShanChu) && (item.Value.XiaoleiXing == xiaoLeiXing)
                        orderby item.Value.CeDianBianHao
                        select item;
                    foreach (KeyValuePair<string, CeDian> item in query)
                    {
                        cedianbianhao = item.Value.CeDianBianHao.Trim();
                        info = this.getCeDianAllInfo(cedianbianhao);
                        if (info != null)
                        {
                            cedianlist.Add(info);
                        }
                    }
                }
                if (((fzh == 0) && (tdlx == 1)) && (xlx == 0))
                {
                    query = from item in this.allcedianlist
                        where ((item.Value.DaLeiXing == daLeiXing) && item.Value.WeiShanChu) && (item.Value.ChuanGanQiLeiXing == tongDaoLeiXing)
                        orderby item.Value.CeDianBianHao
                        select item;
                    foreach (KeyValuePair<string, CeDian> item in query)
                    {
                        cedianbianhao = item.Value.CeDianBianHao.Trim();
                        info = this.getCeDianAllInfo(cedianbianhao);
                        if (info != null)
                        {
                            cedianlist.Add(info);
                        }
                    }
                }
                if (((fzh == 0) && (tdlx == 1)) && (xlx == 1))
                {
                    query = from item in this.allcedianlist
                        where (((item.Value.DaLeiXing == daLeiXing) && item.Value.WeiShanChu) && (item.Value.ChuanGanQiLeiXing == tongDaoLeiXing)) && (item.Value.XiaoleiXing == xiaoLeiXing)
                        orderby item.Value.CeDianBianHao
                        select item;
                    foreach (KeyValuePair<string, CeDian> item in query)
                    {
                        cedianbianhao = item.Value.CeDianBianHao.Trim();
                        info = this.getCeDianAllInfo(cedianbianhao);
                        if (info != null)
                        {
                            cedianlist.Add(info);
                        }
                    }
                }
                if (((fzh == 1) && (tdlx == 0)) && (xlx == 0))
                {
                    query = from item in this.allcedianlist
                        where ((item.Value.DaLeiXing == daLeiXing) && item.Value.WeiShanChu) && (item.Value.FenZhanHao == fenZhanHao)
                        orderby item.Value.CeDianBianHao
                        select item;
                    foreach (KeyValuePair<string, CeDian> item in query)
                    {
                        cedianbianhao = item.Value.CeDianBianHao.Trim();
                        info = this.getCeDianAllInfo(cedianbianhao);
                        if (info != null)
                        {
                            cedianlist.Add(info);
                        }
                    }
                }
                if (((fzh == 1) && (tdlx == 0)) && (xlx == 1))
                {
                    query = from item in this.allcedianlist
                        where (((item.Value.DaLeiXing == daLeiXing) && item.Value.WeiShanChu) && (item.Value.FenZhanHao == fenZhanHao)) && (item.Value.XiaoleiXing == xiaoLeiXing)
                        orderby item.Value.CeDianBianHao
                        select item;
                    foreach (KeyValuePair<string, CeDian> item in query)
                    {
                        cedianbianhao = item.Value.CeDianBianHao.Trim();
                        info = this.getCeDianAllInfo(cedianbianhao);
                        if (info != null)
                        {
                            cedianlist.Add(info);
                        }
                    }
                }
                if (((fzh == 1) && (tdlx == 1)) && (xlx == 0))
                {
                    query = from item in this.allcedianlist
                        where (((item.Value.DaLeiXing == daLeiXing) && item.Value.WeiShanChu) && (item.Value.FenZhanHao == fenZhanHao)) && (item.Value.ChuanGanQiLeiXing == tongDaoLeiXing)
                        orderby item.Value.CeDianBianHao
                        select item;
                    foreach (KeyValuePair<string, CeDian> item in query)
                    {
                        cedianbianhao = item.Value.CeDianBianHao.Trim();
                        info = this.getCeDianAllInfo(cedianbianhao);
                        if (info != null)
                        {
                            cedianlist.Add(info);
                        }
                    }
                }
                if (((fzh == 1) && (tdlx == 1)) && (xlx == 1))
                {
                    query = from item in this.allcedianlist
                        where (((item.Value.DaLeiXing == daLeiXing) && item.Value.WeiShanChu) && ((item.Value.FenZhanHao == fenZhanHao) && (item.Value.ChuanGanQiLeiXing == tongDaoLeiXing))) && (item.Value.XiaoleiXing == xiaoLeiXing)
                        orderby item.Value.CeDianBianHao
                        select item;
                    foreach (KeyValuePair<string, CeDian> item in query)
                    {
                        cedianbianhao = item.Value.CeDianBianHao.Trim();
                        info = this.getCeDianAllInfo(cedianbianhao);
                        if (info != null)
                        {
                            cedianlist.Add(info);
                        }
                    }
                }
                return cedianlist;
            }
            fzh = 0;
            tdlx = 0;
            xlx = 0;
            if (fenZhanHao != 0)
            {
                fzh = 1;
            }
            if (tongDaoLeiXing != -1)
            {
                tdlx = 1;
            }
            if (xiaoLeiXing != "全部")
            {
                xlx = 1;
            }
            if (((fzh == 0) && (tdlx == 0)) && (xlx == 0))
            {
                query = from item in this.allcedianlist
                    where item.Value.WeiShanChu
                    orderby item.Value.CeDianBianHao
                    select item;
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    cedianbianhao = item.Value.CeDianBianHao.Trim();
                    info = this.getCeDianAllInfo(cedianbianhao);
                    if (info != null)
                    {
                        cedianlist.Add(info);
                    }
                }
            }
            if (((fzh == 0) && (tdlx == 0)) && (xlx == 1))
            {
                query = from item in this.allcedianlist
                    where item.Value.WeiShanChu && (item.Value.XiaoleiXing == xiaoLeiXing)
                    orderby item.Value.CeDianBianHao
                    select item;
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    cedianbianhao = item.Value.CeDianBianHao.Trim();
                    info = this.getCeDianAllInfo(cedianbianhao);
                    if (info != null)
                    {
                        cedianlist.Add(info);
                    }
                }
            }
            if (((fzh == 0) && (tdlx == 1)) && (xlx == 0))
            {
                query = from item in this.allcedianlist
                    where item.Value.WeiShanChu && (item.Value.ChuanGanQiLeiXing == tongDaoLeiXing)
                    orderby item.Value.CeDianBianHao
                    select item;
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    cedianbianhao = item.Value.CeDianBianHao.Trim();
                    info = this.getCeDianAllInfo(cedianbianhao);
                    if (info != null)
                    {
                        cedianlist.Add(info);
                    }
                }
            }
            if (((fzh == 0) && (tdlx == 1)) && (xlx == 1))
            {
                query = from item in this.allcedianlist
                    where (item.Value.WeiShanChu && (item.Value.ChuanGanQiLeiXing == tongDaoLeiXing)) && (item.Value.XiaoleiXing == xiaoLeiXing)
                    orderby item.Value.CeDianBianHao
                    select item;
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    cedianbianhao = item.Value.CeDianBianHao.Trim();
                    info = this.getCeDianAllInfo(cedianbianhao);
                    if (info != null)
                    {
                        cedianlist.Add(info);
                    }
                }
            }
            if (((fzh == 1) && (tdlx == 0)) && (xlx == 0))
            {
                query = from item in this.allcedianlist
                    where item.Value.WeiShanChu && (item.Value.FenZhanHao == fenZhanHao)
                    orderby item.Value.CeDianBianHao
                    select item;
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    cedianbianhao = item.Value.CeDianBianHao.Trim();
                    info = this.getCeDianAllInfo(cedianbianhao);
                    if (info != null)
                    {
                        cedianlist.Add(info);
                    }
                }
            }
            if (((fzh == 1) && (tdlx == 0)) && (xlx == 1))
            {
                query = from item in this.allcedianlist
                    where (item.Value.WeiShanChu && (item.Value.FenZhanHao == fenZhanHao)) && (item.Value.XiaoleiXing == xiaoLeiXing)
                    orderby item.Value.CeDianBianHao
                    select item;
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    cedianbianhao = item.Value.CeDianBianHao.Trim();
                    info = this.getCeDianAllInfo(cedianbianhao);
                    if (info != null)
                    {
                        cedianlist.Add(info);
                    }
                }
            }
            if (((fzh == 1) && (tdlx == 1)) && (xlx == 0))
            {
                query = from item in this.allcedianlist
                    where (item.Value.WeiShanChu && (item.Value.FenZhanHao == fenZhanHao)) && (item.Value.ChuanGanQiLeiXing == tongDaoLeiXing)
                    orderby item.Value.CeDianBianHao
                    select item;
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    cedianbianhao = item.Value.CeDianBianHao.Trim();
                    info = this.getCeDianAllInfo(cedianbianhao);
                    if (info != null)
                    {
                        cedianlist.Add(info);
                    }
                }
            }
            if (((fzh == 1) && (tdlx == 1)) && (xlx == 1))
            {
                query = from item in this.allcedianlist
                    where ((item.Value.WeiShanChu && (item.Value.FenZhanHao == fenZhanHao)) && (item.Value.ChuanGanQiLeiXing == tongDaoLeiXing)) && (item.Value.XiaoleiXing == xiaoLeiXing)
                    orderby item.Value.CeDianBianHao
                    select item;
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    cedianbianhao = item.Value.CeDianBianHao.Trim();
                    info = this.getCeDianAllInfo(cedianbianhao);
                    if (info != null)
                    {
                        cedianlist.Add(info);
                    }
                }
            }
            if (tongDaoLeiXing == -1)
            {
                int fzhh = 0;
                int xlxx = 0;
                if (fenZhanHao != 0)
                {
                    fzhh = 1;
                }
                if (xiaoLeiXing != "全部")
                {
                    xlxx = 1;
                }
                if ((fzhh == 0) && (xlxx == 0))
                {
                    query = from item in this.allcedianlist
                        where (item.Value.ChuanGanQiLeiXing == 0) && item.Value.WeiShanChu
                        orderby item.Value.CeDianBianHao
                        select item;
                    foreach (KeyValuePair<string, CeDian> item in query)
                    {
                        cedianbianhao = item.Value.CeDianBianHao.Trim();
                        info = this.getCeDianAllInfo(cedianbianhao);
                        if (info != null)
                        {
                            cedianlist.Add(info);
                        }
                    }
                }
                if ((fzhh == 0) && (xlxx == 1))
                {
                    query = from item in this.allcedianlist
                        where ((item.Value.ChuanGanQiLeiXing == 0) && item.Value.WeiShanChu) && (item.Value.XiaoleiXing == xiaoLeiXing)
                        orderby item.Value.CeDianBianHao
                        select item;
                    foreach (KeyValuePair<string, CeDian> item in query)
                    {
                        cedianbianhao = item.Value.CeDianBianHao.Trim();
                        info = this.getCeDianAllInfo(cedianbianhao);
                        if (info != null)
                        {
                            cedianlist.Add(info);
                        }
                    }
                }
                if ((fzhh == 1) && (xlxx == 0))
                {
                    query = from item in this.allcedianlist
                        where ((item.Value.ChuanGanQiLeiXing == 0) && item.Value.WeiShanChu) && (item.Value.FenZhanHao == fenZhanHao)
                        orderby item.Value.CeDianBianHao
                        select item;
                    foreach (KeyValuePair<string, CeDian> item in query)
                    {
                        cedianbianhao = item.Value.CeDianBianHao.Trim();
                        info = this.getCeDianAllInfo(cedianbianhao);
                        if (info != null)
                        {
                            cedianlist.Add(info);
                        }
                    }
                }
                if ((fzhh != 1) || (xlxx != 1))
                {
                    return cedianlist;
                }
                query = from item in this.allcedianlist
                    where (((item.Value.ChuanGanQiLeiXing == 0) && item.Value.WeiShanChu) && (item.Value.FenZhanHao == fenZhanHao)) && (item.Value.XiaoleiXing == xiaoLeiXing)
                    orderby item.Value.CeDianBianHao
                    select item;
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    cedianbianhao = item.Value.CeDianBianHao.Trim();
                    info = this.getCeDianAllInfo(cedianbianhao);
                    if (info != null)
                    {
                        cedianlist.Add(info);
                    }
                }
            }
            return cedianlist;
        }

        public CeDian getCedianInfo(string bh)
        {
            try
            {
                if (this.allcedianlist.ContainsKey(bh))
                {
                    return this.allcedianlist[bh];
                }
            }
            catch (Exception ee)
            {

            }
            return null;
        }

        public byte[] GetConInfoByTongDao(int fenZhanNum, int tongDaoNum)
        {
            byte[] b;
            int i;
            byte daLeiXing = 0;
            string xiaoLeiXing = string.Empty;
            string ceDianBiaoHao = string.Empty;
            byte chuanGanQiLeiXing = 0;
            IEnumerable<KeyValuePair<string, CeDian>> query = from item in this.allcedianlist
                where ((item.Value.FenZhanHao == fenZhanNum) && (item.Value.TongDaoHao == tongDaoNum)) && (item.Value.DaLeiXing < 2)
                select item;
            try
            {
                if (query.Count<KeyValuePair<string, CeDian>>() == 0)
                {
                    b = new byte[10];
                    for (i = 0; i < b.Length; i++)
                    {
                        b[i] = 0xff;
                    }
                    b[8] = 0;
                    return b;
                }
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    daLeiXing = Convert.ToByte(item.Value.DaLeiXing);
                    xiaoLeiXing = item.Value.XiaoleiXing.ToString().Trim();
                    ceDianBiaoHao = item.Value.CeDianBianHao.ToString().Trim();
                    chuanGanQiLeiXing = 1;
                }
                if (daLeiXing == 0)
                {
                    return MoNiLiangLeiXing.GetConfigInfo(xiaoLeiXing, chuanGanQiLeiXing, ceDianBiaoHao);
                }
                return GlobalParams.AllkgLeiXing.GetConfigInfo(xiaoLeiXing, chuanGanQiLeiXing, ceDianBiaoHao);
            }
            catch
            {
                b = new byte[10];
                for (i = 0; i < b.Length; i++)
                {
                    b[i] = 0xff;
                }
                b[8] = 0;
                return b;
            }
        }

        public CeDian GetCtrCedian(int fds, int chan)
        {
            IEnumerable<KeyValuePair<string, CeDian>> query = from item in this.allcedianlist
                where (item.Value.FenZhanHao == fds) && (item.Value.TongDaoHao == chan)
                select item;
            if (query.Count<KeyValuePair<string, CeDian>>() != 0)
            {
                foreach (KeyValuePair<string, CeDian> item in query)
                {
                    if (Convert.ToByte(item.Value.DaLeiXing) == 2)
                    {
                        return item.Value;
                    }
                }
            }
            return null;
        }

        public string GetDuanDianQuYu(string ceDianBianHao)
        {
            if (ceDianBianHao[2] != 'C')
            {
                return this.GetmDuanDianQuYu(ceDianBianHao);
            }
            return this.GetkDuanDianQuYu(ceDianBianHao);
        }

        public string GetkDuanDianQuYu(string ceDianBianHao)
        {
            var query = from item in allcedianlist
                        join items in DuanDianGuanXi.ddgxc
                       on item.Value.CeDianBianHao equals items.KongZhiCeDianBianHao
                        where item.Value.WeiShanChu == true && items.KongZhiCeDianBianHao == ceDianBianHao
                        select item;
            string area = string.Empty;
            List<string> areas = new List<string>();
            try
            {
                if (query.Count() > 0)
                {
                    foreach (var item in query)
                    {
                        if (!areas.Contains(item.Value.CeDianWeiZhi))
                        {
                            if (area != string.Empty)
                                area += ";";
                            area += item.Value.CeDianWeiZhi;
                            areas.Add(item.Value.CeDianWeiZhi);
                        }
                    }
                }
            }
            catch(Exception ex){


            }
            return area;
        }

     

        public string GetmDuanDianQuYu(string ceDianBianHao)
        {
            var query = from item in allcedianlist
                        join items in DuanDianGuanXi.ddgxc
                         on item.Value.CeDianBianHao equals items.KongZhiCeDianBianHao
                        where item.Value.WeiShanChu == true && items.CeDianBianHao == ceDianBianHao
                        select item;
            string area = string.Empty;
            List<string> areas = new List<string>();
            if (query == null)
                return area;
            
            try
            {
                if (query.Count() > 0)
                {
                    foreach (var item in query)
                    {
                        if (!areas.Contains(item.Value.CeDianWeiZhi))
                        {
                            if (area != string.Empty)
                                area += ";";
                            area += item.Value.CeDianWeiZhi;
                            areas.Add(item.Value.CeDianWeiZhi);
                        }
                    }
                }
            }
            catch(Exception ex){

            }

            return area;
        }

        public CeDian GetMKCedian(int fds, int chan)
        {
            IEnumerable<KeyValuePair<string, CeDian>> query = from item in this.allcedianlist
                where (item.Value.FenZhanHao == fds) && (item.Value.TongDaoHao == chan)
                select item;
            try
            {
                if (query.Count<KeyValuePair<string, CeDian>>() != 0)
                {
                    foreach (KeyValuePair<string, CeDian> item in query)
                    {
                        if (Convert.ToByte(item.Value.DaLeiXing) != 2)
                        {
                            return item.Value;
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public DataTable ListConvertDataTable(byte LeiXing)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ceDianBianHao");
            dt.Columns.Add("daLeiXIng");
            dt.Columns.Add("xiaoLeiXIng");
            dt.Columns.Add("ceDianWeiZhi");
            dt.Columns.Add("chuanGanQiLeiXing");
            dt.Columns.Add("chuanGanQiZhiShi");
            dt.Columns.Add("fenZhanHao");
            dt.Columns.Add("tongDaoHao");
            dt.Columns.Add("weiShanChu");
            dt.Columns.Add("tiaoJiao");
            dt.Columns.Add("baoJing");
            dt.Columns.Add("id");
            dt.Columns.Add("createDate");
            dt.Columns.Add("deleteDate");
            try
            {
                List<CeDian> query = this.GetCeDian(LeiXing);
                foreach (CeDian item in query)
                {
                    DataRow dr = dt.NewRow();
                    dr["daLeiXIng"] = item.DaLeiXing;
                    dr["xiaoLeiXIng"] = item.XiaoleiXing;
                    dr["ceDianWeiZhi"] = item.CeDianWeiZhi;
                    dr["chuanGanQiLeiXing"] = item.ChuanGanQiLeiXing;
                    dr["chuanGanQiZhiShi"] = item.ChuanGanQiZhiShi;
                    dr["fenZhanHao"] = item.FenZhanHao;
                    dr["tongDaoHao"] = item.TongDaoHao;
                    dr["ceDianBianHao"] = item.CeDianBianHao;
                    dr["weiShanChu"] = item.WeiShanChu;
                    dr["tiaoJiao"] = item.TiaoJiao;
                    dr["baoJing"] = item.BaoJing;
                    dr["id"] = item.Id;
                    dr["createDate"] = item.CreateDate;
                    dr["deleteDate"] = item.DeleteDate;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            dt.Dispose();
            return dt;
        }

        public DataTable listtoDatatable(Dictionary<string, CeDian> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("xiaoLeiXIng");
            dt.Columns.Add("ceDianWeiZhi");
            dt.Columns.Add("ceDianBianHao");
            try
            {
                foreach (KeyValuePair<string, CeDian> item in list)
                {
                    DataRow dr = dt.NewRow();
                    dr["xiaoLeiXIng"] = item.Value.XiaoleiXing;
                    dr["ceDianWeiZhi"] = item.Value.CeDianWeiZhi;
                    dr["ceDianBianHao"] = item.Value.CeDianBianHao;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return dt;
        }

        public void ParseCeDianAllInfo(string cedian, out int fenzhan, out int tongdao)
        {
            if (cedian == null)
            {
                fenzhan = 0;
                tongdao = 0;
            }
            else
            {
                fenzhan = Convert.ToInt32(cedian.Substring(0, 2));
                tongdao = Convert.ToInt32(cedian.Substring(3, 2));
            }
        }

        public bool ReCeDian(string ceDianBianHao)
        {
            return this.allcedianlist.ContainsKey(ceDianBianHao);
        }

        public bool UpdateCeDian(string WeiZhi, string XiaoLeiXing, string ChuanGanQiLeiXing, string FenZhanHao, string TongDaoHao, string CeDianBianHao, string Original, bool isBj)
        {
            string sql = "update CeDian set cedianweizhi = '" + WeiZhi + "', xiaoleixing = '" + XiaoLeiXing + "',chuanganqileixing = '" + ChuanGanQiLeiXing + "',fenzhanhao = '" + FenZhanHao + "',tongdaohao= '" + TongDaoHao + "',cedianbianhao= '" + CeDianBianHao + "',baoJing='" + isBj.ToString() + "' where cedianbianhao = '" + Original + "'and weiShanChu=1";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            CeDian cedian = new CeDian(CeDianBianHao);
            this.allcedianlist.Remove(Original);
            this.allcedianlist.Add(CeDianBianHao, cedian);
            this.bhlist.Add(CeDianBianHao);
            this.bhlist.Remove(Original);
            return true;
        }

        public bool UpdateCeDian(string WeiZhi, string XiaoLeiXing, string ChuanGanQiLeiXing, string FenZhanHao, string TongDaoHao, string CeDianBianHao, string fenZhanLeiXing, string Original, bool isBj)
        {
            CeDian cd;
            string sql = "update CeDian set cedianweizhi = '" + WeiZhi + "', xiaoleixing = '" + XiaoLeiXing + "',chuanganqileixing = '" + ChuanGanQiLeiXing + "',chuanganqizhishi = '" + fenZhanLeiXing + "',fenzhanhao = '" + FenZhanHao + "',tongdaohao= '" + TongDaoHao + "',cedianbianhao= '" + CeDianBianHao + "',baoJing='" + isBj.ToString() + "' where cedianbianhao = '" + Original + "' and weishanchu = 1";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            this.allcedianlist.Remove(Original);
            this.bhlist.Remove(Original);
            CeDian cedian = new CeDian(CeDianBianHao);
            if (!this.allcedianlist.TryGetValue(CeDianBianHao, out cd))
            {
                this.allcedianlist.Add(CeDianBianHao, cedian);
            }
            else
            {
                this.allcedianlist[CeDianBianHao] = cedian;
            }
            this.bhlist.Add(CeDianBianHao);
            return true;
        }

        public bool UpdateCeDianKzl(string location, string cedianbianhao, string tongDao, string xiaoleixing, string Original)
        {
            string sql = string.Concat(new object[] { "update KongZhiLiangCeDian set ceDianWeiZhi = '", location, "', ceDianBianHao = '", cedianbianhao, "', kongZhiLiangBianHao = ", Convert.ToInt32(tongDao), ", mingCheng = '", xiaoleixing, "' where ceDianBianHao = '", Original, "' and weiShanChu=1" });
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            this.allcedianlist.Remove(Original);
            this.bhlist.Remove(Original);
            CeDian cedian = new CeDian(cedianbianhao);
            this.allcedianlist.Add(cedianbianhao, cedian);
            this.bhlist.Add(cedianbianhao);
            return true;
        }

        public bool updateCedian(string cdbh,CeDian cd)
        {
            try
            {
                int index = bhlist.IndexOf(cdbh);
                if (index > -1)
                {
                    allcedianlist[cdbh] = cd;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}

