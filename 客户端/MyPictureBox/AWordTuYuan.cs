namespace MyPictureBox
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AWordTuYuan : Label
    {
        private string anZhuangDiDian;
        private string ceDianBianHao;
        private float ceZhi;
        private ControlMoveResize cmr;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private string danWei;
        private byte fenZhanHao;
        private FullPicture fullPicture;
        private string info;
        private string leiXing;
        private PointF offsetFromCenterImg;
        private bool shouAnZhuangDiDian;
        private bool showCeDianBianHao;
        private bool showCeZhi;
        private Label showInfoLabel;
        private bool showLeiXing;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private int type;

        public AWordTuYuan(PictureBox parent, FullPicture fullPicture, string info, int type, string ceDianBianHao, Label showInfoLabel, bool IsEdit)
        {
            this.components = null;
            this.InitializeComponent();
            this.BackColor = Color.Transparent;
            this.AutoSize = true;
            this.fullPicture = fullPicture;
            this.type = type;
            this.info = info;
            this.showInfoLabel = showInfoLabel;
            this.offsetFromCenterImg = new PointF();
            this.offsetFromCenterImg.X = this.offsetFromCenterImg.Y = 0f;
            base.SizeChanged += new EventHandler(this.AWordTuYuan_SizeChanged);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.cmr = new ControlMoveResize(this, parent, 1, IsEdit, this.type);
            this.ceDianBianHao = ceDianBianHao;
            this.fenZhanHao = Convert.ToByte(this.ceDianBianHao.Substring(0, 2));
            if (type == 6)
            {
                this.Text = info;
                this.toolStripMenuItem1.Enabled = this.toolStripMenuItem2.Enabled = this.toolStripMenuItem3.Enabled = this.toolStripMenuItem4.Enabled = false;
            }
            else if (type == 7)
            {
                this.showCeZhi = true;
                this.shouAnZhuangDiDian = this.showCeDianBianHao = this.showLeiXing = false;
            }
            base.BringToFront();
        }

        public AWordTuYuan(PictureBox parent, FullPicture fullPicture, string info, int type, string showCeZhi, string showAnZhuangDiDian, string showLeiXing, string showCeDianBianHao, string ceDianBianHao, Label showInfoLabel, bool IsEdit)
        {
            this.components = null;
            this.InitializeComponent();
            this.BackColor = Color.Transparent;
            this.AutoSize = true;
            this.fullPicture = fullPicture;
            this.type = type;
            this.info = info;
            this.showInfoLabel = showInfoLabel;
            this.offsetFromCenterImg = new PointF();
            this.offsetFromCenterImg.X = this.offsetFromCenterImg.Y = 0f;
            base.SizeChanged += new EventHandler(this.AWordTuYuan_SizeChanged);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.cmr = new ControlMoveResize(this, parent, 1, IsEdit, this.type);
            this.ceDianBianHao = ceDianBianHao;
            this.fenZhanHao = Convert.ToByte(this.ceDianBianHao.Substring(0, 2));
            if (type == 6)
            {
                this.Text = info;
            }
            else if (type == 7)
            {
                this.toolStripMenuItem1.Enabled = this.toolStripMenuItem2.Enabled = this.toolStripMenuItem3.Enabled = this.toolStripMenuItem4.Enabled = true;
                this.showCeZhi = Convert.ToBoolean(showCeZhi);
                this.shouAnZhuangDiDian = Convert.ToBoolean(showAnZhuangDiDian);
                this.showLeiXing = Convert.ToBoolean(showLeiXing);
                this.showCeDianBianHao = Convert.ToBoolean(showCeDianBianHao);
            }
            base.BringToFront();
        }

        private void AWordTuYuan_SizeChanged(object sender, EventArgs e)
        {
            this.calculateOffset();
        }

        public void calculateOffset()
        {
            PointF p = this.fullPicture.getImageCenter();
            Point q = base.Location;
            float zoom = this.fullPicture.getZoom();
            this.offsetFromCenterImg.X = p.X - (((q.X + q.X) + base.Width) / 2);
            this.offsetFromCenterImg.Y = p.Y - (((q.Y + q.Y) + base.Height) / 2);
            this.offsetFromCenterImg.X /= zoom;
            this.offsetFromCenterImg.Y /= zoom;
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "删除")
            {
                this.fullPicture.DeleteAWord(this);
            }
            else if (e.ClickedItem.Text == "当前测值")
            {
                this.showCeZhi = true;
                this.showCeDianBianHao = this.shouAnZhuangDiDian = this.showLeiXing = false;
                this.SetDongTaiText(0);
            }
            else if (e.ClickedItem.Text == "安装地点")
            {
                this.shouAnZhuangDiDian = true;
                this.showCeZhi = this.showCeDianBianHao = this.showLeiXing = false;
                this.SetDongTaiText(0);
            }
            else if (e.ClickedItem.Text == "类型")
            {
                this.showLeiXing = true;
                this.showCeZhi = this.shouAnZhuangDiDian = this.showCeDianBianHao = false;
                this.SetDongTaiText(0);
            }
            else if (e.ClickedItem.Text == "测点编号")
            {
                this.showCeDianBianHao = true;
                this.showCeZhi = this.shouAnZhuangDiDian = this.showLeiXing = false;
                this.SetDongTaiText(0);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (this.type != 6)
            {
                this.toolStripMenuItem1.Checked = this.showCeZhi;
                this.toolStripMenuItem2.Checked = this.shouAnZhuangDiDian;
                this.toolStripMenuItem3.Checked = this.showLeiXing;
                this.toolStripMenuItem4.Checked = this.showCeDianBianHao;
            }
        }

        internal void Dispatch(FenZhanRTdata pd)
        {
            if (pd == null)
            {
                this.ceZhi = 0f;
                this.Text = "通讯失败";
            }
            else if (((this.type == 7) && (this.fenZhanHao == pd.fenZhanHao)) && GlobalParams.AllCeDianList.allcedianlist.ContainsKey(this.ceDianBianHao))
            {
                CeDian cedian = GlobalParams.AllCeDianList.allcedianlist[this.ceDianBianHao];
                byte num = (byte) cedian.TongDaoHao;
                byte zhuangTai = pd.tongDaoZhuangTai[num];
                string ztname = GlobalParams.stateTranslate(zhuangTai);
                switch (zhuangTai)
                {
                    case 4:
                        this.Text = "断线";
                        this.ceZhi = 0f;
                        return;

                    case 5:
                        this.Text = "溢出";
                        this.ceZhi = cedian.MoNiLiang.LiangChengGao;
                        return;

                    case 6:
                        this.Text = "负漂";
                        this.ceZhi = cedian.MoNiLiang.LiangChengGao;
                        return;

                    case 7:
                        this.Text = "故障";
                        this.ceZhi = 0f;
                        return;

                    case 8:
                        this.Text = "I/O错误";
                        this.ceZhi = 0f;
                        return;
                }
                if (this.ceDianBianHao[2] == 'A')
                {
                    if (pd.realValue[num] > 0f)
                    {
                        this.ceZhi = pd.realValue[num];
                    }
                    else
                    {
                        this.ceZhi = 0f;
                    }
                    if (this.showCeZhi)
                    {
                        this.Text = this.ceZhi + this.danWei;
                    }
                }
                else if (this.ceDianBianHao[2] == 'F')
                {
                    if (pd.AC == 0)
                    {
                        this.Text = "交流";
                    }
                    else
                    {
                        this.Text = "直流";
                    }
                }
                else if (cedian.KaiGuanLiang != null)
                {
                    int value = (int) pd.realValue[num];
                    string text = "";
                    if (value == 0)
                    {
                        text = cedian.KaiGuanLiang.LingTai;
                    }
                    else if (value == 1)
                    {
                        text = cedian.KaiGuanLiang.YiTai;
                    }
                    else if (value == 2)
                    {
                        text = cedian.KaiGuanLiang.ErTai;
                    }
                    else
                    {
                        text = "断线";
                    }
                    if (this.showCeZhi)
                    {
                        this.Text = text;
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

        public Point getNewLocation()
        {
            PointF p = this.fullPicture.getImageCenter();
            float zoom = this.fullPicture.getZoom();
            float xOffset = this.offsetFromCenterImg.X * zoom;
            float yOffset = (int) (this.offsetFromCenterImg.Y * zoom);
            xOffset = p.X - xOffset;
            yOffset = p.Y - yOffset;
            return new Point { X = (int) (((xOffset * 2f) - base.Width) / 2f), Y = (int) (((yOffset * 2f) - base.Height) / 2f) };
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new ToolStripMenuItem();
            this.toolStripMenuItem2 = new ToolStripMenuItem();
            this.toolStripMenuItem3 = new ToolStripMenuItem();
            this.toolStripMenuItem4 = new ToolStripMenuItem();
            this.toolStripMenuItem5 = new ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripMenuItem1, this.toolStripMenuItem2, this.toolStripMenuItem3, this.toolStripMenuItem4, this.toolStripMenuItem5 });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x77, 0x72);
            this.contextMenuStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
            this.toolStripMenuItem1.CheckOnClick = true;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new Size(0x76, 0x16);
            this.toolStripMenuItem1.Text = "当前测值";
            this.toolStripMenuItem2.CheckOnClick = true;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new Size(0x76, 0x16);
            this.toolStripMenuItem2.Text = "安装地点";
            this.toolStripMenuItem3.CheckOnClick = true;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new Size(0x76, 0x16);
            this.toolStripMenuItem3.Text = "类型";
            this.toolStripMenuItem4.CheckOnClick = true;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new Size(0x76, 0x16);
            this.toolStripMenuItem4.Text = "测点编号";
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new Size(0x76, 0x16);
            this.toolStripMenuItem5.Text = "删除";
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void SetDongTaiText(byte b)
        {
            if (this.type != 6)
            {
                if (this.shouAnZhuangDiDian && (this.Text != this.anZhuangDiDian))
                {
                    this.Text = this.anZhuangDiDian;
                }
                else if (this.showCeDianBianHao && (this.Text != this.ceDianBianHao))
                {
                    this.Text = this.ceDianBianHao;
                }
                else if (this.showLeiXing && (this.Text != this.leiXing))
                {
                    this.Text = this.leiXing;
                }
                else if (this.showCeZhi)
                {
                    this.Text = b + this.danWei;
                }
            }
        }

        public void SetSize(string s)
        {
            int m = s.LastIndexOf(';');
            base.Width = Convert.ToInt32(s.Substring(0, m));
            base.Height = Convert.ToInt32(s.Substring(m + 1));
        }

        public void ShowInfo(bool show, int X, int Y)
        {
            if (this.type != 6)
            {
                if (show)
                {
                    int labX;
                    int labY;
                    if (this.ceZhi < 0f)
                    {
                        this.ceZhi = 0f;
                    }
                    this.showInfoLabel.Text = string.Concat(new object[] { this.ceDianBianHao, "测值：", this.ceZhi, this.danWei });
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

        public string AnZhuangDiDian
        {
            get
            {
                return this.anZhuangDiDian;
            }
            set
            {
                this.anZhuangDiDian = value;
                if (this.anZhuangDiDian == "")
                {
                    this.anZhuangDiDian = "  ";
                }
            }
        }

        public string CeDianBianHao
        {
            get
            {
                return this.ceDianBianHao;
            }
        }

        public float CeZhi
        {
            get
            {
                return this.ceZhi;
            }
            set
            {
                this.ceZhi = value;
                this.Text = this.ceZhi + this.danWei;
            }
        }

        public string DanWei
        {
            get
            {
                return this.danWei;
            }
            set
            {
                this.danWei = value;
                if (this.danWei == "")
                {
                    this.danWei = "  ";
                }
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

        public string LeiXing
        {
            get
            {
                return this.leiXing;
            }
            set
            {
                this.leiXing = value;
                if (this.leiXing == "")
                {
                    this.leiXing = "  ";
                }
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

        public string ShowAnZHuangDiDian
        {
            get
            {
                return this.shouAnZhuangDiDian.ToString();
            }
            set
            {
                this.shouAnZhuangDiDian = Convert.ToBoolean(value);
            }
        }

        public string ShowCeDianBianHao
        {
            get
            {
                return this.showCeDianBianHao.ToString();
            }
            set
            {
                this.showCeDianBianHao = Convert.ToBoolean(value);
            }
        }

        public string ShowCeZhi
        {
            get
            {
                return this.showCeZhi.ToString();
            }
            set
            {
                this.showCeZhi = Convert.ToBoolean(value);
            }
        }

        public string ShowLeiXing
        {
            get
            {
                return this.showLeiXing.ToString();
            }
            set
            {
                this.showLeiXing = Convert.ToBoolean(value);
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
    }
}

