using System;
using System.Text.RegularExpressions;

namespace Sys.Common.IP
{

    #region IP��
    /// <summary>
    /// ����IP��ַ��
    /// </summary>
    /// <remarks>
    /// ���룺��ֹ��IP��ַ��
    /// ֧��������ʽ����ȷƥ�䡡��Χ���κ�ƥ��
    /// ��ȷƥ�䣺192.169.4.1
    /// ��Χ��192.169.4.1-192.169.5.1
    /// �κ�ƥ�䣺192.169.*.1
    /// </remarks>
    /// <author>
    /// ����Դ For iPortal CMS
    /// </author>
    /// <date>
    /// 2009-08-13
    /// </date>
    public class BlockIP
    {
        #region ��������IP��ַ����������IP��ַ��256����IP��ַ��

        /// <summary>
        /// ��������IP��ַ����������IP��ַ��256����IP��ַ��
        /// </summary>
        /// <param name="ip">Ҫ���Ƶ�IP��ַ�Σ���192.169.4.1��192.169.4.1-192.169.5.1��192.169.*.1������ֵ�����жϸ�ʽ�淶</param>
        /// <returns>RestrictionIPResult�࣬��������ֵ�Ƿ����IP��ַ�淶�������ϣ��򷵻�256���Ƶ�IP��ַ��,StartIPΪ��ʼIP��EndIPΪ����IP</returns>
        /// <example>
        /// if(!BlockIP.RestrictionIPResult("inputIP").IsChecked)
        /// {
        ///     ҳ����ʾ�û����벻��ȷ;
        /// }
        /// else
        /// {
        ///     �洢RestrictionIPResult.StartIP
        ///     �洢RestrictionIPResult.EndIP
        /// }
        /// </example>
        public static RestrictionIPResult GenerateIPList(string ip)
        {
            RestrictionIPResult result = new RestrictionIPResult(false, 0, 0);
            if (!ip.Contains("-") && !ip.Contains("*")) //��ȷƥ��
            {
                if (!IsIP(ip))
                    return result;
                result.IsChecked = true;
                result.StartIP = CalcIP(ip);
                result.EndIP = CalcIP(ip);
                return result;
            }
            if (ip.Contains("-") && !ip.Contains("*")) //��Χ
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
            if (ip.Contains("*")) //�κ�ƥ��
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

        #region ˽�к���

        /// <summary>
        /// �Ƿ�Ϊ�Ϸ�IP
        /// </summary>
        /// <param name="ip">IP��ַ</param>
        /// <returns>�Ƿ�Ϸ�</returns>
        private static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// ����IPΪ256����
        /// </summary>
        /// <param name="ip">IP��ַ</param>
        /// <returns>256���Ƶ�IP��ַ</returns>
        public static long CalcIP(string ip)
        {
            string[] iparray = ip.Split('.');
            return Convert.ToInt64(iparray[0]) * 256L * 256L * 256L + Convert.ToInt64(iparray[1]) * 256L * 256L +
                   Convert.ToInt64(iparray[2]) * 256L + Convert.ToInt64(iparray[3]);
        }

        #endregion
    }
    #endregion

    #region RestrictionIPResult��

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
        /// ��ֹIP��ַ�Ƿ���Ϲ���
        /// </summary>
        public bool IsChecked
        {
            get { return _ischecked; }
            set { _ischecked = value; }
        }

        /// <summary>
        /// ��һ��IP��ַ
        /// </summary>
        public long StartIP
        {
            get { return _startip; }
            set { _startip = value; }
        }

        /// <summary>
        /// �ڶ���IP��ַ
        /// </summary>
        public long EndIP
        {
            get { return _endip; }
            set { _endip = value; }
        }
    }

    #endregion
}