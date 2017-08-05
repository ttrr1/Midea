using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.Common;

public partial class MobilePages_OrderCreate : System.Web.UI.Page
{
    protected string token = Sys.Common.PageRequest.GetQueryString("token");
    protected string userLoginId = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //token = "13052894185";
        var result = new Sys.BLL.Account().CheckToken(token);
        if (result)
        {
            UtilLog.WriteTextLog("log test", token);
            userLoginId = token.Split('|')[0];
        }
        else
        {
            Response.Write("<script>alert('Token验证失败');history.back(-1);</script>");
        }
    }
}