using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Logic;
using System.Runtime.InteropServices; 

namespace MAX_CMSS_V2
{
    public partial class Form_parameter_config : WeifenLuo.WinFormsUI.Docking.DockContentEx
    {
        private TreeNode preSelectedNode;
        private Color initColor;
        private MainForm mainForm;

        Config_area ca ;
        Normal_analog na0 ;
        Normal_analog na1 ;
        Switch sw0, sw1, sw2,sw3;
        Control ct;
        TestPoint tp_analog;
        TestPoint tp_switch;
        TestPoint tp_kong;

        public Form_parameter_config(MainForm form)
        {
            InitializeComponent();
            initColor = this.treeView1.BackColor;

            this.mainForm = form;
            ca = new Config_area();
            na0 = new Normal_analog(0, this.mainForm);
            na1 = new Normal_analog(1, this.mainForm);
            sw0 = new Switch((byte)TongDaoLeiXing.ErTaiKaiGuanLiang, this.mainForm);
            sw1 = new Switch((byte)TongDaoLeiXing.SanTaiKaiGuanLiang, this.mainForm);
            sw2 = new Switch((byte)TongDaoLeiXing.TongDuanLiang, this.mainForm);
            sw3 = new Switch((byte)TongDaoLeiXing.FenZhanLiang, this.mainForm);
            tp_analog = new TestPoint(0);
            tp_switch = new TestPoint(1);
            tp_kong = new TestPoint(2);
            ct = new Control();
        }
        public TreeView Tree
        {
            get
            {
                return this.treeView1;
            }
        }
  //      [DllImport("user32.dll")]
  //      static extern void FlashWindow(IntPtr a, bool b); 
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (preSelectedNode != null)
                preSelectedNode.BackColor = initColor;
            e.Node.BackColor = Color.Gray;
            preSelectedNode = e.Node;

            switch (e.Node.Name)
            {
                case "安装地点":
                    this.splitContainer1.Panel2.Controls.Clear();
                    ca.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(ca);
                    break;
                case "类型":
                    e.Node.Expand();
                    break;
                case "模拟量类型":
                    e.Node.Expand();
                    break;
                case "一般模拟量":
                    
                    this.splitContainer1.Panel2.Controls.Clear();
                    na0.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(na0);
                    break;
                case "累计量":
                    this.splitContainer1.Panel2.Controls.Clear();                    
                    na1.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(na1);
                    break;
                case "开关量类型":
                    e.Node.Expand();
                    break;
                case "两态开关量":
                    this.splitContainer1.Panel2.Controls.Clear();
                   
                    sw0.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(sw0);
                    break;
                case "三态开关量":
                    this.splitContainer1.Panel2.Controls.Clear();
                   
                    sw1.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(sw1);
                    break;
                case "触点开关量":
                    this.splitContainer1.Panel2.Controls.Clear();
                  
                    sw2.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(sw2);
                    break;
                case "控制量":
                    this.splitContainer1.Panel2.Controls.Clear();
                  
                    ct.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(ct);
                    break;
                case "分站量":
                    this.splitContainer1.Panel2.Controls.Clear();
                  
                    sw3.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(sw3);
                    break;
                case "测点":
                    e.Node.Expand();
                    break;
                case "模拟量测点":
                    this.splitContainer1.Panel2.Controls.Clear();
                   
                    tp_analog.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(tp_analog);
                    break;
                case "开关量测点":
                    this.splitContainer1.Panel2.Controls.Clear();
                    tp_switch.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(tp_switch);
                    break;
                case "控制量测点":
                    this.splitContainer1.Panel2.Controls.Clear();
                    tp_kong.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(tp_kong);
                    break;
            }
        }
        private void Form_parameter_config_Load(object sender, EventArgs e)
        {
           // FlashWindow(this.Handle, true);
        }

        private void Form_parameter_config_FormClosed(object sender, FormClosedEventArgs e)
        {
 //           this.Dispose();
        }

    }
}
