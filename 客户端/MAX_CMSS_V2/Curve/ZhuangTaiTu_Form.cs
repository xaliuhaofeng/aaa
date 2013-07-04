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
    public partial class ZhuangTaiTu_Form : UserControl
    {
        public string CeDianBianHao = "";
        public int CeDianId = 0;
        private DataTable Measuredt = new DataTable();
        private bool IsExistMeasure = false;
        private bool IsGetMeasure = false;
        public ZhuangTaiTu_Form( )
        {
            InitializeComponent();
            Type type = dataGridView1.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dataGridView1, true, null);
            this.uC_KglChart1.UpdateDataEvent += new UC_KglZttChart.UpdateData(uC_KglChart1_UpdateDataEvent);
            ClientConfig cConf = ClientConfig.CreateCommon();
            uC_KglChart1.CurveColor = ColorTranslator.FromHtml(cConf.get("moNiLiangColor"));
            this.comboBox1.DataSource = CurveData.GetKglPnts();
            this.dateTimeChooser1.StartTime = DateTime.Now.Date;
            this.dateTimeChooser1.EndTime = System.DateTime.Now;
        }

        void uC_KglChart1_UpdateDataEvent(int Index)
        {
            FillDataGrid(Index);
        }
        public void FillDataGrid(int Index)
        {
            IsGetMeasure = false;           
            this.dataGridView1.Rows[0].Cells[0].Value = this.ceDianWeiZhi + "/" + this.mingCheng;
            if (Index < 0) return;
            this.dataGridView1.Rows[0].Cells[1].Value = "";
            this.dataGridView1.Rows[0].Cells[2].Value = "";
            this.dataGridView1.Rows[0].Cells[3].Value =  "解除";
            this.dataGridView1.Rows[0].Cells[4].Value = "复电";
            this.dataGridView1.Rows[0].Cells[5].Value ="正常";
            this.dataGridView1.Rows[0].Cells[6].Value = "";     
            if ( kglZtuInfo == null) return;
            DateTime indexTime = kglZtuInfo.date[Index];
            this.dataGridView1.Rows[0].Cells[1].Value = kglZtuInfo.status[Index];
            this.dataGridView1.Rows[0].Cells[2].Value = indexTime.ToString("HH:mm:ss");
            this.dataGridView1.Rows[0].Cells[3].Value = kglZtuInfo.baojing[Index] ? "报警" : "解除";
            this.dataGridView1.Rows[0].Cells[4].Value = kglZtuInfo.duandian[Index] ? "断电" : "复电";
            this.dataGridView1.Rows[0].Cells[5].Value = kglZtuInfo.kuidian[Index] ? "异常" : "正常";
            if (IsExistMeasure)
            {
                if (kglZtuInfo.baojing[Index] || kglZtuInfo.duandian[Index] || kglZtuInfo.kuidian[Index])
                {
                    IsGetMeasure = true;
                }
                if (IsGetMeasure)
                {
                    DataRow[] drs = Measuredt.Select(" CuoShiShiJian>='" + indexTime.AddMinutes(-10) + "' and  CuoShiShiJian<='" + indexTime.AddMinutes(10) + "'");
                    foreach (DataRow dr in drs)
                    {
                        dataGridView1.Rows[0].Cells[6].Value = dataGridView1.Rows[0].Cells[6].Value + string.Format("{0} {1}   ", dr["CuoShiShiJian"].ToString(), dr["CuoShi"].ToString());
                    }
                }
            }
            this.dataGridView1.Refresh();
        }

        private void ZhuZhuangTu_Form_Load(object sender, EventArgs e)
        {

        }

        string ceDianWeiZhi, mingCheng;
        KglZtuInfo kglZtuInfo;
        private void OnClick()
        {
            if (this.comboBox1.SelectedIndex == -1)
                return;
          DateTime   startTime = this.dateTimeChooser1.StartTime;
          DateTime endTime = this.dateTimeChooser1.EndTime;
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
            Measuredt = CurveData.GetMeasure(startTime, endTime, CeDianId);
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
            kglZtuInfo = new KglZtuInfo();

            CurveData.KaiGuangLiangData(startTime,endTime, CeDianBianHao, CeDianId, ref kglZtuInfo);

            if (kglZtuInfo == null)
            {
                this.uC_KglChart1.timeStamps = new DateTime[] { startTime, endTime };
                uC_KglChart1.data = new double[] { 0, 0 };
                uC_KglChart1.state = new string[] { "", "" };

            }
            else
            {
                this.uC_KglChart1.state = kglZtuInfo.status;
                this.uC_KglChart1.data = kglZtuInfo.value;
                this.uC_KglChart1.timeStamps = kglZtuInfo.date;
            }
            uC_KglChart1.Title = "开关量状态图";
            uC_KglChart1.YTitle = this.comboBox1.Text;
            this.uC_KglChart1.UpdateDisplay();
            FillDataGrid(0);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OnClick();
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
