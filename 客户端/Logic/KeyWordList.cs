namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;

    public class KeyWordList
    {
        public List<string> all_keyword = new List<string>();
        private string keyword;

        public KeyWordList()
        {
            this.all_keyword.Clear();
        }

        public bool CreateKey(string type)
        {
            if (this.IsExist(type))
            {
                MessageBox.Show("关键字已经存在，不能添加！");
                return false;
            }
            string sql = "insert into GuanJianZi values( '" + type + "')";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            this.all_keyword.Add(type);
            return true;
        }

        public bool DeleteKey(string original)
        {
            if (!this.IsExist(original))
            {
                MessageBox.Show("不存在此关键字，无法删除！");
                return false;
            }
            if (this.IsGuanLianLeiXing(original))
            {
                MessageBox.Show("此关键字关联模拟量类型，无法删除！");
                return false;
            }
            string sql = "delete from GuanJianZi where guanJianZi = '" + original + "'";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            this.all_keyword.Remove(original);
            return true;
        }

        public void FillKeyWord()
        {
            string sql = "select * from GuanJianZi";
            DataTable dt = OperateDB.Select(sql);
            foreach (DataRow dr in dt.Rows)
            {
                this.keyword = dr[0].ToString().Trim();
                this.all_keyword.Add(this.keyword);
            }
        }

        public bool IsExist(string type)
        {
            bool flag = this.all_keyword.Contains(type);
            if (flag)
            {
                return flag;
            }
            return flag;
        }

        public bool IsGuanLianLeiXing(string type)
        {
            return ((from item in GlobalParams.AllmnlLeiXing.allmonileixinglist
                where item.Value.GuanJianZi == type
                select item).Count<KeyValuePair<string, MoNiLiangLeiXing>>() > 0);
        }

        public bool UpdateKey(string type, string original)
        {
            if (!this.IsExist(original))
            {
                MessageBox.Show("不存在此关键字，无法删除！");
                return false;
            }
            string sql = "update GuanJianZi set guanJianZi = '" + type + "' where guanJianZi = '" + original + "'";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            this.all_keyword.Add(type);
            this.all_keyword.Remove(original);
            return true;
        }

        public string Keyword
        {
            get
            {
                return this.keyword;
            }
            set
            {
                this.keyword = value;
            }
        }
    }
}

