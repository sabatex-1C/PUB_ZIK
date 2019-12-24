using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using NTICS;
using NTICS.OLE1C77;
using NTICS.Forms;
using System.Diagnostics;



namespace PUB_ZIK
{
    
    public partial class CommonControl : UserControl
    {
        protected string FError = "";
        public ExecuteStatus Executabled;   //��� ����� ��� ������� (������ ��������� �������������) 


        public static OLE V7Z
        {
            get { return CommonVariables.V7Z; }
        }
        public static OLE V7B
        {
            get { return CommonVariables.V7B; }
        }

        public static DateTime �����������
        {
            get { return CommonVariables.�����������.Begin; }
        }

        public CommonControl()
        {
            InitializeComponent();
            Executabled = ExecuteStatus.None;
        }

   
        // ������������� ���������� (�������� �� �����)
        public void Initialize()
        {
            doInitialize();
        }

        // ������������ ��������
        public void DeInitialize()
        {
            doDeInitialize();
            SaveParams();
        }

        // �������� ������������ ����������
        public bool Execute()
        {
            bool FReady = doExecute();
            if (FReady) doSaveParams();
            return FReady;
        }
    
        public void SaveParams()
        {
            doSaveParams();
        }

        #region PROPERTY
        // ��� ������� ����� �������� ���������

        public bool Ready()
        {
            return doReady();
        }
 

        public string Error
        {
            get { return FError; }
        }

        #endregion

        #region protected virtual

        // Initialize
        protected virtual void  doInitialize()
        {
        }
        protected virtual void doDeInitialize()
        {
        }
        protected virtual void doSaveParams()
        {
        }
        protected virtual bool doExecute()
        {
            return true;
        }
        protected virtual bool doReady()
        {
            return true;
        }


        #endregion

        private void btExecute_Click(object sender, EventArgs e)
        {
            Execute();
        }

    }

    public enum ExecuteStatus
    {
        None,  // ������� �� ����������� (������ ��������� �����������)
        Yes,   // ����� ���������      
        Always // ������ ����� ��������� (������ ����� ������������� �� ���������� �����)
    }
   

}
