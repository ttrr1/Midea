using System;
using System.Data;
using System.Collections.Generic;
using Sys.Common;
namespace Sys.BLL
{
	/// <summary>
	/// Services逻辑类的摘要说明
	/// 作者：王星海
	/// 日期：2014/6/24 9:38:01
	/// </summary>
	public partial class Services
	{
		private readonly Sys.DAL.Services _dal=new Sys.DAL.Services();
		public Services()
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
		public int  Add(Sys.Model.Services model)
		{
			return _dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Sys.Model.Services model)
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
		public Sys.Model.Services GetModel(int ID)
		{
			
			return _dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Sys.Model.Services GetModelByCache(int ID)
		{
			
			var cacheKey = "ServicesModel-" + ID;
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
					return (Sys.Model.Services)objModel;
				}
			}
			return (Sys.Model.Services)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Sys.Model.Services> GetList(int pageSize, int pageIndex,string strWhere,string strOrder)
		{
			var db = new DAL.Common();
			var ds = db.GetList("Services", pageSize, pageIndex, strWhere, strOrder);
			return DataTableToList(ds.Tables[0]);
		}
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Sys.Model.Services> DataTableToList(DataTable dt)
		{
			var modelList = new List<Sys.Model.Services>();
			var rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Sys.Model.Services model;
				for (var n = 0; n < rowsCount; n++)
				{
					model = new Sys.Model.Services();
					if(dt.Rows[n]["ID"]!=null && dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					if(dt.Rows[n]["Name"]!=null && dt.Rows[n]["Name"].ToString()!="")
					{
					model.Name=dt.Rows[n]["Name"].ToString();
					}
					if(dt.Rows[n]["Picpath"]!=null && dt.Rows[n]["Picpath"].ToString()!="")
					{
					model.Picpath=dt.Rows[n]["Picpath"].ToString();
					}
					if(dt.Rows[n]["linkAddress"]!=null && dt.Rows[n]["linkAddress"].ToString()!="")
					{
					model.linkAddress=dt.Rows[n]["linkAddress"].ToString();
					}
					if(dt.Rows[n]["OrderId"]!=null && dt.Rows[n]["OrderId"].ToString()!="")
					{
						model.OrderId=int.Parse(dt.Rows[n]["OrderId"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public List<Sys.Model.Services> GetListByCache(int pageSize, int pageIndex,string strWhere,string strOrder)
		{
			
			var cacheKey = "ServicesList-"+ pageIndex +" - "+ pageIndex+" - "+ strWhere+" - "+ strOrder;
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
					return new List<Sys.Model.Services>();
				}
			}
			return (List<Sys.Model.Services>) objModel;
		}

		/// <summary>
		/// 返回一个数据表  List的前身
		/// </summary>
		public DataTable GetTable(int pageSize, int pageIndex,string strWhere,string strOrder)
		{
			var db = new DAL.Common();
			var ds = db.GetList("Services", pageSize, pageIndex, strWhere, strOrder);
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

