namespace Logic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Timers;

    public class FenZhan
    {
        //1---4   :定义为老分站
        //5---    :定义为新分站

        private int commPort;

        //0-----通讯正常
        //1-----超时
        //2-----通讯失败
        //3-----初始化
        //4-----定义

        public volatile byte commState;

        private byte fenZhanNum;

        public byte FenZhanNum
        {
            get { return fenZhanNum; }
            set { fenZhanNum = value; }
        }
        private int recv_state = 0;
        private Timer timers;

        public  bool isTimeing;

        public FenZhan(byte fenZhanNum)
        {
            this.fenZhanNum = fenZhanNum;
            this.commState = 3;
            isTimeing = false;
        }

        public static byte[] CancelManualControl(byte fenZhanNum)
        {
            return new byte[] { LogicCommon.startByte, fenZhanNum, 0x4b, 0xff, 2, LogicCommon.endByte };
        }

        public static int CheckXiaoLeiXing(string xiaoleixing)
        {
            List<string> list = new List<string>();
            string s = "select mingCheng from MoNiLiangLeiXing union all select mingCheng from KaiGuanLiangLeiXing union all select mingCheng from KongZhiLiang";
            DataTable dt = OperateDB.Select(s);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == xiaoleixing)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        public static byte[] ChuanKouPeiZhi(byte[] chuanKou)
        {
            byte[] b = new byte[0x40];
            b[0] = LogicCommon.startByte;
            b[1] = 1;
            b[2] = 0x41;
            b[0x3f] = LogicCommon.endByte;
            for (int i = 0; i < 60; i++)
            {
                b[3 + i] = chuanKou[i];
                OperateDB.Execute(string.Concat(new object[] { "update FenZhanChuanKou set chuanKouHao=", chuanKou[i], " where fenZhanHao=", i + 1 }));
                if (chuanKou[i] > 0)
                {
                    Log.WriteLog(LogType.FenZhanPeiZhi, string.Concat(new object[] { i + 1, "#$", chuanKou[i], "#$成功" }));
                }
                GlobalParams.AllfenZhans[i + 1].CommPort = chuanKou[i];
            }

            return b;
        }

        public static byte[] CommTest(byte fenZhanNum)
        {
            return new byte[] { LogicCommon.startByte, fenZhanNum, 0x47, LogicCommon.endByte };
        }

        public static void deleteFengDianWaSiBiSuo(byte fenzhan)
        {
            OperateDB.Execute("delete from FengDianWaSi where fenZhanHao = " + fenzhan);
        }

        public void Dispatch(FenZhanRTdata ud)
        {
            if (!ud.isResponse)
            {
                this.commState = 0;
            }
            else
            {
                this.commState = 2;
            }
        }

        public static byte[] FengDianWaSiBiSuo(byte fenZhanHao, byte tongDao1, byte tongDao2, byte tongDao3, byte tongDao4, byte tongDao5, byte tongDao6)
        {
            return new byte[] { LogicCommon.startByte, fenZhanHao, 70, tongDao1, tongDao2, tongDao3, tongDao4, tongDao5, tongDao6, LogicCommon.endByte };
        }

        public static byte[] GetAllChuanKouHao()
        {
            int i;
            string s = "select top(60)* from FenZhanChuanKou ";
            DataTable dt = OperateDB.Select(s);
            if (dt.Rows.Count == 0)
            {
                for (i = 1; i <= 60; i++)
                {
                    OperateDB.Execute(string.Concat(new object[] { "insert into FenZhanChuanKou values(", i, ",", 0, ")" }));
                }
                s = "select * from FenZhanChuanKou order by fenZhanHao asc";
                dt = OperateDB.Select(s);
            }
            byte[] b = new byte[dt.Rows.Count];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                string obj = dt.Rows[i]["chuanKouHao"].ToString();
                if (obj != "")
                {
                    b[i] = Convert.ToByte(obj);
                }
                else
                {
                    b[i] = (byte) (i + 1);
                }
            }
            return b;
        }

        public static string[] GetAllConfigedFenZhan()
        {
            List<string> list = new List<string>();
            string sql = "select fenZhanHao from FenZhanChuanKou where chuanKouHao != 0 order by fenZhanHao";
            DataTable dt = OperateDB.Select(sql);
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                for (int i = 1; i <= 60; i++)
                {
                    list.Add(i.ToString());
                }
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row[0].ToString());
                }
            }
            return list.ToArray();
        }

        public static string[] GetAllConfigedFenZhan2()
        {
            List<string> list = new List<string>();
            string sql = "select fenZhanHao from FenZhanChuanKou where chuankouhao!=0 order by fenZhanHao";
            DataTable dt = OperateDB.Select(sql);
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                for (int i = 1; i <= 60; i++)
                {
                    list.Add(i.ToString());
                }
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row[0].ToString());
                }
            }
            return list.ToArray();
        }

        public static byte[] GetAllFenZhanHao()
        {
            byte[] b = new byte[60];
            for (byte i = 0; i < 60; i = (byte) (i + 1))
            {
                b[i] = (byte) (i + 1);
            }
            return b;
        }

        public static string[] GetAllXiaoLeiXing()
        {
            List<string> list = new List<string>();
            string s = "select mingCheng from MoNiLiangLeiXing union all select mingCheng from KaiGuanLiangLeiXing union all select mingCheng from KongZhiLiang";
            DataTable dt = OperateDB.Select(s);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i][0].ToString());
                }
            }
            return list.ToArray();
        }

        public static DataTable GetCeDianTongJiByFenZhan()
        {
            string sql = "select fenZhanHao, count(case when daLeiXing = 0 then 1 else null end), count(case when daLeiXing = 1 then 1 else null end), count(case when daLeiXing = 3 then 1 else null end), count(ceDianBianHao) from AllCeDian group by fenZhanHao";
            DataTable dt = OperateDB.Select(sql);
            if (dt != null)
            {
                sql = "select count(distinct fenZhanHao) as fenZhanHao, count(case when daLeiXing = 0 then 1 else null end), count(case when daLeiXing = 1 then 1 else null end), count(case when daLeiXing = 3 then 1 else null end), count(ceDianBianHao) from AllCeDian";
                DataTable dt1 = OperateDB.Select(sql);
                if (dt1 != null)
                {
                    DataRow row = dt.NewRow();
                    row["fenZhanHao"] = dt1.Rows[0][0];
                    row[1] = dt1.Rows[0][1];
                    row[2] = dt1.Rows[0][2];
                    row[3] = dt1.Rows[0][3];
                    row[4] = dt1.Rows[0][4];
                    dt.Rows.Add(row);
                }
                dt.Columns[0].ColumnName = "分站号";
                dt.Columns[1].ColumnName = "模拟量";
                dt.Columns[2].ColumnName = "开关量";
                dt.Columns[3].ColumnName = "控制量";
                dt.Columns[4].ColumnName = "总计";
            }
            return dt;
        }

        public static DataTable GetCeDianTongJiByLeiXing(string[] xiaoLeiXings)
        {
            string sql = "select fenZhanHao, ";
            foreach (string s in xiaoLeiXings)
            {
                sql = sql + "count(case when xiaoLeiXing = '" + s + "' then 1 else null end), ";
            }
            DataTable dt = OperateDB.Select(sql + "count(ceDianBianHao) from AllCeDian group by fenZhanHao");
            if (dt != null)
            {
                dt.Columns[0].ColumnName = @"分站\类型";
                for (int i = 1; i <= xiaoLeiXings.Length; i++)
                {
                    dt.Columns[i].ColumnName = xiaoLeiXings[i - 1];
                }
                dt.Columns[dt.Columns.Count - 1].ColumnName = "总计";
            }
            return dt;
        }

        public static DataTable GetCeDianTongJiByTongDao()
        {
            string sql = "select case when daLeiXing = 3 then '控制量' else 'I/O量' end, xiaoLeiXing, count(xiaoLeiXing) from AllCeDian group by daLeiXing, xiaoLeiXing ";
            DataTable dt = OperateDB.Select(sql);
            if (dt != null)
            {
                dt.Columns[0].ColumnName = "通道";
                dt.Columns[1].ColumnName = "名称";
                dt.Columns[2].ColumnName = "测点数";
            }
            return dt;
        }

        public int GetChuanKouHaoByFenZhanHao(int fenzhanhao)
        {
            DataTable dt = OperateDB.Select("select chuankouhao from fenzhanchuankou where fenzhanhao=" + fenzhanhao);
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public static byte[] GetFenZhanConfigInfo(byte fenZhanNum)
        {
            byte[] b = new byte[0xa6];
            b[0] = LogicCommon.startByte;
            b[1] = fenZhanNum;
            b[2] = 0x43;
            b[0xa5] = LogicCommon.endByte;
            byte[] temp = GlobalParams.AllCeDianList.GetAllConfInfoByFenZhan(fenZhanNum);
            for (int i = 0; i < 160; i++)
            {
                b[i + 3] = temp[i];
            }
            temp = KongZhiLiangCeDian.GetKongZhiLiangConfInfo(fenZhanNum);
            b[0xa3] = temp[0];
            b[0xa4] = temp[1];
            return b;
        }

        public static byte[] GetFenZhanHaoByChuanKouHao(int chuankouhao)
        {
            DataTable dt = OperateDB.Select("select fenzhanhao from fenzhanchuankou where chuankouhao=" + chuankouhao);
            byte[] b = new byte[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string obj = dt.Rows[i]["fenZhanHao"].ToString();
                if (obj != "")
                {
                    b[i] = Convert.ToByte(obj);
                }
                else
                {
                    b[i] = (byte) (i + 1);
                }
            }
            return b;
        }

        public static DataTable GetXiaoLeiXingAndCeDian()
        {
            DataTable dt = new DataTable();
            string s = "select mingCheng,'模拟量', count(ceDianBianHao) from MoNiLiangLeiXing,CeDian where mingCheng=xiaoLeiXing and daLeiXing=0 group by mingCheng";
            DataTable dt1 = OperateDB.Select(s);
            dt.Merge(dt1);
            s = "select mingCheng,'开关量', count(ceDianBianHao) from KaiGuanLiangLeiXing,CeDian where mingCheng=xiaoLeiXing and daLeiXing=1 group by mingCheng";
            dt1 = OperateDB.Select(s);
            dt.Merge(dt1);
            s = "select KongZhiLiang.mingCheng, '控制量', count(ceDianBianHao) from KongZhiLiang,KongZhiLiangCeDian where KongZhiLiang.mingCheng=KongZhiLiangCeDian.mingCheng group by KongZhiLiang.mingCheng";
            dt1 = OperateDB.Select(s);
            dt.Merge(dt1);
            dt.Columns[0].ColumnName = "名称";
            dt.Columns[1].ColumnName = "类型";
            dt.Columns[2].ColumnName = "测点数";
            return dt;
        }

        public static byte[] GuZhangBiSuo(byte fenZhanHao, bool enable)
        {
            byte[] b = new byte[5];
            b[0] = LogicCommon.startByte;
            b[1] = fenZhanHao;
            b[2] = 90;
            if (enable)
            {
                b[3] = 1;
            }
            else
            {
                b[3] = 0;
            }
            b[4] = LogicCommon.endByte;
            return b;
        }

        public static bool IsCurrentCeDian(byte fenZhan, byte tongDao, string ceDianBianHao)
        {
            byte fz = Convert.ToByte(ceDianBianHao.Substring(0, 2));
            byte td = Convert.ToByte(ceDianBianHao.Substring(3, 2));
            return ((fenZhan == fz) && (tongDao == td));
        }

        public static byte[] JiaoShi(byte fenZhanNum)
        {
            byte[] b = new byte[11];
            b[0] = LogicCommon.startByte;
            b[1] = fenZhanNum;
            b[2] = 0x54;
            DateTime dt = DateTime.Now;
            int year = dt.Year % 100;
            b[3] = (byte) (((year / 10) << 4) | (year % 10));
            year = dt.Month;
            b[4] = (byte) (((year / 10) << 4) | (year % 10));
            year = dt.Day;
            b[5] = (byte) (((year / 10) << 4) | (year % 10));
            year = (int) dt.DayOfWeek;
            b[6] = (byte) (((year / 10) << 4) | (year % 10));
            year = dt.Hour;
            b[7] = (byte) (((year / 10) << 4) | (year % 10));
            year = dt.Minute;
            b[8] = (byte) (((year / 10) << 4) | (year % 10));
            year = dt.Second;
            b[9] = (byte) (((year / 10) << 4) | (year % 10));
            b[10] = LogicCommon.endByte;
            return b;
        }

        public static DataTable LeiXingTongJi()
        {
            string s = "select xiaoLeiXing,fenZhanHao from CeDian order by fenZhanHao";
            DataTable dt = OperateDB.Select(s);
            s = "select mingCheng,fenZhanHao from KongZhiLiangCeDian";
            DataTable dt1 = OperateDB.Select(s);
            dt.Merge(dt1);
            DataTable returnDt = new DataTable();
            returnDt.Columns.Add("分站");
            Hashtable ht = new Hashtable();
            int rowCount = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string name = dt.Rows[i][0].ToString();
                if (!dt.Columns.Contains(name))
                {
                    returnDt.Columns.Add(name);
                }
                object fenZhanHao = dt.Rows[i][1];
                if (!ht.ContainsKey(fenZhanHao))
                {
                    ht.Add(fenZhanHao, rowCount);
                    rowCount++;
                }
            }
            returnDt.Columns.Add("总数");
            return returnDt;
        }

        public static void SaveFengDianWaSi(byte fenZhanHao, string jinFengWaSi, string huiFengWaSi, string chuanLianTongFeng, string juShan, string fengTongFengLiang, string kongZhiLiang)
        {
            string s;
            if (OperateDB.Select("select * from FengDianWaSi where fenZhanHao=" + fenZhanHao).Rows.Count > 0)
            {
                s = string.Concat(new object[] { "update FengDianWaSi set jinFengWaSi='", jinFengWaSi, "',huiFengWaSi='", huiFengWaSi, "',chuanLianTongFeng='", chuanLianTongFeng, "',juShan='", juShan, "',fengTongFengLiang='", fengTongFengLiang, "',kongZhiLiang='", kongZhiLiang, "' where fenZhanHao=", fenZhanHao });
            }
            else
            {
                s = string.Concat(new object[] { "insert into FengDianWaSi(fenZhanHao,jinFengWaSi,huiFengWaSi,chuanLianTongFeng,juShan,fengTongFengLiang,kongZhiLiang) values(", fenZhanHao, ",'", jinFengWaSi, "','", huiFengWaSi, "','", chuanLianTongFeng, "','", juShan, "','", fengTongFengLiang, "','", kongZhiLiang, "')" });
            }
            OperateDB.Execute(s);
        }

        public static byte[] SetManualControl(byte fenZhanNum, byte kongZhiLiangNum, bool open)
        {
            byte[] b = new byte[6];
            b[0] = LogicCommon.startByte;
            b[1] = fenZhanNum;
            b[2] = 0x4b;
            b[3] = kongZhiLiangNum;
            if (open)
            {
                b[4] = 0;
            }
            else
            {
                b[4] = 1;
            }
            b[5] = LogicCommon.endByte;
            return b;
        }

        public void StopTimer()
        {
            this.timers.Stop();
        }

        public static byte[] YuanChengDianYuanGuanLi(byte fenZhanHao)
        {
            return new byte[] { LogicCommon.startByte, fenZhanHao, 80, 1, LogicCommon.endByte };
        }

        public int CommPort
        {
            get
            {
                return this.commPort;
            }
            set
            {
                this.commPort = value;
            }
        }

        public int Recv_state
        {
            get
            {
                return this.recv_state;
            }
            set
            {
                this.recv_state = value;
            }
        }


        /// <summary>
        ///  设置分站的全部中断
        /// </summary>
        /// <param name="fno"></param>
        public void setCommInter( int fno)
        {
            for (int i = 0; i < 17; i++)
            {
                CeDian cd = GlobalParams.AllCeDianList.GetMKCedian(fno, i);
                if (cd != null)
                {
                    cd.RtState = 0x10000;
                    cd.RtState_pre = 0x10000;
                    GlobalParams.AllCeDianList.updateCedian(cd.CeDianBianHao, cd);
                }              
            }

            for (int i = 1; i < 9; i++)
            {
                CeDian cd = GlobalParams.AllCeDianList.GetCtrCedian(fno, i);
                if (cd != null)
                {
                    cd.RtState = 0x10000;
                    cd.RtState_pre = 0x10000;
                    GlobalParams.AllCeDianList.updateCedian(cd.CeDianBianHao, cd);
                }
            }         
        }
    }
}

