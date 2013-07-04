namespace Logic
{
    using Microsoft.Reporting.WinForms;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Data;

    public class GlobalParams
    {

        public static bool Initing = false;
        public static bool alarm = false;
        public static Area allarea;
        public static CeDianList AllCeDianList;
        public static FenZhan[] AllfenZhans;
        public static KeyWordList AllKeyWord;
        public static KaiGuanLiangList AllkgLeiXing;
        public static KongZhiLiangList AllkzlLeiXing;
        public static MoNiLiangLeiXingList AllmnlLeiXing;
        public static XianXingZhiList AllXianXingZhi;
        public static CedianAlarm cedian_alarm;

        public static DateTime DataState;

        //手动操作列表
        public static List<int> Manula_Ctl_List=new List<int>();

        //  0-----通讯中断   1-----正常    -1------初始化
        
        private static int comm_state;   


        public static ClientConfig config = ClientConfig.CreateCommon();
        public static ArrayList cutList = new ArrayList();
        public static Dictionary<string, List<int>> dualAlarmInfo1 = new Dictionary<string, List<int>>();
        public static Dictionary<int, List<DualAlarmInfo>> dualAlarmInfo2 = new Dictionary<int, List<DualAlarmInfo>>();
        public static bool[] First_Show = new bool[60];
        public static FenZhanRTdata fzRtDt = new FenZhanRTdata();
        public static bool isBeep = false;
        public static bool[] IsModfyControl = new bool[60];
        public static lastkjna[] lastconzhi = new lastkjna[60];
        public static string lastCutFenZhan = "";
        public static string lastCutTongDao = "";
        public static int lastCutType = -1;
        public static int listNumPerPage = 3;
        public static int pageFrameNum = 1;
        private static bool recvRecord = false;
        public static ArrayList replayList = new ArrayList();
        public static bool sgAlarm = false;
        public static bool sgAlarmState = false;
        public static CeDian show_cedian = new CeDian();
        public static string[] state = new string[] { "正常", "报警", "断电", "复电", "断线", "溢出", "负漂", "故障", "I/O错误", "调校" };
        public static ArrayList warnList = new ArrayList();
        public static bool yuyinAlalrm = false;

        public static void PrintDataGridView(DataGridView view, string headName)
        {
            if (view.Rows.Count != 0)
            {
                new DGVPrinter { Title = headName, PageNumbers = true, ShowTotalPageNumber = true, PageNumberInHeader = false, PorportionalColumns = true, HeaderCellAlignment = StringAlignment.Near, FooterSpacing = 15f, PageSeparator = " / ", PageText = "页" }.PrintPreviewDataGridView(view);
            }
        }

        public static void PrintReport(ReportViewer reportViewer1)
        {
            Warning[] Warnings;
            string[] strStreamIds;
            string strMimeType;
            string strEncoding;
            string strFileNameExtension;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            byte[] bytes = reportViewer1.LocalReport.Render("Pdf", null, out strMimeType, out strEncoding, out strFileNameExtension, out strStreamIds, out Warnings);
            string strFilePath = string.Empty;
            saveFileDialog1.Filter = "PDF文件|*.pdf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                strFilePath = saveFileDialog1.FileName;
                try
                {
                    using (FileStream fs = new FileStream(strFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("文件被占用！");
                }
            }
        }

        public static  void setDataState()
        {
            DateTime dt = DateTime.Now;
            DataState = dt;
            setDataState(dt);

        }
        /// <summary>
        /// 更新参数时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        /// 
        public static bool setDataState(DateTime dt){
                        
            string sql="update DataParaTime set lastTime='" +dt.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            try
            {
                OperateDB.Execute(sql);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        ///  增加数据状态，标志位最后修改时间
        /// </summary>
        /// <returns></returns>

        public static bool CreateDataStateTable()
        {
            string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); ;
            string tableName = "DataParaTime";
            if (!OperateDB.IsTableExist("max_cmss", tableName))
            {
                try
                {
                    string sql = "create table " + tableName + " (lastTime datetime)";
                    OperateDB.Execute(sql);

                    sql = "insert into DataParaTime(lastTime) values('" + str + "')";
                    OperateDB.Execute(sql);
                }
                catch (Exception ee)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 获取配置参数是否更改
        /// </summary>
        /// <returns></returns>
        public static DateTime getDataParaTime()
        {
            string sql = "select * from DataParaTime ";
            DataTable dt = OperateDB.Select(sql);
            DateTime time = DateTime.MinValue;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    time = DateTime.Parse(dt.Rows[0]["lastTime"].ToString());
                }
                catch (Exception ee)
                {
                }
            }
            return time;
        }

     

        public static string stateTranslate(int state)
        {
            string s = string.Empty;
            switch (state)
            {
                case 0:
                    return "正常";

                case 1:
                    return "报警";

                case 2:
                    return "断电";

                case 3:
                    return "复电";

                case 4:
                    return "断线";

                case 5:
                    return "溢出";

                case 6:
                    return "负漂";

                case 7:
                    return "故障";

                case 8:
                    return "IO错误";
            }
            return s;
        }

        public static string TimeSpanString(TimeSpan span)
        {
            return string.Concat(new object[] { span.Hours, ":", span.Minutes, ":", span.Seconds });
        }

        public static int Comm_state
        {
            get
            {
                return comm_state;
            }
            set
            {
                comm_state = value;
            }
        }

        public int ListNumPerPage
        {
            get
            {
                return listNumPerPage;
            }
            set
            {
                listNumPerPage = value;
            }
        }

        public int PageFrameNum
        {
            get
            {
                return pageFrameNum;
            }
            set
            {
                pageFrameNum = value;
            }
        }

        public static bool RecvRecord
        {
            get
            {
                return recvRecord;
            }
            set
            {
                recvRecord = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct lastkjna
        {
            public byte number;
            public bool isuse;
            public byte one;
        }
    }
}

