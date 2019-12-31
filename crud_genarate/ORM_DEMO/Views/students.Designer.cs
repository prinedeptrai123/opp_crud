namespace ORM_DEMO.Views
{
    partial class students
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
			            this.itlabelid1 = new System.Windows.Forms.Label();
            this.ittextboxid1 = new System.Windows.Forms.TextBox();
            this.itlabelname2 = new System.Windows.Forms.Label();
            this.ittextboxname2 = new System.Windows.Forms.TextBox();
            this.itlabelschool3 = new System.Windows.Forms.Label();
            this.ittextboxschool3 = new System.Windows.Forms.TextBox();


            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
			
			//
			// add panel 
			//
			            this.panel1.Controls.Add(this.itlabelid1);
            this.panel1.Controls.Add(this.ittextboxid1);
            this.panel1.Controls.Add(this.itlabelname2);
            this.panel1.Controls.Add(this.ittextboxname2);
            this.panel1.Controls.Add(this.itlabelschool3);
            this.panel1.Controls.Add(this.ittextboxschool3);


            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(453, 177);
			// 385
            this.panel1.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(118, 132);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 33);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
        
           
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(234, 132);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 33);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;

			            // 
            // itlabelid1
            // 
            this.itlabelid1.AutoSize = true;
			this.itlabelid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.itlabelid1.Location = new System.Drawing.Point(35, 27);
			this.itlabelid1.Name = "itlabelid1";
			this.itlabelid1.Size = new System.Drawing.Size(46, 17);
			this.itlabelid1.TabIndex = 1;
			this.itlabelid1.Text = "id";


            // 
            // ittextboxid1
            // 
            this.ittextboxid1.Location = new System.Drawing.Point(118, 27);
			this.ittextboxid1.Name = "ittextboxid1";
			this.ittextboxid1.Size = new System.Drawing.Size(312, 20);
			this.ittextboxid1.TabIndex = 1;


            // 
            // itlabelname2
            // 
            this.itlabelname2.AutoSize = true;
			this.itlabelname2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.itlabelname2.Location = new System.Drawing.Point(35, 57);
			this.itlabelname2.Name = "itlabelname2";
			this.itlabelname2.Size = new System.Drawing.Size(46, 17);
			this.itlabelname2.TabIndex = 2;
			this.itlabelname2.Text = "name";


            // 
            // ittextboxname2
            // 
            this.ittextboxname2.Location = new System.Drawing.Point(118, 57);
			this.ittextboxname2.Name = "ittextboxname2";
			this.ittextboxname2.Size = new System.Drawing.Size(312, 20);
			this.ittextboxname2.TabIndex = 2;


            // 
            // itlabelschool3
            // 
            this.itlabelschool3.AutoSize = true;
			this.itlabelschool3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.itlabelschool3.Location = new System.Drawing.Point(35, 87);
			this.itlabelschool3.Name = "itlabelschool3";
			this.itlabelschool3.Size = new System.Drawing.Size(46, 17);
			this.itlabelschool3.TabIndex = 3;
			this.itlabelschool3.Text = "school";


            // 
            // ittextboxschool3
            // 
            this.ittextboxschool3.Location = new System.Drawing.Point(118, 87);
			this.ittextboxschool3.Name = "ittextboxschool3";
			this.ittextboxschool3.Size = new System.Drawing.Size(312, 20);
			this.ittextboxschool3.TabIndex = 3;



        
            // 
            // AddForm
            // 

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
			// 385
            this.ClientSize = new System.Drawing.Size(477, 207); 
            this.Controls.Add(this.panel1);
            this.Name = "AddForm";
            this.Text = "AddForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

		            private System.Windows.Forms.Label itlabelid1;
            private System.Windows.Forms.TextBox ittextboxid1;
            private System.Windows.Forms.Label itlabelname2;
            private System.Windows.Forms.TextBox ittextboxname2;
            private System.Windows.Forms.Label itlabelschool3;
            private System.Windows.Forms.TextBox ittextboxschool3;

    }
}
