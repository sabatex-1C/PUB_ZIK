namespace PUB_ZIK
{
    partial class TransferOperation
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.selectPeriod = new NTICS.Frames.SelectPeriod();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ОбеспечениеОтпусков = new System.Windows.Forms.CheckBox();
            this.cbАналитикаПоПериодам = new System.Windows.Forms.CheckBox();
            this.cbБезАналитикиПоСотрудникам = new System.Windows.Forms.CheckBox();
            this.cbВыгружатьСотрудников = new System.Windows.Forms.CheckBox();
            this.listTrace = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(526, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Генерация проводок";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // selectPeriod
            // 
            this.selectPeriod.Location = new System.Drawing.Point(132, 30);
            this.selectPeriod.Name = "selectPeriod";
            this.selectPeriod.Period = new System.DateTime(2006, 11, 13, 9, 24, 9, 294);
            this.selectPeriod.Size = new System.Drawing.Size(253, 31);
            this.selectPeriod.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ОбеспечениеОтпусков);
            this.groupBox1.Controls.Add(this.cbАналитикаПоПериодам);
            this.groupBox1.Controls.Add(this.cbБезАналитикиПоСотрудникам);
            this.groupBox1.Controls.Add(this.cbВыгружатьСотрудников);
            this.groupBox1.Location = new System.Drawing.Point(10, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(502, 116);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выберите параметры аналитического учета:";
            // 
            // ОбеспечениеОтпусков
            // 
            this.ОбеспечениеОтпусков.AutoSize = true;
            this.ОбеспечениеОтпусков.Location = new System.Drawing.Point(6, 88);
            this.ОбеспечениеОтпусков.Name = "ОбеспечениеОтпусков";
            this.ОбеспечениеОтпусков.Size = new System.Drawing.Size(216, 17);
            this.ОбеспечениеОтпусков.TabIndex = 8;
            this.ОбеспечениеОтпусков.Text = "Использовать обеспечение отпусков";
            this.ОбеспечениеОтпусков.UseVisualStyleBackColor = true;
            this.ОбеспечениеОтпусков.CheckedChanged += new System.EventHandler(this.cbБезАналитикиПоСотрудникам_CheckedChanged);
            // 
            // cbАналитикаПоПериодам
            // 
            this.cbАналитикаПоПериодам.AutoSize = true;
            this.cbАналитикаПоПериодам.Location = new System.Drawing.Point(6, 19);
            this.cbАналитикаПоПериодам.Name = "cbАналитикаПоПериодам";
            this.cbАналитикаПоПериодам.Size = new System.Drawing.Size(181, 17);
            this.cbАналитикаПоПериодам.TabIndex = 6;
            this.cbАналитикаПоПериодам.Text = "С аналитикой по дате периода";
            this.cbАналитикаПоПериодам.UseVisualStyleBackColor = true;
            this.cbАналитикаПоПериодам.CheckedChanged += new System.EventHandler(this.cbБезАналитикиПоСотрудникам_CheckedChanged);
            // 
            // cbБезАналитикиПоСотрудникам
            // 
            this.cbБезАналитикиПоСотрудникам.AutoSize = true;
            this.cbБезАналитикиПоСотрудникам.Location = new System.Drawing.Point(6, 42);
            this.cbБезАналитикиПоСотрудникам.Name = "cbБезАналитикиПоСотрудникам";
            this.cbБезАналитикиПоСотрудникам.Size = new System.Drawing.Size(185, 17);
            this.cbБезАналитикиПоСотрудникам.TabIndex = 5;
            this.cbБезАналитикиПоСотрудникам.Text = "Без аналитики по сотрудникам";
            this.cbБезАналитикиПоСотрудникам.UseVisualStyleBackColor = true;
            this.cbБезАналитикиПоСотрудникам.CheckedChanged += new System.EventHandler(this.cbБезАналитикиПоСотрудникам_CheckedChanged);
            // 
            // cbВыгружатьСотрудников
            // 
            this.cbВыгружатьСотрудников.AutoSize = true;
            this.cbВыгружатьСотрудников.Location = new System.Drawing.Point(6, 65);
            this.cbВыгружатьСотрудников.Name = "cbВыгружатьСотрудников";
            this.cbВыгружатьСотрудников.Size = new System.Drawing.Size(198, 17);
            this.cbВыгружатьСотрудников.TabIndex = 7;
            this.cbВыгружатьСотрудников.Text = "Выгружать данные о сотрудниках";
            this.cbВыгружатьСотрудников.UseVisualStyleBackColor = true;
            this.cbВыгружатьСотрудников.CheckedChanged += new System.EventHandler(this.cbБезАналитикиПоСотрудникам_CheckedChanged);
            // 
            // listTrace
            // 
            this.listTrace.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listTrace.FormattingEnabled = true;
            this.listTrace.Location = new System.Drawing.Point(0, 192);
            this.listTrace.Name = "listTrace";
            this.listTrace.Size = new System.Drawing.Size(526, 212);
            this.listTrace.TabIndex = 14;
            // 
            // TransferOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listTrace);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.selectPeriod);
            this.Controls.Add(this.label1);
            this.Name = "TransferOperation";
            this.Size = new System.Drawing.Size(526, 404);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private NTICS.Frames.SelectPeriod selectPeriod;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbАналитикаПоПериодам;
        private System.Windows.Forms.CheckBox cbБезАналитикиПоСотрудникам;
        private System.Windows.Forms.CheckBox cbВыгружатьСотрудников;
        private System.Windows.Forms.CheckBox ОбеспечениеОтпусков;
        private System.Windows.Forms.ListBox listTrace;
    }
}
