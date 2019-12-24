using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Data;

namespace NTICS_1C77
{
    struct BASE_PATCH_1C77
    {
        string Title;
        string Patch;
        string UserName;
        string Password;
        bool AutoLogin;
    }
    class NTICS_1C77
    {
        SortedList<string, string> RegisteredBases;
        string DataBasePatch;
        string BINPatch;
        public NTICS_1C77()
        {
            string[] bases = Registry.CurrentUser.OpenSubKey("Software\\1C\\1Cv7\\7.7\\Titles").GetValueNames();
            RegisteredBases = new SortedList<string,string>();
            foreach (string s in bases)
            {
                string value = (string)(Registry.CurrentUser.OpenSubKey("Software\\1C\\1Cv7\\7.7\\Titles").GetValue(s));
                RegisteredBases.Add(value, s);
            }
            DataSet ds = new DataSet();


        }


    }
}
