using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace NTICS.OLE1C77
{
    public class RecordReference1C77
    {
        public string Code;
        public string Description;
        public bool IsDeleteMark;
        public bool IsGroup;
        public int Parrent; // —сылка на елемент
        public override string ToString()
        {
            return Code + "  " + Description;
        }        

    }

    public class Reference1C77
    {
        protected List<RecordReference1C77> RecRef;
        protected RecordReference1C77 RecRefType;
        protected OLE Reference;
        protected int FCurrentRecord;

        public Reference1C77(OLEConnection Connection,string RefName)
        {
            Reference = Connection.Global.CreateObject("—правочник."+RefName);
            Reference.Method("¬ыбратьЁлементы");
            FCurrentRecord = -1;
            RecRef = new List<RecordReference1C77>();
            while (Reference.Method("ѕолучитьЁлемент").ToBool())
            {
                FCurrentRecord++;
                doCreateRecord();
                doCopyRecord();
                RecRef.Add(RecRefType);
            }
            if (FCurrentRecord >= 0) { FCurrentRecord = 0; }

        }

        #region PROPERTY
        public List<RecordReference1C77> Items
        {
            get { return RecRef; }
        }
        public string Code
        {
            get { return RecRef[FCurrentRecord].Code; }
            set { RecRef[FCurrentRecord].Code = value; }
        }
        public string Description
        {
            get { return RecRef[FCurrentRecord].Description; }
            set { RecRef[FCurrentRecord].Description = value; }
        }
        public bool IsDeleteMark
        {
            get { return RecRef[FCurrentRecord].IsDeleteMark; }
        }
        public bool IsGroup
        {
            get { return RecRef[FCurrentRecord].IsGroup; }
        }
        public OLE CurrentItem
        {
            get { return GetItemByIndex(FCurrentRecord); }
        }

        #endregion

        #region METHODS
        public void CopyToComboBox(ref ComboBox cb)
        {
            cb.Items.Clear();
            foreach (RecordReference1C77 f in RecRef)
            {
                cb.Items.Add(f);
            }
        }
        public OLE GetItemByIndex(int Index)
        {
            if (Reference.Method("Ќайтипокоду", RecRef[Index].Code).ToBool())
            {
                return Reference.Method("CurrentItem");
            }
            return null;
        }
        public bool SetIndex(int Index)
        {
            if ((Index < Items.Count) && (Index >= 0))
            {
                FCurrentRecord = Index;
                return true;
            }
            return false;
        }

        #endregion

        protected virtual void doCreateRecord()
        {
            RecRefType = new RecordReference1C77();
        }
        protected virtual void doCopyRecord()
        {
            RecRefType.Code = Reference.Property("Code").ToString();
            RecRefType.Description = Reference.Property("Description").ToString();
            RecRefType.IsDeleteMark = Reference.Method("DeleteMark").ToBool();
            RecRefType.IsGroup = Reference.Method("IsGroup").ToBool();

        }


      }
}
