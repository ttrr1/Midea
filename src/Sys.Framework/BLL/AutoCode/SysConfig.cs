using System;
using System.Data;
using System.Collections.Generic;
using Sys.Common;
using Sys.Model;
namespace Sys.BLL
{
    /// <summary>
    /// 业务逻辑类SysConfig 的摘要说明。
    /// </summary>
    public class SysConfig
    {
        private readonly Sys.DAL.SysConfig dal = new Sys.DAL.SysConfig();
        public SysConfig()
        { }
        #region  成员方法
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="Key">键名</param>
        /// <returns></returns>
        public Sys.Model.SysConfig GetModel(string Item, string Key)
        {
            return dal.GetModel(Item, Key);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="Key">键名</param>
        /// <returns></returns>
        public Sys.Model.SysConfig GetModelByCache(string Item, string Key)
        {
            string CacheKey = "SysConfigModel-" + Item +"-" + Key;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetModel(Item, Key);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("DataCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Sys.Model.SysConfig)objModel;
        }

        /// <summary>
        /// 数据列表
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="IsUsing">是否启用 0不启用 1启用 -1全部</param>
        /// <returns></returns>
        public DataSet GetDataSet(string Item, int IsUsing)
        {
            string strWhere = "Item='" + Utils.SqlStringFormat(Item,1) + "'";
            if (IsUsing == 0 || IsUsing == 1)
                strWhere += " and IsUsing=" + IsUsing;
            DAL.Common db = new DAL.Common();
            DataSet ds = db.GetList("SysConfig", 0, 0, strWhere, "OrderID asc"); //GetList(Item, IsUsing);
            return ds;
        }

        /// <summary>
        /// 数据列表，从缓存中。
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="IsUsing">是否启用 0不启用 1启用 -1全部</param>
        /// <returns></returns>
        public DataSet GetDataSetByCache(string Item, int IsUsing)
        {
            string CacheKey = "SysConfigDataSet-" + Item + "-" + IsUsing;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetDataSet(Item, IsUsing);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("DataCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (DataSet)objModel;
        }

        /// <summary>
        /// 数据Select
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="IsUsing">是否启用 0不启用 1启用 -1全部</param>
        /// <returns></returns>
        public DataSet GetSelect(string Item, int IsUsing)
        {
            string strWhere = "Item='" + Utils.SqlStringFormat(Item,1) + "'";
            if (IsUsing == 0 || IsUsing == 1)
                strWhere += " and IsUsing=" + IsUsing;
            DAL.Common db = new DAL.Common();
            DataSet ds = db.GetSelect("SysConfig", strWhere, "OrderID asc","[Value],[Name]"); //GetList(Item, IsUsing);
            return ds;
        }

        /// <summary>
        /// 数据Select，从缓存中。
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="IsUsing">是否启用 0不启用 1启用 -1全部</param>
        /// <returns></returns>
        public DataSet GetSelectByCache(string Item, int IsUsing)
        {
            string CacheKey = "SysConfigSelect-" + Item + "-" + IsUsing;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetSelect(Item, IsUsing);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("DataCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (DataSet)objModel;
        }

        /// <summary>
        /// 数据列表
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="IsUsing">是否启用 0不启用 1启用 -1全部</param>
        /// <returns></returns>
        public List<Sys.Model.SysConfig> GetList(string Item, int IsUsing)
        {
            string strWhere = "Item='" +Utils.SqlStringFormat(Item,1)+"'";
            if (IsUsing == 0 || IsUsing == 1)
                strWhere += " and IsUsing=" + IsUsing;
            DAL.Common db = new DAL.Common();
            DataSet ds = db.GetList("SysConfig", 0, 0, strWhere, "OrderID asc"); //GetList(Item, IsUsing);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 数据列表，从缓存中。
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="IsUsing">是否启用 0不启用 1启用 -1全部</param>
        /// <returns></returns>
        public List<Sys.Model.SysConfig> GetListByCache(string Item, int IsUsing)
        {
            string CacheKey = "SysConfigList-" + Item +"-" + IsUsing;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetList(Item, IsUsing);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("DataCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Sys.Model.SysConfig>)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        private List<Sys.Model.SysConfig> DataTableToList(DataTable dt)
        {
            List<Sys.Model.SysConfig> modelList = new List<Sys.Model.SysConfig>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Sys.Model.SysConfig model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Sys.Model.SysConfig();
                    model.Item = dt.Rows[n]["Item"].ToString();
                    model.Key = dt.Rows[n]["Key"].ToString();
                    model.Name = dt.Rows[n]["Name"].ToString();
                    model.Value = dt.Rows[n]["Value"].ToString();
                    if (dt.Rows[n]["OrderID"].ToString() != "")
                    {
                        model.OrderID = int.Parse(dt.Rows[n]["OrderID"].ToString());
                    }
                    if (dt.Rows[n]["IsUsing"].ToString() != "")
                    {
                        model.IsUsing = int.Parse(dt.Rows[n]["IsUsing"].ToString());
                    }
                    model.Note = dt.Rows[n]["Note"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(Sys.Model.SysConfig model)
        {
            dal.Update(model);
        }


        #endregion  成员方法

        #region  静态方法
        /// <summary>
        /// 得到名称
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="Key">键名</param>
        /// <returns></returns>
        public static string GetName(string Item, string Key)
        {
            Sys.BLL.SysConfig bll = new Sys.BLL.SysConfig();
            Sys.Model.SysConfig model = bll.GetModel(Item, Key);
            if (model == null)
                return "";
            else
                return model.Name;
        }

        /// <summary>
        /// 得到名称，从缓存中。
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="Key">键名</param>
        /// <returns></returns>
        public static string GetNameByCache(string Item, string Key)
        {
            string CacheKey = "SysConfigName-" + Item + "-" + Key;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetName(Item, Key);
                    if (objModel != null)
                    {
                        int ModelCache = 180;// ConfigHelper.GetConfigInt("DataCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (string)objModel;
        }

        /// <summary>
        /// 得到一个string数值
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="Key">键名</param>
        /// <returns></returns>
        public static string GetString(string Item, string Key)
        {
            Sys.BLL.SysConfig bll = new Sys.BLL.SysConfig();
            Sys.Model.SysConfig model = bll.GetModel(Item, Key);
            if (model == null)
                return "";
            else
                return model.Value;
        }

        /// <summary>
        /// 得到一个string数值，从缓存中。
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="Key">键名</param>
        /// <returns></returns>
        public static string GetStringByCache(string Item, string Key)
        {
            string CacheKey = "SysConfigValue-" + Item + "-" + Key;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetString(Item, Key);
                    if (objModel != null)
                    {
                        int ModelCache = 180;// ConfigHelper.GetConfigInt("DataCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (string)objModel;
        }

        /// <summary>
        /// 得到一个int数值
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="Key">键名</param>
        /// <param name="defValue">缺省值</param>
        /// <returns></returns>
        public static int GetInt(string Item, string Key, int defValue)
        {
            return Utils.StrToInt(GetString(Item,Key), defValue);
        }

        /// <summary>
        /// 得到一个int数值，从缓存中。
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="Key">键名</param>
        /// <param name="defValue">缺省值</param>
        /// <returns></returns>
        public static int GetIntByCache(string Item, string Key, int defValue)
        {
            return Utils.StrToInt(GetStringByCache(Item, Key), defValue);
        }



        #endregion  静态方法
    }
}

