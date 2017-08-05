using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UiDesk_admin_adminlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ManageHelper.CheckAdminLogin();
        ManageHelper.CheckAdminPower("system_adminview");
    }
}