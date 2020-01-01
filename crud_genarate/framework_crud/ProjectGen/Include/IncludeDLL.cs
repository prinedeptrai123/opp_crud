using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace framework_crud.ProjectGen
{
    public class IncludeDLL : CommonInclude
    {
        public void includeProject(string folder, string fileName, string solutionPath, string projectName)
        {
            XmlDocument xmldoc = new XmlDocument();
            string path = $"{solutionPath}\\{projectName}.csproj";
            xmldoc.Load(path);
            XmlNamespaceManager mgr = new XmlNamespaceManager(xmldoc.NameTable);
            mgr.AddNamespace("x", "http://schemas.microsoft.com/developer/msbuild/2003");

            XmlNode firstReferenceNode = xmldoc.SelectSingleNode("/x:Project/x:ItemGroup/x:Reference", mgr);
            XmlNode itemGroupReferenceNode = firstReferenceNode.ParentNode;
            //TODO add reference
            XmlNode newDllNode = xmldoc.CreateNode(XmlNodeType.Element, "Reference", string.Empty);
            string dll = $"{fileName}.dll";
            XmlAttribute newDLLNodeAttribute = xmldoc.CreateAttribute("Include");
            newDLLNodeAttribute.Value = dll;
            newDllNode.Attributes.Append(newDLLNodeAttribute);
            XmlNode pathNode = xmldoc.CreateNode(XmlNodeType.Element, "HintPath", string.Empty);
            if (string.IsNullOrEmpty(folder))
            {
                pathNode.InnerText = dll;
            }
            else
            {
                pathNode.InnerText = $"{folder}\\{dll}";
            }
            newDllNode.AppendChild(pathNode);
            itemGroupReferenceNode.AppendChild(newDllNode);

            xmldoc.Save(path);
        }
    }
}
