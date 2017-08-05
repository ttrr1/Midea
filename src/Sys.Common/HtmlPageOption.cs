using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Collections;

namespace Sys.Common
{

    public class HtmlPageOption
    {
        public static void GetPageHtml(string url, string filePath, System.Web.UI.HtmlControls.HtmlGenericControl DynamicNamePrompt)
        {
            String Result="";
            WebResponse MyResponse;
            try
            {
                WebRequest MyRequest = System.Net.HttpWebRequest.Create(url);
                MyResponse = MyRequest.GetResponse(); //读取网址的内容

                using (StreamReader MyReader = new StreamReader(MyResponse.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    Result = MyReader.ReadToEnd();
                    MyReader.Close();
                }
                FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.WriteLine(Result);

                sw.Close();
                fs.Close();
                DynamicNamePrompt.InnerHtml = "生成静态页面成功";
   
            }
            catch (Exception e)
            {
                //DynamicNamePrompt.InnerHtml = "系统出错，请重试";
                throw e;
            }
            
        }

        /// <summary>
        /// 插入xml节点，并生成html静态页面
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        /// <param name="Mark"></param>
        /// <param name="DynamicNamePrompt"></param>
        /// <returns></returns>
        public static string GetPageHtmlWithXML(string url, string filePath, string Mark, System.Web.UI.HtmlControls.HtmlGenericControl DynamicNamePrompt)
        {
            String Result;
            WebResponse MyResponse;
            try
            {
                WebRequest MyRequest = System.Net.HttpWebRequest.Create(url);
                MyResponse = MyRequest.GetResponse(); //读取网址的内容

                Hashtable ht = new Hashtable();
                ht.Add("value", url);
                ht.Add("url", filePath);
                ht.Add("Mark", Mark);
                XMLProcess.InsertHash("Config/Template.xml", "/template/system", "mode", ht);

                using (var MyReader = new StreamReader(MyResponse.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    Result = MyReader.ReadToEnd();
                    MyReader.Close();
                }
                var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                var sw = new StreamWriter(fs, Encoding.UTF8);
                sw.WriteLine(Result);
                sw.Close();
                fs.Close();
                DynamicNamePrompt.InnerHtml = "生成静态页面成功";

                return Result;
            }
            catch (Exception e)
            {
                DynamicNamePrompt.InnerHtml = "系统出错，请重试";
                return null;
            }
        }

        /// <summary>
        /// 更新静态页
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        /// <param name="DynamicNamePrompt"></param>
        public static void HtmlPageUpdate(string url, string filePath, System.Web.UI.HtmlControls.HtmlGenericControl DynamicNamePrompt)
        {
            String Result;
            WebResponse MyResponse;
            try
            {
                FileInfo fiExit = new FileInfo(filePath);

                WebRequest MyRequest = System.Net.HttpWebRequest.Create(url);
                MyResponse = MyRequest.GetResponse();

                using (StreamReader MyReader = new StreamReader(MyResponse.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    Result = MyReader.ReadToEnd();
                    MyReader.Close();
                }

                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);//创建或打开流
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.WriteLine(Result);

                sw.Close();
                sw.Dispose();

                fs.Close();
                fs.Dispose();

                DynamicNamePrompt.InnerHtml = "更新静态页面成功";

            }
            catch (Exception e)
            {
                DynamicNamePrompt.InnerHtml = "系统出错，请重试";
            }
        }

        /// <summary>
        /// 更新html静态页面的内容,并用XML节点记录
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        /// <param name="DynamicNamePrompt"></param>
        public static void HtmlPageUpdateWithXML(string url, string filePath, System.Web.UI.HtmlControls.HtmlGenericControl DynamicNamePrompt)
        {
            String Result;
            WebResponse MyResponse;
            try
            {
                var fiExit = new FileInfo(filePath);

                var UrlResult = XMLProcess.Read("Config/Template.xml", "/template/system/mode[@value='" + url + "']", "url");//查询有没有读取到这个xml节点

                if (!string.IsNullOrEmpty(UrlResult)) //向 XML中 读取mode节点 //假如读取到值
                {
                    var MyRequest = WebRequest.Create(url);
                    MyResponse = MyRequest.GetResponse();

                    using (var MyReader = new StreamReader(MyResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        Result = MyReader.ReadToEnd();
                        MyReader.Close();
                    }

                    var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);//创建或打开流
                    var sw = new StreamWriter(fs, Encoding.UTF8);
                    sw.WriteLine(Result);

                    sw.Dispose();
                    sw.Close();
                    fs.Dispose();
                    fs.Close();

                    DynamicNamePrompt.InnerHtml = "更新静态页面成功";

                }
                else
                {
                    DynamicNamePrompt.InnerHtml = "您要更新的节点不存在";
                    fiExit.Delete(); //把存在的文件删除
                }

            }
            catch (Exception e)
            {
                DynamicNamePrompt.InnerHtml = "系统出错，请重试";

            }
        }


        


        //新闻生成静态页部分
        public static string SendRequest(string uri)
        {
            string responseText = "";
            StreamReader reader = null;
            HttpWebRequest request;
            HttpWebResponse response = null;

            //为指定的URI方案初始化新的WebRequest实例
            request = (HttpWebRequest)WebRequest.Create(uri);

            //返回来自Internet资源的响应
            if (request != null)
                response = (HttpWebResponse)request.GetResponse();

            //用指定的字符编码为指定的流初始化StreamReader类的一个新实例
            if (response != null)
                reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8"));

            if (reader != null)
                responseText = reader.ReadToEnd();

            //关闭相关对象
            response.Close();
            reader.Close();

            return responseText;
        }
    }
}
