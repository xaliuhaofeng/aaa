using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 历史数据显示
{
    class GlobalParams
    {
        public static int pageFrameNum;     //页框个数
        public static int listNumPerPage;   //每个页框内列表个数

        public static string[] state = { "正常", "报警", "断电", "复电", "断线", "溢出", "负漂", "故障", "I/O错误", "调校", "其他", "其他", "其他", "其他", "馈电异常", "其他" };

        public static bool alarm = false;

        //public static ArrayList alarmList = new ArrayList();

        public GlobalParams()
        {
            pageFrameNum = 1;
            listNumPerPage = 3;
        }

        public int PageFrameNum
        {
            get { return pageFrameNum; }
            set { pageFrameNum = value; }
        }

        public int ListNumPerPage
        {
            get { return listNumPerPage; }
            set { listNumPerPage = value; }
        }

        //public static Dictionary<string, CeDian> AllCeDian = new Dictionary<string, CeDian>();
        //public static Dictionary<string, List<int>> dualAlarmInfo1 = new Dictionary<string, List<int>>();
        //public static Dictionary<int, List<DualAlarmInfo>> dualAlarmInfo2 = new Dictionary<int, List<DualAlarmInfo>>();
        //public static int lastCutType = -1;//最后执行的功能，0-复电，1-断电
        //public static string lastCutFenZhan = "";//最后自动执行的分站号
        //public static string lastCutTongDao = "";//最后自动执行的通道号

        public static string stateTranslate(int state)
        {
            string s = string.Empty;
            switch (state)
            {
                case 0:
                    s = "正常";
                    break;
                case 1:
                    s = "报警";
                    break;
                case 2:
                    s = "断电";
                    break;
                case 3:
                    s = "复电";
                    break;
                case 4:
                    s = "断线";
                    break;
                case 5:
                    s = "溢出";
                    break;
                case 6:
                    s = "负漂";
                    break;
                case 7:
                    s = "故障";
                    break;
                case 8:
                    s = "IO错误";
                    break;
            }

            return s;
        }

        //public static void PrintScreen()
        //{
        //    Bitmap bmp = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
        //    Graphics g = Graphics.FromImage(bmp);
        //    g.CopyFromScreen(0, 0, 0, 0, new Size(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height));

        //    frmScreen f = new frmScreen(bmp);
        //    f.Show();
        //}

        //public static void PrintReport(Microsoft.Reporting.WinForms.ReportViewer reportViewer1)
        //{
        //    Microsoft.Reporting.WinForms.Warning[] Warnings;
        //    string[] strStreamIds;
        //    string strMimeType;
        //    string strEncoding;
        //    string strFileNameExtension;

        //    SaveFileDialog saveFileDialog1 = new SaveFileDialog();

        //    byte[] bytes = reportViewer1.LocalReport.Render("Pdf", null, out strMimeType, out strEncoding, out strFileNameExtension, out strStreamIds, out Warnings);

        //    string strFilePath = string.Empty;
        //    saveFileDialog1.Filter = "PDF文件|*.pdf";
        //    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        strFilePath = saveFileDialog1.FileName;
        //    }
        //    else
        //        return;



        //    try
        //    {
        //        using (System.IO.FileStream fs = new FileStream(strFilePath, FileMode.OpenOrCreate, FileAccess.Write))
        //        {
        //            fs.Write(bytes, 0, bytes.Length);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("文件被占用！");
        //    }


        //}

        //public static void PrintDataGridView(DataGridView view, string headName)
        //{
        //    if (view.Rows.Count == 0)
        //        return;

        //    DGVPrinter printer = new DGVPrinter();
        //    printer.Title = headName;
        //    //printer.SubTitle = this.subTitle;
        //    //printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
        //    printer.PageNumbers = true;
        //    printer.ShowTotalPageNumber = true;
        //    printer.PageNumberInHeader = false;
        //    printer.PorportionalColumns = true;
        //    printer.HeaderCellAlignment = StringAlignment.Near;
        //    //printer.Footer = this.yeKuangMingCheng;
        //    printer.FooterSpacing = 15;
        //    printer.PageSeparator = " / ";
        //    printer.PageText = "页";

        //    printer.PrintPreviewDataGridView(view);
        //}

        //public static string TimeSpanString(TimeSpan span)
        //{
        //    string s = span.Hours + ":" + span.Minutes + ":" + span.Seconds;
        //    return s;
        //}
    }
}
