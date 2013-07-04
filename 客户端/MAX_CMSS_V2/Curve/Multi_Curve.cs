using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logic;
using ChartDirector;
using System.Collections;

namespace MAX_CMSS_V2.Curve
{
    public partial class Multi_Curve : UserControl
    {
        DateTime startTime;
        DateTime endTime;
        string ceDianBianHao = "";
        int ceDianId = 0;

        CurveInfo curve1 = new CurveInfo();
        CurveInfo curve2 = new CurveInfo();
        CurveInfo curve3 = new CurveInfo();
        CurveInfo curve4 = new CurveInfo();
        public Multi_Curve()
        {
            InitializeComponent();
            this.dateTimeChooser1.StartTime = DateTime.Now.Date;
            this.dateTimeChooser1.EndTime = DateTime.Now;
        }


        private void button2_Click(object sender, EventArgs e)
        {            
            startTime = this.dateTimeChooser1.StartTime;
            endTime = this.dateTimeChooser1.EndTime;
            curve1.CurveData = new double[0];
            curve2.CurveData = new double[0];
            curve3.CurveData = new double[0];
            curve4.CurveData = new double[0];
            if (startTime > endTime)
            {
                MessageBox.Show("起始时间应小于结束时间");
                return;
            }
            if (startTime.Year != endTime.Year || startTime.Month != endTime.Month)
            {
                MessageBox.Show("不支持跨月份的查询");
                return;
            }
            DataTable CurDataDt = null;
            if (this.comboBox1.SelectedIndex == 0)
            {
                curve1.IsShow = false;
            }
            else
            {
                ceDianBianHao = this.comboBox1.SelectedItem.ToString().Substring(0, 5);
                ceDianId = CurveData.GetLastCeDianId(ceDianBianHao);
                curve1.IsShow = true;
               CurDataDt= CurveData.GetAllVlaueTable(startTime, endTime, ceDianBianHao, ceDianId);
                CurveData.GetHisCurveData(CurDataDt, startTime, endTime, ref curve1);
                curve1.CurveColor = Color.Green;
                curve1.CurveTitle = comboBox1.Text.Remove(comboBox1.Text.LastIndexOf('-'));
            }

            if (this.comboBox2.SelectedIndex == 0)
            {
                curve2.IsShow = false;
            }
            else
            {
                ceDianBianHao = this.comboBox2.SelectedItem.ToString().Substring(0, 5);
                ceDianId = CurveData.GetLastCeDianId(ceDianBianHao);
                curve2.IsShow = true;
                CurDataDt = CurveData.GetAllVlaueTable(startTime, endTime, ceDianBianHao, ceDianId);
                CurveData.GetHisCurveData(CurDataDt, startTime, endTime, ref curve2);
                curve2.CurveColor = Color.Black;
                curve2.CurveTitle = comboBox2.Text.Remove(comboBox2.Text.LastIndexOf('-'));
            }

            if (this.comboBox3.SelectedIndex == 0)
            {
                curve3.IsShow = false;
            }
            else
            {
                ceDianBianHao = this.comboBox3.SelectedItem.ToString().Substring(0, 5);
                ceDianId = CurveData.GetLastCeDianId(ceDianBianHao);
                curve3.IsShow = true;
                CurDataDt = CurveData.GetAllVlaueTable(startTime, endTime, ceDianBianHao, ceDianId);
                CurveData.GetHisCurveData(CurDataDt, startTime, endTime, ref curve3);
                curve3.CurveColor = Color.Blue;
                curve3.CurveTitle = comboBox3.Text.Remove(comboBox3.Text.LastIndexOf('-'));
            }

            if (this.comboBox4.SelectedIndex == 0)
            {
                curve4.IsShow = false;
            }
            else
            {
                ceDianBianHao = this.comboBox4.SelectedItem.ToString().Substring(0, 5);
                ceDianId = CurveData.GetLastCeDianId(ceDianBianHao);
                curve4.IsShow = true;
                CurDataDt = CurveData.GetAllVlaueTable(startTime, endTime, ceDianBianHao, ceDianId);
                CurveData.GetHisCurveData(CurDataDt, startTime, endTime, ref curve4);
                curve4.CurveColor = Color.DarkOrange;
                curve4.CurveTitle = comboBox4.Text.Remove(comboBox4.Text.LastIndexOf('-'));
            }
            this.uC_Curve1.ListCurve.Clear();
            this.uC_Curve1.ListCurve.Add(curve1);
            this.uC_Curve1.ListCurve.Add(curve2);
            this.uC_Curve1.ListCurve.Add(curve3);
            this.uC_Curve1.ListCurve.Add(curve4);

            if (curve1.TimeStamps != null && curve1.IsShow)
            {
                this.uC_Curve1.timeStamps = curve1.TimeStamps;
            }
            else if (curve2.TimeStamps != null && curve2.IsShow)
            {
                this.uC_Curve1.timeStamps = curve2.TimeStamps;
            }
            else if (curve3.TimeStamps != null && curve3.IsShow)
            {
                this.uC_Curve1.timeStamps = curve3.TimeStamps;
            }
            else if (curve4.TimeStamps != null && curve4.IsShow)
            {
                this.uC_Curve1.timeStamps = curve4.TimeStamps;
            }
            else
            {
                this.uC_Curve1.timeStamps = new DateTime[] { startTime, endTime };            
            }
            uC_Curve1.YTitle = "测点值";
            this.uC_Curve1.UpdateDisplay();
        }



        private void Multi_Curve_Load(object sender, EventArgs e)
        {
            this.uC_Curve1.Title = "多曲线比较";
            this.dateTimeChooser1.EndTime = System.DateTime.Now;
            string[] ceDianList=GlobalParams.AllCeDianList.getCeDianAllInfo(0);
            ArrayList ary0=new ArrayList(ceDianList);
            ary0.Insert(0,"不显示");
             ArrayList ary1=new ArrayList(ceDianList);
            ary1.Insert(0,"不显示");
             ArrayList ary2=new ArrayList(ceDianList);
            ary2.Insert(0,"不显示");
             ArrayList ary3=new ArrayList(ceDianList);
            ary3.Insert(0,"不显示");

            this.comboBox1.DataSource = ary0;
            this.comboBox2.DataSource = ary1;
            this.comboBox3.DataSource = ary2;
            this.comboBox4.DataSource = ary3;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            uC_Curve1.SetMouseUsage = WinChartMouseUsage.ScrollOnDrag;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            uC_Curve1.SetMouseUsage = WinChartMouseUsage.ZoomIn;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            uC_Curve1.SetMouseUsage = WinChartMouseUsage.ZoomOut;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IntPtr ucIntPtr = uC_Curve1.Handle;
                Bitmap img = ImageCapture.GetWindow(ucIntPtr);
                PrintCurve print = new PrintCurve();
                print.Print_Curve(img);
            }
            catch
            {
                MessageBox.Show("打印失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
