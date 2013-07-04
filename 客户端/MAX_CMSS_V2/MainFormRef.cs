namespace MAX_CMSS_V2
{
    using System;

    internal class MainFormRef
    {
        public static bool isOrdinaryVersion = false;
        public static MainForm mainForm;

        public static void updateMainForm()
        {
            for (int i = 0; i < mainForm.yeKuangs.Count; i++)
            {
                mainForm.yeKuangs[i].updateAllLieBiaoKuang();
            }
        }
    }
}

