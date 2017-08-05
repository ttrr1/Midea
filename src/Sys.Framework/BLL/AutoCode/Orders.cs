using System;
using System.Data;
using System.Collections.Generic;
using Sys.Model;
namespace Sys.BLL
{
	/// <summary>
	/// Orders
	/// </summary>
	public partial class Orders
	{
		private readonly Sys.DAL.Orders dal=new Sys.DAL.Orders();
		public Orders()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int OrderId)
		{
			return dal.Exists(OrderId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Sys.Model.Orders model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Sys.Model.Orders model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int OrderId)
		{
			
			return dal.Delete(OrderId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string OrderIdlist )
		{
			return dal.DeleteList(OrderIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Sys.Model.Orders GetModel(int OrderId)
		{
			
			return dal.GetModel(OrderId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Sys.Model.Orders GetModelByCache(int OrderId)
		{
			
			string CacheKey = "OrdersModel-" + OrderId;
			object objModel = Sys.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(OrderId);
					if (objModel != null)
					{
						int ModelCache = Sys.Common.ConfigHelper.GetConfigInt("ModelCache");
						Sys.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Sys.Model.Orders)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Sys.Model.Orders> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Sys.Model.Orders> DataTableToList(DataTable dt)
		{
			List<Sys.Model.Orders> modelList = new List<Sys.Model.Orders>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Sys.Model.Orders model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Sys.Model.Orders();
					if(dt.Rows[n]["OrderId"]!=null && dt.Rows[n]["OrderId"].ToString()!="")
					{
						model.OrderId=int.Parse(dt.Rows[n]["OrderId"].ToString());
					}
					if(dt.Rows[n]["UserLoginId"]!=null && dt.Rows[n]["UserLoginId"].ToString()!="")
					{
					model.UserLoginId=dt.Rows[n]["UserLoginId"].ToString();
					}
					if(dt.Rows[n]["WorkerId"]!=null && dt.Rows[n]["WorkerId"].ToString()!="")
					{
					model.WorkerId=dt.Rows[n]["WorkerId"].ToString();
					}
					if(dt.Rows[n]["ProjectName"]!=null && dt.Rows[n]["ProjectName"].ToString()!="")
					{
					model.ProjectName=dt.Rows[n]["ProjectName"].ToString();
					}
					if(dt.Rows[n]["CustomName"]!=null && dt.Rows[n]["CustomName"].ToString()!="")
					{
					model.CustomName=dt.Rows[n]["CustomName"].ToString();
					}
					if(dt.Rows[n]["ProvinceId"]!=null && dt.Rows[n]["ProvinceId"].ToString()!="")
					{
						model.ProvinceId=int.Parse(dt.Rows[n]["ProvinceId"].ToString());
					}
					if(dt.Rows[n]["ProvinceName"]!=null && dt.Rows[n]["ProvinceName"].ToString()!="")
					{
					model.ProvinceName=dt.Rows[n]["ProvinceName"].ToString();
					}
					if(dt.Rows[n]["CityId"]!=null && dt.Rows[n]["CityId"].ToString()!="")
					{
						model.CityId=int.Parse(dt.Rows[n]["CityId"].ToString());
					}
					if(dt.Rows[n]["CityName"]!=null && dt.Rows[n]["CityName"].ToString()!="")
					{
					model.CityName=dt.Rows[n]["CityName"].ToString();
					}
					if(dt.Rows[n]["AreaId"]!=null && dt.Rows[n]["AreaId"].ToString()!="")
					{
						model.AreaId=int.Parse(dt.Rows[n]["AreaId"].ToString());
					}
					if(dt.Rows[n]["AreaName"]!=null && dt.Rows[n]["AreaName"].ToString()!="")
					{
					model.AreaName=dt.Rows[n]["AreaName"].ToString();
					}
					if(dt.Rows[n]["sAreaId"]!=null && dt.Rows[n]["sAreaId"].ToString()!="")
					{
						model.sAreaId=int.Parse(dt.Rows[n]["sAreaId"].ToString());
					}
					if(dt.Rows[n]["sAreaName"]!=null && dt.Rows[n]["sAreaName"].ToString()!="")
					{
					model.sAreaName=dt.Rows[n]["sAreaName"].ToString();
					}
					if(dt.Rows[n]["Address"]!=null && dt.Rows[n]["Address"].ToString()!="")
					{
					model.Address=dt.Rows[n]["Address"].ToString();
					}
					if(dt.Rows[n]["Contact"]!=null && dt.Rows[n]["Contact"].ToString()!="")
					{
					model.Contact=dt.Rows[n]["Contact"].ToString();
					}
					if(dt.Rows[n]["Tel"]!=null && dt.Rows[n]["Tel"].ToString()!="")
					{
					model.Tel=dt.Rows[n]["Tel"].ToString();
					}
					if(dt.Rows[n]["HandleDate"]!=null && dt.Rows[n]["HandleDate"].ToString()!="")
					{
						model.HandleDate=DateTime.Parse(dt.Rows[n]["HandleDate"].ToString());
					}
					if(dt.Rows[n]["OrderStatus"]!=null && dt.Rows[n]["OrderStatus"].ToString()!="")
					{
					model.OrderStatus=dt.Rows[n]["OrderStatus"].ToString();
					}
					if(dt.Rows[n]["StatusFlag"]!=null && dt.Rows[n]["StatusFlag"].ToString()!="")
					{
						model.StatusFlag=int.Parse(dt.Rows[n]["StatusFlag"].ToString());
					}
					if(dt.Rows[n]["Remark"]!=null && dt.Rows[n]["Remark"].ToString()!="")
					{
					model.Remark=dt.Rows[n]["Remark"].ToString();
					}
					if(dt.Rows[n]["PicList"]!=null && dt.Rows[n]["PicList"].ToString()!="")
					{
					model.PicList=dt.Rows[n]["PicList"].ToString();
					}
					if(dt.Rows[n]["CreateUserId"]!=null && dt.Rows[n]["CreateUserId"].ToString()!="")
					{
					model.CreateUserId=dt.Rows[n]["CreateUserId"].ToString();
					}
					if(dt.Rows[n]["CreateDate"]!=null && dt.Rows[n]["CreateDate"].ToString()!="")
					{
						model.CreateDate=DateTime.Parse(dt.Rows[n]["CreateDate"].ToString());
					}
					if(dt.Rows[n]["ModifyUserId"]!=null && dt.Rows[n]["ModifyUserId"].ToString()!="")
					{
					model.ModifyUserId=dt.Rows[n]["ModifyUserId"].ToString();
					}
					if(dt.Rows[n]["ModifyDate"]!=null && dt.Rows[n]["ModifyDate"].ToString()!="")
					{
						model.ModifyDate=DateTime.Parse(dt.Rows[n]["ModifyDate"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method
	}
}

