using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_Member_MemberList : System.Web.UI.Page
{
    protected string userType = Sys.Common.PageRequest.GetString("userType");
    protected void Page_Load(object sender, EventArgs e)
    {
        ManageHelper.CheckAdminLogin();
        ManageHelper.CheckAdminHavePower("system_member_azg");
    }
}