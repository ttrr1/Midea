using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Sys.Common
{
    public class InfoState
    {
        public static XmlNodeList GetXmlNodeList(string path, string node)
        {
            XmlNodeList xnl=null;
            try
            {
                xnl = XMLProcess.ReadAllChild(path, node);
            }
            catch (Exception ex)
            {
                Error.CreateError(ex);
            }
            return xnl;
        }

        public static string GetXmlInfo(XmlNodeList xnl, string checkValue)
        {
            //XmlNodeList xnl = XmlInfo;

            string checkInfo = "";
            if (xnl == null)
                return "";

            foreach (XmlNode xn in xnl)
            {
                if (xn.NodeType != XmlNodeType.Element)
                    continue;

                if (xn.Name.ToLower()=="item" && xn.Attributes["value"].Value == checkValue)
                {
                    checkInfo = xn.InnerXml;
                    break;
                }
            }
            return checkInfo;

        }
    }
}
