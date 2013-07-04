namespace Logic
{
    using System;

    public class voiceAlarm
    {
        private string alarmstr;
        private string cdbh;
        private DateTime startTime;
        private int times;
        private int type;

        public voiceAlarm(string bh)
        {
            this.cdbh = bh;
        }

        public string Alarmstr
        {
            get
            {
                return this.alarmstr;
            }
            set
            {
                this.alarmstr = value;
            }
        }

        public string Cdbh
        {
            get
            {
                return this.cdbh;
            }
            set
            {
                this.cdbh = value;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return this.startTime;
            }
            set
            {
                this.startTime = value;
            }
        }

        public int Times
        {
            get
            {
                return this.times;
            }
            set
            {
                this.times = value;
            }
        }

        public int Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
    }
}

