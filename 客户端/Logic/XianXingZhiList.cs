namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;

    public class XianXingZhiList
    {
        public Dictionary<string, XianXingZhi> allxxz_list = new Dictionary<string, XianXingZhi>();
        public List<string> mnlmingcheng_list = new List<string>();

        public XianXingZhiList()
        {
            this.allxxz_list.Clear();
            this.mnlmingcheng_list.Clear();
        }

        public bool DeleteXianXingZhi(string moniliangmingcheng)
        {
            if (!this.IsExist(moniliangmingcheng))
            {
                MessageBox.Show(moniliangmingcheng + "不存在");
                return false;
            }
            if (this.IsGLCeDian(moniliangmingcheng))
            {
                MessageBox.Show("无法删除，此线性值关联测点");
                return false;
            }
            string sql = "delete from XianXingZhi where moNiLiangMingCheng = '" + moniliangmingcheng + "'";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            this.allxxz_list.Remove(moniliangmingcheng);
            this.mnlmingcheng_list.Remove(moniliangmingcheng);
            return true;
        }

        public void FillXianXingZhi()
        {
            string sql = "select * from XianXingZhi";
            DataTable dt = OperateDB.Select(sql);
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                Console.WriteLine("不存在限性值类型！");
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    XianXingZhi xxz = new XianXingZhi {
                        MoNiLiangMingCheng = dt.Rows[i]["moNiLiangMingCheng"].ToString().Trim(),
                        Id = Convert.ToInt32(dt.Rows[i]["id"]),
                        LiangCheng0 = Convert.ToSingle(dt.Rows[i]["liangCheng0"]),
                        LiangCheng1 = Convert.ToSingle(dt.Rows[i]["liangCheng1"]),
                        LiangCheng2 = Convert.ToSingle(dt.Rows[i]["liangCheng2"]),
                        LiangCheng3 = Convert.ToSingle(dt.Rows[i]["liangCheng3"]),
                        LiangCheng4 = Convert.ToSingle(dt.Rows[i]["liangCheng4"]),
                        Value0 = Convert.ToSingle(dt.Rows[i]["value0"]),
                        Value1 = Convert.ToSingle(dt.Rows[i]["value1"]),
                        Value2 = Convert.ToSingle(dt.Rows[i]["value2"]),
                        Value3 = Convert.ToSingle(dt.Rows[i]["value3"]),
                        Value4 = Convert.ToSingle(dt.Rows[i]["value4"])
                    };
                    this.allxxz_list.Add(xxz.MoNiLiangMingCheng, xxz);
                    this.mnlmingcheng_list.Add(xxz.MoNiLiangMingCheng);
                }
            }
        }

        public DataTable GetFeiXianXing()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("moNiLiangMingCheng");
            dt.Columns.Add("id");
            dt.Columns.Add("liangCheng0");
            dt.Columns.Add("liangCheng1");
            dt.Columns.Add("liangCheng2");
            dt.Columns.Add("liangCheng3");
            dt.Columns.Add("liangCheng4");
            dt.Columns.Add("value0");
            dt.Columns.Add("value1");
            dt.Columns.Add("value2");
            dt.Columns.Add("value3");
            dt.Columns.Add("value4");
            try
            {
                IEnumerable<KeyValuePair<string, XianXingZhi>> query = from item in this.allxxz_list select item;
                foreach (KeyValuePair<string, XianXingZhi> item in query)
                {
                    DataRow dr = dt.NewRow();
                    dr["moNiLiangMingCheng"] = item.Value.MoNiLiangMingCheng;
                    dr["id"] = item.Value.Id;
                    dr["liangCheng0"] = item.Value.LiangCheng0;
                    dr["liangCheng1"] = item.Value.LiangCheng1;
                    dr["liangCheng2"] = item.Value.LiangCheng2;
                    dr["liangCheng3"] = item.Value.LiangCheng3;
                    dr["liangCheng4"] = item.Value.LiangCheng4;
                    dr["value0"] = item.Value.Value0;
                    dr["value1"] = item.Value.Value1;
                    dr["value2"] = item.Value.Value2;
                    dr["value3"] = item.Value.Value3;
                    dr["value4"] = item.Value.Value4;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            dt.Dispose();
            return dt;
        }

        public XianXingZhi GetFeiXianXing(string feiXianXing)
        {
            XianXingZhi xxz = new XianXingZhi();
            IEnumerable<KeyValuePair<string, XianXingZhi>> query = from item in this.allxxz_list
                where item.Value.MoNiLiangMingCheng == feiXianXing
                select item;
            foreach (KeyValuePair<string, XianXingZhi> item in query)
            {
                xxz = item.Value;
            }
            return xxz;
        }

        public bool InsertMnlMC(string MingCheng, string LiangCheng0, string LiangCheng1, string LiangCheng2, string LiangCheng3, string LiangCheng4, string value0, string value1, string value2, string value3, string value4)
        {
            if (this.IsExist(MingCheng))
            {
                MessageBox.Show("该模拟量名称已存在，请重新命名");
                return false;
            }
            string sql = "insert into XianXingZhi(moNiLiangMingCheng,liangCheng0,liangCheng1,liangCheng2,liangCheng3,liangCheng4,value0,value1,value2,value3,value4) values ('" + MingCheng + "', '" + LiangCheng0 + "', '" + LiangCheng1 + "', '" + LiangCheng2 + "', '" + LiangCheng3 + "', '" + LiangCheng4 + "', '" + value0 + "', '" + value1 + "', '" + value2 + "', '" + value3 + "', '" + value4 + "')";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            XianXingZhi xxz = new XianXingZhi(MingCheng);
            this.allxxz_list.Add(MingCheng, xxz);
            this.mnlmingcheng_list.Add(MingCheng);
            return true;
        }

        public bool IsExist(string moniliangmingcheng)
        {
            return this.allxxz_list.ContainsKey(moniliangmingcheng);
        }

        public bool IsGLCeDian(string mnlleixing)
        {
            return ((from item in GlobalParams.AllCeDianList.allcedianlist
                where item.Value.XiaoleiXing == mnlleixing
                select item).Count<KeyValuePair<string, CeDian>>() > 0);
        }

        public bool UpdataXianXingZhi(string MingCheng, string LiangCheng0, string LiangCheng1, string LiangCheng2, string LiangCheng3, string LiangCheng4, string value0, string value1, string value2, string value3, string value4, string Original)
        {
            if (this.IsExist(Original))
            {
                string sql = "update XianXingZhi set moNiLiangMingCheng = '" + MingCheng + "',liangCheng0 ='" + LiangCheng0 + "',liangCheng1 ='" + LiangCheng1 + "',liangCheng2 ='" + LiangCheng2 + "',liangCheng3 ='" + LiangCheng3 + "',liangCheng4 ='" + LiangCheng4 + "',value0 ='" + value0 + "',value1 ='" + value1 + "',value2 ='" + value2 + "',value3 ='" + value3 + "',value4 ='" + value4 + "' where moNiLiangMingCheng = '" + Original + "'";
                try
                {
                    OperateDB.Execute(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                this.allxxz_list.Remove(Original);
                this.mnlmingcheng_list.Remove(Original);
                XianXingZhi xxz = new XianXingZhi(MingCheng);
                this.allxxz_list.Add(MingCheng, xxz);
                this.mnlmingcheng_list.Add(MingCheng);
                return true;
            }
            MessageBox.Show("没有限性值" + Original);
            return false;
        }
    }
}

