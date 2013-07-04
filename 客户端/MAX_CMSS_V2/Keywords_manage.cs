namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public class Keywords_manage : UserControl
    {
        private Button btnAdd;
        private Button btnDelete;
        private IContainer components = null;
        private byte flag = 0;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label3;
        private Label label4;
        private ListBox lbLocation;
        private Panel panel1;
        private Panel panel5;
        private string temp;
        private TextBox txtLocation;

        public Keywords_manage()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.txtLocation.Text.Length == 0)
            {
                MessageBox.Show("请输入关键字");
            }
            else
            {
                IEnumerable<string> query;
                if (this.flag == 0)
                {
                    if (GlobalParams.AllKeyWord.IsExist(this.txtLocation.Text.ToString().Trim()))
                    {
                        MessageBox.Show("该关键字名称已被使用，请重新命名");
                        this.txtLocation.Focus();
                    }
                    else
                    {
                        GlobalParams.AllKeyWord.CreateKey(this.txtLocation.Text.ToString().Trim());
                        this.lbLocation.Items.Clear();
                        query = from item in GlobalParams.AllKeyWord.all_keyword select item;
                        foreach (string item in query)
                        {
                            this.lbLocation.Items.Add(item.Trim());
                        }
                        this.txtLocation.Clear();
                    }
                }
                else if (this.flag == 1)
                {
                    if ((this.txtLocation.Text != this.temp) && GlobalParams.AllKeyWord.IsExist(this.txtLocation.Text.ToString().Trim()))
                    {
                        MessageBox.Show("该关键字名称已被使用，请重新命名");
                        this.txtLocation.Focus();
                    }
                    else
                    {
                        GlobalParams.AllKeyWord.UpdateKey(this.txtLocation.Text.ToString().Trim(), this.temp.Trim());
                        this.lbLocation.Items.Clear();
                        query = from item in GlobalParams.AllKeyWord.all_keyword select item;
                        foreach (string item in query)
                        {
                            this.lbLocation.Items.Add(item.Trim());
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
                MessageBox.Show("请选择要删除的关键字");
                this.lbLocation.Focus();
            }
            else if (this.flag == 1)
            {
                if (GlobalParams.AllKeyWord.IsGuanLianLeiXing(this.temp))
                {
                    GlobalParams.AllKeyWord.DeleteKey(this.txtLocation.Text.ToString().Trim());
                    this.lbLocation.Items.Clear();
                    IEnumerable<string> query = from item in GlobalParams.AllKeyWord.all_keyword select item;
                    foreach (string item in query)
                    {
                        this.lbLocation.Items.Add(item.Trim());
                    }
                    this.txtLocation.Clear();
                }
                else
                {
                    MessageBox.Show("关键字已被引用,请解除引用再删除");
                }
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
            this.txtLocation = new TextBox();
            this.label1 = new Label();
            this.btnDelete = new Button();
            this.lbLocation = new ListBox();
            this.groupBox1 = new GroupBox();
            this.panel5 = new Panel();
            this.label3 = new Label();
            this.groupBox2 = new GroupBox();
            this.panel1 = new Panel();
            this.label4 = new Label();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.btnAdd.BackColor = Color.Chocolate;
            this.btnAdd.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnAdd.ForeColor = SystemColors.ButtonFace;
            this.btnAdd.Location = new Point(0xa5, 0x167);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x4b, 0x17);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "确定";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.txtLocation.BackColor = Color.White;
            this.txtLocation.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.txtLocation.Location = new Point(0x88, 0x52);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new Size(0xca, 0x15);
            this.txtLocation.TabIndex = 1;
            this.txtLocation.TextChanged += new EventHandler(this.txtLocation_TextChanged);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new Point(0x4e, 0x53);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x34, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "关键字";
            this.btnDelete.BackColor = Color.Chocolate;
            this.btnDelete.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnDelete.ForeColor = SystemColors.ButtonFace;
            this.btnDelete.Location = new Point(0x116, 0x167);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x4b, 0x17);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.lbLocation.BackColor = Color.White;
            this.lbLocation.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lbLocation.FormattingEnabled = true;
            this.lbLocation.ItemHeight = 12;
            this.lbLocation.Location = new Point(0x1b, 0x5f);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new Size(0xe1, 0x124);
            this.lbLocation.TabIndex = 0;
            this.lbLocation.SelectedIndexChanged += new EventHandler(this.lbLocation_SelectedIndexChanged);
            this.groupBox1.Controls.Add(this.lbLocation);
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.groupBox1.Location = new Point(0x4b, 0x3e);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x117, 410);
            this.groupBox1.TabIndex = 0x12;
            this.groupBox1.TabStop = false;
            this.panel5.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel5.AutoSize = true;
            this.panel5.BackColor = Color.AliceBlue;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label3);
            this.panel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel5.Location = new Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new Size(0x117, 0x1d);
            this.panel5.TabIndex = 0x29;
            this.label3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new Point(0x29, 1);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x89, 0x1a);
            this.label3.TabIndex = 8;
            this.label3.Text = "关键字列表";
            this.label3.TextAlign = ContentAlignment.MiddleLeft;
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.txtLocation);
            this.groupBox2.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.groupBox2.Location = new Point(400, 0x3d);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x1a2, 0x19b);
            this.groupBox2.TabIndex = 0x13;
            this.groupBox2.TabStop = false;
            this.panel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel1.AutoSize = true;
            this.panel1.BackColor = Color.AliceBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label4);
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1a2, 0x1d);
            this.panel1.TabIndex = 0x2a;
            this.label4.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new Point(0x29, 1);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0xa6, 0x1a);
            this.label4.TabIndex = 8;
            this.label4.Text = "新定义/修改关键字";
            this.label4.TextAlign = ContentAlignment.MiddleLeft;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "Keywords_manage";
            base.Size = new Size(0x3a5, 0x22c);
            base.Load += new EventHandler(this.Keywords_manage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void Keywords_manage_Load(object sender, EventArgs e)
        {
            DataTable dt = KeyWords.GetKey();
            foreach (DataRow row in dt.Rows)
            {
                this.lbLocation.Items.Add(row["guanJianZi"].ToString());
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void lbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtLocation.Text = (string) this.lbLocation.SelectedItem;
            this.temp = this.txtLocation.Text;
            this.txtLocation.SelectAll();
            this.flag = 1;
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
        }
    }
}

