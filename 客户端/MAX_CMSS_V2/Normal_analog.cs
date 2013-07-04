namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Media;
    using System.Windows.Forms;

    public class Normal_analog : UserControl
    {
        private GroupBox BaoJingLeiXing;
        private Button btnClear;
        private Button btnConfirm;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private ComboBox cbbError;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox5;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private MainForm mainForm;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private PictureBox pictureBox1;
        private SoundPlayer player = new SoundPlayer();
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private RadioButton radioButton5;
        private string temp;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private byte tongDaoLeiXing = 1;
        private TextBox txtAlarmLower;
        private TextBox txtAlarmUpper;
        private TextBox txtCut;
        private TextBox txtRev;
        private DataGridViewTextBoxColumn mingCheng;
        private DataGridViewTextBoxColumn leiXing;
        private DataGridViewTextBoxColumn danWei;
        private DataGridViewTextBoxColumn duanDianZhi;
        private DataGridViewTextBoxColumn fuDianZhi;
        private DataGridViewTextBoxColumn liangChengDi;
        private DataGridViewTextBoxColumn liangChengGao;
        private DataGridViewTextBoxColumn baoJingLeiXingGrid;
        private DataGridViewTextBoxColumn baoJingZhiShangXian;
        private DataGridViewTextBoxColumn baoJingZhiXiaXian;
        private DataGridViewTextBoxColumn wuChaDai;
        private DataGridViewTextBoxColumn guanJianZi;
        private DataGridViewTextBoxColumn XianXing;
        private DataGridViewCheckBoxColumn xianXingShuXing;
        private DataGridViewTextBoxColumn FeiXianXingGuanXi;
        private DataGridViewTextBoxColumn baoJingShengYin;
        private DataGridViewImageColumn baoJingTuBiao;
        private byte Type;

        public Normal_analog(byte type, MainForm mainForm)
        {
            this.InitializeComponent();
            this.comboBox3.Enabled = false;
            if (type != 0)
            {
                this.txtCut.Enabled = false;
                this.txtRev.Enabled = false;
                this.tongDaoLeiXing = 2;
            }
            this.Type = type;
            this.mainForm = mainForm;
        }

        private bool arguCheck()
        {
            if (this.txtAlarmUpper.Text.Trim() == string.Empty)
            {
                this.txtAlarmUpper.Text = "0.00";
            }
            if (this.txtAlarmLower.Text.Trim() == string.Empty)
            {
                this.txtAlarmLower.Text = "0.00";
            }
            if (this.textBox2.Text.Trim() == string.Empty)
            {
                this.textBox2.Text = "0.00";
            }
            if (this.textBox1.Text.Trim() == string.Empty)
            {
                this.textBox1.Text = "0.00";
            }
            if (this.txtCut.Text.Trim() == string.Empty)
            {
                this.txtCut.Text = this.textBox2.Text;
            }
            if (this.txtRev.Text.Trim() == string.Empty)
            {
                this.txtRev.Text = this.textBox1.Text;
            }
            if (this.textBox3.Text.Length == 0)
            {
                MessageBox.Show("请输入模拟量名称");
                this.textBox3.Focus();
                return false;
            }
            if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择关键字，关键字可在系统管理中进行管理");
                return false;
            }
            if (this.comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("请选择传感器制式");
                return false;
            }
            if ((this.comboBox3.SelectedIndex == -1) && this.radioButton2.Checked)
            {
                MessageBox.Show("请选择非线性关系");
                return false;
            }
            if (Convert.ToDouble(this.txtAlarmUpper.Text) <= Convert.ToDouble(this.txtAlarmLower.Text))
            {
                MessageBox.Show("报警上限必须高于报警下限");
                this.txtAlarmUpper.Focus();
                return false;
            }
            if (Convert.ToDouble(this.textBox2.Text) <= Convert.ToDouble(this.textBox1.Text))
            {
                MessageBox.Show("量程高值必须高于量程低值");
                this.textBox2.Focus();
                return false;
            }
            if (Convert.ToDouble(this.txtCut.Text) <= Convert.ToDouble(this.txtRev.Text))
            {
                MessageBox.Show("断电值必须高于复电值");
                this.txtCut.Focus();
                return false;
            }
            return true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.doClear();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.arguCheck())
            {
                if (GlobalParams.AllmnlLeiXing.IsExistMnlLx(this.textBox3.Text.ToString().Trim()))
                {
                    MessageBox.Show("该模拟量名称已存在，请重新命名");
                    this.textBox3.Focus();
                }
                else
                {
                    string toPath = this.savePicture();
                    byte BaoJingLeiXing = 0;
                    if (this.Type == 0)
                    {
                        if (this.comboBox2.SelectedItem.ToString() == "电流电压型")
                        {
                            this.tongDaoLeiXing = 1;
                        }
                        else
                        {
                            this.tongDaoLeiXing = 5;
                        }
                    }
                    else
                    {
                        this.tongDaoLeiXing = 2;
                    }
                    if (this.radioButton3.Checked)
                    {
                        BaoJingLeiXing = 0;
                    }
                    else if (this.radioButton4.Checked)
                    {
                        BaoJingLeiXing = 1;
                    }
                    else
                    {
                        BaoJingLeiXing = 2;
                    }
                    if (this.radioButton1.Checked)
                    {
                        GlobalParams.AllmnlLeiXing.InsertMnlLx(this.tongDaoLeiXing, toPath, this.textBox3.Text, this.textBox4.Text, this.comboBox1.SelectedItem.ToString(), this.radioButton1.Checked.ToString(), "", this.cbbError.SelectedItem.ToString(), this.txtAlarmUpper.Text, this.txtAlarmLower.Text, BaoJingLeiXing.ToString(), this.textBox2.Text, this.textBox1.Text, this.txtCut.Text, this.txtRev.Text);
                        Log.WriteLog(LogType.MoNiLiang, "添加模拟量#$" + this.textBox3.Text + "#$" + this.comboBox2.SelectedItem.ToString() + "#$" + this.textBox4.Text + "#$上限报警#$" + this.textBox2.Text + "#$" + this.textBox1.Text + "#$" + this.txtAlarmUpper.Text + "#$" + this.txtAlarmLower.Text + "#$" + this.txtCut.Text + "#$" + this.txtRev.Text);
                    }
                    else
                    {
                        GlobalParams.AllmnlLeiXing.InsertMnlLx(this.tongDaoLeiXing, toPath, this.textBox3.Text, this.textBox4.Text, this.comboBox1.SelectedItem.ToString(), this.radioButton1.Checked.ToString(), this.comboBox3.SelectedItem.ToString(), this.cbbError.SelectedItem.ToString(), this.txtAlarmUpper.Text, this.txtAlarmLower.Text, BaoJingLeiXing.ToString(), this.textBox2.Text, this.textBox1.Text, this.txtCut.Text, this.txtRev.Text);
                        Log.WriteLog(LogType.MoNiLiang, "添加模拟量#$" + this.textBox3.Text + "#$" + this.comboBox2.SelectedItem.ToString() + "#$" + this.textBox4.Text + "#$上限报警#$" + this.textBox2.Text + "#$" + this.textBox1.Text + "#$" + this.txtAlarmUpper.Text + "#$" + this.txtAlarmLower.Text + "#$" + this.txtCut.Text + "#$" + this.txtRev.Text);
                    }
                    this.dataGridView1.Rows.Clear();
                    this.initDataGridView();
                    this.doClear();
                    GlobalParams.setDataState();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog {
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
                this.player.Load();
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
            if ((this.dataGridView1.CurrentRow != null) && (MessageBox.Show("你确定要删除这个模拟量类型吗？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                if (GlobalParams.AllmnlLeiXing.IsGLCeDian(this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString()))
                {
                    MessageBox.Show("无法删除该模拟量，存在测点关联");
                }
                else
                {
                    GlobalParams.AllmnlLeiXing.DeleteMnlLx(this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString().Trim());
                    Log.WriteLog(LogType.MoNiLiang, "删除模拟量#$" + this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString());
                    this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentRow);
                    this.doClear();
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
            System.Windows.Forms.OpenFileDialog dlgP = new System.Windows.Forms.OpenFileDialog {
                CheckFileExists = true,
                Filter = "jpg files (*.jpg,*.gif)|*.jpg;*.gif|bmp files (*.bmp)|*.bmp",
                DefaultExt = ".jpg",
                Title = "打开图片文件"
            };

           
            string strPath = System.IO.Directory.GetCurrentDirectory();//取得当前默认路径

            dlgP.InitialDirectory = strPath;
          


            if (dlgP.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.Image = Image.FromFile(dlgP.FileName);
                this.pictureBox1.ImageLocation = dlgP.FileName;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.arguCheck())
            {
                if ((this.textBox3.Text != this.temp) && GlobalParams.AllmnlLeiXing.IsExistMnlLx(this.textBox3.Text.ToString().Trim()))
                {
                    MessageBox.Show("该模拟量名称已存在，请重新命名");
                    this.textBox3.Focus();
                }
                else
                {
                    byte BaoJingLeiXing = 0;
                    if (this.radioButton3.Checked)
                    {
                        BaoJingLeiXing = 0;
                    }
                    else if (this.radioButton4.Checked)
                    {
                        BaoJingLeiXing = 1;
                    }
                    else
                    {
                        BaoJingLeiXing = 2;
                    }
                    string toPath = this.savePicture();
                    if (this.radioButton2.Checked)
                    {
                        GlobalParams.AllmnlLeiXing.UpdateMnlLx(toPath, this.textBox3.Text, this.textBox4.Text, this.comboBox1.SelectedItem.ToString(), this.radioButton1.Checked.ToString(), this.comboBox3.SelectedItem.ToString(), this.cbbError.SelectedItem.ToString(), this.txtAlarmUpper.Text, this.txtAlarmLower.Text, BaoJingLeiXing.ToString(), this.textBox2.Text, this.textBox1.Text, this.txtCut.Text, this.txtRev.Text, this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString());
                        Log.WriteLog(LogType.MoNiLiang, "修改模拟量#$" + this.textBox3.Text + "#$" + this.comboBox2.SelectedItem.ToString() + "#$" + this.textBox4.Text + "#$上限报警#$" + this.textBox2.Text + "#$" + this.textBox1.Text + "#$" + this.txtAlarmUpper.Text + "#$" + this.txtAlarmLower.Text + "#$" + this.txtCut.Text + "#$" + this.txtRev.Text);
                    }
                    else
                    {
                        GlobalParams.AllmnlLeiXing.UpdateMnlLx(toPath, this.textBox3.Text, this.textBox4.Text, this.comboBox1.SelectedItem.ToString(), this.radioButton1.Checked.ToString(), "", this.cbbError.SelectedItem.ToString(), this.txtAlarmUpper.Text, this.txtAlarmLower.Text, BaoJingLeiXing.ToString(), this.textBox2.Text, this.textBox1.Text, this.txtCut.Text, this.txtRev.Text, this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString());
                        Log.WriteLog(LogType.MoNiLiang, "修改模拟量#$" + this.textBox3.Text + "#$" + this.comboBox2.SelectedItem.ToString() + "#$" + this.textBox4.Text + "#$上限报警#$" + this.textBox2.Text + "#$" + this.textBox1.Text + "#$" + this.txtAlarmUpper.Text + "#$" + this.txtAlarmLower.Text + "#$" + this.txtCut.Text + "#$" + this.txtRev.Text);
                    }
                    MoNiLiangLeiXing moNiLiang = new MoNiLiangLeiXing(this.textBox3.Text);
                    for (int i = 0; i < GlobalParams.AllCeDianList.allcedianlist.Count; i++)
                    {
                        CeDian cedian = GlobalParams.AllCeDianList.allcedianlist.ElementAt<KeyValuePair<string, CeDian>>(i).Value;
                        if ((cedian.MoNiLiang != null) && (cedian.MoNiLiang.MingCheng == this.textBox3.Text))
                        {
                            cedian.MoNiLiang = moNiLiang;
                        }
                    }
                    foreach (List_show show in this.mainForm.yeKuangs)
                    {
                        show.updateAllLieBiaoKuang();
                    }
                    this.dataGridView1.Rows.Clear();
                    this.initDataGridView();
                    this.doClear();
                    GlobalParams.setDataState();
                }
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            this.comboBox1.Items.Clear();
            DataTable dt1 = KeyWords.GetKey();
            foreach (DataRow row in dt1.Rows)
            {
                this.comboBox1.Items.Add(row["guanJianZi"].ToString());
            }
        }

        private void comboBox3_DropDown(object sender, EventArgs e)
        {
            this.comboBox3.Items.Clear();
            DataTable dt = GlobalParams.AllXianXingZhi.GetFeiXianXing();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    this.comboBox3.Items.Add(row["moNiLiangMingCheng"].ToString());
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Rows.Count != 0)
            {
                this.doClear();
                this.textBox3.Text = this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString();
                this.textBox4.Text = this.dataGridView1.CurrentRow.Cells["danWei"].Value.ToString();
                string s = this.dataGridView1.CurrentRow.Cells["guanJianZi"].Value.ToString();
                this.comboBox1.SelectedItem = s;
                this.comboBox2.SelectedItem = this.dataGridView1.CurrentRow.Cells["leiXing"].Value.ToString();
                this.radioButton1.Checked = (bool) this.dataGridView1.CurrentRow.Cells["xianXingShuXing"].Value;
                this.radioButton2.Checked = !this.radioButton1.Checked;
                this.comboBox3.SelectedItem = this.dataGridView1.CurrentRow.Cells["FeiXianXingGuanXi"].Value.ToString();
                this.cbbError.SelectedItem = this.dataGridView1.CurrentRow.Cells["wuChaDai"].Value.ToString();
                this.txtAlarmUpper.Text = this.dataGridView1.CurrentRow.Cells["baoJingZhiShangXian"].Value.ToString();
                this.txtAlarmLower.Text = this.dataGridView1.CurrentRow.Cells["baoJingZhiXiaXian"].Value.ToString();
                string s2 = this.dataGridView1.CurrentRow.Cells["baoJingLeiXingGrid"].Value.ToString();
                if (this.dataGridView1.CurrentRow.Cells["baoJingLeiXingGrid"].Value.ToString() == "上限报警")
                {
                    this.radioButton3.Checked = true;
                }
                else if (this.dataGridView1.CurrentRow.Cells["baoJingLeiXingGrid"].Value.ToString() == "区间报警")
                {
                    this.radioButton4.Checked = true;
                }
                else
                {
                    this.radioButton5.Checked = true;
                }
                this.textBox2.Text = this.dataGridView1.CurrentRow.Cells["liangChengGao"].Value.ToString();
                this.textBox1.Text = this.dataGridView1.CurrentRow.Cells["liangChengDi"].Value.ToString();
                if (this.Type == 0)
                {
                    this.txtCut.Text = this.dataGridView1.CurrentRow.Cells["duanDianZhi"].Value.ToString();
                    this.txtRev.Text = this.dataGridView1.CurrentRow.Cells["fuDianZhi"].Value.ToString();
                }
                this.pictureBox1.ImageLocation = Application.StartupPath + @"\monitor\" + MoNiLiangLeiXing.filePic(this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString());
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

        private void doClear()
        {
            this.textBox3.Clear();
            this.textBox4.Clear();
            this.comboBox1.SelectedIndex = -1;
            this.comboBox2.SelectedIndex = -1;
            this.comboBox3.SelectedIndex = -1;
            this.txtAlarmUpper.Clear();
            this.txtAlarmLower.Clear();
            this.textBox2.Clear();
            this.textBox1.Clear();
            this.txtCut.Clear();
            this.txtRev.Clear();
            this.pictureBox1.Image = null;
        }

        private void initDataGridView()
        {
            int lineNum = 0;
            List<MoNiLiangLeiXing> query = GlobalParams.AllmnlLeiXing.GetMoNiLiangList(this.Type);
            foreach (MoNiLiangLeiXing item in query)
            {
                string filePath;
                this.dataGridView1.Rows.Add();
                DataGridViewRow dataRow = this.dataGridView1.Rows[lineNum];
                dataRow.Cells["mingCheng"].Value = item.MingCheng;
                byte leiXing = (byte) item.LeiXing;
                if (leiXing == 1)
                {
                    dataRow.Cells["leiXing"].Value = "电流电压型";
                }
                else if (leiXing == 5)
                {
                    dataRow.Cells["leiXing"].Value = "频率型";
                }
                else
                {
                    dataRow.Cells["leiXing"].Value = "累计频率型";
                }
                dataRow.Cells["danWei"].Value = item.DanWei;
                dataRow.Cells["guanJianZi"].Value = item.GuanJianZi;
                dataRow.Cells["baoJingZhiShangXian"].Value = item.BaoJingZhiShangXian;
                dataRow.Cells["baoJingZhiXiaXian"].Value = item.BaoJingZhiXiaXian;
                if (item.BaoJingLeiXing == 0)
                {
                    dataRow.Cells["baoJingLeiXingGrid"].Value = "上限报警";
                }
                else if (item.BaoJingLeiXing == 1)
                {
                    dataRow.Cells["baoJingLeiXingGrid"].Value = "区间报警";
                }
                else
                {
                    dataRow.Cells["baoJingLeiXingGrid"].Value = "下限报警";
                }
                dataRow.Cells["liangChengGao"].Value = item.LiangChengGao;
                dataRow.Cells["liangChengDi"].Value = item.LiangChengDi;
                dataRow.Cells["wuChaDai"].Value = item.WuChaDai;
                dataRow.Cells["xianXingShuXing"].Value = item.XianXingShuXing;
                if (item.XianXingShuXing)
                {
                    dataRow.Cells["XianXing"].Value = "线性";
                }
                else
                {
                    dataRow.Cells["XianXing"].Value = "非线性";
                }
                dataRow.Cells["baoJingShengYin"].Value = item.BaoJingShengYin;
                if (item.BaoJingTuBiao.Length != 0)
                {
                    try
                    {
                        filePath = Application.StartupPath + @"\monitor\" + item.BaoJingTuBiao.ToString();
                        dataRow.Cells["baoJingTuBiao"].Value = Image.FromFile(filePath);
                    }
                    catch
                    {

                    }
                }
                else
                {
                    ClientConfig config = ClientConfig.CreateCommon();
                    filePath = string.Empty;
                    if (this.Type == 0)
                    {
                        filePath = config.get("YiBanMoNiLiang");
                    }
                    else
                    {
                        filePath = config.get("LeiJiMoNiLiang");
                    }
                    if (filePath != null)
                    {
                        filePath = Application.StartupPath + @"\monitor\" + filePath;
                        dataRow.Cells["baoJingTuBiao"].Value = Image.FromFile(filePath);
                    }
                }
                dataRow.Cells["FeiXianXingGuanXi"].Value = item.FeiXianXingGuanXi;
                if (this.Type == 0)
                {
                    dataRow.Cells["duanDianZhi"].Value = item.DuanDianZhi;
                    dataRow.Cells["fuDianZhi"].Value = item.FuDianZhi;
                }
                lineNum++;
            }
            if (query.Count<MoNiLiangLeiXing>() > 0)
            {
                foreach (string item in GlobalParams.AllKeyWord.all_keyword)
                {
                    this.comboBox1.Items.Add(item.Trim());
                }
            }
            DataTable dt = GlobalParams.AllXianXingZhi.GetFeiXianXing();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    this.comboBox3.Items.Add(row["moNiLiangMingCheng"].ToString());
                }
            }
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAlarmUpper = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbbError = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAlarmLower = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtRev = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCut = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.button6 = new System.Windows.Forms.Button();
            this.BaoJingLeiXing = new System.Windows.Forms.GroupBox();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.mingCheng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leiXing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.danWei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duanDianZhi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fuDianZhi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.liangChengDi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.liangChengGao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baoJingLeiXingGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baoJingZhiShangXian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baoJingZhiXiaXian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wuChaDai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guanJianZi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XianXing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xianXingShuXing = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FeiXianXingGuanXi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baoJingShengYin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baoJingTuBiao = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.BaoJingLeiXing.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mingCheng,
            this.leiXing,
            this.danWei,
            this.duanDianZhi,
            this.fuDianZhi,
            this.liangChengDi,
            this.liangChengGao,
            this.baoJingLeiXingGrid,
            this.baoJingZhiShangXian,
            this.baoJingZhiXiaXian,
            this.wuChaDai,
            this.guanJianZi,
            this.XianXing,
            this.xianXingShuXing,
            this.FeiXianXingGuanXi,
            this.baoJingShengYin,
            this.baoJingTuBiao});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(901, 307);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(236, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "报警下限";
            // 
            // txtAlarmUpper
            // 
            this.txtAlarmUpper.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAlarmUpper.Location = new System.Drawing.Point(112, 26);
            this.txtAlarmUpper.Name = "txtAlarmUpper";
            this.txtAlarmUpper.Size = new System.Drawing.Size(112, 26);
            this.txtAlarmUpper.TabIndex = 3;
            this.txtAlarmUpper.TextChanged += new System.EventHandler(this.txtAlarmUpper_TextChanged);
            this.txtAlarmUpper.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAlarmUpper_KeyPress);
            this.txtAlarmUpper.Leave += new System.EventHandler(this.txtAlarmUpper_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.comboBox3);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbbError);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(30, 315);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(351, 303);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(10, 185);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 16);
            this.label14.TabIndex = 32;
            this.label14.Text = "非线性关系";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(106, 181);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(212, 24);
            this.comboBox3.TabIndex = 31;
            this.comboBox3.DropDown += new System.EventHandler(this.comboBox3_DropDown);
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "电流电压型",
            "频率型"});
            this.comboBox2.Location = new System.Drawing.Point(106, 123);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(212, 24);
            this.comboBox2.TabIndex = 30;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(10, 127);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 16);
            this.label13.TabIndex = 29;
            this.label13.Text = "传感器制式";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(106, 92);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(212, 24);
            this.comboBox1.TabIndex = 28;
            this.comboBox1.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Location = new System.Drawing.Point(106, 273);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(112, 23);
            this.button5.TabIndex = 27;
            this.button5.Text = "选择图片文件";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(238, 273);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(94, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(238, 243);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "试听";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(106, 243);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "打开声音文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(26, 276);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 16);
            this.label12.TabIndex = 22;
            this.label12.Text = "报警图片";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(26, 246);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "报警声音";
            // 
            // cbbError
            // 
            this.cbbError.Enabled = false;
            this.cbbError.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbError.FormattingEnabled = true;
            this.cbbError.Items.AddRange(new object[] {
            "0"});
            this.cbbError.Location = new System.Drawing.Point(106, 212);
            this.cbbError.Name = "cbbError";
            this.cbbError.Size = new System.Drawing.Size(212, 24);
            this.cbbError.TabIndex = 19;
            this.cbbError.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(42, 216);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 18;
            this.label8.Text = "误差带";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton2.Location = new System.Drawing.Point(208, 154);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(106, 20);
            this.radioButton2.TabIndex = 17;
            this.radioButton2.Text = "非线性属性";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton1.Location = new System.Drawing.Point(106, 152);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(90, 20);
            this.radioButton1.TabIndex = 16;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "线性属性";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(42, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "关键字";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox4.Location = new System.Drawing.Point(106, 59);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(212, 26);
            this.textBox4.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(58, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "单位";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox3.Location = new System.Drawing.Point(106, 26);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(212, 26);
            this.textBox3.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(58, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "名称";
            // 
            // txtAlarmLower
            // 
            this.txtAlarmLower.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAlarmLower.Location = new System.Drawing.Point(314, 25);
            this.txtAlarmLower.Name = "txtAlarmLower";
            this.txtAlarmLower.Size = new System.Drawing.Size(112, 26);
            this.txtAlarmLower.TabIndex = 5;
            this.txtAlarmLower.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAlarmLower_KeyPress);
            this.txtAlarmLower.Leave += new System.EventHandler(this.txtAlarmLower_Leave);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.Chocolate;
            this.btnConfirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnConfirm.Location = new System.Drawing.Point(421, 595);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 30);
            this.btnConfirm.TabIndex = 11;
            this.btnConfirm.Text = "添加";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Chocolate;
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnClear.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClear.Location = new System.Drawing.Point(811, 595);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "重置";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(34, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "报警上限";
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.txtAlarmLower);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtAlarmUpper);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(425, 384);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 61);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "报警门限";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox1);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.textBox2);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.Location = new System.Drawing.Point(425, 315);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(461, 59);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "量程值";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(312, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(112, 26);
            this.textBox1.TabIndex = 5;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(234, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "量程低值";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(110, 25);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(112, 26);
            this.textBox2.TabIndex = 3;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            this.textBox2.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(32, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 2;
            this.label9.Text = "量程高值";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtRev);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtCut);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(425, 526);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(461, 56);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "断电/复电值";
            // 
            // txtRev
            // 
            this.txtRev.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRev.Location = new System.Drawing.Point(309, 24);
            this.txtRev.Name = "txtRev";
            this.txtRev.Size = new System.Drawing.Size(112, 26);
            this.txtRev.TabIndex = 5;
            this.txtRev.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRev_KeyPress);
            this.txtRev.Leave += new System.EventHandler(this.txtRev_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(250, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 16);
            this.label10.TabIndex = 4;
            this.label10.Text = "复电值";
            // 
            // txtCut
            // 
            this.txtCut.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCut.Location = new System.Drawing.Point(107, 24);
            this.txtCut.Name = "txtCut";
            this.txtCut.Size = new System.Drawing.Size(112, 26);
            this.txtCut.TabIndex = 3;
            this.txtCut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCut_KeyPress);
            this.txtCut.Leave += new System.EventHandler(this.txtCut_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(48, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 16);
            this.label11.TabIndex = 2;
            this.label11.Text = "断电值";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Chocolate;
            this.button3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button3.Location = new System.Drawing.Point(681, 595);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 30);
            this.button3.TabIndex = 13;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Chocolate;
            this.button4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button4.Location = new System.Drawing.Point(783, 567);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "关闭";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.DefaultExt = "wav";
            this.OpenFileDialog.Filter = "\"WAV files (*.wav)|*.wav\"";
            this.OpenFileDialog.Title = "打开声音文件";
            this.OpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog_FileOk);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Chocolate;
            this.button6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button6.Location = new System.Drawing.Point(551, 595);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 30);
            this.button6.TabIndex = 15;
            this.button6.Text = "修改";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // BaoJingLeiXing
            // 
            this.BaoJingLeiXing.Controls.Add(this.radioButton5);
            this.BaoJingLeiXing.Controls.Add(this.radioButton4);
            this.BaoJingLeiXing.Controls.Add(this.radioButton3);
            this.BaoJingLeiXing.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BaoJingLeiXing.Location = new System.Drawing.Point(425, 455);
            this.BaoJingLeiXing.Name = "BaoJingLeiXing";
            this.BaoJingLeiXing.Size = new System.Drawing.Size(461, 61);
            this.BaoJingLeiXing.TabIndex = 16;
            this.BaoJingLeiXing.TabStop = false;
            this.BaoJingLeiXing.Text = "报警类型";
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton5.Location = new System.Drawing.Point(327, 29);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(90, 20);
            this.radioButton5.TabIndex = 2;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "下限报警";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton4.Location = new System.Drawing.Point(186, 30);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(90, 20);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "区间报警";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton3.Location = new System.Drawing.Point(45, 31);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(90, 20);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "上限报警";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // mingCheng
            // 
            this.mingCheng.HeaderText = "名称";
            this.mingCheng.Name = "mingCheng";
            this.mingCheng.ReadOnly = true;
            // 
            // leiXing
            // 
            this.leiXing.HeaderText = "传感器制式";
            this.leiXing.Name = "leiXing";
            this.leiXing.ReadOnly = true;
            // 
            // danWei
            // 
            this.danWei.HeaderText = "单位";
            this.danWei.Name = "danWei";
            this.danWei.ReadOnly = true;
            // 
            // duanDianZhi
            // 
            this.duanDianZhi.HeaderText = "断电值";
            this.duanDianZhi.Name = "duanDianZhi";
            this.duanDianZhi.ReadOnly = true;
            // 
            // fuDianZhi
            // 
            this.fuDianZhi.HeaderText = "复电值";
            this.fuDianZhi.Name = "fuDianZhi";
            this.fuDianZhi.ReadOnly = true;
            // 
            // liangChengDi
            // 
            this.liangChengDi.HeaderText = "量程低值";
            this.liangChengDi.Name = "liangChengDi";
            this.liangChengDi.ReadOnly = true;
            // 
            // liangChengGao
            // 
            this.liangChengGao.HeaderText = "量程高值";
            this.liangChengGao.Name = "liangChengGao";
            this.liangChengGao.ReadOnly = true;
            // 
            // baoJingLeiXingGrid
            // 
            this.baoJingLeiXingGrid.HeaderText = "报警类型";
            this.baoJingLeiXingGrid.Name = "baoJingLeiXingGrid";
            this.baoJingLeiXingGrid.ReadOnly = true;
            // 
            // baoJingZhiShangXian
            // 
            this.baoJingZhiShangXian.HeaderText = "报警上限";
            this.baoJingZhiShangXian.Name = "baoJingZhiShangXian";
            this.baoJingZhiShangXian.ReadOnly = true;
            // 
            // baoJingZhiXiaXian
            // 
            this.baoJingZhiXiaXian.HeaderText = "报警下限";
            this.baoJingZhiXiaXian.Name = "baoJingZhiXiaXian";
            this.baoJingZhiXiaXian.ReadOnly = true;
            // 
            // wuChaDai
            // 
            this.wuChaDai.HeaderText = "误差带";
            this.wuChaDai.Name = "wuChaDai";
            this.wuChaDai.ReadOnly = true;
            // 
            // guanJianZi
            // 
            this.guanJianZi.HeaderText = "关键字";
            this.guanJianZi.Name = "guanJianZi";
            this.guanJianZi.ReadOnly = true;
            // 
            // XianXing
            // 
            this.XianXing.HeaderText = "是否线性";
            this.XianXing.Name = "XianXing";
            this.XianXing.ReadOnly = true;
            // 
            // xianXingShuXing
            // 
            this.xianXingShuXing.HeaderText = "线性属性";
            this.xianXingShuXing.Name = "xianXingShuXing";
            this.xianXingShuXing.ReadOnly = true;
            this.xianXingShuXing.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.xianXingShuXing.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.xianXingShuXing.Visible = false;
            // 
            // FeiXianXingGuanXi
            // 
            this.FeiXianXingGuanXi.HeaderText = "非线性关系名称";
            this.FeiXianXingGuanXi.Name = "FeiXianXingGuanXi";
            this.FeiXianXingGuanXi.ReadOnly = true;
            // 
            // baoJingShengYin
            // 
            this.baoJingShengYin.HeaderText = "报警声音";
            this.baoJingShengYin.Name = "baoJingShengYin";
            this.baoJingShengYin.ReadOnly = true;
            // 
            // baoJingTuBiao
            // 
            this.baoJingTuBiao.HeaderText = "图标";
            this.baoJingTuBiao.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.baoJingTuBiao.Name = "baoJingTuBiao";
            this.baoJingTuBiao.ReadOnly = true;
            this.baoJingTuBiao.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.baoJingTuBiao.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
           
            // 
            // Normal_analog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BaoJingLeiXing);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Normal_analog";
            this.Size = new System.Drawing.Size(901, 628);
            this.Load += new System.EventHandler(this.Normal_analog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.BaoJingLeiXing.ResumeLayout(false);
            this.BaoJingLeiXing.PerformLayout();
            this.ResumeLayout(false);

        }

        private void label16_Click(object sender, EventArgs e)
        {
        }

        private void Normal_analog_Load(object sender, EventArgs e)
        {
            this.initDataGridView();
        }

        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton2.Checked)
            {
                this.comboBox3.Enabled = true;
                this.comboBox3.Items.Clear();
                DataTable dt = GlobalParams.AllXianXingZhi.GetFeiXianXing();
                foreach (DataRow row in dt.Rows)
                {
                    this.comboBox3.Items.Add(row["moNiLiangMingCheng"].ToString());
                }
            }
        }

        private string savePicture()
        {
            string toPath = "";
            try
            {
                if ((this.pictureBox1.ImageLocation != null) && (this.pictureBox1.ImageLocation != ""))
                {
                    Image image = this.pictureBox1.Image.GetThumbnailImage(40, 20, null, IntPtr.Zero);
                    string fileName = Path.GetFileName(this.pictureBox1.ImageLocation);
                    toPath = Application.StartupPath + @"\monitor\" + fileName;
                    image.Save(toPath);
                    toPath = fileName;
                }
            }
            catch (Exception)
            {
                return "";
            }
            return toPath;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() == string.Empty)
            {
                this.textBox1.Text = "0.00";
            }
            else
            {
                this.textBox1.Text = Math.Round(Convert.ToDouble(this.textBox1.Text), 2).ToString("#0.00");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (this.textBox2.Text.Trim() == string.Empty)
            {
                this.textBox2.Text = "0.00";
            }
            else
            {
                this.textBox2.Text = Math.Round(Convert.ToDouble(this.textBox2.Text), 2).ToString("#0.00");
            }
        }

        private void txtAlarmLower_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txtAlarmLower_Leave(object sender, EventArgs e)
        {
            if (this.txtAlarmLower.Text.Trim() == string.Empty)
            {
                this.txtAlarmLower.Text = "0.00";
            }
            else
            {
                this.txtAlarmLower.Text = Math.Round(Convert.ToDouble(this.txtAlarmLower.Text), 2).ToString("#0.00");
            }
        }

        private void txtAlarmUpper_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txtAlarmUpper_Leave(object sender, EventArgs e)
        {
            if (this.txtAlarmUpper.Text.Trim() == string.Empty)
            {
                this.txtAlarmUpper.Text = "0.00";
            }
            else
            {
                this.txtAlarmUpper.Text = Math.Round(Convert.ToDouble(this.txtAlarmUpper.Text), 2).ToString("#0.00");
            }
        }

        private void txtAlarmUpper_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtCut_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txtCut_Leave(object sender, EventArgs e)
        {
            if (this.txtCut.Text.Trim() == string.Empty)
            {
                this.txtCut.Text = this.textBox2.Text;
            }
            else
            {
                this.txtCut.Text = Math.Round(Convert.ToDouble(this.txtCut.Text), 2).ToString("#0.00");
            }
        }

        private void txtRev_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((((e.KeyChar == '\b') || (e.KeyChar == '.')) || (e.KeyChar == '-')) || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txtRev_Leave(object sender, EventArgs e)
        {
            if (this.txtRev.Text.Trim() == string.Empty)
            {
                this.txtRev.Text = this.textBox1.Text;
            }
            else
            {
                this.txtRev.Text = Math.Round(Convert.ToDouble(this.txtRev.Text), 2).ToString("#0.00");
            }
        }
    }
}

