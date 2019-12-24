using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NTICS.Frames
{
    public partial class SelectPeriod : UserControl
    {
        public SelectPeriod()
        {
            InitializeComponent();
            Period = DateTime.Now;
        }

        DateTime FPeriod;
        public DateTime Period
        {
            get { return FPeriod; }
            set {
                FPeriod = value;
                lPeriod.Text = FPeriod.ToString("MMMM yyyy");
            }
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            Period = Period.AddMonths(-1);
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            Period = Period.AddMonths(1);
        }


    }
}
