namespace MyPictureBox
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct ZhuangTaiValue
    {
        public int X;
        public int Y;
        public int Value;
        public DateTime time;
    }
}

