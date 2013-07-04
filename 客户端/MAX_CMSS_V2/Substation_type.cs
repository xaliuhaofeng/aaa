namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Substation_type : UserControl
    {
        private Button btnAdd;
        private Button btnDelete;
        private IContainer components = null;
        private byte flag = 0;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label15;
        private Label label2;
        private ListBox lbLocation;
        private Panel panel1;
        private Panel panel3;
        private string temp;
        private TextBox txtLocation;

        public Substation_type()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.txtLocation.Text.Length == 0)
            {
                MessageBox.Show("请输入分站类型");
            }
            else
            {
                DataTable dt;
                if (this.flag == 0)
                {
                    if (OperateDB.Select(FenZhanLeiXing.CountType(this.txtLocation.Text)).Rows.Count > 0)
                    {
                        MessageBox.Show("该分站类型名称已被使用，请重新命名");
                        this.txtLocation.Focus();
                    }
                    else
                    {
                        OperateDB.Execute(FenZhanLeiXing.CreateType(this.txtLocation.Text));
                        this.lbLocation.Items.Clear();
                        dt = FenZhanLeiXing.GetFenZhanType();
                        foreach (DataRow row in dt.Rows)
                        {
                            this.lbLocation.Items.Add(row["leiXing"].ToString());
                        }
                        this.txtLocation.Clear();
                    }
                }
                else if (this.flag == 1)
                {
                    if ((this.txtLocation.Text != this.temp) && (OperateDB.Select(FenZhanLeiXing.CountType(this.txtLocation.Text)).Rows.Count > 0))
                    {
                        MessageBox.Show("该分站类型名称已被使用，请重新命名");
                        this.txtLocation.Focus();
                    }
                    else
                    {
                        OperateDB.Execute(FenZhanLeiXing.UpdateType(this.txtLocation.Text, this.temp));
                        this.lbLocation.Items.Clear();
                        dt = FenZhanLeiXing.GetFenZhanType();
                        foreach (DataRow row in dt.Rows)
                        {
                            this.lbLocation.Items.Add(row["leiXing"].ToString());
                        }
                        this.txtLocation.Clear();
                        this.flag = 0;
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.flag == 0)
            {
                MessageBox.Show("请选择要删除的类型");
                this.lbLocation.Focus();
            }
            else if (this.flag == 1)
            {
                OperateDB.Execute(FenZhanLeiXing.DeleteType(this.txtLocation.Text));
                this.lbLocation.Items.Clear();
                DataTable dt = FenZhanLeiXing.GetFenZhanType();
                foreach (DataRow row in dt.Rows)
                {
                    this.lbLocation.Items.Add(row["leiXing"].ToString());
                }
                this.txtLocation.Clear();
            }
            this.flag = 0;
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
            this.btnAdd = new Button();
            this.groupBox1 = new GroupBox();
            this.panel3 = new Panel();
            this.label15 = new Label();
            this.lbLocation = new ListBox();
            this.groupBox2 = new GroupBox();
            this.panel1 = new Panel();
            this.label2 = new Label();
            this.txtLocation = new TextBox();
            this.label1 = new Label();
            this.btnDelete = new Button();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.btnAdd.BackColor = Color.Chocolate;
            this.btnAdd.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new Point(0xa5, 0x167);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x4b, 0x17);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "确定";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.lbLocation);
            this.groupBox1.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.groupBox1.Location = new Point(0x4b, 0x3e);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x117, 410);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.panel3.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel3.AutoSize = true;
            this.panel3.BackColor = Color.AliceBlue;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label15);
            this.panel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel3.Location = new Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x117, 0x1d);
            this.panel3.TabIndex = 0x29;
            this.label15.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label15.Location = new Point(0x29, 1);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x89, 0x1a);
            this.label15.TabIndex = 8;
            this.label15.Text = "已定义分站类型";
            this.label15.TextAlign = ContentAlignment.MiddleLeft;
            this.lbLocation.FormattingEnabled = true;
            this.lbLocation.ItemHeight = 14;
            this.lbLocation.Location = new Point(0x19, 0x53);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new Size(0xe1, 0x11c);
            this.lbLocation.TabIndex = 0;
            this.lbLocation.SelectedIndexChanged += new EventHandler(this.lbLocation_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.txtLocation);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.groupBox2.Location = new Point(400, 0x3d);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x1a2, 0x19b);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.panel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel1.AutoSize = true;
            this.panel1.BackColor = Color.AliceBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1a2, 0x1d);
            this.panel1.TabIndex = 0x2a;
            this.label2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new Point(0x29, 1);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xa6, 0x1a);
            this.label2.TabIndex = 8;
            this.label2.Text = "新定义/修改分站类型";
            this.label2.TextAlign = ContentAlignment.MiddleLeft;
            this.txtLocation.Location = new Point(0x97, 80);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new Size(0xca, 0x17);
            this.txtLocation.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0x4e, 0x53);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x43, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "分站类型";
            this.btnDelete.BackColor = Color.Chocolate;
            this.btnDelete.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new Point(0x116, 0x167);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x4b, 0x17);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.groupBox2);
            base.Name = "Substation_type";
            base.Size = new Size(0x3a8, 0x22b);
            base.Load += new EventHandler(this.Substation_type_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void lbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtLocation.Text = (string) this.lbLocation.SelectedItem;
            this.temp = this.txtLocation.Text;
            this.txtLocation.SelectAll();
            this.flag = 1;
        }

        private void Substation_type_Load(object sender, EventArgs e)
        {
            DataTable dt = FenZhanLeiXing.GetFenZhanType();
            foreach (DataRow row in dt.Rows)
            {
                this.lbLocation.Items.Add(row["leiXing"].ToString());
            }
        }
    }
}

