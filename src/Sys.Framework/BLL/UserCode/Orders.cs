using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using Sys.Common;
using Sys.Model;

namespace Sys.BLL
{
    public partial class Orders
    {
        public DataTable GetOrderFlowInfo(int orderId)
        {
            return dal.GetOrderFlowInfo(orderId);
        }

        /// <summary>
        /// 订单创建
        /// </summary>
        /// <param name="ordersList"></param>
        /// <param name="orderTypeList"></param>
        /// <returns></returns>
        public bool CreateOrders(IList<Sys.Model.Orders> ordersList, IList<Sys.Model.OrderType> orderTypeList)
        {
            bool result = false;
            try
            {
                if (ordersList.Count > 0)
                {
                    Model.Orders ordersModel = ordersList[0];
                    ordersModel.CreateDate = DateTime.Now;
                    var orderId = dal.Add(ordersModel, 1);

                    foreach (Model.OrderType item in orderTypeList)
                    {
                        item.OrderId = orderId;
                        new Sys.BLL.OrderType().Add(item);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                UtilLog.WriteExceptionLog("CreateOrders Error", ex);
            }
            return result;
        }

        /// <summary>
        /// 订单状态初始化
        /// </summary>
        /// <param name="lis"></param>
        public void OrdersStatusInit(out List<DicModel> lis)
        {
            lis=new List<DicModel>();
            //产品类型
            XmlNodeList OrdersStatus = XmlHelper.GetXmlNodeList(HttpContext.Current.Server.MapPath("../ConfigData/TypeData.xml"), "/items/module[@key='OrdersStatus']/item");
            foreach (XmlNode node in OrdersStatus)
            {
                lis.Add(new DicModel() { Key = node.Attributes["key"].Value, Value = node.InnerText });
            }

        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="ordersId"></param>
        /// <param name="orderStatus"></param>
        /// <param name="staFlag"></param>
        /// <param name="picList"></param>
        /// <param name="staMessage"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool UpdateOrdersStatus(int ordersId, int orderStatus, int staFlag, string picList, string staMessage)
        {
            if (dal.UpdateOrdersStatus(ordersId, orderStatus, staFlag, picList, staMessage))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 订单状态验证
        /// </summary>
        /// <param name="ordersId"></param>
        /// <param name="orderStatus"></param>
        /// <param name="staFlag"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool OrdersStatusCheck(int ordersId, int orderStatus, int staFlag, out string message)
        {
            bool flag = true;
            message = string.Empty;

            //验证当前到哪一步
            var model = dal.GetModel(ordersId);
            if (model != null)
            {
                int iOrderStatus = orderStatus;

                //更新状态时加入
                var statusFlow = new Sys.BLL.OrderStatusFlow().GetModelList("OrderId=" + ordersId);
                if (statusFlow != null && statusFlow.Count > 0)
                {
                    var statusModel = statusFlow.OrderByDescending(f => f.FlowId).FirstOrDefault();
                    if (statusModel != null)
                    {
                        int curStatus = Convert.ToInt32(statusModel.OrderStatus);
                        int curStatusFlag = Convert.ToInt32(statusModel.StatusFlag);
                        if (iOrderStatus < curStatus)
                        {
                            flag = false;
                            message = "订单状态不能为之前状态！";
                        }
                        else
                        {
                            if (iOrderStatus == curStatus)
                            {
                                if (curStatusFlag == 1)
                                {
                                    flag = false;
                                    message = "订单状态已完成，不能选择当前步骤！";
                                }
                                else
                                {
                                    if (staFlag == 0)
                                    {
                                        flag = false;
                                        message = "订单状态已完成，不能选择之前步骤！";
                                    }
                                }
                            }
                            else
                            {
                                if (iOrderStatus != curStatus + 1)
                                {
                                    flag = false;
                                    message = "订单状态必需按步选择！";
                                }
                                else
                                {
                                    if (curStatusFlag == 0)
                                    {
                                        flag = false;
                                        message = "上一步处理未完成，请选完成！";
                                    }
                                    

                                }
                            }
                        }
                    }
                }
                else
                {
                    if (iOrderStatus != 1)
                    {
                        flag = false;
                        message = "订单状态必需从第一步开始！";
                    }
                    
                }
            }

            return flag;
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="orderStatus"></param>
        /// <param name="userType"></param>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataTable GetOrdersList(string orderStatus, string userType, string userId, int pageIndex, int pageSize)
        {
            return dal.GetOrdersList(orderStatus, userType, userId, pageIndex, pageSize);
        }

        /// <summary>
        /// 返回一个数据表  List的前身
        /// </summary>
        public DataTable GetTable(int pageSize, int pageIndex, string strWhere, string strOrder)
        {
            var db = new DAL.Common();
            var ds = db.GetList("Orders", pageSize, pageIndex, strWhere, strOrder);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取订单基础信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable GetOrderByOrderId(int orderId)
        {
            return dal.GetOrderByOrderId(orderId);
        }

        /// <summary>
        /// 订单列表查询
        /// </summary>
        public DataTable GetListForMoreTable(int pageSize, int pageIndex, string strWhere, string order)
        {
            var db = new DAL.Common();
            //var ds = db.GetListForMoreTable("UserInfo u,Orders o", pageSize, pageIndex, condition, order, "u.RealName AS workerName ,o.*");
            var ds = db.GetList("Orders", pageSize, pageIndex, strWhere, order, "UserInfo", "RealName AS workerName", "WorkerId = UserInfo.UserName");
            return ds.Tables[0];
        }

        /// <summary>
        /// 订单删除
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool DeleteOrders(int orderId)
        {
            return dal.DeleteOrders(orderId);
        }
    }
}
