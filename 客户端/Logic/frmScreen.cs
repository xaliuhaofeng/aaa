namespace Logic
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class frmScreen : Form
    {
        public Bitmap bmp;
        private Button button1;
        private IContainer components = null;
        public Rectangle eara = Rectangle.Empty;
        public Bitmap myPic;
        public Point p2;
        public Point pMove;
        private PrintDialog printDialog1;
        private PrintDocument printDocument1;
        private PrintPreviewDialog printPreviewDialog1;
        public Point pStart;
        public Point pTemp;

        public frmScreen(Bitmap bmp)
        {
            this.InitializeComponent();
            this.bmp = bmp;
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.BackgroundImage = bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmScreen_DoubleClick(object sender, EventArgs e)
        {
            if ((this.eara != Rectangle.Empty) && this.eara.Contains(Cursor.Position))
            {
                this.myPic = new Bitmap(this.eara.Width, this.eara.Height);
                Graphics g = Graphics.FromImage(this.bmp);
                this.myPic = this.bmp.Clone(this.eara, PixelFormat.Format32bppRgb);
                if (this.myPic.Width > this.myPic.Height)
                {
                    this.printDocument1.DefaultPageSettings.Landscape = true;
                }
                this.printPreviewDialog1.TopMost = true;
                this.printPreviewDialog1.ShowDialog();
                base.Close();
            }
        }

        private void frmScreen_Load(object sender, EventArgs e)
        {
        }

        private void frmScreen_MouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (this.eara != Rectangle.Empty))
            {
                this.eara = Rectangle.Empty;
                this.pStart = Point.Empty;
                this.pTemp = Point.Empty;
                this.pMove = Point.Empty;
                this.p2 = Point.Empty;
                base.Invalidate();
            }
            else if ((e.Button == MouseButtons.Right) && (this.eara == Rectangle.Empty))
            {
                base.Close();
            }
        }

        private void frmScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (this.eara == Rectangle.Empty))
            {
                this.pStart = new Point(e.X, e.Y);
                this.pTemp = this.pStart;
            }
        }

        private void frmScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.pMove = new Point(e.X, e.Y);
                base.Invalidate();
            }
        }

        private void frmScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.eara = Rectangle.Empty;
                base.Invalidate();
            }
        }

        private void frmScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.bmp, new Point(0, 0));
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmScreen));
            this.button1 = new Button();
            this.printDocument1 = new PrintDocument();
            this.printPreviewDialog1 = new PrintPreviewDialog();
            this.printDialog1 = new PrintDialog();
            base.SuspendLayout();
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = SystemColors.ButtonFace;
            this.button1.Location = new Point(0x1f6, 0x16a);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.printDocument1.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
            this.printPreviewDialog1.AutoScrollMargin = new Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
            this.printPreviewDialog1.ClientSize = new Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = (Icon) resources.GetObject("printPreviewDialog1.Icon");
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode =System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(0x286, 0x1c6);
            base.Controls.Add(this.button1);
            base.FormBorderStyle =System.Windows.Forms.FormBorderStyle.None;
            base.Name = "frmScreen";
            this.Text = "frmScreen";
            base.TopMost = true;
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.frmScreen_Load);
            base.MouseUp += new MouseEventHandler(this.frmScreen_MouseUp);
            base.Paint += new PaintEventHandler(this.frmScreen_Paint);
            base.MouseClick += new MouseEventHandler(this.frmScreen_MouseClick);
            base.DoubleClick += new EventHandler(this.frmScreen_DoubleClick);
            base.MouseDown += new MouseEventHandler(this.frmScreen_MouseDown);
            base.MouseMove += new MouseEventHandler(this.frmScreen_MouseMove);
            base.ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            if ((this.pTemp.X < this.pMove.X) && (this.pTemp.Y < this.pMove.Y))
            {
                this.pStart = this.pTemp;
                this.p2 = this.pMove;
            }
            if ((this.pTemp.X > this.pMove.X) && (this.pTemp.Y < this.pMove.Y))
            {
                this.pStart.Y = this.pTemp.Y;
                this.pStart.X = this.pMove.X;
                this.p2.X = this.pTemp.X;
                this.p2.Y = this.pMove.Y;
            }
            if ((this.pTemp.X > this.pMove.X) && (this.pTemp.Y > this.pMove.Y))
            {
                this.p2 = this.pTemp;
                this.pStart = this.pMove;
            }
            if ((this.pTemp.X < this.pMove.X) && (this.pTemp.Y > this.pMove.Y))
            {
                this.pStart.Y = this.pMove.Y;
                this.pStart.X = this.pTemp.X;
                this.p2.Y = this.pTemp.Y;
                this.p2.X = this.pMove.X;
            }
            this.eara = new Rectangle(this.pStart, new Size(this.p2.X - this.pStart.X, this.p2.Y - this.pStart.Y));
            g.DrawRectangle(Pens.Green, this.eara);
            Brush bs = new SolidBrush(Color.FromArgb(50, Color.Gold));
            g.FillRectangle(bs, this.eara);
            Size pointSize = new Size(3, 3);
            Point pRight = new Point(2, 2);
            Rectangle eara1 = new Rectangle(this.eara.Location.X - 2, this.eara.Location.Y - 2, 5, 5);
            Rectangle eara2 = new Rectangle((this.eara.Location.X + (this.eara.Width / 2)) - 2, this.eara.Location.Y - 2, 5, 5);
            Rectangle eara3 = new Rectangle((this.eara.Location.X + this.eara.Width) - 2, this.eara.Location.Y - 2, 5, 5);
            Rectangle eara4 = new Rectangle(this.eara.Location.X - 2, (this.eara.Location.Y + (this.eara.Height / 2)) - 2, 5, 5);
            Rectangle eara5 = new Rectangle((this.eara.Location.X + this.eara.Width) - 2, (this.eara.Location.Y + (this.eara.Height / 2)) - 2, 5, 5);
            Rectangle eara6 = new Rectangle(this.eara.Location.X - 2, (this.eara.Location.Y + this.eara.Height) - 2, 5, 5);
            Rectangle eara7 = new Rectangle((this.eara.Location.X + (this.eara.Width / 2)) - 2, (this.eara.Location.Y + this.eara.Height) - 2, 5, 5);
            Rectangle eara8 = new Rectangle((this.eara.Location.X + this.eara.Width) - 2, (this.eara.Location.Y + this.eara.Height) - 2, 5, 5);
            g.FillRectangle(Brushes.Goldenrod, eara1);
            g.FillRectangle(Brushes.Goldenrod, eara2);
            g.FillRectangle(Brushes.Goldenrod, eara3);
            g.FillRectangle(Brushes.Goldenrod, eara4);
            g.FillRectangle(Brushes.Goldenrod, eara5);
            g.FillRectangle(Brushes.Goldenrod, eara6);
            g.FillRectangle(Brushes.Goldenrod, eara7);
            g.FillRectangle(Brushes.Goldenrod, eara8);
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(this.myPic, 0, 0, this.myPic.Width, this.myPic.Height);
        }
    }
}

