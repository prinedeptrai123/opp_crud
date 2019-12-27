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

            //TODO add dll



            //TODO add dll
            //XmlNode linkNode = xmldoc.CreateNode(XmlNodeType.Element, "Link", string.Empty);
            //linkNode.InnerText = "CommonFiltsfsfsfsfsfseInfo.cs";

            //newCompileNode.AppendChild(linkNode);
            // itemGroupNode.AppendChild(newCompileNode);

            Console.WriteLine(xmldoc.OuterXml);

            xmldoc.Save(path);

            //foreach (XmlNode item in xmldoc.SelectNodes("//x:ProjectGuid", mgr))
            //{
            //    string test = item.InnerText.ToString();
            //    Console.WriteLine(test);
            //}

        }
    }
}
