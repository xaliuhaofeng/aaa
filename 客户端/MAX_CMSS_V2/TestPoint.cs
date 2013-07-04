namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class TestPoint : UserControl
    {
        private Button btnConfirm;
        private Button button1;
        private CheckBox cbAlarm;
        private ComboBox cbbChannel;
        private ComboBox cbbLoc;
        private ComboBox cbbSub;
        private string cdbhSelect = "";
        private ComboBox comboBox1;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Button DelButton;
        private byte flag = 0;
        private GroupBox groupBox4;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button ModifyBt;
        private byte Type;

        public TestPoint(byte type)
        {
            this.InitializeComponent();
            this.Type = type;
            if (type != 0)
            {
                this.cbAlarm.Visible = false;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.cbbLoc.SelectedIndex == -1)
            {
                MessageBox.Show("请选择测点位置");
            }
            else if (this.cbbSub.SelectedIndex == -1)
            {
                MessageBox.Show("请选择分站号");
            }
            else if ((this.cbbChannel.SelectedIndex == -1) && ((this.Type != 1) || ((this.Type == 1) && (this.dataGridView2.CurrentRow.Cells["chuanGanQiLeiXing2"].Value.ToString() != "分站量"))))
            {
                MessageBox.Show("请选择通道号");
            }
            else if (((this.Type == 1) && (this.dataGridView2.CurrentRow.Cells["chuanGanQiLeiXing2"].Value.ToString() == "分站量")) && (this.comboBox1.SelectedIndex == -1))
            {
                MessageBox.Show("请设置分站量的分站类型");
            }
            else
            {
                string temp1;
                string temp2;
                if (this.cbbSub.Text.Length == 1)
                {
                    temp1 = "0" + this.cbbSub.Text;
                }
                else
                {
                    temp1 = this.cbbSub.Text;
                }
                if (this.cbbChannel.Text.Length == 1)
                {
                    temp2 = "0" + this.cbbChannel.Text;
                }
                else
                {
                    temp2 = this.cbbChannel.Text;
                }
                if (this.Type == 0)
                {
                    if (GlobalParams.AllCeDianList.ReCeDian(temp1 + "A" + temp2))
                    {
                        MessageBox.Show("该测点编号已存在，请重新命名");
                    }
                    else if (GlobalParams.AllCeDianList.CreateCeDian(this.Type.ToString(), this.cbbLoc.SelectedItem.ToString(), this.dataGridView2.CurrentRow.Cells["mingCheng"].Value.ToString(), this.dataGridView2.CurrentRow.Cells["leiXing"].Value.ToString(), this.cbbSub.SelectedItem.ToString(), this.cbbChannel.SelectedItem.ToString(), temp1 + "A" + temp2, ""))
                    {
                        GlobalParams.setDataState();
                        Log.WriteLog(LogType.CeDianPeiZhi, "添加测点#$模拟量#$" + temp1 + "A" + temp2 + "#$" + this.cbbLoc.SelectedItem.ToString() + "#$" + this.cbbSub.SelectedItem.ToString() + "#$" + this.cbbChannel.SelectedItem.ToString());
                        this.UpdateCeDian();
                        MessageBox.Show("该测点编号添加成功");
                    }
                    else
                    {
                        MessageBox.Show("该测点编号添加失败");
                    }
                }
                else if (this.Type == 1)
                {
                    string ceDianBiaoHao;
                    string tongDao;
                    string fenZhanLeiXing;
                    bool isFenZhanLiang = false;
                    if (Convert.ToByte(this.dataGridView2.CurrentRow.Cells["leiXing"].Value) == 8)
                    {
                        ceDianBiaoHao = temp1 + "F00";
                        tongDao = "0";
                        fenZhanLeiXing = this.comboBox1.SelectedItem.ToString();
                        isFenZhanLiang = true;
                    }
                    else
                    {
                        ceDianBiaoHao = temp1 + "D" + temp2;
                        tongDao = this.cbbChannel.SelectedItem.ToString();
                        fenZhanLeiXing = "";
                    }
                    if (GlobalParams.AllCeDianList.ReCeDian(ceDianBiaoHao))
                    {
                        MessageBox.Show("该测点编号已存在，请重新命名");
                    }
                    else if (GlobalParams.AllCeDianList.CreateCeDian(this.Type.ToString(), this.cbbLoc.SelectedItem.ToString(), this.dataGridView2.CurrentRow.Cells["mingCheng"].Value.ToString(), this.dataGridView2.CurrentRow.Cells["leiXing"].Value.ToString(), this.cbbSub.SelectedItem.ToString(), tongDao, ceDianBiaoHao, fenZhanLeiXing))
                    {
                        if (!isFenZhanLiang)
                        {
                            Log.WriteLog(LogType.CeDianPeiZhi, "添加测点#$开关量#$" + ceDianBiaoHao + "#$" + this.cbbLoc.SelectedItem.ToString() + "#$" + this.cbbSub.SelectedItem.ToString() + "#$" + this.cbbChannel.SelectedItem.ToString());
                        }
                        else
                        {
                            Log.WriteLog(LogType.CeDianPeiZhi, string.Concat(new object[] { "添加测点#$分站量#$", ceDianBiaoHao, "#$", this.cbbLoc.SelectedItem.ToString(), "#$", this.cbbSub.SelectedItem.ToString(), "#$", 0 }));
                        }
                        GlobalParams.setDataState();
                        this.UpdateCeDian();
                        MessageBox.Show("该测点编号添加成功");
                    }
                    else
                    {
                        MessageBox.Show("该测点编号添加失败");
                    }
                }
                else if (CeDian.ReKongCeDian(temp1 + "C" + temp2))
                {
                    MessageBox.Show("该测点编号已存在，请重新命名");
                }
                else if (GlobalParams.AllCeDianList.CreateCeDian(this.cbbLoc.SelectedItem.ToString(), this.dataGridView2.CurrentRow.Cells["mingCheng"].Value.ToString(), this.cbbSub.SelectedItem.ToString(), this.cbbChannel.SelectedItem.ToString(), temp1 + "C" + temp2))
                {
                    GlobalParams.setDataState();
                    Log.WriteLog(LogType.CeDianPeiZhi, "添加测点#$控制量#$" + temp1 + "C" + temp2 + "#$" + this.cbbLoc.SelectedItem.ToString() + "#$" + this.cbbSub.SelectedItem.ToString() + "#$" + this.cbbChannel.SelectedItem.ToString());
                    this.UpdateCeDian();
                    
                    MessageBox.Show("该测点编号添加成功");
                }
                else
                {
                    MessageBox.Show("该测点编号添加失败");
                }
                this.flag = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        private void cbbChannel_DropDown(object sender, EventArgs e)
        {
            string[] list;
            this.cbbChannel.Items.Clear();
            if ((this.Type == 0) || (this.Type == 1))
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
                if ((this.Type == 0) || (this.Type == 1))
                {
                    dt = CeDian.GetChannel(this.cbbSub.SelectedItem.ToString());
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            this.cbbChannel.Items.Remove(row["tongDaoHao"].ToString());
                        }
                    }
                    if ((this.flag == 1) && (Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["fenZhanHao"].Value) == (this.cbbSub.SelectedIndex + 1)))
                    {
                        this.cbbChannel.Items.Add(this.dataGridView1.CurrentRow.Cells["tongDaoHao"].Value.ToString());
                    }
                }
                else
                {
                    dt = CeDian.GetKongChannel(this.cbbSub.SelectedItem.ToString());
                    foreach (DataRow row in dt.Rows)
                    {
                        this.cbbChannel.Items.Remove(row["kongZhiLiangBianHao"].ToString());
                    }
                    if ((this.flag == 1) && (Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["fenZhanHao"].Value) == (this.cbbSub.SelectedIndex + 1)))
                    {
                        this.cbbChannel.Items.Add(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString());
                    }
                }
            }
        }

        private void cbbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbbLoc_DropDown(object sender, EventArgs e)
        {
            this.cbbLoc.Items.Clear();
            this.cbbLoc.Items.AddRange(InstallationSite.GetAllLocationAsArray());
        }

        private void cbbLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbbSub_DropDown(object sender, EventArgs e)
        {
            this.cbbSub.Items.Clear();
            string[] list = FenZhan.GetAllConfigedFenZhan();
            foreach (string b in list)
            {
                this.cbbSub.Items.Add(b);
            }
        }

        private void cbbSub_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbbTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbbType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.AddRange(FenZhanLeiXing.GetAllFenZhanLeiXing());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Rows.Count != 0)
            {
                this.flag = 1;
                this.cbbLoc.Items.Clear();
                this.cbbLoc.Items.AddRange(InstallationSite.GetAllLocationAsArray());
                this.cbbLoc.SelectedItem = this.dataGridView1.CurrentRow.Cells["ceDianWeiZhi"].Value.ToString();
                string cdbh = this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString();
                string aa = this.dataGridView1.CurrentRow.Cells["baoJing"].Value.ToString();
                this.cbAlarm.Checked = bool.Parse(aa);
                this.cdbhSelect = cdbh;
                this.cbbSub.Items.Clear();
                this.cbbChannel.Items.Clear();
                string[] list = FenZhan.GetAllConfigedFenZhan();
                foreach (string b in list)
                {
                    this.cbbSub.Items.Add(b);
                }
                for (int i = 0; i < 0x10; i++)
                {
                    int Reflector0003 = i + 1;
                    this.cbbChannel.Items.Add(Reflector0003.ToString());
                }
                this.comboBox1.Items.AddRange(FenZhanLeiXing.GetAllFenZhanLeiXing());
                this.cbbSub.SelectedItem = this.dataGridView1.CurrentRow.Cells["fenZhanHao"].Value.ToString().Trim();
                this.fenZhanOption(false);
                if (this.Type == 2)
                {
                    this.cbbChannel.SelectedItem = this.dataGridView1.CurrentRow.Cells["tongDaoHao"].Value.ToString();
                }
                else if (this.Type == 0)
                {
                    this.cbbChannel.SelectedItem = this.dataGridView1.CurrentRow.Cells["tongDaoHao"].Value.ToString();
                }
                else if (this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString().Substring(2, 1) == "F")
                {
                    this.fenZhanOption(true);
                    this.comboBox1.SelectedItem = this.dataGridView1.CurrentRow.Cells["chuanGanQiZhiShi"].Value.ToString();
                }
                else
                {
                    this.cbbChannel.SelectedItem = this.dataGridView1.CurrentRow.Cells["tongDaoHao"].Value.ToString();
                }
                for (int k = 0; k < this.dataGridView2.Rows.Count; k++)
                {
                    string mingcheng;
                    try
                    {
                        mingcheng = this.dataGridView1.CurrentRow.Cells["xiaoLeiXing"].Value.ToString();
                        if (mingcheng.IndexOf("/") > -1)
                        {
                            mingcheng = mingcheng.Substring(0, mingcheng.IndexOf("/"));
                        }
                        if (mingcheng == this.dataGridView2.Rows[k].Cells["mingCheng"].Value.ToString())
                        {
                            this.dataGridView2.Rows[k].Selected = true;
                            this.dataGridView2.CurrentCell = this.dataGridView2.Rows[k].Cells["mingCheng"];
                        }
                    }
                    catch
                    {
                        mingcheng = this.dataGridView1.CurrentRow.Cells["mingCheng"].Value.ToString();
                        if (mingcheng.IndexOf("/") > -1)
                        {
                            mingcheng = mingcheng.Substring(0, mingcheng.IndexOf("/"));
                        }
                        if (mingcheng == this.dataGridView2.Rows[k].Cells["mingCheng"].Value.ToString())
                        {
                            this.dataGridView2.Rows[k].Selected = true;
                            this.dataGridView2.CurrentCell = this.dataGridView2.Rows[k].Cells["mingCheng"];
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (((this.Type == 1) && (e.RowIndex >= 0)) && (this.dataGridView2.Rows[e.RowIndex].Cells["chuanGanQiLeiXing2"].Value.ToString() == "分站量"))
            {
                this.fenZhanOption(true);
            }
            else
            {
                this.fenZhanOption(false);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void DelButton_Click(object sender, EventArgs e)
        {
            if (this.flag == 0)
            {
                MessageBox.Show("请选择要删除的测点");
            }
            else if ((this.flag == 1) && (MessageBox.Show("你确定要删除这个测点？", "测点删除确认", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                int tempc;
                string cedianbianhao;
                DataTable dt;
                if (this.Type != 2)
                {
                    tempc = DuanDianGuanXi.Count(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString()) + KuiDianGuanXi.Count(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString());
                    if (tempc > 0)
                    {
                        MessageBox.Show("存在断电或馈电关系，请先删除关系后再删除该测点");
                    }
                    else if (DuoSheBei.CountMulti(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString()) > 0)
                    {
                        MessageBox.Show("存在多设备报警关系，请先删除关系后再删除该测点");
                    }
                    else
                    {
                        cedianbianhao = this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString();
                        if (GlobalParams.AllCeDianList.DelCeDian(cedianbianhao))
                        {
                            OperateDBAccess.Execute("delete from LieBiaoAndCeDian where ceDianBianHao like '" + cedianbianhao + "%'");
                            if (cedianbianhao[2] == 'A')
                            {
                                Log.WriteLog(LogType.CeDianPeiZhi, "删除测点#$模拟量#$" + cedianbianhao + "#$" + this.dataGridView1.CurrentRow.Cells["ceDianWeiZhi"].Value.ToString() + "#$" + this.dataGridView1.CurrentRow.Cells["fenZhanHao"].Value.ToString() + "#$" + this.dataGridView1.CurrentRow.Cells["tongDaoHao"].Value.ToString());
                            }
                            else
                            {
                                Log.WriteLog(LogType.CeDianPeiZhi, "删除测点#$开关量#$" + cedianbianhao + "#$" + this.dataGridView1.CurrentRow.Cells["ceDianWeiZhi"].Value.ToString() + "#$" + this.dataGridView1.CurrentRow.Cells["fenZhanHao"].Value.ToString() + "#$" + this.dataGridView1.CurrentRow.Cells["tongDaoHao"].Value.ToString());
                            }
                            if (GlobalParams.AllCeDianList.allcedianlist.ContainsKey(cedianbianhao))
                            {
                                GlobalParams.AllCeDianList.allcedianlist.Remove(cedianbianhao);
                            }
                            dt = GlobalParams.AllCeDianList.ListConvertDataTable(this.Type);
                            GlobalParams.setDataState();
                            this.dataGridView1.DataSource = dt;
                            MainFormRef.updateMainForm();
                            MessageBox.Show("此测点，删除成功。");
                        }
                        else
                        {
                            MessageBox.Show("此测点，删除失败！");
                        }
                    }
                }
                else
                {
                    tempc = DuanDianGuanXi.CountKong(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString()) + KuiDianGuanXi.CountKong(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString());
                    if (tempc > 0)
                    {
                        MessageBox.Show("存在断电或馈电关系，请先删除关系后再删除该测点");
                    }
                    else
                    {
                        cedianbianhao = this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString();
                        if (OperateDB.ExecuteSql(CeDian.DelKongCeDian(cedianbianhao)))
                        {
                            Log.WriteLog(LogType.CeDianPeiZhi, "删除测点#$控制量#$" + cedianbianhao + "#$" + this.dataGridView1.CurrentRow.Cells["ceDianWeiZhi"].Value.ToString() + "#$" + this.dataGridView1.CurrentRow.Cells["fenZhanHao"].Value.ToString() + "#$" + this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString());
                            if (GlobalParams.AllCeDianList.allcedianlist.ContainsKey(cedianbianhao))
                            {
                                GlobalParams.AllCeDianList.allcedianlist.Remove(cedianbianhao);
                            }
                            dt = GlobalParams.AllCeDianList.ListConvertDataTable(this.Type);
                            this.dataGridView1.DataSource = dt;
                            MainFormRef.updateMainForm();
                            GlobalParams.setDataState();
                            MessageBox.Show("此测点，删除成功。");
                        }
                        else
                        {
                            MessageBox.Show("此测点，删除失败！");
                        }
                    }
                }
                this.cbbLoc.SelectedIndex = -1;
                this.dataGridView1.ClearSelection();
                this.cbbSub.SelectedIndex = -1;
                this.cbbChannel.SelectedIndex = -1;
                this.flag = 0;
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

        private void fenZhanOption(bool flag3)
        {
            this.label5.Visible = !flag3;
            this.cbbChannel.Visible = !flag3;
            this.label3.Visible = flag3;
            this.comboBox1.Visible = flag3;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ModifyBt = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cbAlarm = new System.Windows.Forms.CheckBox();
            this.cbbChannel = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DelButton = new System.Windows.Forms.Button();
            this.cbbSub = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbLoc = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Chocolate;
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(232, 192);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 31);
            this.button1.TabIndex = 16;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnConfirm.BackColor = System.Drawing.SystemColors.Control;
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.ForeColor = System.Drawing.Color.Black;
            this.btnConfirm.Location = new System.Drawing.Point(372, 240);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(100, 31);
            this.btnConfirm.TabIndex = 15;
            this.btnConfirm.Text = "增加";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1018, 342);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.AutoSize = true;
            this.groupBox4.Controls.Add(this.ModifyBt);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.dataGridView2);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.comboBox1);
            this.groupBox4.Controls.Add(this.cbAlarm);
            this.groupBox4.Controls.Add(this.btnConfirm);
            this.groupBox4.Controls.Add(this.cbbChannel);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.DelButton);
            this.groupBox4.Controls.Add(this.cbbSub);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.cbbLoc);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(10, 350);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(1008, 655);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "测点信息";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // ModifyBt
            // 
            this.ModifyBt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ModifyBt.BackColor = System.Drawing.SystemColors.Control;
            this.ModifyBt.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ModifyBt.ForeColor = System.Drawing.Color.Black;
            this.ModifyBt.Location = new System.Drawing.Point(585, 241);
            this.ModifyBt.Margin = new System.Windows.Forms.Padding(4);
            this.ModifyBt.Name = "ModifyBt";
            this.ModifyBt.Size = new System.Drawing.Size(100, 31);
            this.ModifyBt.TabIndex = 38;
            this.ModifyBt.Text = "修改";
            this.ModifyBt.UseVisualStyleBackColor = false;
            this.ModifyBt.Click += new System.EventHandler(this.ModifyBt_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(7, 224);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "分站类型";
            this.label3.Visible = false;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.ColumnHeadersHeight = 30;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.Location = new System.Drawing.Point(237, 25);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(763, 205);
            this.dataGridView2.TabIndex = 12;
            this.dataGridView2.TabStop = false;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(81, 216);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(144, 24);
            this.comboBox1.TabIndex = 17;
            this.comboBox1.Visible = false;
            this.comboBox1.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // cbAlarm
            // 
            this.cbAlarm.AutoSize = true;
            this.cbAlarm.Checked = true;
            this.cbAlarm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAlarm.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbAlarm.ForeColor = System.Drawing.Color.Red;
            this.cbAlarm.Location = new System.Drawing.Point(173, 25);
            this.cbAlarm.Margin = new System.Windows.Forms.Padding(4);
            this.cbAlarm.Name = "cbAlarm";
            this.cbAlarm.Size = new System.Drawing.Size(56, 18);
            this.cbAlarm.TabIndex = 9;
            this.cbAlarm.Text = "报警";
            this.cbAlarm.UseVisualStyleBackColor = false;
            this.cbAlarm.CheckedChanged += new System.EventHandler(this.cbAlarm_CheckedChanged);
            // 
            // cbbChannel
            // 
            this.cbbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbChannel.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbChannel.FormattingEnabled = true;
            this.cbbChannel.Location = new System.Drawing.Point(80, 165);
            this.cbbChannel.Margin = new System.Windows.Forms.Padding(4);
            this.cbbChannel.Name = "cbbChannel";
            this.cbbChannel.Size = new System.Drawing.Size(144, 28);
            this.cbbChannel.TabIndex = 8;
            this.cbbChannel.DropDown += new System.EventHandler(this.cbbChannel_DropDown);
            this.cbbChannel.SelectedIndexChanged += new System.EventHandler(this.cbbChannel_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(23, 170);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "通道号";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // DelButton
            // 
            this.DelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DelButton.BackColor = System.Drawing.SystemColors.Control;
            this.DelButton.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DelButton.ForeColor = System.Drawing.Color.Black;
            this.DelButton.Location = new System.Drawing.Point(780, 240);
            this.DelButton.Margin = new System.Windows.Forms.Padding(4);
            this.DelButton.Name = "DelButton";
            this.DelButton.Size = new System.Drawing.Size(100, 31);
            this.DelButton.TabIndex = 11;
            this.DelButton.Text = "删除";
            this.DelButton.UseVisualStyleBackColor = false;
            this.DelButton.Click += new System.EventHandler(this.DelButton_Click);
            // 
            // cbbSub
            // 
            this.cbbSub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSub.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbSub.FormattingEnabled = true;
            this.cbbSub.Location = new System.Drawing.Point(80, 112);
            this.cbbSub.Margin = new System.Windows.Forms.Padding(4);
            this.cbbSub.Name = "cbbSub";
            this.cbbSub.Size = new System.Drawing.Size(144, 28);
            this.cbbSub.TabIndex = 6;
            this.cbbSub.DropDown += new System.EventHandler(this.cbbSub_DropDown);
            this.cbbSub.SelectedIndexChanged += new System.EventHandler(this.cbbSub_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(39, 116);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "分站";
            // 
            // cbbLoc
            // 
            this.cbbLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbLoc.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbLoc.FormattingEnabled = true;
            this.cbbLoc.Location = new System.Drawing.Point(80, 59);
            this.cbbLoc.Margin = new System.Windows.Forms.Padding(4);
            this.cbbLoc.Name = "cbbLoc";
            this.cbbLoc.Size = new System.Drawing.Size(144, 28);
            this.cbbLoc.TabIndex = 3;
            this.cbbLoc.DropDown += new System.EventHandler(this.cbbLoc_DropDown);
            this.cbbLoc.SelectedIndexChanged += new System.EventHandler(this.cbbLoc_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(39, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "位置";
            // 
            // TestPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox4);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TestPoint";
            this.Size = new System.Drawing.Size(1034, 661);
            this.Load += new System.EventHandler(this.TestPoint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void ModifyBt_Click(object sender, EventArgs e)
        {
            if (this.flag != 1)
            {
                MessageBox.Show("请选择要修改的测点");
            }
            else if (this.cbbLoc.SelectedIndex == -1)
            {
                MessageBox.Show("请选择测点位置");
            }
            else if (this.cbbSub.SelectedIndex == -1)
            {
                MessageBox.Show("请选择分站号");
            }
            else if ((this.cbbChannel.SelectedIndex == -1) && ((this.Type != 1) || ((this.Type == 1) && (this.dataGridView2.CurrentRow.Cells["chuanGanQiLeiXing2"].Value.ToString() != "分站量"))))
            {
                MessageBox.Show("请选择通道号");
            }
            else if (((this.Type == 1) && (this.dataGridView2.CurrentRow.Cells["chuanGanQiLeiXing2"].Value.ToString() == "分站量")) && (this.comboBox1.SelectedIndex == -1))
            {
                MessageBox.Show("请设置分站量的分站类型");
            }
            else
            {
                string temp1;
                string temp2;
                int tempc;
                bool isbj;
                if (this.cbbSub.Text.Length == 1)
                {
                    temp1 = "0" + this.cbbSub.Text;
                }
                else
                {
                    temp1 = this.cbbSub.Text;
                }
                if (this.cbbChannel.Text.Length == 1)
                {
                    temp2 = "0" + this.cbbChannel.Text;
                }
                else
                {
                    temp2 = this.cbbChannel.Text;
                }
                if (this.Type == 0)
                {
                    tempc = DuanDianGuanXi.Count(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString()) + KuiDianGuanXi.Count(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString());
                    if (tempc > 0)
                    {
                        MessageBox.Show("存在断电或馈电关系，请先删除关系后再更改该测点");
                    }
                    else
                    {
                        string ceDianBianHao = temp1 + "A" + temp2;
                        isbj = this.cbAlarm.Checked;
                        if (GlobalParams.AllCeDianList.UpdateCeDian(this.cbbLoc.SelectedItem.ToString(), this.dataGridView2.CurrentRow.Cells["mingCheng"].Value.ToString(), this.dataGridView2.CurrentRow.Cells["leiXing"].Value.ToString(), this.cbbSub.SelectedItem.ToString(), this.cbbChannel.SelectedItem.ToString(), ceDianBianHao, this.cdbhSelect, isbj))
                        {
                            Log.WriteLog(LogType.CeDianPeiZhi, "修改测点#$模拟量#$" + ceDianBianHao + "#$" + this.cbbLoc.SelectedItem.ToString() + "#$" + this.cbbSub.SelectedItem.ToString() + "#$" + this.cbbChannel.SelectedItem.ToString());
                            if (this.cdbhSelect != ceDianBianHao)
                            {
                                OperateDBAccess.Execute("update  LieBiaoAndCeDian set ceDianBianHao='" + ceDianBianHao + "' where ceDianBianHao='" + this.cdbhSelect + "'");
                            }
                            GlobalParams.setDataState();
                            this.UpdateCeDian();
                            MainFormRef.updateMainForm();
                            MessageBox.Show("测点修改成功");
                        }
                        else
                        {
                            MainFormRef.updateMainForm();
                            MessageBox.Show("测点修改失败");
                        }
                    }
                }
                else if (this.Type == 1)
                {
                    string ceDianBiaoHao;
                    string tongDao;
                    string fenZhanLeiXing;
                    bool isFenZhanLiang = false;
                    if (Convert.ToByte(this.dataGridView2.CurrentRow.Cells["leiXing"].Value) == 8)
                    {
                        ceDianBiaoHao = temp1 + "F00";
                        tongDao = "0";
                        fenZhanLeiXing = this.comboBox1.SelectedItem.ToString();
                        isFenZhanLiang = true;
                    }
                    else
                    {
                        ceDianBiaoHao = temp1 + "D" + temp2;
                        tongDao = this.cbbChannel.SelectedItem.ToString();
                        fenZhanLeiXing = "";
                    }
                    tempc = DuanDianGuanXi.Count(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString()) + KuiDianGuanXi.Count(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString());
                    if (tempc > 0)
                    {
                        MessageBox.Show("存在断电或馈电关系，请先删除关系后再更改该测点");
                    }
                    else if (DuoSheBei.CountMulti(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString()) > 0)
                    {
                        MessageBox.Show("存在多设备报警关系，请先删除关系后再更改该测点");
                    }
                    else
                    {
                        isbj = this.cbAlarm.Checked;
                        if (GlobalParams.AllCeDianList.UpdateCeDian(this.cbbLoc.SelectedItem.ToString(), this.dataGridView2.CurrentRow.Cells["mingCheng"].Value.ToString(), this.dataGridView2.CurrentRow.Cells["leiXing"].Value.ToString(), this.cbbSub.SelectedItem.ToString(), tongDao, ceDianBiaoHao, fenZhanLeiXing, this.cdbhSelect, isbj))
                        {
                            if (this.cdbhSelect != ceDianBiaoHao)
                            {
                                OperateDBAccess.Execute("update  LieBiaoAndCeDian set ceDianBianHao='" + ceDianBiaoHao + "' where ceDianBianHao='" + this.cdbhSelect + "'");
                            }
                            if (!isFenZhanLiang)
                            {
                                Log.WriteLog(LogType.CeDianPeiZhi, "修改测点#$开关量#$" + ceDianBiaoHao + "#$" + this.cbbLoc.SelectedItem.ToString() + "#$" + this.cbbSub.SelectedItem.ToString() + "#$" + this.cbbChannel.SelectedItem.ToString());
                            }
                            else
                            {
                                Log.WriteLog(LogType.CeDianPeiZhi, string.Concat(new object[] { "修改测点#$开关量#$", ceDianBiaoHao, "#$", this.cbbLoc.SelectedItem.ToString(), "#$", this.cbbSub.SelectedItem.ToString(), "#$", 0 }));
                            }
                            GlobalParams.setDataState();
                            this.UpdateCeDian();
                            MainFormRef.updateMainForm();
                            MessageBox.Show("测点修改成功");
                        }
                        else
                        {
                            MainFormRef.updateMainForm();
                            MessageBox.Show("测点修改失败");
                        }
                    }
                }
                else
                {
                    tempc = DuanDianGuanXi.CountKong(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString()) + KuiDianGuanXi.CountKong(this.dataGridView1.CurrentRow.Cells["ceDianBianHao"].Value.ToString());
                    if (tempc > 0)
                    {
                        MessageBox.Show("存在断电或馈电关系，请先删除关系后再更改该测点");
                    }
                    else if (OperateDB.ExecuteSql(CeDian.UpdateKongCeDian(this.cbbLoc.SelectedItem.ToString(), this.dataGridView2.CurrentRow.Cells["mingCheng"].Value.ToString(), this.cbbSub.SelectedItem.ToString(), this.cbbChannel.SelectedItem.ToString(), temp1 + "C" + temp2, this.cdbhSelect)))
                    {
                        GlobalParams.setDataState();
                        Log.WriteLog(LogType.CeDianPeiZhi, "修改测点#$控制量#$" + temp1 + "C" + temp2 + "#$" + this.cbbLoc.SelectedItem.ToString() + "#$" + this.cbbSub.SelectedItem.ToString() + "#$" + this.cbbChannel.SelectedItem.ToString());
                        OperateDBAccess.Execute("update  LieBiaoAndCeDian set ceDianBianHao='" + temp1 + "C" + temp2 + "' where ceDianBianHao='" + this.cdbhSelect + "'");
                        this.UpdateCeDian();
                        MainFormRef.updateMainForm();
                        MessageBox.Show("测点修改成功");
                    }
                    else
                    {
                        MainFormRef.updateMainForm();
                        MessageBox.Show("测点修改失败");
                    }
                }
                this.flag = 0;
            }
        }

        private void TestPoint_Load(object sender, EventArgs e)
        {
            this.UpdateCeDian();
        }

        private void UpdateCeDian()
        {
            DataTable dt1;
            this.cbbLoc.Items.Clear();
            this.cbbSub.Items.Clear();
            this.cbbChannel.Items.Clear();
            this.comboBox1.Items.Clear();
            if (this.Type == 0)
            {
                
                dt1 = GlobalParams.AllmnlLeiXing.ConvertoDataTable();
               // dt1.Columns.Add("chuanGanQiLeiXing2");
                foreach (DataRow row in dt1.Rows)
                {
                    switch (row["leiXing"].ToString())
                    {
                        case "2":
                            row["chuanGanQiLeiXing2"] = "累计模拟量";
                            break;

                        case "1":
                            row["chuanGanQiLeiXing2"] = "电流型模拟量";
                            break;

                        case "5":
                            row["chuanGanQiLeiXing2"] = "频率型模拟量";
                            break;
                    }
                }
                if (dt1 != null)
                {
                    this.dataGridView2.DataSource = dt1;
                    this.dataGridView2.Columns["mingCheng"].HeaderText = "名称";
                    this.dataGridView2.Columns["leiXing"].Visible = false;
                    this.dataGridView2.Columns["chuanGanQiLeiXing2"].HeaderText = "通道类型";
                    this.dataGridView2.Columns["danWei"].HeaderText = "单位";
                    this.dataGridView2.Columns["guanJianZi"].HeaderText = "关键字";
                    this.dataGridView2.Columns["baoJingZhiShangXian"].HeaderText = "报警值";
                    this.dataGridView2.Columns["liangChengGao"].HeaderText = "量程高值";
                    this.dataGridView2.Columns["liangChengDi"].HeaderText = "量程低值";
                    this.dataGridView2.Columns["duanDianZhi"].HeaderText = "断电值";
                    this.dataGridView2.Columns["fuDianZhi"].HeaderText = "复电值";
                }
                //else if (Common.DEBUG == 1)
                //{
                //    MessageBox.Show("MoNiLiangLeiXing.GetMoNiLiang()返回空");
                //}
            }
            else if (this.Type == 1)
            {
                dt1 = GlobalParams.AllkgLeiXing.ListConvertDataTable();
                dt1.Columns.Add("chuanGanQiLeiXing2");
                foreach (DataRow row in dt1.Rows)
                {
                    switch (row["leiXing"].ToString())
                    {
                        case "3":
                            row["chuanGanQiLeiXing2"] = "二态开关量";
                            break;

                        case "8":
                            row["chuanGanQiLeiXing2"] = "分站量";
                            break;

                        case "4":
                            row["chuanGanQiLeiXing2"] = "三态开关量";
                            break;

                        case "7":
                            row["chuanGanQiLeiXing2"] = "通断量";
                            break;
                    }
                }
                if (dt1 != null)
                {
                    this.dataGridView2.DataSource = dt1;
                    this.dataGridView2.Columns["mingCheng"].HeaderText = "名称";
                    this.dataGridView2.Columns["leiXing"].Visible = false;
                    this.dataGridView2.Columns["chuanGanQiLeiXing2"].HeaderText = "通道类型";
                    this.dataGridView2.Columns["lingTaiMingCheng"].HeaderText = "0态名称";
                    this.dataGridView2.Columns["yiTaiMingCheng"].HeaderText = "1态名称";
                    this.dataGridView2.Columns["erTaiMingCheng"].HeaderText = "2态名称";
                    this.dataGridView2.Columns["baoJingZhuangTai"].HeaderText = "报警状态";
                    this.dataGridView2.Columns["duanDianZhuangTai"].HeaderText = "断电状态";
                }
                //else if (Common.DEBUG == 1)
                //{
                //    MessageBox.Show("KaiGuanLiangLeiXing.GetSwitch()返回空");
                //}
            }
            else
            {
                dt1 = GlobalParams.AllkzlLeiXing.ListConvertDataTable();
                if (dt1 != null)
                {
                    dt1.Columns.Add("kongZhiLiangLeiXing2");
                    foreach (DataRow row in dt1.Rows)
                    {
                        switch (row["kongZhiLiangLeiXing"].ToString())
                        {
                            case "1":
                                row["kongZhiLiangLeiXing2"] = "常开";
                                break;

                            case "2":
                                row["kongZhiLiangLeiXing2"] = "常闭";
                                break;

                            case "3":
                                row["kongZhiLiangLeiXing2"] = "电平";
                                break;
                        }
                    }
                    this.dataGridView2.DataSource = dt1;
                    this.dataGridView2.Columns["mingCheng"].HeaderText = "控制量名称";
                    this.dataGridView2.Columns["lingTaiMingCheng"].HeaderText = "0态名称";
                    this.dataGridView2.Columns["yiTaiMingCheng"].HeaderText = "1态名称";
                    this.dataGridView2.Columns["kongZhiLiangLeiXing2"].HeaderText = "控制量类型";
                    this.dataGridView2.Columns["kongZhiLiangLeiXing"].Visible = false;
                }
                //else if (Common.DEBUG == 1)
                //{
                //    MessageBox.Show("KongZhiLiang.GetKong()返回空");
                //}
            }
            DataTable dt2 = GlobalParams.AllCeDianList.ListConvertDataTable(this.Type);
            dt2.Columns.Add("chuanGanQiLeiXing2");
            if (this.Type != 2)
            {
                foreach (DataRow row in dt2.Rows)
                {
                    switch (Convert.ToInt32(row["chuanGanQiLeiXing"].ToString()))
                    {
                        case 1:
                            row["chuanGanQiLeiXing2"] = "电流型模拟量";
                            break;

                        case 2:
                            row["chuanGanQiLeiXing2"] = "累计模拟量";
                            break;

                        case 3:
                            row["chuanGanQiLeiXing2"] = "二态开关量";
                            break;

                        case 4:
                            row["chuanGanQiLeiXing2"] = "三态开关量";
                            break;

                        case 5:
                            row["chuanGanQiLeiXing2"] = "频率型模拟量";
                            break;

                        case 7:
                            row["chuanGanQiLeiXing2"] = "通断量";
                            break;

                        case 8:
                            row["chuanGanQiLeiXing2"] = "分站量";
                            break;

                        default:
                            row["chuanGanQiLeiXing2"] = "";
                            break;
                    }
                    if (row["ceDianBianHao"].ToString()[2] == 'F')
                    {
                        row["xiaoLeiXing"] = row["xiaoLeiXing"] + "/" + row["chuanGanQiZhiShi"];
                    }
                }
            }
            if (dt2 != null)
            {
                this.dataGridView1.DataSource = dt2;
                this.dataGridView1.Columns["weiShanChu"].Visible = false;
                if (this.Type != 2)
                {
                    this.dataGridView1.Columns["daLeiXIng"].Visible = false;
                    this.dataGridView1.Columns["tiaoJiao"].Visible = false;
                    this.dataGridView1.Columns["baoJing"].Visible = false;
                    this.dataGridView1.Columns["chuanGanQiLeiXing"].Visible = false;
                    this.dataGridView1.Columns["chuanGanQiZhiShi"].Visible = false;
                    this.dataGridView1.Columns["deleteDate"].Visible = false;
                    this.dataGridView1.Columns["chuanGanQiLeiXing2"].HeaderText = "通道类型";
                    this.dataGridView1.Columns["ceDianWeiZhi"].HeaderText = "测点位置";
                    this.dataGridView1.Columns["xiaoLeiXing"].HeaderText = "测点类型";
                    this.dataGridView1.Columns["tongDaoHao"].HeaderText = "通道号";
                    this.dataGridView1.Columns["fenZhanHao"].HeaderText = "分站号";
                    this.dataGridView1.Columns["id"].HeaderText = "测点标识";
                    this.dataGridView1.Columns["ceDianBianHao"].HeaderText = "测点编号";
                    this.dataGridView1.Columns["createDate"].HeaderText = "创建日期";
                    if (this.Type == 1)
                    {
                        this.dataGridView1.Columns["chuanGanQiZhiShi"].Visible = false;
                        this.dataGridView1.Columns["chuanGanQiLeiXing"].Visible = false;
                        this.dataGridView1.Columns["deleteDate"].Visible = false;
                    }
                }
                else
                {
                    this.dataGridView1.Columns["chuanGanQiLeiXing2"].Visible = false;
                    this.dataGridView1.Columns["xiaoLeiXing"].HeaderText = "名称";
                    this.dataGridView1.Columns["ceDianBianHao"].HeaderText = "测点编号";
                    this.dataGridView1.Columns["ceDianWeiZhi"].HeaderText = "测点位置";
                    this.dataGridView1.Columns["fenZhanHao"].HeaderText = "分站号";
                    this.dataGridView1.Columns["tongDaoHao"].HeaderText = "控制量编号";
                    this.dataGridView1.Columns["id"].HeaderText = "测点标识";
                    this.dataGridView1.Columns["ceDianBianHao"].HeaderText = "测点编号";
                    this.dataGridView1.Columns["createDate"].HeaderText = "创建日期";
                    this.dataGridView1.Columns["daLeiXing"].Visible = false;
                    this.dataGridView1.Columns["chuanGanQiLeiXing"].Visible = false;
                    this.dataGridView1.Columns["chuanGanQiZhiShi"].Visible = false;
                    this.dataGridView1.Columns["deleteDate"].Visible = false;
                    this.dataGridView1.Columns["tiaoJiao"].Visible = false;
                    this.dataGridView1.Columns["baoJing"].Visible = false;
                }
            }
            //else if (Common.DEBUG == 1)
            //{
            //    MessageBox.Show("CeDian.GetCeDian(" + this.Type + ")返回空");
            //}
        }

        private void cbAlarm_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

