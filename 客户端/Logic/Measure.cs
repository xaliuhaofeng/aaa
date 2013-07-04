using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace Logic
{
    public class Measure
    {
        private string cedianbianhao;
        private string cuoshi;
        private DateTime cuoshishijian;
        private DateTime Time;
        private byte Type;
        private long CeDianId;

        public Measure(long cedianId, string cedianbianhao, string cuoshi, DateTime cuoshishijian, DateTime time, byte type)
        {
            this.CeDianId = cedianId;
            this.cedianbianhao = cedianbianhao;
            this.cuoshi = cuoshi;
            this.cuoshishijian = cuoshishijian;
            this.Time = time;
            this.Type = type;
            //Type=1——模拟量报警
            //Type=2——模拟量断电
            //Type=3——开关量报警
            //Type=4——开关量断电
            //Type=0——其他所有
        }

        public void Insert()
        {
            string sql = "insert into Measure values('" + cedianbianhao + "', '" + cuoshi + "', '" + cuoshishijian + "', '" + Users.GlobalUserName + "'," + Type + ",'" + Time + "'," + CeDianId + ")";
            OperateDB.Execute(sql);
        }

        public static string GetMeasure(string ceDianBianHao, DateTime start, DateTime end)
        {
            string measure = string.Empty;
            //string sql = "select CuoShi, CuoShiShiJian from Measure where CeDianBianHao = '" + ceDianBianHao + "' and CuoShiShiJian between '" + start + "' and '" + end + "'";
            //DataTable dt = OperateDB.Select(sql);
            //if (dt != null)
            //{
            //    foreach (DataRow row in dt.Rows)
            //        measure += row[0].ToString() + "," + row[1].ToString() + "\n";
            //}

            return measure;
        }

        public static string GetMeasure(long cedianId, DateTime start, DateTime end)
        {
            string measure = string.Empty;

            string sql = "select CuoShi, CuoShiShiJian from Measure where CeDianId = " + cedianId + " and CuoShiShiJian between '" + start + "' and '" + end + "'";
            DataTable dt = OperateDB.Select(sql);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                    measure += row[0].ToString() + "," + row[1].ToString() + "\n";
            }

            return measure;
        }

        public static string GetMeasure(string ceDianBianHao, byte type, DateTime time)
        {
            string measure = string.Empty;
            string sql = "select CuoShi, CuoShiShiJian from Measure where CeDianBianHao = '" + ceDianBianHao + "' and ZhuangTaiBiaoShi = " + type + " and ZhuangTaiShiKe = '" + time + "'";
            DataTable dt = OperateDB.Select(sql);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                    measure += row[0].ToString() + "," + row[1].ToString() + "\n";
            }

            return measure;
        }

        public static string GetMeasure(long cedianId, byte type, DateTime time)
        {
            string measure = string.Empty;
            string sql = "select CuoShi, CuoShiShiJian from Measure where CeDianId = " + cedianId + " and ZhuangTaiBiaoShi = " + type + " and ZhuangTaiShiKe = '" + time + "'";
            DataTable dt = OperateDB.Select(sql);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                    measure += row[0].ToString() + "," + row[1].ToString() + "\n";
            }

            return measure;
        }
    }
}
