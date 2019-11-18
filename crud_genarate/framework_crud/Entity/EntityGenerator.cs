using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace framework_crud.Entity
{
    public class EntityGenerator
    {

        #region global
        private string CMDDir = @"C:\Windows\System32";
        public static string EDMGenDir = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\edmgen.exe ";
        private string formatGenerateMapping = "/mode:fullgeneration /c:\"Data Source ={0}; Initial Catalog = {1}; Integrated Security = SSPI\" /project:{2} /entitycontainer:{3} /namespace:{4} /language:CSharp";
        private string formatGenerateLayer = "/mode:EntityClassGeneration /incsdl:.\\{0}.csdl /outobjectlayer:.\\{0}.Objects.cs /language:CSharp";

        private bool isGenerating = false;
        #endregion

        private string cmdToGenerateMapping;
        private string cmdToGenerateLayer;

        #region varibal

        public string Server { get; set; }
        public string Catalog { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Project { get; set; }
        public string EntityContainer { get; set; }
        public string Namespace { get; set; }
        public string SourceDirectory { get; set; }

        #endregion

        public EntityGenerator(string Server,string Catalog,string Project,string EntityContainer,string Namespace,
            string SourceDirectory)
        {
            this.Server = Server;
            this.Catalog = Catalog;
            this.Project = Project;
            this.EntityContainer = EntityContainer;
            this.Namespace = Namespace;
            this.SourceDirectory = SourceDirectory;

            //hard code
            SourceDirectory = @"D:\KHTNPJ\Opp\TestGen";

            //cmdToGenerateMapping = EDMGenDir + string.Format(formatGenerateMapping, Server, Catalog,
            //    Project, EntityContainer, Namespace);
            //cmdToGenerateLayer = EDMGenDir + string.Format(formatGenerateLayer, Project);
            cmdToGenerateMapping = EDMGenDir + string.Format(formatGenerateMapping, ".\\SQLEXPRESS", "MiniBookStore",
                "model", "MiniBookStoreEntities", "crud_genarate.model");
            //cmdToGenerateLayer = EDMGenDir + string.Format(formatGenerateLayer, "model");
        }

        public void GenerateEntityFiles()
        {
            if (!isGenerating)
            {
                isGenerating = true;
                Directory.CreateDirectory(SourceDirectory);

                Debug.WriteLine(cmdToGenerateMapping);
                Debug.WriteLine(cmdToGenerateLayer);
                try
                {
                    using (var process = new Process())
                    {
                        var startInfo = new ProcessStartInfo
                        {
                            WorkingDirectory = SourceDirectory,
                            WindowStyle = ProcessWindowStyle.Minimized,
                            CreateNoWindow = true,
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                            FileName = "cmd.exe",
                            Verb = "runas"
                        };
                        process.StartInfo = startInfo;
                        process.Start();
                        process.StandardInput.WriteLine(cmdToGenerateMapping);
                        process.StandardInput.Close();
                        process.WaitForExit();
                        process.Close();
                        process.Dispose();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }
}
