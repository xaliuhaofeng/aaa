namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class PrintButton : UserControl
    {
        private Button button1;
        private IContainer components = null;
        private DataGridView dataGridView1 = new DataGridView();
        private string footer = "页脚";
        private string subTitle = "";
        private string title = "DataGridView打印";

        public PrintButton()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new DGVPrinter { Title = this.title, SubTitle = this.subTitle, SubTitleFormatFlags = StringFormatFlags.NoClip | StringFormatFlags.LineLimit, PageNumbers = true, ShowTotalPageNumber = true, PageNumberInHeader = false, PorportionalColumns = true, HeaderCellAlignment = StringAlignment.Near, Footer = this.footer, FooterSpacing = 15f, PageSeparator = " / ", PageText = "页" }.PrintPreviewDataGridView(this.dataGridView1);
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
            this.button1 = new Button();
            base.SuspendLayout();
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0;
            this.button1.Text = "打印";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.button1);
            base.Name = "PrintButton";
            base.Size = new Size(0x51, 0x1c);
            base.ResumeLayout(false);
        }

        public DataGridView DataGridView1
        {
            get
            {
                return this.dataGridView1;
            }
            set
            {
                this.dataGridView1 = value;
            }
        }

        public string Footer
        {
            get
            {
                return this.footer;
            }
            set
            {
                this.footer = value;
            }
        }

        public string SubTitle
        {
            get
            {
                return this.subTitle;
            }
            set
            {
                this.subTitle = value;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }
    }
}

