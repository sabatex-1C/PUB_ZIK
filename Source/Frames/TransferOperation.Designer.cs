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
            this.������������������� = new System.Windows.Forms.CheckBox();
            this.cb������������������� = new System.Windows.Forms.CheckBox();
            this.cb������������������������� = new System.Windows.Forms.CheckBox();
            this.cb�������������������� = new System.Windows.Forms.CheckBox();
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
            this.label1.Text = "��������� ��������";
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
            this.groupBox1.Controls.Add(this.�������������������);
            this.groupBox1.Controls.Add(this.cb�������������������);
            this.groupBox1.Controls.Add(this.cb�������������������������);
            this.groupBox1.Controls.Add(this.cb��������������������);
            this.groupBox1.Location = new System.Drawing.Point(10, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(502, 116);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "�������� ��������� �������������� �����:";
            // 
            // �������������������
            // 
            this.�������������������.AutoSize = true;
            this.�������������������.Location = new System.Drawing.Point(6, 88);
            this.�������������������.Name = "�������������������";
            this.�������������������.Size = new System.Drawing.Size(216, 17);
            this.�������������������.TabIndex = 8;
            this.�������������������.Text = "������������ ����������� ��������";
            this.�������������������.UseVisualStyleBackColor = true;
            this.�������������������.CheckedChanged += new System.EventHandler(this.cb�������������������������_CheckedChanged);
            // 
            // cb�������������������
            // 
            this.cb�������������������.AutoSize = true;
            this.cb�������������������.Location = new System.Drawing.Point(6, 19);
            this.cb�������������������.Name = "cb�������������������";
            this.cb�������������������.Size = new System.Drawing.Size(181, 17);
            this.cb�������������������.TabIndex = 6;
            this.cb�������������������.Text = "� ���������� �� ���� �������";
            this.cb�������������������.UseVisualStyleBackColor = true;
            this.cb�������������������.CheckedChanged += new System.EventHandler(this.cb�������������������������_CheckedChanged);
            // 
            // cb�������������������������
            // 
            this.cb�������������������������.AutoSize = true;
            this.cb�������������������������.Location = new System.Drawing.Point(6, 42);
            this.cb�������������������������.Name = "cb�������������������������";
            this.cb�������������������������.Size = new System.Drawing.Size(185, 17);
            this.cb�������������������������.TabIndex = 5;
            this.cb�������������������������.Text = "��� ��������� �� �����������";
            this.cb�������������������������.UseVisualStyleBackColor = true;
            this.cb�������������������������.CheckedChanged += new System.EventHandler(this.cb�������������������������_CheckedChanged);
            // 
            // cb��������������������
            // 
            this.cb��������������������.AutoSize = true;
            this.cb��������������������.Location = new System.Drawing.Point(6, 65);
            this.cb��������������������.Name = "cb��������������������";
            this.cb��������������������.Size = new System.Drawing.Size(198, 17);
            this.cb��������������������.TabIndex = 7;
            this.cb��������������������.Text = "��������� ������ � �����������";
            this.cb��������������������.UseVisualStyleBackColor = true;
            this.cb��������������������.CheckedChanged += new System.EventHandler(this.cb�������������������������_CheckedChanged);
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
        private System.Windows.Forms.CheckBox cb�������������������;
        private System.Windows.Forms.CheckBox cb�������������������������;
        private System.Windows.Forms.CheckBox cb��������������������;
        private System.Windows.Forms.CheckBox �������������������;
        private System.Windows.Forms.ListBox listTrace;
    }
}
