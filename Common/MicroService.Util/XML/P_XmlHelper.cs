using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common
{
    public class P_XmlHelper
    {
        private XmlDocument xmlDoc = new XmlDocument();
        public List<XmlElement> AllElements = new List<XmlElement>();

        public P_XmlHelper(string xmlFile,string rootName)
        {
            xmlDoc.LoadXml(xmlFile);
            RootName = RootName;
            FileName = xmlFile;
        }

        public string RootName { get; set; }
        public string FileName { get; set; }

        /// <summary>
        /// 根据ID获取元素
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public XmlElement[] GetElementByID(string id)
        {
            List<XmlElement> xe = new List<XmlElement>();

            if (AllElements.Count == 0)
            {
                GetALLNode(ref AllElements, xmlDoc.SelectNodes(RootName));
            }

            foreach (XmlElement x in AllElements)
            {
                if (x.GetAttribute("id") == id || x.GetAttribute("Id") == id)
                {
                    xe.Add(x);
                }
            }

            return xe.ToArray();
        }

        public XmlElement[] GetElementByName(string name)
        {
            List<XmlElement> xe = new List<XmlElement>();

            if (AllElements.Count == 0)
            {
                GetALLNode(ref AllElements, xmlDoc.SelectNodes(RootName));
            }

            foreach (XmlElement x in AllElements)
            {
                if (x.GetAttribute("name") == name || x.GetAttribute("Name") == name)
                {
                    xe.Add(x);
                }
            }

            return xe.ToArray();
        }

        public void RemoveElementAllChid(XmlElement element)
        {
            while (element.HasChildNodes)
            {
                element.RemoveChild(element.FirstChild);
            }
        }

        public void GetALLNode(ref List<XmlElement> elements, XmlNodeList lst)
        {
            if (lst == null || lst.Count == 0) return;
            elements.Clear();
            foreach (XmlNode node in lst)
            {
                XmlElement e = node as XmlElement;
                if (e != null)
                {
                    elements.Add(e);
                }
                if (node.HasChildNodes)
                {
                    GetALLNode(ref elements, node.ChildNodes);
                }
            }
        }


        public void DeleteNodeByInnerValue(string nodeStr, string innerValue)
        {
            try
            {
                XmlNodeList nodes = xmlDoc.SelectNodes(nodeStr);
                foreach (XmlNode node in nodes)
                {
                    XmlElement xe = (XmlElement)node;
                    if (xe.InnerText == innerValue)
                    {
                        node.ParentNode.RemoveChild(node);
                        break;//只删除第一个数据
                        // xn.RemoveAll();  
                    }
                }

                Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Delete configure data Error:" + ex.Message);
            }
        }

        public void UpdateNodeInnerValue(string nodeRootPath, string childName, string innerValue)
        {
            XmlNode nodes = xmlDoc.SelectSingleNode(nodeRootPath);
            foreach (XmlNode node in nodes.ChildNodes)
            {
                XmlElement xe = (XmlElement)node;
                if (xe.Name == childName)
                {
                    xe.InnerText = innerValue;
                }
            }
            Save();
        }

        public void DeleteAllNodeByInnerValue(string nodeStr, string innerValue)
        {
            try
            {
                XmlNodeList nodes = xmlDoc.SelectNodes(nodeStr);
                foreach (XmlNode node in nodes)
                {
                    XmlElement xe = (XmlElement)node;
                    if (xe.InnerText == innerValue)
                    {
                        node.ParentNode.RemoveChild(node);
                        // xn.RemoveAll();  
                    }
                }
                Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Delete configure data Error:" + ex.Message);
            }

        }

        //添加xml节点  
        public void AddNode(string nodePath, string nodeName, string innerValue)
        {
            XmlNode root = xmlDoc.SelectSingleNode(nodePath);//查找<images>    
            XmlElement element = xmlDoc.CreateElement(nodeName);
            element.InnerText = innerValue;
            root.AppendChild(element);

            Save();
        }

        public void AppendElementTo(string destElementId, XmlElement newElement)
        {
            XmlElement[] elements = GetElementByID(destElementId);
            foreach (var e in elements)
            {
                e.AppendChild(newElement);
            }
            Save();
        }

        public void Save()
        {
            using (FileStream fs = File.OpenWrite(FileName))
            {
                xmlDoc.Save(fs);
                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
                XmlWriter writer = XmlWriter.Create(fs, settings);
                writer.Flush();
                fs.Flush();
                writer.Dispose();
                fs.Dispose();
            }
        }
    }
}
