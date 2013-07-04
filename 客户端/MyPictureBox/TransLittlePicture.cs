namespace MyPictureBox
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class TransLittlePicture : TransPictureBox
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
        private int fatherNeedRefreash;
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
        private int refreashTimes;
        private ToolStripMenuItem Right;
        private ToolStripMenuItem RightDown;
        private ToolStripMenuItem RightUp;
        private Label showInfoLabel;
        private Thread t;
        private Color theBackColor;
        private Color theColor;
        private int timeToDelay;
        private Image twoImage;
        private int type;
        private ToolStripMenuItem Up;
        private Image zeroImage;
        private ToolStripMenuItem 乏风;
        private ToolStripMenuItem 新风;
        private ToolStripMenuItem 选择方向;

        public TransLittlePicture(PictureBox parent, FullPicture fullPicture, string info, int type, string ceDianBianHao, Label showInfoLabel, string danWei, bool IsEdit, int angle)
        {
            ThreadStart ts;
            this.components = null;
            this.InitializeComponent();
            this.timeToDelay = 750;
            this.fatherNeedRefreash = 2;
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
            this.refreashTimes = 2;
            base.SizeChanged += new EventHandler(this.ALittlePicture_SizeChanged);
            this.ContextMenuStrip = this.contextMenuStrip1;
            base.SizeMode = PictureBoxSizeMode.StretchImage;
            this.theColor = Color.Green;
            this.angle = angle;
            this.cmr = new ControlMoveResize(this, parent, 2, IsEdit, this.type);
            base.Parent = parent;
            base.BringToFront();
            if (this.type == 2)
            {
                ts = new ThreadStart(this.DrawJianTouForShanShuo);
                this.t = new Thread(ts);
                this.t.IsBackground = true;
                this.t.Start();
            }
            else if (this.type == 1)
            {
                ts = new ThreadStart(this.LiuShui);
                this.t = new Thread(ts);
                this.t.IsBackground = true;
                this.t.Start();
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
            this.fatherNeedRefreash = this.refreashTimes;
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "删除")
            {
                if (this.t != null)
                {
                    this.t.Abort();
                }
                this.fullPicture.deleteATransPicture(this);
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
            else if (this.type == 2)
            {
                this.contextMenuStrip1.Items[1].Visible = false;
                this.contextMenuStrip1.Items[2].Visible = this.contextMenuStrip1.Items[3].Visible = true;
                if (this.theColor == Color.Blue)
                {
                    this.新风.Checked = true;
                    this.乏风.Checked = false;
                }
                else
                {
                    this.新风.Checked = false;
                    this.乏风.Checked = true;
                }
            }
            else if (this.type == 1)
            {
                this.contextMenuStrip1.Items[1].Visible = false;
                this.contextMenuStrip1.Items[2].Visible = this.contextMenuStrip1.Items[3].Visible = false;
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
            this.fatherNeedRefreash = this.refreashTimes;
        }

        private void DrawJianTouForShanShuo()
        {
            int mm = 0;
            while (true)
            {
                if ((base.Height < 1) || (base.Width < 1))
                {
                    Thread.Sleep(this.timeToDelay);
                }
                else
                {
                    this.ff1(mm);
                    mm++;
                    Thread.Sleep(this.timeToDelay);
                }
            }
        }

        private void ff1(int mm)
        {
            Color colorPi;
            if (this.fatherNeedRefreash > 0)
            {
                base.Parent.Refresh();
                this.fatherNeedRefreash--;
            }
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
                colorPi = Color.White;
            }
            else
            {
                colorPi = this.theColor;
            }
            Pen pen = new Pen(colorPi, 3f);
            Pen pen1 = new Pen(colorPi, 2f);
            backGroundImage.DrawLine(pen, p1, p2);
            backGroundImage.DrawLine(pen1, p2, p3);
            backGroundImage.DrawLine(pen1, p2, p4);
            if (base.InvokeRequired)
            {
                UpdateUI update = delegate { this.Image = bitmap; };
                this.Invoke(update);
            }
            else
            {
                base.Image = bitmap;
            }
        }

        private void ff2(int currentIndex)
        {
            if (this.fatherNeedRefreash > 0)
            {
                base.Parent.Refresh();
                this.fatherNeedRefreash--;
            }
            Bitmap bitmap = new Bitmap(base.Width, base.Height);
            Graphics backGroundImage = Graphics.FromImage(bitmap);
            Point p1 = new Point();
            Point p2 = new Point();
            Point p3 = new Point();
            Point p4 = new Point();
            Point p5 = new Point();
            Point p6 = new Point();
            Point p7 = new Point();
            Point p8 = new Point();
            bool drawSecond = true;
            bool drawThird = true;
            if (this.angle == 1)
            {
                p1.X = 0;
                p1.Y = base.Height;
                p2.X = base.Width;
                p2.Y = 0;
                p3.X = ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10) + Math.Min(p1.X, p2.X);
                int introduced17 = Math.Max(p1.Y, p2.Y);
                p3.Y = introduced17 - ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10);
                p4.X = ((Math.Abs((int) (p1.X - p2.X)) * (currentIndex - 1)) / 10) + Math.Min(p1.X, p2.X);
                int introduced18 = Math.Max(p1.Y, p2.Y);
                p4.Y = introduced18 - ((Math.Abs((int) (p1.Y - p2.Y)) * (currentIndex - 1)) / 10);
                p5.X = ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 3) % 10)) / 10) + Math.Min(p1.X, p2.X);
                int introduced19 = Math.Max(p1.Y, p2.Y);
                p5.Y = introduced19 - ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 3) % 10)) / 10);
                p6.X = ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 4) % 10)) / 10) + Math.Min(p1.X, p2.X);
                int introduced20 = Math.Max(p1.Y, p2.Y);
                p6.Y = introduced20 - ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 4) % 10)) / 10);
                p7.X = ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 6) % 10)) / 10) + Math.Min(p1.X, p2.X);
                int introduced21 = Math.Max(p1.Y, p2.Y);
                p7.Y = introduced21 - ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 6) % 10)) / 10);
                p8.X = ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 7) % 10)) / 10) + Math.Min(p1.X, p2.X);
                int introduced22 = Math.Max(p1.Y, p2.Y);
                p8.Y = introduced22 - ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 7) % 10)) / 10);
                if (p5.X > p6.X)
                {
                    drawSecond = false;
                }
                if (p7.X > p8.X)
                {
                    drawThird = false;
                }
            }
            else if (this.angle == 2)
            {
                p1.X = 0;
                p1.Y = 0;
                p2.X = base.Width;
                p2.Y = base.Height;
                p3.X = ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10) + Math.Min(p1.X, p2.X);
                p3.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10) + Math.Min(p1.Y, p2.Y);
                p4.X = ((Math.Abs((int) (p1.X - p2.X)) * (currentIndex - 1)) / 10) + Math.Min(p1.X, p2.X);
                p4.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (currentIndex - 1)) / 10) + Math.Min(p1.Y, p2.Y);
                p5.X = ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 3) % 10)) / 10) + Math.Min(p1.X, p2.X);
                p5.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 3) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                p6.X = ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 4) % 10)) / 10) + Math.Min(p1.X, p2.X);
                p6.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 4) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                p7.X = ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 6) % 10)) / 10) + Math.Min(p1.X, p2.X);
                p7.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 6) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                p8.X = ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 7) % 10)) / 10) + Math.Min(p1.X, p2.X);
                p8.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 7) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                if (p5.X > p6.X)
                {
                    drawSecond = false;
                }
                if (p7.X > p8.X)
                {
                    drawThird = false;
                }
            }
            else if (this.angle == 3)
            {
                p1.X = base.Width;
                p1.Y = 0;
                p2.X = 0;
                p2.Y = base.Height;
                int introduced23 = Math.Max(p1.X, p2.X);
                p3.X = introduced23 - ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10);
                p3.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10) + Math.Min(p1.Y, p2.Y);
                int introduced24 = Math.Max(p1.X, p2.X);
                p4.X = introduced24 - ((Math.Abs((int) (p1.X - p2.X)) * (currentIndex - 1)) / 10);
                p4.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (currentIndex - 1)) / 10) + Math.Min(p1.Y, p2.Y);
                int introduced25 = Math.Max(p1.X, p2.X);
                p5.X = introduced25 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 3) % 10)) / 10);
                p5.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 3) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                int introduced26 = Math.Max(p1.X, p2.X);
                p6.X = introduced26 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 4) % 10)) / 10);
                p6.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 4) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                int introduced27 = Math.Max(p1.X, p2.X);
                p7.X = introduced27 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 6) % 10)) / 10);
                p7.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 6) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                int introduced28 = Math.Max(p1.X, p2.X);
                p8.X = introduced28 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 7) % 10)) / 10);
                p8.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 7) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                if (p6.X > p5.X)
                {
                    drawSecond = false;
                }
                if (p8.X > p7.X)
                {
                    drawThird = false;
                }
            }
            else if (this.angle == 4)
            {
                p1.X = base.Width;
                p1.Y = base.Height;
                p2.X = 0;
                p2.Y = 0;
                int introduced29 = Math.Max(p1.X, p2.X);
                p3.X = introduced29 - ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10);
                int introduced30 = Math.Max(p1.Y, p2.Y);
                p3.Y = introduced30 - ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10);
                int introduced31 = Math.Max(p1.X, p2.X);
                p4.X = introduced31 - ((Math.Abs((int) (p1.X - p2.X)) * (currentIndex - 1)) / 10);
                int introduced32 = Math.Max(p1.Y, p2.Y);
                p4.Y = introduced32 - ((Math.Abs((int) (p1.Y - p2.Y)) * (currentIndex - 1)) / 10);
                int introduced33 = Math.Max(p1.X, p2.X);
                p5.X = introduced33 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 3) % 10)) / 10);
                int introduced34 = Math.Max(p1.Y, p2.Y);
                p5.Y = introduced34 - ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 3) % 10)) / 10);
                int introduced35 = Math.Max(p1.X, p2.X);
                p6.X = introduced35 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 4) % 10)) / 10);
                int introduced36 = Math.Max(p1.Y, p2.Y);
                p6.Y = introduced36 - ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 4) % 10)) / 10);
                int introduced37 = Math.Max(p1.X, p2.X);
                p7.X = introduced37 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 6) % 10)) / 10);
                int introduced38 = Math.Max(p1.Y, p2.Y);
                p7.Y = introduced38 - ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 6) % 10)) / 10);
                int introduced39 = Math.Max(p1.X, p2.X);
                p8.X = introduced39 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 7) % 10)) / 10);
                int introduced40 = Math.Max(p1.Y, p2.Y);
                p8.Y = introduced40 - ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 7) % 10)) / 10);
                if (p6.X > p5.X)
                {
                    drawSecond = false;
                }
                if (p8.X > p7.X)
                {
                    drawThird = false;
                }
            }
            else if (this.angle == 5)
            {
                p1.X = base.Width / 2;
                p1.Y = base.Height;
                p2.X = base.Width / 2;
                p2.Y = 0;
                int introduced41 = Math.Max(p1.X, p2.X);
                p3.X = introduced41 - ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10);
                int introduced42 = Math.Max(p1.Y, p2.Y);
                p3.Y = introduced42 - ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10);
                int introduced43 = Math.Max(p1.X, p2.X);
                p4.X = introduced43 - ((Math.Abs((int) (p1.X - p2.X)) * (currentIndex - 1)) / 10);
                int introduced44 = Math.Max(p1.Y, p2.Y);
                p4.Y = introduced44 - ((Math.Abs((int) (p1.Y - p2.Y)) * (currentIndex - 1)) / 10);
                int introduced45 = Math.Max(p1.X, p2.X);
                p5.X = introduced45 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 3) % 10)) / 10);
                int introduced46 = Math.Max(p1.Y, p2.Y);
                p5.Y = introduced46 - ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 3) % 10)) / 10);
                int introduced47 = Math.Max(p1.X, p2.X);
                p6.X = introduced47 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 4) % 10)) / 10);
                int introduced48 = Math.Max(p1.Y, p2.Y);
                p6.Y = introduced48 - ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 4) % 10)) / 10);
                int introduced49 = Math.Max(p1.X, p2.X);
                p7.X = introduced49 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 6) % 10)) / 10);
                int introduced50 = Math.Max(p1.Y, p2.Y);
                p7.Y = introduced50 - ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 6) % 10)) / 10);
                int introduced51 = Math.Max(p1.X, p2.X);
                p8.X = introduced51 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 7) % 10)) / 10);
                int introduced52 = Math.Max(p1.Y, p2.Y);
                p8.Y = introduced52 - ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 7) % 10)) / 10);
                if (p6.Y > p5.Y)
                {
                    drawSecond = false;
                }
                if (p8.Y > p7.Y)
                {
                    drawThird = false;
                }
            }
            else if (this.angle == 6)
            {
                p1.X = base.Width / 2;
                p1.Y = 0;
                p2.X = base.Width / 2;
                p2.Y = base.Height;
                int introduced53 = Math.Max(p1.X, p2.X);
                p3.X = introduced53 - ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10);
                p3.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10) + Math.Min(p1.Y, p2.Y);
                int introduced54 = Math.Max(p1.X, p2.X);
                p4.X = introduced54 - ((Math.Abs((int) (p1.X - p2.X)) * (currentIndex - 1)) / 10);
                p4.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (currentIndex - 1)) / 10) + Math.Min(p1.Y, p2.Y);
                int introduced55 = Math.Max(p1.X, p2.X);
                p5.X = introduced55 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 3) % 10)) / 10);
                p5.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 3) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                int introduced56 = Math.Max(p1.X, p2.X);
                p6.X = introduced56 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 4) % 10)) / 10);
                p6.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 4) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                int introduced57 = Math.Max(p1.X, p2.X);
                p7.X = introduced57 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 6) % 10)) / 10);
                p7.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 6) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                int introduced58 = Math.Max(p1.X, p2.X);
                p8.X = introduced58 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 7) % 10)) / 10);
                p8.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 7) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                if (p6.Y < p5.Y)
                {
                    drawSecond = false;
                }
                if (p8.Y > p7.Y)
                {
                    drawThird = false;
                }
            }
            else if (this.angle == 7)
            {
                p1.X = base.Width;
                p1.Y = base.Height / 2;
                p2.X = 0;
                p2.Y = base.Height / 2;
                int introduced59 = Math.Max(p1.X, p2.X);
                p3.X = introduced59 - ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10);
                p3.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10) + Math.Min(p1.Y, p2.Y);
                int introduced60 = Math.Max(p1.X, p2.X);
                p4.X = introduced60 - ((Math.Abs((int) (p1.X - p2.X)) * (currentIndex - 1)) / 10);
                p4.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (currentIndex - 1)) / 10) + Math.Min(p1.Y, p2.Y);
                int introduced61 = Math.Max(p1.X, p2.X);
                p5.X = introduced61 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 3) % 10)) / 10);
                p5.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 3) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                int introduced62 = Math.Max(p1.X, p2.X);
                p6.X = introduced62 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 4) % 10)) / 10);
                p6.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 4) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                int introduced63 = Math.Max(p1.X, p2.X);
                p7.X = introduced63 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 6) % 10)) / 10);
                p7.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 6) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                int introduced64 = Math.Max(p1.X, p2.X);
                p8.X = introduced64 - ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 7) % 10)) / 10);
                p8.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 7) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                if (p6.X > p5.X)
                {
                    drawSecond = false;
                }
                if (p8.X > p7.X)
                {
                    drawThird = false;
                }
            }
            else if (this.angle == 8)
            {
                p1.X = 0;
                p1.Y = base.Height / 2;
                p2.X = base.Width;
                p2.Y = base.Height / 2;
                p3.X = ((Math.Abs((int) (p1.X - p2.X)) * currentIndex) / 10) + Math.Min(p1.X, p2.X);
                p3.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * currentIndex) / 10) + Math.Min(p1.Y, p2.Y);
                p4.X = ((Math.Abs((int) (p1.X - p2.X)) * (currentIndex - 1)) / 10) + Math.Min(p1.X, p2.X);
                p4.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (currentIndex - 1)) / 10) + Math.Min(p1.Y, p2.Y);
                p5.X = ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 3) % 10)) / 10) + Math.Min(p1.X, p2.X);
                p5.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 3) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                p6.X = ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 4) % 10)) / 10) + Math.Min(p1.X, p2.X);
                p6.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 4) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                p7.X = ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 6) % 10)) / 10) + Math.Min(p1.X, p2.X);
                p7.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 6) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                p8.X = ((Math.Abs((int) (p1.X - p2.X)) * (((currentIndex - 1) + 7) % 10)) / 10) + Math.Min(p1.X, p2.X);
                p8.Y = ((Math.Abs((int) (p1.Y - p2.Y)) * (((currentIndex - 1) + 7) % 10)) / 10) + Math.Min(p1.Y, p2.Y);
                if (p6.X < p5.X)
                {
                    drawSecond = false;
                }
                if (p8.X < p7.X)
                {
                    drawThird = false;
                }
            }
            Pen pen = new Pen(Color.White, 2f);
            Pen pen1 = new Pen(Color.Green, 6f);
            p3.Y -= 2;
            p4.Y -= 2;
            p5.Y -= 2;
            p6.Y -= 2;
            p7.Y -= 2;
            p8.Y -= 2;
            backGroundImage.DrawLine(pen1, p1, p2);
            backGroundImage.DrawLine(pen, p3, p4);
            if (drawSecond)
            {
                backGroundImage.DrawLine(pen, p5, p6);
            }
            if (drawThird)
            {
                backGroundImage.DrawLine(pen, p7, p8);
            }
            if (base.InvokeRequired)
            {
                UpdateUI update = delegate { this.Image = bitmap; };
                this.Invoke(update);
            }
            else
            {
                base.Image = bitmap;
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
            this.新风 = new ToolStripMenuItem();
            this.乏风 = new ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            ((ISupportInitialize) this).BeginInit();
            base.SuspendLayout();
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.deleteMenuItem1, this.选择方向, this.新风, this.乏风 });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x77, 0x5c);
            this.contextMenuStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
            this.deleteMenuItem1.Name = "deleteMenuItem1";
            this.deleteMenuItem1.Size = new Size(0x76, 0x16);
            this.deleteMenuItem1.Text = "删除";
            this.选择方向.DropDownItems.AddRange(new ToolStripItem[] { this.RightUp, this.RightDown, this.LeftDown, this.LeftUp, this.Up, this.Down, this.Left, this.Right });
            this.选择方向.Name = "选择方向";
            this.选择方向.Size = new Size(0x76, 0x16);
            this.选择方向.Text = "选择方向";
            this.RightUp.Name = "RightUp";
            this.RightUp.Size = new Size(0x52, 0x16);
            this.RightUp.Text = "↗";
            this.RightUp.Click += new EventHandler(this.RightUp_Click);
            this.RightDown.Name = "RightDown";
            this.RightDown.Size = new Size(0x52, 0x16);
            this.RightDown.Text = "↘";
            this.RightDown.Click += new EventHandler(this.RightDown_Click);
            this.LeftDown.Name = "LeftDown";
            this.LeftDown.Size = new Size(0x52, 0x16);
            this.LeftDown.Text = "↙";
            this.LeftDown.Click += new EventHandler(this.LeftDown_Click);
            this.LeftUp.Name = "LeftUp";
            this.LeftUp.Size = new Size(0x52, 0x16);
            this.LeftUp.Text = "↖";
            this.LeftUp.Click += new EventHandler(this.LeftUp_Click);
            this.Up.Name = "Up";
            this.Up.Size = new Size(0x52, 0x16);
            this.Up.Text = "↑";
            this.Up.Click += new EventHandler(this.Up_Click);
            this.Down.Name = "Down";
            this.Down.Size = new Size(0x52, 0x16);
            this.Down.Text = "↓";
            this.Down.Click += new EventHandler(this.Down_Click);
            this.Left.Name = "Left";
            this.Left.Size = new Size(0x52, 0x16);
            this.Left.Text = "←";
            this.Left.Click += new EventHandler(this.Left_Click);
            this.Right.Name = "Right";
            this.Right.Size = new Size(0x52, 0x16);
            this.Right.Text = "→";
            this.Right.Click += new EventHandler(this.Right_Click);
            this.新风.Checked = true;
            this.新风.CheckState = CheckState.Checked;
            this.新风.Name = "新风";
            this.新风.Size = new Size(0x76, 0x16);
            this.新风.Text = "新风";
            this.新风.Click += new EventHandler(this.选择颜色_Click);
            this.乏风.Checked = true;
            this.乏风.CheckState = CheckState.Checked;
            this.乏风.Name = "乏风";
            this.乏风.Size = new Size(0x76, 0x16);
            this.乏风.Text = "乏风";
            this.乏风.Click += new EventHandler(this.设置背景颜色_Click);
            this.BackColor = Color.Transparent;
            this.contextMenuStrip1.ResumeLayout(false);
            ((ISupportInitialize) this).EndInit();
            base.ResumeLayout(false);
        }

        private void Left_Click(object sender, EventArgs e)
        {
            this.angle = 7;
            this.fatherNeedRefreash = this.refreashTimes;
        }

        private void LeftDown_Click(object sender, EventArgs e)
        {
            this.angle = 3;
            this.fatherNeedRefreash = this.refreashTimes;
        }

        private void LeftUp_Click(object sender, EventArgs e)
        {
            this.angle = 4;
            this.fatherNeedRefreash = this.refreashTimes;
        }

        private void LiuShui()
        {
            int currentIndex = 0;
            while (true)
            {
                if ((base.Height < 1) || (base.Width < 1))
                {
                    Thread.Sleep(this.timeToDelay);
                }
                else
                {
                    this.ff2(currentIndex);
                    currentIndex++;
                    if (currentIndex > 10)
                    {
                        currentIndex = 0;
                    }
                    Thread.Sleep(this.timeToDelay);
                }
            }
        }

        public bool NearTheTail(int X, int Y)
        {
            if ((this.type == 1) || (this.type == 2))
            {
                if (this.angle == 1)
                {
                    return ((X > (base.Width - 8)) && (Y < 8));
                }
                if (this.angle == 2)
                {
                    return ((X > (base.Width - 8)) && (Y > (base.Height - 8)));
                }
                if (this.angle == 3)
                {
                    return ((X < 8) && (Y > (base.Height - 8)));
                }
                if (this.angle == 4)
                {
                    return ((X < 8) && (Y < 8));
                }
                if (this.angle == 5)
                {
                    return (Y < 8);
                }
                if (this.angle == 6)
                {
                    return (Y > (base.Height - 8));
                }
                if (this.angle == 7)
                {
                    return (X < 8);
                }
                if (this.angle == 8)
                {
                    return (X > (base.Width - 8));
                }
            }
            return false;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public void ReCalculateAngleAndSize(int newEndPointX, int newEndPointY)
        {
            if ((this.type == 1) || (this.type == 2))
            {
                int locationStartX;
                int locationStartY;
                int locationEndX;
                int locationEndY;
                int trueLocationX;
                int trueLocationY;
                int yuanShiLocationX = base.Location.X;
                int yuanShiLocationY = base.Location.Y;
                int startPointX = 0;
                int startPointY = 0;
                if (this.angle == 1)
                {
                    startPointX = 0;
                    startPointY = base.Height;
                }
                else if (this.angle == 2)
                {
                    startPointX = 0;
                    startPointY = 0;
                }
                else if (this.angle == 3)
                {
                    startPointX = base.Width;
                    startPointY = 0;
                }
                else if (this.angle == 4)
                {
                    startPointX = base.Width;
                    startPointY = base.Height;
                }
                else if (this.angle == 5)
                {
                    startPointX = base.Width / 2;
                    startPointY = base.Height;
                }
                else if (this.angle == 6)
                {
                    startPointX = base.Width / 2;
                    startPointY = 0;
                }
                else if (this.angle == 7)
                {
                    startPointX = base.Width;
                    startPointY = base.Height / 2;
                }
                else if (this.angle == 8)
                {
                    startPointX = 0;
                    startPointY = base.Height / 2;
                }
                if ((Math.Abs((int) (newEndPointX - startPointX)) < 10) && (newEndPointY < startPointY))
                {
                    this.angle = 5;
                    base.Size = new Size(50, startPointY - newEndPointY);
                    locationStartX = startPointX + yuanShiLocationX;
                    locationStartY = startPointY + yuanShiLocationY;
                    locationEndX = newEndPointX + yuanShiLocationX;
                    locationEndY = newEndPointY + yuanShiLocationY;
                    trueLocationX = Math.Min(locationStartX, locationEndX);
                    trueLocationY = Math.Min(locationStartY, locationEndY);
                    base.Location = new Point(trueLocationX - 0x19, trueLocationY);
                }
                else if ((Math.Abs((int) (newEndPointX - startPointX)) < 10) && (newEndPointY > startPointY))
                {
                    this.angle = 6;
                    base.Size = new Size(50, newEndPointY - startPointY);
                    locationStartX = startPointX + yuanShiLocationX;
                    locationStartY = startPointY + yuanShiLocationY;
                    locationEndX = newEndPointX + yuanShiLocationX;
                    locationEndY = newEndPointY + yuanShiLocationY;
                    trueLocationX = Math.Min(locationStartX, locationEndX);
                    trueLocationY = Math.Min(locationStartY, locationEndY);
                    base.Location = new Point(trueLocationX - 0x19, trueLocationY);
                }
                else if ((newEndPointX < startPointX) && (Math.Abs((int) (newEndPointY - startPointY)) < 10))
                {
                    this.angle = 7;
                    base.Size = new Size(startPointX - newEndPointX, 50);
                    locationStartX = startPointX + yuanShiLocationX;
                    locationStartY = startPointY + yuanShiLocationY;
                    locationEndX = newEndPointX + yuanShiLocationX;
                    locationEndY = newEndPointY + yuanShiLocationY;
                    trueLocationX = Math.Min(locationStartX, locationEndX);
                    trueLocationY = Math.Min(locationStartY, locationEndY);
                    base.Location = new Point(trueLocationX, trueLocationY - 0x19);
                }
                else if ((newEndPointX > startPointX) && (Math.Abs((int) (newEndPointY - startPointY)) < 10))
                {
                    this.angle = 8;
                    base.Size = new Size(newEndPointX - startPointX, 50);
                    locationStartX = startPointX + yuanShiLocationX;
                    locationStartY = startPointY + yuanShiLocationY;
                    locationEndX = newEndPointX + yuanShiLocationX;
                    locationEndY = newEndPointY + yuanShiLocationY;
                    trueLocationX = Math.Min(locationStartX, locationEndX);
                    trueLocationY = Math.Min(locationStartY, locationEndY);
                    base.Location = new Point(trueLocationX, trueLocationY - 0x19);
                }
                else
                {
                    int tempX;
                    int tempY;
                    if ((newEndPointX > startPointX) && (newEndPointY < startPointY))
                    {
                        this.angle = 1;
                        tempX = newEndPointX - startPointX;
                        tempY = startPointY - newEndPointY;
                        if (tempX < 15)
                        {
                            tempX = 15;
                        }
                        if (tempY < 15)
                        {
                            tempY = 15;
                        }
                        base.Size = new Size(tempX, tempY);
                        locationStartX = startPointX + yuanShiLocationX;
                        locationStartY = startPointY + yuanShiLocationY;
                        locationEndX = newEndPointX + yuanShiLocationX;
                        locationEndY = newEndPointY + yuanShiLocationY;
                        trueLocationX = Math.Min(locationStartX, locationEndX);
                        trueLocationY = Math.Min(locationStartY, locationEndY);
                        base.Location = new Point(trueLocationX, trueLocationY);
                    }
                    else if ((newEndPointX > startPointX) && (newEndPointY > startPointY))
                    {
                        this.angle = 2;
                        tempX = newEndPointX - startPointX;
                        tempY = newEndPointY - startPointY;
                        if (tempX < 15)
                        {
                            tempX = 15;
                        }
                        if (tempY < 15)
                        {
                            tempY = 15;
                        }
                        base.Size = new Size(tempX, tempY);
                        locationStartX = startPointX + yuanShiLocationX;
                        locationStartY = startPointY + yuanShiLocationY;
                        locationEndX = newEndPointX + yuanShiLocationX;
                        locationEndY = newEndPointY + yuanShiLocationY;
                        trueLocationX = Math.Min(locationStartX, locationEndX);
                        trueLocationY = Math.Min(locationStartY, locationEndY);
                        base.Location = new Point(trueLocationX, trueLocationY);
                    }
                    else if ((newEndPointX < startPointX) && (newEndPointY > startPointY))
                    {
                        this.angle = 3;
                        tempX = startPointX - newEndPointX;
                        tempY = newEndPointY - startPointY;
                        if (tempX < 15)
                        {
                            tempX = 15;
                        }
                        if (tempY < 15)
                        {
                            tempY = 15;
                        }
                        base.Size = new Size(tempX, tempY);
                        locationStartX = startPointX + yuanShiLocationX;
                        locationStartY = startPointY + yuanShiLocationY;
                        locationEndX = newEndPointX + yuanShiLocationX;
                        locationEndY = newEndPointY + yuanShiLocationY;
                        trueLocationX = Math.Min(locationStartX, locationEndX);
                        trueLocationY = Math.Min(locationStartY, locationEndY);
                        base.Location = new Point(trueLocationX, trueLocationY);
                    }
                    else if ((newEndPointX < startPointX) && (newEndPointY < startPointY))
                    {
                        this.angle = 4;
                        tempX = startPointX - newEndPointX;
                        tempY = startPointY - newEndPointY;
                        if (tempX < 15)
                        {
                            tempX = 15;
                        }
                        if (tempY < 15)
                        {
                            tempY = 15;
                        }
                        base.Size = new Size(tempX, tempY);
                        locationStartX = startPointX + yuanShiLocationX;
                        locationStartY = startPointY + yuanShiLocationY;
                        locationEndX = newEndPointX + yuanShiLocationX;
                        locationEndY = newEndPointY + yuanShiLocationY;
                        trueLocationX = Math.Min(locationStartX, locationEndX);
                        trueLocationY = Math.Min(locationStartY, locationEndY);
                        base.Location = new Point(trueLocationX, trueLocationY);
                    }
                }
                this.calculateOffset();
                if (this.type == 2)
                {
                    this.ff1(0);
                    this.ff1(1);
                }
                else if (this.type == 1)
                {
                    this.ff2(0);
                    this.ff2(1);
                }
                this.fatherNeedRefreash = this.refreashTimes;
            }
        }

        private void Right_Click(object sender, EventArgs e)
        {
            this.angle = 8;
            this.fatherNeedRefreash = this.refreashTimes;
        }

        private void RightDown_Click(object sender, EventArgs e)
        {
            this.angle = 2;
            this.fatherNeedRefreash = this.refreashTimes;
        }

        private void RightUp_Click(object sender, EventArgs e)
        {
            this.angle = 1;
            this.fatherNeedRefreash = this.refreashTimes;
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
                            if (this.ceZhi == 0)
                            {
                                lingTaiMingCheng = GlobalParams.AllCeDianList.allcedianlist[this.ceDianBianHao].KaiGuanLiang.LingTai;
                                s = this.ceDianBianHao + "测值：" + lingTaiMingCheng;
                            }
                            else if (this.ceZhi == 1)
                            {
                                lingTaiMingCheng = GlobalParams.AllCeDianList.allcedianlist[this.ceDianBianHao].KaiGuanLiang.YiTai;
                                s = this.ceDianBianHao + "测值：" + lingTaiMingCheng;
                            }
                            else if (this.ceZhi == 2)
                            {
                                lingTaiMingCheng = GlobalParams.AllCeDianList.allcedianlist[this.ceDianBianHao].KaiGuanLiang.ErTai;
                                s = this.ceDianBianHao + "测值：" + lingTaiMingCheng;
                            }
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
                    this.showInfoLabel.Visible = false;
                }
            }
        }

        public bool TuYuanExist(int type, string info)
        {
            return ((this.type == type) && (this.info == info));
        }

        public bool TuYuanExist(int type, int info, string ceDianBianHao)
        {
            return ((this.type == type) && (this.ceDianBianHao == ceDianBianHao));
        }

        private void Up_Click(object sender, EventArgs e)
        {
            this.angle = 5;
            this.fatherNeedRefreash = this.refreashTimes;
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
            this.theColor = Color.Green;
        }

        private void 选择颜色_Click(object sender, EventArgs e)
        {
            this.theColor = Color.Blue;
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
                if (this.theColor == Color.Blue)
                {
                    return 1;
                }
                return 2;
            }
            set
            {
                this.theColor = Color.FromArgb(value);
                if (value == 1)
                {
                    this.theColor = Color.Blue;
                }
                else
                {
                    this.theColor = Color.Green;
                }
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

        private delegate void UpdateUI();
    }
}

