using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NTICS.OLE1C77;
using System.Diagnostics;


namespace PUB_ZIK
{
    
    
    public partial class TransferOperation : CommonControl
    {
        private OLE PUBRefWoker;
        private ListBoxTraceListener TraceListner;
        const string OperationDescription = "NTICS Account transfer programm";

        public TransferOperation()
        {
            InitializeComponent();
            Executabled = ExecuteStatus.Always;
        }

        private bool FindOperation(ref OLE Oper)
        {
            if ((double)Oper.Method("SelectOpers", CommonVariables.ДатаПериода.End, CommonVariables.ДатаПериода.End) == 0) { return false; }
            while ((double)Oper.Method("GetOper") == 1.0)
            {
                string s = (string)Oper.Property("Description");
                if (s.Trim() == OperationDescription) { return true; }
            }
            
            return false;
        }

        // Ищем документ
        // Global    - глобальный контекст конфигурации
        // DocName   - название документа
        // DateStart - начальная дата
        // DateEnd   - конечная дата
        // LabelDoc  - метка документа
        // Возврат:
        //    Найденый документ или null
        private OLE FindDoc(OLE Global, string DocName, DateTime DateStart, DateTime DateEnd, string LabelDoc)
        {
            OLE doc = Global.CreateObject("Документ."+DocName);
            if (!doc.Method("ВыбратьДокументы", DateStart, DateEnd).ToBool()) return null;
            while (doc.Method("Получитьдокумент").ToBool())
            {
                if (doc.Property("Операция").Property("Description").ToString().Trim() == OperationDescription) return doc;
            }
            return null;

        }

        private OLE GenDoc231()
        {
            OLE doc =  FindDoc(V7B,"ЗатратыПроизводственногоХарактера",CommonVariables.ДатаПериода.End,
                               CommonVariables.ДатаПериода.End,OperationDescription);
            if (doc != null)
            {
                if (doc.Method("Проведен").ToBool()) doc.Method("СделатьНеПроведенным");
                doc.Method("УдалитьСтроки");
            }
            else
            {
                doc = V7B.CreateObject("Документ.ЗатратыПроизводственногоХарактера");
                doc.Method("New");
                V7B.Method("глУстановитьФирму",doc);
                doc.Property("ПроводитьПоЭлементамЗатрат",V7B.EvalExpr("?(ИспользоватьСчетаРасходов <> Класс9,1,0)"));
                doc.Property("ДатаДок", CommonVariables.ДатаПериода.End);
                doc.Property("Операция").Property("Description", OperationDescription);
            }
            doc.Property("Примечание", "Генерація проводок " + DateTime.Now);


            // Проверим на пригодность к записи
            bool entr_ok = true;
            do
            {
                try
                {
                    doc.Method("Записать");
                    entr_ok = true;
                }
                catch
                {
                    entr_ok = false;
                    doc.Property("НомерДок", int.Parse(doc.Property("НомерДок").ToString()) + 1);
                }


            } while (!entr_ok);
            return doc;
        }
        private OLE CreateOperation()
        {
            OLE Operation = V7B.CreateObject("Operation");
            if (FindOperation(ref Operation))
            {
                while (Operation.Method("SelectEntries").ToBool())
                {
                    Operation.Method("DeleteEntry");
                }
            }
            else
            {
                Operation.Method("New");
                Operation.Property("OperDate", CommonVariables.ДатаПериода.End);
                Operation.Property("Description", OperationDescription);
            }
            Operation.Property("Документ").Property("Фирма", CommonVariables.GetFirm(Operation) /*CommonVariables.BFirm.CurrentItem*/);
            Operation.Property("Документ").Property("Примечание", "Генерація проводок " + DateTime.Now);


            // Проверим на пригодность к записи
            bool entr_ok = true;
            do
            {
                try
                {
                    Operation.Method("Записать");
                    entr_ok = true;
                }
                catch
                {
                    entr_ok = false;
                    Operation.Property("Документ").Property("НомерДок", int.Parse(Operation.Property("Документ").Property("НомерДок").ToString()) + 1);
                }


            } while (!entr_ok);
            return Operation;
        }
        private void SetLineDoc(ref OLE DOC, Проводка provodka, int WriteCount)
        {
            DOC.Method("НоваяСтрока");
            Trace.WriteLine(WriteCount + " Проводка - Д(" + provodka.Debit.Account.Property("Код").ToString() +
                            ") K(" + provodka.Credit.Account.Property("Код").ToString() +
                            ") SUMM(" + (double)provodka.Summ + ")");

            DOC.Property("ВидДеятельности", provodka.Debit.Subconto[0]);
            DOC.Property("Подразделение", provodka.Debit.Subconto[1]);
            if (provodka.Debit.Zatrat != null)
            {
                DOC.Property("ВидЗатрат", provodka.Debit.Zatrat);
            }
            else
            {
                DOC.Property("ВидЗатрат", provodka.Credit.Zatrat);
            }
            DOC.Property("Счет", provodka.Credit.Account);
            int i;
            for (i = 0; i < provodka.Credit.Subconto.Length; i++)
            {
                if (provodka.Credit.Subconto[i] != null)
                {
                    DOC.Method("НазначитьТип","Субконто" + (i+1),provodka.Credit.Account.Method("ВидСубконто",i+1));
                    DOC.Property("Субконто"+ (i+1),provodka.Credit.Subconto[i]);
                }
            }

            DOC.Property("Сумма", (double)provodka.Summ);
            DOC.Property("Примечание", provodka.Comment);
            //Operation.Property("НашаФирма", CommonVariables.FirmB /* CommonVariables.BFirm.CurrentItem */);
 
        }



        protected override bool doExecute()
        {
            Refresh();
            Trace.WriteLine("Начата синхронизация даных");
            Base DataBase = new Base();
            conf.RestopeBase(ref DataBase);
            OLE temp;

            // Активируем соединения с 1С7.7
            if (CommonVariables.ZConnection != null) { CommonVariables.ZConnection.Dispose(); }
            try
            {
                CommonVariables.ZConnection = new NTICS.OLE1C77.OLEConnection(conf.CONNECTION_BasePatchZ, (NTICS.OLE1C77.V77Servers)conf.CONNECTION_ServerType, false, conf.UserName, conf.PassWord);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Неудачная попытка подключения к  " + conf.CONNECTION_BasePatchZ+"\n"+ e.Message);
                CommonVariables.ZConnection = null;
                return false;
            }
            System.Diagnostics.Trace.WriteLine("Вы подключились к Зик.");
            if (CommonVariables.ZConnection == null) { return false; }

            OLE FirmZ = CommonVariables.GetFirm(CommonVariables.ZConnection.Global);
            if (FirmZ == null)
            {
                System.Diagnostics.Trace.WriteLine("Отсутствует хотябы одна зарегистрированая фирма в ЗиК !!!");
                return false;
            }


            if (CommonVariables.BConnection != null) { CommonVariables.BConnection.Dispose(); }
            try
            {
                CommonVariables.BConnection = new NTICS.OLE1C77.OLEConnection(conf.CONNECTION_BasePatchB, (NTICS.OLE1C77.V77Servers)conf.CONNECTION_ServerType, false, conf.UserName, conf.PassWord);
            }
            catch
            {
                Trace.WriteLine("Неудачная попытка подключения к  " + conf.CONNECTION_BasePatchB);
                CommonVariables.BConnection = null;
                return false;
            }
            System.Diagnostics.Trace.WriteLine("Вы подключились к Бухгалтерии.");
            if (CommonVariables.BConnection == null) { return false; }
            OLE FirmB = CommonVariables.GetFirm(CommonVariables.BConnection.Global);
            if (FirmB == null)
            {
                System.Diagnostics.Trace.WriteLine("Отсутствует хотябы одна зарегистрированая фирма в Бухгалтерии !!!");
                return false;
            }

            System.Diagnostics.Trace.WriteLine("Соединение прошло успешно");

            
            
            CommonVariables.ДатаПериода =  new NTICS.Period(selectPeriod.Period);

            // Справочник Сотрудников ПУБ
            PUBRefWoker = V7B.CreateObject("Справочник.Сотрудники");
 
            // Заполним список сотрудников
            CommonVariables.MEMPUBWoker = MEMReferences.CopyOLERefToMEMRef(PUBRefWoker, "ИНН");
            CommonVariables.MEMPUBWoker.Sort();

            Проводка.МаксКоличествоСубконто = 4;


            // Перебираем всех сотрудников
            OLE Сотрудники = V7Z.CreateObject("Справочник.Сотрудники");
            OLE ЖурналЗарплата = V7Z.CreateObject("ЖурналРасчетов.Зарплата");
            
            
            Сотрудники.Method("ВыбратьЭлементы");
            Проводки prov = new Проводки();
            //НалогиНаФОП nalogs = new НалогиНаФОП();
            CommonVariables.Распределения = new Распределения(V7Z);
            
            // Список действующих налогов на фоп
            List<НалогНаФОП> NalogsFOP = НалогНаФОП.ПолучитьСписокТекущихНалоговНаФОП();

            #region Create Entries and nalogs on FOP
            while (Сотрудники.Method("ПолучитьЭлемент").ToBool())
            {
                Trace.WriteLine("Сотрудник - " + Сотрудники.Property("Description").ToString().Trim());

                // Пропустим сотрудника без начислений
                if (!ЖурналЗарплата.Method("SelectPeriodByObject", Сотрудники.Method("CurrentItem"), ДатаПериода).ToBool())
                    continue;

                SotrFOP Сотрудник = new SotrFOP();
                Сотрудник.INN = Сотрудники.Property("ФизическоеЛицо").Property("ИНН").ToString();
                while (ЖурналЗарплата.Method("GetRecord").ToBool()) // Выведем проводки по даному начислению
                {
                    decimal rez = (decimal)ЖурналЗарплата.Property("Результат");
                    if (rez == 0) continue; // пропустим нулевые
                    if (Проводки.СистемныйВидРасч(ЖурналЗарплата)) continue; // пропустим системные расчёты
                    Trace.WriteLine("Вид расчёта - " + ЖурналЗарплата.Property("ВидРасч").Property("Code").ToString().Trim() + " на сумму - " + rez);
                    prov.Add(Сотрудники.Method("CurrentItem"), ЖурналЗарплата, rez);
                    Сотрудник.Add(ЖурналЗарплата, rez, NalogsFOP);
                }
                Сотрудник.GenEntries(ref prov, Сотрудники, NalogsFOP);  // Сгенегируем проводки для налогов ФОП
                Trace.WriteLine("Проводок - " + prov.Item.Count.ToString());
                Application.DoEvents();
            }
            #endregion
            Trace.WriteLine("Проводок - " + prov.Item.Count.ToString());

            #region Generate Entries

            OLE Operation = CreateOperation();
            OLE DOC = GenDoc231();

            int WriteCount = 0;
            foreach (Проводка provodka in prov.Item)
            {
                if (provodka.Debit == null) continue;
                if (provodka.Debit.Account == null) continue;
                if (provodka.Credit.Account == null) continue;
                // счёт 231
                if (provodka.Debit.Account.Property("CODE").ToString().Trim() == "231")
                {
                    SetLineDoc(ref DOC, provodka,WriteCount);
                    WriteCount++;
                    continue;
                }

                Operation.Method("НоваяПроводка");
                Trace.WriteLine(WriteCount + " Проводка - Д(" +  provodka.Debit.Account.Property("Код").ToString()+
                                ") K("+provodka.Credit.Account.Property("Код").ToString() +
                                ") SUMM("+(double)provodka.Summ+")");

                Operation.Property("Дебет").Property("Счет", provodka.Debit.Account);
                int i;
                for (i = 0; i < provodka.Debit.Subconto.Length; i++)
                {
                    if (provodka.Debit.Subconto[i] != null)
                    {
                        Operation.Property("Debit").Method("Subconto", i + 1, provodka.Debit.Subconto[i]);
                    }
                }


                Operation.Property("Кредит").Property("Счет", provodka.Credit.Account);
                for (i = 0; i < provodka.Credit.Subconto.Length; i++)
                {
                    if (provodka.Credit.Subconto[i] != null)
                    {
                        Operation.Property("Credit").Method("Subconto", i + 1, provodka.Credit.Subconto[i]);
                    }
                }

                Operation.Property("Сумма", (double)provodka.Summ);
                Operation.Property("Комментарий", provodka.Comment);
                Operation.Property("НашаФирма", CommonVariables.GetFirm(Operation) /* CommonVariables.BFirm.CurrentItem */);
                WriteCount++;
 
                if (WriteCount % 100 == 0)
                {
                    try
                    {
                        Operation.Method("Записать");
                        Debug.WriteLine("WRITE OK");
                    }
                    catch
                    {
                        Debug.WriteLine("WRITE ERROR");
                    }

                }

            }
            try
            {
                Operation.Method("Записать");
                DOC.Method("Провести");
                DOC.Method("Записать");
                Debug.WriteLine("WRITE OK");
            }
            catch
            {
                Debug.WriteLine("WRITE ERROR");
            }
            Operation.Dispose();
            Operation = null;



            #endregion

            CommonVariables.MEMPUBWoker = null;
            PUBRefWoker = null;
            CommonVariables.ZConnection = null;
            CommonVariables.BConnection = null;
            Trace.WriteLine("Сохраним базу даных ...");
            conf.StoreBase(DataBase);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Trace.WriteLine("ОК");
            Trace.WriteLine("Перенос проводок выполнен.",Traces.Status);
            return true;
       }

        protected override void doInitialize()
        {
            base.doInitialize();
            TraceListner = new ListBoxTraceListener(listTrace);
            Trace.Listeners.Add(TraceListner);
            selectPeriod.Period = conf.ДатаПериода;
 
            cbБезАналитикиПоСотрудникам.Checked = conf.БезАналитикиПоСотрудникам;
            cbАналитикаПоПериодам.Checked = conf.АналитикаПоПериодам;
            cbВыгружатьСотрудников.Checked = conf.ВыгружатьСотрудников;
            ОбеспечениеОтпусков.Checked = conf.ОбеспечениеОтпусков;
            NalogFOP.ИспользоватьРезервОтпусков = conf.ОбеспечениеОтпусков;
        }

        protected override void doSaveParams()
        {
            base.doSaveParams();
            conf.ДатаПериода = selectPeriod.Period;
            conf.БезАналитикиПоСотрудникам = cbБезАналитикиПоСотрудникам.Checked;
            conf.АналитикаПоПериодам = cbАналитикаПоПериодам.Checked;
            conf.ВыгружатьСотрудников = cbВыгружатьСотрудников.Checked;
            conf.ОбеспечениеОтпусков = NalogFOP.ИспользоватьРезервОтпусков;
        }

        protected override void doDeInitialize()
        {
            Trace.Listeners.Remove(TraceListner);
            base.doDeInitialize();
        }

        #region ACTIONS
        private void cbБезАналитикиПоСотрудникам_CheckedChanged(object sender, EventArgs e)
        {
            conf.БезАналитикиПоСотрудникам = cbБезАналитикиПоСотрудникам.Checked;
            conf.АналитикаПоПериодам = cbАналитикаПоПериодам.Checked;
            conf.ВыгружатьСотрудников = cbВыгружатьСотрудников.Checked;
            NalogFOP.ИспользоватьРезервОтпусков = ОбеспечениеОтпусков.Checked;
        }
        #endregion

 
 

    }



}
