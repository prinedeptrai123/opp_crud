namespace ORM_DEMO.Views
{
    partial class ListViews
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
            this.lbDatabaseName = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            //this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            //this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            //this.linkLabel4 = new System.Windows.Forms.LinkLabel();
			            this.lblstudents = new System.Windows.Forms.LinkLabel();
            this.lbltestdata = new System.Windows.Forms.LinkLabel();


            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbDatabaseName);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(562, 68);
            this.panel1.TabIndex = 0;
            // 
            // lbDatabaseName
            // 
            this.lbDatabaseName.AutoSize = true;
            this.lbDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.lbDatabaseName.Location = new System.Drawing.Point(229, 13);
            this.lbDatabaseName.Name = "lbDatabaseName";
            this.lbDatabaseName.Size = new System.Drawing.Size(79, 29);
            this.lbDatabaseName.TabIndex = 0;
            this.lbDatabaseName.Text = "label1";

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label1.Location = new System.Drawing.Point(12, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "List Views";
			
			            // 
            // lblstudents
            // 
			this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.linkLabel1.Location = new System.Drawing.Point(46, 130);
            this.linkLabel1.Name = "lblstudents";
            this.linkLabel1.Size = new System.Drawing.Size(72, 17);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Models.students";


            // 
            // lbltestdata
            // 
			this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.linkLabel1.Location = new System.Drawing.Point(46, 160);
            this.linkLabel1.Name = "lbltestdata";
            this.linkLabel1.Size = new System.Drawing.Size(72, 17);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Models.testdata";


            // 
            // %LBNAME%
            // 
			this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.linkLabel1.Location = new System.Drawing.Point(46, %LBY%);
            this.linkLabel1.Name = "%LBNAME%";
            this.linkLabel1.Size = new System.Drawing.Size(72, 17);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "%TEXT%";




            // 
            // ListViews
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 190);
            //this.Controls.Add(this.linkLabel4);
            //this.Controls.Add(this.linkLabel3);
            //this.Controls.Add(this.linkLabel2);
			            this.Controls.Add(this.lblstudents);
            this.Controls.Add(this.lbltestdata);

            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "ListViews";
            this.Text = "ListViews";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbDatabaseName;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.LinkLabel linkLabel2;
        //private System.Windows.Forms.LinkLabel linkLabel3;
        //private System.Windows.Forms.LinkLabel linkLabel4;
		        private System.Windows.Forms.LinkLabel lblstudents;
        private System.Windows.Forms.LinkLabel lbltestdata;

    }
}
