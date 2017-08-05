using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

/// <summary>
/// ManageErrMsg 的摘要说明
/// </summary>
public class ManageErrMsg
{
    private HttpResponse response = HttpContext.Current.Response;
    private HttpServerUtility server = HttpContext.Current.Server;
    public string StrMsg = "";
    public ManageErrMsg()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public void AddErr(string strErr)
    {
        StrMsg += "<li>" + strErr + "</li>";
    }
    public void ChkErr()
    {
        if (StrMsg != "")
        {
            string s = doPage("Msg.htm");
            s = s.Replace("{$Msg}", StrMsg);
            response.Write(s);
            response.End();
        }
    }
    public string doPage(string tpl)
    {
        return LoadTpl(tpl);
    }

    public string LoadTpl(string strFileName)
    {
        string s0 = "";//"Template/TinyBlue/" + strFileName;
        if (!File.Exists(server.MapPath(s0)))
        {
            s0 = "/Manage/js/" + strFileName;
        }
        return LoadTxtFile(s0);
    }

    public string LoadTxtFile(string strP)
    {
        string s0 = server.MapPath(strP);
        var fs = new FileStream(s0, FileMode.Open, FileAccess.Read);
        var strHtml = new StreamReader(fs);
        string strV = strHtml.ReadToEnd();
        strHtml.Close();
        fs.Close();
        return strV;
    }

 
}
