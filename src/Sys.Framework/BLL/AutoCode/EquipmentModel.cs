using System;
using System.Data;
using System.Collections.Generic;
using Sys.Model;
namespace Sys.BLL
{
	/// <summary>
	/// EquipmentModel
	/// </summary>
	public partial class EquipmentModel
	{
		private readonly Sys.DAL.EquipmentModel dal=new Sys.DAL.EquipmentModel();
		public EquipmentModel()
		{}
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ModelId)
        {
            return dal.Exists(ModelId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys.Model.EquipmentModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Sys.Model.EquipmentModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ModelId)
        {

            return dal.Delete(ModelId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ModelIdlist)
        {
            return dal.DeleteList(ModelIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.EquipmentModel GetModel(int ModelId)
        {

            return dal.GetModel(ModelId);
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
        public List<Sys.Model.EquipmentModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.EquipmentModel> DataTableToList(DataTable dt)
        {
            List<Sys.Model.EquipmentModel> modelList = new List<Sys.Model.EquipmentModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Sys.Model.EquipmentModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Sys.Model.EquipmentModel();
                    if (dt.Rows[n]["ModelId"] != null && dt.Rows[n]["ModelId"].ToString() != "")
                    {
                        model.ModelId = int.Parse(dt.Rows[n]["ModelId"].ToString());
                    }
                    if (dt.Rows[n]["ModelName"] != null && dt.Rows[n]["ModelName"].ToString() != "")
                    {
                        model.ModelName = dt.Rows[n]["ModelName"].ToString();
                    }
                    if (dt.Rows[n]["EqpType"] != null && dt.Rows[n]["EqpType"].ToString() != "")
                    {
                        model.EqpType = int.Parse(dt.Rows[n]["EqpType"].ToString());
                    }
                    if (dt.Rows[n]["ParentModelId"] != null && dt.Rows[n]["ParentModelId"].ToString() != "")
                    {
                        model.ParentModelId = int.Parse(dt.Rows[n]["ParentModelId"].ToString());
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
                    if (dt.Rows[n]["Status"] != null && dt.Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = dt.Rows[n]["Status"].ToString();
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

