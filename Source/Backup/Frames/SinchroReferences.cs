using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NTICS.OLE1C77;

namespace PUB_ZIK
{
    public partial class SinchroReferences : CommonControl
    {

        void IncCount(Label Count)
        {
            int i = int.Parse(Count.Text);
            i++;
            Count.Text = i.ToString();
            Count.Refresh();
        }

        void IncInsertCounter()
        {
        }

        void IncDeleteCounter()
        {
        }

        void Sinchro(OLE refmain,OLE refchild)
        {
        #region INSERT ELEMENTS
            List<MEMReferences> MEMRef;
            bool FullCod = (refmain.EvalExpr("Метаданные.Справочник(\"" + refmain.Method("Вид").ToString() + "\").СерииКодов").ToString().Trim().ToUpper() == ("ВПределахПодчинения").ToUpper());
            if (FullCod)
            {
                MEMRef= MEMReferences.CopyOLERefToMEMRef(refmain, "FullCode",FullCod,true,true);
            }
            else
            {
                MEMRef= MEMReferences.CopyOLERefToMEMRef(refmain, "Code",FullCod,true,true);
            }

            // Count elements
            CommonVariables.ProgressBarInitialize(0,MEMRef.Count,0);
            //refmain.Method("ВыбратьЭлементы");
            foreach (MEMReferences MRef in MEMRef)
            {
                if (!(bool)refchild.Method("НайтиПоРеквизиту", "КАУ", MRef.INDEX, 0))
                {
                    IncInsertCounter();
                    refchild.Method("New");
                    refchild.Property("Description", MRef.DESCRIPTION);
                    refchild.Property("КАУ", MRef.INDEX);
                    refchild.Method("Write");
                    System.Diagnostics.Trace.WriteLine("Вставлен елемент - " + MRef.INDEX + " " + MRef.DESCRIPTION);
                }
                else
                {
                    if ((bool)refchild.Method("DeleteMark"))
                    {
                        refchild.Method("ClearDeleteMark");
                        //refchild.Method("Write");
                    }
                }

                CommonVariables.ProgressBarInc();
            }
        #endregion
        #region DELEDE ELEMENTS
            // Count elements
            int refchildCount = 0;
            refchild.Method("ВыбратьЭлементы");
            while (refchild.Method("ПолучитьЭлемент").ToBool())
            {
                if ((bool)refchild.Method("DeleteMark")) continue;
                refchildCount++;
            }

            CommonVariables.ProgressBarInitialize(0,refchildCount,0);
            refchild.Method("ВыбратьЭлементы");
            MEMRef.Sort();
            while (refchild.Method("ПолучитьЭлемент").ToBool())
            {
                if ((bool)refchild.Method("DeleteMark")) continue;
                CommonVariables.ProgressBarInc();
                MEMReferences mr = new MEMReferences();
                mr.INDEX = refchild.Property("КАУ").ToString().Trim();

                if (MEMRef.BinarySearch(mr) < 0)
                {
                    IncDeleteCounter();
                    refchild.Method("Delete", 0);
                    refchild.Method("Write");
                    System.Diagnostics.Trace.WriteLine("Помечен к удалению елемент - " +
                                    refchild.Property("Code").ToString() + " "+
                                    refchild.Property("Description").ToString());
                }

                
            }
         #endregion
        }

        public SinchroReferences()
        {
            InitializeComponent();
            Executabled = ExecuteStatus.Yes;
        }


        #region PROTECTED
        protected override bool doExecute()
        {
            OLE ZIKReferences = V7Z.CreateObject("Справочник.ВидыСубконто");
            OLE ZIKRefChild = V7Z.CreateObject("Справочник.ОбъектыАналитУчета");
            #region НашиДенежныеСчета
            OLE PUBRefMain = V7B.CreateObject("Справочник.НашиДенежныеСчета");
            PUBRefMain.Method("ИспользоватьВладельца", CommonVariables.GetFirm(V7B) /* CommonVariables.BFirm.CurrentItem */);

            if ((double)ZIKReferences.Method("НайтиПоРеквизиту", (object)"Идентиф", (object)"НашиДенежныеСчета", (object)1) != 1.0)
            {
                MessageBox.Show("Не могу найти в ЗиК субконто НашиДенежныеСчета", "Ошибка текущей формы !!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            ZIKRefChild.Method("ИспользоватьВладельца", ZIKReferences.Method("ТекущийЭлемент"));
            System.Diagnostics.Trace.WriteLine("Обрабатываем справочник НашиДенежныеСчета");
            Sinchro(PUBRefMain, ZIKRefChild);
            cbДенежныеСредства.Checked = true;
            #endregion
            #region ШкалаСтавок
            PUBRefMain = V7B.CreateObject("Справочник.ШкалаСтавок");
            if ((double)ZIKReferences.Method("НайтиПоРеквизиту", (object)"Идентиф", (object)"ШкалаСтавок", (object)1) != 1.0)
            {
                MessageBox.Show("Не могу найти в ЗиК субконто ШкалаСтавок", "Ошибка текущей формы !!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            ZIKRefChild.Method("ИспользоватьВладельца", ZIKReferences.Method("ТекущийЭлемент"));
            System.Diagnostics.Trace.WriteLine("Обрабатываем справочник ШкалаСтавок");
            Sinchro(PUBRefMain, ZIKRefChild);
            cbШкалаСтавок.Checked = true;
            #endregion
            #region Контрагенты
            PUBRefMain = V7B.CreateObject("Справочник.Контрагенты");
            if ((double)ZIKReferences.Method("НайтиПоРеквизиту", (object)"Идентиф", (object)"Контрагенты", (object)1) != 1.0)
            {
                MessageBox.Show("Не могу найти в ЗиК субконто Контрагенты", "Ошибка текущей формы !!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            ZIKRefChild.Method("ИспользоватьВладельца", ZIKReferences.Method("ТекущийЭлемент"));
            System.Diagnostics.Trace.WriteLine("Обрабатываем справочник Контрагенты");
            Sinchro(PUBRefMain, ZIKRefChild);
            cbКонтрагенты.Checked = true;
            #endregion
            #region ВидыЗатрат
            PUBRefMain = V7B.CreateObject("Справочник.ВидыЗатрат");
            if ((double)PUBRefMain.Method("FindByDescr", (object)"Зарплата") == 0)
            {
                MessageBox.Show("Не могу найти в сравочнике ВидыЗатрат ПУБ папку Зарплата", "Ошибка текущей формы !!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            object zarp = PUBRefMain.Method("CurrentItem");
            PUBRefMain.Method("UseParent", zarp, 0);
            if ((double)ZIKReferences.Method("НайтиПоРеквизиту", (object)"Идентиф", (object)"ВидыЗатрат", (object)1) == 0)
            {
                MessageBox.Show("Не могу найти в ЗиК субконто ВидыЗатрат", "Ошибка текущей формы !!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            ZIKRefChild.Method("ИспользоватьВладельца", ZIKReferences.Method("ТекущийЭлемент"));
            System.Diagnostics.Trace.WriteLine("Обрабатываем справочник ВидыЗатрат");
            Sinchro(PUBRefMain, ZIKRefChild);
            cbВидыЗатрат.Checked = true;
            #endregion
            CommonVariables.ProgressBarHide();
            return true;
        }
    
        #endregion
    }
}
