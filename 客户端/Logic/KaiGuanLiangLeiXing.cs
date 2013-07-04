namespace Logic
{
    using System;
    using System.Data;

    public class KaiGuanLiangLeiXing
    {
        private string baoJingShengYin;
        private int baoJingZhuangTai;
        private int duanDianZhuangTai;
        private string erTai;
        private int id;
        private int leiXing;
        private string lingTai;
        private string mingCheng;
        private bool shiFouBaoJing;
        private bool shiFouDuanDian;
        private string yiTai;
        private string zhengChangTuBiao;

        public KaiGuanLiangLeiXing()
        {
        }

        public KaiGuanLiangLeiXing(string mingCheng)
        {
            DataTable dt = OperateDB.Select("select * from KaiGuanLiangLeiXing where mingCheng = '" + mingCheng + "'");
            if ((dt == null) || (dt.Rows.Count == 0))
            {
               
                    throw new Exception("不存在的开关量类型！mingCheng = " + mingCheng);
                
            }
            else
            {
                this.mingCheng = mingCheng;
                this.leiXing = Convert.ToInt32(dt.Rows[0]["leiXIng"]);
                this.lingTai = dt.Rows[0]["lingTaiMingCheng"].ToString();
                this.yiTai = dt.Rows[0]["yiTaiMingCheng"].ToString();
                this.zhengChangTuBiao = dt.Rows[0]["zhengChangTuBiao"].ToString();
                this.baoJingShengYin = dt.Rows[0]["baoJingShengYin"].ToString();
                this.id = Convert.ToInt32(dt.Rows[0]["id"]);
                if (dt.Rows[0]["erTaiMingCheng"] == null)
                {
                    this.erTai = string.Empty;
                }
                else
                {
                    this.erTai = dt.Rows[0]["erTaiMingCheng"].ToString();
                }
                this.shiFouBaoJing = Convert.ToBoolean(dt.Rows[0]["shiFouBaoJing"]);
                if (dt.Rows[0]["baoJingZhuangTai"] == null)
                {
                    this.baoJingZhuangTai = -1;
                }
                else
                {
                    this.baoJingZhuangTai = Convert.ToInt32(dt.Rows[0]["baoJingZhuangTai"]);
                }
                this.shiFouDuanDian = Convert.ToBoolean(dt.Rows[0]["shiFouDuanDian"]);
                if (dt.Rows[0]["duanDianZhuangTai"] == null)
                {
                    this.duanDianZhuangTai = -1;
                }
                else
                {
                    this.duanDianZhuangTai = Convert.ToInt32(dt.Rows[0]["duanDianZhuangTai"]);
                }
            }
        }

        public string BaoJingShengYin
        {
            get
            {
                return this.baoJingShengYin;
            }
            set
            {
                this.baoJingShengYin = value;
            }
        }

        public int BaoJingZhuangTai
        {
            get
            {
                return this.baoJingZhuangTai;
            }
            set
            {
                this.baoJingZhuangTai = value;
            }
        }

        public int DuanDianZhuangTai
        {
            get
            {
                return this.duanDianZhuangTai;
            }
            set
            {
                this.duanDianZhuangTai = value;
            }
        }

        public string ErTai
        {
            get
            {
                return this.erTai;
            }
            set
            {
                this.erTai = value;
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public int LeiXing
        {
            get
            {
                return this.leiXing;
            }
            set
            {
                this.leiXing = value;
            }
        }

        public string LingTai
        {
            get
            {
                return this.lingTai;
            }
            set
            {
                this.lingTai = value;
            }
        }

        public string MingCheng
        {
            get
            {
                return this.mingCheng;
            }
            set
            {
                this.mingCheng = value;
            }
        }

        public bool ShiFouBaoJing
        {
            get
            {
                return this.shiFouBaoJing;
            }
            set
            {
                this.shiFouBaoJing = value;
            }
        }

        public bool ShiFouDuanDian
        {
            get
            {
                return this.shiFouDuanDian;
            }
            set
            {
                this.shiFouDuanDian = value;
            }
        }

        public string YiTai
        {
            get
            {
                return this.yiTai;
            }
            set
            {
                this.yiTai = value;
            }
        }

        public string ZhengChangTuBiao
        {
            get
            {
                return this.zhengChangTuBiao;
            }
            set
            {
                this.zhengChangTuBiao = value;
            }
        }
    }
}

