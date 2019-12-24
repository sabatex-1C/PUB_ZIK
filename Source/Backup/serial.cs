using System;
using System.Collections.Generic;
using System.Text;

namespace PUB_ZIK
{
    public class serials
    {
        public static string edrpou = "13596193";
        public static string[] Serials = {"13596193" /* XIP */};
        public static bool IsExist(string Serial)
        {
            foreach (string s in Serials)
            {
                if (s == Serial) { return true; }
            }
            return false;
        }
    }
}
