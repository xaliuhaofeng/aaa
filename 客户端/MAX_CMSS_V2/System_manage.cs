namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class System_manage : UserControl
    {
        private Button button1;
        private Button button3;
        private IContainer components = null;
        private ClientConfig config = ClientConfig.CreateCommon();
        private GroupBox groupBox1;
        private Label label1;
        private Label label15;
        private Label label2;
        private MainForm MFt;
        private OpenFileDialog openFileDialog1;
        private Panel panel1;
        private PictureBox pictureBox1;
        private TextBox textBox1;

        public System_manage(MainForm mf)
        {
            this.MFt = mf;
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgP = new OpenFileDialog {
                CheckFileExists = true,
                Filter = "jpg files (*.jpg)|*.jpg",
                DefaultExt = ".jpg",
                Title = "打开图片文件"
            };
            if (dlgP.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.Image = Image.FromFile(dlgP.FileName);
                this.pictureBox1.ImageLocation = dlgP.FileName;
                if (dlgP.FileName.Length != 0)
                {
                    OperateDBAccess.Execute("insert into XiTongTuBiao values ('" + dlgP.FileName + "')");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string systemName = this.textBox1.Text.Trim();
            if (systemName.Length != 0)
            {
                this.MFt.Text = systemName;
                this.config.add("SystemName", systemName);
            }
            if (this.pictureBox1.ImageLocation != null)
            {
                try
                {
                    this.MFt.PictureAdd(this.pictureBox1.ImageLocation);
                    string toPath = Application.StartupPath + @"\monitor\" + Path.GetFileName(this.pictureBox1.ImageLocation);
                    File.Copy(this.pictureBox1.ImageLocation, toPath, true);
                    this.config.add("MainFormImage", Path.GetFileName(this.pictureBox1.ImageLocation));
                }
                catch
                {
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

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.panel1 = new Panel();
            this.label15 = new Label();
            this.button3 = new Button();
            this.pictureBox1 = new PictureBox();
            this.button1 = new Button();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.label1 = new Label();
            this.openFileDialog1 = new OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x30e, 0x146);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.panel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel1.AutoSize = true;
            this.panel1.BackColor = Color.AliceBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label15);
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel1.Location = new Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x30e, 0x1d);
            this.panel1.TabIndex = 0x23;
            this.label15.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label15.Location = new Point(0x29, 1);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x6f, 0x1a);
            this.label15.TabIndex = 8;
            this.label15.Text = "系统配置管理";
            this.label15.TextAlign = ContentAlignment.MiddleLeft;
            this.button3.BackColor = Color.Chocolate;
            this.button3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button3.ForeColor = SystemColors.ButtonFace;
            this.button3.Location = new Point(0x289, 0x110);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 8;
            this.button3.Text = "确定";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new EventHandler(this.button3_Click);
            this.pictureBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.pictureBox1.Location = new Point(0x2d, 0x80);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x2a5, 0x7c);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = SystemColors.ButtonFace;
            this.button1.Location = new Point(0x214, 0x110);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 3;
            this.button1.Text = "浏览";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new Point(0x2b, 0x67);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x43, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "系统图标";
            this.textBox1.Location = new Point(0x79, 0x4c);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x259, 0x15);
            this.textBox1.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0x2b, 0x4f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x43, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "系统名称";
            this.label1.Click += new EventHandler(this.label1_Click);
            this.openFileDialog1.FileName = "openFileDialog1";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            base.Controls.Add(this.groupBox1);
            base.Name = "System_manage";
            base.Size = new Size(0x30e, 0x1b1);
            base.Load += new EventHandler(this.System_manage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void System_manage_Load(object sender, EventArgs e)
        {
            string systemName = this.config.get("SystemName");
            if (systemName == null)
            {
                systemName = "KJ100W";
            }
            this.textBox1.Text = systemName;
            string imagePath = this.config.get("MainFormImage");
            if (imagePath != null)
            {
                imagePath = Application.StartupPath + @"\monitor\" + imagePath;
                try
                {
                    this.pictureBox1.Image = Image.FromFile(imagePath);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("");
                }
            }
        }
    }
}

