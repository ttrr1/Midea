using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;//工具类
namespace Sys.DAL
{
	/// <summary>
	/// 数据访问类:Attendance
	/// 作者：王星海
	/// 日期：2014/6/9 17:21:42
	/// </summary>
	public partial class Attendance
	{
		public Attendance()
		{}
		#region  工具生成常用底层数据访问方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("Id", "Attendance"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
						};
			parameters[0].Value = Id;

			var result= DbHelperSQL.RunProcedure("Attendance_Exists",parameters,out rowsAffected);
			return result==1;
		}

		/// <summary>
		///  增加一条数据
		/// </summary>
		public int Add(Sys.Model.Attendance model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@ICcode", SqlDbType.VarChar,50),
					new SqlParameter("@SId", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateIp", SqlDbType.VarChar,50),
					new SqlParameter("@MendSign", SqlDbType.Int,4)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ICcode;
			parameters[2].Value = model.SId;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.CreateIp;
			parameters[5].Value = model.MendSign;

			DbHelperSQL.RunProcedure("Attendance_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public bool Update(Sys.Model.Attendance model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@ICcode", SqlDbType.VarChar,50),
					new SqlParameter("@SId", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateIp", SqlDbType.VarChar,50),
					new SqlParameter("@MendSign", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.ICcode;
			parameters[2].Value = model.SId;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.CreateIp;
			parameters[5].Value = model.MendSign;

			DbHelperSQL.RunProcedure("Attendance_Update",parameters,out rowsAffected);
			return rowsAffected > 0;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
						};
			parameters[0].Value = Id;

			DbHelperSQL.RunProcedure("Attendance_Delete",parameters,out rowsAffected);
			return rowsAffected > 0;
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			var strSql=new StringBuilder();
			strSql.Append("delete from Attendance ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
			var rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Sys.Model.Attendance GetModel(int Id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
						};
			parameters[0].Value = Id;

			var  model=new Sys.Model.Attendance();
			var ds= DbHelperSQL.RunProcedure("Attendance_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"]!=null && ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ICcode"]!=null && ds.Tables[0].Rows[0]["ICcode"].ToString()!="")
				{
					model.ICcode=ds.Tables[0].Rows[0]["ICcode"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SId"]!=null && ds.Tables[0].Rows[0]["SId"].ToString()!="")
				{
					model.SId=int.Parse(ds.Tables[0].Rows[0]["SId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateTime"]!=null && ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateIp"]!=null && ds.Tables[0].Rows[0]["CreateIp"].ToString()!="")
				{
					model.CreateIp=ds.Tables[0].Rows[0]["CreateIp"].ToString();
				}
				if(ds.Tables[0].Rows[0]["MendSign"]!=null && ds.Tables[0].Rows[0]["MendSign"].ToString()!="")
				{
					model.MendSign=int.Parse(ds.Tables[0].Rows[0]["MendSign"].ToString());
				}
				return model;
			}
				return null;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public   DataTable  GetTableModel(int Id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
						};
			parameters[0].Value = Id;

			var  model=new Sys.Model.Attendance();
			var ds= DbHelperSQL.RunProcedure("Attendance_GetModel",parameters,"ds");
			return ds.Tables[0];
		}

		#endregion  Method
	}
}

