using System;
using System.Collections.Generic;
using System.Text;
using NTICS.OLE1C77;


namespace PUB_ZIK
{
    class FOPDivision //����� � �������������
    {
        public List<PeriodFOP> Period;

        public FOPDivision()
        {
            Period = new   List<PeriodFOP>();
        }
        public void Add(OLE Rasch, decimal rez, List<����������> NalogsFOP)
        {
            PeriodFOP p = new PeriodFOP(Rasch.Method("�������������������", Rasch.Property("����������")).ToDateTime(), Rasch.Property("������"), NalogsFOP);
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
        public bool ��������������������;

        public SortedList<string, FOPDivision> FOPDivision;

        public SotrFOP()
        {
            FOPDivision = new SortedList<string, FOPDivision>();
        }

        public void Add(OLE Rasch, decimal rez, List<����������> NalogsFOP)
        {
            string PodrCod = Rasch.Property("������").Property("�������������").Property("Code").ToString().Trim();
            // ������ ��������� ������������� �� ���������� 
            if (!OLE.IsEmtyValue(Rasch.Property("����������")))
            {
                PodrCod = Rasch.Property("����������").Property("�����������").Property("��������").Property("Code").ToString().Trim();

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


        public void GenEntries(ref �������� prov, OLE Sotr, List<����������> NalogsFOP)
        {
            foreach (FOPDivision Division in FOPDivision.Values)
            {
                foreach (PeriodFOP per in Division.Period)
                {
                    for (int i = 0; i < per.Nalogs.Count; i++)
                    {
                        OLE ����������� = V7Z.CreateObject("����������.��������");
                        �����������.Method("���������������������", per.Nalogs[i].����������.�����������);
                        �����������.Method("���������������");
                        System.Diagnostics.Trace.WriteLine("����� �� ���� " + per.Nalogs[i].����������.����������� + " �� ������ " +
                                                            per.PerD.ToShortDateString() + " - " + per.Nalogs[i].�����.ToString()+
                                                            " � ������� - " + per.Nalogs[i].�����_�_�������.ToString());

                        while (�����������.Method("���������������").ToBool())
                        {
                            if (�����������.Method("���������������").ToBool()) continue;
                            // ������� ������� �� ���������
                            if (NalogFOP.��������������������������)
                            {
                                prov.Item.Add(new ��������(�����������.Method("CurrentItem"),  //��������
                                per.Nalogs[i].�����_�_�������,                                 //�����
                                Code,                                                          //��������� 
                                FOPDivision.Keys[FOPDivision.IndexOfValue(Division)],          //Division
                                per.PerD,                                                     //������
                                true));
                                
                                //471 ����
                            }

                            prov.Item.Add(new ��������(�����������.Method("CurrentItem"),           //��������
                                                       per.Nalogs[i].�����,                         //�����
                                                       Code,                                        //��������� 
                                                       FOPDivision.Keys[FOPDivision.IndexOfValue(Division)],                               /*Division*/
                                                       per.PerD));
                             //������
                        }
                    }
                }
            }

        
        }



    }
}
