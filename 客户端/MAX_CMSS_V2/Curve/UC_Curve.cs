using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChartDirector;
using System.Globalization;

namespace MAX_CMSS_V2.Curve
{
    public partial class UC_Curve : UserControl
    {
        public CurveInfo BaoJingMenXian=new CurveInfo();
        public CurveInfo DuanDianMenXian=new CurveInfo();
        public CurveInfo FuDianMenXian=new CurveInfo();
        public List<CurveInfo> ListCurve=new List<CurveInfo>();
        public DateTime[] timeStamps;
        public string YTitle = "坐标轴标题";
        public string Title = "曲线";
        public delegate void UpdateData(int xIndex);
        public event UpdateData UpdateDataEvent;
        public UC_Curve()
        {
            InitializeComponent();
        }
        private void UC_Curve_Load(object sender, EventArgs e)
        {
            BaoJingMenXian.CurveColor=Color.Yellow;
            DuanDianMenXian.CurveColor=Color.Green;
            FuDianMenXian.CurveColor = Color.Fuchsia;
            winChartViewer1.MouseWheel += new MouseEventHandler(winChartViewer1_MouseWheel);

        }
        /// <summary>
        /// 首先设置 timeStamps
        /// </summary>
        public void UpdateDisplay()
        {           
            if (timeStamps != null)
            {
                winChartViewer1.setFullRange("x", timeStamps[0], timeStamps[timeStamps.Length - 1]);
                //// 初始化视口显示最新20％的时间范围
                //winChartViewer1.ViewPortWidth = 0.2;
                //winChartViewer1.ViewPortLeft = 1 - winChartViewer1.ViewPortWidth;
                // 设置最大放大至10点
                winChartViewer1.ZoomInWidthLimit = 10.0 / timeStamps.Length;
                winChartViewer1.ZoomInWidthLimit = 10.0 / timeStamps.Length;

                winChartViewer1.updateViewPort(true, true);

            }
        }
        public void UpdateViewPort(bool needUpdateChart, bool needUpdateImageMap)
        {
            winChartViewer1.updateViewPort(needUpdateChart, needUpdateImageMap);
        }
        /// <summary>
        /// 鼠标
        /// </summary>
        public WinChartMouseUsage SetMouseUsage
        {
            set
            {
                winChartViewer1.MouseUsage =value;
            }
        }
        /// <summary>
        /// 背景
        /// </summary>
        public Color BackColor
        {
             set
             {
                 winChartViewer1.BackColor = value;
             }
        }
        public Image ChartImage
        {
            get
            {
               return  this.winChartViewer1.Image;
            }
        }
        // 设置最大放大至10点            
        private void winChartViewer1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (winChartViewer1.Chart == null) return;
            // 我们要放大或缩小10％，视乎鼠标滚轮上的方向。
            double r = e.Delta > 0 ? 0.9 : 1 / 0.9;
            // 我们不超越变焦放大的宽度限制。
            if ((r = Math.Max(r, winChartViewer1.ZoomInWidthLimit / winChartViewer1.ViewPortWidth)) == 1)
                return;

            XYChart c = (XYChart)winChartViewer1.Chart;

            // 设置视口的位置和大小，以便它是由所期望的比例的放大/缩小在鼠标附近。
            double mouseOffset = (e.X - c.getPlotArea().getLeftX()) / (double)c.getPlotArea().getWidth();
            winChartViewer1.ViewPortLeft += mouseOffset * (1 - r) * winChartViewer1.ViewPortWidth;
            winChartViewer1.ViewPortWidth *= r;

            // 触发一个视口更改事件来更新图表
            winChartViewer1.updateViewPort(true, false);
        }
        private void winChartViewer1_ViewPortChanged(object sender, WinViewPortEventArgs e)
        {
            // Update the chart if necessary
            if (e.NeedUpdateChart)
                drawChart(winChartViewer1);

            // 我们需要更新的轨迹线。如果在图表上（例如：如果用户在图表上滚动，拖动鼠标），鼠标移动的轨迹线，将更新在MouseMovePlotArea事件。否则，我们需要更新的轨迹线。
            if ((!winChartViewer1.IsInMouseMoveEvent) && winChartViewer1.IsMouseOnPlotArea)
            {
                trackLineLabel((XYChart)winChartViewer1.Chart, winChartViewer1.PlotAreaMouseX, winChartViewer1.PlotAreaMouseY,0);
                winChartViewer1.updateDisplay();
            }
        }
        // 保存原始的主动控制的变量
        private System.Windows.Forms.Control activeControl = null;
        private void winChartViewer1_MouseEnter(object sender, EventArgs e)
        {
            // 保存原来的活动的控制和设置WinChartViewer，是主动控制，这样的WinChartViewer，可以接收鼠标滚轮事件。
            activeControl = winChartViewer1.FindForm().ActiveControl;
            winChartViewer1.FindForm().ActiveControl = winChartViewer1;
        }
        private void winChartViewer1_MouseLeave(object sender, EventArgs e)
        {
            winChartViewer1.FindForm().ActiveControl = activeControl;
        }
        private void winChartViewer1_MouseMovePlotArea(object sender, MouseEventArgs e)
        {
            WinChartViewer viewer = (WinChartViewer)sender;
            trackLineLabel((XYChart)viewer.Chart, viewer.PlotAreaMouseX, viewer.PlotAreaMouseY,e.X);
            viewer.updateDisplay();

            // 当鼠标离开的绘图区，隐藏轨道光标
            viewer.removeDynamicLayer("MouseLeavePlotArea");
        }
        int StartIndex;
        private void drawChart(WinChartViewer viewer)
        {           
            //获取的开始日期和结束日期是在图表上可见。
            DateTime viewPortStartDate = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft));
            DateTime viewPortEndDate = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft + viewer.ViewPortWidth));

            // 获取数组索引对应的可见的开始日期和结束日期
            int startIndex = (int)Math.Floor(Chart.bSearch(timeStamps, viewPortStartDate));
            StartIndex = startIndex;
            int endIndex = (int)Math.Ceiling(Chart.bSearch(timeStamps, viewPortEndDate));
            int noOfPoints = endIndex - startIndex + 1;

            // 提取的数据阵列中的一部分是可见的。
            DateTime[] viewPortTimeStamps = (DateTime[])Chart.arraySlice(timeStamps, startIndex, noOfPoints);


            //
            // 在这个阶段，我们提取的可视化数据。我们可以利用这些数据绘制图表。
            //

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
            c.addTitle(Title, "宋体 Bold", 18);

            // 添加图例框（55，25），使用水平布局。使用8pts宋体加粗字体。设置背景和边框颜色设置为透明，并使用线条样式图例项。
            LegendBox b = c.addLegend(55, 25, false, "宋体 Bold", 10);
            b.setBackground(Chart.Transparent);
            b.setLineStyleKey();

            // 设置轴干透明
            c.xAxis().setColors(Chart.Transparent);
            c.yAxis().setColors(Chart.Transparent);

            // 10分Arial粗体斜体字体添加坐标轴标题
            c.yAxis().setTitle(YTitle, "宋体 Bold", 10);

            //================================================================================
            // 将数据添加到图表
            //================================================================================

            //
            // 在这个例子中，我们所代表的由线的数据。您可以修改下面的代码使用其他陈述（区，散点图等）。
            //

            // 线的线层的添加，使用2个像素的线宽度
            LineLayer layer = c.addLineLayer2();
            layer.setLineWidth(1);

            // 在本演示中，我们并没有太多的数据点。在真正的代码，图表可能会包含大量的数据点时缩小 - 远远超过该地块面积水平像素数。所以这是一个好主意，使用快线模式。
            layer.setFastLineMode();

            // 现在我们添加3个数据系列的线层，使用红色（ff33333），绿色（008800）和蓝色（3333cc）
            layer.setXData(viewPortTimeStamps);
            foreach(CurveInfo info in ListCurve)
            {
                if (info.IsShow && info.CurveData!=null)
                {                    
                    double[] viewPortDataSeries = (double[])Chart.arraySlice(info.CurveData, startIndex, noOfPoints);
                    layer.addDataSet(viewPortDataSeries, Chart.CColor(info.CurveColor), info.CurveTitle);
                }            
            }
            if (DuanDianMenXian.IsShow)
            {
                LineLayer layer0 = c.addLineLayer2();
                layer0.addDataSet(DuanDianMenXian.CurveData, c.dashLineColor(Chart.CColor(DuanDianMenXian.CurveColor), Chart.DashLine), DuanDianMenXian.CurveTitle);
                layer0.setXData(new DateTime[] { viewPortTimeStamps[0], viewPortTimeStamps[viewPortTimeStamps.Length-1] });
                layer0.setLineWidth(1);
            }
            if (FuDianMenXian.IsShow)
            {
                LineLayer layer0 = c.addLineLayer2();
                layer0.addDataSet(FuDianMenXian.CurveData, c.dashLineColor(Chart.CColor(FuDianMenXian.CurveColor), Chart.DashLine), FuDianMenXian.CurveTitle);
                layer0.setXData(new DateTime[] { viewPortTimeStamps[0], viewPortTimeStamps[viewPortTimeStamps.Length - 1] });
                layer0.setLineWidth(1);
            }
            if (BaoJingMenXian.IsShow)
            {
                
                LineLayer layer0 = c.addLineLayer2();
                layer0.addDataSet(BaoJingMenXian.CurveData, c.dashLineColor(Chart.CColor(BaoJingMenXian.CurveColor), Chart.DashLine), BaoJingMenXian.CurveTitle);
                layer0.setXData(new DateTime[] { viewPortTimeStamps[0], viewPortTimeStamps[viewPortTimeStamps.Length - 1] });
                layer0.setLineWidth(1);               
            }
            //================================================================================
            //配置轴的刻度和标签
            //================================================================================

            //根据视图端口x范围轴与规模作为一个日期/时间的x轴设置。
            viewer.syncDateAxisWithViewPort("x", c.xAxis());

            //
            // 在本演示中，可以从几年到了几天的时间范围。我们演示了如何设置不同的日期/时间格式的时间范围。
            //

            // If all ticks are yearly aligned, then we use "yyyy" as the label format.
            c.xAxis().setFormatCondition("align", 360 * 86400);
            c.xAxis().setLabelFormat("{value|yyyy}");

            // If all ticks are monthly aligned, then we use "mmm yyyy" in bold font as the first label of a
            // year, and "mmm" for other labels.
            c.xAxis().setFormatCondition("align", 30 * 86400);
            c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "<*font=bold*>{value|yyyy年mm月}",
                Chart.AllPassFilter(), "{value|mm}");

            // If all ticks are daily algined, then we use "mmm dd<*br*>yyyy" in bold font as the first
            // label of a year, and "mmm dd" in bold font as the first label of a month, and "dd" for other
            // labels.
            c.xAxis().setFormatCondition("align", 86400);
            c.xAxis().setMultiFormat(Chart.StartOfYearFilter(),
                "<*block,halign=left*><*font=bold*>{value|mm月dd日<*br*>yyyy年}", Chart.StartOfMonthFilter(),
                "<*font=bold*>{value|yyyy年mm月dd日}");
            c.xAxis().setMultiFormat2(Chart.AllPassFilter(), "{value|dd}");

            // For all other cases (sub-daily ticks), use "hh:nn<*br*>mmm dd" for the first label of a day,
            // and "hh:nn" for other labels.
            c.xAxis().setFormatCondition("else");
            c.xAxis().setMultiFormat(Chart.StartOfDayFilter(), "<*font=bold*>{value|hh:nn<*br*>yyyy年mm月dd日}",
                Chart.AllPassFilter(), "{value|hh:nn:ss}");

            //================================================================================
            // Output the chart
            //================================================================================

            viewer.Chart = c;
        }        //
        /// <summary>
        /// 绘制轨迹线与数据标签
        /// </summary>
        /// <param name="c"></param>
        /// <param name="mouseX"></param>
        private void trackLineLabel(XYChart c, int mouseX,int mouseY,int Mx)
        {
           
            //清除当前的动态层和得到的DrawArea的对象上绘制它。
            DrawArea d = c.initDynamicLayer();

            // 该地块面积对象
            PlotArea plotArea = c.getPlotArea();

            // 获取数据的x值是最接近的鼠标，发现其像素坐标。
            double xValue = c.getNearestXValue(mouseX);
            int xCoor = c.getXCoor(xValue);

            double yValue = c.getNearestXValue(mouseY);
            int yCoort = c.getXCoor(yValue);
            if (Mx == 0) Mx = mouseX;
            // 的x位置处画一条垂直的轨迹线
            d.vline(plotArea.getTopY(), plotArea.getBottomY(), Mx, Chart.CColor(Color.Red));
            d.hline(plotArea.getLeftX(), plotArea.getRightX(), mouseY, Chart.CColor(Color.Red));

            string PlayTxt = "<*font , bgColor=33FF66*> <*font,Color=000000*>时间：" + c.xAxis().getFormattedLabel(xValue, "hh:nn:ss") + "<*/font*>\r\n";
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
            PlayTxt =PlayTxt+ "<*font*>";
            TTFText t = d.text(PlayTxt, "宋体", 11);
            // 限制的x像素的标签的位置，以确保它保持在图表内图像。
            if (mouseX < t.getWidth())
            {
                t.draw(mouseX, mouseY - t.getHeight(), 0xffffff);
            }
            else if ((plotArea.getRightX() - mouseX) < t.getWidth())
            {
                t.draw(mouseX - t.getWidth()-10, mouseY - t.getHeight(), 0xffffff);
            }
            else
            {
                t.draw(mouseX, mouseY - t.getHeight(), 0xffffff);
            }
            if (UpdateDataEvent != null)
            {
                UpdateDataEvent(xIndex + StartIndex);                          //抛出事件，给所有相应者
            }
        }
    }
}
