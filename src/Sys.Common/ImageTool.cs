using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sz71096.Common
{
    /// <summary>
    /// 功能：图片工具类
    /// 作者：朱平
    /// 时间：2010-3-4 16:14
    /// 最后修改者：
    /// 时间：
    /// </summary>
    public class ImageTool
    {
        /// <summary>
        /// A better alternative to Image.GetThumbnail. Higher quality but slightly slower
        /// </summary>
        /// <param name="source"></param>
        /// <param name="thumbWi"></param>
        /// <param name="thumbHi"></param>
        /// <returns></returns>
        public static System.Drawing.Bitmap CreateThumbnail(System.Drawing.Bitmap source, int thumbWi, int thumbHi, bool maintainAspect)
        {
            // return the source image if it's smaller than the designated thumbnail
            if (source.Width < thumbWi && source.Height < thumbHi) return source;

            System.Drawing.Bitmap ret = null;
            try
            {
                int wi, hi;

                wi = thumbWi;
                hi = thumbHi;

                if (maintainAspect)
                {
                    // maintain the aspect ratio despite the thumbnail size parameters
                    if (source.Width > source.Height)
                    {
                        wi = thumbWi;
                        hi = (int)(source.Height * ((decimal)thumbWi / source.Width));
                    }
                    else
                    {
                        hi = thumbHi;
                        wi = (int)(source.Width * ((decimal)thumbHi / source.Height));
                    }
                }

                // original code that creates lousy thumbnails
                // System.Drawing.Image ret = source.GetThumbnailImage(wi,hi,null,IntPtr.Zero);
                ret = new System.Drawing.Bitmap(wi, hi);
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(ret))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.FillRectangle(System.Drawing.Brushes.White, 0, 0, wi, hi);
                    g.DrawImage(source, 0, 0, wi, hi);
                }
            }
            catch
            {
                ret = null;
            }

            return ret;
        }
    }
}
