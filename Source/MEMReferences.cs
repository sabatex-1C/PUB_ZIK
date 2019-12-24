using System;
using System.Collections.Generic;
using System.Text;
using NTICS.OLE1C77;
using System.Windows.Forms;

namespace PUB_ZIK
{
    public class MEMReferences : CommonVariables,IComparable
    {
        public string INDEX;
        public string CODE;
        public string DESCRIPTION;
        public string FULLCODE;
        public OLE Reference;
        #region IComparable Members

        public int CompareTo(object obj)
        {
            MEMReferences b = (MEMReferences)obj;
            return string.Compare(this.INDEX, b.INDEX);
        }

        public override string ToString()
        {
            return DESCRIPTION;
        }


        public static void InitialComboBox(List<MEMReferences> lmr, ComboBox cb)
        {
            cb.Items.Clear();
            foreach (MEMReferences mr in lmr)
            {
                cb.Items.Add(mr);
            }
        }
        public static List<MEMReferences> CopyOLERefToMEMRef(OLE OLERef, string Index, bool ISMethodIndex, bool NoDeleted, bool NoGroup)
        {
            List<MEMReferences> MEMRef = new List<MEMReferences>();
            OLERef.Method("¬ыбратьЁлементы");
            while (OLERef.Method("ѕолучитьЁлемент").ToBool())
            {
                if (NoDeleted)
                {
                    if (OLERef.Method("DeleteMark").ToBool()) continue;
                }

                if (NoGroup)
                {
                    if ((double)OLERef.Method("IsGroup") == 1.0) continue;
                }

                MEMReferences MR = new MEMReferences();
                try
                {
                    if (ISMethodIndex) 
                    {
                        MR.INDEX = OLERef.Method(Index).ToString().Trim();
                
                    }
                    else
                    {
                        MR.INDEX = OLERef.Property(Index).ToString().Trim();
                    }
                }
                catch
                {
                        throw new Exception("ќшибка копировани€ справочника");
                }
                
                MR.CODE = OLERef.Property("Code").ToString().Trim();
                MR.DESCRIPTION = OLERef.Property("Description").ToString().Trim();
                MR.Reference = OLERef.Method("CurrentItem");
                MR.FULLCODE = OLERef.Method("FullCode").ToString().Trim();
                MEMRef.Add(MR);
            }
            return MEMRef;
        }
        public static List<MEMReferences> CopyOLERefToMEMRef(OLE OLERef, string Index, bool ISMethodIndex, bool NoDeleted)
        {
            return CopyOLERefToMEMRef(OLERef, Index, ISMethodIndex, NoDeleted, false);
        }
        public static List<MEMReferences> CopyOLERefToMEMRef(OLE OLERef, string Index, bool ISMethodIndex)
        {
            return CopyOLERefToMEMRef(OLERef, Index, ISMethodIndex, false);
        }
        public static List<MEMReferences> CopyOLERefToMEMRef(OLE OLERef, string Index)
        {
            return CopyOLERefToMEMRef(OLERef, Index, false);
        }

    


        #endregion
    }


    public class MEMRefCompareFULLCode :CommonVariables,IComparer<MEMReferences>
    {
    #region IComparer<MEMReferences> Members

        public int  Compare(MEMReferences x, MEMReferences y)
        {
 	        return string.Compare(x.FULLCODE,y.FULLCODE);
        }

    #endregion
}
}
