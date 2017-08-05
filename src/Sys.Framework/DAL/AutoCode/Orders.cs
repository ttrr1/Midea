using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;

namespace Sys.DAL
{
	/// <summary>
	/// 数据访问类:Orders
	/// </summary>
	public partial class Orders
	{
		public Orders()
		{}
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int OrderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Orders");
            strSql.Append(" where OrderId=@OrderId");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.Int,4)
			};
            parameters[0].Value = OrderId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys.Model.Orders model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Orders(");
            strSql.Append("UserLoginId,WorkerId,ProjectName,CustomName,ProvinceId,ProvinceName,CityId,CityName,AreaId,AreaName,sAreaId,sAreaName,Address,Contact,Tel,HandleDate,OrderStatus,StatusFlag,Remark,PicList,CreateUserId,CreateDate,ModifyUserId,ModifyDate)");
            strSql.Append(" values (");
            strSql.Append("@UserLoginId,@WorkerId,@ProjectName,@CustomName,@ProvinceId,@ProvinceName,@CityId,@CityName,@AreaId,@AreaName,@sAreaId,@sAreaName,@Address,@Contact,@Tel,@HandleDate,@OrderStatus,@StatusFlag,@Remark,@PicList,@CreateUserId,@CreateDate,@ModifyUserId,@ModifyDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
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
					new SqlParameter("@OrderStatus", SqlDbType.VarChar,1),
					new SqlParameter("@StatusFlag", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@PicList", SqlDbType.VarChar,200),
					new SqlParameter("@CreateUserId", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifyUserId", SqlDbType.VarChar,50),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime)};
            parameters[0].Value = model.UserLoginId;
            parameters[1].Value = model.WorkerId;
            parameters[2].Value = model.ProjectName;
            parameters[3].Value = model.CustomName;
            parameters[4].Value = model.ProvinceId;
            parameters[5].Value = model.ProvinceName;
            parameters[6].Value = model.CityId;
            parameters[7].Value = model.CityName;
            parameters[8].Value = model.AreaId;
            parameters[9].Value = model.AreaName;
            parameters[10].Value = model.sAreaId;
            parameters[11].Value = model.sAreaName;
            parameters[12].Value = model.Address;
            parameters[13].Value = model.Contact;
            parameters[14].Value = model.Tel;
            parameters[15].Value = model.HandleDate;
            parameters[16].Value = model.OrderStatus;
            parameters[17].Value = model.StatusFlag;
            parameters[18].Value = model.Remark;
            parameters[19].Value = model.PicList;
            parameters[20].Value = model.CreateUserId;
            parameters[21].Value = model.CreateDate;
            parameters[22].Value = model.ModifyUserId;
            parameters[23].Value = model.ModifyDate;

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
        public bool Update(Sys.Model.Orders model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Orders set ");
            strSql.Append("UserLoginId=@UserLoginId,");
            strSql.Append("WorkerId=@WorkerId,");
            strSql.Append("ProjectName=@ProjectName,");
            strSql.Append("CustomName=@CustomName,");
            strSql.Append("ProvinceId=@ProvinceId,");
            strSql.Append("ProvinceName=@ProvinceName,");
            strSql.Append("CityId=@CityId,");
            strSql.Append("CityName=@CityName,");
            strSql.Append("AreaId=@AreaId,");
            strSql.Append("AreaName=@AreaName,");
            strSql.Append("sAreaId=@sAreaId,");
            strSql.Append("sAreaName=@sAreaName,");
            strSql.Append("Address=@Address,");
            strSql.Append("Contact=@Contact,");
            strSql.Append("Tel=@Tel,");
            strSql.Append("HandleDate=@HandleDate,");
            strSql.Append("OrderStatus=@OrderStatus,");
            strSql.Append("StatusFlag=@StatusFlag,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("PicList=@PicList,");
            strSql.Append("CreateUserId=@CreateUserId,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("ModifyUserId=@ModifyUserId,");
            strSql.Append("ModifyDate=@ModifyDate");
            strSql.Append(" where OrderId=@OrderId");
            SqlParameter[] parameters = {
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
					new SqlParameter("@OrderStatus", SqlDbType.VarChar,1),
					new SqlParameter("@StatusFlag", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@PicList", SqlDbType.VarChar,200),
					new SqlParameter("@CreateUserId", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifyUserId", SqlDbType.VarChar,50),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime),
					new SqlParameter("@OrderId", SqlDbType.Int,4)};
            parameters[0].Value = model.UserLoginId;
            parameters[1].Value = model.WorkerId;
            parameters[2].Value = model.ProjectName;
            parameters[3].Value = model.CustomName;
            parameters[4].Value = model.ProvinceId;
            parameters[5].Value = model.ProvinceName;
            parameters[6].Value = model.CityId;
            parameters[7].Value = model.CityName;
            parameters[8].Value = model.AreaId;
            parameters[9].Value = model.AreaName;
            parameters[10].Value = model.sAreaId;
            parameters[11].Value = model.sAreaName;
            parameters[12].Value = model.Address;
            parameters[13].Value = model.Contact;
            parameters[14].Value = model.Tel;
            parameters[15].Value = model.HandleDate;
            parameters[16].Value = model.OrderStatus;
            parameters[17].Value = model.StatusFlag;
            parameters[18].Value = model.Remark;
            parameters[19].Value = model.PicList;
            parameters[20].Value = model.CreateUserId;
            parameters[21].Value = model.CreateDate;
            parameters[22].Value = model.ModifyUserId;
            parameters[23].Value = model.ModifyDate;
            parameters[24].Value = model.OrderId;

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
        public bool Delete(int OrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Orders ");
            strSql.Append(" where OrderId=@OrderId");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.Int,4)
			};
            parameters[0].Value = OrderId;

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
        public bool DeleteList(string OrderIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Orders ");
            strSql.Append(" where OrderId in (" + OrderIdlist + ")  ");
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
        public Sys.Model.Orders GetModel(int OrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 OrderId,UserLoginId,WorkerId,ProjectName,CustomName,ProvinceId,ProvinceName,CityId,CityName,AreaId,AreaName,sAreaId,sAreaName,Address,Contact,Tel,HandleDate,OrderStatus,StatusFlag,Remark,PicList,CreateUserId,CreateDate,ModifyUserId,ModifyDate from Orders ");
            strSql.Append(" where OrderId=@OrderId");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.Int,4)
			};
            parameters[0].Value = OrderId;

            Sys.Model.Orders model = new Sys.Model.Orders();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["OrderId"] != null && ds.Tables[0].Rows[0]["OrderId"].ToString() != "")
                {
                    model.OrderId = int.Parse(ds.Tables[0].Rows[0]["OrderId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserLoginId"] != null && ds.Tables[0].Rows[0]["UserLoginId"].ToString() != "")
                {
                    model.UserLoginId = ds.Tables[0].Rows[0]["UserLoginId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WorkerId"] != null && ds.Tables[0].Rows[0]["WorkerId"].ToString() != "")
                {
                    model.WorkerId = ds.Tables[0].Rows[0]["WorkerId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ProjectName"] != null && ds.Tables[0].Rows[0]["ProjectName"].ToString() != "")
                {
                    model.ProjectName = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CustomName"] != null && ds.Tables[0].Rows[0]["CustomName"].ToString() != "")
                {
                    model.CustomName = ds.Tables[0].Rows[0]["CustomName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ProvinceId"] != null && ds.Tables[0].Rows[0]["ProvinceId"].ToString() != "")
                {
                    model.ProvinceId = int.Parse(ds.Tables[0].Rows[0]["ProvinceId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProvinceName"] != null && ds.Tables[0].Rows[0]["ProvinceName"].ToString() != "")
                {
                    model.ProvinceName = ds.Tables[0].Rows[0]["ProvinceName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CityId"] != null && ds.Tables[0].Rows[0]["CityId"].ToString() != "")
                {
                    model.CityId = int.Parse(ds.Tables[0].Rows[0]["CityId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CityName"] != null && ds.Tables[0].Rows[0]["CityName"].ToString() != "")
                {
                    model.CityName = ds.Tables[0].Rows[0]["CityName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AreaId"] != null && ds.Tables[0].Rows[0]["AreaId"].ToString() != "")
                {
                    model.AreaId = int.Parse(ds.Tables[0].Rows[0]["AreaId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AreaName"] != null && ds.Tables[0].Rows[0]["AreaName"].ToString() != "")
                {
                    model.AreaName = ds.Tables[0].Rows[0]["AreaName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sAreaId"] != null && ds.Tables[0].Rows[0]["sAreaId"].ToString() != "")
                {
                    model.sAreaId = int.Parse(ds.Tables[0].Rows[0]["sAreaId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sAreaName"] != null && ds.Tables[0].Rows[0]["sAreaName"].ToString() != "")
                {
                    model.sAreaName = ds.Tables[0].Rows[0]["sAreaName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Address"] != null && ds.Tables[0].Rows[0]["Address"].ToString() != "")
                {
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Contact"] != null && ds.Tables[0].Rows[0]["Contact"].ToString() != "")
                {
                    model.Contact = ds.Tables[0].Rows[0]["Contact"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Tel"] != null && ds.Tables[0].Rows[0]["Tel"].ToString() != "")
                {
                    model.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HandleDate"] != null && ds.Tables[0].Rows[0]["HandleDate"].ToString() != "")
                {
                    model.HandleDate = DateTime.Parse(ds.Tables[0].Rows[0]["HandleDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderStatus"] != null && ds.Tables[0].Rows[0]["OrderStatus"].ToString() != "")
                {
                    model.OrderStatus = ds.Tables[0].Rows[0]["OrderStatus"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StatusFlag"] != null && ds.Tables[0].Rows[0]["StatusFlag"].ToString() != "")
                {
                    model.StatusFlag = int.Parse(ds.Tables[0].Rows[0]["StatusFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PicList"] != null && ds.Tables[0].Rows[0]["PicList"].ToString() != "")
                {
                    model.PicList = ds.Tables[0].Rows[0]["PicList"].ToString();
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
            strSql.Append("select OrderId,UserLoginId,WorkerId,ProjectName,CustomName,ProvinceId,ProvinceName,CityId,CityName,AreaId,AreaName,sAreaId,sAreaName,Address,Contact,Tel,HandleDate,OrderStatus,StatusFlag,Remark,PicList,CreateUserId,CreateDate,ModifyUserId,ModifyDate ");
            strSql.Append(" FROM Orders ");
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
            strSql.Append(" OrderId,UserLoginId,WorkerId,ProjectName,CustomName,ProvinceId,ProvinceName,CityId,CityName,AreaId,AreaName,sAreaId,sAreaName,Address,Contact,Tel,HandleDate,OrderStatus,StatusFlag,Remark,PicList,CreateUserId,CreateDate,ModifyUserId,ModifyDate ");
            strSql.Append(" FROM Orders ");
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
            strSql.Append("select count(1) FROM Orders ");
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
                strSql.Append("order by T.OrderId desc");
            }
            strSql.Append(")AS Row, T.*  from Orders T ");
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
            parameters[0].Value = "Orders";
            parameters[1].Value = "OrderId";
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

