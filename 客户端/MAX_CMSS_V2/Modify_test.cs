namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Modify_test : Form
    {
        private Button btnConfirm;
        private Button button1;
        private CheckBox cbAlarm;
        private ComboBox cbbChannel;
        private ComboBox cbbLoc;
        private ComboBox cbbSub;
        private ComboBox cbbTrans;
        private ComboBox cbbType;
        private string cedianbianhao;
        private IContainer components = null;
        private GroupBox groupBox4;
        private Label label1;
        private Label label15;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private LieBiaoKuang lieBiaoKuang;
        private Panel panel2;

        public Modify_test(LieBiaoKuang leiBiaoKuang, string cedianbianhao)
        {
            this.InitializeComponent();
            this.lieBiaoKuang = leiBiaoKuang;
            this.cedianbianhao = cedianbianhao;
            init();
        }


        private void init()
        {
            CeDian cedian = GlobalParams.AllCeDianList.getCedianInfo(this.cedianbianhao);
            if (cedian != null)
            {
                if (cedian.DaLeiXing == 0)
                    cbAlarm.Visible = true;
                else
                    cbAlarm.Visible = false;
            }

        }
        private bool arguCheck()
        {
            bool ret = true;
            if (this.cbbLoc.SelectedIndex == -1)
            {
                ret = false;
            }
            if (this.TongDao.SelectedIndex == -1)
            {
                ret = false;
            }
            if (this.cbbTrans.SelectedIndex == -1)
            {
                ret = false;
            }
            return ret;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.arguCheck())
            {
                CeDian oldCedian;
                string location = this.cbbLoc.SelectedItem.ToString();
                string tongDao = this.cbbChannel.SelectedItem.ToString();
                string xiaoleixing = this.cbbTrans.SelectedItem.ToString();
                if (tongDao.Length == 1)
                {
                    tongDao = "0" + tongDao;
                }
                string cedianbianhao = this.cedianbianhao.Substring(0, 3) + tongDao;
                if (cedianbianhao[2] != 'C')
                {
                    int chuanGanQiLeiXing = -1;
                    if (cedianbianhao[2] == 'A')
                    {
                        chuanGanQiLeiXing = GlobalParams.AllmnlLeiXing.GetLeiXingByMingCheng(xiaoleixing);
                    }
                    else
                    {
                        chuanGanQiLeiXing = GlobalParams.AllkgLeiXing.GetLeiXingByMingCheng(xiaoleixing);
                    }
                    oldCedian = GlobalParams.AllCeDianList.allcedianlist[this.cedianbianhao];
                    bool isbj = this.cbAlarm.Checked;
                    GlobalParams.AllCeDianList.UpdateCeDian(location, xiaoleixing, chuanGanQiLeiXing.ToString().Trim(), oldCedian.FenZhanHao.ToString().Trim(), tongDao.ToString().Trim(), cedianbianhao, oldCedian.ChuanGanQiZhiShi, oldCedian.CeDianBianHao, isbj);
                    OperateDBAccess.Execute("update LieBiaoAndCeDian set ceDianBianHao='" + cedianbianhao + "' where ceDianBianHao='" + this.cedianbianhao + "'");
                }
                else
                {
                    oldCedian = GlobalParams.AllCeDianList.allcedianlist[this.cedianbianhao];
                    GlobalParams.AllCeDianList.UpdateCeDianKzl(location, cedianbianhao, tongDao, xiaoleixing, this.cedianbianhao);
                    OperateDBAccess.Execute("update LieBiaoAndCeDian set ceDianBianHao='" + cedianbianhao + "' where ceDianBianHao='" + this.cedianbianhao + "'");
                }
                string type = string.Empty;
                if (cedianbianhao[2] == 'A')
                {
                    type = "模拟量";
                }
                else if (cedianbianhao[2] == 'D')
                {
                    type = "开关量";
                }
                else
                {
                    type = "控制量";
                }
                string weizhi = location;
                string fenzhan = cedianbianhao.Substring(0, 2);
                string tongdao = cedianbianhao.Substring(3, 2);
                Log.WriteLog(LogType.CeDianPeiZhi, "修改测点#$" + type + "#$" + cedianbianhao + "#$" + weizhi + "#$" + fenzhan + "#$" + tongdao);
                MainFormRef.updateMainForm();
                GlobalParams.setDataState();
                MessageBox.Show("修改测点成功！");
                base.Close();
            }
            else
            {
                MessageBox.Show("测点的安装地点、类型或通道不能为空！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void cbbChannel_DropDown(object sender, EventArgs e)
        {
            string[] list;
            this.cbbChannel.Items.Clear();
            if (this.cedianbianhao[2] != 'C')
            {
                list = new string[0x10];
            }
            else
            {
                list = new string[8];
            }
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = (i + 1).ToString();
            }
            this.cbbChannel.Items.AddRange(list);
            if (this.cbbSub.SelectedIndex != -1)
            {
                DataTable dt;
                if (this.cedianbianhao[2] != 'C')
                {
                    dt = CeDian.GetChannel(this.cbbSub.SelectedItem.ToString());
                    foreach (DataRow row in dt.Rows)
                    {
                        this.cbbChannel.Items.Remove(row["tongDaoHao"].ToString());
                    }
                }
                else
                {
                    dt = CeDian.GetKongChannel(this.cbbSub.SelectedItem.ToString());
                    foreach (DataRow row in dt.Rows)
                    {
                        this.cbbChannel.Items.Remove(row["kongZhiLiangBianHao"].ToString());
                    }
                }
                this.cbbChannel.Items.Add(Convert.ToInt32(this.cedianbianhao.Substring(3)).ToString());
            }
        }

        private void cbbTrans_DropDown(object sender, EventArgs e)
        {
            string[] leixing;
            this.cbbTrans.Items.Clear();
            if (this.cedianbianhao[2] == 'A')
            {
                leixing = MoNiLiangLeiXing.GetAllMingCheng();
            }
            else if (this.cedianbianhao[2] == 'D')
            {
                leixing = GlobalParams.AllkgLeiXing.GetAllMingCheng();
            }
            else
            {
                leixing = GlobalParams.AllkzlLeiXing.GetAllMingCheng();
            }
            this.cbbTrans.Items.AddRange(leixing);
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbAlarm = new System.Windows.Forms.CheckBox();
            this.cbbChannel = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbSub = new System.Windows.Forms.ComboBox();
            this.cbbTrans = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbType = new System.Windows.Forms.ComboBox();
            this.cbbLoc = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Chocolate;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(393, 354);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 26);
            this.button1.TabIndex = 19;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.Chocolate;
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(277, 354);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(86, 26);
            this.btnConfirm.TabIndex = 18;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbAlarm);
            this.groupBox4.Controls.Add(this.cbbChannel);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.cbbSub);
            this.groupBox4.Controls.Add(this.cbbTrans);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.cbbType);
            this.groupBox4.Controls.Add(this.cbbLoc);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(44, 86);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(430, 249);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            // 
            // cbAlarm
            // 
            this.cbAlarm.AutoSize = true;
            this.cbAlarm.Checked = true;
            this.cbAlarm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAlarm.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbAlarm.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.cbAlarm.Location = new System.Drawing.Point(319, 212);
            this.cbAlarm.Name = "cbAlarm";
            this.cbAlarm.Size = new System.Drawing.Size(86, 18);
            this.cbAlarm.TabIndex = 9;
            this.cbAlarm.Text = "是否报警";
            this.cbAlarm.UseVisualStyleBackColor = true;
            // 
            // cbbChannel
            // 
            this.cbbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbChannel.FormattingEnabled = true;
            this.cbbChannel.Location = new System.Drawing.Point(130, 209);
            this.cbbChannel.Name = "cbbChannel";
            this.cbbChannel.Size = new System.Drawing.Size(164, 24);
            this.cbbChannel.TabIndex = 8;
            this.cbbChannel.DropDown += new System.EventHandler(this.cbbChannel_DropDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label5.Location = new System.Drawing.Point(61, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "通道号";
            // 
            // cbbSub
            // 
            this.cbbSub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSub.Enabled = false;
            this.cbbSub.FormattingEnabled = true;
            this.cbbSub.Location = new System.Drawing.Point(130, 163);
            this.cbbSub.Name = "cbbSub";
            this.cbbSub.Size = new System.Drawing.Size(270, 24);
            this.cbbSub.TabIndex = 6;
            // 
            // cbbTrans
            // 
            this.cbbTrans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTrans.FormattingEnabled = true;
            this.cbbTrans.Location = new System.Drawing.Point(130, 117);
            this.cbbTrans.Name = "cbbTrans";
            this.cbbTrans.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbbTrans.Size = new System.Drawing.Size(270, 24);
            this.cbbTrans.TabIndex = 6;
            this.cbbTrans.DropDown += new System.EventHandler(this.cbbTrans_DropDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new System.Drawing.Point(76, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "分站";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(31, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "新测点类型";
            // 
            // cbbType
            // 
            this.cbbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbType.FormattingEnabled = true;
            this.cbbType.Location = new System.Drawing.Point(130, 71);
            this.cbbType.Name = "cbbType";
            this.cbbType.Size = new System.Drawing.Size(270, 24);
            this.cbbType.TabIndex = 4;
            // 
            // cbbLoc
            // 
            this.cbbLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbLoc.FormattingEnabled = true;
            this.cbbLoc.Location = new System.Drawing.Point(130, 25);
            this.cbbLoc.Name = "cbbLoc";
            this.cbbLoc.Size = new System.Drawing.Size(270, 24);
            this.cbbLoc.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(46, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "测点类型";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(46, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "测点位置";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSize = true;
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label15);
            this.panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel2.Location = new System.Drawing.Point(0, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(553, 33);
            this.panel2.TabIndex = 39;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label15.Location = new System.Drawing.Point(49, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(162, 30);
            this.label15.TabIndex = 8;
            this.label15.Text = "测点基本信息";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Modify_test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 408);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.groupBox4);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Modify_test";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改测点";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public ComboBox AnZhuangDiDian
        {
            get
            {
                return this.cbbLoc;
            }
        }

        public CheckBox BaoJing
        {
            get
            {
                return this.cbAlarm;
            }
        }

        public string CeDianBianHao
        {
            get
            {
                return this.cedianbianhao;
            }
            set
            {
                this.cedianbianhao = value;
            }
        }

        public ComboBox CeDianLeiXing
        {
            get
            {
                return this.cbbType;
            }
        }

        public ComboBox ChuangGanQiLeiXing
        {
            get
            {
                return this.cbbTrans;
            }
        }

        public ComboBox FenZhanHao
        {
            get
            {
                return this.cbbSub;
            }
        }

        public ComboBox TongDao
        {
            get
            {
                return this.cbbChannel;
            }
        }
    }
}

