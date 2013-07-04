using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace 历史数据显示
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string s=String.Empty;
            s = args[0];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(s));
        }
    }
}
