using System;
using System.Web;


namespace Sys.Common
{
    /// <summary>
    /// Request������
    /// </summary>
    public class PageRequest
    {

        public PageRequest()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        /// <summary>
        /// ��ҳ��ʾ
        /// </summary>
        /// <param name="url">A��ǩҳ�����ӵ�ַ</param>
        /// <param name="pageIndex">��ǰҳ��</param>
        /// <param name="pageCounts">��ҳ��</param>
        /// <returns></returns>
        public static string ShowSplitPage(string url, int pageIndex, int PageSize, int PageCS)
        {
            var str = "";
            var pageCounts = 0;//��ҳ��

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
                str += @"<li class=""pg_first""><a href='" + url.Replace("{0}", "1") + @"'><img align='absmiddle' src='../images/Pic/Icons/arrow/arrow_first.gif'>&nbsp;��ҳ</a>&nbsp;</li><li class=""pg_prve""><a href='" + url.Replace("{0}", (pageIndex - 1).ToString()) + "'><img align='absmiddle' src='../images/Pic/Icons/arrow/arrow_prev.gif'>&nbsp;��һҳ</a>&nbsp;</li>";
            for (var i = start; i <= end; i++)
            {
                if (i == pageIndex)
                    str += @"<li class=""curr-page"">" + i + "</li>";
                else
                    str += @"<li><a href='" + url.Replace("{0}", i.ToString()) + "'>" + i + "</a></li>";
            }
            if (pageIndex < pageCounts)
            {
                str += @"<li class=""pg_next"">&nbsp;<a href='" + url.Replace("{0}", (pageIndex + 1).ToString()) + "'><img align='absmiddle' src='../images/Pic/Icons/arrow/arrow_next.gif'>&nbsp;��һҳ</a></li>";
                str += @"<li class=""pg_next"">&nbsp;<a href='" + url.Replace("{0}", (pageCounts).ToString()) + "'><img align='absmiddle' src='../images/Pic/Icons/arrow/arrow_last.gif'>&nbsp;ĩҳ</a></li>";
            }



            str += "</ul></div>";
            str += string.Format(
                 "</ul><div style='float: right; margin-right:30px'>��ǰҳ��<select name='pageselector' id='pageselector'  onchange=\"window.location.href=this.value\" >");
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
        /// ��ҳ��ʾ
        /// </summary>
        /// <param name="url"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageCS"></param>
        /// <returns></returns>
        public static string QShowSplitPage(string url, int PageIndex, int PageSize, int PageCS)
        {

            string str = "";
            int PageCounts = 0;//��ҳ��

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
            str += "<div class='page'><span><strong>��" + PageCounts + "ҳ</strong></span>";
            if (PageIndex != 1)
                str += @"<a  href='" + url.Replace("{0}", "1") + @"'>��ҳ</a>&nbsp;<a href='" + url.Replace("{0}", (PageIndex - 1).ToString()) + "'>��һҳ</a>";
            for (int i = start; i <= end; i++)
            {
                if (i == PageIndex)
                    str += @"<a class='current' href='#'>" + i.ToString() + "</a>";
                else
                    str += @"<a href='" + url.Replace("{0}", i.ToString()) + "'>" + i.ToString() + "</a>";
            }
            if (PageIndex < PageCounts)
            {
                str += @"<a href='" + url.Replace("{0}", (PageIndex + 1).ToString()) + "'>��һҳ</a>";
                str += @"<a href='" + url.Replace("{0}", (PageCounts).ToString()) + "'>ĩҳ</a>";
            }

            str += "</div>";
            return str;
        }


        /// <summary>
        /// ��ҳ��ʾ
        /// </summary>
        /// <param name="url"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageCS"></param>
        /// <returns></returns>
        public static string AjaxShowSplitPage(string url, int PageIndex, int PageSize, int PageCS)
        {

            string str = "";
            int pageCounts = 0;//��ҳ��

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
            str += "<div class='page'><span><strong>��" + pageCounts + "ҳ</strong></span>";
            if (PageIndex != 1)
                str += "<a style=\"cursor:pointer\" onclick=\"NextList('1');\">��ҳ</a>&nbsp;<a style=\"cursor:pointer\" onclick=\"NextList('" + (PageIndex - 1) + "');\">��һҳ</a>";
            for (int i = start; i <= end; i++)
            {
                if (i == PageIndex)
                    str += "<a  style=\"cursor:pointer\" class='current' onclick=\"NextList('" + (i) + "');\">" + i + "</a>";
                else
                    str += "<a style=\"cursor:pointer\" onclick=\"NextList('" + (i) + "');\">" + i.ToString() + "</a>";
            }
            if (PageIndex < pageCounts)
            {
                str += "<a style=\"cursor:pointer\" onclick=\"NextList('" + (PageIndex + 1) + "');\">��һҳ</a>";
                str += "<a style=\"cursor:pointer\" onclick=\"NextList('" + (pageCounts) + "');\">ĩҳ</a>";
            }

            str += "</div>";
            return str;
        }



        /// <summary>
        /// ��ҳ��ʾ
        /// </summary>
        /// <param name="url"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageCS"></param>
        /// <returns></returns>
        public static string AjaxShowSplitPage2(string url, int PageIndex, int PageSize, int PageCS)
        {

            string str = "";
            int pageCounts = 0;//��ҳ��

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
            str += "<div class='page'><span><strong>��" + pageCounts + "ҳ</strong></span>";
            if (PageIndex != 1)
                str += "<a style=\"cursor:pointer\" onclick=\"NextList2('1');\">��ҳ</a>&nbsp;<a style=\"cursor:pointer\" onclick=\"NextList2('" + (PageIndex - 1) + "');\">��һҳ</a>";
            for (int i = start; i <= end; i++)
            {
                if (i == PageIndex)
                    str += "<a  style=\"cursor:pointer\" class='current' onclick=\"NextList2('" + (i) + "');\">" + i + "</a>";
                else
                    str += "<a style=\"cursor:pointer\" onclick=\"NextList2('" + (i) + "');\">" + i.ToString() + "</a>";
            }
            if (PageIndex < pageCounts)
            {
                str += "<a style=\"cursor:pointer\" onclick=\"NextList2('" + (PageIndex + 1) + "');\">��һҳ</a>";
                str += "<a style=\"cursor:pointer\" onclick=\"NextList2('" + (pageCounts) + "');\">ĩҳ</a>";
            }

            str += "</div>";
            return str;
        }







        //public static string QShowSplitPage(string url, int pageIndex, int PageSize, int PageCS)
        //{

        //    string str = "";
        //    int PageCounts = 0;//��ҳ��

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
        //        str += @"<li class=""pg_first""><a href='" + url.Replace("{0}", "1") + @"'><img align='absmiddle' src='images/Pic/Icons/arrow/arrow_first.gif'>&nbsp;��ҳ</a>&nbsp;</li><li class=""pg_prve""><a href='" + url.Replace("{0}", (pageIndex - 1).ToString()) + "'><img align='absmiddle' src='images/Pic/Icons/arrow/arrow_prev.gif'>&nbsp;��һҳ</a>&nbsp;</li>";
        //    for (int i = start; i <= end; i++)
        //    {
        //        if (i == pageIndex)
        //            str += @"<li class=""curr-page"">" + i + "</li>";
        //        else
        //            str += @"<li><a href='" + url.Replace("{0}", i.ToString()) + "'>" + i + "</a></li>";
        //    }
        //    if (pageIndex < PageCounts)
        //    {
        //        str += @"<li class=""pg_next"">&nbsp;<a href='" + url.Replace("{0}", (pageIndex + 1).ToString()) + "'><img align='absmiddle' src='images/Pic/Icons/arrow/arrow_next.gif'>&nbsp;��һҳ</a></li>";
        //        str += @"<li class=""pg_next"">&nbsp;<a href='" + url.Replace("{0}", (PageCounts).ToString()) + "'><img align='absmiddle' src='images/Pic/Icons/arrow/arrow_last.gif'>&nbsp;ĩҳ</a></li>";
        //    }



        //    str += "</ul></div>";
        //    /*   str += string.Format(
        //           "</ul><div style='float: right; margin-right:30px'>��ǰҳ��<select name='pageselector' id='pageselector'  onchange=\"window.location.href=this.value\" >");
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
        /// ҳ��sucmsg�������ֵ
        /// </summary>
        /// <returns></returns>
        public static string PageSucMsg()
        {
            return PageRequest.GetString("sucmsg").Trim();
        }

        /// <summary>
        /// ҳ��errmsg�������ֵ
        /// </summary>
        /// <returns></returns>
        public static string PageErrMsg()
        {
            return PageRequest.GetString("errmsg").Trim();
        }


        /// <summary>
        /// �жϵ�ǰҳ���Ƿ���յ���Post����
        /// </summary>
        /// <returns>�Ƿ���յ���Post����</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }
        /// <summary>
        /// �жϵ�ǰҳ���Ƿ���յ���Get����
        /// </summary>
        /// <returns>�Ƿ���յ���Get����</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        /// <summary>
        /// ����ָ���ķ�����������Ϣ
        /// </summary>
        /// <param name="strName">������������</param>
        /// <returns>������������Ϣ</returns>
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
        /// ������һ��ҳ��ĵ�ַ
        /// </summary>
        /// <returns>��һ��ҳ��ĵ�ַ</returns>
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
        /// �õ���ǰ��������ͷ
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
        /// �õ�����ͷ
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }


        /// <summary>
        /// ��ȡ��ǰ�����ԭʼ URL(URL ������Ϣ֮��Ĳ���,������ѯ�ַ���(�������))
        /// </summary>
        /// <returns>ԭʼ URL</returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        /// <summary>
        /// ��ȡ��ǰ�����ԭʼ ���URL(������ѯ�ַ���(�������))
        /// </summary>
        /// <returns>���� URL</returns>
        public static string GetFullUrl()
        {
            return "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.RawUrl;
        }

        /// <summary>
        /// �жϵ�ǰ�����Ƿ�������������
        /// </summary>
        /// <returns>��ǰ�����Ƿ�������������</returns>
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
        /// �ж��Ƿ�����������������
        /// </summary>
        /// <returns>�Ƿ�����������������</returns>
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
        /// ��õ�ǰ����Url��ַ
        /// </summary>
        /// <returns>��ǰ����Url��ַ</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }


        /// <summary>
        /// ���ָ��Url������ֵ
        /// </summary>
        /// <param name="strName">Url����</param>
        /// <returns>Url������ֵ</returns>
        public static string GetQueryString(string strName)
        {
            return HttpContext.Current.Request.QueryString[strName] == null ? "" : HttpContext.Current.Request.QueryString[strName].Trim();
        }

        public static string GetQueryString()
        {
            return HttpContext.Current.Request.QueryString.ToString();
        }

        /// <summary>
        /// ��õ�ǰҳ�������
        /// </summary>
        /// <returns>��ǰҳ�������</returns>
        public static string GetPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }

        /// <summary>
        /// ���ر���Url�������ܸ���
        /// </summary>
        /// <returns></returns>
        public static int GetParamCount()
        {
            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
        }


        /// <summary>
        /// ���ָ����������ֵ
        /// </summary>
        /// <param name="strName">������</param>
        /// <returns>��������ֵ</returns>
        public static string GetFormString(string strName)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.Form[strName].Trim();
        }

        /// <summary>
        /// ���Url���������ֵ, ���ж�Url�����Ƿ�Ϊ���ַ���, ��ΪTrue�򷵻ر�������ֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <returns>Url���������ֵ</returns>
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
        /// ���Url���������ֵ, ���ж�Url�����Ƿ�Ϊ���ַ���, ��ΪTrue�򷵻ر�������ֵ
        /// </summary>
        /// <param name="strName">��������ð�ȫ��GetStringֵ��</param>
        /// <param name="isHtml">�Ƿ���HTML����ð�ȫ��GetStringֵ��</param>
        /// <returns>Url���������ֵ����ð�ȫ��GetStringֵ��</returns>
        public static string GetString(string strName, bool isHtml)
        {
            var strVal = "";
            strVal = "".Equals(GetQueryString(strName)) ? GetFormString(strName) : GetQueryString(strName);
            //����
            strVal = Utils.FilterStr(strVal);

            if (!isHtml)//��HTMLEncode�ǰ�ȫ��
            {
                strVal = Utils.HtmlEncodeFull(strVal);
            }
            else//��ֹXSS����
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
        /// ���ָ��Url������int����ֵ
        /// </summary>
        /// <param name="strName">Url����</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>Url������int����ֵ</returns>
        public static int GetQueryInt(string strName, int defValue)
        {
            return Utils.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
        }


        /// <summary>
        /// ���ָ����������int����ֵ
        /// </summary>
        /// <param name="strName">������</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>��������int����ֵ</returns>
        public static int GetFormInt(string strName, int defValue)
        {
            return Utils.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
        }

        /// <summary>
        /// ���ָ��Url���������int����ֵ, ���ж�Url�����Ƿ�Ϊȱʡֵ, ��ΪTrue�򷵻ر�������ֵ
        /// </summary>
        /// <param name="strName">Url�������</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>Url���������int����ֵ</returns>
        public static int GetInt(string strName, int defValue)
        {
            return GetQueryInt(strName, defValue) == defValue ? GetFormInt(strName, defValue) : GetQueryInt(strName, defValue);
        }

        /// <summary>
        /// ���ָ��Url������long����ֵ
        /// </summary>
        /// <param name="strName">Url����</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>Url������long����ֵ</returns>
        public static long GetQueryLong(string strName, long defValue)
        {
            return Utils.StrToLong(HttpContext.Current.Request.QueryString[strName], defValue);
        }


        /// <summary>
        /// ���ָ����������long����ֵ
        /// </summary>
        /// <param name="strName">������</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>��������long����ֵ</returns>
        public static long GetFormLong(string strName, long defValue)
        {
            return Utils.StrToLong(HttpContext.Current.Request.Form[strName], defValue);
        }

        /// <summary>
        /// ���ָ��Url���������long����ֵ, ���ж�Url�����Ƿ�Ϊȱʡֵ, ��ΪTrue�򷵻ر�������ֵ
        /// </summary>
        /// <param name="strName">Url�������</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>Url���������long����ֵ</returns>
        public static long GetLong(string strName, long defValue)
        {
            return GetQueryLong(strName, defValue) == defValue ? GetFormLong(strName, defValue) : GetQueryLong(strName, defValue);
        }

        /// <summary>
        /// ���ָ��Url������float����ֵ
        /// </summary>
        /// <param name="strName">Url����</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>Url������int����ֵ</returns>
        public static float GetQueryFloat(string strName, float defValue)
        {
            return Utils.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
        }


        /// <summary>
        /// ���ָ����������float����ֵ
        /// </summary>
        /// <param name="strName">������</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>��������float����ֵ</returns>
        public static float GetFormFloat(string strName, float defValue)
        {
            return Utils.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
        }

        /// <summary>
        /// ���ָ��Url���������float����ֵ, ���ж�Url�����Ƿ�Ϊȱʡֵ, ��ΪTrue�򷵻ر�������ֵ
        /// </summary>
        /// <param name="strName">Url�������</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>Url���������int����ֵ</returns>
        public static float GetFloat(string strName, float defValue)
        {
            return GetQueryFloat(strName, defValue) == defValue ? GetFormFloat(strName, defValue) : GetQueryFloat(strName, defValue);
        }

        /// <summary>
        /// ��÷�����IP
        /// </summary>
        /// <returns></returns>
        public static string GetServerIP()
        {
            return HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
        }

        /// <summary>
        /// ��õ�ǰҳ��ͻ��˵�IP
        /// </summary>
        /// <returns>��ǰҳ��ͻ��˵�IP</returns>
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
        /// �����û��ϴ����ļ�
        /// </summary>
        /// <param name="path">����·��</param>
        public static void SaveRequestFile(string path)
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpContext.Current.Request.Files[0].SaveAs(path);
            }
        }

        //		/// <summary>
        //		/// �����ϴ����ļ�
        //		/// </summary>
        //		/// <param name="MaxAllowFileCount">���������ϴ��ļ�����</param>
        //		/// <param name="MaxAllowFileSize">���������ļ�����(��λ: KB)</param>
        //		/// <param name="AllowFileExtName">������ļ���չ��, ��string[]��ʽ�ṩ</param>
        //		/// <param name="AllowFileType">������ļ�����, ��string[]��ʽ�ṩ</param>
        //		/// <param name="Dir">Ŀ¼</param>
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
        //				// �ж� �ļ���չ��/�ļ���С/�ļ����� �Ƿ����Ҫ��
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
