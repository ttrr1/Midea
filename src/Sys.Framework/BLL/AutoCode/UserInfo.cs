using System;
using System.Data;
using System.Collections.Generic;
using Sys.Common;
using Sys.Model;
namespace Sys.BLL
{
	/// <summary>
	/// 用户信息表
	/// </summary>
	public partial class UserInfo
	{
		private readonly Sys.DAL.UserInfo dal=new Sys.DAL.UserInfo();
		public UserInfo()
		{}
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys.Model.UserInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Sys.Model.UserInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.UserInfo GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Sys.Model.UserInfo GetModelByCache(int ID)
        {

            string CacheKey = "UserInfoModel-" + ID;
            object objModel = Sys.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = Sys.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Sys.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Sys.Model.UserInfo)objModel;
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
        public List<Sys.Model.UserInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.UserInfo> DataTableToList(DataTable dt)
        {
            List<Sys.Model.UserInfo> modelList = new List<Sys.Model.UserInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Sys.Model.UserInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Sys.Model.UserInfo();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["UserId"] != null && dt.Rows[n]["UserId"].ToString() != "")
                    {
                        model.UserId = int.Parse(dt.Rows[n]["UserId"].ToString());
                    }
                    if (dt.Rows[n]["UserCode"] != null && dt.Rows[n]["UserCode"].ToString() != "")
                    {
                        model.UserCode = dt.Rows[n]["UserCode"].ToString();
                    }
                    if (dt.Rows[n]["UserName"] != null && dt.Rows[n]["UserName"].ToString() != "")
                    {
                        model.UserName = dt.Rows[n]["UserName"].ToString();
                    }
                    if (dt.Rows[n]["RealName"] != null && dt.Rows[n]["RealName"].ToString() != "")
                    {
                        model.RealName = dt.Rows[n]["RealName"].ToString();
                    }
                    if (dt.Rows[n]["CreateTime"] != null && dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }
                    if (dt.Rows[n]["Sex"] != null && dt.Rows[n]["Sex"].ToString() != "")
                    {
                        model.Sex = int.Parse(dt.Rows[n]["Sex"].ToString());
                    }
                    if (dt.Rows[n]["Address"] != null && dt.Rows[n]["Address"].ToString() != "")
                    {
                        model.Address = dt.Rows[n]["Address"].ToString();
                    }
                    if (dt.Rows[n]["Introduction"] != null && dt.Rows[n]["Introduction"].ToString() != "")
                    {
                        model.Introduction = dt.Rows[n]["Introduction"].ToString();
                    }
                    if (dt.Rows[n]["SMSMobile"] != null && dt.Rows[n]["SMSMobile"].ToString() != "")
                    {
                        model.SMSMobile = dt.Rows[n]["SMSMobile"].ToString();
                    }
                    if (dt.Rows[n]["Mobile"] != null && dt.Rows[n]["Mobile"].ToString() != "")
                    {
                        model.Mobile = dt.Rows[n]["Mobile"].ToString();
                    }
                    if (dt.Rows[n]["SId"] != null && dt.Rows[n]["SId"].ToString() != "")
                    {
                        model.SId = int.Parse(dt.Rows[n]["SId"].ToString());
                    }
                    if (dt.Rows[n]["User_RId"] != null && dt.Rows[n]["User_RId"].ToString() != "")
                    {
                        model.User_RId = int.Parse(dt.Rows[n]["User_RId"].ToString());
                    }
                    if (dt.Rows[n]["MotionSend"] != null && dt.Rows[n]["MotionSend"].ToString() != "")
                    {
                        model.MotionSend = int.Parse(dt.Rows[n]["MotionSend"].ToString());
                    }
                    if (dt.Rows[n]["CId"] != null && dt.Rows[n]["CId"].ToString() != "")
                    {
                        model.CId = int.Parse(dt.Rows[n]["CId"].ToString());
                    }
                    if (dt.Rows[n]["GId"] != null && dt.Rows[n]["GId"].ToString() != "")
                    {
                        model.GId = int.Parse(dt.Rows[n]["GId"].ToString());
                    }
                    if (dt.Rows[n]["EnrollmentTime"] != null && dt.Rows[n]["EnrollmentTime"].ToString() != "")
                    {
                        model.EnrollmentTime = DateTime.Parse(dt.Rows[n]["EnrollmentTime"].ToString());
                    }
                    if (dt.Rows[n]["Logo"] != null && dt.Rows[n]["Logo"].ToString() != "")
                    {
                        model.Logo = dt.Rows[n]["Logo"].ToString();
                    }
                    if (dt.Rows[n]["CompanyName"] != null && dt.Rows[n]["CompanyName"].ToString() != "")
                    {
                        model.CompanyName = dt.Rows[n]["CompanyName"].ToString();
                    }
                    if (dt.Rows[n]["ProvinceId"] != null && dt.Rows[n]["ProvinceId"].ToString() != "")
                    {
                        model.ProvinceId = int.Parse(dt.Rows[n]["ProvinceId"].ToString());
                    }
                    if (dt.Rows[n]["ProvinceName"] != null && dt.Rows[n]["ProvinceName"].ToString() != "")
                    {
                        model.ProvinceName = dt.Rows[n]["ProvinceName"].ToString();
                    }
                    if (dt.Rows[n]["CityId"] != null && dt.Rows[n]["CityId"].ToString() != "")
                    {
                        model.CityId = int.Parse(dt.Rows[n]["CityId"].ToString());
                    }
                    if (dt.Rows[n]["CityName"] != null && dt.Rows[n]["CityName"].ToString() != "")
                    {
                        model.CityName = dt.Rows[n]["CityName"].ToString();
                    }
                    if (dt.Rows[n]["AreaId"] != null && dt.Rows[n]["AreaId"].ToString() != "")
                    {
                        model.AreaId = int.Parse(dt.Rows[n]["AreaId"].ToString());
                    }
                    if (dt.Rows[n]["AreaName"] != null && dt.Rows[n]["AreaName"].ToString() != "")
                    {
                        model.AreaName = dt.Rows[n]["AreaName"].ToString();
                    }
                    if (dt.Rows[n]["sAreaId"] != null && dt.Rows[n]["sAreaId"].ToString() != "")
                    {
                        model.sAreaId = int.Parse(dt.Rows[n]["sAreaId"].ToString());
                    }
                    if (dt.Rows[n]["sAreaName"] != null && dt.Rows[n]["sAreaName"].ToString() != "")
                    {
                        model.sAreaName = dt.Rows[n]["sAreaName"].ToString();
                    }
                    if (dt.Rows[n]["contact"] != null && dt.Rows[n]["contact"].ToString() != "")
                    {
                        model.contact = dt.Rows[n]["contact"].ToString();
                    }
                    if (dt.Rows[n]["TypeKey"] != null && dt.Rows[n]["TypeKey"].ToString() != "")
                    {
                        model.TypeKey = dt.Rows[n]["TypeKey"].ToString();
                    }
                    if (dt.Rows[n]["TypeValue"] != null && dt.Rows[n]["TypeValue"].ToString() != "")
                    {
                        model.TypeValue = dt.Rows[n]["TypeValue"].ToString();
                    }
                    if (dt.Rows[n]["RoleId"] != null && dt.Rows[n]["RoleId"].ToString() != "")
                    {
                        model.RoleId = int.Parse(dt.Rows[n]["RoleId"].ToString());
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

        /// <summary>
        /// 返回一个数据表  List的前身
        /// </summary>
        public DataTable GetTable(int pageSize, int pageIndex, string strWhere, string strOrder)
        {
            var db = new DAL.Common();
            var ds = db.GetList("UserInfo", pageSize, pageIndex, strWhere, strOrder);
            return ds.Tables[0];
        }
	}
}

