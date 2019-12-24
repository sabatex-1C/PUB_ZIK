using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace PUB_ZIK
{
    class ListBoxTraceListener:TraceListener
    {
        ListBox list;
        public ListBoxTraceListener(ListBox list)
        {
            this.list = list;
            list.Items.Clear();
        }
        public override void Write(string message)
        {
            string[] s = message.Split('\n');
            int i = 0;
            foreach (string str in s)
            {
                if (i == 0)
                {
                    string line = (string)(list.Items[list.Items.Count - 1]);
                    list.Items[list.Items.Count] = line + str;

                }
                else
                {
                    list.Items.Add(str);
                    list.SetSelected(list.Items.Count - 1, true);
                }
                i++;
            }
        }

        public override void WriteLine(string message)
        {
            string[] s = message.Split('\n');
            foreach (string str in s)
            {
                list.Items.Add(str);
            }
            list.SetSelected(list.Items.Count - 1, true);
        }

    }
}
