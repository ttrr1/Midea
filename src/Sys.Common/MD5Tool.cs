using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys.Common
{
    public class MD5Tool
    {
        /// <summary>
        ///common类的成员函数:对字符串md5加密，并转小写
        ///str1:本函数返回对字符串md5加密，并转小写的结果。
        ///写于2007-06-29 
        /// </summary>
        public static string StrMD5(string str1)
        {
            str1 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str1, "md5");//对传来的str1用md5加密
            str1 = (str1.Substring(8, str1.Length - 16)).ToLower();//取出md5码,并转小写
            return str1;
        }

        public static string StrMD5_Common(string str1)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str1, "MD5");
        }
    }
}
