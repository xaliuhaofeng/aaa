namespace MAX_CMSS_V2
{
    using Logic;
    using MAX_CMSS_V2.voice;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public class voiceAlarmPro
    {
        public static int alarm_times = 3;

        public static List<string> index = new List<string>();
        public static List<voiceAlarm> voiceAlarmList = new List<voiceAlarm>();

        private Queue<string> voiceQue = new Queue<string>();

        public void add(voiceAlarm va)
        {
            string cdbh = va.Cdbh;
            if (index.IndexOf(cdbh) < 0)
            {
                index.Add(cdbh);
                voiceAlarmList.Add(va);
            }
        }

        public Queue<string> getAlarmQue()
        {
            DateTime dt = DateTime.Now;
            try
            {
                foreach (string bh in (string[])index.ToArray().Clone())
                {
                    int pos = index.IndexOf(bh);
                    if (pos >= 0)
                    {
                        voiceAlarm va = voiceAlarmList.ElementAt<voiceAlarm>(pos);
                        if (va.Times == 0)
                        {
                            if (dt > va.StartTime.AddMinutes(10.0))
                            {
                                va.Times = alarm_times;
                                va.StartTime = dt;
                                this.voiceQue.Enqueue(va.Alarmstr);
                                va.Times--;
                            }
                        }
                        else
                        {
                            this.voiceQue.Enqueue(va.Alarmstr);
                            va.Times--;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                
            }
            return this.voiceQue;
        }

        public Queue<string> getAlarmQueDisp()
        {
            this.voiceQue.Clear();
            ArrayList newIndex = new ArrayList();
            try
            {

                foreach (KeyValuePair<string, string[]> item in GlobalParams.cedian_alarm.alarm_all_Dict)
                {
                    string key = item.Key;
                    string[] str = item.Value;
                    CeDian cedian = GlobalParams.AllCeDianList.getCedianInfo(key);
                    voiceAlarm va = new voiceAlarm(key)
                    {
                        Times = alarm_times,
                        Type = int.Parse(str[4]),
                        StartTime = DateTime.Parse(str[3])
                    };
                    string alarmstr = this.getDlx(cedian.DaLeiXing) + ToHanzi(key) + cedian.XiaoleiXing + this.getbj(va.Type);
                    va.Alarmstr = (string)alarmstr.Clone();
                    this.add(va);
                    newIndex.Add(key);
                }
                string[] aa = (string[])index.ToArray().Clone();
                for (int i = 0; i < aa.Length; i++)
                {
                    if (newIndex.IndexOf(aa[i].ToString()) < 0)
                    {
                        CeDian cedian = GlobalParams.AllCeDianList.getCedianInfo(aa[i]);
                        if (cedian != null)
                        {
                            string str = this.getDlx(cedian.DaLeiXing) + aa[i] + "恢复正常";
                            voiceAlarmClass.SaySome(str.Trim());
                            Thread.Sleep(2000);
                        }
                        this.remove(aa[i]);                       
                    }
                }
            }
            catch (Exception ee)
            {
            }
            return this.voiceQue;
        }

        public string getbj(int d)
        {
            switch (d)
            {
                case 1:
                    return "超限报警";

                case 2:
                    return "断电报警";

                case 3:
                    return "馈电报警";

                case 4:
                    return "报警";

                case 5:
                    return "断电报警";

                case 6:
                    return "馈电报警";

                case 7:
                    return "控制报警";
                case 8:
                    return "直流供电报警";
            }
            return "报警";
        }

        public string getDlx(int d)
        {
            string str = "开关量，";
            if (d == 0)
                str = "模拟量";
            else if (d == 2)
                str = "控制量";
            return str;
        }

        public void remove(string bh)
        {
            int pos = index.IndexOf(bh);
            if (pos >= 0)
            {
                index.RemoveAt(pos);
                voiceAlarmList.RemoveAt(pos);
            }
        }

        public void voiceThread()
        {
            while (true)
            {
                Thread.Sleep(2000);
                if (GlobalParams.yuyinAlalrm)
                {
                    try
                    {
                        this.getAlarmQueDisp();
                        this.voiceQue = this.getAlarmQue();
                        int size = this.voiceQue.Count;
                        if (size > 0)
                        {
                            for (int i = 0; i < size; i++)
                            {
                                string str = this.voiceQue.Dequeue().ToString();
                                if (GlobalParams.yuyinAlalrm)
                                {
                                    voiceAlarmClass.SaySome(str.Trim());
                                }
                            }
                        }
                    }
                    catch(Exception ee)
                    {
                    }
                }
            }
        }

        public static  string ToHanzi(string key)
        {
            string ret="";

            for (int i = 0; i < key.Length; i++) 
            {

                string aa=key.Substring(i,1);
                switch (aa)
                {
                    case "0":
                        ret += "零";
                        break;
                    case "1":
                        ret += "一";
                        break;
                    case "2":
                        ret += "二";
                        break;
                    case "3":
                        ret += "三";
                        break;
                    case "4":
                        ret += "四";
                        break;
                    case "5":
                        ret += "五";
                        break;
                    case "6":
                        ret += "六";
                        break;
                    case "7":
                        ret += "七";
                        break;
                    case "8":
                        ret += "八";
                        break;
                    case "9":
                        ret += "九";
                        break;
                    default: 
                        ret += aa; // 其他文字逐个复制过来
                        break;
                }
            }
            return ret;
        }
    }
    
}

