namespace Logic
{
    using System;
    using System.Collections.Generic;

    public class CedianAlarm
    {
        public Dictionary<string, string[]> alarm_all_Dict;
        public Dictionary<string, string[]> alarm_ana_cutoff_Dict = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> alarm_ana_feed_Dict = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> alarm_analog_Dict = new Dictionary<string, string[]>();

        public Dictionary<string, string[]> alarm_digi_cutoff_Dict = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> alarm_digi_Dict = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> alarm_digi_feed_Dict = new Dictionary<string, string[]>();

        public Dictionary<string, string[]> alarm_ctl_Dict = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> alarm_fz_Dict = new Dictionary<string, string[]>();

        public Dictionary<string, string[]> alarm_yujing_Dict;

        public List<int> multi_alarm_list = new List<int>();
        private CeDianList AllCedian;
        private ClientConfig config;

        bool isDebug = true;

        public CedianAlarm(CeDianList list)
        {
            this.AllCedian = list;
            this.config = ClientConfig.CreateCommon();
            this.alarm_all_Dict = new Dictionary<string, string[]>();
            this.alarm_yujing_Dict = new Dictionary<string, string[]>();
        }

        private void addAnaToList(string cdbh, int state, float val, string starttime, int at)
        {
            try
            {
                string key = cdbh;
                string[] item;
                switch (at)
                {
                    case 1:
                        if (!this.alarm_analog_Dict.TryGetValue(key, out item))
                        {
                            item = new string[7];
                            item[0] = cdbh;
                            item[1] = state.ToString();
                            item[2] = val.ToString();
                            item[3] = starttime;
                            item[4] = at.ToString();
                            this.alarm_analog_Dict.Add(key, item);
                        }
                        else
                        {
                            item[1] = state.ToString();
                            item[2] = val.ToString();
                            this.alarm_analog_Dict[key] = item;
                        }
                        break;
                    case 2:
                        if (!this.alarm_ana_cutoff_Dict.TryGetValue(key, out item))
                        {
                            item = new string[] { cdbh, state.ToString(), val.ToString(), starttime, at.ToString() };
                            this.alarm_ana_cutoff_Dict.Add(key, item);
                            if (this.alarm_analog_Dict.TryGetValue(key, out item))
                                this.alarm_analog_Dict.Remove(key);
                        }
                        else
                        {
                            item[1] = state.ToString();
                            item[2] = val.ToString();
                            this.alarm_ana_cutoff_Dict[key] = item;
                            if (this.alarm_analog_Dict.TryGetValue(key, out item))                        
                                this.alarm_analog_Dict.Remove(key);
                            
                        }
                        break;
                    case 3:
                        if (!this.alarm_ana_feed_Dict.TryGetValue(key, out item))
                        {
                            item = new string[] { cdbh, state.ToString(), val.ToString(), starttime, at.ToString() };
                            this.alarm_ana_feed_Dict.Add(key, item);
                            if (this.alarm_analog_Dict.TryGetValue(key, out item))
                                this.alarm_analog_Dict.Remove(key);
                        }
                        else
                        {
                            item[1] = state.ToString();
                            item[2] = val.ToString();
                            this.alarm_ana_feed_Dict[key] = item;
                            if (this.alarm_analog_Dict.TryGetValue(key, out item))
                                this.alarm_analog_Dict.Remove(key);
                        }
                        break;
                }

                if (this.alarm_all_Dict.TryGetValue(key, out item))
                {
                    item[1] = state.ToString();
                    item[2] = val.ToString();
                    if (Convert.ToInt32(item[4]) != at)
                    {
                        item[3] = starttime;
                        item[4] = at.ToString();
                    }
                    this.alarm_all_Dict[key] = item;
                }
                else
                {
                    item = new string[] { cdbh, state.ToString(), val.ToString(), starttime, at.ToString() };
                    this.alarm_all_Dict.Add(key, item);
                }
            }
            catch(Exception ee)
            {
                Console.WriteLine("addAnaToList " +ee.ToString());
            }
        }

        private void addDigiToList(string cdbh, int state, float val, string starttime, int at)
        {
            try
            {
                string key = cdbh;
                string[] item;
                switch (at)
                {
                    case 4:

                        if (!this.alarm_digi_Dict.TryGetValue(key, out item))
                        {
                            item = new string[] { cdbh, state.ToString(), val.ToString(), starttime, at.ToString() };
                            this.alarm_digi_Dict.Add(key, item);
                        }
                        else
                        {
                            item[1] = state.ToString();
                            item[2] = val.ToString();
                            this.alarm_digi_Dict[key] = item;
                        }
                        break;

                    case 5:

                        if (!this.alarm_digi_cutoff_Dict.TryGetValue(key, out item))
                        {
                            item = new string[] { cdbh, state.ToString(), val.ToString(), starttime, at.ToString() };
                            this.alarm_digi_cutoff_Dict.Add(key, item);
                        }
                        else
                        {
                            item[1] = state.ToString();
                            item[2] = val.ToString();
                            this.alarm_digi_cutoff_Dict[key] = item;
                        }

                        break;

                    case 6:

                        if (!this.alarm_digi_feed_Dict.TryGetValue(key, out item))
                        {
                            item = new string[] { cdbh, state.ToString(), val.ToString(), starttime, at.ToString() };
                            this.alarm_digi_feed_Dict.Add(key, item);
                        }
                        else
                        {
                            item[1] = state.ToString();
                            item[2] = val.ToString();
                            this.alarm_digi_feed_Dict[key] = item;
                        }
                        break;
                }
                          
  
           
                if (this.alarm_all_Dict.TryGetValue(key, out item))
                {
                    item[1] = state.ToString();
                    item[2] = val.ToString();
                    if (Convert.ToInt32(item[4]) != at)
                    {
                        item[3] = starttime;
                        item[4] = at.ToString();
                    }
                    this.alarm_all_Dict[key] = item;
                }
                else
                {
                    item = new string[] { cdbh, state.ToString(), val.ToString(), starttime, at.ToString() };
                    this.alarm_all_Dict.Add(key, item);
                }
            }
            catch(Exception ee)
            {
                 Console.WriteLine("addDigiToList " +ee.ToString());
            }
        }

        private void addCtlToList(string cdbh, int state, float val, string starttime, int at)
        {
            try
            {
                string key = cdbh;
                string[] item;

                if (this.alarm_all_Dict.TryGetValue(key, out item))
                {
                    item[1] = state.ToString();
                    item[2] = val.ToString();
                    if (Convert.ToInt32(item[4]) != at)
                    {
                        item[3] = starttime;
                        item[4] = at.ToString();
                    }
                    this.alarm_all_Dict[key] = item;
                }
                else
                {
                    item = new string[5];
                    item[0] = cdbh;
                    item[1] = state.ToString();
                    item[2] = val.ToString();
                    item[3] = starttime;
                    item[4] = at.ToString();

                  //  item = new string[] { cdbh, state.ToString(), val.ToString(), starttime, at.ToString() };
                    this.alarm_all_Dict.Add(key, item);
                }
            }
             catch(Exception ee)
            {
                Console.WriteLine("addCtlToList " +ee.ToString());
            } 

        }

        private void clear_alarm(string cdbh, int ct)
        {
            string[] item;
            string key = cdbh;
            switch (ct)
            {
                case 0:
                    if (this.alarm_analog_Dict.TryGetValue(key, out item))
                    {
                        this.alarm_analog_Dict.Remove(key);
                    }
                    if (this.alarm_ana_cutoff_Dict.TryGetValue(key, out item))
                    {
                        this.alarm_ana_cutoff_Dict.Remove(key);
                    }
                    if (this.alarm_ana_feed_Dict.TryGetValue(key, out item))
                    {
                        this.alarm_ana_feed_Dict.Remove(key);
                    }
                    if (this.alarm_all_Dict.TryGetValue(key, out item))
                    {
                        this.alarm_all_Dict.Remove(key);
                    }
                    break;

                case 1:
                    if (this.alarm_digi_Dict.TryGetValue(key, out item))
                    {
                        this.alarm_digi_Dict.Remove(key);
                    }
                    if (this.alarm_digi_cutoff_Dict.TryGetValue(key, out item))
                    {
                        this.alarm_digi_cutoff_Dict.Remove(key);
                    }
                    if (this.alarm_digi_feed_Dict.TryGetValue(key, out item))
                    {
                        this.alarm_digi_feed_Dict.Remove(key);
                    }
                    if (this.alarm_all_Dict.TryGetValue(key, out item))
                    {
                        this.alarm_all_Dict.Remove(key);
                    }
                    break;
                case 2:
                 
                    try
                    {
                        if (this.alarm_all_Dict.TryGetValue(key, out item))
                        {
                            this.alarm_all_Dict.Remove(key);
                        }
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine(ee.ToString());
                    }
                    break;
            }
        }

        public void do_alarm_pro(CeDian cedian, float val)
        {
           
            string alarmString;
            int tp = cedian.DaLeiXing;
            bool isAlarm = false;
            string key = cedian.CeDianBianHao;

            switch (tp)
            {
                case 0:

                    if (cedian.IsAnaCutAlarm(val))
                    {
                        if (!cedian.DuanDianFlag)
                        {
                            cedian.DuanDianFlag = true;
                            cedian.FuDianFlag = false;
                            DuanDianGuanXi.SendControlInfo(cedian.CeDianBianHao, 1, this.AllCedian.allcedianlist);
                            this.AllCedian.allcedianlist[cedian.CeDianBianHao] = cedian;
                        }
                        isAlarm = true;
                        GlobalParams.alarm = true;
                        alarmString = "模拟量，" + cedian.CeDianBianHao + "," + cedian.XiaoleiXing + "断电";
                        if (!GlobalParams.cutList.Contains(alarmString))
                        {
                            GlobalParams.cutList.Add(alarmString);
                        }
                        this.addAnaToList(cedian.CeDianBianHao, cedian.RtState, val, cedian.Time.ToString(), 2);
                    }
                    if ((cedian.RtState > 3) && (cedian.RtState < 8))
                    {
                        isAlarm = true;
                        if (!cedian.DuanDianFlag)
                        {
                            cedian.DuanDianFlag = true;
                            cedian.FuDianFlag = false;
                            DuanDianGuanXi.SendControlInfo(cedian.CeDianBianHao, 1, this.AllCedian.allcedianlist);
                            this.AllCedian.allcedianlist[cedian.CeDianBianHao] = cedian;
                            GlobalParams.alarm = true;
                            alarmString = "模拟量，" + cedian.CeDianBianHao + "," + cedian.XiaoleiXing + "断电";
                            if (!GlobalParams.cutList.Contains(alarmString))
                            {
                                GlobalParams.cutList.Add(alarmString);
                            }
                            this.addAnaToList(cedian.CeDianBianHao, cedian.RtState, val, cedian.Time.ToString(), 2);
                        }
                    }
                    if (cedian.IsAnaFeedAlarm(val))
                    {
                        GlobalParams.alarm = true;
                        isAlarm = true;
                        alarmString = "模拟量，" + cedian.CeDianBianHao + "," + cedian.XiaoleiXing + "馈电异常";
                        if (!GlobalParams.replayList.Contains(alarmString))
                        {
                            GlobalParams.replayList.Add(alarmString);
                        }
                        this.addAnaToList(cedian.CeDianBianHao, cedian.RtState, val, cedian.Time.ToString(), 3);
                    }
                    if (cedian.BaoJing && (cedian.IsAnaAlarm(val) && !isAlarm))
                    {
                        GlobalParams.alarm = true;
                        isAlarm = true;
                        alarmString = "模拟量，" + cedian.CeDianBianHao + "," + cedian.XiaoleiXing + "报警";
                        if (!GlobalParams.warnList.Contains(alarmString))
                        {
                            GlobalParams.warnList.Add(alarmString);
                        }
                        this.addAnaToList(cedian.CeDianBianHao, cedian.RtState, val, cedian.Time.ToString(), 1);
                    }
                    if ((cedian.RtState == 0) && (val < cedian.MoNiLiang.FuDianZhi))
                    {
                        if (val >= cedian.MoNiLiang.BaoJingZhiShangXian)
                        {
                            isAlarm = true;
                        }
                        else
                        {
                            isAlarm = false;
                        }
                        if (!cedian.FuDianFlag)
                        {
                            cedian.FuDianFlag = true;
                            cedian.DuanDianFlag = false;
                            if (DuanDianGuanXi.SendControlInfo(cedian.CeDianBianHao, 0, this.AllCedian.allcedianlist))
                            {
                                cedian.DuanDianFlag = false;
                            }
                            this.AllCedian.allcedianlist[cedian.CeDianBianHao] = cedian;
                        }
                    }
                    if (!isAlarm)
                    {
                        this.clear_alarm(cedian.CeDianBianHao, 0);
                        alarmString = "模拟量，" + cedian.CeDianBianHao + "," + cedian.XiaoleiXing + "报警";
                        if (GlobalParams.warnList.Contains(alarmString))
                        {
                            GlobalParams.warnList.Remove(alarmString);
                        }
                        alarmString = "模拟量，" + cedian.CeDianBianHao + "," + cedian.XiaoleiXing + "馈电异常";
                        if (GlobalParams.replayList.Contains(alarmString))
                        {
                            GlobalParams.replayList.Remove(alarmString);
                        }
                        alarmString = "模拟量，" + cedian.CeDianBianHao + "," + cedian.XiaoleiXing + "断电";
                        if (GlobalParams.cutList.Contains(alarmString))
                        {
                            GlobalParams.cutList.Remove(alarmString);
                        }
                    }
                    cedian.IsAlarm = isAlarm;
                    this.AllCedian.allcedianlist[cedian.CeDianBianHao] = cedian;
                    if (val < cedian.MoNiLiang.BaoJingZhiShangXian)
                    {
                        YuJing yuJing = new YuJing(cedian.FenZhanHao, cedian.TongDaoHao);
                        if (yuJing.Exist() && cedian.isAnaYuJing(yuJing.YuJingValue, yuJing.ChangeRate, val))
                        {
                            string[] a;
                            if (this.alarm_yujing_Dict.ContainsKey(cedian.CeDianBianHao))
                            {
                                a = this.alarm_yujing_Dict[cedian.CeDianBianHao];
                                a[2] = Math.Round((double)val, 2).ToString();
                                a[3] = cedian.RtState.ToString();
                                this.alarm_yujing_Dict[cedian.CeDianBianHao] = a;
                            }
                            else
                            {
                                a = new string[] { cedian.CeDianWeiZhi + "/" + cedian.XiaoleiXing, cedian.CeDianBianHao, Math.Round((double)val, 2).ToString(), cedian.RtState.ToString() };
                                this.alarm_yujing_Dict.Add(cedian.CeDianBianHao, a);
                            }
                        }
                        else
                        {
                            if (this.alarm_yujing_Dict.ContainsKey(cedian.CeDianBianHao))
                            {
                                this.alarm_yujing_Dict.Remove(cedian.CeDianBianHao);

                            }
                        }
                    }
                    else if (this.alarm_yujing_Dict.ContainsKey(cedian.CeDianBianHao))
                    {
                        this.alarm_yujing_Dict.Remove(cedian.CeDianBianHao);
                    }
                    break;

                case 1:

                    if (cedian.TongDaoHao == 0)
                    {
                        if (cedian.RtState == 1)
                        {
                            cedian.IsAlarm = true;
                            addDigiToList(key, cedian.RtState, val, cedian.Time.ToString(), 8);

                        }
                        else
                        {
                            cedian.IsAlarm = false;
                            this.clear_alarm(cedian.CeDianBianHao, 1);
                        }

                        this.AllCedian.allcedianlist[cedian.CeDianBianHao] = cedian;
                        break;
                    }
                    string valstr = cedian.getDigiVal((int)val);
                    if (cedian.IsDigiFeed((int)val))
                    {
                        isAlarm = true;
                        GlobalParams.alarm = true;
                        alarmString = "开关量，" + cedian.CeDianBianHao + "," + cedian.XiaoleiXing + "馈电异常";
                        if (!GlobalParams.replayList.Contains(alarmString))
                        {
                            GlobalParams.replayList.Add(alarmString);
                        }
                        this.addDigiToList(key, cedian.RtState, val, cedian.Time.ToString(), 6);
                    }
                    if (cedian.IsDigiCut((int)val) && cedian.KaiGuanLiang.ShiFouDuanDian)
                    {
                        isAlarm = true;
                        cedian.DuanDianFlag = true;
                        cedian.FuDianFlag = false;
                        this.AllCedian.allcedianlist[cedian.CeDianBianHao] = cedian;
                        GlobalParams.alarm = true;
                        alarmString = "开关量，" + cedian.CeDianBianHao + "," + cedian.XiaoleiXing + "断电";
                        if (!GlobalParams.cutList.Contains(alarmString))
                        {
                            GlobalParams.cutList.Add(alarmString);
                        }
                        DuanDianGuanXi.SendControlInfo(cedian.CeDianBianHao, 1, this.AllCedian.allcedianlist);
                        this.addDigiToList(key, cedian.RtState, val, cedian.Time.ToString(), 5);
                    }
                    if ((cedian.RtState > 3) && (cedian.RtState < 8))
                    {
                        isAlarm = true;
                        if (!cedian.DuanDianFlag)
                        {
                            isAlarm = true;
                            GlobalParams.alarm = true;
                            alarmString = "开关量，" + cedian.CeDianBianHao + "," + cedian.XiaoleiXing + "断电";
                            if (!GlobalParams.cutList.Contains(alarmString))
                            {
                                GlobalParams.cutList.Add(alarmString);
                            }
                            cedian.DuanDianFlag = true;
                            cedian.FuDianFlag = false;
                            this.AllCedian.allcedianlist[cedian.CeDianBianHao] = cedian;
                            DuanDianGuanXi.SendControlInfo(cedian.CeDianBianHao, 1, this.AllCedian.allcedianlist);
                            this.addDigiToList(key, cedian.RtState, val, cedian.Time.ToString(), 5);
                        }
                    }
                    if (cedian.IsDigiAlarm((int)val) && !isAlarm)
                    {
                        isAlarm = true;
                        alarmString = "开关量，" + cedian.CeDianBianHao + "," + cedian.XiaoleiXing + "报警";
                        if (!GlobalParams.warnList.Contains(alarmString))
                        {
                            GlobalParams.warnList.Add(alarmString);
                        }
                        this.addDigiToList(key, cedian.RtState, val, cedian.Time.ToString(), 4);
                    }
                    if (!isAlarm)
                    {
                        if (cedian.KaiGuanLiang.DuanDianZhuangTai != ((int)val))
                        {
                            cedian.FuDianFlag = true;
                            if (cedian.DuanDianFlag && DuanDianGuanXi.SendControlInfo(cedian.CeDianBianHao, 0, this.AllCedian.allcedianlist))
                            {
                                cedian.DuanDianFlag = false;
                            }
                        }
                        //  if (cedian.IsAlarm != isAlarm)   2013 03 22
                        {
                            this.clear_alarm(cedian.CeDianBianHao, 1);
                            alarmString = "开关量，" + cedian.CeDianBianHao + "," + cedian.XiaoleiXing + "报警";
                            if (GlobalParams.warnList.Contains(alarmString))
                            {
                                GlobalParams.warnList.Remove(alarmString);
                            }
                            alarmString = "开关量，" + cedian.CeDianBianHao + "," + cedian.XiaoleiXing + "馈电异常";
                            if (GlobalParams.replayList.Contains(alarmString))
                            {
                                GlobalParams.replayList.Remove(alarmString);
                            }
                            alarmString = "开关量，" + cedian.CeDianBianHao + "," + cedian.XiaoleiXing + "断电";
                            if (GlobalParams.cutList.Contains(alarmString))
                            {
                                GlobalParams.cutList.Remove(alarmString);
                            }
                        }
                    }
                    cedian.IsAlarm = isAlarm;
                    this.AllCedian.allcedianlist[cedian.CeDianBianHao] = cedian;
                    break;

                case 2:

           
                    if (cedian.RtState == 1)
                    {
                        cedian.IsAlarm = true;

                        addCtlToList(key, cedian.RtState, val, cedian.Time.ToString(), 7);
                    }
                    else
                    {
                        cedian.IsAlarm = false;
                        this.clear_alarm(cedian.CeDianBianHao, 2);
                    }

                    try
                    {
                        this.AllCedian.allcedianlist[cedian.CeDianBianHao] = cedian;
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine(ee.ToString());
                    }

                    break;
            }
        }


        public  void do_mAlarm_pro(CeDian cedian)
        {
            foreach (KeyValuePair<string, List<int>> pair in GlobalParams.dualAlarmInfo1)
            {
                string cdbh = pair.Key.ToString();
                if (cedian.CeDianBianHao != cdbh)
                    continue;
                List<int> listid = (List<int>)pair.Value;
                for (int i = 0; i < listid.Count; i++)
                {
                    int id = listid[i];
                    List<DualAlarmInfo> list2;
                    bool baojing = false;
                    GlobalParams.dualAlarmInfo2.TryGetValue(id, out list2);
                    foreach (DualAlarmInfo info in list2)
                    {
                        string icdbh = info.Cedianbianhao;
                        CeDian icedian = GlobalParams.AllCeDianList.getCedianInfo(icdbh);
                        if (info.State == icedian.RtVal)
                            baojing = true;
                        else
                        {
                            baojing = false;
                            break;
                        }
                      
                    }
                    if (baojing)
                    {
                        int index = multi_alarm_list.IndexOf(id);
                        if(index<0)
                            multi_alarm_list.Add(id);
                        Console.WriteLine("multi alarm: add" + id);
                    }
                    else
                    {
                        bool b= multi_alarm_list.Remove(id);
                        Console.WriteLine("multi alarm: remove" + id);
                    }
                }

            }
            
        }

        public static string getAlalarm(int a)
        {
            switch (a)
            {
                case 1:
                    return "模拟量告警";

                case 2:
                    return "模拟量断电告警";

                case 3:
                    return "模拟量馈电异常告警";

                case 4:
                    return "开关量告警";

                case 5:
                    return "开关量断电告警";

                case 6:
                    return "开关量馈电异常告警";
                case 7:
                    return "控制报警";
                case 8:
                    return "直流供电报警";
            }
            return null;
        }

        public static int getAlalarm(string str)
        {
            if (str.Equals("模拟量告警"))
            {
                return 1;
            }
            if (str.Equals("模拟量断电告警"))
            {
                return 2;
            }
            if (str.Equals("模拟量馈电异常告警"))
            {
                return 3;
            }
            if (str.Equals("开关量告警"))
            {
                return 4;
            }
            if (str.Equals("开关量断电告警"))
            {
                return 5;
            }
            if (str.Equals("开关量馈电异常告警"))
            {
                return 6;
            }
            if (str.Equals("控制报警"))
            {
                return 7;
            }
            if (str.Equals("直流供电报警"))
            {
                return 8;
            }
             
            return 0;
        }
    }
}

