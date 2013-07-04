namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class changSkin : UserControl
    {
        private Button cancel;
        private IContainer components = null;
        private ClientConfig config;
        private Button confirm;
        private GroupBox groupBox1;
        private Label label1;
        private MainForm mf;
        private Panel panel4;
        private RadioButton radioButton1;
        private RadioButton rbCalmness;
        private RadioButton rbDeepCyan;
        private RadioButton rbDeepGreen;
        private RadioButton rbMacOS;
        private RadioButton rbMSN;
        private string skinFile;

        public changSkin(MainForm m)
        {
            this.InitializeComponent();
            this.mf = m;
            this.config = ClientConfig.CreateCommon();
            this.skinFile = this.config.get("skin");
            this.mf.skinEngine1.SkinFile = this.skinFile;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.mf.skinEngine1.SkinFile = this.config.get("skin");
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            this.config.set("skin", this.skinFile);
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
            this.rbMacOS = new RadioButton();
            this.rbMSN = new RadioButton();
            this.rbDeepGreen = new RadioButton();
            this.rbDeepCyan = new RadioButton();
            this.rbCalmness = new RadioButton();
            this.cancel = new Button();
            this.confirm = new Button();
            this.panel4 = new Panel();
            this.label1 = new Label();
            this.radioButton1 = new RadioButton();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.rbMacOS);
            this.groupBox1.Controls.Add(this.rbMSN);
            this.groupBox1.Controls.Add(this.rbDeepGreen);
            this.groupBox1.Controls.Add(this.rbDeepCyan);
            this.groupBox1.Controls.Add(this.rbCalmness);
            this.groupBox1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.groupBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox1.Location = new Point(0x87, 0x4d);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x111, 0xf6);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.rbMacOS.AutoSize = true;
            this.rbMacOS.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.rbMacOS.Location = new Point(0x1f, 0xb6);
            this.rbMacOS.Name = "rbMacOS";
            this.rbMacOS.Size = new Size(0x65, 0x12);
            this.rbMacOS.TabIndex = 4;
            this.rbMacOS.TabStop = true;
            this.rbMacOS.Text = "  苹果风格";
            this.rbMacOS.UseVisualStyleBackColor = true;
            this.rbMacOS.CheckedChanged += new EventHandler(this.rbMacOS_CheckedChanged);
            this.rbMSN.AutoSize = true;
            this.rbMSN.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.rbMSN.Location = new Point(0x1f, 0x8f);
            this.rbMSN.Name = "rbMSN";
            this.rbMSN.Size = new Size(0x5f, 0x12);
            this.rbMSN.TabIndex = 3;
            this.rbMSN.TabStop = true;
            this.rbMSN.Text = "  MSN风格";
            this.rbMSN.UseVisualStyleBackColor = true;
            this.rbMSN.CheckedChanged += new EventHandler(this.rbMSN_CheckedChanged);
            this.rbDeepGreen.AutoSize = true;
            this.rbDeepGreen.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.rbDeepGreen.Location = new Point(0x1f, 0x68);
            this.rbDeepGreen.Name = "rbDeepGreen";
            this.rbDeepGreen.Size = new Size(0x56, 0x12);
            this.rbDeepGreen.TabIndex = 2;
            this.rbDeepGreen.TabStop = true;
            this.rbDeepGreen.Text = "  深绿色";
            this.rbDeepGreen.UseVisualStyleBackColor = true;
            this.rbDeepGreen.CheckedChanged += new EventHandler(this.rbDeepGreen_CheckedChanged);
            this.rbDeepCyan.AutoSize = true;
            this.rbDeepCyan.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.rbDeepCyan.Location = new Point(0x1f, 0x41);
            this.rbDeepCyan.Name = "rbDeepCyan";
            this.rbDeepCyan.Size = new Size(0x56, 0x12);
            this.rbDeepCyan.TabIndex = 1;
            this.rbDeepCyan.TabStop = true;
            this.rbDeepCyan.Text = "  深青色";
            this.rbDeepCyan.UseVisualStyleBackColor = true;
            this.rbDeepCyan.CheckedChanged += new EventHandler(this.rbDeepCyan_CheckedChanged);
            this.rbCalmness.AutoSize = true;
            this.rbCalmness.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.rbCalmness.Location = new Point(0x1f, 0x1a);
            this.rbCalmness.Name = "rbCalmness";
            this.rbCalmness.Size = new Size(0x65, 0x12);
            this.rbCalmness.TabIndex = 0;
            this.rbCalmness.TabStop = true;
            this.rbCalmness.Text = "  平静风格";
            this.rbCalmness.UseVisualStyleBackColor = true;
            this.rbCalmness.CheckedChanged += new EventHandler(this.rbCalmness_CheckedChanged);
            this.cancel.BackColor = Color.Chocolate;
            this.cancel.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.cancel.ForeColor = System.Drawing.Color.White;
            this.cancel.Location = new Point(0x12b, 0x158);
            this.cancel.Name = "cancel";
            this.cancel.Size = new Size(0x4b, 0x17);
            this.cancel.TabIndex = 6;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = false;
            this.cancel.Click += new EventHandler(this.cancel_Click);
            this.confirm.BackColor = Color.Chocolate;
            this.confirm.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.confirm.ForeColor = System.Drawing.Color.White;
            this.confirm.Location = new Point(0xa6, 0x158);
            this.confirm.Name = "confirm";
            this.confirm.Size = new Size(0x4b, 0x17);
            this.confirm.TabIndex = 5;
            this.confirm.Text = "确定";
            this.confirm.UseVisualStyleBackColor = false;
            this.confirm.Click += new EventHandler(this.confirm_Click);
            this.panel4.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel4.AutoSize = true;
            this.panel4.BackColor = Color.AliceBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label1);
            this.panel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel4.Location = new Point(0, 0x27);
            this.panel4.Name = "panel4";
            this.panel4.Size = new Size(0x1f2, 0x20);
            this.panel4.TabIndex = 40;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0x23, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x47, 30);
            this.label1.TabIndex = 14;
            this.label1.Text = "皮肤选择";
            this.label1.TextAlign = ContentAlignment.MiddleLeft;
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new Point(0x1f, 0xdd);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0x65, 0x12);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "  系统风格";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.panel4);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.cancel);
            base.Controls.Add(this.confirm);
            base.Name = "changSkin";
            base.Size = new Size(0x1f2, 0x197);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.mf.skinEngine1.SkinFile = null;
            this.skinFile = null;
        }

        private void rbCalmness_CheckedChanged(object sender, EventArgs e)
        {
            this.mf.skinEngine1.SkinFile = @"skin\Calmness.ssk";
            this.skinFile = @"skin\Calmness.ssk";
        }

        private void rbDeepCyan_CheckedChanged(object sender, EventArgs e)
        {
            this.mf.skinEngine1.SkinFile = @"skin\DeepCyan.ssk";
            this.skinFile = @"skin\DeepCyan.ssk";
        }

        private void rbDeepGreen_CheckedChanged(object sender, EventArgs e)
        {
            this.mf.skinEngine1.SkinFile = @"skin\DeepGreen.ssk";
            this.skinFile = @"skin\DeepGreen.ssk";
        }

        private void rbMacOS_CheckedChanged(object sender, EventArgs e)
        {
            this.mf.skinEngine1.SkinFile = @"skin\MacOS.ssk";
            this.skinFile = @"skin\MacOS.ssk";
        }

        private void rbMSN_CheckedChanged(object sender, EventArgs e)
        {
            this.mf.skinEngine1.SkinFile = @"skin\MSN.ssk";
            this.skinFile = @"skin\MSN.ssk";
        }
    }
}

