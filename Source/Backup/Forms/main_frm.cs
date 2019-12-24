using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using NTICS;
using NTICS.Forms;
using System.Diagnostics;

namespace PUB_ZIK
{
    public partial class MainFRM : Form
    {
        private CommonControl CurrentFrame;
 
        class TraceStatus : TraceListener
        {
            private ToolStripStatusLabel Status;

            public TraceStatus(ToolStripStatusLabel StatusLabel)
            {
                this.Status = StatusLabel;
            }
            public override void Write(string message)
            {
               Status.Text = Status.Text + message;
             }
            public override void Write(string message, string category)
            {
                if (category == Traces.Status)
                {
                    this.Write(message);
                }
            }
            public override void WriteLine(string message)
            {
                //Status.Text = message;
            }
            public override void WriteLine(string message, string category)
            {
                if (category == Traces.Status)
                {
                    Status.Text = message;
                }
            }
        }


        public MainFRM()
        {
            InitializeComponent();

            ntics_security pt = new ntics_security(); 
            //config.Initialize(); // инициализация

            CommonVariables.ProgressBar = tsProgressBar;
            tsProgressBar.Visible = false;

            TraceStatus ts = new TraceStatus(StatusInfo);
            Trace.Listeners.Add(ts);

            for (int i = 0; i < 5; i++)
            {
                Trace.WriteLine("");
            }
            Trace.WriteLine("START");
            Trace.WriteLine(DateTime.Now);


            ShowFrame(new Welkome());
            SetEnableButtons();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CurrentFrame.DeInitialize();
            CurrentFrame = null;
            GC.Collect();
            conf.SaveConfig();
            Trace.Flush();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
 
 

        private void bt_Execute_Click(object sender, EventArgs e)
        {
            bool result = CurrentFrame.Execute();
            SetEnableButtons();
        }

        void ShowFrame(CommonControl Frame)
        {
            HideFrames();
            if (CurrentFrame != null) CurrentFrame.DeInitialize();
            CurrentFrame = Frame;
            CurrentFrame.Initialize();
            CurrentFrame.Dock = DockStyle.Fill;
            main_panel.Controls.Add(CurrentFrame);
            CurrentFrame.Visible = true;
            SetEnableButtons();
            Refresh();
        }

        void SetEnableButtons()
        {
            bt_Execute.Enabled = (CurrentFrame.Executabled != ExecuteStatus.None);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            About About = new About(Assembly.GetEntryAssembly());
            About.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowFrame(new Connection());
        }
 
        private void HideFrames()
        {
            main_panel.Controls.Clear();
        }

 
        private void button3_Click(object sender, EventArgs e)
        {
            ShowFrame(new SinchroReferences());
        }

        private void bt_ImportEntries_Click(object sender, EventArgs e)
        {
            ShowFrame(new TransferOperation());
        }






    }


}