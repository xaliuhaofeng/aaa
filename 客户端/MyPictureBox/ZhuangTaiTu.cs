namespace MyPictureBox
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class ZhuangTaiTu : UserControl
    {
        private IContainer components = null;
        private int coordinate = 60;
        private int downCoordinate;
        private float gridHeight;
        private int gridWidth;
        private int leftCoordinate = 60;
        private PictureBox pictureBox1;
        private int pointsNow = 0;
        private int upCoordinate;
        private ZhuangTaiValue[] values = new ZhuangTaiValue[200];
        private string[] yZhou = new string[] { "2态", "1态", "0态" };

        public ZhuangTaiTu()
        {
            this.InitializeComponent();
            this.gridWidth = (this.pictureBox1.Width - this.coordinate) - this.leftCoordinate;
            this.upCoordinate = 80;
            this.downCoordinate = 60;
            this.gridHeight = ((this.pictureBox1.Height - this.upCoordinate) - this.downCoordinate) / (this.yZhou.Length - 1);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private float GetHeight(float f)
        {
            return ((this.pictureBox1.Height - this.downCoordinate) - ((f * ((this.pictureBox1.Height - this.upCoordinate) - this.downCoordinate)) / 2f));
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new PictureBox();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.pictureBox1.BackColor = Color.Chartreuse;
            this.pictureBox1.Dock = DockStyle.Fill;
            this.pictureBox1.Location = new Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x263, 0x13c);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Resize += new EventHandler(this.pictureBox1_Resize);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.LightGray;
            base.Controls.Add(this.pictureBox1);
            base.Name = "ZhuZhuangTu";
            base.Size = new Size(0x263, 0x13c);
            base.Load += new EventHandler(this.ZhuZhuangTu_Load);
            base.Resize += new EventHandler(this.ZhuZhuangTu_Resize);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
        }

        public void SetValues(int[] f, DateTime[] times)
        {
            this.pointsNow = 0;
            for (int i = 0; i < f.Length; i++)
            {
                this.values[i].Value = f[i];
                this.values[i].X = this.coordinate + (i * 40);
                this.values[i].time = times[i];
                this.pointsNow++;
            }
        }

        public void ShowZhuZHuangTu()
        {
            int i;
            Bitmap bitmap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            Font font = new Font("Arial", 10f, FontStyle.Regular);
            Font font1 = new Font("宋体", 20f, FontStyle.Bold);
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, this.pictureBox1.Width, this.pictureBox1.Height), Color.Blue, Color.BlueViolet, 1.2f, true);
            g.FillRectangle(Brushes.WhiteSmoke, 0, 0, this.pictureBox1.Width, this.pictureBox1.Height);
            Brush brush1 = new SolidBrush(Color.Blue);
            g.DrawString(" 开关量状态图", font1, brush, new PointF(70f, 30f));
            g.DrawRectangle(new Pen(Color.Blue), 0, 0, this.pictureBox1.Width - 1, this.pictureBox1.Height - 1);
            Pen mypen = new Pen(brush, 1f);
            string[] n = new string[] { 
                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", 
                "16", "17", "18", "19", "20", "21", "22", "23", "24"
             };
            int x = this.coordinate + this.gridWidth;
            for (i = 1; i < n.Length; i++)
            {
                g.DrawLine(mypen, x, this.upCoordinate, x, this.pictureBox1.Height - this.downCoordinate);
                g.DrawString(n[i].ToString(), font, Brushes.Blue, (float) (x - 4), (float) ((this.pictureBox1.Height - this.downCoordinate) + 8));
                x += this.gridWidth;
            }
            Pen mypen1 = new Pen(Color.Blue, 2f);
            g.DrawLine(mypen1, this.coordinate, this.upCoordinate, this.coordinate, this.pictureBox1.Height - this.downCoordinate);
            g.DrawString(n[0], font, Brushes.Blue, (float) (this.coordinate - 4), (float) ((this.pictureBox1.Height - this.downCoordinate) + 8));
            float y = this.upCoordinate;
            for (i = 0; i < this.yZhou.Length; i++)
            {
                g.DrawString(this.yZhou[i], font, Brushes.Blue, (float) (this.coordinate - 0x23), y - 8f);
                y += this.gridHeight;
            }
            x = this.coordinate;
            Font font2 = new Font("Arial", 8f, FontStyle.Bold);
            SolidBrush mybrush = new SolidBrush(Color.Red);
            SolidBrush mybrush2 = new SolidBrush(Color.Green);
            for (i = 0; i <= this.pointsNow; i++)
            {
                if (i != 0)
                {
                    if (i > 1)
                    {
                        g.DrawLine(Pens.Red, (float) (x - this.gridWidth), this.GetHeight((float) this.values[i - 1].Value), (float) (x - this.gridWidth), this.GetHeight((float) this.values[i].Value));
                    }
                    g.DrawLine(Pens.Red, (float) (x - this.gridWidth), this.GetHeight((float) this.values[i].Value), (float) x, this.GetHeight((float) this.values[i].Value));
                }
                x += this.gridWidth;
            }
            this.pictureBox1.Image = bitmap;
        }

        private void ZhuZhuangTu_Load(object sender, EventArgs e)
        {
        }

        private void ZhuZhuangTu_Resize(object sender, EventArgs e)
        {
            if (this.pictureBox1.Height != 0)
            {
                this.gridWidth = ((this.pictureBox1.Width - this.coordinate) - this.leftCoordinate) / 0x18;
                this.gridHeight = ((float) ((this.pictureBox1.Height - this.upCoordinate) - this.downCoordinate)) / 2f;
                this.ShowZhuZHuangTu();
            }
        }
    }
}

