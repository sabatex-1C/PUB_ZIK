using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace NTICS.Forms
{
    public partial class TraceConsole : UserControl
    {
        public TraceConsole()
        {
            InitializeComponent();
        }
        public void Write(string message)
        {
            string[] s = message.Split('\n');
            int i = 0;
            foreach (string str in s)
            {
                if (i == 0)
                {
                    lbInfo.Items[lbInfo.Items.Count - 1] = lbInfo.Items[lbInfo.Items.Count - 1] + str;
                }
                else lbInfo.Items.Add(str);
                i++;
            }
        }

        public void WriteLine(string message)
        {
            string[] s = message.Split('\n');
            foreach (string str in s)
            {
                lbInfo.Items.Add(str);
            }
        }

    }

    public class TraceConsoleListner : TraceListener
    {
        private TraceConsole trace;

        public TraceConsoleListner()
        {
            trace = new TraceConsole();

        }
        
        public override void Write(string message)
        {
            trace.Write(message);
        }

        public override void WriteLine(string message)
        {
            trace.WriteLine(message);
        }
    }


}
