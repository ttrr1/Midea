using System;
using System.Data;
using System.Collections.Generic;
using Sys.Model;
namespace Sys.BLL
{
	/// <summary>
	/// OrderComment
	/// </summary>
	public partial class OrderComment
	{
		private readonly Sys.DAL.OrderComment dal=new Sys.DAL.OrderComment();
		public OrderComment()
		{}
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CommentId)
        {
            return dal.Exists(CommentId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys.Model.OrderComment model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Sys.Model.OrderComment model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int CommentId)
        {

            return dal.Delete(CommentId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string CommentIdlist)
        {
            return dal.DeleteList(CommentIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.OrderComment GetModel(int CommentId)
        {

            return dal.GetModel(CommentId);
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
        public List<Sys.Model.OrderComment> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.OrderComment> DataTableToList(DataTable dt)
        {
            List<Sys.Model.OrderComment> modelList = new List<Sys.Model.OrderComment>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Sys.Model.OrderComment model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Sys.Model.OrderComment();
                    if (dt.Rows[n]["CommentId"] != null && dt.Rows[n]["CommentId"].ToString() != "")
                    {
                        model.CommentId = int.Parse(dt.Rows[n]["CommentId"].ToString());
                    }
                    if (dt.Rows[n]["OrderId"] != null && dt.Rows[n]["OrderId"].ToString() != "")
                    {
                        model.OrderId = int.Parse(dt.Rows[n]["OrderId"].ToString());
                    }
                    if (dt.Rows[n]["StarLevel"] != null && dt.Rows[n]["StarLevel"].ToString() != "")
                    {
                        model.StarLevel = dt.Rows[n]["StarLevel"].ToString();
                    }
                    if (dt.Rows[n]["Remark"] != null && dt.Rows[n]["Remark"].ToString() != "")
                    {
                        model.Remark = dt.Rows[n]["Remark"].ToString();
                    }
                    if (dt.Rows[n]["CreateUserId"] != null && dt.Rows[n]["CreateUserId"].ToString() != "")
                    {
                        model.CreateUserId = dt.Rows[n]["CreateUserId"].ToString();
                    }
                    if (dt.Rows[n]["CreateDate"] != null && dt.Rows[n]["CreateDate"].ToString() != "")
                    {
                        model.CreateDate = DateTime.Parse(dt.Rows[n]["CreateDate"].ToString());
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

