namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Config_Prealarm : UserControl
    {
        private Button button1;
        private Button button2;
        private CeDianSelector ceDianSelector1;
        private ComboBox comboBox1;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private int currentRow = -1;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private ToolStripMenuItem toolStripMenuItem1;

        public Config_Prealarm()
        {
            this.InitializeComponent();
        }

        private bool ArgumentCheck()
        {
            if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择一个测点！");
                this.comboBox1.Focus();
                return false;
            }
            if ((this.textBox1.Text.Trim() == string.Empty) && (this.textBox2.Text.Trim() == string.Empty))
            {
                MessageBox.Show("预警值和变化率至少设置一个！");
                this.textBox1.Focus();
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ArgumentCheck())
            {
                float yuJingZhi;
                float changeRate;
                string cdstr = this.comboBox1.SelectedItem.ToString();
                string cdbh = cdstr.Substring(0, 5);
                int fenZhanHao = 0;
                int tongDaoHao = 0;
                GlobalParams.AllCeDianList.ParseCeDianAllInfo(cdbh, out fenZhanHao, out tongDaoHao);
                try
                {
                    if (this.textBox1.Text.Trim() == string.Empty)
                    {
                        yuJingZhi = 0f;
                    }
                    else
                    {
                        yuJingZhi = Convert.ToSingle(this.textBox1.Text);
                    }
                    if (this.textBox2.Text.Trim() == string.Empty)
                    {
                        changeRate = 0f;
                    }
                    else
                    {
                        changeRate = Convert.ToSingle(this.textBox2.Text);
                    }
                }
                catch
                {
                    MessageBox.Show("你设置的数值不符合要求，请重新输入！");
                    return;
                }
                YuJing yuJing = new YuJing(fenZhanHao, tongDaoHao, yuJingZhi, changeRate);
                if (yuJing.Exist())
                {
                    yuJing.YuJingValue = Convert.ToSingle(this.textBox1.Text);
                    yuJing.ChangeRate = Convert.ToSingle(this.textBox2.Text);
                    yuJing.update();
                }
                else
                {
                    yuJing.InsertIntoDB();
                }
                this.DisplayAllYuJing();
                modifyYJlist(cdbh, true);
                
                MessageBox.Show("预警设置成功！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.comboBox1.ResetText();
            this.textBox1.ResetText();
            this.textBox2.ResetText();
            this.textBox3.ResetText();
        }

        private void ceDianSelector1_Load(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CeDian cedian = new CeDian(this.comboBox1.SelectedItem.ToString().Substring(0, 5));
            if (cedian.MoNiLiang != null)
            {
                this.textBox3.Text = cedian.MoNiLiang.BaoJingZhiShangXian.ToString();
            }
            YuJing yuJing = new YuJing(cedian.FenZhanHao, cedian.TongDaoHao);
            if (yuJing.Exist())
            {
                this.textBox1.Text = yuJing.YuJingValue.ToString();
                this.textBox2.Text = yuJing.ChangeRate.ToString();
            }
            else
            {
                this.textBox1.Text = string.Empty;
                this.textBox2.Text = string.Empty;
            }
        }

        private void Config_Prealarm_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ClientConfig.CreateCommon().get("UserLevel")) != 0)
            {
                MessageBox.Show("你不是超级管理员，无权进行此操作！");
                this.button1.Enabled = false;
                this.button2.Enabled = false;
            }
            else
            {
                this.ceDianSelector1.DisplayList = this.comboBox1;
                this.ceDianSelector1.setCeDianLeiXing(0);
                this.DisplayAllYuJing();
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = this.dataGridView1.HitTest(e.X, e.Y);
            switch (hti.Type)
            {
                case DataGridViewHitTestType.None:
                    this.ContextMenuStrip = null;
                    break;

                case DataGridViewHitTestType.Cell:
                    this.currentRow = hti.RowIndex;
                    this.ContextMenuStrip = this.contextMenuStrip1;
                    break;

                case DataGridViewHitTestType.ColumnHeader:
                    this.ContextMenuStrip = null;
                    break;

                case DataGridViewHitTestType.RowHeader:
                    this.ContextMenuStrip = null;
                    break;

                default:
                    this.ContextMenuStrip = null;
                    break;
            }
        }

        private void DisplayAllYuJing()
        {
            DataTable dt = YuJing.GetAllYuJing();
            if (dt != null)
            {
                this.dataGridView1.DataSource = dt;
                this.dataGridView1.Columns["ceDianBianHao"].HeaderText = "测点编号";
                this.dataGridView1.Columns["ceDianWeiZhi"].HeaderText = "安装地点";
                this.dataGridView1.Columns["xiaoLeiXing"].HeaderText = "名称";
                this.dataGridView1.Columns["baoJingZhiShangXian"].HeaderText = "报警值上限";
                this.dataGridView1.Columns["yuJingZhi"].HeaderText = "预警值";
                this.dataGridView1.Columns["bianHuaZhi"].HeaderText = "变化率";
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ceDianSelector1 = new MAX_CMSS_V2.CeDianSelector();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox1.Location = new System.Drawing.Point(0, 410);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(883, 50);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预警值设置";
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(106, 32);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(114, 23);
            this.textBox3.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "报警值上限";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(579, 37);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(114, 23);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(491, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "变化率";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(334, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(114, 23);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(246, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "预警值";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.ceDianSelector1);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox2.Location = new System.Drawing.Point(0, 311);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(883, 96);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "测点选择";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(18, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "测点";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(78, 55);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(252, 22);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ceDianSelector1
            // 
            this.ceDianSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ceDianSelector1.BackColor = System.Drawing.SystemColors.Control;
            this.ceDianSelector1.CeDianBianHaos = null;
            this.ceDianSelector1.DisplayList = null;
            this.ceDianSelector1.DisplayList2 = null;
            this.ceDianSelector1.Location = new System.Drawing.Point(7, 16);
            this.ceDianSelector1.Name = "ceDianSelector1";
            this.ceDianSelector1.Size = new System.Drawing.Size(870, 42);
            this.ceDianSelector1.TabIndex = 3;
            this.ceDianSelector1.Load += new System.EventHandler(this.ceDianSelector1_Load);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Chocolate;
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(355, 496);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Chocolate;
            this.button2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(485, 496);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.LightGray;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(880, 302);
            this.dataGridView1.TabIndex = 43;
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem1.Text = "删除预警";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // Config_Prealarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Name = "Config_Prealarm";
            this.Size = new System.Drawing.Size(893, 531);
            this.Load += new System.EventHandler(this.Config_Prealarm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要删除该预警？", "预警", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string cdbh=this.dataGridView1[0, this.currentRow].Value.ToString();
                YuJing.DeleteYuJing(cdbh);
                modifyYJlist(cdbh, false);
                this.DisplayAllYuJing();
            }
        }
        /// <summary>
        ///   
        /// </summary>
        /// <param name="cdbh"></param>
        /// <param name="op">  true:  add cdbh   false:  delete cdbh </param>
        private void modifyYJlist(string cdbh,bool op)
        {
            CeDian cedian = GlobalParams.AllCeDianList.getCedianInfo(cdbh);
             if (cedian == null)
                 return;
             if (op)
             {
                 YuJing yuJing = new YuJing(cedian.FenZhanHao, cedian.TongDaoHao);
                 if (cedian.RtVal < cedian.MoNiLiang.BaoJingZhiShangXian)
                 {
                     if(yuJing.Exist()){
                         if (cedian.isAnaYuJing(yuJing.YuJingValue, yuJing.ChangeRate, cedian.RtVal))
                         {
                             string[] a;
                             if (GlobalParams.cedian_alarm.alarm_yujing_Dict.ContainsKey(cdbh))
                             {
                                 a = GlobalParams.cedian_alarm.alarm_yujing_Dict[cedian.CeDianBianHao];
                                 a[2] = Math.Round((double)cedian.RtVal, 2).ToString();
                                 a[3] = cedian.RtState.ToString();
                                 GlobalParams.cedian_alarm.alarm_yujing_Dict[cedian.CeDianBianHao] = a;
                             }
                             else
                             {
                                 a = new string[] { cedian.CeDianWeiZhi + "/" + cedian.XiaoleiXing, cedian.CeDianBianHao, Math.Round((double)cedian.RtVal, 2).ToString(), cedian.RtState.ToString() };
                                 GlobalParams.cedian_alarm.alarm_yujing_Dict.Add(cedian.CeDianBianHao, a);
                             }

                         }
                         else
                         {
                             if (GlobalParams.cedian_alarm.alarm_yujing_Dict.ContainsKey(cedian.CeDianBianHao))
                                 GlobalParams.cedian_alarm.alarm_yujing_Dict.Remove(cedian.CeDianBianHao);

                         }
                     }
                 }
             }
             else
             {
                 if (GlobalParams.cedian_alarm.alarm_yujing_Dict.ContainsKey(cedian.CeDianBianHao))
                     GlobalParams.cedian_alarm.alarm_yujing_Dict.Remove(cedian.CeDianBianHao);

             }

        }
    }
}

