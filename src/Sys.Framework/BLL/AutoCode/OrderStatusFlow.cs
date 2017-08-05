using System;
using System.Data;
using System.Collections.Generic;
using Sys.Model;
namespace Sys.BLL
{
	/// <summary>
	/// OrderStatusFlow
	/// </summary>
	public partial class OrderStatusFlow
	{
		private readonly Sys.DAL.OrderStatusFlow dal=new Sys.DAL.OrderStatusFlow();
		public OrderStatusFlow()
		{}
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int FlowId)
        {
            return dal.Exists(FlowId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys.Model.OrderStatusFlow model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Sys.Model.OrderStatusFlow model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int FlowId)
        {

            return dal.Delete(FlowId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string FlowIdlist)
        {
            return dal.DeleteList(FlowIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.OrderStatusFlow GetModel(int FlowId)
        {

            return dal.GetModel(FlowId);
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.OrderStatusFlow> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.OrderStatusFlow> DataTableToList(DataTable dt)
        {
            List<Sys.Model.OrderStatusFlow> modelList = new List<Sys.Model.OrderStatusFlow>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Sys.Model.OrderStatusFlow model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Sys.Model.OrderStatusFlow();
                    if (dt.Rows[n]["FlowId"] != null && dt.Rows[n]["FlowId"].ToString() != "")
                    {
                        model.FlowId = int.Parse(dt.Rows[n]["FlowId"].ToString());
                    }
                    if (dt.Rows[n]["OrderId"] != null && dt.Rows[n]["OrderId"].ToString() != "")
                    {
                        model.OrderId = int.Parse(dt.Rows[n]["OrderId"].ToString());
                    }
                    if (dt.Rows[n]["OrderStatus"] != null && dt.Rows[n]["OrderStatus"].ToString() != "")
                    {
                        model.OrderStatus = dt.Rows[n]["OrderStatus"].ToString();
                    }
                    if (dt.Rows[n]["StatusMessage"] != null && dt.Rows[n]["StatusMessage"].ToString() != "")
                    {
                        model.StatusMessage = dt.Rows[n]["StatusMessage"].ToString();
                    }
                    if (dt.Rows[n]["StatusFlag"] != null && dt.Rows[n]["StatusFlag"].ToString() != "")
                    {
                        model.StatusFlag = int.Parse(dt.Rows[n]["StatusFlag"].ToString());
                    }
                    if (dt.Rows[n]["CreateUserId"] != null && dt.Rows[n]["CreateUserId"].ToString() != "")
                    {
                        model.CreateUserId = dt.Rows[n]["CreateUserId"].ToString();
                    }
                    if (dt.Rows[n]["CreateDate"] != null && dt.Rows[n]["CreateDate"].ToString() != "")
                    {
                        model.CreateDate = DateTime.Parse(dt.Rows[n]["CreateDate"].ToString());
                    }
                    if (dt.Rows[n]["ModifyUserId"] != null && dt.Rows[n]["ModifyUserId"].ToString() != "")
                    {
                        model.ModifyUserId = dt.Rows[n]["ModifyUserId"].ToString();
                    }
                    if (dt.Rows[n]["ModifyDate"] != null && dt.Rows[n]["ModifyDate"].ToString() != "")
                    {
                        model.ModifyDate = DateTime.Parse(dt.Rows[n]["ModifyDate"].ToString());
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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

