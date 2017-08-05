using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Midea.Common;
using Sys.Common;

public partial class MobileInterface_InterfaceService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UtilLog.WriteTextLog("------", "调用开始");
        Stream resStream = HttpContext.Current.Request.InputStream;
        string strPostContent = string.Empty;
        string result = string.Empty;
        string dicType = string.Empty;
        using (StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8))
        {
            strPostContent = sr.ReadToEnd();
        }

        //strPostContent =
        //    @"{""request_id"":""update_orderstatus"",""token"":""88888888888|7626d3bc9ebd8738885c9ed14155b647"",""ordersId"":""60"",""orderStatus"":"""",""staFlag"":""1"",""staMessage"":""在学校"",""PicList"":""""}";

        UtilLog.WriteTextLog("收到数据", strPostContent);

        Dictionary<string, object> dicResult = new Dictionary<string, object>();
        try
        {
            if (!string.IsNullOrEmpty(strPostContent))
            {
                Dictionary<string, object> dicParams =
                    JsonHelper.JsonToObject<Dictionary<string, object>>(strPostContent);

                if (dicParams != null && dicParams.ContainsKey("request_id"))
                {
                    dicType = dicParams["request_id"].ToString();
                    switch (dicParams["request_id"].ToString())
                    {
                        case "register_init":
                            dicResult = RegisterInit(dicParams);
                            break;
                        case "register":
                            dicResult = Register(dicParams);
                            break;
                        case "login":
                            dicResult = GetUserInfo(dicParams);
                            break;
                        case "orders_init":
                            dicResult = OrdersInit(dicParams);
                            break;
                        case "create_orders":
                            dicResult = CreateOrders(dicParams);
                            break;
                        case "get_orders_list":
                            dicResult = GetOrdersList(dicParams);
                            break;
                        case "get_orders_detail":
                            dicResult = GetOrdersInfo(dicParams);
                            break;
                        case "update_orderstatus_init":
                            dicResult = UpdateOrdersInit(dicParams);
                            break;
                        case "update_orderstatus":
                            dicResult = UpdateOrdersStatus(dicParams);
                            break;
                        case "orders_comment":
                            dicResult = OrdersComment(dicParams);
                            break;

                    }
                }
            }
        }
        catch (Exception ex)
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "处理异常！");
            UtilLog.WriteExceptionLog(dicType, ex);
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

            UtilLog.WriteTextLog(dicType, result);
            Response.Write(result);
            Response.End();
        }

    }

    #region 注册初始化

    /// <summary>
    /// 注册初始化
    /// </summary>
    /// <param name="dicParams"></param>
    /// <returns></returns>
    public Dictionary<string, object> RegisterInit(Dictionary<string, object> dicParams)
    {
        Dictionary<string, object> dicResult = new Dictionary<string, object>();
        try
        {
            List<Sys.Model.DicModel> lisAgentType, lisInstallerTypeList;
            new Sys.BLL.Account().RegisterInit(out lisAgentType, out lisInstallerTypeList);
            dicResult.Add("AgentType", lisAgentType);
            dicResult.Add("InstallerTypeList", lisInstallerTypeList);

            List<Sys.Model.DicModel> lisProvice, lisCity, lisArea;
            new Sys.BLL.UserInfo().AreaInit(out lisProvice, out lisCity, out lisArea);
            dicResult.Add("Provice", lisProvice);
            dicResult.Add("City", lisCity);
            dicResult.Add("Area", lisArea);
            
            dicResult.Add("response_id", 1);
            dicResult.Add("response_msg", "初始化成功！");
        }
        catch (Exception)
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "登陆失败！");
            throw;
        }

        return dicResult;
    }

    #endregion

    #region 注册

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="dicParams"></param>
    /// <returns></returns>
    public Dictionary<string, object> Register(Dictionary<string, object> dicParams)
    {
        Dictionary<string, object> dicResult = new Dictionary<string, object>();
        var dt = JsonHelper.JsonToDataTable(dicParams["user_info"].ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            var row = dt.Rows[0];
            var dtUser = new Sys.BLL.Account().GetUserInfoByUserLoginId(row["USERLOGINID"].ToString());
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                dicResult.Add("response_id", 0);
                dicResult.Add("response_msg", "手机号重复，不能注册！");
                return dicResult;
            }

            var modelUserInfo = new Sys.Model.UserInfo()
            {
                UserName = row["USERLOGINID"].ToString(),
                RealName = row["USERNAME"].ToString(),
                CompanyName = row["CompanyName"].ToString(),
                ProvinceId = !string.IsNullOrEmpty(row["ProvinceId"].ToString()) ? Convert.ToInt32(row["ProvinceId"].ToString()) : 0,
                ProvinceName = row["ProvinceName"].ToString(),
                CityId = !string.IsNullOrEmpty(row["CityId"].ToString()) ? Convert.ToInt32(row["CityId"].ToString()) : 0,
                CityName = row["CityName"].ToString(),
                AreaId = !string.IsNullOrEmpty(row["AreaId"].ToString()) ? Convert.ToInt32(row["AreaId"].ToString()) : 0,
                AreaName = row["AreaName"].ToString(),
                Address = row["Address"].ToString(),
                contact = row["contact"].ToString(),
                TypeKey = row["TypeKey"].ToString(),
                TypeValue = row["TypeValue"].ToString(),
                RoleId = !string.IsNullOrEmpty(row["RoleId"].ToString()) ? Convert.ToInt32(row["RoleId"].ToString()) : 0,
            };
            var result = new Sys.BLL.UserInfo().Add(modelUserInfo, Utils.MD5(row["PASSWORD"].ToString()));
            if (result > 0)
            {
                dicResult.Add("response_id", 1);
                dicResult.Add("response_msg", "注册成功！");
            }
            else
            {
                dicResult.Add("response_id", 0);
                dicResult.Add("response_msg", "注册失败！");
            }

        }

        return dicResult;
    }

    #endregion


    #region 用户登录
    /// <summary>
    /// 用户登陆
    /// </summary>
    /// <param name="dicParams"></param>
    /// <returns></returns>
    private Dictionary<string, object> GetUserInfo(Dictionary<string, object> dicParams)
    {
        Dictionary<string, object> dicResult = new Dictionary<string, object>();
        if (!dicParams.ContainsKey("user_id") || !dicParams.ContainsKey("password"))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "登陆接口缺少参数！");
        }
        else
        {
            string userId = dicParams["user_id"].ToString();
            string password = Utils.MD5(dicParams["password"].ToString());

            var dt = new Sys.BLL.Account().UserLogin(userId, password);
            if (dt == null || dt.Rows.Count == 0)
            {
                // 失败，返回信息
                dicResult.Add("response_id", 0);
                dicResult.Add("response_msg", "登陆失败！");
            }
            else
            {
                var state = dt.Rows[0]["State"];
                if (state.ToString().Equals("0"))
                {
                    dicResult.Add("response_id", 0);
                    dicResult.Add("response_msg", "用户未激活！");
                }
                else
                {
                    dicResult.Add("response_id", 1);
                    dicResult.Add("response_msg", "登陆成功！");
                    dicResult.Add("user_info", dt);
                }
                
            }
        }

        return dicResult;
    }

    #endregion


    #region 创建订单初始化

    /// <summary>
    /// 创建订单初始化
    /// </summary>
    /// <param name="dicParams"></param>
    /// <returns></returns>
    public Dictionary<string, object> OrdersInit(Dictionary<string, object> dicParams)
    {
        Dictionary<string, object> dicResult = new Dictionary<string, object>();
        if (!dicParams.ContainsKey("token"))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "登陆接口缺少参数！");
            return dicResult;
        }
        if (!new Sys.BLL.Account().CheckToken(dicParams["token"].ToString()))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "Token验证失败！");
            return dicResult;
        }

        DataTable dtParent, dtSub;
        new Sys.BLL.EquipmentModel().GetEqpInfo(out dtParent, out dtSub);
        dicResult.Add("parent_eqpmodel_info", dtParent);
        dicResult.Add("sub_eqpmodel_info", dtSub);

        List<Sys.Model.DicModel> lisProvice, lisCity, lisArea;
        new Sys.BLL.UserInfo().AreaInit(out lisProvice, out lisCity, out lisArea);
        dicResult.Add("Provice", lisProvice);
        dicResult.Add("City", lisCity);
        dicResult.Add("Area", lisArea);

        dicResult.Add("response_id", 1);
        dicResult.Add("response_msg", "初始化成功！");

        return dicResult;
    }

    #endregion

    #region 创建订单

    /// <summary>
    /// 创建订单
    /// </summary>
    /// <param name="dicParams"></param>
    /// <returns></returns>
    private Dictionary<string, object> CreateOrders(Dictionary<string, object> dicParams)
    {
        Dictionary<string, object> dicResult = new Dictionary<string, object>();
        if (!dicParams.ContainsKey("orders") || !dicParams.ContainsKey("orders_type") || !dicParams.ContainsKey("token"))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "登陆接口缺少参数！");
            return dicResult;
        }
        if (!new Sys.BLL.Account().CheckToken(dicParams["token"].ToString()))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "Token验证失败！");
            return dicResult;
        }

        var ds = new DataSet();
        ds.Tables.Add(JsonHelper.JsonToDataTable(dicParams["orders"].ToString()));
        ds.Tables.Add(JsonHelper.JsonToDataTable(dicParams["orders_type"].ToString()));
        var ordersList = JsonHelper.DataSetToIList<Sys.Model.Orders>(ds, 0);
        var orderTypeList = JsonHelper.DataSetToIList<Sys.Model.OrderType>(ds, 1);
        if (ordersList == null || orderTypeList == null)
        {
            // 失败，返回信息
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "创建失败！");
        }
        else
        {
            bool result = new Sys.BLL.Orders().CreateOrders(ordersList, orderTypeList);
            if (result)
            {
                dicResult.Add("response_id", 1);
                dicResult.Add("response_msg", "订单创建成功！");
            }
            else
            {
                dicResult.Add("response_id", 0);
                dicResult.Add("response_msg", "订单创建失败！");
            }

        }

        return dicResult;
    }

    #endregion

    #region 订单列表

    /// <summary>
    /// 订单列表
    /// </summary>
    /// <param name="dicParams"></param>
    /// <returns></returns>
    private Dictionary<string, object> GetOrdersList(Dictionary<string, object> dicParams)
    {
        Dictionary<string, object> dicResult = new Dictionary<string, object>();
        if (!dicParams.ContainsKey("token") || !dicParams.ContainsKey("orderStatus")
             || !dicParams.ContainsKey("pageIndex") || !dicParams.ContainsKey("pageSize"))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "接口缺少参数！");
            return dicResult;
        }
        if (!new Sys.BLL.Account().CheckToken(dicParams["token"].ToString()))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "Token验证失败！");
            return dicResult;
        }
        
        var orderStatus = dicParams["orderStatus"].ToString();
        var userType = dicParams["userType"].ToString();
        var userLoginId = dicParams["userLoginId"].ToString();
        var pageIndex = Convert.ToInt32(dicParams["pageIndex"]);
        var pageSize = Convert.ToInt32(dicParams["pageSize"]);
        var dt = new Sys.BLL.Orders().GetOrdersList(orderStatus, userType, userLoginId, pageIndex, pageSize);
        if (dt == null || dt.Rows.Count == 0)
        {
            // 失败，返回信息
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "暂无数据！");
        }
        else
        {
            dicResult.Add("response_id", 1);
            dicResult.Add("response_msg", "订单信息获取成功！");
            dicResult.Add("orders_info", dt);
        }

        return dicResult;
    }

    #endregion


    #region 订单明细

    /// <summary>
    /// 根据UserLoginId获取用户信息和订单信息
    /// </summary>
    /// <param name="dicParams"></param>
    /// <returns></returns>
    private Dictionary<string, object> GetOrdersInfo(Dictionary<string, object> dicParams)
    {
        Dictionary<string, object> dicResult = new Dictionary<string, object>();
        if (!dicParams.ContainsKey("token") || !dicParams.ContainsKey("orderId"))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "接口缺少参数！");
            return dicResult;
        }
        if (!new Sys.BLL.Account().CheckToken(dicParams["token"].ToString()))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "Token验证失败！");
            return dicResult;
        }

        int orderId = Utils.StrToInt(dicParams["orderId"], 0);
        var orderModel = new Sys.BLL.Orders().GetModel(orderId);
        var orderTypeList = new Sys.BLL.OrderType().GetModelList("OrderId=" + orderId);
        var orderFlow = new Sys.BLL.Orders().GetOrderFlowInfo(orderId);
        if (orderModel == null || orderTypeList==null)
        {
            // 失败，返回信息
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "订单明细获取异常！");
        }
        else
        {
            dicResult.Add("response_id", 1);
            dicResult.Add("response_msg", "订单信息获取成功！");
            dicResult.Add("order_main", orderModel);
            dicResult.Add("order_detail", orderTypeList);
            dicResult.Add("order_flow", orderFlow);
        }

        return dicResult;
    }

    #endregion


    #region 更新订单时初始化

    /// <summary>
    /// 更新订单时初始化
    /// </summary>
    /// <param name="dicParams"></param>
    /// <returns></returns>
    public Dictionary<string, object> UpdateOrdersInit(Dictionary<string, object> dicParams)
    {
        Dictionary<string, object> dicResult = new Dictionary<string, object>();
        if (!dicParams.ContainsKey("token"))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "接口缺少参数！");
            return dicResult;
        }
        if (!new Sys.BLL.Account().CheckToken(dicParams["token"].ToString()))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "Token验证失败！");
            return dicResult;
        }

        List<Sys.Model.DicModel> lis;
        new Sys.BLL.Orders().OrdersStatusInit(out lis);
        dicResult.Add("OrdersStatus", lis);

        dicResult.Add("response_id", 1);
        dicResult.Add("response_msg", "初始化成功！");

        return dicResult;
    }

    #endregion

    #region 更新订单

    /// <summary>
    /// 更新订单
    /// </summary>
    /// <param name="dicParams"></param>
    /// <returns></returns>
    public Dictionary<string, object> UpdateOrdersStatus(Dictionary<string, object> dicParams)
    {
        Dictionary<string, object> dicResult = new Dictionary<string, object>();
        //int ordersId, string orderStatus, int staFlag
        if (!dicParams.ContainsKey("token") || !dicParams.ContainsKey("ordersId")
            || !dicParams.ContainsKey("orderStatus") || !dicParams.ContainsKey("staFlag")
             || !dicParams.ContainsKey("PicList") || !dicParams.ContainsKey("staMessage"))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "接口缺少参数！");
            return dicResult;
        }
        if (!new Sys.BLL.Account().CheckToken(dicParams["token"].ToString()))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "Token验证失败！");
            return dicResult;
        }
        int ordersId = Convert.ToInt32(dicParams["ordersId"]);
        int orderStatus = Convert.ToInt32(dicParams["orderStatus"]);
        int staFlag = Convert.ToInt32(dicParams["staFlag"]);
        string picList = dicParams["PicList"].ToString();
        string staMessage = dicParams["staMessage"].ToString();

        string message;
        var bllOrder = new Sys.BLL.Orders();
        if (!bllOrder.OrdersStatusCheck(ordersId, orderStatus, staFlag, out message))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", message);
        }
        else
        {
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

    #region 订单评价

    /// <summary>
    /// 订单评价
    /// </summary>
    /// <param name="dicParams"></param>
    /// <returns></returns>
    public Dictionary<string, object> OrdersComment(Dictionary<string, object> dicParams)
    {
        Dictionary<string, object> dicResult = new Dictionary<string, object>();
        //int ordersId, string orderStatus, int staFlag
        if (!dicParams.ContainsKey("token"))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "接口缺少参数！");
            return dicResult;
        }
        if (!new Sys.BLL.Account().CheckToken(dicParams["token"].ToString()))
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "Token验证失败！");
            return dicResult;
        }

        string commentInfo = dicParams["comment_info"].ToString();
        var dt = JsonHelper.JsonToDataTable(commentInfo);
        var ds = new DataSet();
        ds.Tables.Add(dt);
        var list = JsonHelper.DataSetToIList<Sys.Model.OrderComment>(ds, 0);
        if (list != null && list.Count > 0)
        {
            var model = list[0];
            model.CreateDate = DateTime.Now;
            if (new Sys.BLL.OrderComment().Add(list[0]) > 0)
            {
                dicResult.Add("response_id", 1);
                dicResult.Add("response_msg", "评论添加成功！");
            }
        }
        else
        {
            dicResult.Add("response_id", 0);
            dicResult.Add("response_msg", "评论添加失败！");
        }

        return dicResult;
    }

    #endregion

    
}