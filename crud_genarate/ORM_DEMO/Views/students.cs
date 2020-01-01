using framework_crud.ORM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ORM_DEMO.Views
{
    public partial class students : Form
    {
        private string connstring = "Data Source=DESKTOP-15SIF8Q\\SQLEXPRESS; database=oop;" +
            "Integrated Security=True;Connect Timeout=10";
        private MSSQLDatabase database;
        private BindingSource bd = new BindingSource();
        private bool isUpdate = false;

        public students(string conn)
        {
            this.connstring = conn;
            InitializeComponent();
            regisEvents();
            initial();
        }

        public students()
        {
            InitializeComponent();
            regisEvents();
            initial();
        }

        public students(Models.students entity)
        {
            InitializeComponent();
            regisEvents();
            initial(entity);
            isUpdate = true;
			this.Text = "UpdateForm";
        }

        private void initial(Models.students entity)
        {
			itnumericupdownid1.Text = entity.id;
			ittextboxname2.Text = entity.name;
			ittextboxschool3.Text = entity.school;

			  database = new MSSQLDatabase(new System.Data.SqlClient.SqlConnection(connstring));
        }

        private void initial()
        {
            // connect database
            database = new MSSQLDatabase(new System.Data.SqlClient.SqlConnection(connstring));
        }

        private void regisEvents()
        {
            btnCancel.Click += new System.EventHandler(btnCancel_Click);
            btnSave.Click += new System.EventHandler(btnSave_Click);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string id = itnumericupdownid1.Text;
                string name = ittextboxname2.Text;
                string school = ittextboxschool3.Text;

                // add
                if (!isUpdate)
                {
                    Models.students tmp = new  Models.students();
                    tmp.id = id;
                    tmp.name = name;
                    tmp.school = school;

                    database.Table(typeof(Models.students)).Insert(tmp);
                }
                // update
                else
                {
                    Models.students tmp = new  Models.students();
                    tmp.id = id;
                    tmp.name = name;
                    tmp.school = school;

                    database.Table(typeof(Models.students)).Update(tmp);
                }
            }
            catch(InvalidCastException ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show("Invalid value for type Number! Please try again.");
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


