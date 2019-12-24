using System;
using System.Collections.Generic;
using System.Text;
using NTICS.OLE1C77;

namespace PUB_ZIK
{
    public class Распределение : CommonVariables, IComparable
    {
        public string DocNumber;
        public OLE Doc;



        #region IComparable Members

        public int CompareTo(object obj)
        {
            return string.Compare(this.DocNumber,((Распределение)obj).DocNumber);
        }

        #endregion
    }

    
    
    
    public class Распределения : CommonVariables
    {
        private List<Распределение> FRaspr;
        public List<Распределение> Items
        {
            get { return FRaspr; }
        }
        public Распределения(OLE obj)
        {
            OLE V7Z = obj.Global;
            FRaspr = new List<Распределение>();
            FRaspr.Sort();
            // заполняет список спРаспределения документами Распределения за указанный период
            string ТекстЗапр = "Период с '" + ДатаПериода.Begin.ToShortDateString() + "' по '" + ДатаПериода.End.ToShortDateString() + "';" +
                "Обрабатывать НеПомеченныеНаУдаление;" +
                "Рас = Документ.РаспределениеОсновныхНачислений.ТекущийДокумент;" +
                "Фир = Документ.РаспределениеОсновныхНачислений.Фирма;" +
                "Сот = Документ.РаспределениеОсновныхНачислений.Сотрудник;" +
                "Наз = Документ.РаспределениеОсновныхНачислений.Назначение;" +
                "Под = Документ.РаспределениеОсновныхНачислений.Подразделение;" +
                "Кат = Документ.РаспределениеОсновныхНачислений.Категория;" +
                "Группировка Рас без упорядочивания;" +
                "Условие(Фир.Code = \"" +  CommonVariables.GetFirm(obj).Property("CODE").ToString().Trim()/*ZFirm.Code*/ + "\");";

            OLE зРаспределения = V7Z.CreateObject("Запрос");
            if (!(bool)зРаспределения.Method("Выполнить", ТекстЗапр))
            {
                throw new Exception("ОШИБКА!!! Не выполнился запрос по распределениям основных начислений!");
            }

            while ((double)зРаспределения.Method("Группировка", "Рас") == 1.0)
            {
                // в список нужно добавить либо назначение, либо сотрудника, либо подразделение, либо категорию
                OLE ЧтоДобавляем = null;
                if (!OLE.IsEmtyValue(зРаспределения.Property("Кат")))
                {
                    ЧтоДобавляем = зРаспределения.Property("Кат"); // priore 3
                }

                if (!OLE.IsEmtyValue(зРаспределения.Property("Под")))
                {
                    ЧтоДобавляем = зРаспределения.Property("Под"); // priore 2
                }
                 
                if (!OLE.IsEmtyValue(зРаспределения.Property("Сот")))
                {
                    ЧтоДобавляем = зРаспределения.Property("Сот"); // priore 1
                }
                
                
                if (!OLE.IsEmtyValue(зРаспределения.Property("Наз")))
                {
                    ЧтоДобавляем = зРаспределения.Property("Наз"); // priore 0
                }

                if (ЧтоДобавляем == null)
                {
                    System.Diagnostics.Trace.WriteLine("В документе " + (string)зРаспределения.Property("Рас") + " не указано ни назначение, ни сотрудник, ни подразделение, ни категория! Документ не учитывается при формировании проводок!");
                    continue;
                }

                Распределение r = new Распределение();
                r.DocNumber = V7Z.Method("ЗначениеВСтрокуВнутр",ЧтоДобавляем).ToString();
                r.Doc = ЧтоДобавляем;

                int Инд = Items.BinarySearch(r);
                if (Инд >= 0)
                {
                    // информация о распределении для данного объекта уже есть
                    System.Diagnostics.Trace.WriteLine("Документ РаспределениеОсновныхНачислений " + (string)зРаспределения.Property("Рас").Property("НомерДок") + " дублирует информацию о " + (string)ЧтоДобавляем + ", внесенную документом " + Items[Инд].DocNumber);
                    continue;
                }
                Items.Add(r);
            }
        }
    }
}
