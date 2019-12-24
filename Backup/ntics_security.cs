using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace NTICS
{
     
    
    public class ntics_security
    {

        // ��������� ��������� ������ � ������
        // 0..3 - uint ������
        // 4    - ���� ������ �����
        // 5    - ����� ������ �����
        // 6..7 - ��� ������ �����
        // 8    - ���������� ������� �������� �����
        // 9    - (NTICS_1C_XML,Synchronizer,Tools1C)
        public static ulong BoolArrayToulong(bool[] bytes)
        {
            ulong Result = 0;
            int i = 0;
            foreach (bool bit in bytes)
            {
                if (bit) Result = Result | ((ulong)1 << i);
                i++;
            }
            return Result;
        }





        public enum Applications
        {
            NTICS_XML,
            Synchronizer,
            Tools1C
        }

        // �������������� 4 - ���� � �����
        public static uint GetUint(byte[] key, int index)
        {
            return (uint)key[index + 0] | ((uint)key[index + 1] << 8) | ((uint)key[index + 2] << 16) | ((uint)key[index + 3] << 24);
        }
        public static ushort GetUshort(byte[] key, int index)
        {
            return (ushort)((uint)key[index + 0] | ((uint)key[index + 1] << 8));
        }
        ulong GetUlong(byte[] key, int index)
        {
            return (ulong)GetUint(key, index) | ((ulong)GetUint(key, index + 4) << 32);
        }
        public static bool GetFlag(byte value, int index)
        {
            return ((value >> index) & 1) == 1;
        }


       
        static string FChar = "WERTYUPASDFGHJKLZXCVBNM123456789";

  
        
        
        
        
        public static uint[] ByteArrayToUInt(byte[] Array)
        {
            int size = Array.Length / 4;
            if (Array.Length % 4 > 0) size++;
            uint[] result = new uint[size];
            int j = 0;
            int i = 0;
            while (i < Array.Length)
            {
                result[j] = result[j] | ((uint)Array[i] << (i % 4) * 8);
                i++;           
                if (((i % 4) == 0) && (i != 0)) j++;
            }
            return result;
        }
        public static byte[] UIntArrayToByte(uint[] Array)
        {
            int size = Array.Length * 4;
            byte[] result = new byte[size];
            int j = 0;
            foreach (uint value in Array)
            {
                result[j]   = (byte)(value);
                result[j+1] = (byte)(value >> 8);
                result[j + 2] = (byte)(value >> 16);
                result[j + 3] = (byte)(value >> 24);
                j = j + 4;
            }
            return result;
        }
        // ����������� ����� ����� � ������ �������� ��� ����� ��������� CRC
        public static string IntToString(uint value)
        {
            string result = "";
            for (int i = 0; i < 6; i++)
            {
                result = result + FChar[(int)((value >> (i * 5)) & (uint)0x1f)];
            }
            // ������� CRC
            return result + FChar[(int)(((value >> (30)) & (uint)0x1f) | (crc3(value) << 2))];
        }
        // �������������� ������ �������� � ����� � ��������� CRC
        public static uint StringToUInt(string Value)
        {
            if ((Value.Length < 7) || (Value.Length % 7 != 0)) throw new Exception("������ �������������� ������");
            uint Result = 0;
            for (int i = 0; i < 6; i++)
            {
                Result = Result | ((uint)FChar.IndexOf(Value[i]) << (i*5));
            }
            // ������� CRC
            Result = Result | (((uint)FChar.IndexOf(Value[6]) << 30) & (uint)0xC0000000);
            // �������� CRC
            if (crc3(Result) != (((uint)FChar.IndexOf(Value[6]) >> 2) & (uint)0x00000007))
                throw new Exception("������ CRC");
            return Result;
        }
        // �� ������ ������� ����� ��������� 7 ��������
        // ����� ����������� ������ -
        public static string ArrayToString(uint[]  Value)
        {
            string s = "";
            for (int i = 0; i < Value.Length; i++)
            {
                if (i != 0) {s=s + "-";}
                s = s + IntToString(Value[i]);
            }
            return s;
        }
        // ���������� �������� ������ ���� ������ 7 (�� �������� ������������)
        public static uint[] StringToArray(string Value)
        {
            // ������� ����������� '-'
            string s="";
            foreach (char c in Value)
            {
                if (c != '-') {s = s + c;}
            }
            if ((s.Length < 7) || (s.Length % 7 != 0)) throw new Exception("");
            uint[] Result = new uint[s.Length / 7];
            for (int i=0; i <  Math.Min(Result.Length, s.Length / 7); i++)
            {
                Result[i] = StringToUInt(s.Substring(i*7,7));
            }
            return Result;
        }
        // ������ � ������ ����
        public static byte[] StringAsBytes(string str)
        {
            byte[] Result = new byte[str.Length];
            int i =0;
            foreach (char c in str)
            {
                Result[i] = (byte)c;
                i++;
            }
            return Result;
        }
        // ������ ���� � ������ (����� ��������� ��������)
        public static string BytesToString(byte[] value)
        {
            string s = "";
            foreach (byte b in value)
            {
                s = s + b.ToString() + ",";
            }
            return s;
        }
        // �������� ��� ���������� �:
        public static byte[] HardwareKey()
        {
            return (byte[])Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\MountedDevices", "\\DosDevices\\C:", null);
        }
        // �������� ��� ��� ���������� �:
        public static byte[] HashHardvare()
        {
            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1Managed();
            return sha.ComputeHash(HardwareKey());
        }
        public static uint crc3(uint value)
        {
            return value % 7;
        }



    }
}
