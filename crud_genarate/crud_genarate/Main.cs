using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using framework_crud.MSSQL;
using crud_genarate.model;
using framework_crud.Entity;

namespace crud_genarate
{
    public partial class Main : Form
    {
        SQLConnector connector;
        public Main()
        {
            InitializeComponent();
        }

        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            string SQLServer = "DESKTOP-GR8RADT\\SQLEXPRESS";

            connector = new SQLConnector(SQLServer);

            DataTable catelog = connector.GetCatalogList();
            cbxCatalog.DataSource = catelog;
            cbxCatalog.DisplayMember = "name";
            cbxCatalog.Enabled = true;
            //using(var db = new MiniBookStoreEntities())
            //{
            //    Debug.WriteLine("vao day");

            //    var data = db.Company;
            //    foreach(var c in data)
            //    {
            //        Debug.WriteLine(c.Company_ID);
            //    }
            //    Debug.WriteLine("End loop");
            //    Company a = new Company { Company_Name = "quitest" };
            //    db.Company.AddObject(a);
            //    db.SaveChanges();
            //    Debug.WriteLine("End save");
            //}
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            connector.Catalog = cbxCatalog.Text;
            DataTable db = connector.GetTableList();
            //for(int i = 0; i < db.Rows.Count - 1; i++)
            //{
            //    Debug.WriteLine(db.Rows[i][0]);
            //    DataTable column = connector.GetColumnList(db.Rows[i][0].ToString());
            //    for (int j = 0; j < column.Rows.Count; j++)
            //    {
            //        Debug.WriteLine(column.Rows[j][0] + " " + column.Rows[j][1]);

            //    }
            //}

            _callGenCode();
        }

        private void _callGenCode()
        {
            string catalog = cbxCatalog.Text.ToString();
            Debug.WriteLine(catalog);
            EntityGenerator generator = new EntityGenerator(".\\SQLEXPRESS", catalog, "Model",
                catalog + "Entities", "something.Model", @"D:\KHTNPJ\Opp\TestGen");
            //EntityGenerator generator = new EntityGenerator("1", "1", "Model",
            //    "1", "something.Model", "1");
            generator.GenerateEntityFiles();
        }
    }
}
