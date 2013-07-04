using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 历史数据显示
{
    public partial class DateTimeChooser : UserControl
    {
        public DateTimeChooser()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.SelectedIndex = 0;
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox3.SelectedIndex = 0;
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox4.SelectedIndex = 0;
            this.comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;

            this.comboBox3.SelectedIndex = 23;
            this.comboBox4.SelectedIndex = 59;
        }
        public DateTime StartTime
        {
            get
            {
                DateTime picker = this.dateTimePicker1.Value;
               
                DateTime start = new DateTime(picker.Year, picker.Month, picker.Day, Convert.ToInt32(this.comboBox1.Text), Convert.ToInt32(this.comboBox2.Text), 0);
                return start;
            }
            set
            {
                this.dateTimePicker1.Value = value;
                this.comboBox1.SelectedIndex = value.Hour;
                this.comboBox2.SelectedIndex = value.Minute;
            }
        }
        public DateTime EndTime
        {
            get
            {
                DateTime picker = this.dateTimePicker2.Value;
                DateTime end = new DateTime(picker.Year, picker.Month, picker.Day, Convert.ToInt32(this.comboBox3.Text), Convert.ToInt32(this.comboBox4.Text), 0);
                return end;
            }
            set
            {
                this.dateTimePicker2.Value = value;
                this.comboBox3.SelectedIndex = value.Hour;
                this.comboBox4.SelectedIndex = value.Minute;
            }
        }
        /// <summary>
        /// 检查下拉框是否有空值
        /// </summary>
        /// <returns>true:有空值，false:无空值</returns>
        private Boolean checkNull()
        {
            if (this.comboBox3.Text == "" || this.comboBox1.Text == "" || this.comboBox2.Text == "" || this.comboBox4.Text == "")
            {
                return true;
            }

            return false;
        }

       
    }
}
