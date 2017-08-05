using System;
using System.Data;
using System.Collections.Generic;
using Sys.Model;
namespace Sys.BLL
{
	/// <summary>
	/// OrderType
	/// </summary>
	public partial class OrderType
	{
		private readonly Sys.DAL.OrderType dal=new Sys.DAL.OrderType();
		public OrderType()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int TypeId)
		{
			return dal.Exists(TypeId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Sys.Model.OrderType model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Sys.Model.OrderType model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int TypeId)
		{
			
			return dal.Delete(TypeId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string TypeIdlist )
		{
			return dal.DeleteList(TypeIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Sys.Model.OrderType GetModel(int TypeId)
		{
			
			return dal.GetModel(TypeId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Sys.Model.OrderType GetModelByCache(int TypeId)
		{
			
			string CacheKey = "OrderTypeModel-" + TypeId;
			object objModel = Sys.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(TypeId);
					if (objModel != null)
					{
						int ModelCache = Sys.Common.ConfigHelper.GetConfigInt("ModelCache");
						Sys.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Sys.Model.OrderType)objModel;
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
		public List<Sys.Model.OrderType> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Sys.Model.OrderType> DataTableToList(DataTable dt)
		{
			List<Sys.Model.OrderType> modelList = new List<Sys.Model.OrderType>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Sys.Model.OrderType model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Sys.Model.OrderType();
					if(dt.Rows[n]["TypeId"]!=null && dt.Rows[n]["TypeId"].ToString()!="")
					{
						model.TypeId=int.Parse(dt.Rows[n]["TypeId"].ToString());
					}
					if(dt.Rows[n]["OrderId"]!=null && dt.Rows[n]["OrderId"].ToString()!="")
					{
						model.OrderId=int.Parse(dt.Rows[n]["OrderId"].ToString());
					}
					if(dt.Rows[n]["ProductType"]!=null && dt.Rows[n]["ProductType"].ToString()!="")
					{
						model.ProductType=int.Parse(dt.Rows[n]["ProductType"].ToString());
					}
					if(dt.Rows[n]["ProductTypeName"]!=null && dt.Rows[n]["ProductTypeName"].ToString()!="")
					{
					model.ProductTypeName=dt.Rows[n]["ProductTypeName"].ToString();
					}
					if(dt.Rows[n]["MainModelId"]!=null && dt.Rows[n]["MainModelId"].ToString()!="")
					{
						model.MainModelId=int.Parse(dt.Rows[n]["MainModelId"].ToString());
					}
					if(dt.Rows[n]["MainModelName"]!=null && dt.Rows[n]["MainModelName"].ToString()!="")
					{
					model.MainModelName=dt.Rows[n]["MainModelName"].ToString();
					}
					if(dt.Rows[n]["MainModelNum"]!=null && dt.Rows[n]["MainModelNum"].ToString()!="")
					{
						model.MainModelNum=int.Parse(dt.Rows[n]["MainModelNum"].ToString());
					}
					if(dt.Rows[n]["SubModelId1"]!=null && dt.Rows[n]["SubModelId1"].ToString()!="")
					{
						model.SubModelId1=int.Parse(dt.Rows[n]["SubModelId1"].ToString());
					}
					if(dt.Rows[n]["SubModelName1"]!=null && dt.Rows[n]["SubModelName1"].ToString()!="")
					{
					model.SubModelName1=dt.Rows[n]["SubModelName1"].ToString();
					}
					if(dt.Rows[n]["SubModelNum1"]!=null && dt.Rows[n]["SubModelNum1"].ToString()!="")
					{
						model.SubModelNum1=int.Parse(dt.Rows[n]["SubModelNum1"].ToString());
					}
					if(dt.Rows[n]["SubModelId2"]!=null && dt.Rows[n]["SubModelId2"].ToString()!="")
					{
						model.SubModelId2=int.Parse(dt.Rows[n]["SubModelId2"].ToString());
					}
					if(dt.Rows[n]["SubModelName2"]!=null && dt.Rows[n]["SubModelName2"].ToString()!="")
					{
					model.SubModelName2=dt.Rows[n]["SubModelName2"].ToString();
					}
					if(dt.Rows[n]["SubModelNum2"]!=null && dt.Rows[n]["SubModelNum2"].ToString()!="")
					{
						model.SubModelNum2=int.Parse(dt.Rows[n]["SubModelNum2"].ToString());
					}
					if(dt.Rows[n]["SubModelId3"]!=null && dt.Rows[n]["SubModelId3"].ToString()!="")
					{
						model.SubModelId3=int.Parse(dt.Rows[n]["SubModelId3"].ToString());
					}
					if(dt.Rows[n]["SubModelName3"]!=null && dt.Rows[n]["SubModelName3"].ToString()!="")
					{
					model.SubModelName3=dt.Rows[n]["SubModelName3"].ToString();
					}
					if(dt.Rows[n]["SubModelNum3"]!=null && dt.Rows[n]["SubModelNum3"].ToString()!="")
					{
						model.SubModelNum3=int.Parse(dt.Rows[n]["SubModelNum3"].ToString());
					}
					if(dt.Rows[n]["SubModelId4"]!=null && dt.Rows[n]["SubModelId4"].ToString()!="")
					{
						model.SubModelId4=int.Parse(dt.Rows[n]["SubModelId4"].ToString());
					}
					if(dt.Rows[n]["SubModelName4"]!=null && dt.Rows[n]["SubModelName4"].ToString()!="")
					{
					model.SubModelName4=dt.Rows[n]["SubModelName4"].ToString();
					}
					if(dt.Rows[n]["SubModelNum4"]!=null && dt.Rows[n]["SubModelNum4"].ToString()!="")
					{
						model.SubModelNum4=int.Parse(dt.Rows[n]["SubModelNum4"].ToString());
					}
					if(dt.Rows[n]["ModelId"]!=null && dt.Rows[n]["ModelId"].ToString()!="")
					{
						model.ModelId=int.Parse(dt.Rows[n]["ModelId"].ToString());
					}
					if(dt.Rows[n]["ModelName"]!=null && dt.Rows[n]["ModelName"].ToString()!="")
					{
					model.ModelName=dt.Rows[n]["ModelName"].ToString();
					}
					if(dt.Rows[n]["ModelNum"]!=null && dt.Rows[n]["ModelNum"].ToString()!="")
					{
						model.ModelNum=int.Parse(dt.Rows[n]["ModelNum"].ToString());
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

