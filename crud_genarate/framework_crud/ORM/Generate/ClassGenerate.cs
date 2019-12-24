using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    class ClassGenerate : IGenerate
    {
        private string pathName;

        public ClassGenerate(string pathName)
        {
            this.pathName = pathName;
        }

        //TODO generate class file
        public void Generate(TableDefinition table)
        {
            string saveName = table.name + ".cs";

            StringBuilder sb = new StringBuilder();

            //generate field
            foreach (var field in table.fields)
            {
                sb.Append(string.Format(Format.FORMAT_FIELD, field.columnName, field.memberName));
            }
            string classStatement = sb.ToString();
            sb.Clear();

            sb.Append(Format.FORMAT_MODEL);

            sb.Replace("%NAMESPACE%", "Testing");
            sb.Replace("%CLASS%", table.name);
            sb.Replace("%FIELDS%", classStatement);
            sb.Replace("%DB%", "dbo");
            //Write to file
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(pathName, saveName)))
            {
                outputFile.WriteLine(sb.ToString());
            }
        }

    }
}
