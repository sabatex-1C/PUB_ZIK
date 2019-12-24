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
        public ExecuteStatus Executabled;   //Для форма без методов (кнопка выполнить заблокирована) 


        public static OLE V7Z
        {
            get { return CommonVariables.V7Z; }
        }
        public static OLE V7B
        {
            get { return CommonVariables.V7B; }
        }

        public static DateTime ДатаПериода
        {
            get { return CommonVariables.ДатаПериода.Begin; }
        }

        public CommonControl()
        {
            InitializeComponent();
            Executabled = ExecuteStatus.None;
        }

   
        // Инициализация переменных (загрузка из файла)
        public void Initialize()
        {
            doInitialize();
        }

        // Освобождение ресурсов
        public void DeInitialize()
        {
            doDeInitialize();
            SaveParams();
        }

        // Проверка правильности заполнения
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
        // Все условия даной страницы выполнены

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
        None,  // Никогда не исполняется (кнопка исполнить блокирована)
        Yes,   // Можна исполнить      
        Always // Всегда нужно исполнять (кнопка вперёд заблокирована до готовности формы)
    }
   

}
