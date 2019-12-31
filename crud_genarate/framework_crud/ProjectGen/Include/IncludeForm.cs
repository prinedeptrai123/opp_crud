using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace framework_crud.ProjectGen
{
    public class IncludeForm : CommonInclude
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

            //TODO add form
            XmlNode newFormNode = xmldoc.CreateNode(XmlNodeType.Element, "Compile", string.Empty);
            XmlAttribute newFormNodeAttribute = xmldoc.CreateAttribute("Include");

            string formName = $"{ fileName}.cs";
            if (!string.IsNullOrEmpty(folder))
            {
                formName = $"{folder}\\{fileName}.cs";
            }

            newFormNodeAttribute.Value = formName;
            newFormNode.Attributes.Append(newFormNodeAttribute);
            XmlNode Type = xmldoc.CreateNode(XmlNodeType.Element, "SubType", string.Empty);
            Type.InnerText = "Form";
            newFormNode.AppendChild(Type);
            itemGroupCompileNode.AppendChild(newFormNode);

            //Add designer
            XmlNode newFormDesign = xmldoc.CreateNode(XmlNodeType.Element, "Compile", string.Empty);
            XmlAttribute newFormDesignAttribute = xmldoc.CreateAttribute("Include");

            string designName = $"{fileName}.Designer.cs";
            if (string.IsNullOrEmpty(folder))
            {
                designName = $"{folder}\\{fileName}.Designer.cs";
            }
            newFormDesignAttribute.Value = designName;
            newFormDesign.Attributes.Append(newFormDesignAttribute);
            XmlNode upon = xmldoc.CreateNode(XmlNodeType.Element, "DependentUpon", string.Empty);
            upon.InnerText = $"{fileName}.cs";
            newFormDesign.AppendChild(upon);
            itemGroupCompileNode.AppendChild(newFormDesign);

            //TODO add folder

            xmldoc.Save(path);
        }
    }
}
