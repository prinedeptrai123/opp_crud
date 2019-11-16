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
using crud_genarate.SQLConnection;

namespace crud_genarate
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            string SQLServer = "DESKTOP-GR8RADT\\SQLEXPRESS";

            SQLConnector connector = new SQLConnector(SQLServer);

            DataTable catelog = connector.GetCatalogList();
            cbxCatalog.DataSource = catelog;
            cbxCatalog.DisplayMember = "name";
            cbxCatalog.Enabled = true;
        }
    }
}
