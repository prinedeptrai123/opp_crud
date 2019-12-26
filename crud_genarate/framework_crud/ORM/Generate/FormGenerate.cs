using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    public class FormGenerate : IGenerate
    {
        private string _pathName;
        private string _csPostFix = ".cs";
        private string _designPostFix = ".Designer.cs";
        private string _namespace;

        public FormGenerate(string pathName, string _namespace)
        {
            this._pathName = pathName;
            this._namespace = _namespace;
        }

        // generate form view CRUD
        public void Generate(TableDefinition table)
        {
            // generate file  .cs and file Designer.cs
            GenerateCSFile(table);
            GenerateDSFile(table);

        }

        private void GenerateCSFile(TableDefinition table)
        {
            string csName = table.name + _csPostFix;
            StringBuilder sb = new StringBuilder();

            // read template cs file from resource
            using (StreamReader sr = new StreamReader("CSTemplate.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();
                sb.Append(line);
            }

            // replace namespace and class content
            sb.Replace("%NAMESPACE%", _namespace);
            sb.Replace("%CLASS%", table.name);

            // write to file
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_pathName, csName)))
            {
                outputFile.WriteLine(sb.ToString());
            }
        }

        private void GenerateDSFile(TableDefinition table)
        {
            string dsName = table.name + _designPostFix;
            // 2 param: {0}: namespace, {1} form name
            StringBuilder sb = new StringBuilder();

            // read template cs file from resource
            using (StreamReader sr = new StreamReader("DSTemplate.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();
                sb.Append(line);
            }

            // replace namespace and class content
            sb.Replace("%NAMESPACE%", _namespace);
            sb.Replace("%CLASS%", table.name);
            sb.Replace("%FORMNAME%", table.name);

            // write to file
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_pathName, dsName)))
            {
                outputFile.WriteLine(sb.ToString());
            }
        }
    }
}
