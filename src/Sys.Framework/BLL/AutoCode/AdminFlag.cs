using System;
using System.Data;
using System.Collections.Generic;
using Sys.Common;
using Sys.Model;
namespace Sys.BLL
{
    /// <summary>
    /// 业务逻辑类AdminFlag 的摘要说明。
    /// </summary>
    public class AdminFlag
    {
        private readonly Sys.DAL.AdminFlag dal = new Sys.DAL.AdminFlag();
        public AdminFlag()

        { }
        #region  成员方法
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.AdminFlag GetModel(int ID)
        {

            return dal.GetModel(ID);
        }


        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Sys.Model.AdminFlag GetModelByCache(int ID)
        {

            string CacheKey = "AdminFlagModel-" + ID;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("DataCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Sys.Model.AdminFlag)objModel;
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.AdminFlag> GetList(int ParentID, int FlagType, int IsNav, string FlagAction)
        {
            string strWhere = "ParentID = " + ParentID;
            if (FlagType!=-1)
                strWhere += " and FlagType=" + FlagType.ToString();

            if (IsNav == 0 || IsNav == 1)
                strWhere += " and IsNav=" + IsNav;

            FlagAction = FlagAction.ToLower();
            if (FlagAction != "")
                strWhere += " and FlagAction='" + Utils.SqlStringFormat(FlagAction,1) + "'";

            DAL.Common db = new DAL.Common();
            DataSet ds = db.GetList("AdminFlag", -1, -1, strWhere, "OrderID asc"); // dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.AdminFlag> GetList(int parentID, int flagType)
        {
            string strWhere = "ParentID = " + parentID ;
            if (flagType != -1)
            {
                strWhere += "AND FlagType=" + flagType;
            }
            DAL.Common db = new DAL.Common();
            DataSet ds = db.GetList("AdminFlag", -1, -1, strWhere, "OrderID asc"); // dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.AdminFlag> DataTableToList(DataTable dt)
        {
            var modelList = new List<Model.AdminFlag>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.AdminFlag model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Sys.Model.AdminFlag();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["ParentID"].ToString() != "")
                    {
                        model.ParentID = int.Parse(dt.Rows[n]["ParentID"].ToString());
                    }
                    model.Flag = dt.Rows[n]["Flag"].ToString();
                    model.FlagName = dt.Rows[n]["FlagName"].ToString();
                    model.FlagAction = dt.Rows[n]["FlagAction"].ToString();
                    if (dt.Rows[n]["FlagType"].ToString() != "")
                    {
                        model.FlagType = int.Parse(dt.Rows[n]["FlagType"].ToString());
                    }
                    if (dt.Rows[n]["IsNav"].ToString() != "")
                    {
                        model.IsNav = int.Parse(dt.Rows[n]["IsNav"].ToString());
                    }
                    model.AppUrl = dt.Rows[n]["AppUrl"].ToString();
                    model.Icon = dt.Rows[n]["Icon"].ToString();
                    if (dt.Rows[n]["OrderID"].ToString() != "")
                    {
                        model.OrderID = int.Parse(dt.Rows[n]["OrderID"].ToString());
                    }
                    if (dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }
                    if (dt.Rows[n]["IsOpen"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsOpen"].ToString() == "1") || (dt.Rows[n]["IsOpen"].ToString().ToLower() == "true"))
                        {
                            model.IsOpen = true;
                        }
                        else
                        {
                            model.IsOpen = false;
                        }
                    }
                    if (dt.Rows[n]["HaveChildNav"].ToString() != "")
                    {
                        if ((dt.Rows[n]["HaveChildNav"].ToString() == "1") || (dt.Rows[n]["HaveChildNav"].ToString().ToLower() == "true"))
                        {
                            model.HaveChildNav = true;
                        }
                        else
                        {
                            model.HaveChildNav = false;
                        }
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        ///  获得子集列表
        /// </summary>
        /// <param name="ParentID">父级ID</param>
        /// <param name="FlagType">权限类别：0系统 1站点 2内容</param>
        /// <returns>ds</returns>
        public DataSet GetRoleChildFlag(int ParentID,int FlagType)
        {
            DAL.Common db = new DAL.Common();
            return db.GetSelect("AdminFlag ", " ParentID =" + ParentID + " AND FlagType=" + FlagType + "", " ID ", " ID,FlagName,HaveChildNav,Icon,Flag");
        }
        #endregion  成员方法

        public int GetMaxOrderID()
        {
            Sys.DAL.Common comm = new Sys.DAL.Common();
            return comm.GetMaxOrderID(" AdminFlag ", " OrderID ");
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.AdminFlag model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.AdminFlag model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ID)
        {

            return dal.Delete(ID);
        }



        /// <summary>
        /// 获得选择列表
        /// </summary>
        public DataSet GetCheckList(int FlagType)
        {
            DAL.Common db = new DAL.Common();
            DataSet ds = db.GetSelect("AdminFlag", "  FlagType = " + FlagType + "", "OrderID asc", " ID,FlagName"); // dal.GetList(strWhere);
            return ds;
        }





        #region  getlist方法
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.AdminFlag> GetList(string strWhere)
        {
           
            var db = new DAL.Common();
            var ds = db.GetList("AdminFlag", -1, -1, strWhere, "OrderID asc"); // dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        #endregion


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetTableModel(int fid)
        {

            return dal.GetTableModel(fid);
        }


        /// <summary>
        /// 返回一个数据表  List的前身
        /// </summary>
        public DataTable GetTable(int pageSize, int pageIndex, string strWhere, string strOrder)
        {
            var db = new DAL.Common();
            var ds = db.GetList("AdminFlag", pageSize, pageIndex, strWhere, strOrder);
            return ds.Tables[0];
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string FlagIdlist)
        {
            return dal.DeleteList(FlagIdlist);
        }


        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("OrderID", "AdminFlag");
        }
    }
}

