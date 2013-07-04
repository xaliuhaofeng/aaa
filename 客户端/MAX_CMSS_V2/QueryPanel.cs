namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class QueryPanel : UserControl
    {
        private Button button1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private ComboBox comboBox1;
        private IContainer components;
        private DataGridView dataGridView1;
        private DateTimeChooser dateTimeChooser1;
        private Label label1;
        private Logic.LogType logType;
        private PrintButton printButton1;

        public QueryPanel()
        {
            this.components = null;
            this.InitializeComponent();
            this.comboBox1.Items.Add("全部");
            this.printButton1.DataGridView1 = this.dataGridView1;
        }

        public QueryPanel(Logic.LogType type)
        {
            this.components = null;
            this.InitializeComponent();
            this.comboBox1.Items.Add("全部");
            this.logType = type;
        }

        private void addDataToGridView(Log[] logs, int columns)
        {
            foreach (Log log in logs)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1[0, index].Value = log.UserName;
                this.dataGridView1[1, index].Value = log.LogTime;
                string[] splits = new string[] { "#$" };
                string[] ops = log.Operation.Split(splits, StringSplitOptions.None);
                int len = columns - 2;
                if (ops.Length >= len)
                {
                    for (int i = 0; i < len; i++)
                    {
                        this.dataGridView1[i + 2, index].Value = ops[i];
                    }
                }
                else
                {
                    this.dataGridView1[columns - 1, index].Value = log.Operation;
                }
            }
        }

        private void addDataToTiaoJiaoGrid(Log[] logs)
        {
            foreach (Log log in logs)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1[0, index].Value = log.UserName;
                this.dataGridView1[1, index].Value = log.LogTime;
                string[] splits = new string[] { "#$" };
                string[] ops = log.Operation.Split(splits, StringSplitOptions.None);
                if (ops.Length >= 4)
                {
                    this.dataGridView1[2, index].Value = ops[0];
                    this.dataGridView1[3, index].Value = ops[1];
                    this.dataGridView1[4, index].Value = ops[2];
                    this.dataGridView1[5, index].Value = ops[3];
                }
                else
                {
                    this.dataGridView1[2, index].Value = log.Operation;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要查找的用户！");
            }
            else
            {
                string username = this.comboBox1.SelectedItem.ToString();
                DateTime start = this.dateTimeChooser1.StartTime;
                DateTime end = this.dateTimeChooser1.EndTime;
                Log[] logs = Log.GetSelectedLogs(username, this.LogType, new DateTime?(start), new DateTime?(end));
                string[] header = new string[0];
                switch (this.logType)
                {
                    case Logic.LogType.TiaoJiao:
                        header = new string[] { "用户名", "时间", "测点编号", "安装地点", "类型名称", "事件" };
                        break;

                    case Logic.LogType.CeDianPeiZhi:
                        header = new string[] { "用户名", "时间", "操作类型", "测点类型", "测点编号", "安装地点", "分站号", "通道号" };
                        break;

                    case Logic.LogType.FenZhanPeiZhi:
                        header = new string[] { "用户名", "时间", "分站号", "串口号", "通讯状态" };
                        break;

                    case Logic.LogType.GuZhangBiSuo:
                        header = new string[] { "用户名", "时间", "分站号", "故障闭锁状态" };
                        break;

                    case Logic.LogType.FengDianWaSiBiSuo:
                        header = new string[] { "用户名", "时间", "分站号", "各通道闭锁时间", "通讯状态" };
                        break;

                    case Logic.LogType.ShouDongKongZhi:
                        header = new string[] { "用户名", "时间", "操作类型", "分站号", "端口", "端口状态" };
                        break;

                    case Logic.LogType.JiaoShi:
                        header = new string[] { "用户名", "校时时间", "分站号", "操作时间", "通讯状态" };
                        break;

                    case Logic.LogType.MoNiLiang:
                        header = new string[] { "用户名", "时间", "操作标识", "名称", "传感器类型", "单位", "是否报警", "量程高值", "量程低值", "报警上限", "报警下限", "断电值", "复电值" };
                        break;

                    case Logic.LogType.KaiGuanLiang:
                        header = new string[] { "用户名", "时间", "操作标识", "名称", "0态名称", "1态名称", "2态名称", "报警状态" };
                        break;

                    case Logic.LogType.DuoSheBei:
                        header = new string[] { "用户名", "时间", "操作类型", "测点编号", "类型", "地点", "报警状态" };
                        break;

                    case Logic.LogType.KuiDianGuanXi:
                        header = new string[] { "用户名", "时间", "操作类型", "控制量测点编号", "地点", "名称", "馈电量点号" };
                        break;

                    case Logic.LogType.KongZhiLuoJi:
                        header = new string[] { "用户名", "时间", "操作类型", "被控制量测点编号", "地点", "名称", "控制量测点编号", "地点", "名称" };
                        break;
                }
                this.drawDataGridView(header);
                this.addDataToGridView(logs, header.Length);
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

        private void drawDataGridView(string[] headText)
        {
            DataGridViewColumn[] columns = new DataGridViewColumn[headText.Length];
            for (int i = 0; i < headText.Length; i++)
            {
                columns[i] = new DataGridViewTextBoxColumn();
                columns[i].HeaderText = headText[i];
            }
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.Columns.AddRange(columns);
        }

        private void drawTiaoJiaoGrid()
        {
            DataGridViewTextBoxColumn userName = new DataGridViewTextBoxColumn {
                HeaderText = "用户名"
            };
            DataGridViewTextBoxColumn time = new DataGridViewTextBoxColumn {
                HeaderText = "时间"
            };
            DataGridViewTextBoxColumn ceDianBianHao = new DataGridViewTextBoxColumn {
                HeaderText = "测点编号"
            };
            DataGridViewTextBoxColumn anZhuangDiDian = new DataGridViewTextBoxColumn {
                HeaderText = "安装地点"
            };
            DataGridViewTextBoxColumn mingCheng = new DataGridViewTextBoxColumn {
                HeaderText = "类型名称"
            };
            DataGridViewTextBoxColumn tiaoJiaoEvent = new DataGridViewTextBoxColumn {
                HeaderText = "调校事件"
            };
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { userName, time, ceDianBianHao, anZhuangDiDian, mingCheng, tiaoJiaoEvent });
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.printButton1 = new MAX_CMSS_V2.PrintButton();
            this.dateTimeChooser1 = new MAX_CMSS_V2.DateTimeChooser();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(14, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(103, 43);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(231, 24);
            this.comboBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(470, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 27);
            this.button1.TabIndex = 3;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
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
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(17, 71);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(989, 600);
            this.dataGridView1.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "用户名";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "操作时间";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "事件";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // printButton1
            // 
            this.printButton1.BackColor = System.Drawing.SystemColors.Control;
            this.printButton1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.printButton1.Footer = "页脚";
            this.printButton1.ForeColor = System.Drawing.Color.Black;
            this.printButton1.Location = new System.Drawing.Point(604, 36);
            this.printButton1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.printButton1.Name = "printButton1";
            this.printButton1.Size = new System.Drawing.Size(107, 29);
            this.printButton1.SubTitle = "这是默认设置";
            this.printButton1.TabIndex = 5;
            this.printButton1.Title = "DataGridView打印";
            // 
            // dateTimeChooser1
            // 
            this.dateTimeChooser1.EndTime = DateTime.Now;
            this.dateTimeChooser1.Location = new System.Drawing.Point(3, 3);
            this.dateTimeChooser1.Name = "dateTimeChooser1";
            this.dateTimeChooser1.Size = new System.Drawing.Size(1019, 40);
            DateTime dt=DateTime.Now;
            this.dateTimeChooser1.StartTime =new DateTime(dt.Year,dt.Month,dt.Day,0,0,0);
            this.dateTimeChooser1.TabIndex = 2;
            // 
            // QueryPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.printButton1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimeChooser1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "QueryPanel";
            this.Size = new System.Drawing.Size(1025, 600);
            this.Load += new System.EventHandler(this.QueryPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void QueryPanel_Load(object sender, EventArgs e)
        {
            this.comboBox1.Items.AddRange(Users.GetAllUserName());
        }

        public Logic.LogType LogType
        {
            get
            {
                return this.logType;
            }
            set
            {
                this.logType = value;
            }
        }
    }
}

