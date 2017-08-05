using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Sys.Common
{

    public class FileTool
    {
        //原文件名称传到指定路径
        public static string UploadFile(string inputfilename, string path, FileType ft)
        {
            //先取得该表单上传文件对象 
            var postedFile = HttpContext.Current.Request.Files[inputfilename];

            if (postedFile == null)
                return "";
            if (postedFile.ContentLength <= 0)
                return "";

            var fileInfo = "";
            try
            {
                if (CheckContentType(postedFile, ft))
                {
                    if (path.LastIndexOf("/") != path.Length - 1) path += "/";

                    fileInfo = path + postedFile.FileName;

                    //上传
                    postedFile.SaveAs(HttpContext.Current.Server.MapPath(fileInfo));
                }
            }
            catch (Exception ex)
            {
                Error.CreateError(ex);
            }
            return fileInfo;
        }


        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="inputfilename">控件名称</param>
        /// <param name="newfilename">新文件名称</param>
        /// <param name="path">上传路径</param>
        /// <param name="ft"></param>
        /// <returns></returns>
        public static string UploadFile(string inputfilename, string newfilename, string path, FileType ft)
        {
            //先取得该表单上传文件对象 
            var postedFile = HttpContext.Current.Request.Files[inputfilename];
            if (postedFile == null)
                return "";
            if (postedFile.ContentLength <= 0)
                return "";
            if (postedFile.ContentLength > 2019752)
            {
                return "max";
            }

            //路径+文件名
            var fileInfo = "";
            try
            {
                //如果文件类型合法
                if (CheckContentType(postedFile, ft))
                {
                    //构造新的文件
                    // ReSharper disable PossibleNullReferenceException
                    var newFile = newfilename + System.IO.Path.GetExtension(postedFile.FileName).ToLower();
                    // ReSharper restore PossibleNullReferenceException

                    //检测相关目录信息
                    var filePath = Utils.CreateSaveFilePath(path, Utils.PathFormat.Year_Month);

                    fileInfo = filePath + newFile;

                    //上传
                    postedFile.SaveAs(HttpContext.Current.Server.MapPath(fileInfo));
                }
            }
            catch (Exception ex)
            {
                Error.CreateError(ex);
            }
            return fileInfo;
        }

        public static bool CheckContentType(HttpPostedFile file, FileType ft)
        {//application/octet-stream
            string extension = System.IO.Path.GetExtension(file.FileName).ToUpper();
            string[] exts = null;
            string[] contentType = null;
            if (ft == FileType.Image)
            {
                exts = new string[] { ".JPEG", ".JPG", ".GIF", ".PNG", ".BMP" };
                contentType = new string[] { "image/jpeg", "image/pjpeg", "image/gif", "image/png", "image/x-png", "image/bmp" };
            }
            else if (ft == FileType.File)
            {
                exts = new string[] { ".RAR", ".ZIP", ".DOC", ".PPT", ".XLS" };
                contentType = new string[] { "application/octet-stream", "application/vnd.ms-excel", "application/vnd.ms-powerpoint", "application/msword", "application/x-zip-compressed" };
            }
            if (exts.Contains(extension) && contentType.Contains(file.ContentType))
            {
                return true;
            }
            return false;
        }

        public enum FileType
        {
            Image,
            File
        }

        public static void FileDelete(string fileName)
        {
            //string serverPath = HttpContext.Current.Server.MapPath(fileName);
            if (System.IO.File.Exists(fileName))
                System.IO.File.Delete(fileName);
        }

        /// <summary>   
        /// 用递归方法删除文件夹目录及文件   
        /// </summary>   
        /// <param name="dir">带文件夹名的路径</param>    
        public static void DeleteFolder(string dir)
        {
            //dir = HttpContext.Current.Server.MapPath(dir);
            if (System.IO.Directory.Exists(dir)) //如果存在这个文件夹删除之    
            {
                foreach (string d in System.IO.Directory.GetFileSystemEntries(dir))
                {
                    if (System.IO.File.Exists(d))
                        System.IO.File.Delete(d); //直接删除其中的文件                           
                    else
                        DeleteFolder(d); //递归删除子文件夹    
                }
                System.IO.Directory.Delete(dir, true); //删除已空文件夹                    
            }
        }

        public static string FileNewName()
        {
            string str = DateTime.Now.ToString("yyyyMMddHHmmssms") + new Random().Next(100, 999).ToString();
            return str;
        }
    }
}
