namespace Logic
{
    using System;
    using System.Data;

    public class TiaoJiao
    {
        private string ceDianBianHao;
        private DateTime? endTime;
        private int fenZhan;
        private DateTime startTime;
        private int tongDao;

        public TiaoJiao()
        {
        }

        public TiaoJiao(int fenZhan, int tongDao)
        {
            this.fenZhan = fenZhan;
            this.tongDao = tongDao;
        }

        public TiaoJiao(string bianhao, int fenzhan, int tongdao, DateTime start, DateTime? finish)
        {
            this.ceDianBianHao = bianhao;
            this.fenZhan = fenzhan;
            this.tongDao = tongdao;
            this.startTime = start;
            this.endTime = finish;
        }

        public static void DeleteAllTiaoJiao(DateTime finish)
        {
            string sql = "select ceDianBianHao from CeDian where tiaoJiao = 'true'";
            DataTable dt = OperateDB.Select(sql);
            foreach (DataRow row in dt.Rows)
            {
                string ceDianBianHao = row[0].ToString();
                OperateDB.Execute(string.Concat(new object[] { "update TiaoJiao set finishTime = '", finish, "' where ceDianBianHao = '", ceDianBianHao, "' and finishTime is null" }));
            }
            sql = "update CeDian set tiaoJiao = 'false' where tiaoJiao = 'true'";
            OperateDB.Execute(sql);
        }

        public static void DeleteTiaoJiao(int fenzhan, int tongdao, DateTime endTime)
        {
            OperateDB.Execute(string.Concat(new object[] { "update TiaoJiao set finishTime = '", endTime, "' where fenZhanHao = ", fenzhan, "and tongDaoHao = ", tongdao, " and finishTime is null" }));
        }

        public static DataTable GetAllTiaoJiaoById(int fenZhan, int tongDao)
        {
            return OperateDB.Select(string.Concat(new object[] { "select id,ceDianBianHao, fenZhanHao, tongDaoHao, startTime, finishTime from TiaoJiao where fenZhanHao = '", fenZhan, "' and tongDaoHao = '", tongDao, "'order by startTime desc;" }));
        }

        public void InsertIntoDB()
        {
            string sql = string.Empty;
            if (!this.endTime.HasValue)
            {
                sql = string.Concat(new object[] { "insert into TiaoJiao values ('", this.ceDianBianHao, "', '", this.fenZhan, "', '", this.tongDao, "', '", this.startTime, "', null);" });
            }
            else
            {
                sql = string.Concat(new object[] { "insert into TiaoJiao values ('", this.ceDianBianHao, "', '", this.fenZhan, "', '", this.tongDao, "', '", this.startTime, "', '", this.endTime, "');" });
            }
            OperateDB.Execute(sql);
        }

        public static void updateFinishTime(int id, DateTime finish)
        {
            OperateDB.Execute(string.Concat(new object[] { "update TiaoJiao set finishTime = '", finish, "' where id = '", id, "';" }));
        }

        public static void updateHistoryDataState(int id, string cedianbianhao)
        {
            DataTable dt = OperateDB.Select("select * from TiaoJiao where id = '" + id + "';");
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                DateTime start = Convert.ToDateTime(dt.Rows[0]["startTime"]);
                DateTime end = Convert.ToDateTime(dt.Rows[0]["finishTime"]);
                string table = "MoNiLiangValue" + start.ToString("yyyy_MM");
                OperateDB.Execute(string.Concat(new object[] { "update ", table, " set state = '", 9, "' where ceDianBianHao = '", cedianbianhao, "' and uploadTime between '", start, "' and '", end, "';" }));
            }
        }

        public static void uploadHistoryDataState(CeDian cedian, DateTime finish)
        {
            DataTable dt = OperateDB.Select(string.Concat(new object[] { "select * from TiaoJiao where finishTime = '", finish, "' and ceDianBianHao = '", cedian.CeDianBianHao, "'" }));
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                DateTime start = Convert.ToDateTime(dt.Rows[0]["startTime"]);
                DateTime end = Convert.ToDateTime(dt.Rows[0]["finishTime"]);
                string table = "MoNiLiangValue" + start.ToString("yyyy_MM");
                OperateDB.Execute(string.Concat(new object[] { "update ", table, " set state = '", 9, "' where ceDianBianHao = '", cedian.CeDianBianHao, "' and uploadTime between '", start, "' and '", end, "';" }));
            }
        }
    }
}

