using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using NTICS.OLE1C77;
using System.Diagnostics;

namespace PUB_ZIK
{
    public enum TraceSwitch
    {
        
    }

    public class Traces
    {
        public static string Info = "Info";
        public static string Status = "Status";
    }
   
    
    public class CommonVariables 
    {

        public static OLEConnection ZConnection;
        public static OLEConnection BConnection;

        
        public static NTICS.Period ДатаПериода;
        public static bool AutoInsertUser;
        protected static ConfigVersion1C Version1C;
        public static Распределения Распределения;
        public static List<MEMReferences> MEMPUBWoker;


        //Для файла лога
        public static LogBrowser log = new LogBrowser();


        // Для прогресс бара
        public static ToolStripProgressBar ProgressBar = null;
        public static void ProgressBarInitialize(int Min, int Max, int Start)
        {
            ProgressBar.Minimum = Min;
            ProgressBar.Maximum = Max;
            ProgressBar.Value = Start;
            ProgressBar.Visible = true;
        }
        public static void ProgressBarInc()
        {
            ProgressBar.Value++;
            ProgressBar.Owner.Refresh();
        }
        public static void ProgressBarHide()
        {
            ProgressBar.Visible = false;
        }



        public static OLE V7Z
        {
            get { return ZConnection.Global; }
        }
        public static OLE V7B
        {
            get { return BConnection.Global; }
        }


        #region FUNCTION

        public static OLE ПолучитьСотрудникаПУБ(string СотрудникКод)
        {
            OLE SotrZIK = GetReferenceElementZIK("Сотрудники", СотрудникКод);
            string TIN = SotrZIK.Property("ФизическоеЛицо").Property("ИНН").ToString().Trim();
            MEMReferences MR = new MEMReferences();
            MR.INDEX = TIN;
            int i = 0;
            OLE PUB = V7B.CreateObject("Справочник.Сотрудники");
            do
            {
                i = MEMPUBWoker.BinarySearch(MR);
                if (i < 0)
                {
                    // Нет сотрудника в ПУБ
                    MR.DESCRIPTION = SotrZIK.Property("Description").ToString().Trim();
                    if (AutoInsertUser)
                    {
                        MR.Reference = SotrZIK;
                        PUB.Method("New");
                        PUB.Property("ИНН", MR.INDEX);
                        PUB.Property("Description", MR.DESCRIPTION);
                        PUB.Method("Write");
                        MR.CODE = PUB.Property("Code").ToString().Trim();
                        MR.FULLCODE = PUB.Method("FullCode").ToString().Trim();
                        MEMPUBWoker.Add(MR);
                        MEMPUBWoker.Sort();
                        Trace.WriteLine("Вставим сотрудника в Бухгалтерию - " + MR.CODE + "  " + MR.DESCRIPTION + " ИНН - " + MR.INDEX);
                        return PUB.Method("CurrentItem");
                    }
                    DialogResult result = MessageBox.Show("В 1С7.7 Бух отсутствует сотрудник " + MR.DESCRIPTION + " с кодом ИНН - " + MR.INDEX +
                                    ". Загрузите не монопольно 1С7.7 и введите даного пользователя, после чего нажмите ОК.",
                                    "Ошибка в справочнике Сотрудники.",
                                    MessageBoxButtons.OKCancel);
                    if (result == DialogResult.Cancel)
                    {
                        return null;
                    }
                    CommonVariables.MEMPUBWoker = MEMReferences.CopyOLERefToMEMRef(PUB, "ИНН");
                    MEMPUBWoker.Sort();
                }
            } while (i < 0);
            PUB.Method("FindByCode", MEMPUBWoker[i].CODE);
            return PUB.Method("CurrentItem");

        }
        public static OLE ПолучитьСотрудникаПУБ_IHH(string TIN)
        {
            MEMReferences MR = new MEMReferences();
            MR.INDEX = TIN;
            int i = 0;
            OLE PUB = V7B.CreateObject("Справочник.Сотрудники");
            do
            {
                i = MEMPUBWoker.BinarySearch(MR);
                if (i < 0)
                {
                    // Нет сотрудника в ПУБ
                    //MR.DESCRIPTION = SotrZIK.Property("Description").ToString().Trim();
                    //if (AutoInsertUser)
                    //{
                    //    MR.Reference = SotrZIK;
                    //    PUB.Method("New");
                    //    PUB.Property("ИНН", MR.INDEX);
                    //    PUB.Property("Description", MR.DESCRIPTION);
                    //    PUB.Method("Write");
                    //    MR.CODE = PUB.Property("Code").ToString().Trim();
                    //    MR.FULLCODE = PUB.Method("FullCode").ToString().Trim();
                    //    MEMPUBWoker.Add(MR);
                    //    MEMPUBWoker.Sort();
                    //    Trace.WriteLine("Вставим сотрудника в Бухгалтерию - " + MR.CODE + "  " + MR.DESCRIPTION + " ИНН - " + MR.INDEX);
                    //    return PUB.Method("CurrentItem");
                    //}
                    DialogResult result = MessageBox.Show("В 1С7.7 Бух отсутствует сотрудник " + MR.DESCRIPTION + " с кодом ИНН - " + MR.INDEX +
                                    ". Загрузите не монопольно 1С7.7 и введите даного пользователя, после чего нажмите ОК.",
                                    "Ошибка в справочнике Сотрудники.",
                                    MessageBoxButtons.OKCancel);
                    if (result == DialogResult.Cancel)
                    {
                        return null;
                    }
                    CommonVariables.MEMPUBWoker = MEMReferences.CopyOLERefToMEMRef(PUB, "ИНН");
                    MEMPUBWoker.Sort();
                }
            } while (i < 0);
            PUB.Method("FindByCode", MEMPUBWoker[i].CODE);
            return PUB.Method("CurrentItem");

        }


       
        public static OLE GetReferenceElementPUB(string ReferenceType, string Code)
        {
            OLE reference = V7B.CreateObject("Справочник." + ReferenceType);
            if (reference.Method("FindByCode", Code).ToBool()) { return reference.Method("CurrentItem"); }
            return null;
        }

        public static OLE GetReferenceElementZIK(string ReferenceType, string Code)
        {
            OLE reference = V7Z.CreateObject("Справочник." + ReferenceType);
            if (reference.Method("FindByCode", Code).ToBool()) { return reference.Method("CurrentItem"); }
            return null;
        }




        /// <summary>
        ///  Получить зарегистрированный элемент справочника "фирмы"
        /// </summary>
        /// <param name="handle">Ссылка на активную 1С7.7 </param>
        public static OLE GetFirm(OLE handle)
        {
            if (handle == null) { return null; }
            OLE temp = handle.Global.CreateObject("Справочник.Фирмы");
            temp.Method("ВыбратьЭлементы");
            while (temp.Method("ПолучитьЭлемент").ToBool())
            {
                if (temp.Method("DeleteMark").ToBool()) continue;
                if (temp.Property("ЕДРПОУ").ToString().Trim() == conf.CommonAllConfig.ЕДРПОУ)
                {
                    return temp.Method("CurrentItem");
                }
            }
            System.Diagnostics.Trace.WriteLine("Программа не зарегистрирована для даной базы: " + handle.EvalExpr("SystemCaption()").ToString());
            temp.Dispose();
            return null;
        }

        #endregion



    }
}
