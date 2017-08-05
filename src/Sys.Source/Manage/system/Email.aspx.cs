using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Sys.Common;

public partial class Manage_System_Email : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ManageHelper.CheckAdminLogin();
        ManageHelper.CheckAdminPower("system_configemail");
        if (ManageHelper.PageAct() == "save")
            ConfigSave();
        else if (ManageHelper.PageAct() == "test")
            SendTest();
    }

    private void SendTest()
    {
         var ret = ManageHelper.MailSend(PageRequest.GetString("EmailTest"), Sys.Kernel.Software.Name + " 的测试邮件。", "这是一封来之 " + Sys.Kernel.Software.ChineseName + " 重要的测试邮件，发送时间：" + DateTime.Now.ToString() + "。");
        Response.Write(ret == "ok" ? "yes" : Utils.RemoveHtmlContent(ret));

        Response.End();
    }

    private void ConfigSave()
    {
        ManageHelper.CheckAdminPower("system_configemailupdate");
        var bll = new Sys.BLL.SysConfig();
        var model = new Sys.Model.SysConfig();
        var json = Request["data"];
        var rows = (ArrayList)PluSoft.Utils.JSON.Decode(json);



        foreach (Hashtable row in rows)
        {
            model = bll.GetModel("WebConfig", "EmailServer");
            model.Value = row["EmailServer"].ToString();
            bll.Update(model);
            DataCache.RemoveCache("SysConfigValue-WebConfig-EmailServer");

            model = bll.GetModel("WebConfig", "EmailPort");
            model.Value = row["EmailPort"].ToString();
            bll.Update(model);
            DataCache.RemoveCache("SysConfigValue-WebConfig-EmailPort");

            model = bll.GetModel("WebConfig", "EmailAccount");
            model.Value = row["EmailAccount"].ToString();
            bll.Update(model);
            DataCache.RemoveCache("SysConfigValue-WebConfig-EmailAccount");

            model = bll.GetModel("WebConfig", "EmailPassword");
            model.Value = row["EmailPassword"].ToString();
            bll.Update(model);
            DataCache.RemoveCache("SysConfigValue-WebConfig-EmailPassword");

            model = bll.GetModel("WebConfig", "EmailSender");
            model.Value = row["EmailSender"].ToString();
            bll.Update(model);
            ManageHelper.AddLog("system_configemailupdate", "修改了邮件设置");
            DataCache.RemoveCache("SysConfigValue-WebConfig-EmailSender");

        }
        Response.Write("yes");
        Response.End();







    }
}
