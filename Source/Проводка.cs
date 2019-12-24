using System;
using System.Collections.Generic;
using System.Text;
using NTICS.OLE1C77;

namespace PUB_ZIK
{
    class Debit : CommonVariables
    {
        public OLE Account;
        public OLE[] Subconto;
        public OLE Zatrat;

        private OLE TemplateEntry;
        
       

        protected virtual string Marker
        {
            get { return "Дебет"; }
        }
        
        private bool IsAutoSubconto(int NumberSubconto)
        {
            return TemplateEntry.Property("АвтоСубконто" + Marker + NumberSubconto).ToBool();                        
        }
        private OLE DefaultSubconto(int SubcontoNumber, string SubcontoName)
        {
            OLE Result;
            if (SubcontoName == "договора")
            {
                Result = V7B.CreateObject("Документ.Договор");
                if (Result.Method("НайтиПоНомеру", TemplateEntry.Property("Субконто" + Marker + (SubcontoNumber+1)).Property("Code").ToString()).ToBool())
                {
                    return Result.Method("CurrentItem");
                }
            }
            else
            {

                Result = V7B.CreateObject("Справочник." + SubcontoName);
                int search = 0;
                if (V7B.EvalExpr("Метаданные.Справочник(\"" + SubcontoName + "\").СерииКодов").ToString().Trim().ToUpper() == ("ВПределахПодчинения").ToUpper())
                {
                    search = 2;
                }

                if (Result.Method("FindByCode", TemplateEntry.Property("Субконто" + Marker + (SubcontoNumber + 1)).Property("КАУ").ToString().Trim(), search).ToBool())
                {
                    if (SubcontoName.ToUpper() == "ВидыЗатрат".ToUpper()) Zatrat = Result.Method("CurrentItem");
                    return Result.Method("CurrentItem");
                }
            }
            return null;
        }
        private OLE getSotr(string СотрудникКод)
        {
            if (СотрудникКод == "") { return null; }
            return ПолучитьСотрудникаПУБ(СотрудникКод);
        }
        private OLE ВидДеятельности(string ПодразделениеКод)
        {
            switch (ПодразделениеКод.Trim())
            {
                case "0000000019": return GetReferenceElementPUB("видыдеятельности", "10");
                case "0000000011": return GetReferenceElementPUB("видыдеятельности", "7");
                case "0000000017": return GetReferenceElementPUB("видыдеятельности", "2");
                case "0000000005": return GetReferenceElementPUB("видыдеятельности", "5");
                case "0000000018": return GetReferenceElementPUB("видыдеятельности", "9");
                case "0000000008": return GetReferenceElementPUB("видыдеятельности", "8");
                case "0000000013": return GetReferenceElementPUB("видыдеятельности", "6");
                case "0000000001": return GetReferenceElementPUB("видыдеятельности", "1");
                case "0000000006": return GetReferenceElementPUB("видыдеятельности", "4");
                case "0000000007": return GetReferenceElementPUB("видыдеятельности", "3");
                default: return GetReferenceElementPUB("видыдеятельности", "1");
            }
           
         }
        private string СчётЗатрат(string ПодразделениеКод)
        {
            switch (ПодразделениеКод.Trim())
            {
                case "0000000019": return "231";
                case "0000000011": return "91";
                case "0000000017": return "231";
                case "0000000005": return "231";
                case "0000000018": return "231";
                case "0000000008": return "231";
                case "0000000013": return "93";
                case "0000000001": return "92";
                case "0000000006": return "231";
                case "0000000007": return "231";
                default: return "92";
            }
           
         }
        private bool ЭтоСчетЗатрат(string Счёт)
        {
            if ((Счёт == "231") || (Счёт == "91") || (Счёт == "92") || (Счёт == "93")) return true;
            return false;
        }


        private OLE Analiz(string SubcontoName, int SubcontoNumber, string СотрудникКод, string ПодразделениеКод, DateTime Period)
        {
            if (SubcontoName == "сотрудники")
            {
                if (!conf.БезАналитикиПоСотрудникам)
                {
                    return getSotr(СотрудникКод);
                }
            }

            if (SubcontoName == "подразделения")
            {
                return GetReferenceElementPUB("подразделения", ПодразделениеКод);
            }

            if (SubcontoName == "месяцначислениязп")
            {
                if (conf.АналитикаПоПериодам)
                {
                    return new OLE(DateTime.Parse("01."+Period.Month.ToString() +"."+Period.Year.ToString()));
                }
            }


            if (SubcontoName == "видыдеятельности")
            {
                return ВидДеятельности(ПодразделениеКод);
            }
            
            return DefaultSubconto(SubcontoNumber,SubcontoName);
             
        }
        public Debit(OLE Проводка, string СотрудникКод, string ПодразделениеКод, DateTime Period, bool ЭтоНачисленияНаОтпуск)
        {
            this.TemplateEntry = Проводка;
            string tAccount = Проводка.Property("Счет" + Marker + "а").Property("Code").ToString().Trim();
            if (tAccount == "")
            {
                throw new Exception("");
            }
            if (tAccount == "ВТ")
            {// Спецподстановка
                tAccount = СчётЗатрат(ПодразделениеКод);
                if (tAccount == "")
                {
                    tAccount = "91";
                    //throw new Exception("");
                }

            }
            this.Account = CommonVariables.BConnection.Global.EvalExpr("СчетПоКоду(\"" + tAccount + "\")");
            tAccount = this.Account.Property("Code").ToString().Trim();
            if (tAccount == "")
            {
                tAccount = "91";
                this.Account = CommonVariables.BConnection.Global.EvalExpr("СчетПоКоду(\"" + tAccount + "\")");
                //throw new Exception("");
            }

            // если начисление на отпуск то
            if (ЭтоНачисленияНаОтпуск && (Marker == "Дебет") && ЭтоСчетЗатрат(tAccount))
            {
                this.Account = CommonVariables.BConnection.Global.EvalExpr("СчетПоКоду(\"" + "471" + "\")");
                Subconto = new OLE[this.Account.Method("КоличествоСубконто").ToInt()];
                Subconto[0] = ВидДеятельности(ПодразделениеКод);
                Subconto[1] = GetReferenceElementPUB("подразделения", ПодразделениеКод);
                Subconto[2] = GetReferenceElementPUB("видызатрат", "0014"); //Отпускные
                return;
            }

            Subconto = new OLE[this.Account.Method("КоличествоСубконто").ToInt()];
            for (int i = 0; i < Subconto.Length; i++)
            {
                Subconto[i] = Analiz(this.Account.Method("ВидСубконто",i+1).Method("Идентификатор").ToString().Trim().ToLower(),i, СотрудникКод, ПодразделениеКод, Period);
            }
        }
    }
    class Credit : Debit
    {
        public Credit(OLE Проводка, string СотрудникКод, string ПодразделениеКод, DateTime Period, bool ЭтоНачисленияНаОтпуск) : base(Проводка, СотрудникКод, ПодразделениеКод, Period, ЭтоНачисленияНаОтпуск) { }

        protected override string Marker
        {
            get
            {
                return "Кредит";
            }
        }

    }
    class Проводка : CommonVariables,IComparable
    {
        public static int МаксКоличествоСубконто;
        public string Error = "";

        public Debit Debit;
        public Credit Credit;
        public decimal Summ;
        public string Comment;

        // OLE     Проводка         - элемент справочника ЭП_Проводки, на основании которого формируется проводка
        // decimal Сумма            - сумма проводки
        // string СотрудникКод      - код сотрудника, по которому формирует проводка
        // string ПодразделениеКод  - код подразделения по которому формируется проводка
        // DateTime Period          - Период формируемой проводки
        // bool ЭтоНачисленияНаОтпуск - Прзнак начисления на отпуск 
        public Проводка(OLE ШаблонПроводки, decimal Сумма, string СотрудникКод, string ПодразделениеКод, DateTime Period, bool ЭтоНачисленияНаОтпуск)
        {

            if (OLE.IsEmtyValue(ШаблонПроводки.Property("СчетДебета")) && OLE.IsEmtyValue(ШаблонПроводки.Property("СчетКредита")))
            {
                Error = "В проводке " + ШаблонПроводки.Property("Код") + " хоз. операции " + ШаблонПроводки.Property("Владелец").Property("Description") + " не заданы счета! Проводка не формируется!";
                return;
            }

            
            Сумма = decimal.Round(Сумма,2);
            
            if (Сумма == 0)
            {
                Error = "По проводке №" + ШаблонПроводки.Property("Код") + " хоз. операции " + ШаблонПроводки.Property("Владелец").Property("Description") + " сумма 0! Проводка не формируется!";
                return;
            }


            if (ШаблонПроводки.Property("УказанПроцент").ToBool())
            {
                Summ = decimal.Round(Сумма * ШаблонПроводки.Property("Процент").ToDecimal() / 100, 2);
            }
            else
            {
                Summ = Сумма;
            }

            Debit = new Debit(ШаблонПроводки, СотрудникКод, ПодразделениеКод, Period, ЭтоНачисленияНаОтпуск);
            Credit = new Credit(ШаблонПроводки, СотрудникКод, ПодразделениеКод, Period, ЭтоНачисленияНаОтпуск);
            Comment = " ";
        }
        public Проводка(OLE ШаблонПроводки, decimal Сумма, string СотрудникКод, string ПодразделениеКод, DateTime Period):
            this(ШаблонПроводки, Сумма, СотрудникКод, ПодразделениеКод, Period, false){}

    

    #region IComparable Members
        public int CompareTo(object obj)
        {
            Проводка pr = (Проводка)obj;
            return 0;
         
        }

        #endregion
    }
}
