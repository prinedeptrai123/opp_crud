using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace framework_crud.ProjectGen
{
    public class IncludeFile : CommonInclude
    {
        public void includeProject(string folder, string fileName, string solutionPath, string projectName)
        {
            XmlDocument xmldoc = new XmlDocument();
            string path = $"{solutionPath}\\{projectName}.csproj";
            xmldoc.Load(path);
            XmlNamespaceManager mgr = new XmlNamespaceManager(xmldoc.NameTable);
            mgr.AddNamespace("x", "http://schemas.microsoft.com/developer/msbuild/2003");

            //TODO add Normal Class
            XmlNode firstCompileNode = xmldoc.SelectSingleNode("/x:Project/x:ItemGroup/x:Compile", mgr);
            XmlNode itemGroupCompileNode = firstCompileNode.ParentNode;
            string className = $"{fileName}.cs";
            if (!string.IsNullOrEmpty(folder))
            {
                className = $"{folder}\\{fileName}.cs";
            }
            //string className = "Qui.cs";
            XmlNode newCompileNode = xmldoc.CreateNode(XmlNodeType.Element, "Compile", string.Empty);
            XmlAttribute newCompileNodeAttribute = xmldoc.CreateAttribute("Include");
            newCompileNodeAttribute.Value = className;
            newCompileNode.Attributes.Append(newCompileNodeAttribute);
            itemGroupCompileNode.AppendChild(newCompileNode);

            xmldoc.Save(path);
        }
    }
}
