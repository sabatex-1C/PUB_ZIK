using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NTICS.OLE1C77;

namespace PUB_ZIK
{
    public partial class SinchroReferences : CommonControl
    {

        void IncCount(Label Count)
        {
            int i = int.Parse(Count.Text);
            i++;
            Count.Text = i.ToString();
            Count.Refresh();
        }

        void IncInsertCounter()
        {
        }

        void IncDeleteCounter()
        {
        }

        void Sinchro(OLE refmain,OLE refchild)
        {
        #region INSERT ELEMENTS
            List<MEMReferences> MEMRef;
            bool FullCod = (refmain.EvalExpr("����������.����������(\"" + refmain.Method("���").ToString() + "\").����������").ToString().Trim().ToUpper() == ("�������������������").ToUpper());
            if (FullCod)
            {
                MEMRef= MEMReferences.CopyOLERefToMEMRef(refmain, "FullCode",FullCod,true,true);
            }
            else
            {
                MEMRef= MEMReferences.CopyOLERefToMEMRef(refmain, "Code",FullCod,true,true);
            }

            // Count elements
            CommonVariables.ProgressBarInitialize(0,MEMRef.Count,0);
            //refmain.Method("���������������");
            foreach (MEMReferences MRef in MEMRef)
            {
                if (!(bool)refchild.Method("����������������", "���", MRef.INDEX, 0))
                {
                    IncInsertCounter();
                    refchild.Method("New");
                    refchild.Property("Description", MRef.DESCRIPTION);
                    refchild.Property("���", MRef.INDEX);
                    refchild.Method("Write");
                    System.Diagnostics.Trace.WriteLine("�������� ������� - " + MRef.INDEX + " " + MRef.DESCRIPTION);
                }
                else
                {
                    if ((bool)refchild.Method("DeleteMark"))
                    {
                        refchild.Method("ClearDeleteMark");
                        //refchild.Method("Write");
                    }
                }

                CommonVariables.ProgressBarInc();
            }
        #endregion
        #region DELEDE ELEMENTS
            // Count elements
            int refchildCount = 0;
            refchild.Method("���������������");
            while (refchild.Method("���������������").ToBool())
            {
                if ((bool)refchild.Method("DeleteMark")) continue;
                refchildCount++;
            }

            CommonVariables.ProgressBarInitialize(0,refchildCount,0);
            refchild.Method("���������������");
            MEMRef.Sort();
            while (refchild.Method("���������������").ToBool())
            {
                if ((bool)refchild.Method("DeleteMark")) continue;
                CommonVariables.ProgressBarInc();
                MEMReferences mr = new MEMReferences();
                mr.INDEX = refchild.Property("���").ToString().Trim();

                if (MEMRef.BinarySearch(mr) < 0)
                {
                    IncDeleteCounter();
                    refchild.Method("Delete", 0);
                    refchild.Method("Write");
                    System.Diagnostics.Trace.WriteLine("������� � �������� ������� - " +
                                    refchild.Property("Code").ToString() + " "+
                                    refchild.Property("Description").ToString());
                }

                
            }
         #endregion
        }

        public SinchroReferences()
        {
            InitializeComponent();
            Executabled = ExecuteStatus.Yes;
        }


        #region PROTECTED
        protected override bool doExecute()
        {
            OLE ZIKReferences = V7Z.CreateObject("����������.������������");
            OLE ZIKRefChild = V7Z.CreateObject("����������.������������������");
            #region �����������������
            OLE PUBRefMain = V7B.CreateObject("����������.�����������������");
            PUBRefMain.Method("���������������������", CommonVariables.GetFirm(V7B) /* CommonVariables.BFirm.CurrentItem */);

            if ((double)ZIKReferences.Method("����������������", (object)"�������", (object)"�����������������", (object)1) != 1.0)
            {
                MessageBox.Show("�� ���� ����� � ��� �������� �����������������", "������ ������� ����� !!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            ZIKRefChild.Method("���������������������", ZIKReferences.Method("��������������"));
            System.Diagnostics.Trace.WriteLine("������������ ���������� �����������������");
            Sinchro(PUBRefMain, ZIKRefChild);
            cb����������������.Checked = true;
            #endregion
            #region �����������
            PUBRefMain = V7B.CreateObject("����������.�����������");
            if ((double)ZIKReferences.Method("����������������", (object)"�������", (object)"�����������", (object)1) != 1.0)
            {
                MessageBox.Show("�� ���� ����� � ��� �������� �����������", "������ ������� ����� !!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            ZIKRefChild.Method("���������������������", ZIKReferences.Method("��������������"));
            System.Diagnostics.Trace.WriteLine("������������ ���������� �����������");
            Sinchro(PUBRefMain, ZIKRefChild);
            cb�����������.Checked = true;
            #endregion
            #region �����������
            PUBRefMain = V7B.CreateObject("����������.�����������");
            if ((double)ZIKReferences.Method("����������������", (object)"�������", (object)"�����������", (object)1) != 1.0)
            {
                MessageBox.Show("�� ���� ����� � ��� �������� �����������", "������ ������� ����� !!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            ZIKRefChild.Method("���������������������", ZIKReferences.Method("��������������"));
            System.Diagnostics.Trace.WriteLine("������������ ���������� �����������");
            Sinchro(PUBRefMain, ZIKRefChild);
            cb�����������.Checked = true;
            #endregion
            #region ����������
            PUBRefMain = V7B.CreateObject("����������.����������");
            if ((double)PUBRefMain.Method("FindByDescr", (object)"��������") == 0)
            {
                MessageBox.Show("�� ���� ����� � ���������� ���������� ��� ����� ��������", "������ ������� ����� !!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            object zarp = PUBRefMain.Method("CurrentItem");
            PUBRefMain.Method("UseParent", zarp, 0);
            if ((double)ZIKReferences.Method("����������������", (object)"�������", (object)"����������", (object)1) == 0)
            {
                MessageBox.Show("�� ���� ����� � ��� �������� ����������", "������ ������� ����� !!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            ZIKRefChild.Method("���������������������", ZIKReferences.Method("��������������"));
            System.Diagnostics.Trace.WriteLine("������������ ���������� ����������");
            Sinchro(PUBRefMain, ZIKRefChild);
            cb����������.Checked = true;
            #endregion
            CommonVariables.ProgressBarHide();
            return true;
        }
    
        #endregion
    }
}
