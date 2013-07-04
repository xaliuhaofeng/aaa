namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;

    public class KongZhiLiangList
    {
        public Dictionary<string, KongZhiLiang> allkongzhilianglist = new Dictionary<string, KongZhiLiang>();
        public List<string> list = new List<string>();

        public KongZhiLiangList()
        {
            this.allkongzhilianglist.Clear();
            this.list.Clear();
        }

        public bool DeleteKzl(string MingCheng)
        {
            bool flag = this.IsExistKzl(MingCheng);
            if (!flag)
            {
                MessageBox.Show("所删除的控制量不存在");
                return flag;
            }
            if (this.IsGuanLianCeDian(MingCheng))
            {
                MessageBox.Show("控制量类型与测点关联，不能删除！");
                return false;
            }
            string sql = "delete from KongZhiLiang where mingCheng = '" + MingCheng + "'";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.allkongzhilianglist.Remove(MingCheng);
            this.list.Remove(MingCheng);
            return true;
        }

        public void FillKongZhiLiang()
        {
            string sql = "select * from KongZhiLiang order by kongZhiLiangLeiXing desc";
            DataTable dt = OperateDB.Select(sql);
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                
                    throw new Exception("不存在控制量类型！");
                
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    KongZhiLiang temp;
                    KongZhiLiang kzl = new KongZhiLiang {
                        Litaimingcheng = dt.Rows[i]["lingTaiMingCheng"].ToString().Trim(),
                        Yitaimingcheng = dt.Rows[i]["yiTaiMingCheng"].ToString().Trim(),
                        Leixing = Convert.ToInt32(dt.Rows[i]["kongZhiLiangLeiXing"]),
                        Mingcheng = dt.Rows[i]["mingCheng"].ToString().Trim()
                    };
                    if (!this.allkongzhilianglist.TryGetValue(kzl.Mingcheng, out temp))
                    {
                        this.allkongzhilianglist.Add(kzl.Mingcheng, kzl);
                        this.list.Add(kzl.Mingcheng);
                    }
                }
            }
        }

        public string[] GetAllMingCheng()
        {
            return this.list.ToArray();
        }

        public KongZhiLiang GetKongZhiLiang(string name)
        {
            KongZhiLiang m_kzllx = null;
            if (this.allkongzhilianglist.ContainsKey(name))
            {
                IOrderedEnumerable<KeyValuePair<string, KongZhiLiang>> query = from item in this.allkongzhilianglist
                    where item.Key == name
                    orderby item.Key
                    select item;
                foreach (KeyValuePair<string, KongZhiLiang> item in query)
                {
                    m_kzllx = item.Value;
                }
            }
            return m_kzllx;
        }

        public bool InsertKzl(string MingCheng, string LingTai, string YiTai, string LeiXing)
        {
            if (this.IsExistKzl(MingCheng))
            {
                MessageBox.Show("控制量类型已存在");
                return false;
            }
            string sql = "insert into KongZhiLiang (mingcheng,lingtaimingcheng,yitaimingcheng,kongzhiliangleiXing) values ('" + MingCheng + "', '" + LingTai + "', '" + YiTai + "', '" + LeiXing + "')";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            KongZhiLiang kzl = new KongZhiLiang(MingCheng);
            this.allkongzhilianglist.Add(MingCheng, kzl);
            this.list.Add(MingCheng);
            return true;
        }

        public bool IsExistKzl(string kzl)
        {
            return this.allkongzhilianglist.ContainsKey(kzl);
        }

        public bool IsGuanLianCeDian(string kzl)
        {
            return ((from item in GlobalParams.AllCeDianList.allcedianlist
                where item.Value.XiaoleiXing == kzl
                select item).Count<KeyValuePair<string, CeDian>>() > 0);
        }

        public DataTable ListConvertDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("mingCheng");
            dt.Columns.Add("lingTaiMingCheng");
            dt.Columns.Add("yiTaiMingCheng");
            dt.Columns.Add("kongzhiliangleixing");
            try
            {
                IEnumerable<KeyValuePair<string, KongZhiLiang>> query = from item in GlobalParams.AllkzlLeiXing.allkongzhilianglist select item;
                foreach (KeyValuePair<string, KongZhiLiang> item in query)
                {
                    DataRow dr = dt.NewRow();
                    dr["mingCheng"] = item.Value.Mingcheng;
                    dr["kongzhiliangleixing"] = item.Value.Leixing;
                    dr["lingTaiMingCheng"] = item.Value.Litaimingcheng;
                    dr["yiTaiMingCheng"] = item.Value.Yitaimingcheng;
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

        public bool UpdataKzl(string MingCheng, string LingTai, string YiTai, string LeiXing, string OriginalMingCheng)
        {
            bool flag = this.IsExistKzl(OriginalMingCheng);
            if (flag)
            {
                string sql = "update KongZhiLiang set mingCheng = '" + MingCheng + "',lingtaimingcheng = '" + LingTai + "',yitaimingcheng = '" + YiTai + "',kongzhiliangleixing= '" + LeiXing + "' where mingCheng = '" + OriginalMingCheng + "'";
                try
                {
                    OperateDB.Execute(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                KongZhiLiang kzl = new KongZhiLiang(MingCheng);
                this.allkongzhilianglist.Remove(OriginalMingCheng);
                this.allkongzhilianglist.Add(MingCheng, kzl);
                this.list.Remove(OriginalMingCheng);
                this.list.Add(MingCheng);
                return flag;
            }
            MessageBox.Show("控制量类型不存在");
            return flag;
        }
    }
}

