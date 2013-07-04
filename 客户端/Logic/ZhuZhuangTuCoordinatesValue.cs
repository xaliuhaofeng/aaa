namespace Logic
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ZhuZhuangTuCoordinatesValue
    {
        public int X;
        public int Y;
        public float Value;
        public int kaiTingCiShu;
        public TimeSpan kaiTingShiJian;
    }
}

