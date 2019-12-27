using framework_crud.ORM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace framework_crud
{
    public class ProjectMaster
    {
        #region singleton
        private IDatabase database;
        private string folderName = "Models";
        private string folderView = "Views";
        private string _namespace;
        private string _directoryName;

        // const temp
        //private string con = "Data Source={0}; database={1};Integrated Security=True;Connect Timeout=10";

        private static string con = "Data Source=DESKTOP-15SIF8Q\\SQLEXPRESS; database=School;Integrated Security=True;Connect Timeout=10";
        private static string con2 = "Data Source=DESKTOP-GR8RADT\\SQLEXPRESS; database=test;Integrated Security=True;Connect Timeout=10";
        //private string _namespace = "ORM_DEMO";
        private string _applicationName = "ORM_DEMO";


        public ProjectMaster(string connString, string nameSpace, string directoryPath)
        {

            _namespace = nameSpace;
            _directoryName = directoryPath;


            tables = new List<TableDefinition>();
            Console.WriteLine("Opening database connection: " + connString);

            // FIXME: connect database failed
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
            Console.WriteLine(tables[0].name + tables[0].schema + tables[0].fields[0].columnName + 
                tables[0].fields[1].columnName);

            // loop generate table to class model
            // STEP 1: create folder to gen
            // STEP 2: gen into folder with struct
            // STEP 3: gen view into folder with struct

            Console.WriteLine("Database ORM will generated into " + folderName);
            string pathName = String.Format(@"{0}\{1}", _directoryName, folderName);

            // STEP 1:
            System.IO.Directory.CreateDirectory(pathName);

            //STEP 2:
            foreach (var table in tables)
            {
                table.generate(new ClassGenerate(pathName));
            }

            string pathView = String.Format(@"{0}\{1}", _directoryName, folderView);
            System.IO.Directory.CreateDirectory(pathView);
            // STEP 3
            foreach (var table in tables)
            {
                table.generate(new FormGenerate(pathView, _namespace));
            }
        }

        //TODO: code this
        public void genForm()
        {
            
        }

        public void generateProject(string solutionName, string projectName, string generateLocation)
        {
            System.Type dteType = Type.GetTypeFromProgID("VisualStudio.DTE.15.0", true);
            Object obj = System.Activator.CreateInstance(dteType, true);
            EnvDTE.DTE dte = (EnvDTE.DTE)obj;
            dte.MainWindow.Visible = true;
            dte.Solution.Create(generateLocation, solutionName);
            var solution = dte.Solution;
            string solutionPath = generateLocation + solutionName;
            EnvDTE.Project project = solution.AddFromTemplate(@"D:\\WindowsApplication\csWindowsApplication.vstemplate", solutionPath, projectName);
            dte.ExecuteCommand("File.SaveAll");
            dte.Quit();
        }
    }
}
