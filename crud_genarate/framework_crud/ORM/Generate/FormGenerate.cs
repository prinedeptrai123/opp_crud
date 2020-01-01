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
        private List<TableDefinition> tables;

        public FormGenerate(string pathName, List<TableDefinition> tables)
        {
            this._pathName = pathName;
            this.tables = tables;
        }


        public void GenerateFormLink(string nameSpace)
        {
            if (tables.Count <= 0) return;
            // generate file .cs and file Designer.cs FormMain
            GenerateCSListViews(tables, nameSpace + ".Views");
            GenerateDSListViews(tables, nameSpace + ".Views");
        }

        // generate form view CRUD
        public void Generate(TableDefinition table, string nameSpace)
        {
            // generate form show all link
            GenerateFormLink(nameSpace);

            // generate form main
            GenerateCSFormMain(table, nameSpace + ".Views");
            GenerateDSFormMain(table, nameSpace + ".Views");

            // generate file  .cs and file Designer.cs addForm and updateForm
            GenerateCSFile(table, nameSpace + ".Views");
            GenerateDSFile(table, nameSpace + ".Views");
        }

        private void GenerateDSListViews(List<TableDefinition> tables, string nameSpace)
        {
            string dsName = "ListViews" + _designPostFix;
            StringBuilder sb = new StringBuilder();
            StringBuilder sbDecalre = new StringBuilder();
            StringBuilder sbContent = new StringBuilder();
            StringBuilder sbAddd = new StringBuilder();
            StringBuilder sbDefine = new StringBuilder();

            string declareTemplate = "            this.{0} = new System.Windows.Forms.LinkLabel();\n";
            string dfTemplate = "        private System.Windows.Forms.LinkLabel {0};\n";
            string addTemplatle = "            this.Controls.Add(this.{0});\n";
            string contentTemplate = "";
            using (StreamReader sr = new StreamReader("DSListViews.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();
                sb.Append(line);
            }

            using (StreamReader sr = new StreamReader("LabelLinkTemplate.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                contentTemplate = sr.ReadToEnd();
            }

            int Y = 150;
            int tabIndex = 2;
            foreach (TableDefinition td in tables)
            {
                string lblName = "lbl" + td.name;
                sbDecalre.Append(string.Format(declareTemplate, lblName));
                sbDefine.Append(string.Format(dfTemplate, lblName));
                sbAddd.Append(string.Format(addTemplatle, lblName));

                // content
                sbContent.Append(contentTemplate);
                sbContent.Replace("%LBNAME%", lblName);
                sbContent.Replace("%TEXT%", "Models." + td.name);
                sbContent.Replace("%LBY%", Y.ToString());
                sbContent.Replace("%TABINDEX%", tabIndex.ToString());

                tabIndex++;
                Y += 30;
            }

            sb.Replace("%NAMESPACE%", nameSpace);
            sb.Replace("%Y%", Y.ToString());
            sb.Replace("%DECLARE%", sbDecalre.ToString());
            sb.Replace("%ADD%", sbAddd.ToString());
            sb.Replace("%CONTENT%", sbContent.ToString());
            sb.Replace("%DEFINE%", sbDefine.ToString());

            // write to file
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_pathName, dsName)))
            {
                outputFile.WriteLine(sb.ToString());
            }
        }

        private void GenerateCSListViews(List<TableDefinition> tables, string nameSpace)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbFunction = new StringBuilder();
            StringBuilder sbRegisters = new StringBuilder();
            String functionTemplate = "";

            using (StreamReader sr = new StreamReader("FNLinkTemplate.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                functionTemplate = sr.ReadToEnd();
            }

            using (StreamReader sr = new StreamReader("CSListViews.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();
                sb.Append(line);
            }

            foreach(TableDefinition td in tables)
            {
                string csName = "FM" + td.name + _csPostFix;
                string fnName = "FM" + td.name;
                string lbLinkName = "lbl" + td.name;
                string functionClick = lbLinkName + "_click";

                // registers
                sbRegisters.Append(string.Format("			{0}.Click += new System.EventHandler({1});\n", lbLinkName, functionClick));

                // functions
                sbFunction.Append(functionTemplate);
                sbFunction.Replace("%FUNCTIONNAME%", functionClick);
                sbFunction.Replace("%FORMNAME%", fnName);
                sbFunction.Replace("%TABLENAME%", td.name);
            }

            sb.Replace("%NAMESPACE%", nameSpace);
            sb.Replace("%REGISTER%", sbRegisters.ToString());
            sb.Replace("%FUNCTION%", sbFunction.ToString());

            // write to file
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_pathName, "ListViews.cs")))
            {
                outputFile.WriteLine(sb.ToString());
            }
        }

        private void GenerateCSFormMain(TableDefinition table, string nameSpace)
        {
            string csName = "FM" + table.name + _csPostFix;
            StringBuilder sb = new StringBuilder();

            using (StreamReader sr = new StreamReader("FormMain.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();
                sb.Append(line);
            }

            sb.Replace("%NAMESPACE%", nameSpace);
            sb.Replace("%TABLENAME%", table.name);
            sb.Replace("%FORMNAME%", "FM" + table.name);

            // write to file
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_pathName, csName)))
            {
                outputFile.WriteLine(sb.ToString());
            }
        }

        private void GenerateDSFormMain(TableDefinition table, string nameSpace)
        {
            string dsName = "FM" + table.name + _designPostFix;
            StringBuilder sb = new StringBuilder();

            using (StreamReader sr = new StreamReader("DSFormMain.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();
                sb.Append(line);
            }

            sb.Replace("%NAMESPACE%", nameSpace);
            sb.Replace("%FORMNAME%", "FM" + table.name);

            // write to file
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_pathName, dsName)))
            {
                outputFile.WriteLine(sb.ToString());
            }
        }


        private void GenerateCSFile(TableDefinition table, string nameSpace)
        {
            string csName = table.name + _csPostFix;
            StringBuilder sb = new StringBuilder();
            StringBuilder sbContent = new StringBuilder();
            StringBuilder sbSave = new StringBuilder();
            StringBuilder sbFields = new StringBuilder();

            using (StreamReader sr = new StreamReader("CSTemplate.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();
                sb.Append(line);
            }

            // content initial
            List<FieldDefinition> fields = table.fields;
            int tabIndex = 1;
            foreach (FieldDefinition fd in fields)
            {
                //name of type input
                string typeInput = getTypeName(fd);
                string nameItem = "it" + typeInput.ToLower() + fd.columnName + tabIndex.ToString();

                // case number
                if (isNumeric(fd.memberName))
                {
                    sbContent.Append(string.Format("			{0}.Value = Decimal.Parse(entity.{1}.ToString());\n", nameItem, fd.columnName));
                    // content save
                    string ctSave = "                Int32 {0} = Int32.Parse(Math.Floor({1}.Value).ToString());\n";
                    sbSave.Append(string.Format(ctSave, fd.columnName, nameItem));
                }
                else if (isBool(fd.memberName))
                {
                    sbContent.Append(string.Format("			{1}.Checked = entity.{1};\n", nameItem, fd.columnName));
                    // content save
                    string ctSave = "                bool {0} = {1}.Checked;\n";
                    sbSave.Append(string.Format(ctSave, fd.columnName, nameItem));
                }
                else // text case
                {
                    sbContent.Append(string.Format("			{0}.Text = entity.{1};\n", nameItem, fd.columnName));
                    // content save
                    string ctSave = "                string {0} = {1}.Text;\n";
                    sbSave.Append(string.Format(ctSave, fd.columnName, nameItem));
                }
                // content fields
                sbFields.Append(string.Format("                    tmp.{0} = {0};\n", fd.columnName));
                tabIndex++;
            }

            // replace namespace and class content
            sb.Replace("%NAMESPACE%", nameSpace);
            sb.Replace("%CLASS%", table.name);
            sb.Replace("%TABLENAME%", "Models." + table.name);
            sb.Replace("%FIELDS%", sbFields.ToString());
            sb.Replace("%CTINITIAL%", sbContent.ToString());
            sb.Replace("%CTSAVE%", sbSave.ToString());

            // write to file
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_pathName, csName)))
            {
                outputFile.WriteLine(sb.ToString());
            }
        }

        private void GenerateDSFile(TableDefinition table, string nameSpace)
        {

            // declare location
            float Y = 27;

            string tmlClare, tmlDefine, tmlTemplate, tmlInputTemplate;
            string tmlAdd = "            this.panel1.Controls.Add(this.%ITEM%);\n";

            // read declare
            using (StreamReader rdt = new StreamReader("DLTemplate.txt"))
            {
                tmlClare = rdt.ReadToEnd();
            }

            // read label content
            using (StreamReader rdt = new StreamReader("CTLabelTemplate.txt"))
            {
                tmlTemplate = rdt.ReadToEnd();
            }

            // read input content
            using (StreamReader rdt = new StreamReader("CTInputTemplate.txt"))
            {
                tmlInputTemplate = rdt.ReadToEnd();
            }

            // read define
            using (StreamReader rdt = new StreamReader("DFTemplate.txt"))
            {
                tmlDefine = rdt.ReadToEnd();
            }

            StringBuilder strAdd = new StringBuilder();
            StringBuilder strContent = new StringBuilder();
            StringBuilder strDeclare = new StringBuilder();
            StringBuilder strDefine = new StringBuilder();

            int tabIndex = 1;
            foreach (FieldDefinition fd in table.fields)
            {
                // Label
                //
                // declare
                string nameItem = AddDefineAndClare(strAdd, strDeclare, strDefine, tmlClare, tmlDefine, tmlAdd, fd, "Label", tabIndex);

                // text label
                ReplaceLabel(Y, tmlTemplate, strContent, tabIndex, nameItem, fd.columnName);

                // content for input
                ReplaceInput(Y, tmlClare, tmlDefine, tmlInputTemplate, tmlAdd, strAdd, strContent, strDeclare, strDefine, tabIndex, fd);

                // add Y = 30 for item
                Y = Y + 30;
                tabIndex++;
            }

            // replace content add form
            // add to design file
            // read template cs file from resource
            string dsName = table.name + _designPostFix;
            // 2 param: {0}: namespace, {1} form name
            StringBuilder dsTemplate = new StringBuilder();
            // read template cs file from resource
            using (StreamReader sr = new StreamReader("DesignTemplate.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();
                dsTemplate.Append(line);
            }

            ReplaceDesign(table, Y, strAdd, strContent, strDeclare, strDefine, dsTemplate, nameSpace);

            // write to file
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_pathName, dsName)))
            {
                outputFile.WriteLine(dsTemplate.ToString());
            }
        }

        private void ReplaceDesign(TableDefinition table, float Y, StringBuilder strAdd, StringBuilder strContent, 
            StringBuilder strDeclare, StringBuilder strDefine, StringBuilder dsTemplate, string nameSpace)
        {
            // scale for panel // scroll
            dsTemplate.Replace("%PANELY%", (Y + 60).ToString());
            dsTemplate.Replace("%FORMY%", (Y + 90).ToString());
            dsTemplate.Replace("%SAVEY%", (Y + 15).ToString());

            // replace namespace and class content
            dsTemplate.Replace("%NAMESPACE%", nameSpace);
            dsTemplate.Replace("%CLASSNAME%", table.name);

            dsTemplate.Replace("%DECLARE%", strDeclare.ToString());
            dsTemplate.Replace("%ADDPANEL%", strAdd.ToString());
            dsTemplate.Replace("%CONTENT%", strContent.ToString());
            dsTemplate.Replace("%DEFINE%", strDefine.ToString());
        }

        private void ReplaceInput(float Y, string tmlClare, string tmlDefine, string tmlInputTemplate, string tmlAdd, StringBuilder strAdd, StringBuilder strContent, StringBuilder strDeclare, StringBuilder strDefine, int tabIndex, FieldDefinition fd)
        {
            //name of type input
            string typeInput;
            // case 1: integer
            typeInput = getTypeName(fd);

            string nameInput = AddDefineAndClare(strAdd, strDeclare, strDefine, tmlClare, tmlDefine, tmlAdd, fd, typeInput, tabIndex);

            // content
            strContent.Append(tmlInputTemplate);
            strContent.Replace("%LOCATIONY%", Y.ToString());
            strContent.Replace("%NAME%", nameInput);
            strContent.Replace("%TABINDEX%", tabIndex.ToString());
        }

        private string getTypeName(FieldDefinition fd)
        {
            string typeInput;
            if (isInteger(fd.memberName) || isNumeric(fd.memberName))
            {
                typeInput = "NumericUpDown";
            }
            // case 2: bool
            else if (isBool(fd.memberName))
            {
                typeInput = "CheckBox";
            }
            // case 3: datetime
            else if (isDateTime(fd.memberName))
            {
                typeInput = "DateTimePicker";
            }
            else
            {
                typeInput = "TextBox";
            }

            return typeInput;
        }

        private static void ReplaceLabel(float Y, string tmlTemplate, StringBuilder strContent, int tabIndex, string nameItem, string columnName)
        {
            // content
            strContent.Append(tmlTemplate);
            strContent.Replace("%LOCATIONY%", Y.ToString());
            strContent.Replace("%NAME%", nameItem);
            strContent.Replace("%TABINDEX%", tabIndex.ToString());
            strContent.Replace("%TEXT%", columnName);
        }

        private static string AddDefineAndClare(StringBuilder strAdd, StringBuilder strDeclare, StringBuilder strDefine,
            string tmlClare, string tmlDefine, string tmlAdd, FieldDefinition fd, string type, int tabIndex)
        {
            strDeclare.Append(tmlClare);
            string nameItem = "it" + type.ToLower() + fd.columnName + tabIndex.ToString();
            strDeclare.Replace("%NAME%", nameItem);
            strDeclare.Replace("%TYPE%", type);
            // add
            strAdd.Append(tmlAdd);
            strAdd.Replace("%ITEM%", nameItem);
            // define
            strDefine.Append(tmlDefine);
            strDefine.Replace("%ITEM%", nameItem);
            strDefine.Replace("%NAME%", nameItem);
            strDefine.Replace("%TYPE%", type);
            return nameItem;
        }

        private bool isInteger(string type)
        {
            if ("Byte" == type || "Int32" == type || "Int16" == type || "Int64" == type)
                return true;
            return false;
        }

        private bool isNumeric(string type)
        {
            if ("Double" == type || "Decimal" == type || "Int32" == type) return true;
            return false;
        }

        private bool isBool(string type)
        {
            return "Boolean" == type;
        }

        private bool isDateTime(string type)
        {
            return "DateTime" == type;
        }
    }
}
