using System;
using System.Data;
using System.Collections.Generic;
using Sys.Common;
namespace Sys.BLL
{
	/// <summary>
	/// NewsType逻辑类的摘要说明
	/// 作者：王星海
	/// 日期：2014/5/7 22:05:59
	/// </summary>
	public partial class NewsType
	{
		private readonly Sys.DAL.NewsType _dal=new Sys.DAL.NewsType();
		public NewsType()
		{}
		#region  工具生成常用业务逻辑操作方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return _dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			return _dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Sys.Model.NewsType model)
		{
			return _dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Sys.Model.NewsType model)
		{
			return _dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return _dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return _dal.DeleteList(IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Sys.Model.NewsType GetModel(int ID)
		{
			
			return _dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Sys.Model.NewsType GetModelByCache(int ID)
		{
			
			var cacheKey = "NewsTypeModel-" + ID;
			var objModel = DataCache.GetCache(cacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = _dal.GetModel(ID);
					if (objModel != null)
					{
						var modelCache = ConfigHelper.GetConfigInt("DataCache");
						DataCache.SetCache(cacheKey, objModel, DateTime.Now.AddMinutes(modelCache), TimeSpan.Zero);
					}
				}
				catch
				{
					objModel = new object();
					return (Sys.Model.NewsType)objModel;
				}
			}
			return (Sys.Model.NewsType)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Sys.Model.NewsType> GetList(int pageSize, int pageIndex,string strWhere,string strOrder)
		{
			var db = new DAL.Common();
			var ds = db.GetList("NewsType", pageSize, pageIndex, strWhere, strOrder);
			return DataTableToList(ds.Tables[0]);
		}
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Sys.Model.NewsType> DataTableToList(DataTable dt)
		{
			var modelList = new List<Sys.Model.NewsType>();
			var rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Sys.Model.NewsType model;
				for (var n = 0; n < rowsCount; n++)
				{
					model = new Sys.Model.NewsType();
					if(dt.Rows[n]["ID"]!=null && dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					if(dt.Rows[n]["PId"]!=null && dt.Rows[n]["PId"].ToString()!="")
					{
						model.PId=int.Parse(dt.Rows[n]["PId"].ToString());
					}
					if(dt.Rows[n]["TypeName"]!=null && dt.Rows[n]["TypeName"].ToString()!="")
					{
					model.TypeName=dt.Rows[n]["TypeName"].ToString();
					}
					if(dt.Rows[n]["CreateTime"]!=null && dt.Rows[n]["CreateTime"].ToString()!="")
					{
						model.CreateTime=DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
					}
					if(dt.Rows[n]["CreateIp"]!=null && dt.Rows[n]["CreateIp"].ToString()!="")
					{
					model.CreateIp=dt.Rows[n]["CreateIp"].ToString();
					}
					if(dt.Rows[n]["Count"]!=null && dt.Rows[n]["Count"].ToString()!="")
					{
						model.Count=int.Parse(dt.Rows[n]["Count"].ToString());
					}
					if(dt.Rows[n]["TypeAction"]!=null && dt.Rows[n]["TypeAction"].ToString()!="")
					{
					model.TypeAction=dt.Rows[n]["TypeAction"].ToString();
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public List<Sys.Model.NewsType> GetListByCache(int pageSize, int pageIndex,string strWhere,string strOrder)
		{
			
			var cacheKey = "NewsTypeList-"+ pageIndex +" - "+ pageIndex+" - "+ strWhere+" - "+ strOrder;
			var objModel = DataCache.GetCache(cacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = GetList(pageSize,pageIndex,strWhere,strOrder);
					if (objModel != null)
					{
						var modelCache = ConfigHelper.GetConfigInt("DataCache");
						DataCache.SetCache(cacheKey, objModel, DateTime.Now.AddMinutes(modelCache), TimeSpan.Zero);
					}
				}
				catch
				{
					return new List<Sys.Model.NewsType>();
				}
			}
			return (List<Sys.Model.NewsType>) objModel;
		}

		/// <summary>
		/// 返回一个数据表  List的前身
		/// </summary>
		public DataTable GetTable(int pageSize, int pageIndex,string strWhere,string strOrder)
		{
			var db = new DAL.Common();
			var ds = db.GetList("NewsType", pageSize, pageIndex, strWhere, strOrder);
			return ds.Tables[0];
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public   DataTable  GetTableModel(int ID)
		{
			
			return _dal.GetTableModel(ID);
		}

		#endregion  Method
	}
}

