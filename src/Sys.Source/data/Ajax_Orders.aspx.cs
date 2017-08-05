using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.Common;
using Sys.Model;

public partial class data_Ajax_Orders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var methodName = PageRequest.GetString("method");
        var type = this.GetType();
        var method = type.GetMethod(methodName);
        if (method == null) throw new Exception("method is null");
        method.Invoke(this, null);
    }

    /// <summary>
    /// 查询
    /// </summary>
    public void SearchData()
    {
        //查询条件
        var orderStatus = PageRequest.GetString("orderStatus");
        var projectName = PageRequest.GetString("projectName");
        var cmbDate = PageRequest.GetString("cmbDate");
        //分页
        var pageIndex = PageRequest.GetInt("pageIndex", 1) + 1;
        var pageSize = PageRequest.GetInt("pageSize", 1);
        //字段排序
        var sortField = PageRequest.GetString("sortField");
        var sortOrder = PageRequest.GetString("sortOrder");
        var order = "";
        if (String.IsNullOrEmpty(sortField) == false)
        {
            if (sortOrder != "desc") sortOrder = "asc";
            order = " u." + sortField + " " + sortOrder;
        }
        else
        {
            order += " CreateDate desc";
        }
        var strWhere = "1=1";
        if (!string.IsNullOrEmpty(orderStatus))
        {
            if (orderStatus.Equals("0"))
            {
                strWhere += " and  OrderStatus is null";
            }
            else
            {
                strWhere += " and  OrderStatus=" + orderStatus;
            }
            
        }
        if (!string.IsNullOrEmpty(projectName))
        {
            strWhere += string.Format(" and ProjectName like '%{0}%' ", DataSecurity.FilterBadChar(projectName));
        }
        if (!string.IsNullOrEmpty(cmbDate))
        {
            //1day,3day,1week,1month,halfyear,oneyear
            if (cmbDate.Equals("1day"))
            {
                strWhere += " and CreateDate>=DATEADD(DAY,-1,GETDATE())";
            }
            else if (cmbDate.Equals("3day"))
            {
                strWhere += " and CreateDate>=DATEADD(DAY,-3,GETDATE())";
            }
            else if (cmbDate.Equals("1week"))
            {
                strWhere += " and CreateDate>=DATEADD(DAY,-7,GETDATE())";
            }
            else if (cmbDate.Equals("1month"))
            {
                strWhere += " and CreateDate>=DATEADD(DAY,-30,GETDATE())";
            }
            else if (cmbDate.Equals("halfyear"))
            {
                strWhere += " and CreateDate>=DATEADD(DAY,-180,GETDATE())";
            }
            else if (cmbDate.Equals("oneyear"))
            {
                strWhere += " and CreateDate>=DATEADD(DAY,-365,GETDATE())";
            }
        }
        if (pageSize == 1)
        {
            pageSize = 100;
        }
        var dt = new Sys.BLL.Orders().GetListForMoreTable(pageSize, pageIndex, strWhere, order);
        var dataAll = MyDBUtils.DataTable2ArrayList(dt);
        var result = new Hashtable();
        result["data"] = dataAll;
        var total = new Sys.BLL.Common().GetCount("Orders o", strWhere);
        result["total"] = total;
        //JSON
        var json = PluSoft.Utils.JSON.Encode(result);
        Response.Write(json);

    }

    /// <summary>
    /// 订单状态获取
    /// </summary>
    public void GetOrderStatus()
    {
        List<Sys.Model.DicModel> lis;
        new Sys.BLL.Orders().OrdersStatusInit(out lis);
        lis.Insert(0, new DicModel(){Key = "0",Value = "未处理"});
        var json = PluSoft.Utils.JSON.Encode(lis);
        Response.Write(json);
    }

   
    public void GetView()
    {
        int id = PageRequest.GetInt("id", 0);
        var dt = new Sys.BLL.Orders().GetOrderByOrderId(id);
        var data = MyDBUtils.DataTable2ArrayList(dt);
        var htData = data.Count > 0 ? (Hashtable)data[0] : null;
        var json = PluSoft.Utils.JSON.Encode(htData);
        Response.Write(json);
    }


    public void GetOrderType()
    {
        int orderId = PageRequest.GetInt("orderId", 0);
        var dt = new Sys.BLL.OrderType().GetList("OrderId=" + orderId).Tables[0];
        //产品类型显示优化
        if (dt != null)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["ProductType"].ToString().Equals("1"))
                {
                    row["ModelId"] = DBNull.Value;
                    row["ModelName"] = "";
                    row["ModelNum"] = DBNull.Value;
                }
                if (row["ProductType"].ToString().Equals("2"))
                {
                    row["MainModelId"] = DBNull.Value;
                    row["MainModelName"] = "";
                    row["MainModelNum"] = DBNull.Value;

                    row["SubModelId1"] = DBNull.Value;
                    row["SubModelName1"] = "";
                    row["SubModelNum1"] = DBNull.Value;

                    row["SubModelId2"] = DBNull.Value;
                    row["SubModelName2"] = "";
                    row["SubModelNum2"] = DBNull.Value;

                    row["SubModelId3"] = DBNull.Value;
                    row["SubModelName3"] = "";
                    row["SubModelNum3"] = DBNull.Value;

                    row["SubModelId4"] = DBNull.Value;
                    row["SubModelName4"] = "";
                    row["SubModelNum4"] = DBNull.Value;
                }
            }
            dt.AcceptChanges();
        }
        var json = PluSoft.Utils.JSON.Encode(dt);
        Response.Write(json);
    }

    /// <summary>
    /// 订单人员分配
    /// </summary>
    public void GetUserInfoByArea()
    {
        var condition = "1=1";
        if (!string.IsNullOrEmpty(Request["proId"]))
        {
            condition += " and ProvinceId=" + Request["proId"];
        }
        if (!string.IsNullOrEmpty(Request["cityId"]))
        {
            condition += " and CityId=" + Request["cityId"];
        }
        if (!string.IsNullOrEmpty(Request["areaId"]))
        {
            condition += " and AreaId=" + Request["areaId"];
        }
        var dt = new Sys.BLL.UserInfo().GetList(condition).Tables[0];
        List<Sys.Model.DicModel> lis = new List<Sys.Model.DicModel>();
        foreach (DataRow row in dt.Rows)
        {
            lis.Add(new DicModel() { Key = row["UserName"].ToString(), Value = row["RealName"].ToString() });
        }
        var json = PluSoft.Utils.JSON.Encode(lis);
        Response.Write(json);
    }

    /// <summary>
    /// 订单分配
    /// </summary>
    public void InstallerAssign()
    {
        var json = Request["data"];
        var rows = (ArrayList)PluSoft.Utils.JSON.Decode(json);
        foreach (Hashtable row in rows)
        {
            var orderId = Convert.ToInt32(row["OrderId"]);
            var detailCount = new Sys.BLL.OrderStatusFlow().GetRecordCount("OrderId=" + orderId);
            if (detailCount > 0)
            {
                Response.Write("-1");
                return;
            }
            else
            {
                var model = new Sys.BLL.Orders().GetModel(orderId);
                if (model != null)
                {
                    model.WorkerId = row["newWorkerId"].ToString();
                    var result = new Sys.BLL.Orders().Update(model);
                    if (result)
                    {
                        Response.Write("1");
                    }
                }
            }
        }

    }

    public void DelOrder()
    {
        int id = PageRequest.GetInt("id", 0);
        var bll = new Sys.BLL.Orders();
        if (bll.DeleteOrders(id))
        {
            Response.Write("1");
        }
        else
        {
            Response.Write("0");
        }
    }

    public void GetOrderFlow()
    {
        int id = PageRequest.GetInt("orderId", 0);
        var dt = new Sys.BLL.OrderStatusFlow().GetList("OrderId=" + id).Tables[0];
        if (dt != null)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = "FlowId desc";
            dt = dv.ToTable();
        }
        var json = PluSoft.Utils.JSON.Encode(dt);
        Response.Write(json);
    }


}