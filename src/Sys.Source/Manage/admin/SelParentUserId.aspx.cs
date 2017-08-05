using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.Common;

public partial class UiDesk_admin_SelParentUserId : System.Web.UI.Page
{

    protected int SelectUserId = PageRequest.GetInt("SelectUserId",0);
    protected void Page_Load(object sender, EventArgs e)
    {


        ManageHelper.CheckAdminLogin();

    }
}