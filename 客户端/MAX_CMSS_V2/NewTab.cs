namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NewTab : Form
    {
        private Button button1;
        private Button button2;
        private IContainer components = null;
        private Label label1;
        private TextBox textBox1;

        public NewTab()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            this.label1 = new Label();
            this.textBox1 = new TextBox();
            this.button1 = new Button();
            this.button2 = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x2c, 40);
            this.label1.Margin = new Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "标签页名称";
            this.textBox1.Location = new Point(140, 0x23);
            this.textBox1.Margin = new Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0xcc, 0x19);
            this.textBox1.TabIndex = 1;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new Point(0x60, 0x59);
            this.button1.Margin = new Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new Size(100, 0x1d);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new Point(0xd8, 0x58);
            this.button2.Margin = new Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new Size(100, 0x1d);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            base.AcceptButton = this.button1;
            base.AutoScaleDimensions = new SizeF(8f, 15f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.CancelButton = this.button2;
            base.ClientSize = new Size(0x185, 0x9a);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label1);
            base.Margin = new Padding(4, 4, 4, 4);
            base.Name = "NewTab";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "标签页命名";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public string TabName
        {
            get
            {
                return this.textBox1.Text;
            }
        }
    }
}

