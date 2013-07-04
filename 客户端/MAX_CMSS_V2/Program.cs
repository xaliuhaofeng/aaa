namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Windows.Forms;

    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Exception ex;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                OperateDBAccess.Connect();
            }
            catch (Exception exception1)
            {
                ex = exception1;
                MessageBox.Show("缺少MAXSafe.mdb文件");
                Application.Exit();
                return;
            }
            try
            {
                OperateDB.Connect();
            }
            catch (Exception exception2)
            {
                ex = exception2;
                MessageBox.Show("与SQLSERVER数据库连接失败");
                Application.Exit();
                return;
            }
            MainForm form = new MainForm();
            Application.Run(form);
        }
    }
}

