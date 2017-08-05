using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;//工具类
namespace Sys.DAL
{
	/// <summary>
	/// 数据访问类:Activity
	/// 作者：王星海
	/// 日期：2014/6/19 6:57:00
	/// </summary>
	public partial class Activity
	{
		public Activity()
		{}
		#region  工具生成常用底层数据访问方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID", "Activity"); 
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

			var result= DbHelperSQL.RunProcedure("Activity_Exists",parameters,out rowsAffected);
			return result==1;
		}

		/// <summary>
		///  增加一条数据
		/// </summary>
		public int Add(Sys.Model.Activity model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@SId", SqlDbType.Int,4),
					new SqlParameter("@GId", SqlDbType.Int,4),
					new SqlParameter("@CId", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar,150),
					new SqlParameter("@Content", SqlDbType.NVarChar,1500)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.CreateTime;
			parameters[2].Value = model.SId;
			parameters[3].Value = model.GId;
			parameters[4].Value = model.CId;
			parameters[5].Value = model.Title;
			parameters[6].Value = model.Content;

			DbHelperSQL.RunProcedure("Activity_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public bool Update(Sys.Model.Activity model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@SId", SqlDbType.Int,4),
					new SqlParameter("@GId", SqlDbType.Int,4),
					new SqlParameter("@CId", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar,150),
					new SqlParameter("@Content", SqlDbType.NVarChar,1500)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.CreateTime;
			parameters[2].Value = model.SId;
			parameters[3].Value = model.GId;
			parameters[4].Value = model.CId;
			parameters[5].Value = model.Title;
			parameters[6].Value = model.Content;

			DbHelperSQL.RunProcedure("Activity_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Activity_Delete",parameters,out rowsAffected);
			return rowsAffected > 0;
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			var strSql=new StringBuilder();
			strSql.Append("delete from Activity ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			var rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Sys.Model.Activity GetModel(int ID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
						};
			parameters[0].Value = ID;

			var  model=new Sys.Model.Activity();
			var ds= DbHelperSQL.RunProcedure("Activity_GetModel",parameters,"ds");
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
				if(ds.Tables[0].Rows[0]["GId"]!=null && ds.Tables[0].Rows[0]["GId"].ToString()!="")
				{
					model.GId=int.Parse(ds.Tables[0].Rows[0]["GId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CId"]!=null && ds.Tables[0].Rows[0]["CId"].ToString()!="")
				{
					model.CId=int.Parse(ds.Tables[0].Rows[0]["CId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Title"]!=null && ds.Tables[0].Rows[0]["Title"].ToString()!="")
				{
					model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Content"]!=null && ds.Tables[0].Rows[0]["Content"].ToString()!="")
				{
					model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
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

			var  model=new Sys.Model.Activity();
			var ds= DbHelperSQL.RunProcedure("Activity_GetModel",parameters,"ds");
			return ds.Tables[0];
		}

		#endregion  Method
	}
}

