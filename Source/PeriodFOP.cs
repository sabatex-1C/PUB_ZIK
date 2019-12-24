using System;
using System.Collections.Generic;
using System.Text;
using NTICS.OLE1C77;

namespace PUB_ZIK
{
    class PeriodFOP: CommonVariables, IComparable,IDisposable
    {
        public DateTime PerD;
        public List<NalogFOP> Nalogs;

        public PeriodFOP(DateTime Period, OLE Sotr, List<ЌалогЌа‘ќѕ> NalogsFOP)
        {
            PerD = Period;
            Nalogs = new List<NalogFOP>();
            bool Invalid = (Sotr.Property("‘изическоеЋицо").Property("√руппа»нвалидности").Method("ѕолучить",PerD).ToInt() != 0);
            bool Ќе—отрудник“олько√ѕ’ = (Sotr.Property("Ќе—отрудник“олько√ѕ’").Method("ѕолучить", PerD).ToInt() != 0);
            for (int i = 0;i<NalogsFOP.Count;i++)
            {
                switch (NalogsFOP[i].—прЎкала од)
                {
                    case "‘«ѕѕенс": if (Invalid) continue; break;
                    case "‘«ѕѕенсЋет": continue;
                    case "‘«ѕѕенс»нв": if (!Invalid) continue; break;
                    case "‘«ѕ—оц—трах": if (Ќе—отрудник“олько√ѕ’) continue; break;
                    case "‘«ѕ—оц—трах»нв": continue;
                    case "‘«ѕЅезраб": if (Ќе—отрудник“олько√ѕ’) continue; break;
                    case "‘«ѕЅезраб»нв": continue;
                    case "‘«ѕ—оц—трахЌесч": break;
                    case "‘«ѕ–езервќтп": if (!NalogFOP.»спользовать–езервќтпусков | Ќе—отрудник“олько√ѕ’) continue; break;
                    case "‘«ѕЅезраб√ѕ’": if (!Ќе—отрудник“олько√ѕ’) continue; break;
                    default: continue;
                 }
                NalogFOP n = new NalogFOP(NalogsFOP[i], Period);
                Nalogs.Add(n);
            }
         }

        public void Add(OLE Rasch, decimal rez)
        { 
            // перебираем налоги
            foreach (NalogFOP nalog in Nalogs)
            {
                if (nalog.ЌалогЌа‘ќѕ.¬ходит¬Ќалог(Rasch))
                {// етот расчЄт проходит дл€ даного налога
                    bool ќтпуск = Rasch.Property("¬ид–асч").Property("CODE").ToString().Trim() == "ќтпускные";
                    nalog.Add(rez, ќтпуск);

                }
            }
        }


        #region IComparable Members
        public int CompareTo(object obj)
        {
            return DateTime.Compare(this.PerD, ((PeriodFOP)obj).PerD);
        }
        #endregion
        #region IDisposable Members

        public void Dispose()
        {
            Nalogs.Clear();
            Nalogs = null;
        }

        #endregion
    }
}
