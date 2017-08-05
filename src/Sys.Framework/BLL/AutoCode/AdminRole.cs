using System;
using System.Data;
using System.Collections.Generic;
using Sys.Common;
using Sys.Model;
namespace Sys.BLL
{
    /// <summary>
    /// 业务逻辑类AdminRole 的摘要说明。
    /// </summary>
    public class AdminRole
    {
        private readonly Sys.DAL.AdminRole dal = new Sys.DAL.AdminRole();
        public AdminRole()
        { }
        #region  成员方法
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.AdminRole GetModel(int RoleID)
        {
            return dal.GetModel(RoleID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Sys.Model.AdminRole GetModelByCache(int RoleID)
        {

            string CacheKey = "AdminRoleModel-" + RoleID;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(RoleID);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("DataCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Sys.Model.AdminRole)objModel;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.AdminRole> GetList()
        {
            DAL.Common db = new DAL.Common();
            DataSet ds = db.GetList("AdminRole", -1, -1, "", "OrderID asc");  //dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.AdminRole> GetList(string RoleIDs)
        {
            DAL.Common db = new DAL.Common();
            string strWhere ="";
            if (RoleIDs == "")
                strWhere = " 1=2";
            else
                strWhere = "RoleID in (" + RoleIDs + ")";

            DataSet ds = db.GetList("AdminRole", -1, -1, strWhere , "OrderID asc");  //dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.AdminRole> DataTableToList(DataTable dt)
        {
            List<Sys.Model.AdminRole> modelList = new List<Sys.Model.AdminRole>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Sys.Model.AdminRole model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Sys.Model.AdminRole();
                    if (dt.Rows[n]["RoleID"].ToString() != "")
                    {
                        model.RoleID = int.Parse(dt.Rows[n]["RoleID"].ToString());
                    }
                    model.RoleName = dt.Rows[n]["RoleName"].ToString();
                    model.RoleFlag = dt.Rows[n]["RoleFlag"].ToString();
                    if (dt.Rows[n]["AdminNum"].ToString() != "")
                    {
                        model.AdminNum = int.Parse(dt.Rows[n]["AdminNum"].ToString());
                    }
                    if (dt.Rows[n]["OrderID"].ToString() != "")
                    {
                        model.OrderID = int.Parse(dt.Rows[n]["OrderID"].ToString());
                    }
                    model.Note = dt.Rows[n]["Note"].ToString();
                    if (dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }
                    if (dt.Rows[n]["UpdateTime"].ToString() != "")
                    {
                        model.UpdateTime = DateTime.Parse(dt.Rows[n]["UpdateTime"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys.Model.AdminRole model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Sys.Model.AdminRole model)
        {
           return dal.Update(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdateRoleFlagForSiteFlag(Sys.Model.AdminRole model)
        {
            return dal.UpdateRoleFlagForSiteFlag(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int  Delete(int RoleID)
        {

            return dal.Delete(RoleID);
        }

        public int GetMaxOrderID()
        {
            Sys.DAL.Common comm = new Sys.DAL.Common();
            return comm.GetMaxOrderID(" AdminRole ", " OrderID ");
        }



    #endregion  成员方法



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.AdminRole> GetList(int pageSize, int pageIndex, string strWhere, string strOrder)
        {
            var db = new DAL.Common();
            var ds = db.GetList("AdminRole", pageSize, pageIndex, strWhere, strOrder);
            return DataTableToList(ds.Tables[0]);
        }


        /// <summary>
        /// 返回一个数据表  List的前身
        /// </summary>
        public DataTable GetTable(int pageSize, int pageIndex, string strWhere, string strOrder)
        {
            var db = new DAL.Common();
            var ds = db.GetList("AdminRole", pageSize, pageIndex, strWhere, strOrder);
            return ds.Tables[0];
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetTableModel(int RoleId)
        {

            return dal.GetTableModel(RoleId);
        }
    }
}

