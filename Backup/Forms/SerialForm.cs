using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NTICS;

namespace NTICS.Forms
{
    public partial class SerialForm : UserControl
    {
        bool Lock= false;
        public delegate void SerialComplete(string Serial);
        public event SerialComplete SerialCompleted;
        public SerialForm()
        {
            InitializeComponent();
        }
        private bool CheckSerial(string Value)
        {
            try
            {
                uint test = ntics_security.StringToUInt(Value);
                return true;
            }
            catch 
            {
                return false;
            }
        }
        private void CheckBlocks()
        {
            if (CheckSerial(serial1.Text)) L1.ForeColor = Color.Green; else L1.ForeColor = Color.Black;
            if (CheckSerial(serial2.Text)) L2.ForeColor = Color.Green; else L2.ForeColor = Color.Black;
            if (CheckSerial(serial3.Text)) L3.ForeColor = Color.Green; else L3.ForeColor = Color.Black;
            if (CheckSerial(serial4.Text)) L4.ForeColor = Color.Green; else L4.ForeColor = Color.Black;
            if (CheckSerial(serial5.Text)) L5.ForeColor = Color.Green; else L5.ForeColor = Color.Black;
            if (CheckSerial(serial6.Text)) L6.ForeColor = Color.Green; else L6.ForeColor = Color.Black;
            if (CheckSerial(serial7.Text)) L7.ForeColor = Color.Green; else L7.ForeColor = Color.Black;
            if (CheckSerial(serial8.Text)) L8.ForeColor = Color.Green; else L8.ForeColor = Color.Black;
            if (CheckSerial(serial9.Text)) L9.ForeColor = Color.Green; else L9.ForeColor = Color.Black;
            if (CheckSerial(serial10.Text)) L10.ForeColor = Color.Green; else L10.ForeColor = Color.Black;
            if (CheckSerial(serial11.Text)) L11.ForeColor = Color.Green; else L11.ForeColor = Color.Black;
            if (CheckSerial(serial12.Text)) L12.ForeColor = Color.Green; else L12.ForeColor = Color.Black;
        }
        private void TextChanged(object sender, EventArgs e)
        {
            if (!Lock)
            {
                CheckBlocks();
                Lock = true;
                serial_line.Text = serial1.Text + "-" + serial2.Text + "-" + serial3.Text + "-" + serial4.Text + "-" +
                                   serial5.Text + "-" + serial6.Text + "-" + serial7.Text + "-" + serial8.Text + "-" +
                                   serial9.Text + "-" + serial10.Text + "-" + serial11.Text + "-" + serial12.Text;
                     if (SerialCompleted != null)
                    {
                        SerialCompleted(serial_line.Text);
                    }
                 Lock = false;
            }
        }

        private string GetKeyToken(string str, uint num)
        {
            int i = 1;
            int pos = 0;
            while (i < num)
            {
                pos = str.IndexOf('-',pos)+1;
                if (pos == -1) break;
                i++;
            } 
            if (pos == -1) return "";
            if (str.IndexOf('-',pos) == -1) return str.Substring(pos);
            return str.Substring(pos,str.IndexOf('-',pos) - pos);
        }
        private void serial_line_TextChanged(object sender, EventArgs e)
        {
            if (!Lock)
            {
                Lock = true;
                serial1.Text = GetKeyToken(serial_line.Text, 1);
                serial2.Text = GetKeyToken(serial_line.Text, 2);
                serial3.Text = GetKeyToken(serial_line.Text, 3);
                serial4.Text = GetKeyToken(serial_line.Text, 4);
                serial5.Text = GetKeyToken(serial_line.Text, 5);
                serial6.Text = GetKeyToken(serial_line.Text, 6);
                serial7.Text = GetKeyToken(serial_line.Text, 7);
                serial8.Text = GetKeyToken(serial_line.Text, 8);
                serial9.Text = GetKeyToken(serial_line.Text, 9);
                serial10.Text = GetKeyToken(serial_line.Text, 10);
                serial11.Text = GetKeyToken(serial_line.Text, 11);
                serial12.Text = GetKeyToken(serial_line.Text, 12);
                     if (SerialCompleted != null)
                    {
                        SerialCompleted(serial_line.Text);
                    }

                Lock = false;
                CheckBlocks();
            }
        }
        public string Serial
        {
            get { return serial_line.Text; }
            set { serial_line.Text = value; }
        }
 

    }
}
