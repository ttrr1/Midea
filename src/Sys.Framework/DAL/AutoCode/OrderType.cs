using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;

namespace Sys.DAL
{
	/// <summary>
	/// 数据访问类:OrderType
	/// </summary>
	public partial class OrderType
	{
		public OrderType()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("TypeId", "OrderType"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int TypeId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from OrderType");
			strSql.Append(" where TypeId=@TypeId");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeId", SqlDbType.Int,4)
			};
			parameters[0].Value = TypeId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Sys.Model.OrderType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into OrderType(");
			strSql.Append("OrderId,ProductType,ProductTypeName,MainModelId,MainModelName,MainModelNum,SubModelId1,SubModelName1,SubModelNum1,SubModelId2,SubModelName2,SubModelNum2,SubModelId3,SubModelName3,SubModelNum3,SubModelId4,SubModelName4,SubModelNum4,ModelId,ModelName,ModelNum,CreateUserId,CreateDate,ModifyUserId,ModifyDate)");
			strSql.Append(" values (");
			strSql.Append("@OrderId,@ProductType,@ProductTypeName,@MainModelId,@MainModelName,@MainModelNum,@SubModelId1,@SubModelName1,@SubModelNum1,@SubModelId2,@SubModelName2,@SubModelNum2,@SubModelId3,@SubModelName3,@SubModelNum3,@SubModelId4,@SubModelName4,@SubModelNum4,@ModelId,@ModelName,@ModelNum,@CreateUserId,@CreateDate,@ModifyUserId,@ModifyDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.Int,4),
					new SqlParameter("@ProductType", SqlDbType.Int,4),
					new SqlParameter("@ProductTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@MainModelId", SqlDbType.Int,4),
					new SqlParameter("@MainModelName", SqlDbType.NVarChar,50),
					new SqlParameter("@MainModelNum", SqlDbType.Int,4),
					new SqlParameter("@SubModelId1", SqlDbType.Int,4),
					new SqlParameter("@SubModelName1", SqlDbType.NVarChar,50),
					new SqlParameter("@SubModelNum1", SqlDbType.Int,4),
					new SqlParameter("@SubModelId2", SqlDbType.Int,4),
					new SqlParameter("@SubModelName2", SqlDbType.NVarChar,50),
					new SqlParameter("@SubModelNum2", SqlDbType.Int,4),
					new SqlParameter("@SubModelId3", SqlDbType.Int,4),
					new SqlParameter("@SubModelName3", SqlDbType.NVarChar,50),
					new SqlParameter("@SubModelNum3", SqlDbType.Int,4),
					new SqlParameter("@SubModelId4", SqlDbType.Int,4),
					new SqlParameter("@SubModelName4", SqlDbType.NVarChar,50),
					new SqlParameter("@SubModelNum4", SqlDbType.Int,4),
					new SqlParameter("@ModelId", SqlDbType.Int,4),
					new SqlParameter("@ModelName", SqlDbType.NVarChar,50),
					new SqlParameter("@ModelNum", SqlDbType.Int,4),
					new SqlParameter("@CreateUserId", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifyUserId", SqlDbType.VarChar,50),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime)};
			parameters[0].Value = model.OrderId;
			parameters[1].Value = model.ProductType;
			parameters[2].Value = model.ProductTypeName;
			parameters[3].Value = model.MainModelId;
			parameters[4].Value = model.MainModelName;
			parameters[5].Value = model.MainModelNum;
			parameters[6].Value = model.SubModelId1;
			parameters[7].Value = model.SubModelName1;
			parameters[8].Value = model.SubModelNum1;
			parameters[9].Value = model.SubModelId2;
			parameters[10].Value = model.SubModelName2;
			parameters[11].Value = model.SubModelNum2;
			parameters[12].Value = model.SubModelId3;
			parameters[13].Value = model.SubModelName3;
			parameters[14].Value = model.SubModelNum3;
			parameters[15].Value = model.SubModelId4;
			parameters[16].Value = model.SubModelName4;
			parameters[17].Value = model.SubModelNum4;
			parameters[18].Value = model.ModelId;
			parameters[19].Value = model.ModelName;
			parameters[20].Value = model.ModelNum;
			parameters[21].Value = model.CreateUserId;
			parameters[22].Value = model.CreateDate;
			parameters[23].Value = model.ModifyUserId;
			parameters[24].Value = model.ModifyDate;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(Sys.Model.OrderType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update OrderType set ");
			strSql.Append("OrderId=@OrderId,");
			strSql.Append("ProductType=@ProductType,");
			strSql.Append("ProductTypeName=@ProductTypeName,");
			strSql.Append("MainModelId=@MainModelId,");
			strSql.Append("MainModelName=@MainModelName,");
			strSql.Append("MainModelNum=@MainModelNum,");
			strSql.Append("SubModelId1=@SubModelId1,");
			strSql.Append("SubModelName1=@SubModelName1,");
			strSql.Append("SubModelNum1=@SubModelNum1,");
			strSql.Append("SubModelId2=@SubModelId2,");
			strSql.Append("SubModelName2=@SubModelName2,");
			strSql.Append("SubModelNum2=@SubModelNum2,");
			strSql.Append("SubModelId3=@SubModelId3,");
			strSql.Append("SubModelName3=@SubModelName3,");
			strSql.Append("SubModelNum3=@SubModelNum3,");
			strSql.Append("SubModelId4=@SubModelId4,");
			strSql.Append("SubModelName4=@SubModelName4,");
			strSql.Append("SubModelNum4=@SubModelNum4,");
			strSql.Append("ModelId=@ModelId,");
			strSql.Append("ModelName=@ModelName,");
			strSql.Append("ModelNum=@ModelNum,");
			strSql.Append("CreateUserId=@CreateUserId,");
			strSql.Append("CreateDate=@CreateDate,");
			strSql.Append("ModifyUserId=@ModifyUserId,");
			strSql.Append("ModifyDate=@ModifyDate");
			strSql.Append(" where TypeId=@TypeId");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.Int,4),
					new SqlParameter("@ProductType", SqlDbType.Int,4),
					new SqlParameter("@ProductTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@MainModelId", SqlDbType.Int,4),
					new SqlParameter("@MainModelName", SqlDbType.NVarChar,50),
					new SqlParameter("@MainModelNum", SqlDbType.Int,4),
					new SqlParameter("@SubModelId1", SqlDbType.Int,4),
					new SqlParameter("@SubModelName1", SqlDbType.NVarChar,50),
					new SqlParameter("@SubModelNum1", SqlDbType.Int,4),
					new SqlParameter("@SubModelId2", SqlDbType.Int,4),
					new SqlParameter("@SubModelName2", SqlDbType.NVarChar,50),
					new SqlParameter("@SubModelNum2", SqlDbType.Int,4),
					new SqlParameter("@SubModelId3", SqlDbType.Int,4),
					new SqlParameter("@SubModelName3", SqlDbType.NVarChar,50),
					new SqlParameter("@SubModelNum3", SqlDbType.Int,4),
					new SqlParameter("@SubModelId4", SqlDbType.Int,4),
					new SqlParameter("@SubModelName4", SqlDbType.NVarChar,50),
					new SqlParameter("@SubModelNum4", SqlDbType.Int,4),
					new SqlParameter("@ModelId", SqlDbType.Int,4),
					new SqlParameter("@ModelName", SqlDbType.NVarChar,50),
					new SqlParameter("@ModelNum", SqlDbType.Int,4),
					new SqlParameter("@CreateUserId", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifyUserId", SqlDbType.VarChar,50),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime),
					new SqlParameter("@TypeId", SqlDbType.Int,4)};
			parameters[0].Value = model.OrderId;
			parameters[1].Value = model.ProductType;
			parameters[2].Value = model.ProductTypeName;
			parameters[3].Value = model.MainModelId;
			parameters[4].Value = model.MainModelName;
			parameters[5].Value = model.MainModelNum;
			parameters[6].Value = model.SubModelId1;
			parameters[7].Value = model.SubModelName1;
			parameters[8].Value = model.SubModelNum1;
			parameters[9].Value = model.SubModelId2;
			parameters[10].Value = model.SubModelName2;
			parameters[11].Value = model.SubModelNum2;
			parameters[12].Value = model.SubModelId3;
			parameters[13].Value = model.SubModelName3;
			parameters[14].Value = model.SubModelNum3;
			parameters[15].Value = model.SubModelId4;
			parameters[16].Value = model.SubModelName4;
			parameters[17].Value = model.SubModelNum4;
			parameters[18].Value = model.ModelId;
			parameters[19].Value = model.ModelName;
			parameters[20].Value = model.ModelNum;
			parameters[21].Value = model.CreateUserId;
			parameters[22].Value = model.CreateDate;
			parameters[23].Value = model.ModifyUserId;
			parameters[24].Value = model.ModifyDate;
			parameters[25].Value = model.TypeId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(int TypeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from OrderType ");
			strSql.Append(" where TypeId=@TypeId");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeId", SqlDbType.Int,4)
			};
			parameters[0].Value = TypeId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string TypeIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from OrderType ");
			strSql.Append(" where TypeId in ("+TypeIdlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public Sys.Model.OrderType GetModel(int TypeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 TypeId,OrderId,ProductType,ProductTypeName,MainModelId,MainModelName,MainModelNum,SubModelId1,SubModelName1,SubModelNum1,SubModelId2,SubModelName2,SubModelNum2,SubModelId3,SubModelName3,SubModelNum3,SubModelId4,SubModelName4,SubModelNum4,ModelId,ModelName,ModelNum,CreateUserId,CreateDate,ModifyUserId,ModifyDate from OrderType ");
			strSql.Append(" where TypeId=@TypeId");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeId", SqlDbType.Int,4)
			};
			parameters[0].Value = TypeId;

			Sys.Model.OrderType model=new Sys.Model.OrderType();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["TypeId"]!=null && ds.Tables[0].Rows[0]["TypeId"].ToString()!="")
				{
					model.TypeId=int.Parse(ds.Tables[0].Rows[0]["TypeId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OrderId"]!=null && ds.Tables[0].Rows[0]["OrderId"].ToString()!="")
				{
					model.OrderId=int.Parse(ds.Tables[0].Rows[0]["OrderId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ProductType"]!=null && ds.Tables[0].Rows[0]["ProductType"].ToString()!="")
				{
					model.ProductType=int.Parse(ds.Tables[0].Rows[0]["ProductType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ProductTypeName"]!=null && ds.Tables[0].Rows[0]["ProductTypeName"].ToString()!="")
				{
					model.ProductTypeName=ds.Tables[0].Rows[0]["ProductTypeName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["MainModelId"]!=null && ds.Tables[0].Rows[0]["MainModelId"].ToString()!="")
				{
					model.MainModelId=int.Parse(ds.Tables[0].Rows[0]["MainModelId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MainModelName"]!=null && ds.Tables[0].Rows[0]["MainModelName"].ToString()!="")
				{
					model.MainModelName=ds.Tables[0].Rows[0]["MainModelName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["MainModelNum"]!=null && ds.Tables[0].Rows[0]["MainModelNum"].ToString()!="")
				{
					model.MainModelNum=int.Parse(ds.Tables[0].Rows[0]["MainModelNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SubModelId1"]!=null && ds.Tables[0].Rows[0]["SubModelId1"].ToString()!="")
				{
					model.SubModelId1=int.Parse(ds.Tables[0].Rows[0]["SubModelId1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SubModelName1"]!=null && ds.Tables[0].Rows[0]["SubModelName1"].ToString()!="")
				{
					model.SubModelName1=ds.Tables[0].Rows[0]["SubModelName1"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SubModelNum1"]!=null && ds.Tables[0].Rows[0]["SubModelNum1"].ToString()!="")
				{
					model.SubModelNum1=int.Parse(ds.Tables[0].Rows[0]["SubModelNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SubModelId2"]!=null && ds.Tables[0].Rows[0]["SubModelId2"].ToString()!="")
				{
					model.SubModelId2=int.Parse(ds.Tables[0].Rows[0]["SubModelId2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SubModelName2"]!=null && ds.Tables[0].Rows[0]["SubModelName2"].ToString()!="")
				{
					model.SubModelName2=ds.Tables[0].Rows[0]["SubModelName2"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SubModelNum2"]!=null && ds.Tables[0].Rows[0]["SubModelNum2"].ToString()!="")
				{
					model.SubModelNum2=int.Parse(ds.Tables[0].Rows[0]["SubModelNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SubModelId3"]!=null && ds.Tables[0].Rows[0]["SubModelId3"].ToString()!="")
				{
					model.SubModelId3=int.Parse(ds.Tables[0].Rows[0]["SubModelId3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SubModelName3"]!=null && ds.Tables[0].Rows[0]["SubModelName3"].ToString()!="")
				{
					model.SubModelName3=ds.Tables[0].Rows[0]["SubModelName3"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SubModelNum3"]!=null && ds.Tables[0].Rows[0]["SubModelNum3"].ToString()!="")
				{
					model.SubModelNum3=int.Parse(ds.Tables[0].Rows[0]["SubModelNum3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SubModelId4"]!=null && ds.Tables[0].Rows[0]["SubModelId4"].ToString()!="")
				{
					model.SubModelId4=int.Parse(ds.Tables[0].Rows[0]["SubModelId4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SubModelName4"]!=null && ds.Tables[0].Rows[0]["SubModelName4"].ToString()!="")
				{
					model.SubModelName4=ds.Tables[0].Rows[0]["SubModelName4"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SubModelNum4"]!=null && ds.Tables[0].Rows[0]["SubModelNum4"].ToString()!="")
				{
					model.SubModelNum4=int.Parse(ds.Tables[0].Rows[0]["SubModelNum4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ModelId"]!=null && ds.Tables[0].Rows[0]["ModelId"].ToString()!="")
				{
					model.ModelId=int.Parse(ds.Tables[0].Rows[0]["ModelId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ModelName"]!=null && ds.Tables[0].Rows[0]["ModelName"].ToString()!="")
				{
					model.ModelName=ds.Tables[0].Rows[0]["ModelName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ModelNum"]!=null && ds.Tables[0].Rows[0]["ModelNum"].ToString()!="")
				{
					model.ModelNum=int.Parse(ds.Tables[0].Rows[0]["ModelNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateUserId"]!=null && ds.Tables[0].Rows[0]["CreateUserId"].ToString()!="")
				{
					model.CreateUserId=ds.Tables[0].Rows[0]["CreateUserId"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CreateDate"]!=null && ds.Tables[0].Rows[0]["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ModifyUserId"]!=null && ds.Tables[0].Rows[0]["ModifyUserId"].ToString()!="")
				{
					model.ModifyUserId=ds.Tables[0].Rows[0]["ModifyUserId"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ModifyDate"]!=null && ds.Tables[0].Rows[0]["ModifyDate"].ToString()!="")
				{
					model.ModifyDate=DateTime.Parse(ds.Tables[0].Rows[0]["ModifyDate"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select TypeId,OrderId,ProductType,ProductTypeName,MainModelId,MainModelName,MainModelNum,SubModelId1,SubModelName1,SubModelNum1,SubModelId2,SubModelName2,SubModelNum2,SubModelId3,SubModelName3,SubModelNum3,SubModelId4,SubModelName4,SubModelNum4,ModelId,ModelName,ModelNum,CreateUserId,CreateDate,ModifyUserId,ModifyDate ");
			strSql.Append(" FROM OrderType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" TypeId,OrderId,ProductType,ProductTypeName,MainModelId,MainModelName,MainModelNum,SubModelId1,SubModelName1,SubModelNum1,SubModelId2,SubModelName2,SubModelNum2,SubModelId3,SubModelName3,SubModelNum3,SubModelId4,SubModelName4,SubModelNum4,ModelId,ModelName,ModelNum,CreateUserId,CreateDate,ModifyUserId,ModifyDate ");
			strSql.Append(" FROM OrderType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM OrderType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.TypeId desc");
			}
			strSql.Append(")AS Row, T.*  from OrderType T ");
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
			parameters[0].Value = "OrderType";
			parameters[1].Value = "TypeId";
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

