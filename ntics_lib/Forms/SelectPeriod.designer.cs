namespace NTICS.Frames
{
    partial class SelectPeriod
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
            this.btBack = new System.Windows.Forms.Button();
            this.lPeriod = new System.Windows.Forms.Label();
            this.btNext = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btBack
            // 
            this.btBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.btBack.Image = global::NTICS.Properties.Resources.arrow_back_24;
            this.btBack.Location = new System.Drawing.Point(0, 0);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(29, 26);
            this.btBack.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btBack, "Предыдущий период");
            this.btBack.UseVisualStyleBackColor = true;
            this.btBack.Click += new System.EventHandler(this.btBack_Click);
            // 
            // lPeriod
            // 
            this.lPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lPeriod.Location = new System.Drawing.Point(29, 0);
            this.lPeriod.Name = "lPeriod";
            this.lPeriod.Size = new System.Drawing.Size(183, 26);
            this.lPeriod.TabIndex = 3;
            this.lPeriod.Text = "label2";
            this.lPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btNext
            // 
            this.btNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.btNext.Image = global::NTICS.Properties.Resources.arrow_forward_24;
            this.btNext.Location = new System.Drawing.Point(183, 0);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(29, 26);
            this.btNext.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btNext, "Следующий период");
            this.btNext.UseVisualStyleBackColor = true;
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // SelectPeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btNext);
            this.Controls.Add(this.lPeriod);
            this.Controls.Add(this.btBack);
            this.Name = "SelectPeriod";
            this.Size = new System.Drawing.Size(212, 26);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btBack;
        private System.Windows.Forms.Label lPeriod;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
