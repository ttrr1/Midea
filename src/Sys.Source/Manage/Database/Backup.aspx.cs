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

public partial class Manage_Database_Backup : System.Web.UI.Page
{
    public List<Sys.Model.AdminLog> list;
    public string DatabaseBackupPath = PageRequest.GetString("DatabaseBackupPath");

    protected void Page_Load(object sender, EventArgs e)
    {
        ManageHelper.CheckAdminLogin();
        ManageHelper.CheckAdminPower("system_databasebackup");

        if (ManageHelper.PageAct() != "bak") return;
        WebConfig.SetString("DatabaseBackupPath", DatabaseBackupPath);

        var db = new Sys.BLL.Common();
        try
        {
            var bakfile = db.DatabaseBackup(DatabaseBackupPath);
            Response.Write("yes");
            ManageHelper.AddLog("system_databasebackup", "数据库备份成功，路径：" + bakfile);

        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);
            ManageHelper.AddLog("system_databasebackup", "数据库备份失败，路径：" + DatabaseBackupPath);
            Response.End();
        }


        Response.End();
    }
}
