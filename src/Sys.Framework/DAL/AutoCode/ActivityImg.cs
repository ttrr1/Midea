using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;//工具类
namespace Sys.DAL
{
	/// <summary>
	/// 数据访问类:ActivityImg
	/// 作者：王星海
	/// 日期：2014/6/19 6:57:09
	/// </summary>
	public partial class ActivityImg
	{
		public ActivityImg()
		{}
		#region  工具生成常用底层数据访问方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID", "ActivityImg"); 
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

			var result= DbHelperSQL.RunProcedure("ActivityImg_Exists",parameters,out rowsAffected);
			return result==1;
		}

		/// <summary>
		///  增加一条数据
		/// </summary>
		public int Add(Sys.Model.ActivityImg model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@AId", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@FilePath", SqlDbType.VarChar,500)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.AId;
			parameters[2].Value = model.CreateTime;
			parameters[3].Value = model.FilePath;

			DbHelperSQL.RunProcedure("ActivityImg_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public bool Update(Sys.Model.ActivityImg model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@AId", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@FilePath", SqlDbType.VarChar,500)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.AId;
			parameters[2].Value = model.CreateTime;
			parameters[3].Value = model.FilePath;

			DbHelperSQL.RunProcedure("ActivityImg_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("ActivityImg_Delete",parameters,out rowsAffected);
			return rowsAffected > 0;
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			var strSql=new StringBuilder();
			strSql.Append("delete from ActivityImg ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			var rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Sys.Model.ActivityImg GetModel(int ID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
						};
			parameters[0].Value = ID;

			var  model=new Sys.Model.ActivityImg();
			var ds= DbHelperSQL.RunProcedure("ActivityImg_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AId"]!=null && ds.Tables[0].Rows[0]["AId"].ToString()!="")
				{
					model.AId=int.Parse(ds.Tables[0].Rows[0]["AId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateTime"]!=null && ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FilePath"]!=null && ds.Tables[0].Rows[0]["FilePath"].ToString()!="")
				{
					model.FilePath=ds.Tables[0].Rows[0]["FilePath"].ToString();
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

			var  model=new Sys.Model.ActivityImg();
			var ds= DbHelperSQL.RunProcedure("ActivityImg_GetModel",parameters,"ds");
			return ds.Tables[0];
		}

		#endregion  Method
	}
}

