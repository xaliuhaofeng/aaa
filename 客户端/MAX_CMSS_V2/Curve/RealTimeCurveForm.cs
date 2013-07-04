using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logic;

namespace MAX_CMSS_V2.Curve
{
    public partial class RealTimeCurveForm : Form
    {
        private RealTime_curve curve;

        public RealTime_curve Curve
        {
            get { return curve; }
        }

        public RealTimeCurveForm(string ceDianBianHao)
        {
            InitializeComponent();
            curve = new RealTime_curve(ceDianBianHao);
            curve.Dock = DockStyle.Fill;
            this.Controls.Add(curve);
        }

        public void Dispatch(FenZhanRTdata ud)
        {
            this.curve.Dispatch(ud);
        }
    }
}
