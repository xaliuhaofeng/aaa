namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Config_list : UserControl
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private CheckBox checkBox1;
        private ComboBox comboBox1;
        private IContainer components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label15;
        private Label label2;
        private Label label3;
        private ListBox listBox1;
        private ListBox listBox2;
        private List_show Ls_g;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private TextBox textBox1;

        public Config_list(List_show Ls)
        {
            this.InitializeComponent();
            this.Ls_g = Ls;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.panel2 = new Panel();
            this.label15 = new Label();
            this.groupBox2 = new GroupBox();
            this.panel3 = new Panel();
            this.label3 = new Label();
            this.button4 = new Button();
            this.button3 = new Button();
            this.button2 = new Button();
            this.button1 = new Button();
            this.listBox2 = new ListBox();
            this.listBox1 = new ListBox();
            this.panel1 = new Panel();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.checkBox1 = new CheckBox();
            this.label1 = new Label();
            this.comboBox1 = new ComboBox();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(750, 0x20e);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new EventHandler(this.groupBox1_Enter);
            this.panel2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel2.AutoSize = true;
            this.panel2.BackColor = Color.AliceBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label15);
            this.panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel2.Location = new Point(0, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(750, 0x1d);
            this.panel2.TabIndex = 0x26;
            this.label15.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label15.Location = new Point(0x33, 1);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x6f, 0x1a);
            this.label15.TabIndex = 8;
            this.label15.Text = "列表显示";
            this.label15.TextAlign = ContentAlignment.MiddleLeft;
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.listBox2);
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Location = new Point(0, 140);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x2eb, 0x17b);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.panel3.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel3.AutoSize = true;
            this.panel3.BackColor = Color.AliceBlue;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label3);
            this.panel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel3.Location = new Point(0, 8);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x2f1, 0x1d);
            this.panel3.TabIndex = 0x27;
            this.label3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new Point(0x33, 1);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x6f, 0x1a);
            this.label3.TabIndex = 8;
            this.label3.Text = "表项显示";
            this.label3.TextAlign = ContentAlignment.MiddleLeft;
            this.button4.BackColor = Color.Chocolate;
            this.button4.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new Point(0x151, 0x10d);
            this.button4.Name = "button4";
            this.button4.Size = new Size(0x6a, 0x17);
            this.button4.TabIndex = 5;
            this.button4.Text = "全部移除 <<";
            this.button4.UseVisualStyleBackColor = false;
            this.button3.BackColor = Color.Chocolate;
            this.button3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new Point(0x151, 0xe3);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x6a, 0x17);
            this.button3.TabIndex = 4;
            this.button3.Text = "全部添加 >>";
            this.button3.UseVisualStyleBackColor = false;
            this.button2.BackColor = Color.Chocolate;
            this.button2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new Point(0x151, 0x8e);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x6a, 0x17);
            this.button2.TabIndex = 3;
            this.button2.Text = "移除 <";
            this.button2.UseVisualStyleBackColor = false;
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(0x151, 0x63);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x6a, 0x17);
            this.button1.TabIndex = 2;
            this.button1.Text = "添加 >";
            this.button1.UseVisualStyleBackColor = false;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new Point(0x1c9, 0x38);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new Size(0x10a, 0x130);
            this.listBox2.TabIndex = 1;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(0x38, 0x38);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0x10a, 0x130);
            this.listBox1.TabIndex = 0;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Location = new Point(0, 0x2a);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x2e8, 0x4e);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new PaintEventHandler(this.panel1_Paint);
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new Point(0x35, 20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x43, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "页框名称";
            this.label2.Click += new EventHandler(this.label2_Click);
            this.textBox1.Location = new Point(0x7c, 0x10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x126, 0x15);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = CheckState.Checked;
            this.checkBox1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.checkBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.checkBox1.Location = new Point(0x14c, 50);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x56, 0x12);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "显示列表";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0x35, 50);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x43, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择列表";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "列表一", "列表二", "列表三" });
            this.comboBox1.Location = new Point(0x7c, 0x2f);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0xb1, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.groupBox1);
            base.Name = "Config_list";
            base.Size = new Size(750, 0x211);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}

