namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Config_color : UserControl
    {
        private Button btn0;
        private Button btn1;
        private Button btn2;
        private Button btnAnaAlarm;
        private Button btnAnaCom;
        private Button btnAnaCut;
        private Button btnAnaError;
        private Button btnAnaIO;
        private Button btnAnaNormal;
        private Button btnAnaOff;
        private Button btnAnaOver;
        private Button btnAnaPos;
        private Button btnSwiIO;
        private Button button5;
        private ClientConfig clientConfig;
        private ColorDialog colorDialog1;
        private IContainer components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label11;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label17;
        private Label label19;
        private Label label2;
        private Label label20;
        private Label label22;
        private Label label3;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label9;
        private Label lbl0;
        private Label lbl1;
        private Label lbl2;
        private Label lblAnaAlarm;
        private Label lblAnaCom;
        private Label lblAnaCut;
        private Label lblAnaError;
        private Label lblAnaIO;
        private Label lblAnaNormal;
        private Label lblAnaOff;
        private Label lblAnaOver;
        private Label lblAnapos;
        private Label lblSwiIO;
        private Label lblTiaoJiao;

        public Config_color()
        {
            this.InitializeComponent();
            this.clientConfig = ClientConfig.CreateCommon();
            this.InitializeBackColor();
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lbl0.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("kLingTaiColor", ColorTranslator.ToHtml(this.lbl0.BackColor));
                MainFormRef.updateMainForm();
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lbl1.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("kYiTaiColor", ColorTranslator.ToHtml(this.lbl1.BackColor));
                MainFormRef.updateMainForm();
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lbl2.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("kErTaiColor", ColorTranslator.ToHtml(this.lbl2.BackColor));
                MainFormRef.updateMainForm();
            }
        }

        private void btnAnaAlarm_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblAnaAlarm.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("mBaoJingColor", ColorTranslator.ToHtml(this.lblAnaAlarm.BackColor));
                MainFormRef.updateMainForm();
            }
        }

        private void btnAnaCom_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblAnaCom.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("mCommFailColor", ColorTranslator.ToHtml(this.lblAnaCom.BackColor));
                MainFormRef.updateMainForm();
            }
        }

        private void btnAnaCut_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblAnaCut.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("mDuanDianColor", ColorTranslator.ToHtml(this.lblAnaCut.BackColor));
                MainFormRef.updateMainForm();
            }
        }

        private void btnAnaError_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblAnaError.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("mGuZhangColor", ColorTranslator.ToHtml(this.lblAnaError.BackColor));
                MainFormRef.updateMainForm();
            }
        }

        private void btnAnaIO_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblAnaIO.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("mIOCommFailColor", ColorTranslator.ToHtml(this.lblAnaIO.BackColor));
                MainFormRef.updateMainForm();
            }
        }

        private void btnAnaNormal_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblAnaNormal.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("mZhengChangColor", ColorTranslator.ToHtml(this.lblAnaNormal.BackColor));
                MainFormRef.updateMainForm();
            }
        }

        private void btnAnaOff_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblAnaOff.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("mDuanXianColor", ColorTranslator.ToHtml(this.lblAnaOff.BackColor));
                MainFormRef.updateMainForm();
            }
        }

        private void btnAnaOver_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblAnaOver.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("mYiChuColor", ColorTranslator.ToHtml(this.lblAnaOver.BackColor));
                MainFormRef.updateMainForm();
            }
        }

        private void btnAnaPos_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblAnapos.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("mFuPiaoColor", ColorTranslator.ToHtml(this.lblAnapos.BackColor));
                MainFormRef.updateMainForm();
            }
        }

        private void btnSwiIO_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblSwiIO.BackColor = this.colorDialog1.Color;
                this.clientConfig.set("kIOCommFailColor", ColorTranslator.ToHtml(this.lblSwiIO.BackColor));
                MainFormRef.updateMainForm();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.setBackColorAna();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.resetBackColorAna();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.setBackColorSwi();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.resetBackColorSwi();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblTiaoJiao.BackColor = this.colorDialog1.Color;
                this.clientConfig.add("mTiaoJiao", ColorTranslator.ToHtml(this.lblTiaoJiao.BackColor));
                MainFormRef.updateMainForm();
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void InitializeBackColor()
        {
            this.InitializeBackColorAna();
            this.InitializeBackColorSwi();
        }

        private void InitializeBackColorAna()
        {
            this.lblAnaNormal.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("mZhengChangColor"));
            this.lblAnaAlarm.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("mBaoJingColor"));
            this.lblAnaCut.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("mDuanDianColor"));
            this.lblAnaOff.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("mDuanXianColor"));
            this.lblAnaOver.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("mYiChuColor"));
            this.lblAnapos.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("mFuPiaoColor"));
            this.lblAnaError.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("mGuZhangColor"));
            this.lblAnaIO.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("mIOCommFailColor"));
            this.lblAnaCom.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("mCommFailColor"));
            this.lblTiaoJiao.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("mTiaoJiao"));
        }

        private void InitializeBackColorSwi()
        {
            this.lblSwiIO.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("kIOCommFailColor"));
            this.lbl0.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("kLingTaiColor"));
            this.lbl1.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("kYiTaiColor"));
            this.lbl2.BackColor = ColorTranslator.FromHtml(this.clientConfig.get("kErTaiColor"));
        }

        private void InitializeComponent()
        {
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnAnaNormal = new System.Windows.Forms.Button();
            this.lblAnaNormal = new System.Windows.Forms.Label();
            this.btnAnaAlarm = new System.Windows.Forms.Button();
            this.lblAnaAlarm = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAnaCut = new System.Windows.Forms.Button();
            this.lblAnaCut = new System.Windows.Forms.Label();
            this.btnAnaOff = new System.Windows.Forms.Button();
            this.lblAnaOff = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAnaOver = new System.Windows.Forms.Button();
            this.lblAnaOver = new System.Windows.Forms.Label();
            this.btnAnaPos = new System.Windows.Forms.Button();
            this.lblAnapos = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnAnaError = new System.Windows.Forms.Button();
            this.lblAnaError = new System.Windows.Forms.Label();
            this.btnAnaIO = new System.Windows.Forms.Button();
            this.lblAnaIO = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnAnaCom = new System.Windows.Forms.Button();
            this.lblAnaCom = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.lblTiaoJiao = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSwiIO = new System.Windows.Forms.Button();
            this.lblSwiIO = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btn0 = new System.Windows.Forms.Button();
            this.lbl0 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn2 = new System.Windows.Forms.Button();
            this.lbl2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btn1 = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAnaNormal
            // 
            this.btnAnaNormal.BackColor = System.Drawing.Color.Chocolate;
            this.btnAnaNormal.ForeColor = System.Drawing.Color.White;
            this.btnAnaNormal.Location = new System.Drawing.Point(302, 22);
            this.btnAnaNormal.Name = "btnAnaNormal";
            this.btnAnaNormal.Size = new System.Drawing.Size(93, 23);
            this.btnAnaNormal.TabIndex = 0;
            this.btnAnaNormal.Text = "选择颜色";
            this.btnAnaNormal.UseVisualStyleBackColor = false;
            this.btnAnaNormal.Click += new System.EventHandler(this.btnAnaNormal_Click);
            // 
            // lblAnaNormal
            // 
            this.lblAnaNormal.BackColor = System.Drawing.Color.White;
            this.lblAnaNormal.Location = new System.Drawing.Point(138, 30);
            this.lblAnaNormal.Name = "lblAnaNormal";
            this.lblAnaNormal.Size = new System.Drawing.Size(137, 15);
            this.lblAnaNormal.TabIndex = 1;
            this.lblAnaNormal.Text = "    ";
            // 
            // btnAnaAlarm
            // 
            this.btnAnaAlarm.BackColor = System.Drawing.Color.Chocolate;
            this.btnAnaAlarm.ForeColor = System.Drawing.Color.White;
            this.btnAnaAlarm.Location = new System.Drawing.Point(715, 22);
            this.btnAnaAlarm.Name = "btnAnaAlarm";
            this.btnAnaAlarm.Size = new System.Drawing.Size(99, 23);
            this.btnAnaAlarm.TabIndex = 3;
            this.btnAnaAlarm.Text = "选择颜色";
            this.btnAnaAlarm.UseVisualStyleBackColor = false;
            this.btnAnaAlarm.Click += new System.EventHandler(this.btnAnaAlarm_Click);
            // 
            // lblAnaAlarm
            // 
            this.lblAnaAlarm.BackColor = System.Drawing.Color.White;
            this.lblAnaAlarm.Location = new System.Drawing.Point(568, 30);
            this.lblAnaAlarm.Name = "lblAnaAlarm";
            this.lblAnaAlarm.Size = new System.Drawing.Size(132, 15);
            this.lblAnaAlarm.TabIndex = 4;
            this.lblAnaAlarm.Text = "    ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(484, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "报警状态";
            // 
            // btnAnaCut
            // 
            this.btnAnaCut.BackColor = System.Drawing.Color.Chocolate;
            this.btnAnaCut.ForeColor = System.Drawing.Color.White;
            this.btnAnaCut.Location = new System.Drawing.Point(302, 58);
            this.btnAnaCut.Name = "btnAnaCut";
            this.btnAnaCut.Size = new System.Drawing.Size(93, 23);
            this.btnAnaCut.TabIndex = 6;
            this.btnAnaCut.Text = "选择颜色";
            this.btnAnaCut.UseVisualStyleBackColor = false;
            this.btnAnaCut.Click += new System.EventHandler(this.btnAnaCut_Click);
            // 
            // lblAnaCut
            // 
            this.lblAnaCut.BackColor = System.Drawing.Color.White;
            this.lblAnaCut.Location = new System.Drawing.Point(138, 66);
            this.lblAnaCut.Name = "lblAnaCut";
            this.lblAnaCut.Size = new System.Drawing.Size(137, 15);
            this.lblAnaCut.TabIndex = 7;
            this.lblAnaCut.Text = "    ";
            // 
            // btnAnaOff
            // 
            this.btnAnaOff.BackColor = System.Drawing.Color.Chocolate;
            this.btnAnaOff.ForeColor = System.Drawing.Color.White;
            this.btnAnaOff.Location = new System.Drawing.Point(715, 58);
            this.btnAnaOff.Name = "btnAnaOff";
            this.btnAnaOff.Size = new System.Drawing.Size(99, 23);
            this.btnAnaOff.TabIndex = 9;
            this.btnAnaOff.Text = "选择颜色";
            this.btnAnaOff.UseVisualStyleBackColor = false;
            this.btnAnaOff.Click += new System.EventHandler(this.btnAnaOff_Click);
            // 
            // lblAnaOff
            // 
            this.lblAnaOff.BackColor = System.Drawing.Color.White;
            this.lblAnaOff.Location = new System.Drawing.Point(568, 66);
            this.lblAnaOff.Name = "lblAnaOff";
            this.lblAnaOff.Size = new System.Drawing.Size(132, 15);
            this.lblAnaOff.TabIndex = 10;
            this.lblAnaOff.Text = "    ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(484, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "断线状态";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // btnAnaOver
            // 
            this.btnAnaOver.BackColor = System.Drawing.Color.Chocolate;
            this.btnAnaOver.ForeColor = System.Drawing.Color.White;
            this.btnAnaOver.Location = new System.Drawing.Point(302, 94);
            this.btnAnaOver.Name = "btnAnaOver";
            this.btnAnaOver.Size = new System.Drawing.Size(93, 23);
            this.btnAnaOver.TabIndex = 12;
            this.btnAnaOver.Text = "选择颜色";
            this.btnAnaOver.UseVisualStyleBackColor = false;
            this.btnAnaOver.Click += new System.EventHandler(this.btnAnaOver_Click);
            // 
            // lblAnaOver
            // 
            this.lblAnaOver.BackColor = System.Drawing.Color.White;
            this.lblAnaOver.Location = new System.Drawing.Point(138, 102);
            this.lblAnaOver.Name = "lblAnaOver";
            this.lblAnaOver.Size = new System.Drawing.Size(137, 15);
            this.lblAnaOver.TabIndex = 13;
            this.lblAnaOver.Text = "    ";
            // 
            // btnAnaPos
            // 
            this.btnAnaPos.BackColor = System.Drawing.Color.Chocolate;
            this.btnAnaPos.ForeColor = System.Drawing.Color.White;
            this.btnAnaPos.Location = new System.Drawing.Point(715, 94);
            this.btnAnaPos.Name = "btnAnaPos";
            this.btnAnaPos.Size = new System.Drawing.Size(99, 23);
            this.btnAnaPos.TabIndex = 15;
            this.btnAnaPos.Text = "选择颜色";
            this.btnAnaPos.UseVisualStyleBackColor = false;
            this.btnAnaPos.Click += new System.EventHandler(this.btnAnaPos_Click);
            // 
            // lblAnapos
            // 
            this.lblAnapos.BackColor = System.Drawing.Color.White;
            this.lblAnapos.Location = new System.Drawing.Point(568, 102);
            this.lblAnapos.Name = "lblAnapos";
            this.lblAnapos.Size = new System.Drawing.Size(132, 15);
            this.lblAnapos.TabIndex = 16;
            this.lblAnapos.Text = "    ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(484, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 16);
            this.label9.TabIndex = 17;
            this.label9.Text = "负漂状态";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // btnAnaError
            // 
            this.btnAnaError.BackColor = System.Drawing.Color.Chocolate;
            this.btnAnaError.ForeColor = System.Drawing.Color.White;
            this.btnAnaError.Location = new System.Drawing.Point(302, 130);
            this.btnAnaError.Name = "btnAnaError";
            this.btnAnaError.Size = new System.Drawing.Size(93, 23);
            this.btnAnaError.TabIndex = 18;
            this.btnAnaError.Text = "选择颜色";
            this.btnAnaError.UseVisualStyleBackColor = false;
            this.btnAnaError.Click += new System.EventHandler(this.btnAnaError_Click);
            // 
            // lblAnaError
            // 
            this.lblAnaError.BackColor = System.Drawing.Color.White;
            this.lblAnaError.Location = new System.Drawing.Point(138, 138);
            this.lblAnaError.Name = "lblAnaError";
            this.lblAnaError.Size = new System.Drawing.Size(137, 15);
            this.lblAnaError.TabIndex = 19;
            this.lblAnaError.Text = "    ";
            // 
            // btnAnaIO
            // 
            this.btnAnaIO.BackColor = System.Drawing.Color.Chocolate;
            this.btnAnaIO.ForeColor = System.Drawing.Color.White;
            this.btnAnaIO.Location = new System.Drawing.Point(715, 130);
            this.btnAnaIO.Name = "btnAnaIO";
            this.btnAnaIO.Size = new System.Drawing.Size(98, 23);
            this.btnAnaIO.TabIndex = 21;
            this.btnAnaIO.Text = "选择颜色";
            this.btnAnaIO.UseVisualStyleBackColor = false;
            this.btnAnaIO.Click += new System.EventHandler(this.btnAnaIO_Click);
            // 
            // lblAnaIO
            // 
            this.lblAnaIO.BackColor = System.Drawing.Color.White;
            this.lblAnaIO.Location = new System.Drawing.Point(568, 138);
            this.lblAnaIO.Name = "lblAnaIO";
            this.lblAnaIO.Size = new System.Drawing.Size(132, 15);
            this.lblAnaIO.TabIndex = 22;
            this.lblAnaIO.Text = "    ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(500, 137);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 16);
            this.label13.TabIndex = 23;
            this.label13.Text = "IO错误";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // btnAnaCom
            // 
            this.btnAnaCom.BackColor = System.Drawing.Color.Chocolate;
            this.btnAnaCom.ForeColor = System.Drawing.Color.White;
            this.btnAnaCom.Location = new System.Drawing.Point(302, 166);
            this.btnAnaCom.Name = "btnAnaCom";
            this.btnAnaCom.Size = new System.Drawing.Size(93, 23);
            this.btnAnaCom.TabIndex = 24;
            this.btnAnaCom.Text = "选择颜色";
            this.btnAnaCom.UseVisualStyleBackColor = false;
            this.btnAnaCom.Click += new System.EventHandler(this.btnAnaCom_Click);
            // 
            // lblAnaCom
            // 
            this.lblAnaCom.BackColor = System.Drawing.Color.White;
            this.lblAnaCom.Location = new System.Drawing.Point(138, 174);
            this.lblAnaCom.Name = "lblAnaCom";
            this.lblAnaCom.Size = new System.Drawing.Size(137, 15);
            this.lblAnaCom.TabIndex = 25;
            this.lblAnaCom.Text = "    ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.lblTiaoJiao);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblAnaCom);
            this.groupBox1.Controls.Add(this.btnAnaCom);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.lblAnaIO);
            this.groupBox1.Controls.Add(this.btnAnaIO);
            this.groupBox1.Controls.Add(this.lblAnaError);
            this.groupBox1.Controls.Add(this.btnAnaError);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblAnapos);
            this.groupBox1.Controls.Add(this.btnAnaPos);
            this.groupBox1.Controls.Add(this.lblAnaOver);
            this.groupBox1.Controls.Add(this.btnAnaOver);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblAnaOff);
            this.groupBox1.Controls.Add(this.btnAnaOff);
            this.groupBox1.Controls.Add(this.lblAnaCut);
            this.groupBox1.Controls.Add(this.btnAnaCut);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblAnaAlarm);
            this.groupBox1.Controls.Add(this.btnAnaAlarm);
            this.groupBox1.Controls.Add(this.lblAnaNormal);
            this.groupBox1.Controls.Add(this.btnAnaNormal);
            this.groupBox1.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox1.Location = new System.Drawing.Point(0, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(873, 216);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模拟量颜色配置";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(714, 166);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(99, 23);
            this.button5.TabIndex = 36;
            this.button5.Text = "选择颜色";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // lblTiaoJiao
            // 
            this.lblTiaoJiao.BackColor = System.Drawing.Color.White;
            this.lblTiaoJiao.Location = new System.Drawing.Point(570, 175);
            this.lblTiaoJiao.Name = "lblTiaoJiao";
            this.lblTiaoJiao.Size = new System.Drawing.Size(132, 14);
            this.lblTiaoJiao.TabIndex = 35;
            this.lblTiaoJiao.Text = "      ";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(484, 173);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 16);
            this.label17.TabIndex = 34;
            this.label17.Text = "调教状态";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(68, 173);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(76, 16);
            this.label19.TabIndex = 31;
            this.label19.Text = "通讯失败";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(68, 137);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 16);
            this.label15.TabIndex = 30;
            this.label15.Text = "故障状态";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(68, 101);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 16);
            this.label11.TabIndex = 29;
            this.label11.Text = "溢出状态";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(68, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 16);
            this.label7.TabIndex = 28;
            this.label7.Text = "断电状态";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(68, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "正常状态";
            // 
            // btnSwiIO
            // 
            this.btnSwiIO.BackColor = System.Drawing.Color.Chocolate;
            this.btnSwiIO.ForeColor = System.Drawing.Color.White;
            this.btnSwiIO.Location = new System.Drawing.Point(716, 67);
            this.btnSwiIO.Name = "btnSwiIO";
            this.btnSwiIO.Size = new System.Drawing.Size(98, 23);
            this.btnSwiIO.TabIndex = 21;
            this.btnSwiIO.Text = "选择颜色";
            this.btnSwiIO.UseVisualStyleBackColor = false;
            this.btnSwiIO.Click += new System.EventHandler(this.btnSwiIO_Click);
            // 
            // lblSwiIO
            // 
            this.lblSwiIO.BackColor = System.Drawing.Color.White;
            this.lblSwiIO.Location = new System.Drawing.Point(568, 71);
            this.lblSwiIO.Name = "lblSwiIO";
            this.lblSwiIO.Size = new System.Drawing.Size(132, 15);
            this.lblSwiIO.TabIndex = 22;
            this.lblSwiIO.Text = "    ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(502, 70);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 16);
            this.label14.TabIndex = 23;
            this.label14.Text = "IO错误";
            // 
            // btn0
            // 
            this.btn0.BackColor = System.Drawing.Color.Chocolate;
            this.btn0.ForeColor = System.Drawing.Color.White;
            this.btn0.Location = new System.Drawing.Point(302, 32);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(93, 23);
            this.btn0.TabIndex = 32;
            this.btn0.Text = "选择颜色";
            this.btn0.UseVisualStyleBackColor = false;
            this.btn0.Click += new System.EventHandler(this.btn0_Click);
            // 
            // lbl0
            // 
            this.lbl0.BackColor = System.Drawing.Color.White;
            this.lbl0.Location = new System.Drawing.Point(138, 36);
            this.lbl0.Name = "lbl0";
            this.lbl0.Size = new System.Drawing.Size(137, 15);
            this.lbl0.TabIndex = 33;
            this.lbl0.Text = "    ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(68, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 34;
            this.label6.Text = "正常状态";
            // 
            // btn2
            // 
            this.btn2.BackColor = System.Drawing.Color.Chocolate;
            this.btn2.ForeColor = System.Drawing.Color.White;
            this.btn2.Location = new System.Drawing.Point(302, 74);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(93, 23);
            this.btn2.TabIndex = 35;
            this.btn2.Text = "选择颜色";
            this.btn2.UseVisualStyleBackColor = false;
            this.btn2.Visible = false;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // lbl2
            // 
            this.lbl2.BackColor = System.Drawing.Color.White;
            this.lbl2.Location = new System.Drawing.Point(138, 79);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(137, 15);
            this.lbl2.TabIndex = 36;
            this.lbl2.Text = "    ";
            this.lbl2.Visible = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(68, 79);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(68, 16);
            this.label20.TabIndex = 37;
            this.label20.Text = "2态状态";
            this.label20.Visible = false;
            // 
            // btn1
            // 
            this.btn1.BackColor = System.Drawing.Color.Chocolate;
            this.btn1.ForeColor = System.Drawing.Color.White;
            this.btn1.Location = new System.Drawing.Point(715, 32);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(98, 23);
            this.btn1.TabIndex = 38;
            this.btn1.Text = "选择颜色";
            this.btn1.UseVisualStyleBackColor = false;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // lbl1
            // 
            this.lbl1.BackColor = System.Drawing.Color.White;
            this.lbl1.Location = new System.Drawing.Point(568, 36);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(132, 15);
            this.lbl1.TabIndex = 39;
            this.lbl1.Text = "    ";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(486, 35);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(76, 16);
            this.label22.TabIndex = 40;
            this.label22.Text = "告警状态";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.lbl1);
            this.groupBox2.Controls.Add(this.btn1);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.lbl2);
            this.groupBox2.Controls.Add(this.btn2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lbl0);
            this.groupBox2.Controls.Add(this.btn0);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.lblSwiIO);
            this.groupBox2.Controls.Add(this.btnSwiIO);
            this.groupBox2.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox2.Location = new System.Drawing.Point(0, 320);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(873, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "开关量颜色配置";
            // 
            // Config_color
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Config_color";
            this.Size = new System.Drawing.Size(943, 538);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        private void resetBackColorAna()
        {
            this.InitializeBackColorAna();
        }

        private void resetBackColorSwi()
        {
            this.InitializeBackColorSwi();
        }

        private void setBackColorAna()
        {
            this.clientConfig.set("mZhengChangColor", ColorTranslator.ToHtml(this.lblAnaNormal.BackColor));
            this.clientConfig.set("mBaoJingColor", ColorTranslator.ToHtml(this.lblAnaAlarm.BackColor));
            this.clientConfig.set("mDuanDianColor", ColorTranslator.ToHtml(this.lblAnaCut.BackColor));
            this.clientConfig.set("mDuanXianColor", ColorTranslator.ToHtml(this.lblAnaOff.BackColor));
            this.clientConfig.set("mYiChuColor", ColorTranslator.ToHtml(this.lblAnaOver.BackColor));
            this.clientConfig.set("mFuPiaoColor", ColorTranslator.ToHtml(this.lblAnapos.BackColor));
            this.clientConfig.set("mGuZhangColor", ColorTranslator.ToHtml(this.lblAnaError.BackColor));
            this.clientConfig.set("mIOCommFailColor", ColorTranslator.ToHtml(this.lblAnaIO.BackColor));
            this.clientConfig.set("mCommFailColor", ColorTranslator.ToHtml(this.lblAnaCom.BackColor));
            this.clientConfig.set("mTiaoJiao", ColorTranslator.ToHtml(this.lblTiaoJiao.BackColor));
        }

        private void setBackColorSwi()
        {
            this.clientConfig.set("kIOCommFailColor", ColorTranslator.ToHtml(this.lblSwiIO.BackColor));
            this.clientConfig.set("kLingTaiColor", ColorTranslator.ToHtml(this.lbl0.BackColor));
            this.clientConfig.set("kYiTaiColor", ColorTranslator.ToHtml(this.lbl1.BackColor));
            this.clientConfig.set("kErTaiColor", ColorTranslator.ToHtml(this.lbl2.BackColor));
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

