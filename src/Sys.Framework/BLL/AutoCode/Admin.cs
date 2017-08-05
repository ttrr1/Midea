using System;
using System.Data;
using System.Collections.Generic;
using Sys.Common;
namespace Sys.BLL
{
    /// <summary>
    /// Admin逻辑类的摘要说明
    /// 作者：王星海
    /// 日期：2013/1/9 8:40:42
    /// </summary>
    public partial class Admin
    {
        private readonly DAL.Admin _dal = new DAL.Admin();
        public Admin()
        { }
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
        public bool Exists(int UserId)
        {
            return _dal.Exists(UserId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys.Model.Admin model)
        {
            return _dal.Add(model);
        }



        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int UserId)
        {

            return _dal.Delete(UserId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string UserIdlist)
        {
            return _dal.DeleteList(UserIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.Admin GetModel(int UserId)
        {

            return _dal.GetModel(UserId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Sys.Model.Admin GetModelByCache(int UserId)
        {

            var cacheKey = "AdminModel-" + UserId;
            var objModel = DataCache.GetCache(cacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = _dal.GetModel(UserId);
                    if (objModel != null)
                    {
                        var modelCache = ConfigHelper.GetConfigInt("DataCache");
                        DataCache.SetCache(cacheKey, objModel, DateTime.Now.AddMinutes(modelCache), TimeSpan.Zero);
                    }
                }
                catch
                {
                    objModel = new object();
                    return (Sys.Model.Admin)objModel;
                }
            }
            return (Sys.Model.Admin)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.Admin> GetList(int pageSize, int pageIndex, string strWhere, string strOrder)
        {
            var db = new DAL.Common();
            var ds = db.GetList("Admin", pageSize, pageIndex, strWhere, strOrder);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.Admin> DataTableToList(DataTable dt)
        {
            var modelList = new List<Sys.Model.Admin>();
            var rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Sys.Model.Admin model;
                for (var n = 0; n < rowsCount; n++)
                {
                    model = new Sys.Model.Admin();
                    if (dt.Rows[n]["UserId"] != null && dt.Rows[n]["UserId"].ToString() != "")
                    {
                        model.UserId = int.Parse(dt.Rows[n]["UserId"].ToString());
                    }
                    if (dt.Rows[n]["Username"] != null && dt.Rows[n]["Username"].ToString() != "")
                    {
                        model.Username = dt.Rows[n]["Username"].ToString();
                    }
                    if (dt.Rows[n]["UserFlag"] != null && dt.Rows[n]["UserFlag"].ToString() != "")
                    {
                        model.UserFlag = dt.Rows[n]["UserFlag"].ToString();
                    }
                    if (dt.Rows[n]["RoleIDs"] != null && dt.Rows[n]["RoleIDs"].ToString() != "")
                    {
                        model.RoleIDs = dt.Rows[n]["RoleIDs"].ToString();
                    }
                    if (dt.Rows[n]["RoleNames"] != null && dt.Rows[n]["RoleNames"].ToString() != "")
                    {
                        model.RoleNames = dt.Rows[n]["RoleNames"].ToString();
                    }
                    if (dt.Rows[n]["RoleFlags"] != null && dt.Rows[n]["RoleFlags"].ToString() != "")
                    {
                        model.RoleFlags = dt.Rows[n]["RoleFlags"].ToString();
                    }
                    if (dt.Rows[n]["PlusFlag"] != null && dt.Rows[n]["PlusFlag"].ToString() != "")
                    {
                        model.PlusFlag = dt.Rows[n]["PlusFlag"].ToString();
                    }
                    if (dt.Rows[n]["RealName"] != null && dt.Rows[n]["RealName"].ToString() != "")
                    {
                        model.RealName = dt.Rows[n]["RealName"].ToString();
                    }
                    if (dt.Rows[n]["JobTitle"] != null && dt.Rows[n]["JobTitle"].ToString() != "")
                    {
                        model.JobTitle = dt.Rows[n]["JobTitle"].ToString();
                    }
                    if (dt.Rows[n]["JobDept"] != null && dt.Rows[n]["JobDept"].ToString() != "")
                    {
                        model.JobDept = dt.Rows[n]["JobDept"].ToString();
                    }
                    if (dt.Rows[n]["QQ"] != null && dt.Rows[n]["QQ"].ToString() != "")
                    {
                        model.QQ = dt.Rows[n]["QQ"].ToString();
                    }
                    if (dt.Rows[n]["MSN"] != null && dt.Rows[n]["MSN"].ToString() != "")
                    {
                        model.MSN = dt.Rows[n]["MSN"].ToString();
                    }
                    if (dt.Rows[n]["Email"] != null && dt.Rows[n]["Email"].ToString() != "")
                    {
                        model.Email = dt.Rows[n]["Email"].ToString();
                    }
                    if (dt.Rows[n]["Mobile"] != null && dt.Rows[n]["Mobile"].ToString() != "")
                    {
                        model.Mobile = dt.Rows[n]["Mobile"].ToString();
                    }
                    if (dt.Rows[n]["HomeTel"] != null && dt.Rows[n]["HomeTel"].ToString() != "")
                    {
                        model.HomeTel = dt.Rows[n]["HomeTel"].ToString();
                    }
                    if (dt.Rows[n]["OfficeTel"] != null && dt.Rows[n]["OfficeTel"].ToString() != "")
                    {
                        model.OfficeTel = dt.Rows[n]["OfficeTel"].ToString();
                    }
                    if (dt.Rows[n]["IsFounder"] != null && dt.Rows[n]["IsFounder"].ToString() != "")
                    {
                        model.IsFounder = int.Parse(dt.Rows[n]["IsFounder"].ToString());
                    }
                    if (dt.Rows[n]["State"] != null && dt.Rows[n]["State"].ToString() != "")
                    {
                        model.State = int.Parse(dt.Rows[n]["State"].ToString());
                    }
                    if (dt.Rows[n]["Guid"] != null && dt.Rows[n]["Guid"].ToString() != "")
                    {
                        model.Guid = dt.Rows[n]["Guid"].ToString();
                    }
                    if (dt.Rows[n]["CreateTime"] != null && dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }
                    if (dt.Rows[n]["CreateIP"] != null && dt.Rows[n]["CreateIP"].ToString() != "")
                    {
                        model.CreateIP = dt.Rows[n]["CreateIP"].ToString();
                    }
                    if (dt.Rows[n]["ParentUserID"] != null && dt.Rows[n]["ParentUserID"].ToString() != "")
                    {
                        model.ParentUserID = int.Parse(dt.Rows[n]["ParentUserID"].ToString());
                    }
                    if (dt.Rows[n]["ParentUserIDs"] != null && dt.Rows[n]["ParentUserIDs"].ToString() != "")
                    {
                        model.ParentUserIDs = dt.Rows[n]["ParentUserIDs"].ToString();
                    }
                    if (dt.Rows[n]["IsPublic"] != null && dt.Rows[n]["IsPublic"].ToString() != "")
                    {
                        model.IsPublic = int.Parse(dt.Rows[n]["IsPublic"].ToString());
                    }
                  



                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public List<Sys.Model.Admin> GetListByCache(int pageSize, int pageIndex, string strWhere, string strOrder)
        {

            var cacheKey = "AdminList-" + pageIndex + " - " + pageIndex + " - " + strWhere + " - " + strOrder;
            var objModel = DataCache.GetCache(cacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetList(pageSize, pageIndex, strWhere, strOrder);
                    if (objModel != null)
                    {
                        var modelCache = ConfigHelper.GetConfigInt("DataCache");
                        DataCache.SetCache(cacheKey, objModel, DateTime.Now.AddMinutes(modelCache), TimeSpan.Zero);
                    }
                }
                catch
                {
                    return new List<Sys.Model.Admin>();
                }
            }
            return (List<Sys.Model.Admin>)objModel;
        }

        /// <summary>
        /// 返回一个数据表  List的前身
        /// </summary>
        public DataTable GetTable(int pageSize, int pageIndex, string strWhere, string strOrder)
        {
            var db = new DAL.Common();
            var ds = db.GetList("Admin", pageSize, pageIndex, strWhere, strOrder);
            return ds.Tables[0];
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetTableModel(int UserId)
        {

            return _dal.GetTableModel(UserId);
        }

        #endregion  Method


        /// <summary>
        /// 获取角色列表 
        /// </summary>
        /// <returns></returns>
        public DataSet GetRoleChk(string RoleIDs)
        {
            var db = new DAL.Common();
            var ds = db.GetSelect("AdminRole", "", " RoleID ", " RoleID,RoleName");
            if (RoleIDs.IndexOf("67") == -1)
            {
                DataTable dt = ds.Tables[0];
                int rowsCount = dt.Rows.Count;
                for (int i = 0; i < rowsCount; i++)
                {
                    if (RoleIDs.IndexOf(dt.Rows[i]["RoleID"].ToString()) == -1)
                    {
                        dt.Rows[i]["RoleID"] = -1;
                    }
                }
            }

            return ds;
        }



        public int UpdateAdminInfo(Model.Admin model)
        {
            return _dal.updateAdminInfo(model);
        }


        public DataSet GetRoleChildFlag(int ParentID)
        {
            DAL.Common db = new DAL.Common();
            return db.GetSelect("AdminFlag ", " ParentID =" + ParentID + " AND FlagType=0", " ID ", " ID,FlagName,HaveChildNav,Icon,Flag");
        }

        /// <summary>
        /// 设置管理员帐户
        /// </summary>
        /// <param name="Username">帐户</param>
        public static void SetUsername(string Username)
        {
            //Cookie保存
            //518400年 43200月 1440天
            Utils.WriteCookie("manage_Username", Utils.UrlEncode(Username), 518400, ConfigHelper.GetConfigString("CookieDomain"));
        }

        // <summary>
        /// 获得管理员姓名
        /// </summary>
        /// <returns></returns>
        public static string GetRealName()
        {
            string realname = Utils.UrlDecode(Utils.GetCookie("manage_RealName"));
            if (realname == "")
            {
                Admin bll = new Admin();
                Model.Admin model = bll.GetModel(Account.GetLoginUserID());
                realname = model.RealName;
                //Cookie保存
                //518400年 43200月 1440天
                Utils.WriteCookie("manage_RealName", Utils.UrlEncode(realname), 518400, ConfigHelper.GetConfigString("CookieDomain"));
            }
            return realname;
        }

        /// <summary>
        /// 设置管理员姓名
        /// </summary>
        /// <param name="RealName">姓名</param>
        /// <returns></returns>
        public static void SetRealName(string RealName)
        {
            //Cookie保存
            //518400年 43200月 1440天
            Utils.WriteCookie("manage_RealName", Utils.UrlEncode(RealName), 518400, ConfigHelper.GetConfigString("CookieDomain"));
        }



        /// <summary>
        /// 获得数据列表 
        /// </summary>
        /// <returns></returns>
        public List<Sys.Model.Admin> GetList(string strWhere)
        {
            const string strOrder = " UserName";
            const int pageSize = -1;
            const int pageIndex = -1;
            var db = new DAL.Common();
            var ds = db.GetList("Admin", pageSize, pageIndex, strWhere, strOrder);
            return DataTableToList(ds.Tables[0]);
        }



        /// <summary>
        /// 移动账号层次
        /// </summary>
        /// <param name="model"></param>
        public void UpDateLeave(Model.Admin model)
        {
            _dal.UpDateLeave(model);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys.Model.Admin model, string password)
        {
            return _dal.Add(model, password);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.Admin model, string password)
        {
            return _dal.Update(model, password);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdateForPlusSysRoleFlag(Model.Admin model)
        {
            return _dal.UpdateForPlusSysRoleFlag(model);
        }








        /// <summary>
        /// 返回所有子帐号的集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetChildIds(int userId)
        {
            var msg = userId.ToString();
            var list = GetList("ParentUserID=" + userId);
            foreach (var admin in list)
            {
                msg += "," + admin.UserId + ReturnChildId(admin.UserId);
            }

            msg = msg.Replace(",,", ",");
            return Utils.Strquotes(msg);


        }

        public string ReturnChildId(int userId)
        {
            var msg = ",";
            var list = GetList("ParentUserID=" + userId);
            foreach (var admin in list)
            {
                msg +=  admin.UserId + ",";
                msg += ReturnChildId(admin.UserId);
            }

            return Utils.Strquotes(msg);

        }
    }
}

