using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.Common;
using Sys.Model;

public partial class MobilePages_OrderDetail : System.Web.UI.Page
{
    protected Sys.Model.Orders orderModel = new Sys.Model.Orders();
    protected List<Sys.Model.OrderType> orderTypeList = new List<Sys.Model.OrderType>();
    protected DataTable dtFlow = new DataTable();
    protected DataTable dtComment=new DataTable();
    List<DicModel> lis;
    protected void Page_Load(object sender, EventArgs e)
    {
        string token = PageRequest.GetQueryString("token");
        int orderId = PageRequest.GetQueryInt("orderId",0);
        //orderId = 31;
        var result = new Sys.BLL.Account().CheckToken(token);
        if (result)
        {
            //userLoginId = token.Split('|')[0];
        }
        else
        {
            Response.Write("<script>alert('Token验证失败');history.back(-1);</script>");
        }

        orderModel = new Sys.BLL.Orders().GetModel(orderId);
        orderTypeList = new Sys.BLL.OrderType().GetModelList("OrderId=" + orderId);
        dtFlow = new Sys.BLL.OrderStatusFlow().GetList(1000, "OrderId=" + orderId, "FlowId desc").Tables[0];

        new Sys.BLL.Orders().OrdersStatusInit(out lis);

        dtComment = new Sys.BLL.OrderComment().GetList("OrderId=" + orderId).Tables[0];
    }

    protected string GetOrderStatus(string status)
    {
        var model = lis.Find(q => q.Key == status);
        if (model != null)
            return model.Value;
        return "";

    }

    protected string GetCommentStar()
    {
        var starHtml = string.Empty;
        if (dtComment != null && dtComment.Rows.Count > 0)
        {
            var starLevel = Convert.ToInt32(dtComment.Rows[0]["StarLevel"]);
            for (int i = 1; i <= starLevel; i++)
            {
                starHtml += "<img src=\"images/pl_xing_hover.png\" width=\"15\" height=\"15\" />";
            }
            for (int i = starLevel+1; i <= 5; i++)
            {
                starHtml += "<img src=\"images/pl_xing.png\" width=\"15\" height=\"15\" />";
            }
        }
        return starHtml;
    }

}