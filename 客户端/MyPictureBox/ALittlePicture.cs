namespace MyPictureBox
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Threading;
    using System.Windows.Forms;

    public class ALittlePicture : PictureBox
    {
        private int angle;
        private string ceDianBianHao;
        private byte ceZhi;
        private ControlMoveResize cmr;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private Image currentImage;
        private string danWei;
        private ToolStripMenuItem deleteMenuItem1;
        private ToolStripMenuItem Down;
        private byte fenZhanHao;
        private FullPicture fullPicture;
        private string imagePath;
        private string info;
        private bool isEdit;
        private ToolStripMenuItem Left;
        private ToolStripMenuItem LeftDown;
        private ToolStripMenuItem LeftUp;
        private PointF offsetFromCenterImg;
        private Image oneImage;
        private ToolStripMenuItem Right;
        private ToolStripMenuItem RightDown;
        private ToolStripMenuItem RightUp;
        private Label showInfoLabel;
        private Color theBackColor;
        private Color theColor;
        private Image twoImage;
        private int type;
        private ToolStripMenuItem Up;
        private Image zeroImage;
        private ToolStripMenuItem 设置背景颜色;
        private ToolStripMenuItem 选择方向;
        private ToolStripMenuItem 选择颜色;

        public ALittlePicture(PictureBox parent, FullPicture fullPicture, string info, int type, string ceDianBianHao, Label showInfoLabel, string danWei, bool IsEdit, int angle)
        {
            ThreadStart ts;
            this.components = null;
            this.InitializeComponent();
            this.imagePath = "";
            this.angle = angle;
            this.isEdit = IsEdit;
            this.fullPicture = fullPicture;
            this.type = type;
            this.info = info;
            this.ceDianBianHao = ceDianBianHao;
            this.showInfoLabel = showInfoLabel;
            this.fenZhanHao = Convert.ToByte(this.ceDianBianHao.Substring(0, 2));
            this.danWei = danWei;
            this.offsetFromCenterImg = new PointF();
            this.offsetFromCenterImg.X = this.offsetFromCenterImg.Y = 0f;
            base.SizeChanged += new EventHandler(this.ALittlePicture_SizeChanged);
            this.ContextMenuStrip = this.contextMenuStrip1;
            base.SizeMode = PictureBoxSizeMode.StretchImage;
            this.theColor = Color.Green;
            this.angle = angle;
            this.cmr = new ControlMoveResize(this, parent, 0, IsEdit, this.type);
            base.Parent = parent;
            base.BringToFront();
            if (this.type == 2)
            {
                ts = new ThreadStart(this.DrawJianTouForShanShuo);
                Thread t = new Thread(ts) {
                    IsBackground = true
                };
                t.Start();
            }
            else if (this.type == 1)
            {
                ts = new ThreadStart(this.LiuShui);
                new Thread(ts) { IsBackground = true }.Start();
            }
        }

        private void ALittlePicture_SizeChanged(object sender, EventArgs e)
        {
            this.calculateOffset();
        }

        public void calculateOffset()
        {
            PointF p = this.fullPicture.getImageCenter();
            PointF q = (PointF) base.Location;
            float zoom = this.fullPicture.getZoom();
            this.offsetFromCenterImg.X = p.X - (((q.X + q.X) + base.Width) / 2f);
            this.offsetFromCenterImg.Y = p.Y - (((q.Y + q.Y) + base.Height) / 2f);
            this.offsetFromCenterImg.X /= zoom;
            this.offsetFromCenterImg.Y /= zoom;
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "删除")
            {
                this.fullPicture.deleteAALittlePicture(this);
            }
            else if (e.ClickedItem.Text == "向左旋转")
            {
                string tempAngle = this.contextMenuStrip1.Items[2].Text;
                bool formatOK = true;
                if (tempAngle.Length == 0)
                {
                    formatOK = false;
                }
                for (int i = 0; i < tempAngle.Length; i++)
                {
                    if ((tempAngle[i] > '9') || (tempAngle[i] < '0'))
                    {
                        formatOK = false;
                        break;
                    }
                }
                if (formatOK)
                {
                    this.angle = Convert.ToInt32(tempAngle);
                    base.Image = this.RotateImg(this.currentImage, this.angle);
                }
                else
                {
                    MessageBox.Show("输入角度错误");
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (!this.isEdit)
            {
                e.Cancel = true;
            }
            if ((this.type == 1) || (this.type == 2))
            {
                this.contextMenuStrip1.Items[1].Visible = this.contextMenuStrip1.Items[3].Visible = true;
                this.contextMenuStrip1.Items[2].Visible = false;
            }
            else
            {
                this.contextMenuStrip1.Items[1].Visible = this.contextMenuStrip1.Items[2].Visible = this.contextMenuStrip1.Items[3].Visible = false;
            }
        }

        internal void Dispatch(FenZhanRTdata pd)
        {
            if (pd == null)
            {
                this.ceZhi = 0;
                base.Image = this.zeroImage;
            }
            else if ((this.type < 5) && (this.fenZhanHao == pd.fenZhanHao))
            {
                byte num = Convert.ToByte(this.ceDianBianHao.Substring(3, 2));
                if (this.ceDianBianHao[2] == 'C')
                {
                    this.ceZhi = pd.kongZhiLiangZhuangTai[num];
                }
                else
                {
                    this.ceZhi = (byte) pd.realValue[num];
                }
                if ((this.type != 1) && (this.type != 2))
                {
                    if (this.ceZhi == 0)
                    {
                        if (base.Image != this.zeroImage)
                        {
                            base.Image = this.zeroImage;
                        }
                    }
                    else if (this.ceZhi == 1)
                    {
                        if (base.Image != this.oneImage)
                        {
                            base.Image = this.oneImage;
                        }
                    }
                    else if ((this.ceZhi == 2) && (base.Image != this.twoImage))
                    {
                        base.Image = this.twoImage;
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Down_Click(object sender, EventArgs e)
        {
            this.angle = 6;
        }

        private void DrawJianTouForShanShuo()
        {
            int mm = 0;
            while (true)
            {
                if ((base.Height < 1) || (base.Width < 1))
                {
                    Thread.Sleep(0x3e8);
                }
                else
                {
                    this.BackColor = this.theBackColor;
                    Bitmap bitmap = new Bitmap(base.Width, base.Height);
                    Graphics backGroundImage = Graphics.FromImage(bitmap);
                    Point p1 = new Point();
                    Point p2 = new Point();
                    Point p3 = new Point();
                    Point p4 = new Point();
                    if (this.angle == 1)
                    {
                        p1.X = 0;
                        p1.Y = base.Height;
                        p2.X = base.Width;
                        p2.Y = 0;
                        p3.X = base.Width / 2;
                        p3.Y = base.Height / 4;
                        p4.X = (3 * base.Width) / 4;
                        p4.Y = base.Height / 2;
                    }
                    else if (this.angle == 2)
                    {
                        p1.X = 0;
                        p1.Y = 0;
                        p2.X = base.Width;
                        p2.Y = base.Height;
                        p3.X = (3 * base.Width) / 4;
                        p3.Y = base.Height / 2;
                        p4.X = base.Width / 2;
                        p4.Y = (3 * base.Height) / 4;
                    }
                    else if (this.angle == 3)
                    {
                        p1.X = base.Width;
                        p1.Y = 0;
                        p2.X = 0;
                        p2.Y = base.Height;
                        p3.X = base.Width / 4;
                        p3.Y = base.Height / 2;
                        p4.X = base.Width / 2;
                        p4.Y = (3 * base.Height) / 4;
                    }
                    else if (this.angle == 4)
                    {
                        p1.X = base.Width;
                        p1.Y = base.Height;
                        p2.X = 0;
                        p2.Y = 0;
                        p3.X = base.Width / 2;
                        p3.Y = base.Height / 4;
                        p4.X = base.Width / 4;
                        p4.Y = base.Height / 2;
                    }
                    else if (this.angle == 5)
                    {
                        p1.X = base.Width / 2;
                        p1.Y = base.Height;
                        p2.X = base.Width / 2;
                        p2.Y = 0;
                        p3.X = p2.X - 5;
                        p3.Y = p2.Y + 7;
                        p4.X = p2.X + 5;
                        p4.Y = p2.Y + 7;
                    }
                    else if (this.angle == 6)
                    {
                        p1.X = base.Width / 2;
                        p1.Y = 0;
                        p2.X = base.Width / 2;
                        p2.Y = base.Height;
                        p3.X = p2.X - 5;
                        p3.Y = p2.Y - 7;
                        p4.X = p2.X + 5;
                        p4.Y = p2.Y - 7;
                    }
                    else if (this.angle == 7)
                    {
                        p1.X = base.Width;
                        p1.Y = base.Height / 2;
                        p2.X = 0;
                        p2.Y = base.Height / 2;
                        p3.X = p2.X + 7;
                        p3.Y = p2.Y - 5;
                        p4.X = p2.X + 7;
                        p4.Y = p2.Y + 5;
                    }
                    else if (this.angle == 8)
                    {
                        p1.X = 0;
                        p1.Y = base.Height / 2;
                        p2.X = base.Width;
                        p2.Y = base.Height / 2;
                        p3.X = p2.X - 7;
                        p3.Y = p2.Y - 5;
                        p4.X = p2.X - 7;
                        p4.Y = p2.Y + 5;
                    }
                    if ((mm % 2) == 0)
                    {
                        this.theColor = Color.Green;
                    }
                    else
                    {
                        this.theColor = Color.Blue;
                    }
                    Pen pen = new Pen(this.theColor, 3f);
                    Pen pen1 = new Pen(this.theColor, 2f);
                    backGroundImage.DrawLine(pen, p1, p2);
                    backGroundImage.DrawLine(pen1, p2, p3);
                    backGroundImage.DrawLine(pen1, p2, p4);
                    base.Image = bitmap;
                    mm++;
                    Thread.Sleep(0x3e8);
                }
            }
        }

        public Point getNewLocation()
        {
            PointF p = this.fullPicture.getImageCenter();
            float zoom = this.fullPicture.getZoom();
            float xOffset = (int) (this.offsetFromCenterImg.X * zoom);
            float yOffset = (int) (this.offsetFromCenterImg.Y * zoom);
            xOffset = p.X - xOffset;
            yOffset = p.Y - yOffset;
            return new Point { X = (int) (((xOffset * 2f) - base.Width) / 2f), Y = (int) (((yOffset * 2f) - base.Height) / 2f) };
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.deleteMenuItem1 = new ToolStripMenuItem();
            this.选择方向 = new ToolStripMenuItem();
            this.RightUp = new ToolStripMenuItem();
            this.RightDown = new ToolStripMenuItem();
            this.LeftDown = new ToolStripMenuItem();
            this.LeftUp = new ToolStripMenuItem();
            this.Up = new ToolStripMenuItem();
            this.Down = new ToolStripMenuItem();
            this.Left = new ToolStripMenuItem();
            this.Right = new ToolStripMenuItem();
            this.选择颜色 = new ToolStripMenuItem();
            this.设置背景颜色 = new ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            ((ISupportInitialize) this).BeginInit();
            base.SuspendLayout();
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.deleteMenuItem1, this.选择方向, this.选择颜色, this.设置背景颜色 });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x99, 0x72);
            this.contextMenuStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
            this.deleteMenuItem1.Name = "deleteMenuItem1";
            this.deleteMenuItem1.Size = new Size(0x98, 0x16);
            this.deleteMenuItem1.Text = "删除";
            this.选择方向.DropDownItems.AddRange(new ToolStripItem[] { this.RightUp, this.RightDown, this.LeftDown, this.LeftUp, this.Up, this.Down, this.Left, this.Right });
            this.选择方向.Name = "选择方向";
            this.选择方向.Size = new Size(0x98, 0x16);
            this.选择方向.Text = "选择方向";
            this.RightUp.Name = "RightUp";
            this.RightUp.Size = new Size(0x98, 0x16);
            this.RightUp.Text = "↗";
            this.RightUp.Click += new EventHandler(this.RightUp_Click);
            this.RightDown.Name = "RightDown";
            this.RightDown.Size = new Size(0x98, 0x16);
            this.RightDown.Text = "↘";
            this.RightDown.Click += new EventHandler(this.RightDown_Click);
            this.LeftDown.Name = "LeftDown";
            this.LeftDown.Size = new Size(0x98, 0x16);
            this.LeftDown.Text = "↙";
            this.LeftDown.Click += new EventHandler(this.LeftDown_Click);
            this.LeftUp.Name = "LeftUp";
            this.LeftUp.Size = new Size(0x98, 0x16);
            this.LeftUp.Text = "↖";
            this.LeftUp.Click += new EventHandler(this.LeftUp_Click);
            this.Up.Name = "Up";
            this.Up.Size = new Size(0x98, 0x16);
            this.Up.Text = "↑";
            this.Up.Click += new EventHandler(this.Up_Click);
            this.Down.Name = "Down";
            this.Down.Size = new Size(0x98, 0x16);
            this.Down.Text = "↓";
            this.Down.Click += new EventHandler(this.Down_Click);
            this.Left.Name = "Left";
            this.Left.Size = new Size(0x98, 0x16);
            this.Left.Text = "←";
            this.Left.Click += new EventHandler(this.Left_Click);
            this.Right.Name = "Right";
            this.Right.Size = new Size(0x98, 0x16);
            this.Right.Text = "→";
            this.Right.Click += new EventHandler(this.Right_Click);
            this.选择颜色.Name = "选择颜色";
            this.选择颜色.Size = new Size(0x98, 0x16);
            this.选择颜色.Text = "选择颜色";
            this.选择颜色.Click += new EventHandler(this.选择颜色_Click);
            this.设置背景颜色.Name = "设置背景颜色";
            this.设置背景颜色.Size = new Size(0x98, 0x16);
            this.设置背景颜色.Text = "设置背景颜色";
            this.设置背景颜色.Click += new EventHandler(this.设置背景颜色_Click);
            this.BackColor = Color.Transparent;
            this.contextMenuStrip1.ResumeLayout(false);
            ((ISupportInitialize) this).EndInit();
            base.ResumeLayout(false);
        }

        private void Left_Click(object sender, EventArgs e)
        {
            this.angle = 7;
        }

        private void LeftDown_Click(object sender, EventArgs e)
        {
            this.angle = 3;
        }

        private void LeftUp_Click(object sender, EventArgs e)
        {
            this.angle = 4;
        }

        private void LiuShui()
        {
            int currentIndex = 0;
            while (true)
            {
                if ((base.Height < 1) || (base.Width < 1))
                {
                    Thread.Sleep(0x3e8);
                }
                else
                {
                    this.BackColor = this.theBackColor;
                    Bitmap bitmap = new Bitmap(base.Width, base.Height);
                    Graphics backGroundImage = Graphics.FromImage(bitmap);
                    Point p1 = new Point();
                    Point p2 = new Point();
                    Point p3 = new Point();
                    if (this.angle == 1)
                    {
                        p1.X = 0;
                        p1.Y = base.Height;
                        p2.X = base.Width;
                        p2.Y = 0;
                        p3.X = ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10) + Math.Min(p1.X, p2.X);
                        int introduced9 = Math.Max(p1.Y, p2.Y);
                        p3.Y = introduced9 - ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10);
                    }
                    else if (this.angle == 2)
                    {
                        p1.X = 0;
                        p1.Y = 0;
                        p2.X = base.Width;
                        p2.Y = base.Height;
                        p3.X = ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10) + Math.Min(p1.X, p2.X);
                        p3.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10) + Math.Min(p1.Y, p2.Y);
                    }
                    else if (this.angle == 3)
                    {
                        p1.X = base.Width;
                        p1.Y = 0;
                        p2.X = 0;
                        p2.Y = base.Height;
                        int introduced10 = Math.Max(p1.X, p2.X);
                        p3.X = introduced10 - ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10);
                        p3.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10) + Math.Min(p1.Y, p2.Y);
                    }
                    else if (this.angle == 4)
                    {
                        p1.X = base.Width;
                        p1.Y = base.Height;
                        p2.X = 0;
                        p2.Y = 0;
                        int introduced11 = Math.Max(p1.X, p2.X);
                        p3.X = introduced11 - ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10);
                        int introduced12 = Math.Max(p1.Y, p2.Y);
                        p3.Y = introduced12 - ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10);
                    }
                    else if (this.angle == 5)
                    {
                        p1.X = base.Width / 2;
                        p1.Y = base.Height;
                        p2.X = base.Width / 2;
                        p2.Y = 0;
                        int introduced13 = Math.Max(p1.X, p2.X);
                        p3.X = introduced13 - ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10);
                        int introduced14 = Math.Max(p1.Y, p2.Y);
                        p3.Y = introduced14 - ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10);
                    }
                    else if (this.angle == 6)
                    {
                        p1.X = base.Width / 2;
                        p1.Y = 0;
                        p2.X = base.Width / 2;
                        p2.Y = base.Height;
                        int introduced15 = Math.Max(p1.X, p2.X);
                        p3.X = introduced15 - ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10);
                        p3.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10) + Math.Min(p1.Y, p2.Y);
                    }
                    else if (this.angle == 7)
                    {
                        p1.X = base.Width;
                        p1.Y = base.Height / 2;
                        p2.X = 0;
                        p2.Y = base.Height / 2;
                        int introduced16 = Math.Max(p1.X, p2.X);
                        p3.X = introduced16 - ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10);
                        p3.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10) + Math.Min(p1.Y, p2.Y);
                    }
                    else if (this.angle == 8)
                    {
                        p1.X = 0;
                        p1.Y = base.Height / 2;
                        p2.X = base.Width;
                        p2.Y = base.Height / 2;
                        p3.X = ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10) + Math.Min(p1.X, p2.X);
                        p3.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10) + Math.Min(p1.Y, p2.Y);
                    }
                    Pen pen = new Pen(Color.Red, 3f);
                    Pen pen1 = new Pen(Color.Green, 3f);
                    backGroundImage.DrawLine(pen, p1, p3);
                    backGroundImage.DrawLine(pen1, p3, p2);
                    base.Image = bitmap;
                    currentIndex++;
                    if (currentIndex > 10)
                    {
                        currentIndex = 0;
                    }
                    Thread.Sleep(0x3e8);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void Right_Click(object sender, EventArgs e)
        {
            this.angle = 8;
        }

        private void RightDown_Click(object sender, EventArgs e)
        {
            this.angle = 2;
        }

        private void RightUp_Click(object sender, EventArgs e)
        {
            this.angle = 1;
        }

        public Image RotateImg(Image bb, int angle)
        {
            Image b = (Image) bb.Clone();
            angle = angle % 360;
            double radian = (angle * 3.1415926535897931) / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            int w = b.Width;
            int h = b.Height;
            int W = (int) Math.Max(Math.Abs((double) ((w * cos) - (h * sin))), Math.Abs((double) ((w * cos) + (h * sin))));
            int H = (int) Math.Max(Math.Abs((double) ((w * sin) - (h * cos))), Math.Abs((double) ((w * sin) + (h * cos))));
            Bitmap dsImage = new Bitmap(W, H);
            Graphics g = Graphics.FromImage(dsImage);
            g.InterpolationMode = InterpolationMode.Bilinear;
            g.SmoothingMode = SmoothingMode.HighQuality;
            Point Offset = new Point((W - w) / 2, (H - h) / 2);
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2));
            g.TranslateTransform((float) center.X, (float) center.Y);
            g.RotateTransform((float) (360 - angle));
            g.TranslateTransform((float) -center.X, (float) -center.Y);
            g.DrawImage(b, rect);
            g.ResetTransform();
            g.Save();
            g.Dispose();
            b.Dispose();
            return dsImage;
        }

        public void ShowInfo(bool show, int X, int Y)
        {
            if (this.type != 5)
            {
                if (show)
                {
                    int labX;
                    int labY;
                    string s = "";
                    if (this.ceDianBianHao[2] == 'D')
                    {
                        if (GlobalParams.AllCeDianList.allcedianlist.ContainsKey(this.ceDianBianHao))
                        {
                            string lingTaiMingCheng;
                            CeDian cedian = GlobalParams.AllCeDianList.allcedianlist[this.ceDianBianHao];
                            if (this.ceZhi == 0)
                            {
                                lingTaiMingCheng = cedian.KaiGuanLiang.LingTai;
                                s = this.ceDianBianHao + "测值：" + lingTaiMingCheng;
                            }
                            else if (this.ceZhi == 1)
                            {
                                lingTaiMingCheng = cedian.KaiGuanLiang.YiTai;
                                s = this.ceDianBianHao + "测值：" + lingTaiMingCheng;
                            }
                            else if (this.ceZhi == 2)
                            {
                                lingTaiMingCheng = cedian.KaiGuanLiang.ErTai;
                                s = this.ceDianBianHao + "测值：" + lingTaiMingCheng;
                            }
                        }
                        else
                        {
                            this.showInfoLabel.BackColor = Color.Blue;
                            s = "未找到测点编号为" + this.ceDianBianHao + "的测点";
                        }
                    }
                    else
                    {
                        s = string.Concat(new object[] { this.ceDianBianHao, "测值：", this.ceZhi, this.danWei });
                    }
                    this.showInfoLabel.Text = s;
                    Point p = this.getNewLocation();
                    X = p.X;
                    Y = p.Y;
                    if (Y <= this.showInfoLabel.Height)
                    {
                        labY = (Y + this.showInfoLabel.Height) + 5;
                    }
                    else
                    {
                        labY = Y - this.showInfoLabel.Height;
                    }
                    if (X >= (base.Parent.Width - this.showInfoLabel.Width))
                    {
                        labX = X - this.showInfoLabel.Width;
                    }
                    else
                    {
                        labX = X + 5;
                    }
                    this.showInfoLabel.Top = labY;
                    this.showInfoLabel.Left = labX;
                    this.showInfoLabel.BringToFront();
                    this.showInfoLabel.Visible = true;
                }
                else
                {
                    this.showInfoLabel.BackColor = SystemColors.Control;
                    this.showInfoLabel.Visible = false;
                }
            }
        }

        public bool TuYuanExist(int type, string info)
        {
            string info1 = this.info;
            string info2 = info;
            if (info1.Length > 5)
            {
                info1 = info1.Substring(0, 5);
            }
            if (info2.Length > 5)
            {
                info2 = info2.Substring(0, 5);
            }
            return ((this.type == type) && (info1 == info2));
        }

        public bool TuYuanExist(int type, int info, string ceDianBianHao)
        {
            return ((this.type == type) && (this.ceDianBianHao == ceDianBianHao));
        }

        private void Up_Click(object sender, EventArgs e)
        {
            this.angle = 5;
        }

        public void UpdateImage(byte state)
        {
            if (this.type == 5)
            {
                base.Image = this.zeroImage;
            }
            else if ((this.type == 1) || (this.type == 4))
            {
                if (state == 0)
                {
                    base.Image = this.RotateImg(this.zeroImage, this.angle);
                    this.currentImage = this.zeroImage;
                }
                else if (state == 1)
                {
                    base.Image = this.RotateImg(this.oneImage, this.angle);
                    this.currentImage = this.oneImage;
                }
                else if (state == 2)
                {
                    base.Image = this.RotateImg(this.twoImage, this.angle);
                    this.currentImage = this.twoImage;
                }
            }
            else if (state == 0)
            {
                base.Image = this.zeroImage;
            }
            else if (state == 1)
            {
                base.Image = this.oneImage;
            }
            else if (state == 2)
            {
                base.Image = this.twoImage;
            }
        }

        private void 设置背景颜色_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() != DialogResult.Cancel)
            {
                this.theBackColor = cd.Color;
            }
        }

        private void 选择颜色_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() != DialogResult.Cancel)
            {
                this.theColor = cd.Color;
            }
        }

        public int Angle
        {
            get
            {
                return this.angle;
            }
            set
            {
                this.angle = value;
            }
        }

        public string CeDianBianHao
        {
            get
            {
                return this.ceDianBianHao;
            }
        }

        public int IBGColor
        {
            get
            {
                return this.theBackColor.ToArgb();
            }
            set
            {
                this.theBackColor = Color.FromArgb(value);
            }
        }

        public int IJianTouColor
        {
            get
            {
                return this.theColor.ToArgb();
            }
            set
            {
                this.theColor = Color.FromArgb(value);
            }
        }

        public string ImagePath
        {
            get
            {
                return this.imagePath;
            }
            set
            {
                this.imagePath = value;
            }
        }

        public string Info
        {
            get
            {
                return this.info;
            }
            set
            {
                this.info = value;
            }
        }

        public string OffsetFromCenterImg
        {
            get
            {
                return (this.offsetFromCenterImg.X.ToString() + ";" + this.offsetFromCenterImg.Y.ToString());
            }
            set
            {
                int m = value.LastIndexOf(';');
                this.offsetFromCenterImg.X = Convert.ToSingle(value.Substring(0, m));
                this.offsetFromCenterImg.Y = Convert.ToSingle(value.Substring(m + 1));
            }
        }

        public Image OneImage
        {
            get
            {
                return this.oneImage;
            }
            set
            {
                this.oneImage = value;
            }
        }

        public Image TwoImage
        {
            get
            {
                return this.twoImage;
            }
            set
            {
                this.twoImage = value;
            }
        }

        public int Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        public Image ZeroImage
        {
            get
            {
                return this.zeroImage;
            }
            set
            {
                this.zeroImage = value;
            }
        }
    }
}

