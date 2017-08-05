using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class RandomCode
    {
        /// <summary>
        /// 重命名
        /// </summary>
        /// <returns>重命名字符串</returns>
        public static string Rename()
        {
            var temp = new StringBuilder(string.Empty);
            temp.Append(DateTime.Now.ToString("yyyyMMddhhmmss"));
            temp.Append(new Random().Next(10000, 99999));
            return temp.ToString();
        }

        /// <summary>
        /// 随机指定长度的数字组合
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>指定长度的数字组合</returns>
        public static string Number(int length)
        {
            var temp = new StringBuilder(string.Empty);
            char[] c = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var r = new Random();
            for (var i = 0; i < length; i++)
            {
                temp.Append(c[r.Next(0, c.Length)]);
            }
            return temp.ToString();
        }
        /// <summary>
        /// 随机指定长度的字母数字组合
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>指定长度的字母数字组合</returns>
        public static string CharacterAndNumber(int length)
        {
            var temp = new StringBuilder(string.Empty);
            char[] c = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            var r = new Random();
            for (var i = 0; i < length; i++)
            {
                temp.Append(c[r.Next(0, c.Length)]);
            }
            return temp.ToString();
        }
        /// <summary>
        /// 随机指定长度的字母组合
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>指定长度的字母组合</returns>
        public static string Character(int length)
        {
            var temp = new StringBuilder(string.Empty);
            char[] c = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            var r = new Random();
            for (var i = 0; i < length; i++)
            {
                temp.Append(c[r.Next(0, c.Length)]);
            }
            return temp.ToString();
        }
    }

