namespace MyPictureBox
{
    using Logic;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    public class FullPicture : UserControl
    {
        private ArrayList allFullPictures;
        private Bitmap bmp;
        private IContainer components = null;
        private string fullPictureName;
        private PointF ImageCenter;
        private bool IsDown;
        private bool IsLoadBmp;
        private Label labShowView;
        private List<ALittlePicture> littlePictures;
        private Point newPoint;
        private Point oldPoint;
        private PictureBox pictureBox1;
        private List<TransLittlePicture> transPictures;
        private List<AWordTuYuan> wordTuYuans;
        private float zoom;

        public FullPicture()
        {
            this.InitializeComponent();
            this.littlePictures = new List<ALittlePicture>();
            this.transPictures = new List<TransLittlePicture>();
            this.allFullPictures = new ArrayList();
            this.wordTuYuans = new List<AWordTuYuan>();
            this.getAllFullPictures();
            this.ImageCenter = (PointF) new Point(0, 0);
            this.pictureBox1.Paint += new PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseWheel += new MouseEventHandler(this.pictureBox1_MouseWheel);
        }

        public void AddATuYuan(int type, string info, string ceDianBianHao)
        {
            if (this.bmp == null)
            {
                MessageBox.Show("先加载背景图片");
            }
            else
            {
                DataTable dt = CeDian.GetInfoByCeDianBianHao(ceDianBianHao);
                if ((dt == null) || (dt.Rows.Count == 0))
                {
                    MessageBox.Show("你所选择的测点不存在，请重新选择测点！");
                }
                else
                {
                    int i;
                    string danWei = "";
                    if ((ceDianBianHao[2] == 'A') || (ceDianBianHao[2] == 'a'))
                    {
                        danWei = Convert.ToString(dt.Rows[0]["danWei"]);
                    }
                    if (type == 5)
                    {
                        for (i = 0; i < this.littlePictures.Count; i++)
                        {
                            ALittlePicture little = this.littlePictures[i];
                            if (little.TuYuanExist(type, 1, ceDianBianHao))
                            {
                                MessageBox.Show("已存在");
                                return;
                            }
                        }
                        ALittlePicture aLittle = new ALittlePicture(this.pictureBox1, this, info, type, ceDianBianHao, this.labShowView, danWei, true, 0)
                        {
                            Location = new Point(this.pictureBox1.Location.X, this.pictureBox1.Location.Y),
                            ZeroImage = Image.FromFile(Application.StartupPath + @"\monitor\jingtai.gif")
                        };
                        aLittle.UpdateImage(0);
                        aLittle.Size = new Size(aLittle.Image.Width, aLittle.Image.Height);
                        this.littlePictures.Add(aLittle);
                        base.Controls.Add(aLittle);
                        aLittle.BringToFront();
                    }
                    else if (((type <= 4) && (type != 1)) && (type != 2))
                    {
                        for (i = 0; i < this.littlePictures.Count; i++)
                        {
                            ALittlePicture little = this.littlePictures[i];
                            if (little.TuYuanExist(type, info))
                            {
                                MessageBox.Show("已存在");
                                return;
                            }
                        }
                        ALittlePicture aLittle = new ALittlePicture(this.pictureBox1, this, info, type, ceDianBianHao, this.labShowView, danWei, true, 1)
                        {
                            Location = new Point(this.pictureBox1.Location.X, this.pictureBox1.Location.Y),
                            TwoImage = Image.FromFile(Application.StartupPath + @"\monitor\two.gif")
                        };
                        if (type == 3)
                        {
                            FolderBrowserDialog sfd = new FolderBrowserDialog {
                                SelectedPath = Application.StartupPath + @"\monitor"
                            };
                            if (sfd.ShowDialog() == DialogResult.Cancel)
                            {
                                return;
                            }
                            string dir = sfd.SelectedPath;
                            if (!dir.Contains(Application.StartupPath + @"\monitor"))
                            {
                                MessageBox.Show("所选路径无效");
                                return;
                            }
                            try
                            {
                                dir = dir.Substring(dir.LastIndexOf('\\') + 1);
                                string fullDir = Application.StartupPath + @"\monitor\" + dir;
                                aLittle.ZeroImage = Image.FromFile(fullDir + @"\0.gif");
                                aLittle.OneImage = Image.FromFile(fullDir + @"\1.gif");
                                aLittle.TwoImage = Image.FromFile(fullDir + @"\2.gif");
                                aLittle.ImagePath = dir;
                            }
                            catch (Exception err)
                            {
                                MessageBox.Show(err.Message);
                                return;
                            }
                            aLittle.UpdateImage(0);
                            aLittle.Size = new Size(aLittle.Image.Width, aLittle.Image.Height);
                        }
                        else if (type == 0)
                        {
                            aLittle.ZeroImage = Image.FromFile(Application.StartupPath + @"\monitor\xuanzhuanzero.gif");
                            aLittle.OneImage = Image.FromFile(Application.StartupPath + @"\monitor\xuanzhuanone.gif");
                            aLittle.TwoImage = Image.FromFile(Application.StartupPath + @"\monitor\xuanzhuantwo.gif");
                            aLittle.UpdateImage(0);
                            aLittle.Size = new Size(aLittle.Image.Width, aLittle.Image.Height);
                        }
                        else if (type == 4)
                        {
                            aLittle.ZeroImage = Image.FromFile(Application.StartupPath + @"\monitor\weiyizero.gif");
                            aLittle.OneImage = Image.FromFile(Application.StartupPath + @"\monitor\weiyione.gif");
                            aLittle.TwoImage = Image.FromFile(Application.StartupPath + @"\monitor\weiyitwo.gif");
                            aLittle.UpdateImage(0);
                            aLittle.Size = new Size(aLittle.Image.Width, aLittle.Image.Height);
                        }
                        this.littlePictures.Add(aLittle);
                        base.Controls.Add(aLittle);
                        aLittle.BringToFront();
                    }
                    else if ((type == 1) || (type == 2))
                    {
                        int ibgcolor;
                        for (i = 0; i < this.transPictures.Count; i++)
                        {
                            TransLittlePicture little = this.transPictures[i];
                            if (little.TuYuanExist(type, info))
                            {
                                MessageBox.Show("已存在");
                                return;
                            }
                        }
                        TransLittlePicture aLittle = new TransLittlePicture(this.pictureBox1, this, info, type, ceDianBianHao, this.labShowView, danWei, true, 1) {
                            Location = new Point(this.pictureBox1.Location.X, this.pictureBox1.Location.Y)
                        };
                        if (type == 1)
                        {
                            ibgcolor = SystemColors.Control.ToArgb();
                            aLittle.IBGColor = ibgcolor;
                            aLittle.Size = new Size(20, 20);
                        }
                        else if (type == 2)
                        {
                            ibgcolor = SystemColors.Control.ToArgb();
                            aLittle.IBGColor = ibgcolor;
                            aLittle.IJianTouColor = 1;
                            aLittle.Size = new Size(20, 20);
                        }
                        this.transPictures.Add(aLittle);
                        base.Controls.Add(aLittle);
                        aLittle.BringToFront();
                    }
                    else
                    {
                        AWordTuYuan little;
                        AWordTuYuan aWord;
                        if (type == 6)
                        {
                            for (i = 0; i < this.wordTuYuans.Count; i++)
                            {
                                little = this.wordTuYuans[i];
                                if (little.TuYuanExist(type, info))
                                {
                                    MessageBox.Show("已存在");
                                    return;
                                }
                            }
                            aWord = new AWordTuYuan(this.pictureBox1, this, info, type, ceDianBianHao, this.labShowView, true) {
                                Location = new Point(this.pictureBox1.Location.X, this.pictureBox1.Location.Y),
                                Text = info
                            };
                            this.wordTuYuans.Add(aWord);
                            base.Controls.Add(aWord);
                            aWord.BringToFront();
                        }
                        else if (type == 7)
                        {
                            for (i = 0; i < this.wordTuYuans.Count; i++)
                            {
                                little = this.wordTuYuans[i];
                                if (little.TuYuanExist(type, info))
                                {
                                    MessageBox.Show("已存在");
                                    return;
                                }
                            }
                            aWord = new AWordTuYuan(this.pictureBox1, this, info, type, "true", "false", "false", "false", ceDianBianHao, this.labShowView, true) {
                                Location = new Point(this.pictureBox1.Location.X, this.pictureBox1.Location.Y),
                                AnZhuangDiDian = Convert.ToString(dt.Rows[0]["ceDianWeiZhi"]),
                                LeiXing = "",
                                DanWei = ""
                            };
                            if ((ceDianBianHao[2] == 'A') || (ceDianBianHao[2] == 'a'))
                            {
                                aWord.DanWei = Convert.ToString(dt.Rows[0]["danWei"]);
                            }
                            aWord.LeiXing = Convert.ToString(dt.Rows[0]["xiaoLeiXIng"]);
                            aWord.SetDongTaiText(0);
                            this.wordTuYuans.Add(aWord);
                            base.Controls.Add(aWord);
                            aWord.BringToFront();
                        }
                    }
                }
            }
        }

        public void deleteAALittlePicture(ALittlePicture aLittle)
        {
            base.Controls.Remove(aLittle);
            this.littlePictures.Remove(aLittle);
            aLittle.Dispose();
        }

        public void DeleteAllCeDian()
        {
            int i;
            for (i = this.littlePictures.Count - 1; i >= 0; i--)
            {
                ALittlePicture aLittle = this.littlePictures[i];
                this.deleteAALittlePicture(aLittle);
            }
            for (i = this.transPictures.Count - 1; i >= 0; i--)
            {
                TransLittlePicture aLittle = this.transPictures[i];
                this.deleteATransPicture(aLittle);
            }
            for (i = this.wordTuYuans.Count - 1; i >= 0; i--)
            {
                AWordTuYuan aLittle = this.wordTuYuans[i];
                this.DeleteAWord(aLittle);
            }
            this.SaveMoNiTu();
        }

        public void deleteATransPicture(TransLittlePicture aLittle)
        {
            base.Controls.Remove(aLittle);
            this.transPictures.Remove(aLittle);
            aLittle.Dispose();
        }

        public void DeleteAWord(AWordTuYuan aWord)
        {
            base.Controls.Remove(aWord);
            this.wordTuYuans.Remove(aWord);
            aWord.Dispose();
        }

        public void Dispatch(FenZhanRTdata ud)
        {
            int i;
            for (i = 0; i < this.littlePictures.Count; i++)
            {
                this.littlePictures[i].Dispatch(ud);
            }
            for (i = 0; i < this.transPictures.Count; i++)
            {
                this.transPictures[i].Dispatch(ud);
            }
            for (i = 0; i < this.wordTuYuans.Count; i++)
            {
                this.wordTuYuans[i].Dispatch(ud);
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

        public void FangDa(float ff)
        {
            if ((this.bmp != null) && (this.zoom <= 5f))
            {
                this.zoom += ff;
                int widthChange = (int) (this.bmp.Width * ff);
                int heightChange = (int) (this.bmp.Height * ff);
                this.newPoint.X -= widthChange / 2;
                this.newPoint.Y -= heightChange / 2;
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                rect.X = newPoint.X;
                rect.Y = newPoint.Y;
                rect.Width = (int)(rect.Width * zoom);
                rect.Height = (int)(rect.Height * zoom);

                ImageCenter.X = rect.X + rect.Width / 2;
                ImageCenter.Y = rect.Y + rect.Height / 2;


                moveAllCeDian();
                pictureBox1.Refresh();              
            }
        }

        private void FullPicture_Load(object sender, EventArgs e)
        {
            this.IsLoadBmp = this.IsDown = false;
            this.zoom = 1f;
            this.oldPoint = new Point(0, 0);
            this.newPoint = new Point(0, 0);
        }

        private void getAllFullPictures()
        {
            this.allFullPictures.Clear();
            string fileName = Application.StartupPath + @"\monitor\cfg.xml";
            XmlDocument mydoc = new XmlDocument();
            mydoc.Load(fileName);
            XmlNodeList elemList = mydoc.GetElementsByTagName("fullpic");
            int i = elemList.Count;
            for (int j = 0; j < i; j++)
            {
                XmlNode rootNode = elemList.Item(j);
                this.allFullPictures.Add(rootNode.InnerText);
            }
        }

        public PointF getImageCenter()
        {
            return this.ImageCenter;
        }

        public float getZoom()
        {
            return this.zoom;
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new PictureBox();
            this.labShowView = new Label();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            this.pictureBox1.Dock = DockStyle.Fill;
            this.pictureBox1.Location = new Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x391, 0x1f9);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseWheel += new MouseEventHandler(this.pictureBox1_MouseWheel);
            this.pictureBox1.MouseMove += new MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseDown += new MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.Paint += new PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseUp += new MouseEventHandler(this.pictureBox1_MouseUp);
            this.pictureBox1.MouseEnter += new EventHandler(this.pictureBox1_MouseEnter);
            this.labShowView.AutoSize = true;
            this.labShowView.BackColor = Color.Transparent;
            this.labShowView.ForeColor = Color.Red;
            this.labShowView.Location = new Point(0x1b4, 0xf6);
            this.labShowView.Name = "labShowView";
            this.labShowView.Size = new Size(0x29, 12);
            this.labShowView.TabIndex = 5;
            this.labShowView.Text = "label1";
            this.labShowView.TextAlign = ContentAlignment.MiddleCenter;
            this.labShowView.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.labShowView);
            base.Controls.Add(this.pictureBox1);
            base.Name = "FullPicture";
            base.Size = new Size(0x391, 0x1f9);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void moveAllCeDian()
        {
            int i;
            Point newLocation;
            for (i = 0; i < this.littlePictures.Count; i++)
            {
                ALittlePicture little = this.littlePictures[i];
                newLocation = little.getNewLocation();
                little.Left = newLocation.X;
                little.Top = newLocation.Y;
            }
            for (i = 0; i < this.transPictures.Count; i++)
            {
                TransLittlePicture little = this.transPictures[i];
                newLocation = little.getNewLocation();
                little.Left = newLocation.X;
                little.Top = newLocation.Y;
            }
            for (i = 0; i < this.wordTuYuans.Count; i++)
            {
                AWordTuYuan little = this.wordTuYuans[i];
                newLocation = little.getNewLocation();
                little.Left = newLocation.X;
                little.Top = newLocation.Y;
            }
        }

        public void OpenImg(string fileName1, bool IsEdit)
        {
            int i;
            for (i = 0; i < this.littlePictures.Count; i++)
            {
                ALittlePicture little = this.littlePictures[i];
                base.Controls.Remove(little);
            }
            for (i = 0; i < this.transPictures.Count; i++)
            {
                TransLittlePicture little = this.transPictures[i];
                base.Controls.Remove(little);
            }
            for (i = 0; i < this.wordTuYuans.Count; i++)
            {
                AWordTuYuan word = this.wordTuYuans[i];
                base.Controls.Remove(word);
            }
            this.littlePictures.Clear();
            this.transPictures.Clear();
            this.wordTuYuans.Clear();
            this.FullPicture_Load(null, null);
            string bgPicName = Application.StartupPath + @"\monitor\" + fileName1 + ".jpg";
            if (File.Exists(bgPicName))
            {
                Image img = Image.FromFile(bgPicName);
                this.fullPictureName = fileName1;
                this.bmp = new Bitmap(img);
                this.ImageCenter.X = this.bmp.Width / 2;
                this.ImageCenter.Y = this.bmp.Height / 2;
                if ((this.pictureBox1.Width < this.bmp.Width) || (this.pictureBox1.Height < this.bmp.Height))
                {
                    this.zoom = Math.Min((float) (((float) this.pictureBox1.Width) / ((float) this.bmp.Width)), (float) (((float) this.pictureBox1.Height) / ((float) this.bmp.Height)));
                    this.ImageCenter.X *= this.zoom;
                    this.ImageCenter.Y *= this.zoom;
                }
                else
                {
                    this.zoom = 1f;
                }
                img.Dispose();
                this.IsLoadBmp = true;
                this.pictureBox1.Refresh();
                if (this.allFullPictures.Contains(this.fullPictureName))
                {
                    string fileName = Application.StartupPath + @"\monitor\cfg.xml";
                    XmlDocument mydoc = new XmlDocument();
                    mydoc.Load(fileName);
                    XmlNodeList groupNodes = mydoc.GetElementsByTagName("root").Item(0).ChildNodes;
                    i = groupNodes.Count;
                    for (int j = 0; j < i; j++)
                    {
                        XmlNode n = groupNodes.Item(j);
                        if (n["fullpic"].InnerText.Trim() == this.fullPictureName)
                        {
                            XmlNodeList monitors = n.ChildNodes;
                            for (int k = 0; k < monitors.Count; k++)
                            {
                                XmlNode monitor = monitors.Item(k);
                                if (monitor.Name == "monitor")
                                {
                                    int angle;
                                    CeDian cedian;
                                    string danWei;
                                    string info = monitor["info"].InnerText;
                                    int type = Convert.ToInt32(monitor["type"].InnerText);
                                    string location = monitor["location"].InnerText;
                                    string ceDianBianHao = monitor["cedianbianhao"].InnerText;
                                    if (((type <= 5) && (type != 1)) && (type != 2))
                                    {
                                        angle = Convert.ToInt32(monitor["angle"].InnerText);
                                        cedian = new CeDian(ceDianBianHao);
                                        danWei = "";
                                        if (cedian.MoNiLiang != null)
                                        {
                                            danWei = cedian.MoNiLiang.DanWei;
                                        }
                                        ALittlePicture aLittle = new ALittlePicture(this.pictureBox1, this, info, type, ceDianBianHao, this.labShowView, danWei, IsEdit, angle);
                                        if (type == 5)
                                        {
                                            aLittle.ZeroImage = Image.FromFile(Application.StartupPath + @"\monitor\jingtai.gif");
                                            aLittle.UpdateImage(0);
                                        }
                                        else if (type == 3)
                                        {
                                            string dir = monitor["imagepath"].InnerText;
                                            aLittle.ImagePath = dir;
                                            string fullPath = Application.StartupPath + @"\monitor\" + dir;
                                            aLittle.ZeroImage = Image.FromFile(fullPath + @"\0.gif");
                                            aLittle.OneImage = Image.FromFile(fullPath + @"\1.gif");
                                            aLittle.TwoImage = Image.FromFile(fullPath + @"\2.gif");
                                            aLittle.UpdateImage(0);
                                        }
                                        else if (type == 0)
                                        {
                                            aLittle.ZeroImage = Image.FromFile(Application.StartupPath + @"\monitor\xuanzhuanzero.gif");
                                            aLittle.OneImage = Image.FromFile(Application.StartupPath + @"\monitor\xuanzhuanone.gif");
                                            aLittle.TwoImage = Image.FromFile(Application.StartupPath + @"\monitor\xuanzhuantwo.gif");
                                            aLittle.UpdateImage(0);
                                        }
                                        else if (type == 4)
                                        {
                                            aLittle.ZeroImage = Image.FromFile(Application.StartupPath + @"\monitor\weiyizero.gif");
                                            aLittle.OneImage = Image.FromFile(Application.StartupPath + @"\monitor\weiyione.gif");
                                            aLittle.TwoImage = Image.FromFile(Application.StartupPath + @"\monitor\weiyitwo.gif");
                                            aLittle.UpdateImage(0);
                                        }
                                        aLittle.Size = new Size(aLittle.Image.Width, aLittle.Image.Height);
                                        aLittle.OffsetFromCenterImg = location;
                                        aLittle.Location = aLittle.getNewLocation();
                                        this.littlePictures.Add(aLittle);
                                        base.Controls.Add(aLittle);
                                        aLittle.BringToFront();
                                    }
                                    else if ((type == 1) || (type == 2))
                                    {
                                        int ibgcolor;
                                        string sSize;
                                        int ii;
                                        int w;
                                        int h;
                                        angle = Convert.ToInt32(monitor["angle"].InnerText);
                                        cedian = new CeDian(ceDianBianHao);
                                        danWei = "";
                                        if (cedian.MoNiLiang != null)
                                        {
                                            danWei = cedian.MoNiLiang.DanWei;
                                        }
                                        TransLittlePicture aLittle = new TransLittlePicture(this.pictureBox1, this, info, type, ceDianBianHao, this.labShowView, danWei, IsEdit, angle);
                                        switch (type)
                                        {
                                            case 1:
                                                ibgcolor = Convert.ToInt32(monitor["ibgcolor"].InnerText.Trim());
                                                aLittle.IBGColor = ibgcolor;
                                                sSize = monitor["ssize"].InnerText.Trim();
                                                ii = sSize.LastIndexOf(';');
                                                w = Convert.ToInt32(sSize.Substring(0, ii));
                                                h = Convert.ToInt32(sSize.Substring(ii + 1));
                                                aLittle.Size = new Size(w, h);
                                                break;

                                            case 2:
                                            {
                                                ibgcolor = Convert.ToInt32(monitor["ibgcolor"].InnerText.Trim());
                                                aLittle.IBGColor = ibgcolor;
                                                int iTheColor = Convert.ToInt32(monitor["ijiantoucolor"].InnerText.Trim());
                                                aLittle.IJianTouColor = iTheColor;
                                                sSize = monitor["ssize"].InnerText.Trim();
                                                ii = sSize.LastIndexOf(';');
                                                w = Convert.ToInt32(sSize.Substring(0, ii));
                                                h = Convert.ToInt32(sSize.Substring(ii + 1));
                                                aLittle.Size = new Size(w, h);
                                                break;
                                            }
                                        }
                                        aLittle.OffsetFromCenterImg = location;
                                        aLittle.Location = aLittle.getNewLocation();
                                        this.transPictures.Add(aLittle);
                                        base.Controls.Add(aLittle);
                                        aLittle.BringToFront();
                                    }
                                    else
                                    {
                                        string size;
                                        AWordTuYuan aLittle;
                                        if (type == 6)
                                        {
                                            size = monitor["size"].InnerText;
                                            aLittle = new AWordTuYuan(this.pictureBox1, this, info, type, ceDianBianHao, this.labShowView, IsEdit);
                                            aLittle.SetSize(size);
                                            aLittle.OffsetFromCenterImg = location;
                                            aLittle.Location = aLittle.getNewLocation();
                                            this.wordTuYuans.Add(aLittle);
                                            base.Controls.Add(aLittle);
                                            aLittle.BringToFront();
                                        }
                                        else if (type == 7)
                                        {
                                            size = monitor["size"].InnerText;
                                            string show1 = monitor["showcezhi"].InnerText;
                                            string show2 = monitor["showanzhuangdidian"].InnerText;
                                            string show3 = monitor["showleixing"].InnerText;
                                            string show4 = monitor["showcedianbianhao"].InnerText;
                                            if (info.Length > 5)
                                            {
                                                info = info.Substring(0, 5);
                                            }
                                            DataTable dt = CeDian.GetInfoByCeDianBianHao(info);
                                            if ((dt == null) || (dt.Rows.Count == 0))
                                            {
                                                break;
                                            }
                                            aLittle = new AWordTuYuan(this.pictureBox1, this, info, type, show1, show2, show3, show4, ceDianBianHao, this.labShowView, IsEdit) {
                                                LeiXing = Convert.ToString(dt.Rows[0]["xiaoLeiXIng"]),
                                                AnZhuangDiDian = Convert.ToString(dt.Rows[0]["ceDianWeiZhi"])
                                            };
                                            danWei = "";
                                            try
                                            {
                                                danWei = Convert.ToString(dt.Rows[0]["danWei"]);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                            aLittle.DanWei = danWei;
                                            aLittle.SetDongTaiText(0);
                                            aLittle.SetSize(size);
                                            aLittle.OffsetFromCenterImg = location;
                                            aLittle.Location = aLittle.getNewLocation();
                                            this.wordTuYuans.Add(aLittle);
                                            base.Controls.Add(aLittle);
                                            aLittle.BringToFront();
                                        }
                                    }
                                }
                            }
                            this.pictureBox1.Refresh();
                            break;
                        }
                    }
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.IsDown = true;
            this.oldPoint.X = e.Location.X - this.newPoint.X;
            this.oldPoint.Y = e.Location.Y - this.newPoint.Y;
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox1.Focus();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.bmp != null) && this.IsDown)
            {
                Point temp = e.Location;
                newPoint.X = temp.X - oldPoint.X;
                newPoint.Y = temp.Y - oldPoint.Y;

                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                rect.X = newPoint.X;
                rect.Y = newPoint.Y;
                rect.Width = (int)(rect.Width * zoom);
                rect.Height = (int)(rect.Height * zoom);

                ImageCenter.X = rect.X + rect.Width / 2;
                ImageCenter.Y = rect.Y + rect.Height / 2;


                moveAllCeDian();
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.IsDown = false;
            this.Cursor = Cursors.Default;
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (this.IsLoadBmp)
            {
                if (e.Delta > 0)
                {
                    this.FangDa(((float) e.Delta) / 1000f);
                }
                else
                {
                    this.SuoXiao(((float) -e.Delta) / 1000f);
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (this.bmp != null)
            {
                Graphics g = e.Graphics;
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                rect.X = newPoint.X;
                rect.Y = newPoint.Y;
                rect.Width = (int)(rect.Width * zoom);
                rect.Height = (int)(rect.Height * zoom);
                Brush bb = new SolidBrush(this.BackColor);
                g.FillRectangle(bb, pictureBox1.ClientRectangle);
                g.DrawImage(bmp, rect, new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                ImageCenter.X = rect.X + rect.Width / 2;
                ImageCenter.Y = rect.Y + rect.Height / 2;
            }
        }

        public void SaveMoNiTu()
        {
            if (this.bmp != null)
            {
                int i;
                XmlNode newMonitir;
                XmlNode newspecial;
                XmlNode newtype;
                XmlNode newlocation;
                XmlNode newCeDianBianHao;
                XmlNode angle;
                XmlNode newImagePath;
                XmlNode newSize;
                XmlNode newBGColor;
                XmlNode newJianTouColor;
                string fileName = Application.StartupPath + @"\monitor\cfg.xml";
                XmlDocument mydoc = new XmlDocument();
                mydoc.Load(fileName);
                XmlNode rootNode = mydoc.GetElementsByTagName("root").Item(0);
                if (this.allFullPictures.Contains(this.fullPictureName))
                {
                    XmlNodeList groupNodes = rootNode.ChildNodes;
                    i = groupNodes.Count;
                    for (int j = 0; j < i; j++)
                    {
                        XmlNode n = groupNodes.Item(j);
                        if (n["fullpic"].InnerText.Trim() == this.fullPictureName)
                        {
                            rootNode.RemoveChild(n);
                            break;
                        }
                    }
                }
                XmlNode newGroup = mydoc.CreateNode(XmlNodeType.Element, "group", "");
                XmlNode fileName1 = mydoc.CreateNode(XmlNodeType.Element, "fullpic", "");
                fileName1.InnerText = this.fullPictureName;
                newGroup.AppendChild(fileName1);
                for (i = 0; i < this.littlePictures.Count; i++)
                {
                    ALittlePicture little = this.littlePictures[i];
                    newMonitir = mydoc.CreateNode(XmlNodeType.Element, "monitor", "");
                    newspecial = mydoc.CreateNode(XmlNodeType.Element, "info", "");
                    newspecial.InnerText = little.Info;
                    newMonitir.AppendChild(newspecial);
                    newtype = mydoc.CreateNode(XmlNodeType.Element, "type", "");
                    newtype.InnerText = little.Type.ToString();
                    newMonitir.AppendChild(newtype);
                    newlocation = mydoc.CreateNode(XmlNodeType.Element, "location", "");
                    newlocation.InnerText = little.OffsetFromCenterImg;
                    newMonitir.AppendChild(newlocation);
                    newCeDianBianHao = mydoc.CreateNode(XmlNodeType.Element, "cedianbianhao", "");
                    newCeDianBianHao.InnerText = little.CeDianBianHao;
                    newMonitir.AppendChild(newCeDianBianHao);
                    angle = mydoc.CreateNode(XmlNodeType.Element, "angle", "");
                    angle.InnerText = little.Angle.ToString();
                    newMonitir.AppendChild(angle);
                    newImagePath = mydoc.CreateNode(XmlNodeType.Element, "imagepath", "");
                    newImagePath.InnerText = little.ImagePath;
                    newMonitir.AppendChild(newImagePath);
                    newSize = mydoc.CreateNode(XmlNodeType.Element, "ssize", "");
                    newSize.InnerText = little.Width.ToString() + ";" + little.Height.ToString();
                    newMonitir.AppendChild(newSize);
                    newBGColor = mydoc.CreateNode(XmlNodeType.Element, "ibgcolor", "");
                    newBGColor.InnerText = little.IBGColor.ToString();
                    newMonitir.AppendChild(newBGColor);
                    newJianTouColor = mydoc.CreateNode(XmlNodeType.Element, "ijiantoucolor", "");
                    newJianTouColor.InnerText = little.IJianTouColor.ToString();
                    newMonitir.AppendChild(newJianTouColor);
                    newGroup.AppendChild(newMonitir);
                }
                rootNode.AppendChild(newGroup);
                for (i = 0; i < this.transPictures.Count; i++)
                {
                    TransLittlePicture little = this.transPictures[i];
                    newMonitir = mydoc.CreateNode(XmlNodeType.Element, "monitor", "");
                    newspecial = mydoc.CreateNode(XmlNodeType.Element, "info", "");
                    newspecial.InnerText = little.Info;
                    newMonitir.AppendChild(newspecial);
                    newtype = mydoc.CreateNode(XmlNodeType.Element, "type", "");
                    newtype.InnerText = little.Type.ToString();
                    newMonitir.AppendChild(newtype);
                    newlocation = mydoc.CreateNode(XmlNodeType.Element, "location", "");
                    newlocation.InnerText = little.OffsetFromCenterImg;
                    newMonitir.AppendChild(newlocation);
                    newCeDianBianHao = mydoc.CreateNode(XmlNodeType.Element, "cedianbianhao", "");
                    newCeDianBianHao.InnerText = little.CeDianBianHao;
                    newMonitir.AppendChild(newCeDianBianHao);
                    angle = mydoc.CreateNode(XmlNodeType.Element, "angle", "");
                    angle.InnerText = little.Angle.ToString();
                    newMonitir.AppendChild(angle);
                    newImagePath = mydoc.CreateNode(XmlNodeType.Element, "imagepath", "");
                    newImagePath.InnerText = little.ImagePath;
                    newMonitir.AppendChild(newImagePath);
                    newSize = mydoc.CreateNode(XmlNodeType.Element, "ssize", "");
                    newSize.InnerText = little.Width.ToString() + ";" + little.Height.ToString();
                    newMonitir.AppendChild(newSize);
                    newBGColor = mydoc.CreateNode(XmlNodeType.Element, "ibgcolor", "");
                    newBGColor.InnerText = little.IBGColor.ToString();
                    newMonitir.AppendChild(newBGColor);
                    newJianTouColor = mydoc.CreateNode(XmlNodeType.Element, "ijiantoucolor", "");
                    newJianTouColor.InnerText = little.IJianTouColor.ToString();
                    newMonitir.AppendChild(newJianTouColor);
                    newGroup.AppendChild(newMonitir);
                }
                rootNode.AppendChild(newGroup);
                for (i = 0; i < this.wordTuYuans.Count; i++)
                {
                    AWordTuYuan aWord = this.wordTuYuans[i];
                    newMonitir = mydoc.CreateNode(XmlNodeType.Element, "monitor", "");
                    newspecial = mydoc.CreateNode(XmlNodeType.Element, "info", "");
                    newspecial.InnerText = aWord.Info;
                    newMonitir.AppendChild(newspecial);
                    newtype = mydoc.CreateNode(XmlNodeType.Element, "type", "");
                    newtype.InnerText = aWord.Type.ToString();
                    newMonitir.AppendChild(newtype);
                    newlocation = mydoc.CreateNode(XmlNodeType.Element, "location", "");
                    newlocation.InnerText = aWord.OffsetFromCenterImg;
                    newMonitir.AppendChild(newlocation);
                    newSize = mydoc.CreateNode(XmlNodeType.Element, "size", "");
                    newSize.InnerText = aWord.Width + ";" + aWord.Height;
                    newMonitir.AppendChild(newSize);
                    XmlNode newShow1 = mydoc.CreateNode(XmlNodeType.Element, "showcezhi", "");
                    newShow1.InnerText = aWord.ShowCeZhi;
                    newMonitir.AppendChild(newShow1);
                    XmlNode newShow2 = mydoc.CreateNode(XmlNodeType.Element, "showanzhuangdidian", "");
                    newShow2.InnerText = aWord.ShowAnZHuangDiDian;
                    newMonitir.AppendChild(newShow2);
                    XmlNode newShow3 = mydoc.CreateNode(XmlNodeType.Element, "showleixing", "");
                    newShow3.InnerText = aWord.ShowLeiXing;
                    newMonitir.AppendChild(newShow3);
                    XmlNode newShow4 = mydoc.CreateNode(XmlNodeType.Element, "showcedianbianhao", "");
                    newShow4.InnerText = aWord.ShowCeDianBianHao;
                    newMonitir.AppendChild(newShow4);
                    newCeDianBianHao = mydoc.CreateNode(XmlNodeType.Element, "cedianbianhao", "");
                    newCeDianBianHao.InnerText = aWord.CeDianBianHao;
                    newMonitir.AppendChild(newCeDianBianHao);
                    newGroup.AppendChild(newMonitir);
                }
                rootNode.AppendChild(newGroup);
                mydoc.Save(fileName);
                this.getAllFullPictures();
                MoNiTuFiles.trncateMoNiTuFiles();
                string xmlContent = mydoc.OuterXml;
                string xmlFileName = "cfg.xml";
                MoNiTuFiles.insertXmlContent(xmlFileName, xmlContent);
                string dirPath = Application.StartupPath + @"\monitor";
                if (Directory.Exists(dirPath))
                {
                    FileStream fs = null;
                    foreach (string fname in Directory.GetFiles(dirPath))
                    {
                        string name = Path.GetFileName(fname);
                        if ((((fname != null) && (fname.Trim().Length != 0)) && !name.Equals("cfg.xml")) && !name.Equals("Thumbs.db"))
                        {
                            fs = new FileStream(fname, FileMode.Open, FileAccess.Read);
                            byte[] ib = new byte[fs.Length];
                            fs.Read(ib, 0, (int) fs.Length);
                            MoNiTuFiles.insertImageContent(name, ib);
                            fs.Close();
                        }
                    }
                }
            }
        }

        public void SuoXiao(float ff)
        {
            if ((this.bmp != null) && (this.zoom >= 0.2))
            {
                 
                this.zoom -= ff;
                if (this.zoom < 0f)
                {
                    this.zoom = 0f;
                }
                else
                {
                    int widthChange = (int) (this.bmp.Width * ff);
                    int heightChange = (int) (this.bmp.Height * ff);
                    this.newPoint.X += widthChange / 2;
                    this.newPoint.Y += heightChange / 2;
                }
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                rect.X = newPoint.X;
                rect.Y = newPoint.Y;
                rect.Width = (int)(rect.Width * zoom);
                rect.Height = (int)(rect.Height * zoom);

                ImageCenter.X = rect.X + rect.Width / 2;
                ImageCenter.Y = rect.Y + rect.Height / 2;


                moveAllCeDian();
                pictureBox1.Refresh();
            }
        }
    }
}

