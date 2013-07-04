namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class TestpointPicture_manage : UserControl
    {
        private Button button7;
        private Button button8;
        private ComboBox comboBox1;
        private IContainer components = null;
        private ClientConfig config = ClientConfig.CreateCommon();
        private GroupBox groupBox5;
        private Label label15;
        private Label label8;
        private Panel panel1;
        private PictureBox pictureBox2;

        public TestpointPicture_manage()
        {
            this.InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择测点类型");
            }
            else if (this.pictureBox2.ImageLocation == null)
            {
                MessageBox.Show("请选择要显示的图片");
            }
            else
            {
                Image image = this.pictureBox2.Image.GetThumbnailImage(40, 20, null, IntPtr.Zero);
                string fileName = Path.GetFileName(this.pictureBox2.ImageLocation);
                string toPath = Application.StartupPath + @"\monitor\" + fileName;
                try
                {
                    image.Save(toPath);
                }
                catch (Exception)
                {
                    return;
                }
                toPath = fileName;
                switch (this.comboBox1.SelectedIndex)
                {
                    case 0:
                        this.config.add("YiBanMoNiLiang", toPath);
                        break;

                    case 1:
                        this.config.add("LeiJiMoNiLiang", toPath);
                        break;

                    case 2:
                        this.config.add("LiangTaiKaiGuanLiang", toPath);
                        break;

                    case 3:
                        this.config.add("SanTaiKaiGuanLiang", toPath);
                        break;

                    case 4:
                        this.config.add("TongDuanLiang", toPath);
                        break;

                    case 5:
                        this.config.add("FenZhanLiang", toPath);
                        break;
                }
                MessageBox.Show("图片添加成功");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgP = new OpenFileDialog {
                CheckFileExists = true,
                Filter = "jpg files (*.jpg)|*.jpg",
                DefaultExt = ".jpg",
                Title = "打开图片文件"
            };
            if (dlgP.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox2.Image = Image.FromFile(dlgP.FileName);
                this.pictureBox2.ImageLocation = dlgP.FileName;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pictureBox2.Image = null;
            string imagePath = string.Empty;
            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    imagePath = this.config.get("YiBanMoNiLiang");
                    break;

                case 1:
                    imagePath = this.config.get("LeiJiMoNiLiang");
                    break;

                case 2:
                    imagePath = this.config.get("LiangTaiKaiGuanLiang");
                    break;

                case 3:
                    imagePath = this.config.get("SanTaiKaiGuanLiang");
                    break;

                case 4:
                    imagePath = this.config.get("TongDuanLiang");
                    break;

                case 5:
                    imagePath = this.config.get("FenZhanLiang");
                    break;
            }
            if (imagePath != null)
            {
                imagePath = Application.StartupPath + @"\monitor\" + imagePath;
                this.pictureBox2.Image = Image.FromFile(imagePath);
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

        private void InitializeComponent()
        {
            this.groupBox5 = new GroupBox();
            this.panel1 = new Panel();
            this.label15 = new Label();
            this.comboBox1 = new ComboBox();
            this.button7 = new Button();
            this.pictureBox2 = new PictureBox();
            this.button8 = new Button();
            this.label8 = new Label();
            this.groupBox5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.pictureBox2).BeginInit();
            base.SuspendLayout();
            this.groupBox5.Controls.Add(this.panel1);
            this.groupBox5.Controls.Add(this.comboBox1);
            this.groupBox5.Controls.Add(this.button7);
            this.groupBox5.Controls.Add(this.pictureBox2);
            this.groupBox5.Controls.Add(this.button8);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Dock = DockStyle.Top;
            this.groupBox5.Location = new Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x2b9, 0x15a);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.panel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel1.AutoSize = true;
            this.panel1.BackColor = Color.AliceBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label15);
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel1.Location = new Point(0, 0x12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x2b9, 0x1d);
            this.panel1.TabIndex = 0x24;
            this.label15.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label15.Location = new Point(0x29, 1);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0xa8, 0x1a);
            this.label15.TabIndex = 8;
            this.label15.Text = "测点类型图标管理";
            this.label15.TextAlign = ContentAlignment.MiddleLeft;
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "一般模拟量", "累计量", "两态开关量", "三态开关量", "触点开关量", "分站量" });
            this.comboBox1.Location = new Point(0x9c, 0x60);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x182, 20);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.button7.BackColor = Color.Chocolate;
            this.button7.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button7.ForeColor = SystemColors.ButtonFace;
            this.button7.Location = new Point(0x38, 0x117);
            this.button7.Name = "button7";
            this.button7.Size = new Size(0x4b, 0x17);
            this.button7.TabIndex = 13;
            this.button7.Text = "确定";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new EventHandler(this.button7_Click);
            this.pictureBox2.BackColor = Color.White;
            this.pictureBox2.Location = new Point(0x9c, 0x93);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Size(0x182, 0x9b);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            this.button8.BackColor = Color.Chocolate;
            this.button8.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Location = new Point(0x38, 0x93);
            this.button8.Name = "button8";
            this.button8.Size = new Size(0x4b, 0x17);
            this.button8.TabIndex = 10;
            this.button8.Text = "浏览";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new EventHandler(this.button8_Click);
            this.label8.AutoSize = true;
            this.label8.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label8.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label8.Location = new Point(0x35, 0x61);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x43, 14);
            this.label8.TabIndex = 9;
            this.label8.Text = "测点类型";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.groupBox5);
            base.Name = "TestpointPicture_manage";
            base.Size = new Size(0x2b9, 0x167);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize) this.pictureBox2).EndInit();
            base.ResumeLayout(false);
        }
    }
}

