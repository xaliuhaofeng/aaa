﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logic;
using ChartDirector;
using System.Reflection;

namespace MAX_CMSS_V2.Curve
{
    public partial class Call_curve : UserControl
    {
       int ceDianId = 0;
        string ceDianBianHao = "";
        ClientConfig cConf;
        DateTime startTime;
        DateTime endTime;
        private string ceDianWeiZhi;
        private string mingCheng;
        private string danwei;
        private float baoJingZhi;
        private float duanDianZhi;
        private float fuDianZhi;
        private string duanDianFanWei;
        private string duanDianQiZhiShiKe;
        private int duanDianLeiJiShiJian;
        private string baoJingQIZhiShike;
        private int baoJingLeiJiShiJian;
        private string kuiDianQiZhiShiKe;
        private int kuiDianLeiJiShiJian;
        /// <summary>
        /// 报警值
        /// </summary>
        private CurveInfo DataCurve = new CurveInfo();
        private CurveInfo ZuiDaZhiCurve = new CurveInfo();
        private CurveInfo ZuiXiaoZhiCurve = new CurveInfo();
        private CurveInfo PingJunZhiCurve = new CurveInfo();
        private DataTable Measuredt = new DataTable();
        private bool IsExistMeasure = false;
        private bool IsGetMeasure = false;
        public Call_curve()
        {
            InitializeComponent();

            Type type = dataGridView1.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dataGridView1, true, null);
            this.uC_Curve1.UpdateDataEvent += new UC_Curve.UpdateData(uC_Curve1_UpdateDataEvent);
        }

        void uC_Curve1_UpdateDataEvent(int xIndex)
        {
            FillDataGrid(xIndex);
        }
        public void FillDataGrid(int index)
        {
            if (index == -1) return;
            IsGetMeasure = false;
            this.dataGridView1.Rows[0].Cells[0].Value = this.ceDianWeiZhi + "/"+this.mingCheng;
            this.dataGridView1.Rows[0].Cells[1].Value = this.baoJingZhi;
            this.dataGridView1.Rows[0].Cells[2].Value = this.duanDianZhi;
            this.dataGridView1.Rows[0].Cells[3].Value = this.fuDianZhi;
            this.dataGridView1.Rows[0].Cells[4].Value = this.duanDianFanWei;
            this.dataGridView1.Rows[0].Cells[5].Value = this.uC_Curve1.timeStamps[index].ToString("HH时mm分ss秒");

            this.dataGridView1.Rows[0].Cells[6].Value = "";
            this.dataGridView1.Rows[0].Cells[7].Value = "";
            this.dataGridView1.Rows[0].Cells[8].Value = "";
            this.dataGridView1.Rows[0].Cells[9].Value = "";
            this.dataGridView1.Rows[0].Cells[10].Value = "解除";
            this.dataGridView1.Rows[0].Cells[11].Value = "复电";
            this.dataGridView1.Rows[0].Cells[12].Value = "正常";
            this.dataGridView1.Rows[0].Cells[13].Value = "";

            if (DataCurve.TimeStamps != null)
            {
                if (DataCurve.TimeStamps.Length > 0)
                {
                    if (DataCurve.CurveData[index] != Chart.NoValue)
                    {
                        this.dataGridView1.Rows[0].Cells[6].Value = DataCurve.CurveData[index].ToString();
                        this.dataGridView1.Rows[0].Cells[7].Value = ZuiDaZhiCurve.CurveData[index].ToString();
                        this.dataGridView1.Rows[0].Cells[8].Value = PingJunZhiCurve.CurveData[index].ToString();
                        this.dataGridView1.Rows[0].Cells[9].Value = ZuiXiaoZhiCurve.CurveData[index].ToString();
                        this.dataGridView1.Rows[0].Cells[10].Value = "解除";
                        this.dataGridView1.Rows[0].Cells[11].Value = "复电";
                        this.dataGridView1.Rows[0].Cells[12].Value = "正常";
                        double ceZhi = DataCurve.CurveData[index];
                        if (ceZhi >= this.duanDianZhi)
                        {
                            this.dataGridView1.Rows[0].Cells[11].Value = "断电";
                            IsGetMeasure = true;
                        }
                        else if (ceZhi >= this.baoJingZhi)
                        {
                            this.dataGridView1.Rows[0].Cells[10].Value = "报警";
                            IsGetMeasure = true;
                        }
                        if (DataCurve.KuiDianYiChang[index] == 1)
                        {
                            this.dataGridView1.Rows[0].Cells[12].Value = "异常";
                            IsGetMeasure = true;
                        }
                        if (IsGetMeasure && IsExistMeasure)
                        {
                           DataRow[] drs=Measuredt.Select(" CuoShiShiJian>='" + DataCurve.TimeStamps[index].AddMinutes(-30) + "' and  CuoShiShiJian<='" + DataCurve.TimeStamps[index].AddMinutes(30) + "'");
                           foreach (DataRow dr in drs)
                           {
                               dataGridView1.Rows[0].Cells[13].Value = dataGridView1.Rows[0].Cells[13].Value + string.Format("{0} {1}   ", dr["CuoShiShiJian"].ToString(), dr["CuoShi"].ToString());
                           }
                        }
                    }
                }
            }           
            this.dataGridView1.Refresh();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            OnClick();
        }


        private void OnClick()
        {

            if (this.comboBox1.SelectedIndex == -1)
                return;
            if (ceDianBianHao != this.comboBox1.SelectedItem.ToString().Substring(0, 5))
            {
                ceDianBianHao = this.comboBox1.SelectedItem.ToString().Substring(0, 5);
                ceDianId = CurveData.GetLastCeDianId(ceDianBianHao);
            }
            startTime = this.dateTimeChooser1.StartTime;
            endTime = this.dateTimeChooser1.EndTime;
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
            IsExistMeasure = false;
            Measuredt = CurveData.GetMeasure(startTime, endTime, ceDianId);
            if (Measuredt != null)
            {
                if (Measuredt.Rows.Count > 0)
                {
                    IsExistMeasure = true;
                }
            }
            this.duanDianQiZhiShiKe = this.baoJingQIZhiShike =
            this.kuiDianQiZhiShiKe = "";
            this.duanDianLeiJiShiJian = this.baoJingLeiJiShiJian = this.kuiDianLeiJiShiJian = 0;
            DataTable dt1 = CeDian.GetInfoByCeDianBianHao(ceDianBianHao);
            this.ceDianWeiZhi = dt1.Rows[0]["ceDianWeiZhi"].ToString();
            this.mingCheng = dt1.Rows[0]["xiaoLeiXing"].ToString();
            this.baoJingZhi = Convert.ToSingle(dt1.Rows[0]["baoJingZhiShangXian"]);
            this.duanDianZhi = Convert.ToSingle(dt1.Rows[0]["duanDianZhi"]);
            this.fuDianZhi = Convert.ToSingle(dt1.Rows[0]["fuDianZhi"]);
            this.danwei = dt1.Rows[0]["danwei"].ToString();
            this.duanDianFanWei = GlobalParams.AllCeDianList.GetDuanDianQuYu(ceDianBianHao);
            float[] info = CeDian.GetMoNiLiangInfoByCeDiaanBianHao(ceDianBianHao);
            this.uC_Curve1.BaoJingMenXian.CurveData = new double[] { info[2], info[2] };
            this.uC_Curve1.BaoJingMenXian.CurveTitle = "报警门限";
            this.uC_Curve1.DuanDianMenXian.CurveData = new double[] { info[3], info[3] };
            this.uC_Curve1.DuanDianMenXian.CurveTitle = "断电门限";
            this.uC_Curve1.FuDianMenXian.CurveData = new double[] { info[4], info[4] };
            this.uC_Curve1.FuDianMenXian.CurveTitle = "复电门限";
            this.uC_Curve1.ListCurve.Clear();
            DataTable CurDataDt = CurveData.GetAllVlaueTable(startTime, endTime, ceDianBianHao, ceDianId);
            if (CurDataDt != null)
            {
                CurveData.GetHisCurveData(CurDataDt, startTime, endTime, ref DataCurve, ref ZuiDaZhiCurve, ref ZuiXiaoZhiCurve, ref PingJunZhiCurve);
                this.uC_Curve1.timeStamps = DataCurve.TimeStamps;
                this.uC_Curve1.ListCurve.Add(ZuiDaZhiCurve);
                this.uC_Curve1.ListCurve.Add(ZuiXiaoZhiCurve);
                this.uC_Curve1.ListCurve.Add(PingJunZhiCurve);
                this.uC_Curve1.ListCurve.Add(DataCurve);
            }
            else
            {
                this.uC_Curve1.timeStamps = new DateTime[] { startTime, endTime };
            }
            this.uC_Curve1.YTitle = comboBox1.Text + "(" + danwei + ")";
            this.uC_Curve1.UpdateDisplay();
            FillDataGrid(0); 
        }
        private void button2_Click(object sender, EventArgs e)
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

       

        private void label1_Click(object sender, EventArgs e)
        {
            uC_Curve1.SetMouseUsage = WinChartMouseUsage.ScrollOnDrag;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            uC_Curve1.SetMouseUsage = WinChartMouseUsage.ZoomIn;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            uC_Curve1.SetMouseUsage = WinChartMouseUsage.ZoomOut;
        }

        private void Call_curve_Load(object sender, EventArgs e)
        {
            this.uC_Curve1.Title = "调用历史数据曲线";
            this.dateTimeChooser1.EndTime = System.DateTime.Now;
            this.comboBox1.DataSource = GlobalParams.AllCeDianList.getCeDianAllInfo(0);
            cConf = ClientConfig.CreateCommon();

            this.uC_Curve1.BaoJingMenXian.IsShow = Convert.ToBoolean(cConf.get("diaoYongShowBaoJingMenXian"));
            this.uC_Curve1.BaoJingMenXian.CurveColor = ColorTranslator.FromHtml(cConf.get("baoJingZhiColor"));
            this.checkBox1.Checked = this.uC_Curve1.BaoJingMenXian.IsShow;
            

            this.uC_Curve1.DuanDianMenXian.IsShow = Convert.ToBoolean(cConf.get("diaoYongShowDuanDianMenXian"));
            this.uC_Curve1.DuanDianMenXian.CurveColor = ColorTranslator.FromHtml(cConf.get("duanDianZhiColor"));
            this.checkBox2.Checked = this.uC_Curve1.DuanDianMenXian.IsShow;



            this.uC_Curve1.FuDianMenXian.IsShow = Convert.ToBoolean(cConf.get("diaoYongShowFuDianMenXian"));
            this.uC_Curve1.FuDianMenXian.CurveColor = ColorTranslator.FromHtml(cConf.get("fuDianZhiColor"));
            this.checkBox3.Checked = this.uC_Curve1.FuDianMenXian.IsShow;

            DataCurve.IsShow = Convert.ToBoolean(cConf.get("diaoYongShowBaoJingCurve"));
            DataCurve.CurveColor = ColorTranslator.FromHtml(cConf.get("moNiLiangColor"));
            DataCurve.CurveTitle = "调用曲线";
            this.checkBox4.Checked = DataCurve.IsShow;

            ZuiDaZhiCurve.IsShow = Convert.ToBoolean(cConf.get("diaoYongShowZuiDaZhiCurve"));
            ZuiDaZhiCurve.CurveColor = ColorTranslator.FromHtml(cConf.get("zuiDaZhiColor"));
            ZuiDaZhiCurve.CurveTitle = "最大值曲线";
            this.checkBox5.Checked = ZuiDaZhiCurve.IsShow;

            ZuiXiaoZhiCurve.IsShow = Convert.ToBoolean(cConf.get("diaoYongShowZuiXiaoZhiCurve"));
            ZuiXiaoZhiCurve.CurveColor = ColorTranslator.FromHtml(cConf.get("zuiXiaoZhiColor"));
            ZuiXiaoZhiCurve.CurveTitle = "最小值曲线";
            this.checkBox6.Checked = ZuiXiaoZhiCurve.IsShow;

            PingJunZhiCurve.IsShow = Convert.ToBoolean(cConf.get("diaoYongShowPingJunZhiCurve"));
            PingJunZhiCurve.CurveColor = ColorTranslator.FromHtml(cConf.get("pingJunZhiColor"));
            uC_Curve1.BackColor = ColorTranslator.FromHtml(cConf.get("quXianBeiJingColor"));
            PingJunZhiCurve.CurveTitle = "平均值曲线";
            this.checkBox7.Checked = PingJunZhiCurve.IsShow;
            this.dateTimeChooser1.StartTime = System.DateTime.Now.Date;
            this.dateTimeChooser1.EndTime = System.DateTime.Now;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            uC_Curve1.BaoJingMenXian.IsShow = this.checkBox1.Checked;
            cConf.set("diaoYongShowBaoJingMenXian", this.checkBox1.Checked.ToString());
            if(this.uC_Curve1.timeStamps!=null)
                this.uC_Curve1.UpdateDisplay();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            uC_Curve1.DuanDianMenXian.IsShow = this.checkBox2.Checked;
            cConf.set("diaoYongShowDuanDianMenXian", this.checkBox2.Checked.ToString());
            if (this.uC_Curve1.timeStamps != null)
                this.uC_Curve1.UpdateDisplay();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            uC_Curve1.FuDianMenXian.IsShow = this.checkBox3.Checked;
            cConf.set("diaoYongShowFuDianMenXian", this.checkBox3.Checked.ToString());
            if (this.uC_Curve1.timeStamps != null)
                this.uC_Curve1.UpdateDisplay();
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            DataCurve.IsShow = this.checkBox4.Checked;
            cConf.set("diaoYongShowBaoJingCurve", this.checkBox4.Checked.ToString());
            if (this.uC_Curve1.timeStamps != null)
                this.uC_Curve1.UpdateDisplay();
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)//是否显示最大值
        {
            ZuiDaZhiCurve.IsShow = this.checkBox5.Checked;
            cConf.set("diaoYongShowZuiDaZhiCurve", this.checkBox5.Checked.ToString());
            if (this.uC_Curve1.timeStamps != null)
                this.uC_Curve1.UpdateDisplay();
        }     

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            ZuiXiaoZhiCurve.IsShow = this.checkBox6.Checked;
            cConf.set("diaoYongShowZuiXiaoZhiCurve", this.checkBox6.Checked.ToString());
            if (this.uC_Curve1.timeStamps != null)
                this.uC_Curve1.UpdateDisplay();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            PingJunZhiCurve.IsShow = this.checkBox7.Checked;
            cConf.set("diaoYongShowPingJunZhiCurve", this.checkBox7.Checked.ToString());
            if (this.uC_Curve1.timeStamps != null)
                this.uC_Curve1.UpdateDisplay();
        }

        internal void SetCurve(string ceDianBianHao)
        {
            int index = -1;
            for (int i = 0; i < this.comboBox1.Items.Count; i++)
            {
                if (this.comboBox1.Items[i].ToString().Substring(0, 5) == ceDianBianHao)
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                this.comboBox1.SelectedIndex = index;
                OnClick();
            }
        }

    }
}
