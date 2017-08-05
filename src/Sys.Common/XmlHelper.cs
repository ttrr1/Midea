using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Sys.Common
{
    public sealed class XmlHelper
    {
        private XmlHelper()
        {

        }

        public static XmlDocument GetXmlDocument(string xmlFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);
            return xmlDoc;
        }

        public static XmlNodeList GetXmlNodeList(XmlDocument xmlDoc, string nodeName)
        {
            return xmlDoc.SelectNodes(nodeName);
        }

        public static XmlNode GetXmlNode(XmlNodeList xmlNodeList, string attributeName, string attributeValue)
        {
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (xmlNode.Attributes[attributeName].Value == attributeValue)
                {
                    return xmlNode;
                }
            }
            return null;
        }

        public static void UpdateXmlNodeAttributeValue(XmlNode xmlNode, string attributeName, string attributeValue)
        {
            xmlNode.Attributes[attributeName].Value = attributeValue;
        }

        public static void SaveXmlDocument(XmlDocument xmlDoc, string xmlFile)
        {
            xmlDoc.Save(xmlFile);
        }

        public static XmlNode GetXmlNode(string xmlFile, string nodeName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);
            return xmlDoc.SelectSingleNode(nodeName);
        }

        public static XmlNode GetXmlNode(XmlNode xmlNode, string nodeName)
        {
            return xmlNode.SelectSingleNode(nodeName);
        }

        public static XmlNodeList GetXmlNodeList(string xmlFile, string nodeName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);
            return xmlDoc.SelectNodes(nodeName);
        }

        public static XmlNodeList GetXmlNodeList(XmlNode xmlNode, string nodeName)
        {
            return xmlNode.SelectNodes(nodeName);
        }

        public static string GetXmlNodeAttributeValue(XmlNode xmlNode, string attributeName)
        {
            return xmlNode.Attributes[attributeName].Value;
        }

        public static string GetXmlNodeValue(XmlNode xmlNode)
        {
            return xmlNode.InnerText;
        }
    }
}
