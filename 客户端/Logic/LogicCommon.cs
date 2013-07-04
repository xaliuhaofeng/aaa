namespace Logic
{
    using System;

    public class LogicCommon
    {
        public static int DateStartPos = 0x25;
        public static byte endByte = 0x21;
        public static byte startByte = 0x7e;

        public static string[] GetAllLeiXing()
        {
            return new string[] { "瓦斯", "一氧化碳" };
        }
    }
}

