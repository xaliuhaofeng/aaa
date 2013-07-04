namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Config_area : UserControl
    {
        private Button btnAdd;
        private Button btnDelete;
        private IContainer components = null;
        private int flag = 0;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private ListBox lbLocation;
        private string temp;
        private TextBox txtLocation;

        public Config_area()
        {
            this.InitializeComponent();
        }


        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.txtLocation.Text.Length == 0)
            {
                MessageBox.Show("请输入安装地点名称");
            }
            else
            {
                DataTable dt;
                if (this.flag == 0)
                {
                    if (OperateDB.Select(InstallationSite.CountLocation(this.txtLocation.Text)).Rows.Count > 0)
                    {
                        MessageBox.Show("该地点名称已被使用，请重新命名");
                        this.txtLocation.Focus();
                    }
                    else
                    {
                        OperateDB.Execute(new InstallationSite(this.txtLocation.Text).CreateLocation());
                        this.lbLocation.Items.Clear();
                        dt = InstallationSite.GetAllLocation();
                        foreach (DataRow row in dt.Rows)
                        {
                            this.lbLocation.Items.Add(row["DiDian"].ToString());
                        }
                        this.txtLocation.Clear();

                        GlobalParams.setDataState();
                    }
                }
                else if (this.flag == 1)
                {
                    if ((this.txtLocation.Text != this.temp) && (OperateDB.Select(InstallationSite.CountLocation(this.txtLocation.Text)).Rows.Count > 0))
                    {
                        MessageBox.Show("该地点名称已被使用，请重新命名");
                        this.txtLocation.Focus();
                    }
                    else
                    {
                        OperateDB.Execute(new InstallationSite(this.txtLocation.Text).UpdateLocation(this.lbLocation.Text));
                        OperateDB.Execute(CeDian.UpdateCeDianWeiZhi(this.txtLocation.Text, this.lbLocation.Text));
                        this.lbLocation.Items.Clear();
                        dt = InstallationSite.GetAllLocation();
                        foreach (DataRow row in dt.Rows)
                        {
                            this.lbLocation.Items.Add(row["DiDian"].ToString());
                        }
                        this.txtLocation.Clear();
                        this.flag = 0;
                        GlobalParams.setDataState();
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.flag == 0)
            {
                MessageBox.Show("请选择要删除的位置");
                this.lbLocation.Focus();
            }
            else if (this.flag == 1)
            {
                if (OperateDB.Select(CeDian.CountCeDian2((string) this.lbLocation.SelectedItem)).Rows.Count > 0)
                {
                    MessageBox.Show("无法删除该安装地点，存在测点关联");
                }
                else
                {
                    OperateDB.Execute(new InstallationSite().DeleteLocation(this.lbLocation.Text));
                    this.lbLocation.Items.Clear();
                    DataTable dt = InstallationSite.GetAllLocation();
                    foreach (DataRow row in dt.Rows)
                    {
                        this.lbLocation.Items.Add(row["DiDian"].ToString());
                    }
                    this.txtLocation.Clear();
                    GlobalParams.setDataState();
                }
            }
            this.flag = 0;
        }

        private void Config_area_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < GlobalParams.allarea.m_AllAddr.Count; i++)
            {
                this.lbLocation.Items.Add(GlobalParams.allarea.m_AllAddr[i]);
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbLocation = new System.Windows.Forms.ListBox();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.Control;
            this.btnAdd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAdd.Location = new System.Drawing.Point(120, 312);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 38);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "确定";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbLocation);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(34, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 410);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "已定义位置";
            // 
            // lbLocation
            // 
            this.lbLocation.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLocation.FormattingEnabled = true;
            this.lbLocation.ItemHeight = 16;
            this.lbLocation.Location = new System.Drawing.Point(6, 31);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(319, 356);
            this.lbLocation.TabIndex = 0;
            this.lbLocation.SelectedIndexChanged += new System.EventHandler(this.lbLocation_SelectedIndexChanged);
            // 
            // txtLocation
            // 
            this.txtLocation.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLocation.Location = new System.Drawing.Point(57, 164);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(348, 35);
            this.txtLocation.TabIndex = 1;
            this.txtLocation.TextChanged += new System.EventHandler(this.txtLocation_TextChanged);
            this.txtLocation.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocation_Validating);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.txtLocation);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(425, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(447, 410);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "新定义/修改位置";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDelete.Location = new System.Drawing.Point(233, 312);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(87, 38);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(64, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "位置";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Config_area
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Config_area";
            this.Size = new System.Drawing.Size(919, 549);
            this.Load += new System.EventHandler(this.Config_area_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

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

        private void txtLocation_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtLocation.TextLength > 40)
            {
                MessageBox.Show("字符长度不得超过40个字节");
                e.Cancel = true;
                this.txtLocation.Focus();
            }
        }
    }
}

