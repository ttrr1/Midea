using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;//工具类
namespace Sys.DAL
{
	/// <summary>
	/// 数据访问类:Services
	/// 作者：王星海
	/// 日期：2014/6/24 9:38:01
	/// </summary>
	public partial class Services
	{
		public Services()
		{}
		#region  工具生成常用底层数据访问方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID", "Services"); 
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

			var result= DbHelperSQL.RunProcedure("Services_Exists",parameters,out rowsAffected);
			return result==1;
		}

		/// <summary>
		///  增加一条数据
		/// </summary>
		public int Add(Sys.Model.Services model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Picpath", SqlDbType.VarChar,250),
					new SqlParameter("@linkAddress", SqlDbType.VarChar,500),
					new SqlParameter("@OrderId", SqlDbType.Int,4)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.Picpath;
			parameters[3].Value = model.linkAddress;
			parameters[4].Value = model.OrderId;

			DbHelperSQL.RunProcedure("Services_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public bool Update(Sys.Model.Services model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Picpath", SqlDbType.VarChar,250),
					new SqlParameter("@linkAddress", SqlDbType.VarChar,500),
					new SqlParameter("@OrderId", SqlDbType.Int,4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.Picpath;
			parameters[3].Value = model.linkAddress;
			parameters[4].Value = model.OrderId;

			DbHelperSQL.RunProcedure("Services_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Services_Delete",parameters,out rowsAffected);
			return rowsAffected > 0;
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			var strSql=new StringBuilder();
			strSql.Append("delete from Services ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			var rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Sys.Model.Services GetModel(int ID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
						};
			parameters[0].Value = ID;

			var  model=new Sys.Model.Services();
			var ds= DbHelperSQL.RunProcedure("Services_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Name"]!=null && ds.Tables[0].Rows[0]["Name"].ToString()!="")
				{
					model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Picpath"]!=null && ds.Tables[0].Rows[0]["Picpath"].ToString()!="")
				{
					model.Picpath=ds.Tables[0].Rows[0]["Picpath"].ToString();
				}
				if(ds.Tables[0].Rows[0]["linkAddress"]!=null && ds.Tables[0].Rows[0]["linkAddress"].ToString()!="")
				{
					model.linkAddress=ds.Tables[0].Rows[0]["linkAddress"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OrderId"]!=null && ds.Tables[0].Rows[0]["OrderId"].ToString()!="")
				{
					model.OrderId=int.Parse(ds.Tables[0].Rows[0]["OrderId"].ToString());
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

			var  model=new Sys.Model.Services();
			var ds= DbHelperSQL.RunProcedure("Services_GetModel",parameters,"ds");
			return ds.Tables[0];
		}

		#endregion  Method
	}
}

