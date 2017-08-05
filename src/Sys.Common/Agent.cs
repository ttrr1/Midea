using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Collections;
using System.Security.Cryptography;

namespace sz71096.Common
{

    /// <summary>
    ///Class1 的摘要说明
    /// </summary>
    public class Tool
    {
        private static String[] hexDigits = { "0", "1", "2", "3", "4", "5",
		"6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
        private static MD5 md5Hasher = MD5.Create();

        public static String LINE_SEPARATOR = "\n\r";
        private Tool() { }
        /**
         * 转换字节数组为16进制字串
         * 
         * @param b
         *            字节数组
         * @return 16进制字串
         */
        public static String toHexString(byte[] b)
        {
            StringBuilder resultSb = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                resultSb.Append(toHexString(b[i]));
            }
            return resultSb.ToString();
        }

        /**
         * 将一人个字节转换为一个十六进制的字符串
         * @param b
         * @return
         */
        public static String toHexString(byte b)
        {
            int n = b;
            if (n < 0)
                n = 256 + n;
            int d1 = n / 16;
            int d2 = n % 16;
            return hexDigits[d1] + hexDigits[d2];
        }

        /**
         * 将一个十六进制的字符串轩换为一个字节数组
         * @param hexString
         * @return
         */
        public static byte[] toBytes(String hexString)
        {
            if (hexString == null) return null;
            if (hexString.Length == 0) return new byte[0];
            byte[] rtv = new byte[hexString.Length / 2 + hexString.Length % 2];
            byte one = 0;
            char[] hexString1 = hexString.ToCharArray();

            for (int i = 0; i < hexString1.Length; i++)
            {

                if (i % 2 == 0)
                {
                    one = toByte(hexString1[i]);
                    rtv[i / 2] = one;
                }
                else
                {
                    one = (byte)(16 * one + toByte(hexString1[i]));
                    rtv[i / 2] = one;
                }
            }

            return rtv;
        }
        private static byte toByte(char c)
        {
            switch (c)
            {
                case '0':
                    return 0;
                case '1':
                    return 1;
                case '2':
                    return 2;
                case '3':
                    return 3;
                case '4':
                    return 4;
                case '5':
                    return 5;
                case '6':
                    return 6;
                case '7':
                    return 7;
                case '8':
                    return 8;
                case '9':
                    return 9;
                case 'a':
                    return 10;
                case 'b':
                    return 11;
                case 'c':
                    return 12;
                case 'd':
                    return 13;
                case 'e':
                    return 14;
                case 'f':
                    return 15;
                case 'A':
                    return 10;
                case 'B':
                    return 11;
                case 'C':
                    return 12;
                case 'D':
                    return 13;
                case 'E':
                    return 14;
                case 'F':
                    return 15;
                default:
                    return 0;
            }
        }
        /**
         * 将一个十六进制的字符串转换成一个指定字符集的字符串
         * @param hexString
         * @param encode
         * @return
         * @throws UnsupportedEncodingException
         */
        public static String hexStringToString(String hexString, String encode)
        {
            if (hexString == null || hexString.Length == 0) return hexString;
            Encoding en = Encoding.GetEncoding(encode);
            byte[] temp = toBytes(hexString);
            String str1 = en.GetString(temp);
            return str1;
        }
        /**
         * 将一个字符串取指定的字符集字节后转成放便表示的十六进制字符串
         * @param string
         * @param encode
         * @return
         */
        public static String stringtohexString(String str, String encode)
        {
            if (str == null || str.Length == 0) return str;

            Encoding en = Encoding.GetEncoding(encode);
            byte[] temp = en.GetBytes(str);
            return toHexString(temp);
        }
        /**
         * 将toSplitStr解析成一个Dictionary
         * @param toSplitStr
         * @return
         */
        public static Dictionary<String, String> splitNameValueString(String toSplitStr)
        {
            Dictionary<String, String> splitDictionary = new Dictionary<String, String>();
            splitNameValueString(toSplitStr, splitDictionary);
            return splitDictionary;
        }
        /**
         * 取字符串的UFT-8字节
         * @param string
         * @return
         */
        public static byte[] getBytes(String str)
        {

            byte[] byData;
            char[] charData;
            try
            {
                charData = str.ToCharArray();
                //初始化字节数组
                byData = new byte[charData.Length];
                //将字符数组转换为正确的字节格式
                Encoder enc = Encoding.UTF8.GetEncoder();
                enc.GetBytes(charData, 0, charData.Length, byData, 0, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return byData;
        }
        /**
         * 将toSplitStr 分解手存放到splitDictionary
         * @param toSplitStr
         * @param splitDictionary<String,String>
         * @return
         */
        public static int splitNameValueString(String toSplitStr, Dictionary<String, String> splitDictionary)
        {
            if (toSplitStr == null)
                return 0;
            if (toSplitStr.Length < 1)
                return 0;
            if (splitDictionary == null)
                return 0;
            char[] toSplitStr1 = toSplitStr.ToCharArray();
            int toSplitStrLength = toSplitStr1.Length;
            int startIndex = 0;
            int endIndex = 0;
            int equalsIndex = 0;
            String name;
            String value;
            String oneStr;

            while (startIndex < toSplitStrLength)
            {
                endIndex = toSplitStr.IndexOf(';', startIndex);
                if (endIndex < 0)
                    endIndex = toSplitStrLength;
                oneStr = toSplitStr.Substring(startIndex, endIndex - startIndex);
                if (oneStr.Length > 0)
                {
                    equalsIndex = oneStr.IndexOf('=');
                    if (equalsIndex > 0)
                    {
                        name = oneStr.Substring(0, equalsIndex);
                        value = oneStr.Substring(equalsIndex + 1);
                        splitDictionary.Add(name, value);
                    }
                }
                startIndex = endIndex + 1;
            }
            return splitDictionary.Count;
        }

        /**
         * 将nameValue编码为一个字符串
         * @param nameValue
         * @return
         */
        public static String buildNameValueString(Dictionary<String, String> nameValue)
        {
            if (nameValue == null)
                return "";
            StringBuilder tempstrbuf = new StringBuilder();
            foreach (KeyValuePair<String, string> entry in nameValue)
            {
                //for (Dictionary.Entry<String,String> entry :  nameValue.entrySet()) {
                tempstrbuf.Append((String)entry.Key);
                tempstrbuf.Append('=');
                tempstrbuf.Append(entry.Value);
                tempstrbuf.Append(';');
            }
            return tempstrbuf.ToString();
        }

        /**
         * 当需要提交时间数据时用一个long型的数值表示
         * @param theData
         * @return
         */
        public static String getStringValue(DateTime theData)
        {
            return string.Format("{0:yyyy-MM-dd HH.mm.ss.fff}", theData);
            //(theData.Ticks / 10000).ToString();
        }

        public static DateTime getDateValue(String theData)
        {
            String[] parts = theData.Split(new char[] {'-',' ','.'});

            int y = 0;
            if (parts.Length > 0) y = int.Parse(parts[0]);
            int m = 0;
            if (parts.Length > 1) m = int.Parse(parts[1]);
            int d = 0;
            if (parts.Length > 2) d = int.Parse(parts[2]);

            int h = 0;
            if (parts.Length > 3) h = int.Parse(parts[3]);
            int m1 = 0;
            if (parts.Length > 4) m1 = int.Parse(parts[4]);
            int s = 0;
            if (parts.Length > 5) s = int.Parse(parts[5]);
            int f = 0;
            if (parts.Length > 6) f = int.Parse(parts[6]);


            return new DateTime(y,m,d,h,m1,s,f);
            //return new DateTime(long.Parse(theData));
        }
        public static void buildNode(StringBuilder sb, String name, String value)
        {
            sb.Append('<').Append(name).Append('>');
            sb.Append(encodeToXMLString(value));
            sb.Append('<').Append('/').Append(name).Append('>').Append(LINE_SEPARATOR);
        }
        public static String encodeToXMLString(String str)
        {
            if (str == null)
                return "";
            if ("".Equals(str))
                return "";

            StringBuilder tempstrbuf = new StringBuilder();
            char c;
            int i = 0;
            char[] str1 = str.ToCharArray();
            int length = str1.Length;
            for (i = 0; i < length; i++)
            {
                c = str1[i];
                switch (c)
                {
                    case '&':
                        tempstrbuf.Append("&amp;");
                        break;
                    case '\'':
                        tempstrbuf.Append("&apos;");
                        break;
                    case '\"':
                        tempstrbuf.Append("&quot;");
                        break;
                    case '<':
                        tempstrbuf.Append("&lt;");
                        break;
                    case '>':
                        tempstrbuf.Append("&gt;");
                        break;
                    default:
                        tempstrbuf.Append(c);
                        break;
                }
            }
            return tempstrbuf.ToString();
        }
        public static void buildItem(StringBuilder sb, String name, String value)
        {
            buildItem(sb, name, value, false);
        }
        public static void buildItem(StringBuilder sb, String name, String value, bool isLast)
        {
            sb.Append(name).Append(' ').Append(':').Append(' ').Append('\"');
            sb.Append(encodeToJSONString(value)).Append('\"');
            if (isLast == false) sb.Append(',');
            sb.Append(LINE_SEPARATOR);
        }
        public static String encodeToJSONString(String str)
        {
            if (str == null)
                return "";
            if ("".Equals(str))
                return "";

            StringBuilder tempstrbuf = new StringBuilder();
            char c;
            int i = 0;
            char[] str1 = str.ToCharArray();
            int length = str1.Length;
            
            for (i = 0; i < length; i++)
            {
                c = str1[i];
                switch (c)
                {
                    case '\'':
                        tempstrbuf.Append("''");
                        break;
                    case '\"':
                        tempstrbuf.Append("\"\"");
                        break;
                    default:
                        tempstrbuf.Append(c);
                        break;
                }
            }
            return tempstrbuf.ToString();
        }
        public static String computeDigest(byte[] agentKey, byte[] timestamp, byte[] doWhat, byte[] parameter)
        {
            byte[] all = new byte[agentKey.Length + timestamp.Length + doWhat.Length + parameter.Length];
            Array.Copy(agentKey, 0, all, 0, agentKey.Length);
            Array.Copy(timestamp, 0, all, agentKey.Length, timestamp.Length);
            Array.Copy(doWhat, 0, all, agentKey.Length + timestamp.Length, doWhat.Length);
            Array.Copy(parameter, 0, all, agentKey.Length + timestamp.Length + doWhat.Length, parameter.Length);
            byte[] hash = md5Hasher.ComputeHash(all);
            return toHexString(hash);
        }

        public static String computeDigest(byte[] passwordBs, byte[] timestamp)
        {
            byte[] all = new byte[passwordBs.Length + timestamp.Length];
            Array.Copy(passwordBs, 0, all, 0, passwordBs.Length);
            Array.Copy(timestamp, 0, all, passwordBs.Length, timestamp.Length);
            byte[] hash = md5Hasher.ComputeHash(all);
            return toHexString(hash);
        }


    }

}
