using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnvDTE;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace crud_genarate
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Main());

            //System.Type dteType = Type.GetTypeFromProgID("VisualStudio.DTE.15.0", true);
            //Object obj = System.Activator.CreateInstance(dteType, true);
            //EnvDTE.DTE dte = (EnvDTE.DTE)obj;
            ////dte.MainWindow.Visible = true;
            //dte.Solution.Create(@"C:\TemplateSolution", "TemplateSolution");
            //var solution = dte.Solution;

            //EnvDTE.Project project = solution.AddFromTemplate(@"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\ProjectTemplates\CSharp\Windows\1033\WindowsApplication\csWindowsApplication.vstemplate",
            //    @"C:\TemplateSolution\TestTemplate", "TestTemplate");
            //dte.ExecuteCommand("File.SaveAll");
            //dte.Quit();
        }
    }
}
