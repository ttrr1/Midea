using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.Common;

public partial class UiDesk_role_rolelist : System.Web.UI.Page
{


    protected int t = PageRequest.GetInt("t",0);
    protected void Page_Load(object sender, EventArgs e)
    {
        ManageHelper.CheckAdminLogin();
        ManageHelper.CheckAdminHavePower("system_adminroleview");
    }
}