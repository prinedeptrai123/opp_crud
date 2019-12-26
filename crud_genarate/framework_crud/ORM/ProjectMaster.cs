using framework_crud.ORM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud
{
    public class ProjectMaster
    {
        #region singleton
        private static ProjectMaster _instance = null;
        private IDatabase database;
        private static string con = "Data Source=DESKTOP-GR8RADT\\SQLEXPRESS; database=test;Integrated Security=True;Connect Timeout=10";

        public static ProjectMaster Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ProjectMaster(con);
                }

                return _instance;
            }
        }

        private ProjectMaster(string connString)
        {
            tables = new List<TableDefinition>();
            Console.WriteLine("Opening database connection: " + connString);
            database = new MSSQLDatabase(
                    new SqlConnection(connString));

            //get all infomation of Table
            tables = database.listTable();
        }

        #endregion

        List<TableDefinition> tables;

        //TODO: code this
        public void genTable()
        {
            //TODO: remove
            //IList data = database.Table(tables[0].GetType()).Query().Select();
            Console.WriteLine(tables[0].name + tables[0].schema + tables[0].fields[0].columnName + tables[0].fields[1].columnName);

            foreach (var table in tables)
            {
                table.generate(new ClassGenerate("D:\\"));
            }
        }

        //TODO: code this
        public void genForm()
        {
            
        }
    }
}
