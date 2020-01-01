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
    public partial class testdata : Form
    {
        private string connstring = "Data Source=DESKTOP-15SIF8Q\\SQLEXPRESS; database=oop;" +
            "Integrated Security=True;Connect Timeout=10";
        private MSSQLDatabase database;
        private BindingSource bd = new BindingSource();
        private bool isUpdate = false;

        public testdata(string conn)
        {
            this.connstring = conn;
            InitializeComponent();
            regisEvents();
            initial();
        }

        public testdata()
        {
            InitializeComponent();
            regisEvents();
            initial();
        }

        public testdata(Models.testdata entity)
        {
            InitializeComponent();
            regisEvents();
            initial(entity);
            isUpdate = true;
			this.Text = "UpdateForm";
        }

        private void initial(Models.testdata entity)
        {
			ittextboxrowID1.Text = entity.rowID;
			itnumericupdownbyte12.Text = entity.byte1;
			itnumericupdownshort13.Text = entity.short1;
			itnumericupdownint14.Text = entity.int1;
			itnumericupdownlong15.Text = entity.long1;
			ittextboxfloat16.Text = entity.float1;
			itnumericupdowndouble17.Value = Decimal.Parse(entity.double1.ToString();
			itnumericupdowndecimal18.Value = Decimal.Parse(entity.decimal1.ToString();
			itnumericupdownmoney19.Value = Decimal.Parse(entity.money1.ToString();
			ittextboxstring110.Text = entity.string1;
			ittextboxbinary111.Text = entity.binary1;
			ittextboximage112.Text = entity.image1;
			bool1.Checked = entity.bool1;
			itdatetimepickerdatetime114.Text = entity.datetime1;
			ittextboxguid115.Text = entity.guid1;

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
                string rowID = ittextboxrowID1.Text;
                string byte1 = itnumericupdownbyte12.Text;
                string short1 = itnumericupdownshort13.Text;
                string int1 = itnumericupdownint14.Text;
                string long1 = itnumericupdownlong15.Text;
                string float1 = ittextboxfloat16.Text;
                string double1 = Int32.Parse(itnumericupdowndouble17.Value.ToString());
                string decimal1 = Int32.Parse(itnumericupdowndecimal18.Value.ToString());
                string money1 = Int32.Parse(itnumericupdownmoney19.Value.ToString());
                string string1 = ittextboxstring110.Text;
                string binary1 = ittextboxbinary111.Text;
                string image1 = ittextboximage112.Text;
                bool bool1 = itcheckboxbool113.Checked;
                string datetime1 = itdatetimepickerdatetime114.Text;
                string guid1 = ittextboxguid115.Text;

                // add
                if (!isUpdate)
                {
                    Models.testdata tmp = new  Models.testdata();
                    tmp.rowID = rowID;
                    tmp.byte1 = byte1;
                    tmp.short1 = short1;
                    tmp.int1 = int1;
                    tmp.long1 = long1;
                    tmp.float1 = float1;
                    tmp.double1 = double1;
                    tmp.decimal1 = decimal1;
                    tmp.money1 = money1;
                    tmp.string1 = string1;
                    tmp.binary1 = binary1;
                    tmp.image1 = image1;
                    tmp.bool1 = bool1;
                    tmp.datetime1 = datetime1;
                    tmp.guid1 = guid1;

                    database.Table(typeof(Models.testdata)).Insert(tmp);
                }
                // update
                else
                {
                    Models.testdata tmp = new  Models.testdata();
                    tmp.rowID = rowID;
                    tmp.byte1 = byte1;
                    tmp.short1 = short1;
                    tmp.int1 = int1;
                    tmp.long1 = long1;
                    tmp.float1 = float1;
                    tmp.double1 = double1;
                    tmp.decimal1 = decimal1;
                    tmp.money1 = money1;
                    tmp.string1 = string1;
                    tmp.binary1 = binary1;
                    tmp.image1 = image1;
                    tmp.bool1 = bool1;
                    tmp.datetime1 = datetime1;
                    tmp.guid1 = guid1;

                    database.Table(typeof(Models.testdata)).Update(tmp);
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


