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

public partial class Manage_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Session.Remove("userid");
        HttpContext.Current.Session.Remove("adminid");
        Cookie.ClearUserCookie();
        Response.Redirect("login.aspx");
    }
}
