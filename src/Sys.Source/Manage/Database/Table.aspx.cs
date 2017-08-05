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

public partial class Manage_Database_Table : System.Web.UI.Page
{
    public DataSet ds;
    public string act = ManageHelper.PageAct();
    public string tbl = PageRequest.GetString("tbl").Replace("'", "");

    protected void Page_Load(object sender, EventArgs e)
    {
        ManageHelper.CheckAdminLogin();
        ManageHelper.CheckAdminPower("system_databaseview");
        //if (act == "db")
        //{
        //    var db = new Sys.DAL.Common();
        //    ds = db.GetSelect(tbl, "", "", "top 200 *");
        //    return;
        //}

        //var bll = new Sys.BLL.Common();
        
        //ds = bll.DatabaseTable();
    }
}
