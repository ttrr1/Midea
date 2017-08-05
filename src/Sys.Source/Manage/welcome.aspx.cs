using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UiDesk_welcome : System.Web.UI.Page
{
    protected int messageCount;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ManageHelper.CheckAdminLogin();
        GetCount();
    }
    /// <summary>
    /// 返回剩余短信
    /// </summary>
    protected void GetCount()
    {
        var strUrl = "http://api.sms.cn/mm/?uid=wxhai999&pwd=abb62178f2898a7c5bc99be697d6822a";

        var str = CallWebPage(strUrl, 10000, Encoding.Default);
        if (!string.IsNullOrEmpty(str))
        {
            if (str.IndexOf("remain")>0)
            {
                var temp = str.Substring(str.IndexOf("remain")).Replace("remain=", "");
                messageCount = Sys.Common.Utils.StrToInt(temp,0);
            }
        }

      
    }
    /// 访问URL地址
    public string CallWebPage(string url, int httpTimeout, Encoding postEncoding)
    {
        string rStr = "";
        System.Net.WebRequest req = null;
        System.Net.WebResponse resp = null;
        System.IO.Stream os = null;
        System.IO.StreamReader sr = null;
        try
        {
            //创建连接
            req = System.Net.WebRequest.Create(url);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "GET";
            //时间
            if (httpTimeout > 0)
            {
                req.Timeout = httpTimeout;
            }

            //读取返回结果
            resp = req.GetResponse();
            sr = new System.IO.StreamReader(resp.GetResponseStream(), postEncoding);
            rStr = sr.ReadToEnd();


        }
        catch (Exception ex)
        {
            // LogUtil.Warn("HttpUtil.CallWebPage 异常(WebException) ：" + ex.Message);

        }
        finally
        {
            try
            {
                //关闭资源
                if (os != null)
                {
                    os.Dispose();
                    os.Close();
                }
                if (sr != null)
                {
                    sr.Dispose();
                    sr.Close();
                }
                if (resp != null)
                {
                    resp.Close();
                }
                if (req != null)
                {
                    req.Abort();
                    req = null;
                }
            }
            catch (Exception ex2)
            {
                // LogUtil.Exception("HttpUtil.CallWebPage 关闭连接异常：" + ex2.Message);
            }
        }
        return rStr;

    }
}