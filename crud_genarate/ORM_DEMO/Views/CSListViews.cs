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

namespace ORM_DEMO.Views
{
    public partial class ListViews : Form
    {

        private string connstring = "Data Source=DESKTOP-15SIF8Q\\SQLEXPRESS; database=oop;" +
            "Integrated Security=True;Connect Timeout=10";
        private IList listData;
        private MSSQLDatabase database;
        private List<TableDefinition> tables;

        public ListViews()
        {
            InitializeComponent();
            prepareLoad();
            registerEvents();
        }

		public ListViews(string conn)
        {
			this.connstring = conn;
            InitializeComponent();
            prepareLoad();
            registerEvents();
        }

        private void prepareLoad()
        {
            database = new MSSQLDatabase(new System.Data.SqlClient.SqlConnection(connstring));
            tables = database.listTable();
        }

        private void registerEvents()
        {
            			lblstudents.Click += new System.EventHandler(lblstudents_click);
			lbltestdata.Click += new System.EventHandler(lbltestdata_click);

        }

        		private void lblstudents_click(object sender, EventArgs e) 
		{
			Views.FMstudents frm = new FMstudents(connstring);
			frm.Show();
		}

		private void lbltestdata_click(object sender, EventArgs e) 
		{
			Views.FMtestdata frm = new FMtestdata(connstring);
			frm.Show();
		}

		private void %FUNCTIONNAME%(object sender, EventArgs e) 
		{
			Views.%FORMNAME% frm = new %FORMNAME%(connstring);
			frm.Show();
		}


    }
}

