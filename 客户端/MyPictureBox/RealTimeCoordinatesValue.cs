namespace MyPictureBox
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct RealTimeCoordinatesValue
    {
        public int X;
        public int Y;
        public float Value;
        public DateTime time;
    }
}

