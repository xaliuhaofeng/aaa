namespace MAX_CMSS_V2
{
    using System;
    using System.Runtime.InteropServices;

    public class APIFunctions
    {
        public const int DURATION = 120;
        public const int FREQ = 0xdac;

        [DllImport("kernel32.dll")]
        public static extern int Beep(int dwFreq, int dwDuration);
        public static void CollateBeep()
        {
            Beep(0xdac, 120);
        }
    }
}

