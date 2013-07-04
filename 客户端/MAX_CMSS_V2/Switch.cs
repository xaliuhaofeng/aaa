namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Media;
    using System.Windows.Forms;

    public class Switch : UserControl
    {
        private DataGridViewTextBoxColumn baoJingShengYin;
        private DataGridViewTextBoxColumn baoJingZhuangTai;
        private Button btnClear;
        private Button btnConfirm;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn duanDianZhuangTai;
        private DataGridViewTextBoxColumn erTaiMingCheng;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private DataGridViewTextBoxColumn lingTaiMingCheng;
        private MainForm mainForm;
        private DataGridViewTextBoxColumn mingCheng;
        private PictureBox pictureBox1;
        private SoundPlayer player = new SoundPlayer();
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton5;
        private RadioButton radioButton6;
        private RadioButton rbAlarmStatus2;
        private RadioButton rbCutStatus2;
        private DataGridViewCheckBoxColumn shiFouBaoJing;
        private DataGridViewCheckBoxColumn shiFouDuanDian;
        private byte switchtype = 0;
        private string temp = "";
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private DataGridViewImageColumn TuBiao;
        private TextBox txtStatus2;
        private DataGridViewTextBoxColumn yiTaiMingCheng;
        private DataGridViewTextBoxColumn zhengChangTuBiao;

        public Switch(byte type, MainForm form)
        {
            this.InitializeComponent();
            this.switchtype = type;
            if (type == 4)
            {
                this.txtStatus2.Enabled = true;
                this.rbAlarmStatus2.Enabled = true;
                this.rbCutStatus2.Enabled = true;
            }
            else if (type == 8)
            {
                this.groupBox3.Visible = false;
            }
            this.mainForm = form;
        }

        private bool argCheck()
        {
            if (this.textBox3.Text.Length == 0)
            {
                MessageBox.Show("请输入开关量名称");
                return false;
            }
            if (this.textBox1.Text.Length == 0)
            {
                MessageBox.Show("请输入0态名称");
                return false;
            }
            if (this.textBox2.Text.Length == 0)
            {
                MessageBox.Show("请输入1态名称");
                return false;
            }
            if ((this.txtStatus2.Text.Length == 0) && (this.switchtype == 1))
            {
                MessageBox.Show("请输入2态名称");
                return false;
            }
            return true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.textBox3.Clear();
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.txtStatus2.Clear();
            this.pictureBox1.Image = null;
            this.checkBox2.Checked = false;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.argCheck())
            {
                if (GlobalParams.AllkgLeiXing.IsExistKgl(this.textBox3.Text.ToString().Trim()))
                {
                    MessageBox.Show("该开关量名称已存在，请重新命名");
                    this.textBox3.Focus();
                }
                else
                {
                    string toPath = "";
                    if ((this.pictureBox1.ImageLocation != null) && (this.pictureBox1.ImageLocation != ""))
                    {
                        toPath = Application.StartupPath + @"\monitor\" + Path.GetFileName(this.pictureBox1.ImageLocation);
                        try
                        {
                            File.Copy(this.pictureBox1.ImageLocation, toPath, true);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    byte temp1 = 0;
                    byte temp2 = 0;
                    if (this.radioButton1.Checked)
                    {
                        temp1 = 0;
                    }
                    else if (this.radioButton2.Checked)
                    {
                        temp1 = 1;
                    }
                    else if (this.rbAlarmStatus2.Checked)
                    {
                        temp1 = 2;
                    }
                    if (this.radioButton6.Checked)
                    {
                        temp2 = 0;
                    }
                    else if (this.radioButton5.Checked)
                    {
                        temp2 = 1;
                    }
                    else if (this.rbCutStatus2.Checked)
                    {
                        temp2 = 2;
                    }
                    if (GlobalParams.AllkgLeiXing.InsertKglLx(this.switchtype, this.pictureBox1.ImageLocation, this.textBox3.Text, this.textBox1.Text, this.textBox2.Text, this.txtStatus2.Text, temp1.ToString(), this.checkBox1.Checked.ToString(), this.checkBox2.Checked.ToString(), temp2.ToString()))
                    {
                        Log.WriteLog(LogType.KaiGuanLiang, string.Concat(new object[] { "添加开关量#$", this.textBox3.Text, "#$", this.textBox1.Text, "#$", this.textBox2.Text, "#$", this.txtStatus2.Text, "#$", temp2, "态" }));
                    }
                    this.dataGridView1.Rows.Clear();
                    this.initDataGridView();
                    this.textBox3.Clear();
                    this.textBox1.Clear();
                    this.textBox2.Clear();
                    this.txtStatus2.Clear();
                    this.pictureBox1.Image = null;
                    GlobalParams.setDataState();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog {
                CheckFileExists = true,
                Filter = "WAV files (*.wav)|*.wav",
                DefaultExt = ".wav",
                Title = "打开声音文件"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.player.SoundLocation = dlg.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.player.SoundLocation.Length < 2)
            {
                MessageBox.Show("请选择声音文件");
            }
            else if (this.button2.Text == "试听")
            {
                this.player.Play();
                this.button2.Text = "停止";
            }
            else
            {
                this.button2.Text = "试听";
                this.player.Stop();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((this.dataGridView1.CurrentRow != null) && (MessageBox.Show("你确认要删除这个开关量类型吗？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                if (GlobalParams.AllCeDianList.CanDelete(this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString().Trim()))
                {
                    MessageBox.Show("存在该类型的测点，该类型不能被删除");
                }
                else
                {
                    if (GlobalParams.AllkgLeiXing.DeleteKglLx(this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString().Trim()))
                    {
                        Log.WriteLog(LogType.KaiGuanLiang, "删除开关量#$" + this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString());
                    }
                    this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentRow);
                    this.textBox3.Clear();
                    this.textBox1.Clear();
                    this.textBox2.Clear();
                    this.txtStatus2.Clear();
                    this.pictureBox1.Image = null;
                    GlobalParams.setDataState();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            base.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgP = new OpenFileDialog {
                CheckFileExists = true,
                Filter = "jpg files (*.jpg)|*.jpg",
                DefaultExt = ".jpg",
                Title = "打开图片文件"
            };
            if (dlgP.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.Image = Image.FromFile(dlgP.FileName);
                this.pictureBox1.ImageLocation = dlgP.FileName;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.argCheck())
            {
                if ((this.textBox3.Text != this.temp) && GlobalParams.AllkgLeiXing.IsExistKgl(this.textBox3.Text.ToString().Trim()))
                {
                    MessageBox.Show("该开关量名称已存在，请重新命名");
                    this.textBox3.Focus();
                }
                else
                {
                    byte temp1 = 0;
                    byte temp2 = 0;
                    if (this.radioButton1.Checked)
                    {
                        temp1 = 0;
                    }
                    else if (this.radioButton2.Checked)
                    {
                        temp1 = 1;
                    }
                    else if (this.rbAlarmStatus2.Checked)
                    {
                        temp1 = 2;
                    }
                    if (this.radioButton6.Checked)
                    {
                        temp2 = 0;
                    }
                    else if (this.radioButton5.Checked)
                    {
                        temp2 = 1;
                    }
                    else if (this.rbCutStatus2.Checked)
                    {
                        temp2 = 2;
                    }
                    if (GlobalParams.AllkgLeiXing.XiuGaiLX(this.pictureBox1.ImageLocation, this.textBox3.Text, this.textBox1.Text, this.textBox2.Text, this.txtStatus2.Text, temp1.ToString(), this.checkBox1.Checked.ToString(), this.checkBox2.Checked.ToString(), temp2.ToString(), this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString()))
                    {
                        Log.WriteLog(LogType.KaiGuanLiang, string.Concat(new object[] { "修改开关量#$", this.textBox3.Text, "#$", this.textBox1.Text, "#$", this.textBox2.Text, "#$", this.txtStatus2.Text, "#$", temp2, "态" }));
                    }
                    KaiGuanLiangLeiXing kaiGuanLiang = new KaiGuanLiangLeiXing(this.textBox3.Text);
                    for (int i = 0; i < GlobalParams.AllCeDianList.allcedianlist.Count; i++)
                    {
                        CeDian cedian = GlobalParams.AllCeDianList.allcedianlist.ElementAt<KeyValuePair<string, CeDian>>(i).Value;
                        if ((cedian.KaiGuanLiang != null) && (cedian.KaiGuanLiang.MingCheng == this.textBox3.Text))
                        {
                            cedian.KaiGuanLiang = kaiGuanLiang;
                        }
                    }
                    foreach (List_show show in this.mainForm.yeKuangs)
                    {
                        show.updateAllLieBiaoKuang();
                    }
                    this.dataGridView1.Rows.Clear();
                    this.initDataGridView();
                    this.textBox3.Clear();
                    this.textBox1.Clear();
                    this.textBox2.Clear();
                    this.txtStatus2.Clear();
                    this.pictureBox1.Image = null;
                    GlobalParams.setDataState();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.radioButton1.Enabled = this.checkBox1.Checked;
            this.radioButton2.Enabled = this.checkBox1.Checked;
            this.rbAlarmStatus2.Enabled = this.checkBox1.Checked && (this.switchtype == 4);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.radioButton5.Enabled = this.checkBox2.Checked;
            this.radioButton6.Enabled = this.checkBox2.Checked;
            this.rbCutStatus2.Enabled = this.checkBox2.Checked && (this.switchtype == 4);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Rows.Count != 0)
            {
                this.textBox3.Text = this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString();
                this.textBox1.Text = this.dataGridView1.CurrentRow.Cells["lingTaiMingCheng"].Value.ToString();
                this.textBox2.Text = this.dataGridView1.CurrentRow.Cells["yiTaiMingCheng"].Value.ToString();
                this.checkBox1.Checked = (bool) this.dataGridView1.CurrentRow.Cells["shiFouBaoJing"].Value;
                if (int.Parse(this.dataGridView1.CurrentRow.Cells["baoJingZhuangTai"].Value.ToString().Trim()) == 0)
                {
                    this.radioButton1.Checked = true;
                }
                else if (int.Parse(this.dataGridView1.CurrentRow.Cells["baoJingZhuangTai"].Value.ToString().Trim()) == 1)
                {
                    this.radioButton2.Checked = true;
                }
                else if (int.Parse(this.dataGridView1.CurrentRow.Cells["baoJingZhuangTai"].Value.ToString().Trim()) == 2)
                {
                    this.rbAlarmStatus2.Checked = true;
                }
                if (this.switchtype != 8)
                {
                    this.checkBox2.Checked = (bool) this.dataGridView1.CurrentRow.Cells["shiFouDuanDian"].Value;
                    if (short.Parse(this.dataGridView1.CurrentRow.Cells["duanDianZhuangTai"].Value.ToString().Trim()) == 0)
                    {
                        this.radioButton6.Checked = true;
                    }
                    else if (short.Parse(this.dataGridView1.CurrentRow.Cells["duanDianZhuangTai"].Value.ToString().Trim()) == 1)
                    {
                        this.radioButton5.Checked = true;
                    }
                    else if (short.Parse(this.dataGridView1.CurrentRow.Cells["duanDianZhuangTai"].Value.ToString().Trim()) == 2)
                    {
                        this.rbCutStatus2.Checked = true;
                    }
                }
                if (this.switchtype == 4)
                {
                    this.txtStatus2.Text = this.dataGridView1.CurrentRow.Cells["erTaiMingCheng"].Value.ToString();
                }
                this.pictureBox1.ImageLocation = this.dataGridView1.CurrentRow.Cells["zhengChangTuBiao"].Value.ToString();
                this.temp = this.textBox3.Text;
                if (File.Exists(this.pictureBox1.ImageLocation))
                {
                    this.pictureBox1.Image = Image.FromFile(this.pictureBox1.ImageLocation);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void initDataGridView()
        {
            List<KaiGuanLiangLeiXing> kgllist = GlobalParams.AllkgLeiXing.GetSwitch(this.switchtype);
            if (kgllist.Count != 0)
            {
                foreach (KaiGuanLiangLeiXing item in kgllist)
                {
                    int index2 = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index2].Cells["zhengChangTuBiao"].Value = item.ZhengChangTuBiao.Trim();
                    this.dataGridView1.Rows[index2].Cells["mingCheng"].Value = item.MingCheng.Trim();
                    this.dataGridView1.Rows[index2].Cells["lingTaiMingCheng"].Value = item.LingTai.Trim();
                    this.dataGridView1.Rows[index2].Cells["yiTaiMingCheng"].Value = item.YiTai.Trim();
                    if (this.switchtype == 4)
                    {
                        this.dataGridView1.Rows[index2].Cells["erTaiMingCheng"].Value = item.ErTai.Trim();
                        this.dataGridView1.Columns["erTaiMingCheng"].Visible = true;
                    }
                    this.dataGridView1.Rows[index2].Cells["shiFouBaoJing"].Value = item.ShiFouBaoJing;
                    this.dataGridView1.Rows[index2].Cells["baoJingZhuangTai"].Value = item.BaoJingZhuangTai;
                    if (this.switchtype != 8)
                    {
                        this.dataGridView1.Columns["shiFouDuanDian"].Visible = true;
                        this.dataGridView1.Columns["duanDianZhuangTai"].Visible = true;
                        this.dataGridView1.Rows[index2].Cells["shiFouDuanDian"].Value = item.ShiFouDuanDian;
                        this.dataGridView1.Rows[index2].Cells["duanDianZhuangTai"].Value = item.DuanDianZhuangTai;
                    }
                    this.dataGridView1.Rows[index2].Cells["baoJingShengYin"].Value = item.BaoJingShengYin;
                    if (this.dataGridView1.Rows[index2].Cells["zhengChangTuBiao"].Value.ToString().Length != 0)
                    {
                        this.dataGridView1.Rows[index2].Cells["TuBiao"].Value = Image.FromFile(this.dataGridView1.Rows[index2].Cells["zhengChangTuBiao"].Value.ToString());
                    }
                    else
                    {
                        ClientConfig config = ClientConfig.CreateCommon();
                        string filePath = string.Empty;
                        if (this.switchtype == 3)
                        {
                            filePath = config.get("LiangTaiKaiGuanLiang");
                        }
                        else if (this.switchtype == 4)
                        {
                            filePath = config.get("SanTaiKaiGuanLiang");
                        }
                        else if (this.switchtype == 7)
                        {
                            filePath = config.get("TongDuanLiang");
                        }
                        else if (this.switchtype == 8)
                        {
                            filePath = config.get("FenZhanLiang");
                        }
                        if (filePath != null)
                        {
                            filePath = Application.StartupPath + @"\monitor\" + filePath;
                            this.dataGridView1.Rows[index2].Cells["TuBiao"].Value = Image.FromFile(filePath);
                        }
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.TuBiao = new System.Windows.Forms.DataGridViewImageColumn();
            this.zhengChangTuBiao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mingCheng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lingTaiMingCheng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yiTaiMingCheng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erTaiMingCheng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shiFouDuanDian = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.duanDianZhuangTai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shiFouBaoJing = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.baoJingZhuangTai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baoJingShengYin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStatus2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.rbAlarmStatus2 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbCutStatus2 = new System.Windows.Forms.RadioButton();
            this.button4 = new System.Windows.Forms.Button();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TuBiao,
            this.zhengChangTuBiao,
            this.mingCheng,
            this.lingTaiMingCheng,
            this.yiTaiMingCheng,
            this.erTaiMingCheng,
            this.shiFouDuanDian,
            this.duanDianZhuangTai,
            this.shiFouBaoJing,
            this.baoJingZhuangTai,
            this.baoJingShengYin});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1040, 286);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // TuBiao
            // 
            this.TuBiao.HeaderText = "图标";
            this.TuBiao.Name = "TuBiao";
            this.TuBiao.ReadOnly = true;
            // 
            // zhengChangTuBiao
            // 
            this.zhengChangTuBiao.HeaderText = "正常图标";
            this.zhengChangTuBiao.Name = "zhengChangTuBiao";
            this.zhengChangTuBiao.ReadOnly = true;
            this.zhengChangTuBiao.Visible = false;
            // 
            // mingCheng
            // 
            this.mingCheng.HeaderText = "名称";
            this.mingCheng.Name = "mingCheng";
            this.mingCheng.ReadOnly = true;
            // 
            // lingTaiMingCheng
            // 
            this.lingTaiMingCheng.HeaderText = "0态名称";
            this.lingTaiMingCheng.Name = "lingTaiMingCheng";
            this.lingTaiMingCheng.ReadOnly = true;
            // 
            // yiTaiMingCheng
            // 
            this.yiTaiMingCheng.HeaderText = "1态名称";
            this.yiTaiMingCheng.Name = "yiTaiMingCheng";
            this.yiTaiMingCheng.ReadOnly = true;
            // 
            // erTaiMingCheng
            // 
            this.erTaiMingCheng.HeaderText = "2态名称";
            this.erTaiMingCheng.Name = "erTaiMingCheng";
            this.erTaiMingCheng.ReadOnly = true;
            this.erTaiMingCheng.Visible = false;
            // 
            // shiFouDuanDian
            // 
            this.shiFouDuanDian.HeaderText = "是否断电";
            this.shiFouDuanDian.Name = "shiFouDuanDian";
            this.shiFouDuanDian.ReadOnly = true;
            this.shiFouDuanDian.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.shiFouDuanDian.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.shiFouDuanDian.Visible = false;
            // 
            // duanDianZhuangTai
            // 
            this.duanDianZhuangTai.HeaderText = "断电状态";
            this.duanDianZhuangTai.Name = "duanDianZhuangTai";
            this.duanDianZhuangTai.ReadOnly = true;
            this.duanDianZhuangTai.Visible = false;
            // 
            // shiFouBaoJing
            // 
            this.shiFouBaoJing.HeaderText = "是否报警";
            this.shiFouBaoJing.Name = "shiFouBaoJing";
            this.shiFouBaoJing.ReadOnly = true;
            this.shiFouBaoJing.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.shiFouBaoJing.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // baoJingZhuangTai
            // 
            this.baoJingZhuangTai.HeaderText = "报警状态";
            this.baoJingZhuangTai.Name = "baoJingZhuangTai";
            this.baoJingZhuangTai.ReadOnly = true;
            // 
            // baoJingShengYin
            // 
            this.baoJingShengYin.HeaderText = "报警声音";
            this.baoJingShengYin.Name = "baoJingShengYin";
            this.baoJingShengYin.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtStatus2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(28, 306);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(423, 311);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(285, 258);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(97, 31);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Location = new System.Drawing.Point(122, 258);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(144, 31);
            this.button5.TabIndex = 12;
            this.button5.Text = "打开图片文件";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(35, 265);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "报警图片";
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(311, 213);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 28);
            this.button2.TabIndex = 10;
            this.button2.Text = "试听";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(122, 213);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 28);
            this.button1.TabIndex = 9;
            this.button1.Text = "打开声音文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(32, 221);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "报警声音";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox3.Location = new System.Drawing.Point(122, 41);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(260, 26);
            this.textBox3.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(64, 45);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "名 称";
            // 
            // txtStatus2
            // 
            this.txtStatus2.Enabled = false;
            this.txtStatus2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStatus2.Location = new System.Drawing.Point(122, 170);
            this.txtStatus2.Margin = new System.Windows.Forms.Padding(4);
            this.txtStatus2.Name = "txtStatus2";
            this.txtStatus2.Size = new System.Drawing.Size(260, 26);
            this.txtStatus2.TabIndex = 5;
            this.txtStatus2.Leave += new System.EventHandler(this.txtStatus2_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(43, 177);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "2态名称";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(122, 127);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(260, 26);
            this.textBox2.TabIndex = 3;
            this.textBox2.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(43, 133);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "1态名称";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(122, 84);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(260, 26);
            this.textBox1.TabIndex = 1;
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(43, 89);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "0态名称";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.rbAlarmStatus2);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(489, 331);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(490, 81);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "报警状态";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(56, 45);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(59, 20);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "报警";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // rbAlarmStatus2
            // 
            this.rbAlarmStatus2.AutoSize = true;
            this.rbAlarmStatus2.Enabled = false;
            this.rbAlarmStatus2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbAlarmStatus2.Location = new System.Drawing.Point(401, 45);
            this.rbAlarmStatus2.Margin = new System.Windows.Forms.Padding(4);
            this.rbAlarmStatus2.Name = "rbAlarmStatus2";
            this.rbAlarmStatus2.Size = new System.Drawing.Size(50, 20);
            this.rbAlarmStatus2.TabIndex = 3;
            this.rbAlarmStatus2.Text = "2态";
            this.rbAlarmStatus2.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton2.Location = new System.Drawing.Point(289, 45);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(50, 20);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "1态";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton1.Location = new System.Drawing.Point(177, 45);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(50, 20);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "0态";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbCutStatus2);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.radioButton5);
            this.groupBox3.Controls.Add(this.radioButton6);
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(489, 453);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(490, 77);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "断电状态";
            // 
            // rbCutStatus2
            // 
            this.rbCutStatus2.AutoSize = true;
            this.rbCutStatus2.Enabled = false;
            this.rbCutStatus2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbCutStatus2.Location = new System.Drawing.Point(401, 38);
            this.rbCutStatus2.Margin = new System.Windows.Forms.Padding(4);
            this.rbCutStatus2.Name = "rbCutStatus2";
            this.rbCutStatus2.Size = new System.Drawing.Size(50, 20);
            this.rbCutStatus2.TabIndex = 3;
            this.rbCutStatus2.Text = "2态";
            this.rbCutStatus2.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Chocolate;
            this.button4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button4.Location = new System.Drawing.Point(471, -17);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 31);
            this.button4.TabIndex = 18;
            this.button4.Text = "关闭";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton5.Location = new System.Drawing.Point(289, 39);
            this.radioButton5.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(50, 20);
            this.radioButton5.TabIndex = 2;
            this.radioButton5.Text = "1态";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Checked = true;
            this.radioButton6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton6.Location = new System.Drawing.Point(177, 39);
            this.radioButton6.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(50, 20);
            this.radioButton6.TabIndex = 1;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "0态";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox2.Location = new System.Drawing.Point(56, 39);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(59, 20);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "断电";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Control;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button3.Location = new System.Drawing.Point(763, 571);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 35);
            this.button3.TabIndex = 17;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.SystemColors.Control;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnConfirm.Location = new System.Drawing.Point(489, 571);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(90, 35);
            this.btnConfirm.TabIndex = 15;
            this.btnConfirm.Text = "添加";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.Control;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnClear.Location = new System.Drawing.Point(900, 571);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 35);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "重置";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.Control;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button6.Location = new System.Drawing.Point(626, 571);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(90, 35);
            this.button6.TabIndex = 19;
            this.button6.Text = "修改";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Switch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Switch";
            this.Size = new System.Drawing.Size(1040, 657);
            this.Load += new System.EventHandler(this.Switch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        private void Switch_Load(object sender, EventArgs e)
        {
            this.initDataGridView();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (this.textBox1.TextLength > 40)
            {
                MessageBox.Show("0态名称字符长度不得超过40个字节");
                this.textBox1.Focus();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (this.textBox2.TextLength > 40)
            {
                MessageBox.Show("1态名称字符长度不得超过40个字节");
                this.textBox2.Focus();
            }
        }

        private void txtStatus2_Leave(object sender, EventArgs e)
        {
            if (this.txtStatus2.TextLength > 40)
            {
                MessageBox.Show("2态名称字符长度不得超过40个字节");
                this.txtStatus2.Focus();
            }
        }
    }
}

