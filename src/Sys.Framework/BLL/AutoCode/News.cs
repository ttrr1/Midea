using System;
using System.Data;
using System.Collections.Generic;
using Sys.Common;
namespace Sys.BLL
{
	/// <summary>
	/// News逻辑类的摘要说明
	/// 作者：王星海
	/// 日期：2014/6/21 15:33:30
	/// </summary>
	public partial class News
	{
		private readonly Sys.DAL.News _dal=new Sys.DAL.News();
		public News()
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
		public int  Add(Sys.Model.News model)
		{
			return _dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Sys.Model.News model)
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
		public Sys.Model.News GetModel(int ID)
		{
			
			return _dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Sys.Model.News GetModelByCache(int ID)
		{
			
			var cacheKey = "NewsModel-" + ID;
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
					return (Sys.Model.News)objModel;
				}
			}
			return (Sys.Model.News)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Sys.Model.News> GetList(int pageSize, int pageIndex,string strWhere,string strOrder)
		{
			var db = new DAL.Common();
			var ds = db.GetList("News", pageSize, pageIndex, strWhere, strOrder);
			return DataTableToList(ds.Tables[0]);
		}
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Sys.Model.News> DataTableToList(DataTable dt)
		{
			var modelList = new List<Sys.Model.News>();
			var rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Sys.Model.News model;
				for (var n = 0; n < rowsCount; n++)
				{
					model = new Sys.Model.News();
					if(dt.Rows[n]["ID"]!=null && dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					if(dt.Rows[n]["CreateTime"]!=null && dt.Rows[n]["CreateTime"].ToString()!="")
					{
						model.CreateTime=DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
					}
					if(dt.Rows[n]["UpdateTime"]!=null && dt.Rows[n]["UpdateTime"].ToString()!="")
					{
						model.UpdateTime=DateTime.Parse(dt.Rows[n]["UpdateTime"].ToString());
					}
					if(dt.Rows[n]["CreateIp"]!=null && dt.Rows[n]["CreateIp"].ToString()!="")
					{
					model.CreateIp=dt.Rows[n]["CreateIp"].ToString();
					}
					if(dt.Rows[n]["UpdateIp"]!=null && dt.Rows[n]["UpdateIp"].ToString()!="")
					{
					model.UpdateIp=dt.Rows[n]["UpdateIp"].ToString();
					}
					if(dt.Rows[n]["NewsTitle"]!=null && dt.Rows[n]["NewsTitle"].ToString()!="")
					{
					model.NewsTitle=dt.Rows[n]["NewsTitle"].ToString();
					}
					if(dt.Rows[n]["Abstract"]!=null && dt.Rows[n]["Abstract"].ToString()!="")
					{
					model.Abstract=dt.Rows[n]["Abstract"].ToString();
					}
					if(dt.Rows[n]["Content"]!=null && dt.Rows[n]["Content"].ToString()!="")
					{
					model.Content=dt.Rows[n]["Content"].ToString();
					}
					if(dt.Rows[n]["TypeId"]!=null && dt.Rows[n]["TypeId"].ToString()!="")
					{
						model.TypeId=int.Parse(dt.Rows[n]["TypeId"].ToString());
					}
					if(dt.Rows[n]["GUID"]!=null && dt.Rows[n]["GUID"].ToString()!="")
					{
					model.GUID=dt.Rows[n]["GUID"].ToString();
					}
					if(dt.Rows[n]["Publisher"]!=null && dt.Rows[n]["Publisher"].ToString()!="")
					{
					model.Publisher=dt.Rows[n]["Publisher"].ToString();
					}
					if(dt.Rows[n]["Source"]!=null && dt.Rows[n]["Source"].ToString()!="")
					{
					model.Source=dt.Rows[n]["Source"].ToString();
					}
					if(dt.Rows[n]["TotalClick"]!=null && dt.Rows[n]["TotalClick"].ToString()!="")
					{
						model.TotalClick=int.Parse(dt.Rows[n]["TotalClick"].ToString());
					}
					if(dt.Rows[n]["Pic"]!=null && dt.Rows[n]["Pic"].ToString()!="")
					{
					model.Pic=dt.Rows[n]["Pic"].ToString();
					}
					if(dt.Rows[n]["IsAudit"]!=null && dt.Rows[n]["IsAudit"].ToString()!="")
					{
						model.IsAudit=int.Parse(dt.Rows[n]["IsAudit"].ToString());
					}
					if(dt.Rows[n]["IsAddName"]!=null && dt.Rows[n]["IsAddName"].ToString()!="")
					{
						model.IsAddName=int.Parse(dt.Rows[n]["IsAddName"].ToString());
					}
					if(dt.Rows[n]["UserID"]!=null && dt.Rows[n]["UserID"].ToString()!="")
					{
						model.UserID=int.Parse(dt.Rows[n]["UserID"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public List<Sys.Model.News> GetListByCache(int pageSize, int pageIndex,string strWhere,string strOrder)
		{
			
			var cacheKey = "NewsList-"+ pageIndex +" - "+ pageIndex+" - "+ strWhere+" - "+ strOrder;
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
					return new List<Sys.Model.News>();
				}
			}
			return (List<Sys.Model.News>) objModel;
		}

		/// <summary>
		/// 返回一个数据表  List的前身
		/// </summary>
		public DataTable GetTable(int pageSize, int pageIndex,string strWhere,string strOrder)
		{
			var db = new DAL.Common();
			var ds = db.GetList("News", pageSize, pageIndex, strWhere, strOrder);
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

