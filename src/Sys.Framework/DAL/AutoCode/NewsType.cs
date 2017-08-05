using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;//工具类
namespace Sys.DAL
{
	/// <summary>
	/// 数据访问类:NewsType
	/// 作者：王星海
	/// 日期：2014/5/7 22:05:59
	/// </summary>
	public partial class NewsType
	{
		public NewsType()
		{}
		#region  工具生成常用底层数据访问方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID", "NewsType"); 
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

			var result= DbHelperSQL.RunProcedure("NewsType_Exists",parameters,out rowsAffected);
			return result==1;
		}

		/// <summary>
		///  增加一条数据
		/// </summary>
		public int Add(Sys.Model.NewsType model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@PId", SqlDbType.Int,4),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,150),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateIp", SqlDbType.VarChar,50),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@TypeAction", SqlDbType.VarChar,50)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.PId;
			parameters[2].Value = model.TypeName;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.CreateIp;
			parameters[5].Value = model.Count;
			parameters[6].Value = model.TypeAction;

			DbHelperSQL.RunProcedure("NewsType_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public bool Update(Sys.Model.NewsType model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@PId", SqlDbType.Int,4),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,150),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateIp", SqlDbType.VarChar,50),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@TypeAction", SqlDbType.VarChar,50)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.PId;
			parameters[2].Value = model.TypeName;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.CreateIp;
			parameters[5].Value = model.Count;
			parameters[6].Value = model.TypeAction;

			DbHelperSQL.RunProcedure("NewsType_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("NewsType_Delete",parameters,out rowsAffected);
			return rowsAffected > 0;
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			var strSql=new StringBuilder();
			strSql.Append("delete from NewsType ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			var rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Sys.Model.NewsType GetModel(int ID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
						};
			parameters[0].Value = ID;

			var  model=new Sys.Model.NewsType();
			var ds= DbHelperSQL.RunProcedure("NewsType_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PId"]!=null && ds.Tables[0].Rows[0]["PId"].ToString()!="")
				{
					model.PId=int.Parse(ds.Tables[0].Rows[0]["PId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TypeName"]!=null && ds.Tables[0].Rows[0]["TypeName"].ToString()!="")
				{
					model.TypeName=ds.Tables[0].Rows[0]["TypeName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CreateTime"]!=null && ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateIp"]!=null && ds.Tables[0].Rows[0]["CreateIp"].ToString()!="")
				{
					model.CreateIp=ds.Tables[0].Rows[0]["CreateIp"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Count"]!=null && ds.Tables[0].Rows[0]["Count"].ToString()!="")
				{
					model.Count=int.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TypeAction"]!=null && ds.Tables[0].Rows[0]["TypeAction"].ToString()!="")
				{
					model.TypeAction=ds.Tables[0].Rows[0]["TypeAction"].ToString();
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

			var  model=new Sys.Model.NewsType();
			var ds= DbHelperSQL.RunProcedure("NewsType_GetModel",parameters,"ds");
			return ds.Tables[0];
		}

		#endregion  Method
	}
}

