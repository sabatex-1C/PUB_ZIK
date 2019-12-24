using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NTICS
{
    [DesignTimeVisible(true)]
    public partial class ConnectionDlg : UserControl
    {
        public ConnectionDlg()
        {
            InitializeComponent();
        }
        public string ConnectionName
        {
            set { lConnectionName.Text = value; }
            get { return lConnectionName.Text; }
        }
        public string BasePatch
        {
            set { lBasePatch.Text = value; }
            get { return lBasePatch.Text; }
        }
        public string UserName
        {
            set { lUserName.Text = value; }
            get { return lUserName.Text; }
        }
        public string PassWord
        {
            set { lPassWord.Text = value; }
            get { return lPassWord.Text; }
        }

    }
}
