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

public partial class Manage_System_System : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ManageHelper.CheckAdminLogin();
        ManageHelper.CheckAdminPower("system_configsystem");
        if (ManageHelper.PageAct() == "save")
            ConfigSave();
    }

    private void ConfigSave()
    {
        ManageHelper.CheckAdminPower("system_configsystemupdate");

        var bll = new Sys.BLL.SysConfig();
        var model = new Sys.Model.SysConfig();


        var json = Request["data"];
        var rows = (ArrayList)PluSoft.Utils.JSON.Decode(json);
        foreach (Hashtable row in rows)
        {

            model = bll.GetModel("WebConfig", "WebsiteName");
            model.Value = row["WebsiteName"].ToString();
            bll.Update(model);
            DataCache.RemoveCache("SysConfigValue-WebConfig-WebsiteName");

            model = bll.GetModel("WebConfig", "WebsiteUrl");
            model.Value = row["WebsiteUrl"].ToString();
           
            bll.Update(model);
            DataCache.RemoveCache("SysConfigValue-WebConfig-WebsiteUrl");

            model = bll.GetModel("WebConfig", "PageHeadTitle");
            model.Value = row["PageHeadTitle"].ToString();
            bll.Update(model);
            DataCache.RemoveCache("SysConfigValue-WebConfig-PageHeadTitle");

            model = bll.GetModel("WebConfig", "PageHeadMetaKeywords");
            model.Value = row["PageHeadMetaKeywords"].ToString();
            bll.Update(model);
            DataCache.RemoveCache("SysConfigValue-WebConfig-PageHeadMetaKeywords");


            model = bll.GetModel("WebConfig", "PageHeadMetaDescription");
            model.Value = row["PageHeadMetaDescription"].ToString(); 
            bll.Update(model);
            ManageHelper.AddLog("system_configsystemupdate", "修改了系统配置");
            DataCache.RemoveCache("SysConfigValue-WebConfig-PageHeadMetaDescription");
        }

        Response.Write("yes");
        Response.End();



    }
}
