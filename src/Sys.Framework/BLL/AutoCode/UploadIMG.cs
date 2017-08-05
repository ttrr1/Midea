//支持缩略图，是否等比缩放，指定像素缩放，限定上传大小，上传尺寸，图片存放路径，信息导出
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;

/// <summary>
/// UploadIMG 的摘要说明
/// </summary>
public class UploadIMG
{
    public UploadIMG()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public UploadIMG(string path) {
        this.Path = path;
    }

    private string _MSG;
    private string _ofullname;
    private string _tfullname;
    private string _ofilename;
    private string _tfilename;
    private int _limitwidth = 2048;
    private int _limitheight = 1536;
    private int _twidth = 100;
    private int _theight = 100;
    private int _size = 1000000;
    private bool _israte = true;
    private string _path = "UpdateFile";
    private string _filepath = "";

    /// <summary>
    /// 数据保存路劲
    /// </summary>
    public string FileName {
        get { return _filepath; }
        set { _filepath = value; }
    }

    /// <summary>
    /// 信息
    /// </summary>
    public string MSG
    {
        get { return _MSG; }
        set { _MSG = value; }
    }
    /// <summary>
    /// 保存时的完整路径.原图
    /// </summary>
    public string OFullName
    {
        get { return _ofullname; }
        set { _ofullname = value; }
    }
    /// <summary>
    /// 保存时的完整路径.缩略图
    /// </summary>
    public string TFullName
    {
        get { return _tfullname; }
        set { _tfullname = value; }
    }
    /// <summary>
    /// 保存时的图片名称.原图
    /// </summary>
    public string OFileName
    {
        get { return _ofilename; }
        set { _ofilename = value; }
    }
    /// <summary>
    /// 保存时的图片名称.缩略图
    /// </summary>
    public string TFileName
    {
        get { return _tfilename; }
        set { _tfilename = value; }
    }
    /// <summary>
    /// 限定宽度
    /// </summary>
    public int LimitWidth
    {
        get { return _limitwidth; }
        set { _limitwidth = value; }
    }
    /// <summary>
    /// 限定高度
    /// </summary>
    public int LimitHeight
    {
        get { return _limitheight; }
        set { _limitheight = value; }
    }
    /// <summary>
    /// 缩略图宽度
    /// </summary>
    public int TWidth
    {
        get { return _twidth; }
        set { _twidth = value; }
    }
    /// <summary>
    /// 缩略图高度
    /// </summary>
    public int THeight
    {
        get { return _theight; }
        set { _theight = value; }
    }
    /// <summary>
    /// 文件大小
    /// </summary>
    public int Size
    {
        get { return _size; }
        set { _size = value; }
    }
    /// <summary>
    /// 是否成比例
    /// </summary>
    public bool IsRate
    {
        get { return _israte; }
        set { _israte = value; }
    }
    /// <summary>
    /// 存放图片的文件夹名称
    /// </summary>
    public string Path
    {
        get { return _path; }
        set { _path = value; }
    }

    /// <summary>
    /// 图片上传(默认:"等比压缩,限定上传尺寸2048*1536,缩略图尺寸100*100,限定上传大小1MB,存放在根目录UpdateFile中")
    /// </summary>
    /// <param name="UploadFile">文件上传控件</param>
    /// <returns>返回是否成功保存图片</returns>
    public bool UpLoadIMG(HttpPostedFile UploadFile)
    {
        if (UploadFile != null)//检查是否已经选择文件
        {
            string filename = UploadFile.FileName.ToLower();
            int i = filename.LastIndexOf(".");
            filename = filename.Substring(i, filename.Length - i);
            if (!(filename == ".jpg" || filename == ".jpeg" || filename == ".gif" || filename == ".png" || filename == ".bmp"))
            {
                MSG = "不受支持的类型,请重新选择!";
                return false;
            }//检查上传文件的格式是否有效
            if (UploadFile.ContentLength == 0 || UploadFile.ContentLength >= Size)
            {
                MSG = "指定的文件大小不符合要求,限定上传大小1MB!";
                return false;
            }//检查图片文件的大小
            //生成原图 
            Stream oStream = UploadFile.InputStream;
            System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream);
            int owidth = oImage.Width; //原图宽度 
            int oheight = oImage.Height; //原图高度
            if (owidth > LimitWidth || oheight > LimitHeight)
            {
                MSG = "超过允许的图片尺寸范围,限定上传尺寸2048*1536!";
                return false;
            }//检查是否超出规定尺寸
            if (IsRate)
            {
                //按比例计算出缩略图的宽度和高度 
                if (owidth >= oheight)
                {
                    THeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(TWidth) / Convert.ToDouble(owidth)));//等比设定高度
                }
                else
                {
                    TWidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(THeight) / Convert.ToDouble(oheight)));//等比设定宽度
                }
            }
            //生成缩略原图 
            Bitmap tImage = new Bitmap(TWidth, THeight);
            Graphics g = Graphics.FromImage(tImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; //设置高质量插值法 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
            g.Clear(Color.Transparent); //清空画布并以透明背景色填充 
            g.DrawImage(oImage, new Rectangle(0, 0, TWidth, THeight), new Rectangle(0, 0, owidth, oheight), GraphicsUnit.Pixel);
            Random oRandom = new Random();
            string oStringRandom = oRandom.Next(9999).ToString();//生成4位随机数字
            //格式化日期作为文件名

            string oStringTime = DateTime.Now.ToString("yyyyMM");
            OFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + filename;
            TFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + filename.Replace(".", "_t.");
            string oSavePath = HttpContext.Current.Server.MapPath("~") + "\\" + Path + "\\" + oStringTime + "\\";
            if (!Directory.Exists(oSavePath))
            {
                Directory.CreateDirectory(oSavePath);//在根目录下建立文件夹
            }
            //保存路径+完整文件名
            OFullName = oSavePath + OFileName;
            TFullName = oSavePath + TFileName;
            FileName = "/" + Path + "/" + oStringTime + "/" + OFileName;

            //开始保存图片至服务器
            try
            {
                switch (filename)
                {
                    case ".jpeg":
                    case ".jpg":
                        {
                            oImage.Save(OFullName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            tImage.Save(TFullName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        }
                    case ".gif":
                        {
                            oImage.Save(OFullName, System.Drawing.Imaging.ImageFormat.Gif);
                            tImage.Save(TFullName, System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                        }
                    case ".png":
                        {
                            oImage.Save(OFullName, System.Drawing.Imaging.ImageFormat.Png);
                            tImage.Save(TFullName, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                        }
                    case ".bmp":
                        {
                            oImage.Save(OFullName, System.Drawing.Imaging.ImageFormat.Bmp);
                            tImage.Save(TFullName, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                        }
                }
                MSG = "图片上传成功!";
                return true;
            }
            catch (Exception ex)
            {
                MSG = ex.Message;
                return false;
            }
            finally
            {
                //释放资源 
                oImage.Dispose();
                g.Dispose();
                tImage.Dispose();
            }
        }
        else
        {
            MSG = "";
            return true;
        }
    }

    /// <summary>
    /// 图片上传(默认:"等比压缩,限定上传尺寸2048*1536,缩略图尺寸100*100,限定上传大小1MB,存放在根目录UpdateFile中")
    /// </summary>
    /// <param name="UploadFile">文件上传控件</param>
    /// <returns>返回是否成功保存图片</returns>
    public bool UpLoadFile(HttpPostedFile UploadFile)
    {
        if (UploadFile != null)//检查是否已经选择文件
        {
            string filename = UploadFile.FileName.ToLower();
            int i = filename.LastIndexOf(".");
            filename = filename.Substring(i, filename.Length - i);

            if (UploadFile.ContentLength == 0 || UploadFile.ContentLength >= Size)
            {
                MSG = "指定的文件大小不符合要求,限定上传大小5MB!";
                return false;
            }
            string oStringTime = DateTime.Now.ToString("yyyyMM");
            OFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + filename;
            string oSavePath = HttpContext.Current.Server.MapPath("~") + "\\" + Path + "\\" + oStringTime + "\\";
            if (!Directory.Exists(oSavePath))
            {
                Directory.CreateDirectory(oSavePath);//在根目录下建立文件夹
            }
            //保存路径+完整文件名
            OFullName = oSavePath + OFileName;
            FileName = "/" + Path + "/" + oStringTime + "/" + OFileName;

            //开始保存图片至服务器
            try
            {
                UploadFile.SaveAs(OFullName);

                MSG = "课件上传成功!";
                return true;
            }
            catch (Exception ex)
            {
                MSG = ex.Message;
                return false;
            }
            finally
            {
                //释放资源 

            }
        }
        else
        {
            MSG = "";
            return true;
        }
    }
}


