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
using System.Collections.Generic;

public partial class Manage_Database_DelLog : System.Web.UI.Page
{
    public List<Sys.Model.AdminLog> list;

    protected void Page_Load(object sender, EventArgs e)
    {
        ManageHelper.CheckAdminLogin();
        ManageHelper.CheckAdminPower("system_databasedellog");
        if (ManageHelper.PageAct() != "del") return;
        ManageHelper.CheckAdminPower("system_databasedellog");
        var db = new Sys.BLL.Common();


        try
        {
            db.DatabaseLogDel();
            Response.Write("yes");
            ManageHelper.AddLog("system_databasedellog", "清除数据库日志成功");

        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);
            ManageHelper.AddLog("system_databasedellog", "清除数据库日志失败");
            Response.End();
        }


        Response.End();
    }
}




