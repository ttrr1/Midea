using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.BLL;

public partial class UiDesk_admin_adminitem : System.Web.UI.Page
{

    protected Sys.Model.Admin model = new Sys.Model.Admin();
    protected bool Flag;
    protected void Page_Load(object sender, EventArgs e)
    {
        ManageHelper.CheckAdminLogin();
        model = new Admin().GetModel(Account.GetLoginUserID()) ?? new Sys.Model.Admin();
        Flag = model.UserFlag.IndexOf("system_allot_role")!=-1;
    }
}