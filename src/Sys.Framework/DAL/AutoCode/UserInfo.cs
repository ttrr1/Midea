using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;

namespace Sys.DAL
{
	/// <summary>
	/// 数据访问类:UserInfo
	/// </summary>
	public partial class UserInfo
	{
		public UserInfo()
		{}
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys.Model.UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserInfo(");
            strSql.Append("UserId,UserCode,UserName,RealName,CreateTime,Sex,Address,Introduction,SMSMobile,Mobile,SId,User_RId,MotionSend,CId,GId,EnrollmentTime,Logo,CompanyName,ProvinceId,ProvinceName,CityId,CityName,AreaId,AreaName,sAreaId,sAreaName,contact,TypeKey,TypeValue,RoleId)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@UserCode,@UserName,@RealName,@CreateTime,@Sex,@Address,@Introduction,@SMSMobile,@Mobile,@SId,@User_RId,@MotionSend,@CId,@GId,@EnrollmentTime,@Logo,@CompanyName,@ProvinceId,@ProvinceName,@CityId,@CityName,@AreaId,@AreaName,@sAreaId,@sAreaName,@contact,@TypeKey,@TypeValue,@RoleId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@RealName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Sex", SqlDbType.Int,4),
					new SqlParameter("@Address", SqlDbType.NVarChar,150),
					new SqlParameter("@Introduction", SqlDbType.Text),
					new SqlParameter("@SMSMobile", SqlDbType.VarChar,50),
					new SqlParameter("@Mobile", SqlDbType.VarChar,50),
					new SqlParameter("@SId", SqlDbType.Int,4),
					new SqlParameter("@User_RId", SqlDbType.Int,4),
					new SqlParameter("@MotionSend", SqlDbType.Int,4),
					new SqlParameter("@CId", SqlDbType.Int,4),
					new SqlParameter("@GId", SqlDbType.Int,4),
					new SqlParameter("@EnrollmentTime", SqlDbType.DateTime),
					new SqlParameter("@Logo", SqlDbType.VarChar,150),
					new SqlParameter("@CompanyName", SqlDbType.NVarChar,100),
					new SqlParameter("@ProvinceId", SqlDbType.Int,4),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar,50),
					new SqlParameter("@CityId", SqlDbType.Int,4),
					new SqlParameter("@CityName", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaId", SqlDbType.Int,4),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@sAreaId", SqlDbType.Int,4),
					new SqlParameter("@sAreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@contact", SqlDbType.VarChar,50),
					new SqlParameter("@TypeKey", SqlDbType.Char,2),
					new SqlParameter("@TypeValue", SqlDbType.NVarChar,50),
					new SqlParameter("@RoleId", SqlDbType.Int,4)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.RealName;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.Sex;
            parameters[6].Value = model.Address;
            parameters[7].Value = model.Introduction;
            parameters[8].Value = model.SMSMobile;
            parameters[9].Value = model.Mobile;
            parameters[10].Value = model.SId;
            parameters[11].Value = model.User_RId;
            parameters[12].Value = model.MotionSend;
            parameters[13].Value = model.CId;
            parameters[14].Value = model.GId;
            parameters[15].Value = model.EnrollmentTime;
            parameters[16].Value = model.Logo;
            parameters[17].Value = model.CompanyName;
            parameters[18].Value = model.ProvinceId;
            parameters[19].Value = model.ProvinceName;
            parameters[20].Value = model.CityId;
            parameters[21].Value = model.CityName;
            parameters[22].Value = model.AreaId;
            parameters[23].Value = model.AreaName;
            parameters[24].Value = model.sAreaId;
            parameters[25].Value = model.sAreaName;
            parameters[26].Value = model.contact;
            parameters[27].Value = model.TypeKey;
            parameters[28].Value = model.TypeValue;
            parameters[29].Value = model.RoleId;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Sys.Model.UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserInfo set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("RealName=@RealName,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("Address=@Address,");
            strSql.Append("Introduction=@Introduction,");
            strSql.Append("SMSMobile=@SMSMobile,");
            strSql.Append("Mobile=@Mobile,");
            strSql.Append("SId=@SId,");
            strSql.Append("User_RId=@User_RId,");
            strSql.Append("MotionSend=@MotionSend,");
            strSql.Append("CId=@CId,");
            strSql.Append("GId=@GId,");
            strSql.Append("EnrollmentTime=@EnrollmentTime,");
            strSql.Append("Logo=@Logo,");
            strSql.Append("CompanyName=@CompanyName,");
            strSql.Append("ProvinceId=@ProvinceId,");
            strSql.Append("ProvinceName=@ProvinceName,");
            strSql.Append("CityId=@CityId,");
            strSql.Append("CityName=@CityName,");
            strSql.Append("AreaId=@AreaId,");
            strSql.Append("AreaName=@AreaName,");
            strSql.Append("sAreaId=@sAreaId,");
            strSql.Append("sAreaName=@sAreaName,");
            strSql.Append("contact=@contact,");
            strSql.Append("TypeKey=@TypeKey,");
            strSql.Append("TypeValue=@TypeValue,");
            strSql.Append("RoleId=@RoleId");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@RealName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Sex", SqlDbType.Int,4),
					new SqlParameter("@Address", SqlDbType.NVarChar,150),
					new SqlParameter("@Introduction", SqlDbType.Text),
					new SqlParameter("@SMSMobile", SqlDbType.VarChar,50),
					new SqlParameter("@Mobile", SqlDbType.VarChar,50),
					new SqlParameter("@SId", SqlDbType.Int,4),
					new SqlParameter("@User_RId", SqlDbType.Int,4),
					new SqlParameter("@MotionSend", SqlDbType.Int,4),
					new SqlParameter("@CId", SqlDbType.Int,4),
					new SqlParameter("@GId", SqlDbType.Int,4),
					new SqlParameter("@EnrollmentTime", SqlDbType.DateTime),
					new SqlParameter("@Logo", SqlDbType.VarChar,150),
					new SqlParameter("@CompanyName", SqlDbType.NVarChar,100),
					new SqlParameter("@ProvinceId", SqlDbType.Int,4),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar,50),
					new SqlParameter("@CityId", SqlDbType.Int,4),
					new SqlParameter("@CityName", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaId", SqlDbType.Int,4),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@sAreaId", SqlDbType.Int,4),
					new SqlParameter("@sAreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@contact", SqlDbType.VarChar,50),
					new SqlParameter("@TypeKey", SqlDbType.Char,2),
					new SqlParameter("@TypeValue", SqlDbType.NVarChar,50),
					new SqlParameter("@RoleId", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.RealName;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.Sex;
            parameters[6].Value = model.Address;
            parameters[7].Value = model.Introduction;
            parameters[8].Value = model.SMSMobile;
            parameters[9].Value = model.Mobile;
            parameters[10].Value = model.SId;
            parameters[11].Value = model.User_RId;
            parameters[12].Value = model.MotionSend;
            parameters[13].Value = model.CId;
            parameters[14].Value = model.GId;
            parameters[15].Value = model.EnrollmentTime;
            parameters[16].Value = model.Logo;
            parameters[17].Value = model.CompanyName;
            parameters[18].Value = model.ProvinceId;
            parameters[19].Value = model.ProvinceName;
            parameters[20].Value = model.CityId;
            parameters[21].Value = model.CityName;
            parameters[22].Value = model.AreaId;
            parameters[23].Value = model.AreaName;
            parameters[24].Value = model.sAreaId;
            parameters[25].Value = model.sAreaName;
            parameters[26].Value = model.contact;
            parameters[27].Value = model.TypeKey;
            parameters[28].Value = model.TypeValue;
            parameters[29].Value = model.RoleId;
            parameters[30].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserInfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserInfo ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.UserInfo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,UserId,UserCode,UserName,RealName,CreateTime,Sex,Address,Introduction,SMSMobile,Mobile,SId,User_RId,MotionSend,CId,GId,EnrollmentTime,Logo,CompanyName,ProvinceId,ProvinceName,CityId,CityName,AreaId,AreaName,sAreaId,sAreaName,contact,TypeKey,TypeValue,RoleId from UserInfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Sys.Model.UserInfo model = new Sys.Model.UserInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserId"] != null && ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserCode"] != null && ds.Tables[0].Rows[0]["UserCode"].ToString() != "")
                {
                    model.UserCode = ds.Tables[0].Rows[0]["UserCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RealName"] != null && ds.Tables[0].Rows[0]["RealName"].ToString() != "")
                {
                    model.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sex"] != null && ds.Tables[0].Rows[0]["Sex"].ToString() != "")
                {
                    model.Sex = int.Parse(ds.Tables[0].Rows[0]["Sex"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Address"] != null && ds.Tables[0].Rows[0]["Address"].ToString() != "")
                {
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Introduction"] != null && ds.Tables[0].Rows[0]["Introduction"].ToString() != "")
                {
                    model.Introduction = ds.Tables[0].Rows[0]["Introduction"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SMSMobile"] != null && ds.Tables[0].Rows[0]["SMSMobile"].ToString() != "")
                {
                    model.SMSMobile = ds.Tables[0].Rows[0]["SMSMobile"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Mobile"] != null && ds.Tables[0].Rows[0]["Mobile"].ToString() != "")
                {
                    model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SId"] != null && ds.Tables[0].Rows[0]["SId"].ToString() != "")
                {
                    model.SId = int.Parse(ds.Tables[0].Rows[0]["SId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["User_RId"] != null && ds.Tables[0].Rows[0]["User_RId"].ToString() != "")
                {
                    model.User_RId = int.Parse(ds.Tables[0].Rows[0]["User_RId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MotionSend"] != null && ds.Tables[0].Rows[0]["MotionSend"].ToString() != "")
                {
                    model.MotionSend = int.Parse(ds.Tables[0].Rows[0]["MotionSend"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CId"] != null && ds.Tables[0].Rows[0]["CId"].ToString() != "")
                {
                    model.CId = int.Parse(ds.Tables[0].Rows[0]["CId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GId"] != null && ds.Tables[0].Rows[0]["GId"].ToString() != "")
                {
                    model.GId = int.Parse(ds.Tables[0].Rows[0]["GId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EnrollmentTime"] != null && ds.Tables[0].Rows[0]["EnrollmentTime"].ToString() != "")
                {
                    model.EnrollmentTime = DateTime.Parse(ds.Tables[0].Rows[0]["EnrollmentTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Logo"] != null && ds.Tables[0].Rows[0]["Logo"].ToString() != "")
                {
                    model.Logo = ds.Tables[0].Rows[0]["Logo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CompanyName"] != null && ds.Tables[0].Rows[0]["CompanyName"].ToString() != "")
                {
                    model.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ProvinceId"] != null && ds.Tables[0].Rows[0]["ProvinceId"].ToString() != "")
                {
                    model.ProvinceId = int.Parse(ds.Tables[0].Rows[0]["ProvinceId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProvinceName"] != null && ds.Tables[0].Rows[0]["ProvinceName"].ToString() != "")
                {
                    model.ProvinceName = ds.Tables[0].Rows[0]["ProvinceName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CityId"] != null && ds.Tables[0].Rows[0]["CityId"].ToString() != "")
                {
                    model.CityId = int.Parse(ds.Tables[0].Rows[0]["CityId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CityName"] != null && ds.Tables[0].Rows[0]["CityName"].ToString() != "")
                {
                    model.CityName = ds.Tables[0].Rows[0]["CityName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AreaId"] != null && ds.Tables[0].Rows[0]["AreaId"].ToString() != "")
                {
                    model.AreaId = int.Parse(ds.Tables[0].Rows[0]["AreaId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AreaName"] != null && ds.Tables[0].Rows[0]["AreaName"].ToString() != "")
                {
                    model.AreaName = ds.Tables[0].Rows[0]["AreaName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sAreaId"] != null && ds.Tables[0].Rows[0]["sAreaId"].ToString() != "")
                {
                    model.sAreaId = int.Parse(ds.Tables[0].Rows[0]["sAreaId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sAreaName"] != null && ds.Tables[0].Rows[0]["sAreaName"].ToString() != "")
                {
                    model.sAreaName = ds.Tables[0].Rows[0]["sAreaName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["contact"] != null && ds.Tables[0].Rows[0]["contact"].ToString() != "")
                {
                    model.contact = ds.Tables[0].Rows[0]["contact"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TypeKey"] != null && ds.Tables[0].Rows[0]["TypeKey"].ToString() != "")
                {
                    model.TypeKey = ds.Tables[0].Rows[0]["TypeKey"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TypeValue"] != null && ds.Tables[0].Rows[0]["TypeValue"].ToString() != "")
                {
                    model.TypeValue = ds.Tables[0].Rows[0]["TypeValue"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RoleId"] != null && ds.Tables[0].Rows[0]["RoleId"].ToString() != "")
                {
                    model.RoleId = int.Parse(ds.Tables[0].Rows[0]["RoleId"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,UserId,UserCode,UserName,RealName,CreateTime,Sex,Address,Introduction,SMSMobile,Mobile,SId,User_RId,MotionSend,CId,GId,EnrollmentTime,Logo,CompanyName,ProvinceId,ProvinceName,CityId,CityName,AreaId,AreaName,sAreaId,sAreaName,contact,TypeKey,TypeValue,RoleId ");
            strSql.Append(" FROM UserInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,UserId,UserCode,UserName,RealName,CreateTime,Sex,Address,Introduction,SMSMobile,Mobile,SId,User_RId,MotionSend,CId,GId,EnrollmentTime,Logo,CompanyName,ProvinceId,ProvinceName,CityId,CityName,AreaId,AreaName,sAreaId,sAreaName,contact,TypeKey,TypeValue,RoleId ");
            strSql.Append(" FROM UserInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM UserInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from UserInfo T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "UserInfo";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
	}
}

