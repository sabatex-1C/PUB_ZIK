using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NTICS.Str
{
    public class NTICS_Str
    {
        public static string Parse(ref string value, char delimiter)
        {
            if (value.Trim().Length == 0) return "";
            string result = value.Substring(0, value.IndexOf(';')).Trim();
            value = value.Substring(value.IndexOf(';') + 1);
            return result;
        }
        public static string Parse(ref string value)
        {
            // Parse on default delimiter
            return Parse(ref value, ';');
        }
        public static string Parse(string value, char delimiter)
        {
            if (value.Trim().Length == 0) return "";
            return value.Substring(0, value.IndexOf(';')).Trim();
        }
        public static string Parse(string value)
        {
            // Parse on default delimiter
            return Parse(value, ';');
        }
        public static bool CheckString(string value, string check)
        {
            foreach (char c in value)
            {
                if (check.IndexOf(c) == -1) return false;
            }
            return true;
        }
 
    }
}
