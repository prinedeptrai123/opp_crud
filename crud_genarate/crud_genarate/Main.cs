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
using framework_crud;
using System.Data.SqlClient;

namespace crud_genarate
{
    public partial class Main : Form
    {
        SQLConnector connector;

        private string _sourceDirectory;

        public Main()
        {
            InitializeComponent();
        }

        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            //string SQLServer = "DESKTOP-15SIF8Q\\SQLEXPRESS";
            string SQLServer = "DESKTOP-GR8RADT\\SQLEXPRESS";
            //string SQLServer = txtSQLServer.Text;

            if (!validateTestCon())
            {
                MessageBox.Show("Please fill SQL Server or username, password");
            }
            if (chbWindowAuth.Checked) connector = new SQLConnector(SQLServer);
            else connector = new SQLConnector(SQLServer, txtUserName.Text, txtPassWord.Text);

            if (!connector.CheckConnection())
            {
                MessageBox.Show("Please check your information filled");
                btnTestConnect.Text = "Test Connection";
                return;
            }
            DataTable catelog = connector.GetCatalogList();
            txtCatalog.DataSource = catelog;
            txtCatalog.DisplayMember = "name";
            txtCatalog.Enabled = true;
        }

        private bool validateTestCon()
        {
            if (chbWindowAuth.Checked == false)
            {
                if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassWord.Text))
                {
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtSQLServer.Text))
            {
                return false;
            }
            return true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            _callGenCode();
        }

        private void _callGenCode()
        {
            //string err = validData();
            //if (err != null)
            //{
            //    MessageBox.Show(err);
            //    return;
            //}
            txtDirectory.Text = @"F:\QUIOPP";
            txtNameApp.Text = "DEMO";
            txtNamesSpace.Text = "DEMO";
            connector.Catalog = txtCatalog.Text;
            ProjectMaster projectMaster = new ProjectMaster(connector.ConnectionString, txtNamesSpace.Text, txtDirectory.Text);
            projectMaster.genTable();
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Directory path";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                _sourceDirectory = fbd.SelectedPath;
                txtDirectory.Text = _sourceDirectory;
            }
        }

        private string validData()
        {
            string conn = txtSQLServer.Text;
            bool wdAuthen = chbWindowAuth.Checked;
            string username = txtUserName.Text;
            string password = txtPassWord.Text;
            string catalog = txtCatalog.Text;
            string directory = txtDirectory.Text;
            string nameApp = txtNameApp.Text;
            string nameSpace = txtNamesSpace.Text;

            if (string.IsNullOrEmpty(conn) || string.IsNullOrEmpty(catalog) || string.IsNullOrEmpty(directory) || string.IsNullOrEmpty(directory) || string.IsNullOrEmpty(nameApp) || string.IsNullOrEmpty(nameSpace))
            {
                return "Please fill all information";
            }

            if (wdAuthen)
            {
                if (username == null || txtPassWord == null)
                {
                    return "Please fill username and password";
                }
            }

            return null;
        }

        private void chbWindowAuth_CheckedChanged(object sender, EventArgs e)
        {
            if (chbWindowAuth.Checked)
            {
                txtUserName.Enabled = false;
                txtPassWord.Enabled = false;
            }
            else
            {
                txtUserName.Enabled = true;
                txtPassWord.Enabled = true;
            }
        }
    }
}
