using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 历史数据显示
{
    class History_data
    {
        //private int init_bianhao = 0;

        //public History_data(string s)
        //{
        //    InitializeComponent();

        //    this.dateTimeChooser1.StartTime = new System.DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
        //    this.dateTimeChooser1.EndTime = DateTime.Now;
        //    this.init_bianhao = 0;

        //    this.initListBox(s);
        //}

        //public History_data(string s, DateTime start, DateTime end)
        //{
        //    InitializeComponent();

        //    this.dateTimeChooser1.StartTime = start;
        //    this.dateTimeChooser1.EndTime = end;
        //    this.init_bianhao = 0;

        //    this.initListBox(s);
        //}

        //private void initListBox(string s)
        //{
        //    panel_feed.SendToBack();
        //    panel_cut.SendToBack();
        //    panel_test.SendToBack();

        //    if (!GlobalParams.AllCeDian.ContainsKey(s))
        //        return;
        //    CeDian cedian = GlobalParams.AllCeDian[s];

        //    foreach (CeDian cd in GlobalParams.AllCeDian.Values)
        //    {
        //        if (cedian.DaLeiXing == cedian.DaLeiXing)
        //        {
        //            string info = cd.CeDianBianHao + " " + cd.CeDianWeiZhi + " " + cd.XiaoleiXing;
        //            this.comboBox1.Items.Add(info);

        //            if (cedian.CeDianBianHao == cd.CeDianBianHao)
        //            {
        //                this.comboBox1.SelectedItem = info;
        //            }

        //        }
        //    }

        //    listBox1.Items.AddRange(updateList(comboBox1.SelectedItem.ToString()));

        //    string[] ss = DuanDianGuanXi.getDuanDianCeDianBianHao(s);
        //    this.comboBox2.Items.AddRange(ss);
        //    this.comboBox2.SelectedIndex = -1;

        //    string[] kuidian = KuiDianGuanXi.getKuiDianCeDianBianHao(s);
        //    this.comboBox3.Items.AddRange(kuidian);
        //    this.comboBox3.SelectedIndex = -1;

        //    panel1.SendToBack();
        //}

        //private void label1_Click(object sender, EventArgs e)
        //{

        //}

        //private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        //{
        //    if (comboBox1.SelectedIndex > -1)
        //    {
        //        listBox1.Items.AddRange(updateList(comboBox1.SelectedItem.ToString()));
        //    }
        //    if (comboBox3.SelectedIndex > -1)
        //    {
        //        listBox3.Items.AddRange(updateList(comboBox3.SelectedItem.ToString()));
        //    }
        //    if (comboBox2.SelectedIndex > -1)
        //    {
        //        listBox2.Items.AddRange(updateList(comboBox2.SelectedItem.ToString()));
        //    }
        //}
        //private string[] updateList(string ss)
        //{
        //    string s = ss.Substring(0, 5);
        //    DataTable dt1 = new DataTable();
        //    DataTable dt2 = new DataTable();
        //    List<string> list = new List<string>();
        //    if ("D" == s.Substring(2, 1) || "F" == s.Substring(2, 1))
        //    {
        //        ReportDataSuply.doKaiGuanLiangSelect(this.dateTimeChooser1.StartTime, this.dateTimeChooser1.EndTime, ref dt1, ref dt2, s);
        //        if (dt1 != null)
        //        {
        //            foreach (DataRow row in dt1.Rows)
        //            {
        //                int state = Convert.ToInt32(row["state"]);

        //                if (state == 7)
        //                {

        //                    list.Add("故障 " + row["uploadTime"].ToString() + " 故障");
        //                    continue;
        //                }

        //                string ss2 = state.ToString();
        //                if (state >= 0 && state < GlobalParams.state.Length)
        //                    ss2 = GlobalParams.state[state];


        //                list.Add(switchValue(s, row["uploadValue"].ToString()).PadRight(5) + " " + row["uploadTime"].ToString() + " " + ss2);

        //            }
        //        }
        //    }
        //    else if ("A" == s.Substring(2, 1))
        //    {
        //        ReportDataSuply.doMoNiLiangSelect(this.dateTimeChooser1.StartTime, this.dateTimeChooser1.EndTime, ref dt1, ref dt2, s);
        //        if (dt1 != null)
        //        {
        //            foreach (DataRow row in dt1.Rows)
        //            {
        //                int state = Convert.ToInt32(row["state"]);
        //                string ss1 = state.ToString();
        //                if (state < GlobalParams.state.Length && state >= 0)
        //                    ss1 = GlobalParams.state[state];
        //                list.Add(row["uploadValue"].ToString().PadRight(8) + " " + row["uploadTime"].ToString() + " " + ss1);
        //            }
        //        }
        //    }
        //    else if ("C" == s.Substring(2, 1))
        //    {
        //        string fenZhanHao = string.Empty;
        //        if (s.Substring(0, 1) == "0")
        //        {
        //            fenZhanHao = s.Substring(1, 1);
        //        }
        //        else
        //            fenZhanHao = s.Substring(0, 2);
        //        string sql = "select * from KongZhiLiangValue where ceDianBianHao = " + s.Substring(4, 1) + " and fenZhanHao= " + fenZhanHao + " and uploadTime between '" + this.dateTimeChooser1.StartTime + "' and '" + this.dateTimeChooser1.EndTime + "' order by ceDianBianHao, uploadTime";
        //        dt1 = OperateDB.Select(sql);
        //        if (dt1 != null)
        //        {
        //            foreach (DataRow row in dt1.Rows)
        //            {
        //                if (row["state"] + "" == "7")
        //                {
        //                    list.Add("故障 " + row["uploadTime"].ToString());
        //                }
        //                else
        //                {
        //                    list.Add(controlValue(s, row["uploadValue"].ToString()).PadRight(5) + " " + row["uploadTime"].ToString());
        //                }
        //            }
        //        }
        //    }
        //    return list.ToArray();
        //}
        //private string switchValue(string ceDian, string value)
        //{
        //    if (ceDian.Substring(2, 1).ToUpper() == "F")
        //    {
        //        switch (value)
        //        {
        //            case "0":
        //                return "交流";
        //            default:
        //                return "直流";
        //        }
        //    }
        //    else
        //    {

        //        DataTable dt = KaiGuanLiangLeiXing.GetSwitchAlarm(ceDian);
        //        switch (value)
        //        {
        //            case "0":
        //                return dt.Rows[0]["lingTaiMingCheng"].ToString();
        //            case "1":
        //                return dt.Rows[0]["yiTaiMingCheng"].ToString();
        //            case "2":
        //                return dt.Rows[0]["erTaiMingCheng"].ToString();
        //            default:
        //                return "其它";
        //        }
        //    }
        //}
        //private string controlValue(string ceDian, string value)
        //{
        //    DataTable dt = KongZhiLiang.GetKongAlarm(ceDian);
        //    if (value == "0")
        //    {
        //        return dt.Rows[0]["lingTaiMingCheng"].ToString();
        //    }
        //    else if (value == "1")
        //    {
        //        return dt.Rows[0]["yiTaiMingCheng"].ToString();
        //    }
        //    else
        //        return "其它";
        //}

        //private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        //{
        //    if (comboBox1.SelectedIndex > -1)
        //    {
        //        listBox1.Items.AddRange(updateList(comboBox1.SelectedItem.ToString()));
        //    }
        //    if (comboBox3.SelectedIndex > -1)
        //    {
        //        listBox3.Items.AddRange(updateList(comboBox3.SelectedItem.ToString()));
        //    }
        //    if (comboBox2.SelectedIndex > -1)
        //    {
        //        listBox2.Items.AddRange(updateList(comboBox2.SelectedItem.ToString()));
        //    }
        //}

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (init_bianhao == 1)
        //    {
        //        string s = comboBox1.SelectedItem.ToString();
        //        s = s.Substring(0, 5);

        //        string[] ss = DuanDianGuanXi.getDuanDianCeDianBianHao(s);
        //        this.comboBox2.Items.Clear();
        //        this.comboBox2.Items.AddRange(ss);
        //        this.comboBox2.SelectedIndex = -1;

        //        string[] kuidian = KuiDianGuanXi.getKuiDianCeDianBianHao(s);
        //        this.comboBox3.Items.Clear();
        //        this.comboBox3.Items.AddRange(kuidian);
        //        this.comboBox3.SelectedIndex = -1;

        //        listBox2.Items.Clear();
        //        listBox3.Items.Clear();

        //    }
        //    else
        //    {

        //        init_bianhao = 1;
        //    }
        //}

        //private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (comboBox2.SelectedIndex > -1)
        //    {
        //        listBox2.Items.Clear();
        //        listBox2.Items.AddRange(updateList(comboBox2.SelectedItem.ToString()));

        //        string s = comboBox2.SelectedItem.ToString();
        //        if (s[2] == 'C')
        //        {
        //            this.comboBox3.Items.Clear();
        //            this.listBox3.Items.Clear();
        //            string[] kuidian = KuiDianGuanXi.getKuiDianCeDianBianHao(s.Substring(0, 5));
        //            this.comboBox3.Items.AddRange(kuidian);
        //            this.comboBox3.SelectedIndex = -1;
        //        }
        //    }
        //}

        //private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (comboBox3.SelectedIndex > -1)
        //    {
        //        listBox3.Items.Clear();
        //        listBox3.Items.AddRange(updateList(comboBox3.SelectedItem.ToString()));

        //        string s = comboBox3.SelectedItem.ToString();
        //        if (s[2] == 'C')
        //        {
        //            this.comboBox2.Items.Clear();
        //            this.listBox2.Items.Clear();
        //            string[] ss = DuanDianGuanXi.getDuanDianCeDianBianHao(s.Substring(0, 5));
        //            this.comboBox2.Items.AddRange(ss);
        //            this.comboBox2.SelectedIndex = -1;
        //        }
        //    }
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //this.dateTimeChooser1.EndTime = DateTime.Now;
        //    if (this.dateTimeChooser1.StartTime >= this.dateTimeChooser1.EndTime)
        //    {
        //        MessageBox.Show("开始时间大于结束时间，请重新选择！");
        //        return;
        //    }
        //    else
        //    {
        //        if (comboBox1.SelectedIndex > -1)
        //        {
        //            listBox1.Items.Clear();
        //            listBox1.Items.AddRange(updateList(comboBox1.SelectedItem.ToString()));
        //        }
        //        if (comboBox3.SelectedIndex > -1)
        //        {
        //            listBox3.Items.Clear();
        //            listBox3.Items.AddRange(updateList(comboBox3.SelectedItem.ToString()));
        //        }
        //        if (comboBox2.SelectedIndex > -1)
        //        {
        //            listBox2.Items.Clear();
        //            listBox2.Items.AddRange(updateList(comboBox2.SelectedItem.ToString()));
        //        }
        //    }
        //}

        //int pages = 0;
        //int pageSize = 201;
        //ListBox prt_lbx = null;
        //ComboBox prt_cbx = null;
        //string type = "";
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    if (this.printDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        pages = 0;
        //        prt_lbx = this.listBox1;
        //        prt_cbx = this.comboBox1;
        //        type = "测点";
        //        this.printDocument1.Print();

        //    }

        //}

        //private void prt_2_Click(object sender, EventArgs e)
        //{
        //    if (this.printDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        pages = 0;
        //        prt_lbx = this.listBox2;
        //        prt_cbx = this.comboBox2;
        //        type = "断电关系";
        //        this.printDocument1.Print();

        //    }
        //}

        //private void prt_3_Click(object sender, EventArgs e)
        //{
        //    if (this.printDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        pages = 0;
        //        prt_lbx = this.listBox3;
        //        prt_cbx = this.comboBox3;
        //        type = "馈电关系";
        //        this.printDocument1.Print();

        //    }
        //}

        //private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //    try
        //    {
        //        Font font = new Font("宋体", 10, FontStyle.Regular);//设置画笔 
        //        Brush bru = Brushes.Black;
        //        Pen pen = new Pen(bru);
        //        pen.Width = 0;
        //        //设置各边距 
        //        int nLeft = 0;
        //        int nTop = 0;
        //        int nRight = 0;
        //        int nBottom = 0;
        //        int nWidth = 200 - nRight - nLeft;
        //        int nHeight = 200 - nTop - nBottom;
        //        ////打印各边距 
        //        //e.Graphics.DrawLine(pen, nLeft, nTop, nLeft, nTop + nHeight);
        //        //e.Graphics.DrawLine(pen, nLeft + nWidth, nTop, nLeft + nWidth, nTop + nHeight);
        //        //e.Graphics.DrawLine(pen, nLeft, nTop, nLeft + nWidth, nTop);
        //        //e.Graphics.DrawLine(pen, nLeft, nTop + nHeight, nLeft + nWidth, nTop + nHeight);

        //        e.Graphics.DrawString("历史数据，" + type + "：" + prt_cbx.SelectedItem + ",时间：" + dateTimeChooser1.StartTime.ToString("yyyy年MM月dd日 HH时mm分") + " 至 " + dateTimeChooser1.EndTime.ToString("yyyy年MM月dd日 HH时mm分"), font, bru, 5, 5);
        //        e.Graphics.DrawString("第" + (pages + 1) + "页", font, bru, 400, 1130);

        //        int n = pages * pageSize;

        //        int count = pageSize;


        //        if (this.prt_lbx.Items.Count < pageSize)
        //        {
        //            count = this.prt_lbx.Items.Count;
        //        }
        //        else if (n + pageSize > this.prt_lbx.Items.Count)
        //        {
        //            count = this.prt_lbx.Items.Count;
        //        }
        //        else
        //        {
        //            count = n + pageSize;
        //        }

        //        string content = "";
        //        for (int i = n; i < count; i++)
        //        {
        //            string item = this.prt_lbx.Items[i].ToString();

        //            if (i != 0 && i % 3 == 0)
        //            {
        //                content += "\n";
        //            }
        //            else if (i > 0)
        //            {
        //                content += "        ";
        //            }

        //            content += item;
        //        }

        //        e.Graphics.DrawString(content, font, bru, nLeft + 10, nTop + 30);

        //        pages++;
        //        if (pages == 1 && this.prt_lbx.Items.Count > pages * pageSize)
        //        {

        //            e.HasMorePages = true;
        //        }
        //        else if (this.prt_lbx.Items.Count > 30 && this.prt_lbx.Items.Count > pages * pageSize)
        //        {

        //            e.HasMorePages = true;
        //        }
        //        else
        //        {

        //            e.HasMorePages = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("打印出现错误，请检查是否安装打印机。");
        //    }


        //}

        

        
    }
}
