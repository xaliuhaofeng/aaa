namespace MAX_CMSS_V2
{
    using Logic;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;

    public class List_show : DockContentEx2
    {
        private IContainer components;
        public int id;
        private LieBiaoKuang[] lieBiaoKuangs;
        private MainForm mf_g;
        private TableLayoutPanel tableLayoutPanel1;
        private string yeKuangMingCheng;

        public List_show(MainForm mf, string yeKuangMingCheng, bool isOpen)
        {
            int i;
            this.id = 0;
            this.components = null;
            this.InitializeComponent();
            base.tsmiClose.Text = "删除页框";
            this.Text = this.yeKuangMingCheng = yeKuangMingCheng;
            this.mf_g = mf;
            this.lieBiaoKuangs = new LieBiaoKuang[3];
            if (isOpen)
            {
                for (i = 0; i < 3; i++)
                {
                    this.lieBiaoKuangs[i] = new LieBiaoKuang(this.yeKuangMingCheng, i + 1, this.mf_g, true, this);
                }
            }
            else
            {
                OperateDBAccess.Execute("insert into YeKuang(name,lieBiaoKuangShuLiang) values('" + this.yeKuangMingCheng + "',3)");
                for (i = 0; i < 3; i++)
                {
                    this.lieBiaoKuangs[i] = new LieBiaoKuang(this.yeKuangMingCheng, i + 1, this.mf_g, false, this);
                }
            }
            this.lieBiaoKuangs[0].Dock = DockStyle.Fill;
            this.lieBiaoKuangs[1].Dock = DockStyle.Fill;
            this.lieBiaoKuangs[2].Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(this.lieBiaoKuangs[0], 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lieBiaoKuangs[1], 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lieBiaoKuangs[2], 2, 0);
            this.LayoutTable();
        }


        public void setInit()
        {
            for (int i = 0; i < 3; i++)
            {
                this.lieBiaoKuangs[i].GetInfoFromDB();
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
            this.tableLayoutPanel1 = new TableLayoutPanel();
            base.SuspendLayout();
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel1.BackColor = Color.White;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32.30769f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.25641f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.Size = new Size(0x2fc, 0x12f);
            this.tableLayoutPanel1.TabIndex = 1;
            base.ClientSize = new Size(0x2fc, 0x142);
            base.CloseButton = false;
            base.Controls.Add(this.tableLayoutPanel1);
             base.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Name = "List_show";
            base.Activated += new EventHandler(this.List_show_Activated);
            base.ResumeLayout(false);
        }

        private void LayoutTable()
        {
            int i;
            int visibleCount = 0;
            for (i = 0; i < 3; i++)
            {
                if (this.lieBiaoKuangs[i].xianShi)
                {
                    visibleCount++;
                }
            }
            for (i = 0; i < 3; i++)
            {
                if (this.lieBiaoKuangs[i].xianShi)
                {
                    switch (visibleCount)
                    {
                        case 1:
                            this.tableLayoutPanel1.ColumnStyles[i].Width = 100f;
                            break;

                        case 2:
                            this.tableLayoutPanel1.ColumnStyles[i].Width = 50f;
                            break;

                        case 3:
                            this.tableLayoutPanel1.ColumnStyles[i].Width = 33.33f;
                            break;
                    }
                }
                else
                {
                    this.tableLayoutPanel1.ColumnStyles[i].Width = 0f;
                }
            }
        }

        private void List_show_Activated(object sender, EventArgs e)
        {
            for (int i = 0; i < this.lieBiaoKuangs.Length; i++)
            {
                if (this.lieBiaoKuangs[i] != null)
                {
                    //if (GlobalParams.Comm_state == 0)
                    //{
                    //    this.lieBiaoKuangs[i].update_comm_interupt();
                    //}
                    //else
                    //{
                        this.lieBiaoKuangs[i].freshData();
                    //}
                }
            }
            this.mf_g.list_view_id = this.id;
        }

        public void ReGet(string yeKuangMingCheng)
        {
            this.Text = this.yeKuangMingCheng = yeKuangMingCheng;
            this.lieBiaoKuangs = new LieBiaoKuang[3];
            this.tableLayoutPanel1.Controls.Clear();
            for (int i = 0; i < 3; i++)
            {
                this.lieBiaoKuangs[i] = new LieBiaoKuang(this.yeKuangMingCheng, i + 1, this.mf_g, true, this);
            }
            this.lieBiaoKuangs[0].Dock = DockStyle.Fill;
            this.lieBiaoKuangs[1].Dock = DockStyle.Fill;
            this.lieBiaoKuangs[2].Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(this.lieBiaoKuangs[0], 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lieBiaoKuangs[1], 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lieBiaoKuangs[2], 2, 0);
            this.LayoutTable();
        }

        public override void tsmiClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要删除该页框吗？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OperateDBAccess.Execute("delete from YeKuang where name='" + this.yeKuangMingCheng + "'");
                OperateDBAccess.Execute("delete from LieBiaoKuang where yeKuangName='" + this.yeKuangMingCheng + "'");
                OperateDBAccess.Execute("delete from LieBiaoAndCeDian where yeKuangName='" + this.yeKuangMingCheng + "'");
                this.mf_g.yeKuangs.Remove(this);
                base.Close();
            }
        }

        public override void tsmiRename_Click(object sender, EventArgs e)
        {
            NewTab tab = new NewTab {
                TopMost = true
            };
            if (tab.ShowDialog() == DialogResult.OK)
            {
                string tabName = tab.TabName;
                if ((tabName != string.Empty) && (tabName != this.YeKuangMingCheng))
                {
                    foreach (List_show p in this.mf_g.yeKuangs)
                    {
                        if (p.YeKuangMingCheng.Equals(tabName))
                        {
                            MessageBox.Show("名称为[" + tabName + "]的标签页已经存在！");
                            return;
                        }
                    }
                    OperateDBAccess.Execute("update YeKuang set name = '" + tabName + "' where name = '" + this.yeKuangMingCheng + "'");
                    OperateDBAccess.Execute("update LieBiaoKuang set yeKuangName = '" + tabName + "' where yeKuangName = '" + this.yeKuangMingCheng + "'");
                    OperateDBAccess.Execute("update LieBiaoAndCeDian set yeKuangName = '" + tabName + "' where yeKuangName = '" + this.yeKuangMingCheng + "'");
                    this.Text = this.yeKuangMingCheng = tabName;
                    for (int i = 0; i < this.lieBiaoKuangs.Length; i++)
                    {
                        this.lieBiaoKuangs[i].yeKuangMingCheng = tabName;
                    }
                }
            }
        }

        public void updateAllLieBiaoKuang()
        {
            foreach (LieBiaoKuang liebiao in this.lieBiaoKuangs)
            {
                liebiao.GetInfoFromDB();
            }
        }

        public LieBiaoKuang[] LieBiaoKuangs
        {
            get
            {
                return this.lieBiaoKuangs;
            }
        }

        public string YeKuangMingCheng
        {
            get
            {
                return this.yeKuangMingCheng;
            }
            set
            {
                this.yeKuangMingCheng = value;
            }
        }
    }
}

