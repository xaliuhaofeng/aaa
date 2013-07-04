namespace MyPictureBox
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class RealTimeCurveControl : UserControl
    {
        private Color backGroundColor = Color.Black;
        private Color baoJingCurveColor = Color.Yellow;
        private float baoJingMenXian;
        private Color baoJingMenXianColor = Color.Green;
        private int baoJingNow = 0;
        private RealTimeCoordinatesValue[] baoJingQuXian;
        private Container components = null;
        private float coordinate = 50f;
        private float curvePenWidth = 1f;
        private int curveRemove = 1;
        private float duanDianMenXian;
        private Color duanDianMenXianColor = Color.DeepPink;
        private float fuDianMenXian;
        private Color fuDianMenXianColor = Color.Fuchsia;
        private Color gridColor = Color.DarkGreen;
        private float gridCompart = 1f;
        private float gridFontSize = 9f;
        private Color gridForeColor = Color.Yellow;
        private float gridHeight = 30f;
        private float gridPenWidth = 1f;
        private float gridRemoveX = 1f;
        private float gridWidth = 10f;
        private Label labShowView;
        private int lastTimeSystemWindowHeight = 0;
        private int lastTimeSystemWindowWidth = 0;
        private int maxNote = 0x3e8;
        private PictureBox picCurveShow;
        private Color pingJunZhiCurveColor = Color.Red;
        private int pingJunZhiNow = 0;
        private RealTimeCoordinatesValue[] pingJunZhiQuXian;
        private Color rectangleColor = Color.White;
        private float rectangleWidth = 1f;
        private bool removeGrid = true;
        private bool showBaoJingMenXian;
        private bool showBaoJingQuXian;
        private bool showBgGrid;
        private bool showDuanDianMenXian;
        private bool showFuDianMenXian;
        private bool showPingJunZhiQuXian;
        private bool showTime = true;
        private Color showTimeColor = Color.Red;
        private bool showZuiDaZhiQuXian;
        private bool showZuiXiaoZhiQuXian;
        private TextBox textBox1;
        private string title = "王品曲线代码测试DEMO";
        private Color titleColor = Color.Yellow;
        private float xYPrecision = 4f;
        private string yBaoJingMenXianString = "上限：";
        private string yDuanDianMenXianString = "下限：";
        private string yFuDianMenXianString = "下限：";
        private string yMaxString = "最大量程：";
        private float yMaxValue = 100f;
        private string yMinString = "最小量程：";
        private float yMinValue = 0f;
        private Color yUpperAndLowerColor = Color.Lime;
        private float yUpperAndLowerPenWidth = 1f;
        private Color zuiDaZhiCurveColor = Color.White;
        private int zuiDaZhiNow = 0;
        private RealTimeCoordinatesValue[] zuiDaZhiQuXian;
        private Color zuiXiaoZhiCurveColor = Color.Blue;
        private int zuiXiaoZhiNow = 0;
        private RealTimeCoordinatesValue[] zuiXiaoZhiQuXian;

        public RealTimeCurveControl(int width, int height)
        {
            this.InitializeComponent();
            this.zuiDaZhiQuXian = new RealTimeCoordinatesValue[this.maxNote];
            this.zuiXiaoZhiQuXian = new RealTimeCoordinatesValue[this.maxNote];
            this.pingJunZhiQuXian = new RealTimeCoordinatesValue[this.maxNote];
            this.baoJingQuXian = new RealTimeCoordinatesValue[this.maxNote];
            this.lastTimeSystemWindowHeight = this.picCurveShow.Height;
            this.lastTimeSystemWindowWidth = this.picCurveShow.Width;
        }

        public void AddBaoJingNewValue(float newValue, DateTime dt)
        {
            int i;
            if (this.baoJingNow >= (this.maxNote - 1))
            {
                for (i = 0; i < this.baoJingNow; i++)
                {
                    this.baoJingQuXian[i] = this.baoJingQuXian[i + 1];
                    this.baoJingQuXian[i].X -= this.curveRemove;
                }
                this.baoJingQuXian[this.baoJingNow].Value = newValue;
                this.baoJingQuXian[this.baoJingNow].time = dt;
                this.baoJingQuXian[this.baoJingNow].X = this.picCurveShow.Width;
                this.baoJingQuXian[this.baoJingNow].Y = this.picCurveShow.Height - ((int) ((newValue / (this.yMaxValue - this.yMinValue)) * this.picCurveShow.Height));
            }
            else
            {
                for (i = 0; i < this.baoJingNow; i++)
                {
                    this.baoJingQuXian[i].X -= this.curveRemove;
                }
                this.baoJingQuXian[this.baoJingNow].Value = newValue;
                this.baoJingQuXian[this.baoJingNow].time = DateTime.Now;
                this.baoJingQuXian[this.baoJingNow].X = this.picCurveShow.Width;
                this.baoJingQuXian[this.baoJingNow].Y = this.picCurveShow.Height - ((int) ((newValue / (this.yMaxValue - this.yMinValue)) * this.picCurveShow.Height));
                this.baoJingNow++;
            }
        }

        public void AddPingJunZhiNewValue(float newValue, DateTime dt)
        {
            int i;
            if (this.pingJunZhiNow >= (this.maxNote - 1))
            {
                for (i = 0; i < this.pingJunZhiNow; i++)
                {
                    this.pingJunZhiQuXian[i] = this.pingJunZhiQuXian[i + 1];
                    this.pingJunZhiQuXian[i].X -= this.curveRemove;
                }
                this.pingJunZhiQuXian[this.pingJunZhiNow].Value = newValue;
                this.pingJunZhiQuXian[this.pingJunZhiNow].time = dt;
                this.pingJunZhiQuXian[this.pingJunZhiNow].X = this.picCurveShow.Width;
                this.pingJunZhiQuXian[this.pingJunZhiNow].Y = this.picCurveShow.Height - ((int) ((newValue / (this.yMaxValue - this.yMinValue)) * this.picCurveShow.Height));
            }
            else
            {
                for (i = 0; i < this.pingJunZhiNow; i++)
                {
                    this.pingJunZhiQuXian[i].X -= this.curveRemove;
                }
                this.pingJunZhiQuXian[this.pingJunZhiNow].Value = newValue;
                this.pingJunZhiQuXian[this.pingJunZhiNow].time = DateTime.Now;
                this.pingJunZhiQuXian[this.pingJunZhiNow].X = this.picCurveShow.Width;
                this.pingJunZhiQuXian[this.pingJunZhiNow].Y = this.picCurveShow.Height - ((int) ((newValue / (this.yMaxValue - this.yMinValue)) * this.picCurveShow.Height));
                this.pingJunZhiNow++;
            }
        }

        public void AddZuiDaZhiNewValue(float newValue, DateTime dt)
        {
            int i;
            if (this.zuiDaZhiNow >= (this.maxNote - 1))
            {
                for (i = 0; i < this.zuiDaZhiNow; i++)
                {
                    this.zuiDaZhiQuXian[i] = this.zuiDaZhiQuXian[i + 1];
                    this.zuiDaZhiQuXian[i].X -= this.curveRemove;
                }
                this.zuiDaZhiQuXian[this.zuiDaZhiNow].Value = newValue;
                this.zuiDaZhiQuXian[this.zuiDaZhiNow].time = dt;
                this.zuiDaZhiQuXian[this.zuiDaZhiNow].X = this.picCurveShow.Width;
                this.zuiDaZhiQuXian[this.zuiDaZhiNow].Y = this.picCurveShow.Height - ((int) ((newValue / (this.yMaxValue - this.yMinValue)) * this.picCurveShow.Height));
            }
            else
            {
                for (i = 0; i < this.zuiDaZhiNow; i++)
                {
                    this.zuiDaZhiQuXian[i].X -= this.curveRemove;
                }
                this.zuiDaZhiQuXian[this.zuiDaZhiNow].Value = newValue;
                this.zuiDaZhiQuXian[this.zuiDaZhiNow].time = DateTime.Now;
                this.zuiDaZhiQuXian[this.zuiDaZhiNow].X = this.picCurveShow.Width;
                Random r = new Random();
                this.zuiDaZhiQuXian[this.zuiDaZhiNow].Y = this.picCurveShow.Height - ((int) ((newValue / (this.yMaxValue - this.yMinValue)) * this.picCurveShow.Height));
                this.zuiDaZhiNow++;
            }
        }

        public void AddZuiXiaoZhiNewValue(float newValue, DateTime dt)
        {
            int i;
            if (this.zuiXiaoZhiNow >= (this.maxNote - 1))
            {
                for (i = 0; i < this.zuiXiaoZhiNow; i++)
                {
                    this.zuiXiaoZhiQuXian[i] = this.zuiXiaoZhiQuXian[i + 1];
                    this.zuiXiaoZhiQuXian[i].X -= this.curveRemove;
                }
                this.zuiXiaoZhiQuXian[this.zuiXiaoZhiNow].Value = newValue;
                this.zuiXiaoZhiQuXian[this.zuiXiaoZhiNow].time = dt;
                this.zuiXiaoZhiQuXian[this.zuiXiaoZhiNow].X = this.picCurveShow.Width;
                this.zuiXiaoZhiQuXian[this.zuiXiaoZhiNow].Y = this.picCurveShow.Height - ((int) ((newValue / (this.yMaxValue - this.yMinValue)) * this.picCurveShow.Height));
            }
            else
            {
                for (i = 0; i < this.zuiXiaoZhiNow; i++)
                {
                    this.zuiXiaoZhiQuXian[i].X -= this.curveRemove;
                }
                this.zuiXiaoZhiQuXian[this.zuiXiaoZhiNow].Value = newValue;
                this.zuiXiaoZhiQuXian[this.zuiXiaoZhiNow].time = DateTime.Now;
                this.zuiXiaoZhiQuXian[this.zuiXiaoZhiNow].X = this.picCurveShow.Width;
                this.zuiXiaoZhiQuXian[this.zuiXiaoZhiNow].Y = this.picCurveShow.Height - ((int) ((newValue / (this.yMaxValue - this.yMinValue)) * this.picCurveShow.Height));
                this.zuiXiaoZhiNow++;
            }
        }

        public void ClearCurve()
        {
            this.pingJunZhiNow = 0;
        }

        private void CurveControl_Load(object sender, EventArgs e)
        {
            this.ShowCurve();
        }

        private void CurveControl_Resize(object sender, EventArgs e)
        {
            this.ShowCurve();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.picCurveShow = new PictureBox();
            this.labShowView = new Label();
            this.textBox1 = new TextBox();
            ((ISupportInitialize) this.picCurveShow).BeginInit();
            base.SuspendLayout();
            this.picCurveShow.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.picCurveShow.BackColor = Color.White;
            this.picCurveShow.Cursor = Cursors.Default;
            this.picCurveShow.Location = new Point(0, 3);
            this.picCurveShow.Name = "picCurveShow";
            this.picCurveShow.Size = new Size(0x38c, 0x1aa);
            this.picCurveShow.TabIndex = 0;
            this.picCurveShow.TabStop = false;
            this.picCurveShow.DoubleClick += new EventHandler(this.picCurveShow_DoubleClick);
            this.picCurveShow.MouseLeave += new EventHandler(this.picCurveShow_MouseLeave);
            this.picCurveShow.MouseMove += new MouseEventHandler(this.picCurveShow_MouseMove);
            this.picCurveShow.Resize += new EventHandler(this.picCurveShow_Resize);
            this.labShowView.AutoSize = true;
            this.labShowView.BackColor = Color.Black;
            this.labShowView.ForeColor = Color.White;
            this.labShowView.Location = new Point(0xc0, 0x60);
            this.labShowView.Name = "labShowView";
            this.labShowView.Size = new Size(0x29, 12);
            this.labShowView.TabIndex = 1;
            this.labShowView.Text = "label1";
            this.labShowView.TextAlign = ContentAlignment.MiddleCenter;
            this.labShowView.Visible = false;
            this.textBox1.Location = new Point(0x1c, 0xd1);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x1f0, 0x15);
            this.textBox1.TabIndex = 2;
            this.textBox1.Visible = false;
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.labShowView);
            base.Controls.Add(this.picCurveShow);
            this.Cursor = Cursors.SizeAll;
            base.Name = "CurveControl";
            base.Size = new Size(0x38c, 0x1c1);
            base.Load += new EventHandler(this.CurveControl_Load);
            base.Resize += new EventHandler(this.CurveControl_Resize);
            ((ISupportInitialize) this.picCurveShow).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void JustifyOldSize()
        {
            this.lastTimeSystemWindowWidth = this.picCurveShow.Width;
            this.lastTimeSystemWindowHeight = this.picCurveShow.Height;
        }

        private void picCurveShow_DoubleClick(object sender, EventArgs e)
        {
            if (this.Dock == DockStyle.Fill)
            {
            }
        }

        private void picCurveShow_MouseLeave(object sender, EventArgs e)
        {
            this.labShowView.Visible = false;
            this.ShowCurve();
        }

        private void picCurveShow_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g = this.ShowCurve();
            if (g != null)
            {
                Pen p = new Pen(Color.Red);
                g.DrawLine(p, 0, e.Y, this.picCurveShow.Width, e.Y);
                g.DrawLine(p, e.X, 0, e.X, this.picCurveShow.Height);
                this.picCurveShow.Refresh();
                this.ShowMouseCoordinateMessage(e.X, e.Y);
                this.labShowView.BringToFront();
                this.labShowView.ForeColor = Color.YellowGreen;
                this.labShowView.Refresh();
            }
        }

        private void picCurveShow_Resize(object sender, EventArgs e)
        {
        }

        private Bitmap RefAndRemoveBackground()
        {
            if (this.removeGrid)
            {
                if (this.gridRemoveX >= this.gridWidth)
                {
                    this.gridRemoveX = 1f;
                }
                else
                {
                    this.gridRemoveX += this.curveRemove;
                }
            }
            return this.RefBackground();
        }

        private Bitmap RefBackground()
        {
            Pen baoJingMenXianPen;
            float yHeight;
            if ((this.picCurveShow.Height < 1) || (this.picCurveShow.Width < 1))
            {
                return null;
            }
            Pen gridPen = new Pen(this.gridColor, this.gridPenWidth);
            Pen gridCompartPen = new Pen(this.gridForeColor, this.gridCompart);
            Bitmap bitmap = new Bitmap(this.picCurveShow.Width, this.picCurveShow.Height);
            Graphics backGroundImage = Graphics.FromImage(bitmap);
            backGroundImage.TranslateTransform(this.coordinate, 0f);
            backGroundImage.Clear(this.backGroundColor);
            if (this.showBgGrid)
            {
                float i;
                PointF pointFBegin;
                PointF pointFEnd;
                for (i = this.picCurveShow.Height; i >= 0f; i -= this.gridHeight)
                {
                    pointFBegin = new PointF(0f, i);
                    pointFEnd = new PointF((float) this.picCurveShow.Width, i);
                    backGroundImage.DrawLine(gridPen, pointFBegin, pointFEnd);
                }
                for (i = this.picCurveShow.Width; i >= 0f; i -= this.gridWidth)
                {
                    if ((i - this.gridRemoveX) >= 0f)
                    {
                        pointFBegin = new PointF(i - this.gridRemoveX, 0f);
                        pointFEnd = new PointF(i - this.gridRemoveX, (float) this.picCurveShow.Height);
                        backGroundImage.DrawLine(gridPen, pointFBegin, pointFEnd);
                    }
                }
            }
            backGroundImage.DrawLine(gridCompartPen, 0, 0, 0, this.picCurveShow.Height);
            Font backGroundFont = new Font("Arial", this.gridFontSize);
            float fontHight = backGroundFont.GetHeight();
            SolidBrush brush = new SolidBrush(this.gridForeColor);
            Pen upperAndLowerPen = new Pen(this.yUpperAndLowerColor, this.yUpperAndLowerPenWidth) {
                DashStyle = DashStyle.Dash
            };
            if (this.showBaoJingMenXian)
            {
                baoJingMenXianPen = new Pen(this.baoJingMenXianColor, this.yUpperAndLowerPenWidth);
                yHeight = this.picCurveShow.Height - ((this.baoJingMenXian / (this.yMaxValue - this.yMinValue)) * this.picCurveShow.Height);
                backGroundImage.DrawLine(baoJingMenXianPen, 0f, yHeight, (float) this.picCurveShow.Width, yHeight);
                backGroundImage.DrawString(this.BaoJingMenXianString, backGroundFont, brush, -this.coordinate, yHeight - (fontHight / 2f));
            }
            if (this.showDuanDianMenXian)
            {
                baoJingMenXianPen = new Pen(this.duanDianMenXianColor, this.yUpperAndLowerPenWidth);
                yHeight = this.picCurveShow.Height - ((this.duanDianMenXian / (this.yMaxValue - this.yMinValue)) * this.picCurveShow.Height);
                backGroundImage.DrawLine(baoJingMenXianPen, 0f, yHeight, (float) this.picCurveShow.Width, yHeight);
                backGroundImage.DrawString(this.DuanDianMenXianString, backGroundFont, brush, -this.coordinate, yHeight - (fontHight / 2f));
            }
            if (this.showFuDianMenXian)
            {
                baoJingMenXianPen = new Pen(this.fuDianMenXianColor, this.yUpperAndLowerPenWidth);
                yHeight = this.picCurveShow.Height - ((this.fuDianMenXian / (this.yMaxValue - this.yMinValue)) * this.picCurveShow.Height);
                backGroundImage.DrawLine(baoJingMenXianPen, 0f, yHeight, (float) this.picCurveShow.Width, yHeight);
                backGroundImage.DrawString(this.FuDianMenXianString, backGroundFont, brush, -this.coordinate, yHeight - (fontHight / 2f));
            }
            backGroundImage.DrawString(this.yMaxString, backGroundFont, brush, -this.coordinate, 0f);
            backGroundImage.DrawString(this.yMinString, backGroundFont, brush, -this.coordinate, this.picCurveShow.Height - fontHight);
            brush = new SolidBrush(this.titleColor);
            backGroundImage.DrawString(this.title, backGroundFont, brush, (float) ((this.picCurveShow.Width / 2) - (this.title.Length * this.gridFontSize)), (float) 0f);
            if (this.showTime)
            {
                brush = new SolidBrush(this.showTimeColor);
                backGroundImage.DrawString(DateTime.Now.ToString("T"), backGroundFont, brush, (float) (this.picCurveShow.Width - 0x73), this.picCurveShow.Height - fontHight);
            }
            return bitmap;
        }

        private void RefurbishArray()
        {
            int i;
            for (i = 0; i <= this.zuiDaZhiNow; i++)
            {
                this.zuiDaZhiQuXian[i].X += this.picCurveShow.Width - this.lastTimeSystemWindowWidth;
                this.zuiDaZhiQuXian[i].Y = this.picCurveShow.Height - ((int) ((this.zuiDaZhiQuXian[i].Value / (this.yMaxValue - this.yMinValue)) * this.picCurveShow.Height));
            }
            for (i = 0; i <= this.zuiXiaoZhiNow; i++)
            {
                this.zuiXiaoZhiQuXian[i].X += this.picCurveShow.Width - this.lastTimeSystemWindowWidth;
                this.zuiXiaoZhiQuXian[i].Y = this.picCurveShow.Height - ((int) ((this.zuiXiaoZhiQuXian[i].Value / (this.yMaxValue - this.yMinValue)) * this.picCurveShow.Height));
            }
            for (i = 0; i <= this.pingJunZhiNow; i++)
            {
                this.pingJunZhiQuXian[i].X += this.picCurveShow.Width - this.lastTimeSystemWindowWidth;
                this.pingJunZhiQuXian[i].Y = this.picCurveShow.Height - ((int) ((this.pingJunZhiQuXian[i].Value / (this.yMaxValue - this.yMinValue)) * this.picCurveShow.Height));
            }
            for (i = 0; i <= this.baoJingNow; i++)
            {
                this.baoJingQuXian[i].X += this.picCurveShow.Width - this.lastTimeSystemWindowWidth;
                this.baoJingQuXian[i].Y = this.picCurveShow.Height - ((int) ((this.baoJingQuXian[i].Value / (this.yMaxValue - this.yMinValue)) * this.picCurveShow.Height));
            }
            this.lastTimeSystemWindowHeight = this.picCurveShow.Height;
            this.lastTimeSystemWindowWidth = this.picCurveShow.Width;
        }

        public Graphics ShowCurve()
        {
            Point[] pointsTemp;
            Point[] points;
            int pointI;
            int i;
            if ((this.picCurveShow.Height != this.lastTimeSystemWindowHeight) || (this.picCurveShow.Width != this.lastTimeSystemWindowWidth))
            {
                this.RefurbishArray();
            }
            Bitmap bitmap = this.RefAndRemoveBackground();
            if (bitmap == null)
            {
                return null;
            }
            Graphics graphics = Graphics.FromImage(bitmap);
            if (this.showZuiDaZhiQuXian && (this.zuiDaZhiNow > 1))
            {
                pointsTemp = new Point[this.zuiDaZhiNow];
                pointI = 0;
                for (i = 0; i <= (this.zuiDaZhiNow - 1); i++)
                {
                    if (this.zuiDaZhiQuXian[i].X >= this.coordinate)
                    {
                        pointsTemp[pointI].X = this.zuiDaZhiQuXian[i].X;
                        pointsTemp[pointI].Y = this.zuiDaZhiQuXian[i].Y;
                        pointI++;
                    }
                }
                points = new Point[pointI];
                Array.Copy(pointsTemp, 0, points, 0, pointI);
                graphics.DrawCurve(new Pen(this.zuiDaZhiCurveColor, this.curvePenWidth), points);
            }
            if (this.showZuiXiaoZhiQuXian && (this.zuiXiaoZhiNow > 1))
            {
                pointsTemp = new Point[this.zuiXiaoZhiNow];
                pointI = 0;
                for (i = 0; i <= (this.zuiXiaoZhiNow - 1); i++)
                {
                    if (this.zuiXiaoZhiQuXian[i].X >= this.coordinate)
                    {
                        pointsTemp[pointI].X = this.zuiXiaoZhiQuXian[i].X;
                        pointsTemp[pointI].Y = this.zuiXiaoZhiQuXian[i].Y;
                        pointI++;
                    }
                }
                points = new Point[pointI];
                Array.Copy(pointsTemp, 0, points, 0, pointI);
                graphics.DrawCurve(new Pen(this.zuiXiaoZhiCurveColor, this.curvePenWidth), points);
            }
            if (this.showPingJunZhiQuXian && (this.pingJunZhiNow > 1))
            {
                pointsTemp = new Point[this.pingJunZhiNow];
                pointI = 0;
                for (i = 0; i <= (this.pingJunZhiNow - 1); i++)
                {
                    if (this.pingJunZhiQuXian[i].X >= this.coordinate)
                    {
                        pointsTemp[pointI].X = this.pingJunZhiQuXian[i].X;
                        pointsTemp[pointI].Y = this.pingJunZhiQuXian[i].Y;
                        pointI++;
                    }
                }
                points = new Point[pointI];
                Array.Copy(pointsTemp, 0, points, 0, pointI);
                graphics.DrawCurve(new Pen(this.pingJunZhiCurveColor, this.curvePenWidth), points);
            }
            if (this.showBaoJingQuXian && (this.baoJingNow > 1))
            {
                pointsTemp = new Point[this.baoJingNow];
                pointI = 0;
                for (i = 0; i <= (this.baoJingNow - 1); i++)
                {
                    if (this.baoJingQuXian[i].X >= this.coordinate)
                    {
                        pointsTemp[pointI].X = this.baoJingQuXian[i].X;
                        pointsTemp[pointI].Y = this.baoJingQuXian[i].Y;
                        pointI++;
                    }
                }
                points = new Point[pointI];
                Array.Copy(pointsTemp, 0, points, 0, pointI);
                graphics.DrawCurve(new Pen(this.baoJingCurveColor, this.curvePenWidth), points);
            }
            this.picCurveShow.Image = bitmap;
            return graphics;
        }

        public void ShowCurve(float[] Value, DateTime[] time, byte type)
        {
            int i;
            if (type == 0)
            {
                this.zuiDaZhiNow = 0;
                for (i = 0; i < Value.Length; i++)
                {
                    this.AddZuiDaZhiNewValue(Value[i], time[i]);
                }
            }
            else if (type == 1)
            {
                for (i = 0; i < Value.Length; i++)
                {
                    this.AddZuiXiaoZhiNewValue(Value[i], time[i]);
                }
            }
            else if (type == 2)
            {
                for (i = 0; i < Value.Length; i++)
                {
                    this.AddPingJunZhiNewValue(Value[i], time[i]);
                }
            }
            else if (type == 3)
            {
                for (i = 0; i < Value.Length; i++)
                {
                    this.AddBaoJingNewValue(Value[i], time[i]);
                }
            }
            this.ShowCurve();
        }

        private void ShowMouseCoordinateMessage(int X, int Y)
        {
            float x = X;
            float y = Y;
            if (x >= this.coordinate)
            {
                this.labShowView.Text = "";
                if (this.showPingJunZhiQuXian)
                {
                    foreach (RealTimeCoordinatesValue valueTemp in this.pingJunZhiQuXian)
                    {
                        if ((valueTemp.X <= (x + this.xYPrecision)) && (valueTemp.X >= (x - this.xYPrecision)))
                        {
                            string theValue;
                            if (valueTemp.Value < 0f)
                            {
                                theValue = "0";
                            }
                            else
                            {
                                theValue = valueTemp.Value.ToString();
                            }
                            string Reflector0004 = this.labShowView.Text;
                            this.labShowView.Text = Reflector0004 + "时间" + valueTemp.time.ToString("T") + "   测值" + theValue;
                            break;
                        }
                    }
                }
                if (this.labShowView.Text != "")
                {
                    int labX;
                    int labY;
                    if (Y <= this.labShowView.Height)
                    {
                        labY = (Y + this.labShowView.Height) + 5;
                    }
                    else
                    {
                        labY = Y - this.labShowView.Height;
                    }
                    if (X >= (this.picCurveShow.Width - this.labShowView.Width))
                    {
                        labX = X - this.labShowView.Width;
                    }
                    else
                    {
                        labX = X + 5;
                    }
                    this.labShowView.Top = labY;
                    this.labShowView.Left = labX;
                    this.labShowView.BringToFront();
                    this.labShowView.Visible = true;
                    return;
                }
            }
            this.labShowView.Visible = false;
        }

        public Color BaoJingCurveColor
        {
            get
            {
                return this.baoJingCurveColor;
            }
            set
            {
                this.baoJingCurveColor = value;
            }
        }

        public float BaoJingMenXian
        {
            get
            {
                return this.baoJingMenXian;
            }
            set
            {
                this.baoJingMenXian = value;
            }
        }

        public Color BaoJingMenXianColor
        {
            get
            {
                return this.baoJingMenXianColor;
            }
            set
            {
                this.baoJingMenXianColor = value;
            }
        }

        public string BaoJingMenXianString
        {
            get
            {
                return this.yBaoJingMenXianString;
            }
            set
            {
                this.yBaoJingMenXianString = value;
            }
        }

        public float CarveCoordinate
        {
            get
            {
                return this.coordinate;
            }
            set
            {
                this.coordinate = value;
            }
        }

        public float CarveCurvePenWidth
        {
            get
            {
                return this.curvePenWidth;
            }
            set
            {
                this.curvePenWidth = value;
            }
        }

        public int CarveCurveRemove
        {
            get
            {
                return this.curveRemove;
            }
            set
            {
                this.curveRemove = value;
            }
        }

        public Color CarveGridColor
        {
            get
            {
                return this.gridColor;
            }
            set
            {
                this.gridColor = value;
            }
        }

        public float CarveGridCompart
        {
            get
            {
                return this.gridCompart;
            }
            set
            {
                this.gridCompart = value;
            }
        }

        public float CarveGridFontSize
        {
            get
            {
                return this.gridFontSize;
            }
            set
            {
                this.gridFontSize = value;
            }
        }

        public Color CarveGridForeColor
        {
            get
            {
                return this.gridForeColor;
            }
            set
            {
                this.gridForeColor = value;
            }
        }

        public float CarveGridHeight
        {
            get
            {
                return this.gridHeight;
            }
            set
            {
                this.gridHeight = value;
            }
        }

        public float CarveGridPenWidth
        {
            get
            {
                return this.gridPenWidth;
            }
            set
            {
                this.gridPenWidth = value;
            }
        }

        public float CarveGridWidth
        {
            get
            {
                return this.gridWidth;
            }
            set
            {
                this.gridWidth = value;
            }
        }

        public int CarveMaxNote
        {
            get
            {
                return this.maxNote;
            }
            set
            {
                this.maxNote = value;
            }
        }

        public Color CarveRectangleColor
        {
            get
            {
                return this.rectangleColor;
            }
            set
            {
                this.rectangleColor = value;
            }
        }

        public float CarveRectangleWidth
        {
            get
            {
                return this.rectangleWidth;
            }
            set
            {
                this.rectangleWidth = value;
            }
        }

        public bool CarveRemoveGrid
        {
            get
            {
                return this.removeGrid;
            }
            set
            {
                this.removeGrid = value;
            }
        }

        public bool CarveShowTime
        {
            get
            {
                return this.showTime;
            }
            set
            {
                this.showTime = value;
            }
        }

        public Color CarveShowTimeColor
        {
            get
            {
                return this.showTimeColor;
            }
            set
            {
                this.showTimeColor = value;
            }
        }

        public string CarveTitle
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        public Color CarveTitleColor
        {
            get
            {
                return this.titleColor;
            }
            set
            {
                this.titleColor = value;
            }
        }

        public float CarveXYPrecision
        {
            get
            {
                return this.xYPrecision;
            }
            set
            {
                this.xYPrecision = value;
            }
        }

        public string CarveYMaxString
        {
            get
            {
                return this.yMaxString;
            }
            set
            {
                this.yMaxString = value;
            }
        }

        public float CarveYMaxValue
        {
            get
            {
                return this.yMaxValue;
            }
            set
            {
                this.yMaxValue = value;
            }
        }

        public string CarveYMinString
        {
            get
            {
                return this.yMinString;
            }
            set
            {
                this.yMinString = value;
            }
        }

        public float CarveYMinValue
        {
            get
            {
                return this.yMinValue;
            }
            set
            {
                this.yMinValue = value;
            }
        }

        public Color CarveYUpperAndLowerColor
        {
            get
            {
                return this.yUpperAndLowerColor;
            }
            set
            {
                this.yUpperAndLowerColor = value;
            }
        }

        public float CarveYUpperAndLowerPenWidth
        {
            get
            {
                return this.yUpperAndLowerPenWidth;
            }
            set
            {
                this.yUpperAndLowerPenWidth = value;
            }
        }

        public Color CurveBackGroundColor
        {
            get
            {
                return this.backGroundColor;
            }
            set
            {
                this.backGroundColor = value;
            }
        }

        public float DuanDianMenXian
        {
            get
            {
                return this.duanDianMenXian;
            }
            set
            {
                this.duanDianMenXian = value;
            }
        }

        public Color DuanDianMenXianColor
        {
            get
            {
                return this.duanDianMenXianColor;
            }
            set
            {
                this.duanDianMenXianColor = value;
            }
        }

        public string DuanDianMenXianString
        {
            get
            {
                return this.yDuanDianMenXianString;
            }
            set
            {
                this.yDuanDianMenXianString = value;
            }
        }

        public float FuanDianMenXian
        {
            get
            {
                return this.fuDianMenXian;
            }
            set
            {
                this.fuDianMenXian = value;
            }
        }

        public Color FuDianMenXianColor
        {
            get
            {
                return this.fuDianMenXianColor;
            }
            set
            {
                this.fuDianMenXianColor = value;
            }
        }

        public string FuDianMenXianString
        {
            get
            {
                return this.yFuDianMenXianString;
            }
            set
            {
                this.yFuDianMenXianString = value;
            }
        }

        public Color PingJunZhiCurveColor
        {
            get
            {
                return this.pingJunZhiCurveColor;
            }
            set
            {
                this.pingJunZhiCurveColor = value;
            }
        }

        public bool ShowBaoJingMenXian
        {
            get
            {
                return this.showBaoJingMenXian;
            }
            set
            {
                this.showBaoJingMenXian = value;
                this.ShowCurve();
            }
        }

        public bool ShowBaoJingQuXian
        {
            get
            {
                return this.showBaoJingQuXian;
            }
            set
            {
                this.showBaoJingQuXian = value;
                this.ShowCurve();
            }
        }

        public bool ShowBgGrid
        {
            get
            {
                return this.showBgGrid;
            }
            set
            {
                this.showBgGrid = value;
                this.ShowCurve();
            }
        }

        public bool ShowDuanDianMenXian
        {
            get
            {
                return this.showDuanDianMenXian;
            }
            set
            {
                this.showDuanDianMenXian = value;
                this.ShowCurve();
            }
        }

        public bool ShowFuDianMenXian
        {
            get
            {
                return this.showFuDianMenXian;
            }
            set
            {
                this.showFuDianMenXian = value;
                this.ShowCurve();
            }
        }

        public bool ShowPingJunZhiQuXian
        {
            get
            {
                return this.showPingJunZhiQuXian;
            }
            set
            {
                this.showPingJunZhiQuXian = value;
                this.ShowCurve();
            }
        }

        public bool ShowZuiDaZhiQuXian
        {
            get
            {
                return this.showZuiDaZhiQuXian;
            }
            set
            {
                this.showZuiDaZhiQuXian = value;
                this.ShowCurve();
            }
        }

        public bool ShowZuiXiaoZhiQuXian
        {
            get
            {
                return this.showZuiXiaoZhiQuXian;
            }
            set
            {
                this.showZuiXiaoZhiQuXian = value;
                this.ShowCurve();
            }
        }

        public Color ZuiDaZhiCurveColor
        {
            get
            {
                return this.zuiDaZhiCurveColor;
            }
            set
            {
                this.zuiDaZhiCurveColor = value;
            }
        }

        public Color ZuiXiaoZhiCurveColor
        {
            get
            {
                return this.zuiXiaoZhiCurveColor;
            }
            set
            {
                this.zuiXiaoZhiCurveColor = value;
            }
        }
    }
}

