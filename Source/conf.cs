using System;
using System.Collections.Generic;
using System.Text;
using NTICS;
using System.Data;
using System.IO;



namespace PUB_ZIK
{
    class conf
    {
        public const string UserName = "NTICS";
        public const string PassWord = "NTICS";
        const string ApplicationName = "Synchronizer";
        public static ntics_CommonApplicationConfig CommonApplicationConfig = new ntics_CommonApplicationConfig(ApplicationName);
        public static ntics_CommonAllConfig CommonAllConfig = new ntics_CommonAllConfig();

        public static int CONNECTION_ServerType
        {
            get { return int.Parse(CommonApplicationConfig.GetAsString("CONNECTION.ServerType", "4")); }
            set { CommonApplicationConfig.Set("CONNECTION.ServerType", value.ToString()); }
        }
        public static string CONNECTION_BasePatchZ
        {
            get { return CommonApplicationConfig.GetAsString("CONNECTION.BasePatchZ", ""); }
            set { CommonApplicationConfig.Set("CONNECTION.BasePatchZ", value); }
        }
        public static string CONNECTION_BasePatchB
        {
            get { return CommonApplicationConfig.GetAsString("CONNECTION.BasePatchB", ""); }
            set { CommonApplicationConfig.Set("CONNECTION.BasePatchB", value); }
        }



        public static void StoreBase(Base dataset)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\NTICS\\SYNCHRONIZER\\DataBase.xml";
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\NTICS\\SYNCHRONIZER");
            }
            dataset.WriteXml(path);
        }
        public static void RestopeBase(ref Base dataset)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\NTICS\\SYNCHRONIZER\\DataBase.xml";
            if (File.Exists(path)) dataset.ReadXml(path);
        }




        static ntics_PersonalUserConfig PersonalUserConfig = new ntics_PersonalUserConfig(ApplicationName);
        public static DateTime ДатаПериода
        {
            get { return DateTime.Parse(PersonalUserConfig.GetAsString("ДатаПериода", DateTime.Now.ToShortDateString())); }
            set { PersonalUserConfig.Set("ДатаПериода", value.ToShortDateString()); }
        }
        public static bool БезАналитикиПоСотрудникам
        {
            get { return bool.Parse(PersonalUserConfig.GetAsString("БезАналитикиПоСотрудникам", "false")); }
            set { PersonalUserConfig.Set("БезАналитикиПоСотрудникам", value.ToString()); }
        }
        public static bool АналитикаПоПериодам
        {
            get { return bool.Parse(PersonalUserConfig.GetAsString("АналитикаПоПериодам", "false")); }
            set { PersonalUserConfig.Set("АналитикаПоПериодам", value.ToString()); }

        }
        public static bool ВыгружатьСотрудников
        {
            get { return bool.Parse(PersonalUserConfig.GetAsString("ВыгружатьСотрудников", "false")); }
            set { PersonalUserConfig.Set("ВыгружатьСотрудников", value.ToString()); }
        }
        public static bool ОбеспечениеОтпусков
        {
            get { return bool.Parse(PersonalUserConfig.GetAsString("ОбеспечениеОтпусков", "false")); }
            set { PersonalUserConfig.Set("ОбеспечениеОтпусков", value.ToString()); }
        }


        public static void SaveConfig()
        {
            PersonalUserConfig.WriteConfig();
        }

    }
}
