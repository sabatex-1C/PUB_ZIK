namespace PUB_ZIK
{
    partial class MainFRM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bt_Execute = new System.Windows.Forms.Button();
            this.pnLeft = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.bt_ImportEntries = new System.Windows.Forms.Button();
            this.bt_Sinchro_References = new System.Windows.Forms.Button();
            this.bt_Connection = new System.Windows.Forms.Button();
            this.main_panel = new System.Windows.Forms.Panel();
            this.statusStrip.SuspendLayout();
            this.pnLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.statusStrip.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusInfo,
            this.tsProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 424);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(688, 22);
            this.statusStrip.TabIndex = 9;
            // 
            // StatusInfo
            // 
            this.StatusInfo.AutoSize = false;
            this.StatusInfo.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.StatusInfo.Name = "StatusInfo";
            this.StatusInfo.Size = new System.Drawing.Size(471, 17);
            this.StatusInfo.Spring = true;
            this.StatusInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsProgressBar
            // 
            this.tsProgressBar.AutoSize = false;
            this.tsProgressBar.Name = "tsProgressBar";
            this.tsProgressBar.RightToLeftLayout = true;
            this.tsProgressBar.Size = new System.Drawing.Size(200, 16);
            // 
            // bt_Execute
            // 
            this.bt_Execute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Execute.Image = global::PUB_ZIK.Properties.Resources.applications_241;
            this.bt_Execute.Location = new System.Drawing.Point(0, 394);
            this.bt_Execute.Name = "bt_Execute";
            this.bt_Execute.Size = new System.Drawing.Size(128, 30);
            this.bt_Execute.TabIndex = 10;
            this.toolTip1.SetToolTip(this.bt_Execute, "Исполнить операцию");
            this.bt_Execute.UseVisualStyleBackColor = true;
            this.bt_Execute.Click += new System.EventHandler(this.bt_Execute_Click);
            // 
            // pnLeft
            // 
            this.pnLeft.Controls.Add(this.bt_Execute);
            this.pnLeft.Controls.Add(this.button5);
            this.pnLeft.Controls.Add(this.bt_ImportEntries);
            this.pnLeft.Controls.Add(this.bt_Sinchro_References);
            this.pnLeft.Controls.Add(this.bt_Connection);
            this.pnLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnLeft.Location = new System.Drawing.Point(0, 0);
            this.pnLeft.Name = "pnLeft";
            this.pnLeft.Size = new System.Drawing.Size(128, 424);
            this.pnLeft.TabIndex = 13;
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Top;
            this.button5.Image = global::PUB_ZIK.Properties.Resources.ntics_software;
            this.button5.Location = new System.Drawing.Point(0, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(128, 45);
            this.button5.TabIndex = 9;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // bt_ImportEntries
            // 
            this.bt_ImportEntries.Location = new System.Drawing.Point(5, 145);
            this.bt_ImportEntries.Name = "bt_ImportEntries";
            this.bt_ImportEntries.Size = new System.Drawing.Size(117, 38);
            this.bt_ImportEntries.TabIndex = 7;
            this.bt_ImportEntries.Text = "Импорт проводок";
            this.bt_ImportEntries.UseVisualStyleBackColor = true;
            this.bt_ImportEntries.Click += new System.EventHandler(this.bt_ImportEntries_Click);
            // 
            // bt_Sinchro_References
            // 
            this.bt_Sinchro_References.Location = new System.Drawing.Point(5, 102);
            this.bt_Sinchro_References.Name = "bt_Sinchro_References";
            this.bt_Sinchro_References.Size = new System.Drawing.Size(117, 38);
            this.bt_Sinchro_References.TabIndex = 6;
            this.bt_Sinchro_References.Text = "Синхронизация Справочников";
            this.bt_Sinchro_References.UseVisualStyleBackColor = true;
            this.bt_Sinchro_References.Click += new System.EventHandler(this.button3_Click);
            // 
            // bt_Connection
            // 
            this.bt_Connection.Location = new System.Drawing.Point(5, 60);
            this.bt_Connection.Name = "bt_Connection";
            this.bt_Connection.Size = new System.Drawing.Size(117, 38);
            this.bt_Connection.TabIndex = 0;
            this.bt_Connection.Text = "Соединения";
            this.bt_Connection.UseVisualStyleBackColor = true;
            this.bt_Connection.Click += new System.EventHandler(this.button1_Click);
            // 
            // main_panel
            // 
            this.main_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_panel.Location = new System.Drawing.Point(128, 0);
            this.main_panel.Name = "main_panel";
            this.main_panel.Size = new System.Drawing.Size(560, 424);
            this.main_panel.TabIndex = 14;
            // 
            // MainFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 446);
            this.Controls.Add(this.main_panel);
            this.Controls.Add(this.pnLeft);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainFRM";
            this.Text = "Синхронизация Расчёта с Бухгалтерией";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.pnLeft.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel StatusInfo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pnLeft;
        private System.Windows.Forms.Panel main_panel;
        private System.Windows.Forms.Button bt_Connection;
        private System.Windows.Forms.Button bt_ImportEntries;
        private System.Windows.Forms.Button bt_Sinchro_References;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button bt_Execute;
    }
}

