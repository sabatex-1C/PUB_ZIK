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

        public PeriodFOP(DateTime Period, OLE Sotr, List<����������> NalogsFOP)
        {
            PerD = Period;
            Nalogs = new List<NalogFOP>();
            bool Invalid = (Sotr.Property("��������������").Property("������������������").Method("��������",PerD).ToInt() != 0);
            bool �������������������� = (Sotr.Property("��������������������").Method("��������", PerD).ToInt() != 0);
            for (int i = 0;i<NalogsFOP.Count;i++)
            {
                switch (NalogsFOP[i].�����������)
                {
                    case "�������": if (Invalid) continue; break;
                    case "����������": continue;
                    case "����������": if (!Invalid) continue; break;
                    case "�����������": if (��������������������) continue; break;
                    case "��������������": continue;
                    case "���������": if (��������������������) continue; break;
                    case "������������": continue;
                    case "���������������": break;
                    case "������������": if (!NalogFOP.�������������������������� | ��������������������) continue; break;
                    case "������������": if (!��������������������) continue; break;
                    default: continue;
                 }
                NalogFOP n = new NalogFOP(NalogsFOP[i], Period);
                Nalogs.Add(n);
            }
         }

        public void Add(OLE Rasch, decimal rez)
        { 
            // ���������� ������
            foreach (NalogFOP nalog in Nalogs)
            {
                if (nalog.����������.������������(Rasch))
                {// ���� ������ �������� ��� ������ ������
                    bool ������ = Rasch.Property("�������").Property("CODE").ToString().Trim() == "���������";
                    nalog.Add(rez, ������);

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
