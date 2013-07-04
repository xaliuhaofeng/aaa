namespace MyPictureBox
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class EditMoNiTu : UserControl
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private IContainer components = null;
        private FullPicture fullPicture;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private string moNiTiType;
        private Button OpenImg;
        private SplitContainer splitContainer1;
        private TextBox textBox1;
        private TreeView treeView1;
        private int type;

        public EditMoNiTu(string s)
        {
            this.InitializeComponent();
            this.moNiTiType = s;
            this.label1.Text = "编辑" + s + "模拟图";
            this.fullPicture = new FullPicture();
            this.fullPicture.Dock = DockStyle.Fill;
            this.splitContainer1.Panel1.Controls.Add(this.fullPicture);
            this.treeView1.ExpandAll();
            this.treeView1.SelectedNode = this.treeView1.Nodes["静态图标"];
            this.fullPicture.OpenImg(this.moNiTiType, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.fullPicture.FangDa(0.2f);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.fullPicture.SuoXiao(0.2f);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!this.comboBox1.Visible)
            {
                MessageBox.Show("请选择要添加的图元类型");
            }
            else if (this.comboBox1.SelectedItem == null)
            {
                MessageBox.Show("请选择图元对应的测点编号");
            }
            else
            {
                string temp = "";
                if (((this.type <= 4) || (this.type == 5)) || (this.type == 7))
                {
                    if (this.comboBox1.SelectedItem.ToString() == "")
                    {
                        MessageBox.Show("输入特征");
                        return;
                    }
                    temp = this.comboBox1.SelectedItem.ToString();
                }
                else if (this.type == 6)
                {
                    if (this.textBox1.Text == "")
                    {
                        MessageBox.Show("输入特征");
                        return;
                    }
                    temp = this.textBox1.Text;
                }
                string ceDianBianHao = this.comboBox1.SelectedItem.ToString().Substring(0, 5);
                if (this.type == 5)
                {
                    temp = this.comboBox2.SelectedItem.ToString();
                    if (ceDianBianHao[2] == 'A')
                    {
                        if (OperateDB.Select("select mingCheng from MoNiLiangLeiXing where mingCheng='" + temp + "'").Rows.Count <= 0)
                        {
                            MessageBox.Show("测点与类型不匹配");
                            return;
                        }
                    }
                    else if ((ceDianBianHao[2] == 'D') && (OperateDB.Select("select mingCheng from KaiGuanLiangLeiXing where mingCheng='" + temp + "'").Rows.Count <= 0))
                    {
                        MessageBox.Show("测点与类型不匹配");
                        return;
                    }
                }
                this.fullPicture.AddATuYuan(this.type, temp, ceDianBianHao);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.fullPicture.SaveMoNiTu();
            MessageBox.Show("保存成功！");
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
            TreeNode treeNode1 = new TreeNode("旋转");
            TreeNode treeNode2 = new TreeNode("流水");
            TreeNode treeNode3 = new TreeNode("闪烁");
            TreeNode treeNode4 = new TreeNode("红绿灯");
            TreeNode treeNode5 = new TreeNode("开关量动画图元", new TreeNode[] { treeNode1, treeNode2, treeNode3, treeNode4 });
            TreeNode treeNode6 = new TreeNode("动态文字图元");
            TreeNode treeNode7 = new TreeNode("静态文字图元");
            TreeNode treeNode8 = new TreeNode("静态图标");
            TreeNode treeNode9 = new TreeNode("添加图元", new TreeNode[] { treeNode5, treeNode6, treeNode7, treeNode8 });
            this.splitContainer1 = new SplitContainer();
            this.label4 = new Label();
            this.comboBox2 = new ComboBox();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.textBox1 = new TextBox();
            this.comboBox1 = new ComboBox();
            this.button3 = new Button();
            this.button4 = new Button();
            this.treeView1 = new TreeView();
            this.button2 = new Button();
            this.OpenImg = new Button();
            this.button1 = new Button();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            base.SuspendLayout();
            this.splitContainer1.BorderStyle = BorderStyle.Fixed3D;
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Margin = new Padding(4, 3, 4, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel2.BackColor = Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.comboBox2);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Panel2.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel2.Controls.Add(this.button3);
            this.splitContainer1.Panel2.Controls.Add(this.button4);
            this.splitContainer1.Panel2.Controls.Add(this.treeView1);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.OpenImg);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Size = new Size(0x4ac, 540);
            this.splitContainer1.SplitterDistance = 0x388;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            this.label4.AutoSize = true;
            this.label4.ForeColor = Color.CornflowerBlue;
            this.label4.Location = new Point(6, 0x15a);
            this.label4.Margin = new Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x54, 0x12);
            this.label4.TabIndex = 0x17;
            this.label4.Text = "显示内容";
            this.label4.Visible = false;
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new Point(0x5c, 300);
            this.comboBox2.Margin = new Padding(4, 3, 4, 3);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(0xb0, 0x19);
            this.comboBox2.TabIndex = 0x16;
            this.comboBox2.Visible = false;
            this.label3.AutoSize = true;
            this.label3.ForeColor = Color.CornflowerBlue;
            this.label3.Location = new Point(6, 0x12f);
            this.label3.Margin = new Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x54, 0x12);
            this.label3.TabIndex = 0x15;
            this.label3.Text = "选择类型";
            this.label3.Visible = false;
            this.label2.AutoSize = true;
            this.label2.ForeColor = Color.CornflowerBlue;
            this.label2.Location = new Point(6, 0x103);
            this.label2.Margin = new Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x54, 0x12);
            this.label2.TabIndex = 20;
            this.label2.Text = "选择测点";
            this.label2.Visible = false;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x15, 0xe9);
            this.label1.Margin = new Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x44, 0x12);
            this.label1.TabIndex = 0x13;
            this.label1.Text = "label1";
            this.label1.Click += new EventHandler(this.label1_Click);
            this.textBox1.Location = new Point(0x5c, 0x157);
            this.textBox1.Margin = new Padding(4, 3, 4, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0xb0, 0x1b);
            this.textBox1.TabIndex = 0x12;
            this.textBox1.Visible = false;
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(0x5c, 0xff);
            this.comboBox1.Margin = new Padding(4, 3, 4, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0xb0, 0x19);
            this.comboBox1.TabIndex = 0x11;
            this.comboBox1.Visible = false;
            this.button3.BackColor = Color.Chocolate;
            this.button3.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button3.ForeColor = Color.White;
            this.button3.Location = new Point(0x18, 0x189);
            this.button3.Margin = new Padding(4, 3, 4, 3);
            this.button3.Name = "button3";
            this.button3.Size = new Size(220, 0x1b);
            this.button3.TabIndex = 14;
            this.button3.Text = "在模拟图上添加新测点";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new EventHandler(this.button3_Click);
            this.button4.BackColor = Color.Chocolate;
            this.button4.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button4.ForeColor = Color.White;
            this.button4.Location = new Point(0x18, 0x1ba);
            this.button4.Margin = new Padding(4, 3, 4, 3);
            this.button4.Name = "button4";
            this.button4.Size = new Size(220, 0x1b);
            this.button4.TabIndex = 0x10;
            this.button4.Text = "保存模拟图";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new EventHandler(this.button4_Click);
            this.treeView1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new Point(-4, 0);
            this.treeView1.Margin = new Padding(4, 3, 4, 3);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node2";
            treeNode1.Text = "旋转";
            treeNode2.Name = "Node3";
            treeNode2.Text = "流水";
            treeNode3.Name = "Node4";
            treeNode3.Text = "闪烁";
            treeNode4.Name = "Node5";
            treeNode4.Text = "红绿灯";
            treeNode5.Name = "Node1";
            treeNode5.Text = "开关量动画图元";
            treeNode6.Name = "Node7";
            treeNode6.Text = "动态文字图元";
            treeNode7.Name = "Node8";
            treeNode7.Text = "静态文字图元";
            treeNode8.Name = "Node9";
            treeNode8.Text = "静态图标";
            treeNode9.Name = "Node0";
            treeNode9.Text = "添加图元";
            this.treeView1.Nodes.AddRange(new TreeNode[] { treeNode9 });
            this.treeView1.Size = new Size(0x120, 0xba);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.button2.BackColor = Color.Chocolate;
            this.button2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button2.ForeColor = Color.White;
            this.button2.Location = new Point(0x90, 0xbf);
            this.button2.Margin = new Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new Size(100, 0x1b);
            this.button2.TabIndex = 13;
            this.button2.Text = "缩小";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.OpenImg.BackColor = Color.Chocolate;
            this.OpenImg.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.OpenImg.ForeColor = Color.White;
            this.OpenImg.Location = new Point(0x18, 0x1ec);
            this.OpenImg.Margin = new Padding(4, 3, 4, 3);
            this.OpenImg.Name = "OpenImg";
            this.OpenImg.Size = new Size(220, 0x1b);
            this.OpenImg.TabIndex = 11;
            this.OpenImg.Text = "加载新背景图";
            this.OpenImg.UseVisualStyleBackColor = false;
            this.OpenImg.Click += new EventHandler(this.OpenImg_Click);
            this.button1.BackColor = Color.Chocolate;
            this.button1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.button1.ForeColor = Color.White;
            this.button1.Location = new Point(0x11, 0xbf);
            this.button1.Margin = new Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new Size(100, 0x1b);
            this.button1.TabIndex = 12;
            this.button1.Text = "放大";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            base.AutoScaleDimensions = new SizeF(10f, 17f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.splitContainer1);
            this.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.ForeColor = Color.Chocolate;
            base.Margin = new Padding(4, 3, 4, 3);
            base.Name = "EditMoNiTu";
            base.Size = new Size(0x4ac, 540);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void OpenImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofg = new OpenFileDialog {
                Filter = "图片文件(*.jpg)|*.jpg",
                Multiselect = false
            };
            if ((ofg.ShowDialog() == DialogResult.OK) && (MessageBox.Show("加载新背景图将替换原来背景图及配置，是否继续？", "", MessageBoxButtons.YesNo) != DialogResult.No))
            {
                this.fullPicture.DeleteAllCeDian();
                string newFileName = ofg.FileName;
                string bgPicName = Application.StartupPath + @"\monitor\" + this.moNiTiType + ".jpg";
                if (File.Exists(bgPicName))
                {
                    File.Delete(bgPicName);
                }
                File.Copy(newFileName, bgPicName);
                this.fullPicture.OpenImg(this.moNiTiType, true);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Text)
            {
                case "静态图标":
                    this.comboBox1.DataSource = GlobalParams.AllCeDianList.getCeDianAllInfo((byte) 3);
                    this.comboBox2.DataSource = CeDian.GetAllLeiXing();
                    this.label2.Visible = true;
                    this.comboBox1.Visible = true;
                    this.label3.Visible = true;
                    this.comboBox2.Visible = true;
                    this.label4.Visible = false;
                    this.textBox1.Visible = false;
                    this.type = 5;
                    break;

                case "红绿灯":
                    this.comboBox1.DataSource = GlobalParams.AllCeDianList.getCeDianAllInfo((byte) 1);
                    this.label2.Visible = true;
                    this.comboBox1.Visible = true;
                    this.label3.Visible = false;
                    this.comboBox2.Visible = false;
                    this.label4.Visible = false;
                    this.textBox1.Visible = false;
                    this.type = 3;
                    break;

                case "旋转":
                    this.comboBox1.DataSource = GlobalParams.AllCeDianList.getCeDianAllInfo((byte) 1);
                    this.label2.Visible = true;
                    this.comboBox1.Visible = true;
                    this.label3.Visible = false;
                    this.comboBox2.Visible = false;
                    this.label4.Visible = false;
                    this.textBox1.Visible = false;
                    this.type = 0;
                    break;

                case "流水":
                    this.comboBox1.DataSource = GlobalParams.AllCeDianList.getCeDianAllInfo((byte) 1);
                    this.label2.Visible = true;
                    this.comboBox1.Visible = true;
                    this.label3.Visible = false;
                    this.comboBox2.Visible = false;
                    this.label4.Visible = false;
                    this.textBox1.Visible = false;
                    this.type = 1;
                    break;

                case "闪烁":
                    this.comboBox1.DataSource = GlobalParams.AllCeDianList.getCeDianAllInfo((byte) 1);
                    this.label2.Visible = true;
                    this.comboBox1.Visible = true;
                    this.label3.Visible = false;
                    this.comboBox2.Visible = false;
                    this.label4.Visible = false;
                    this.textBox1.Visible = false;
                    this.type = 2;
                    break;

                case "位移":
                    this.comboBox1.DataSource = GlobalParams.AllCeDianList.getCeDianAllInfo((byte) 1);
                    this.label2.Visible = true;
                    this.comboBox1.Visible = true;
                    this.label3.Visible = false;
                    this.comboBox2.Visible = false;
                    this.label4.Visible = false;
                    this.textBox1.Visible = false;
                    this.type = 4;
                    break;

                case "静态文字图元":
                    this.label2.Visible = true;
                    this.comboBox1.Visible = true;
                    this.label3.Visible = false;
                    this.comboBox2.Visible = false;
                    this.label4.Visible = true;
                    this.textBox1.Visible = true;
                    this.type = 6;
                    break;

                case "动态文字图元":
                    this.comboBox1.DataSource = GlobalParams.AllCeDianList.getCeDianAllInfo((byte) 3);
                    this.label2.Visible = true;
                    this.comboBox1.Visible = true;
                    this.label3.Visible = false;
                    this.comboBox2.Visible = false;
                    this.label4.Visible = false;
                    this.textBox1.Visible = false;
                    this.type = 7;
                    break;
            }
        }
    }
}

