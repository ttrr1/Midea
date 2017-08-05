using System;
using System.Xml.Linq;


namespace Sys.Common
{
    public class Error
    {
        public static void CreateError(Exception ex)
        {
            //创建文件存放路径
            var configPath = System.Configuration.ConfigurationManager.AppSettings["errPath"];
            CreateError(ex, configPath);
        }

        public static void CreateError(Exception ex, string configPath)
        {
            var name = DateTime.Now.ToString("yyyyMMddhhmmss");
            var xe = new XElement("errors", new XElement("error", "" + ex.ToString() + ""));
            //创建文件存放路径
            //var configPath = System.Configuration.ConfigurationManager.AppSettings["errPath"];
            var filePath = Utils.CreatePhysicalPath(configPath, Utils.PathFormat.Year_Month);

            //保存XML
            xe.Save(filePath + "/error_" + name + ".xml");

        }
    }
}
