using System;
using System.Configuration;
using System.Xml;
using System.Web;
using sz71096.Common;

namespace Sys.Common
{
    /// <summary>
    /// web.config������
    /// </summary>
    public sealed class ConfigHelper
    {
        public static void SetAppConfig(string AppKey, string AppValue)
        {
            XmlDocument xDoc = new XmlDocument();
            //��ȡ��ִ���ļ���·��������
            xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");

            XmlNode xNode;
            XmlElement xElem1;
            XmlElement xElem2;
            xNode = xDoc.SelectSingleNode("//appSettings");

            xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
            if (xElem1 != null) xElem1.SetAttribute("value", AppValue);
            else
            {
                xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", AppKey);
                xElem2.SetAttribute("value", AppValue);
                xNode.AppendChild(xElem2);
            }
            xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");
        }
        public static void SetWebConfig(string key, string strValue)
        {
            string XPath = "/configuration/appSettings/add[@key='?']";
            var domWebConfig = new XmlDocument();

            domWebConfig.Load((HttpContext.Current.Server.MapPath("~/web.config")));
            XmlNode addKey = domWebConfig.SelectSingleNode((XPath.Replace("?", key)));
            if (addKey == null)
            {
                throw new ArgumentException("û���ҵ�<add key='" + key + "' value=/>�����ý�");
            }
            addKey.Attributes["value"].InnerText = strValue;
            domWebConfig.Save((HttpContext.Current.Server.MapPath("~/web.config")));

        }

        /// <summary>
        /// �õ�AppSettings�е������ַ�����Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key)
        {
            string CacheKey = "AppSettings-" + key;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = ConfigurationManager.AppSettings[key];//web.config��ȡ
                    //if (objModel == null)
                    //    objModel = (object)GetDBConfigString(key);//���ݿ��ȡ

                    if (objModel != null)
                    {
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(180), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return objModel.ToString();
        }

        /// <summary>
        /// �õ�AppSettings�е�����Bool��Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetConfigBool(string key)
        {
            bool result = false;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }
            return result;
        }
        /// <summary>
        /// �õ�AppSettings�е�����Decimal��Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetConfigDecimal(string key)
        {
            decimal result = 0;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
        /// <summary>
        /// �õ�AppSettings�е�����int��Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetConfigInt(string key)
        {
            int result = 0;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }

        /// <summary>
        /// �õ�AppSettings�е�����int��Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static int GetConfigInt(string key, int defValue)
        {
            int result = 0;
            string cfgVal = GetConfigString(key);
            result = Utils.StrToInt(cfgVal, defValue);
            return result;
        }

    }
}
