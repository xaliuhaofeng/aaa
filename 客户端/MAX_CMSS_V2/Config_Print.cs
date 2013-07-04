namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Config_Print : UserControl
    {
        private IContainer components = null;
        private ConfigPrintA configPrintA1;
        private ConfigPrintABaoJing configPrintABaoJing1;
        private ConfigPrintADuanDian configPrintADuanDian1;
        private ConfigPrintAKuiDian configPrintAKuiDian1;
        private ConfigPrintATongJi configPrintATongJi1;
        private ConfigPrintDBaoJing configPrintDBaoJing1;
        private ConfigPrintDKuiDian configPrintDKuiDian1;
        private ConfigPrintDState configPrintDState1;
        private ConfigPrintSheBeiGuZhang configPrintSheBeiGuZhang1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private TabPage tabPage8;
        private TabPage tabPage9;

        public Config_Print()
        {
            this.InitializeComponent();
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
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.configPrintA1 = new ConfigPrintA();
            this.tabPage2 = new TabPage();
            this.configPrintABaoJing1 = new ConfigPrintABaoJing();
            this.tabPage3 = new TabPage();
            this.configPrintADuanDian1 = new ConfigPrintADuanDian();
            this.tabPage4 = new TabPage();
            this.configPrintAKuiDian1 = new ConfigPrintAKuiDian();
            this.tabPage5 = new TabPage();
            this.configPrintDBaoJing1 = new ConfigPrintDBaoJing();
            this.tabPage6 = new TabPage();
            this.configPrintDKuiDian1 = new ConfigPrintDKuiDian();
            this.tabPage7 = new TabPage();
            this.configPrintDState1 = new ConfigPrintDState();
            this.tabPage8 = new TabPage();
            this.configPrintSheBeiGuZhang1 = new ConfigPrintSheBeiGuZhang();
            this.tabPage9 = new TabPage();
            this.configPrintATongJi1 = new ConfigPrintATongJi();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage9.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.ItemSize = new Size(0x54, 0x19);
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x413, 0x28e);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Controls.Add(this.configPrintA1);
            this.tabPage1.Location = new Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x40b, 0x275);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "模拟量日报表";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new EventHandler(this.tabPage1_Click);
            this.configPrintA1.Dock = DockStyle.Fill;
            this.configPrintA1.Location = new Point(3, 3);
            this.configPrintA1.Name = "configPrintA1";
            this.configPrintA1.Size = new Size(0x405, 0x16d);
            this.configPrintA1.TabIndex = 0;
            this.tabPage2.Controls.Add(this.configPrintABaoJing1);
            this.tabPage2.Location = new Point(4, 0x15);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x40b, 0x275);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "模拟量报警日报表";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.configPrintABaoJing1.Dock = DockStyle.Fill;
            this.configPrintABaoJing1.Location = new Point(3, 3);
            this.configPrintABaoJing1.Name = "configPrintABaoJing1";
            this.configPrintABaoJing1.Size = new Size(0x405, 0x16d);
            this.configPrintABaoJing1.TabIndex = 0;
            this.tabPage3.Controls.Add(this.configPrintADuanDian1);
            this.tabPage3.Location = new Point(4, 0x15);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new Padding(3);
            this.tabPage3.Size = new Size(0x40b, 0x275);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "模拟量断电日报表";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.configPrintADuanDian1.Dock = DockStyle.Fill;
            this.configPrintADuanDian1.Location = new Point(3, 3);
            this.configPrintADuanDian1.Name = "configPrintADuanDian1";
            this.configPrintADuanDian1.Size = new Size(0x405, 0x16d);
            this.configPrintADuanDian1.TabIndex = 0;
            this.tabPage4.Controls.Add(this.configPrintAKuiDian1);
            this.tabPage4.Location = new Point(4, 0x15);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new Padding(3);
            this.tabPage4.Size = new Size(0x40b, 0x275);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "模拟量馈电异常日报表";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.configPrintAKuiDian1.Dock = DockStyle.Fill;
            this.configPrintAKuiDian1.Location = new Point(3, 3);
            this.configPrintAKuiDian1.Name = "configPrintAKuiDian1";
            this.configPrintAKuiDian1.Size = new Size(0x405, 0x16d);
            this.configPrintAKuiDian1.TabIndex = 0;
            this.tabPage5.Controls.Add(this.configPrintDBaoJing1);
            this.tabPage5.Location = new Point(4, 0x15);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new Padding(3);
            this.tabPage5.Size = new Size(0x40b, 0x275);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "开关量报警及断电日报表";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.configPrintDBaoJing1.Dock = DockStyle.Fill;
            this.configPrintDBaoJing1.Location = new Point(3, 3);
            this.configPrintDBaoJing1.Name = "configPrintDBaoJing1";
            this.configPrintDBaoJing1.Size = new Size(0x405, 0x16d);
            this.configPrintDBaoJing1.TabIndex = 0;
            this.tabPage6.Controls.Add(this.configPrintDKuiDian1);
            this.tabPage6.Location = new Point(4, 0x1d);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new Padding(3);
            this.tabPage6.Size = new Size(0x40b, 0x26d);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "开关量馈电异常日报表";
            this.tabPage6.UseVisualStyleBackColor = true;
            this.configPrintDKuiDian1.Dock = DockStyle.Fill;
            this.configPrintDKuiDian1.Location = new Point(3, 3);
            this.configPrintDKuiDian1.Name = "configPrintDKuiDian1";
            this.configPrintDKuiDian1.Size = new Size(0x405, 0x16d);
            this.configPrintDKuiDian1.TabIndex = 0;
            this.tabPage7.Controls.Add(this.configPrintDState1);
            this.tabPage7.Location = new Point(4, 0x15);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new Padding(3);
            this.tabPage7.Size = new Size(0x40b, 0x275);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "开关量状态变动日报表";
            this.tabPage7.UseVisualStyleBackColor = true;
            this.configPrintDState1.Dock = DockStyle.Fill;
            this.configPrintDState1.Location = new Point(3, 3);
            this.configPrintDState1.Name = "configPrintDState1";
            this.configPrintDState1.Size = new Size(0x405, 0x16d);
            this.configPrintDState1.TabIndex = 0;
            this.tabPage8.Controls.Add(this.configPrintSheBeiGuZhang1);
            this.tabPage8.Location = new Point(4, 0x15);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new Padding(3);
            this.tabPage8.Size = new Size(0x40b, 0x275);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "监控设备故障日报表";
            this.tabPage8.UseVisualStyleBackColor = true;
            this.configPrintSheBeiGuZhang1.Dock = DockStyle.Fill;
            this.configPrintSheBeiGuZhang1.Location = new Point(3, 3);
            this.configPrintSheBeiGuZhang1.Name = "configPrintSheBeiGuZhang1";
            this.configPrintSheBeiGuZhang1.Size = new Size(0x405, 0x16d);
            this.configPrintSheBeiGuZhang1.TabIndex = 0;
            this.tabPage9.Controls.Add(this.configPrintATongJi1);
            this.tabPage9.Location = new Point(4, 0x15);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new Padding(3);
            this.tabPage9.Size = new Size(0x40b, 0x275);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "模拟量统计值历史记录查询报表";
            this.tabPage9.UseVisualStyleBackColor = true;
            this.configPrintATongJi1.Dock = DockStyle.Fill;
            this.configPrintATongJi1.Location = new Point(3, 3);
            this.configPrintATongJi1.Name = "configPrintATongJi1";
            this.configPrintATongJi1.Size = new Size(0x405, 0x16d);
            this.configPrintATongJi1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
             base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.tabControl1);
            base.Name = "Config_Print";
            base.Size = new Size(0x413, 0x28e);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }
    }
}

