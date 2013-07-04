namespace Logic
{
    using System;
    using System.Data;

    public class YuJing
    {
        private float changeRate;
        private int fenZhan;
        public static bool kk = true;
        private static byte Laa = 0;
        private static byte Las = 0;
        private static byte Lca = 0;
        private static byte Lcs = 0;
        private static byte Lfa = 0;
        private static byte Lfs = 0;
        private int tongDao;
        private float yuJingValue;

        public YuJing(int fenzhan, int tongdao)
        {
            this.fenZhan = fenzhan;
            this.tongDao = tongdao;
        }

        public YuJing(int fenZhan, int tongDao, float yuJingValue, float changeRate)
        {
            this.fenZhan = fenZhan;
            this.tongDao = tongDao;
            this.yuJingValue = yuJingValue;
            this.changeRate = changeRate;
        }

        public static void DeleteYuJing(string ceDianBianHao)
        {

            CeDian cedian = GlobalParams.AllCeDianList.getCedianInfo(ceDianBianHao);
            if (cedian != null)
                OperateDB.Execute(string.Concat(new object[] { "delete from YuJing where fenZhanHao = ", cedian.FenZhanHao, " and tongDaoHao = ", cedian.TongDaoHao }));
        }

        public bool Exist()
        {
            DataTable dt = OperateDB.Select(string.Concat(new object[] { "select * from YuJing where fenZhanHao = ", this.fenZhan, "and tongDaoHao = ", this.tongDao }));
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                return false;
            }
            this.yuJingValue = Convert.ToSingle(dt.Rows[0]["yuJingZhi"]);
            this.changeRate = Convert.ToSingle(dt.Rows[0]["bianHuaZhi"]);
            return true;
        }

        public static DataTable GetAllYuJing()
        {
            string sql = "select ceDianBianHao, ceDianWeiZhi, xiaoLeiXing, baoJingZhiShangXian, yuJingZhi, bianHuaZhi from MoNiLiangLeiXing, YuJing inner join CeDian on YuJing.fenZhanHao = CeDian.fenZhanHao and YuJing.tongDaoHao = CeDian.tongDaoHao where CeDian.xiaoLeiXing = MoNiLiangLeiXing.mingCheng  and CeDian.weiShanChu=1";
            return OperateDB.Select(sql);
        }

        public static bool getValue(byte a)
        {
            if (!kk)
            {
                return false;
            }
            if (((((Laa == 0) && (Las == 0)) && ((Lca == 0) && (Lcs == 0))) && (Lfa == 0)) && (Lfs == 0))
            {
                return true;
            }
            if (a == 1)
            {
                return (Laa == 1);
            }
            if (a == 2)
            {
                return (Las == 1);
            }
            if (a == 3)
            {
                return (Lca == 1);
            }
            if (a == 4)
            {
                return (Lcs == 1);
            }
            if (a == 5)
            {
                return (Lfa == 1);
            }
            return ((a == 6) && (Lfs == 1));
        }

        public void InsertIntoDB()
        {
            OperateDB.Execute(string.Concat(new object[] { "insert into YuJing values('", this.fenZhan, "', '", this.tongDao, "', '", this.YuJingValue, "', '", this.ChangeRate, "')" }));
        }

        public static void setValue(byte a)
        {
            Laa = 0;
            Las = 0;
            Lca = 0;
            Lcs = 0;
            Lfa = 0;
            Lfs = 0;
            switch (a)
            {
                case 1:
                    Laa = 1;
                    break;

                case 2:
                    Las = 1;
                    break;

                case 3:
                    Lca = 1;
                    break;

                case 4:
                    Lcs = 1;
                    break;

                case 5:
                    Lfa = 1;
                    break;

                case 6:
                    Lfs = 1;
                    break;
            }
        }

        public void update()
        {
            OperateDB.Execute(string.Concat(new object[] { "update YuJing set yuJingZhi = ", this.YuJingValue, ", bianHuaZhi = ", this.ChangeRate, " where fenZhanHao = ", this.fenZhan, "and tongDaoHao = ", this.tongDao }));
        }

        public float ChangeRate
        {
            get
            {
                return this.changeRate;
            }
            set
            {
                this.changeRate = value;
            }
        }

        public float YuJingValue
        {
            get
            {
                return this.yuJingValue;
            }
            set
            {
                this.yuJingValue = value;
            }
        }
    }
}

