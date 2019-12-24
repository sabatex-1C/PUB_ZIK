using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;


namespace PUB_ZIK
{
    public partial class Connection : CommonControl
    {
        const string UserName = "NTICS";
        const string PassWord = "NTICS";




        public Connection()
        {
            InitializeComponent();
            Executabled = ExecuteStatus.Always;
        }

        private void btSetConnectionZ_Click(object sender, EventArgs e)
        {
            if (CommonVariables.ZConnection != null) { CommonVariables.ZConnection.Dispose(); }
            try
            {
                CommonVariables.ZConnection = new NTICS.OLE1C77.OLEConnection(cbZIK_Patch.Text, (NTICS.OLE1C77.V77Servers)cbServerType.SelectedIndex, false, UserName, PassWord);
            }
            catch
            {
                Trace.WriteLine("Неудачная попытка подключения к  " + cbZIK_Patch.Text);
                CommonVariables.ZConnection = null;
                return;
            }
            System.Diagnostics.Trace.WriteLine("Вы подключились к Зик.");
        }
        private void btConnectionB_Click(object sender, EventArgs e)
        {
            if (CommonVariables.BConnection != null) { CommonVariables.BConnection.Dispose(); }
            try
            {
                CommonVariables.BConnection = new NTICS.OLE1C77.OLEConnection(cbPUB_Patch.Text, (NTICS.OLE1C77.V77Servers)cbServerType.SelectedIndex, false, UserName, PassWord);
            }
            catch
            {
                Trace.WriteLine("Неудачная попытка подключения к  " + cbPUB_Patch.Text);
                CommonVariables.BConnection = null;
                return;
            }
            System.Diagnostics.Trace.WriteLine("Вы подключились к Бухгалтерии.");
                
        }
        protected override bool doReady()
        {
            if (CommonVariables.BConnection == null) { return false; }
            if (CommonVariables.ZConnection == null) { return false; }
            return true;
        }
        protected override void doInitialize()
        {
            base.doInitialize();
            cbServerType.SelectedIndex = conf.CONNECTION_ServerType;
            cbZIK_Patch.Text = conf.CONNECTION_BasePatchZ;
            cbPUB_Patch.Text = conf.CONNECTION_BasePatchB;
            string[] bases = Registry.CurrentUser.OpenSubKey("Software\\1C\\1Cv7\\7.7\\Titles").GetValueNames();
            foreach (string s in bases)
            {
                cbZIK_Patch.Items.Add(s);
                cbPUB_Patch.Items.Add(s);
            }
 
        }
        protected override void doSaveParams()
        {
            base.doSaveParams();
            conf.CONNECTION_ServerType = cbServerType.SelectedIndex;
            conf.CONNECTION_BasePatchZ = cbZIK_Patch.Text;
            conf.CONNECTION_BasePatchB = cbPUB_Patch.Text;
        }
        protected override void doDeInitialize()
        {
            
            if (CommonVariables.ZConnection != null) { CommonVariables.ZConnection.Dispose(); }
            if (CommonVariables.BConnection != null) { CommonVariables.BConnection.Dispose(); }
            CommonVariables.ZConnection = null;
            CommonVariables.BConnection = null;
            base.doDeInitialize();
        }
        protected override bool doExecute()
        {
            return Ready();                
        }


    }
}
