using System;
using System.Collections.Generic;
using System.Text;
using NTICS.OLE1C77;
using System.Windows.Forms;

namespace PUB_ZIK
{
    struct NALOGSALL:IComparable
    {
        public decimal[] BASE;
        public decimal[] NALOG;
        public decimal[] PROCENT;
        public DateTime PERIOD;

        public static DateTime CurrentFindPeriod;
        public static bool FindPeriod(NALOGSALL p)
        {
            return (p.PERIOD == CurrentFindPeriod);
        }

        #region IComparable Members
        public int CompareTo(object obj)
        {
            return DateTime.Compare(this.PERIOD,((NALOGSALL)obj).PERIOD);
        }
        #endregion
    }
    
    class НалогиНаФОП : CommonVariables
    {
        public List<SotrFOP> Sotrs;
        
        public НалогиНаФОП()
        {
            Sotrs = new List<SotrFOP>();
            List<НалогНаФОП>  NalogsFOP = new List<НалогНаФОП>();

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
        
        }
    }
}
