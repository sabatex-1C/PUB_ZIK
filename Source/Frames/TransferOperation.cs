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
            if ((double)Oper.Method("SelectOpers", CommonVariables.�����������.End, CommonVariables.�����������.End) == 0) { return false; }
            while ((double)Oper.Method("GetOper") == 1.0)
            {
                string s = (string)Oper.Property("Description");
                if (s.Trim() == OperationDescription) { return true; }
            }
            
            return false;
        }

        // ���� ��������
        // Global    - ���������� �������� ������������
        // DocName   - �������� ���������
        // DateStart - ��������� ����
        // DateEnd   - �������� ����
        // LabelDoc  - ����� ���������
        // �������:
        //    �������� �������� ��� null
        private OLE FindDoc(OLE Global, string DocName, DateTime DateStart, DateTime DateEnd, string LabelDoc)
        {
            OLE doc = Global.CreateObject("��������."+DocName);
            if (!doc.Method("����������������", DateStart, DateEnd).ToBool()) return null;
            while (doc.Method("����������������").ToBool())
            {
                if (doc.Property("��������").Property("Description").ToString().Trim() == OperationDescription) return doc;
            }
            return null;

        }

        private OLE GenDoc231()
        {
            OLE doc =  FindDoc(V7B,"���������������������������������",CommonVariables.�����������.End,
                               CommonVariables.�����������.End,OperationDescription);
            if (doc != null)
            {
                if (doc.Method("��������").ToBool()) doc.Method("��������������������");
                doc.Method("�������������");
            }
            else
            {
                doc = V7B.CreateObject("��������.���������������������������������");
                doc.Method("New");
                V7B.Method("�����������������",doc);
                doc.Property("��������������������������",V7B.EvalExpr("?(������������������������� <> �����9,1,0)"));
                doc.Property("�������", CommonVariables.�����������.End);
                doc.Property("��������").Property("Description", OperationDescription);
            }
            doc.Property("����������", "��������� �������� " + DateTime.Now);


            // �������� �� ����������� � ������
            bool entr_ok = true;
            do
            {
                try
                {
                    doc.Method("��������");
                    entr_ok = true;
                }
                catch
                {
                    entr_ok = false;
                    doc.Property("��������", int.Parse(doc.Property("��������").ToString()) + 1);
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
                Operation.Property("OperDate", CommonVariables.�����������.End);
                Operation.Property("Description", OperationDescription);
            }
            Operation.Property("��������").Property("�����", CommonVariables.GetFirm(Operation) /*CommonVariables.BFirm.CurrentItem*/);
            Operation.Property("��������").Property("����������", "��������� �������� " + DateTime.Now);


            // �������� �� ����������� � ������
            bool entr_ok = true;
            do
            {
                try
                {
                    Operation.Method("��������");
                    entr_ok = true;
                }
                catch
                {
                    entr_ok = false;
                    Operation.Property("��������").Property("��������", int.Parse(Operation.Property("��������").Property("��������").ToString()) + 1);
                }


            } while (!entr_ok);
            return Operation;
        }
        private void SetLineDoc(ref OLE DOC, �������� provodka, int WriteCount)
        {
            DOC.Method("�����������");
            Trace.WriteLine(WriteCount + " �������� - �(" + provodka.Debit.Account.Property("���").ToString() +
                            ") K(" + provodka.Credit.Account.Property("���").ToString() +
                            ") SUMM(" + (double)provodka.Summ + ")");

            DOC.Property("���������������", provodka.Debit.Subconto[0]);
            DOC.Property("�������������", provodka.Debit.Subconto[1]);
            if (provodka.Debit.Zatrat != null)
            {
                DOC.Property("���������", provodka.Debit.Zatrat);
            }
            else
            {
                DOC.Property("���������", provodka.Credit.Zatrat);
            }
            DOC.Property("����", provodka.Credit.Account);
            int i;
            for (i = 0; i < provodka.Credit.Subconto.Length; i++)
            {
                if (provodka.Credit.Subconto[i] != null)
                {
                    DOC.Method("������������","��������" + (i+1),provodka.Credit.Account.Method("�����������",i+1));
                    DOC.Property("��������"+ (i+1),provodka.Credit.Subconto[i]);
                }
            }

            DOC.Property("�����", (double)provodka.Summ);
            DOC.Property("����������", provodka.Comment);
            //Operation.Property("���������", CommonVariables.FirmB /* CommonVariables.BFirm.CurrentItem */);
 
        }



        protected override bool doExecute()
        {
            Refresh();
            Trace.WriteLine("������ ������������� �����");
            Base DataBase = new Base();
            conf.RestopeBase(ref DataBase);
            OLE temp;

            // ���������� ���������� � 1�7.7
            if (CommonVariables.ZConnection != null) { CommonVariables.ZConnection.Dispose(); }
            try
            {
                CommonVariables.ZConnection = new NTICS.OLE1C77.OLEConnection(conf.CONNECTION_BasePatchZ, (NTICS.OLE1C77.V77Servers)conf.CONNECTION_ServerType, false, conf.UserName, conf.PassWord);
            }
            catch (Exception e)
            {
                Trace.WriteLine("��������� ������� ����������� �  " + conf.CONNECTION_BasePatchZ+"\n"+ e.Message);
                CommonVariables.ZConnection = null;
                return false;
            }
            System.Diagnostics.Trace.WriteLine("�� ������������ � ���.");
            if (CommonVariables.ZConnection == null) { return false; }

            OLE FirmZ = CommonVariables.GetFirm(CommonVariables.ZConnection.Global);
            if (FirmZ == null)
            {
                System.Diagnostics.Trace.WriteLine("����������� ������ ���� ����������������� ����� � ��� !!!");
                return false;
            }


            if (CommonVariables.BConnection != null) { CommonVariables.BConnection.Dispose(); }
            try
            {
                CommonVariables.BConnection = new NTICS.OLE1C77.OLEConnection(conf.CONNECTION_BasePatchB, (NTICS.OLE1C77.V77Servers)conf.CONNECTION_ServerType, false, conf.UserName, conf.PassWord);
            }
            catch
            {
                Trace.WriteLine("��������� ������� ����������� �  " + conf.CONNECTION_BasePatchB);
                CommonVariables.BConnection = null;
                return false;
            }
            System.Diagnostics.Trace.WriteLine("�� ������������ � �����������.");
            if (CommonVariables.BConnection == null) { return false; }
            OLE FirmB = CommonVariables.GetFirm(CommonVariables.BConnection.Global);
            if (FirmB == null)
            {
                System.Diagnostics.Trace.WriteLine("����������� ������ ���� ����������������� ����� � ����������� !!!");
                return false;
            }

            System.Diagnostics.Trace.WriteLine("���������� ������ �������");

            
            
            CommonVariables.����������� =  new NTICS.Period(selectPeriod.Period);

            // ���������� ����������� ���
            PUBRefWoker = V7B.CreateObject("����������.����������");
 
            // �������� ������ �����������
            CommonVariables.MEMPUBWoker = MEMReferences.CopyOLERefToMEMRef(PUBRefWoker, "���");
            CommonVariables.MEMPUBWoker.Sort();

            ��������.���������������������� = 4;


            // ���������� ���� �����������
            OLE ���������� = V7Z.CreateObject("����������.����������");
            OLE �������������� = V7Z.CreateObject("��������������.��������");
            
            
            ����������.Method("���������������");
            �������� prov = new ��������();
            //����������� nalogs = new �����������();
            CommonVariables.������������� = new �������������(V7Z);
            
            // ������ ����������� ������� �� ���
            List<����������> NalogsFOP = ����������.���������������������������������();

            #region Create Entries and nalogs on FOP
            while (����������.Method("���������������").ToBool())
            {
                Trace.WriteLine("��������� - " + ����������.Property("Description").ToString().Trim());

                // ��������� ���������� ��� ����������
                if (!��������������.Method("SelectPeriodByObject", ����������.Method("CurrentItem"), �����������).ToBool())
                    continue;

                SotrFOP ��������� = new SotrFOP();
                ���������.INN = ����������.Property("��������������").Property("���").ToString();
                while (��������������.Method("GetRecord").ToBool()) // ������� �������� �� ������ ����������
                {
                    decimal rez = (decimal)��������������.Property("���������");
                    if (rez == 0) continue; // ��������� �������
                    if (��������.����������������(��������������)) continue; // ��������� ��������� �������
                    Trace.WriteLine("��� ������� - " + ��������������.Property("�������").Property("Code").ToString().Trim() + " �� ����� - " + rez);
                    prov.Add(����������.Method("CurrentItem"), ��������������, rez);
                    ���������.Add(��������������, rez, NalogsFOP);
                }
                ���������.GenEntries(ref prov, ����������, NalogsFOP);  // ����������� �������� ��� ������� ���
                Trace.WriteLine("�������� - " + prov.Item.Count.ToString());
                Application.DoEvents();
            }
            #endregion
            Trace.WriteLine("�������� - " + prov.Item.Count.ToString());

            #region Generate Entries

            OLE Operation = CreateOperation();
            OLE DOC = GenDoc231();

            int WriteCount = 0;
            foreach (�������� provodka in prov.Item)
            {
                if (provodka.Debit == null) continue;
                if (provodka.Debit.Account == null) continue;
                if (provodka.Credit.Account == null) continue;
                // ���� 231
                if (provodka.Debit.Account.Property("CODE").ToString().Trim() == "231")
                {
                    SetLineDoc(ref DOC, provodka,WriteCount);
                    WriteCount++;
                    continue;
                }

                Operation.Method("�������������");
                Trace.WriteLine(WriteCount + " �������� - �(" +  provodka.Debit.Account.Property("���").ToString()+
                                ") K("+provodka.Credit.Account.Property("���").ToString() +
                                ") SUMM("+(double)provodka.Summ+")");

                Operation.Property("�����").Property("����", provodka.Debit.Account);
                int i;
                for (i = 0; i < provodka.Debit.Subconto.Length; i++)
                {
                    if (provodka.Debit.Subconto[i] != null)
                    {
                        Operation.Property("Debit").Method("Subconto", i + 1, provodka.Debit.Subconto[i]);
                    }
                }


                Operation.Property("������").Property("����", provodka.Credit.Account);
                for (i = 0; i < provodka.Credit.Subconto.Length; i++)
                {
                    if (provodka.Credit.Subconto[i] != null)
                    {
                        Operation.Property("Credit").Method("Subconto", i + 1, provodka.Credit.Subconto[i]);
                    }
                }

                Operation.Property("�����", (double)provodka.Summ);
                Operation.Property("�����������", provodka.Comment);
                Operation.Property("���������", CommonVariables.GetFirm(Operation) /* CommonVariables.BFirm.CurrentItem */);
                WriteCount++;
 
                if (WriteCount % 100 == 0)
                {
                    try
                    {
                        Operation.Method("��������");
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
                Operation.Method("��������");
                DOC.Method("��������");
                DOC.Method("��������");
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
            Trace.WriteLine("�������� ���� ����� ...");
            conf.StoreBase(DataBase);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Trace.WriteLine("��");
            Trace.WriteLine("������� �������� ��������.",Traces.Status);
            return true;
       }

        protected override void doInitialize()
        {
            base.doInitialize();
            TraceListner = new ListBoxTraceListener(listTrace);
            Trace.Listeners.Add(TraceListner);
            selectPeriod.Period = conf.�����������;
 
            cb�������������������������.Checked = conf.�������������������������;
            cb�������������������.Checked = conf.�������������������;
            cb��������������������.Checked = conf.��������������������;
            �������������������.Checked = conf.�������������������;
            NalogFOP.�������������������������� = conf.�������������������;
        }

        protected override void doSaveParams()
        {
            base.doSaveParams();
            conf.����������� = selectPeriod.Period;
            conf.������������������������� = cb�������������������������.Checked;
            conf.������������������� = cb�������������������.Checked;
            conf.�������������������� = cb��������������������.Checked;
            conf.������������������� = NalogFOP.��������������������������;
        }

        protected override void doDeInitialize()
        {
            Trace.Listeners.Remove(TraceListner);
            base.doDeInitialize();
        }

        #region ACTIONS
        private void cb�������������������������_CheckedChanged(object sender, EventArgs e)
        {
            conf.������������������������� = cb�������������������������.Checked;
            conf.������������������� = cb�������������������.Checked;
            conf.�������������������� = cb��������������������.Checked;
            NalogFOP.�������������������������� = �������������������.Checked;
        }
        #endregion

 
 

    }



}
