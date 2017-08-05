using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Sys.Common;
using Sys.Model;
namespace Sys.BLL
{
	/// <summary>
	/// 业务逻辑类Account 的摘要说明。
	/// </summary>
	public partial class Account
	{
		private readonly Sys.DAL.Account dal=new Sys.DAL.Account();
		public Account()
		{}
		#region  成员方法

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
		public bool Exists(int UserID)
		{
			return dal.Exists(UserID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Sys.Model.Account model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(Sys.Model.Account model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int UserID)
		{
			
			dal.Delete(UserID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Sys.Model.Account GetModel(int UserID)
		{
			
			return dal.GetModel(UserID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public Sys.Model.Account GetModelByCache(int UserID)
		{
			
			string CacheKey = "AccountModel-" + UserID;
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
				catch{}
			}
			return (Sys.Model.Account)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Sys.Model.Account> GetList(string strCondition)
		{
			var strWhere ="";
			var strOrder ="";
			var PageSize = -1;
			var PageIndex = -1;
			var db = new DAL.Common();
			var ds = db.GetList("Account", PageSize, PageIndex, strWhere, strOrder);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表，从缓存中。
		/// </summary>
		public List<Sys.Model.Account> GetListByCache(string strCondition)
		{
			
			string CacheKey = "AccountList-" + strCondition ;
			object objModel = DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = GetList(strCondition);
					if (objModel != null)
					{
						int ModelCache = ConfigHelper.GetConfigInt("DataCache");
						DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (List<Sys.Model.Account>)objModel;
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Sys.Model.Account> DataTableToList(DataTable dt)
		{
			List<Sys.Model.Account> modelList = new List<Sys.Model.Account>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Sys.Model.Account model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Sys.Model.Account();
					if(dt.Rows[n]["UserID"].ToString()!="")
					{
						model.UserID=int.Parse(dt.Rows[n]["UserID"].ToString());
					}
					model.Username=dt.Rows[n]["Username"].ToString();
					model.Password=dt.Rows[n]["Password"].ToString();
					if(dt.Rows[n]["CreateTime"].ToString()!="")
					{
						model.CreateTime=DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
					}
					model.CreateIP=dt.Rows[n]["CreateIP"].ToString();
					model.Guid=dt.Rows[n]["Guid"].ToString();
					modelList.Add(model);
				}
			}
			return modelList;
		}



		#endregion  成员方法



        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Sys.Model.Account model, string oldPwd)
        {
            return dal.Update(model, oldPwd);
        }

        /// <summary>
        /// 用户登录 -1失败 -2没有帐户 >0用户UserID
        /// </summary>
        /// <param name="Username">用户名</param>
        /// <param name="Password">密码MD5</param>
        /// <returns></returns>
        public int CheckLogin(string Username, string Password)
        {
            return dal.CheckLogin(Username, Password);
        }


        /// <summary>
        /// 用户登录 -1失败 -2没有帐户 ,-3企业号错误>0用户UserID
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码MD5</param>
        /// <param name="companyCode">企业号</param>
        /// <returns></returns>
        public int CheckLogin(string username, string password, string companyCode)
        {
            return dal.CheckLogin(username, password, companyCode);
        }


        /// <summary>
        /// 用户登录 -1失败 >0用户UserID
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="Password">密码MD5</param>
        /// <returns></returns>
        public int CheckLogin(int UserID, string Password)
        {
            return dal.CheckLogin(UserID, Password);
        }


       


        /// <summary>
        /// 获得Cookie帐户身份
        /// </summary>
        /// <param name="IsAdmin">是否是管理员身份验证</param>
        /// <returns></returns>
        private static int GetCookieUserID(bool IsAdmin)
        {
            #region Cookie认证 Cookie
            //Cookie信息
            if (Utils.GetCookie(ConfigHelper.GetConfigString("CookieName")) == "")
            {
                //Cookie账户不存在
                Cookie.ClearUserCookie();
                return -1;
            }

            //Cookie信息
            int CookieUid = Utils.StrToInt(Cookie.GetCookie(ConfigHelper.GetConfigString("CookieUserid")), -1);
            string CookiePWD = Cookie.GetCookiePassword(Cookie.GetCookie(ConfigHelper.GetConfigString("CookiePassword")));
            if (CookieUid == -1 || CookiePWD == "")
            {
                //Cookie账户数据错误
                Cookie.ClearUserCookie();
                return -1;
            }
            #endregion

            #region IP认证 IpBlock
            ////IP访问限制选项
            //int AdminIpBlockType = iPortal.BLL.SysConfig.GetInt("WebConfig", "AdminIpBlockType", 0);
            //if (AdminIpBlockType == 0)//无访问限制
            //{ }
            //else if (AdminIpBlockType == 1) //启用黑名单，禁止黑名单中的IP进行访问，其余允许访问
            //{
            //    SysIpBlock bllIpBlock = new SysIpBlock();
            //    if (bllIpBlock.Exists(0, 1, Utils.GetRealIP()))
            //        return -1;
            //}
            //else if (AdminIpBlockType == 2) //启用白名单，允许白名单中的IP进行访问，其余禁止访问
            //{
            //    SysIpBlock bllIpBlock = new SysIpBlock();
            //    if (!bllIpBlock.Exists(0, 0, Utils.GetRealIP()))
            //        return -1;
            //}
            #endregion

            #region 账户认证 Account
            //账户认证
            Account bllAccount = new Account();
            int ret = bllAccount.CheckLogin(CookieUid, CookiePWD);
            if (ret < 1)
            {
                //账户登陆失败
                Cookie.ClearUserCookie();
                return -1;
            }
            #endregion

            #region 用户认证 Member
            //用户认证，错误检验
            //Member bllMember = new Member();
            //Model.Member modelMember = bllMember.GetModel(ret);
            //if (modelMember == null)
            //{
            //    //不是用户
            //    Cookie.ClearUserCookie();
            //    return -1;
            //}
            //else if (modelMember.State == 0)
            //{
            //    //用户锁定
            //    Cookie.ClearUserCookie();
            //    return -1;
            //}
            #endregion

            //用户登陆，成功返回
            if (!IsAdmin)
                return ret;

            #region 管理员认证 Admin
            //管理员认证，错误检验
            Admin bllAdmin = new Admin();
            Model.Admin modelAdmin = bllAdmin.GetModel(ret);
            if (modelAdmin == null)
            {
                //不是管理员
                Cookie.ClearUserCookie();
                return -1;
            }
            else if (modelAdmin.State == 0)
            {
                //管理员锁定
                Cookie.ClearUserCookie();
                return -1;
            }
            #endregion

            //管理员认证，成功返回
            return ret;
        }


        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int AccountUpdate(int userId, string password)
        {

            return dal.AccountUpdate(userId, password);

        }


        /// <summary>
        /// 获得登陆帐户身份（管理员）
        /// </summary>
        /// <returns></returns>
        public static int GetLoginAdminID()
        {
            var userid = -1;

            //Session身份
            if (System.Web.HttpContext.Current.Session["adminid"] == null)
            {
                userid = GetCookieUserID(true);
                if (userid > 0)
                    System.Web.HttpContext.Current.Session["adminid"] = userid.ToString();
            }
            else
            {
                userid = Utils.StrToInt(System.Web.HttpContext.Current.Session["adminid"].ToString(), -1);
                if (userid <= 0)
                    System.Web.HttpContext.Current.Session.Remove("adminid");
            }
            //Session维持
            /*
            if (userid > 0)
                System.Web.HttpContext.Current.Session["adminid"] = userid.ToString();
            else
                System.Web.HttpContext.Current.Session.Remove("adminid");
            */
            return userid;
        }




        /// <summary>
        /// 获得登陆帐户身份
        /// </summary>
        /// <returns></returns>
        public static int GetLoginUserID()
        {
            int userid = -1;

            //Session身份
            if (System.Web.HttpContext.Current.Session["userid"] == null)
            {
                userid = GetCookieUserID(false);
                if (userid > 0)
                    System.Web.HttpContext.Current.Session["userid"] = userid.ToString();
            }
            else
            {
                userid = Utils.StrToInt(System.Web.HttpContext.Current.Session["userid"].ToString(), -1);
                if (userid <= 0)
                    System.Web.HttpContext.Current.Session.Remove("userid");
            }

            //Session维持
            /*if (userid > 0)
                System.Web.HttpContext.Current.Session["userid"] = userid.ToString();
            else
                System.Web.HttpContext.Current.Session.Remove("userid");
            */
            return userid;
        }
	}
}

