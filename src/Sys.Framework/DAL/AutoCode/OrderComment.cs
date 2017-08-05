using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;

namespace Sys.DAL
{
	/// <summary>
	/// 数据访问类:OrderComment
	/// </summary>
	public partial class OrderComment
	{
		public OrderComment()
		{}
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CommentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from OrderComment");
            strSql.Append(" where CommentId=@CommentId");
            SqlParameter[] parameters = {
					new SqlParameter("@CommentId", SqlDbType.Int,4)
			};
            parameters[0].Value = CommentId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys.Model.OrderComment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OrderComment(");
            strSql.Append("OrderId,StarLevel,Remark,CreateUserId,CreateDate)");
            strSql.Append(" values (");
            strSql.Append("@OrderId,@StarLevel,@Remark,@CreateUserId,@CreateDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.Int,4),
					new SqlParameter("@StarLevel", SqlDbType.VarChar,10),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateUserId", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.StarLevel;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.CreateUserId;
            parameters[4].Value = model.CreateDate;

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
        public bool Update(Sys.Model.OrderComment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OrderComment set ");
            strSql.Append("OrderId=@OrderId,");
            strSql.Append("StarLevel=@StarLevel,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreateUserId=@CreateUserId,");
            strSql.Append("CreateDate=@CreateDate");
            strSql.Append(" where CommentId=@CommentId");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.Int,4),
					new SqlParameter("@StarLevel", SqlDbType.VarChar,10),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateUserId", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CommentId", SqlDbType.Int,4)};
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.StarLevel;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.CreateUserId;
            parameters[4].Value = model.CreateDate;
            parameters[5].Value = model.CommentId;

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
        public bool Delete(int CommentId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OrderComment ");
            strSql.Append(" where CommentId=@CommentId");
            SqlParameter[] parameters = {
					new SqlParameter("@CommentId", SqlDbType.Int,4)
			};
            parameters[0].Value = CommentId;

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
        public bool DeleteList(string CommentIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OrderComment ");
            strSql.Append(" where CommentId in (" + CommentIdlist + ")  ");
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
        public Sys.Model.OrderComment GetModel(int CommentId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CommentId,OrderId,StarLevel,Remark,CreateUserId,CreateDate from OrderComment ");
            strSql.Append(" where CommentId=@CommentId");
            SqlParameter[] parameters = {
					new SqlParameter("@CommentId", SqlDbType.Int,4)
			};
            parameters[0].Value = CommentId;

            Sys.Model.OrderComment model = new Sys.Model.OrderComment();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CommentId"] != null && ds.Tables[0].Rows[0]["CommentId"].ToString() != "")
                {
                    model.CommentId = int.Parse(ds.Tables[0].Rows[0]["CommentId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderId"] != null && ds.Tables[0].Rows[0]["OrderId"].ToString() != "")
                {
                    model.OrderId = int.Parse(ds.Tables[0].Rows[0]["OrderId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["StarLevel"] != null && ds.Tables[0].Rows[0]["StarLevel"].ToString() != "")
                {
                    model.StarLevel = ds.Tables[0].Rows[0]["StarLevel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateUserId"] != null && ds.Tables[0].Rows[0]["CreateUserId"].ToString() != "")
                {
                    model.CreateUserId = ds.Tables[0].Rows[0]["CreateUserId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
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
            strSql.Append("select CommentId,OrderId,StarLevel,Remark,CreateUserId,CreateDate ");
            strSql.Append(" FROM OrderComment ");
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
            strSql.Append(" CommentId,OrderId,StarLevel,Remark,CreateUserId,CreateDate ");
            strSql.Append(" FROM OrderComment ");
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
            strSql.Append("select count(1) FROM OrderComment ");
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
                strSql.Append("order by T.CommentId desc");
            }
            strSql.Append(")AS Row, T.*  from OrderComment T ");
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
            parameters[0].Value = "OrderComment";
            parameters[1].Value = "CommentId";
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

