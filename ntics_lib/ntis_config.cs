using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Data;

namespace ntics_lib
{
    public class ntics_config
    {
        public static bool AutoSaveConfig = true;
        
        DS_config ds;
        DataTable dt;
        string MyDocumentsFolder;
        string ConfigFileName;
        string LogFileName;
        TextWriterTraceListener LogFile;


        public ntics_config()
        {
            ds = new DS_config();
            MyDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" +
                                Application.CompanyName + "\\" + Application.ProductName + "\\" +
                                Application.ProductVersion + "\\";
            ConfigFileName = MyDocumentsFolder + "CONFIG.XML";
            LogFileName = MyDocumentsFolder + "ApplicationLog.txt";
            if (File.Exists(ConfigFileName)) ds.ReadXml(ConfigFileName);
            dt = ds.Tables["CONFIG"];
            LogFile = new TextWriterTraceListener(LogFileName);
            Trace.Listeners.Add(LogFile);
        }

        public string GetAsString(string ParName, string def)
        {
            DataRow dr = dt.Rows.Find(ParName);
            if (dr != null) return (string)dr["Param_Value"];
            return def;
        }
        public string GetAsString(string ParName)
        {
            return GetAsString(ParName, "");
        }


        public void Set(string ParName, string Value)
        {
            DataRow dr = dt.Rows.Find(ParName);
            if (dr == null)
            {
                dr = dt.NewRow();
                dr["Param_Name"] = ParName;
                dt.Rows.Add(dr);
            }
            dr["Param_Value"] = Value;
            if (AutoSaveConfig) ds.WriteXml(ConfigFileName);
        }

 
    }
}
