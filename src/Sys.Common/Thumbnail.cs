using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Sys.Common
{
	/// <summary>
	/// Thumbnail 的摘要说明。
	/// </summary>
	public class Thumbnail
	{
        //全局变量的使用感觉不太好，这里直接改写了不少方法体
		//private Image srcImage;
		//private string srcFileName;		
		
		/// <summary>
		/// 创建
		/// </summary>
		/// <param name="FileName">原始图片路径</param>
        //public static bool SetImage(string FileName)
        //{
        //    srcFileName = Utils.GetMapPath(FileName);
        //    try
        //    {
        //        srcImage = Image.FromFile(srcFileName);
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    return true;

        //}

		/// <summary>
		/// 回调
		/// </summary>
		/// <returns></returns>
        static bool ThumbnailCallback()
		{
			return false;
		}

		/// <summary>
		/// 生成缩略图,返回缩略图的Image对象
		/// </summary>
		/// <param name="Width">缩略图宽度</param>
		/// <param name="Height">缩略图高度</param>
		/// <returns>缩略图的Image对象</returns>
        public static Image GetImage(string filePath, int Width, int Height)
		{
			Image img;
			Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            img = GetImageSource(filePath).GetThumbnailImage(Width, Height, callb, IntPtr.Zero);
 			return img;
		}

		/// <summary>
		/// 保存缩略图
		/// </summary>
		/// <param name="Width"></param>
		/// <param name="Height"></param>
        public static void SaveThumbnailImage(string filePath, string savePath, int Width, int Height)
		{
            try
            {
                SaveImage(filePath, savePath, Width, Height, GetFormat(filePath));
            }
            catch
            { }
		}

		/// <summary>
		/// 生成缩略图并保存
		/// </summary>
		/// <param name="Width">缩略图的宽度</param>
		/// <param name="Height">缩略图的高度</param>
		/// <param name="imgformat">保存的图像格式</param>
		/// <returns>缩略图的Image对象</returns>
        static void SaveImage(string filePath, string savePath, int Width, int Height, ImageFormat imgformat)
		{
            Image srcImage = GetImageSource(filePath);
            if (imgformat != ImageFormat.Gif && (srcImage.Width > Width) || (srcImage.Height > Height))
            {
                Image img;
                Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                img = srcImage.GetThumbnailImage(Width, Height, callb, IntPtr.Zero);
                srcImage.Dispose();
                img.Save(savePath, imgformat);
                img.Dispose();
            }
		}

		#region Helper

		/// <summary>
		/// 保存图片
		/// </summary>
		/// <param name="image">Image 对象</param>
		/// <param name="savePath">保存路径</param>
		/// <param name="ici">指定格式的编解码参数</param>
		private static void SaveImage(Image image, string savePath, ImageCodecInfo ici)
		{
			//设置 原图片 对象的 EncoderParameters 对象
			EncoderParameters parameters = new EncoderParameters(1);
			parameters.Param[0] = new EncoderParameter(Encoder.Quality, ((long) 100));
			image.Save(savePath, ici, parameters);
			parameters.Dispose();
		}

		/// <summary>
		/// 获取图像编码解码器的所有相关信息
		/// </summary>
		/// <param name="mimeType">包含编码解码器的多用途网际邮件扩充协议 (MIME) 类型的字符串</param>
		/// <returns>返回图像编码解码器的所有相关信息</returns>
		private static ImageCodecInfo GetCodecInfo(string mimeType)
		{
			ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
			foreach(ImageCodecInfo ici in CodecInfo)
			{
				if(ici.MimeType == mimeType)
                    return ici;
			}
			return null;
		}

		/// <summary>
		/// 计算新尺寸
		/// </summary>
		/// <param name="width">原始宽度</param>
		/// <param name="height">原始高度</param>
		/// <param name="maxWidth">最大新宽度</param>
		/// <param name="maxHeight">最大新高度</param>
		/// <returns></returns>
		private static Size ResizeImage(int width, int height, int maxWidth, int maxHeight)
		{
			decimal MAX_WIDTH = (decimal)maxWidth;
			decimal MAX_HEIGHT = (decimal)maxHeight;
			decimal ASPECT_RATIO = MAX_WIDTH / MAX_HEIGHT;

			int newWidth, newHeight;
			decimal originalWidth = (decimal)width;
			decimal originalHeight = (decimal)height;
			
			if (originalWidth > MAX_WIDTH || originalHeight > MAX_HEIGHT) 
			{
				decimal factor;
				// determine the largest factor 
				if (originalWidth / originalHeight > ASPECT_RATIO) 
				{
					factor = originalWidth / MAX_WIDTH;
					newWidth = Convert.ToInt32(originalWidth / factor);
					newHeight = Convert.ToInt32(originalHeight / factor);
				} 
				else 
				{
					factor = originalHeight / MAX_HEIGHT;
					newWidth = Convert.ToInt32(originalWidth / factor);
					newHeight = Convert.ToInt32(originalHeight / factor);
				}	  
			} 
			else 
			{
				newWidth = width;
				newHeight = height;
			}
			return new Size(newWidth,newHeight);			
		}

		/// <summary>
		/// 得到图片格式
		/// </summary>
		/// <param name="name">文件名称</param>
		/// <returns></returns>
		public static ImageFormat GetFormat(string name)
		{
			string ext = name.Substring(name.LastIndexOf(".") + 1);
			switch(ext.ToLower())
			{
				case "jpg":
				case "jpeg":
					return ImageFormat.Jpeg;
				case "bmp":
					return ImageFormat.Bmp;
				case "png":
					return ImageFormat.Png;
				case "gif":
					return ImageFormat.Gif;
				default:
					return ImageFormat.Jpeg;
			}
		}
		#endregion

		/// <summary>
		/// 制作小正方形
		/// </summary>
		/// <param name="fileName">原图的文件路径</param>
		/// <param name="newFileName">新地址</param>
		/// <param name="newSize">长度或宽度</param>
		public static void MakeSquareImage(string fileName, string newFileName, int newSize)
		{
			Image image = Image.FromFile(fileName);
	
			int i = 0;
			int width = image.Width;
			int height = image.Height;
			if (width > height)
				i = height;
			else
				i = width;

            Bitmap b = new Bitmap(newSize, newSize);

			try
			{
				Graphics g = Graphics.FromImage(b);
				g.InterpolationMode = InterpolationMode.High;
				g.SmoothingMode = SmoothingMode.HighQuality;

				//清除整个绘图面并以透明背景色填充
				g.Clear(Color.Transparent);
				if (width < height)
					g.DrawImage(image,  new Rectangle(0, 0, newSize, newSize), new Rectangle(0, (height-width)/2, width, width), GraphicsUnit.Pixel);
				else
					g.DrawImage(image, new Rectangle(0, 0, newSize, newSize), new Rectangle((width-height)/2, 0, height, height), GraphicsUnit.Pixel);

                SaveImage(b, newFileName, GetCodecInfo("image/" + GetFormat(fileName).ToString().ToLower()));
			}
			finally
			{
				image.Dispose();
				b.Dispose();
			}
		}

		/// <summary>
		/// 制作缩略图
		/// </summary>
		/// <param name="fileName">原图路径</param>
		/// <param name="newFileName">新图路径</param>
		/// <param name="maxWidth">最大宽度</param>
		/// <param name="maxHeight">最大高度</param>
		public static void MakeThumbnailImage(string fileName, string newFileName, int maxWidth, int maxHeight)
		{
			Image original = null;
            try
            {
                original = GetImageSource(fileName);

                Size _newSize = ResizeImage(original.Width, original.Height, maxWidth, maxHeight);
                Image displayImage = new Bitmap(original, _newSize);

                displayImage.Save(newFileName, GetFormat(fileName));
            }
            catch
            { }
			finally
			{
                if (original != null)
                    original.Dispose();
			}	
		}

        /// <summary>
        /// A better alternative to Image.GetThumbnail. Higher quality but slightly slower
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="newFileName">新文件路径</param>
        /// <param name="thumbWi">宽</param>
        /// <param name="thumbHi">高</param>
        /// <param name="maintainAspect">是否等比例压缩</param>
        public static void CreateThumbnail(string fileName, string newFileName, int thumbWi, int thumbHi, bool maintainAspect)
        {
            Image source = GetImageSource(fileName);

            System.Drawing.Image bitMap = null;
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
                bitMap = new System.Drawing.Bitmap(wi, hi);
               
                using (Graphics g = Graphics.FromImage(bitMap))
                {
                    g.InterpolationMode = InterpolationMode.High;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.FillRectangle(Brushes.White, 0, 0, wi, hi);
                    g.DrawImage(source, 0, 0, wi, hi);
                }
                
                bitMap.Save(newFileName, GetFormat(fileName));
            }
            catch
            { }
            finally
            {
                if (bitMap != null)
                    bitMap.Dispose();
                if (source != null)
                    source.Dispose();
            }

        }

        /// <summary>
        /// 获取Image对象，支持两种途径－－本地资源、WEB方式
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        public static System.Drawing.Image GetImageSource(string fileName)
        {
            System.Drawing.Image source = null;

            System.Net.WebRequest request = null;
            System.IO.Stream stream = null;
            try
            {
                if (fileName.IndexOf("http://") != -1)
                {
                    request = System.Net.WebRequest.Create(fileName);
                    stream = request.GetResponse().GetResponseStream();
                    source = System.Drawing.Image.FromStream(stream);
                }
                else
                {
                    source = Image.FromFile(fileName);
                }
            }
            catch
            { }
            finally
            {
                //释放对象
                if (request != null)
                    request = null;
                if (stream != null)
                    stream.Dispose();
            }
            return source;
        }

        /// <summary>
        /// 生成小图,固定宽高
        /// </summary>
        /// <param name="oldfile">原图片</param>
        /// <param name="newfile">新图片</param>
        /// <param name="white">宽度</param>
        /// <param name="height">高度</param>
        public static void ShowThumbnail(string oldfile, string newfile, int width, int height)
        {
            try
            {
                System.Drawing.Image image = GetImageSource(oldfile);

                //获取原图高度和宽度
                int oldh = image.Height;
                int oldw = image.Width;

                //直接设定新图的高宽
                int neww, newh;
                neww = width; 
                newh = height;   

                //提供一个回调方法,用于确定方法在何时提前取消执行
                System.Drawing.Image.GetThumbnailImageAbort callb = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                System.Drawing.Image bt = new System.Drawing.Bitmap(neww, newh);

                //封装一个GDI+绘图页面
                System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bt);  
                gr.Clear(Color.White);
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gr.DrawImage(image, new Rectangle(0, 0, neww, newh), 0, 0, oldw, oldh, GraphicsUnit.Pixel);

                //保存新图
                bt.Save(newfile, GetFormat(oldfile));

                //释放资源
                gr.Dispose();
                bt.Dispose();
                image.Dispose();
            }
            catch { }

        }
	}
}
