using System;
using System.Collections.Generic;
using System.Text;
using NTICS.OLE1C77;

namespace PUB_ZIK
{


    public class НалогНаФОП : CommonVariables
    {
        public string СпрШкалаКод;   // код налога в справочнике шкалы ставок
        /// <summary>
        ///  Проверить использутся ли данный вид расчета в даном налоге
        /// </summary>
        /// <param name="Rasch">Пороверяемый расчет </param>
        public bool ВходитВНалог(OLE Rasch)   
        {
            switch (СпрШкалаКод)
            {
                case "ФЗППенс":
                    return Rasch.Property("ВидРасч").Method("ВходитВГруппу", Rasch.EvalExpr("ГруппаРасчетов.БазаПенсионныйФЗП2004")).ToBool();
                case "ФЗППенсЛет":
                    return Rasch.Property("ВидРасч").Method("ВходитВГруппу", Rasch.EvalExpr("ГруппаРасчетов.БазаПенсионныйФЗП2004")).ToBool();
                case "ФЗППенсИнв":
                    return Rasch.Property("ВидРасч").Method("ВходитВГруппу", Rasch.EvalExpr("ГруппаРасчетов.БазаПенсионныйФЗП2004")).ToBool();
                case "ФЗПСоцСтрах":
                    return Rasch.Property("ВидРасч").Method("ВходитВГруппу", Rasch.EvalExpr("ГруппаРасчетов.БазаСоцСтрахФЗП2004")).ToBool();
                case "ФЗПСоцСтрахИнв":
                    return Rasch.Property("ВидРасч").Method("ВходитВГруппу", Rasch.EvalExpr("ГруппаРасчетов.БазаСоцСтрахФЗП2004")).ToBool();
                case "ФЗПБезраб":
                    return Rasch.Property("ВидРасч").Method("ВходитВГруппу", Rasch.EvalExpr("ГруппаРасчетов.БазаБезработицаФЗП2004")).ToBool();
                case "ФЗПБезрабИнв":
                    return Rasch.Property("ВидРасч").Method("ВходитВГруппу", Rasch.EvalExpr("ГруппаРасчетов.БазаБезработицаФЗП2004")).ToBool();
                case "ФЗПСоцСтрахНесч":
                    return Rasch.Property("ВидРасч").Method("ВходитВГруппу", Rasch.EvalExpr("ГруппаРасчетов.БазаСоцСтрахНесчФЗП2004")).ToBool();
                case "ФЗПРезервОтп":
                    return Rasch.Property("ВидРасч").Method("ВходитВГруппу", Rasch.EvalExpr("ГруппаРасчетов.ВключатьВСреднююДляОтпуска")).ToBool();
                case "ФЗПБезрабГПХ":
                    return Rasch.Property("ВидРасч").Property("CODE").ToString().Trim() == "ОплатаПоДоговору";
            }
            return false;

        }

        public НалогНаФОП(string СпрШкалаКод)
        {
            this.СпрШкалаКод = СпрШкалаКод;
            CurrentProcent = Procent(ДатаПериода.Begin);
            CurrentPredel = Predel(ДатаПериода.Begin);
        }
        
        private decimal CurrentProcent;
        public decimal Procent()
        {
            return CurrentProcent;
        }
        public decimal Procent(DateTime onDate)
        {
            OLE СпрШкала = V7Z.CreateObject("Справочник.ШкалаСтавок");
            if (!СпрШкала.Method("НайтиПоКоду", СпрШкалаКод).ToBool())
            {
                throw new Exception("Ошибка позиционирования по коду ШкалаСтавок.");
            }
            return (decimal)СпрШкала.Property("Ставка").Method("Получить", onDate);
        }

        private decimal CurrentPredel;
        public decimal Predel()
        {
            return CurrentPredel;
        }
        public decimal Predel(DateTime onDate)
        {
            OLE СпрШкала = V7Z.CreateObject("Справочник.ШкалаСтавок");
            if (!СпрШкала.Method("НайтиПоКоду", СпрШкалаКод).ToBool())
            {
                throw new Exception("Ошибка позиционирования по коду ШкалаСтавок.");
            }
            string s = (string)СпрШкала.Property("Дополнительно").Method("Получить", onDate);
            int start = s.LastIndexOf('=') + 1;
            s = s.Substring(start, s.IndexOf(';', start) - start);
            return decimal.Parse(s);
        }

        public OLE ХозОперация
        {
            get
            {
                OLE СпрШкала = V7Z.CreateObject("Справочник.ШкалаСтавок");
                if (!СпрШкала.Method("НайтиПоКоду", СпрШкалаКод).ToBool())
                {
                    throw new Exception("Ошибка позиционирования по коду ШкалаСтавок.");
                }
                return СпрШкала.Property("ХозОперация");
            }
        }


        public static List<НалогНаФОП> ПолучитьСписокТекущихНалоговНаФОП()
        {
            List<НалогНаФОП> NalogsFOP = new List<НалогНаФОП>();

            OLE ноЗПФонды;
            OLE СпрШкала = V7Z.CreateObject("Справочник.ШкалаСтавок");
            if (СпрШкала.Method("НайтиПоКоду", (object)"ЗПФонды").ToBool())
            {
                ноЗПФонды = СпрШкала.Method("ТекущийЭлемент");
            }
            else
            {
                throw new Exception("ОШИБКА !!! Не могу найти сведения о налогах с фонда оплаты труда \n В справочнике 'Шкалы ставок' не обнаружен элемент с кодом 'ЗПФонды'");
            }

            СпрШкала.Method("ИспользоватьРодителя", ноЗПФонды);
            СпрШкала.Method("ИспользоватьДату", ДатаПериода.End);
            СпрШкала.Method("ПорядокКодов");
            СпрШкала.Method("ВыбратьЭлементы");

            while (СпрШкала.Method("ПолучитьЭлемент").ToBool())
            {
                НалогНаФОП nalog = new НалогНаФОП((string)СпрШкала.Property("Код"));
                NalogsFOP.Add(nalog);

            }
            return NalogsFOP;
        }

     }
}





