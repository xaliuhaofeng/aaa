namespace Logic
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DualAlarmInfo
    {
        private string cedianbianhao;
        private int state;
        private int fenzhan;
        private int tongdao;
        public string Cedianbianhao
        {
            get
            {
                return this.cedianbianhao;
            }
            set
            {
                this.cedianbianhao = value;
            }
        }
        public int State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }
        public int Fenzhan
        {
            get
            {
                return this.fenzhan;
            }
            set
            {
                this.fenzhan = value;
            }
        }
        public int Tongdao
        {
            get
            {
                return this.tongdao;
            }
            set
            {
                this.tongdao = value;
            }
        }
    }
}

