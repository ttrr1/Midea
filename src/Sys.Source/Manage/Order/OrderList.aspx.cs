using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.Common;

public partial class Manage_Order_OrderList : System.Web.UI.Page
{
    protected string orderStatus = PageRequest.GetString("orderStatus");
    protected void Page_Load(object sender, EventArgs e)
    {
        ManageHelper.CheckAdminLogin();
        ManageHelper.CheckAdminHavePower("system_order_status0");
    }
}