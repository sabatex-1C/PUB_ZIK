using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using NTICS.OLE1C77;
using System.Diagnostics;

namespace PUB_ZIK
{
    public enum TraceSwitch
    {
        
    }

    public class Traces
    {
        public static string Info = "Info";
        public static string Status = "Status";
    }
   
    
    public class CommonVariables 
    {

        public static OLEConnection ZConnection;
        public static OLEConnection BConnection;

        
        public static NTICS.Period �����������;
        public static bool AutoInsertUser;
        protected static ConfigVersion1C Version1C;
        public static ������������� �������������;
        public static List<MEMReferences> MEMPUBWoker;


        //��� ����� ����
        public static LogBrowser log = new LogBrowser();


        // ��� �������� ����
        public static ToolStripProgressBar ProgressBar = null;
        public static void ProgressBarInitialize(int Min, int Max, int Start)
        {
            ProgressBar.Minimum = Min;
            ProgressBar.Maximum = Max;
            ProgressBar.Value = Start;
            ProgressBar.Visible = true;
        }
        public static void ProgressBarInc()
        {
            ProgressBar.Value++;
            ProgressBar.Owner.Refresh();
        }
        public static void ProgressBarHide()
        {
            ProgressBar.Visible = false;
        }



        public static OLE V7Z
        {
            get { return ZConnection.Global; }
        }
        public static OLE V7B
        {
            get { return BConnection.Global; }
        }


        #region FUNCTION

        public static OLE ���������������������(string ������������)
        {
            OLE SotrZIK = GetReferenceElementZIK("����������", ������������);
            string TIN = SotrZIK.Property("��������������").Property("���").ToString().Trim();
            MEMReferences MR = new MEMReferences();
            MR.INDEX = TIN;
            int i = 0;
            OLE PUB = V7B.CreateObject("����������.����������");
            do
            {
                i = MEMPUBWoker.BinarySearch(MR);
                if (i < 0)
                {
                    // ��� ���������� � ���
                    MR.DESCRIPTION = SotrZIK.Property("Description").ToString().Trim();
                    if (AutoInsertUser)
                    {
                        MR.Reference = SotrZIK;
                        PUB.Method("New");
                        PUB.Property("���", MR.INDEX);
                        PUB.Property("Description", MR.DESCRIPTION);
                        PUB.Method("Write");
                        MR.CODE = PUB.Property("Code").ToString().Trim();
                        MR.FULLCODE = PUB.Method("FullCode").ToString().Trim();
                        MEMPUBWoker.Add(MR);
                        MEMPUBWoker.Sort();
                        Trace.WriteLine("������� ���������� � ����������� - " + MR.CODE + "  " + MR.DESCRIPTION + " ��� - " + MR.INDEX);
                        return PUB.Method("CurrentItem");
                    }
                    DialogResult result = MessageBox.Show("� 1�7.7 ��� ����������� ��������� " + MR.DESCRIPTION + " � ����� ��� - " + MR.INDEX +
                                    ". ��������� �� ���������� 1�7.7 � ������� ������ ������������, ����� ���� ������� ��.",
                                    "������ � ����������� ����������.",
                                    MessageBoxButtons.OKCancel);
                    if (result == DialogResult.Cancel)
                    {
                        return null;
                    }
                    CommonVariables.MEMPUBWoker = MEMReferences.CopyOLERefToMEMRef(PUB, "���");
                    MEMPUBWoker.Sort();
                }
            } while (i < 0);
            PUB.Method("FindByCode", MEMPUBWoker[i].CODE);
            return PUB.Method("CurrentItem");

        }
        public static OLE ���������������������_IHH(string TIN)
        {
            MEMReferences MR = new MEMReferences();
            MR.INDEX = TIN;
            int i = 0;
            OLE PUB = V7B.CreateObject("����������.����������");
            do
            {
                i = MEMPUBWoker.BinarySearch(MR);
                if (i < 0)
                {
                    // ��� ���������� � ���
                    //MR.DESCRIPTION = SotrZIK.Property("Description").ToString().Trim();
                    //if (AutoInsertUser)
                    //{
                    //    MR.Reference = SotrZIK;
                    //    PUB.Method("New");
                    //    PUB.Property("���", MR.INDEX);
                    //    PUB.Property("Description", MR.DESCRIPTION);
                    //    PUB.Method("Write");
                    //    MR.CODE = PUB.Property("Code").ToString().Trim();
                    //    MR.FULLCODE = PUB.Method("FullCode").ToString().Trim();
                    //    MEMPUBWoker.Add(MR);
                    //    MEMPUBWoker.Sort();
                    //    Trace.WriteLine("������� ���������� � ����������� - " + MR.CODE + "  " + MR.DESCRIPTION + " ��� - " + MR.INDEX);
                    //    return PUB.Method("CurrentItem");
                    //}
                    DialogResult result = MessageBox.Show("� 1�7.7 ��� ����������� ��������� " + MR.DESCRIPTION + " � ����� ��� - " + MR.INDEX +
                                    ". ��������� �� ���������� 1�7.7 � ������� ������ ������������, ����� ���� ������� ��.",
                                    "������ � ����������� ����������.",
                                    MessageBoxButtons.OKCancel);
                    if (result == DialogResult.Cancel)
                    {
                        return null;
                    }
                    CommonVariables.MEMPUBWoker = MEMReferences.CopyOLERefToMEMRef(PUB, "���");
                    MEMPUBWoker.Sort();
                }
            } while (i < 0);
            PUB.Method("FindByCode", MEMPUBWoker[i].CODE);
            return PUB.Method("CurrentItem");

        }


       
        public static OLE GetReferenceElementPUB(string ReferenceType, string Code)
        {
            OLE reference = V7B.CreateObject("����������." + ReferenceType);
            if (reference.Method("FindByCode", Code).ToBool()) { return reference.Method("CurrentItem"); }
            return null;
        }

        public static OLE GetReferenceElementZIK(string ReferenceType, string Code)
        {
            OLE reference = V7Z.CreateObject("����������." + ReferenceType);
            if (reference.Method("FindByCode", Code).ToBool()) { return reference.Method("CurrentItem"); }
            return null;
        }




        /// <summary>
        ///  �������� ������������������ ������� ����������� "�����"
        /// </summary>
        /// <param name="handle">������ �� �������� 1�7.7 </param>
        public static OLE GetFirm(OLE handle)
        {
            if (handle == null) { return null; }
            OLE temp = handle.Global.CreateObject("����������.�����");
            temp.Method("���������������");
            while (temp.Method("���������������").ToBool())
            {
                if (temp.Method("DeleteMark").ToBool()) continue;
                if (temp.Property("������").ToString().Trim() == conf.CommonAllConfig.������)
                {
                    return temp.Method("CurrentItem");
                }
            }
            System.Diagnostics.Trace.WriteLine("��������� �� ���������������� ��� ����� ����: " + handle.EvalExpr("SystemCaption()").ToString());
            temp.Dispose();
            return null;
        }

        #endregion



    }
}
