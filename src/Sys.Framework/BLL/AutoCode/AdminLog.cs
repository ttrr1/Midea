using System;
using System.Data;
using System.Collections.Generic;
using Sys.Common;
using Sys.Model;
namespace Sys.BLL
{
    /// <summary>
    /// 业务逻辑类AdminLog 的摘要说明。
    /// </summary>
    public class AdminLog
    {
        private readonly Sys.DAL.AdminLog dal = new Sys.DAL.AdminLog();
        public AdminLog()
        { }
        #region 静态方法
        /// <summary>
        /// 添加系统日志
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="Flag">模块</param>
        /// <param name="Log">日志</param>
        public static void Add(int UserID,string Flag,string Log)
        {
            AdminLog bll = new AdminLog();
            Model.AdminLog model = new Model.AdminLog();
            model.UserID=UserID;
            model.Flag = Flag;
            model.Log = Log;
            model.CreateIP = Utils.GetRealIP();
            bll.AddModel(model);
        }

        /// <summary>
        /// 获得用户
        /// </summary>
        /// <returns></returns>
        public static DataSet GetUser()
        {
            DAL.Common db = new Sys.DAL.Common();
            return db.GetSelect("AdminLog", "", "", "distinct userid,username");
        }
        #endregion

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddModel(Sys.Model.AdminLog model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.AdminLog GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Sys.Model.AdminLog GetModelByCache(int ID)
        {

            string CacheKey = "AdminLogModel-" + ID;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("DataCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Sys.Model.AdminLog)objModel;
        }


        /// <summary>
        /// 获得总数
        /// </summary>
        /// <param name="Flag"></param>
        /// <param name="FlagLike"></param>
        /// <param name="UserID"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public int GetCount(string Flag, int FlagLike, int UserID, string StartTime, string EndTime)
        {
            string strWhere = "";
            if (Flag != "")
            {
                if (strWhere != "")
                    strWhere += " and ";
                if (FlagLike == 1)
                    strWhere += "Flag like '%" + Utils.SqlStringFormat(Flag,2) + "%'";  
                else
                    strWhere += "Flag ='" + Utils.SqlStringFormat(Flag,1) + "'";
            }

            if (UserID > 0)
            {
                if (strWhere != "")
                    strWhere += " and ";
                strWhere += "UserID =" + UserID + "";
            }

            if (StartTime != "" & Utils.IsDateTime(StartTime))
            {
                if (strWhere != "")
                    strWhere += " and ";
                strWhere += "CreateTime >='" + Utils.SqlStringFormat(StartTime,1) + "'";
            }

            if (EndTime != "" & Utils.IsDateTime(EndTime))
            {
                if (strWhere != "")
                    strWhere += " and ";
                strWhere += "CreateTime <='" + Utils.SqlStringFormat(EndTime,1) + "'";
            }
            DAL.Common db = new DAL.Common();
            return db.GetCount("AdminLog", strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="Flag"></param>
        /// <param name="UserID"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public List<Sys.Model.AdminLog> GetList(int PageSize,int PageIndex,string Flag,int FlagLike, int UserID,string StartTime,string EndTime)
        {
            string strWhere = "";
            if (Flag != "")
            {
                if (strWhere != "")
                    strWhere += " and ";
                if(FlagLike==1)
                    strWhere += "Flag like '%" + Utils.SqlStringFormat(Flag,2) + "%'";
                else
                    strWhere += "Flag ='" + Utils.SqlStringFormat(Flag,1) + "'";
            }

            if (UserID > 0)
            {
                if (strWhere != "")
                    strWhere += " and ";
                strWhere += "UserID =" + UserID + "";
            }

            if (StartTime != "" & Utils.IsDateTime(StartTime))
            {
                if (strWhere != "")
                    strWhere += " and ";
                strWhere += "CreateTime >='" + Utils.SqlStringFormat(StartTime,1) + "'";
            }

            if (EndTime != "" & Utils.IsDateTime(EndTime))
            {
                if (strWhere != "")
                    strWhere += " and ";
                strWhere += "CreateTime <='" + Utils.SqlStringFormat(EndTime,1) + "'";
            }

            string strOrder = "ID desc";
            DAL.Common db = new DAL.Common();
            DataSet ds = db.GetList("AdminLog", PageSize, PageIndex, strWhere, strOrder);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表，从缓存中。
        /// </summary>
        public List<Sys.Model.AdminLog> GetListByCache(int PageSize, int PageIndex, string Flag,int FlagLike, int UserID, string StartTime, string EndTime)
        {

            string CacheKey = "AdminLogList-" + PageSize + "-" + PageIndex + "-" + Flag + "-" + FlagLike + "-" + UserID + "-" + StartTime + "-" + EndTime;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetList(PageSize, PageIndex, Flag,FlagLike, UserID, StartTime, EndTime);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("DataCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Sys.Model.AdminLog>)objModel;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.AdminLog> DataTableToList(DataTable dt)
        {
            List<Sys.Model.AdminLog> modelList = new List<Sys.Model.AdminLog>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Sys.Model.AdminLog model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Sys.Model.AdminLog();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(dt.Rows[n]["UserID"].ToString());
                    }
                    model.Username = dt.Rows[n]["Username"].ToString();
                    model.Flag = dt.Rows[n]["Flag"].ToString();
                    model.Log = dt.Rows[n]["Log"].ToString();
                    if (dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }
                    model.CreateIP = dt.Rows[n]["CreateIP"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }



        #endregion  成员方法


        /// <summary>
        /// 返回一个数据表  List的前身
        /// </summary>
        public DataTable GetTable(int pageSize, int pageIndex, string strWhere, string strOrder)
        {
            var db = new DAL.Common();
            var ds = db.GetList("AdminLog", pageSize, pageIndex, strWhere, strOrder);
            return ds.Tables[0];
        }
    }
}

