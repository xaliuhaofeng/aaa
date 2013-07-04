namespace Logic
{
    using System;
    using System.Data;

    public class KongZhiLiang
    {
        private int leixing;
        private string litaimingcheng;
        private string mingcheng;
        private string yitaimingcheng;

        public KongZhiLiang()
        {
        }

        public KongZhiLiang(string mingcheng)
        {
            this.mingcheng = mingcheng;
            DataTable dt = OperateDB.Select("select * from KongZhiLiang where mingCheng = '" + mingcheng + "'");
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                this.litaimingcheng = dt.Rows[0]["lingTaiMingCheng"].ToString();
                this.yitaimingcheng = dt.Rows[0]["yiTaiMingCheng"].ToString();
                this.leixing = Convert.ToInt32(dt.Rows[0]["kongZhiLiangLeiXing"]);
            }
        }

        public static DataTable GetKongAlarm(string ceDianBianHao)
        {
            return OperateDB.Select("select mingCheng, kongZhiLiangLeiXing, lingTaiMingCheng, yiTaiMingCheng from KongZhiLiang where mingCheng in(select mingCheng from KongZhiLiangCeDian where ceDianBianHao = '" + ceDianBianHao + "')");
        }

        public int Leixing
        {
            get
            {
                return this.leixing;
            }
            set
            {
                this.leixing = value;
            }
        }

        public string Litaimingcheng
        {
            get
            {
                return this.litaimingcheng;
            }
            set
            {
                this.litaimingcheng = value;
            }
        }

        public string Mingcheng
        {
            get
            {
                return this.mingcheng;
            }
            set
            {
                this.mingcheng = value;
            }
        }

        public string Yitaimingcheng
        {
            get
            {
                return this.yitaimingcheng;
            }
            set
            {
                this.yitaimingcheng = value;
            }
        }
    }
}

