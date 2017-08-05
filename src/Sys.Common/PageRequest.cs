using System;
using System.Web;


namespace Sys.Common
{
    /// <summary>
    /// Request操作类
    /// </summary>
    public class PageRequest
    {

        public PageRequest()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 分页显示
        /// </summary>
        /// <param name="url">A标签页面链接地址</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageCounts">总页码</param>
        /// <returns></returns>
        public static string ShowSplitPage(string url, int pageIndex, int PageSize, int PageCS)
        {
            var str = "";
            var pageCounts = 0;//总页数

            if (PageCS % PageSize == 0)
                pageCounts = PageCS / PageSize;
            else
                pageCounts = PageCS / PageSize + 1;


            if (pageCounts <= 1)
            {
                return str;
            }


            var start = (pageIndex - 4) > 0 ? (pageIndex - 4) : 1;
            var end = (pageIndex + 4) < pageCounts ? (pageIndex + 4) : pageCounts;
            if ((pageIndex - 1) < 4)
            {
                start = 1;
                end = (start + 8) < pageCounts ? (start + 8) : pageCounts;
            }
            if ((pageCounts - pageIndex) < 4)
            {
                end = pageCounts;
                start = (end - 9) > 0 ? (end - 9) : 1;
            }
            str += "<div class='pagesplit'><ul>";
            if (pageIndex != 1)
                str += @"<li class=""pg_first""><a href='" + url.Replace("{0}", "1") + @"'><img align='absmiddle' src='../images/Pic/Icons/arrow/arrow_first.gif'>&nbsp;首页</a>&nbsp;</li><li class=""pg_prve""><a href='" + url.Replace("{0}", (pageIndex - 1).ToString()) + "'><img align='absmiddle' src='../images/Pic/Icons/arrow/arrow_prev.gif'>&nbsp;上一页</a>&nbsp;</li>";
            for (var i = start; i <= end; i++)
            {
                if (i == pageIndex)
                    str += @"<li class=""curr-page"">" + i + "</li>";
                else
                    str += @"<li><a href='" + url.Replace("{0}", i.ToString()) + "'>" + i + "</a></li>";
            }
            if (pageIndex < pageCounts)
            {
                str += @"<li class=""pg_next"">&nbsp;<a href='" + url.Replace("{0}", (pageIndex + 1).ToString()) + "'><img align='absmiddle' src='../images/Pic/Icons/arrow/arrow_next.gif'>&nbsp;下一页</a></li>";
                str += @"<li class=""pg_next"">&nbsp;<a href='" + url.Replace("{0}", (pageCounts).ToString()) + "'><img align='absmiddle' src='../images/Pic/Icons/arrow/arrow_last.gif'>&nbsp;末页</a></li>";
            }



            str += "</ul></div>";
            str += string.Format(
                 "</ul><div style='float: right; margin-right:30px'>当前页：<select name='pageselector' id='pageselector'  onchange=\"window.location.href=this.value\" >");
            for (int i = 1; i < pageCounts + 1; i++)
            {
                if (i >= pageIndex - 50 && i <= pageIndex + 50)
                {
                    if (pageIndex == i)
                    {
                        str += string.Format("<option selected='selected' value='{0}'>{1} / {2}</option>", url.Replace("{0}", (i).ToString()), i, pageCounts);
                    }
                    else
                    {
                        str += string.Format("<option value='{0}'>{1} / {2}</option>", url.Replace("{0}", (i).ToString()), i, pageCounts);
                    }
                }
            }

            str += "</select></div></div>";
            return str;
        }


        /// <summary>
        /// 分页显示
        /// </summary>
        /// <param name="url"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageCS"></param>
        /// <returns></returns>
        public static string QShowSplitPage(string url, int PageIndex, int PageSize, int PageCS)
        {

            string str = "";
            int PageCounts = 0;//总页数

            if (PageCS % PageSize == 0)
                PageCounts = PageCS / PageSize;
            else
                PageCounts = PageCS / PageSize + 1;


            if (PageCounts <= 1)
            {
                return str;
            }

            int start = (PageIndex - 4) > 0 ? (PageIndex - 4) : 1;
            int end = (PageIndex + 4) < PageCounts ? (PageIndex + 4) : PageCounts;
            if ((PageIndex - 1) < 4)
            {
                start = 1;
                end = (start + 8) < PageCounts ? (start + 8) : PageCounts;
            }
            if ((PageCounts - PageIndex) < 4)
            {
                end = PageCounts;
                start = (end - 9) > 0 ? (end - 9) : 1;
            }
            str += "<div class='page'><span><strong>共" + PageCounts + "页</strong></span>";
            if (PageIndex != 1)
                str += @"<a  href='" + url.Replace("{0}", "1") + @"'>首页</a>&nbsp;<a href='" + url.Replace("{0}", (PageIndex - 1).ToString()) + "'>上一页</a>";
            for (int i = start; i <= end; i++)
            {
                if (i == PageIndex)
                    str += @"<a class='current' href='#'>" + i.ToString() + "</a>";
                else
                    str += @"<a href='" + url.Replace("{0}", i.ToString()) + "'>" + i.ToString() + "</a>";
            }
            if (PageIndex < PageCounts)
            {
                str += @"<a href='" + url.Replace("{0}", (PageIndex + 1).ToString()) + "'>下一页</a>";
                str += @"<a href='" + url.Replace("{0}", (PageCounts).ToString()) + "'>末页</a>";
            }

            str += "</div>";
            return str;
        }


        /// <summary>
        /// 分页显示
        /// </summary>
        /// <param name="url"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageCS"></param>
        /// <returns></returns>
        public static string AjaxShowSplitPage(string url, int PageIndex, int PageSize, int PageCS)
        {

            string str = "";
            int pageCounts = 0;//总页数

            if (PageCS % PageSize == 0)
                pageCounts = PageCS / PageSize;
            else
                pageCounts = PageCS / PageSize + 1;


            if (pageCounts <= 1)
            {
                return str;
            }

            int start = (PageIndex - 4) > 0 ? (PageIndex - 4) : 1;
            int end = (PageIndex + 4) < pageCounts ? (PageIndex + 4) : pageCounts;
            if ((PageIndex - 1) < 4)
            {
                start = 1;
                end = (start + 8) < pageCounts ? (start + 8) : pageCounts;
            }
            if ((pageCounts - PageIndex) < 4)
            {
                end = pageCounts;
                start = (end - 9) > 0 ? (end - 9) : 1;
            }
            str += "<div class='page'><span><strong>共" + pageCounts + "页</strong></span>";
            if (PageIndex != 1)
                str += "<a style=\"cursor:pointer\" onclick=\"NextList('1');\">首页</a>&nbsp;<a style=\"cursor:pointer\" onclick=\"NextList('" + (PageIndex - 1) + "');\">上一页</a>";
            for (int i = start; i <= end; i++)
            {
                if (i == PageIndex)
                    str += "<a  style=\"cursor:pointer\" class='current' onclick=\"NextList('" + (i) + "');\">" + i + "</a>";
                else
                    str += "<a style=\"cursor:pointer\" onclick=\"NextList('" + (i) + "');\">" + i.ToString() + "</a>";
            }
            if (PageIndex < pageCounts)
            {
                str += "<a style=\"cursor:pointer\" onclick=\"NextList('" + (PageIndex + 1) + "');\">下一页</a>";
                str += "<a style=\"cursor:pointer\" onclick=\"NextList('" + (pageCounts) + "');\">末页</a>";
            }

            str += "</div>";
            return str;
        }



        /// <summary>
        /// 分页显示
        /// </summary>
        /// <param name="url"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageCS"></param>
        /// <returns></returns>
        public static string AjaxShowSplitPage2(string url, int PageIndex, int PageSize, int PageCS)
        {

            string str = "";
            int pageCounts = 0;//总页数

            if (PageCS % PageSize == 0)
                pageCounts = PageCS / PageSize;
            else
                pageCounts = PageCS / PageSize + 1;


            if (pageCounts <= 1)
            {
                return str;
            }

            int start = (PageIndex - 4) > 0 ? (PageIndex - 4) : 1;
            int end = (PageIndex + 4) < pageCounts ? (PageIndex + 4) : pageCounts;
            if ((PageIndex - 1) < 4)
            {
                start = 1;
                end = (start + 8) < pageCounts ? (start + 8) : pageCounts;
            }
            if ((pageCounts - PageIndex) < 4)
            {
                end = pageCounts;
                start = (end - 9) > 0 ? (end - 9) : 1;
            }
            str += "<div class='page'><span><strong>共" + pageCounts + "页</strong></span>";
            if (PageIndex != 1)
                str += "<a style=\"cursor:pointer\" onclick=\"NextList2('1');\">首页</a>&nbsp;<a style=\"cursor:pointer\" onclick=\"NextList2('" + (PageIndex - 1) + "');\">上一页</a>";
            for (int i = start; i <= end; i++)
            {
                if (i == PageIndex)
                    str += "<a  style=\"cursor:pointer\" class='current' onclick=\"NextList2('" + (i) + "');\">" + i + "</a>";
                else
                    str += "<a style=\"cursor:pointer\" onclick=\"NextList2('" + (i) + "');\">" + i.ToString() + "</a>";
            }
            if (PageIndex < pageCounts)
            {
                str += "<a style=\"cursor:pointer\" onclick=\"NextList2('" + (PageIndex + 1) + "');\">下一页</a>";
                str += "<a style=\"cursor:pointer\" onclick=\"NextList2('" + (pageCounts) + "');\">末页</a>";
            }

            str += "</div>";
            return str;
        }







        //public static string QShowSplitPage(string url, int pageIndex, int PageSize, int PageCS)
        //{

        //    string str = "";
        //    int PageCounts = 0;//总页数

        //    if (PageCS % PageSize == 0)
        //        PageCounts = PageCS / PageSize;
        //    else
        //        PageCounts = PageCS / PageSize + 1;


        //    if (PageCounts <= 1)
        //    {
        //        return str;
        //    }


        //    int start = (pageIndex - 4) > 0 ? (pageIndex - 4) : 1;
        //    int end = (pageIndex + 4) < PageCounts ? (pageIndex + 4) : PageCounts;
        //    if ((pageIndex - 1) < 4)
        //    {
        //        start = 1;
        //        end = (start + 8) < PageCounts ? (start + 8) : PageCounts;
        //    }
        //    if ((PageCounts - pageIndex) < 4)
        //    {
        //        end = PageCounts;
        //        start = (end - 9) > 0 ? (end - 9) : 1;
        //    }
        //    str += "<div class='pagesplit'><ul>";
        //    if (pageIndex != 1)
        //        str += @"<li class=""pg_first""><a href='" + url.Replace("{0}", "1") + @"'><img align='absmiddle' src='images/Pic/Icons/arrow/arrow_first.gif'>&nbsp;首页</a>&nbsp;</li><li class=""pg_prve""><a href='" + url.Replace("{0}", (pageIndex - 1).ToString()) + "'><img align='absmiddle' src='images/Pic/Icons/arrow/arrow_prev.gif'>&nbsp;上一页</a>&nbsp;</li>";
        //    for (int i = start; i <= end; i++)
        //    {
        //        if (i == pageIndex)
        //            str += @"<li class=""curr-page"">" + i + "</li>";
        //        else
        //            str += @"<li><a href='" + url.Replace("{0}", i.ToString()) + "'>" + i + "</a></li>";
        //    }
        //    if (pageIndex < PageCounts)
        //    {
        //        str += @"<li class=""pg_next"">&nbsp;<a href='" + url.Replace("{0}", (pageIndex + 1).ToString()) + "'><img align='absmiddle' src='images/Pic/Icons/arrow/arrow_next.gif'>&nbsp;下一页</a></li>";
        //        str += @"<li class=""pg_next"">&nbsp;<a href='" + url.Replace("{0}", (PageCounts).ToString()) + "'><img align='absmiddle' src='images/Pic/Icons/arrow/arrow_last.gif'>&nbsp;末页</a></li>";
        //    }



        //    str += "</ul></div>";
        //    /*   str += string.Format(
        //           "</ul><div style='float: right; margin-right:30px'>当前页：<select name='pageselector' id='pageselector'  onchange=\"window.location.href=this.value\" >");
        //       for (int i = 1; i < pageCounts + 1; i++)
        //       {
        //           if (i >= pageIndex - 50 && i <= pageIndex + 50)
        //           {
        //               if (pageIndex == i)
        //               {
        //                   str += string.Format("<option selected='selected' value='{0}'>{1} / {2}</option>", url.Replace("{0}", (i).ToString()), i, pageCounts);
        //               }
        //               else
        //               {
        //                   str += string.Format("<option value='{0}'>{1} / {2}</option>", url.Replace("{0}", (i).ToString()), i, pageCounts);
        //               }
        //           }
        //       }

        //       str += "</select></div></div>";* */
        //    return str;
        //}


        /// <summary>
        /// 页面sucmsg请求参数值
        /// </summary>
        /// <returns></returns>
        public static string PageSucMsg()
        {
            return PageRequest.GetString("sucmsg").Trim();
        }

        /// <summary>
        /// 页面errmsg请求参数值
        /// </summary>
        /// <returns></returns>
        public static string PageErrMsg()
        {
            return PageRequest.GetString("errmsg").Trim();
        }


        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }
        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        /// <summary>
        /// 返回指定的服务器变量信息
        /// </summary>
        /// <param name="strName">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        public static string GetServerString(string strName)
        {
            //
            if (HttpContext.Current.Request.ServerVariables[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.ServerVariables[strName].ToString();
        }

        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;

            try
            {
                retVal = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch { }

            if (retVal == null)
                return "";

            return retVal;

        }

        /// <summary>
        /// 得到当前完整主机头
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost()
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            if (!request.Url.IsDefaultPort)
            {
                return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
            }
            return request.Url.Host;
        }

        /// <summary>
        /// 得到主机头
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }


        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
        /// </summary>
        /// <returns>原始 URL</returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        /// <summary>
        /// 获取当前请求的原始 完成URL(包括查询字符串(如果存在))
        /// </summary>
        /// <returns>完整 URL</returns>
        public static string GetFullUrl()
        {
            return "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.RawUrl;
        }

        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns>当前访问是否来自浏览器软件</returns>
        public static bool IsBrowserGet()
        {
            string[] BrowserName = { "ie", "opera", "netscape", "mozilla" };
            string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns>是否来自搜索引擎链接</returns>
        public static bool IsSearchEnginesGet()
        {
            string[] SearchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom" };
            string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
            for (int i = 0; i < SearchEngine.Length; i++)
            {
                if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }


        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            return HttpContext.Current.Request.QueryString[strName] == null ? "" : HttpContext.Current.Request.QueryString[strName].Trim();
        }

        public static string GetQueryString()
        {
            return HttpContext.Current.Request.QueryString.ToString();
        }

        /// <summary>
        /// 获得当前页面的名称
        /// </summary>
        /// <returns>当前页面的名称</returns>
        public static string GetPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }

        /// <summary>
        /// 返回表单或Url参数的总个数
        /// </summary>
        /// <returns></returns>
        public static int GetParamCount()
        {
            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
        }


        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.Form[strName].Trim();
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName)
        {
            if ("".Equals(GetQueryString(strName)))
            {
                return GetFormString(strName);
            }
            else
            {
                return GetQueryString(strName);
            }
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数（获得安全的GetString值）</param>
        /// <param name="isHtml">是否是HTML（获得安全的GetString值）</param>
        /// <returns>Url或表单参数的值（获得安全的GetString值）</returns>
        public static string GetString(string strName, bool isHtml)
        {
            var strVal = "";
            strVal = "".Equals(GetQueryString(strName)) ? GetFormString(strName) : GetQueryString(strName);
            //脏字
            strVal = Utils.FilterStr(strVal);

            if (!isHtml)//用HTMLEncode是安全的
            {
                strVal = Utils.HtmlEncodeFull(strVal);
            }
            else//防止XSS入侵
            {
                strVal = Utils.FilterXss(strVal);
                if (strVal.Length > 0)
                {
                    var textArray1 = Utils.SplitString(strVal, "<p>");
                    if (textArray1.Length == 2)
                    {
                        strVal = strVal.Substring(3, strVal.Length - 7);
                    }
                }
            }

            return strVal;
        }


        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName, int defValue)
        {
            return Utils.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
        }


        /// <summary>
        /// 获得指定表单参数的int类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的int类型值</returns>
        public static int GetFormInt(string strName, int defValue)
        {
            return Utils.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
        }

        /// <summary>
        /// 获得指定Url或表单参数的int类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static int GetInt(string strName, int defValue)
        {
            return GetQueryInt(strName, defValue) == defValue ? GetFormInt(strName, defValue) : GetQueryInt(strName, defValue);
        }

        /// <summary>
        /// 获得指定Url参数的long类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的long类型值</returns>
        public static long GetQueryLong(string strName, long defValue)
        {
            return Utils.StrToLong(HttpContext.Current.Request.QueryString[strName], defValue);
        }


        /// <summary>
        /// 获得指定表单参数的long类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的long类型值</returns>
        public static long GetFormLong(string strName, long defValue)
        {
            return Utils.StrToLong(HttpContext.Current.Request.Form[strName], defValue);
        }

        /// <summary>
        /// 获得指定Url或表单参数的long类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的long类型值</returns>
        public static long GetLong(string strName, long defValue)
        {
            return GetQueryLong(strName, defValue) == defValue ? GetFormLong(strName, defValue) : GetQueryLong(strName, defValue);
        }

        /// <summary>
        /// 获得指定Url参数的float类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static float GetQueryFloat(string strName, float defValue)
        {
            return Utils.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
        }


        /// <summary>
        /// 获得指定表单参数的float类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的float类型值</returns>
        public static float GetFormFloat(string strName, float defValue)
        {
            return Utils.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
        }

        /// <summary>
        /// 获得指定Url或表单参数的float类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static float GetFloat(string strName, float defValue)
        {
            return GetQueryFloat(strName, defValue) == defValue ? GetFormFloat(strName, defValue) : GetQueryFloat(strName, defValue);
        }

        /// <summary>
        /// 获得服务器IP
        /// </summary>
        /// <returns></returns>
        public static string GetServerIP()
        {
            return HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {


            var result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (string.IsNullOrEmpty(result) || !Utils.IsIP(result))
            {
                return "0.0.0.0";
            }

            return result;

        }

        /// <summary>
        /// 保存用户上传的文件
        /// </summary>
        /// <param name="path">保存路径</param>
        public static void SaveRequestFile(string path)
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpContext.Current.Request.Files[0].SaveAs(path);
            }
        }

        //		/// <summary>
        //		/// 保存上传的文件
        //		/// </summary>
        //		/// <param name="MaxAllowFileCount">最大允许的上传文件个数</param>
        //		/// <param name="MaxAllowFileSize">最大允许的文件长度(单位: KB)</param>
        //		/// <param name="AllowFileExtName">允许的文件扩展名, 以string[]形式提供</param>
        //		/// <param name="AllowFileType">允许的文件类型, 以string[]形式提供</param>
        //		/// <param name="Dir">目录</param>
        //		/// <returns></returns>
        //		public static Forum.AttachmentInfo[] SaveRequestFiles(int MaxAllowFileCount, int MaxAllowFileSize, string[] AllowFileExtName, string[] AllowFileType, string Dir)
        //		{
        //			int savefilecount = 0;
        //			
        //			int fcount = Math.Min(MaxAllowFileCount, HttpContext.Current.Request.Files.Count);
        //
        //			Forum.AttachmentInfo[] attachmentinfo = new Forum.AttachmentInfo[fcount];
        //			for(int i=0;i<fcount;i++)
        //			{
        //				string filename = HttpContext.Current.Request.Files[i].FileName;
        //				string fileextname = filename.Substring(filename.LastIndexOf("."));
        //				string filetype = HttpContext.Current.Request.Files[i].ContentType;
        //				int filesize = HttpContext.Current.Request.Files[i].ContentLength;
        //				// 判断 文件扩展名/文件大小/文件类型 是否符合要求
        //				if(Utils.InArray(fileextname, AllowFileExtName) && (filesize <= MaxAllowFileSize * 1024) && Utils.InArray(filetype, AllowFileType))
        //				{
        //
        //					HttpContext.Current.Request.Files[i].SaveAs(Dir + Utils.GetDateTime() + Environment.TickCount.ToString() + fileextname);
        //					attachmentinfo[savefilecount].Filename = filename;
        //					attachmentinfo[savefilecount].Filesize = filesize;
        //					attachmentinfo[savefilecount].Description = filetype;
        //					attachmentinfo[savefilecount].Filetype = fileextname;
        //					savefilecount++;
        //				}
        //			}
        //			return attachmentinfo;
        //			
        //		}

    }
}
