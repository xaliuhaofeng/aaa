using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChartDirector;

namespace MAX_CMSS_V2.Curve
{
    public partial class UC_KglZttChart1 : UserControl
    {

        public double[] data;
        public string[] state;
        public DateTime[] timeStamps;
        public string YTitle = "坐标轴标题";
        public string Title = "曲线";
        public delegate void UpdateData(DateTime datetime,int Index);
        public event UpdateData UpdateDataEvent;
        public Color CurveColor=Color.Red;
        public UC_KglZttChart1()
        {
            InitializeComponent();
        }
        private void UC_Curve_Load(object sender, EventArgs e)
        {       

        }
        /// <summary>
        /// 首先设置 timeStamps
        /// </summary>
        public void UpdateDisplay()
        {           
            if (timeStamps != null)
            {
                winChartViewer1.updateViewPort(true, true);
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
        private void winChartViewer1_ViewPortChanged(object sender, WinViewPortEventArgs e)
        {
            // Update the chart if necessary
            if (e.NeedUpdateChart)
                drawChart(winChartViewer1);
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
        private void drawChart(WinChartViewer viewer)
        {   
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
            c.yAxis().setLinearScale(-1, 2, 1);
            c.xAxis().setLinearScale(Chart.CTime(timeStamps[0]), Chart.CTime(timeStamps[timeStamps.Length - 1]),3600);


            //================================================================================
            //配置轴的刻度和标签
            //================================================================================
            // 线的线层的添加，使用2个像素的线宽度
            LineLayer layer = c.addLineLayer2();
            layer.setLineWidth(1);

            // 在本演示中，我们并没有太多的数据点。在真正的代码，图表可能会包含大量的数据点时缩小 - 远远超过该地块面积水平像素数。所以这是一个好主意，使用快线模式。
            layer.setFastLineMode();

            // 现在我们添加3个数据系列的线层，使用红色（ff33333），绿色（008800）和蓝色（3333cc）
            layer.setXData(timeStamps);
            layer.addDataSet(data, Chart.CColor(CurveColor));            
            c.xAxis().setMultiFormat(Chart.StartOfDayFilter(), "<*font=bold*>{value|hh:nn<*br*>yyyy年mm月dd日}", Chart.AllPassFilter(), "{value|hh:nn}");
            viewer.Chart = c;
        }
        /// <summary>
        /// 绘制轨迹线与数据标签
        /// </summary>
        /// <param name="c"></param>
        /// <param name="mouseX"></param>
        private void trackLineLabel(XYChart c, int mouseX, int mouseY, int Mx)
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
            d.vline(plotArea.getTopY(), plotArea.getBottomY(), mouseX, Chart.CColor(Color.Red));
            d.hline(plotArea.getLeftX(), plotArea.getRightX(), mouseY, Chart.CColor(Color.Red));


            DateTime mouDateTime = Convert.ToDateTime(c.xAxis().getFormattedLabel(xValue, "yyyy-mm-dd hh:nn:ss"));

            string PlayTxt = "<*div, bgColor=33FF66*><*font,Color=000000*>" + mouDateTime.ToString("HH:mm:ss") + "<*/font*>";
            int xIndex=0;
            if (state.Length == timeStamps.Length)
            {
                for (int i = 0; i < c.getLayerCount(); ++i)
                {
                    Layer layer = c.getLayerByZ(i);
                    // 的x值的数据数组的索引
                    xIndex = layer.getXIndexOf(xValue);
                    if (xIndex == -1) continue;
                    // 遍历所有数据集在层
                    for (int j = 0; j < layer.getDataSetCount(); ++j)
                    {
                        ChartDirector.DataSet dataSet = layer.getDataSetByZ(j);
                        string value = c.formatValue(dataSet.getValue(xIndex), "{value|P4}");
                        if (value == "") continue;
                        PlayTxt = PlayTxt + string.Format(" <*font,Color=000000*>{0}:{1}<*/font*>", "状态", state[xIndex]);
                    }
                }
            }
            PlayTxt =PlayTxt+ "<*div*>";
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
            if (UpdateDataEvent != null)
            {
                UpdateDataEvent(mouDateTime, xIndex);                          //抛出事件，给所有相应者
            }
        }
        private void winChartViewer1_MouseMovePlotArea(object sender, MouseEventArgs e)
        {
            WinChartViewer viewer = (WinChartViewer)sender;
            trackLineLabel((XYChart)viewer.Chart, viewer.PlotAreaMouseX, viewer.PlotAreaMouseY,e.X);
            viewer.updateDisplay();

            // 当鼠标离开的绘图区，隐藏轨道光标
            viewer.removeDynamicLayer("MouseLeavePlotArea");
        }
    }
}
