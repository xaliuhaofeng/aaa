namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Add_Control : Form
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private Label label1;
        private Label label2;
        private ListBox listBox1;
        private ListBox listBox2;
        private Panel panel1;
        private Panel panel2;

        public Add_Control()
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            this.dataGridView1 = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.listBox2 = new ListBox();
            this.listBox1 = new ListBox();
            this.button1 = new Button();
            this.button2 = new Button();
            this.panel1 = new Panel();
            this.label1 = new Label();
            this.panel2 = new Panel();
            this.label2 = new Label();
            this.button3 = new Button();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Chocolate;
            dataGridViewCellStyle1.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.Column1, this.Column2 });
            this.dataGridView1.Dock = DockStyle.Top;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 0x17;
            this.dataGridView1.Size = new Size(0x2e7, 0x7d);
            this.dataGridView1.TabIndex = 0;
            this.Column1.HeaderText = "测点";
            this.Column1.Name = "Column1";
            this.Column2.HeaderText = "控制量";
            this.Column2.Name = "Column2";
            this.listBox2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new Point(0x156, 0xc3);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new Size(0x13b, 0xd0);
            this.listBox2.TabIndex = 1;
            this.listBox1.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(0x39, 0xc3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0x102, 0xd0);
            this.listBox1.TabIndex = 0;
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = SystemColors.ButtonFace;
            this.button1.Location = new Point(0xb8, 0x1b3);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 4;
            this.button1.Text = "添加";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.BackColor = Color.Chocolate;
            this.button2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button2.ForeColor = SystemColors.ButtonFace;
            this.button2.Location = new Point(0x180, 0x1b3);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 5;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = false;
            this.panel1.BackColor = Color.DarkTurquoise;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new Point(0x39, 0x92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x102, 0x24);
            this.panel1.TabIndex = 6;
            this.label1.BackColor = Color.AliceBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x102, 0x24);
            this.label1.TabIndex = 0;
            this.label1.Text = "测点";
            this.label1.TextAlign = ContentAlignment.MiddleLeft;
            this.label1.Click += new EventHandler(this.label1_Click);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new Point(0x156, 0x92);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x13b, 0x24);
            this.panel2.TabIndex = 0;
            this.label2.BackColor = Color.AliceBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x13b, 0x24);
            this.label2.TabIndex = 1;
            this.label2.Text = "控制量";
            this.label2.TextAlign = ContentAlignment.MiddleLeft;
            this.button3.BackColor = Color.Chocolate;
            this.button3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button3.ForeColor = SystemColors.ButtonFace;
            this.button3.Location = new Point(0x11d, 0x1b3);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 7;
            this.button3.Text = "修改";
            this.button3.UseVisualStyleBackColor = false;
            base.AutoScaleDimensions = new SizeF(7f, 14f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(0x2e7, 0x1e9);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.listBox2);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.listBox1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.dataGridView1);
            this.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            base.Name = "Add_Control";
            this.Text = "添加控制通道";
            ((ISupportInitialize) this.dataGridView1).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}

