using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.BLL;
using Sys.Common;

public partial class Manage_system_updatepassword : System.Web.UI.Page
{
    protected string act = PageRequest.GetString("act");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (act == "updatePwd")
        {
            updatePwd();
        }
    }

    private void updatePwd()
    {
        var result = new Account().AccountUpdate(Account.GetLoginUserID(), Utils.MD5(PageRequest.GetString("pwd")));

        Response.Write(result > 0 ? "yes" : "no");

        Response.End();
    }
}