namespace MAX_CMSS_V2
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct LineCoordinatesValue
    {
        public int X;
        public int Y;
        public float Value;
        public string status;
        public DateTime time;
    }
}

