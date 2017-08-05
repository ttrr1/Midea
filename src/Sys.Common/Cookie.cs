using System;
using System.Text.RegularExpressions;
using System.Web;
using iPortal.Common;
using Sys.Common;

namespace Sys.Common
{

    public class Cookie
    {
        public static bool IsValidDomain(string host)
        {
            Regex regex = new Regex(@"^\d+$");
            if (host.IndexOf(".") == -1)
            {
                return false;
            }
            return !regex.IsMatch(host.Replace(".", string.Empty));
        }

        public static string GetCookie(string strName)
        {
            return GetCookie(strName, ConfigHelper.GetConfigString("CookieName"));
        }

        public static string GetCookie(string strName, string cookiename)
        {
            if (((HttpContext.Current.Request.Cookies != null) && (HttpContext.Current.Request.Cookies[cookiename] != null)) && (HttpContext.Current.Request.Cookies[cookiename][strName] != null))
            {
                return Utils.UrlDecode(HttpContext.Current.Request.Cookies[cookiename][strName].ToString());
            }
            return "";
        }

        public static string GetCookie(string strName, string cookiename, string cod)
        {
            if (((HttpContext.Current.Request.Cookies != null) && (HttpContext.Current.Request.Cookies[cookiename] != null)) && (HttpContext.Current.Request.Cookies[cookiename][strName] != null))
            {
                return Utils.UrlDecode(HttpContext.Current.Request.Cookies[cookiename][strName].ToString(), cod);
            }
            return "";
        }

        public static void ClearUserCookie()
        {
            ClearUserCookie(ConfigHelper.GetConfigString("CookieName"), ConfigHelper.GetConfigString("CookieDomain"));
        }

        public static void ClearUserCookie(string cookiename, string cookiedomain)
        {
            var cookie = new HttpCookie(cookiename);
            cookie.Values.Clear();
            cookie.Expires = DateTime.Now.AddYears(-1);
            string text = cookiedomain;
            if (((text != string.Empty) && (HttpContext.Current.Request.Url.Host.IndexOf(text) > -1)) && IsValidDomain(HttpContext.Current.Request.Url.Host))
            {
                cookie.Domain = text;
            }
            HttpContext.Current.Response.AppendCookie(cookie);
        }


        public static void WriteUserCookie(int uid, string password, int expires)
        {
            WriteUserCookie(uid, password, expires, ConfigHelper.GetConfigString("CookieName"), ConfigHelper.GetConfigString("CookieDomain"), ConfigHelper.GetConfigString("PasswordKey"), ConfigHelper.GetConfigString("CookieUserid"), ConfigHelper.GetConfigString("CookiePassword"));
        }

        public static void WriteUserCookie(int uid, string password, int expires, string cookiename, string cookiedomain, string passwordkey, string cookieuid, string cookiepassword)
        {
            HttpCookie cookie = new HttpCookie(cookiename);
            cookie.Values[cookieuid] = uid.ToString();
            cookie.Values[cookiepassword] = Utils.UrlEncode(SetCookiePassword(password, passwordkey));
            if (expires > 0)
            {
                cookie.Expires = DateTime.Now.AddMinutes((double)expires);
            }
            string text2 = cookiedomain;
            if (((text2 != string.Empty) && (HttpContext.Current.Request.Url.Host.IndexOf(text2) > -1)) && IsValidDomain(HttpContext.Current.Request.Url.Host))
            {
                cookie.Domain = text2;
            }

            if (HttpContext.Current.Response.Cookies[cookiename] == null)
            {
                HttpContext.Current.Response.AppendCookie(cookie);
                HttpContext.Current.Session.Add("userid", uid);
            }
            else
            {
                HttpContext.Current.Response.Cookies.Set(cookie);
                HttpContext.Current.Session["userid"] = uid;
            }




        }


        public static string GetCookiePassword(string password)
        {
            return GetCookiePassword(password, ConfigHelper.GetConfigString("Passwordkey"));
        }

        public static string GetCookiePassword(string password, string passwordkey)
        {
            if (passwordkey == "")
                return password;
            else
                return DES.Decode(password, passwordkey).Trim();
        }

        public static string SetCookiePassword(string password)
        {
            return SetCookiePassword(password, ConfigHelper.GetConfigString("Passwordkey"));
        }

        public static string SetCookiePassword(string password, string passwordkey)
        {
            if (passwordkey == "")
                return password;
            else
                return DES.Encode(password, passwordkey);
        }
    }
}
