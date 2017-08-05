using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Sys.Common;

namespace Sys.DAL
{
    public partial class Orders
    {
        public DataTable GetOrderFlowInfo(int orderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT DISTINCT * FROM dbo.OrderStatusFlow WHERE OrderId=@orderId");
            SqlParameter[] parameters = {
					new SqlParameter("@orderId", SqlDbType.Int,4)};
            parameters[0].Value = orderId;
            return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="ordersId"></param>
        /// <param name="orderStatus"></param>
        /// <param name="staFlag"></param>
        /// <param name="picList"></param>
        /// <param name="staMessage"></param>
        /// <returns></returns>
        public bool UpdateOrdersStatus(int ordersId, int orderStatus, int? staFlag, string picList, string staMessage)
        {
            bool result = false;
            try
            {
                var sqlStr1 = string.Format("UPDATE dbo.Orders SET OrderStatus=@orderStatus,StatusFlag=@StatusFlag,PicList=@PicList WHERE OrderId=@ordersId");
                SqlParameter[] parameters1 = {
                    new SqlParameter("@orderStatus", SqlDbType.VarChar,1),
					new SqlParameter("@StatusFlag", SqlDbType.Int,4),
                    new SqlParameter("@PicList", SqlDbType.VarChar,200),
                    new SqlParameter("@ordersId", SqlDbType.Int,4)};
                parameters1[0].Value = orderStatus;
                parameters1[1].Value = staFlag;
                parameters1[2].Value = picList;
                parameters1[3].Value = ordersId;
                CommandInfo cmdInfo1 = new CommandInfo() { CommandText = sqlStr1, Parameters = parameters1 };

                var sqlStr2 = string.Format(@"INSERT INTO dbo.OrderStatusFlow
        ( 
          OrderId ,
          OrderStatus ,
          StatusMessage ,
          StatusFlag ,
          CreateUserId ,
          CreateDate
        )
VALUES  ( 
          @ordersId , -- OrderId - int
          @orderStatus , -- OrderStatus - char(1)
          @staMessage , -- StatusMessage - nvarchar(100)
          @StatusFlag , -- StatusFlag - int
          '' , -- CreateUserId - varchar(50)
          '{0}'
        )", DateTime.Now);
                SqlParameter[] parameters2 = {
                    new SqlParameter("@ordersId", SqlDbType.Int,4),
					new SqlParameter("@orderStatus", SqlDbType.VarChar,1),
                    new SqlParameter("@StatusFlag", SqlDbType.Int,4),
                    new SqlParameter("@staMessage", SqlDbType.NVarChar,500)};
                parameters2[0].Value = ordersId;
                parameters2[1].Value = orderStatus;
                parameters2[2].Value = staFlag;
                parameters2[3].Value = staMessage;
                CommandInfo cmdInfo2 = new CommandInfo() { CommandText = sqlStr2, Parameters = parameters2 };
                List<CommandInfo> listCmd = new List<CommandInfo>();
                listCmd.Add(cmdInfo1);
                listCmd.Add(cmdInfo2);
                DbHelperSQL.ExecuteSqlTran(listCmd);
                result = true;
            }
            catch (Exception ex)
            {
                UtilLog.WriteExceptionLog("UpdateOrdersStatus Error", ex);
            }

            return result;
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
            ////userType,userLoginId
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  o.OrderId ,
        o.ProjectName ,
        u.USERNAME ,
        o.Address ,
        o.CreateDate ,
        o.OrderStatus ,
        o.StatusFlag ,
        StarLevel = ( SELECT TOP 1
                                StarLevel
                      FROM      dbo.OrderComment
                      WHERE     OrderId = o.OrderId
                      ORDER BY  CreateDate DESC
                    )
FROM    dbo.Orders o
        LEFT JOIN dbo.UserInfo u ON o.UserLoginId = u.UserName
WHERE   1 = 1");
            List<SqlParameter> lp=new List<SqlParameter>();
            if (!string.IsNullOrEmpty(orderStatus))
            {
                strSql.Append(" and o.OrderStatus = @orderStatus");
                lp.Add(new SqlParameter("@orderStatus", orderStatus));                
            }
            if (userType.Equals("1"))
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    strSql.Append(" and o.UserLoginId = @UserLoginId");
                    lp.Add(new SqlParameter("@UserLoginId", userId));
                }
            }
            else if(userType.Equals("2"))
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    strSql.Append(" and o.WorkerId = @WorkerId");
                    lp.Add(new SqlParameter("@WorkerId", userId));
                }
            }

            var lowerBound = pageIndex*pageSize + 1;
            var upperBound = lowerBound + pageSize - 1;

            var pageSql = @"SELECT  OrderId ,
        ProjectName ,
        USERNAME ,
        Address ,
        CreateDate ,
        OrderStatus ,
        StatusFlag ,
        StarLevel ,
        TOTAL_COUNT
FROM    ( SELECT    * ,
                    ROW_NUMBER() OVER ( ORDER BY CreateDate DESC ) AS RowNumber ,
                    COUNT(1) OVER ( ) AS TOTAL_COUNT
          FROM      ( ";
            pageSql += strSql.ToString();
            pageSql += string.Format(@") T1
        ) T2
WHERE   RowNumber BETWEEN {0} AND {1}", lowerBound, upperBound);

            if (lp.Count > 0)
            {
                return DbHelperSQL.Query(pageSql, lp.ToArray()).Tables[0];

            }
            return DbHelperSQL.Query(pageSql).Tables[0];
        }

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(Sys.Model.Orders model, int type)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.Int,4),
					new SqlParameter("@UserLoginId", SqlDbType.VarChar,50),
					new SqlParameter("@WorkerId", SqlDbType.VarChar,50),
					new SqlParameter("@ProjectName", SqlDbType.NVarChar,100),
					new SqlParameter("@CustomName", SqlDbType.NVarChar,100),
					new SqlParameter("@ProvinceId", SqlDbType.Int,4),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar,50),
					new SqlParameter("@CityId", SqlDbType.Int,4),
					new SqlParameter("@CityName", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaId", SqlDbType.Int,4),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@sAreaId", SqlDbType.Int,4),
					new SqlParameter("@sAreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,100),
					new SqlParameter("@Contact", SqlDbType.NVarChar,50),
					new SqlParameter("@Tel", SqlDbType.VarChar,50),
					new SqlParameter("@HandleDate", SqlDbType.DateTime),
					new SqlParameter("@OrderStatus", SqlDbType.Char,1),
					new SqlParameter("@StatusFlag", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@PicList", SqlDbType.VarChar,200),
					new SqlParameter("@CreateUserId", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifyUserId", SqlDbType.VarChar,50),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.UserLoginId;
            parameters[2].Value = model.WorkerId;
            parameters[3].Value = model.ProjectName;
            parameters[4].Value = model.CustomName;
            parameters[5].Value = model.ProvinceId;
            parameters[6].Value = model.ProvinceName;
            parameters[7].Value = model.CityId;
            parameters[8].Value = model.CityName;
            parameters[9].Value = model.AreaId;
            parameters[10].Value = model.AreaName;
            parameters[11].Value = model.sAreaId;
            parameters[12].Value = model.sAreaName;
            parameters[13].Value = model.Address;
            parameters[14].Value = model.Contact;
            parameters[15].Value = model.Tel;
            parameters[16].Value = model.HandleDate;
            parameters[17].Value = model.OrderStatus;
            parameters[18].Value = model.StatusFlag;
            parameters[19].Value = model.Remark;
            parameters[20].Value = model.PicList;
            parameters[21].Value = model.CreateUserId;
            parameters[22].Value = model.CreateDate;
            parameters[23].Value = model.ModifyUserId;
            parameters[24].Value = model.ModifyDate;

            DbHelperSQL.RunProcedure("Orders_ADD_New", parameters, out rowsAffected);
            return (int)parameters[0].Value;
        }

        /// <summary>
        /// 获取订单基础信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable GetOrderByOrderId(int orderId)
        {
            var sqlStr = @"SELECT  o.* ,
        u.RealName AS agentName ,
        u1.RealName AS workerName
FROM    dbo.Orders o
        LEFT JOIN dbo.UserInfo u ON o.UserLoginId = u.UserName
        LEFT JOIN dbo.UserInfo u1 ON o.WorkerId = u1.UserName
WHERE   o.OrderId = @OrderId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@OrderId", orderId)
            };
            return DbHelperSQL.Query(sqlStr, parameters).Tables[0];
        }

        /// <summary>
        /// 订单删除
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool DeleteOrders(int orderId)
        {
            List<string> sqList=new List<string>();
            sqList.Add("DELETE dbo.OrderType WHERE OrderId=" + orderId);
            sqList.Add("DELETE dbo.OrderStatusFlow WHERE OrderId=" + orderId);
            sqList.Add("DELETE dbo.OrderComment WHERE OrderId=" + orderId);
            sqList.Add("DELETE dbo.Orders WHERE OrderId=" + orderId);
            return DbHelperSQL.ExecuteSqlTran(sqList) > 0;
        }


    }
}
