namespace crud_genarate
{
    partial class Main
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.cbxCatalog = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTestConnect = new System.Windows.Forms.Button();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtSQLServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.cbxCatalog);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnTestConnect);
            this.panel1.Controls.Add(this.txtPassWord);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtUserName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.txtSQLServer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 292);
            this.panel1.TabIndex = 0;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(345, 239);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(135, 41);
            this.btnNext.TabIndex = 11;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(405, 209);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 24);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // cbxCatalog
            // 
            this.cbxCatalog.FormattingEnabled = true;
            this.cbxCatalog.Location = new System.Drawing.Point(90, 209);
            this.cbxCatalog.Name = "cbxCatalog";
            this.cbxCatalog.Size = new System.Drawing.Size(299, 24);
            this.cbxCatalog.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "catalog";
            // 
            // btnTestConnect
            // 
            this.btnTestConnect.Location = new System.Drawing.Point(90, 143);
            this.btnTestConnect.Name = "btnTestConnect";
            this.btnTestConnect.Size = new System.Drawing.Size(390, 42);
            this.btnTestConnect.TabIndex = 7;
            this.btnTestConnect.Text = "Test Connection";
            this.btnTestConnect.UseVisualStyleBackColor = true;
            this.btnTestConnect.Click += new System.EventHandler(this.btnTestConnect_Click);
            // 
            // txtPassWord
            // 
            this.txtPassWord.Enabled = false;
            this.txtPassWord.Location = new System.Drawing.Point(90, 115);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Size = new System.Drawing.Size(390, 22);
            this.txtPassWord.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password";
            // 
            // txtUserName
            // 
            this.txtUserName.Enabled = false;
            this.txtUserName.Location = new System.Drawing.Point(90, 71);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(390, 22);
            this.txtUserName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "UserName";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(90, 41);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(186, 21);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Windows authentications";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtSQLServer
            // 
            this.txtSQLServer.Location = new System.Drawing.Point(90, 12);
            this.txtSQLServer.Name = "txtSQLServer";
            this.txtSQLServer.Size = new System.Drawing.Size(390, 22);
            this.txtSQLServer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "SQL Sever";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(22, 310);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(96, 38);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 351);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL GenerateCode";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSQLServer;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTestConnect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxCatalog;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnNext;
    }
}

