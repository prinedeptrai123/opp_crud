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
        //private string nameSpace;

        public ClassGenerate(string pathName,string nameSpace)
        {
            this.pathName = pathName;
            //this.nameSpace = nameSpace;
        }

        public ClassGenerate(string pathName)
        {
            this.pathName = pathName;
        }

        //TODO: generate class file
        public void Generate(TableDefinition table, string nameSpace)
        {
            string saveName = table.name + ".cs";

            StringBuilder sb = new StringBuilder();

            Console.WriteLine(table.name + "-------------");

            foreach (var field in table.fields)
            {
                Console.WriteLine(field.flags);
                
                if (field.flags.HasFlag(FieldFlags.ForeignKey))
                {
                    sb.Append(string.Format(Format.FORMAT_REFERENCE_KEY, field.columnName, field.memberName,
                         field.fieldReference.table, field.fieldReference.column));
                }
                else if (field.flags.HasFlag(FieldFlags.Key))
                {
                    sb.Append(string.Format(Format.FORMAT_KEY, field.columnName, field.memberName));
                }
                else
                {
                    sb.Append(string.Format(Format.FORMAT_FIELD, field.columnName, field.memberName, ""));
                }
            }
            string classStatement = sb.ToString();
            sb.Clear();

            //string line;
            //read template from file
            using (StreamReader sr = new StreamReader("ModelTemplate.txt")) 
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();
                sb.Append(line);
            }
            //TODO:fix hash code
            sb.Replace("%NAMESPACE%", nameSpace);
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
