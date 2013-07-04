namespace MAX_CMSS_V2.Curve
{
    partial class UC_Curve
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.winChartViewer1 = new ChartDirector.WinChartViewer();
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // winChartViewer1
            // 
            this.winChartViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winChartViewer1.Location = new System.Drawing.Point(0, 0);
            this.winChartViewer1.Name = "winChartViewer1";
            this.winChartViewer1.ScrollDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.winChartViewer1.Size = new System.Drawing.Size(998, 465);
            this.winChartViewer1.TabIndex = 0;
            this.winChartViewer1.TabStop = false;
            this.winChartViewer1.ZoomDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.winChartViewer1.MouseMovePlotArea += new System.Windows.Forms.MouseEventHandler(this.winChartViewer1_MouseMovePlotArea);
            this.winChartViewer1.ViewPortChanged += new ChartDirector.WinViewPortEventHandler(this.winChartViewer1_ViewPortChanged);
            this.winChartViewer1.MouseEnter += new System.EventHandler(this.winChartViewer1_MouseEnter);
            this.winChartViewer1.MouseLeave += new System.EventHandler(this.winChartViewer1_MouseLeave);
            // 
            // UC_Curve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.winChartViewer1);
            this.Name = "UC_Curve";
            this.Size = new System.Drawing.Size(998, 465);
            this.Load += new System.EventHandler(this.UC_Curve_Load);
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ChartDirector.WinChartViewer winChartViewer1;
    }
}
