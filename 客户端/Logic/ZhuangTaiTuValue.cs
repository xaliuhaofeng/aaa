namespace Logic
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ZhuangTaiTuValue
    {
        public int value;
        public DateTime date;
        public int baojing;
        public int duandian;
        public int kuidian;
        public string cuoshi;
        public string status;
    }
}

