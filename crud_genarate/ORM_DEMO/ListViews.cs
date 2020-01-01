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

namespace ORM_DEMO
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

        private void prepareLoad()
        {
            database = new MSSQLDatabase(new System.Data.SqlClient.SqlConnection(connstring));
            tables = database.listTable();
        }

        private void registerEvents()
        {
            foreach(TableDefinition fd in tables)
            {
                // register event for table link
            }
        }

        // generate function handle click
    }
}
