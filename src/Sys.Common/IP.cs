using System;
using System.Text.RegularExpressions;

namespace Sys.Common.IP
{

    #region IP类
    /// <summary>
    /// 限制IP地址类
    /// </summary>
    /// <remarks>
    /// 输入：阻止的IP地址段
    /// 支持三种形式：精确匹配　范围　任何匹配
    /// 精确匹配：192.169.4.1
    /// 范围：192.169.4.1-192.169.5.1
    /// 任何匹配：192.169.*.1
    /// </remarks>
    /// <author>
    /// 邱鹏源 For iPortal CMS
    /// </author>
    /// <date>
    /// 2009-08-13
    /// </date>
    public class BlockIP
    {
        #region 输入限制IP地址，生成限制IP地址的256进制IP地址段

        /// <summary>
        /// 输入限制IP地址，生成限制IP地址的256进制IP地址段
        /// </summary>
        /// <param name="ip">要限制的IP地址段，如192.169.4.1，192.169.4.1-192.169.5.1，192.169.*.1，输入值无需判断格式规范</param>
        /// <returns>RestrictionIPResult类，包含输入值是否符合IP地址规范，若符合，则返回256进制的IP地址段,StartIP为起始IP，EndIP为结束IP</returns>
        /// <example>
        /// if(!BlockIP.RestrictionIPResult("inputIP").IsChecked)
        /// {
        ///     页面提示用户输入不正确;
        /// }
        /// else
        /// {
        ///     存储RestrictionIPResult.StartIP
        ///     存储RestrictionIPResult.EndIP
        /// }
        /// </example>
        public static RestrictionIPResult GenerateIPList(string ip)
        {
            RestrictionIPResult result = new RestrictionIPResult(false, 0, 0);
            if (!ip.Contains("-") && !ip.Contains("*")) //精确匹配
            {
                if (!IsIP(ip))
                    return result;
                result.IsChecked = true;
                result.StartIP = CalcIP(ip);
                result.EndIP = CalcIP(ip);
                return result;
            }
            if (ip.Contains("-") && !ip.Contains("*")) //范围
            {
                string[] argsip = ip.Split('-');
                if (!IsIP(argsip[0]) || !IsIP(argsip[1]))
                    return result;
                result.IsChecked = true;
                if (CalcIP(argsip[0]) < CalcIP(argsip[1]))
                {
                    result.StartIP = CalcIP(argsip[0]);
                    result.EndIP = CalcIP(argsip[1]);
                }
                else
                {
                    result.StartIP = CalcIP(argsip[1]);
                    result.EndIP = CalcIP(argsip[0]);
                }
                return result;
            }
            if (ip.Contains("*")) //任何匹配
            {
                string[] argsip = new string[2];
                argsip[0] = ip.Replace("*", "0");
                argsip[1] = ip.Replace("*", "254");
                if (!IsIP(argsip[0]) || !IsIP(argsip[1]))
                    return result;
                result.IsChecked = true;
                if (CalcIP(argsip[0]) < CalcIP(argsip[1]))
                {
                    result.StartIP = CalcIP(argsip[0]);
                    result.EndIP = CalcIP(argsip[1]);
                }
                else
                {
                    result.StartIP = CalcIP(argsip[1]);
                    result.EndIP = CalcIP(argsip[0]);
                }
                return result;
            }
            return result;
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 是否为合法IP
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns>是否合法</returns>
        private static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 计算IP为256进制
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns>256进制的IP地址</returns>
        public static long CalcIP(string ip)
        {
            string[] iparray = ip.Split('.');
            return Convert.ToInt64(iparray[0]) * 256L * 256L * 256L + Convert.ToInt64(iparray[1]) * 256L * 256L +
                   Convert.ToInt64(iparray[2]) * 256L + Convert.ToInt64(iparray[3]);
        }

        #endregion
    }
    #endregion

    #region RestrictionIPResult类

    public class RestrictionIPResult
    {
        private bool _ischecked;
        private long _startip;
        private long _endip;

        public RestrictionIPResult(bool ischecked, long ip1, long ip2)
        {
            IsChecked = ischecked;
            StartIP = ip1;
            EndIP = ip2;
        }

        /// <summary>
        /// 阻止IP地址是否符合规则
        /// </summary>
        public bool IsChecked
        {
            get { return _ischecked; }
            set { _ischecked = value; }
        }

        /// <summary>
        /// 第一段IP地址
        /// </summary>
        public long StartIP
        {
            get { return _startip; }
            set { _startip = value; }
        }

        /// <summary>
        /// 第二段IP地址
        /// </summary>
        public long EndIP
        {
            get { return _endip; }
            set { _endip = value; }
        }
    }

    #endregion
}