using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;//工具类
namespace Sys.DAL
{
	/// <summary>
	/// 数据访问类:News
	/// 作者：王星海
	/// 日期：2014/6/21 15:33:30
	/// </summary>
	public partial class News
	{
		public News()
		{}
		#region  工具生成常用底层数据访问方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID", "News"); 
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

			var result= DbHelperSQL.RunProcedure("News_Exists",parameters,out rowsAffected);
			return result==1;
		}

		/// <summary>
		///  增加一条数据
		/// </summary>
		public int Add(Sys.Model.News model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateIp", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateIp", SqlDbType.VarChar,50),
					new SqlParameter("@NewsTitle", SqlDbType.NVarChar,250),
					new SqlParameter("@Abstract", SqlDbType.NVarChar,500),
					new SqlParameter("@Content", SqlDbType.Text),
					new SqlParameter("@TypeId", SqlDbType.Int,4),
					new SqlParameter("@GUID", SqlDbType.Char,50),
					new SqlParameter("@Publisher", SqlDbType.NVarChar,50),
					new SqlParameter("@Source", SqlDbType.NVarChar,150),
					new SqlParameter("@TotalClick", SqlDbType.Int,4),
					new SqlParameter("@Pic", SqlDbType.VarChar,250),
					new SqlParameter("@IsAudit", SqlDbType.Int,4),
					new SqlParameter("@IsAddName", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.CreateTime;
			parameters[2].Value = model.UpdateTime;
			parameters[3].Value = model.CreateIp;
			parameters[4].Value = model.UpdateIp;
			parameters[5].Value = model.NewsTitle;
			parameters[6].Value = model.Abstract;
			parameters[7].Value = model.Content;
			parameters[8].Value = model.TypeId;
			parameters[9].Value = model.GUID;
			parameters[10].Value = model.Publisher;
			parameters[11].Value = model.Source;
			parameters[12].Value = model.TotalClick;
			parameters[13].Value = model.Pic;
			parameters[14].Value = model.IsAudit;
			parameters[15].Value = model.IsAddName;
			parameters[16].Value = model.UserID;

			DbHelperSQL.RunProcedure("News_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public bool Update(Sys.Model.News model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateIp", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateIp", SqlDbType.VarChar,50),
					new SqlParameter("@NewsTitle", SqlDbType.NVarChar,250),
					new SqlParameter("@Abstract", SqlDbType.NVarChar,500),
					new SqlParameter("@Content", SqlDbType.Text),
					new SqlParameter("@TypeId", SqlDbType.Int,4),
					new SqlParameter("@GUID", SqlDbType.Char,50),
					new SqlParameter("@Publisher", SqlDbType.NVarChar,50),
					new SqlParameter("@Source", SqlDbType.NVarChar,150),
					new SqlParameter("@TotalClick", SqlDbType.Int,4),
					new SqlParameter("@Pic", SqlDbType.VarChar,250),
					new SqlParameter("@IsAudit", SqlDbType.Int,4),
					new SqlParameter("@IsAddName", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.CreateTime;
			parameters[2].Value = model.UpdateTime;
			parameters[3].Value = model.CreateIp;
			parameters[4].Value = model.UpdateIp;
			parameters[5].Value = model.NewsTitle;
			parameters[6].Value = model.Abstract;
			parameters[7].Value = model.Content;
			parameters[8].Value = model.TypeId;
			parameters[9].Value = model.GUID;
			parameters[10].Value = model.Publisher;
			parameters[11].Value = model.Source;
			parameters[12].Value = model.TotalClick;
			parameters[13].Value = model.Pic;
			parameters[14].Value = model.IsAudit;
			parameters[15].Value = model.IsAddName;
			parameters[16].Value = model.UserID;

			DbHelperSQL.RunProcedure("News_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("News_Delete",parameters,out rowsAffected);
			return rowsAffected > 0;
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			var strSql=new StringBuilder();
			strSql.Append("delete from News ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			var rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Sys.Model.News GetModel(int ID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
						};
			parameters[0].Value = ID;

			var  model=new Sys.Model.News();
			var ds= DbHelperSQL.RunProcedure("News_GetModel",parameters,"ds");
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
				if(ds.Tables[0].Rows[0]["UpdateTime"]!=null && ds.Tables[0].Rows[0]["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateIp"]!=null && ds.Tables[0].Rows[0]["CreateIp"].ToString()!="")
				{
					model.CreateIp=ds.Tables[0].Rows[0]["CreateIp"].ToString();
				}
				if(ds.Tables[0].Rows[0]["UpdateIp"]!=null && ds.Tables[0].Rows[0]["UpdateIp"].ToString()!="")
				{
					model.UpdateIp=ds.Tables[0].Rows[0]["UpdateIp"].ToString();
				}
				if(ds.Tables[0].Rows[0]["NewsTitle"]!=null && ds.Tables[0].Rows[0]["NewsTitle"].ToString()!="")
				{
					model.NewsTitle=ds.Tables[0].Rows[0]["NewsTitle"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Abstract"]!=null && ds.Tables[0].Rows[0]["Abstract"].ToString()!="")
				{
					model.Abstract=ds.Tables[0].Rows[0]["Abstract"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Content"]!=null && ds.Tables[0].Rows[0]["Content"].ToString()!="")
				{
					model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				}
				if(ds.Tables[0].Rows[0]["TypeId"]!=null && ds.Tables[0].Rows[0]["TypeId"].ToString()!="")
				{
					model.TypeId=int.Parse(ds.Tables[0].Rows[0]["TypeId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GUID"]!=null && ds.Tables[0].Rows[0]["GUID"].ToString()!="")
				{
					model.GUID=ds.Tables[0].Rows[0]["GUID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Publisher"]!=null && ds.Tables[0].Rows[0]["Publisher"].ToString()!="")
				{
					model.Publisher=ds.Tables[0].Rows[0]["Publisher"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Source"]!=null && ds.Tables[0].Rows[0]["Source"].ToString()!="")
				{
					model.Source=ds.Tables[0].Rows[0]["Source"].ToString();
				}
				if(ds.Tables[0].Rows[0]["TotalClick"]!=null && ds.Tables[0].Rows[0]["TotalClick"].ToString()!="")
				{
					model.TotalClick=int.Parse(ds.Tables[0].Rows[0]["TotalClick"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Pic"]!=null && ds.Tables[0].Rows[0]["Pic"].ToString()!="")
				{
					model.Pic=ds.Tables[0].Rows[0]["Pic"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsAudit"]!=null && ds.Tables[0].Rows[0]["IsAudit"].ToString()!="")
				{
					model.IsAudit=int.Parse(ds.Tables[0].Rows[0]["IsAudit"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsAddName"]!=null && ds.Tables[0].Rows[0]["IsAddName"].ToString()!="")
				{
					model.IsAddName=int.Parse(ds.Tables[0].Rows[0]["IsAddName"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UserID"]!=null && ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
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

			var  model=new Sys.Model.News();
			var ds= DbHelperSQL.RunProcedure("News_GetModel",parameters,"ds");
			return ds.Tables[0];
		}

		#endregion  Method
	}
}

