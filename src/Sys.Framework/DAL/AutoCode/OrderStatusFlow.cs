using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;

namespace Sys.DAL
{
	/// <summary>
	/// 数据访问类:OrderStatusFlow
	/// </summary>
	public partial class OrderStatusFlow
	{
		public OrderStatusFlow()
		{}
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int FlowId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from OrderStatusFlow");
            strSql.Append(" where FlowId=@FlowId");
            SqlParameter[] parameters = {
					new SqlParameter("@FlowId", SqlDbType.Int,4)
			};
            parameters[0].Value = FlowId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys.Model.OrderStatusFlow model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OrderStatusFlow(");
            strSql.Append("OrderId,OrderStatus,StatusMessage,StatusFlag,CreateUserId,CreateDate,ModifyUserId,ModifyDate)");
            strSql.Append(" values (");
            strSql.Append("@OrderId,@OrderStatus,@StatusMessage,@StatusFlag,@CreateUserId,@CreateDate,@ModifyUserId,@ModifyDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.Int,4),
					new SqlParameter("@OrderStatus", SqlDbType.Char,1),
					new SqlParameter("@StatusMessage", SqlDbType.NVarChar,500),
					new SqlParameter("@StatusFlag", SqlDbType.Int,4),
					new SqlParameter("@CreateUserId", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifyUserId", SqlDbType.VarChar,50),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime)};
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.OrderStatus;
            parameters[2].Value = model.StatusMessage;
            parameters[3].Value = model.StatusFlag;
            parameters[4].Value = model.CreateUserId;
            parameters[5].Value = model.CreateDate;
            parameters[6].Value = model.ModifyUserId;
            parameters[7].Value = model.ModifyDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Sys.Model.OrderStatusFlow model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OrderStatusFlow set ");
            strSql.Append("OrderId=@OrderId,");
            strSql.Append("OrderStatus=@OrderStatus,");
            strSql.Append("StatusMessage=@StatusMessage,");
            strSql.Append("StatusFlag=@StatusFlag,");
            strSql.Append("CreateUserId=@CreateUserId,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("ModifyUserId=@ModifyUserId,");
            strSql.Append("ModifyDate=@ModifyDate");
            strSql.Append(" where FlowId=@FlowId");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.Int,4),
					new SqlParameter("@OrderStatus", SqlDbType.Char,1),
					new SqlParameter("@StatusMessage", SqlDbType.NVarChar,500),
					new SqlParameter("@StatusFlag", SqlDbType.Int,4),
					new SqlParameter("@CreateUserId", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifyUserId", SqlDbType.VarChar,50),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime),
					new SqlParameter("@FlowId", SqlDbType.Int,4)};
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.OrderStatus;
            parameters[2].Value = model.StatusMessage;
            parameters[3].Value = model.StatusFlag;
            parameters[4].Value = model.CreateUserId;
            parameters[5].Value = model.CreateDate;
            parameters[6].Value = model.ModifyUserId;
            parameters[7].Value = model.ModifyDate;
            parameters[8].Value = model.FlowId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int FlowId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OrderStatusFlow ");
            strSql.Append(" where FlowId=@FlowId");
            SqlParameter[] parameters = {
					new SqlParameter("@FlowId", SqlDbType.Int,4)
			};
            parameters[0].Value = FlowId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string FlowIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OrderStatusFlow ");
            strSql.Append(" where FlowId in (" + FlowIdlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.OrderStatusFlow GetModel(int FlowId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 FlowId,OrderId,OrderStatus,StatusMessage,StatusFlag,CreateUserId,CreateDate,ModifyUserId,ModifyDate from OrderStatusFlow ");
            strSql.Append(" where FlowId=@FlowId");
            SqlParameter[] parameters = {
					new SqlParameter("@FlowId", SqlDbType.Int,4)
			};
            parameters[0].Value = FlowId;

            Sys.Model.OrderStatusFlow model = new Sys.Model.OrderStatusFlow();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["FlowId"] != null && ds.Tables[0].Rows[0]["FlowId"].ToString() != "")
                {
                    model.FlowId = int.Parse(ds.Tables[0].Rows[0]["FlowId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderId"] != null && ds.Tables[0].Rows[0]["OrderId"].ToString() != "")
                {
                    model.OrderId = int.Parse(ds.Tables[0].Rows[0]["OrderId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderStatus"] != null && ds.Tables[0].Rows[0]["OrderStatus"].ToString() != "")
                {
                    model.OrderStatus = ds.Tables[0].Rows[0]["OrderStatus"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StatusMessage"] != null && ds.Tables[0].Rows[0]["StatusMessage"].ToString() != "")
                {
                    model.StatusMessage = ds.Tables[0].Rows[0]["StatusMessage"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StatusFlag"] != null && ds.Tables[0].Rows[0]["StatusFlag"].ToString() != "")
                {
                    model.StatusFlag = int.Parse(ds.Tables[0].Rows[0]["StatusFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateUserId"] != null && ds.Tables[0].Rows[0]["CreateUserId"].ToString() != "")
                {
                    model.CreateUserId = ds.Tables[0].Rows[0]["CreateUserId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ModifyUserId"] != null && ds.Tables[0].Rows[0]["ModifyUserId"].ToString() != "")
                {
                    model.ModifyUserId = ds.Tables[0].Rows[0]["ModifyUserId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ModifyDate"] != null && ds.Tables[0].Rows[0]["ModifyDate"].ToString() != "")
                {
                    model.ModifyDate = DateTime.Parse(ds.Tables[0].Rows[0]["ModifyDate"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FlowId,OrderId,OrderStatus,StatusMessage,StatusFlag,CreateUserId,CreateDate,ModifyUserId,ModifyDate ");
            strSql.Append(" FROM OrderStatusFlow ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" FlowId,OrderId,OrderStatus,StatusMessage,StatusFlag,CreateUserId,CreateDate,ModifyUserId,ModifyDate ");
            strSql.Append(" FROM OrderStatusFlow ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM OrderStatusFlow ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.FlowId desc");
            }
            strSql.Append(")AS Row, T.*  from OrderStatusFlow T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "OrderStatusFlow";
            parameters[1].Value = "FlowId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
	}
}

