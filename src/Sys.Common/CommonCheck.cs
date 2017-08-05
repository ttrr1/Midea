using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sz71096.Common
{
    public class CommonCheck
    {
        /// <summary>
        /// common类的成员函数:检查一个网页接收到的从客户端传来的request.query值是否有sql注入等非法字符 
        /// </summary>
        public static void CheckRequestQuery()
        {
            int i, j;
            string str1;

            //定义get非法参数,字符串数组 
            string[] Badword = { ":", ";", ">", "<", "--", "sp_", "xp_", "\\", "dir", "cmd", "^", "(", ")", "+", "$", "'", "*", "copy", "format", "and", "exec", "insert", "select", "delete", "update", "count", "%", "chr", "mid", "master", "truncate", "char", "declare", "or", "<script", ".js", "<iframe", "<frameset", "</iframe", "</frameset>", "exist" };
            //-----对get传来值的过滤,如果get传来的值不为空.
            if (System.Web.HttpContext.Current.Request.QueryString.ToString() != "")
            {
                for (i = 0; i < System.Web.HttpContext.Current.Request.QueryString.Count; i++)
                {
                    //获取每一个用&分隔的get传来的值
                    str1 = System.Web.HttpContext.Current.Request.QueryString.GetValues(i)[0].ToString().ToLower();
                    for (j = 0; j < Badword.Length; j++) //分析str1有无非法参数数组中的值
                    {
                        if (str1.IndexOf(Badword[j]) >= 0)
                        {
                            System.Web.HttpContext.Current.Response.Write("<font color='#ff0000'>有get传来非法字符</font>");
                            System.Web.HttpContext.Current.Response.End();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// common类的成员函数:检查一个网页接收到的从客户端传来的request.form表单值是否有非法字符
        /// </summary>
        public static void CheckRequestForm()
        {
            int i, j;
            string str1;

            //定义post非法参数,字符串数组  
            //string[] Badword ={ ":", ";", ">", "<", "--", "sp_", "xp_", "\\", "dir", "cmd", "^", "(", ")", "+", "$","'", "copy", "format", "and", "exec", "insert", "select", "delete", "update", "count", "*", "%", "chr", "mid", "master", "truncate", "char", "declare", "or", "<script", ".js", "<iframe", "<frameset","</iframe","</frameset>" };
            string[] Badword = { "sp_", "xp_", "\\", "dir", "cmd", "'", "^", "$", "copy", "creat", "format", "exec", "insert", "select", "delete", "update", "count", "chr", "mid", "master", "truncate", "char", "declare", "script", ".js", "<iframe", "<frameset", "</iframe", "</frameset>" };

            //-----对post传来值的过滤,如果post传来的值不为空.
            if (System.Web.HttpContext.Current.Request.Form.ToString() != "")
            {
                for (i = 0; i < System.Web.HttpContext.Current.Request.Form.Count; i++)
                {
                    //获取每一个用&分隔的get传来的值
                    str1 = System.Web.HttpContext.Current.Request.Form[i].ToString().ToLower();
                    //Response.Write(str1 + "<br>");

                    for (j = 0; j < Badword.Length; j++) //分析str1有无非法参数数组中的数组元素
                    {
                        if (str1.IndexOf(Badword[j]) >= 0)
                        {
                            System.Web.HttpContext.Current.Response.Write("<font color='#ff0000'>有post传来非法字符</font>");
                            System.Web.HttpContext.Current.Response.End();
                        }
                    }
                }
            }
        }



        /// <summary>
        /// 页面访问安全性
        /// Error1:恶意访问测试
        /// Error2:站外提交
        /// HTTP_REFERER,本网页的前一个网页的地址
        /// SERVER_NAME，即主机名或IP地址
        /// SERVER_PORT，即为 80
        /// SCRIPT_NAME，主机名除外的网页地址
        /// </summary>
        public static void CheckRequestPage2()
        {

            string comeurl = "", curl = "";

            //得到本网页的前一个网页的地址
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"] == null)
            {
                System.Web.HttpContext.Current.Response.Write("<br><p align=center><font color='red'>Error1</font></p>");
                System.Web.HttpContext.Current.Response.Write("<script >window.location.href='/Users/Login.aspx'</script>");
                System.Web.HttpContext.Current.Response.End();
            }
            else
            {
                //获得本网页的前一个网页的地址
                comeurl = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"].Trim();
            }





            //1.获取当前URL
            /*
            curl = ("http://" + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"]).Trim();

            //网页是否有端口号":"
            if (comeurl.Substring(curl.Length, 1) == ":")
                curl = curl + ":" + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];

            //取当前网页的网页纯文件名+服务器名和端口号构成完整的当前网页网页地址名
            curl = curl + System.Web.HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
            */

            //2.上面的获取当前URL方式完全可以简化为
            curl = System.Web.HttpContext.Current.Request.Url.ToString();

            //此处为验证两个地址主机名称是否相同，因此应该是取[主机名称+端口号]即可
            string leftcomeurl = comeurl.Substring(0, comeurl.IndexOf("/") + 1);
            string leftcurl = curl.Substring(0, curl.IndexOf("/") + 1);

            if (!comeurl.Contains("71096.com") || comeurl=="")
            {
                System.Web.HttpContext.Current.Response.Write("<br><p align=center><font color='red'>Error2</font></p>");
                System.Web.HttpContext.Current.Response.End();

            }

            //不相同则为站外提交
            if (leftcomeurl != leftcurl)
            {
                System.Web.HttpContext.Current.Response.Write("<br><p align=center><font color='red'>Error2</font></p>");
                System.Web.HttpContext.Current.Response.End();
            }

        }

        /// <summary>
        /// 页面访问安全性
        /// Error1:恶意访问测试
        /// Error2:站外提交
        /// HTTP_REFERER,本网页的前一个网页的地址
        /// SERVER_NAME，即主机名或IP地址
        /// SERVER_PORT，即为 80
        /// SCRIPT_NAME，主机名除外的网页地址
        /// </summary>
        public static void CheckRequestPage()
        {
            string comeurl = "", curl = "";

            //得到本网页的前一个网页的地址
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"] == null)
            {
                System.Web.HttpContext.Current.Response.Write("<br><p align=center><font color='red'>Error1</font></p>");
                System.Web.HttpContext.Current.Response.Write("<script >window.location.href='/Users/Login.aspx'</script>");
                System.Web.HttpContext.Current.Response.End();
            }
            else
            {
                //获得本网页的前一个网页的地址
                comeurl = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"].Trim();
            }

            //1.获取当前URL
            /*
            curl = ("http://" + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"]).Trim();

            //网页是否有端口号":"
            if (comeurl.Substring(curl.Length, 1) == ":")
                curl = curl + ":" + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];

            //取当前网页的网页纯文件名+服务器名和端口号构成完整的当前网页网页地址名
            curl = curl + System.Web.HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
            */

            //2.上面的获取当前URL方式完全可以简化为
            curl = System.Web.HttpContext.Current.Request.Url.ToString();

            //此处为验证两个地址主机名称是否相同，因此应该是取[主机名称+端口号]即可
            string leftcomeurl = comeurl.Substring(0, comeurl.IndexOf("/") + 1);
            string leftcurl = curl.Substring(0, curl.IndexOf("/") + 1);

            //不相同则为站外提交
            if (leftcomeurl != leftcurl)
            {
                System.Web.HttpContext.Current.Response.Write("<br><p align=center><font color='red'>Error2</font></p>");
                System.Web.HttpContext.Current.Response.End();
            }

        }


    }
}
