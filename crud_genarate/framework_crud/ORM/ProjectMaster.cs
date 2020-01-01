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
using framework_crud.ProjectGen;

namespace framework_crud
{
    public class ProjectMaster
    {
        #region singleton
        private IDatabase database;
        private string folderName = "Models";
        private string folderView = "Views";
        private string folderLib = "Libs";
        private string dll = "framework_crud";
        private string _namespace;
        private string _directoryName;

        // const temp
        //private string con = "Data Source={0}; database={1};Integrated Security=True;Connect Timeout=10";

        private static string con = "Data Source=DESKTOP-15SIF8Q\\SQLEXPRESS; database=School;Integrated Security=True;Connect Timeout=10";


        private static string con2 = "Data Source=DESKTOP-GR8RADT\\SQLEXPRESS; database=CaffeApp;Integrated Security=True;Connect Timeout=10";
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
            //gen project
            generateProject(_namespace, _namespace, _directoryName);

            //TODO: remove
            //IList data = database.Table(tables[0].GetType()).Query().Select();
            //Console.WriteLine(tables[0].name + tables[0].schema + tables[0].fields[0].columnName + 
            //    tables[0].fields[1].columnName);

            // loop generate table to class model
            // STEP 1: create folder to gen
            // STEP 2: gen into folder with struct
            // STEP 3: gen view main, add, update into folder with struct
            // STEP 4: gen view show all link to form of models

            Console.WriteLine("Database ORM will generated into " + folderName);
            string pathName = String.Format(@"{0}\{1}\{2}", _directoryName,_namespace, folderName);
            string projectPath = string.Format(@"{0}\{1}", _directoryName, _namespace);

            CommonInclude includeFile= new IncludeFile();
            CommonInclude includeForm = new IncludeForm();
            CommonInclude includeDLL = new IncludeDLL();

            // STEP 1:
            System.IO.Directory.CreateDirectory(pathName);

            //STEP 2:
            foreach (var table in tables)
            {
                table.generate(new ClassGenerate(pathName), _namespace);
            }

            string pathView = String.Format(@"{0}\{1}\{2}", _directoryName, _namespace, folderView);
            System.IO.Directory.CreateDirectory(pathView);

            // STEP 3

            foreach (var table in tables)
            {
                table.generate(new FormGenerate(pathView, tables), _namespace);
            }

            foreach(var table in tables)
            {
                //include add | update
                includeForm.includeProject(folderView, table.name, projectPath, _namespace);
                //include main | form
                includeForm.includeProject(folderView, $"FM{table.name}", projectPath, _namespace);
                //include Model
                includeFile.includeProject(folderName, table.name, projectPath, _namespace);
            }

            //include List
            includeForm.includeProject(folderView, "ListViews", projectPath, _namespace);

            //copy | add reference dll
            string pathLibs = String.Format(@"{0}\{1}\{2}", _directoryName, _namespace, folderLib);
            copyFile(dll + ".dll", pathLibs);
            includeDLL.includeProject(folderLib, dll, projectPath, _namespace);
        }

        private void copyFile(string fileName,string targetPath)
        {
            string sourceFile = System.IO.Path.Combine(@".\", fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);
            System.IO.File.Copy(sourceFile, destFile, true);
        }

        //TODO: code this
        public void genForm()
        {

        }

        public void genFormMain()
        {

        }

        public void generateProject(string solutionName, string projectName, string generateLocation)
        {
            Console.WriteLine($"{solutionName} {projectName} {generateLocation}");

            System.Threading.Thread.Sleep(1000);
            System.Type dteType = Type.GetTypeFromProgID("VisualStudio.DTE.15.0", true);
            Object obj = System.Activator.CreateInstance(dteType, true);
            EnvDTE.DTE dte = (EnvDTE.DTE)obj;
            dte.MainWindow.Visible = true;
            dte.Solution.Create(generateLocation, solutionName);
            var solution = dte.Solution;
            System.Threading.Thread.Sleep(1000);
            string projectPath = generateLocation + "\\" + solutionName;
            EnvDTE.Project project = solution.AddFromTemplate(@"D:\WindowsApplication\csWindowsApplication.vstemplate", projectPath, projectName);
            System.Threading.Thread.Sleep(5000);
            dte.ExecuteCommand("File.SaveAll");
            System.Threading.Thread.Sleep(0);
            dte.Quit();
        }
    }
}
