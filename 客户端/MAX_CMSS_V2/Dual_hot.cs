namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Dual_hot : UserControl
    {
        private Button button1;
        private IContainer components = null;
        private GroupBox groupBox1;
        private Label label15;
        private Label label3;
        private Label label4;
        private Panel panel2;
        private TextBox textBox1;
        private TextBox textBox2;

        public Dual_hot()
        {
            this.InitializeComponent();
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
            this.textBox2 = new TextBox();
            this.textBox1 = new TextBox();
            this.label4 = new Label();
            this.label3 = new Label();
            this.button1 = new Button();
            this.panel2 = new Panel();
            this.label15 = new Label();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new Point(0x47, 0x56);
            this.groupBox1.Margin = new Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(4, 3, 4, 3);
            this.groupBox1.Size = new Size(0x278, 0xa8);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.textBox2.Location = new Point(0xac, 0x63);
            this.textBox2.Margin = new Padding(4, 3, 4, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(360, 0x17);
            this.textBox2.TabIndex = 6;
            this.textBox1.Location = new Point(0xac, 0x2c);
            this.textBox1.Margin = new Padding(4, 3, 4, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(360, 0x17);
            this.textBox1.TabIndex = 5;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x34, 0x67);
            this.label4.Margin = new Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "备机IP";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x34, 0x30);
            this.label3.Margin = new Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "主机IP";
            this.button1.BackColor = Color.Chocolate;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(0x210, 0x112);
            this.button1.Margin = new Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 3;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = false;
            this.panel2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel2.AutoSize = true;
            this.panel2.BackColor = Color.AliceBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label15);
            this.panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel2.Location = new Point(0, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x2fd, 0x1d);
            this.panel2.TabIndex = 0x29;
            this.label15.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label15.Location = new Point(0x29, 1);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x89, 0x1a);
            this.label15.TabIndex = 8;
            this.label15.Text = "双机热备配置";
            this.label15.TextAlign = ContentAlignment.MiddleLeft;
            base.AutoScaleDimensions = new SizeF(8f, 14f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.groupBox1);
            this.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.ForeColor = System.Drawing.Color.CornflowerBlue;
            base.Margin = new Padding(4, 3, 4, 3);
            base.Name = "Dual_hot";
            base.Size = new Size(0x2fd, 0x161);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

