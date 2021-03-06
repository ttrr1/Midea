﻿using System;
using System.Data;
using System.Collections.Generic;
using Sys.Common;
namespace Sys.BLL
{
	/// <summary>
	/// Attendance逻辑类的摘要说明
	/// 作者：王星海
	/// 日期：2014/6/9 17:21:42
	/// </summary>
	public partial class Attendance
	{
		private readonly Sys.DAL.Attendance _dal=new Sys.DAL.Attendance();
		public Attendance()
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
		public bool Exists(int Id)
		{
			return _dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Sys.Model.Attendance model)
		{
			return _dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Sys.Model.Attendance model)
		{
			return _dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			return _dal.Delete(Id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			return _dal.DeleteList(Idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Sys.Model.Attendance GetModel(int Id)
		{
			
			return _dal.GetModel(Id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Sys.Model.Attendance GetModelByCache(int Id)
		{
			
			var cacheKey = "AttendanceModel-" + Id;
			var objModel = DataCache.GetCache(cacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = _dal.GetModel(Id);
					if (objModel != null)
					{
						var modelCache = ConfigHelper.GetConfigInt("DataCache");
						DataCache.SetCache(cacheKey, objModel, DateTime.Now.AddMinutes(modelCache), TimeSpan.Zero);
					}
				}
				catch
				{
					objModel = new object();
					return (Sys.Model.Attendance)objModel;
				}
			}
			return (Sys.Model.Attendance)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Sys.Model.Attendance> GetList(int pageSize, int pageIndex,string strWhere,string strOrder)
		{
			var db = new DAL.Common();
			var ds = db.GetList("Attendance", pageSize, pageIndex, strWhere, strOrder);
			return DataTableToList(ds.Tables[0]);
		}
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Sys.Model.Attendance> DataTableToList(DataTable dt)
		{
			var modelList = new List<Sys.Model.Attendance>();
			var rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Sys.Model.Attendance model;
				for (var n = 0; n < rowsCount; n++)
				{
					model = new Sys.Model.Attendance();
					if(dt.Rows[n]["Id"]!=null && dt.Rows[n]["Id"].ToString()!="")
					{
						model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
					}
					if(dt.Rows[n]["ICcode"]!=null && dt.Rows[n]["ICcode"].ToString()!="")
					{
					model.ICcode=dt.Rows[n]["ICcode"].ToString();
					}
					if(dt.Rows[n]["SId"]!=null && dt.Rows[n]["SId"].ToString()!="")
					{
						model.SId=int.Parse(dt.Rows[n]["SId"].ToString());
					}
					if(dt.Rows[n]["CreateTime"]!=null && dt.Rows[n]["CreateTime"].ToString()!="")
					{
						model.CreateTime=DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
					}
					if(dt.Rows[n]["CreateIp"]!=null && dt.Rows[n]["CreateIp"].ToString()!="")
					{
					model.CreateIp=dt.Rows[n]["CreateIp"].ToString();
					}
					if(dt.Rows[n]["MendSign"]!=null && dt.Rows[n]["MendSign"].ToString()!="")
					{
						model.MendSign=int.Parse(dt.Rows[n]["MendSign"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public List<Sys.Model.Attendance> GetListByCache(int pageSize, int pageIndex,string strWhere,string strOrder)
		{
			
			var cacheKey = "AttendanceList-"+ pageIndex +" - "+ pageIndex+" - "+ strWhere+" - "+ strOrder;
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
					return new List<Sys.Model.Attendance>();
				}
			}
			return (List<Sys.Model.Attendance>) objModel;
		}

		/// <summary>
		/// 返回一个数据表  List的前身
		/// </summary>
		public DataTable GetTable(int pageSize, int pageIndex,string strWhere,string strOrder)
		{
			var db = new DAL.Common();
			var ds = db.GetList("Attendance", pageSize, pageIndex, strWhere, strOrder);
			return ds.Tables[0];
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public   DataTable  GetTableModel(int Id)
		{
			
			return _dal.GetTableModel(Id);
		}

		#endregion  Method
	}
}

