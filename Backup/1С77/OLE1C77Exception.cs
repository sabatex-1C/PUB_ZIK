using System;
using System.Collections.Generic;
using System.Text;

namespace NTICS.OLE1C77
{
    class OLE1C77Exception : Exception
    {
        public OLE1C77Exception(){}
        public OLE1C77Exception(string message) : base(message) { }
    }
}
