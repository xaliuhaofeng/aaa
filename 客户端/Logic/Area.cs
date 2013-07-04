namespace Logic
{
    using System;


    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    public class Area
    {
        private string addr;
        public List<string> m_AllAddr = new List<string>();

        public Area()
        {
            this.m_AllAddr.Clear();
        }

        public bool AddAddr(string str)
        {
            if (this.IsExistAddr(str))
            {
                MessageBox.Show("该测点已经存在");
                return false;
            }
            string sql = "insert into AnZhuangDiDian(DiDian) values('" + str + "')";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString().Trim());
                return false;
            }
            this.m_AllAddr.Add(str);
            return true;
        }

        public void ClearAddr()
        {
            this.m_AllAddr.Clear();
        }

        public string CreateLocation(string location)
        {
            return ("insert into AnZhuangDiDian values( '" + location + "')");
        }

        public bool DelAddr(string str)
        {
            if (this.IsExistAddr(str))
            {
                string sql = this.DeleteLocation(str);
                try
                {
                    OperateDB.Execute(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString().Trim());
                    return false;
                }
                this.m_AllAddr.Remove(str);
                return true;
            }
            MessageBox.Show("该测点不存在");
            return false;
        }

        public string DeleteLocation(string original)
        {
            return ("delete from AnZhuangDiDian where DiDian = '" + original + "'");
        }

        public void FillAddr()
        {
            string sql = "select * from AnZhuangDiDian order by DiDian";
            DataTable dt = OperateDB.Select(sql);
            foreach (DataRow dr in dt.Rows)
            {
                string arr = dr[0].ToString().Trim();
                this.m_AllAddr.Add(arr);
            }
        }

        public bool IsExistAddr(string str)
        {
            return this.m_AllAddr.Contains(str.ToString().Trim());
        }

        public string Addr
        {
            get
            {
                return this.addr;
            }
            set
            {
                this.addr = value;
            }
        }
    }
}

