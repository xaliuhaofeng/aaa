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
using System.Reflection;

namespace MAX_CMSS_V2.Curve
{
    public partial class ZhuZhuangTu_Form : UserControl
    {
        public string CeDianBianHao = "";
        public int CeDianId = 0;
        private DataTable Measuredt = new DataTable();
        private bool IsExistMeasure = false;
        private bool IsGetMeasure = false;
        public ZhuZhuangTu_Form(string ceDianBianHao)
        {
            InitializeComponent();
            Type type = dataGridView1.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dataGridView1, true, null);
            this.uC_KglChart1.UpdateDataEvent += new UC_KglChart.UpdateData(uC_KglChart1_UpdateDataEvent);
            ClientConfig cConf = ClientConfig.CreateCommon();
            this.comboBox1.DataSource = CurveData.GetKglPnts(); 
            if (ceDianBianHao != "")
            {
                for (int i = 0; i < this.comboBox1.Items.Count; i++)
                {
                    if (this.comboBox1.Items[i].ToString().Substring(0, 5) == ceDianBianHao)
                    {
                        this.comboBox1.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        void uC_KglChart1_UpdateDataEvent(DateTime datetime)
        {
            FillDataGrid(datetime);
        }
        public void FillDataGrid(DateTime indexTime)
        {
            IsGetMeasure = false;
            this.dataGridView1.Rows[0].Cells[0].Value = this.ceDianWeiZhi + "/" + this.mingCheng;
            int Index = Convert.ToInt32((indexTime - this.dateTimePicker1.Value.Date).TotalMinutes);
            if (Index < 0) return;
            this.dataGridView1.Rows[0].Cells[1].Value = "";
            this.dataGridView1.Rows[0].Cells[2].Value = "";
            this.dataGridView1.Rows[0].Cells[3].Value = "";
            this.dataGridView1.Rows[0].Cells[4].Value = "";
            this.dataGridView1.Rows[0].Cells[5].Value = "";
            this.dataGridView1.Rows[0].Cells[6].Value = "";
            this.dataGridView1.Rows[0].Cells[7].Value =  "解除";
            this.dataGridView1.Rows[0].Cells[8].Value = "复电";
            this.dataGridView1.Rows[0].Cells[9].Value ="正常";
            this.dataGridView1.Rows[0].Cells[10].Value = "";     
            if (kglZzuInfo == null || kglZtuInfo == null) return;
            if (Index >= kglZtuInfo.status.Length) return;
            this.dataGridView1.Rows[0].Cells[1].Value = kglZtuInfo.status[Index];
            this.dataGridView1.Rows[0].Cells[2].Value = indexTime.ToString("HH:00:00")+"--"+ indexTime.AddHours(1).ToString("HH:00:00");
            this.dataGridView1.Rows[0].Cells[3].Value = indexTime.ToString("HH:mm:ss");
            this.dataGridView1.Rows[0].Cells[4].Value =Math.Round((double) kglZzuInfo.Value[indexTime.Hour],2).ToString()+"%";
            this.dataGridView1.Rows[0].Cells[5].Value =GlobalParams.TimeSpanString(kglZzuInfo.kaiTingShiJian[indexTime.Hour]);
            this.dataGridView1.Rows[0].Cells[6].Value = kglZzuInfo.kaiTingCiShu[indexTime.Hour];
            this.dataGridView1.Rows[0].Cells[7].Value = kglZtuInfo.baojing[Index] ? "报警" : "解除";
            this.dataGridView1.Rows[0].Cells[8].Value = kglZtuInfo.duandian[Index] ? "断电" : "复电";
            this.dataGridView1.Rows[0].Cells[9].Value = kglZtuInfo.kuidian[Index] ? "异常" : "正常";

            if (IsExistMeasure)
            {
                if (kglZtuInfo.baojing[Index] || kglZtuInfo.duandian[Index] || kglZtuInfo.kuidian[Index])
                {
                    IsGetMeasure = true;
                }
                if (IsGetMeasure)
                {
                    DataRow[] drs = Measuredt.Select(" CuoShiShiJian>='" + indexTime.AddMinutes(-30) + "' and  CuoShiShiJian<='" + indexTime.AddMinutes(30) + "'");
                    foreach (DataRow dr in drs)
                    {
                        dataGridView1.Rows[0].Cells[10].Value = dataGridView1.Rows[0].Cells[10].Value + string.Format("{0} {1}   ", dr["CuoShiShiJian"].ToString(), dr["CuoShi"].ToString());
                    }
                }
            }
            this.dataGridView1.Refresh();
        }

        private void ZhuZhuangTu_Form_Load(object sender, EventArgs e)
        {

        }

        string ceDianWeiZhi, mingCheng;
        KglZzuInfo kglZzuInfo;
        KglZtuInfo kglZtuInfo;
        private void button1_Click(object sender, EventArgs e)
        {
           
            if (this.comboBox1.SelectedIndex == -1)
                return;
            if (dateTimePicker1.Value.Date > DateTime.Now.Date)
            {
                dateTimePicker1.Value = DateTime.Now.Date;
            }
            if (CeDianBianHao != this.comboBox1.SelectedItem.ToString().Substring(0, 5))
            {
                CeDianBianHao = this.comboBox1.SelectedItem.ToString().Substring(0, 5);
                if (CeDianBianHao.Contains("C"))
                {
                    CeDianId = CurveData.GetLastKZLCeDianId(CeDianBianHao);
                }
                else
                {
                    CeDianId = CurveData.GetLastCeDianId(CeDianBianHao);
                }
            }
            IsExistMeasure = false;
            Measuredt = CurveData.GetMeasure(dateTimePicker1.Value.Date, dateTimePicker1.Value.Date.AddDays(1), CeDianId);
            if (Measuredt != null)
            {
                if (Measuredt.Rows.Count > 0)
                {
                    IsExistMeasure = true;
                }
            }
            DataTable dt1 = CeDian.GetInfoByCeDianBianHao(CeDianBianHao);
            this.ceDianWeiZhi = dt1.Rows[0]["ceDianWeiZhi"].ToString();
            this.mingCheng = dt1.Rows[0]["xiaoLeiXing"].ToString();
            kglZzuInfo = new KglZzuInfo();
            kglZtuInfo = new KglZtuInfo();
            CurveData.ZhuZhuangTuData(this.dateTimePicker1.Value.Date, CeDianBianHao, CeDianId, ref kglZzuInfo);
            CurveData.KaiGuangLiangData(this.dateTimePicker1.Value.Date, this.dateTimePicker1.Value.Date.AddDays(1), CeDianBianHao, CeDianId, ref kglZtuInfo);
            if (kglZzuInfo == null)
            {
                this.uC_KglChart1.timeStamps = new DateTime[] { this.dateTimePicker1.Value.Date, this.dateTimePicker1.Value.Date.AddDays(1) };
                uC_KglChart1.data = new double[] { 0,0};
            }
            else
            { 
                this.uC_KglChart1.data = kglZzuInfo.Value;
                this.uC_KglChart1.timeStamps = kglZzuInfo.date;
            }
            uC_KglChart1.Title = "开关量柱状图";
            uC_KglChart1.YTitle = this.comboBox1.Text;      
            this.uC_KglChart1.UpdateDisplay();
            FillDataGrid(uC_KglChart1.timeStamps[0]);        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                IntPtr ucIntPtr = this.uC_KglChart1.Handle;
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
