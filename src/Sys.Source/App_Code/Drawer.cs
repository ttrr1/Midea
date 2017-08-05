using System;
using System.Drawing;
using System.Web;
using sz71096.Common;

namespace App_Code
{
    public class Drawer
    {
        public enum SetOut
        {
            Number, CharacterAndNumber, Character
        }
        /// <summary>
        /// 验证码
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="top">内容距离顶部边框距离</param>
        /// <param name="left">内容距离左边框距离</param>
        /// <param name="length">验证码字符个数</param>
        /// <param name="rank">糙级</param>
        /// <param name="size">字体大小</param>
        /// <param name="code">验证码类型</param>
        /// <param name="cookieName">存入cookie</param>
        public static void DrawImage(int width, int height, int top, int left, int length, int rank, int size, SetOut code, string cookieName)
        {
            var img = new Bitmap(width, height);
            var g = Graphics.FromImage(img);
            g.Clear(Color.White);
            Color[] color = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.LightSlateGray };
            string[] fontFamily = { "Verdana", "Microsoft Sans Serif", "Arial", "宋体", "Geneva", "sans-serif Georgia", "Times New Roman", "Times", "serif", "Book Antiqua", "Palatino" };
            FontStyle[] fontStyle = { FontStyle.Bold, FontStyle.Italic, FontStyle.Regular };//, FontStyle.Strikeout, FontStyle.Underline
            var r = new Random();
            for (var i = 0; i < rank; i++)//画随机线条
            {
                var x1 = r.Next(img.Width);
                var x2 = r.Next(img.Width);
                var y1 = r.Next(img.Height);
                var y2 = r.Next(img.Height);
                g.DrawLine(new Pen(color[r.Next(color.Length)], 1), x1, y1, x2, y2);
            }
            var character = string.Empty;



            switch (code)
            {
                case SetOut.Number:
                    character = RandomCode.Number(length);
                    break;
                case SetOut.Character:
                    character = RandomCode.Character(length);
                    break;
                case SetOut.CharacterAndNumber:
                    character = RandomCode.CharacterAndNumber(length);
                    break;
                default:
                    break;
            }




            if (character.Length > 0)
            {
                //var cookie = new HttpCookie(cookieName) { Value = character };
                //HttpContext.Current.Response.Cookies.Add(cookie);
                HttpContext.Current.Session["ValidateCode"] = character;

            }
            for (var j = 0; j < length; j++)//画验证码
            {
                try
                {
                    var f = new Font(fontFamily[r.Next(fontFamily.Length)], size, fontStyle[r.Next(fontStyle.Length)]);
                    Brush b = new SolidBrush(color[r.Next(color.Length)]);
                    g.DrawString(character.Substring(j, 1), f, b, new PointF(size * j + left, top - 2));
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write(ex.Message);
                    FileObj.WriteFile(HttpContext.Current.Server.MapPath("~/log/log.txt"), ex.Message);

                }
            }
            g.DrawRectangle(new Pen(Color.Black, 0), 0, 0, img.Width - 1, img.Height - 1);//画验证码边框
            img.Save(HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}
