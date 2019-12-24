using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Security.Cryptography;

namespace NTICS
{

    public abstract class ntics_base_config
    {
        protected DS_config ds;
        protected DataTable dt;
        protected string ConfigID;          // ��� ����������
        public bool AutoSaveConfig = true;  // ����������� ���������� ������������ ��� ������ ��������� ����������

        /// <summary>
        ///  �������� ����������� ���������� ��� ������
        /// </summary>
        /// <param name="ParName">������������ ����������� ���������� </param>
        /// <param name="def">�������� �� ���������</param>
        public string GetAsString(string ParName, string def)
        {
            DataRow dr = dt.Rows.Find(ParName);
            if (dr != null) return (string)dr["Param_Value"];
            return def;
        }
        /// <summary>
        ///  �������� ����������� ���������� ��� ������
        /// </summary>
        /// <param name="ParName">������������ ����������� ���������� </param>
        public string GetAsString(string ParName)
        {
            return GetAsString(ParName, "");
        }
        /// <summary>
        ///  ��������� ����������� ���������� ��� ������
        /// </summary>
        /// <param name="ParName">������������ ����������� ���������� </param>
        /// <param name="Value">�������� </param>
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
        /// <summary>
        ///  ��������� ����������������� ����
        /// </summary>
        /// <param name="ParName">������������ ����������� ���������� </param>
        /// <param name="def">�������� �� ���������</param>
        public void WriteConfig()
        {
            ds.WriteXml(ConfigFileName);
        }

        public abstract string ConfigFolder { get;  }
        protected string ConfigFileName    // ���� � ����� ������������  
        {
            get {return ConfigFolder + "CONFIG.XML"; }
        }

        void initialize()
        {
            ds = new DS_config();
            // ��������� ���� � ����� ������������
            if (!Directory.Exists(ConfigFolder)) Directory.CreateDirectory(ConfigFolder);
            if (File.Exists(ConfigFileName)) ds.ReadXml(ConfigFileName);
            dt = ds.Tables["CONFIG"];
        }
        public ntics_base_config()
        {
            initialize();
        }
        public ntics_base_config(string AppName)
        {
            ConfigID = AppName;
            initialize();
        }

     }

    public class ntics_CommonAllConfig : ntics_base_config
    {
        public override string ConfigFolder
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\NTICS\\"; }
        }
        byte[] SecretKey
        {
            get
            {
                // ��������� ������� ������
                RSACryptoServiceProvider RSA;
                RSA = new RSACryptoServiceProvider();
                RSA.FromXmlString("<RSAKeyValue><Modulus>7RXWNpO6yVx03AZ/eeJabvA0Of35a32jsBFiM+tV2T09Hevfl5pEnssDpKI8/wbP</Modulus><Exponent>AQAB</Exponent><P>+FS4QNrkEHAJB2xaLw2lMbl6TNMj8IeR</P><Q>9Gg1ym8VljXLwLhKsVUj2tzotjFPRThf</Q><DP>SdrQbaFsAKOBW+7Sp3nUboRuJhkJcEix</DP><DQ>EOD5hgdx6DPC5IZVsjV9CmpjL+Hr5Y3l</DQ><InverseQ>D56vb9fyeVkLIK2zhkfj523cf4GsFtOc</InverseQ><D>4I3TRAQuYXQxtwhsiwwKbZMTVG4qZFYs9e4yyxzyT3S6vKwj/88MOpGgJxNlQT/h</D></RSAKeyValue>");

                // ��������� ��������
                try
                {
                    return RSA.Decrypt(ntics_security.UIntArrayToByte(ntics_security.StringToArray(Serial)), false);
                }
                catch
                {
                    return new byte[20];
                }
            }
        }
        public string ������
        {
            get { return ntics_security.GetUint(SecretKey, 0).ToString().PadLeft(8, '0'); }
        }
        public DateTime ���������������
        {
            get
            {
                return DateTime.Parse(SecretKey[4].ToString().PadLeft(2, '0') + "." +
                     SecretKey[5].ToString().PadLeft(2, '0') + "." +
                     ntics_security.GetUshort(SecretKey, 6).ToString().PadLeft(4, '0'));
            }
        }
        public int ������������������
        {
            get { return (int)SecretKey[8]; }
        }
        // �������� ����
        public bool DateValidation(DateTime DateTest)
        {

            return DateTest <= ���������������.AddMonths(������������������);
        }
        public string Serial
        {
            get { return GetAsString("SERIAL", ""); }
            set { Set("SERIAL", value); }
        }

        public string Version_�����������
        {
            get { return GetAsString("Version_�����������", ""); }
            set { Set("Version_�����������", value); }
        }
 
        public bool NTICS_1C_XML
        {
            get { return ntics_security.GetFlag(SecretKey[9], 0); }
        }
        public string Version_NTICS_1C_XML
        {
            get { return GetAsString("Version_NTICS_1C_XML", ""); }
            set { Set("Version_NTICS_1C_XML", value); }
        }

        public bool Synchronizer
        {
            get { return ntics_security.GetFlag(SecretKey[9], 1); }
        }
        public string Version_Synchronizer
        {
            get { return GetAsString("Version_Synchronizer", ""); }
            set { Set("Version_Synchronizer", value); }
        }

        public bool Tools1C
        {
            get { return ntics_security.GetFlag(SecretKey[9], 2); }
        }
        public string Version_Tools1C
        {
            get { return GetAsString("Version_Tools1C", ""); }
            set { Set("Version_Tools1C", value); }
        }
       


    }

    public class ntics_CommonApplicationConfig : ntics_base_config
    {
        string LogFileName;
        TextWriterTraceListener LogFile;

        ntics_CommonApplicationConfig()
        {
        }
        public ntics_CommonApplicationConfig(string AppName):base(AppName)
        {
            LogFileName = ConfigFolder + "ApplicationLog.txt";
            LogFile = new TextWriterTraceListener(LogFileName);
            Trace.Listeners.Add(LogFile);
        }

        public override string ConfigFolder
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\NTICS\\" + ConfigID + "\\"; }
        }

    }

    public class ntics_PersonalUserConfig : ntics_base_config
    {

        ntics_PersonalUserConfig()
        {
        }

        public ntics_PersonalUserConfig(string AppName) : base(AppName)
        {
        }

        public override string ConfigFolder
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NTICS\\" + ConfigID + "\\"; }
        }

    }
}
