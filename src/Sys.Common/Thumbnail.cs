using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Sys.Common
{
	/// <summary>
	/// Thumbnail ��ժҪ˵����
	/// </summary>
	public class Thumbnail
	{
        //ȫ�ֱ�����ʹ�øо���̫�ã�����ֱ�Ӹ�д�˲��ٷ�����
		//private Image srcImage;
		//private string srcFileName;		
		
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="FileName">ԭʼͼƬ·��</param>
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
		/// �ص�
		/// </summary>
		/// <returns></returns>
        static bool ThumbnailCallback()
		{
			return false;
		}

		/// <summary>
		/// ��������ͼ,��������ͼ��Image����
		/// </summary>
		/// <param name="Width">����ͼ���</param>
		/// <param name="Height">����ͼ�߶�</param>
		/// <returns>����ͼ��Image����</returns>
        public static Image GetImage(string filePath, int Width, int Height)
		{
			Image img;
			Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            img = GetImageSource(filePath).GetThumbnailImage(Width, Height, callb, IntPtr.Zero);
 			return img;
		}

		/// <summary>
		/// ��������ͼ
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
		/// ��������ͼ������
		/// </summary>
		/// <param name="Width">����ͼ�Ŀ��</param>
		/// <param name="Height">����ͼ�ĸ߶�</param>
		/// <param name="imgformat">�����ͼ���ʽ</param>
		/// <returns>����ͼ��Image����</returns>
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
		/// ����ͼƬ
		/// </summary>
		/// <param name="image">Image ����</param>
		/// <param name="savePath">����·��</param>
		/// <param name="ici">ָ����ʽ�ı�������</param>
		private static void SaveImage(Image image, string savePath, ImageCodecInfo ici)
		{
			//���� ԭͼƬ ����� EncoderParameters ����
			EncoderParameters parameters = new EncoderParameters(1);
			parameters.Param[0] = new EncoderParameter(Encoder.Quality, ((long) 100));
			image.Save(savePath, ici, parameters);
			parameters.Dispose();
		}

		/// <summary>
		/// ��ȡͼ���������������������Ϣ
		/// </summary>
		/// <param name="mimeType">��������������Ķ���;�����ʼ�����Э�� (MIME) ���͵��ַ���</param>
		/// <returns>����ͼ���������������������Ϣ</returns>
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
		/// �����³ߴ�
		/// </summary>
		/// <param name="width">ԭʼ���</param>
		/// <param name="height">ԭʼ�߶�</param>
		/// <param name="maxWidth">����¿��</param>
		/// <param name="maxHeight">����¸߶�</param>
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
		/// �õ�ͼƬ��ʽ
		/// </summary>
		/// <param name="name">�ļ�����</param>
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
		/// ����С������
		/// </summary>
		/// <param name="fileName">ԭͼ���ļ�·��</param>
		/// <param name="newFileName">�µ�ַ</param>
		/// <param name="newSize">���Ȼ���</param>
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

				//���������ͼ�沢��͸������ɫ���
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
		/// ��������ͼ
		/// </summary>
		/// <param name="fileName">ԭͼ·��</param>
		/// <param name="newFileName">��ͼ·��</param>
		/// <param name="maxWidth">�����</param>
		/// <param name="maxHeight">���߶�</param>
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
        /// <param name="fileName">�ļ�·��</param>
        /// <param name="newFileName">���ļ�·��</param>
        /// <param name="thumbWi">��</param>
        /// <param name="thumbHi">��</param>
        /// <param name="maintainAspect">�Ƿ�ȱ���ѹ��</param>
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
        /// ��ȡImage����֧������;������������Դ��WEB��ʽ
        /// </summary>
        /// <param name="fileName">�ļ�·��</param>
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
                //�ͷŶ���
                if (request != null)
                    request = null;
                if (stream != null)
                    stream.Dispose();
            }
            return source;
        }

        /// <summary>
        /// ����Сͼ,�̶����
        /// </summary>
        /// <param name="oldfile">ԭͼƬ</param>
        /// <param name="newfile">��ͼƬ</param>
        /// <param name="white">���</param>
        /// <param name="height">�߶�</param>
        public static void ShowThumbnail(string oldfile, string newfile, int width, int height)
        {
            try
            {
                System.Drawing.Image image = GetImageSource(oldfile);

                //��ȡԭͼ�߶ȺͿ��
                int oldh = image.Height;
                int oldw = image.Width;

                //ֱ���趨��ͼ�ĸ߿�
                int neww, newh;
                neww = width; 
                newh = height;   

                //�ṩһ���ص�����,����ȷ�������ں�ʱ��ǰȡ��ִ��
                System.Drawing.Image.GetThumbnailImageAbort callb = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                System.Drawing.Image bt = new System.Drawing.Bitmap(neww, newh);

                //��װһ��GDI+��ͼҳ��
                System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bt);  
                gr.Clear(Color.White);
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gr.DrawImage(image, new Rectangle(0, 0, neww, newh), 0, 0, oldw, oldh, GraphicsUnit.Pixel);

                //������ͼ
                bt.Save(newfile, GetFormat(oldfile));

                //�ͷ���Դ
                gr.Dispose();
                bt.Dispose();
                image.Dispose();
            }
            catch { }

        }
	}
}
