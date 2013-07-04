using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logic;
using MyPictureBox;
using System.Drawing.Printing;
using MAX_CMSS_V2.Curve;
using ChartDirector;
using System.Collections;

namespace MAX_CMSS_V2.Curve
{
    public partial class RealTime_curve : UserControl
    {
        ClientConfig cConf;
        string currentCeDianBianHao;
        byte fenZhanHao;
        byte tongDaoHao;

        /// <summary>
        /// 报警值
        /// </summary>
        private CurveInfo DataCurve = new CurveInfo();
        public CurveInfo BaoJingMenXian = new CurveInfo();
        public CurveInfo DuanDianMenXian = new CurveInfo();
        public CurveInfo FuDianMenXian = new CurveInfo();
        private delegate void RefreshCurve(FenZhanRTdata ud);

        public RealTime_curve(string ceDianBianHao)
        {
            InitializeComponent();
            this.comboBox1.DataSource = GlobalParams.AllCeDianList.getCeDianAllInfo(0);
            if (ceDianBianHao == null)
            {
                if (this.comboBox1.Items.Count ==0)
                    return;
                this.comboBox1.SelectedIndex = 0;
                this.currentCeDianBianHao = this.comboBox1.SelectedItem.ToString().Substring(0, 5);
            }
            else if(ceDianBianHao!="")
            {
                this.currentCeDianBianHao = ceDianBianHao;
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

        public void click()
        {
            if (this.comboBox1.SelectedIndex == -1)
                return;

            this.currentCeDianBianHao = this.comboBox1.SelectedItem.ToString().Substring(0, 5);
            this.fenZhanHao = Convert.ToByte(this.currentCeDianBianHao.Substring(0, 2));
            this.tongDaoHao = Convert.ToByte(this.currentCeDianBianHao.Substring(3, 2));
            float[] info = CeDian.GetMoNiLiangInfoByCeDiaanBianHao(this.currentCeDianBianHao);
            if (this.comboBox1.SelectedIndex == -1)
                return;

            this.currentCeDianBianHao = this.comboBox1.SelectedItem.ToString().Substring(0, 5);
            this.fenZhanHao = Convert.ToByte(this.currentCeDianBianHao.Substring(0, 2));
            this.tongDaoHao = Convert.ToByte(this.currentCeDianBianHao.Substring(3, 2));

            BaoJingMenXian.CurveTitle = "报警门限";
            DuanDianMenXian.CurveTitle = "断电门限";
            FuDianMenXian.CurveTitle = "复电门限";
            InitData(info);
            this.winChartViewer1.updateViewPort(true, false);
        }
        private const int sampleSize = 120;
        private DateTime[] timeStamps = new DateTime[sampleSize];
        public void InitData(float[] info)
        {
            BaoJingMenXian.CurveData = new double[sampleSize];
            DuanDianMenXian.CurveData = new double[sampleSize];
            FuDianMenXian.CurveData = new double[sampleSize];
            DataCurve.CurveData = new double[sampleSize];
            for (int i = 0; i < sampleSize; ++i)
            {
                BaoJingMenXian.CurveData[i] = info[2];
                DuanDianMenXian.CurveData[i] = info[3];
                FuDianMenXian.CurveData[i] = info[4];
                DataCurve.CurveData[i] = Chart.NoValue;
                timeStamps[i] = DateTime.Now.AddSeconds((sampleSize - i) * (-5));
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            BaoJingMenXian.IsShow = this.checkBox1.Checked;
            cConf.set("shiShiShowBaoJingMenXian", this.checkBox1.Checked.ToString());
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            DuanDianMenXian.IsShow = this.checkBox2.Checked;
            cConf.set("shiShiShowDuanDianMenXian", this.checkBox2.Checked.ToString());
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            FuDianMenXian.IsShow = this.checkBox3.Checked;
            cConf.set("shiShiShowFuDianMenXian", this.checkBox3.Checked.ToString());
        }

        public void Dispatch(FenZhanRTdata ud)
        {
            if (this.fenZhanHao != ud.fenZhanHao)
            {
                return;
            }
            while (!this.winChartViewer1.IsHandleCreated)
                ;
            this.winChartViewer1.BeginInvoke(new RefreshCurve(refreshCurve), new object[] { ud });

        }
        private void refreshCurve(FenZhanRTdata ud)
        {
            shiftData(DataCurve.CurveData, ud.realValue[this.tongDaoHao]);
            shiftData(timeStamps, ud.uploadTime);
            this.winChartViewer1.updateViewPort(true, false);
        }


        private void shiftData(double[] data, double newValue)
        {
            for (int i = 1; i < data.Length; ++i)
                data[i - 1] = data[i];
            data[data.Length - 1] = newValue;
        }
        private void shiftData(DateTime[] data, DateTime newValue)
        {
            for (int i = 1; i < data.Length; ++i)
                data[i - 1] = data[i];
            data[data.Length - 1] = newValue;
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                IntPtr ucIntPtr = winChartViewer1.Handle;
                Bitmap img = ImageCapture.GetWindow(ucIntPtr);
                PrintCurve print = new PrintCurve();
                print.Print_Curve(img);
            }
            catch
            {
                MessageBox.Show("打印失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            click();         
        }
        private void RealTime_curve_Load(object sender, EventArgs e)
        {
          
          //  this.comboBox1.DataSource = GlobalParams.AllCeDianList.getCeDianAllInfo(0);
            cConf = ClientConfig.CreateCommon();
            BaoJingMenXian.IsShow = Convert.ToBoolean(cConf.get("shiShiShowBaoJingMenXian"));
            BaoJingMenXian.CurveColor = ColorTranslator.FromHtml(cConf.get("baoJingZhiColor"));
            this.checkBox1.Checked = BaoJingMenXian.IsShow;
            DuanDianMenXian.IsShow = Convert.ToBoolean(cConf.get("shiShiShowDuanDianMenXian"));
            DuanDianMenXian.CurveColor = ColorTranslator.FromHtml(cConf.get("duanDianZhiColor"));
            this.checkBox2.Checked = DuanDianMenXian.IsShow;
            FuDianMenXian.IsShow = Convert.ToBoolean(cConf.get("shiShiShowFuDianMenXian"));
            FuDianMenXian.CurveColor = ColorTranslator.FromHtml(cConf.get("fuDianZhiColor"));
            this.checkBox3.Checked = FuDianMenXian.IsShow;
            DataCurve.IsShow = true;
            DataCurve.CurveColor = ColorTranslator.FromHtml(cConf.get("moNiLiangColor"));
            DataCurve.CurveTitle = "曲线数据";
            click();
            this.timer1.Start();

           
        }

        private void winChartViewer1_ViewPortChanged(object sender, ChartDirector.WinViewPortEventArgs e)
        {
            drawChart(winChartViewer1);
        }
        private void drawChart(WinChartViewer viewer)
        {

            //================================================================================
            // 配置整体图外观。
            //================================================================================

            XYChart c = new XYChart(this.winChartViewer1.Width, winChartViewer1.Height);
            // 设置的plotarea，（55，50）的宽度小于图表的宽度和高度小于图表高度85像素80像素。使用淡蓝色（e8f0f8）的背景下，透明的边框，和 灰色（888888）的点缀水平和垂直网格线。
            c.setPlotArea(55, 50, c.getWidth() - 80, c.getHeight() - 85, 0xe8f0f8, -1, Chart.Transparent, c.dashLineColor(0x888888, Chart.DotLine), -1);
            c.setBackground(c.linearGradientColor(0, 0, 0, c.getHeight() / 2, 0xe8f0f8, 0xaaccff), 0x88aaee);
            c.setRoundedFrame();
            c.setDropShadow();
            // 由于数据可以不在plotarea，一个放大的图表，我们需要启用裁剪。
            c.setClipping();
            // 将标题添加到图表
            c.addTitle("实时数据曲线", "宋体 Bold", 18);

            // 添加图例框（55，25），使用水平布局。使用8pts宋体加粗字体。设置背景和边框颜色设置为透明，并使用线条样式图例项。
            LegendBox b = c.addLegend(55, 25, false, "宋体 Bold", 10);
            b.setBackground(Chart.Transparent);
            b.setLineStyleKey();

            // 设置轴干透明
            c.xAxis().setColors(Chart.Transparent);
            c.yAxis().setColors(Chart.Transparent);

            // 10分Arial粗体斜体字体添加坐标轴标题
            c.yAxis().setTitle(this.comboBox1.Text, "宋体 Bold", 10);
            // Now we add the data to the chart
            DateTime lastTime = timeStamps[timeStamps.Length - 1];

            //// Set up the x-axis scale. In this demo, we set the x-axis to show the last 240 
            //// samples, with 250ms per sample.
          //  c.xAxis().setDateScale(lastTime.AddSeconds( -5 * timeStamps.Length ), lastTime);

            // Set the x-axis label format
            c.xAxis().setLabelFormat("{value|hh:nn:ss}");

            // Create a line layer to plot the lines
            LineLayer layer = c.addLineLayer2();
            // The x-coordinates are the timeStamps.
            layer.setXData(timeStamps);
            layer.addDataSet(DataCurve.CurveData, Chart.CColor(Color.Black),"实时数据");
            if (DuanDianMenXian.IsShow)
            {
                layer.addDataSet(DuanDianMenXian.CurveData, c.dashLineColor(Chart.CColor(DuanDianMenXian.CurveColor), Chart.DashLine), DuanDianMenXian.CurveTitle);
            }
            if (FuDianMenXian.IsShow)
            {
                layer.addDataSet(FuDianMenXian.CurveData, c.dashLineColor(Chart.CColor(FuDianMenXian.CurveColor), Chart.DashLine), FuDianMenXian.CurveTitle);
            }
            if (BaoJingMenXian.IsShow)
            {
                layer.addDataSet(BaoJingMenXian.CurveData, c.dashLineColor(Chart.CColor(BaoJingMenXian.CurveColor), Chart.DashLine), BaoJingMenXian.CurveTitle);
            }
            // Assign the chart to the WinChartViewer
            viewer.Chart = c;
        }
        private void winChartViewer1_MouseMovePlotArea(object sender, MouseEventArgs e)
        {
            WinChartViewer viewer = (WinChartViewer)sender;
            trackLineLegend((XYChart)viewer.Chart, viewer.PlotAreaMouseX, viewer.PlotAreaMouseY);
            viewer.updateDisplay();
        }

        //
        // Draw the track line with legend
        //
        private void trackLineLegend(XYChart c, int mouseX,int mouseY)
        {
            // Clear the current dynamic layer and get the DrawArea object to draw on it.
            DrawArea d = c.initDynamicLayer();

            // The plot area object
            PlotArea plotArea = c.getPlotArea();

            // Get the data x-value that is nearest to the mouse, and find its pixel coordinate.
            double xValue = c.getNearestXValue(mouseX);
            int xCoor = c.getXCoor(xValue);

            // Draw a vertical track line at the x-position
            d.vline(plotArea.getTopY(), plotArea.getBottomY(), xCoor, Chart.CColor(Color.Red));
            d.hline(plotArea.getLeftX(), plotArea.getRightX(), mouseY, Chart.CColor(Color.Red));

            string PlayTxt = "<*font, bgColor=33FF66*>  <*font,Color=000000*>时间：" + c.xAxis().getFormattedLabel(xValue, "hh:nn:ss") + "<*/font*><*br*>";
            int xIndex = 0;
            for (int i = 0; i < c.getLayerCount(); ++i)
            {
                Layer layer = c.getLayerByZ(i);
                // 的x值的数据数组的索引
                xIndex = layer.getXIndexOf(xValue);
                // 遍历所有数据集在层
                for (int j = 0; j < layer.getDataSetCount(); ++j)
                {
                    ChartDirector.DataSet dataSet = layer.getDataSetByZ(j);
                    string value = c.formatValue(dataSet.getValue(xIndex), "{value|P4}");
                    if (value == "") continue;
                    PlayTxt = PlayTxt + string.Format(" <*font,Color={0}*>{1}:{2}<*/font*><*br*>", dataSet.getDataColor().ToString("x"), dataSet.getDataName(), c.formatValue(dataSet.getValue(xIndex), "{value|P4}"));
                }
            }
            PlayTxt = PlayTxt + "<*font*>";
            TTFText t = d.text(PlayTxt, "宋体", 11);
            // 限制的x像素的标签的位置，以确保它保持在图表内图像。
            if (mouseX < t.getWidth())
            {
                t.draw(mouseX, mouseY - t.getHeight(), 0xffffff);
            }
            else if ((plotArea.getRightX() - mouseX) < t.getWidth())
            {
                t.draw(mouseX - t.getWidth(), mouseY - t.getHeight(), 0xffffff);
            }
            else
            {
                t.draw(mouseX, mouseY - t.getHeight(), 0xffffff);
            }
        }
        DateTime curdatetime=DateTime.MinValue;

        private void timer1_Tick(object sender, EventArgs e)
        {
            CeDian cedian = CurveData.GetMKCedian(this.fenZhanHao, tongDaoHao);
          if (curdatetime == cedian.Time) return;
          curdatetime = cedian.Time;
          shiftData(DataCurve.CurveData,cedian.RtVal);
          shiftData(timeStamps, cedian.Time);
          this.winChartViewer1.updateViewPort(true, false);
        }
    }
}
