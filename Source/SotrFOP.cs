using System;
using System.Collections.Generic;
using System.Text;
using NTICS.OLE1C77;


namespace PUB_ZIK
{
    class FOPDivision //Налог с подразделения
    {
        public List<PeriodFOP> Period;

        public FOPDivision()
        {
            Period = new   List<PeriodFOP>();
        }
        public void Add(OLE Rasch, decimal rez, List<НалогНаФОП> NalogsFOP)
        {
            PeriodFOP p = new PeriodFOP(Rasch.Method("НачалоПериодаПоДате", Rasch.Property("ДатаНачала")).ToDateTime(), Rasch.Property("Объект"), NalogsFOP);
            int i = Period.BinarySearch(p);
            if (i < 0) // New Period
            {
                Period.Add(p);
                Period.Sort();
                i = Period.BinarySearch(p);
            }
            Period[i].Add(Rasch, rez);
        }


    }
    
    
    class SotrFOP : CommonVariables
    {
        
        public string INN;
        public string Code;
        public bool НеСотрудникТолькоГПХ;

        public SortedList<string, FOPDivision> FOPDivision;

        public SotrFOP()
        {
            FOPDivision = new SortedList<string, FOPDivision>();
        }

        public void Add(OLE Rasch, decimal rez, List<НалогНаФОП> NalogsFOP)
        {
            string PodrCod = Rasch.Property("Объект").Property("Подразделение").Property("Code").ToString().Trim();
            // Вводим поддержку подразделений из назначения 
            if (!OLE.IsEmtyValue(Rasch.Property("Назначение")))
            {
                PodrCod = Rasch.Property("Назначение").Property("МестоРаботы").Property("Владелец").Property("Code").ToString().Trim();

            }
            
            FOPDivision Division;
            if (FOPDivision.ContainsKey(PodrCod))
            {
                Division = FOPDivision[PodrCod];
            }
            else
            {
                Division = new FOPDivision();
                FOPDivision.Add(PodrCod, Division);
            }

            Division.Add(Rasch, rez, NalogsFOP);
        }


        public void GenEntries(ref Проводки prov, OLE Sotr, List<НалогНаФОП> NalogsFOP)
        {
            foreach (FOPDivision Division in FOPDivision.Values)
            {
                foreach (PeriodFOP per in Division.Period)
                {
                    for (int i = 0; i < per.Nalogs.Count; i++)
                    {
                        OLE спрПроводки = V7Z.CreateObject("Справочник.Проводки");
                        спрПроводки.Method("ИспользоватьВладельца", per.Nalogs[i].НалогНаФОП.ХозОперация);
                        спрПроводки.Method("ВыбратьЭлементы");
                        System.Diagnostics.Trace.WriteLine("Налог на фонд " + per.Nalogs[i].НалогНаФОП.СпрШкалаКод + " за период " +
                                                            per.PerD.ToShortDateString() + " - " + per.Nalogs[i].Налог.ToString()+
                                                            " с отпуска - " + per.Nalogs[i].Налог_с_Отпуска.ToString());

                        while (спрПроводки.Method("ПолучитьЭлемент").ToBool())
                        {
                            if (спрПроводки.Method("ПометкаУдаления").ToBool()) continue;
                            // Проведём деление на отпускные
                            if (NalogFOP.ИспользоватьРезервОтпусков)
                            {
                                prov.Item.Add(new Проводка(спрПроводки.Method("CurrentItem"),  //Проводка
                                per.Nalogs[i].Налог_с_Отпуска,                                 //Сумма
                                Code,                                                          //Сотрудник 
                                FOPDivision.Keys[FOPDivision.IndexOfValue(Division)],          //Division
                                per.PerD,                                                     //Период
                                true));
                                
                                //471 счёт
                            }

                            prov.Item.Add(new Проводка(спрПроводки.Method("CurrentItem"),           //Проводка
                                                       per.Nalogs[i].Налог,                         //Сумма
                                                       Code,                                        //Сотрудник 
                                                       FOPDivision.Keys[FOPDivision.IndexOfValue(Division)],                               /*Division*/
                                                       per.PerD));
                             //Период
                        }
                    }
                }
            }

        
        }



    }
}
