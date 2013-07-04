namespace MAX_CMSS_V2
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct CoordinatesValue
    {
        public int X;
        public int Y;
        public float Value;
        public DateTime time;
    }
}

