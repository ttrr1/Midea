using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Midea.Common;
using Sys.Common;

public partial class MobileInterface_OrderStatusHandle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var requestId = Request["request_id"];
        string result = string.Empty;
        Dictionary<string, object> dicResult = new Dictionary<string, object>();
        try
        {
            if (requestId.Equals("update_orderstatus"))
            {
                dicResult = UpdateOrdersStatus();
            }
        }
        catch (Exception ex)
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "处理异常！");
            UtilLog.WriteExceptionLog(requestId, ex);
        }
        finally
        {
            if (dicResult.Count > 0)
            {
                result = JsonHelper.ObjectToJson(dicResult);
            }
            else
            {
                result = JsonHelper.ObjectToJson(new Sys.Model.OrderComment());
            }

            UtilLog.WriteTextLog(requestId, result);
            Response.Write(result);
            Response.End();
        }
    }

    #region 更新订单

    /// <summary>
    /// 更新订单
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, object> UpdateOrdersStatus()
    {
        Dictionary<string, object> dicResult = new Dictionary<string, object>();
        if (!Request.Params.AllKeys.Contains("ordersId")
            || !Request.Params.AllKeys.Contains("orderStatus") || !Request.Params.AllKeys.Contains("staFlag")
             || !Request.Params.AllKeys.Contains("staMessage"))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "接口缺少参数！");
            return dicResult;
        }
        
        var token = Request["token"];
        var ordersId = Convert.ToInt32(Request["ordersId"]);
        var orderStatus = Convert.ToInt32(Request["orderStatus"]);
        var staFlag = Convert.ToInt32(Request["staFlag"]);
        var staMessage = Request["staMessage"];
        //var picList = Request["PicList"];
        if (!new Sys.BLL.Account().CheckToken(token))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "Token验证失败！");
            return dicResult;
        }

        string message;
        var bllOrder = new Sys.BLL.Orders();
        if (!bllOrder.OrdersStatusCheck(ordersId, orderStatus, staFlag, out message))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", message);
        }
        else
        {
            var picList = UploadPhotos();
            var result = bllOrder.UpdateOrdersStatus(ordersId, orderStatus, staFlag, picList, staMessage);
            if (result)
            {
                dicResult.Add("response_id", 1);
                dicResult.Add("response_msg", "订单状态更新成功！");
            }
        }

        return dicResult;
    }

    #endregion

    /// <summary>
    /// 批量上传图片
    /// </summary>
    public string UploadPhotos()
    {
        var Photos = "";
        try
        {
            if (Request.Files.Count > 0)
            {
                foreach (string str in Request.Files)
                {
                    string path = "";
                    var file = Request.Files[str];
                    path = Utils.CreateSaveFilePath("/Upload/photos/", Utils.PathFormat.Year_Month);
                    string fileName = DataSecurity.MakeFileRndName() + Utils.GetFileExtName(file.FileName);
                    Photos += path + fileName + ",";
                    file.SaveAs(HttpContext.Current.Server.MapPath(path + fileName));
                }
                Photos = Utils.Strquotes(Photos);
            }
            
        }
        catch (Exception ex)
        {
            UtilLog.WriteExceptionLog("Upload Error", ex);
            throw;
        }
        return Photos;
    }

}