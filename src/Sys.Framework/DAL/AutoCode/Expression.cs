using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;//工具类
namespace Sys.DAL
{
	/// <summary>
	/// 数据访问类:Expression
	/// 作者：王星海
	/// 日期：2014/6/21 16:10:04
	/// </summary>
	public partial class Expression
	{
		public Expression()
		{}
		#region  工具生成常用底层数据访问方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID", "Expression"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
						};
			parameters[0].Value = ID;

			var result= DbHelperSQL.RunProcedure("Expression_Exists",parameters,out rowsAffected);
			return result==1;
		}

		/// <summary>
		///  增加一条数据
		/// </summary>
		public int Add(Sys.Model.Expression model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@SId", SqlDbType.Int,4),
					new SqlParameter("@StuId", SqlDbType.Int,4),
					new SqlParameter("@GId", SqlDbType.Int,4),
					new SqlParameter("@CId", SqlDbType.NChar,10),
					new SqlParameter("@Content", SqlDbType.NVarChar,1000),
					new SqlParameter("@F_UserId", SqlDbType.NChar,10),
					new SqlParameter("@F_RealName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateIp", SqlDbType.VarChar,50),
					new SqlParameter("@SendName", SqlDbType.Int,4),
					new SqlParameter("@SendStates", SqlDbType.Int,4),
					new SqlParameter("@SendSuccess", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.CreateTime;
			parameters[2].Value = model.SId;
			parameters[3].Value = model.StuId;
			parameters[4].Value = model.GId;
			parameters[5].Value = model.CId;
			parameters[6].Value = model.Content;
			parameters[7].Value = model.F_UserId;
			parameters[8].Value = model.F_RealName;
			parameters[9].Value = model.CreateIp;
			parameters[10].Value = model.SendName;
			parameters[11].Value = model.SendStates;
			parameters[12].Value = model.SendSuccess;
			parameters[13].Value = model.TypeID;

			DbHelperSQL.RunProcedure("Expression_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public bool Update(Sys.Model.Expression model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@SId", SqlDbType.Int,4),
					new SqlParameter("@StuId", SqlDbType.Int,4),
					new SqlParameter("@GId", SqlDbType.Int,4),
					new SqlParameter("@CId", SqlDbType.NChar,10),
					new SqlParameter("@Content", SqlDbType.NVarChar,1000),
					new SqlParameter("@F_UserId", SqlDbType.NChar,10),
					new SqlParameter("@F_RealName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateIp", SqlDbType.VarChar,50),
					new SqlParameter("@SendName", SqlDbType.Int,4),
					new SqlParameter("@SendStates", SqlDbType.Int,4),
					new SqlParameter("@SendSuccess", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.CreateTime;
			parameters[2].Value = model.SId;
			parameters[3].Value = model.StuId;
			parameters[4].Value = model.GId;
			parameters[5].Value = model.CId;
			parameters[6].Value = model.Content;
			parameters[7].Value = model.F_UserId;
			parameters[8].Value = model.F_RealName;
			parameters[9].Value = model.CreateIp;
			parameters[10].Value = model.SendName;
			parameters[11].Value = model.SendStates;
			parameters[12].Value = model.SendSuccess;
			parameters[13].Value = model.TypeID;

			DbHelperSQL.RunProcedure("Expression_Update",parameters,out rowsAffected);
			return rowsAffected > 0;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
						};
			parameters[0].Value = ID;

			DbHelperSQL.RunProcedure("Expression_Delete",parameters,out rowsAffected);
			return rowsAffected > 0;
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			var strSql=new StringBuilder();
			strSql.Append("delete from Expression ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			var rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Sys.Model.Expression GetModel(int ID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
						};
			parameters[0].Value = ID;

			var  model=new Sys.Model.Expression();
			var ds= DbHelperSQL.RunProcedure("Expression_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateTime"]!=null && ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SId"]!=null && ds.Tables[0].Rows[0]["SId"].ToString()!="")
				{
					model.SId=int.Parse(ds.Tables[0].Rows[0]["SId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StuId"]!=null && ds.Tables[0].Rows[0]["StuId"].ToString()!="")
				{
					model.StuId=int.Parse(ds.Tables[0].Rows[0]["StuId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GId"]!=null && ds.Tables[0].Rows[0]["GId"].ToString()!="")
				{
					model.GId=int.Parse(ds.Tables[0].Rows[0]["GId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CId"]!=null && ds.Tables[0].Rows[0]["CId"].ToString()!="")
				{
					model.CId=ds.Tables[0].Rows[0]["CId"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Content"]!=null && ds.Tables[0].Rows[0]["Content"].ToString()!="")
				{
					model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				}
				if(ds.Tables[0].Rows[0]["F_UserId"]!=null && ds.Tables[0].Rows[0]["F_UserId"].ToString()!="")
				{
					model.F_UserId=ds.Tables[0].Rows[0]["F_UserId"].ToString();
				}
				if(ds.Tables[0].Rows[0]["F_RealName"]!=null && ds.Tables[0].Rows[0]["F_RealName"].ToString()!="")
				{
					model.F_RealName=ds.Tables[0].Rows[0]["F_RealName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CreateIp"]!=null && ds.Tables[0].Rows[0]["CreateIp"].ToString()!="")
				{
					model.CreateIp=ds.Tables[0].Rows[0]["CreateIp"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SendName"]!=null && ds.Tables[0].Rows[0]["SendName"].ToString()!="")
				{
					model.SendName=int.Parse(ds.Tables[0].Rows[0]["SendName"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SendStates"]!=null && ds.Tables[0].Rows[0]["SendStates"].ToString()!="")
				{
					model.SendStates=int.Parse(ds.Tables[0].Rows[0]["SendStates"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SendSuccess"]!=null && ds.Tables[0].Rows[0]["SendSuccess"].ToString()!="")
				{
					model.SendSuccess=int.Parse(ds.Tables[0].Rows[0]["SendSuccess"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TypeID"]!=null && ds.Tables[0].Rows[0]["TypeID"].ToString()!="")
				{
					model.TypeID=int.Parse(ds.Tables[0].Rows[0]["TypeID"].ToString());
				}
				return model;
			}
				return null;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public   DataTable  GetTableModel(int ID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
						};
			parameters[0].Value = ID;

			var  model=new Sys.Model.Expression();
			var ds= DbHelperSQL.RunProcedure("Expression_GetModel",parameters,"ds");
			return ds.Tables[0];
		}

		#endregion  Method
	}
}

