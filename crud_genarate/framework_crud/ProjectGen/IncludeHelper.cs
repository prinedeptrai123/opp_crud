using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace framework_crud.ProjectGen
{
    /// <summary>
    /// use this class for inlcude file to Project
    /// </summary>
    public class IncludeHelper
    {
        public static void IncludeFile(string folder, string solutionPath, string projectName)
        {
            XmlDocument xmldoc = new XmlDocument();
            string path = $"{solutionPath}\\{projectName}.csproj";
            xmldoc.Load(path);

            XmlNamespaceManager mgr = new XmlNamespaceManager(xmldoc.NameTable);
            mgr.AddNamespace("x", "http://schemas.microsoft.com/developer/msbuild/2003");

            //TODO add reference
            XmlNode firstReferenceNode = xmldoc.SelectSingleNode("/x:Project/x:ItemGroup/x:Reference", mgr);
            XmlNode itemGroupReferenceNode = firstReferenceNode.ParentNode;
            string DLL = "System.Xml";
            XmlNode newRefrerentNode = xmldoc.CreateNode(XmlNodeType.Element, "Reference", string.Empty);
            XmlAttribute newReferenceNodeAttribute = xmldoc.CreateAttribute("Include");
            newReferenceNodeAttribute.Value = DLL;
            newRefrerentNode.Attributes.Append(newReferenceNodeAttribute);
            itemGroupReferenceNode.AppendChild(newRefrerentNode);

            //TODO add Normal Class
            XmlNode firstCompileNode = xmldoc.SelectSingleNode("/x:Project/x:ItemGroup/x:Compile", mgr);
            XmlNode itemGroupCompileNode = firstCompileNode.ParentNode;
            string className = "Qui.cs";
            XmlNode newCompileNode = xmldoc.CreateNode(XmlNodeType.Element, "Compile", string.Empty);
            XmlAttribute newCompileNodeAttribute = xmldoc.CreateAttribute("Include");
            newCompileNodeAttribute.Value = className;
            newCompileNode.Attributes.Append(newCompileNodeAttribute);
            itemGroupCompileNode.AppendChild(newCompileNode);

            //TODO add asemdll
           
            XmlNode newDllNode = xmldoc.CreateNode(XmlNodeType.Element, "Reference", string.Empty);
            string dll = "framework_crud.dll";
            XmlAttribute newDLLNodeAttribute = xmldoc.CreateAttribute("Include");
            newDLLNodeAttribute.Value = dll;
            newDllNode.Attributes.Append(newDLLNodeAttribute);
            XmlNode pathNode = xmldoc.CreateNode(XmlNodeType.Element, "HintPath", string.Empty);
            pathNode.InnerText = $"lib\\{dll}";
            newDllNode.AppendChild(pathNode);
            itemGroupReferenceNode.AppendChild(newDllNode);

            //TODO add form
            string fileName = "Form2";
            XmlNode newFormNode = xmldoc.CreateNode(XmlNodeType.Element, "Compile", string.Empty);
            XmlAttribute newFormNodeAttribute = xmldoc.CreateAttribute("Include");
            newFormNodeAttribute.Value = $"{fileName}.cs";
            newFormNode.Attributes.Append(newFormNodeAttribute);
            XmlNode Type = xmldoc.CreateNode(XmlNodeType.Element, "SubType", string.Empty);
            Type.InnerText = "Form";
            newFormNode.AppendChild(Type);
            itemGroupCompileNode.AppendChild(newFormNode);

            //Add designer
            XmlNode newFormDesign = xmldoc.CreateNode(XmlNodeType.Element, "Compile", string.Empty);
            XmlAttribute newFormDesignAttribute = xmldoc.CreateAttribute("Include");
            newFormDesignAttribute.Value = $"{fileName}.Designer.cs";
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
