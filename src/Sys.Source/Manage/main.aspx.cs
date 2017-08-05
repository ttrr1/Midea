using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UiDesk_main : System.Web.UI.Page
{


    protected string StrHtml = "";

    protected Sys.BLL.AdminFlag BllSysFlag = new Sys.BLL.AdminFlag();
    protected List<Sys.Model.AdminFlag> List = new List<Sys.Model.AdminFlag>();

    protected void Page_Load(object sender, EventArgs e)
    {
      ManageHelper.CheckAdminLogin();


        




    }



    protected string MenuShow(int parentId)
    {
        var str = "";

        return str;



    }
}