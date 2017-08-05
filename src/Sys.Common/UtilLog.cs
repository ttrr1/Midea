using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace Sys.Common
{
    /// <summary>
    /// Utility Log Handle Class
    /// </summary>
    public sealed class UtilLog
    {
        /// <summary>  
        /// 写入日志到文本文件  
        /// </summary>  
        /// <param name="action">动作</param>  
        /// <param name="strMessage">日志内容</param>  
        /// <param name="time">时间</param>  
        public static void WriteTextLog(string action, string strMessage, DateTime? time=null)
        {
            time = time ?? DateTime.Now;
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (System.Configuration.ConfigurationManager.AppSettings["logPath"] != "")
            {
                path += System.Configuration.ConfigurationManager.AppSettings["logPath"];
            }
            path += @"ApplicationLogs\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileFullPath = path + "Logs." + time.Value.ToString("yyyy-MM-dd") + ".txt";
            StringBuilder str = new StringBuilder();
            str.Append(time + ",");
            str.Append(action + ",");
            str.Append(strMessage + ",");
            StreamWriter sw = null;
            try
            {
                if (!File.Exists(fileFullPath))
                {
                    sw = File.CreateText(fileFullPath);
                }
                else
                {
                    sw = File.AppendText(fileFullPath);
                }
                sw.WriteLine(str.ToString());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
            
            
        }

        /// <summary>  
        /// 写入日志到文本文件  
        /// </summary>  
        /// <param name="action">动作</param>  
        /// <param name="ex">异常信息</param>  
        /// <param name="time">时间</param>  
        public static void WriteExceptionLog(string action, Exception ex, DateTime? time = null)
        {
            time = time ?? DateTime.Now;
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (System.Configuration.ConfigurationManager.AppSettings["logPath"] != "")
            {
                path += System.Configuration.ConfigurationManager.AppSettings["logPath"];
            }
            path += @"ApplicationLogs\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileFullPath = path + "Logs." + time.Value.ToString("yyyy-MM-dd") + ".txt";
            StringBuilder str = new StringBuilder();
            str.Append("Time:    " + time + "\r\n");
            str.Append("Action:  " + action + "\r\n");
            str.Append("Source: " + ex.Source + "\r\n");
            str.Append("Message: " + ex.Message + "\r\n");
            str.Append("StackTrace: " + ex.StackTrace + "\r\n");
            str.Append("-----------------------------------------------------------\r\n");
            StreamWriter sw = null;
            try
            {
                if (!File.Exists(fileFullPath))
                {
                    sw = File.CreateText(fileFullPath);
                }
                else
                {
                    sw = File.AppendText(fileFullPath);
                }
                sw.WriteLine(str.ToString());
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }


        }

    }
}
