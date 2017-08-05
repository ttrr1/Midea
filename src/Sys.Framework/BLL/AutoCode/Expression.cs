using System;
using System.Data;
using System.Collections.Generic;
using Sys.Common;
namespace Sys.BLL
{
	/// <summary>
	/// Expression逻辑类的摘要说明
	/// 作者：王星海
	/// 日期：2014/6/21 16:10:04
	/// </summary>
	public partial class Expression
	{
		private readonly Sys.DAL.Expression _dal=new Sys.DAL.Expression();
		public Expression()
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
		public int  Add(Sys.Model.Expression model)
		{
			return _dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Sys.Model.Expression model)
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
		public Sys.Model.Expression GetModel(int ID)
		{
			
			return _dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Sys.Model.Expression GetModelByCache(int ID)
		{
			
			var cacheKey = "ExpressionModel-" + ID;
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
					return (Sys.Model.Expression)objModel;
				}
			}
			return (Sys.Model.Expression)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Sys.Model.Expression> GetList(int pageSize, int pageIndex,string strWhere,string strOrder)
		{
			var db = new DAL.Common();
			var ds = db.GetList("Expression", pageSize, pageIndex, strWhere, strOrder);
			return DataTableToList(ds.Tables[0]);
		}
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Sys.Model.Expression> DataTableToList(DataTable dt)
		{
			var modelList = new List<Sys.Model.Expression>();
			var rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Sys.Model.Expression model;
				for (var n = 0; n < rowsCount; n++)
				{
					model = new Sys.Model.Expression();
					if(dt.Rows[n]["ID"]!=null && dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					if(dt.Rows[n]["CreateTime"]!=null && dt.Rows[n]["CreateTime"].ToString()!="")
					{
						model.CreateTime=DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
					}
					if(dt.Rows[n]["SId"]!=null && dt.Rows[n]["SId"].ToString()!="")
					{
						model.SId=int.Parse(dt.Rows[n]["SId"].ToString());
					}
					if(dt.Rows[n]["StuId"]!=null && dt.Rows[n]["StuId"].ToString()!="")
					{
						model.StuId=int.Parse(dt.Rows[n]["StuId"].ToString());
					}
					if(dt.Rows[n]["GId"]!=null && dt.Rows[n]["GId"].ToString()!="")
					{
						model.GId=int.Parse(dt.Rows[n]["GId"].ToString());
					}
					if(dt.Rows[n]["CId"]!=null && dt.Rows[n]["CId"].ToString()!="")
					{
					model.CId=dt.Rows[n]["CId"].ToString();
					}
					if(dt.Rows[n]["Content"]!=null && dt.Rows[n]["Content"].ToString()!="")
					{
					model.Content=dt.Rows[n]["Content"].ToString();
					}
					if(dt.Rows[n]["F_UserId"]!=null && dt.Rows[n]["F_UserId"].ToString()!="")
					{
					model.F_UserId=dt.Rows[n]["F_UserId"].ToString();
					}
					if(dt.Rows[n]["F_RealName"]!=null && dt.Rows[n]["F_RealName"].ToString()!="")
					{
					model.F_RealName=dt.Rows[n]["F_RealName"].ToString();
					}
					if(dt.Rows[n]["CreateIp"]!=null && dt.Rows[n]["CreateIp"].ToString()!="")
					{
					model.CreateIp=dt.Rows[n]["CreateIp"].ToString();
					}
					if(dt.Rows[n]["SendName"]!=null && dt.Rows[n]["SendName"].ToString()!="")
					{
						model.SendName=int.Parse(dt.Rows[n]["SendName"].ToString());
					}
					if(dt.Rows[n]["SendStates"]!=null && dt.Rows[n]["SendStates"].ToString()!="")
					{
						model.SendStates=int.Parse(dt.Rows[n]["SendStates"].ToString());
					}
					if(dt.Rows[n]["SendSuccess"]!=null && dt.Rows[n]["SendSuccess"].ToString()!="")
					{
						model.SendSuccess=int.Parse(dt.Rows[n]["SendSuccess"].ToString());
					}
					if(dt.Rows[n]["TypeID"]!=null && dt.Rows[n]["TypeID"].ToString()!="")
					{
						model.TypeID=int.Parse(dt.Rows[n]["TypeID"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public List<Sys.Model.Expression> GetListByCache(int pageSize, int pageIndex,string strWhere,string strOrder)
		{
			
			var cacheKey = "ExpressionList-"+ pageIndex +" - "+ pageIndex+" - "+ strWhere+" - "+ strOrder;
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
					return new List<Sys.Model.Expression>();
				}
			}
			return (List<Sys.Model.Expression>) objModel;
		}

		/// <summary>
		/// 返回一个数据表  List的前身
		/// </summary>
		public DataTable GetTable(int pageSize, int pageIndex,string strWhere,string strOrder)
		{
			var db = new DAL.Common();
			var ds = db.GetList("Expression", pageSize, pageIndex, strWhere, strOrder);
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

