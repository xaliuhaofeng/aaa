namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class MoNiLiangLeiXing
    {
        private byte baoJingLeiXing;
        private string baoJingShengYin;
        private string baoJingTuBiao;
        private float baoJingZhiShangXian;
        private float baoJingZhiXiaXian;
        private string danWei;
        private float duanDianZhi;
        private string feiXianXingGuanXi;
        private float fuDianZhi;
        private string guanJianZi;
        private int id;
        private int leiXing;
        private float liangChengDi;
        private float liangChengGao;
        private string mingCheng;
        private int wuChaDai;
        private bool xianXingShuXing;

        public MoNiLiangLeiXing()
        {
        }

        public MoNiLiangLeiXing(string mingCheng)
        {
            DataTable dt = OperateDB.Select("select * from MoNiLiangLeiXing where mingCheng = '" + mingCheng + "'");
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                Console.WriteLine("不存在的模拟量类型！");
            }
            else
            {
                this.mingCheng = mingCheng;
                this.id = Convert.ToInt32(dt.Rows[0]["id"]);
                this.leiXing = Convert.ToInt32(dt.Rows[0]["leiXing"]);
                this.danWei = dt.Rows[0]["danWei"].ToString().Trim();
                this.guanJianZi = dt.Rows[0]["guanJianZi"].ToString().Trim();
                this.xianXingShuXing = Convert.ToBoolean(dt.Rows[0]["xianXingShuXing"]);
                this.liangChengDi = Convert.ToSingle(dt.Rows[0]["liangChengDi"]);
                this.liangChengGao = Convert.ToSingle(dt.Rows[0]["liangChengGao"]);
                this.baoJingZhiShangXian = Convert.ToSingle(dt.Rows[0]["baoJingZhiShangXian"]);
                this.baoJingZhiXiaXian = Convert.ToSingle(dt.Rows[0]["baoJingZhiXiaXian"]);
                this.duanDianZhi = Convert.ToSingle(dt.Rows[0]["duanDianZhi"]);
                this.fuDianZhi = Convert.ToSingle(dt.Rows[0]["fuDianZhi"]);
                this.baoJingLeiXing = Convert.ToByte(dt.Rows[0]["baoJingLeiXing"]);
                this.feiXianXingGuanXi = dt.Rows[0]["feiXianXingGuanXi"].ToString().Trim();
                this.wuChaDai = Convert.ToInt32(dt.Rows[0]["wuChaDai"]);
                this.baoJingShengYin = dt.Rows[0]["baoJingShengYin"].ToString().Trim();
                this.baoJingTuBiao = dt.Rows[0]["baoJingTuBiao"].ToString().Trim();
            }
        }

        public static string CountMoNiLiang(string MingCheng)
        {
            return ("select mingCheng from MoNiLiangLeiXing where mingCheng = '" + MingCheng + "'");
        }

        public static int CountXianXing(string Original)
        {
            return OperateDB.Select("select feiXianXingGuanXi from MoNiLiangLeiXing where feiXianXingGuanXi = '" + Original + "'").Rows.Count;
        }

        public static string CreateMoNiLiang(byte LeiXing, string TuBiao, string MingCheng, string DanWei, string GuanJianZi, string XianXingShuXing, string FeiXianXingGuanXi, string WuChaDai, string BaoJingZhiShangXian, string BaoJingZhiXiaXian, string BaoJingLeiXing, string LiangChengGao, string LiangChengDi, string DuanDianZhi, string FuDianZhi)
        {
            string sql = string.Concat(new object[] { 
                "insert into MoNiLiangLeiXing ( leiXing, baoJingTuBiao, mingCheng, danWei, guanJianZi, xianXingShuXing,feiXianXingGuanXi, wuChaDai, baoJingZhiShangXian,baoJingZhiXiaXian, baoJingLeiXing, liangChengGao, liangChengDi, duanDianZhi, fuDianZhi) values ('", LeiXing, "', '", TuBiao, "', '", MingCheng, "', '", DanWei, "', '", GuanJianZi, "', '", XianXingShuXing, "', '", FeiXianXingGuanXi, "', '", WuChaDai, 
                "', '", BaoJingZhiShangXian, "', '", BaoJingZhiXiaXian, "', '", BaoJingLeiXing, "', '", LiangChengGao, "', '", LiangChengDi, "', '", DuanDianZhi, "', '", FuDianZhi, "')"
             });
            OperateDB.Execute(sql);
            return sql;
        }

        public static string DelMoNiLiang(string MingCheng)
        {
            return ("delete from MoNiLiangLeiXing where mingCheng = '" + MingCheng + "'");
        }

        public static string filePic(string ming)
        {
            return OperateDB.Select("select baoJingTuBiao from MoNiLiangLeiXing where mingCheng = '" + ming + "'").Rows[0][0].ToString();
        }

        public static string[] GetAllMingCheng()
        {
            string sql = "select mingCheng from MoNiLiangLeiXing";
            List<string> mingchengs = new List<string>();
            DataTable dt = OperateDB.Select(sql);
            if ((dt == null) || (dt.Rows.Count <= 0))
            {
                return new string[0];
            }
            foreach (DataRow row in dt.Rows)
            {
                mingchengs.Add(row["mingCheng"].ToString());
            }
            return mingchengs.ToArray();
        }

        public static DataTable GetAnalogAlarm(string ceDianBianHao)
        {
            return OperateDB.Select("select mingCheng, leiXing, danWei, guanJianZi, xianXingShuXing,feiXianXingGuanXi, wuChaDai, liangChengDi, liangChengGao, baoJingZhiShangXian, baoJingZhiXiaXian, baoJingLeiXing, duanDianZhi, fuDianZhi, baoJingShengYin, baoJingTuBiao from MoNiLiangLeiXing where mingCheng in(select xiaoLeiXing from CeDian where ceDianBianHao = '" + ceDianBianHao + "')");
        }

        public static byte[] GetConfigInfo(string xiaoLeiXing, byte chuanGanQiLeiXing, string ceDianBianHao)
        {
            byte[] b = new byte[10];
            DataTable dt = OperateDB.Select("select * from MoNiLiangLeiXing where mingCheng='" + xiaoLeiXing + "'");
            float liangChengDi = Convert.ToSingle(dt.Rows[0]["liangChengDi"]);
            float liangCheng = Convert.ToSingle(dt.Rows[0]["liangChengGao"]) - liangChengDi;
            int yLiangCheng = (int) liangCheng;
            b[1] = (byte) (yLiangCheng & 0xff);
            b[0] = (byte) (yLiangCheng >> 8);
            byte tongDaoLeiXing = Convert.ToByte(dt.Rows[0]["leiXing"]);
            int yBaoJingZhi = ShiJiZhiToYuanZhi(Convert.ToSingle(dt.Rows[0]["baoJingZhiShangXian"]), tongDaoLeiXing, liangCheng);
            b[3] = (byte) (yBaoJingZhi & 0xff);
            b[2] = (byte) (yBaoJingZhi >> 8);
            int yduanDianZhi = ShiJiZhiToYuanZhi(Convert.ToSingle(dt.Rows[0]["duanDianZhi"]), tongDaoLeiXing, liangCheng);
            b[5] = (byte) (yduanDianZhi & 0xff);
            b[4] = (byte) (yduanDianZhi >> 8);
            int yfuDianZhi = ShiJiZhiToYuanZhi(Convert.ToSingle(dt.Rows[0]["fuDianZhi"]), tongDaoLeiXing, liangCheng);
            b[7] = (byte) (yfuDianZhi & 0xff);
            b[6] = (byte) (yfuDianZhi >> 8);
            b[8] = DuanDianGuanXi.GetConfInfoByCeDianBianHao(ceDianBianHao);
            b[9] = (byte) ((Convert.ToByte(dt.Rows[0]["leiXing"]) << 5) | chuanGanQiLeiXing);
            return b;
        }

        public static DataTable GetMo()
        {
            string sql = "select mingCheng, duanDianZhi, fuDianZhi from MoNiLiangLeiXing where leiXing = 1 or leiXing = 5";
            return OperateDB.Select(sql);
        }

        public static DataTable GetMoNiLiang()
        {
            string sql = "select mingCheng, leiXing, danWei, guanJianZi, baoJingZhiShangXian, duanDianZhi, fuDianZhi, liangChengDi, liangChengGao from MoNiLiangLeiXing";
            return OperateDB.Select(sql);
        }

        public static DataTable GetMoNiLiang(byte LeiXing)
        {
            string sql;
            if (LeiXing == 0)
            {
                sql = "select mingCheng, leiXing, danWei, guanJianZi, xianXingShuXing,feiXianXingGuanXi, wuChaDai, liangChengDi, liangChengGao, baoJingZhiShangXian, baoJingZhiXiaXian,baoJingLeiXing, duanDianZhi, fuDianZhi, baoJingShengYin, baoJingTuBiao from MoNiLiangLeiXing where leiXing = 1 or leiXing = 5";
            }
            else
            {
                sql = "select mingCheng, leiXing, danWei, guanJianZi, xianXingShuXing, feiXianXingGuanXi,wuChaDai, liangChengDi, liangChengGao, baoJingZhiShangXian, baoJingZhiXiaXian,baoJingLeiXing, baoJingShengYin, baoJingTuBiao from MoNiLiangLeiXing where leiXing = 2";
            }
            return OperateDB.Select(sql);
        }

        public static int ShiJiZhiToYuanZhi(float shiJiZhi, byte chuanGanQiLeiXing, float liangCheng)
        {
            if (chuanGanQiLeiXing == 1)
            {
                return (int) (300f + ((1200f / liangCheng) * shiJiZhi));
            }
            return (int) (200f + ((800f / liangCheng) * shiJiZhi));
        }

        public byte BaoJingLeiXing
        {
            get
            {
                return this.baoJingLeiXing;
            }
            set
            {
                this.baoJingLeiXing = value;
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

        public string BaoJingTuBiao
        {
            get
            {
                return this.baoJingTuBiao;
            }
            set
            {
                this.baoJingTuBiao = value;
            }
        }

        public float BaoJingZhiShangXian
        {
            get
            {
                return this.baoJingZhiShangXian;
            }
            set
            {
                this.baoJingZhiShangXian = value;
            }
        }

        public float BaoJingZhiXiaXian
        {
            get
            {
                return this.baoJingZhiXiaXian;
            }
            set
            {
                this.baoJingZhiXiaXian = value;
            }
        }

        public string DanWei
        {
            get
            {
                return this.danWei;
            }
            set
            {
                this.danWei = value;
            }
        }

        public float DuanDianZhi
        {
            get
            {
                return this.duanDianZhi;
            }
            set
            {
                this.duanDianZhi = value;
            }
        }

        public string FeiXianXingGuanXi
        {
            get
            {
                return this.feiXianXingGuanXi;
            }
            set
            {
                this.feiXianXingGuanXi = value;
            }
        }

        public float FuDianZhi
        {
            get
            {
                return this.fuDianZhi;
            }
            set
            {
                this.fuDianZhi = value;
            }
        }

        public string GuanJianZi
        {
            get
            {
                return this.guanJianZi;
            }
            set
            {
                this.guanJianZi = value;
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

        public float LiangChengDi
        {
            get
            {
                return this.liangChengDi;
            }
            set
            {
                this.liangChengDi = value;
            }
        }

        public float LiangChengGao
        {
            get
            {
                return this.liangChengGao;
            }
            set
            {
                this.liangChengGao = value;
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

        public int WuChaDai
        {
            get
            {
                return this.wuChaDai;
            }
            set
            {
                this.wuChaDai = value;
            }
        }

        public bool XianXingShuXing
        {
            get
            {
                return this.xianXingShuXing;
            }
            set
            {
                this.xianXingShuXing = value;
            }
        }
    }
}

