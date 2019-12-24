namespace PUB_ZIK
{
    partial class Connection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Connection));
            this.cbServerType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btSetConnectionZ = new System.Windows.Forms.Button();
            this.btConnectionB = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPUB_Patch = new System.Windows.Forms.ComboBox();
            this.cbZIK_Patch = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbServerType
            // 
            this.cbServerType.FormattingEnabled = true;
            this.cbServerType.Items.AddRange(new object[] {
            "1C7.7 независимый ключ",
            "1С7.7 зависимый ключ",
            "1С7.7 зависимый ключ, SQL версия",
            "1С7.7 зависимый ключ, локальная версия",
            "1С7.7 зависимый ключ, сетевая версия"});
            this.cbServerType.Location = new System.Drawing.Point(154, 46);
            this.cbServerType.Name = "cbServerType";
            this.cbServerType.Size = new System.Drawing.Size(255, 21);
            this.cbServerType.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(495, 23);
            this.label1.TabIndex = 32;
            this.label1.Text = "Настройка соединений";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Вид дистрибутива 1С7.7";
            // 
            // btSetConnectionZ
            // 
            this.btSetConnectionZ.Location = new System.Drawing.Point(415, 72);
            this.btSetConnectionZ.Name = "btSetConnectionZ";
            this.btSetConnectionZ.Size = new System.Drawing.Size(75, 23);
            this.btSetConnectionZ.TabIndex = 34;
            this.btSetConnectionZ.Text = "Соединить";
            this.btSetConnectionZ.UseVisualStyleBackColor = true;
            this.btSetConnectionZ.Click += new System.EventHandler(this.btSetConnectionZ_Click);
            // 
            // btConnectionB
            // 
            this.btConnectionB.Location = new System.Drawing.Point(415, 109);
            this.btConnectionB.Name = "btConnectionB";
            this.btConnectionB.Size = new System.Drawing.Size(75, 23);
            this.btConnectionB.TabIndex = 35;
            this.btConnectionB.Text = "Соединить";
            this.btConnectionB.UseVisualStyleBackColor = true;
            this.btConnectionB.Click += new System.EventHandler(this.btConnectionB_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Местоположение базы ПУБ";
            // 
            // cbPUB_Patch
            // 
            this.cbPUB_Patch.FormattingEnabled = true;
            this.cbPUB_Patch.Location = new System.Drawing.Point(154, 110);
            this.cbPUB_Patch.Name = "cbPUB_Patch";
            this.cbPUB_Patch.Size = new System.Drawing.Size(255, 21);
            this.cbPUB_Patch.TabIndex = 37;
            // 
            // cbZIK_Patch
            // 
            this.cbZIK_Patch.FormattingEnabled = true;
            this.cbZIK_Patch.Location = new System.Drawing.Point(154, 73);
            this.cbZIK_Patch.Name = "cbZIK_Patch";
            this.cbZIK_Patch.Size = new System.Drawing.Size(255, 21);
            this.cbZIK_Patch.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Местоположение базы ЗиК";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(3, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(487, 156);
            this.label5.TabIndex = 40;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // Connection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbZIK_Patch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbPUB_Patch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btConnectionB);
            this.Controls.Add(this.btSetConnectionZ);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbServerType);
            this.Name = "Connection";
            this.Size = new System.Drawing.Size(495, 379);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbServerType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btSetConnectionZ;
        private System.Windows.Forms.Button btConnectionB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPUB_Patch;
        private System.Windows.Forms.ComboBox cbZIK_Patch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}
