namespace NTICS
{
    partial class ConnectionDlg
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
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lPassWord = new System.Windows.Forms.TextBox();
            this.lUserName = new System.Windows.Forms.TextBox();
            this.lBasePatch = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lConnectionName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Пароль";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "Имя пользователя";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Местоположение базы";
            // 
            // lPassWord
            // 
            this.lPassWord.Location = new System.Drawing.Point(148, 77);
            this.lPassWord.Name = "lPassWord";
            this.lPassWord.PasswordChar = '*';
            this.lPassWord.Size = new System.Drawing.Size(261, 20);
            this.lPassWord.TabIndex = 39;
            // 
            // lUserName
            // 
            this.lUserName.Location = new System.Drawing.Point(148, 51);
            this.lUserName.Name = "lUserName";
            this.lUserName.Size = new System.Drawing.Size(261, 20);
            this.lUserName.TabIndex = 38;
            // 
            // lBasePatch
            // 
            this.lBasePatch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.lBasePatch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.lBasePatch.Location = new System.Drawing.Point(148, 25);
            this.lBasePatch.Name = "lBasePatch";
            this.lBasePatch.Size = new System.Drawing.Size(261, 20);
            this.lBasePatch.TabIndex = 37;
            // 
            // lConnectionName
            // 
            this.lConnectionName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lConnectionName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lConnectionName.Location = new System.Drawing.Point(0, 0);
            this.lConnectionName.Name = "lConnectionName";
            this.lConnectionName.Size = new System.Drawing.Size(412, 22);
            this.lConnectionName.TabIndex = 43;
            this.lConnectionName.Text = "Connection name";
            this.lConnectionName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConnectionDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lConnectionName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lPassWord);
            this.Controls.Add(this.lUserName);
            this.Controls.Add(this.lBasePatch);
            this.Name = "ConnectionDlg";
            this.Size = new System.Drawing.Size(412, 102);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lPassWord;
        private System.Windows.Forms.TextBox lUserName;
        private System.Windows.Forms.TextBox lBasePatch;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lConnectionName;
    }
}
