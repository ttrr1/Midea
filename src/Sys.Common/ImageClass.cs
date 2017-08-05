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
using System.Drawing.Imaging;
using System.IO;
/// <summary>
/// ImageClass 的摘要说明
/// </summary>
namespace sz71096.Common
{
    public class ImageClass
    {
        /// <summary>
        /// common类的成员函数:上传网页file控件name=**的一个客户端文件
        /// </summary>
        /// <param name="inputfilename">表单file控件的name</param>
        /// <param name="newfilename">上传到服务器保存成的新文件名</param>
        /// <param name="path">上传到服务器的保存路径 , 使用相对于调用这个函数的*.aspx网页的相对路径</param>
        /// <param name="fileextyes">允许的上传文件类型,如:".jpg,.gif"字串</param>
        /// <returns></returns>
        public static string UploadFile(string inputfilename, string newfilename, string path, string fileextyes)
        {
            //先取得该表单上传文件对象 
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[inputfilename];
            //取得文件扩展名,并转为小写 
            string fileExtension = System.IO.Path.GetExtension(postedFile.FileName).ToLower();

            //如果上传文件字节数>0
            if (postedFile.ContentLength > 0)
            {
                //如果文件类型合法
                if (fileextyes.IndexOf(fileExtension) >= 0)
                {
                    //构造新的文件名
                    newfilename = newfilename + fileExtension;

                    //如果上传路径文件夹不存在就先建立文件夹
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                    }
                    postedFile.SaveAs(HttpContext.Current.Server.MapPath(path) + newfilename);

                    //上传成功
                    return newfilename;
                }
                else
                {
                    //文件类型不合法
                    return "";
                }
            }
            return "";
        }


    }
}
