namespace Logic
{
    using System;
    using System.Data;

    public class DuanDianGuanXiClass
    {
        private string ceDianBianHao;
        private float duanDianFuDian;
        private int duanDianGuanXiID;
        private string kongZhiCeDianBianHao;
        private bool kongZhiFangShi;
        private string kongZhiQuYu;
        private int type;

        public DuanDianGuanXiClass()
        {
        }

        public DuanDianGuanXiClass(int id, string cedianbianhao, string kongzhiliangcedianbianhao)
        {
            DataTable dt = OperateDB.Select(string.Concat(new object[] { "select * from DuanDianGuanXi where duanDianGuanXiID=", id, " and ceDianBianHao='", cedianbianhao, "' and kongZhiCeDianBianHao='", kongzhiliangcedianbianhao, "' " }));
            if ((dt != null) && (dt.Rows.Count != 0))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DuanDianGuanXiClass ddgxc = new DuanDianGuanXiClass {
                        DuanDianGuanXiID = Convert.ToInt32(dt.Rows[i]["duanDianGuanXiID"]),
                        CeDianBianHao = dt.Rows[i]["ceDianBianHao"].ToString().Trim(),
                        KongZhiFangShi = Convert.ToBoolean(dt.Rows[i]["kongZhiFangShi"]),
                        KongZhiCeDianBianHao = dt.Rows[i]["kongZhiCeDianBianHao"].ToString().Trim()
                    };
                    DuanDianGuanXi.ddgxc.Add(ddgxc);
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

        public float DuanDianFuDian
        {
            get
            {
                return this.duanDianFuDian;
            }
            set
            {
                this.duanDianFuDian = value;
            }
        }

        public int DuanDianGuanXiID
        {
            get
            {
                return this.duanDianGuanXiID;
            }
            set
            {
                this.duanDianGuanXiID = value;
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

        public bool KongZhiFangShi
        {
            get
            {
                return this.kongZhiFangShi;
            }
            set
            {
                this.kongZhiFangShi = value;
            }
        }

        public string KongZhiQuYu
        {
            get
            {
                return this.kongZhiQuYu;
            }
            set
            {
                this.kongZhiQuYu = value;
            }
        }

        public int Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
    }
}

