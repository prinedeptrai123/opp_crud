using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ProjectGen
{
    /// <summary>
    /// use this class for inlcude file to Project
    /// </summary>
    public class IncludeHelper
    {
        public static void IncludeFile(string folder,string solutionPath,string projectName)
        {
            //STEP
            //var p = new Microsoft.Build.Evaluation.Project($"{solutionPath}\\{projectName}.csproj");

            //p.AddItem("Folder", $"{solutionPath}\\{folder}");
            //p.Save();

            var p = new Microsoft.Build.Evaluation.Project(@"D:\TemplateSolution\TemplateProject.csproj");
            p.AddItem("Folder", @"D:\TemplateSolution\Models\tabletest.cs");
            //p.AddItem("Compile", @"D:\TemplateSolution\testdata.cs");
            p.Save();
        }
    }
}
