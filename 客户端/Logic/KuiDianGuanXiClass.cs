namespace Logic
{
    using System;
    using System.Data;

    public class KuiDianGuanXiClass
    {
        private string ceDianBianHao;
        private string kongZhiCeDianBianHao;
        private int kuiDianGuanXiID;

        public KuiDianGuanXiClass()
        {
        }

        public KuiDianGuanXiClass(int id)
        {
            DataTable dt = OperateDB.Select("select * from KuiDianGuanXi where kuiDianGuanXiID=" + id);
            if ((dt != null) && (dt.Rows.Count != 0))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.KuiDianGuanXiID = Convert.ToInt32(dt.Rows[i]["kuiDianGuanXiID"]);
                    this.CeDianBianHao = dt.Rows[i]["ceDianBianHao"].ToString().Trim();
                    this.KongZhiCeDianBianHao = dt.Rows[i]["kongZhiCeDianBianHao"].ToString().Trim();
                }
            }
        }

        public string CeDianBianHao
        {
            get
            {
                return this.ceDianBianHao;
            }
            set
            {
                this.ceDianBianHao = value;
            }
        }

        public string KongZhiCeDianBianHao
        {
            get
            {
                return this.kongZhiCeDianBianHao;
            }
            set
            {
                this.kongZhiCeDianBianHao = value;
            }
        }

        public int KuiDianGuanXiID
        {
            get
            {
                return this.kuiDianGuanXiID;
            }
            set
            {
                this.kuiDianGuanXiID = value;
            }
        }
    }
}

