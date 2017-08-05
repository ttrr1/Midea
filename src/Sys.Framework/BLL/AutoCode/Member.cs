using System;
using System.Data;
using System.Collections.Generic;
using Sys.Model;
using Sys.Common;
namespace Sys.BLL
{
    /// <summary>
    /// 业务逻辑类Member 的摘要说明。
    /// </summary>
    public class Member
    {
        private readonly Sys.DAL.Member dal = new Sys.DAL.Member();
        public Member()
        { }
        #region  成员方法
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.Member GetModel(int UserID)
        {

            return dal.GetModel(UserID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Sys.Model.Member GetModelByCache(int UserID)
        {

            string CacheKey = "MemberModel-" + UserID;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(UserID);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("DataCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Sys.Model.Member)objModel;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys.Model.Member model, string Password)
        {
            return dal.Add(model, Password);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddReturnUserID(Sys.Model.Member model, string Password)
        {
            return dal.AddReturnUserID(model, Password);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Sys.Model.Member model)
        {
            return dal.Update(model);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdateBasicInfo(Sys.Model.Member model)
        {
            return dal.UpdateBasicInfo(model);
        }

        /// <summary>
        ///  更新一条数据(包含所有字段)
        /// </summary>
        public int Update_AllInfo(Sys.Model.Member model)
        {
            return dal.Update_AllInfo(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int UserID)
        {
            dal.Delete(UserID);
        }

        /// <summary>
        /// 解锁和锁定
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="isAdmin">1admin 2member 其他 seller</param>
        /// <returns></returns>
        public int ChageState(int UserID, int isAdmin)
        {
            return dal.ChageState(UserID, isAdmin);
        }
        /// <summary>
        /// 返回用户分组下来列表
        /// </summary>
        /// <returns>返回用户分组下来列表</returns>
        public DataSet GetSel()
        {
            DAL.Common db = new DAL.Common();
            return db.GetSelect("MemberGroup", "", " GroupID ", " GroupID,GroupName ");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="isAdmin">1表示是Amdin,-1表是不是Admin</param>
        /// <returns></returns>
        public List<Sys.Model.Member> GetList(int isAdmin)
        {
            string strWhere = "";
            if (isAdmin == 1)
            {
                strWhere += " UserID in (SELECT UserID FROM Admin) ";
            }
            else if (isAdmin == -1)
            {
                strWhere += " UserID not in (SELECT UserID FROM Admin) ";
            }
            DAL.Common db = new DAL.Common();
            DataSet ds = db.GetList("Member", -1, -1, strWhere, "UserID asc");
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.Member> DataTableToList(DataTable dt)
        {
            List<Sys.Model.Member> modelList = new List<Sys.Model.Member>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Sys.Model.Member model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Sys.Model.Member();
                    if (dt.Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(dt.Rows[n]["UserID"].ToString());
                    }
                    model.Username = dt.Rows[n]["Username"].ToString();
                    if (dt.Rows[n]["GroupID"].ToString() != "")
                    {
                        model.GroupID = int.Parse(dt.Rows[n]["GroupID"].ToString());
                    }
                    model.GroupName = dt.Rows[n]["GroupName"].ToString();
                    model.Email = dt.Rows[n]["Email"].ToString();
                    if (dt.Rows[n]["EmailState"].ToString() != "")
                    {
                        model.EmailState = int.Parse(dt.Rows[n]["EmailState"].ToString());
                    }
                    if (dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }
                    model.CreateIP = dt.Rows[n]["CreateIP"].ToString();
                    model.Question = dt.Rows[n]["Question"].ToString();
                    model.Answer = dt.Rows[n]["Answer"].ToString();
                    if (dt.Rows[n]["LoginTimes"].ToString() != "")
                    {
                        model.LoginTimes = int.Parse(dt.Rows[n]["LoginTimes"].ToString());
                    }
                    if (dt.Rows[n]["LastLoginTime"].ToString() != "")
                    {
                        model.LastLoginTime = DateTime.Parse(dt.Rows[n]["LastLoginTime"].ToString());
                    }
                    model.LastLoginIP = dt.Rows[n]["LastLoginIP"].ToString();
                    if (dt.Rows[n]["ThisLoginTime"].ToString() != "")
                    {
                        model.ThisLoginTime = DateTime.Parse(dt.Rows[n]["ThisLoginTime"].ToString());
                    }
                    model.ThisLoginIP = dt.Rows[n]["ThisLoginIP"].ToString();
                    if (dt.Rows[n]["ActiveTime"].ToString() != "")
                    {
                        model.ActiveTime = DateTime.Parse(dt.Rows[n]["ActiveTime"].ToString());
                    }
                    if (dt.Rows[n]["State"].ToString() != "")
                    {
                        model.State = int.Parse(dt.Rows[n]["State"].ToString());
                    }
                    if (dt.Rows[n]["IsVIP"].ToString() != "")
                    {
                        model.IsVIP = int.Parse(dt.Rows[n]["IsVIP"].ToString());
                    }
                    if (dt.Rows[n]["VIPStartTime"].ToString() != "")
                    {
                        model.VIPStartTime = DateTime.Parse(dt.Rows[n]["VIPStartTime"].ToString());
                    }
                    if (dt.Rows[n]["VIPEndTime"].ToString() != "")
                    {
                        model.VIPEndTime = DateTime.Parse(dt.Rows[n]["VIPEndTime"].ToString());
                    }
                    if (dt.Rows[n]["Birthday"].ToString() != "")
                    {
                        model.Birthday = DateTime.Parse(dt.Rows[n]["Birthday"].ToString());
                    }
                    model.RealName = dt.Rows[n]["RealName"].ToString();
                    model.Sign = dt.Rows[n]["Sign"].ToString();
                    model.QQ = dt.Rows[n]["QQ"].ToString();
                    model.MSN = dt.Rows[n]["MSN"].ToString();
                    model.Guid = dt.Rows[n]["Guid"].ToString();
                    if (dt.Rows[n]["Integral"].ToString() != "")
                    {
                        model.Integral = int.Parse(dt.Rows[n]["Integral"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 检测用户注册的Email是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>是否存在</returns>
        public bool CheckEmailExist(string userName)
        {
            return dal.CheckEmailExist(userName);
        }


        /// <summary>
        /// 根据用户名、找回密码问题、找回密码答案找回密码
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="question">找回密码问题</param>
        /// <param name="answer">找回密码答案</param>
        /// <returns>密码</returns>
        public int UpdatePwd(string name, string question, string answer, string newpwd)
        {
            return dal.UpdatePwd(name, question, answer, newpwd);
        }

        /// <summary>
        /// 获得数据列表(分页获取)
        /// </summary>
        public List<Sys.Model.Member> GetList(int PageSize, int PageIndex, string strWhere)
        {
            string strOrder = "CreateTime desc";
            DAL.Common db = new DAL.Common();
            DataSet ds = db.GetList("Member", PageSize,PageIndex, strWhere, "UserID asc");
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得用户总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            DAL.Common db = new DAL.Common();
            return db.GetCount("Member", strWhere);
        }
        #endregion  成员方法
    }
}

