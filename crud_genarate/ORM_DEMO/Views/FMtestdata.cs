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
using ORM_DEMO.Views;
using System.Diagnostics;

namespace ORM_DEMO.Views
{
    public partial class FMtestdata : Form
    {
        public FMtestdata()
        {
            InitializeComponent();
            prepareData();
        }

        public FMtestdata(string tableName, string connstring)
        {
            this.tableName = tableName;
            this.connstring = connstring;
        }

        private string tableName = "Models.testdata";

        private string connstring = "Data Source=DESKTOP-15SIF8Q\\SQLEXPRESS; database=oop;" +
            "Integrated Security=True;Connect Timeout=10";
        private IList listData;
        private List<Models.testdata> list;
        private MSSQLDatabase database;
        private BindingSource bd = new BindingSource();

        private void prepareData()
        {
            // connect database
            database = new MSSQLDatabase(new System.Data.SqlClient.SqlConnection(connstring));

            // load data
            loadData();

            grvData.Dock = DockStyle.Fill;
            grvData.AutoGenerateColumns = true;
            lbNameTable.Text = tableName;
            this.Text = "FormMain";
            this.Refresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Show add form of table
            Views.testdata frmAdd = new Views.testdata();
            frmAdd.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            list = new List<Models.testdata>();
            listData = database.Table(typeof(Models.testdata)).Query().Select();

            foreach (Models.testdata item in listData)
            {
                list.Add(item);
            }

            // Set up the DataGridView.
            grvData.Dock = DockStyle.Fill;

            // Automatically generate the DataGridView columns.
            grvData.AutoGenerateColumns = true;
            
            bd.DataSource = list;
            grvData.DataSource = bd;

            grvData.Update();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Models.testdata selectedRow = (Models.testdata) grvData.CurrentRow.DataBoundItem;
            database.Table(typeof(Models.testdata)).Delete(selectedRow);
            MessageBox.Show("Delete successfully. Refresh...");
            loadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (grvData.CurrentRow == null) return;
            Models.testdata selectedRow = (Models.testdata)grvData.CurrentRow.DataBoundItem;
            Views.testdata updateForm = new Views.testdata(selectedRow);
            updateForm.Show();
        }
    }
}

