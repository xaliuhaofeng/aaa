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
    public partial class UC_KglChart : UserControl
    {

        public double[] data;
        public DateTime[] timeStamps;
        public string YTitle = "坐标轴标题";
        public string Title = "曲线";
        public delegate void UpdateData(DateTime datetime);
        public event UpdateData UpdateDataEvent;
        public UC_KglChart()
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
            c.setPlotArea(60, 40, c.getWidth() - 80, c.getHeight() - 80, 0xe8f0f8, -1, Chart.Transparent, c.dashLineColor(0x888888, Chart.DotLine), -1);
            c.setBackground(c.linearGradientColor(0, 0, 0, c.getHeight() / 2, 0xe8f0f8, 0xaaccff), 0x88aaee);
            c.setRoundedFrame();
            c.setDropShadow();
            // 将标题添加到图表
            c.addTitle(Title, "宋体 Bold", 18);
            c.yAxis().setTitle(YTitle, "宋体 Bold", 10);
            // 设置轴干透明
            c.xAxis().setColors(Chart.Transparent);
            c.yAxis().setColors(Chart.Transparent);
            c.yAxis().setLabelFormat("{value}%");
            c.yAxis().setLinearScale(0, 100, 20);
            c.xAxis().setRounding(false, false);           
            //================================================================================
            //配置轴的刻度和标签
            //================================================================================
            c.xAxis().setLabels(timeStamps);
            c.xAxis().setTickOffset(0.5);
            c.xAxis().setLabelOffset(-0.5);
            //c.addBarLayer3(data, new int[] { Chart.CColor(Color.Black) }).setBarShape(Chart.CircleShape);
            BarLayer ba = c.addBarLayer2();
            ba.addDataSet(data, Chart.CColor(SystemColors.ControlDark));
            c.xAxis().setMultiFormat(Chart.StartOfDayFilter(), "<*font=bold*>{value|hh:nn<*br*>yyyy年mm月dd日}", Chart.AllPassFilter(), "{value|hh:nn}");
            viewer.Chart = c;
            double width = c.getPlotArea().getWidth();
            pxM = Math.Round(timeStamps.Length*60 / width, 3);//
            Hpx = width / timeStamps.Length;
        }
        /// <summary>
        /// 1像素多少分；
        /// </summary>
        private double pxM;
        /// <summary>
        /// 1小时多少像素;
        /// </summary>
        private double Hpx;
        /// <summary>
        /// 绘制轨迹线与数据标签
        /// </summary>
        /// <param name="c"></param>
        /// <param name="mouseX"></param>
        private void trackLineLabel(XYChart c, int mouseX,int mouseY,int MX)
        {           
            //清除当前的动态层和得到的DrawArea的对象上绘制它。
            DrawArea d = c.initDynamicLayer();
            // 该地块面积对象
            PlotArea plotArea = c.getPlotArea();
            // 获取数据的x值是最接近的鼠标，发现其像素坐标。
            double xValue = c.getNearestXValue(mouseX);
            int xCoor = c.getXCoor(xValue);
            // 的x位置处画一条垂直的轨迹线
            d.vline(plotArea.getTopY(), plotArea.getBottomY(), MX, Chart.CColor(Color.Red));
            d.hline(plotArea.getLeftX(), plotArea.getRightX(),mouseY, Chart.CColor(Color.Red));
            DateTime mouDateTime=timeStamps[(int)xValue].AddMinutes(pxM * (MX - 60 - xValue * Hpx));
            string PlayTxt = "<*div, bgColor=33FF66*><*font,Color=000000*>" + mouDateTime.ToString("HH:mm:00") + "<*/font*>";
            for (int i = 0; i < c.getLayerCount(); ++i)
            {
                Layer layer = c.getLayerByZ(i);
                // 的x值的数据数组的索引
                int xIndex = layer.getXIndexOf(xValue);
                if (xIndex == -1) continue; 
                // 遍历所有数据集在层
                for (int j = 0; j < layer.getDataSetCount(); ++j)
                {                    
                    ChartDirector.DataSet dataSet = layer.getDataSetByZ(j);
                    string value = c.formatValue(dataSet.getValue(xIndex), "{value|P4}");
                    if (value == "") continue;
                    PlayTxt = PlayTxt + string.Format(" <*font,Color=000000*>{0}:{1}<*/font*>", "开机效率", c.formatValue(dataSet.getValue(xIndex), "{value|P4}%")); 
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
                UpdateDataEvent(mouDateTime);                          //抛出事件，给所有相应者
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
