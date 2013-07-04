namespace Logic
{
    using System;
    using System.Linq;

    public class FenZhanRTdata
    {
        public byte AC;
        private byte dianYa;
        public byte fenZhanHao;
        public bool[] isMoNiLiang = new bool[0x11];
        public bool isResponse;
        public byte[] kongZhiLiangZhuangTai = new byte[9];
        public float[] realValue = new float[0x11];
        public string responseInfo = "";
        public int[] tongDaoData = new int[0x11];
        public byte[] tongDaoZhuangTai = new byte[0x11];
        public DateTime uploadTime;
        bool isTest = true;

        public void CalculateData(byte[] buf)
        {
            string fenZhan;
            int i;
            string tongdao;

            if (GlobalParams.Initing)
                return;
            if (buf[2] == 0x44)
            {
                this.isResponse = false;
                this.fenZhanHao = buf[1];
                this.uploadTime = DateTime.Now;
                GlobalParams.AllfenZhans[this.fenZhanHao].commState = 0;
                this.AC = buf[0x24];
                this.dianYa = (byte) (buf[0x24] & 15);
                fenZhan = buf[1].ToString();
                if (fenZhan.Length == 1)
                {
                    fenZhan = '0' + fenZhan;
                }
                string bh = "";
                CeDian cedian = GlobalParams.AllCeDianList.GetMKCedian(this.fenZhanHao, 0);
                if (cedian != null)
                {
                    cedian.RtState = this.AC & 15;
                    float astate = cedian.RtState_pre;
                    if (cedian.RtState != astate)
                    {
                        cedian.Time = this.uploadTime;
                        cedian.RtVal = this.dianYa & 15;
                        cedian.RtState_pre = cedian.RtState;
                        cedian.RtVal_pre = cedian.RtVal;
                        if (cedian.IsMultiBaoji)
                            GlobalParams.cedian_alarm.do_mAlarm_pro(cedian);
                        else
                            GlobalParams.cedian_alarm.do_alarm_pro(cedian, cedian.RtVal);
                    }                   
                       

                    if (cedian.Time == DateTime.MinValue)
                    {
                        cedian.Time = this.uploadTime;
                    }
                    
                    
                    bh = cedian.CeDianBianHao;

                    GlobalParams.AllCeDianList.allcedianlist[bh] = cedian;
                }
  
                for ( i = 1; i < 17; i++)
                {
                    int startPos = 3 + ((i - 1) * 2);
                    tongdao = i.ToString();
                    if (i < 10)
                    {
                        tongdao = '0' + tongdao;
                    }
                    cedian = GlobalParams.AllCeDianList.GetMKCedian(this.fenZhanHao, i);
                    if (cedian == null)
                        continue;

                    float aval;
                    bh = cedian.CeDianBianHao;
                    int daLeiXing = cedian.DaLeiXing;
                    string xiaoLeiXing = cedian.XiaoleiXing;
                    byte chuanGanQiLeiXing = (byte)cedian.ChuanGanQiLeiXing;
                    this.tongDaoZhuangTai[i] = (byte)(buf[startPos] >> 4);
                    if (daLeiXing == 0)
                    {
                        this.isMoNiLiang[i] = true;
                        if (this.tongDaoZhuangTai[i] != 15)
                        {
                            this.tongDaoData[i] = buf[startPos + 1] + ((buf[startPos] & 15) << 8);
                            if (cedian.MoNiLiang == null)
                            {
                                
                                    Console.WriteLine("无法得到指定模拟量的信息，xiaoLeiXing = " + xiaoLeiXing);
                                
                            }
                            else
                            {
                                if (cedian.MoNiLiang.XianXingShuXing)
                                {
                                    float liangChengDi = cedian.MoNiLiang.LiangChengDi;
                                    float liangCheng = cedian.MoNiLiang.LiangChengGao - liangChengDi;
                                    if (chuanGanQiLeiXing == 1)
                                    {
                                        this.realValue[i] = ((this.tongDaoData[i] - 300f) * liangCheng) / 1200f;
                                    }
                                    else
                                    {
                                        this.realValue[i] = ((this.tongDaoData[i] - 200) * liangCheng) / 800f;
                                    }
                                    float v = this.realValue[i];
                                    this.realValue[i] = (float)Math.Round((double)this.realValue[i], 2);
                                }
                                else
                                {
                                    string jj = GlobalParams.show_cedian.MoNiLiang.FeiXianXingGuanXi;
                                    XianXingZhi xxz = GlobalParams.AllXianXingZhi.GetFeiXianXing(jj);
                                    jj = xxz.Value0.ToString();
                                    float valueDi = float.Parse(xxz.Value0.ToString());
                                    jj = xxz.Value4.ToString();
                                    float valueGao = float.Parse(xxz.Value4.ToString());
                                    float valueLiangcheng = float.Parse(xxz.LiangCheng4.ToString()) - float.Parse(xxz.LiangCheng0.ToString());
                                    byte flag = 0;
                                    if (this.tongDaoData[i] >= float.Parse(xxz.Value0.ToString()))
                                    {
                                        valueDi = float.Parse(xxz.Value0.ToString());
                                        valueGao = float.Parse(xxz.Value1.ToString());
                                        valueLiangcheng = float.Parse(xxz.LiangCheng1.ToString()) - float.Parse(xxz.LiangCheng0.ToString());
                                        flag = 0;
                                    }
                                    if (this.tongDaoData[i] >= float.Parse(xxz.Value1.ToString()))
                                    {
                                        valueDi = float.Parse(xxz.Value1.ToString());
                                        valueGao = float.Parse(xxz.Value2.ToString());
                                        valueLiangcheng = float.Parse(xxz.LiangCheng2.ToString()) - float.Parse(xxz.LiangCheng1.ToString());
                                        flag = 1;
                                    }
                                    if (this.tongDaoData[i] >= float.Parse(xxz.Value2.ToString()))
                                    {
                                        valueDi = float.Parse(xxz.Value2.ToString());
                                        valueGao = float.Parse(xxz.Value3.ToString());
                                        valueLiangcheng = float.Parse(xxz.LiangCheng3.ToString()) - float.Parse(xxz.LiangCheng2.ToString());
                                        flag = 2;
                                    }
                                    if (this.tongDaoData[i] >= float.Parse(xxz.Value3.ToString()))
                                    {
                                        valueDi = float.Parse(xxz.Value3.ToString());
                                        valueGao = float.Parse(xxz.Value4.ToString());
                                        valueLiangcheng = float.Parse(xxz.LiangCheng4.ToString()) - float.Parse(xxz.LiangCheng3.ToString());
                                        flag = 3;
                                    }
                                    if (flag == 0)
                                    {
                                        this.realValue[i] = (((this.tongDaoData[i] - valueDi) * valueLiangcheng) / (valueGao - valueDi)) + float.Parse(xxz.LiangCheng0.ToString());
                                        this.realValue[i] = (float)Math.Round((double)this.realValue[i], 2);
                                    }
                                    if (flag == 1)
                                    {
                                        this.realValue[i] = (((this.tongDaoData[i] - valueDi) * valueLiangcheng) / (valueGao - valueDi)) + float.Parse(xxz.LiangCheng1.ToString());
                                        this.realValue[i] = (float)Math.Round((double)this.realValue[i], 2);
                                    }
                                    if (flag == 2)
                                    {
                                        this.realValue[i] = (((this.tongDaoData[i] - valueDi) * valueLiangcheng) / (valueGao - valueDi)) + float.Parse(xxz.LiangCheng2.ToString());
                                        this.realValue[i] = (float)Math.Round((double)this.realValue[i], 2);
                                    }
                                    if (flag == 3)
                                    {
                                        this.realValue[i] = (((this.tongDaoData[i] - valueDi) * valueLiangcheng) / (valueGao - valueDi)) + float.Parse(xxz.LiangCheng3.ToString());
                                        this.realValue[i] = (float)Math.Round((double)this.realValue[i], 2);
                                    }
                                }
                                cedian.RtState = this.tongDaoZhuangTai[i];
                                aval = cedian.RtVal_pre;
                                if ((this.realValue[i] != aval) || (cedian.RtState != cedian.RtState_pre))
                                {
                                    cedian.Time = this.uploadTime;
                                    cedian.RtVal = this.realValue[i];
                                    if (cedian.RtVal > cedian.MoNiLiang.LiangChengGao)
                                        cedian.RtVal = cedian.MoNiLiang.LiangChengGao;
                                    cedian.RtState_pre = this.tongDaoZhuangTai[i];
                                    cedian.RtVal_pre = this.realValue[i];                                   
                                    GlobalParams.cedian_alarm.do_alarm_pro(cedian, cedian.RtVal);
                                }

                                bh = cedian.CeDianBianHao;
                                GlobalParams.AllCeDianList.allcedianlist[bh] = cedian;
                            }
                        }
                        else
                        {
                            this.realValue[i] = 0f;
                        }
                    }
                    else
                    {
                        this.isMoNiLiang[i] = false;
                        this.realValue[i] = buf[startPos + 1];
                        cedian.RtState = this.tongDaoZhuangTai[i];
                        cedian.RtVal = this.realValue[i];
                        aval = cedian.RtVal_pre;
                        int ast = (int)cedian.RtState_pre;
                        if ((cedian.RtVal != aval) || (ast != cedian.RtState))
                        {
                            cedian.Time = this.uploadTime;
                            cedian.RtState_pre = cedian.RtState;
                            cedian.RtVal_pre = cedian.RtVal;
                            if (cedian.IsMultiBaoji)
                                GlobalParams.cedian_alarm.do_mAlarm_pro(cedian);
                            else
                                GlobalParams.cedian_alarm.do_alarm_pro(cedian, cedian.RtVal);
                        }
                        bh = cedian.CeDianBianHao;
                        GlobalParams.AllCeDianList.allcedianlist[bh] = cedian;
                    }


                }
                for (i = 1; i < 9; i++)
                {
                    byte kzlzt = (byte) ((buf[0x23] >> (i - 1)) & 1);
                    this.kongZhiLiangZhuangTai[i] = kzlzt;
                    cedian = GlobalParams.AllCeDianList.GetCtrCedian(this.fenZhanHao, i);
                    if (cedian != null)
                    {
                        cedian.RtState = (int)kzlzt;
                        cedian.RtVal = (int)kzlzt;
                        if (cedian.Time == DateTime.MinValue)                        
                            cedian.Time = this.uploadTime;
                        
                        if (!((cedian.RtState == cedian.RtState_pre) && (cedian.RtVal == cedian.RtVal_pre)))
                        {
                            cedian.Time = this.uploadTime;                           
                            cedian.RtState_pre = cedian.RtState;
                            cedian.RtVal_pre = cedian.RtVal;
                            GlobalParams.cedian_alarm.do_alarm_pro(cedian, cedian.RtVal);
                        }
                       
                        bh = cedian.CeDianBianHao;
                        GlobalParams.AllCeDianList.allcedianlist[bh] = cedian;
                    }                  
                }

                // 处理预警

                if (GlobalParams.AllfenZhans[fenZhanHao].CommPort > 4)
                {
                    try
                    {
                        byte[] time = new byte[6];
                        Array.Copy(buf, 37, time, 0, 6);

                        if (GlobalParams.AllfenZhans[fenZhanHao].isTimeing)
                        {
                            if (!IsTime(time))
                            {
                                byte[] b = FenZhan.JiaoShi(fenZhanHao);
                                Log.WriteLog(LogType.JiaoShi, string.Concat(new object[] { fenZhanHao, "#$", DateTime.Now, "#$启动校时" }));
                                UDPComm.Send(b);
                            }
                        }
                        else
                        {
                            byte[] b = FenZhan.JiaoShi(fenZhanHao);
                            Log.WriteLog(LogType.JiaoShi, string.Concat(new object[] { fenZhanHao, "#$", DateTime.Now, "#$启动校时" }));
                            UDPComm.Send(b);
                            GlobalParams.AllfenZhans[fenZhanHao].isTimeing = true;
                        }
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine("Timing error "+ee.ToString());
                    }
                    


                }
            }
            else
            {
                this.isResponse = true;
                this.fenZhanHao = buf[1];
                string temp = "分站" + buf[1] + "：";
                if (buf[2] == 0x41)
                {
                    if (buf[3] == 1)
                    {
                        this.responseInfo = temp + "配置分站串口号成功！";
                    }
                    else
                    {
                        this.responseInfo = temp + "配置分站串口号失败！";
                    }
                }
                else if (buf[2] == 0x54)
                {
                    if (buf[3] == 1)
                    {
                        Log.WriteLog(LogType.JiaoShi, string.Concat(new object[] { this.fenZhanHao, "#$", DateTime.Now, "#$成功" }));
                        this.responseInfo = temp + "校时成功！";
                    }
                    else
                    {
                        Log.WriteLog(LogType.JiaoShi, string.Concat(new object[] { this.fenZhanHao, "#$", DateTime.Now, "#$失败" }));
                        this.responseInfo = temp + "校时失败！";
                    }
                }
                else if (buf[2] == 0x4b)
                {
                    if (buf[3] != 0)
                    {
                        this.responseInfo = temp + "手动控制操作成功！";
                    }
                    else
                    {
                        this.responseInfo = temp + "手动控制操作失败！";
                    }
                }
                else if (buf[2] == 0x43)
                {
                    if (buf[3] == 1)
                    {
                        this.responseInfo = temp + "测点配置成功！";
                        GlobalParams.AllfenZhans[this.fenZhanHao].commState = 0;
                    }
                    else
                    {
                        this.responseInfo = temp + "测点配置失败！";
                    }
                }
                else if (buf[2] == 0x47)
                {
                    object Reflector0003;
                    this.responseInfo = temp + "\r\n";
                    for (i = 3; i < 0x13; i++)
                    {
                        if (i == 11)
                        {
                            this.responseInfo = this.responseInfo + "\r\n";
                        }
                        Reflector0003 = this.responseInfo;
                        this.responseInfo = string.Concat(new object[] { Reflector0003, "通道", i - 2, "：", this.calculateState(buf[i]), "；" });
                    }
                    this.responseInfo = this.responseInfo + "\r\n";
                    for (i = 0; i < 8; i++)
                    {
                        string fenzhan = this.fenZhanHao.ToString();
                        if (fenzhan.Length == 1)
                        {
                            fenzhan = "0" + fenzhan;
                        }
                        tongdao = "0" + ((i + 1)).ToString();
                        string cedianbianhao = fenzhan + "C" + tongdao;
                        byte state = (byte) ((buf[0x13] >> i) & 1);
                        string sstate = state.ToString();
                        if (state == 0)
                        {
                        }
                        Reflector0003 = this.responseInfo;
                        this.responseInfo = string.Concat(new object[] { Reflector0003, "控制量", i + 1, "：", sstate, "；" });
                    }
                    this.responseInfo = this.responseInfo + "\r\n";
                    string s = "";
                    if (buf[20] == 0)
                    {
                        s = "交流";
                    }
                    else
                    {
                        s = "直流";
                    }
                    this.responseInfo = this.responseInfo + "交直流状态：" + s;
                }
                else if (buf[2] == 90)
                {
                    if (buf[3] == 1)
                    {
                        Log.WriteLog(LogType.GuZhangBiSuo, this.fenZhanHao + "#$#$成功");
                        this.responseInfo = temp + "故障闭锁配置成功！";
                    }
                    else
                    {
                        Log.WriteLog(LogType.GuZhangBiSuo, this.fenZhanHao + "#$#$失败");
                        this.responseInfo = temp + "故障闭锁配置失败！";
                    }
                }
                else if (buf[2] == 70)
                {
                    if (buf[3] == 1)
                    {
                        Log.WriteLog(LogType.FengDianWaSiBiSuo, this.fenZhanHao + "#$#$配置成功");
                        this.responseInfo = temp + "风电瓦斯闭锁配置成功！";
                    }
                    else
                    {
                        Log.WriteLog(LogType.FengDianWaSiBiSuo, this.fenZhanHao + "#$#$配置失败");
                        this.responseInfo = temp + "风电瓦斯闭锁配置失败！";
                    }
                }
                else if (buf[2] == 0x42)
                {
                    this.responseInfo = "";
                    if ((buf.Length == 5) && (buf[3] == 1))
                    {
                        fenZhan = GlobalParams.lastCutFenZhan;
                        if (fenZhan.Length == 1)
                        {
                            fenZhan = '0' + fenZhan;
                        }
                        tongdao = GlobalParams.lastCutTongDao;
                        if (tongdao.Length == 1)
                        {
                            tongdao = '0' + tongdao;
                        }
                        string[] cedianbianhao = GlobalParams.AllCeDianList.allcedianlist.Keys.ToArray<string>();
                        foreach (string bianhao in cedianbianhao)
                        {
                            if ((bianhao.Substring(0, 2) == fenZhan) && (bianhao.Substring(3, 2) == tongdao))
                            {
                                if (GlobalParams.lastCutType == 0)
                                {
                                    if (buf[3] == 1)
                                    {
                                        GlobalParams.AllCeDianList.allcedianlist[bianhao].FuDianFlag = false;
                                    }
                                    else
                                    {
                                        GlobalParams.AllCeDianList.allcedianlist[bianhao].FuDianFlag = true;
                                    }
                                }
                                else if (GlobalParams.lastCutType == 1)
                                {
                                    if (buf[3] == 1)
                                    {
                                        GlobalParams.AllCeDianList.allcedianlist[bianhao].DuanDianFlag = false;
                                    }
                                    else
                                    {
                                        GlobalParams.AllCeDianList.allcedianlist[bianhao].DuanDianFlag = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (buf[2] == 80)
                {
                    if (buf[3] == 1)
                    {
                        this.responseInfo = temp + "电源板重启成功！";
                    }
                    else
                    {
                        this.responseInfo = temp + "电源板重启失败！";
                    }
                }
                else if (buf[2] == 0x45)
                {
                    GlobalParams.AllfenZhans[this.fenZhanHao].commState = 2;

                    CeDian cedian = GlobalParams.AllCeDianList.GetMKCedian(this.fenZhanHao, 0);
                    if (cedian != null)
                    {
                        cedian.RtState_pre = 100000;
                    }
                    if (GlobalParams.AllfenZhans[this.fenZhanHao].CommPort > 0)
                    {
                        this.responseInfo = temp + "通信失败！";
                    }

                    // UDPComm.Send(FenZhan.GetFenZhanConfigInfo(fenZhanHao));
                }
            }
        }

      

        public string calculateState(byte state)
        {
            switch (state)
            {
                case 0:
                    return "正常";

                case 1:
                    return "报警";

                case 2:
                    return "断电";

                case 3:
                    return "复电";

                case 4:
                    return "断线";

                case 5:
                    return "溢出";

                case 6:
                    return "负漂";

                case 7:
                    return "故障";

                case 8:
                    return "IO错误";

                case 0x11:
                    return "未配置";
            }
            return state.ToString();
        }

        public void ClearFenZhanRTdata()
        {
            Array.Clear(this.tongDaoZhuangTai, 0, 0x11);
            Array.Clear(this.tongDaoData, 0, 0x11);
            Array.Clear(this.isMoNiLiang, 0, 0x11);
            Array.Clear(this.kongZhiLiangZhuangTai, 0, 9);
            Array.Clear(this.realValue, 0, 0x11);
        }

        private bool IsTime(byte[] time)
        {
            DateTime CurTime = DateTime.Now;
            int[] t = new int[6];
            t[0] = 2000 + ((time[0] >> 4) * 10) + (time[0] & 0x0f);
            t[1] = ((time[1] >> 4) * 10) + (time[1] & 0x0f);
            t[2] = ((time[2] >> 4) * 10) + (time[2] & 0x0f);
            t[3] = ((time[3] >> 4) * 10) + (time[3] & 0x0f);
            t[4] = ((time[4] >> 4) * 10) + (time[4] & 0x0f);
            t[5] = ((time[5] >> 4) * 10) + (time[5] & 0x0f);
            if (t[0] <2001)
                return false;
            DateTime recv_time;
            try
            {
                recv_time = new DateTime(t[0], t[1], t[2], t[3], t[4], t[5]);
            }
            catch (Exception ee)
            {
                Console.WriteLine("datetime error " + ee.ToString());
                return false;
            }

            TimeSpan ts1 = new TimeSpan(CurTime.Ticks);
            TimeSpan ts2 = new TimeSpan(recv_time.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();

            if (ts.TotalHours > 1 )
                return false;

            return true;
        }
    }
}

